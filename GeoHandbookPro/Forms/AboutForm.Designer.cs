namespace GeoHandbookPro.Forms
{
    partial class AboutForm
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
            button1 = new Button();
            lblProgramTitle = new Label();
            lblVersionInfo = new Label();
            lblAuthorInfo = new Label();
            lblDescription = new Label();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(60, 150);
            button1.Name = "button1";
            button1.Size = new Size(142, 26);
            button1.TabIndex = 4;
            button1.Text = "ОК";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnOkAbout_Click;
            // 
            // lblProgramTitle
            // 
            lblProgramTitle.AutoSize = true;
            lblProgramTitle.Location = new Point(117, 25);
            lblProgramTitle.Name = "lblProgramTitle";
            lblProgramTitle.Size = new Size(38, 15);
            lblProgramTitle.TabIndex = 5;
            lblProgramTitle.Text = "label1";
            // 
            // lblVersionInfo
            // 
            lblVersionInfo.AutoSize = true;
            lblVersionInfo.Location = new Point(117, 58);
            lblVersionInfo.Name = "lblVersionInfo";
            lblVersionInfo.Size = new Size(38, 15);
            lblVersionInfo.TabIndex = 6;
            lblVersionInfo.Text = "label1";
            // 
            // lblAuthorInfo
            // 
            lblAuthorInfo.AutoSize = true;
            lblAuthorInfo.Location = new Point(12, 92);
            lblAuthorInfo.Name = "lblAuthorInfo";
            lblAuthorInfo.Size = new Size(38, 15);
            lblAuthorInfo.TabIndex = 7;
            lblAuthorInfo.Text = "label1";
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(12, 118);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(38, 15);
            lblDescription.TabIndex = 8;
            lblDescription.Text = "label1";
            // 
            // AboutForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(337, 198);
            Controls.Add(lblDescription);
            Controls.Add(lblAuthorInfo);
            Controls.Add(lblVersionInfo);
            Controls.Add(lblProgramTitle);
            Controls.Add(button1);
            Name = "AboutForm";
            Text = "Про програму";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button button1;
        private Label lblProgramTitle;
        private Label lblVersionInfo;
        private Label lblAuthorInfo;
        private Label lblDescription;
    }
}