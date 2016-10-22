namespace Klient
{
    partial class Menyskjema
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
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.radSolgt = new System.Windows.Forms.RadioButton();
            this.txtAdresse = new System.Windows.Forms.TextBox();
            this.txtMatrikkel = new System.Windows.Forms.TextBox();
            this.radBy = new System.Windows.Forms.RadioButton();
            this.radLand = new System.Windows.Forms.RadioButton();
            this.grpSolgt = new System.Windows.Forms.GroupBox();
            this.radUsolgt = new System.Windows.Forms.RadioButton();
            this.grpSted = new System.Windows.Forms.GroupBox();
            this.txtEnr = new System.Windows.Forms.TextBox();
            this.numTakst = new System.Windows.Forms.NumericUpDown();
            this.butTøm = new System.Windows.Forms.Button();
            this.butReg = new System.Windows.Forms.Button();
            this.butSøk = new System.Windows.Forms.Button();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.butLagreAlt = new System.Windows.Forms.Button();
            this.butBud = new System.Windows.Forms.Button();
            this.grpSolgt.SuspendLayout();
            this.grpSted.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTakst)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enr:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Verditakst:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 205);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 16);
            this.label5.TabIndex = 5;
            this.label5.Text = "Adresse:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 240);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 16);
            this.label6.TabIndex = 6;
            this.label6.Text = "Matrikkelnr:";
            // 
            // radSolgt
            // 
            this.radSolgt.Location = new System.Drawing.Point(80, 21);
            this.radSolgt.Name = "radSolgt";
            this.radSolgt.Size = new System.Drawing.Size(61, 20);
            this.radSolgt.TabIndex = 8;
            this.radSolgt.Text = "Solgt";
            this.radSolgt.UseVisualStyleBackColor = true;
            // 
            // txtAdresse
            // 
            this.txtAdresse.Location = new System.Drawing.Point(95, 202);
            this.txtAdresse.Name = "txtAdresse";
            this.txtAdresse.Size = new System.Drawing.Size(309, 22);
            this.txtAdresse.TabIndex = 5;
            // 
            // txtMatrikkel
            // 
            this.txtMatrikkel.Enabled = false;
            this.txtMatrikkel.Location = new System.Drawing.Point(96, 237);
            this.txtMatrikkel.Name = "txtMatrikkel";
            this.txtMatrikkel.Size = new System.Drawing.Size(308, 22);
            this.txtMatrikkel.TabIndex = 6;
            // 
            // radBy
            // 
            this.radBy.Checked = true;
            this.radBy.Location = new System.Drawing.Point(6, 21);
            this.radBy.Name = "radBy";
            this.radBy.Size = new System.Drawing.Size(42, 20);
            this.radBy.TabIndex = 12;
            this.radBy.TabStop = true;
            this.radBy.Text = "By";
            this.radBy.UseVisualStyleBackColor = true;
            this.radBy.CheckedChanged += new System.EventHandler(this.radBy_CheckedChanged);
            // 
            // radLand
            // 
            this.radLand.Location = new System.Drawing.Point(54, 21);
            this.radLand.Name = "radLand";
            this.radLand.Size = new System.Drawing.Size(56, 20);
            this.radLand.TabIndex = 13;
            this.radLand.Text = "Land";
            this.radLand.UseVisualStyleBackColor = true;
            // 
            // grpSolgt
            // 
            this.grpSolgt.Controls.Add(this.radUsolgt);
            this.grpSolgt.Controls.Add(this.radSolgt);
            this.grpSolgt.Location = new System.Drawing.Point(95, 131);
            this.grpSolgt.Name = "grpSolgt";
            this.grpSolgt.Size = new System.Drawing.Size(145, 54);
            this.grpSolgt.TabIndex = 3;
            this.grpSolgt.TabStop = false;
            this.grpSolgt.Text = "Solgt?";
            // 
            // radUsolgt
            // 
            this.radUsolgt.Checked = true;
            this.radUsolgt.Location = new System.Drawing.Point(8, 21);
            this.radUsolgt.Name = "radUsolgt";
            this.radUsolgt.Size = new System.Drawing.Size(65, 20);
            this.radUsolgt.TabIndex = 9;
            this.radUsolgt.TabStop = true;
            this.radUsolgt.Text = "Usolgt";
            this.radUsolgt.UseVisualStyleBackColor = true;
            // 
            // grpSted
            // 
            this.grpSted.Controls.Add(this.radBy);
            this.grpSted.Controls.Add(this.radLand);
            this.grpSted.Location = new System.Drawing.Point(253, 131);
            this.grpSted.Name = "grpSted";
            this.grpSted.Size = new System.Drawing.Size(151, 54);
            this.grpSted.TabIndex = 4;
            this.grpSted.TabStop = false;
            this.grpSted.Text = "Sted";
            // 
            // txtEnr
            // 
            this.txtEnr.Location = new System.Drawing.Point(95, 25);
            this.txtEnr.Name = "txtEnr";
            this.txtEnr.Size = new System.Drawing.Size(100, 22);
            this.txtEnr.TabIndex = 0;
            this.txtEnr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtEnr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.sjekkEnr);
            // 
            // numTakst
            // 
            this.numTakst.Increment = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numTakst.Location = new System.Drawing.Point(95, 54);
            this.numTakst.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numTakst.Name = "numTakst";
            this.numTakst.Size = new System.Drawing.Size(100, 22);
            this.numTakst.TabIndex = 1;
            this.numTakst.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numTakst.ThousandsSeparator = true;
            // 
            // butTøm
            // 
            this.butTøm.Location = new System.Drawing.Point(329, 22);
            this.butTøm.Name = "butTøm";
            this.butTøm.Size = new System.Drawing.Size(75, 28);
            this.butTøm.TabIndex = 11;
            this.butTøm.Text = "Nullstill";
            this.butTøm.UseVisualStyleBackColor = true;
            this.butTøm.Click += new System.EventHandler(this.butTøm_Click);
            // 
            // butReg
            // 
            this.butReg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butReg.Location = new System.Drawing.Point(307, 276);
            this.butReg.Name = "butReg";
            this.butReg.Size = new System.Drawing.Size(94, 47);
            this.butReg.TabIndex = 7;
            this.butReg.Text = "Registrer eiendom";
            this.butReg.UseVisualStyleBackColor = true;
            this.butReg.Click += new System.EventHandler(this.butReg_Click);
            // 
            // butSøk
            // 
            this.butSøk.Location = new System.Drawing.Point(12, 276);
            this.butSøk.Name = "butSøk";
            this.butSøk.Size = new System.Drawing.Size(75, 47);
            this.butSøk.TabIndex = 8;
            this.butSøk.Text = "Søk";
            this.butSøk.UseVisualStyleBackColor = true;
            this.butSøk.Click += new System.EventHandler(this.butSøk_Click);
            // 
            // cboType
            // 
            this.cboType.FormattingEnabled = true;
            this.cboType.Location = new System.Drawing.Point(95, 82);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(121, 24);
            this.cboType.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Type:";
            // 
            // butLagreAlt
            // 
            this.butLagreAlt.Location = new System.Drawing.Point(93, 276);
            this.butLagreAlt.Name = "butLagreAlt";
            this.butLagreAlt.Size = new System.Drawing.Size(75, 47);
            this.butLagreAlt.TabIndex = 10;
            this.butLagreAlt.Text = "Lagre alt";
            this.butLagreAlt.UseVisualStyleBackColor = true;
            this.butLagreAlt.Click += new System.EventHandler(this.butLagreAlt_Click);
            // 
            // butBud
            // 
            this.butBud.Location = new System.Drawing.Point(174, 276);
            this.butBud.Name = "butBud";
            this.butBud.Size = new System.Drawing.Size(75, 47);
            this.butBud.TabIndex = 9;
            this.butBud.Text = "Reg. bud";
            this.butBud.UseVisualStyleBackColor = true;
            this.butBud.Click += new System.EventHandler(this.butBud_Click);
            // 
            // Menyskjema
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 329);
            this.Controls.Add(this.butLagreAlt);
            this.Controls.Add(this.cboType);
            this.Controls.Add(this.butBud);
            this.Controls.Add(this.butSøk);
            this.Controls.Add(this.butReg);
            this.Controls.Add(this.butTøm);
            this.Controls.Add(this.numTakst);
            this.Controls.Add(this.txtEnr);
            this.Controls.Add(this.grpSolgt);
            this.Controls.Add(this.grpSted);
            this.Controls.Add(this.txtMatrikkel);
            this.Controls.Add(this.txtAdresse);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Menyskjema";
            this.Text = "Menyskjema";
            this.Load += new System.EventHandler(this.Menyskjema_Load);
            this.grpSolgt.ResumeLayout(false);
            this.grpSted.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numTakst)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton radSolgt;
        private System.Windows.Forms.TextBox txtAdresse;
        private System.Windows.Forms.TextBox txtMatrikkel;
        private System.Windows.Forms.RadioButton radBy;
        private System.Windows.Forms.RadioButton radLand;
        private System.Windows.Forms.GroupBox grpSolgt;
        private System.Windows.Forms.GroupBox grpSted;
        private System.Windows.Forms.TextBox txtEnr;
        private System.Windows.Forms.NumericUpDown numTakst;
        private System.Windows.Forms.RadioButton radUsolgt;
        private System.Windows.Forms.Button butTøm;
        private System.Windows.Forms.Button butReg;
        private System.Windows.Forms.Button butSøk;
        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button butLagreAlt;
        private System.Windows.Forms.Button butBud;
    }
}

