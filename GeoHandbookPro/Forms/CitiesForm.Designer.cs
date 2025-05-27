namespace GeoHandbookPro.Forms
{
    partial class CitiesForm
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
            dgvCities = new DataGridView();
            grpCityDetails = new GroupBox();
            btnCloseCities = new Button();
            btnCancelChangesCity = new Button();
            btnDeleteCity = new Button();
            btnEditCity = new Button();
            btnAddCity = new Button();
            cmbCityRegion = new ComboBox();
            cmbCityCountry = new ComboBox();
            numCityPopulation = new NumericUpDown();
            numCityLongitude = new NumericUpDown();
            numCityLatitude = new NumericUpDown();
            txtCityName = new TextBox();
            txtCityId = new TextBox();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvCities).BeginInit();
            grpCityDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numCityPopulation).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numCityLongitude).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numCityLatitude).BeginInit();
            SuspendLayout();
            // 
            // dgvCities
            // 
            dgvCities.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCities.Location = new Point(12, 12);
            dgvCities.Name = "dgvCities";
            dgvCities.Size = new Size(658, 411);
            dgvCities.TabIndex = 0;
            dgvCities.SelectionChanged += dgvCities_SelectionChanged;
            // 
            // grpCityDetails
            // 
            grpCityDetails.Controls.Add(btnCloseCities);
            grpCityDetails.Controls.Add(btnCancelChangesCity);
            grpCityDetails.Controls.Add(btnDeleteCity);
            grpCityDetails.Controls.Add(btnEditCity);
            grpCityDetails.Controls.Add(btnAddCity);
            grpCityDetails.Controls.Add(cmbCityRegion);
            grpCityDetails.Controls.Add(cmbCityCountry);
            grpCityDetails.Controls.Add(numCityPopulation);
            grpCityDetails.Controls.Add(numCityLongitude);
            grpCityDetails.Controls.Add(numCityLatitude);
            grpCityDetails.Controls.Add(txtCityName);
            grpCityDetails.Controls.Add(txtCityId);
            grpCityDetails.Controls.Add(label7);
            grpCityDetails.Controls.Add(label6);
            grpCityDetails.Controls.Add(label5);
            grpCityDetails.Controls.Add(label4);
            grpCityDetails.Controls.Add(label3);
            grpCityDetails.Controls.Add(label2);
            grpCityDetails.Controls.Add(label1);
            grpCityDetails.Location = new Point(676, 12);
            grpCityDetails.Name = "grpCityDetails";
            grpCityDetails.Size = new Size(310, 411);
            grpCityDetails.TabIndex = 1;
            grpCityDetails.TabStop = false;
            grpCityDetails.Text = "Деталі міста";
            // 
            // btnCloseCities
            // 
            btnCloseCities.Location = new Point(152, 370);
            btnCloseCities.Name = "btnCloseCities";
            btnCloseCities.Size = new Size(140, 36);
            btnCloseCities.TabIndex = 19;
            btnCloseCities.Text = "Закрити";
            btnCloseCities.UseVisualStyleBackColor = true;
            btnCloseCities.Click += btnCloseCities_Click;
            // 
            // btnCancelChangesCity
            // 
            btnCancelChangesCity.Location = new Point(6, 370);
            btnCancelChangesCity.Name = "btnCancelChangesCity";
            btnCancelChangesCity.Size = new Size(127, 36);
            btnCancelChangesCity.TabIndex = 18;
            btnCancelChangesCity.Text = "Скасувати";
            btnCancelChangesCity.UseVisualStyleBackColor = true;
            btnCancelChangesCity.Click += btnCancelChangesCity_Click;
            // 
            // btnDeleteCity
            // 
            btnDeleteCity.Location = new Point(6, 323);
            btnDeleteCity.Name = "btnDeleteCity";
            btnDeleteCity.Size = new Size(127, 36);
            btnDeleteCity.TabIndex = 16;
            btnDeleteCity.Text = "Видалити";
            btnDeleteCity.UseVisualStyleBackColor = true;
            btnDeleteCity.Click += btnDeleteCity_Click;
            // 
            // btnEditCity
            // 
            btnEditCity.Location = new Point(152, 323);
            btnEditCity.Name = "btnEditCity";
            btnEditCity.Size = new Size(140, 36);
            btnEditCity.TabIndex = 15;
            btnEditCity.Text = "Редагувати";
            btnEditCity.UseVisualStyleBackColor = true;
            btnEditCity.Click += btnEditCity_Click;
            // 
            // btnAddCity
            // 
            btnAddCity.Location = new Point(6, 272);
            btnAddCity.Name = "btnAddCity";
            btnAddCity.Size = new Size(286, 36);
            btnAddCity.TabIndex = 14;
            btnAddCity.Text = "Додати";
            btnAddCity.UseVisualStyleBackColor = true;
            btnAddCity.Click += btnAddCity_Click;
            // 
            // cmbCityRegion
            // 
            cmbCityRegion.FormattingEnabled = true;
            cmbCityRegion.Location = new Point(139, 230);
            cmbCityRegion.Name = "cmbCityRegion";
            cmbCityRegion.Size = new Size(150, 23);
            cmbCityRegion.TabIndex = 13;
            cmbCityRegion.SelectedIndexChanged += cmbCityRegion_SelectedIndexChanged;
            // 
            // cmbCityCountry
            // 
            cmbCityCountry.FormattingEnabled = true;
            cmbCityCountry.Location = new Point(82, 195);
            cmbCityCountry.Name = "cmbCityCountry";
            cmbCityCountry.Size = new Size(207, 23);
            cmbCityCountry.TabIndex = 12;
            cmbCityCountry.SelectedIndexChanged += cmbCityCountry_SelectedIndexChanged;
            // 
            // numCityPopulation
            // 
            numCityPopulation.Location = new Point(82, 160);
            numCityPopulation.Name = "numCityPopulation";
            numCityPopulation.Size = new Size(207, 23);
            numCityPopulation.TabIndex = 11;
            // 
            // numCityLongitude
            // 
            numCityLongitude.DecimalPlaces = 6;
            numCityLongitude.Location = new Point(82, 120);
            numCityLongitude.Maximum = new decimal(new int[] { 180, 0, 0, 0 });
            numCityLongitude.Minimum = new decimal(new int[] { 180, 0, 0, int.MinValue });
            numCityLongitude.Name = "numCityLongitude";
            numCityLongitude.Size = new Size(207, 23);
            numCityLongitude.TabIndex = 10;
            // 
            // numCityLatitude
            // 
            numCityLatitude.DecimalPlaces = 6;
            numCityLatitude.Location = new Point(82, 86);
            numCityLatitude.Maximum = new decimal(new int[] { 90, 0, 0, 0 });
            numCityLatitude.Minimum = new decimal(new int[] { 90, 0, 0, int.MinValue });
            numCityLatitude.Name = "numCityLatitude";
            numCityLatitude.Size = new Size(207, 23);
            numCityLatitude.TabIndex = 9;
            // 
            // txtCityName
            // 
            txtCityName.Location = new Point(82, 54);
            txtCityName.Name = "txtCityName";
            txtCityName.Size = new Size(210, 23);
            txtCityName.TabIndex = 8;
            // 
            // txtCityId
            // 
            txtCityId.Enabled = false;
            txtCityId.Location = new Point(82, 21);
            txtCityId.Name = "txtCityId";
            txtCityId.Size = new Size(210, 23);
            txtCityId.TabIndex = 7;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(6, 238);
            label7.Name = "label7";
            label7.Size = new Size(127, 15);
            label7.TabIndex = 6;
            label7.Text = "Регіон (опціонально):";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 203);
            label6.Name = "label6";
            label6.Size = new Size(43, 15);
            label6.TabIndex = 5;
            label6.Text = "Країна";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 168);
            label5.Name = "label5";
            label5.Size = new Size(70, 15);
            label5.TabIndex = 4;
            label5.Text = "Населення:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 128);
            label4.Name = "label4";
            label4.Size = new Size(54, 15);
            label4.TabIndex = 3;
            label4.Text = "Довгота:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 94);
            label3.Name = "label3";
            label3.Size = new Size(53, 15);
            label3.TabIndex = 2;
            label3.Text = "Широта:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 62);
            label2.Name = "label2";
            label2.Size = new Size(42, 15);
            label2.TabIndex = 1;
            label2.Text = "Назва:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 29);
            label1.Name = "label1";
            label1.Size = new Size(21, 15);
            label1.TabIndex = 0;
            label1.Text = "ID:";
            // 
            // CitiesForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(997, 435);
            Controls.Add(grpCityDetails);
            Controls.Add(dgvCities);
            Name = "CitiesForm";
            Text = "Форма управління містами";
            ((System.ComponentModel.ISupportInitialize)dgvCities).EndInit();
            grpCityDetails.ResumeLayout(false);
            grpCityDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numCityPopulation).EndInit();
            ((System.ComponentModel.ISupportInitialize)numCityLongitude).EndInit();
            ((System.ComponentModel.ISupportInitialize)numCityLatitude).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvCities;
        private GroupBox grpCityDetails;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private Button btnAddCity;
        private ComboBox cmbCityRegion;
        private ComboBox cmbCityCountry;
        private NumericUpDown numCityPopulation;
        private NumericUpDown numCityLongitude;
        private NumericUpDown numCityLatitude;
        private TextBox txtCityName;
        private TextBox txtCityId;
        private Button btnCloseCities;
        private Button btnCancelChangesCity;
        private Button btnDeleteCity;
        private Button btnEditCity;
    }
}