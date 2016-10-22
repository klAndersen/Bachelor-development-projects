namespace guiModellTog {
    partial class frmSok {
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMnr = new System.Windows.Forms.TextBox();
            this.txtNavn = new System.Windows.Forms.TextBox();
            this.txtType = new System.Windows.Forms.TextBox();
            this.cmdSokMnr = new System.Windows.Forms.Button();
            this.cmdSokType = new System.Windows.Forms.Button();
            this.cmdSokNavn = new System.Windows.Forms.Button();
            this.cmdTilbake = new System.Windows.Forms.Button();
            this.lblTittel = new System.Windows.Forms.Label();
            this.lblNote = new System.Windows.Forms.Label();
            this.tipSok = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(30, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "Skriv inn modellnr:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(30, 191);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 17);
            this.label2.TabIndex = 10;
            this.label2.Text = "Skriv inn typen:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(30, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Skriv inn navn:";
            // 
            // txtMnr
            // 
            this.txtMnr.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMnr.Location = new System.Drawing.Point(160, 71);
            this.txtMnr.Name = "txtMnr";
            this.txtMnr.Size = new System.Drawing.Size(100, 23);
            this.txtMnr.TabIndex = 0;
            this.tipSok.SetToolTip(this.txtMnr, "Skriv inn modellnummeret\r\n(Kun siffer: 0-9)");
            // 
            // txtNavn
            // 
            this.txtNavn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNavn.Location = new System.Drawing.Point(160, 126);
            this.txtNavn.Name = "txtNavn";
            this.txtNavn.Size = new System.Drawing.Size(100, 23);
            this.txtNavn.TabIndex = 2;
            this.tipSok.SetToolTip(this.txtNavn, "Skriv inn navnet på modellen du ønsker å finne");
            // 
            // txtType
            // 
            this.txtType.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtType.Location = new System.Drawing.Point(160, 185);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(100, 23);
            this.txtType.TabIndex = 4;
            this.tipSok.SetToolTip(this.txtType, "Skriv inn modelltypen du ønsker å finne");
            // 
            // cmdSokMnr
            // 
            this.cmdSokMnr.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSokMnr.Location = new System.Drawing.Point(281, 68);
            this.cmdSokMnr.Name = "cmdSokMnr";
            this.cmdSokMnr.Size = new System.Drawing.Size(123, 26);
            this.cmdSokMnr.TabIndex = 1;
            this.cmdSokMnr.Text = "Søk på modellnr";
            this.cmdSokMnr.UseVisualStyleBackColor = true;
            this.cmdSokMnr.Click += new System.EventHandler(this.cmdSokMnr_Click);
            // 
            // cmdSokType
            // 
            this.cmdSokType.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSokType.Location = new System.Drawing.Point(281, 182);
            this.cmdSokType.Name = "cmdSokType";
            this.cmdSokType.Size = new System.Drawing.Size(123, 26);
            this.cmdSokType.TabIndex = 5;
            this.cmdSokType.Text = "Søk på type";
            this.cmdSokType.UseVisualStyleBackColor = true;
            this.cmdSokType.Click += new System.EventHandler(this.cmdSokType_Click);
            // 
            // cmdSokNavn
            // 
            this.cmdSokNavn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSokNavn.Location = new System.Drawing.Point(281, 123);
            this.cmdSokNavn.Name = "cmdSokNavn";
            this.cmdSokNavn.Size = new System.Drawing.Size(123, 26);
            this.cmdSokNavn.TabIndex = 3;
            this.cmdSokNavn.Text = "Søk på navn";
            this.cmdSokNavn.UseVisualStyleBackColor = true;
            this.cmdSokNavn.Click += new System.EventHandler(this.cmdSokNavn_Click);
            // 
            // cmdTilbake
            // 
            this.cmdTilbake.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdTilbake.Location = new System.Drawing.Point(33, 293);
            this.cmdTilbake.Name = "cmdTilbake";
            this.cmdTilbake.Size = new System.Drawing.Size(164, 28);
            this.cmdTilbake.TabIndex = 6;
            this.cmdTilbake.Text = "Tilbake til hovedmeny";
            this.cmdTilbake.UseVisualStyleBackColor = true;
            this.cmdTilbake.Click += new System.EventHandler(this.cmdTilbake_Click);
            // 
            // lblTittel
            // 
            this.lblTittel.AutoSize = true;
            this.lblTittel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTittel.Location = new System.Drawing.Point(135, 15);
            this.lblTittel.Name = "lblTittel";
            this.lblTittel.Size = new System.Drawing.Size(124, 20);
            this.lblTittel.TabIndex = 7;
            this.lblTittel.Text = "Søk etter modell";
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNote.ForeColor = System.Drawing.Color.Red;
            this.lblNote.Location = new System.Drawing.Point(30, 242);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(294, 34);
            this.lblNote.TabIndex = 11;
            this.lblNote.Text = "NB! Du trenger kun fylle et av tekstfeltene og \r\ntrykke på tilhørende knapp";
            // 
            // frmSok
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PapayaWhip;
            this.ClientSize = new System.Drawing.Size(429, 336);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.lblTittel);
            this.Controls.Add(this.cmdTilbake);
            this.Controls.Add(this.cmdSokNavn);
            this.Controls.Add(this.cmdSokType);
            this.Controls.Add(this.cmdSokMnr);
            this.Controls.Add(this.txtType);
            this.Controls.Add(this.txtNavn);
            this.Controls.Add(this.txtMnr);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "frmSok";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Søk etter modell";
            this.Load += new System.EventHandler(this.frmSok_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMnr;
        private System.Windows.Forms.TextBox txtNavn;
        private System.Windows.Forms.TextBox txtType;
        private System.Windows.Forms.Button cmdSokMnr;
        private System.Windows.Forms.Button cmdSokType;
        private System.Windows.Forms.Button cmdSokNavn;
        private System.Windows.Forms.Button cmdTilbake;
        private System.Windows.Forms.Label lblTittel;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.ToolTip tipSok;
    }
}