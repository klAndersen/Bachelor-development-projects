using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server {
    public class LandEiendom : Eiendom { //arver fra Eiendom
        private string matrikkelnr; //kommunenr/gnr/bnr

        public string getMatrikkelnr() {
            return matrikkelnr;
        } //getMatrikkelnr

        internal void setMatrikkelnr(string matrikkelnr) {
            if (matrikkelnr.Equals("")) { //er matrikkelnr skrevet inn?
                //intet innskrevet, kast feil
                throw
                    new Exception("Matrikkelnr må skrives inn (feltet er tomt).");
            } //if (adresse.Equals(""))
            this.matrikkelnr = matrikkelnr;
        } //setMatrikkelnr

        //kilden til hjelp for arv: http://support.microsoft.com/kb/307205
        internal LandEiendom(int enr, Etype type, int verditakst, bool solgt, string matrikkelnr) 
                            : base(enr, type, verditakst, solgt) {
            setMatrikkelnr(matrikkelnr); //setter matrikkelnr
        } //konstruktør

        public override string ToString() {
            string tekst = "";
            bool solgt = this.getSolgt(); //henter ut verdien for om eiendommen er solgt/ikke solgt
            tekst = "Landeiendom: " + this.getEnr() + " " + this.getType() + " " + this.getMatrikkelnr() + " kr. " + this.getVerditakst();

            if (solgt) { //er eiendommen solgt?
                tekst += " er solgt ";
            } else { //eiendommen er ikke solgt
                tekst += " er ikke solgt ";
            } //if (solgt)
            //siden getBud() returnerer en liste, må man her loope gjennom for å hente ut budene og omgjøre disse til string
            foreach (Bud bud in getBud()) { //for hvert objekt bud i listen getBud()
                tekst += bud.ToString() + " "; //hent ut bud.Tostring() og legg denne til på slutten (i tillegg space hvis flere bud)
            } //foreach
            return tekst; //returner teksten  
        } //ToString
    } //LandEiendom
} //namespace