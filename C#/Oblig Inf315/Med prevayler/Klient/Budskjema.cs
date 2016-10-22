using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Server;

namespace Klient
{
    public partial class Budskjema : Form
    {
        public Budskjema()
        {
            InitializeComponent();
        }
        private void txtEnr_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) | e.KeyChar==(Char)8))  //tillat siffer og backspace
                e.Handled = true; 
        }
        private void butReg_Click(object sender, EventArgs e)
        {
            try
            {
                int enr;
                int bud = (int)numBud.Value;
                if (!int.TryParse(txtEnr.Text, out enr))
                    MessageBox.Show("Du må fylle ut Enr (kan ikke forstås som et nummer)");
                else
                {
                    Kontroll.kontroll.addBud(enr, bud);
                    MessageBox.Show("Registrert");
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void tømSkjema()
        {
            txtEnr.Text = "";
            numBud.Value = 0;
        }
    }
}
