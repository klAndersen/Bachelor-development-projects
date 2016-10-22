using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace modellTog {
    /// <summary>
    /// Samme egenskaper som sub-klassen Tog. Her registreres vogner, samme 
    /// parametre som Tog-klassen, men i tillegg blir det her registrert
    /// antallet vogner av en gitt type.
    /// </summary>
    [Serializable]
    public sealed class Vogn : Spesifikasjon {     

        /// <summary>
        /// Registrerer modellnr, navnet på vognen, typen, årsmodellen,
        /// prisen på vognen, størrelsen på vognen, antall vogner av denne
        /// typen/modellen og id på landet det kommer fra/tilhører
        /// </summary>
        /// <param name="mNr">Vognens modellnr (int)</param>
        /// <param name="navn">Vognens navn</param>
        /// <param name="type">Typebetegnelsen på vognen</param>
        /// <param name="epoke">Vognens tidsepoke</param>
        /// <param name="pris">Vognens pris</param>
        /// <param name="strl">Vognens størrelse</param>
        /// <param name="antall">Antall vogner av gitt modell</param>
        /// <param name="idLand">ID til landet vognen kommer fra</param>
        internal Vogn(int mNr, string navn, string type, string epoke, double pris, double strl, int antall, int idLand)
            : base(mNr, navn, type, epoke, pris, strl, idLand) {
                setAntall(antall);
        } //konstruktør

        /// <summary>
        /// Skriver ut modellnr, navnet, typen, årsmodell, størrelse og pris i den rekkefølgen
        /// </summary>
        /// <returns>string</returns>
        public override string ToString() {
            string tekst = "Modellnr: " + this.getModellNr() + "\nNavn: " + this.getNavn() + "\nType: " + this.getType()
            + "\nÅrsmodell: " + this.getAarsmodell() + "\nStørrelse: " + this.getStrl() + "\nPris: " + this.getPris();
            return tekst;
        } //ToString
    } //Vogn
}//namespace
