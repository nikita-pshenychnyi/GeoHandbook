namespace GeoHandbookPro.Forms
{
    partial class CountriesForm
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
            dgvCountries = new DataGridView();
            grpCountryDetails = new GroupBox();
            btnCloseCountries = new Button();
            btnCancelChangesCountry = new Button();
            btnDeleteCountry = new Button();
            btnEditCountry = new Button();
            btnAddCountry = new Button();
            cmbCountryContinent = new ComboBox();
            txtCountryCapitalName = new TextBox();
            txtCountryGovernmentForm = new TextBox();
            numCountryPopulation = new NumericUpDown();
            numCountryArea = new NumericUpDown();
            txtCountryName = new TextBox();
            txtCountryId = new TextBox();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvCountries).BeginInit();
            grpCountryDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numCountryPopulation).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numCountryArea).BeginInit();
            SuspendLayout();
            // 
            // dgvCountries
            // 
            dgvCountries.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCountries.Location = new Point(12, 12);
            dgvCountries.Name = "dgvCountries";
            dgvCountries.Size = new Size(711, 436);
            dgvCountries.TabIndex = 0;
            dgvCountries.SelectionChanged += dgvCountries_SelectionChanged;
            // 
            // grpCountryDetails
            // 
            grpCountryDetails.Controls.Add(btnCloseCountries);
            grpCountryDetails.Controls.Add(btnCancelChangesCountry);
            grpCountryDetails.Controls.Add(btnDeleteCountry);
            grpCountryDetails.Controls.Add(btnEditCountry);
            grpCountryDetails.Controls.Add(btnAddCountry);
            grpCountryDetails.Controls.Add(cmbCountryContinent);
            grpCountryDetails.Controls.Add(txtCountryCapitalName);
            grpCountryDetails.Controls.Add(txtCountryGovernmentForm);
            grpCountryDetails.Controls.Add(numCountryPopulation);
            grpCountryDetails.Controls.Add(numCountryArea);
            grpCountryDetails.Controls.Add(txtCountryName);
            grpCountryDetails.Controls.Add(txtCountryId);
            grpCountryDetails.Controls.Add(label7);
            grpCountryDetails.Controls.Add(label6);
            grpCountryDetails.Controls.Add(label5);
            grpCountryDetails.Controls.Add(label4);
            grpCountryDetails.Controls.Add(label3);
            grpCountryDetails.Controls.Add(label2);
            grpCountryDetails.Controls.Add(label1);
            grpCountryDetails.Location = new Point(738, 12);
            grpCountryDetails.Name = "grpCountryDetails";
            grpCountryDetails.Size = new Size(308, 436);
            grpCountryDetails.TabIndex = 1;
            grpCountryDetails.TabStop = false;
            grpCountryDetails.Text = "Деталі країни";
            // 
            // btnCloseCountries
            // 
            btnCloseCountries.Location = new Point(166, 385);
            btnCloseCountries.Name = "btnCloseCountries";
            btnCloseCountries.Size = new Size(135, 34);
            btnCloseCountries.TabIndex = 19;
            btnCloseCountries.Text = "Закрити";
            btnCloseCountries.UseVisualStyleBackColor = true;
            btnCloseCountries.Click += btnCloseCountries_Click;
            // 
            // btnCancelChangesCountry
            // 
            btnCancelChangesCountry.Location = new Point(15, 385);
            btnCancelChangesCountry.Name = "btnCancelChangesCountry";
            btnCancelChangesCountry.Size = new Size(135, 34);
            btnCancelChangesCountry.TabIndex = 18;
            btnCancelChangesCountry.Text = "Скасувати";
            btnCancelChangesCountry.UseVisualStyleBackColor = true;
            btnCancelChangesCountry.Click += btnCancelChangesCountry_Click;
            // 
            // btnDeleteCountry
            // 
            btnDeleteCountry.Location = new Point(15, 334);
            btnDeleteCountry.Name = "btnDeleteCountry";
            btnDeleteCountry.Size = new Size(135, 34);
            btnDeleteCountry.TabIndex = 16;
            btnDeleteCountry.Text = "Видалити";
            btnDeleteCountry.UseVisualStyleBackColor = true;
            btnDeleteCountry.Click += btnDeleteCountry_Click;
            // 
            // btnEditCountry
            // 
            btnEditCountry.Location = new Point(166, 334);
            btnEditCountry.Name = "btnEditCountry";
            btnEditCountry.Size = new Size(135, 34);
            btnEditCountry.TabIndex = 15;
            btnEditCountry.Text = "Редагувати";
            btnEditCountry.UseVisualStyleBackColor = true;
            btnEditCountry.Click += btnEditCountry_Click;
            // 
            // btnAddCountry
            // 
            btnAddCountry.Location = new Point(15, 285);
            btnAddCountry.Name = "btnAddCountry";
            btnAddCountry.Size = new Size(286, 34);
            btnAddCountry.TabIndex = 14;
            btnAddCountry.Text = "Додати";
            btnAddCountry.UseVisualStyleBackColor = true;
            btnAddCountry.Click += btnAddCountry_Click;
            // 
            // cmbCountryContinent
            // 
            cmbCountryContinent.FormattingEnabled = true;
            cmbCountryContinent.Location = new Point(142, 242);
            cmbCountryContinent.Name = "cmbCountryContinent";
            cmbCountryContinent.Size = new Size(149, 23);
            cmbCountryContinent.TabIndex = 13;
            // 
            // txtCountryCapitalName
            // 
            txtCountryCapitalName.Location = new Point(142, 207);
            txtCountryCapitalName.Name = "txtCountryCapitalName";
            txtCountryCapitalName.Size = new Size(149, 23);
            txtCountryCapitalName.TabIndex = 12;
            // 
            // txtCountryGovernmentForm
            // 
            txtCountryGovernmentForm.Location = new Point(142, 175);
            txtCountryGovernmentForm.Name = "txtCountryGovernmentForm";
            txtCountryGovernmentForm.Size = new Size(149, 23);
            txtCountryGovernmentForm.TabIndex = 11;
            // 
            // numCountryPopulation
            // 
            numCountryPopulation.Location = new Point(142, 139);
            numCountryPopulation.Maximum = new decimal(new int[] { 276447232, 23283, 0, 0 });
            numCountryPopulation.Name = "numCountryPopulation";
            numCountryPopulation.Size = new Size(149, 23);
            numCountryPopulation.TabIndex = 10;
            // 
            // numCountryArea
            // 
            numCountryArea.Location = new Point(142, 105);
            numCountryArea.Maximum = new decimal(new int[] { -1486618624, 232830643, 0, 0 });
            numCountryArea.Name = "numCountryArea";
            numCountryArea.Size = new Size(149, 23);
            numCountryArea.TabIndex = 9;
            // 
            // txtCountryName
            // 
            txtCountryName.Location = new Point(142, 68);
            txtCountryName.Name = "txtCountryName";
            txtCountryName.Size = new Size(149, 23);
            txtCountryName.TabIndex = 8;
            // 
            // txtCountryId
            // 
            txtCountryId.Enabled = false;
            txtCountryId.Location = new Point(142, 35);
            txtCountryId.Name = "txtCountryId";
            txtCountryId.Size = new Size(149, 23);
            txtCountryId.TabIndex = 7;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(15, 245);
            label7.Name = "label7";
            label7.Size = new Size(58, 15);
            label7.TabIndex = 6;
            label7.Text = "Материк:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(15, 210);
            label6.Name = "label6";
            label6.Size = new Size(57, 15);
            label6.TabIndex = 5;
            label6.Text = "Столиця:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(15, 178);
            label5.Name = "label5";
            label5.Size = new Size(107, 15);
            label5.TabIndex = 4;
            label5.Text = "Форма правління:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(15, 141);
            label4.Name = "label4";
            label4.Size = new Size(70, 15);
            label4.TabIndex = 3;
            label4.Text = "Населення:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(15, 107);
            label3.Name = "label3";
            label3.Size = new Size(86, 15);
            label3.TabIndex = 2;
            label3.Text = "Площа, кв.км:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 71);
            label2.Name = "label2";
            label2.Size = new Size(42, 15);
            label2.TabIndex = 1;
            label2.Text = "Назва:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 38);
            label1.Name = "label1";
            label1.Size = new Size(21, 15);
            label1.TabIndex = 0;
            label1.Text = "ID:";
            // 
            // CountriesForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1053, 457);
            Controls.Add(grpCountryDetails);
            Controls.Add(dgvCountries);
            Name = "CountriesForm";
            Text = "Форма управління країнами";
            ((System.ComponentModel.ISupportInitialize)dgvCountries).EndInit();
            grpCountryDetails.ResumeLayout(false);
            grpCountryDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numCountryPopulation).EndInit();
            ((System.ComponentModel.ISupportInitialize)numCountryArea).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvCountries;
        private GroupBox grpCountryDetails;
        private ComboBox cmbCountryContinent;
        private TextBox txtCountryCapitalName;
        private TextBox txtCountryGovernmentForm;
        private NumericUpDown numCountryPopulation;
        private NumericUpDown numCountryArea;
        private TextBox txtCountryName;
        private TextBox txtCountryId;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private Button btnCloseCountries;
        private Button btnCancelChangesCountry;
        private Button btnDeleteCountry;
        private Button btnEditCountry;
        private Button btnAddCountry;
    }
}