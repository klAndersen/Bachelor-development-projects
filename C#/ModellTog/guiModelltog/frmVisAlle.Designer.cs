namespace guiModellTog {
    partial class frmVisAlle {
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
            this.dgvModeller = new System.Windows.Forms.DataGridView();
            this.cmdLagre = new System.Windows.Forms.Button();
            this.cmdLukk = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.bwUpdate = new System.ComponentModel.BackgroundWorker();
            this.cmdSlett = new System.Windows.Forms.Button();
            this.cmdSkrivUt = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvModeller)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvModeller
            // 
            this.dgvModeller.AllowUserToAddRows = false;
            this.dgvModeller.AllowUserToDeleteRows = false;
            this.dgvModeller.AllowUserToResizeRows = false;
            this.dgvModeller.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvModeller.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvModeller.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvModeller.Location = new System.Drawing.Point(0, 0);
            this.dgvModeller.Name = "dgvModeller";
            this.dgvModeller.RowHeadersVisible = false;
            this.dgvModeller.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvModeller.Size = new System.Drawing.Size(712, 481);
            this.dgvModeller.TabIndex = 0;
            // 
            // cmdLagre
            // 
            this.cmdLagre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdLagre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdLagre.Location = new System.Drawing.Point(491, 487);
            this.cmdLagre.Name = "cmdLagre";
            this.cmdLagre.Size = new System.Drawing.Size(212, 29);
            this.cmdLagre.TabIndex = 1;
            this.cmdLagre.Text = "Lagre endringer";
            this.cmdLagre.UseVisualStyleBackColor = true;
            this.cmdLagre.Click += new System.EventHandler(this.cmdLagre_Click);
            // 
            // cmdLukk
            // 
            this.cmdLukk.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdLukk.Location = new System.Drawing.Point(12, 487);
            this.cmdLukk.Name = "cmdLukk";
            this.cmdLukk.Size = new System.Drawing.Size(212, 29);
            this.cmdLukk.TabIndex = 2;
            this.cmdLukk.Text = "Lukk vinduet";
            this.cmdLukk.UseVisualStyleBackColor = true;
            this.cmdLukk.Click += new System.EventHandler(this.cmdLukk_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(9, 569);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(688, 23);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "label1";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bwUpdate
            // 
            this.bwUpdate.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwUpdate_DoWork);
            this.bwUpdate.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwUpdate_ProgressChanged);
            this.bwUpdate.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwUpdate_RunWorkerCompleted);
            // 
            // cmdSlett
            // 
            this.cmdSlett.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSlett.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSlett.Location = new System.Drawing.Point(491, 522);
            this.cmdSlett.Name = "cmdSlett";
            this.cmdSlett.Size = new System.Drawing.Size(209, 29);
            this.cmdSlett.TabIndex = 4;
            this.cmdSlett.Text = "Slett modell";
            this.cmdSlett.UseVisualStyleBackColor = true;
            this.cmdSlett.Click += new System.EventHandler(this.cmdSlett_Click);
            // 
            // cmdSkrivUt
            // 
            this.cmdSkrivUt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSkrivUt.Location = new System.Drawing.Point(12, 522);
            this.cmdSkrivUt.Name = "cmdSkrivUt";
            this.cmdSkrivUt.Size = new System.Drawing.Size(212, 29);
            this.cmdSkrivUt.TabIndex = 5;
            this.cmdSkrivUt.Text = "Skriv ønskeliste til fil";
            this.cmdSkrivUt.UseVisualStyleBackColor = true;
            this.cmdSkrivUt.Visible = false;
            this.cmdSkrivUt.Click += new System.EventHandler(this.cmdSkrivUt_Click);
            // 
            // frmVisAlle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PapayaWhip;
            this.ClientSize = new System.Drawing.Size(712, 601);
            this.Controls.Add(this.cmdSkrivUt);
            this.Controls.Add(this.cmdSlett);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.cmdLukk);
            this.Controls.Add(this.cmdLagre);
            this.Controls.Add(this.dgvModeller);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "frmVisAlle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vis alle";
            this.Load += new System.EventHandler(this.frmVisAlle_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvModeller)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvModeller;
        private System.Windows.Forms.Button cmdLagre;
        private System.Windows.Forms.Button cmdLukk;
        private System.Windows.Forms.Label lblStatus;
        private System.ComponentModel.BackgroundWorker bwUpdate;
        private System.Windows.Forms.Button cmdSlett;
        private System.Windows.Forms.Button cmdSkrivUt;
    }
}