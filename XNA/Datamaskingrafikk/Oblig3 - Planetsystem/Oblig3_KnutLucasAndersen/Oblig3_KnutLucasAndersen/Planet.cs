using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oblig3_KnutLucasAndersen {

    /// <summary>
    /// internal pga klassen skal kun brukes innenfor planetsystemet
    /// Denne klassen brukes også for måne
    /// </summary>
    internal class Planet {
        //inkluderte navn med tanke på at om jeg får det til 
        //så hadde det vært "kult" å kunne bruke mousehover over 
        //planeten for å se navnet, i tillegg brukes det som forelder 
        //(er klar over at integer som ID kunne vært en bedre ide)
        private string navn;
        private float strl;
        private int antMaaner;
        private float x, y, z;
        private float fart;
        private string forelder;
        private bool retning;

        #region KONSTRUKTØR
        /// <summary>
        /// Konstruktør for Månene
        /// </summary>
        /// <param name="strl">Månens størrelse</param>
        /// <param name="x">Månens x - posisjon</param>
        /// <param name="y">Månens y - posisjon</param>
        /// <param name="z">Månens z - posisjon</param>
        /// <param name="fart">Månens fart/hastighet</param>
        /// <param name="retning">Månens retning/sirkulasjon</param>
        /// <param name="forelder">Månens forelder (planeten den tilhører)</param>
        internal Planet(float strl, float x, float y, float z, float fart, bool retning, string forelder) {
            PlanetStrl = strl;
            PosisjonX = x;
            PosisjonY = y;
            PosisjonZ = z;
            PlanetFart = fart;
            PlanetRetning = retning;
            ForelderPlanet = forelder;
        } //konstruktør måne

        /// <summary>
        /// Konstruktør til Planetene
        /// </summary>
        /// <param name="navn">Planetens navn</param>
        /// <param name="strl">Planetens størrelse</param>
        /// <param name="x">Planetens x - posisjon</param>
        /// <param name="y">Planetens y - posisjon</param>
        /// <param name="z">Planetens z - posisjon</param>
        /// <param name="fart">Planetens fart/hastighet</param>
        /// <param name="retning">Planetens retning/sirkulasjon</param>
        internal Planet(string navn, float strl, int antMaaner, float x, float y, float z, float fart, bool retning) {
            PlanetNavn = navn;
            PlanetStrl = strl;
            PlanetAntMaaner = antMaaner;
            PosisjonX = x;
            PosisjonY = y;
            PosisjonZ = z;
            PlanetFart = fart;
            PlanetRetning = retning;
        } //konstruktør planet

        #endregion

        #region GET & SET
        /// <summary>
        /// Henter/setter navnet på planeten
        /// </summary>
        internal string PlanetNavn {
            get {
                return navn;
            }
            set {
                this.navn = value;
            }
        } //PlanetNavn

        /// <summary>
        /// Henter/setter størrelsen på planeten
        /// </summary>
        internal float PlanetStrl {
            get {
                return strl;
            }
            set {
                this.strl = value;
            }
        } //PlanetStrl

        /// <summary>
        /// Henter/setter antallet måner som planeten har
        /// </summary>
        internal int PlanetAntMaaner {
            get {
                return antMaaner;
            }
            set {
                this.antMaaner = value;
            }
        } //PlanetAntMaaner

        /// <summary>
        /// Henter/setter planetens x - posisjon
        /// </summary>
        internal float PosisjonX {
            get {
                return x;
            }
            set {
                this.x = value;
            }
        } //PosisjonX

        /// <summary>
        /// Henter/setter planetens y - posisjon
        /// </summary>
        internal float PosisjonY {
            get {
                return y;
            }
            set {
                this.y = value;
            }
        } //PosisjonY

        /// <summary>
        /// Henter/setter planetens y - posisjon
        /// </summary>
        internal float PosisjonZ {
            get {
                return z;
            }
            set {
                this.z = value;
            }
        } //PosisjonZ

        /// <summary>
        /// Henter/setter planetens fart
        /// </summary>
        internal float PlanetFart {
            get {
                return fart;
            }
            set {
                this.fart = value;
            }
        } //PlanetFart

        /// <summary>
        /// Henter/setter planetens retning
        /// </summary>
        internal bool PlanetRetning {
            get {
                return retning;
            }
            set {
                this.retning = value;
            }
        } //PlanetRetning

        /// <summary>
        /// Henter/setter månens forelder 
        /// (planeten den tilhører)
        /// </summary>
        internal string ForelderPlanet {
            get {
                return forelder;
            }
            set {
                this.forelder = value;
            }
        } //ForelderPlanet
        #endregion
    } //Planet
} //namespace