using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using modellTog;
using System.Text.RegularExpressions;

namespace guiModellTog {
    public partial class frmRegistrering : Form {
        private const int MODELL = 7;
        private const int TILBEHOR = 5;
        private const int ONSKE = 4;
        private string registrering;
        private frmInput inputBoks;
        private bool kanLukkes;
        //regulære uttrykk for tallverdi og årstall
        private string regUttrykk = "^[0-9]+$";
        private string regUttrykk_Aar = @"^[0-9]{4}$";
        private string regUttrykk_Epoke = @"^([0-9]{4})\-([0-9]{4})$";

        #region FORMLOAD

        public frmRegistrering(string registrering) {
            this.registrering = registrering;
            InitializeComponent();                
        } //konstruktør

        private void frmRegistrering_Load(object sender, EventArgs e) {
            //fargelegg knappene
            cmdLeggTil.BackColor = Color.PeachPuff;
            cmdLagre.BackColor = Color.PeachPuff;
            cmdTilbake.BackColor = Color.PeachPuff;
            //hent ut hva slags modell som skal registreres
            switch (registrering) {
                case frmMeny.TOG:
                    //henter tilhørende tooltips for tekstboksene
                    toolTipTogOgVogn();
                    //viser og skjuler tilhørende kontroller
                    visRegistrering(MODELL);
                    fyllComboBoxLand();
                    break;
                case frmMeny.VOGN:
                    //henter tilhørende tooltips for tekstboksene
                    toolTipTogOgVogn();
                    //viser og skjuler tilhørende kontroller
                    visRegistrering(MODELL);
                    fyllComboBoxLand();
                    break;
                case frmMeny.TILBEHOR:
                    //henter tilhørende tooltips for tekstboksene
                    toolTipTilbehor();
                    //viser og skjuler tilhørende kontroller
                    visRegistrering(TILBEHOR);
                    fyllComboBoxProdusent();
                    break;
                case frmMeny.ONSKELISTE:
                    //henter tilhørende tooltips for tekstboksene
                    toolTipOnsker();
                    //viser og skjuler tilhørende kontroller
                    visRegistrering(ONSKE);
                    fyllComboBoxSalgssted();
                    break;
            } //switch
        } //frmRegistrering_Load

        #region COMBOBOX

        private void fyllComboBoxLand() {
            cmbTilknytning.Items.Clear();
            foreach (Land land in Kontroll.kontrollInstans.returnLand()) {
                cmbTilknytning.Items.Add(land.getLand());
            } //foreach
            //er det land registrert?
            if (cmbTilknytning.Items.Count > 0) {
                cmbTilknytning.SelectedIndex = 0;
            } //if (cmbTilknytning.Items.Count > 0)
        } //fyllComboBoxLand

        private void fyllComboBoxProdusent() {
            cmbTilknytning.Items.Clear();
            foreach (Produsent prod in Kontroll.kontrollInstans.returnProdusent()) {
                cmbTilknytning.Items.Add(prod.getProdusent());
            } //foreach
            //er det produsenter registrert?
            if (cmbTilknytning.Items.Count > 0) {
                cmbTilknytning.SelectedIndex = 0;
            } //if (cmbTilknytning.Items.Count > 0)
        } //fyllComboBoxProdusent

        private void fyllComboBoxSalgssted() {
            cmbTilknytning.Items.Clear();
            foreach (Salgssted sted in Kontroll.kontrollInstans.returnSalgssted()) {
                cmbTilknytning.Items.Add(sted.getSalgssted());
            } //foreach
            //er det salgssteder registrert?
            if (cmbTilknytning.Items.Count > 0) {
                cmbTilknytning.SelectedIndex = 0;
            } //if (cmbTilknytning.Items.Count > 0)
        } //fyllComboBoxSalgssted

        #endregion

        #region VIS REGISTRERINGER

