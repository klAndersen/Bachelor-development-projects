using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace modellTog {
    /// <summary>
    /// Abstrakt klasse som inneholder spesifikasjon som gjelder for alle klassene.
    /// Unntak er land, produsent, salgssted og ønskeliste (se klassen).
    /// Alle felles metoder og funksjoner opprettes her, og har to egne
    /// konstruktører - se tilhørende kommentar.
    /// </summary>
    [Serializable]
    public abstract class Spesifikasjon {
        //Modelldetaljer
        private int modellNr; //nr til modellen	
        private string navn; //navnet på modellen
        private string type; //typen modell
        private int aarsModell; //når er det laget
        private string epoke; //vognens tidsepoke
        private double strl; //størrelse/lengde på modellen	
        private int antall; //antall av spesifikk modell    
        private int idLand; //id på landet det kommer fra
        //Salgsdetaljer
        private double pris; //prisen på modell	

        /// <summary>
        /// Konstruktør for lokomotiv(er)
        /// </summary>
        /// <param name="modellNr">Lokomotiv/Vognens modellnr</param>
        /// <param name="navn">Navn på modellen</param>
        /// <param name="type">Typebetegnelse</param>
        /// <param name="aarsModell">Årsmodell</param>
        /// <param name="pris">Modellens pris</param>
        /// <param name="strl">Modellens størrelse</param>
        /// <param name="idLand">Landets ID</param>
        public Spesifikasjon(int modellNr, string navn, string type, int aarsModell, 
                            double pris, double strl, int idLand) {
            setModellNr(modellNr);
            setNavn(navn);
            setType(type);
            setAarsmodell(aarsModell);
            setPris(pris);
            setStrl(strl);
            setIdLand(idLand);
        } //konstruktør

        /// <summary>
        /// Konstruktør for vogn(er)
        /// </summary>
        /// <param name="modellNr">Lokomotiv/Vognens modellnr</param>
        /// <param name="navn">Navn på modellen</param>
        /// <param name="type">Typebetegnelse</param>
        /// <param name="epoke">Vognens tidsepoke</param>
        /// <param name="pris">Modellens pris</param>
        /// <param name="strl">Modellens størrelse</param>
        /// <param name="idLand">Landets ID</param>
        public Spesifikasjon(int modellNr, string navn, string type, string epoke,
                            double pris, double strl, int idLand) {
            setModellNr(modellNr);
            setNavn(navn);
            setType(type);
            setEpoke(epoke);
            setPris(pris);
            setStrl(strl);
            setIdLand(idLand);
        } //konstruktør

        /// <summary>
        /// Konstruktør for tilbehør
        /// </summary>
        /// <param name="modellNr">Tilbehørets modellnr (int)</param>
        /// <param name="navn">Tilbehørets navn</param>
        /// <param name="type">Typebetegnelsen på tilbehør (eks: perrong, strømforsyning, etc)</param>
        /// <param name="pris">Tilbehørets pris</param>
        public Spesifikasjon(int modellNr, string navn, string type, double pris) {
            setModellNr(modellNr);
            setNavn(navn);
            setType(type);
            setPris(pris);    
        } //konstruktør

        #region GET METODER
        /// <summary>
        /// Henter modellens modellnr
        /// </summary>
        /// <returns>int</returns>
        public int getModellNr() {
            return modellNr;
        }

        /// <summary>
        /// Henter modellens navn
        /// </summary>
        /// <returns>string</returns>
        public string getNavn() {
            return navn;
        }

        /// <summary>
        /// Henter modellens type
        /// </summary>
        /// <returns>string</returns>
        public string getType() {
            return type;
        }

        /// <summary>
        /// Henter modellens årsmodell
        /// </summary>
        /// <returns>int</returns>
        public int getAarsmodell() {
            return aarsModell;
        }

        /// <summary>
        /// Henter vognens tidsepoke
        /// </summary>
        /// <returns>string</returns>
        public string getEpoke() {
            return epoke;
        }

        /// <summary>
        /// Henter modellens pris
        /// </summary>
        /// <returns>double</returns>
        public double getPris() {
            return pris;
        }

        /// <summary>
        /// Henter modellens størrelse
        /// </summary>
        /// <returns>double</returns>
        public double getStrl() {
            return strl;
        }

        /// <summary>
        /// Henter vognens antall
        /// </summary>
        /// <returns>int</returns>
        public int getAntall() {
            return antall;
        }

        /// <summary>
        /// Henter modellens land ID
        /// </summary>
        /// <returns></returns>
        public int getLand() {
            return idLand;
        }
        #endregion

        #region SET METODER
        /// <summary>
        /// Setter modellnr (bør ikke endres foruten ved oppdatering)
        /// </summary>
        /// <param name="modellNr">Modellnr</param>
        public void setModellNr(int modellNr) {
            this.modellNr = modellNr;
        }

        /// <summary>
        /// Setter modellens navn
        /// </summary>
        /// <param name="navn">Modellens navn</param>
        public void setNavn(string navn) {
            this.navn = navn;
        }

        /// <summary>
        /// Setter modellens typebetegnelse
        /// </summary>
        /// <param name="type">Modellens type</param>
        public void setType(string type) {
            this.type = type;
        }

        /// <summary>
        /// Setter modellens årsmodell
        /// </summary>
        /// <param name="aarsModell">Modellens årsmodell</param>
        public void setAarsmodell(int aarsModell) {
            this.aarsModell = aarsModell;
        }

        /// <summary>
        /// Setter modellens tidsepoke
        /// </summary>
        /// <param name="epoke">Modellens tidsepoke</param>
        public void setEpoke(string epoke) {
            this.epoke = epoke;
        }

        /// <summary>
        /// Setter modellens pris
        /// </summary>
        /// <param name="pris">Modellens pris</param>
        public void setPris(double pris) {
            this.pris = pris;
        }

        /// <summary>
        /// Setter modellens størrelse
        /// </summary>
        /// <param name="strl">Modellens størrelse</param>
        public void setStrl(double strl) {
            this.strl = strl;
        }

        /// <summary>
        /// Setter antall vogner
        /// </summary>
        /// <param name="antall">Antall vogner av gitt modell</param>
        public void setAntall(int antall) {
            this.antall = antall;
        }

        /// <summary>
        /// Setter ID til landet
        /// </summary>
        /// <param name="idLand">Landets ID</param>
        public void setIdLand(int idLand) {
            this.idLand = idLand;
        }
        #endregion

        /// <summary>
        /// Overrides ToSTring() for subklasser
        /// </summary>
        /// <returns>string</returns>
        public override abstract string ToString();
    } //Spesifikasjon
} //namespace