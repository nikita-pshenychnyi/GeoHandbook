namespace GeoHandbookPro.Forms
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            gmapControlMain = new GMap.NET.WindowsForms.GMapControl();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            entitiesToolStripMenuItem = new ToolStripMenuItem();
            manageCountriesToolStripMenuItem = new ToolStripMenuItem();
            manageRegionsToolStripMenuItem = new ToolStripMenuItem();
            manageCitiesToolStripMenuItem = new ToolStripMenuItem();
            manageContinentsToolStripMenuItem = new ToolStripMenuItem();
            reportsToolStripMenuItem = new ToolStripMenuItem();
            continentPopulationReportToolStripMenuItem = new ToolStripMenuItem();
            картаToolStripMenuItem = new ToolStripMenuItem();
            mapToolStripMenuItem = new ToolStripMenuItem();
            zoomToUkraineToolStripMenuItem = new ToolStripMenuItem();
            довідкаToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            groupBox1 = new GroupBox();
            btnClearSearchResults = new Button();
            btnSearch = new Button();
            cmbSearchCriteria = new ComboBox();
            txtSearchQuery = new TextBox();
            dgvSearchResults = new DataGridView();
            statusStrip1 = new StatusStrip();
            toolStripStatusMessages = new ToolStripStatusLabel();
            toolStripMapCoordinates = new ToolStripStatusLabel();
            menuStrip1.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSearchResults).BeginInit();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // gmapControlMain
            // 
            gmapControlMain.Bearing = 0F;
            gmapControlMain.CanDragMap = true;
            gmapControlMain.EmptyTileColor = Color.Navy;
            gmapControlMain.GrayScaleMode = false;
            gmapControlMain.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            gmapControlMain.LevelsKeepInMemory = 5;
            gmapControlMain.Location = new Point(462, 103);
            gmapControlMain.MarkersEnabled = true;
            gmapControlMain.MaxZoom = 2;
            gmapControlMain.MinZoom = 2;
            gmapControlMain.MouseWheelZoomEnabled = true;
            gmapControlMain.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            gmapControlMain.Name = "gmapControlMain";
            gmapControlMain.NegativeMode = false;
            gmapControlMain.PolygonsEnabled = true;
            gmapControlMain.RetryLoadTile = 0;
            gmapControlMain.RoutesEnabled = true;
            gmapControlMain.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            gmapControlMain.SelectedAreaFillColor = Color.FromArgb(33, 65, 105, 225);
            gmapControlMain.ShowTileGridLines = false;
            gmapControlMain.Size = new Size(666, 538);
            gmapControlMain.TabIndex = 0;
            gmapControlMain.Zoom = 0D;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, entitiesToolStripMenuItem, reportsToolStripMenuItem, картаToolStripMenuItem, довідкаToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1142, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(48, 20);
            fileToolStripMenuItem.Text = "Файл";
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(103, 22);
            exitToolStripMenuItem.Text = "Вихід";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // entitiesToolStripMenuItem
            // 
            entitiesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { manageCountriesToolStripMenuItem, manageRegionsToolStripMenuItem, manageCitiesToolStripMenuItem, manageContinentsToolStripMenuItem });
            entitiesToolStripMenuItem.Name = "entitiesToolStripMenuItem";
            entitiesToolStripMenuItem.Size = new Size(76, 20);
            entitiesToolStripMenuItem.Text = "Довідники";
            // 
            // manageCountriesToolStripMenuItem
            // 
            manageCountriesToolStripMenuItem.Name = "manageCountriesToolStripMenuItem";
            manageCountriesToolStripMenuItem.Size = new Size(129, 22);
            manageCountriesToolStripMenuItem.Text = "Країни";
            manageCountriesToolStripMenuItem.Click += manageCountriesToolStripMenuItem_Click;
            // 
            // manageRegionsToolStripMenuItem
            // 
            manageRegionsToolStripMenuItem.Name = "manageRegionsToolStripMenuItem";
            manageRegionsToolStripMenuItem.Size = new Size(129, 22);
            manageRegionsToolStripMenuItem.Text = "Регіони";
            manageRegionsToolStripMenuItem.Click += manageRegionsToolStripMenuItem_Click;
            // 
            // manageCitiesToolStripMenuItem
            // 
            manageCitiesToolStripMenuItem.Name = "manageCitiesToolStripMenuItem";
            manageCitiesToolStripMenuItem.Size = new Size(129, 22);
            manageCitiesToolStripMenuItem.Text = "Міста";
            manageCitiesToolStripMenuItem.Click += manageCitiesToolStripMenuItem_Click;
            // 
            // manageContinentsToolStripMenuItem
            // 
            manageContinentsToolStripMenuItem.Name = "manageContinentsToolStripMenuItem";
            manageContinentsToolStripMenuItem.Size = new Size(129, 22);
            manageContinentsToolStripMenuItem.Text = "Материки";
            manageContinentsToolStripMenuItem.Click += manageContinentsToolStripMenuItem_Click;
            // 
            // reportsToolStripMenuItem
            // 
            reportsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { continentPopulationReportToolStripMenuItem });
            reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            reportsToolStripMenuItem.Size = new Size(47, 20);
            reportsToolStripMenuItem.Text = "Звіти";
            // 
            // continentPopulationReportToolStripMenuItem
            // 
            continentPopulationReportToolStripMenuItem.Name = "continentPopulationReportToolStripMenuItem";
            continentPopulationReportToolStripMenuItem.Size = new Size(199, 22);
            continentPopulationReportToolStripMenuItem.Text = "Населеність материків";
            continentPopulationReportToolStripMenuItem.Click += continentPopulationReportToolStripMenuItem_Click;
            // 
            // картаToolStripMenuItem
            // 
            картаToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { mapToolStripMenuItem, zoomToUkraineToolStripMenuItem });
            картаToolStripMenuItem.Name = "картаToolStripMenuItem";
            картаToolStripMenuItem.Size = new Size(50, 20);
            картаToolStripMenuItem.Text = "Карта";
            // 
            // mapToolStripMenuItem
            // 
            mapToolStripMenuItem.Name = "mapToolStripMenuItem";
            mapToolStripMenuItem.Size = new Size(196, 22);
            mapToolStripMenuItem.Text = "Очистити маркери";
            mapToolStripMenuItem.Click += mapToolStripMenuItem_Click;
            // 
            // zoomToUkraineToolStripMenuItem
            // 
            zoomToUkraineToolStripMenuItem.Name = "zoomToUkraineToolStripMenuItem";
            zoomToUkraineToolStripMenuItem.Size = new Size(196, 22);
            zoomToUkraineToolStripMenuItem.Text = "Центрувати на Україні";
            zoomToUkraineToolStripMenuItem.Click += zoomToUkraineToolStripMenuItem_Click;
            // 
            // довідкаToolStripMenuItem
            // 
            довідкаToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { helpToolStripMenuItem });
            довідкаToolStripMenuItem.Name = "довідкаToolStripMenuItem";
            довідкаToolStripMenuItem.Size = new Size(61, 20);
            довідкаToolStripMenuItem.Text = "Довідка";
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(154, 22);
            helpToolStripMenuItem.Text = "Про програму";
            helpToolStripMenuItem.Click += helpToolStripMenuItem_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnClearSearchResults);
            groupBox1.Controls.Add(btnSearch);
            groupBox1.Controls.Add(cmbSearchCriteria);
            groupBox1.Controls.Add(txtSearchQuery);
            groupBox1.Location = new Point(462, 36);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(666, 70);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "ПОШУК";
            // 
            // btnClearSearchResults
            // 
            btnClearSearchResults.Location = new Point(523, 22);
            btnClearSearchResults.Name = "btnClearSearchResults";
            btnClearSearchResults.Size = new Size(118, 32);
            btnClearSearchResults.TabIndex = 4;
            btnClearSearchResults.Text = "Очистити";
            btnClearSearchResults.UseVisualStyleBackColor = true;
            btnClearSearchResults.Click += btnClearSearchResults_Click;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(373, 22);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(118, 31);
            btnSearch.TabIndex = 3;
            btnSearch.Text = "Пошук";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // cmbSearchCriteria
            // 
            cmbSearchCriteria.FormattingEnabled = true;
            cmbSearchCriteria.Location = new Point(231, 28);
            cmbSearchCriteria.Name = "cmbSearchCriteria";
            cmbSearchCriteria.Size = new Size(121, 23);
            cmbSearchCriteria.TabIndex = 2;
            // 
            // txtSearchQuery
            // 
            txtSearchQuery.Location = new Point(36, 28);
            txtSearchQuery.Name = "txtSearchQuery";
            txtSearchQuery.Size = new Size(178, 23);
            txtSearchQuery.TabIndex = 1;
            // 
            // dgvSearchResults
            // 
            dgvSearchResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSearchResults.Location = new Point(12, 36);
            dgvSearchResults.Name = "dgvSearchResults";
            dgvSearchResults.Size = new Size(444, 605);
            dgvSearchResults.TabIndex = 3;
            dgvSearchResults.SelectionChanged += dgvSearchResults_SelectionChanged;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusMessages, toolStripMapCoordinates });
            statusStrip1.Location = new Point(0, 631);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1142, 22);
            statusStrip1.TabIndex = 4;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusMessages
            // 
            toolStripStatusMessages.Name = "toolStripStatusMessages";
            toolStripStatusMessages.Size = new Size(102, 17);
            toolStripStatusMessages.Text = "Для повідомлень";
            // 
            // toolStripMapCoordinates
            // 
            toolStripMapCoordinates.Name = "toolStripMapCoordinates";
            toolStripMapCoordinates.Size = new Size(183, 17);
            toolStripMapCoordinates.Text = "Для координат курсора на карті";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1142, 653);
            Controls.Add(statusStrip1);
            Controls.Add(dgvSearchResults);
            Controls.Add(groupBox1);
            Controls.Add(gmapControlMain);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            Text = "Головне меню";
            Load += MainForm_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSearchResults).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private GMap.NET.WindowsForms.GMapControl gmapControlMain;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem entitiesToolStripMenuItem;
        private ToolStripMenuItem manageCountriesToolStripMenuItem;
        private ToolStripMenuItem manageRegionsToolStripMenuItem;
        private ToolStripMenuItem manageCitiesToolStripMenuItem;
        private ToolStripMenuItem reportsToolStripMenuItem;
        private ToolStripMenuItem continentPopulationReportToolStripMenuItem;
        private ToolStripMenuItem картаToolStripMenuItem;
        private ToolStripMenuItem mapToolStripMenuItem;
        private ToolStripMenuItem zoomToUkraineToolStripMenuItem;
        private ToolStripMenuItem довідкаToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private GroupBox groupBox1;
        private Button btnClearSearchResults;
        private Button btnSearch;
        private ComboBox cmbSearchCriteria;
        private TextBox txtSearchQuery;
        private DataGridView dgvSearchResults;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusMessages;
        private ToolStripStatusLabel toolStripMapCoordinates;
        private ToolStripMenuItem manageContinentsToolStripMenuItem;
    }
}