        private void visRegistrering(int antall) {
            //tittel går igjen på alle utenom den siste
            string tittel = "Registrering av ";
            //brukes på alle (uten om label for comboboks)
            string start = "Skriv inn ";
            //skal det registreres et tog eller en vogn?
            if (antall == MODELL) {
                //er det et tog?
                if (registrering.Equals(frmMeny.TOG)) { 
                    tittel += "nytt lokomotiv";
                    cmdLagre.Text = "Lagre nytt lokomotiv";
                } else { //det er en vogn
                    tittel += "ny vogn";
                    cmdLagre.Text = "Lagre ny vogn";
                } //if (registrering.Equals(frmRegMeny.TOG))
                //hent ut de første label's
                fellesLabel(start);
                lblVerdi5.Text = start + "årsmodell:";
                lblVerdi6.Text = start + "størrelse:";
                lblVerdi7.Text = "Velg land:";
                visLokomotiv();
            } else if (antall == TILBEHOR) { //skal registrere tilbehør
                tittel += "nytt tilbehør";
                fellesLabel(start);
                lblVerdi5.Text = "Velg produsent:";
                visTilbehor();
            } else if (antall == ONSKE) { //skal registrere ønske
                tittel = "Registrer et nytt ønske";
                lblVerdi1.Text = start + "modellnr:";
                lblVerdi2.Text = start + "navn:";
                lblVerdi3.Text = start + "pris:";
                lblVerdi4.Text = "Velg salgssted:";
                visOnskeliste();
            } //if (antall == 7)            
            lblTittel.Text = tittel;
        } //visRegistrering

        private void fellesLabel(string start) {
            lblVerdi1.Text = start + "modellnr:";
            lblVerdi2.Text = start + "navn:";
            lblVerdi3.Text = start + "type:";
            lblVerdi4.Text = start + "pris:";
        } //fellesLabel

        private void visLokomotiv() {
            //labels
            cmdLeggTil.Text = "Legg til nytt land";
            lblVerdi4.Visible = true;
            lblVerdi5.Visible = true;
            lblVerdi6.Visible = true;
            lblVerdi7.Visible = true;
            //tekstfelt
            txtVerdi3.Visible = true;
            txtVerdi4.Visible = true;
            txtVerdi5.Visible = true;
            txtVerdi5.MaxLength = 9;
            txtVerdi6.Visible = true;
        } //registrerLokomotiv

        private void visTilbehor() {
            this.Height = 445;
            cmdLeggTil.Text = "Legg til ny produsent";
            cmdLagre.Text = "Lagre nytt tilbehør";
            //setter ny lokasjon for combobox og knapper
            cmbTilknytning.Location = new Point(207, 227);
            cmdLeggTil.Location = new Point(26, 267);
            cmdLagre.Location = new Point(26, 314);
            cmdTilbake.Location = new Point(26, 361);
            //labels            
            lblVerdi4.Visible = true;
            lblVerdi5.Visible = true;
            //tekstfelt
            txtVerdi3.Visible = true;
            txtVerdi4.Visible = true;
            //skjul labels
            lblVerdi6.Visible = false;
            lblVerdi7.Visible = false;
            //skjul tekstfelt
            txtVerdi5.Visible = false;
            txtVerdi6.Visible = false;
        } //registrerTilbehor

        private void visOnskeliste() {
            //setter ny høyde for frmRegistrering
            this.Height = 420;
            cmdLeggTil.Text = "Legg til nytt salgssted";
            cmdLagre.Text = "Lagre nytt ønske";
            //setter ny lokasjon for combobox og knapper
            cmbTilknytning.Location = new Point(207, 180);
            cmdLeggTil.Location = new Point(26, 240);
            cmdLagre.Location = new Point(26, 287);
            cmdTilbake.Location = new Point(26, 334);
            txtVerdi3.Visible = true;
            lblVerdi4.Visible = true;
            //skjul labels
            lblVerdi5.Visible = false;
            lblVerdi6.Visible = false;
            lblVerdi7.Visible = false;
            //skjul tekstfelt
            txtVerdi4.Visible = false;
            txtVerdi5.Visible = false;
            txtVerdi6.Visible = false;
        } //registrerOnskeliste
        #endregion

        #endregion

        #region KNAPPER

        private void cmdLeggTil_Click(object sender, EventArgs e) {
            switch (registrering) {
                case frmMeny.TOG:
                    inputBoks = new frmInput(frmMeny.LAND);
                    inputBoks.ShowDialog(this);
                    fyllComboBoxLand();
                    break;
                case frmMeny.VOGN:
                    inputBoks = new frmInput(frmMeny.LAND);
                    inputBoks.ShowDialog(this);
                    fyllComboBoxLand();
                    break;
                case frmMeny.TILBEHOR:
                    inputBoks = new frmInput(frmMeny.PRODUSENT);
                    inputBoks.ShowDialog(this);
                    fyllComboBoxProdusent();
                    break;
                case frmMeny.ONSKELISTE:
                    inputBoks = new frmInput(frmMeny.SALGSSTED);
                    inputBoks.ShowDialog(this);
                    fyllComboBoxSalgssted();
                    break;
            } //switch
        } //cmdLeggTil_Click

