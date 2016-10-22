namespace guiModellTog {
    partial class frmAbout {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAbout));
            this.txtDescription = new System.Windows.Forms.RichTextBox();
            this.lblSeperator1 = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.lblProductName = new System.Windows.Forms.Label();
            this.cmdShowTerms = new System.Windows.Forms.Button();
            this.cmdShowLicense = new System.Windows.Forms.Button();
            this.cmdOk = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(12, 158);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.Size = new System.Drawing.Size(436, 196);
            this.txtDescription.TabIndex = 19;
            this.txtDescription.TabStop = false;
            this.txtDescription.Text = "";
            // 
            // lblSeperator1
            // 
            this.lblSeperator1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSeperator1.Location = new System.Drawing.Point(12, 140);
            this.lblSeperator1.Name = "lblSeperator1";
            this.lblSeperator1.Size = new System.Drawing.Size(436, 10);
            this.lblSeperator1.TabIndex = 18;
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(146, 48);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(42, 13);
            this.lblVersion.TabIndex = 15;
            this.lblVersion.Text = "Version";
            // 
            // lblCopyright
            // 
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.Location = new System.Drawing.Point(146, 79);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(51, 13);
            this.lblCopyright.TabIndex = 16;
            this.lblCopyright.Text = "Copyright";
            // 
            // lblProductName
            // 
            this.lblProductName.AutoSize = true;
            this.lblProductName.Location = new System.Drawing.Point(147, 13);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(75, 13);
            this.lblProductName.TabIndex = 14;
            this.lblProductName.Text = "Product Name";
            // 
            // cmdShowTerms
            // 
            this.cmdShowTerms.Location = new System.Drawing.Point(12, 360);
            this.cmdShowTerms.Name = "cmdShowTerms";
            this.cmdShowTerms.Size = new System.Drawing.Size(162, 23);
            this.cmdShowTerms.TabIndex = 10;
            this.cmdShowTerms.Text = "Vis &Terms and conditions";
            this.cmdShowTerms.UseVisualStyleBackColor = true;
            this.cmdShowTerms.Click += new System.EventHandler(this.cmdShowTerms_Click);
            // 
            // cmdShowLicense
            // 
            this.cmdShowLicense.Location = new System.Drawing.Point(180, 360);
            this.cmdShowLicense.Name = "cmdShowLicense";
            this.cmdShowLicense.Size = new System.Drawing.Size(112, 23);
            this.cmdShowLicense.TabIndex = 12;
            this.cmdShowLicense.Text = "Vis &Lisens";
            this.cmdShowLicense.UseVisualStyleBackColor = true;
            this.cmdShowLicense.Click += new System.EventHandler(this.cmdShowLicense_Click);
            // 
            // cmdOk
            // 
            this.cmdOk.Location = new System.Drawing.Point(373, 360);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(75, 23);
            this.cmdOk.TabIndex = 13;
            this.cmdOk.Text = "&OK";
            this.cmdOk.UseVisualStyleBackColor = true;
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.ImageLocation = "";
            this.pictureBox1.Location = new System.Drawing.Point(12, 18);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(128, 85);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // frmAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 400);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblSeperator1);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblCopyright);
            this.Controls.Add(this.lblProductName);
            this.Controls.Add(this.cmdShowTerms);
            this.Controls.Add(this.cmdShowLicense);
            this.Controls.Add(this.cmdOk);
            this.Controls.Add(this.pictureBox1);
            this.Name = "frmAbout";
            this.Text = "frmAbout";
            this.Load += new System.EventHandler(this.frmAbout_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtDescription;
        private System.Windows.Forms.Label lblSeperator1;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblCopyright;
        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.Button cmdShowTerms;
        private System.Windows.Forms.Button cmdShowLicense;
        private System.Windows.Forms.Button cmdOk;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}