namespace GeoHandbookPro.Forms
{
    using System;
    using System.Data;
    using System.Linq;
    using System.Windows.Forms;
    using GeoHandbookPro.Models;
    using GeoHandbookPro.Services;
    using System.Collections.Generic;

    public partial class CitiesForm : Form
    {
        private readonly DatabaseManager _dbManager;
        private City _selectedCity = null;
        private bool _isLoadingData = false; // Для каскадних ComboBox

        public CitiesForm(DatabaseManager dbManager)
        {
            InitializeComponent();
            _dbManager = dbManager ?? throw new ArgumentNullException(nameof(dbManager));

            LoadCountriesToComboBox();
            cmbCityRegion.DataSource = null;

            // Завантаження міст на основі фільтрів (якщо є) або всіх міст
            if (cmbCityCountry.Items.Count > 0 && cmbCityCountry.SelectedIndex != -1 && (cmbCityCountry.SelectedValue is long cId && cId != 0))
            {
                // Перевіряємо, чи cmbCityRegion має джерело даних і вибраний елемент
                if (cmbCityRegion.DataSource != null && cmbCityRegion.Items.Count > 0 && cmbCityRegion.SelectedIndex != -1 && (cmbCityRegion.SelectedValue is long rId && rId != 0))
                {
                    LoadCitiesBySelectedRegion(rId); 
                }
                else
                {
                    LoadCitiesBySelectedCountry(cId); 
                }
            }
            else
            {
                LoadAllCities();
            }
            ClearFormAndSetButtonStates();
        }

        public CitiesForm()
        {
            InitializeComponent();
            MessageBox.Show("Увага: CitiesForm була створена без DatabaseManager. Функціональність може бути обмежена.", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void LoadCountriesToComboBox()
        {
            if (_dbManager == null) return;
            _isLoadingData = true;
            try
            {
                var countries = _dbManager.GetAllCountries();
                var countryListWithAll = new List<Country> { new Country { CountryID = 0, Name = "(Всі країни)" } };
                countryListWithAll.AddRange(countries);

                cmbCityCountry.DataSource = countryListWithAll;
                cmbCityCountry.DisplayMember = "Name";
                cmbCityCountry.ValueMember = "CountryID";
                cmbCityCountry.SelectedValue = 0L;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка завантаження країн для списку: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { _isLoadingData = false; }
        }

        private void cmbCityCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoadingData) return;
            LoadRegionsBySelectedCountryInComboBox();
            // Після зміни країни, фільтруємо міста
            LoadCitiesBySelectedFilters();
            ClearFormAndSetButtonStates();
        }

        private void LoadRegionsBySelectedCountryInComboBox() // Для заповнення cmbCityRegion
        {
            if (_dbManager == null) return;
            _isLoadingData = true;
            try
            {
                cmbCityRegion.DataSource = null;
                cmbCityRegion.Items.Clear();

                if (cmbCityCountry.SelectedValue is long countryId && countryId != 0)
                {
                    var regions = _dbManager.GetRegionsByCountryId(countryId);
                    var regionListWithAll = new List<Region> { new Region { RegionID = 0, Name = "(Всі регіони)" } };
                    regionListWithAll.AddRange(regions);

                    cmbCityRegion.DataSource = regionListWithAll;
                    cmbCityRegion.DisplayMember = "Name";
                    cmbCityRegion.ValueMember = "RegionID";
                    cmbCityRegion.SelectedValue = 0L;
                }
                else
                {
                    cmbCityRegion.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка завантаження регіонів для списку: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cmbCityRegion.Enabled = (cmbCityRegion.DataSource != null && cmbCityRegion.Items.Count > 0);
                _isLoadingData = false;
            }
        }

        private void cmbCityRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoadingData) return;
            LoadCitiesBySelectedFilters();
            ClearFormAndSetButtonStates();
        }

        private void LoadCitiesBySelectedFilters()
        {
            if (cmbCityCountry.SelectedValue is long countryId)
            {
                if (countryId == 0)
                {
                    LoadAllCities();
                }
                else
                {
                    // Перевіряємо, чи cmbCityRegion має джерело даних і вибраний елемент
                    if (cmbCityRegion.DataSource != null && cmbCityRegion.SelectedValue is long regionId && regionId != 0)
                    {
                        LoadCitiesBySelectedRegion(regionId);
                    }
                    else
                    {
                        LoadCitiesBySelectedCountry(countryId);
                    }
                }
            }
            else
            {
                LoadAllCities();
            }
        }


