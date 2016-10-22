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
    public partial class frmDetaljer : Form {
        private string[] detaljArray = {
                                           "Land",
                                           "Produsent",
                                           "Salgssted"
                                       };

        #region FORMLOAD
        public frmDetaljer() {
            InitializeComponent();
        } //konstruktør

        private void frmDetaljer_Load(object sender, EventArgs e) {
            //fargelegg knappene
            cmdLagre.BackColor = Color.PeachPuff;
            cmdLukk.BackColor = Color.PeachPuff;
            cmdSlett.BackColor = Color.PeachPuff;
            //fyll combobox
            for (int i = 0; i < detaljArray.Length; i++) {
                cmbDetaljer.Items.Add(detaljArray[i]);
            } //foreach
            //sett første element som valgt
            cmbDetaljer.SelectedIndex = 0;
            fyllDatagridView();
        } //frmDetaljer_Load
        #endregion

        #region KNAPPER
        private void cmdLukk_Click(object sender, EventArgs e) {
            this.Close();
        } //cmdLukk_Click

        private void cmdLagre_Click(object sender, EventArgs e) {
            //hent ut antall rader
            int antRader = dgvDetaljer.Rows.Count;
            //hent ut valgt detalj
            string detalj = cmbDetaljer.SelectedItem.ToString();
            switch (detalj) {
                case "Land":
                    oppdaterLand(antRader);
                    break;
                case "Produsent":
                    oppdaterProdusent(antRader);
                    break;
                case "Salgssted":
                    oppdaterSalgssted(antRader);
                    break;
            } //switch
        } //cmdLagre_Click

        private void cmdSlett_Click(object sender, EventArgs e) {
            //hent ut valgt detalj
            string detalj = cmbDetaljer.SelectedItem.ToString();
            //hent ut valgt rad
            int rad = dgvDetaljer.CurrentCell.RowIndex;
            //hent id for valgt rad
            int ID = int.Parse(dgvDetaljer.Rows[rad].Cells[0].Value.ToString());
            switch (detalj) {
                case "Land":
                    slettLand(ID);
                    break;
                case "Produsent":
                    slettProdusent(ID);
                    break;
                case "Salgssted":
                    slettSalgssted(ID);
                    break;
            } //switch
            dgvDetaljer.Rows.Clear();
            fyllDatagridView();
        } //cmdSlett_Click
        #endregion

        #region DATAGRIDVIEW
        private void fyllDatagridView() {
            //tøm datagridview for tidligere data
            dgvDetaljer.Rows.Clear();
            //hent valgt detalj
            string detalj = cmbDetaljer.SelectedItem.ToString();
            dgvDetaljer.ColumnCount = 2;
            //navngi kolonner
            dgvDetaljer.Columns[0].Name = "columnID";
            dgvDetaljer.Columns[1].Name = "columnNavn";
            dgvDetaljer.Columns[0].HeaderText = detalj + " ID";
            dgvDetaljer.Columns[1].HeaderText = "Navn på " + detalj;
            //lås første kolonne og sett at vidden er basert på kolonneheader
            dgvDetaljer.Columns[0].ReadOnly = true;
            dgvDetaljer.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
            try {
                //fyll datagridview med valgt detalj
                switch (detalj) {
                    case "Land":
                        hentLand();
                        break;
                    case "Produsent":
                        hentProdusenter();
                        break;
                    case "Salgssted":
                        hentSalgssteder();
                        break;
                } //switch
            } catch (Exception ex) {
                MessageBox.Show("En feil oppsto:\n" + ex.Message, "Feilmelding");
            } //try/catch
        } //fyllDatagridView

        private void hentLand() {
            //hent array som inneholder detaljens verdi
            Land[] landArray = Kontroll.kontrollInstans.returnLand();
            //finnes det land registrert?
            if (landArray.Length > 0) {
                //land kan slettes
                cmdSlett.Enabled = true;
                //sett antall rader som datagridview skal inneholde
                dgvDetaljer.Rows.Add(landArray.Length);
                for (int i = 0; i < landArray.Length; i++) {
                    dgvDetaljer.Rows[i].Cells[0].Value = landArray[i].getIdLand();
                    dgvDetaljer.Rows[i].Cells[1].Value = landArray[i].getLand();
                } //for
            } else { //ingen land registrert
                //disable slette knapp
                cmdSlett.Enabled = false;
            } //if (landArray.Length > 0)
        } //hentLand

        private void hentProdusenter() {
            //hent array som inneholder detaljens verdi
            Produsent[] prodArray = Kontroll.kontrollInstans.returnProdusent();
            //finnes det produsenter registrert?
            if (prodArray.Length > 0) {
                //produsenter kan slettes
                cmdSlett.Enabled = true;
                //sett antall rader som datagridview skal inneholde
                dgvDetaljer.Rows.Add(prodArray.Length);
                for (int i = 0; i < prodArray.Length; i++) {
                    dgvDetaljer.Rows[i].Cells[0].Value = prodArray[i].getProdusentId();
                    dgvDetaljer.Rows[i].Cells[1].Value = prodArray[i].getProdusent();
                } //for
            } else { //ingen produsenter registrert
                //disable slette knapp
                cmdSlett.Enabled = false;
            } //if (prodArray.Length > 0)
        } //hentProdusenter

        private void hentSalgssteder() {
            //hent array som inneholder detaljens verdi
            Salgssted[] stedArray = Kontroll.kontrollInstans.returnSalgssted();
            //finnes det salgssteder registrert?
            if (stedArray.Length > 0) {
                //salgssteder kan slettes
                cmdSlett.Enabled = true;
                //sett antall rader som datagridview skal inneholde
                dgvDetaljer.Rows.Add(stedArray.Length);
                for (int i = 0; i < stedArray.Length; i++) {
                    dgvDetaljer.Rows[i].Cells[0].Value = stedArray[i].getIdSted();
                    dgvDetaljer.Rows[i].Cells[1].Value = stedArray[i].getSalgssted();
                } //for
            } else { //ingen salgssteder registrert
                //disable slette knapp
                cmdSlett.Enabled = false;
            } //if (stedArray.Length > 0)
        } //hentSalgssteder
        #endregion

        private void cmbDetaljer_SelectedIndexChanged(object sender, EventArgs e) {
            fyllDatagridView();
        } //cmbDetaljer_SelectedIndexChanged

        #region UPDATE
        private void oppdaterLand(int antRader) {
            int i = 0;
            while (i < antRader) {
                //hent verdier
                string idLand = dgvDetaljer.Rows[i].Cells[0].Value.ToString();
                string land = dgvDetaljer.Rows[i].Cells[1].Value.ToString();
                //konverter ID til heltall
                int ID = int.Parse(idLand);
                Kontroll.kontrollInstans.updateLand(ID, land);
                i++;
            } //while
        } //oppdaterLand

        private void oppdaterProdusent(int antRader) {
            int i = 0;
            while (i < antRader) {
                //hent verdier
                string idProd = dgvDetaljer.Rows[i].Cells[0].Value.ToString();
                string prod = dgvDetaljer.Rows[i].Cells[1].Value.ToString();
                //konverter ID til heltall
                int ID = int.Parse(idProd);
                Kontroll.kontrollInstans.updateProdusent(ID, prod);
                i++;
            } //while
        } //oppdaterProdusent

        private void oppdaterSalgssted(int antRader) {
            int i = 0;
            while (i < antRader) {
                //hent verdier
                string idSted = dgvDetaljer.Rows[i].Cells[0].Value.ToString();
                string land = dgvDetaljer.Rows[i].Cells[1].Value.ToString();
                //konverter ID til heltall
                int ID = int.Parse(idSted);
                Kontroll.kontrollInstans.updateSalgssted(ID, land);
                i++;
            } //while
        } //oppdaterSalgssted
        #endregion

        #region SLETTING
        private void slettLand(int ID) {
            try {
                Kontroll.kontrollInstans.delLand(ID);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Feilmelding");
            } //try/catch
        } //slettLand

        private void slettProdusent(int ID) {
            try {
                Kontroll.kontrollInstans.delProdusent(ID);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Feilmelding");
            } //try/catch
        } //slettProdusent

        private void slettSalgssted(int ID) {
            try {
                Kontroll.kontrollInstans.delSalgssted(ID);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Feilmelding");
            } //try/catch
        } //slettSalgssted
        #endregion
    } //frmDetaljer
} //namespace