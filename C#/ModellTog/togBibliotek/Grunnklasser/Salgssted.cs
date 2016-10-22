using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace modellTog {
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Salgssted {
        private int idSted;
        //hvem selger modellen	
        private string salgssted; 

        /// <summary>
        /// Konstruktør for salgssted
        /// </summary>
        /// <param name="idSted"></param>
        /// <param name="salgssted"></param>
        public Salgssted(int idSted, string salgssted) {
            setIdSted(idSted);
            setSalgssted(salgssted);
        } //konstruktør

        /// <summary>
        /// Henter ID for salgsstedet
        /// </summary>
        /// <returns>int</returns>
        public int getIdSted() {
            return idSted;
        }

        /// <summary>
        /// Henter navnet på salgsstedet
        /// </summary>
        /// <returns>string</returns>
        public string getSalgssted() {
            return salgssted;
        }

        /// <summary>
        /// Setter ID til salgsstedet
        /// Bør ikke forandres foruten ved sletting
        /// </summary>
        /// <param name="idSted">Salgsstedets ID</param>
        public void setIdSted(int idSted) {
            this.idSted = idSted;
        }

        /// <summary>
        /// Setter navnet på salgsstedet
        /// </summary>
        /// <param name="salgssted">Navn på salgsstedet</param>
        public void setSalgssted(string salgssted) {
            this.salgssted = salgssted;
        }
    } //Salgssted
} //namespace