        private void LoadAllCities()
        {
            if (_dbManager == null) return;
            _isLoadingData = true;
            try
            {
                var cities = _dbManager.GetAllCities();
                dgvCities.DataSource = null;
                dgvCities.DataSource = cities;
                FormatDataGridView();
            }
            catch (Exception ex) { MessageBox.Show($"Помилка завантаження всіх міст: {ex.Message}"); }
            finally { _isLoadingData = false; }
        }
        private void LoadCitiesBySelectedCountry(long countryId)
        {
            if (_dbManager == null) return;
            _isLoadingData = true;
            try
            {
                var cities = _dbManager.GetCitiesByCountryId(countryId);
                dgvCities.DataSource = null;
                dgvCities.DataSource = cities;
                FormatDataGridView();
            }
            catch (Exception ex) { MessageBox.Show($"Помилка завантаження міст для країни ID {countryId}: {ex.Message}"); }
            finally { _isLoadingData = false; }
        }
        private void LoadCitiesBySelectedRegion(long regionId)
        {
            if (_dbManager == null) return;
            _isLoadingData = true;
            try
            {
                var cities = _dbManager.GetCitiesByRegionId(regionId);
                dgvCities.DataSource = null;
                dgvCities.DataSource = cities;
                FormatDataGridView();
            }
            catch (Exception ex) { MessageBox.Show($"Помилка завантаження міст для регіону ID {regionId}: {ex.Message}"); }
            finally { _isLoadingData = false; }
        }


        private void FormatDataGridView()
        {
            if (dgvCities.Columns.Contains("CityID")) dgvCities.Columns["CityID"].HeaderText = "ID";
            if (dgvCities.Columns.Contains("Name")) { dgvCities.Columns["Name"].HeaderText = "Назва міста"; dgvCities.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; }
            if (dgvCities.Columns.Contains("Latitude")) { dgvCities.Columns["Latitude"].HeaderText = "Широта"; dgvCities.Columns["Latitude"].DefaultCellStyle.Format = "F6"; }
            if (dgvCities.Columns.Contains("Longitude")) { dgvCities.Columns["Longitude"].HeaderText = "Довгота"; dgvCities.Columns["Longitude"].DefaultCellStyle.Format = "F6"; }
            if (dgvCities.Columns.Contains("Population")) { dgvCities.Columns["Population"].HeaderText = "Населення"; dgvCities.Columns["Population"].DefaultCellStyle.Format = "N0"; }
            if (dgvCities.Columns.Contains("RegionID")) dgvCities.Columns["RegionID"].Visible = false;
            if (dgvCities.Columns.Contains("CountryID")) dgvCities.Columns["CountryID"].Visible = false;
        }

        private void ClearFormFields()
        {
            txtCityId.Clear();
            txtCityName.Clear();
            numCityLatitude.Value = 0;
            numCityLongitude.Value = 0;
            numCityPopulation.Value = 0;
            if (!cmbCityRegion.Enabled)
            {
                cmbCityRegion.DataSource = null;
                cmbCityRegion.SelectedIndex = -1;
            }
            txtCityName.Focus();
        }

        private void ClearFormAndSetButtonStates()
        {
            ClearFormFields();
            _selectedCity = null;
            btnAddCity.Enabled = true;
            if (this.Controls.ContainsKey("btnEditCity"))
                this.Controls["btnEditCity"].Enabled = false;
            btnDeleteCity.Enabled = false;
        }


        private void dgvCities_SelectionChanged(object sender, EventArgs e)
        {
            if (_isLoadingData) return;

            if (dgvCities.CurrentRow != null && dgvCities.CurrentRow.DataBoundItem is City selected)
            {
                _selectedCity = selected;
                txtCityId.Text = _selectedCity.CityID.ToString();
                txtCityName.Text = _selectedCity.Name;
                numCityLatitude.Value = (decimal)_selectedCity.Latitude;
                numCityLongitude.Value = (decimal)_selectedCity.Longitude;
                numCityPopulation.Value = _selectedCity.Population ?? 0;

                _isLoadingData = true;
                cmbCityCountry.SelectedValue = _selectedCity.CountryID;
                this.BeginInvoke(new Action(() =>
                {
                    if (_selectedCity != null && _selectedCity.RegionID.HasValue && cmbCityRegion.DataSource != null)
                    {
                        cmbCityRegion.SelectedValue = _selectedCity.RegionID.Value;
                    }
                    else if (_selectedCity != null && cmbCityRegion.DataSource != null)
                    {
                        cmbCityRegion.SelectedValue = 0L;
                    }
                    _isLoadingData = false;
                }));


                btnAddCity.Enabled = false;
                if (this.Controls.ContainsKey("btnEditCity"))
                    this.Controls["btnEditCity"].Enabled = true;
                btnDeleteCity.Enabled = true;
            }
            else
            {
                ClearFormAndSetButtonStates();
            }
        }

