namespace GeoHandbookPro.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using GeoHandbookPro.Models;
    using GeoHandbookPro.Services;
    using GMap.NET;
    using GMap.NET.WindowsForms;
    using GMap.NET.WindowsForms.Markers;

    public partial class MainForm : Form
    {
        private readonly DatabaseManager _dbManager;
        private readonly MapManager _mapManager;

        public MainForm()
        {
            InitializeComponent();

            _dbManager = new DatabaseManager();

            if (this.gmapControlMain == null)
            {
                MessageBox.Show("Критична помилка: GMapControl 'gmapControlMain' не ініціалізовано.", "Помилка ініціалізації", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Load += (s, e) => this.Close();
                return;
            }

            _mapManager = new MapManager(this.gmapControlMain);

            SetupSearchCriteria();
            LoadInitialData();

            // 👇 додано: обробка кліку по карті
            this.gmapControlMain.MouseClick += GmapControlMain_MouseClick;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (this.gmapControlMain != null)
            {
                this.gmapControlMain.OnMarkerClick += GmapControlMain_OnMarkerClick;
                this.gmapControlMain.MouseMove += GmapControlMain_MouseMove;
            }

            if (toolStripStatusMessages != null)
                toolStripStatusMessages.Text = "Готово";
            if (toolStripMapCoordinates != null)
                toolStripMapCoordinates.Text = "Координати: -, -";
        }

        private void SetupSearchCriteria()
        {
            cmbSearchCriteria.Items.Clear();
            cmbSearchCriteria.Items.Add("Назва міста");
            cmbSearchCriteria.Items.Add("Назва країни");
            cmbSearchCriteria.Items.Add("Назва регіону");
            if (cmbSearchCriteria.Items.Count > 0)
                cmbSearchCriteria.SelectedIndex = 0;
        }

        private void LoadInitialData()
        {
            if (dgvSearchResults != null) dgvSearchResults.DataSource = null;
            if (_mapManager != null)
                _mapManager.CenterMapOnPoint(48.3794, 31.1656, 6); // Центр України
        }

        #region Menu Event Handlers

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) => Application.Exit();

        private void manageContinentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { using (var form = new ContinentsForm(_dbManager)) form.ShowDialog(this); }
            catch (Exception ex) { MessageBox.Show($"Помилка відкриття материків: {ex.Message}"); }
        }

        private void manageCountriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { using (var form = new CountriesForm(_dbManager)) form.ShowDialog(this); }
            catch (Exception ex) { MessageBox.Show($"Помилка відкриття країн: {ex.Message}"); }
        }

        private void manageRegionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { using (var form = new RegionsForm(_dbManager)) form.ShowDialog(this); }
            catch (Exception ex) { MessageBox.Show($"Помилка відкриття регіонів: {ex.Message}"); }
        }

        private void manageCitiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { using (var form = new CitiesForm(_dbManager)) form.ShowDialog(this); }
            catch (Exception ex) { MessageBox.Show($"Помилка відкриття міст: {ex.Message}"); }
        }

        private void continentPopulationReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { using (var form = new ContinentPopulationReportForm(_dbManager)) form.ShowDialog(this); }
            catch (Exception ex) { MessageBox.Show($"Помилка відкриття звіту: {ex.Message}"); }
        }

        private void mapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mapManager?.ClearAllOverlays();
            dgvSearchResults.DataSource = null;
            toolStripStatusMessages.Text = "Карту та результати очищено.";
        }

        private void zoomToUkraineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mapManager?.CenterMapOnPoint(48.3794, 31.1656, 6);
            toolStripStatusMessages.Text = "Карту центровано на Україні.";
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { using (var form = new AboutForm()) form.ShowDialog(this); }
            catch (Exception ex) { MessageBox.Show($"Помилка 'Про програму': {ex.Message}"); }
        }

        #endregion

        #region Search Event Handlers

        private void btnSearch_Click(object sender, EventArgs e) => PerformSearch();

        private void PerformSearch()
        {
            string searchTerm = txtSearchQuery.Text.Trim();
            string criteria = cmbSearchCriteria.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(criteria)) { MessageBox.Show("Оберіть критерій пошуку."); return; }

            try
            {
                List<City> foundCities = new List<City>();
                if (criteria == "Назва міста") foundCities = _dbManager.SearchCities(searchTerm, criteria);
                else if (criteria == "Назва країни")
                {
                    var countries = string.IsNullOrWhiteSpace(searchTerm)
                        ? _dbManager.GetAllCountries()
                        : _dbManager.GetAllCountries().Where(c => c.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
                    foreach (var c in countries)
                        foundCities.AddRange(_dbManager.GetCitiesByCountryId(c.CountryID));
                    foundCities = foundCities.GroupBy(c => c.CityID).Select(g => g.First()).ToList();
                }
                else if (criteria == "Назва регіону")
                {
                    var regions = string.IsNullOrWhiteSpace(searchTerm)
                        ? _dbManager.GetAllRegions()
                        : _dbManager.GetAllRegions().Where(r => r.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
                    foreach (var r in regions)
                        foundCities.AddRange(_dbManager.GetCitiesByRegionId(r.RegionID));
                    foundCities = foundCities.GroupBy(c => c.CityID).Select(g => g.First()).ToList();
                }

                dgvSearchResults.DataSource = null;
                dgvSearchResults.DataSource = foundCities;
                FormatDgvSearchResults();
                _mapManager?.PlotCities(foundCities);

                toolStripStatusMessages.Text = foundCities.Any()
                    ? $"Знайдено міст: {foundCities.Count}"
                    : "Міст не знайдено.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка пошуку: {ex.Message}");
                toolStripStatusMessages.Text = "Помилка пошуку.";
            }
        }

        private void FormatDgvSearchResults()
        {
            if (dgvSearchResults == null || dgvSearchResults.DataSource == null) return;
            var columnsToShow = new Dictionary<string, string>
            {
                { "Name", "Назва міста" },
                { "Latitude", "Широта" },
                { "Longitude", "Довгота" },
                { "Population", "Населення" }
            };

            foreach (DataGridViewColumn col in dgvSearchResults.Columns)
            {
                if (columnsToShow.ContainsKey(col.Name))
                {
                    col.HeaderText = columnsToShow[col.Name];
                    col.Visible = true;
                    if (col.Name == "Name") col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    if (col.Name == "Latitude" || col.Name == "Longitude") col.DefaultCellStyle.Format = "F4";
                    if (col.Name == "Population") col.DefaultCellStyle.Format = "N0";
                }
                else col.Visible = false;
            }
        }

        private void btnClearSearchResults_Click(object sender, EventArgs e)
        {
            txtSearchQuery.Clear();
            cmbSearchCriteria.SelectedIndex = 0;
            dgvSearchResults.DataSource = null;
            _mapManager?.ClearMarkers();
            toolStripStatusMessages.Text = "Пошук та карта очищені.";
        }

        #endregion

        #region DataGridView and Map Interaction

        private void dgvSearchResults_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSearchResults.CurrentRow?.DataBoundItem is City city)
            {
                _mapManager?.CenterMapOnPoint(city.Latitude, city.Longitude, 12);
                toolStripStatusMessages.Text = $"Вибрано місто: {city.Name}";
            }
        }

        private void GmapControlMain_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            if (item.Tag is long cityId)
            {
                foreach (DataGridViewRow row in dgvSearchResults.Rows)
                {
                    if (row.DataBoundItem is City city && city.CityID == cityId)
                    {
                        dgvSearchResults.ClearSelection();
                        row.Selected = true;
                        dgvSearchResults.CurrentCell = row.Cells.OfType<DataGridViewCell>().FirstOrDefault(c => c.Visible);
                        toolStripStatusMessages.Text = $"Маркер натиснуто: {city.Name}";
                        break;
                    }
                }
            }
            else
            {
                toolStripStatusMessages.Text = $"Маркер натиснуто: {item.ToolTipText}";
            }
        }

        private void GmapControlMain_MouseMove(object sender, MouseEventArgs e)
        {
            PointLatLng point = gmapControlMain.FromLocalToLatLng(e.X, e.Y);
            toolStripMapCoordinates.Text = $"Координати: {point.Lat:F4}°, {point.Lng:F4}°";
        }

        private void GmapControlMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PointLatLng point = gmapControlMain.FromLocalToLatLng(e.X, e.Y);

                // Ставимо маркер
                GMapOverlay overlay = new GMapOverlay("clickMarker");
                overlay.Markers.Add(new GMarkerGoogle(point, GMarkerGoogleType.red));
                gmapControlMain.Overlays.Clear();
                gmapControlMain.Overlays.Add(overlay);

                // Передаємо координати у форму додавання міста
                foreach (Form f in Application.OpenForms)
                {
                    if (f is CitiesForm cf)
                    {
                        var latBox = cf.Controls.Find("txtLatitude", true).FirstOrDefault() as TextBox;
                        var lngBox = cf.Controls.Find("txtLongitude", true).FirstOrDefault() as TextBox;

                        if (latBox != null) latBox.Text = point.Lat.ToString("F6");
                        if (lngBox != null) lngBox.Text = point.Lng.ToString("F6");
                    }
                }

                toolStripStatusMessages.Text = $"Обрано точку: {point.Lat:F4}, {point.Lng:F4}";
            }
        }

        #endregion
    }
}
