namespace CleanMyFolder {
    partial class frmCleanMyFolder {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCleanMyFolder));
            this.menuClean = new System.Windows.Forms.MenuStrip();
            this.tsmFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmStartFullscan = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSpecifiedScan = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmShowHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmOpenLog = new System.Windows.Forms.ToolStripMenuItem();
            this.tabWindows = new System.Windows.Forms.TabControl();
            this.tabMenu = new System.Windows.Forms.TabPage();
            this.dtpSetDate = new System.Windows.Forms.DateTimePicker();
            this.txtExtension = new System.Windows.Forms.TextBox();
            this.lblUserExtension = new System.Windows.Forms.Label();
            this.chkIncludeSubFolders = new System.Windows.Forms.CheckBox();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.cmdReset = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.lstInformation = new System.Windows.Forms.ListBox();
            this.cmdStartScan = new System.Windows.Forms.Button();
            this.lblDateValue = new System.Windows.Forms.Label();
            this.lblSize = new System.Windows.Forms.Label();
            this.lblExtension = new System.Windows.Forms.Label();
            this.lblPath = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.numericSize = new System.Windows.Forms.NumericUpDown();
            this.cmbDateType = new System.Windows.Forms.ComboBox();
            this.cmbDateValue = new System.Windows.Forms.ComboBox();
            this.cmbSize = new System.Windows.Forms.ComboBox();
            this.cmbExtension = new System.Windows.Forms.ComboBox();
            this.chkIncludeDateType = new System.Windows.Forms.CheckBox();
            this.chkIncludeDate = new System.Windows.Forms.CheckBox();
            this.chkIncludeSize = new System.Windows.Forms.CheckBox();
            this.chkIncludeExtension = new System.Windows.Forms.CheckBox();
            this.cmdSetPath = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tabResult = new System.Windows.Forms.TabPage();
            this.lblResult = new System.Windows.Forms.Label();
            this.lblFilesOptions = new System.Windows.Forms.Label();
            this.lblFolderOptions = new System.Windows.Forms.Label();
            this.cmdOpenFile = new System.Windows.Forms.Button();
            this.cmdOpenFolder = new System.Windows.Forms.Button();
            this.cmdDelFilesPermanently = new System.Windows.Forms.Button();
            this.cmdMoveFilesRecycleBin = new System.Windows.Forms.Button();
            this.cmdMoveFolderRecycleBin = new System.Windows.Forms.Button();
            this.cmdDelFoldersPermanently = new System.Windows.Forms.Button();
            this.cmdMoveFiles = new System.Windows.Forms.Button();
            this.lblChosenFolder = new System.Windows.Forms.Label();
            this.lblFolderSize = new System.Windows.Forms.Label();
            this.lblSizeEarned = new System.Windows.Forms.Label();
            this.lblDriveSize = new System.Windows.Forms.Label();
            this.treeviewFolders = new System.Windows.Forms.TreeView();
            this.ctmTreeView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmScan = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmScanFolderSpecified = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmFullScan = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmNewScan = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmExpandClose = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmExpandAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCloseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmExpandSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCloseSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmOpenFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmViewFoldersInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvFiles = new System.Windows.Forms.DataGridView();
            this.columnFolder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnExtension = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnDateCreated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnDateModified = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnLastAccessed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnMove = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.columnDelete = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ctmDataGridView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmMove = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSelectMoveOne = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmUnselectMoveOne = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSelectAllMove = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmUnselectAllMove = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDeleteOne = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmUncheckDelOne = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSelectAllDel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmUncheckDelAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmOpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdMoveFolder = new System.Windows.Forms.Button();
            this.lblProgress = new System.Windows.Forms.Label();
            this.bwScan = new System.ComponentModel.BackgroundWorker();
            this.hpHelper = new System.Windows.Forms.HelpProvider();
            this.tipLabel = new System.Windows.Forms.ToolTip(this.components);
            this.fdbPath = new System.Windows.Forms.FolderBrowserDialog();
            this.menuClean.SuspendLayout();
            this.tabWindows.SuspendLayout();
            this.tabMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericSize)).BeginInit();
            this.tabResult.SuspendLayout();
            this.ctmTreeView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiles)).BeginInit();
            this.ctmDataGridView.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuClean
            // 
            this.menuClean.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmFile,
            this.tsmHelp});
            this.menuClean.Location = new System.Drawing.Point(0, 0);
            this.menuClean.Name = "menuClean";
            this.menuClean.Size = new System.Drawing.Size(1284, 24);
            this.menuClean.TabIndex = 0;
            this.menuClean.Text = "menuStrip1";
            // 
            // tsmFile
            // 
            this.tsmFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmStartFullscan,
            this.tsmSpecifiedScan,
            this.tsmExit});
            this.tsmFile.Name = "tsmFile";
            this.tsmFile.Size = new System.Drawing.Size(37, 20);
            this.tsmFile.Text = "File";
            // 
            // tsmStartFullscan
            // 
            this.tsmStartFullscan.Name = "tsmStartFullscan";
            this.tsmStartFullscan.Size = new System.Drawing.Size(209, 22);
            this.tsmStartFullscan.Text = "Start fullscan";
            this.tsmStartFullscan.Click += new System.EventHandler(this.tsmStartFullscan_Click);
            // 
            // tsmSpecifiedScan
            // 
            this.tsmSpecifiedScan.Name = "tsmSpecifiedScan";
            this.tsmSpecifiedScan.Size = new System.Drawing.Size(209, 22);
            this.tsmSpecifiedScan.Text = "Start a new specified scan";
            this.tsmSpecifiedScan.Click += new System.EventHandler(this.tsmSpecifiedScan_Click);
            // 
            // tsmExit
            // 
            this.tsmExit.Name = "tsmExit";
            this.tsmExit.Size = new System.Drawing.Size(209, 22);
            this.tsmExit.Text = "Exit program";
            this.tsmExit.Click += new System.EventHandler(this.tsmExit_Click);
            // 
            // tsmHelp
            // 
            this.tsmHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmShowHelp,
            this.tsmAbout,
            this.tsmOpenLog});
            this.tsmHelp.Name = "tsmHelp";
            this.tsmHelp.Size = new System.Drawing.Size(44, 20);
            this.tsmHelp.Text = "Help";
            // 
            // tsmShowHelp
            // 
            this.tsmShowHelp.Name = "tsmShowHelp";
            this.tsmShowHelp.Size = new System.Drawing.Size(139, 22);
            this.tsmShowHelp.Text = "Help";
            this.tsmShowHelp.Click += new System.EventHandler(this.tsmShowHelp_Click);
            // 
            // tsmAbout
            // 
            this.tsmAbout.Name = "tsmAbout";
            this.tsmAbout.Size = new System.Drawing.Size(139, 22);
            this.tsmAbout.Text = "About";
            this.tsmAbout.Click += new System.EventHandler(this.tsmAbout_Click);
            // 
            // tsmOpenLog
            // 
            this.tsmOpenLog.Name = "tsmOpenLog";
            this.tsmOpenLog.Size = new System.Drawing.Size(139, 22);
            this.tsmOpenLog.Text = "Open logfile";
            this.tsmOpenLog.Click += new System.EventHandler(this.tsmOpenLog_Click);
            // 
            // tabWindows
            // 
            this.tabWindows.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabWindows.Controls.Add(this.tabMenu);
            this.tabWindows.Controls.Add(this.tabResult);
            this.hpHelper.SetHelpKeyword(this.tabWindows, "");
            this.hpHelper.SetHelpString(this.tabWindows, "");
            this.tabWindows.Location = new System.Drawing.Point(0, 24);
            this.tabWindows.Name = "tabWindows";
            this.tabWindows.SelectedIndex = 0;
            this.hpHelper.SetShowHelp(this.tabWindows, true);
            this.tabWindows.Size = new System.Drawing.Size(1284, 680);
            this.tabWindows.TabIndex = 1;
            // 
            // tabMenu
            // 
            this.tabMenu.Controls.Add(this.dtpSetDate);
            this.tabMenu.Controls.Add(this.txtExtension);
            this.tabMenu.Controls.Add(this.lblUserExtension);
            this.tabMenu.Controls.Add(this.chkIncludeSubFolders);
            this.tabMenu.Controls.Add(this.chkSelectAll);
            this.tabMenu.Controls.Add(this.cmdReset);
            this.tabMenu.Controls.Add(this.cmdCancel);
            this.tabMenu.Controls.Add(this.lstInformation);
            this.tabMenu.Controls.Add(this.cmdStartScan);
            this.tabMenu.Controls.Add(this.lblDateValue);
            this.tabMenu.Controls.Add(this.lblSize);
            this.tabMenu.Controls.Add(this.lblExtension);
            this.tabMenu.Controls.Add(this.lblPath);
            this.tabMenu.Controls.Add(this.lblInfo);
            this.tabMenu.Controls.Add(this.numericSize);
            this.tabMenu.Controls.Add(this.cmbDateType);
            this.tabMenu.Controls.Add(this.cmbDateValue);
            this.tabMenu.Controls.Add(this.cmbSize);
            this.tabMenu.Controls.Add(this.cmbExtension);
            this.tabMenu.Controls.Add(this.chkIncludeDateType);
            this.tabMenu.Controls.Add(this.chkIncludeDate);
            this.tabMenu.Controls.Add(this.chkIncludeSize);
            this.tabMenu.Controls.Add(this.chkIncludeExtension);
            this.tabMenu.Controls.Add(this.cmdSetPath);
            this.tabMenu.Controls.Add(this.lblTitle);
            this.hpHelper.SetHelpKeyword(this.tabMenu, "Scanning");
            this.hpHelper.SetHelpNavigator(this.tabMenu, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.tabMenu.Location = new System.Drawing.Point(4, 22);
            this.tabMenu.Name = "tabMenu";
            this.tabMenu.Padding = new System.Windows.Forms.Padding(3);
            this.hpHelper.SetShowHelp(this.tabMenu, true);
            this.tabMenu.Size = new System.Drawing.Size(1276, 654);
            this.tabMenu.TabIndex = 0;
            this.tabMenu.Text = "Scan menu";
            this.tabMenu.UseVisualStyleBackColor = true;
            // 
            // dtpSetDate
            // 
            this.dtpSetDate.Checked = false;
            this.dtpSetDate.Enabled = false;
            this.dtpSetDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpSetDate.Location = new System.Drawing.Point(358, 428);
            this.dtpSetDate.Name = "dtpSetDate";
            this.dtpSetDate.Size = new System.Drawing.Size(149, 22);
            this.dtpSetDate.TabIndex = 11;
            // 
            // txtExtension
            // 
            this.txtExtension.Enabled = false;
            this.txtExtension.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hpHelper.SetHelpKeyword(this.txtExtension, "Filetypes/Extensions");
            this.hpHelper.SetHelpNavigator(this.txtExtension, System.Windows.Forms.HelpNavigator.TableOfContents);
            this.txtExtension.Location = new System.Drawing.Point(220, 272);
            this.txtExtension.Name = "txtExtension";
            this.hpHelper.SetShowHelp(this.txtExtension, true);
            this.txtExtension.Size = new System.Drawing.Size(100, 22);
            this.txtExtension.TabIndex = 5;
            this.tipLabel.SetToolTip(this.txtExtension, "Enter an extension/filetype \r\nexample: *.txt  (gets all textfiles)");
            this.txtExtension.Enter += new System.EventHandler(this.txtExtension_Enter);
            // 
            // lblUserExtension
            // 
            this.lblUserExtension.AutoSize = true;
            this.lblUserExtension.Enabled = false;
            this.lblUserExtension.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserExtension.Location = new System.Drawing.Point(99, 275);
            this.lblUserExtension.Name = "lblUserExtension";
            this.lblUserExtension.Size = new System.Drawing.Size(114, 16);
            this.lblUserExtension.TabIndex = 22;
            this.lblUserExtension.Text = "Or enter your own:";
            // 
            // chkIncludeSubFolders
            // 
            this.chkIncludeSubFolders.AutoSize = true;
            this.chkIncludeSubFolders.Checked = true;
            this.chkIncludeSubFolders.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIncludeSubFolders.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIncludeSubFolders.Location = new System.Drawing.Point(55, 172);
            this.chkIncludeSubFolders.Name = "chkIncludeSubFolders";
            this.chkIncludeSubFolders.Size = new System.Drawing.Size(131, 20);
            this.chkIncludeSubFolders.TabIndex = 2;
            this.chkIncludeSubFolders.Text = "Scan subfolders?";
            this.chkIncludeSubFolders.UseVisualStyleBackColor = true;
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSelectAll.Location = new System.Drawing.Point(11, 134);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(132, 20);
            this.chkSelectAll.TabIndex = 1;
            this.chkSelectAll.Text = "Select all criterias";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // cmdReset
            // 
            this.cmdReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdReset.Location = new System.Drawing.Point(1167, 479);
            this.cmdReset.Name = "cmdReset";
            this.cmdReset.Size = new System.Drawing.Size(98, 31);
            this.cmdReset.TabIndex = 16;
            this.cmdReset.Text = "&Reset";
            this.tipLabel.SetToolTip(this.cmdReset, "Resets the value to default");
            this.cmdReset.UseVisualStyleBackColor = true;
            this.cmdReset.Click += new System.EventHandler(this.cmdReset_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.Enabled = false;
            this.cmdCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCancel.Location = new System.Drawing.Point(1063, 479);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(98, 31);
            this.cmdCancel.TabIndex = 15;
            this.cmdCancel.Text = "&Cancel Scan";
            this.tipLabel.SetToolTip(this.cmdCancel, "Cancels the scan, no results will be shown");
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // lstInformation
            // 
            this.lstInformation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstInformation.FormattingEnabled = true;
            this.lstInformation.HorizontalScrollbar = true;
            this.lstInformation.ItemHeight = 16;
            this.lstInformation.Location = new System.Drawing.Point(6, 516);
            this.lstInformation.Name = "lstInformation";
            this.lstInformation.Size = new System.Drawing.Size(1257, 132);
            this.lstInformation.TabIndex = 17;
            this.lstInformation.TabStop = false;
            this.tipLabel.SetToolTip(this.lstInformation, "Displays information about scan and potential errors");
            // 
            // cmdStartScan
            // 
            this.cmdStartScan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdStartScan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdStartScan.Location = new System.Drawing.Point(959, 479);
            this.cmdStartScan.Name = "cmdStartScan";
            this.cmdStartScan.Size = new System.Drawing.Size(98, 31);
            this.cmdStartScan.TabIndex = 14;
            this.cmdStartScan.Text = "&Start scan";
            this.tipLabel.SetToolTip(this.cmdStartScan, "Starts the scan");
            this.cmdStartScan.UseVisualStyleBackColor = true;
            this.cmdStartScan.Click += new System.EventHandler(this.cmdStartScan_Click);
            // 
            // lblDateValue
            // 
            this.lblDateValue.AutoSize = true;
            this.lblDateValue.Enabled = false;
            this.lblDateValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateValue.Location = new System.Drawing.Point(104, 428);
            this.lblDateValue.Name = "lblDateValue";
            this.lblDateValue.Size = new System.Drawing.Size(138, 16);
            this.lblDateValue.TabIndex = 24;
            this.lblDateValue.Text = "Scan for files on date :";
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Enabled = false;
            this.lblSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSize.Location = new System.Drawing.Point(104, 351);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(110, 16);
            this.lblSize.TabIndex = 23;
            this.lblSize.Text = "Files bigger then:";
            // 
            // lblExtension
            // 
            this.lblExtension.AutoSize = true;
            this.lblExtension.Enabled = false;
            this.lblExtension.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExtension.Location = new System.Drawing.Point(99, 245);
            this.lblExtension.Name = "lblExtension";
            this.lblExtension.Size = new System.Drawing.Size(104, 16);
            this.lblExtension.TabIndex = 21;
            this.lblExtension.Text = "Choose filetype:";
            // 
            // lblPath
            // 
            this.lblPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPath.Location = new System.Drawing.Point(55, 66);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(1106, 23);
            this.lblPath.TabIndex = 20;
            this.lblPath.Text = "Choose a path to scan...";
            this.lblPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.Location = new System.Drawing.Point(8, 69);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(41, 17);
            this.lblInfo.TabIndex = 19;
            this.lblInfo.Text = "Path:";
            // 
            // numericSize
            // 
            this.numericSize.Enabled = false;
            this.numericSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericSize.Location = new System.Drawing.Point(220, 349);
            this.numericSize.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericSize.Name = "numericSize";
            this.numericSize.Size = new System.Drawing.Size(82, 22);
            this.numericSize.TabIndex = 7;
            // 
            // cmbDateType
            // 
            this.cmbDateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDateType.Enabled = false;
            this.cmbDateType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDateType.FormattingEnabled = true;
            this.cmbDateType.Location = new System.Drawing.Point(248, 461);
            this.cmbDateType.Name = "cmbDateType";
            this.cmbDateType.Size = new System.Drawing.Size(128, 24);
            this.cmbDateType.TabIndex = 13;
            // 
            // cmbDateValue
            // 
            this.cmbDateValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDateValue.Enabled = false;
            this.cmbDateValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDateValue.FormattingEnabled = true;
            this.cmbDateValue.Location = new System.Drawing.Point(248, 424);
            this.cmbDateValue.Name = "cmbDateValue";
            this.cmbDateValue.Size = new System.Drawing.Size(104, 24);
            this.cmbDateValue.TabIndex = 10;
            // 
            // cmbSize
            // 
            this.cmbSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSize.Enabled = false;
            this.cmbSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSize.FormattingEnabled = true;
            this.cmbSize.Location = new System.Drawing.Point(308, 347);
            this.cmbSize.Name = "cmbSize";
            this.cmbSize.Size = new System.Drawing.Size(68, 24);
            this.cmbSize.TabIndex = 8;
            // 
            // cmbExtension
            // 
            this.cmbExtension.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExtension.Enabled = false;
            this.cmbExtension.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbExtension.FormattingEnabled = true;
            this.hpHelper.SetHelpKeyword(this.cmbExtension, "Filetypes/Extensions");
            this.hpHelper.SetHelpNavigator(this.cmbExtension, System.Windows.Forms.HelpNavigator.TableOfContents);
            this.cmbExtension.Location = new System.Drawing.Point(220, 241);
            this.cmbExtension.Name = "cmbExtension";
            this.hpHelper.SetShowHelp(this.cmbExtension, true);
            this.cmbExtension.Size = new System.Drawing.Size(162, 24);
            this.cmbExtension.TabIndex = 4;
            // 
            // chkIncludeDateType
            // 
            this.chkIncludeDateType.AutoSize = true;
            this.chkIncludeDateType.Enabled = false;
            this.chkIncludeDateType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIncludeDateType.Location = new System.Drawing.Point(108, 463);
            this.chkIncludeDateType.Name = "chkIncludeDateType";
            this.chkIncludeDateType.Size = new System.Drawing.Size(134, 20);
            this.chkIncludeDateType.TabIndex = 12;
            this.chkIncludeDateType.Text = "Where the date is:";
            this.chkIncludeDateType.UseVisualStyleBackColor = true;
            this.chkIncludeDateType.CheckStateChanged += new System.EventHandler(this.chkIncludeDateType_CheckedChanged);
            // 
            // chkIncludeDate
            // 
            this.chkIncludeDate.AutoSize = true;
            this.chkIncludeDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIncludeDate.Location = new System.Drawing.Point(60, 398);
            this.chkIncludeDate.Name = "chkIncludeDate";
            this.chkIncludeDate.Size = new System.Drawing.Size(200, 20);
            this.chkIncludeDate.TabIndex = 9;
            this.chkIncludeDate.Text = "Scan for files based on date?";
            this.chkIncludeDate.UseVisualStyleBackColor = true;
            this.chkIncludeDate.CheckStateChanged += new System.EventHandler(this.chkIncludeDate_CheckedChanged);
            // 
            // chkIncludeSize
            // 
            this.chkIncludeSize.AutoSize = true;
            this.chkIncludeSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIncludeSize.Location = new System.Drawing.Point(60, 322);
            this.chkIncludeSize.Name = "chkIncludeSize";
            this.chkIncludeSize.Size = new System.Drawing.Size(197, 20);
            this.chkIncludeSize.TabIndex = 6;
            this.chkIncludeSize.Text = "Scan for files based on size?";
            this.chkIncludeSize.UseVisualStyleBackColor = true;
            this.chkIncludeSize.CheckedChanged += new System.EventHandler(this.chkIncludeSize_CheckedChanged);
            // 
            // chkIncludeExtension
            // 
            this.chkIncludeExtension.AutoSize = true;
            this.chkIncludeExtension.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hpHelper.SetHelpKeyword(this.chkIncludeExtension, "Filetypes/Extensions");
            this.hpHelper.SetHelpNavigator(this.chkIncludeExtension, System.Windows.Forms.HelpNavigator.TableOfContents);
            this.chkIncludeExtension.Location = new System.Drawing.Point(55, 215);
            this.chkIncludeExtension.Name = "chkIncludeExtension";
            this.hpHelper.SetShowHelp(this.chkIncludeExtension, true);
            this.chkIncludeExtension.Size = new System.Drawing.Size(214, 20);
            this.chkIncludeExtension.TabIndex = 3;
            this.chkIncludeExtension.Text = "Scan for files of extension/type?";
            this.chkIncludeExtension.UseVisualStyleBackColor = true;
            this.chkIncludeExtension.CheckedChanged += new System.EventHandler(this.chkIncludeExtension_CheckedChanged);
            // 
            // cmdSetPath
            // 
            this.cmdSetPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSetPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSetPath.Location = new System.Drawing.Point(1167, 63);
            this.cmdSetPath.Name = "cmdSetPath";
            this.cmdSetPath.Size = new System.Drawing.Size(96, 28);
            this.cmdSetPath.TabIndex = 0;
            this.cmdSetPath.Text = "&Browse...";
            this.tipLabel.SetToolTip(this.cmdSetPath, "Browse for the folder(s) to be scanned");
            this.cmdSetPath.UseVisualStyleBackColor = true;
            this.cmdSetPath.Click += new System.EventHandler(this.cmdSetPath_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(493, 3);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(147, 24);
            this.lblTitle.TabIndex = 18;
            this.lblTitle.Text = "Settings for scan";
            // 
            // tabResult
            // 
            this.tabResult.Controls.Add(this.lblResult);
            this.tabResult.Controls.Add(this.lblFilesOptions);
            this.tabResult.Controls.Add(this.lblFolderOptions);
            this.tabResult.Controls.Add(this.cmdOpenFile);
            this.tabResult.Controls.Add(this.cmdOpenFolder);
            this.tabResult.Controls.Add(this.cmdDelFilesPermanently);
            this.tabResult.Controls.Add(this.cmdMoveFilesRecycleBin);
            this.tabResult.Controls.Add(this.cmdMoveFolderRecycleBin);
            this.tabResult.Controls.Add(this.cmdDelFoldersPermanently);
            this.tabResult.Controls.Add(this.cmdMoveFiles);
            this.tabResult.Controls.Add(this.lblChosenFolder);
            this.tabResult.Controls.Add(this.lblFolderSize);
            this.tabResult.Controls.Add(this.lblSizeEarned);
            this.tabResult.Controls.Add(this.lblDriveSize);
            this.tabResult.Controls.Add(this.treeviewFolders);
            this.tabResult.Controls.Add(this.dgvFiles);
            this.tabResult.Controls.Add(this.cmdMoveFolder);
            this.hpHelper.SetHelpKeyword(this.tabResult, "Result");
            this.hpHelper.SetHelpNavigator(this.tabResult, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.tabResult.Location = new System.Drawing.Point(4, 22);
            this.tabResult.Name = "tabResult";
            this.tabResult.Padding = new System.Windows.Forms.Padding(3);
            this.hpHelper.SetShowHelp(this.tabResult, true);
            this.tabResult.Size = new System.Drawing.Size(1276, 654);
            this.tabResult.TabIndex = 1;
            this.tabResult.Text = "Scan result";
            this.tabResult.UseVisualStyleBackColor = true;
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResult.ForeColor = System.Drawing.Color.Red;
            this.lblResult.Location = new System.Drawing.Point(20, 18);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(235, 17);
            this.lblResult.TabIndex = 24;
            this.lblResult.Text = "No files matching criteria was found.";
            this.lblResult.Visible = false;
            // 
            // lblFilesOptions
            // 
            this.lblFilesOptions.AutoSize = true;
            this.lblFilesOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilesOptions.Location = new System.Drawing.Point(1033, 199);
            this.lblFilesOptions.Name = "lblFilesOptions";
            this.lblFilesOptions.Size = new System.Drawing.Size(84, 17);
            this.lblFilesOptions.TabIndex = 23;
            this.lblFilesOptions.Text = "File options:";
            // 
            // lblFolderOptions
            // 
            this.lblFolderOptions.AutoSize = true;
            this.lblFolderOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFolderOptions.Location = new System.Drawing.Point(749, 199);
            this.lblFolderOptions.Name = "lblFolderOptions";
            this.lblFolderOptions.Size = new System.Drawing.Size(102, 17);
            this.lblFolderOptions.TabIndex = 22;
            this.lblFolderOptions.Text = "Folder options:";
            // 
            // cmdOpenFile
            // 
            this.cmdOpenFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdOpenFile.Location = new System.Drawing.Point(1036, 228);
            this.cmdOpenFile.Name = "cmdOpenFile";
            this.hpHelper.SetShowHelp(this.cmdOpenFile, false);
            this.cmdOpenFile.Size = new System.Drawing.Size(161, 38);
            this.cmdOpenFile.TabIndex = 21;
            this.cmdOpenFile.Text = "Open file";
            this.tipLabel.SetToolTip(this.cmdOpenFile, "Opens the selected file");
            this.cmdOpenFile.UseVisualStyleBackColor = true;
            this.cmdOpenFile.Click += new System.EventHandler(this.cmdOpenFile_Click);
            // 
            // cmdOpenFolder
            // 
            this.cmdOpenFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdOpenFolder.Location = new System.Drawing.Point(752, 228);
            this.cmdOpenFolder.Name = "cmdOpenFolder";
            this.hpHelper.SetShowHelp(this.cmdOpenFolder, false);
            this.cmdOpenFolder.Size = new System.Drawing.Size(161, 38);
            this.cmdOpenFolder.TabIndex = 20;
            this.cmdOpenFolder.Text = "Open folder";
            this.tipLabel.SetToolTip(this.cmdOpenFolder, "Opens the chosen folder");
            this.cmdOpenFolder.UseVisualStyleBackColor = true;
            this.cmdOpenFolder.Click += new System.EventHandler(this.cmdOpenFolder_Click);
            // 
            // cmdDelFilesPermanently
            // 
            this.cmdDelFilesPermanently.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hpHelper.SetHelpKeyword(this.cmdDelFilesPermanently, "Deletion");
            this.hpHelper.SetHelpNavigator(this.cmdDelFilesPermanently, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.cmdDelFilesPermanently.Location = new System.Drawing.Point(1036, 360);
            this.cmdDelFilesPermanently.Name = "cmdDelFilesPermanently";
            this.hpHelper.SetShowHelp(this.cmdDelFilesPermanently, false);
            this.cmdDelFilesPermanently.Size = new System.Drawing.Size(161, 38);
            this.cmdDelFilesPermanently.TabIndex = 9;
            this.cmdDelFilesPermanently.Text = "Delete  file(s) permanently";
            this.tipLabel.SetToolTip(this.cmdDelFilesPermanently, "Deletes all the selected file(s) permanently (not restorable)");
            this.cmdDelFilesPermanently.UseVisualStyleBackColor = true;
            this.cmdDelFilesPermanently.Click += new System.EventHandler(this.cmdDelFilePermanently_Click);
            // 
            // cmdMoveFilesRecycleBin
            // 
            this.cmdMoveFilesRecycleBin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hpHelper.SetHelpKeyword(this.cmdMoveFilesRecycleBin, "Deletion");
            this.hpHelper.SetHelpNavigator(this.cmdMoveFilesRecycleBin, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.cmdMoveFilesRecycleBin.Location = new System.Drawing.Point(1036, 316);
            this.cmdMoveFilesRecycleBin.Name = "cmdMoveFilesRecycleBin";
            this.hpHelper.SetShowHelp(this.cmdMoveFilesRecycleBin, false);
            this.cmdMoveFilesRecycleBin.Size = new System.Drawing.Size(161, 38);
            this.cmdMoveFilesRecycleBin.TabIndex = 8;
            this.cmdMoveFilesRecycleBin.Text = "Move  file(s) to Recycle bin";
            this.tipLabel.SetToolTip(this.cmdMoveFilesRecycleBin, "Moves all the selected file(s) to the Recycle bin");
            this.cmdMoveFilesRecycleBin.UseVisualStyleBackColor = true;
            this.cmdMoveFilesRecycleBin.Click += new System.EventHandler(this.cmdMoveFilesRecycleBin_Click);
            // 
            // cmdMoveFolderRecycleBin
            // 
            this.cmdMoveFolderRecycleBin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hpHelper.SetHelpKeyword(this.cmdMoveFolderRecycleBin, "Deletion");
            this.hpHelper.SetHelpNavigator(this.cmdMoveFolderRecycleBin, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.cmdMoveFolderRecycleBin.Location = new System.Drawing.Point(752, 316);
            this.cmdMoveFolderRecycleBin.Name = "cmdMoveFolderRecycleBin";
            this.hpHelper.SetShowHelp(this.cmdMoveFolderRecycleBin, false);
            this.cmdMoveFolderRecycleBin.Size = new System.Drawing.Size(161, 38);
            this.cmdMoveFolderRecycleBin.TabIndex = 5;
            this.cmdMoveFolderRecycleBin.Text = "Move folder to Recycle  bin";
            this.tipLabel.SetToolTip(this.cmdMoveFolderRecycleBin, "Moves the chosen folder to the Recycle bin");
            this.cmdMoveFolderRecycleBin.UseVisualStyleBackColor = true;
            this.cmdMoveFolderRecycleBin.Click += new System.EventHandler(this.cmdMoveFolderRecycleBin_Click);
            // 
            // cmdDelFoldersPermanently
            // 
            this.cmdDelFoldersPermanently.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hpHelper.SetHelpKeyword(this.cmdDelFoldersPermanently, "Deletion");
            this.hpHelper.SetHelpNavigator(this.cmdDelFoldersPermanently, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.cmdDelFoldersPermanently.Location = new System.Drawing.Point(752, 360);
            this.cmdDelFoldersPermanently.Name = "cmdDelFoldersPermanently";
            this.hpHelper.SetShowHelp(this.cmdDelFoldersPermanently, false);
            this.cmdDelFoldersPermanently.Size = new System.Drawing.Size(161, 38);
            this.cmdDelFoldersPermanently.TabIndex = 6;
            this.cmdDelFoldersPermanently.Text = "Delete folder permanently";
            this.tipLabel.SetToolTip(this.cmdDelFoldersPermanently, "Deletes the chosen folder permanently (not restorable)");
            this.cmdDelFoldersPermanently.UseVisualStyleBackColor = true;
            this.cmdDelFoldersPermanently.Click += new System.EventHandler(this.cmdDelFoldersPermanently_Click);
            // 
            // cmdMoveFiles
            // 
            this.cmdMoveFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hpHelper.SetHelpKeyword(this.cmdMoveFiles, "Moving");
            this.hpHelper.SetHelpNavigator(this.cmdMoveFiles, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.cmdMoveFiles.Location = new System.Drawing.Point(1036, 272);
            this.cmdMoveFiles.Name = "cmdMoveFiles";
            this.hpHelper.SetShowHelp(this.cmdMoveFiles, false);
            this.cmdMoveFiles.Size = new System.Drawing.Size(161, 38);
            this.cmdMoveFiles.TabIndex = 7;
            this.cmdMoveFiles.Text = "Move file(s)";
            this.tipLabel.SetToolTip(this.cmdMoveFiles, "Moves all the selected file(s) to a new location");
            this.cmdMoveFiles.UseVisualStyleBackColor = true;
            this.cmdMoveFiles.Click += new System.EventHandler(this.cmdMoveFiles_Click);
            // 
            // lblChosenFolder
            // 
            this.lblChosenFolder.AutoSize = true;
            this.lblChosenFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChosenFolder.Location = new System.Drawing.Point(749, 33);
            this.lblChosenFolder.Name = "lblChosenFolder";
            this.lblChosenFolder.Size = new System.Drawing.Size(100, 17);
            this.lblChosenFolder.TabIndex = 17;
            this.lblChosenFolder.Text = "Chosen folder:";
            // 
            // lblFolderSize
            // 
            this.lblFolderSize.AutoSize = true;
            this.lblFolderSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFolderSize.Location = new System.Drawing.Point(749, 65);
            this.lblFolderSize.Name = "lblFolderSize";
            this.lblFolderSize.Size = new System.Drawing.Size(135, 17);
            this.lblFolderSize.TabIndex = 18;
            this.lblFolderSize.Text = "Current folders size:";
            this.tipLabel.SetToolTip(this.lblFolderSize, "The size of the folder (size is based on scan value)");
            // 
            // lblSizeEarned
            // 
            this.lblSizeEarned.AutoSize = true;
            this.lblSizeEarned.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSizeEarned.Location = new System.Drawing.Point(749, 98);
            this.lblSizeEarned.Name = "lblSizeEarned";
            this.lblSizeEarned.Size = new System.Drawing.Size(182, 17);
            this.lblSizeEarned.TabIndex = 19;
            this.lblSizeEarned.Text = "Space that will be available:";
            this.tipLabel.SetToolTip(this.lblSizeEarned, "The space that will be available.\r\nMay be inaccurate if folder/file(s) are \r\nmove" +
        "d to new location on same drive.\r\n");
            // 
            // lblDriveSize
            // 
            this.lblDriveSize.AutoSize = true;
            this.lblDriveSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDriveSize.Location = new System.Drawing.Point(749, 6);
            this.lblDriveSize.Name = "lblDriveSize";
            this.lblDriveSize.Size = new System.Drawing.Size(142, 17);
            this.lblDriveSize.TabIndex = 16;
            this.lblDriveSize.Text = "Space used on drive:";
            this.tipLabel.SetToolTip(this.lblDriveSize, "The size of the drive the folder(s) exists on \r\n(size is based on scan value)");
            // 
            // treeviewFolders
            // 
            this.treeviewFolders.ContextMenuStrip = this.ctmTreeView;
            this.treeviewFolders.Enabled = false;
            this.treeviewFolders.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hpHelper.SetHelpKeyword(this.treeviewFolders, "Folder");
            this.hpHelper.SetHelpNavigator(this.treeviewFolders, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.treeviewFolders.Location = new System.Drawing.Point(8, 6);
            this.treeviewFolders.Name = "treeviewFolders";
            this.hpHelper.SetShowHelp(this.treeviewFolders, true);
            this.treeviewFolders.Size = new System.Drawing.Size(735, 392);
            this.treeviewFolders.TabIndex = 14;
            this.treeviewFolders.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeviewFolders_AfterSelect);
            this.treeviewFolders.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeviewFolders_NodeMouseClick);
            this.treeviewFolders.Enter += new System.EventHandler(this.treeviewFolders_Enter);
            // 
            // ctmTreeView
            // 
            this.ctmTreeView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmScan,
            this.tsmExpandClose,
            this.tsmOpenFolder,
            this.tsmViewFoldersInfo});
            this.ctmTreeView.Name = "ctmRightClick";
            this.ctmTreeView.Size = new System.Drawing.Size(205, 92);
            this.ctmTreeView.Opening += new System.ComponentModel.CancelEventHandler(this.ctmTreeView_Opening);
            // 
            // tsmScan
            // 
            this.tsmScan.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmScanFolderSpecified,
            this.tsmFullScan,
            this.tsmNewScan});
            this.tsmScan.Name = "tsmScan";
            this.tsmScan.Size = new System.Drawing.Size(204, 22);
            this.tsmScan.Text = "Scan";
            // 
            // tsmScanFolderSpecified
            // 
            this.tsmScanFolderSpecified.Name = "tsmScanFolderSpecified";
            this.tsmScanFolderSpecified.Size = new System.Drawing.Size(250, 22);
            this.tsmScanFolderSpecified.Text = "Run specified scan";
            this.tsmScanFolderSpecified.Click += new System.EventHandler(this.tsmScanFolderSpecified_Click);
            // 
            // tsmFullScan
            // 
            this.tsmFullScan.Name = "tsmFullScan";
            this.tsmFullScan.Size = new System.Drawing.Size(250, 22);
            this.tsmFullScan.Text = "Run fullscan of this folder";
            this.tsmFullScan.Click += new System.EventHandler(this.tsmFullScan_Click);
            // 
            // tsmNewScan
            // 
            this.tsmNewScan.Name = "tsmNewScan";
            this.tsmNewScan.Size = new System.Drawing.Size(250, 22);
            this.tsmNewScan.Text = "Scan this folder with new criterias";
            this.tsmNewScan.Click += new System.EventHandler(this.tsmNewScan_Click);
            // 
            // tsmExpandClose
            // 
            this.tsmExpandClose.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmExpandAll,
            this.tsmCloseAll,
            this.tsmExpandSelected,
            this.tsmCloseSelected});
            this.tsmExpandClose.Name = "tsmExpandClose";
            this.tsmExpandClose.Size = new System.Drawing.Size(204, 22);
            this.tsmExpandClose.Text = "Expand/Close";
            // 
            // tsmExpandAll
            // 
            this.tsmExpandAll.Name = "tsmExpandAll";
            this.tsmExpandAll.Size = new System.Drawing.Size(158, 22);
            this.tsmExpandAll.Text = "Expand all";
            this.tsmExpandAll.Click += new System.EventHandler(this.tsmExpandAll_Click);
            // 
            // tsmCloseAll
            // 
            this.tsmCloseAll.Name = "tsmCloseAll";
            this.tsmCloseAll.Size = new System.Drawing.Size(158, 22);
            this.tsmCloseAll.Text = "Close all";
            this.tsmCloseAll.Click += new System.EventHandler(this.tsmCloseAll_Click);
            // 
            // tsmExpandSelected
            // 
            this.tsmExpandSelected.Name = "tsmExpandSelected";
            this.tsmExpandSelected.Size = new System.Drawing.Size(158, 22);
            this.tsmExpandSelected.Text = "Expand selected";
            this.tsmExpandSelected.Click += new System.EventHandler(this.tsmExpandSelected_Click);
            // 
            // tsmCloseSelected
            // 
            this.tsmCloseSelected.Name = "tsmCloseSelected";
            this.tsmCloseSelected.Size = new System.Drawing.Size(158, 22);
            this.tsmCloseSelected.Text = "Close selected";
            this.tsmCloseSelected.Click += new System.EventHandler(this.tsmCloseSelected_Click);
            // 
            // tsmOpenFolder
            // 
            this.tsmOpenFolder.Name = "tsmOpenFolder";
            this.tsmOpenFolder.Size = new System.Drawing.Size(204, 22);
            this.tsmOpenFolder.Text = "Open folder";
            this.tsmOpenFolder.Click += new System.EventHandler(this.tsmOpenFolder_Click);
            // 
            // tsmViewFoldersInfo
            // 
            this.tsmViewFoldersInfo.Name = "tsmViewFoldersInfo";
            this.tsmViewFoldersInfo.Size = new System.Drawing.Size(204, 22);
            this.tsmViewFoldersInfo.Text = "View folders information";
            this.tsmViewFoldersInfo.Click += new System.EventHandler(this.tsmViewFoldersInfo_Click);
            // 
            // dgvFiles
            // 
            this.dgvFiles.AllowUserToAddRows = false;
            this.dgvFiles.AllowUserToDeleteRows = false;
            this.dgvFiles.AllowUserToResizeRows = false;
            this.dgvFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvFiles.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnFolder,
            this.columnExtension,
            this.columnSize,
            this.columnDateCreated,
            this.columnDateModified,
            this.columnLastAccessed,
            this.columnMove,
            this.columnDelete});
            this.dgvFiles.ContextMenuStrip = this.ctmDataGridView;
            this.dgvFiles.Enabled = false;
            this.hpHelper.SetHelpKeyword(this.dgvFiles, "Files");
            this.hpHelper.SetHelpNavigator(this.dgvFiles, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.dgvFiles.Location = new System.Drawing.Point(6, 404);
            this.dgvFiles.Name = "dgvFiles";
            this.dgvFiles.ReadOnly = true;
            this.dgvFiles.RowHeadersVisible = false;
            this.dgvFiles.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.hpHelper.SetShowHelp(this.dgvFiles, true);
            this.dgvFiles.Size = new System.Drawing.Size(1262, 244);
            this.dgvFiles.TabIndex = 15;
            this.dgvFiles.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFiles_CellContentClick);
            this.dgvFiles.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvFiles_CellMouseDown);
            this.dgvFiles.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvFiles_ColumnHeaderMouseClick);
            this.dgvFiles.Click += new System.EventHandler(this.dgvFiles_Click);
            // 
            // columnFolder
            // 
            this.columnFolder.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.columnFolder.HeaderText = "Content of \'Folder\'";
            this.columnFolder.MinimumWidth = 10;
            this.columnFolder.Name = "columnFolder";
            this.columnFolder.ReadOnly = true;
            // 
            // columnExtension
            // 
            this.columnExtension.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.columnExtension.HeaderText = "Extension";
            this.columnExtension.MinimumWidth = 10;
            this.columnExtension.Name = "columnExtension";
            this.columnExtension.ReadOnly = true;
            this.columnExtension.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.columnExtension.Width = 78;
            // 
            // columnSize
            // 
            this.columnSize.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnSize.HeaderText = "Size";
            this.columnSize.MinimumWidth = 10;
            this.columnSize.Name = "columnSize";
            this.columnSize.ReadOnly = true;
            this.columnSize.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.columnSize.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.columnSize.Width = 75;
            // 
            // columnDateCreated
            // 
            this.columnDateCreated.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnDateCreated.HeaderText = "Date created";
            this.columnDateCreated.MinimumWidth = 10;
            this.columnDateCreated.Name = "columnDateCreated";
            this.columnDateCreated.ReadOnly = true;
            this.columnDateCreated.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.columnDateCreated.Width = 125;
            // 
            // columnDateModified
            // 
            this.columnDateModified.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnDateModified.HeaderText = "Date modified";
            this.columnDateModified.MinimumWidth = 10;
            this.columnDateModified.Name = "columnDateModified";
            this.columnDateModified.ReadOnly = true;
            this.columnDateModified.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.columnDateModified.Width = 125;
            // 
            // columnLastAccessed
            // 
            this.columnLastAccessed.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnLastAccessed.HeaderText = "Last Accessed";
            this.columnLastAccessed.MinimumWidth = 10;
            this.columnLastAccessed.Name = "columnLastAccessed";
            this.columnLastAccessed.ReadOnly = true;
            this.columnLastAccessed.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.columnLastAccessed.Width = 125;
            // 
            // columnMove
            // 
            this.columnMove.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnMove.HeaderText = "Move?";
            this.columnMove.MinimumWidth = 10;
            this.columnMove.Name = "columnMove";
            this.columnMove.ReadOnly = true;
            this.columnMove.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.columnMove.Width = 60;
            // 
            // columnDelete
            // 
            this.columnDelete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnDelete.HeaderText = "Delete?";
            this.columnDelete.MinimumWidth = 10;
            this.columnDelete.Name = "columnDelete";
            this.columnDelete.ReadOnly = true;
            this.columnDelete.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.columnDelete.Width = 60;
            // 
            // ctmDataGridView
            // 
            this.ctmDataGridView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmMove,
            this.tsmDelete,
            this.tsmOpenFile});
            this.ctmDataGridView.Name = "ctmGridView";
            this.ctmDataGridView.Size = new System.Drawing.Size(123, 70);
            this.ctmDataGridView.Opening += new System.ComponentModel.CancelEventHandler(this.ctmDataGridView_Opening);
            // 
            // tsmMove
            // 
            this.tsmMove.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmSelectMoveOne,
            this.tsmUnselectMoveOne,
            this.tsmSelectAllMove,
            this.tsmUnselectAllMove});
            this.tsmMove.Name = "tsmMove";
            this.tsmMove.Size = new System.Drawing.Size(122, 22);
            this.tsmMove.Text = "Move";
            // 
            // tsmSelectMoveOne
            // 
            this.tsmSelectMoveOne.Name = "tsmSelectMoveOne";
            this.tsmSelectMoveOne.Size = new System.Drawing.Size(201, 22);
            this.tsmSelectMoveOne.Text = "Set this to be moved";
            this.tsmSelectMoveOne.Click += new System.EventHandler(this.tsmSelectMoveOne_Click);
            // 
            // tsmUnselectMoveOne
            // 
            this.tsmUnselectMoveOne.Name = "tsmUnselectMoveOne";
            this.tsmUnselectMoveOne.Size = new System.Drawing.Size(201, 22);
            this.tsmUnselectMoveOne.Text = "Remove this from move";
            this.tsmUnselectMoveOne.Click += new System.EventHandler(this.tsmUnselectMoveOne_Click);
            // 
            // tsmSelectAllMove
            // 
            this.tsmSelectAllMove.Name = "tsmSelectAllMove";
            this.tsmSelectAllMove.Size = new System.Drawing.Size(201, 22);
            this.tsmSelectAllMove.Text = "Select all to be moved";
            this.tsmSelectAllMove.Click += new System.EventHandler(this.tsmSelectAllMove_Click);
            // 
            // tsmUnselectAllMove
            // 
            this.tsmUnselectAllMove.Name = "tsmUnselectAllMove";
            this.tsmUnselectAllMove.Size = new System.Drawing.Size(201, 22);
            this.tsmUnselectAllMove.Text = "Remove all from move";
            this.tsmUnselectAllMove.Click += new System.EventHandler(this.tsmUnselectAllMove_Click);
            // 
            // tsmDelete
            // 
            this.tsmDelete.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmDeleteOne,
            this.tsmUncheckDelOne,
            this.tsmSelectAllDel,
            this.tsmUncheckDelAll});
            this.tsmDelete.Name = "tsmDelete";
            this.tsmDelete.Size = new System.Drawing.Size(122, 22);
            this.tsmDelete.Text = "Deletion";
            // 
            // tsmDeleteOne
            // 
            this.tsmDeleteOne.Name = "tsmDeleteOne";
            this.tsmDeleteOne.Size = new System.Drawing.Size(214, 22);
            this.tsmDeleteOne.Text = "Set this for deletion";
            this.tsmDeleteOne.Click += new System.EventHandler(this.tsmDeleteOne_Click);
            // 
            // tsmUncheckDelOne
            // 
            this.tsmUncheckDelOne.Name = "tsmUncheckDelOne";
            this.tsmUncheckDelOne.Size = new System.Drawing.Size(214, 22);
            this.tsmUncheckDelOne.Text = "Remove this from deletion";
            this.tsmUncheckDelOne.Click += new System.EventHandler(this.tsmUncheckDelOne_Click);
            // 
            // tsmSelectAllDel
            // 
            this.tsmSelectAllDel.Name = "tsmSelectAllDel";
            this.tsmSelectAllDel.Size = new System.Drawing.Size(214, 22);
            this.tsmSelectAllDel.Text = "Select all for deletion";
            this.tsmSelectAllDel.Click += new System.EventHandler(this.tsmSelectAllDel_Click);
            // 
            // tsmUncheckDelAll
            // 
            this.tsmUncheckDelAll.Name = "tsmUncheckDelAll";
            this.tsmUncheckDelAll.Size = new System.Drawing.Size(214, 22);
            this.tsmUncheckDelAll.Text = "Remove all from deletion";
            this.tsmUncheckDelAll.Click += new System.EventHandler(this.tsmUncheckDelAll_Click);
            // 
            // tsmOpenFile
            // 
            this.tsmOpenFile.Name = "tsmOpenFile";
            this.tsmOpenFile.Size = new System.Drawing.Size(122, 22);
            this.tsmOpenFile.Text = "Open file";
            this.tsmOpenFile.Click += new System.EventHandler(this.tsmOpenFile_Click);
            // 
            // cmdMoveFolder
            // 
            this.cmdMoveFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hpHelper.SetHelpKeyword(this.cmdMoveFolder, "Moving");
            this.hpHelper.SetHelpNavigator(this.cmdMoveFolder, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.cmdMoveFolder.Location = new System.Drawing.Point(752, 272);
            this.cmdMoveFolder.Name = "cmdMoveFolder";
            this.hpHelper.SetShowHelp(this.cmdMoveFolder, false);
            this.cmdMoveFolder.Size = new System.Drawing.Size(161, 38);
            this.cmdMoveFolder.TabIndex = 4;
            this.cmdMoveFolder.Text = "Move folder";
            this.tipLabel.SetToolTip(this.cmdMoveFolder, "Moves the chosen folder to a new location");
            this.cmdMoveFolder.UseVisualStyleBackColor = true;
            this.cmdMoveFolder.Click += new System.EventHandler(this.cmdMoveFolder_Click);
            // 
            // lblProgress
            // 
            this.lblProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProgress.Location = new System.Drawing.Point(1, 711);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(1280, 13);
            this.lblProgress.TabIndex = 2;
            this.lblProgress.Text = "Waiting for input...";
            // 
            // bwScan
            // 
            this.bwScan.WorkerReportsProgress = true;
            this.bwScan.WorkerSupportsCancellation = true;
            this.bwScan.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwScan_DoWork);
            this.bwScan.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwScan_ProgressChanged);
            this.bwScan.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwScan_RunWorkerCompleted);
            // 
            // hpHelper
            // 
            this.hpHelper.HelpNamespace = "";
            // 
            // fdbPath
            // 
            this.fdbPath.Description = "Choose a path to be scanned";
            this.fdbPath.ShowNewFolderButton = false;
            // 
            // frmCleanMyFolder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 730);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.tabWindows);
            this.Controls.Add(this.menuClean);
            this.hpHelper.SetHelpKeyword(this, "");
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuClean;
            this.Name = "frmCleanMyFolder";
            this.hpHelper.SetShowHelp(this, true);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CleanMyFolder";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCleanMyFolder_FormClosing);
            this.Load += new System.EventHandler(this.frmCleanMyFolder_Load);
            this.menuClean.ResumeLayout(false);
            this.menuClean.PerformLayout();
            this.tabWindows.ResumeLayout(false);
            this.tabMenu.ResumeLayout(false);
            this.tabMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericSize)).EndInit();
            this.tabResult.ResumeLayout(false);
            this.tabResult.PerformLayout();
            this.ctmTreeView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiles)).EndInit();
            this.ctmDataGridView.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuClean;
        private System.Windows.Forms.ToolStripMenuItem tsmFile;
        private System.Windows.Forms.ToolStripMenuItem tsmHelp;
        private System.Windows.Forms.TabControl tabWindows;
        private System.Windows.Forms.TabPage tabMenu;
        private System.Windows.Forms.TabPage tabResult;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Label lblDateValue;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Label lblExtension;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.NumericUpDown numericSize;
        private System.Windows.Forms.ComboBox cmbDateType;
        private System.Windows.Forms.ComboBox cmbDateValue;
        private System.Windows.Forms.ComboBox cmbSize;
        private System.Windows.Forms.ComboBox cmbExtension;
        private System.Windows.Forms.CheckBox chkIncludeDateType;
        private System.Windows.Forms.CheckBox chkIncludeDate;
        private System.Windows.Forms.CheckBox chkIncludeSize;
        private System.Windows.Forms.CheckBox chkIncludeExtension;
        private System.Windows.Forms.Button cmdStartScan;
        private System.Windows.Forms.Button cmdSetPath;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dgvFiles;
        private System.Windows.Forms.ListBox lstInformation;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Label lblChosenFolder;
        private System.Windows.Forms.Label lblFolderSize;
        private System.Windows.Forms.Label lblSizeEarned;
        private System.Windows.Forms.Label lblDriveSize;
        private System.Windows.Forms.TreeView treeviewFolders;
        private System.Windows.Forms.Button cmdDelFilesPermanently;
        private System.Windows.Forms.Button cmdMoveFilesRecycleBin;
        private System.Windows.Forms.Button cmdMoveFolder;
        private System.Windows.Forms.Button cmdMoveFolderRecycleBin;
        private System.Windows.Forms.Button cmdDelFoldersPermanently;
        private System.Windows.Forms.Button cmdMoveFiles;
        private System.Windows.Forms.ToolStripMenuItem tsmStartFullscan;
        private System.Windows.Forms.ToolStripMenuItem tsmSpecifiedScan;
        private System.Windows.Forms.ToolStripMenuItem tsmExit;
        private System.Windows.Forms.ToolStripMenuItem tsmShowHelp;
        private System.Windows.Forms.ToolStripMenuItem tsmAbout;
        private System.Windows.Forms.Button cmdOpenFolder;
        private System.ComponentModel.BackgroundWorker bwScan;
        private System.Windows.Forms.ContextMenuStrip ctmTreeView;
        private System.Windows.Forms.ToolStripMenuItem tsmExpandClose;
        private System.Windows.Forms.ToolStripMenuItem tsmExpandAll;
        private System.Windows.Forms.ToolStripMenuItem tsmCloseAll;
        private System.Windows.Forms.ToolStripMenuItem tsmExpandSelected;
        private System.Windows.Forms.ToolStripMenuItem tsmCloseSelected;
        private System.Windows.Forms.ToolStripMenuItem tsmOpenFolder;
        private System.Windows.Forms.ToolStripMenuItem tsmViewFoldersInfo;
        private System.Windows.Forms.ToolTip tipLabel;
        private System.Windows.Forms.FolderBrowserDialog fdbPath;
        private System.Windows.Forms.ContextMenuStrip ctmDataGridView;
        private System.Windows.Forms.ToolStripMenuItem tsmMove;
        private System.Windows.Forms.ToolStripMenuItem tsmSelectMoveOne;
        private System.Windows.Forms.ToolStripMenuItem tsmUnselectMoveOne;
        private System.Windows.Forms.ToolStripMenuItem tsmSelectAllMove;
        private System.Windows.Forms.ToolStripMenuItem tsmUnselectAllMove;
        private System.Windows.Forms.ToolStripMenuItem tsmDelete;
        private System.Windows.Forms.ToolStripMenuItem tsmDeleteOne;
        private System.Windows.Forms.ToolStripMenuItem tsmUncheckDelOne;
        private System.Windows.Forms.ToolStripMenuItem tsmSelectAllDel;
        private System.Windows.Forms.ToolStripMenuItem tsmUncheckDelAll;
        private System.Windows.Forms.ToolStripMenuItem tsmOpenFile;
        private System.Windows.Forms.ToolStripMenuItem tsmOpenLog;
        private System.Windows.Forms.Button cmdOpenFile;
        private System.Windows.Forms.ToolStripMenuItem tsmScan;
        private System.Windows.Forms.ToolStripMenuItem tsmFullScan;
        private System.Windows.Forms.ToolStripMenuItem tsmScanFolderSpecified;
        private System.Windows.Forms.ToolStripMenuItem tsmNewScan;
        private System.Windows.Forms.Button cmdReset;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.Label lblFilesOptions;
        private System.Windows.Forms.Label lblFolderOptions;
        private System.Windows.Forms.CheckBox chkIncludeSubFolders;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnFolder;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnExtension;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnDateCreated;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnDateModified;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnLastAccessed;
        private System.Windows.Forms.DataGridViewCheckBoxColumn columnMove;
        private System.Windows.Forms.DataGridViewCheckBoxColumn columnDelete;
        private System.Windows.Forms.TextBox txtExtension;
        private System.Windows.Forms.Label lblUserExtension;
        private System.Windows.Forms.HelpProvider hpHelper;
        private System.Windows.Forms.DateTimePicker dtpSetDate;
    }
}

