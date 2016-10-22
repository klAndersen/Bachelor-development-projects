namespace Klient
{
    partial class Budskjema
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
            this.txtEnr = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numBud = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.butReg = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numBud)).BeginInit();
            this.SuspendLayout();
            // 
            // txtEnr
            // 
            this.txtEnr.Location = new System.Drawing.Point(58, 7);
            this.txtEnr.Margin = new System.Windows.Forms.Padding(4);
            this.txtEnr.Name = "txtEnr";
            this.txtEnr.Size = new System.Drawing.Size(132, 22);
            this.txtEnr.TabIndex = 18;
            this.txtEnr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtEnr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEnr_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 16);
            this.label1.TabIndex = 17;
            this.label1.Text = "Enr:";
            // 
            // numBud
            // 
            this.numBud.Increment = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numBud.Location = new System.Drawing.Point(59, 52);
            this.numBud.Margin = new System.Windows.Forms.Padding(4);
            this.numBud.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numBud.Name = "numBud";
            this.numBud.Size = new System.Drawing.Size(133, 22);
            this.numBud.TabIndex = 20;
            this.numBud.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numBud.ThousandsSeparator = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 54);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 16);
            this.label3.TabIndex = 19;
            this.label3.Text = "Bud:";
            // 
            // butReg
            // 
            this.butReg.Location = new System.Drawing.Point(117, 95);
            this.butReg.Name = "butReg";
            this.butReg.Size = new System.Drawing.Size(75, 28);
            this.butReg.TabIndex = 21;
            this.butReg.Text = "Registrer";
            this.butReg.UseVisualStyleBackColor = true;
            this.butReg.Click += new System.EventHandler(this.butReg_Click);
            // 
            // Budskjema
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(213, 135);
            this.Controls.Add(this.butReg);
            this.Controls.Add(this.numBud);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtEnr);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Budskjema";
            this.Text = "Budskjema";
            ((System.ComponentModel.ISupportInitialize)(this.numBud)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtEnr;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numBud;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button butReg;
    }
}