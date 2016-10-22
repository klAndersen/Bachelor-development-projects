namespace guiModellTog {
    partial class frmInput {
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
            this.cmdOk = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.lblSkrivInn = new System.Windows.Forms.Label();
            this.txtFelt1 = new System.Windows.Forms.TextBox();
            this.txtFelt2 = new System.Windows.Forms.TextBox();
            this.lblAntall = new System.Windows.Forms.Label();
            this.tipInput = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // cmdOk
            // 
            this.cmdOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdOk.Location = new System.Drawing.Point(142, 127);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(87, 33);
            this.cmdOk.TabIndex = 2;
            this.cmdOk.Text = "&Ok";
            this.cmdOk.UseVisualStyleBackColor = true;
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCancel.Location = new System.Drawing.Point(295, 127);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(80, 33);
            this.cmdCancel.TabIndex = 3;
            this.cmdCancel.Text = "&Avbryt";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // lblSkrivInn
            // 
            this.lblSkrivInn.AutoSize = true;
            this.lblSkrivInn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSkrivInn.Location = new System.Drawing.Point(12, 22);
            this.lblSkrivInn.Name = "lblSkrivInn";
            this.lblSkrivInn.Size = new System.Drawing.Size(217, 17);
            this.lblSkrivInn.TabIndex = 4;
            this.lblSkrivInn.Text = "Skriv inn navnet på produsenten:";
            // 
            // txtFelt1
            // 
            this.txtFelt1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFelt1.Location = new System.Drawing.Point(233, 22);
            this.txtFelt1.Name = "txtFelt1";
            this.txtFelt1.Size = new System.Drawing.Size(142, 23);
            this.txtFelt1.TabIndex = 0;
            // 
            // txtFelt2
            // 
            this.txtFelt2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFelt2.Location = new System.Drawing.Point(233, 70);
            this.txtFelt2.Name = "txtFelt2";
            this.txtFelt2.Size = new System.Drawing.Size(142, 23);
            this.txtFelt2.TabIndex = 1;
            // 
            // lblAntall
            // 
            this.lblAntall.AutoSize = true;
            this.lblAntall.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntall.Location = new System.Drawing.Point(12, 70);
            this.lblAntall.Name = "lblAntall";
            this.lblAntall.Size = new System.Drawing.Size(179, 17);
            this.lblAntall.TabIndex = 5;
            this.lblAntall.Text = "Skriv inn antall nye vogner:";
            // 
            // frmInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PapayaWhip;
            this.ClientSize = new System.Drawing.Size(387, 184);
            this.Controls.Add(this.txtFelt2);
            this.Controls.Add(this.lblAntall);
            this.Controls.Add(this.txtFelt1);
            this.Controls.Add(this.lblSkrivInn);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmInput";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registrer eksisterende vogn";
            this.Load += new System.EventHandler(this.frmInput_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdOk;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Label lblSkrivInn;
        private System.Windows.Forms.TextBox txtFelt1;
        private System.Windows.Forms.TextBox txtFelt2;
        private System.Windows.Forms.Label lblAntall;
        private System.Windows.Forms.ToolTip tipInput;
    }
}