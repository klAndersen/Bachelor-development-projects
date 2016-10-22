using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using modellTog;

namespace guiModellTog {
    public partial class frmMeny : Form {
        #region VARIABLER
        //konstanter pga disse brukes og kalles flere plasser
        //dersom noen ikke er i bruk blir de fortsatt liggende hvis behov for
        //utvidelse/senere endringer/oppdateringer
        public const string TOG = "TOG";
        public const string VOGN = "VOGN";
        public const string LAND = "LAND";
        public const string ANT_VOGN = "ANT_VOGN";
        public const string TILBEHOR = "TILBEHOR";
        public const string PRODUSENT = "PRODUSENT";
        public const string SALGSSTED = "SALGSSTED";
        public const string ONSKELISTE = "ONSKELISTE";
        //konstant som inneholder navnet på filen ønskelisten lagres i
        public const string TXT_ONSKELISTE = "Ønskeliste.txt";
        //oppretting av variabel for opplisting av alle, siden denne kalles flere plasser
        private frmVisAlle visAlle;
        //oppretting av variabel for registreringsform
        private frmRegistrering registrering;

        #region Arrays
        private string[] modellAlternativer = {
                                                  "Registrer nytt lokomotiv",
                                                  "Registrer ny vogn",
                                                  "Legg til eksisterende vogn",
                                                  "Registrer nytt tilbehør",
                                                  "Registrer nytt ønske"
                                              };

        private string[] visModeller = {
                                           "Vis alle lokomotiv",
                                           "Vis alle vogner",
                                           "Vis alt tilbehør",
                                           "Vis ønskeliste"
                                       };
        #endregion

        #endregion

        #region FORMLOAD

        public frmMeny() {
            InitializeComponent();
        } //konstruktør

        private void frmMeny_Load(object sender, EventArgs e) {
            //fyll combobox med registreringsalternativer
            for (int i = 0; i < modellAlternativer.Length; i++) {
                cmbRegAlternativ.Items.Add(modellAlternativer[i]);
            } //for
            //fyll combobox med visninger
            for (int i = 0; i < visModeller.Length; i++) {
                cmbVisAlle.Items.Add(visModeller[i]);
            } //for
            //sett første alternativ som valgt
            cmbRegAlternativ.SelectedIndex = 0;
            cmbVisAlle.SelectedIndex = 0;
            //sett bakgrunnsfargen på kontrollene
            menuMain.BackColor = Color.PeachPuff;
            cmdRegModell.BackColor = Color.PeachPuff;
            cmdVisValgte.BackColor = Color.PeachPuff;
            cmdSokModell.BackColor = Color.PeachPuff;
            cmdAvslutt.BackColor = Color.PeachPuff;
            //last inn hjelpen
            try {
                //hent hjelpefilen
                string hjelpefil = "ModellTog_Hjelp.chm";
                //sett namespace til HelpProvider
                hpHjelp.HelpNamespace = hjelpefil;
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Feilmelding");
            } //try/catch
        } //frmMeny_Load

        #endregion

        #region MENYBAR

