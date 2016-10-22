using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace modellTog {
    /// <summary>
    /// Sub-klasse som oppretter ønskeliste, her blir ønsker registrert.
    /// Siden ønsker kan inneholde modeller som allerede er registrert, 
    /// har ønsker derfor en ID som identifikator.
    /// </summary>
    [Serializable]
    public class Onskeliste {        
        private int idOnske; //id til ønske
        private int mNr; //modellnr
        private string navn; //navn på modell
        private double pris; //pris på modell
        private int idSted; //id på hvor modellen selges        

        /// <summary>
        /// Konstruktør for oppretting av ønskeliste
        /// Her registreres id, navn på modellen, pris på modellen og id til salgssted
        /// hvor modellen selges
        /// </summary>
        /// <param name="id">Ønskets ID</param>
        /// <param name="mNr">Ønsket modells modellnr</param>
        /// <param name="navn">Navn på ønsket modell</param>
        /// <param name="pris">Pris på ønsket modell</param>
        /// <param name="idSted">ID til salgssted</param>
        internal Onskeliste(int id, int mNr,  string navn, double pris, int idSted) {
            setIdOnske(id);
            setMnr(mNr);
            setNavn(navn);
            setPris(pris);
            setSalgssted(idSted);
	    } //konstruktør

        #region GET METODER
        /// <summary>
        /// Henter ønskets ID
        /// </summary>
        /// <returns>int</returns>
        public int getIdOnske() {
            return idOnske;
        }

        /// <summary>
        /// Henter ønsket modells modellnr
        /// </summary>
        /// <returns>int</returns>
        public int getMnr() {
            return mNr;
        }

        /// <summary>
        /// Henter ønsket modells navn
        /// </summary>
        /// <returns>string</returns>
        public string getNavn() {
            return navn;
        }

        /// <summary>
        /// Henter ønsket modells pris
        /// </summary>
        /// <returns>double</returns>
        public double getPris() {
            return pris;
        }

        /// <summary>
        /// Henter ønsket modells salgssted
        /// </summary>
        /// <returns>int</returns>
        public int getIdSted() {
            return idSted;
        }
        #endregion

        #region SET METODER
        /// <summary>
        /// Setter ID til ønsket
        /// </summary>
        /// <param name="idOnske">ØnsketsID</param>
        public void setIdOnske(int idOnske) {
            this.idOnske = idOnske;
        }

        /// <summary>
        /// Setter ønsket modells modellnr
        /// </summary>
        /// <param name="mNr">Modellnr</param>
        public void setMnr(int mNr) {
            this.mNr = mNr;
        }

        /// <summary>
        /// Setter navn på ønsket modell
        /// </summary>
        /// <param name="navn">Ønskets navn</param>
        public void setNavn(string navn) {
            this.navn = navn;
        }

        /// <summary>
        /// Setter pris på ønsket modell
        /// </summary>
        /// <param name="pris">Ønskets pris</param>
        public void setPris(double pris) {
            this.pris = pris;
        }

        /// <summary>
        /// Setter salgsstedets ID
        /// </summary>
        /// <param name="idSted">ID til salgssted</param>
        public void setSalgssted(int idSted) {
            this.idSted = idSted;
        }
        #endregion

        /// <summary>
        /// Skriver ut modellnr, navnet, typen, årsmodell, størrelse og pris i denne rekkefølgen
        /// </summary>
        /// <returns>string</returns>
        public override string ToString() {
            string tekst = "Modellnr: " + this.getMnr() + "\t\tNavn: " + this.getNavn() + "\t\tPris: " + this.getPris() + "\t\tSalgssted: ";
            return tekst;
        } //Tostring
    } //Onskeliste
} //namespace