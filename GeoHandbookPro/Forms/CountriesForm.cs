namespace GeoHandbookPro.Forms
{
    using System;
    using System.Data;
    using System.Linq;
    using System.Windows.Forms;
    using GeoHandbookPro.Models;
    using GeoHandbookPro.Services;
    using System.Collections.Generic;

    public partial class CountriesForm : Form
    {
        private readonly DatabaseManager _dbManager;
        private Country _selectedCountry = null;

        public CountriesForm(DatabaseManager dbManager)
        {
            InitializeComponent();
            _dbManager = dbManager ?? throw new ArgumentNullException(nameof(dbManager));

            LoadContinentsToComboBox();
            LoadAllCountries();

            ClearFormAndSetButtonStates();
        }

        public CountriesForm()
        {
            InitializeComponent();
            MessageBox.Show("Увага: CountriesForm була створена без DatabaseManager. Функціональність може бути обмежена.", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void LoadContinentsToComboBox()
        {
            if (_dbManager == null) return;
            try
            {
                var continents = _dbManager.GetAllContinents();
                cmbCountryContinent.DataSource = continents;
                cmbCountryContinent.DisplayMember = "Name";
                cmbCountryContinent.ValueMember = "ContinentID";

                if (continents.Any())
                {
                    cmbCountryContinent.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка завантаження материків для списку: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAllCountries()
        {
            if (_dbManager == null) return;
            try
            {
                var countries = _dbManager.GetAllCountries();
                dgvCountries.DataSource = null;
                dgvCountries.DataSource = countries;
                FormatDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка завантаження всіх країн: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDataGridView()
        {
            if (dgvCountries.Columns.Contains("CountryID"))
                dgvCountries.Columns["CountryID"].HeaderText = "ID";
            if (dgvCountries.Columns.Contains("Name"))
            {
                dgvCountries.Columns["Name"].HeaderText = "Назва країни";
                dgvCountries.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (dgvCountries.Columns.Contains("Area"))
            {
                dgvCountries.Columns["Area"].HeaderText = "Площа (кв.км)";
                dgvCountries.Columns["Area"].DefaultCellStyle.Format = "N2";
            }
            if (dgvCountries.Columns.Contains("Population"))
            {
                dgvCountries.Columns["Population"].HeaderText = "Населення";
                dgvCountries.Columns["Population"].DefaultCellStyle.Format = "N0";
            }
            if (dgvCountries.Columns.Contains("GovernmentForm"))
                dgvCountries.Columns["GovernmentForm"].HeaderText = "Форма правління";
            if (dgvCountries.Columns.Contains("CapitalCityName"))
                dgvCountries.Columns["CapitalCityName"].HeaderText = "Столиця";
            if (dgvCountries.Columns.Contains("ContinentID"))
                dgvCountries.Columns["ContinentID"].Visible = false;
        }

        private void ClearFormFields()
        {
            txtCountryId.Clear();
            txtCountryName.Clear();
            numCountryArea.Value = 0;
            numCountryPopulation.Value = 0;
            txtCountryGovernmentForm.Clear();
            txtCountryCapitalName.Clear();
            cmbCountryContinent.SelectedIndex = -1;
            txtCountryName.Focus();
        }

        private void ClearFormAndSetButtonStates()
        {
            ClearFormFields();
            _selectedCountry = null;
            btnAddCountry.Enabled = true;
            // Кнопка "Редагувати" (якщо вона є і називається btnEditCountry) буде неактивна
            // Переконайтеся, що у вас є кнопка btnEditCountry на формі, якщо ви використовуєте цю логіку
            if (this.Controls.Find("btnEditCountry", true).FirstOrDefault() is Button editButton)
            {
                editButton.Enabled = false;
            }
            btnDeleteCountry.Enabled = false;
            // Кнопка "Зберегти" (btnSaveCountry) не використовується в цій версії
        }

        private void dgvCountries_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCountries.CurrentRow != null && dgvCountries.CurrentRow.DataBoundItem is Country selected)
            {
                _selectedCountry = selected;
                txtCountryId.Text = _selectedCountry.CountryID.ToString();
                txtCountryName.Text = _selectedCountry.Name;
                numCountryArea.Value = (decimal)(_selectedCountry.Area ?? 0);
                numCountryPopulation.Value = _selectedCountry.Population ?? 0;
                txtCountryGovernmentForm.Text = _selectedCountry.GovernmentForm;
                txtCountryCapitalName.Text = _selectedCountry.CapitalCityName;
                cmbCountryContinent.SelectedValue = _selectedCountry.ContinentID;

                btnAddCountry.Enabled = false; // Не можна додавати, коли вибрано для редагування
                if (this.Controls.Find("btnEditCountry", true).FirstOrDefault() is Button editButton)
                {
                    editButton.Enabled = true; // Тепер можна редагувати
                }
                btnDeleteCountry.Enabled = true;
            }
            else
            {
                // Якщо виділення знято, повернути до стану "готовий до додавання"
                ClearFormAndSetButtonStates();
            }
        }

        private void btnAddCountry_Click(object sender, EventArgs e)
        {
            if (_dbManager == null) return;

            if (string.IsNullOrWhiteSpace(txtCountryName.Text))
            {
                MessageBox.Show("Назва країни не може бути порожньою.", "Валідація", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCountryName.Focus();
                return;
            }
            if (cmbCountryContinent.SelectedValue == null)
            {
                MessageBox.Show("Будь ласка, оберіть материк для країни.", "Валідація", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCountryContinent.Focus();
                return;
            }

            var newCountry = new Country
            {
                Name = txtCountryName.Text.Trim(),
                Area = (double)numCountryArea.Value == 0 ? (double?)null : (double)numCountryArea.Value,
                Population = (long)numCountryPopulation.Value == 0 ? (long?)null : (long)numCountryPopulation.Value,
                GovernmentForm = txtCountryGovernmentForm.Text.Trim(),
                CapitalCityName = txtCountryCapitalName.Text.Trim(),
                ContinentID = (long)cmbCountryContinent.SelectedValue
            };

            if (_dbManager.AddCountry(newCountry))
            {
                MessageBox.Show("Країну успішно додано.", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAllCountries();
                ClearFormAndSetButtonStates();
            }
        }

        // Тепер btnEditCountry_Click виконує збереження змін
        private void btnEditCountry_Click(object sender, EventArgs e)
        {
            if (_dbManager == null || _selectedCountry == null)
            {
                MessageBox.Show("Будь ласка, спочатку виберіть країну зі списку для редагування.", "Редагування", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCountryName.Text))
            {
                MessageBox.Show("Назва країни не може бути порожньою.", "Валідація", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCountryName.Focus();
                return;
            }
            if (cmbCountryContinent.SelectedValue == null)
            {
                MessageBox.Show("Будь ласка, оберіть материк для країни.", "Валідація", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCountryContinent.Focus();
                return;
            }

            _selectedCountry.Name = txtCountryName.Text.Trim();
            _selectedCountry.Area = (double)numCountryArea.Value == 0 ? (double?)null : (double)numCountryArea.Value;
            _selectedCountry.Population = (long)numCountryPopulation.Value == 0 ? (long?)null : (long)numCountryPopulation.Value;
            _selectedCountry.GovernmentForm = txtCountryGovernmentForm.Text.Trim();
            _selectedCountry.CapitalCityName = txtCountryCapitalName.Text.Trim();
            _selectedCountry.ContinentID = (long)cmbCountryContinent.SelectedValue;

            if (_dbManager.UpdateCountry(_selectedCountry))
            {
                MessageBox.Show("Країну успішно оновлено.", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAllCountries();
                ClearFormAndSetButtonStates();
            }
        }

        private void btnDeleteCountry_Click(object sender, EventArgs e)
        {
            if (_dbManager == null || _selectedCountry == null) return;

            var confirmResult = MessageBox.Show($"Ви впевнені, що хочете видалити країну '{_selectedCountry.Name}'? Це може вплинути на пов'язані регіони та міста.", "Підтвердження видалення", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.Yes)
            {
                if (_dbManager.DeleteCountry(_selectedCountry.CountryID))
                {
                    MessageBox.Show("Країну успішно видалено.", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAllCountries();
                    ClearFormAndSetButtonStates();
                }
            }
        }

        private void btnCancelChangesCountry_Click(object sender, EventArgs e)
        {
            ClearFormAndSetButtonStates();
            dgvCountries.ClearSelection();
            if (dgvCountries.CurrentCell != null)
                dgvCountries.CurrentCell = null;
        }

        private void btnCloseCountries_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
