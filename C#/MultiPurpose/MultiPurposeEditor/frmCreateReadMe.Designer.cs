namespace MultiPurposeEditor {
    partial class frmCreateReadMe {
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
            this.lblFileName = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblVersionNo = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.chkOverWrite = new System.Windows.Forms.CheckBox();
            this.chkAsteric = new System.Windows.Forms.CheckBox();
            this.chkIncludeVersion = new System.Windows.Forms.CheckBox();
            this.cmdShowExample = new System.Windows.Forms.Button();
            this.cmdInsertAsteric = new System.Windows.Forms.Button();
            this.cmdEmptyFields = new System.Windows.Forms.Button();
            this.cmdSave = new System.Windows.Forms.Button();
            this.txtContent = new System.Windows.Forms.RichTextBox();
            this.lblFilepathDescription = new System.Windows.Forms.Label();
            this.lblFilePath = new System.Windows.Forms.Label();
            this.cmdBrowse = new System.Windows.Forms.Button();
            this.fdbFilePath = new System.Windows.Forms.FolderBrowserDialog();
            this.cmdClose = new System.Windows.Forms.Button();
            this.tipHelp = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileName.Location = new System.Drawing.Point(12, 42);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(96, 16);
            this.lblFileName.TabIndex = 15;
            this.lblFileName.Text = "Enter filename:";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(12, 70);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(65, 16);
            this.lblTitle.TabIndex = 16;
            this.lblTitle.Text = "Enter title:";
            // 
            // lblVersionNo
            // 
            this.lblVersionNo.AutoSize = true;
            this.lblVersionNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersionNo.Location = new System.Drawing.Point(12, 98);
            this.lblVersionNo.Name = "lblVersionNo";
            this.lblVersionNo.Size = new System.Drawing.Size(137, 16);
            this.lblVersionNo.TabIndex = 17;
            this.lblVersionNo.Text = "Enter version number:";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(12, 142);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(260, 16);
            this.lblDescription.TabIndex = 18;
            this.lblDescription.Text = "Write in text (Use \'Insert asteric\' for **** row)";
            // 
            // txtFileName
            // 
            this.txtFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFileName.Location = new System.Drawing.Point(182, 39);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(338, 22);
            this.txtFileName.TabIndex = 1;
            // 
            // txtVersion
            // 
            this.txtVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVersion.Location = new System.Drawing.Point(182, 95);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(338, 22);
            this.txtVersion.TabIndex = 5;
            // 
            // txtTitle
            // 
            this.txtTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTitle.Location = new System.Drawing.Point(182, 67);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(338, 22);
            this.txtTitle.TabIndex = 3;
            // 
            // chkOverWrite
            // 
            this.chkOverWrite.AutoSize = true;
            this.chkOverWrite.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkOverWrite.Location = new System.Drawing.Point(526, 41);
            this.chkOverWrite.Name = "chkOverWrite";
            this.chkOverWrite.Size = new System.Drawing.Size(184, 20);
            this.chkOverWrite.TabIndex = 2;
            this.chkOverWrite.Text = "Overwrite file (if file exists)?";
            this.chkOverWrite.UseVisualStyleBackColor = true;
            // 
            // chkAsteric
            // 
            this.chkAsteric.AutoSize = true;
            this.chkAsteric.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAsteric.Location = new System.Drawing.Point(526, 70);
            this.chkAsteric.Name = "chkAsteric";
            this.chkAsteric.Size = new System.Drawing.Size(104, 20);
            this.chkAsteric.TabIndex = 4;
            this.chkAsteric.Text = "Asteric Title?";
            this.chkAsteric.UseVisualStyleBackColor = true;
            // 
            // chkIncludeVersion
            // 
            this.chkIncludeVersion.AutoSize = true;
            this.chkIncludeVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIncludeVersion.Location = new System.Drawing.Point(526, 98);
            this.chkIncludeVersion.Name = "chkIncludeVersion";
            this.chkIncludeVersion.Size = new System.Drawing.Size(172, 20);
            this.chkIncludeVersion.TabIndex = 6;
            this.chkIncludeVersion.Text = "Include version number?";
            this.chkIncludeVersion.UseVisualStyleBackColor = true;
            // 
            // cmdShowExample
            // 
            this.cmdShowExample.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdShowExample.Location = new System.Drawing.Point(171, 522);
            this.cmdShowExample.Name = "cmdShowExample";
            this.cmdShowExample.Size = new System.Drawing.Size(137, 28);
            this.cmdShowExample.TabIndex = 9;
            this.cmdShowExample.Text = "Show Example";
            this.cmdShowExample.UseVisualStyleBackColor = true;
            this.cmdShowExample.Click += new System.EventHandler(this.cmdShowExample_Click);
            // 
            // cmdInsertAsteric
            // 
            this.cmdInsertAsteric.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdInsertAsteric.Location = new System.Drawing.Point(332, 522);
            this.cmdInsertAsteric.Name = "cmdInsertAsteric";
            this.cmdInsertAsteric.Size = new System.Drawing.Size(137, 28);
            this.cmdInsertAsteric.TabIndex = 10;
            this.cmdInsertAsteric.Text = "Insert asterics (*)";
            this.cmdInsertAsteric.UseVisualStyleBackColor = true;
            this.cmdInsertAsteric.Click += new System.EventHandler(this.cmdInsertAsteric_Click);
            // 
            // cmdEmptyFields
            // 
            this.cmdEmptyFields.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdEmptyFields.Location = new System.Drawing.Point(491, 522);
            this.cmdEmptyFields.Name = "cmdEmptyFields";
            this.cmdEmptyFields.Size = new System.Drawing.Size(125, 28);
            this.cmdEmptyFields.TabIndex = 11;
            this.cmdEmptyFields.Text = "Empty all fields";
            this.cmdEmptyFields.UseVisualStyleBackColor = true;
            this.cmdEmptyFields.Click += new System.EventHandler(this.cmdEmptyFields_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Location = new System.Drawing.Point(647, 522);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(125, 28);
            this.cmdSave.TabIndex = 12;
            this.cmdSave.Text = "Save changes";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // txtContent
            // 
            this.txtContent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContent.Location = new System.Drawing.Point(12, 161);
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(760, 355);
            this.txtContent.TabIndex = 7;
            this.txtContent.Text = "";
            // 
            // lblFilepathDescription
            // 
            this.lblFilepathDescription.AutoSize = true;
            this.lblFilepathDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilepathDescription.Location = new System.Drawing.Point(12, 14);
            this.lblFilepathDescription.Name = "lblFilepathDescription";
            this.lblFilepathDescription.Size = new System.Drawing.Size(59, 16);
            this.lblFilepathDescription.TabIndex = 13;
            this.lblFilepathDescription.Text = "Filepath:";
            // 
            // lblFilePath
            // 
            this.lblFilePath.BackColor = System.Drawing.Color.White;
            this.lblFilePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilePath.Location = new System.Drawing.Point(182, 9);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(509, 23);
            this.lblFilePath.TabIndex = 14;
            this.lblFilePath.Text = "Select a folder to save the ReadMe.txt...";
            this.lblFilePath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmdBrowse
            // 
            this.cmdBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdBrowse.Location = new System.Drawing.Point(697, 6);
            this.cmdBrowse.Name = "cmdBrowse";
            this.cmdBrowse.Size = new System.Drawing.Size(75, 28);
            this.cmdBrowse.TabIndex = 0;
            this.cmdBrowse.Text = "Browse...";
            this.cmdBrowse.UseVisualStyleBackColor = true;
            this.cmdBrowse.Click += new System.EventHandler(this.cmdBrowse_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.Location = new System.Drawing.Point(12, 522);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(137, 28);
            this.cmdClose.TabIndex = 8;
            this.cmdClose.Text = "Close window";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // frmCreateReadMe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleGreen;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdBrowse);
            this.Controls.Add(this.lblFilePath);
            this.Controls.Add(this.lblFilepathDescription);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.cmdEmptyFields);
            this.Controls.Add(this.cmdInsertAsteric);
            this.Controls.Add(this.cmdShowExample);
            this.Controls.Add(this.chkIncludeVersion);
            this.Controls.Add(this.chkAsteric);
            this.Controls.Add(this.chkOverWrite);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.txtVersion);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblVersionNo);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblFileName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "frmCreateReadMe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create a ReadMe.txt";
            this.Load += new System.EventHandler(this.frmCreateReadMe_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblVersionNo;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.CheckBox chkOverWrite;
        private System.Windows.Forms.CheckBox chkAsteric;
        private System.Windows.Forms.CheckBox chkIncludeVersion;
        private System.Windows.Forms.Button cmdShowExample;
        private System.Windows.Forms.Button cmdInsertAsteric;
        private System.Windows.Forms.Button cmdEmptyFields;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.RichTextBox txtContent;
        private System.Windows.Forms.Label lblFilepathDescription;
        private System.Windows.Forms.Label lblFilePath;
        private System.Windows.Forms.Button cmdBrowse;
        private System.Windows.Forms.FolderBrowserDialog fdbFilePath;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.ToolTip tipHelp;
    }
}