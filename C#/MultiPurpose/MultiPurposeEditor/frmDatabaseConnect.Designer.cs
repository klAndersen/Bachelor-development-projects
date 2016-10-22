namespace MultiPurposeEditor {
    partial class frmDatabaseConnect {
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
            this.txtQuery = new System.Windows.Forms.RichTextBox();
            this.tipHelp = new System.Windows.Forms.ToolTip(this.components);
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdAddParameter = new System.Windows.Forms.Button();
            this.cmbParameter = new System.Windows.Forms.ComboBox();
            this.lblParameter = new System.Windows.Forms.Label();
            this.cmdAddTable = new System.Windows.Forms.Button();
            this.cmdAddDatabase = new System.Windows.Forms.Button();
            this.bwLoading = new System.ComponentModel.BackgroundWorker();
            this.dgvContent = new System.Windows.Forms.DataGridView();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmdRunQuery = new System.Windows.Forms.Button();
            this.lblSporring = new System.Windows.Forms.Label();
            this.cmbServer = new System.Windows.Forms.ComboBox();
            this.lblServer = new System.Windows.Forms.Label();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.cmdLogIn = new System.Windows.Forms.Button();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblPwd = new System.Windows.Forms.Label();
            this.cmdUpdateDatabase = new System.Windows.Forms.Button();
            this.cmdLogOut = new System.Windows.Forms.Button();
            this.lblDB = new System.Windows.Forms.Label();
            this.cmbTable = new System.Windows.Forms.ComboBox();
            this.cmbDatabase = new System.Windows.Forms.ComboBox();
            this.lblTabell = new System.Windows.Forms.Label();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvContent)).BeginInit();
            this.SuspendLayout();
            // 
            // txtQuery
            // 
            this.txtQuery.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuery.Location = new System.Drawing.Point(15, 182);
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.Size = new System.Drawing.Size(616, 131);
            this.txtQuery.TabIndex = 13;
            this.txtQuery.Text = "";
            // 
            // cmdClose
            // 
            this.cmdClose.BackColor = System.Drawing.Color.PeachPuff;
            this.cmdClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.Location = new System.Drawing.Point(263, 334);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(205, 27);
            this.cmdClose.TabIndex = 5;
            this.cmdClose.Text = "Close this window";
            this.cmdClose.UseVisualStyleBackColor = false;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdAddParameter
            // 
            this.cmdAddParameter.BackColor = System.Drawing.Color.PeachPuff;
            this.cmdAddParameter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAddParameter.Location = new System.Drawing.Point(511, 111);
            this.cmdAddParameter.Name = "cmdAddParameter";
            this.cmdAddParameter.Size = new System.Drawing.Size(140, 29);
            this.cmdAddParameter.TabIndex = 12;
            this.cmdAddParameter.Text = "Add parameters";
            this.cmdAddParameter.UseVisualStyleBackColor = false;
            this.cmdAddParameter.Click += new System.EventHandler(this.cmdAddParameter_Click);
            // 
            // cmbParameter
            // 
            this.cmbParameter.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.cmbParameter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbParameter.FormattingEnabled = true;
            this.cmbParameter.Location = new System.Drawing.Point(11, 114);
            this.cmbParameter.Name = "cmbParameter";
            this.cmbParameter.Size = new System.Drawing.Size(494, 24);
            this.cmbParameter.TabIndex = 11;
            // 
            // lblParameter
            // 
            this.lblParameter.AutoSize = true;
            this.lblParameter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParameter.Location = new System.Drawing.Point(9, 95);
            this.lblParameter.Name = "lblParameter";
            this.lblParameter.Size = new System.Drawing.Size(163, 16);
            this.lblParameter.TabIndex = 24;
            this.lblParameter.Text = "Add a parameter to query:";
            // 
            // cmdAddTable
            // 
            this.cmdAddTable.BackColor = System.Drawing.Color.PeachPuff;
            this.cmdAddTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAddTable.Location = new System.Drawing.Point(365, 48);
            this.cmdAddTable.Name = "cmdAddTable";
            this.cmdAddTable.Size = new System.Drawing.Size(140, 30);
            this.cmdAddTable.TabIndex = 10;
            this.cmdAddTable.Text = "Add table";
            this.cmdAddTable.UseVisualStyleBackColor = false;
            this.cmdAddTable.Click += new System.EventHandler(this.cmdAddTable_Click);
            // 
            // cmdAddDatabase
            // 
            this.cmdAddDatabase.BackColor = System.Drawing.Color.PeachPuff;
            this.cmdAddDatabase.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAddDatabase.Location = new System.Drawing.Point(365, 8);
            this.cmdAddDatabase.Name = "cmdAddDatabase";
            this.cmdAddDatabase.Size = new System.Drawing.Size(140, 30);
            this.cmdAddDatabase.TabIndex = 7;
            this.cmdAddDatabase.Text = "Add database";
            this.cmdAddDatabase.UseVisualStyleBackColor = false;
            this.cmdAddDatabase.Click += new System.EventHandler(this.cmdAddDatabase_Click);
            // 
            // bwLoading
            // 
            this.bwLoading.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwLoading_DoWork);
            this.bwLoading.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwLoading_ProgressChanged);
            this.bwLoading.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwLoading_RunWorkerCompleted);
            // 
            // dgvContent
            // 
            this.dgvContent.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dgvContent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvContent.Location = new System.Drawing.Point(15, 321);
            this.dgvContent.Name = "dgvContent";
            this.dgvContent.Size = new System.Drawing.Size(759, 216);
            this.dgvContent.TabIndex = 17;
            // 
            // cmdCancel
            // 
            this.cmdCancel.BackColor = System.Drawing.Color.PeachPuff;
            this.cmdCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCancel.Location = new System.Drawing.Point(637, 214);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(142, 29);
            this.cmdCancel.TabIndex = 15;
            this.cmdCancel.Text = "Cancel operations";
            this.cmdCancel.UseVisualStyleBackColor = false;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(12, 540);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(759, 21);
            this.lblStatus.TabIndex = 26;
            this.lblStatus.Text = "Status: Waiting for user login";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmdRunQuery
            // 
            this.cmdRunQuery.BackColor = System.Drawing.Color.PeachPuff;
            this.cmdRunQuery.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRunQuery.Location = new System.Drawing.Point(637, 178);
            this.cmdRunQuery.Name = "cmdRunQuery";
            this.cmdRunQuery.Size = new System.Drawing.Size(141, 31);
            this.cmdRunQuery.TabIndex = 14;
            this.cmdRunQuery.Text = "Run query";
            this.cmdRunQuery.UseVisualStyleBackColor = false;
            this.cmdRunQuery.Click += new System.EventHandler(this.cmdRunQuery_Click);
            // 
            // lblSporring
            // 
            this.lblSporring.AutoSize = true;
            this.lblSporring.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSporring.Location = new System.Drawing.Point(12, 159);
            this.lblSporring.Name = "lblSporring";
            this.lblSporring.Size = new System.Drawing.Size(177, 16);
            this.lblSporring.TabIndex = 25;
            this.lblSporring.Text = "Enter query to be excecuted:";
            // 
            // cmbServer
            // 
            this.cmbServer.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.cmbServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbServer.FormattingEnabled = true;
            this.cmbServer.Location = new System.Drawing.Point(346, 253);
            this.cmbServer.Name = "cmbServer";
            this.cmbServer.Size = new System.Drawing.Size(121, 24);
            this.cmbServer.TabIndex = 3;
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServer.Location = new System.Drawing.Point(262, 260);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(51, 16);
            this.lblServer.TabIndex = 21;
            this.lblServer.Text = "Server:";
            // 
            // txtPwd
            // 
            this.txtPwd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPwd.Location = new System.Drawing.Point(346, 185);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.Size = new System.Drawing.Size(121, 22);
            this.txtPwd.TabIndex = 1;
            // 
            // txtUser
            // 
            this.txtUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUser.Location = new System.Drawing.Point(346, 148);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(121, 22);
            this.txtUser.TabIndex = 0;
            // 
            // cmdLogIn
            // 
            this.cmdLogIn.BackColor = System.Drawing.Color.PeachPuff;
            this.cmdLogIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdLogIn.Location = new System.Drawing.Point(262, 301);
            this.cmdLogIn.Name = "cmdLogIn";
            this.cmdLogIn.Size = new System.Drawing.Size(205, 27);
            this.cmdLogIn.TabIndex = 4;
            this.cmdLogIn.Text = "&Log in";
            this.cmdLogIn.UseVisualStyleBackColor = false;
            this.cmdLogIn.Click += new System.EventHandler(this.cmdLogIn_Click);
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.Location = new System.Drawing.Point(262, 154);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(74, 16);
            this.lblUser.TabIndex = 18;
            this.lblUser.Text = "Username:";
            // 
            // lblPwd
            // 
            this.lblPwd.AutoSize = true;
            this.lblPwd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPwd.Location = new System.Drawing.Point(262, 191);
            this.lblPwd.Name = "lblPwd";
            this.lblPwd.Size = new System.Drawing.Size(71, 16);
            this.lblPwd.TabIndex = 19;
            this.lblPwd.Text = "Password:";
            // 
            // cmdUpdateDatabase
            // 
            this.cmdUpdateDatabase.BackColor = System.Drawing.Color.PeachPuff;
            this.cmdUpdateDatabase.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdUpdateDatabase.Location = new System.Drawing.Point(511, 8);
            this.cmdUpdateDatabase.Name = "cmdUpdateDatabase";
            this.cmdUpdateDatabase.Size = new System.Drawing.Size(141, 30);
            this.cmdUpdateDatabase.TabIndex = 8;
            this.cmdUpdateDatabase.Text = "Update database list";
            this.cmdUpdateDatabase.UseVisualStyleBackColor = false;
            this.cmdUpdateDatabase.Click += new System.EventHandler(this.cmdUpdateDatabase_Click);
            // 
            // cmdLogOut
            // 
            this.cmdLogOut.BackColor = System.Drawing.Color.PeachPuff;
            this.cmdLogOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdLogOut.Location = new System.Drawing.Point(637, 249);
            this.cmdLogOut.Name = "cmdLogOut";
            this.cmdLogOut.Size = new System.Drawing.Size(142, 31);
            this.cmdLogOut.TabIndex = 16;
            this.cmdLogOut.Text = "Log out";
            this.cmdLogOut.UseVisualStyleBackColor = false;
            this.cmdLogOut.Click += new System.EventHandler(this.cmdLogOut_Click);
            // 
            // lblDB
            // 
            this.lblDB.AutoSize = true;
            this.lblDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDB.Location = new System.Drawing.Point(12, 15);
            this.lblDB.Name = "lblDB";
            this.lblDB.Size = new System.Drawing.Size(119, 16);
            this.lblDB.TabIndex = 22;
            this.lblDB.Text = "Choose database:";
            // 
            // cmbTable
            // 
            this.cmbTable.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.cmbTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTable.FormattingEnabled = true;
            this.cmbTable.Location = new System.Drawing.Point(137, 52);
            this.cmbTable.Name = "cmbTable";
            this.cmbTable.Size = new System.Drawing.Size(222, 24);
            this.cmbTable.TabIndex = 9;
            // 
            // cmbDatabase
            // 
            this.cmbDatabase.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.cmbDatabase.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDatabase.FormattingEnabled = true;
            this.cmbDatabase.Location = new System.Drawing.Point(137, 12);
            this.cmbDatabase.Name = "cmbDatabase";
            this.cmbDatabase.Size = new System.Drawing.Size(222, 24);
            this.cmbDatabase.TabIndex = 6;
            this.cmbDatabase.TextChanged += new System.EventHandler(this.cmbDatabase_TextChanged);
            // 
            // lblTabell
            // 
            this.lblTabell.AutoSize = true;
            this.lblTabell.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTabell.Location = new System.Drawing.Point(12, 55);
            this.lblTabell.Name = "lblTabell";
            this.lblTabell.Size = new System.Drawing.Size(91, 16);
            this.lblTabell.TabIndex = 23;
            this.lblTabell.Text = "Choose table:";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPort.Location = new System.Drawing.Point(263, 220);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(35, 16);
            this.lblPort.TabIndex = 20;
            this.lblPort.Text = "Port:";
            // 
            // txtPort
            // 
            this.txtPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPort.Location = new System.Drawing.Point(346, 220);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(121, 22);
            this.txtPort.TabIndex = 2;
            // 
            // frmDatabaseConnect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.ClientSize = new System.Drawing.Size(793, 568);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.txtQuery);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdAddParameter);
            this.Controls.Add(this.cmbParameter);
            this.Controls.Add(this.lblParameter);
            this.Controls.Add(this.cmdAddTable);
            this.Controls.Add(this.cmdAddDatabase);
            this.Controls.Add(this.dgvContent);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.cmdRunQuery);
            this.Controls.Add(this.lblSporring);
            this.Controls.Add(this.cmbServer);
            this.Controls.Add(this.lblServer);
            this.Controls.Add(this.txtPwd);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.cmdLogIn);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.lblPwd);
            this.Controls.Add(this.cmdUpdateDatabase);
            this.Controls.Add(this.cmdLogOut);
            this.Controls.Add(this.lblDB);
            this.Controls.Add(this.cmbTable);
            this.Controls.Add(this.cmbDatabase);
            this.Controls.Add(this.lblTabell);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "frmDatabaseConnect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MySQL Connection";
            this.Load += new System.EventHandler(this.frmDatabaseConnect_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvContent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtQuery;
        private System.Windows.Forms.ToolTip tipHelp;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdAddParameter;
        private System.Windows.Forms.ComboBox cmbParameter;
        private System.Windows.Forms.Label lblParameter;
        private System.Windows.Forms.Button cmdAddTable;
        private System.Windows.Forms.Button cmdAddDatabase;
        private System.ComponentModel.BackgroundWorker bwLoading;
        private System.Windows.Forms.DataGridView dgvContent;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button cmdRunQuery;
        private System.Windows.Forms.Label lblSporring;
        private System.Windows.Forms.ComboBox cmbServer;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Button cmdLogIn;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblPwd;
        private System.Windows.Forms.Button cmdUpdateDatabase;
        private System.Windows.Forms.Button cmdLogOut;
        private System.Windows.Forms.Label lblDB;
        private System.Windows.Forms.ComboBox cmbTable;
        private System.Windows.Forms.ComboBox cmbDatabase;
        private System.Windows.Forms.Label lblTabell;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox txtPort;
    }
}