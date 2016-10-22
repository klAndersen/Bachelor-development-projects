namespace MultiPurposeEditor {
    partial class frmGenerateText {
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
            this.lblBeginning = new System.Windows.Forms.Label();
            this.lblEnding = new System.Windows.Forms.Label();
            this.lblEndNumber = new System.Windows.Forms.Label();
            this.lblJump = new System.Windows.Forms.Label();
            this.lblStartNumber = new System.Windows.Forms.Label();
            this.txtEndNumber = new System.Windows.Forms.TextBox();
            this.txtStartNumber = new System.Windows.Forms.TextBox();
            this.txtJump = new System.Windows.Forms.TextBox();
            this.txtStartText = new System.Windows.Forms.TextBox();
            this.txtEndText = new System.Windows.Forms.TextBox();
            this.cmdPrintBeginning = new System.Windows.Forms.Button();
            this.cmdPrintStartEnd = new System.Windows.Forms.Button();
            this.cmdEmpty = new System.Windows.Forms.Button();
            this.cmdPrintNoText = new System.Windows.Forms.Button();
            this.cmdPrintNoAndTxt = new System.Windows.Forms.Button();
            this.cmdHelp = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.RichTextBox();
            this.cmdClose = new System.Windows.Forms.Button();
            this.tipHelp = new System.Windows.Forms.ToolTip(this.components);
            this.lblBrowser = new System.Windows.Forms.Label();
            this.cmbBrowser = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lblBeginning
            // 
            this.lblBeginning.AutoSize = true;
            this.lblBeginning.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBeginning.Location = new System.Drawing.Point(13, 15);
            this.lblBeginning.Name = "lblBeginning";
            this.lblBeginning.Size = new System.Drawing.Size(197, 16);
            this.lblBeginning.TabIndex = 13;
            this.lblBeginning.Text = "Type in the beginning of the text:";
            // 
            // lblEnding
            // 
            this.lblEnding.AutoSize = true;
            this.lblEnding.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnding.Location = new System.Drawing.Point(13, 50);
            this.lblEnding.Name = "lblEnding";
            this.lblEnding.Size = new System.Drawing.Size(179, 16);
            this.lblEnding.TabIndex = 14;
            this.lblEnding.Text = "Type in the ending of the text:";
            // 
            // lblEndNumber
            // 
            this.lblEndNumber.AutoSize = true;
            this.lblEndNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndNumber.Location = new System.Drawing.Point(13, 129);
            this.lblEndNumber.Name = "lblEndNumber";
            this.lblEndNumber.Size = new System.Drawing.Size(244, 16);
            this.lblEndNumber.TabIndex = 16;
            this.lblEndNumber.Text = "Type in the ending number (mandatory):";
            // 
            // lblJump
            // 
            this.lblJump.AutoSize = true;
            this.lblJump.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJump.Location = new System.Drawing.Point(13, 166);
            this.lblJump.Name = "lblJump";
            this.lblJump.Size = new System.Drawing.Size(161, 16);
            this.lblJump.TabIndex = 17;
            this.lblJump.Text = "Type in the number steps:";
            // 
            // lblStartNumber
            // 
            this.lblStartNumber.AutoSize = true;
            this.lblStartNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartNumber.Location = new System.Drawing.Point(13, 89);
            this.lblStartNumber.Name = "lblStartNumber";
            this.lblStartNumber.Size = new System.Drawing.Size(246, 16);
            this.lblStartNumber.TabIndex = 15;
            this.lblStartNumber.Text = "Type in the starting number (mandatory):";
            // 
            // txtEndNumber
            // 
            this.txtEndNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEndNumber.Location = new System.Drawing.Point(345, 126);
            this.txtEndNumber.Name = "txtEndNumber";
            this.txtEndNumber.Size = new System.Drawing.Size(265, 22);
            this.txtEndNumber.TabIndex = 3;
            // 
            // txtStartNumber
            // 
            this.txtStartNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStartNumber.Location = new System.Drawing.Point(345, 86);
            this.txtStartNumber.Name = "txtStartNumber";
            this.txtStartNumber.Size = new System.Drawing.Size(265, 22);
            this.txtStartNumber.TabIndex = 2;
            // 
            // txtJump
            // 
            this.txtJump.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJump.Location = new System.Drawing.Point(345, 163);
            this.txtJump.Name = "txtJump";
            this.txtJump.Size = new System.Drawing.Size(265, 22);
            this.txtJump.TabIndex = 4;
            // 
            // txtStartText
            // 
            this.txtStartText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStartText.Location = new System.Drawing.Point(345, 12);
            this.txtStartText.Name = "txtStartText";
            this.txtStartText.Size = new System.Drawing.Size(265, 22);
            this.txtStartText.TabIndex = 0;
            // 
            // txtEndText
            // 
            this.txtEndText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEndText.Location = new System.Drawing.Point(345, 47);
            this.txtEndText.Name = "txtEndText";
            this.txtEndText.Size = new System.Drawing.Size(265, 22);
            this.txtEndText.TabIndex = 1;
            // 
            // cmdPrintBeginning
            // 
            this.cmdPrintBeginning.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPrintBeginning.Location = new System.Drawing.Point(20, 444);
            this.cmdPrintBeginning.Name = "cmdPrintBeginning";
            this.cmdPrintBeginning.Size = new System.Drawing.Size(306, 36);
            this.cmdPrintBeginning.TabIndex = 6;
            this.cmdPrintBeginning.Text = "Print the beginning text";
            this.cmdPrintBeginning.UseVisualStyleBackColor = true;
            this.cmdPrintBeginning.Click += new System.EventHandler(this.cmdPrintBeginning_Click);
            // 
            // cmdPrintStartEnd
            // 
            this.cmdPrintStartEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPrintStartEnd.Location = new System.Drawing.Point(20, 486);
            this.cmdPrintStartEnd.Name = "cmdPrintStartEnd";
            this.cmdPrintStartEnd.Size = new System.Drawing.Size(306, 36);
            this.cmdPrintStartEnd.TabIndex = 7;
            this.cmdPrintStartEnd.Text = "Print text before/after";
            this.cmdPrintStartEnd.UseVisualStyleBackColor = true;
            this.cmdPrintStartEnd.Click += new System.EventHandler(this.cmdPrintStartEnd_Click);
            // 
            // cmdEmpty
            // 
            this.cmdEmpty.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdEmpty.Location = new System.Drawing.Point(464, 444);
            this.cmdEmpty.Name = "cmdEmpty";
            this.cmdEmpty.Size = new System.Drawing.Size(150, 36);
            this.cmdEmpty.TabIndex = 10;
            this.cmdEmpty.Text = "Empty textfields";
            this.cmdEmpty.UseVisualStyleBackColor = true;
            this.cmdEmpty.Click += new System.EventHandler(this.cmdEmpty_Click);
            // 
            // cmdPrintNoText
            // 
            this.cmdPrintNoText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPrintNoText.Location = new System.Drawing.Point(20, 532);
            this.cmdPrintNoText.Name = "cmdPrintNoText";
            this.cmdPrintNoText.Size = new System.Drawing.Size(306, 36);
            this.cmdPrintNoText.TabIndex = 8;
            this.cmdPrintNoText.Text = "Print numbers (with starttext)";
            this.cmdPrintNoText.UseVisualStyleBackColor = true;
            this.cmdPrintNoText.Click += new System.EventHandler(this.cmdPrintNoText_Click);
            // 
            // cmdPrintNoAndTxt
            // 
            this.cmdPrintNoAndTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPrintNoAndTxt.Location = new System.Drawing.Point(20, 574);
            this.cmdPrintNoAndTxt.Name = "cmdPrintNoAndTxt";
            this.cmdPrintNoAndTxt.Size = new System.Drawing.Size(306, 36);
            this.cmdPrintNoAndTxt.TabIndex = 9;
            this.cmdPrintNoAndTxt.Text = "Print numbers (with text before/after)";
            this.cmdPrintNoAndTxt.UseVisualStyleBackColor = true;
            this.cmdPrintNoAndTxt.Click += new System.EventHandler(this.cmdPrintNoAndTxt_Click);
            // 
            // cmdHelp
            // 
            this.cmdHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdHelp.Location = new System.Drawing.Point(464, 486);
            this.cmdHelp.Name = "cmdHelp";
            this.cmdHelp.Size = new System.Drawing.Size(150, 36);
            this.cmdHelp.TabIndex = 11;
            this.cmdHelp.Text = "Help";
            this.cmdHelp.UseVisualStyleBackColor = true;
            this.cmdHelp.Click += new System.EventHandler(this.cmdHelp_Click);
            // 
            // txtResult
            // 
            this.txtResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResult.Location = new System.Drawing.Point(16, 229);
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(598, 209);
            this.txtResult.TabIndex = 5;
            this.txtResult.Text = "";
            this.txtResult.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.txtResult_LinkClicked);
            // 
            // cmdClose
            // 
            this.cmdClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.Location = new System.Drawing.Point(464, 528);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(150, 36);
            this.cmdClose.TabIndex = 12;
            this.cmdClose.Text = "Close window";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // lblBrowser
            // 
            this.lblBrowser.AutoSize = true;
            this.lblBrowser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBrowser.Location = new System.Drawing.Point(13, 202);
            this.lblBrowser.Name = "lblBrowser";
            this.lblBrowser.Size = new System.Drawing.Size(227, 16);
            this.lblBrowser.TabIndex = 18;
            this.lblBrowser.Text = "Select a browser to view the link(s) in:";
            this.lblBrowser.Visible = false;
            // 
            // cmbBrowser
            // 
            this.cmbBrowser.BackColor = System.Drawing.Color.LightBlue;
            this.cmbBrowser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBrowser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBrowser.FormattingEnabled = true;
            this.cmbBrowser.Location = new System.Drawing.Point(341, 199);
            this.cmbBrowser.Name = "cmbBrowser";
            this.cmbBrowser.Size = new System.Drawing.Size(265, 24);
            this.cmbBrowser.TabIndex = 19;
            this.cmbBrowser.Visible = false;
            // 
            // frmGenerateText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(629, 624);
            this.Controls.Add(this.cmbBrowser);
            this.Controls.Add(this.lblBrowser);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.cmdHelp);
            this.Controls.Add(this.cmdPrintNoAndTxt);
            this.Controls.Add(this.cmdPrintNoText);
            this.Controls.Add(this.cmdEmpty);
            this.Controls.Add(this.cmdPrintStartEnd);
            this.Controls.Add(this.cmdPrintBeginning);
            this.Controls.Add(this.txtEndText);
            this.Controls.Add(this.txtStartText);
            this.Controls.Add(this.txtJump);
            this.Controls.Add(this.txtStartNumber);
            this.Controls.Add(this.txtEndNumber);
            this.Controls.Add(this.lblStartNumber);
            this.Controls.Add(this.lblJump);
            this.Controls.Add(this.lblEndNumber);
            this.Controls.Add(this.lblEnding);
            this.Controls.Add(this.lblBeginning);
            this.Name = "frmGenerateText";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmGenerateText";
            this.Load += new System.EventHandler(this.frmGenerateText_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblBeginning;
        private System.Windows.Forms.Label lblEnding;
        private System.Windows.Forms.Label lblEndNumber;
        private System.Windows.Forms.Label lblJump;
        private System.Windows.Forms.Label lblStartNumber;
        private System.Windows.Forms.TextBox txtEndNumber;
        private System.Windows.Forms.TextBox txtStartNumber;
        private System.Windows.Forms.TextBox txtJump;
        private System.Windows.Forms.TextBox txtStartText;
        private System.Windows.Forms.TextBox txtEndText;
        private System.Windows.Forms.Button cmdPrintBeginning;
        private System.Windows.Forms.Button cmdPrintStartEnd;
        private System.Windows.Forms.Button cmdEmpty;
        private System.Windows.Forms.Button cmdPrintNoText;
        private System.Windows.Forms.Button cmdPrintNoAndTxt;
        private System.Windows.Forms.Button cmdHelp;
        private System.Windows.Forms.RichTextBox txtResult;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.ToolTip tipHelp;
        private System.Windows.Forms.Label lblBrowser;
        private System.Windows.Forms.ComboBox cmbBrowser;
    }
}