        private void cmdLagre_Click(object sender, EventArgs e) {
            switch (registrering) {
                case frmMeny.TOG:
                    kontrollerModell(registrering);
                    break;
                case frmMeny.VOGN:
                    kontrollerModell(registrering);
                    break;
                case frmMeny.TILBEHOR:
                    kontrollerTilbehor();
                    break;
                case frmMeny.ONSKELISTE:
                    kontrollerOnske();
                    break;
            } //switch
            //sjekk om formen kan lukkes
            if (getKanLukkes()) {
                this.Close();
            } //if (getKanLukkes())
        } //cmdLagre_Click

        private void cmdTilbake_Click(object sender, EventArgs e) {
            this.Close();
        } //cmdTilbake_Click

        #endregion

        #region KONTROLLER INNHOLD

        private void kontrollerModell(string modellObjekt) {
            string tittel = "Tomt felt";
            double pris = 0;
            double strl = 0;
            if (txtVerdi1.Text.Equals("")) {
                //sett fokus
                txtVerdi1.Focus();
                //gi feilmelding
                MessageBox.Show("Modellnr må skrives inn.", tittel);
            } else if (!(Regex.IsMatch(txtVerdi1.Text, regUttrykk))) { 
                //består tekstboks kun av tall?
                MessageBox.Show("Modellnr består bare av tall og må oppgis med numeriske verdier (0-9).", "Kun tall");
                txtVerdi1.Focus();
                txtVerdi1.SelectAll();
            } else if (txtVerdi2.Text.Equals("")) {
                //sett fokus
                txtVerdi2.Focus();
                //gi feilmelding
                MessageBox.Show("Navn må skrives inn.", tittel);
            } else if (txtVerdi3.Text.Equals("")) {
                //sett fokus
                txtVerdi3.Focus();
                //gi feilmelding
                MessageBox.Show("Type må skrives inn.", tittel);
            } else if (txtVerdi4.Text.Equals("")) {
                //sett fokus
                txtVerdi4.Focus();
                //gi feilmelding
                MessageBox.Show("Pris må skrives inn.", tittel);
            } else if (!(Double.TryParse(txtVerdi4.Text, out pris))) {
                //er pris konverterbar til double?
                txtVerdi4.Focus();
                txtVerdi4.SelectAll();
                MessageBox.Show("Pris oppgitt er feil." 
                    + "\nPris må bestå av tall (0-9) og ha komma (,) som skilletegn.", "Ugyldig pris");
            } else if (txtVerdi5.Text.Equals("")) {
                //sett fokus
                txtVerdi5.Focus();
                //gi feilmelding
                if (registrering == frmMeny.TOG) {
                    MessageBox.Show("Årsmodell må skrives inn (fire siffer).", tittel);
                } else {
                    MessageBox.Show("Epoke må skrives inn (Eks: 1990-1995).", tittel);
                } //if (registrering == frmMeny.TOG)
            } else if (registrering == frmMeny.TOG && !(Regex.IsMatch(txtVerdi5.Text, regUttrykk_Aar))) {
                //består tekstboks kun av årstall?
                txtVerdi5.Focus();
                txtVerdi5.SelectAll();
                MessageBox.Show("Årsmodell skal kun bestå av fire siffer (0-9).", "Ugyldig årsmodell");
            } else if (registrering == frmMeny.VOGN && !(Regex.IsMatch(txtVerdi5.Text, regUttrykk_Epoke))) {
                //består tekstboks kun av årstall?
                txtVerdi5.Focus();
                txtVerdi5.SelectAll();
                MessageBox.Show("Epoke kan kun bestå av siffer og bindestrek (Eks: 1990-1995).", "Ugyldig årsmodell");
            } else if (txtVerdi6.Text.Equals("")) {
                //sett fokus
                txtVerdi6.Focus();
                //gi feilmelding
                MessageBox.Show("Størrelse må skrives inn.", tittel);
            } else if (!(Double.TryParse(txtVerdi6.Text, out strl))) {
                //er størrelse konverterbar til double?
                txtVerdi6.Focus();
                txtVerdi6.SelectAll();
                MessageBox.Show("Størrelse oppgitt er feil."
                    + "\nStørrelse må bestå av tall (0-9) og ha komma (,) som skilletegn.", "Ugyldig størrelse");
            } else if (cmbTilknytning.Text.Equals("")) {
                MessageBox.Show("Ingen land er registrert."
                    + "\nDu må legge til et land før modellen kan lagres.", tittel);
            } else { //alt ok, registrer den nye modellen
                registrerNyModell(modellObjekt, pris, strl);
            } //if (txtVerdi1.Text.Equals(""))
        } //kontrollerModell

