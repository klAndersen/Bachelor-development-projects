namespace Klient
{
    partial class SøkeSkjema
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
            this.butAlle = new System.Windows.Forms.Button();
            this.butEnr = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtEnr = new System.Windows.Forms.TextBox();
            this.butSted = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numFratakst = new System.Windows.Forms.NumericUpDown();
            this.numTiltakst = new System.Windows.Forms.NumericUpDown();
            this.butTakst = new System.Windows.Forms.Button();
            this.lstVis = new System.Windows.Forms.ListBox();
            this.butTøm = new System.Windows.Forms.Button();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.butSlett = new System.Windows.Forms.Button();
            this.lblSlettnr = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numFratakst)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTiltakst)).BeginInit();
            this.SuspendLayout();
            // 
            // butAlle
            // 
            this.butAlle.Location = new System.Drawing.Point(13, 13);
            this.butAlle.Margin = new System.Windows.Forms.Padding(4);
            this.butAlle.Name = "butAlle";
            this.butAlle.Size = new System.Drawing.Size(100, 28);
            this.butAlle.TabIndex = 0;
            this.butAlle.Text = "Vis alle";
            this.butAlle.UseVisualStyleBackColor = true;
            this.butAlle.Click += new System.EventHandler(this.butAlle_Click);
            // 
            // butEnr
            // 
            this.butEnr.Location = new System.Drawing.Point(224, 65);
            this.butEnr.Name = "butEnr";
            this.butEnr.Size = new System.Drawing.Size(100, 27);
            this.butEnr.TabIndex = 1;
            this.butEnr.Text = "Søk etter Enr";
            this.butEnr.UseVisualStyleBackColor = true;
            this.butEnr.Click += new System.EventHandler(this.butEnr_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Enr:";
            // 
            // txtEnr
            // 
            this.txtEnr.Location = new System.Drawing.Point(118, 70);
            this.txtEnr.Name = "txtEnr";
            this.txtEnr.Size = new System.Drawing.Size(100, 22);
            this.txtEnr.TabIndex = 17;
            this.txtEnr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtEnr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.sjekkEnr);
            // 
            // butSted
            // 
            this.butSted.Location = new System.Drawing.Point(224, 111);
            this.butSted.Name = "butSted";
            this.butSted.Size = new System.Drawing.Size(100, 23);
            this.butSted.TabIndex = 24;
            this.butSted.Text = "Søk etter type";
            this.butSted.Click += new System.EventHandler(this.butSted_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 185);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 16);
            this.label2.TabIndex = 20;
            this.label2.Text = "Fra takst:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(215, 185);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 16);
            this.label3.TabIndex = 20;
            this.label3.Text = "Til takst:";
            // 
            // numFratakst
            // 
            this.numFratakst.Increment = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numFratakst.Location = new System.Drawing.Point(95, 185);
            this.numFratakst.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numFratakst.Name = "numFratakst";
            this.numFratakst.Size = new System.Drawing.Size(100, 22);
            this.numFratakst.TabIndex = 21;
            this.numFratakst.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numFratakst.ThousandsSeparator = true;
            this.numFratakst.ValueChanged += new System.EventHandler(this.numFratakst_ValueChanged);
            // 
            // numTiltakst
            // 
            this.numTiltakst.Increment = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numTiltakst.Location = new System.Drawing.Point(287, 183);
            this.numTiltakst.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numTiltakst.Name = "numTiltakst";
            this.numTiltakst.Size = new System.Drawing.Size(100, 22);
            this.numTiltakst.TabIndex = 21;
            this.numTiltakst.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numTiltakst.ThousandsSeparator = true;
            // 
            // butTakst
            // 
            this.butTakst.Location = new System.Drawing.Point(412, 185);
            this.butTakst.Name = "butTakst";
            this.butTakst.Size = new System.Drawing.Size(106, 23);
            this.butTakst.TabIndex = 19;
            this.butTakst.Text = "Søk etter takst";
            this.butTakst.UseVisualStyleBackColor = true;
            this.butTakst.Click += new System.EventHandler(this.butTakst_Click);
            // 
            // lstVis
            // 
            this.lstVis.FormattingEnabled = true;
            this.lstVis.HorizontalScrollbar = true;
            this.lstVis.ItemHeight = 16;
            this.lstVis.Location = new System.Drawing.Point(18, 230);
            this.lstVis.Name = "lstVis";
            this.lstVis.Size = new System.Drawing.Size(568, 164);
            this.lstVis.TabIndex = 22;
            this.lstVis.SelectedIndexChanged += new System.EventHandler(this.lstVis_SelectedIndexChanged);
            // 
            // butTøm
            // 
            this.butTøm.Location = new System.Drawing.Point(637, 12);
            this.butTøm.Name = "butTøm";
            this.butTøm.Size = new System.Drawing.Size(75, 28);
            this.butTøm.TabIndex = 23;
            this.butTøm.Text = "Tøm alt";
            this.butTøm.UseVisualStyleBackColor = true;
            this.butTøm.Click += new System.EventHandler(this.butTøm_Click);
            // 
            // cboType
            // 
            this.cboType.FormattingEnabled = true;
            this.cboType.Location = new System.Drawing.Point(97, 111);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(121, 24);
            this.cboType.TabIndex = 26;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 16);
            this.label4.TabIndex = 25;
            this.label4.Text = "Type:";
            // 
            // butSlett
            // 
            this.butSlett.Enabled = false;
            this.butSlett.Location = new System.Drawing.Point(637, 262);
            this.butSlett.Name = "butSlett";
            this.butSlett.Size = new System.Drawing.Size(75, 45);
            this.butSlett.TabIndex = 27;
            this.butSlett.Text = "Slett den merkede";
            this.butSlett.UseVisualStyleBackColor = true;
            this.butSlett.Click += new System.EventHandler(this.butSlett_Click);
            // 
            // lblSlettnr
            // 
            this.lblSlettnr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSlettnr.Location = new System.Drawing.Point(637, 230);
            this.lblSlettnr.Name = "lblSlettnr";
            this.lblSlettnr.Size = new System.Drawing.Size(75, 29);
            this.lblSlettnr.TabIndex = 28;
            this.lblSlettnr.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // SøkeSkjema
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 420);
            this.Controls.Add(this.lblSlettnr);
            this.Controls.Add(this.butSlett);
            this.Controls.Add(this.cboType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.butTøm);
            this.Controls.Add(this.lstVis);
            this.Controls.Add(this.numTiltakst);
            this.Controls.Add(this.numFratakst);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.butTakst);
            this.Controls.Add(this.butSted);
            this.Controls.Add(this.txtEnr);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.butEnr);
            this.Controls.Add(this.butAlle);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SøkeSkjema";
            this.Text = "SøkeSkjema";
            this.Load += new System.EventHandler(this.SøkeSkjema_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numFratakst)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTiltakst)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button butAlle;
        private System.Windows.Forms.Button butEnr;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEnr;
        private System.Windows.Forms.Button butSted;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numFratakst;
        private System.Windows.Forms.NumericUpDown numTiltakst;
        private System.Windows.Forms.Button butTakst;
        private System.Windows.Forms.ListBox lstVis;
        private System.Windows.Forms.Button butTøm;
        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button butSlett;
        private System.Windows.Forms.Label lblSlettnr;
    }
}