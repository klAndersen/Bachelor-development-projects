using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BilDemoBibliotek {
    /// <summary>
    /// En struct som inneholder verdier for Flagg. Flagg er en modell 
    /// som legges ut på veien, som gir bruker poeng ved sammenstøt/kollisjon.
    /// Denne struct'n er KUN brukbar til flaggmodellen, da den er lagd med denne 
    /// hensikten.
    /// </summary>
    internal struct FlaggStruktur {
        private Model flagg;
        private bool tegnFlagg;
        private Matrix[] flaggMatrise;
        private Vector3 posisjon;

        #region GET & SET METODE
        /// <summary>
        /// Henter/setter modellen
        /// (Model)
        /// </summary>
        internal Model FlaggModell {
            get {
                return flagg;
            }
            set {
                flagg = value;
            }
        }
        /// <summary>
        /// Henter/setter posisjonen hvor flagget skal settes ut
        /// (Vector3)
        /// </summary>
        internal Vector3 Posisjon {
            get {
                return posisjon;
            }
            set {
                posisjon = value;
            }
        }
        /// <summary>
        ///Henter/setter  om flagg skal tegnes på skjerm
        ///(bool)
        /// </summary>
        internal bool TegnFlagg {
            get {
                return tegnFlagg;
            }
            set {
                tegnFlagg = value;
            }
        }
        /// <summary>
        /// Henter/setter matrisen som inneholder verdiene til modellen
        /// (Matrix[])
        /// </summary>
        internal Matrix[] FlaggMatrise {
            get {
                return flaggMatrise;
            }
            set {
                flaggMatrise = value;
            }
        }
        #endregion
    } //FlaggStruktur
} //namespace