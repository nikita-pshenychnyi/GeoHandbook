namespace GeoHandbookPro.Forms
{
    partial class ContinentsForm
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
            dgvContinents = new DataGridView();
            grpContinentDetails = new GroupBox();
            btnCloseContinents = new Button();
            btnCancelChangesContinent = new Button();
            btnDeleteContinent = new Button();
            btnEditContinent = new Button();
            btnAddContinent = new Button();
            txtContinentName = new TextBox();
            txtContinentId = new TextBox();
            label2 = new Label();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvContinents).BeginInit();
            grpContinentDetails.SuspendLayout();
            SuspendLayout();
            // 
            // dgvContinents
            // 
            dgvContinents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvContinents.Location = new Point(12, 9);
            dgvContinents.Name = "dgvContinents";
            dgvContinents.Size = new Size(388, 346);
            dgvContinents.TabIndex = 0;
            dgvContinents.SelectionChanged += dgvContinents_SelectionChanged;
            // 
            // grpContinentDetails
            // 
            grpContinentDetails.Controls.Add(btnCloseContinents);
            grpContinentDetails.Controls.Add(btnCancelChangesContinent);
            grpContinentDetails.Controls.Add(btnDeleteContinent);
            grpContinentDetails.Controls.Add(btnEditContinent);
            grpContinentDetails.Controls.Add(btnAddContinent);
            grpContinentDetails.Controls.Add(txtContinentName);
            grpContinentDetails.Controls.Add(txtContinentId);
            grpContinentDetails.Controls.Add(label2);
            grpContinentDetails.Controls.Add(label1);
            grpContinentDetails.Location = new Point(423, 12);
            grpContinentDetails.Name = "grpContinentDetails";
            grpContinentDetails.Size = new Size(238, 343);
            grpContinentDetails.TabIndex = 1;
            grpContinentDetails.TabStop = false;
            grpContinentDetails.Text = "Деталі материка";
            // 
            // btnCloseContinents
            // 
            btnCloseContinents.Location = new Point(6, 296);
            btnCloseContinents.Name = "btnCloseContinents";
            btnCloseContinents.Size = new Size(220, 33);
            btnCloseContinents.TabIndex = 9;
            btnCloseContinents.Text = "Закрити";
            btnCloseContinents.UseVisualStyleBackColor = true;
            btnCloseContinents.Click += btnCloseContinents_Click;
            // 
            // btnCancelChangesContinent
            // 
            btnCancelChangesContinent.Location = new Point(6, 253);
            btnCancelChangesContinent.Name = "btnCancelChangesContinent";
            btnCancelChangesContinent.Size = new Size(220, 33);
            btnCancelChangesContinent.TabIndex = 8;
            btnCancelChangesContinent.Text = "Скасувати";
            btnCancelChangesContinent.UseVisualStyleBackColor = true;
            btnCancelChangesContinent.Click += btnCancelChangesContinent_Click;
            // 
            // btnDeleteContinent
            // 
            btnDeleteContinent.Location = new Point(6, 208);
            btnDeleteContinent.Name = "btnDeleteContinent";
            btnDeleteContinent.Size = new Size(220, 33);
            btnDeleteContinent.TabIndex = 6;
            btnDeleteContinent.Text = "Видалити";
            btnDeleteContinent.UseVisualStyleBackColor = true;
            btnDeleteContinent.Click += btnDeleteContinent_Click;
            // 
            // btnEditContinent
            // 
            btnEditContinent.Location = new Point(6, 159);
            btnEditContinent.Name = "btnEditContinent";
            btnEditContinent.Size = new Size(220, 33);
            btnEditContinent.TabIndex = 5;
            btnEditContinent.Text = "Редагувати";
            btnEditContinent.UseVisualStyleBackColor = true;
            btnEditContinent.Click += btnEditContinent_Click;
            // 
            // btnAddContinent
            // 
            btnAddContinent.Location = new Point(6, 111);
            btnAddContinent.Name = "btnAddContinent";
            btnAddContinent.Size = new Size(220, 33);
            btnAddContinent.TabIndex = 4;
            btnAddContinent.Text = "Додати";
            btnAddContinent.UseVisualStyleBackColor = true;
            btnAddContinent.Click += btnAddContinent_Click;
            // 
            // txtContinentName
            // 
            txtContinentName.Location = new Point(59, 64);
            txtContinentName.Name = "txtContinentName";
            txtContinentName.Size = new Size(167, 23);
            txtContinentName.TabIndex = 3;
            // 
            // txtContinentId
            // 
            txtContinentId.Enabled = false;
            txtContinentId.Location = new Point(59, 26);
            txtContinentId.Name = "txtContinentId";
            txtContinentId.Size = new Size(167, 23);
            txtContinentId.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 68);
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
            // ContinentsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(675, 365);
            Controls.Add(grpContinentDetails);
            Controls.Add(dgvContinents);
            Name = "ContinentsForm";
            Text = "Форма управління материками";
            ((System.ComponentModel.ISupportInitialize)dgvContinents).EndInit();
            grpContinentDetails.ResumeLayout(false);
            grpContinentDetails.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvContinents;
        private GroupBox grpContinentDetails;
        private Button btnCloseContinents;
        private Button btnCancelChangesContinent;
        private Button btnDeleteContinent;
        private Button btnEditContinent;
        private Button btnAddContinent;
        private TextBox txtContinentName;
        private TextBox txtContinentId;
        private Label label2;
        private Label label1;
    }
}