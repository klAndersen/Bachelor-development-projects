using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server {
    public class Kontroll {
        private SortedList<int, Eiendom> eiendomsliste = new SortedList<int, Eiendom>(); //ikke med db4o
        public static readonly Kontroll kontroll = new Kontroll();

        /*ADD METODER*/
        public void addByEiendom(int enr, Etype type, int verditakst, string adresse, bool solgt = false) {
            finnesEnr(enr); //sjekk at enr ikke allerede er registrert
            ByEiendom by = new ByEiendom(enr, type, verditakst, solgt, adresse);
            eiendomsliste.Add(by.getEnr(), by);
        } //addByEiendom

        public void addLandEiendom(int enr, Etype type, int verditakst, string matrikkelnr, bool solgt = false) {
            finnesEnr(enr); //sjekk at enr ikke allerede er registrert
            LandEiendom land = new LandEiendom(enr, type, verditakst, solgt, matrikkelnr);
            eiendomsliste.Add(land.getEnr(), land);
        } //addByEiendom

        public void finnesEnr(int enr) { //funksjon som sjekker etter om enr allerede finnes
            if (eiendomsliste.ContainsKey(enr)) { //sjekker om eiendomsliste har enr som nøkkel...
                //enr finnes allerede, kast feil
                throw
                    new Exception("Enr innskrevet (" + enr + ") finnes allerede."
                                   + "Vennligst skriv inn et nytt enr.");
            } //if (eiendomsliste.ContainsKey(enr))
        } //finnesEnr

        public void addBud(int enr, int belop) {   
            Eiendom e = findEiendom(enr); //sjekk om eiendom finnes
            if (e == null) { //finnes eiendom?
                throw
                    new Exception("Eiendommen med enr: " + enr + " finnes ikke.");
            } //if (e != null)
            eiendomsliste[enr].addBud(belop); //legg til bud på eiendom
        } //addBud

        public void delEiendom(int enr) {
            eiendomsliste.Remove(enr); //fjerner fra listen eiendom med gitt enr
        } //delEiendom

        public void updateEiendom(int enr, int verditakst, bool solgt) { //andre detaljer kan ikke endres            
            eiendomsliste[enr].setVerditakst(verditakst); //setter ny verditakst til eiendom på plass 'enr'
            eiendomsliste[enr].setSolgt(solgt);
        } //updateEiendom

        public int countEiendom() {
            int antall = eiendomsliste.Count; //henter ut antallet fra eiendomslisten
            return antall;
        } //countEiendom
        
        /*OPPLISTINGSMETODER*/
        public List<Eiendom> getEiendomsliste() { //skal liste opp alle eiendommer
            return eiendomsliste.Values.ToList(); //retunerer verdiene i eiendomsliste og gjør disse om til en liste
        } //getEiendomsliste

        public Eiendom findEiendom(int enr) {
            int finnes = eiendomsliste.IndexOfKey(enr); //hent ut fra indeks resultat etter søk på enr
            //referer her til: http://msdn.microsoft.com/en-us/library/system.collections.sortedlist.indexofkey.aspx
            if (finnes == -1) { //finnes eiendommen det søkes etter?
                return null; //eiendommen finnes ikke, returner null
            } //if (finnes == -1)
            return eiendomsliste[enr]; //returnerer eiendommen på gitt plass
        } //findEiendom

        public List<Eiendom> findEiendom(Etype type) { //alle eiendommer av oppgitt type
            return (from e in eiendomsliste.Values
                    where e.getType() == type
                    select e).ToList();
        } //findEiendom

        public List<Eiendom> findEiendom(int fratakst, int tiltakst) { //alle eiendommer med verditakst i intervallet
            return (from e in eiendomsliste.Values
                    where e.getVerditakst() >= fratakst
                     && e.getVerditakst() <= tiltakst                
                    select e).ToList();
        } //findEiendom

        public void lagreAlt() {
            //skal lage snapshot med Prevayler og er derfor ikke implementert her
            throw
                new NotImplementedException("Metoden lagreAlt() er ikke implementert, da dette programmet er uten persistens.\n"
                                            + "Dette programmet lagrer kun objekter i RAM.");
        } //lagreAlt

        public void hentAlt() {
            //kaster feil siden denne ikke er lagd, og ikke kalles noen plass
            throw
                new NotImplementedException("Metoden hentAlt() er ikke implementert.");
        } //hentAlt

        private Kontroll() { //tom konstruktør
        } //Konstruktør
    } //Kontroll
} //namespace