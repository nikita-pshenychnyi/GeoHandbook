namespace GeoHandbookPro.Forms
{
    using System;
    using System.Data;
    using System.Linq;
    using System.Windows.Forms;
    using GeoHandbookPro.Models;
    using GeoHandbookPro.Services;
    using System.Collections.Generic;

    public partial class RegionsForm : Form
    {
        private readonly DatabaseManager _dbManager;
        private Region _selectedRegion = null;
        private bool _isLoadingData = false;

        public RegionsForm(DatabaseManager dbManager)
        {
            InitializeComponent();
            _dbManager = dbManager ?? throw new ArgumentNullException(nameof(dbManager));

            LoadCountriesToComboBox();
            if (cmbRegionCountry.Items.Count > 0 && cmbRegionCountry.SelectedIndex != -1)
            {
                LoadRegionsBySelectedCountry();
            }
            else
            {
                LoadAllRegions();
            }
            ClearFormAndSetButtonStates();
        }

        public RegionsForm()
        {
            InitializeComponent();
            MessageBox.Show("Увага: RegionsForm була створена без DatabaseManager. Функціональність може бути обмежена.", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                cmbRegionCountry.DataSource = countryListWithAll;
                cmbRegionCountry.DisplayMember = "Name";
                cmbRegionCountry.ValueMember = "CountryID";
                cmbRegionCountry.SelectedValue = 0L;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка завантаження країн для списку: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isLoadingData = false;
            }
        }

        private void cmbRegionCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoadingData) return;
            LoadRegionsBySelectedCountry();
            ClearFormAndSetButtonStates();
        }

        private void LoadRegionsBySelectedCountry()
        {
            if (_dbManager == null) return;
            if (cmbRegionCountry.SelectedValue is long selectedCountryId)
            {
                if (selectedCountryId == 0)
                {
                    LoadAllRegions();
                }
                else
                {
                    LoadRegionsForCountry(selectedCountryId);
                }
            }
            else
            {
                LoadAllRegions();
            }
        }

        private void LoadAllRegions()
        {
            if (_dbManager == null) return;
            _isLoadingData = true;
            try
            {
                var regions = _dbManager.GetAllRegions();
                dgvRegions.DataSource = null;
                dgvRegions.DataSource = regions;
                FormatDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка завантаження всіх регіонів: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isLoadingData = false;
            }
        }

        private void LoadRegionsForCountry(long countryId)
        {
            if (_dbManager == null) return;
            _isLoadingData = true;
            try
            {
                var regions = _dbManager.GetRegionsByCountryId(countryId);
                dgvRegions.DataSource = null;
                dgvRegions.DataSource = regions;
                FormatDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка завантаження регіонів для країни ID {countryId}: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isLoadingData = false;
            }
        }

        private void FormatDataGridView()
        {
            if (dgvRegions.Columns.Contains("RegionID"))
                dgvRegions.Columns["RegionID"].HeaderText = "ID";
            if (dgvRegions.Columns.Contains("Name"))
            {
                dgvRegions.Columns["Name"].HeaderText = "Назва регіону";
                dgvRegions.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (dgvRegions.Columns.Contains("Type"))
                dgvRegions.Columns["Type"].HeaderText = "Тип";
            if (dgvRegions.Columns.Contains("Population"))
            {
                dgvRegions.Columns["Population"].HeaderText = "Населення";
                dgvRegions.Columns["Population"].DefaultCellStyle.Format = "N0";
            }
            if (dgvRegions.Columns.Contains("CapitalCityName"))
                dgvRegions.Columns["CapitalCityName"].HeaderText = "Столиця";
            if (dgvRegions.Columns.Contains("CountryID"))
                dgvRegions.Columns["CountryID"].Visible = false;
        }

        private void ClearFormFields()
        {
            txtRegionId.Clear();
            txtRegionName.Clear();
            txtRegionType.Clear();
            numRegionPopulation.Value = 0;
            txtRegionCapitalName.Clear();
            // cmbRegionCountry не очищаємо, він для фільтрації
            txtRegionName.Focus();
        }

        private void ClearFormAndSetButtonStates()
        {
            ClearFormFields();
            _selectedRegion = null;
            btnAddRegion.Enabled = true;
            if (this.Controls.ContainsKey("btnEditRegion"))
                this.Controls["btnEditRegion"].Enabled = false;
            btnDeleteRegion.Enabled = false;
        }

        private void dgvRegions_SelectionChanged(object sender, EventArgs e)
        {
            if (_isLoadingData) return;

            if (dgvRegions.CurrentRow != null && dgvRegions.CurrentRow.DataBoundItem is Region selected)
            {
                _selectedRegion = selected;
                txtRegionId.Text = _selectedRegion.RegionID.ToString();
                txtRegionName.Text = _selectedRegion.Name;
                txtRegionType.Text = _selectedRegion.Type;
                numRegionPopulation.Value = _selectedRegion.Population ?? 0;
                txtRegionCapitalName.Text = _selectedRegion.CapitalCityName;

                _isLoadingData = true;
                cmbRegionCountry.SelectedValue = _selectedRegion.CountryID;
                _isLoadingData = false;

                btnAddRegion.Enabled = false;
                if (this.Controls.ContainsKey("btnEditRegion"))
                    this.Controls["btnEditRegion"].Enabled = true;
                btnDeleteRegion.Enabled = true;
            }
            else
            {
                ClearFormAndSetButtonStates();
            }
        }

        private void btnAddRegion_Click(object sender, EventArgs e)
        {
            if (_dbManager == null) return;

            if (cmbRegionCountry.SelectedValue == null || (cmbRegionCountry.SelectedValue is long currentCountryId && currentCountryId == 0))
            {
                MessageBox.Show("Будь ласка, спочатку оберіть конкретну країну зі списку для додавання нового регіону.", "Додавання регіону", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbRegionCountry.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtRegionName.Text))
            {
                MessageBox.Show("Назва регіону не може бути порожньою.", "Валідація", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRegionName.Focus();
                return;
            }

            var newRegion = new Region
            {
                Name = txtRegionName.Text.Trim(),
                Type = txtRegionType.Text.Trim(),
                Population = (long)numRegionPopulation.Value == 0 ? (long?)null : (long)numRegionPopulation.Value,
                CapitalCityName = txtRegionCapitalName.Text.Trim(),
                CountryID = (long)cmbRegionCountry.SelectedValue
            };

            if (_dbManager.AddRegion(newRegion))
            {
                MessageBox.Show("Регіон успішно додано.", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadRegionsBySelectedCountry();
                ClearFormAndSetButtonStates();
            }
        }

        private void btnEditRegion_Click(object sender, EventArgs e)
        {
            if (_dbManager == null || _selectedRegion == null)
            {
                MessageBox.Show("Будь ласка, спочатку виберіть регіон зі списку для редагування.", "Редагування", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cmbRegionCountry.SelectedValue == null || (cmbRegionCountry.SelectedValue is long currentCountryId && currentCountryId == 0))
            {
                MessageBox.Show("Будь ласка, оберіть конкретну країну для регіону.", "Валідація", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbRegionCountry.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtRegionName.Text))
            {
                MessageBox.Show("Назва регіону не може бути порожньою.", "Валідація", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRegionName.Focus();
                return;
            }

            _selectedRegion.Name = txtRegionName.Text.Trim();
            _selectedRegion.Type = txtRegionType.Text.Trim();
            _selectedRegion.Population = (long)numRegionPopulation.Value == 0 ? (long?)null : (long)numRegionPopulation.Value;
            _selectedRegion.CapitalCityName = txtRegionCapitalName.Text.Trim();
            _selectedRegion.CountryID = (long)cmbRegionCountry.SelectedValue;

            if (_dbManager.UpdateRegion(_selectedRegion))
            {
                MessageBox.Show("Регіон успішно оновлено.", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadRegionsBySelectedCountry();
                ClearFormAndSetButtonStates();
            }
        }

        private void btnDeleteRegion_Click(object sender, EventArgs e)
        {
            if (_dbManager == null || _selectedRegion == null) return;

            var confirmResult = MessageBox.Show($"Ви впевнені, що хочете видалити регіон '{_selectedRegion.Name}'? Це може вплинути на пов'язані міста.", "Підтвердження видалення", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.Yes)
            {
                if (_dbManager.DeleteRegion(_selectedRegion.RegionID))
                {
                    MessageBox.Show("Регіон успішно видалено.", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadRegionsBySelectedCountry();
                    ClearFormAndSetButtonStates();
                }
            }
        }

        private void btnCancelChangesRegion_Click(object sender, EventArgs e)
        {
            ClearFormAndSetButtonStates();
            dgvRegions.ClearSelection();
            if (dgvRegions.CurrentCell != null)
                dgvRegions.CurrentCell = null;
        }

        private void btnCloseRegions_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
