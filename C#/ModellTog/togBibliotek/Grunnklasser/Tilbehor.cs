using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace modellTog {
    /// <summary>
    /// Sub-klasse for oppretting av tilbehør. 
    /// Dette er alt fra skinner og perronger, til små figurer og strømforsyning.
    /// </summary>
    [Serializable]
    public sealed class Tilbehor : Spesifikasjon {
        //id på produsenten
        private int idProd; 

        /// <summary>
        /// Tilbehørets konstruktør
        /// Registrerer modellnr, navn, type, pris og id til produsent.
        /// </summary>
        /// <param name="mNr">Tilbehørets modellnr (int)</param>
        /// <param name="navn">Tilbehørets navn</param>
        /// <param name="type">Typebetegnelsen på tilbehør (eks: perrong, strømforsyning, etc)</param>
        /// <param name="pris">Tilbehørets pris</param>
        /// <param name="idProd">Produsentens ID</param>
        internal Tilbehor(int mNr, string navn, string type, double pris, int idProd)
            :base (mNr, navn, type, pris){                
                setIdProd(idProd);      
	        } //konstruktør

        /// <summary>
        /// Henter produsentens ID
        /// </summary>
        /// <returns></returns>
        public int getProdusent() {
            return idProd;
        } //getProdusent

        /// <summary>
        /// Setter produsentens ID
        /// </summary>
        /// <param name="idProd">Produsentens ID</param>
        public void setIdProd(int idProd) {
            this.idProd = idProd;
        } //setIdProd

        /// <summary>
        /// Skriver ut modellnr, navn, type og pris i den rekkefølgen
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            string tekst = "Modellnr: " + this.getModellNr() + "\nNavn: " + this.getNavn() + "\nType: " + this.getType() 
		        + "\nPris: " + this.getPris();
		        return tekst;
        } //ToString
    } //Tilbehor
} //namespace