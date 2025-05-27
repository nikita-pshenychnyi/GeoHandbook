namespace GeoHandbookPro.Forms
{
    partial class ContinentPopulationReportForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dgvContinentPopulationReport = new DataGridView();
            btnRefreshReportData = new Button();
            btnCloseReportForm = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvContinentPopulationReport).BeginInit();
            SuspendLayout();
            // 
            // dgvContinentPopulationReport
            // 
            dgvContinentPopulationReport.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvContinentPopulationReport.Location = new Point(12, 54);
            dgvContinentPopulationReport.Name = "dgvContinentPopulationReport";
            dgvContinentPopulationReport.Size = new Size(534, 444);
            dgvContinentPopulationReport.TabIndex = 0;
            // 
            // btnRefreshReportData
            // 
            btnRefreshReportData.Location = new Point(50, 8);
            btnRefreshReportData.Name = "btnRefreshReportData";
            btnRefreshReportData.Size = new Size(201, 36);
            btnRefreshReportData.TabIndex = 1;
            btnRefreshReportData.Text = "Оновити дані";
            btnRefreshReportData.UseVisualStyleBackColor = true;
            // 
            // btnCloseReportForm
            // 
            btnCloseReportForm.Location = new Point(284, 8);
            btnCloseReportForm.Name = "btnCloseReportForm";
            btnCloseReportForm.Size = new Size(201, 36);
            btnCloseReportForm.TabIndex = 2;
            btnCloseReportForm.Text = "Закрити";
            btnCloseReportForm.UseVisualStyleBackColor = true;
            // 
            // ContinentPopulationReportForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(558, 505);
            Controls.Add(btnCloseReportForm);
            Controls.Add(btnRefreshReportData);
            Controls.Add(dgvContinentPopulationReport);
            Name = "ContinentPopulationReportForm";
            Text = "Звіт: Населеність материків";
            ((System.ComponentModel.ISupportInitialize)dgvContinentPopulationReport).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvContinentPopulationReport;
        private Button btnRefreshReportData;
        private Button btnCloseReportForm;
    }
}