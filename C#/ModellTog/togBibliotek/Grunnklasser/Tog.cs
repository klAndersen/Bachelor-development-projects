using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace modellTog {
    /// <summary>
    /// Sub-klasse for oppretting av Tog/Lokomotiv. 
    /// Med dette menes da kun de som "styrer/leder" toget, m.a.o fronten av toget.
    /// </summary>
    [Serializable]
    public sealed class Tog : Spesifikasjon {

        /// <summary>
        /// Lokomotivets konstruktør.
        /// Registrerer modellnr, navnet på toget/lokomotivet, typen, årsmodell,
        /// prisen på toget/lokomotivet, størrelsen på toget/lokomotivet og 
        /// ID på landet det kommer fra/tilhører
        /// </summary>
        /// <param name="mNr">Lokomotivets modellnr (int)</param>
        /// <param name="navn">Lokomotivets navn</param>
        /// <param name="type">Typebetegnelsen på lokomotivet</param>
        /// <param name="aar">Lokomotivets årsmodell</param>
        /// <param name="pris">Lokomotivets pris</param>
        /// <param name="strl">Lokomotivets størrelse</param>
        /// <param name="idLand">ID til landet lokomotivet kommer fra</param>
        internal Tog(int mNr, string navn, string type, int aar, double pris, double strl, int idLand) 
        : base(mNr, navn, type, aar, pris, strl, idLand) {
        } //konstruktør

        /// <summary>
        /// Skriver ut modellnr, navnet, typen, årsmodell, størrelse og pris i denne rekkefølgen
        /// </summary>
        /// <returns>string</returns>
        public override string ToString() {
            string tekst = "Modellnr: " + this.getModellNr() + "\nNavn: " + this.getNavn() + "\nType: " + this.getType()
            + "\nÅrsmodell: " + this.getAarsmodell() + "\nStørrelse: " + this.getStrl() + "\nPris: " + this.getPris();
            return tekst;
        } //Tostring
    } //Tog
} //namespace