        private void btnAddCity_Click(object sender, EventArgs e)
        {
            if (_dbManager == null) return;

            if (cmbCityCountry.SelectedValue == null || (cmbCityCountry.SelectedValue is long cId && cId == 0))
            {
                MessageBox.Show("Будь ласка, оберіть конкретну країну для додавання міста.", "Валідація", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCityCountry.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCityName.Text)) { MessageBox.Show("Назва міста не може бути порожньою."); txtCityName.Focus(); return; }
            if (numCityLatitude.Value < -90 || numCityLatitude.Value > 90) { MessageBox.Show("Широта: -90 до 90."); numCityLatitude.Focus(); return; }
            if (numCityLongitude.Value < -180 || numCityLongitude.Value > 180) { MessageBox.Show("Довгота: -180 до 180."); numCityLongitude.Focus(); return; }

            var newCity = new City
            {
                Name = txtCityName.Text.Trim(),
                Latitude = (double)numCityLatitude.Value,
                Longitude = (double)numCityLongitude.Value,
                Population = (long)numCityPopulation.Value == 0 ? (long?)null : (long)numCityPopulation.Value,
                CountryID = (long)cmbCityCountry.SelectedValue,
                RegionID = (cmbCityRegion.Enabled && cmbCityRegion.SelectedValue is long rId && rId != 0) ? rId : (long?)null
            };

            if (_dbManager.AddCity(newCity))
            {
                MessageBox.Show("Місто успішно додано.");
                LoadCitiesBySelectedFilters();
                ClearFormAndSetButtonStates();
            }
        }

        private void btnEditCity_Click(object sender, EventArgs e)
        {
            if (_dbManager == null || _selectedCity == null) { MessageBox.Show("Оберіть місто для редагування."); return; }

            if (cmbCityCountry.SelectedValue == null || (cmbCityCountry.SelectedValue is long cId && cId == 0))
            {
                MessageBox.Show("Будь ласка, оберіть конкретну країну для міста.", "Валідація", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCityCountry.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtCityName.Text)) { MessageBox.Show("Назва міста не може бути порожньою."); txtCityName.Focus(); return; }
            if (numCityLatitude.Value < -90 || numCityLatitude.Value > 90) { MessageBox.Show("Широта: -90 до 90."); numCityLatitude.Focus(); return; }
            if (numCityLongitude.Value < -180 || numCityLongitude.Value > 180) { MessageBox.Show("Довгота: -180 до 180."); numCityLongitude.Focus(); return; }

            _selectedCity.Name = txtCityName.Text.Trim();
            _selectedCity.Latitude = (double)numCityLatitude.Value;
            _selectedCity.Longitude = (double)numCityLongitude.Value;
            _selectedCity.Population = (long)numCityPopulation.Value == 0 ? (long?)null : (long)numCityPopulation.Value;
            _selectedCity.CountryID = (long)cmbCityCountry.SelectedValue;
            _selectedCity.RegionID = (cmbCityRegion.Enabled && cmbCityRegion.SelectedValue is long rId && rId != 0) ? rId : (long?)null;

            if (_dbManager.UpdateCity(_selectedCity))
            {
                MessageBox.Show("Місто успішно оновлено.");
                LoadCitiesBySelectedFilters();
                ClearFormAndSetButtonStates();
            }
        }

        private void btnDeleteCity_Click(object sender, EventArgs e)
        {
            if (_dbManager == null || _selectedCity == null) return;
            if (MessageBox.Show($"Видалити місто '{_selectedCity.Name}'?", "Підтвердження", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (_dbManager.DeleteCity(_selectedCity.CityID))
                {
                    MessageBox.Show("Місто видалено.");
                    LoadCitiesBySelectedFilters();
                    ClearFormAndSetButtonStates();
                }
            }
        }

        private void btnCancelChangesCity_Click(object sender, EventArgs e)
        {
            ClearFormAndSetButtonStates();
            dgvCities.ClearSelection();
            if (dgvCities.CurrentCell != null) dgvCities.CurrentCell = null;
        }

        private void btnCloseCities_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
