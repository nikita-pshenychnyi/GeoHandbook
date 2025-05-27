namespace GeoHandbookPro.Forms
{
    partial class RegionsForm
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
            dgvRegions = new DataGridView();
            grpRegionDetails = new GroupBox();
            btnCloseRegions = new Button();
            btnCancelChangesRegion = new Button();
            btnDeleteRegion = new Button();
            btnEditRegion = new Button();
            btnAddRegion = new Button();
            cmbRegionCountry = new ComboBox();
            txtRegionCapitalName = new TextBox();
            numRegionPopulation = new NumericUpDown();
            txtRegionType = new TextBox();
            txtRegionName = new TextBox();
            txtRegionId = new TextBox();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvRegions).BeginInit();
            grpRegionDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numRegionPopulation).BeginInit();
            SuspendLayout();
            // 
            // dgvRegions
            // 
            dgvRegions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRegions.Location = new Point(12, 12);
            dgvRegions.Name = "dgvRegions";
            dgvRegions.Size = new Size(545, 415);
            dgvRegions.TabIndex = 0;
            dgvRegions.SelectionChanged += dgvRegions_SelectionChanged;
            // 
            // grpRegionDetails
            // 
            grpRegionDetails.Controls.Add(btnCloseRegions);
            grpRegionDetails.Controls.Add(btnCancelChangesRegion);
            grpRegionDetails.Controls.Add(btnDeleteRegion);
            grpRegionDetails.Controls.Add(btnEditRegion);
            grpRegionDetails.Controls.Add(btnAddRegion);
            grpRegionDetails.Controls.Add(cmbRegionCountry);
            grpRegionDetails.Controls.Add(txtRegionCapitalName);
            grpRegionDetails.Controls.Add(numRegionPopulation);
            grpRegionDetails.Controls.Add(txtRegionType);
            grpRegionDetails.Controls.Add(txtRegionName);
            grpRegionDetails.Controls.Add(txtRegionId);
            grpRegionDetails.Controls.Add(label6);
            grpRegionDetails.Controls.Add(label5);
            grpRegionDetails.Controls.Add(label4);
            grpRegionDetails.Controls.Add(label3);
            grpRegionDetails.Controls.Add(label2);
            grpRegionDetails.Controls.Add(label1);
            grpRegionDetails.Location = new Point(563, 12);
            grpRegionDetails.Name = "grpRegionDetails";
            grpRegionDetails.Size = new Size(273, 415);
            grpRegionDetails.TabIndex = 1;
            grpRegionDetails.TabStop = false;
            grpRegionDetails.Text = "Деталі регіону";
            // 
            // btnCloseRegions
            // 
            btnCloseRegions.Location = new Point(142, 360);
            btnCloseRegions.Name = "btnCloseRegions";
            btnCloseRegions.Size = new Size(114, 32);
            btnCloseRegions.TabIndex = 17;
            btnCloseRegions.Text = "Закрити";
            btnCloseRegions.UseVisualStyleBackColor = true;
            btnCloseRegions.Click += btnCloseRegions_Click;
            // 
            // btnCancelChangesRegion
            // 
            btnCancelChangesRegion.Location = new Point(15, 360);
            btnCancelChangesRegion.Name = "btnCancelChangesRegion";
            btnCancelChangesRegion.Size = new Size(114, 32);
            btnCancelChangesRegion.TabIndex = 16;
            btnCancelChangesRegion.Text = "Скасувати";
            btnCancelChangesRegion.UseVisualStyleBackColor = true;
            btnCancelChangesRegion.Click += btnCancelChangesRegion_Click;
            // 
            // btnDeleteRegion
            // 
            btnDeleteRegion.Location = new Point(15, 309);
            btnDeleteRegion.Name = "btnDeleteRegion";
            btnDeleteRegion.Size = new Size(114, 32);
            btnDeleteRegion.TabIndex = 14;
            btnDeleteRegion.Text = "Видалити";
            btnDeleteRegion.UseVisualStyleBackColor = true;
            btnDeleteRegion.Click += btnDeleteRegion_Click;
            // 
            // btnEditRegion
            // 
            btnEditRegion.Location = new Point(142, 309);
            btnEditRegion.Name = "btnEditRegion";
            btnEditRegion.Size = new Size(114, 32);
            btnEditRegion.TabIndex = 13;
            btnEditRegion.Text = "Редагувати";
            btnEditRegion.UseVisualStyleBackColor = true;
            btnEditRegion.Click += btnEditRegion_Click;
            // 
            // btnAddRegion
            // 
            btnAddRegion.Location = new Point(15, 261);
            btnAddRegion.Name = "btnAddRegion";
            btnAddRegion.Size = new Size(241, 32);
            btnAddRegion.TabIndex = 12;
            btnAddRegion.Text = "Додати";
            btnAddRegion.UseVisualStyleBackColor = true;
            btnAddRegion.Click += btnAddRegion_Click;
            // 
            // cmbRegionCountry
            // 
            cmbRegionCountry.FormattingEnabled = true;
            cmbRegionCountry.Location = new Point(91, 217);
            cmbRegionCountry.Name = "cmbRegionCountry";
            cmbRegionCountry.Size = new Size(165, 23);
            cmbRegionCountry.TabIndex = 11;
            cmbRegionCountry.SelectedIndexChanged += cmbRegionCountry_SelectedIndexChanged;
            // 
            // txtRegionCapitalName
            // 
            txtRegionCapitalName.Location = new Point(91, 181);
            txtRegionCapitalName.Name = "txtRegionCapitalName";
            txtRegionCapitalName.Size = new Size(165, 23);
            txtRegionCapitalName.TabIndex = 10;
            // 
            // numRegionPopulation
            // 
            numRegionPopulation.Location = new Point(91, 143);
            numRegionPopulation.Name = "numRegionPopulation";
            numRegionPopulation.Size = new Size(165, 23);
            numRegionPopulation.TabIndex = 9;
            // 
            // txtRegionType
            // 
            txtRegionType.Location = new Point(91, 102);
            txtRegionType.Name = "txtRegionType";
            txtRegionType.Size = new Size(165, 23);
            txtRegionType.TabIndex = 8;
            // 
            // txtRegionName
            // 
            txtRegionName.Location = new Point(91, 65);
            txtRegionName.Name = "txtRegionName";
            txtRegionName.Size = new Size(165, 23);
            txtRegionName.TabIndex = 7;
            // 
            // txtRegionId
            // 
            txtRegionId.Enabled = false;
            txtRegionId.Location = new Point(91, 30);
            txtRegionId.Name = "txtRegionId";
            txtRegionId.Size = new Size(165, 23);
            txtRegionId.TabIndex = 6;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(15, 225);
            label6.Name = "label6";
            label6.Size = new Size(46, 15);
            label6.TabIndex = 5;
            label6.Text = "Країна:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(15, 184);
            label5.Name = "label5";
            label5.Size = new Size(57, 15);
            label5.TabIndex = 4;
            label5.Text = "Столиця:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(15, 145);
            label4.Name = "label4";
            label4.Size = new Size(70, 15);
            label4.TabIndex = 3;
            label4.Text = "Населення:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(15, 105);
            label3.Name = "label3";
            label3.Size = new Size(30, 15);
            label3.TabIndex = 2;
            label3.Text = "Тип:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 68);
            label2.Name = "label2";
            label2.Size = new Size(42, 15);
            label2.TabIndex = 1;
            label2.Text = "Назва:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 33);
            label1.Name = "label1";
            label1.Size = new Size(21, 15);
            label1.TabIndex = 0;
            label1.Text = "ID:";
            // 
            // RegionsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(851, 438);
            Controls.Add(grpRegionDetails);
            Controls.Add(dgvRegions);
            Name = "RegionsForm";
            Text = "Форма управління регіонами";
            ((System.ComponentModel.ISupportInitialize)dgvRegions).EndInit();
            grpRegionDetails.ResumeLayout(false);
            grpRegionDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numRegionPopulation).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvRegions;
        private GroupBox grpRegionDetails;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private ComboBox cmbRegionCountry;
        private TextBox txtRegionCapitalName;
        private NumericUpDown numRegionPopulation;
        private TextBox txtRegionType;
        private TextBox txtRegionName;
        private TextBox txtRegionId;
        private Button btnCloseRegions;
        private Button btnCancelChangesRegion;
        private Button btnDeleteRegion;
        private Button btnEditRegion;
        private Button btnAddRegion;
    }
}