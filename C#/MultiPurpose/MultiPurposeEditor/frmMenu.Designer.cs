namespace MultiPurposeEditor {
    partial class frmMenu {
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
            this.cmdGenerateText = new System.Windows.Forms.Button();
            this.cmdCreateReadMe = new System.Windows.Forms.Button();
            this.cmdMySQLConnect = new System.Windows.Forms.Button();
            this.cmdAbout = new System.Windows.Forms.Button();
            this.cmdExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmdGenerateText
            // 
            this.cmdGenerateText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGenerateText.Location = new System.Drawing.Point(34, 32);
            this.cmdGenerateText.Name = "cmdGenerateText";
            this.cmdGenerateText.Size = new System.Drawing.Size(168, 30);
            this.cmdGenerateText.TabIndex = 0;
            this.cmdGenerateText.Text = "Generate text";
            this.cmdGenerateText.UseVisualStyleBackColor = true;
            this.cmdGenerateText.Click += new System.EventHandler(this.cmdGenerateText_Click);
            // 
            // cmdCreateReadMe
            // 
            this.cmdCreateReadMe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCreateReadMe.Location = new System.Drawing.Point(34, 68);
            this.cmdCreateReadMe.Name = "cmdCreateReadMe";
            this.cmdCreateReadMe.Size = new System.Drawing.Size(168, 30);
            this.cmdCreateReadMe.TabIndex = 1;
            this.cmdCreateReadMe.Text = "Make ReadMe";
            this.cmdCreateReadMe.UseVisualStyleBackColor = true;
            this.cmdCreateReadMe.Click += new System.EventHandler(this.cmdCreateReadMe_Click);
            // 
            // cmdMySQLConnect
            // 
            this.cmdMySQLConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdMySQLConnect.Location = new System.Drawing.Point(34, 104);
            this.cmdMySQLConnect.Name = "cmdMySQLConnect";
            this.cmdMySQLConnect.Size = new System.Drawing.Size(168, 30);
            this.cmdMySQLConnect.TabIndex = 2;
            this.cmdMySQLConnect.Text = "Connect to MySQL";
            this.cmdMySQLConnect.UseVisualStyleBackColor = true;
            this.cmdMySQLConnect.Click += new System.EventHandler(this.cmdMySQLConnect_Click);
            // 
            // cmdAbout
            // 
            this.cmdAbout.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAbout.Location = new System.Drawing.Point(34, 144);
            this.cmdAbout.Name = "cmdAbout";
            this.cmdAbout.Size = new System.Drawing.Size(168, 30);
            this.cmdAbout.TabIndex = 3;
            this.cmdAbout.Text = "About";
            this.cmdAbout.UseVisualStyleBackColor = true;
            this.cmdAbout.Click += new System.EventHandler(this.cmdAbout_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Location = new System.Drawing.Point(34, 180);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(168, 30);
            this.cmdExit.TabIndex = 4;
            this.cmdExit.Text = "Exit program";
            this.cmdExit.UseVisualStyleBackColor = true;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // frmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(237, 237);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.cmdAbout);
            this.Controls.Add(this.cmdMySQLConnect);
            this.Controls.Add(this.cmdCreateReadMe);
            this.Controls.Add(this.cmdGenerateText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "frmMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MultiPurpose";
            this.Load += new System.EventHandler(this.frmMenu_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdGenerateText;
        private System.Windows.Forms.Button cmdCreateReadMe;
        private System.Windows.Forms.Button cmdMySQLConnect;
        private System.Windows.Forms.Button cmdAbout;
        private System.Windows.Forms.Button cmdExit;
    }
}

