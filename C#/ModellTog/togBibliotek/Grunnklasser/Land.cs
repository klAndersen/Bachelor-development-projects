using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace modellTog {
    /// <summary>
    /// Klassen land som inneholder verdier for hvilket land modellen tilhører 
    /// (ikke hvor modellen er laget, men hvor modellen orginalt kommer fra).
    /// </summary>
    [Serializable]
    public class Land {
        //landets id
        private int idLand;
        //landet det er produsert/lagd
        private string land; 

        /// <summary>
        /// Konstruktøren for Land
        /// </summary>
        /// <param name="idLand">Landets ID</param>
        /// <param name="land">Landets navn</param>
        public Land(int idLand, string land) {
            setIdLand(idLand);
            setLand(land);
        } //konstruktør

        /// <summary>
        /// Henter landets ID
        /// </summary>
        /// <returns>int</returns>
        public int getIdLand() {
            return idLand;
        }

        /// <summary>
        /// Henter navnet på landet
        /// </summary>
        /// <returns>string</returns>
        public string getLand() {
            return land;
        }

        /// <summary>
        /// ID'n til landet (autoinkrement)
        /// Bør ikke forandres foruten ved sletting
        /// </summary>
        /// <param name="idLand">int</param>
        public void setIdLand(int idLand) {
            this.idLand = idLand;
        }

        /// <summary>
        /// Sett nytt navn på landet
        /// </summary>
        /// <param name="land">Navn på landet</param>
        public void setLand(string land) {
            this.land = land;
        }
    } //Land
} //namespace