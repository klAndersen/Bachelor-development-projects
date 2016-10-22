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
    public partial class Menyskjema : Form
    {
        public Menyskjema()
        {
            InitializeComponent();
        }

        private void radBy_CheckedChanged(object sender, EventArgs e)
        
        {
            txtAdresse.Enabled = !txtAdresse.Enabled;
            txtMatrikkel.Enabled = !txtMatrikkel.Enabled;
        }

        private void sjekkEnr(object sender, KeyPressEventArgs e)
        //Fjerner fra input alle tegn som ikke er siffer
        {
            if (!(char.IsDigit(e.KeyChar) | e.KeyChar == (Char)8)) //tillat siffer og backspace
                e.Handled=true;         
        }

        private void butTøm_Click(object sender, EventArgs e)
        {
            tømSkjema();
        }

        private void butReg_Click(object sender, EventArgs e)
        {
            try
            {
                int enr;
                Etype etype = (Etype)cboType.SelectedItem;
                int verditakst = (int)numTakst.Value;
                bool solgt = radSolgt.Checked;

                if (!int.TryParse(txtEnr.Text, out enr)) //Enr er ikke OK
                    MessageBox.Show("Du må fylle ut Enr (kan ikke forstås som et nummer)");
                else //Enr er utfyllt
                {
                    if (radBy.Checked) //By
                        Kontroll.kontroll.addByEiendom(enr, etype, verditakst, txtAdresse.Text, solgt);
                    else //Land
                        Kontroll.kontroll.addLandEiendom(enr, etype, verditakst, txtMatrikkel.Text, solgt);
                    MessageBox.Show("Registrert");
                    tømSkjema();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void butSøk_Click(object sender, EventArgs e)
        {
            SøkeSkjema søkeskjema = new SøkeSkjema();
            søkeskjema.ShowDialog(this);
        }

        private void Menyskjema_Load(object sender, EventArgs e)
        {
            foreach (Etype t in Enum.GetValues(typeof(Etype)))
                cboType.Items.Add(t);
            cboType.SelectedIndex = 0;
        }
        private void tømSkjema()
        {
            txtEnr.Text = "";
            numTakst.Value = 0;
            cboType.SelectedIndex = 0;
            radUsolgt.Checked = true;
            radBy.Checked = true;
            txtAdresse.Text = "";
            txtMatrikkel.Text = "";
            txtEnr.Focus();
        }

        private void butLagreAlt_Click(object sender, EventArgs e)
        {
            try
            {
                Kontroll.kontroll.lagreAlt();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void butBud_Click(object sender, EventArgs e)
        {
            Budskjema budskjema = new Budskjema();
            budskjema.ShowDialog();
        }
    }
}
