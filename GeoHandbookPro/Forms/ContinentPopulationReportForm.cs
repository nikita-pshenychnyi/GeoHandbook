namespace GeoHandbookPro.Forms
{
    using System;
    using System.Windows.Forms;
    using GeoHandbookPro.Services;
    using System.Linq; // Для Any()
    using System.Collections.Generic; // Для List<>


    public partial class ContinentPopulationReportForm : Form
    {
        private readonly DatabaseManager _dbManager;

        public ContinentPopulationReportForm(DatabaseManager dbManager)
        {
            InitializeComponent();
            _dbManager = dbManager ?? throw new ArgumentNullException(nameof(dbManager));
            LoadReportData();
        }

        public ContinentPopulationReportForm()
        {
            InitializeComponent();
            MessageBox.Show("Увага: ContinentPopulationReportForm була створена без DatabaseManager. Звіт не буде завантажено.", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }


        private void LoadReportData()
        {
            if (_dbManager == null) return;
            try
            {
                var reportData = _dbManager.GetContinentPopulationDensityReport();
                // Переконайтеся, що у вас є DataGridView з іменем, наприклад, dgvContinentPopulationReport
                dgvContinentPopulationReport.DataSource = null;
                dgvContinentPopulationReport.DataSource = reportData;
                FormatDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка завантаження даних звіту: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDataGridView()
        {
            if (dgvContinentPopulationReport.Columns.Contains("ContinentName"))
            {
                dgvContinentPopulationReport.Columns["ContinentName"].HeaderText = "Материк";
                dgvContinentPopulationReport.Columns["ContinentName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (dgvContinentPopulationReport.Columns.Contains("TotalArea"))
            {
                dgvContinentPopulationReport.Columns["TotalArea"].HeaderText = "Загальна площа (кв.км)";
                dgvContinentPopulationReport.Columns["TotalArea"].DefaultCellStyle.Format = "N2";
            }
            if (dgvContinentPopulationReport.Columns.Contains("TotalPopulation"))
            {
                dgvContinentPopulationReport.Columns["TotalPopulation"].HeaderText = "Загальне населення";
                dgvContinentPopulationReport.Columns["TotalPopulation"].DefaultCellStyle.Format = "N0";
            }
            if (dgvContinentPopulationReport.Columns.Contains("Density"))
            {
                dgvContinentPopulationReport.Columns["Density"].HeaderText = "Густота (осіб/кв.км)";
                dgvContinentPopulationReport.Columns["Density"].DefaultCellStyle.Format = "F2";
            }
        }

        private void btnRefreshReportData_Click(object sender, EventArgs e)
        {
            LoadReportData();
        }

        private void btnCloseReportForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
