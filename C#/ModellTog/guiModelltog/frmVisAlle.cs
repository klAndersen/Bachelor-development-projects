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
    public partial class frmVisAlle : Form {
        private const int LOKOMOTIV = 7;
        private const int VOGNER = 8;
        private const int TILBEHOR = 5;
        private const int ONSKER = 4;
        private string visning;
        private string status;
        private bool lagret = false;
        //regulære uttrykk for tallverdi og årstall
        private string regUttrykk = "^[0-9]+$";
        private string regUttrykk_Aar = @"^[0-9]{4}$";
        private string regUttrykk_Epoke = @"^([0-9]{4})\-([0-9]{4})$";

        #region FORMLOAD
        public frmVisAlle(string visning) {
            this.visning = visning;
            InitializeComponent();
            //sett at backgroundworker støtter rapportering og kansellering
            bwUpdate.WorkerReportsProgress = true;
            bwUpdate.WorkerSupportsCancellation = true;
        } //konstruktør

        private void frmVisAlle_Load(object sender, EventArgs e) {
            string frmTekst = "Vis alle ";
            switch (visning) {
                case frmMeny.TOG:
                    //setter formens tekst
                    this.Text = frmTekst + "lokomotiv";
                    //setter formens størrelse
                    this.Size = new System.Drawing.Size(715, 633);
                    opprettDataGridView(LOKOMOTIV);
                    break;
                case frmMeny.VOGN:
                    //setter formens tekst
                    this.Text = frmTekst + "vogner";
                    //setter formens størrelse
                    this.Size = new System.Drawing.Size(815, 633);
                    opprettDataGridView(VOGNER);
                    break;
                case frmMeny.TILBEHOR:
                    //setter formens tekst
                    this.Text = "Vis alt tilbehør";
                    //setter formens størrelse
                    this.Size = new System.Drawing.Size(515, 633);
                    opprettDataGridView(TILBEHOR);
                    break;
                case frmMeny.ONSKELISTE:
                    //setter formens tekst
                    this.Text = frmTekst + "ønsker";
                    //setter formens størrelse
                    this.Size = new System.Drawing.Size(515, 633);
                    //vis knapp for å kunne skrive ut ønskeliste
                    cmdSkrivUt.Visible = true;
                    opprettDataGridView(ONSKER);
                    break;
            } //switch
            //fargelegg knappene
            cmdLagre.BackColor = Color.PeachPuff;
            cmdLukk.BackColor = Color.PeachPuff;
            cmdSkrivUt.BackColor = Color.PeachPuff;
            cmdSlett.BackColor = Color.PeachPuff;
            //sett status tekst i label
            lblStatus.Text = "Status: Innlasting vellykket!";
            //lås første kolonne i datagridview
            dgvModeller.Columns[0].ReadOnly = true;
        } //frmVisAlle_Load

        private void opprettDataGridView(int antKolonner) {
            //legg til kolonner
            dgvModeller.ColumnCount = antKolonner;
            //hvilken visning skal legges inn i datagridview?
            switch (antKolonner) {
                case LOKOMOTIV:
                    leggTilLokomotiv();
                    fyllDataGridViewLokomotiv();
                    break;
                case VOGNER:
                    leggTilVogner();
                    fyllDataGridViewVogner();
                    break;
                case TILBEHOR:
                    leggTilTilbehor();
                    fyllDataGridViewTilbehor();
                    break;
                case ONSKER:
                    // +1 pga ID er med i visning (brukes ved sletting)
                    dgvModeller.ColumnCount = antKolonner + 1;
                    leggTilOnsker();
                    fyllDataGridViewOnsker();
                    break;
            } //switch
            disableCmdSlett();
        } //opprettDataGridView

        private void disableCmdSlett() {
            //er datagridviewet tomt?
            if (dgvModeller.Rows.Count == 0) {
                //disable slette knapp
                cmdSlett.Enabled = false;
            } //if (dgvModeller.Rows.Count == 0)
        } //disableCmdSlett
        #endregion

        #region LEGG TIL KOLONNER

        private void leggTilLokomotiv() {
            leggTilTilbehor();
            //rename kolonnene for å tilpasse lokomotiv
            dgvModeller.Columns[4].Name = "columnAarsmodell";
            dgvModeller.Columns[5].Name = "columnStrl";
            dgvModeller.Columns[6].Name = "columnLand";
            //rename tekst i header for å tilpasse lokomotiv
            dgvModeller.Columns[4].HeaderText = "Årsmodell";
            dgvModeller.Columns[5].HeaderText = "Størrelse";
            //legg til land i visning
            leggTilLand(6);
        } //leggTilLokomotiv

        private void leggTilVogner() {
            leggTilTilbehor();
            //rename kolonnene for å tilpasse vogner
            dgvModeller.Columns[4].Name = "columnAarsmodell";
            dgvModeller.Columns[5].Name = "columnAntall";
            dgvModeller.Columns[6].Name = "columnStrl";
            dgvModeller.Columns[7].Name = "columnLand";
            //rename tekst i header for å tilpasse vogner
            dgvModeller.Columns[4].HeaderText = "Årsmodell";
            dgvModeller.Columns[5].HeaderText = "Antall";
            dgvModeller.Columns[6].HeaderText = "Størrelse";
            leggTilLand(7);
        } //leggTilVogner

        private void leggTilLand(int kolonneNr) {
            //av uviss grunn legges en kolonne ekstra kolonne til, så den må fjernes
            dgvModeller.Columns.RemoveAt(kolonneNr);
            //opprett en kolonne som inneholder comboboxer
            DataGridViewComboBoxColumn cmbKolonne = new DataGridViewComboBoxColumn();
            //fyll combobox
            foreach (Land land in Kontroll.kontrollInstans.returnLand()) {
                cmbKolonne.Items.Add(land.getLand());
            } //foreach       
            dgvModeller.Columns.Add(cmbKolonne);
            dgvModeller.Columns[kolonneNr].HeaderText = "Land";
        } //leggTilLand

        private void leggTilTilbehor() {
            //navngi kolonnene
            dgvModeller.Columns[0].Name = "columnMnr";
            dgvModeller.Columns[1].Name = "columnNavn";
            dgvModeller.Columns[2].Name = "columnType";
            dgvModeller.Columns[3].Name = "columnPris";
            dgvModeller.Columns[4].Name = "columnProdusent";
            //sett headers tekst
            dgvModeller.Columns[0].HeaderText = "Modellnr";
            dgvModeller.Columns[1].HeaderText = "Navn";
            dgvModeller.Columns[2].HeaderText = "Type";
            dgvModeller.Columns[3].HeaderText = "Pris";
            //av uviss grunn legges en kolonne ekstra kolonne til, så den må fjernes
            dgvModeller.Columns.RemoveAt(4);
            //opprett en kolonne som inneholder comboboxer
            DataGridViewComboBoxColumn cmbKolonne = new DataGridViewComboBoxColumn();
            //fyll combobox
            foreach (Produsent prod in Kontroll.kontrollInstans.returnProdusent()) {
                cmbKolonne.Items.Add(prod.getProdusent());
            } //foreach       
            dgvModeller.Columns.Add(cmbKolonne);
            dgvModeller.Columns[4].HeaderText = "Produsent";
        } //leggTilTilbehor

        private void leggTilOnsker() {
            //navngi kolonnene
            dgvModeller.Columns[0].Name = "columnID";
            dgvModeller.Columns[1].Name = "columnMnr";
            dgvModeller.Columns[2].Name = "columnNavn";
            dgvModeller.Columns[3].Name = "columnPris";
            dgvModeller.Columns[4].Name = "columnSalgssted";
            //sett headers tekst
            dgvModeller.Columns[0].HeaderText = "ID";
            dgvModeller.Columns[1].HeaderText = "Modellnr";
            dgvModeller.Columns[2].HeaderText = "Navn";
            dgvModeller.Columns[3].HeaderText = "Pris";
            //av uviss grunn legges en kolonne ekstra kolonne til, så den må fjernes
            dgvModeller.Columns.RemoveAt(4);
            //opprett en kolonne som inneholder comboboxer
            DataGridViewComboBoxColumn cmbKolonne = new DataGridViewComboBoxColumn();
            //fyll combobox
            foreach (Salgssted sted in Kontroll.kontrollInstans.returnSalgssted()) {
                cmbKolonne.Items.Add(sted.getSalgssted());
            } //foreach       
            dgvModeller.Columns.Add(cmbKolonne);
            dgvModeller.Columns[4].HeaderText = "Salgssted";
        } //leggTilOnsker

        #endregion

        #region FYLL DATAGGRIDVIEW

        private void fyllDataGridViewLokomotiv() {
            //hent antall lokomotiv registrert
            int antRader = Kontroll.kontrollInstans.countLokomotiv();
            //finnes det lokomotiv registrert?
            if (antRader > 0) {
                //opprett rader
                dgvModeller.Rows.Add(antRader);
                //hent listen som inneholder lokomotivene
                List<Tog> togListe = Kontroll.kontrollInstans.returnTogListe();
                //hent array som inneholder land
                Land[] landArray = Kontroll.kontrollInstans.returnLand();
                //loop gjennom alle lokomotivene
                for (int i = 0; i < togListe.Count; i++) {
                    dgvModeller.Rows[i].Cells[0].Value = togListe[i].getModellNr();
                    dgvModeller.Rows[i].Cells[1].Value = togListe[i].getNavn();
                    dgvModeller.Rows[i].Cells[2].Value = togListe[i].getType();
                    dgvModeller.Rows[i].Cells[3].Value = togListe[i].getPris();
                    dgvModeller.Rows[i].Cells[4].Value = togListe[i].getAarsmodell();
                    dgvModeller.Rows[i].Cells[5].Value = togListe[i].getStrl();
                    //hent ut landets ID
                    int idLand = togListe[i].getLand();
                    //hent ut navnet på landet
                    string land = landArray[idLand].getLand();
                    //legg til landet i datagridview
                    dgvModeller.Rows[i].Cells[6].Value = land;
                } //for
            } //if (antRader > 0)
        } //fyllDataGridViewLokomotiv

        private void fyllDataGridViewVogner() {
            //hent antall vogner registrert
            int antRader = Kontroll.kontrollInstans.countVogner();
            //finnes det vogner registrert?
            if (antRader > 0) {
                //opprett rader
                dgvModeller.Rows.Add(antRader);
                //hent listen som inneholder vognene
                List<Vogn> vognListe = Kontroll.kontrollInstans.returnVognListe();
                //hent array som inneholder land
                Land[] landArray = Kontroll.kontrollInstans.returnLand();
                //loop gjennom alle vognene
                for (int i = 0; i < vognListe.Count; i++) {
                    dgvModeller.Rows[i].Cells[0].Value = vognListe[i].getModellNr();
                    dgvModeller.Rows[i].Cells[1].Value = vognListe[i].getNavn();
                    dgvModeller.Rows[i].Cells[2].Value = vognListe[i].getType();
                    dgvModeller.Rows[i].Cells[3].Value = vognListe[i].getPris();
                    dgvModeller.Rows[i].Cells[4].Value = vognListe[i].getEpoke();
                    dgvModeller.Rows[i].Cells[5].Value = vognListe[i].getAntall();
                    dgvModeller.Rows[i].Cells[6].Value = vognListe[i].getStrl();
                    //hent ut landets ID
                    int idLand = vognListe[i].getLand();
                    //hent ut navnet på landet
                    string land = landArray[idLand].getLand();
                    //legg til landet i datagridview
                    dgvModeller.Rows[i].Cells[7].Value = land;
                } //for
            } //if (antRader > 0)
        } //fyllDataGridViewVogner

        private void fyllDataGridViewTilbehor() {
            //hent antall tilbehør registrert
            int antRader = Kontroll.kontrollInstans.countTilbehor();
            //finnes det tilbehør registrert?
            if (antRader > 0) {
                //opprett rader
                dgvModeller.Rows.Add(antRader);
                //hent listen som inneholder tilbehøret
                List<Tilbehor> tilbehorListe = Kontroll.kontrollInstans.returnTilbehorListe();
                //hent array som inneholder produsenter
                Produsent[] prod = Kontroll.kontrollInstans.returnProdusent();
                //loop gjennom alt tilbehøret
                for (int i = 0; i < tilbehorListe.Count; i++) {
                    dgvModeller.Rows[i].Cells[0].Value = tilbehorListe[i].getModellNr();
                    dgvModeller.Rows[i].Cells[1].Value = tilbehorListe[i].getNavn();
                    dgvModeller.Rows[i].Cells[2].Value = tilbehorListe[i].getType();
                    dgvModeller.Rows[i].Cells[3].Value = tilbehorListe[i].getPris();
                    //hent ut produsentens ID
                    int idProd = tilbehorListe[i].getProdusent();
                    //hent ut navn på produsenten
                    string produsent = prod[idProd].getProdusent();
                    //legg til produsenten i datagridview
                    dgvModeller.Rows[i].Cells[4].Value = produsent;
                } //for
            } //if (antRader > 0)
        } //fyllDataGridViewTilbehor

        private void fyllDataGridViewOnsker() {
            //hent antall ønsker registrert
            int antRader = Kontroll.kontrollInstans.countOnsker();
            //finnes det ønsker registrert?
            if (antRader > 0) {
                //opprett rader
                dgvModeller.Rows.Add(antRader);
                //hent listen som inneholder ønskene
                List<Onskeliste> onskeListe = Kontroll.kontrollInstans.returnOnskeListe();
                //hent array som inneholder salgsstedene
                Salgssted[] sted = Kontroll.kontrollInstans.returnSalgssted();
                //loop gjennom alle ønskene
                for (int i = 0; i < onskeListe.Count; i++) {
                    dgvModeller.Rows[i].Cells[0].Value = onskeListe[i].getIdOnske();
                    dgvModeller.Rows[i].Cells[1].Value = onskeListe[i].getMnr();
                    dgvModeller.Rows[i].Cells[2].Value = onskeListe[i].getNavn();
                    dgvModeller.Rows[i].Cells[3].Value = onskeListe[i].getPris();
                    //hent ut salgsstedets ID
                    int idSted = onskeListe[i].getIdSted();
                    //hent ut navnet på salgsstedet
                    string salgssted = sted[idSted].getSalgssted();
                    //legg til salgssted i datagridview
                    dgvModeller.Rows[i].Cells[4].Value = salgssted;
                } //for
            } //if (antRader > 0)
        } //fyllDataGridViewOnsker

        #endregion

        #region KNAPPER
        private void cmdLukk_Click(object sender, EventArgs e) {
            if (bwUpdate.WorkerSupportsCancellation) {
                //avbryt operasjoner for backgroundworker
                bwUpdate.CancelAsync();
            } //if (bwUpdate.WorkerSupportsCancellation)
            //lukk vinduet
            this.Close();
        } //cmdLukk_Click

        private void cmdSlett_Click(object sender, EventArgs e) {
            int rad = dgvModeller.CurrentCell.RowIndex;
            int ID = int.Parse(dgvModeller.Rows[rad].Cells[0].Value.ToString());
            switch (visning) {
                case frmMeny.TOG:
                    Kontroll.kontrollInstans.delTog(ID);
                    dgvModeller.Rows.Clear();
                    fyllDataGridViewLokomotiv();                    
                    break;
                case frmMeny.VOGN:
                    Kontroll.kontrollInstans.delVogn(ID);
                    dgvModeller.Rows.Clear();
                    fyllDataGridViewVogner();
                    break;
                case frmMeny.TILBEHOR:
                    Kontroll.kontrollInstans.delTilbehor(ID);
                    dgvModeller.Rows.Clear();
                    fyllDataGridViewTilbehor();
                    break;
                case frmMeny.ONSKELISTE:
                    Kontroll.kontrollInstans.delOnske(ID);
                    dgvModeller.Rows.Clear();
                    fyllDataGridViewOnsker();
                    break;
            } //switch
            //i tilfelle datagridview er tom, disable slette knapp
            disableCmdSlett();
        } //cmdSlett_Click

        private void cmdSkrivUt_Click(object sender, EventArgs e) {
            try {
                bool lagret = Kontroll.kontrollInstans.skrivOnskeTilFil();
                if (lagret) {
                    if (MessageBox.Show("Ønskeliste ble opprettet. \nVil du åpne tekstfilen?", "Lagret",
                            MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes) {
                        System.Diagnostics.Process.Start(frmMeny.TXT_ONSKELISTE);
                    } //if (MessageBox.Show(...) 
                } //if (lagret)
            } catch (Exception ex) {
                MessageBox.Show("En feil oppsto: \n" + ex.Message);
            } //try/catch
        } //cmdSkrivUt_Click

        private void cmdLagre_Click(object sender, EventArgs e) {
            //er backgroundworker ledig
            if (!bwUpdate.IsBusy) {
                status = "Status: Kontrollerer verdier...";
                setStatus(status);
                bwUpdate.RunWorkerAsync();
            } //if (!bwUpdate.IsBusy)
        } //cmdLagre_Click
        #endregion

        #region UPDATE

        private void oppdaterTog() {
            try {
                //hent antallet rader i datagridview
                int antRader = dgvModeller.RowCount;
                int i = 0;
                double pris = 0;
                double strl = 0;
                //loop gjennom alle radene i datagridview
                while (i < antRader) {
                    //hent ut verdiene fra datagridview
                    int mNr = (int)dgvModeller.Rows[i].Cells[0].Value;
                    string navn = dgvModeller.Rows[i].Cells[1].Value.ToString();
                    string type = dgvModeller.Rows[i].Cells[2].Value.ToString();
                    string prisVerdi = dgvModeller.Rows[i].Cells[3].Value.ToString();
                    string aar = dgvModeller.Rows[i].Cells[4].Value.ToString();
                    string strlVerdi = dgvModeller.Rows[i].Cells[5].Value.ToString();
                    string land = dgvModeller.Rows[i].Cells[6].Value.ToString();
                    //kan pris konverteres til double?
                    if (!double.TryParse(prisVerdi, out pris)) {
                        throw
                            new Exception("Pris skal kun bestå av siffer (0-9) med komma som skilletegn.\n"
                                + "Cellen som feilen ligger i er nr. " + (i + 1).ToString() + ".");
                    } //if (!double.TryParse(prisVerdi, out pris))
                    //er riktig årsformat for årsmodell oppgitt?
                    if (!Regex.IsMatch(aar, regUttrykk_Aar)) {
                        MessageBox.Show(aar + " " + regUttrykk_Aar);
                        throw
                            new Exception("Årsmodell skal kun bestå av fire siffer (0-9).\n"
                                + "Cellen som feilen ligger i er nr. " + (i + 1).ToString() + ".");
                    } //if (!Regex.IsMatch(aar, regUttrykk_Aar))
                    //kan størrelse konverteres til double?
                    if (!double.TryParse(strlVerdi, out strl)) {
                        throw
                            new Exception("Størrelse skal kun bestå av siffer (0-9) med komma som skilletegn.\n"
                                + "Cellen som feilen ligger i er nr. " + (i + 1).ToString() + ".");
                    } //if (!double.TryParse(strlVerdi, out strl))
                    //konverter år til heltall
                    int aarsmodell = int.Parse(aar);
                    //oppdater verdiene
                    Kontroll.kontrollInstans.updateLokomotiv(mNr, navn, type, pris, aarsmodell, strl, land);
                    //øk teller
                    i++;
                } //while
                //lagre endringer og ta snapshot
                Kontroll.kontrollInstans.lagreAlt();
                //sett at oppdateringen ble lagret 
                lagret = true;
            } catch (NullReferenceException) {
                //sett at endringer ikke ble lagret
                lagret = false;
                MessageBox.Show("Et eller flere felter er tomme. Disse feltene må fylles ut før lagring. ", "Tomme felt");
            } catch (FormatException) {
                //sett at endringer ikke ble lagret
                lagret = false;
                //gi feilmelding
                MessageBox.Show("Et eller flere felter inneholder feil. " 
                    + "\nHusk at årstall kun er fire siffer, og at skilletegn for pris og størrelse er komma.", "Feil format");
            } catch (Exception ex) {
                lagret = false;
                MessageBox.Show(ex.Message, "Feilmelding");
            } //try/catch
        } //oppdaterTog

        private void oppdaterVogn() {
            try {
                //hent antallet rader i datagridview
                int antRader = dgvModeller.RowCount;
                int i = 0;
                double pris = 0;
                int antall = 0;
                double strl = 0;
                //loop gjennom alle radene i datagridview
                while (i < antRader) {
                    //hent ut verdiene fra datagridview
                    int mNr = (int)dgvModeller.Rows[i].Cells[0].Value;
                    string navn = dgvModeller.Rows[i].Cells[1].Value.ToString();
                    string type = dgvModeller.Rows[i].Cells[2].Value.ToString();
                    string prisVerdi = dgvModeller.Rows[i].Cells[3].Value.ToString();
                    string epoke = dgvModeller.Rows[i].Cells[4].Value.ToString();
                    string antallVerdi = dgvModeller.Rows[i].Cells[5].Value.ToString();
                    string strlVerdi = dgvModeller.Rows[i].Cells[6].Value.ToString();
                    string land = dgvModeller.Rows[i].Cells[7].Value.ToString();
                    //kan pris konverteres til double?
                    if (!double.TryParse(prisVerdi, out pris)) {
                        throw
                            new Exception("Pris skal kun bestå av siffer (0-9) med komma som skilletegn.\n"
                                + "Cellen som feilen ligger i er nr. " + (i + 1).ToString());
                    } //if (!double.TryParse(prisVerdi, out pris))
                    //er riktig årsformat for årsmodell oppgitt?
                    if (!Regex.IsMatch(epoke, regUttrykk_Epoke)) {
                        throw
                            new Exception("Årsmodell skal kun bestå av fire siffer (0-9).\n"
                                + "Cellen som feilen ligger i er nr. " + (i + 1).ToString() + ".");
                    } //if (!Regex.IsMatch(aar, regUttrykk_Aar))
                    //kan antall konverteres til int?
                    if (!int.TryParse(antallVerdi, out antall)) {
                        throw
                            new Exception("Antall skal kun bestå av siffer (0-9).\n"
                                + "Cellen som feilen ligger i er nr. " + (i + 1).ToString() + ".");
                    } //if (!double.TryParse(strlVerdi, out strl))
                    //konverter år til heltall
                    //kan størrelse konverteres til double?
                    if (!double.TryParse(strlVerdi, out strl)) {
                        throw
                            new Exception("Størrelse skal kun bestå av siffer (0-9) med komma som skilletegn.\n"
                                + "Cellen som feilen ligger i er nr. " + (i + 1).ToString() + ".");
                    } //if (!double.TryParse(strlVerdi, out strl))
                    //oppdater verdiene
                    Kontroll.kontrollInstans.updateVogner(mNr, navn, type, pris, epoke, strl, antall, land);
                    //øk teller
                    i++;
                } //while
                //lagre endringer og ta snapshot
                Kontroll.kontrollInstans.lagreAlt();
                //sett at oppdateringen ble lagret 
                lagret = true;
            } catch (NullReferenceException) {
                //sett at endringer ikke ble lagret
                lagret = false;
                MessageBox.Show("Et eller flere felter er tomme. Disse feltene må fylles ut før lagring. ", "Tomme felt");
            } catch (FormatException) {
                //sett at endringer ikke ble lagret
                lagret = false;
                //gi feilmelding
                MessageBox.Show("Et eller flere felter inneholder feil. "
                    + "\nHusk at årstall kun er fire siffer, og at skilletegn for pris og størrelse er komma.", "Feil format");
            } catch (Exception ex) {
                lagret = false;
                MessageBox.Show(ex.Message, "Feilmelding");
            } //try/catch
        } //oppdaterVogn

        private void oppdaterTilbehor() {
            try {
                //hent antallet rader i datagridview
                int antRader = dgvModeller.RowCount;
                int i = 0;
                double pris = 0;
                //loop gjennom alle radene i datagridview
                while (i < antRader) {
                    //hent ut verdiene fra datagridview
                    int mNr = (int)dgvModeller.Rows[i].Cells[0].Value;
                    string navn = dgvModeller.Rows[i].Cells[1].Value.ToString();
                    string type = dgvModeller.Rows[i].Cells[2].Value.ToString();
                    string prisVerdi = dgvModeller.Rows[i].Cells[3].Value.ToString();
                    string produsent = dgvModeller.Rows[i].Cells[4].Value.ToString();
                    //kan pris konverteres til double?
                    if (!double.TryParse(prisVerdi, out pris)) {
                        throw
                            new Exception("Pris skal kun bestå av siffer (0-9) med komma som skilletegn.\n"
                                + "Cellen som feilen ligger i er nr. " + (i + 1).ToString() + ".");
                    } //if (!double.TryParse(prisVerdi, out pris))
                    //oppdater verdiene
                    Kontroll.kontrollInstans.updateTilbehor(mNr, navn, type, pris, produsent);
                    //øk teller
                    i++;
                } //while
                //lagre endringer og ta snapshot
                Kontroll.kontrollInstans.lagreAlt();
                //sett at oppdateringen ble lagret 
                lagret = true;
            } catch (NullReferenceException) {
                //sett at endringer ikke ble lagret
                lagret = false;
                MessageBox.Show("Et eller flere felter er tomme. Disse feltene må fylles ut før lagring. ", "Tomme felt");
            } catch (FormatException) {
                //sett at endringer ikke ble lagret
                lagret = false;
                //gi feilmelding
                MessageBox.Show("Et eller flere felter inneholder feil. " 
                    + "\nHusk at årstall kun er fire siffer, og at skilletegn for pris og størrelse er komma.", "Feil format");
            } catch (Exception ex) {
                lagret = false;
                MessageBox.Show(ex.Message, "Feilmelding");
            } //try/catch
        } //oppdaterTilbehor

        private void oppdaterOnsker() {
            try {
                //hent antallet rader i datagridview
                int antRader = dgvModeller.RowCount;
                int i = 0;
                int mNr = 0;
                double pris = 0;
                //loop gjennom alle radene i datagridview
                while (i < antRader) {
                    //hent ut verdiene fra datagridview
                    int id = (int)dgvModeller.Rows[i].Cells[0].Value;
                    string modellNr = dgvModeller.Rows[i].Cells[1].Value.ToString();
                    string navn = dgvModeller.Rows[i].Cells[2].Value.ToString();
                    string prisVerdi = dgvModeller.Rows[i].Cells[3].Value.ToString();
                    string salgssted = dgvModeller.Rows[i].Cells[4].Value.ToString();
                    //består modellnr kun av tall?
                    if (!Regex.IsMatch(modellNr, regUttrykk)) {
                        throw
                            new Exception("Modellnr skal kun bestå av siffer (0-9).\n"
                                + "Cellen som feilen ligger i er nr. " + (i + 1).ToString() + ".");
                    } //if (!Regex.IsMatch(mNr, regUttrykk))
                    mNr = int.Parse(modellNr);
                    //kan pris konverteres til double?
                    if (!double.TryParse(prisVerdi, out pris)) {
                        throw
                            new Exception("Pris skal kun bestå av siffer (0-9) med komma som skilletegn.\n"
                                + "Cellen som feilen ligger i er nr. " + (i + 1).ToString() + ".");
                    } //if (!double.TryParse(prisVerdi, out pris))
                    //oppdater verdiene
                    Kontroll.kontrollInstans.updateOnsker(id, mNr, navn, pris, salgssted);
                    //øk teller
                    i++;
                } //while
                //lagre endringer og ta snapshot
                Kontroll.kontrollInstans.lagreAlt();
                //sett at oppdateringen ble lagret 
                lagret = true;
            } catch (NullReferenceException) {
                //sett at endringer ikke ble lagret
                lagret = false;
                MessageBox.Show("Et eller flere felter er tomme. Disse feltene må fylles ut før lagring. ", "Tomme felt");
            } catch (FormatException) {
                //sett at endringer ikke ble lagret
                lagret = false;
                //gi feilmelding
                MessageBox.Show("Et eller flere felter inneholder feil. "
                    + "\nHusk at årstall kun er fire siffer, og at skilletegn for pris og størrelse er komma.", "Feil format");
            } catch (Exception ex) {
                lagret = false;
                MessageBox.Show(ex.Message, "Feilmelding");
            } //try/catch
        } //oppdaterOnsker

        #endregion

        #region BACKGROUNDWORKER
        private void bwUpdate_DoWork(object sender, DoWorkEventArgs e) {
                status = "Status: Forsøker å oppdaterer verdier...";
                setStatus(status);
            switch (visning) {
                case frmMeny.TOG:
                    oppdaterTog();
                    break;
                case frmMeny.VOGN:
                    oppdaterVogn();
                    break;
                case frmMeny.TILBEHOR:
                    oppdaterTilbehor();
                    break;
                case frmMeny.ONSKELISTE:
                    oppdaterOnsker();
                    break;
            } //switch
        } //bwUpdate_DoWork

        private void bwUpdate_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            lblStatus.Text = getStatus();
        } //bwUpdate_ProgressChanged

        private void bwUpdate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            //ble oppdatering avbrutt?
            if (e.Cancelled) {
                //gi tilbakemelding til bruker
                lblStatus.Text = "Status: Oppdatering ble avbrutt!";
            } else if (e.Error != null) { //oppsto det en feil under oppdatering?
                //opprett en feilmelding som fjerner linjeskift
                string feilmelding = e.Error.Message.Replace("\n", "");
                //gi tilbakemelding til bruker
                lblStatus.Text = "Status: En feil oppsto - Feilen er: " + e.Error.Message;
            } else {
                if (lagret) {
                    //gi tilbakemelding til bruker
                    lblStatus.Text = "Status: Endringene ble lagret!";
                } else {
                    lblStatus.Text = "Status: Endringer kunne ikke lagres pga. feil.";
                } //if (lagret)
            } //if (e.Cancelled)
        } //bwUpdate_RunWorkerCompleted

        private string getStatus() {
            return status;
        } //getStatus

        private void setStatus(string status) {
            this.status = status;
        } //setStatus
        #endregion
    } //frmVisAlle
} //namespace