        private void kontrollerTilbehor() {
            string tittel = "Tomt felt";
            double pris = 0;
            if (txtVerdi1.Text.Equals("")) {
                //sett fokus
                txtVerdi1.Focus();
                //gi feilmelding
                MessageBox.Show("Modellnr må skrives inn.", tittel);
            } else if (!(Regex.IsMatch(txtVerdi1.Text, regUttrykk))) {
                //består tekstboks kun av tall?
                MessageBox.Show("Modellnr består bare av tall og må oppgis med numeriske verdier (0-9).", "Kun tall");
                txtVerdi1.Focus();
                txtVerdi1.SelectAll();
            } else if (txtVerdi2.Text.Equals("")) {
                //sett fokus
                txtVerdi2.Focus();
                //gi feilmelding
                MessageBox.Show("Navn må skrives inn.", tittel);
            } else if (txtVerdi3.Text.Equals("")) {
                //sett fokus
                txtVerdi3.Focus();
                //gi feilmelding
                MessageBox.Show("Type må skrives inn.", tittel);
            } else if (txtVerdi4.Text.Equals("")) {
                //sett fokus
                txtVerdi4.Focus();
                //gi feilmelding
                MessageBox.Show("Pris må skrives inn.", tittel);
            } else if (!(Double.TryParse(txtVerdi4.Text, out pris))) {
                //er pris konverterbar til double?
                txtVerdi4.Focus();
                txtVerdi4.SelectAll();
                MessageBox.Show("Pris oppgitt er feil."
                    + "\nPris må bestå av tall (0-9) og ha komma (,) som skilletegn.", "Ugyldig pris");
            } else if (cmbTilknytning.Text.Equals("")) {
                MessageBox.Show("Det finnes ikke produsenter. "
                    + "\nDu må legge til en produsent før du kan registrere tilbehøret.", tittel);
            } else {
                registrerTilbehor(pris);
            } //if (txtVerdi1.Text.Equals(""))
        } //kontrollerTilbehor

        private void kontrollerOnske() {
            string tittel = "Tomt felt";
            double pris = 0;
            if (txtVerdi1.Text.Equals("")) {
                //sett fokus
                txtVerdi1.Focus();
                //gi feilmelding
                MessageBox.Show("Modellnr må skrives inn.", tittel);
            } else if (!(Regex.IsMatch(txtVerdi1.Text, regUttrykk))) {
                //består tekstboks kun av tall?
                MessageBox.Show("Modellnr består bare av tall og må oppgis med numeriske verdier (0-9).", "Kun tall");
                txtVerdi1.Focus();
                txtVerdi1.SelectAll();
            } else if (txtVerdi2.Text.Equals("")) {
                //sett fokus
                txtVerdi2.Focus();
                //gi feilmelding
                MessageBox.Show("Navn må skrives inn.", tittel);
            } else if (txtVerdi3.Text.Equals("")) {
                //sett fokus
                txtVerdi3.Focus();
                //gi feilmelding
                MessageBox.Show("Pris må skrives inn.", tittel);
            } else if (!(Double.TryParse(txtVerdi3.Text, out pris))) {
                //er pris konverterbar til double?
                txtVerdi3.Focus();
                txtVerdi3.SelectAll();
                MessageBox.Show("Pris oppgitt er feil."
                    + "\nPris må bestå av tall (0-9) og ha komma (,) som skilletegn.", "Ugyldig pris");
            } else if (cmbTilknytning.Text.Equals("")) {
                MessageBox.Show("Det finnes ikke salgssteder. "
                    + "\nDu må legge til et salgssted før du kan registrere ønsket.", tittel);
            } else {
                registrerOnske(pris);
            } //if (txtVerdi1.Text.Equals(""))
        } //kontrollerOnske

        #endregion

        #region REGISTRER NY MODELL

