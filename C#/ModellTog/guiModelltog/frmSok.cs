using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using modellTog;

/**
 * Her er intet implementert...
 */

namespace guiModellTog {
    public partial class frmSok : Form {

        public frmSok() {
            InitializeComponent();
        } //konstruktør

        private void cmdSokMnr_Click(object sender, EventArgs e) {
            try {
                int mNr = 0;
                //er innskrevet verdi konverterbar til integer?
                if (int.TryParse(txtMnr.Text, out mNr)) {
                        Spesifikasjon modell = Kontroll.kontrollInstans.sokMnr(mNr);
                        MessageBox.Show(modell.ToString(), "Resultat");
                } else {
                    MessageBox.Show("Modellnr oppgitt er feil, modellNr kan kun bestå av tall (0-9).", "Feil modellNr");
                } //if (int.TryParse(txtMnr.Text, out mNr))
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Feilmelding");
            } //try/catch 
        } //cmdSokMnr_Click

        private void cmdSokNavn_Click(object sender, EventArgs e) {
            try {
                Spesifikasjon modell = Kontroll.kontrollInstans.sokNavn(txtNavn.Text);
                MessageBox.Show(modell.ToString(), "Resultat");
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Feilmelding");
            } //try/catch 
        } //cmdSokNavn_Click

        private void cmdSokType_Click(object sender, EventArgs e) {
            try {
                Spesifikasjon modell = Kontroll.kontrollInstans.sokType(txtType.Text);
                MessageBox.Show(modell.ToString(), "Resultat");
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Feilmelding");
            } //try/catch 
        } //cmdSokType_Click

        private void cmdTilbake_Click(object sender, EventArgs e) {
            //lukker dette vinduet
            this.Close();
        } //cmdTilbake_Click

        private void frmSok_Load(object sender, EventArgs e) {
            //fargelegg knappene
            cmdSokMnr.BackColor = Color.PeachPuff;
            cmdSokNavn.BackColor = Color.PeachPuff;
            cmdSokType.BackColor = Color.PeachPuff;
            cmdTilbake.BackColor = Color.PeachPuff;
        } //frmSok_Load
    } //frmSok
} //namespace