using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Db4objects.Db4o;

namespace Server {
    public class Kontroll {
        private const string dbNavn = "db4Objectbase.dbo"; //navn på databasen
        private Db4objects.Db4o.IObjectContainer eiendomDB; //objekt av databasen
        public static readonly Kontroll kontroll = new Kontroll();

        /*ADD METODER*/
        public void addByEiendom(int enr, Etype type, int verditakst, string adresse, bool solgt = false) {
            finnesEnr(enr); //sjekk at enr ikke allerede er registrert
            ByEiendom by = new ByEiendom(enr, type, verditakst, solgt, adresse);
            eiendomDB.Store(by); //lagrer eiendommen i db
        } //addByEiendom

        public void addLandEiendom(int enr, Etype type, int verditakst, string matrikkelnr, bool solgt = false) {
            finnesEnr(enr); //sjekk at enr ikke allerede er registrert
            LandEiendom land = new LandEiendom(enr, type, verditakst, solgt, matrikkelnr);
            eiendomDB.Store(land); //lagrer eiendommen i db
        } //addByEiendom

        public void finnesEnr(int enr) { //funksjon som sjekker etter om enr allerede finnes
            IEnumerable<Eiendom> funnet = (from Eiendom e in eiendomDB.Query<Eiendom>()
                                           where e.getEnr() == enr
                                           select e);
            if (funnet.Count<Eiendom>() > 0) { //sjekker om eiendomsliste har enr som nøkkel...
                //enr finnes allerede, kast feil
                throw
                    new Exception("Enr innskrevet (" + enr + ") finnes allerede."
                                   + "Vennligst skriv inn et nytt enr.");
            } //if (eiendomsliste.ContainsKey(enr))
        } //finnesEnr

        public void addBud(int enr, int belop) {
            Eiendom e = findEiendom(enr); //hent eiendommen (hvis den ikke finnes får bruker feilmelding)            
            if (e == null) { //finnes eiendom?
                throw
                    new Exception("Eiendommen med enr: " + enr + " finnes ikke.");
            } //if (e != null)
            e.addBud(belop); //legg til bud
            eiendomDB.Store(e.getBud()); //lagrer budet i databasen
        } //addBud

        public void delEiendom(int enr) {
            eiendomDB.Delete(findEiendom(enr));
        } //delEiendom

        public void updateEiendom(int enr, int verditakst, bool solgt) { //andre detaljer kan ikke endres        
            Eiendom e = findEiendom(enr); //hent eiendommen (hvis den ikke finnes får bruker feilmelding)
            e.setVerditakst(verditakst); //sett ny verditakst
            e.setSolgt(solgt); //sett solgt/ikke solgt
            eiendomDB.Store(e); //lagre i db
        } //updateEiendom

        public int countEiendom() {
            IEnumerable<Eiendom> funnet = (from Eiendom e in eiendomDB.Query<Eiendom>()
                                           select e); //hent ut antall eiendommer
            return funnet.Count<Eiendom>(); //returner antallet
        } //countEiendom

        /*OPPLISTINGSMETODER*/
        public List<Eiendom> getEiendomsliste() { //skal liste opp alle eiendommer
            return (eiendomDB.Query<Eiendom>()).ToList<Eiendom>();
        } //getEiendomsliste

        public Eiendom findEiendom(int enr) {
            //IEnumerable<Eiendom> funnet = eiendomDB.Query<Eiendom>(e => e.getEnr() == enr);
            IEnumerable<Eiendom> funnet = (from Eiendom e in eiendomDB.Query<Eiendom>()
                                           where e.getEnr() == enr
                                           select e);
            if (funnet.Count<Eiendom>() == 0) { //ble eiendommen funnet?
                return null; //eiendommen finnes ikke, returner null
            } //if (funnet.Count<Eiendom>() == 0)
            return funnet.ToList<Eiendom>()[0];
        } //findEiendom

        public List<Eiendom> findEiendom(Etype type) { //alle eiendommer av oppgitt type
            return (from Eiendom e in eiendomDB.Query<Eiendom>()
                    where e.getType() == type
                    select e).ToList();
        } //findEiendom

        public List<Eiendom> findEiendom(int fratakst, int tiltakst) { //alle eiendommer med verditakst i intervallet
            return (from Eiendom e in eiendomDB.Query<Eiendom>()
                    where e.getVerditakst() >= fratakst
                     && e.getVerditakst() <= tiltakst
                    select e).ToList();
        } //findEiendom

        public void lagreAlt() {
            //skal lage snapshot med Prevayler og er derfor ikke implementert her
            throw
                new NotImplementedException("Metoden lagreAlt() er ikke implementert.\n"
                                            + "Eiendommer og bud lagres når de blir registrert.");
        } //lagreAlt

        public void hentAlt() {
            //kaster feil siden denne ikke er lagd, og ikke kalles noen plass
            throw
                new NotImplementedException("Metoden hentAlt() er ikke implementert.");
        } //hentAlt

        private Kontroll() { //tom konstruktør
            eiendomDB = Db4oFactory.OpenFile(dbNavn); //åpner databasen
        } //Konstruktør

        ~Kontroll() {
            eiendomDB.Close(); //lukker databasen
        } //destruktør
    } //Kontroll
} //namespace

