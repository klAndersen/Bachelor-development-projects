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
    public partial class SøkeSkjema : Form
    {
        public SøkeSkjema()
        {
            InitializeComponent();
        }
        private void sjekkEnr(object sender, KeyPressEventArgs e)
        //Fjerner fra input alle tegn som ikke er siffer
        {
            if (!(char.IsDigit(e.KeyChar) | e.KeyChar == (Char)8)) //tillat siffer og backspace
                e.Handled = true;
        }

        private void butTøm_Click(object sender, EventArgs e)
        //Tømmer skjemaets verdier
        {
            txtEnr.Text = "";
            cboType.SelectedIndex = 0;
            numTiltakst.Minimum = 0;
            numFratakst.Value = 0;
            numTiltakst.Value = 0;
            lstVis.Items.Clear();
            lblSlettnr.Text = "";
            butSlett.Enabled = false;
            txtEnr.Focus();
        }

        private void numFratakst_ValueChanged(object sender, EventArgs e)
        //Passer på at "til" alltid er større eller lik "fra"
        {
            if (numFratakst.Value > numTiltakst.Value)
            {
                numTiltakst.Minimum = numFratakst.Value;
            }
        }

        private void butAlle_Click(object sender, EventArgs e)
        {
            visAlle();
        }

        private void butEnr_Click(object sender, EventArgs e)
        {
            int enr;
            int.TryParse(txtEnr.Text, out enr); //gir evt. 0
            lstVis.Items.Clear();
            Eiendom eiendom = Kontroll.kontroll.findEiendom(enr);
            if (eiendom == null)
                lstVis.Items.Add("Eiendommen " + enr + " er ikke funnet (glemte du å fylle inn Enr?)");
            else
                lstVis.Items.Add(eiendom);
        }

        private void SøkeSkjema_Load(object sender, EventArgs e)
        {
            foreach (Etype t in Enum.GetValues(typeof(Etype)))
                cboType.Items.Add(t);
            cboType.SelectedIndex = 0;
        }

        private void butSted_Click(object sender, EventArgs e)
        {
            Etype etype = (Etype)cboType.SelectedItem;
            lstVis.Items.Clear();
            List<Eiendom> eiendomsliste = Kontroll.kontroll.findEiendom(etype);
            if (eiendomsliste.Count == 0) lstVis.Items.Add
                ("Ingen eiendommer av type " + etype + " ble funnet");
            foreach (Eiendom eiendom in eiendomsliste)
                lstVis.Items.Add(eiendom);
        }

        private void butTakst_Click(object sender, EventArgs e)
        {
            int fraTakst = (int)numFratakst.Value;
            int tilTakst = (int)numTiltakst.Value;
            lstVis.Items.Clear();
            List<Eiendom> eiendomsliste = Kontroll.kontroll.findEiendom(fraTakst, tilTakst);
            if(eiendomsliste.Count==0) lstVis.Items.Add
                ("Ingen eiendommer funnet med takst f.o.m. " + fraTakst.ToString("#,##0")
                + " t.o.m. " + tilTakst.ToString("#,##0"));
            foreach (Eiendom eiendom in eiendomsliste)
                lstVis.Items.Add(eiendom);
        }

        private void lstVis_SelectedIndexChanged(object sender, EventArgs e)
        {
            //la inn denne testen her pga hvis man ved uhell klikker inni listeboksen uten å ha valgt noen
            //så krasjer ikke bare applikasjonen, men endringer gjort blir ikke lagret i db4o
            try { //prøv å hent ut eiendomsnr
                butSlett.Enabled = true;
                lblSlettnr.Text = ((Eiendom)lstVis.SelectedItem).getEnr().ToString();
            } catch(Exception) { //fang eventuell feil
                MessageBox.Show("Du må klikke på eiendommen i listen for å kunne slette denne.");
            } //try/catch
        } //lstVis

        private void butSlett_Click(object sender, EventArgs e)
        {
            int nr;
            if (int.TryParse(lblSlettnr.Text, out nr)) 
                Kontroll.kontroll.delEiendom(nr);
            visAlle();
        }
            private void visAlle()
            {
                lstVis.Items.Clear();
                if (Kontroll.kontroll.countEiendom() == 0)
                    lstVis.Items.Add("Ingen eiendommer er registert");
                foreach (Eiendom eiendom in Kontroll.kontroll.getEiendomsliste())
                    lstVis.Items.Add(eiendom);
                butSlett.Enabled = false;
                lblSlettnr.Text = "";
            }
    }
}
