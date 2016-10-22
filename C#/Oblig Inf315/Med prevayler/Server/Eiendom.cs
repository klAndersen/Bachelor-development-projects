using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server {
   public abstract class Eiendom {        
        private readonly int enr;
        private Etype type; //eiendomstypen
        private int verditakst; //må være større enn null
        private bool solgt; //er eiendommen solgt?
        private List<Bud> budliste = new List<Bud>();

        /*GET METODER*/
        public int getEnr() {
            return enr;
        } //getNr

        public Etype getType() {
            return type;
        } //getType

        public int getVerditakst() {
            return verditakst;
        } //getVerditakst

        public bool getSolgt() {
            return solgt;
        } //getSolgt

        public List<Bud> getBud() {
            return budliste;
        } //getBud

        public int getSnittBud() {
            int antBud = 0; //antall bud til gitt eiendom
            int sumBud = 0; //summen for bud til gitt eiendom
            int gjennomsnitt = 0; //gjennomsnitt for bud til gitt eiendom
            foreach (Bud bud in getBud()) {
                antBud++; //teller opp antall bud
                sumBud += bud.getBelop(); //legger til bud i sum
            } //foreach
            if (antBud > 0) { //finnes det bud?
                gjennomsnitt = sumBud / antBud; //regner ut gjennomsnitt
            } //if (antBud != 0)
            return gjennomsnitt; //returnerer gjennomsnittet
        } //getSnittBud

        /*SET METODER*/
        internal void setType(Etype type) {
            this.type = type;
        } //setType

        internal void setVerditakst(int verditakst) {
            if (verditakst == 0) { //er verditakst null?
                throw 
                    new Exception("Verditakst kan ikke være null (innskrevet verditakst: " 
                                    + verditakst.ToString() + ")");
            } //if (verditakst < 0)            
            this.verditakst = verditakst;
        } //setVerditakst

        internal void setSolgt(bool solgt) {
            this.solgt = solgt;
        } //setSolgt

        internal void addBud(int belop) {
            Bud bud = new Bud(belop); //oppretter nytt objekt av Bud
            budliste.Add(bud); //legger budet inn i budlisten
        } //addBud

        public Eiendom(int enr, Etype type, int verditakst, bool solgt) {
            this.enr = enr; //setter enr
            setType(type);
            setVerditakst(verditakst);
            setSolgt(solgt);        
        } //konstruktør
       
       //fant ut hvordan jeg skulle override abstract ToString her:
       //http://stackoverflow.com/questions/1332052/is-there-a-way-to-make-derived-classes-override-tostring
       public override abstract string ToString();
    } //Eiendom
} //namespace