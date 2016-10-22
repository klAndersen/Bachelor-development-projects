namespace guiModellTog {
    partial class frmDetaljer {
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
            this.cmdSlett = new System.Windows.Forms.Button();
            this.cmdLukk = new System.Windows.Forms.Button();
            this.cmdLagre = new System.Windows.Forms.Button();
            this.dgvDetaljer = new System.Windows.Forms.DataGridView();
            this.lblVelg = new System.Windows.Forms.Label();
            this.cmbDetaljer = new System.Windows.Forms.ComboBox();
            this.tipHelp = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetaljer)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdSlett
            // 
            this.cmdSlett.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSlett.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSlett.Location = new System.Drawing.Point(260, 492);
            this.cmdSlett.Name = "cmdSlett";
            this.cmdSlett.Size = new System.Drawing.Size(152, 29);
            this.cmdSlett.TabIndex = 5;
            this.cmdSlett.Text = "Slett detalj";
            this.cmdSlett.UseVisualStyleBackColor = true;
            this.cmdSlett.Click += new System.EventHandler(this.cmdSlett_Click);
            // 
            // cmdLukk
            // 
            this.cmdLukk.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdLukk.Location = new System.Drawing.Point(12, 456);
            this.cmdLukk.Name = "cmdLukk";
            this.cmdLukk.Size = new System.Drawing.Size(152, 29);
            this.cmdLukk.TabIndex = 3;
            this.cmdLukk.Text = "Lukk vinduet";
            this.cmdLukk.UseVisualStyleBackColor = true;
            this.cmdLukk.Click += new System.EventHandler(this.cmdLukk_Click);
            // 
            // cmdLagre
            // 
            this.cmdLagre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdLagre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdLagre.Location = new System.Drawing.Point(260, 457);
            this.cmdLagre.Name = "cmdLagre";
            this.cmdLagre.Size = new System.Drawing.Size(152, 29);
            this.cmdLagre.TabIndex = 4;
            this.cmdLagre.Text = "Lagre endringer";
            this.cmdLagre.UseVisualStyleBackColor = true;
            this.cmdLagre.Click += new System.EventHandler(this.cmdLagre_Click);
            // 
            // dgvDetaljer
            // 
            this.dgvDetaljer.AllowUserToAddRows = false;
            this.dgvDetaljer.AllowUserToDeleteRows = false;
            this.dgvDetaljer.AllowUserToResizeRows = false;
            this.dgvDetaljer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDetaljer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvDetaljer.Location = new System.Drawing.Point(13, 56);
            this.dgvDetaljer.Name = "dgvDetaljer";
            this.dgvDetaljer.RowHeadersVisible = false;
            this.dgvDetaljer.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvDetaljer.Size = new System.Drawing.Size(399, 394);
            this.dgvDetaljer.TabIndex = 2;
            // 
            // lblVelg
            // 
            this.lblVelg.AutoSize = true;
            this.lblVelg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVelg.Location = new System.Drawing.Point(12, 18);
            this.lblVelg.Name = "lblVelg";
            this.lblVelg.Size = new System.Drawing.Size(217, 16);
            this.lblVelg.TabIndex = 0;
            this.lblVelg.Text = "Velg detaljen du ønsker å redigere:";
            // 
            // cmbDetaljer
            // 
            this.cmbDetaljer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDetaljer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDetaljer.FormattingEnabled = true;
            this.cmbDetaljer.Location = new System.Drawing.Point(235, 15);
            this.cmbDetaljer.Name = "cmbDetaljer";
            this.cmbDetaljer.Size = new System.Drawing.Size(176, 24);
            this.cmbDetaljer.TabIndex = 1;
            this.tipHelp.SetToolTip(this.cmbDetaljer, "Liste over detaljer\r\nViser alle registrerte og gir mulighet til \r\nå redigere/slet" +
        "te registrerte detaljer");
            this.cmbDetaljer.SelectedIndexChanged += new System.EventHandler(this.cmbDetaljer_SelectedIndexChanged);
            // 
            // frmDetaljer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PeachPuff;
            this.ClientSize = new System.Drawing.Size(424, 533);
            this.Controls.Add(this.cmbDetaljer);
            this.Controls.Add(this.lblVelg);
            this.Controls.Add(this.cmdSlett);
            this.Controls.Add(this.cmdLukk);
            this.Controls.Add(this.cmdLagre);
            this.Controls.Add(this.dgvDetaljer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "frmDetaljer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmDetaljer";
            this.Load += new System.EventHandler(this.frmDetaljer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetaljer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdSlett;
        private System.Windows.Forms.Button cmdLukk;
        private System.Windows.Forms.Button cmdLagre;
        private System.Windows.Forms.DataGridView dgvDetaljer;
        private System.Windows.Forms.Label lblVelg;
        private System.Windows.Forms.ComboBox cmbDetaljer;
        private System.Windows.Forms.ToolTip tipHelp;
    }
}