        #region FILE
        private void tsmSkrivTilFil_Click(object sender, EventArgs e) {
            try {
                bool lagret = Kontroll.kontrollInstans.skrivOnskeTilFil();
                if (lagret) {
                    if (MessageBox.Show("Ønskeliste ble opprettet. \nVil du åpne tekstfilen?", "Lagret", 
                            MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes) {
                        System.Diagnostics.Process.Start(TXT_ONSKELISTE);
                    } //if (MessageBox.Show(...) 
                } //if (lagret)
            } catch (Exception ex) {
                MessageBox.Show("En feil oppsto: \n" + ex.Message);
            } //try/catch
        } //tsmSkrivTilFil_Click

        private void tsmAvslutt_Click(object sender, EventArgs e) {
            //avslutter programmet
            this.Close();
        } //tsmAvslutt_Click

        private void tsmLeggTilLand_Click(object sender, EventArgs e) {
            frmInput inputBoks = new frmInput(frmMeny.LAND);
            inputBoks.ShowDialog(this);
        } //tsmLeggTilLand_Click

        private void tsmLeggTilProdusent_Click(object sender, EventArgs e) {
            frmInput inputBoks = new frmInput(frmMeny.PRODUSENT);
            inputBoks.ShowDialog(this);
        } //tsmLeggTilProdusent_Click

        private void tsmLeggTilSalgssted_Click(object sender, EventArgs e) {
            frmInput inputBoks = new frmInput(frmMeny.SALGSSTED);
            inputBoks.ShowDialog(this);
        } //tsmLeggTilSalgssted_Click

        private void tsmRedigerSlett_Click(object sender, EventArgs e) {
            frmDetaljer detalj = new frmDetaljer();
            detalj.ShowDialog(this);
        } //tsmRedigerSlett_Click
        #endregion

        #region REGISTRERING
        private void tsmRegLokomotiv_Click(object sender, EventArgs e) {
            //registrering av nytt lokomotiv
            registrering = new frmRegistrering(TOG);
            registrering.ShowDialog(this);
        } //tsmRegLokomotiv_Click

        private void tsmRegVogn_Click(object sender, EventArgs e) {
            //registrering av ny vogn
            registrering = new frmRegistrering(VOGN);
            registrering.ShowDialog(this);
        } //tsmRegVogn_Click

        private void tsmLeggTilVogn_Click(object sender, EventArgs e) {
            //øk antall vogner (legg til nytt antall)
            frmInput inputBoks = new frmInput(ANT_VOGN);
            inputBoks.ShowDialog(this);
        } //tsmLeggTilVogn_Click

        private void tsmRegTilbehor_Click(object sender, EventArgs e) {
            //registrering av nytt tilbehør
            registrering = new frmRegistrering(TILBEHOR);
            registrering.ShowDialog(this);
        } //tsmRegTilbehor_Click

        private void tsmRegOnske_Click(object sender, EventArgs e) {
            //registrering av nytt ønske
            registrering = new frmRegistrering(ONSKELISTE);
            registrering.ShowDialog(this);
        } //tsmRegOnske_Click
        #endregion

        #region VISNING
        private void tsmVisAlleLokomotiv_Click(object sender, EventArgs e) {
            frmVisAlle visAlle = new frmVisAlle(TOG);
            visAlle.ShowDialog(this);
        } //tsmVisAlleLokomotiv_Click

        private void tsmVisAlleVogner_Click(object sender, EventArgs e) {
            frmVisAlle visAlle = new frmVisAlle(VOGN);
            visAlle.ShowDialog(this);
        } //tsmVisAlleVogner_Click

        private void tsmVisAltTilbehor_Click(object sender, EventArgs e) {
            frmVisAlle visAlle = new frmVisAlle(TILBEHOR);
            visAlle.ShowDialog(this);
        } //tsmVisAltTilbehor_Click

        private void tsmVisAlleOnsker_Click(object sender, EventArgs e) {
            frmVisAlle visAlle = new frmVisAlle(ONSKELISTE);
            visAlle.ShowDialog(this);
        } //tsmVisAlleOnsker_Click
        #endregion

        #region SØKING
        private void tsmSoking_Click(object sender, EventArgs e) {
            frmSok sok = new frmSok();
            sok.ShowDialog(this);
        } //tsmSoking_Click
        #endregion

        #region HJELP
        private void tsmHjelp_Click(object sender, EventArgs e) {
            try {
                //vis hjelpen
                System.Diagnostics.Process.Start(hpHjelp.HelpNamespace);
            } catch (Win32Exception ex) {
                MessageBox.Show("Kan ikke vise hjelpefilen.\n" + ex.Message, "Kan ikke vise hjelpen");
            } catch (Exception ex) {
                MessageBox.Show("Kan ikke vise hjelpefilen.\n" + ex.Message, "Kan ikke vise hjelpen");
            } //try/catch
        } //tsmHjelp_Click

        private void tsmAbout_Click(object sender, EventArgs e) {
            frmAbout about = new frmAbout();
            about.ShowDialog(this);
        } //tsmAbout_Click
        #endregion

        #endregion

        #region KNAPPER

        private void cmdRegModell_Click(object sender, EventArgs e) {
            switch (cmbRegAlternativ.SelectedIndex) {
                case 0: 
                    //registrering av nytt lokomotiv
                    registrering = new frmRegistrering(TOG);
                    registrering.ShowDialog(this);
                    break;
                case 1:
                    //registrering av ny vogn
                    registrering = new frmRegistrering(VOGN);
                    registrering.ShowDialog(this);
                    break;
                case 2: 
                    //øk antall vogner (legg til nytt antall)
                    frmInput inputBoks = new frmInput(ANT_VOGN);
                    inputBoks.ShowDialog(this);
                    break;
                case 3:
                    //registrering av nytt tilbehør
                    registrering = new frmRegistrering(TILBEHOR);
                    registrering.ShowDialog(this);
                    break;
                case 4:
                    //registrering av nytt ønske
                    registrering = new frmRegistrering(ONSKELISTE);
                    registrering.ShowDialog(this);
                    break;
            } //switch
        } //cmdRegModell_Click

        private void cmdVisValgte_Click(object sender, EventArgs e) {
            switch (cmbVisAlle.SelectedIndex) {
                case 0:
                    //vis alle lokomotiv
                    visAlle = new frmVisAlle(TOG);
                    break;
                case 1:
                    //vis alle vogner
                    visAlle = new frmVisAlle(VOGN);
                    break;
                case 2:
                    //vis alt tilbehør
                    visAlle = new frmVisAlle(TILBEHOR);
                    break;
                case 3:
                    //vis ønskeliste
                    visAlle = new frmVisAlle(ONSKELISTE);
                    break;
            } //switch
            visAlle.ShowDialog(this);
        } //cmdVisValgte_Click

        private void cmdSokModell_Click(object sender, EventArgs e) {
            frmSok sokeSkjema = new frmSok();
            sokeSkjema.ShowDialog(this);
        } //cmdSokModell_Click

        private void cmdAvslutt_Click(object sender, EventArgs e) {
            //avslutter programmet
            this.Close();
        } //cmdAvslutt_Click

        #endregion
    } //frmMeny
} //namespace