using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server {
    public class ByEiendom : Eiendom { //arver fra Eiendom
        private string adresse;

        public string getAdresse() {
            return adresse;
        } //getAdresse

        internal void setAdresse(string adresse) {
            if (adresse.Equals("")) { //er adresse skrevet inn?
                //intet innskrevet, kast feil
                throw
                    new Exception("Adresse må skrives inn (feltet er tomt).");
            } //if (adresse.Equals(""))
            this.adresse = adresse;
        } //setAdresse

        //kilden til hjelp for arv: http://support.microsoft.com/kb/307205
        internal ByEiendom(int enr, Etype type, int verditakst, bool solgt, string adresse) : base(enr, type,verditakst, solgt) {
            setAdresse(adresse); //setter adresse
        } //Konstruktør

        public override string ToString() {
            string tekst = "";
            bool solgt = this.getSolgt(); //henter ut verdien for om eiendommen er solgt/ikke solgt
            tekst = "Byeiendom: " + this.getEnr() + " " + this.getType() + " " + this.getAdresse() + " kr. " + this.getVerditakst(); 
           
            if (solgt) { //er eiendommen solgt?
                tekst += " er solgt ";
            } else { //eiendommen er ikke solgt
                tekst += " er ikke solgt ";
            } //if (solgt)
            //siden getBud() returnerer en liste, må man her loope gjennom for å hente ut budene og omgjøre disse til string
            foreach (Bud bud in getBud()) { //for hvert objekt bud i listen getBud()
                tekst += bud.ToString()+ " "; //hent ut bud.Tostring() og legg denne til på slutten (i tillegg space hvis flere bud)
            } //foreach
            return tekst; //returner teksten
        }//ToString
    } //ByEiendom
} //namespace