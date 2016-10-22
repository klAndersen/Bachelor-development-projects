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
    public partial class frmInput : Form {
        private string visInput;
        private bool kanLukkes;

        #region FORM LOAD
        public frmInput(string visInput) {
            this.visInput = visInput;
            InitializeComponent();
        } //konstruktør

        private void frmInput_Load(object sender, EventArgs e) {
            //skjules pga skal kun vises ved registrering av ekstra vogner
            //fargelegg knappene
            cmdOk.BackColor = Color.PeachPuff;
            cmdCancel.BackColor = Color.PeachPuff;
            //opprett "inputboksen"
            opprettInputBoks();
            //sett fokus i tekstfeltet
            txtFelt1.Focus();
            //markerer alt i tekstfeltet
            txtFelt1.SelectAll();
        } //frmInput_Load

        private void opprettInputBoks() {
            switch (visInput) {
                case frmMeny.ANT_VOGN:
                    //skal registrere antall vogner
                    this.Text = "Registrer eksisterende vogn";
                    lblSkrivInn.Text = "Skriv inn modellnr:";
                    lblAntall.Visible = true;
                    txtFelt2.Visible = true;
                    cmdOk.Location = new Point(142, 127);
                    cmdCancel.Location = new Point(295, 127);
                    tipInput.SetToolTip(txtFelt1, "Skriv inn modellnr (kun siffer: 0-9)");
                    tipInput.SetToolTip(txtFelt2, "Skriv inn antall (kun siffer: 0-9) - husk at en vogn allerede er registrert");
                    break;
                case frmMeny.LAND:
                    //skal registrere nytt land
                    this.Text = "Registrer nytt land";
                    enkelInput();
                    lblSkrivInn.Text = "Skriv inn navnet på landet:";
                    tipInput.SetToolTip(txtFelt1, "Skriv inn landet");
                    break;
                case frmMeny.PRODUSENT:
                    //skal registrere ny produsent
                    this.Text = "Registrer ny produsent";
                    enkelInput();
                    lblSkrivInn.Text = "Skriv inn navnet på produsenten:";
                    tipInput.SetToolTip(txtFelt1, "Skriv inn navn på produsenten");
                    break;
                case frmMeny.SALGSSTED:
                    //skal registrere nytt salgssted
                    this.Text = "Registrer nytt salgssted";
                    enkelInput();
                    lblSkrivInn.Text = "Skriv inn navnet på salgsstedet:";
                    tipInput.SetToolTip(txtFelt1, "Skriv inn navn på salgsstedet");
                    break;
            } //switch
        } //opprettInputBoks

        private void enkelInput() {
            this.Height = 157;
            this.Width = 403;
            cmdOk.Location = new Point(142, 72);
            cmdCancel.Location = new Point(295, 72);
            lblAntall.Visible = false;
            txtFelt2.Visible = false;
        } //enkelInput
        #endregion

        private void cmdOk_Click(object sender, EventArgs e) {
            try {
                bool sjekk;
                switch (visInput) {
                    case frmMeny.ANT_VOGN: 
                        //skal registrere antall vogner
                        erAntallOk();
                        break;
                    case frmMeny.LAND: 
                        //skal registrere nytt land
                        sjekk = kontrollerDetalj(txtFelt1.Text, "land");
                        if (sjekk) {
                            Kontroll.kontrollInstans.addLand(txtFelt1.Text);
                            setKanLukkes(true);
                        } //if (sjekk)
                        break;
                    case frmMeny.PRODUSENT: 
                        //skal registrere ny produsent
                        sjekk = kontrollerDetalj(txtFelt1.Text, "produsent");
                        if (sjekk) {
                            Kontroll.kontrollInstans.addProdusent(txtFelt1.Text);
                            setKanLukkes(true);
                        } //if (sjekk)
                        break;
                    case frmMeny.SALGSSTED: 
                        //skal registrere nytt salgssted
                        sjekk = kontrollerDetalj(txtFelt1.Text, "salgssted");
                        if (sjekk) {
                            Kontroll.kontrollInstans.addSalgssted(txtFelt1.Text);
                            setKanLukkes(true);
                        } //if (sjekk)
                        break;
                } //switch
                //kan formen lukkes?
                if (getKanLukkes()) {
                    //lukker form
                    this.Close();
                } else { //form kan ikke lukkes
                    //sett fokus i første tekstfelt
                    txtFelt1.Focus();
                } //if (kanLukkes())
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            } //try/catch
        } //cmdOk_Click

        private void cmdCancel_Click(object sender, EventArgs e) {
            this.Close(); //lukker "inputboksen"
        } //cmdCancel_Click

        #region LEGG TIL VOGNER
        private void erAntallOk() {
            bool ok = kontrollerAntall(txtFelt1.Text, "modellnummer");
            //er første tekstfelt ok?
            if (ok) {
                ok = kontrollerAntall(txtFelt2.Text, "antall");
                //er andre tekstfelt ok?
                if (ok) {
                    int mNr = int.Parse(txtFelt1.Text);
                    int antall = int.Parse(txtFelt2.Text);
                    registrerVogner(mNr, antall);
                } else {
                    txtFelt2.Focus();
                    txtFelt2.SelectAll();
                } //indre if (ok)
            } else {
                txtFelt1.Focus();
                txtFelt1.SelectAll();
            } //ytre if (ok)
        } //erAntallOk

        private bool kontrollerAntall(string input, string type) {
            try {
                //er noe skrivd inn?
                if (input.Equals("")) {
                    MessageBox.Show("Feltet for " + type + " er tomt. Du må skrive inn "
                                    + type + ".", "Tomt felt");
                } else { //noe er skrivd inn, sjekk om det er et heltall
                    //forsøk å omgjøre til et heltall
                    int.Parse(input);
                    return true;
                } //if (input.Equals(""))
            } catch (FormatException) { //ikke et tall innskrevet
                type = Kontroll.kontrollInstans.storForbokstav(type);
                MessageBox.Show(type + " må være et heltall (siffer).", "Ikke et siffer");
            } catch (Exception e) { //en uventet feil oppsto
                MessageBox.Show("En feil oppsto: " + e, "Uventet feil");
            } //try/catch
            return false;
        } //kontrollerAntall

        private void registrerVogner(int mNr, int antall) {
            //oppdater antallet vogner
            Kontroll.kontrollInstans.updateAntallVogner(mNr, antall);
            //sett at formen kan lukkes
            setKanLukkes(true);
        } //registrerVogner
        #endregion

        #region LEGG TIL DETALJ
        private bool kontrollerDetalj(string input, string type) {
            //er noe skrevet inn i tekstfeltet?
            if (input.Equals("")) {
                MessageBox.Show("Feltet for " + type + " er tomt. Du må skrive inn " + type + ".", "Tomt felt");
                return false;
            } else if (input.Length < 2) { //er det som er innskrevet mindre enn to tegn?
                MessageBox.Show("Feltet for " + type + " inneholder mindre enn 2 tegn.", "For få tegn");
                return false;
            } //if (input.Equals(""))
            return true;
        } //kontrollerDetalj
        #endregion

        private bool getKanLukkes() {
            return kanLukkes;
        } //getKanLukkes

        private void setKanLukkes(bool kanLukkes) {
            this.kanLukkes = kanLukkes;
        } //setKanLukkes
    } //frmInput
} //namespace