        private void registrerNyModell(string modellObjekt, double pris, double strl) {
            try {
                //konverter til tallverdier
                int mNr = int.Parse(txtVerdi1.Text);
                int idLand = cmbTilknytning.SelectedIndex;
                string navn = txtVerdi2.Text;
                string type = txtVerdi3.Text;
                //er det et lokomotiv som skal registreres?
                if (modellObjekt.Equals(frmMeny.TOG)) {
                    int aarsmodell = int.Parse(txtVerdi5.Text);
                    Kontroll.kontrollInstans.addTog(mNr, navn, type, aarsmodell, pris, strl, idLand);
                    MessageBox.Show("Lokomotivet ble registrert.", "Registrert");
                } else { //vogn som skal registreres
                    string epoke = txtVerdi5.Text;
                    Kontroll.kontrollInstans.addVogn(mNr, navn, type, epoke, pris, strl, 1, idLand);
                    MessageBox.Show("Vognen ble registrert.", "Registrert");
                } //if (modellObjekt.Equals(frmMeny.TOG))
                //ta en snapshot
                Kontroll.kontrollInstans.lagreAlt();
                //sett at formen kan lukkes
                setKanLukkes(true);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Feilmelding");
            } //try /catch
        } //registrerNyModell

        private void registrerTilbehor(double pris) {
            try {
                int mNr = int.Parse(txtVerdi1.Text);
                string navn = txtVerdi2.Text;
                string type = txtVerdi3.Text;
                int idProd = cmbTilknytning.SelectedIndex;
                //registrer tilbehøret
                Kontroll.kontrollInstans.addTilbehor(mNr, navn, type, pris, idProd);
                //gi tilbakemelding
                MessageBox.Show("Tilbehøret ble registrert.", "Registrert");
                //ta en snapshot
                Kontroll.kontrollInstans.lagreAlt();
                //sett at formen kan lukkes
                setKanLukkes(true);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Feilmelding");
            } //try/catch
        } //registrerTilbehor

        private void registrerOnske(double pris) {
            try {
                int mNr = int.Parse(txtVerdi1.Text);
                string navn = txtVerdi2.Text;
                int idSted = cmbTilknytning.SelectedIndex;
                //registrer ønsket
                Kontroll.kontrollInstans.addOnskeliste(mNr, navn, pris, idSted);
                //gi tilbakemelding
                MessageBox.Show("Ønsket ble registrert.", "Registrert");
                //ta en snapshot
                Kontroll.kontrollInstans.lagreAlt();
                //sett at formen kan lukkes
                setKanLukkes(true);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Feilmelding");
            } //try/catch
        } //registrerOnske

        #endregion

        #region TOOLTIPS

        private void toolTipTogOgVogn() {
            toolTipTilbehor();
            tipRegistrering.SetToolTip(txtVerdi4, "Skriv inn modellens pris (kun siffer: 0-9 og komma som skilletegn)");
            tipRegistrering.SetToolTip(txtVerdi5, "Skriv inn årsmodellen (fire siffer: 0-9), \r\nhvis vogn skriv inn epoken (eks: 1959-1965)");
            tipRegistrering.SetToolTip(txtVerdi6, "Skriv inn modellens størrelse(kun siffer: 0-9 og komma som skilletegn)");
        } //toolTipTog

        private void toolTipTilbehor() {
            tipRegistrering.SetToolTip(txtVerdi1, "Skriv inn modellnr (kun siffer: 0-9)");
            tipRegistrering.SetToolTip(txtVerdi2, "Skriv inn modellens navn");
            tipRegistrering.SetToolTip(txtVerdi3, "Skriv inn modellens type");
        } //toolTipTilbehor

        private void toolTipOnsker() {
            tipRegistrering.SetToolTip(txtVerdi1, "Skriv inn modellnr (kun siffer: 0-9)");
            tipRegistrering.SetToolTip(txtVerdi2, "Skriv inn modellens navn");
            tipRegistrering.SetToolTip(txtVerdi3, "Skriv inn modellens pris (kun siffer: 0-9 og komma som skilletegn)");
        } //toolTipOnsker

        #endregion

        private bool getKanLukkes() {
            return kanLukkes;
        } //getKanLukkes

        private void setKanLukkes(bool kanLukkes) {
            this.kanLukkes = kanLukkes;
        } //setKanLukkes
    } //frmRegistrering
} //namespace