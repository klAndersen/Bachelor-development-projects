namespace CleanMyFolder {
    partial class frmProperties {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProperties));
            this.lblSeperator1 = new System.Windows.Forms.Label();
            this.lblInfoName = new System.Windows.Forms.Label();
            this.lblFolderName = new System.Windows.Forms.Label();
            this.lblSizeScanned = new System.Windows.Forms.Label();
            this.lblInfoSize = new System.Windows.Forms.Label();
            this.lblNumFilesScan = new System.Windows.Forms.Label();
            this.lblInfoNumFilesScan = new System.Windows.Forms.Label();
            this.lblInfoLastAccessed = new System.Windows.Forms.Label();
            this.lblLastAccessed = new System.Windows.Forms.Label();
            this.lblInfoModified = new System.Windows.Forms.Label();
            this.lblDateModified = new System.Windows.Forms.Label();
            this.lblDateCreated = new System.Windows.Forms.Label();
            this.lblInfoCreated = new System.Windows.Forms.Label();
            this.lblSeperator2 = new System.Windows.Forms.Label();
            this.cmdClose = new System.Windows.Forms.Button();
            this.tipProperties = new System.Windows.Forms.ToolTip(this.components);
            this.lblInfoTotFiles = new System.Windows.Forms.Label();
            this.lblTotalNumFiles = new System.Windows.Forms.Label();
            this.lblInfoContains = new System.Windows.Forms.Label();
            this.lblContains = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.lblInfoFolderLocation = new System.Windows.Forms.Label();
            this.lblSeperator3 = new System.Windows.Forms.Label();
            this.cmdOpen = new System.Windows.Forms.Button();
            this.tabFolder = new System.Windows.Forms.TabControl();
            this.tabProperties = new System.Windows.Forms.TabPage();
            this.bwCountFiles = new System.ComponentModel.BackgroundWorker();
            this.hpHelper = new System.Windows.Forms.HelpProvider();
            this.tabFolder.SuspendLayout();
            this.tabProperties.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSeperator1
            // 
            this.lblSeperator1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSeperator1.Location = new System.Drawing.Point(9, 92);
            this.lblSeperator1.Name = "lblSeperator1";
            this.lblSeperator1.Size = new System.Drawing.Size(345, 2);
            this.lblSeperator1.TabIndex = 0;
            // 
            // lblInfoName
            // 
            this.lblInfoName.AutoSize = true;
            this.lblInfoName.Location = new System.Drawing.Point(13, 34);
            this.lblInfoName.Name = "lblInfoName";
            this.lblInfoName.Size = new System.Drawing.Size(73, 13);
            this.lblInfoName.TabIndex = 1;
            this.lblInfoName.Text = "Folders name:";
            // 
            // lblFolderName
            // 
            this.lblFolderName.AutoSize = true;
            this.lblFolderName.Location = new System.Drawing.Point(109, 34);
            this.lblFolderName.Name = "lblFolderName";
            this.lblFolderName.Size = new System.Drawing.Size(94, 13);
            this.lblFolderName.TabIndex = 2;
            this.lblFolderName.Text = "name  comes here";
            // 
            // lblSizeScanned
            // 
            this.lblSizeScanned.AutoSize = true;
            this.lblSizeScanned.Location = new System.Drawing.Point(143, 111);
            this.lblSizeScanned.Name = "lblSizeScanned";
            this.lblSizeScanned.Size = new System.Drawing.Size(139, 13);
            this.lblSizeScanned.TabIndex = 3;
            this.lblSizeScanned.Text = "size of folder based on scan";
            this.tipProperties.SetToolTip(this.lblSizeScanned, "The size of the folder based on \r\nthe scanned files and subfolders.\r\n");
            // 
            // lblInfoSize
            // 
            this.lblInfoSize.AutoSize = true;
            this.lblInfoSize.Location = new System.Drawing.Point(13, 111);
            this.lblInfoSize.Name = "lblInfoSize";
            this.lblInfoSize.Size = new System.Drawing.Size(103, 13);
            this.lblInfoSize.TabIndex = 4;
            this.lblInfoSize.Text = "Size based on scan:";
            this.tipProperties.SetToolTip(this.lblInfoSize, "The size of the folder based on \r\nthe scanned files and subfolders.\r\n");
            // 
            // lblNumFilesScan
            // 
            this.lblNumFilesScan.AutoSize = true;
            this.lblNumFilesScan.Location = new System.Drawing.Point(143, 198);
            this.lblNumFilesScan.Name = "lblNumFilesScan";
            this.lblNumFilesScan.Size = new System.Drawing.Size(148, 13);
            this.lblNumFilesScan.TabIndex = 5;
            this.lblNumFilesScan.Text = "number of files based on scan";
            this.tipProperties.SetToolTip(this.lblNumFilesScan, "The scanned number of files this folder contains \r\n(excluding hidden and system f" +
        "iles).");
            // 
            // lblInfoNumFilesScan
            // 
            this.lblInfoNumFilesScan.AutoSize = true;
            this.lblInfoNumFilesScan.Location = new System.Drawing.Point(13, 198);
            this.lblInfoNumFilesScan.Name = "lblInfoNumFilesScan";
            this.lblInfoNumFilesScan.Size = new System.Drawing.Size(124, 13);
            this.lblInfoNumFilesScan.TabIndex = 6;
            this.lblInfoNumFilesScan.Text = "Number of files scanned:";
            this.tipProperties.SetToolTip(this.lblInfoNumFilesScan, "The scanned number of files this folder contains \r\n(excluding hidden and system f" +
        "iles).");
            // 
            // lblInfoLastAccessed
            // 
            this.lblInfoLastAccessed.AutoSize = true;
            this.lblInfoLastAccessed.Location = new System.Drawing.Point(13, 295);
            this.lblInfoLastAccessed.Name = "lblInfoLastAccessed";
            this.lblInfoLastAccessed.Size = new System.Drawing.Size(79, 13);
            this.lblInfoLastAccessed.TabIndex = 12;
            this.lblInfoLastAccessed.Text = "Last accessed:";
            // 
            // lblLastAccessed
            // 
            this.lblLastAccessed.AutoSize = true;
            this.lblLastAccessed.Location = new System.Drawing.Point(109, 295);
            this.lblLastAccessed.Name = "lblLastAccessed";
            this.lblLastAccessed.Size = new System.Drawing.Size(157, 13);
            this.lblLastAccessed.TabIndex = 11;
            this.lblLastAccessed.Text = "date last accessed  comes here";
            // 
            // lblInfoModified
            // 
            this.lblInfoModified.AutoSize = true;
            this.lblInfoModified.Location = new System.Drawing.Point(13, 270);
            this.lblInfoModified.Name = "lblInfoModified";
            this.lblInfoModified.Size = new System.Drawing.Size(75, 13);
            this.lblInfoModified.TabIndex = 10;
            this.lblInfoModified.Text = "Date modified:";
            // 
            // lblDateModified
            // 
            this.lblDateModified.AutoSize = true;
            this.lblDateModified.Location = new System.Drawing.Point(109, 270);
            this.lblDateModified.Name = "lblDateModified";
            this.lblDateModified.Size = new System.Drawing.Size(128, 13);
            this.lblDateModified.TabIndex = 9;
            this.lblDateModified.Text = "date modified comes here";
            // 
            // lblDateCreated
            // 
            this.lblDateCreated.AutoSize = true;
            this.lblDateCreated.Location = new System.Drawing.Point(109, 246);
            this.lblDateCreated.Name = "lblDateCreated";
            this.lblDateCreated.Size = new System.Drawing.Size(128, 13);
            this.lblDateCreated.TabIndex = 8;
            this.lblDateCreated.Text = "date created  comes here";
            // 
            // lblInfoCreated
            // 
            this.lblInfoCreated.AutoSize = true;
            this.lblInfoCreated.Location = new System.Drawing.Point(13, 246);
            this.lblInfoCreated.Name = "lblInfoCreated";
            this.lblInfoCreated.Size = new System.Drawing.Size(72, 13);
            this.lblInfoCreated.TabIndex = 7;
            this.lblInfoCreated.Text = "Date created:";
            // 
            // lblSeperator2
            // 
            this.lblSeperator2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSeperator2.Location = new System.Drawing.Point(9, 223);
            this.lblSeperator2.Name = "lblSeperator2";
            this.lblSeperator2.Size = new System.Drawing.Size(345, 2);
            this.lblSeperator2.TabIndex = 13;
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(16, 360);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(75, 23);
            this.cmdClose.TabIndex = 14;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // lblInfoTotFiles
            // 
            this.lblInfoTotFiles.AutoSize = true;
            this.lblInfoTotFiles.Location = new System.Drawing.Point(13, 168);
            this.lblInfoTotFiles.Name = "lblInfoTotFiles";
            this.lblInfoTotFiles.Size = new System.Drawing.Size(105, 13);
            this.lblInfoTotFiles.TabIndex = 20;
            this.lblInfoTotFiles.Text = "Total number of files:";
            this.tipProperties.SetToolTip(this.lblInfoTotFiles, "The total number of files this folder \r\ncontains (excluding hidden and system fil" +
        "es).");
            // 
            // lblTotalNumFiles
            // 
            this.lblTotalNumFiles.AutoSize = true;
            this.lblTotalNumFiles.Location = new System.Drawing.Point(143, 168);
            this.lblTotalNumFiles.Name = "lblTotalNumFiles";
            this.lblTotalNumFiles.Size = new System.Drawing.Size(170, 13);
            this.lblTotalNumFiles.TabIndex = 19;
            this.lblTotalNumFiles.Text = "number of files folder contains total";
            this.tipProperties.SetToolTip(this.lblTotalNumFiles, "The total number of files this folder \r\ncontains (excluding hidden and system fil" +
        "es).");
            // 
            // lblInfoContains
            // 
            this.lblInfoContains.AutoSize = true;
            this.lblInfoContains.Location = new System.Drawing.Point(13, 140);
            this.lblInfoContains.Name = "lblInfoContains";
            this.lblInfoContains.Size = new System.Drawing.Size(111, 13);
            this.lblInfoContains.TabIndex = 23;
            this.lblInfoContains.Text = "Contains (scan result):";
            this.tipProperties.SetToolTip(this.lblInfoContains, "The number of subfolders and files this folder contains. \r\nValues are based on sc" +
        "an.");
            // 
            // lblContains
            // 
            this.lblContains.AutoSize = true;
            this.lblContains.Location = new System.Drawing.Point(143, 140);
            this.lblContains.Name = "lblContains";
            this.lblContains.Size = new System.Drawing.Size(147, 13);
            this.lblContains.TabIndex = 22;
            this.lblContains.Text = "number of subfolders and files";
            this.tipProperties.SetToolTip(this.lblContains, "The number of subfolders and files this folder contains. \r\nValues are based on sc" +
        "an.");
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(109, 65);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(105, 13);
            this.lblLocation.TabIndex = 16;
            this.lblLocation.Text = "location  comes here";
            // 
            // lblInfoFolderLocation
            // 
            this.lblInfoFolderLocation.AutoSize = true;
            this.lblInfoFolderLocation.Location = new System.Drawing.Point(13, 65);
            this.lblInfoFolderLocation.Name = "lblInfoFolderLocation";
            this.lblInfoFolderLocation.Size = new System.Drawing.Size(84, 13);
            this.lblInfoFolderLocation.TabIndex = 15;
            this.lblInfoFolderLocation.Text = "Folders location:";
            // 
            // lblSeperator3
            // 
            this.lblSeperator3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSeperator3.Location = new System.Drawing.Point(9, 335);
            this.lblSeperator3.Name = "lblSeperator3";
            this.lblSeperator3.Size = new System.Drawing.Size(345, 2);
            this.lblSeperator3.TabIndex = 18;
            // 
            // cmdOpen
            // 
            this.cmdOpen.Location = new System.Drawing.Point(112, 360);
            this.cmdOpen.Name = "cmdOpen";
            this.cmdOpen.Size = new System.Drawing.Size(75, 23);
            this.cmdOpen.TabIndex = 21;
            this.cmdOpen.Text = "Open folder";
            this.cmdOpen.UseVisualStyleBackColor = true;
            this.cmdOpen.Click += new System.EventHandler(this.cmdOpen_Click);
            // 
            // tabFolder
            // 
            this.tabFolder.Controls.Add(this.tabProperties);
            this.tabFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabFolder.Location = new System.Drawing.Point(0, 0);
            this.tabFolder.Name = "tabFolder";
            this.tabFolder.SelectedIndex = 0;
            this.tabFolder.Size = new System.Drawing.Size(371, 433);
            this.tabFolder.TabIndex = 22;
            // 
            // tabProperties
            // 
            this.tabProperties.Controls.Add(this.lblInfoContains);
            this.tabProperties.Controls.Add(this.lblContains);
            this.tabProperties.Controls.Add(this.lblDateModified);
            this.tabProperties.Controls.Add(this.lblInfoSize);
            this.tabProperties.Controls.Add(this.cmdOpen);
            this.tabProperties.Controls.Add(this.lblSeperator1);
            this.tabProperties.Controls.Add(this.lblInfoTotFiles);
            this.tabProperties.Controls.Add(this.lblInfoName);
            this.tabProperties.Controls.Add(this.lblTotalNumFiles);
            this.tabProperties.Controls.Add(this.lblFolderName);
            this.tabProperties.Controls.Add(this.lblSeperator3);
            this.tabProperties.Controls.Add(this.lblSizeScanned);
            this.tabProperties.Controls.Add(this.lblLocation);
            this.tabProperties.Controls.Add(this.lblNumFilesScan);
            this.tabProperties.Controls.Add(this.lblInfoFolderLocation);
            this.tabProperties.Controls.Add(this.lblInfoNumFilesScan);
            this.tabProperties.Controls.Add(this.cmdClose);
            this.tabProperties.Controls.Add(this.lblInfoCreated);
            this.tabProperties.Controls.Add(this.lblSeperator2);
            this.tabProperties.Controls.Add(this.lblDateCreated);
            this.tabProperties.Controls.Add(this.lblInfoLastAccessed);
            this.tabProperties.Controls.Add(this.lblInfoModified);
            this.tabProperties.Controls.Add(this.lblLastAccessed);
            this.tabProperties.Location = new System.Drawing.Point(4, 22);
            this.tabProperties.Name = "tabProperties";
            this.tabProperties.Padding = new System.Windows.Forms.Padding(3);
            this.tabProperties.Size = new System.Drawing.Size(363, 407);
            this.tabProperties.TabIndex = 0;
            this.tabProperties.Text = "Folder properties";
            this.tabProperties.UseVisualStyleBackColor = true;
            // 
            // bwCountFiles
            // 
            this.bwCountFiles.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwCountFiles_DoWork);
            this.bwCountFiles.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwCountFiles_ProgressChanged);
            this.bwCountFiles.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwCountFiles_RunWorkerCompleted);
            // 
            // frmProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 433);
            this.Controls.Add(this.tabFolder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.hpHelper.SetHelpKeyword(this, "Folder information");
            this.hpHelper.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProperties";
            this.hpHelper.SetShowHelp(this, true);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Folder properties";
            this.Load += new System.EventHandler(this.frmFolderProperties_Load);
            this.tabFolder.ResumeLayout(false);
            this.tabProperties.ResumeLayout(false);
            this.tabProperties.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblSeperator1;
        private System.Windows.Forms.Label lblInfoName;
        private System.Windows.Forms.Label lblFolderName;
        private System.Windows.Forms.Label lblSizeScanned;
        private System.Windows.Forms.Label lblInfoSize;
        private System.Windows.Forms.Label lblNumFilesScan;
        private System.Windows.Forms.Label lblInfoNumFilesScan;
        private System.Windows.Forms.Label lblInfoLastAccessed;
        private System.Windows.Forms.Label lblLastAccessed;
        private System.Windows.Forms.Label lblInfoModified;
        private System.Windows.Forms.Label lblDateModified;
        private System.Windows.Forms.Label lblDateCreated;
        private System.Windows.Forms.Label lblInfoCreated;
        private System.Windows.Forms.Label lblSeperator2;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.ToolTip tipProperties;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Label lblInfoFolderLocation;
        private System.Windows.Forms.Label lblSeperator3;
        private System.Windows.Forms.Label lblInfoTotFiles;
        private System.Windows.Forms.Label lblTotalNumFiles;
        private System.Windows.Forms.Button cmdOpen;
        private System.Windows.Forms.TabControl tabFolder;
        private System.Windows.Forms.TabPage tabProperties;
        private System.Windows.Forms.Label lblInfoContains;
        private System.Windows.Forms.Label lblContains;
        private System.ComponentModel.BackgroundWorker bwCountFiles;
        private System.Windows.Forms.HelpProvider hpHelper;
    }
}