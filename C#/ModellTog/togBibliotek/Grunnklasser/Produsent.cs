using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace modellTog {
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Produsent {
	    private int idProd;
        //hvem har lagd modellen
        private string produsent; 
	
        /// <summary>
        /// Konstruktør for produsent
        /// </summary>
        /// <param name="idProd">Produsentens ID</param>
        /// <param name="produsent">Navn på produsenten</param>
	    public Produsent(int idProd, string produsent) {
            setProdusentId(idProd);
            setProdusent(produsent);
	    } //konstruktør
	
        /// <summary>
        /// Henter produsentens ID
        /// </summary>
        /// <returns>int</returns>
	    public int getProdusentId() {
		    return idProd;
	    }
	
        /// <summary>
        /// Henter produsentens navn
        /// </summary>
        /// <returns>string</returns>
	    public string getProdusent() {
		    return produsent;
	    }

        /// <summary>
        /// Setter produsentens ID
        /// Bør ikke forandres foruten ved sletting
        /// </summary>
        /// <param name="idProd">Produsentens ID</param>
        public void setProdusentId(int idProd) {
            this.idProd = idProd;
        }

        /// <summary>
        /// Setter navnet på produsenten
        /// </summary>
        /// <param name="produsent">Produsentens navn</param>
        public void setProdusent(string produsent) {
            this.produsent = produsent;
        }
    } //Produsent
} //namespace