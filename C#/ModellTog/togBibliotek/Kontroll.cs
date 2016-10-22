using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Bamboo.Prevalence;
using Bamboo.Prevalence.Attributes;

namespace modellTog {
    /// <summary>
    /// Kontroll klassen som fungerer som "kommunikasjon"
    /// mellom grensesnitt og biblioteksklassene
    /// </summary>
    [Serializable]
    public class Kontroll : MarshalByRefObject {
        #region VARIABLER
        //Lister for modeller
        private SortedList<int, Tog> togListe = new SortedList<int, Tog>();
        private SortedList<int, Vogn> vognListe = new SortedList<int, Vogn>();
        private SortedList<int, Tilbehor> tilbehorListe = new SortedList<int, Tilbehor>();
        private SortedList<int, Onskeliste> onskeListe = new SortedList<int, Onskeliste>();
        //Lister for egenspesifiserte verdier for de forskjellige
        private SortedList<int, Land> landListe = new SortedList<int, Land>();
        private SortedList<int, Produsent> produsentListe = new SortedList<int, Produsent>();
        private SortedList<int, Salgssted> salgsstedListe = new SortedList<int, Salgssted>();
        //variabler for Kontroll klassen
        /// <summary>
        /// ReadOnly variabel av klassen Kontroll som kaller på de forskjellige 
        /// public funksjonene, static slik at den kalles ved oppstart
        /// </summary>
        public static readonly Kontroll kontrollInstans = new Kontroll();
        private const string TXT_ONSKELISTE = "Ønskeliste.txt";
        private static PrevalenceEngine bambooEngine;
        #endregion

        static Kontroll() {
            //oppretter en string for mappe til prevayler snapshot
            string katalog = Environment.CurrentDirectory + "\\snapshot";
            //lager mappen
            Directory.CreateDirectory(katalog); 
            bambooEngine = Bamboo.Prevalence.PrevalenceActivator.CreateTransparentEngine(typeof(Kontroll), katalog);
            kontrollInstans = (Kontroll)bambooEngine.PrevalentSystem;
        } //Konstruktør/destruktør

        #region ADD METODER
        /// <summary>
        /// Registrerer et nytt lokomotiv i listen, 
        /// såfremt det ikke finnes en modell med oppgitt modellnr fra før
        /// </summary>
        /// <param name="mNr">Lokomotivets modellnr (int)</param>
        /// <param name="navn">Lokomotivets navn</param>
        /// <param name="type">Typebetegnelsen på lokomotivet</param>
        /// <param name="aar">Lokomotivets årsmodell</param>
        /// <param name="pris">Lokomotivets pris</param>
        /// <param name="strl">Lokomotivets størrelse</param>
        /// <param name="idLand">ID til landet lokomotivet kommer fra</param>
        public void addTog(int mNr, string navn, string type, int aar, double pris, double strl, int idLand) {
            //sjekk at mNr ikke allerede er registrert
            finnesModellNr(mNr); 
            Tog tog = new Tog(mNr, navn, type, aar, pris, strl, idLand);
            //legger toget i listen
            togListe.Add(tog.getModellNr(), tog); 
        } //addTog

        /// <summary>
        /// Registrerer en ny vogn i listen, 
        /// såfremt det ikke finnes en modell med oppgitt modellnr fra før
        /// </summary>
        /// <param name="mNr">Vognens modellnr (int)</param>
        /// <param name="navn">Vognens navn</param>
        /// <param name="type">Typebetegnelsen på vognen</param>
        /// <param name="epoke">Vognens epoke</param>
        /// <param name="pris">Vognens pris</param>
        /// <param name="strl">Vognens størrelse</param>
        /// <param name="antall">Antall vogner av gitt modell</param>
        /// <param name="idLand">ID til landet vognen kommer fra</param>
        public void addVogn(int mNr, string navn, string type, string epoke, double pris, double strl, int antall, int idLand) {
            //sjekk at mNr ikke allerede er registrert
            finnesModellNr(mNr);
            Vogn vogn = new Vogn(mNr, navn, type, epoke, pris, strl, antall, idLand);
            //legger vogn i listen
            vognListe.Add(vogn.getModellNr(), vogn); 
        } //addVogn

        /// <summary>
        /// Registrerer ett nytt tilbehør i listen, 
        /// såfremt det ikke finnes en modell med oppgitt modellnr fra før
        /// </summary>
        /// <param name="mNr">Tilbehørets modellnr (int)</param>
        /// <param name="navn">Tilbehørets navn</param>
        /// <param name="type">Typebetegnelsen på tilbehør (eks: perrong, strømforsyning, etc)</param>
        /// <param name="pris">Tilbehørets pris</param>
        /// <param name="idProd">Produsentens ID</param>
        public void addTilbehor(int mNr, string navn, string type, double pris, int idProd) {
            //sjekk at mNr ikke allerede er registrert
            finnesModellNr(mNr); 
            Tilbehor tilbehor = new Tilbehor(mNr, navn, type, pris, idProd);
            //legger tilbehøret i listen
            tilbehorListe.Add(tilbehor.getModellNr(), tilbehor); 
        } //addTilbehor

        /// <summary>
        /// Registrer et nytt ønske i listen, 
        /// såfremt det ikke finnes et ønske med samme modellnr fra før
        /// </summary>
        /// <param name="mNr">Ønsket modells modellnr</param>
        /// <param name="navn">Navn på ønsket modell</param>
        /// <param name="pris">Pris på ønsket modell</param>
        /// <param name="idSted">ID til salgssted</param>
        public void addOnskeliste(int mNr, string navn, double pris, int idSted) {
            //finnes ønske fra før?
            finnesOnske(mNr);
            int id = onskeListe.Count;
            Onskeliste onske = new Onskeliste(id, mNr, navn, pris, idSted);
            //legger ønsket i listen
            onskeListe.Add(onske.getIdOnske(), onske);
        } //addOnskeliste

        /// <summary>
        /// Registrer ett nytt land, 
        /// såfremt det ikke allerede er registrert
        /// </summary>
        /// <param name="land">Navn på landet</param>
        public void addLand(string land) {
            finnesLand(land);
            int idLand = countLand();
            Land l = new Land(idLand, land);
            //legger land i listen
            landListe.Add(l.getIdLand(), l); 
        } //addLand

        /// <summary>
        /// Registrerer en ny produsent, 
        /// såfremt den ikke allerede er registrert
        /// </summary>
        /// <param name="prod"></param>
        public void addProdusent(string prod) {
            finnesProdusent(prod);            
            int idProd = countProdusent();
            Produsent produsent = new Produsent(idProd, prod);
            //legger produsent i listen
            produsentListe.Add(produsent.getProdusentId(), produsent); 
        } //addProdusent

        /// <summary>
        /// Registrerer et nytt salgssted, 
        /// såfremt det ikke allerede er registrert
        /// </summary>
        /// <param name="sted"></param>
        public void addSalgssted(string sted) {
            finnesSalgssted(sted);
            int idSted = countSalgssted();
            Salgssted salgssted = new Salgssted(idSted, sted);
            //legger salgssted i listen
            salgsstedListe.Add(salgssted.getIdSted(), salgssted); 
        } //addSalgssted
        #endregion

        #region SJEKK AV EKSISTENS
        /// <summary>
        /// Sjekker om modellnr oppgitt er registrert, 
        /// og kaster Exception dersom modellnr finnes fra før
        /// </summary>
        /// <param name="mNr">Modellnr som skal sjekkes</param>
        public void finnesModellNr(int mNr) {
            //sjekker om modellnr allerede er registrert
            if (togListe.ContainsKey(mNr) || vognListe.ContainsKey(mNr) || tilbehorListe.ContainsKey(mNr)) { 
                //mNr finnes allerede, kast feil
                throw
                    new Exception("Modellnr innskrevet (" + mNr + ") finnes allerede. "
                                   + "Vennligst skriv inn et nytt modellnr.");
            } //if (togListe.ContainsKey(mNr) || vognListe.ContainsKey(mNr) || tilbehorListe.ContainsKey(mNr))
        } //finnesModellNr

        /// <summary>
        /// Sjekker om et ønske med oppgitt modellnr er registrert, 
        /// og kaster Exception dersom modellnr allerede er registrert på et ønske
        /// </summary>
        /// <param name="mNr">Modellnr som skal sjekkes</param>
        public void finnesOnske(int mNr) {
            List<Onskeliste> listOnske = findOnske(mNr);
            //sjekker om ønsket som skal registreres finnes...
            if (listOnske.Count > 0) { 
                //ønsket finnes allerede, kast feil
                throw
                    new Exception("Et ønske med modellnr (" + mNr + ") finnes allerede. "
                                   + "Vennligst skriv inn et nytt modellnr.");
            } //if (listOnske.Count > 0)
        } //finnesOnske

        /// <summary>
        /// Sjekker om land oppgitt er registrert, 
        /// og kaster Exception dersom land allerede er registrert
        /// </summary>
        /// <param name="land">Land som skal sjekkes</param>
        public void finnesLand(string land) {
            List<Land> listLand = findLand(land);
            //sjekker om landet som skal registreres finnes...
            if (listLand.Count > 0) { 
                //land finnes allerede, kast feil
                throw
                    new Exception("Land innskrevet (" + land + ") finnes allerede. "
                                   + "Vennligst skriv inn et nytt land.");
            } //if (listLand.Count > 0)
        } //finnesLand

        /// <summary>
        /// Sjekker om produsent oppgitt er registrert, 
        /// og kaster Exception dersom produsent allerede er registrert
        /// </summary>
        /// <param name="prod">Produsent som skal sjekkes</param>
        public void finnesProdusent(string prod) {
            List<Produsent> listProdusent = findProdusent(prod);
            //sjekker om produsent som skal registreres finnes...
            if (listProdusent.Count > 0) { 
                //produsent finnes allerede, kast feil
                throw
                    new Exception("Produsent innskrevet (" + prod + ") finnes allerede. "
                                   + "Vennligst skriv inn ny produsent.");
            } //if (listProdusent.Count > 0)
        } //finnesProdusent

        /// <summary>
        /// Sjekker om salgssted oppgitt er registrert, 
        /// og kaster Exception dersom salgssted allerede er registrert
        /// </summary>
        /// <param name="sted">Salgssted som skal sjekkes</param>
        public void finnesSalgssted(string sted) {
            List<Salgssted> listSalgssted = findSalgssted(sted);
            //sjekker om salgssted som skal registreres finnes...
            if (listSalgssted.Count > 0) { 
                //land finnes allerede, kast feil
                throw
                    new Exception("Salgsstedet innskrevet (" + sted + ") finnes allerede. "
                                   + "Vennligst skriv inn et nytt salgssted.");
            } //if (listSalgssted.Count > 0)
        } //finnesSalgssted
        #endregion

        #region FIND METODER
        /// <summary>
        /// Sjekker om vognen med oppgitt modellnr finnes, 
        /// og returnerer vognen hvis den finnes, hvis ikke
        /// returneres null
        /// </summary>
        /// <param name="mNr"></param>
        /// <returns>Vogn | null</returns>
        public Vogn findVogn(int mNr) {
            //hent ut fra indeks resultat etter søk på mNr
            int finnes = vognListe.IndexOfKey(mNr);
            //finnes vognen det søkes etter?
            if (finnes == -1) {
                //vognen finnes ikke, returner null
                return null; 
            } //if (finnes == -1)
            return vognListe[mNr];
        } //findVogn

        /// <summary>
        /// Returnerer en liste med ønsker som matcher oppgitt 
        /// modellnr
        /// </summary>
        /// <param name="mNr">Ønskets modellnr som skal finnes</param>
        /// <returns>List&lt;Onskeliste&gt;</returns>
        public List<Onskeliste> findOnske(int mNr) {
            return (from onske in onskeListe.Values
                    where onske.getMnr() == mNr
                    select onske).ToList();
        } //findOnske

        /// <summary>
        /// Returnerer navn på landet basert på oppgitt ID
        /// </summary>
        /// <param name="idland">Landets ID som skal finnes</param>
        /// <returns>string</returns>
        public string findLand(int idland) {
            return landListe[idland].getLand();
        } //findLand

        /// <summary>
        /// Returnerer en liste over land basert på oppgitt verdi
        /// </summary>
        /// <param name="land">Landet som skal finnes</param>
        /// <returns>List&lt;Land&gt;</returns>
        public List<Land> findLand(string land) {
            return (from l in landListe.Values
                    where l.getLand() == land
                    select l).ToList();
        } //findLand

        /// <summary>
        /// Returnerer navnet på produsenten basert på oppgitt ID
        /// </summary>
        /// <param name="idProd">Produsentens ID som skal finnes</param>
        /// <returns>string</returns>
        public string findProdusent(int idProd) {
            return produsentListe[idProd].getProdusent();
        } //findProdusent

        /// <summary>
        /// Returnerer navnet på salgsstedet basert på oppgitt ID
        /// </summary>
        /// <param name="idSted">Salgsstedets ID som skal finnes</param>
        /// <returns>string</returns>
        public string findSalgssted(int idSted) {
            return salgsstedListe[idSted].getSalgssted();
        } //findSalgssted

        /// <summary>
        /// Returnerer en liste over produsenter basert på oppgitt verdi
        /// </summary>
        /// <param name="prod">Produsenten som skal finnes</param>
        /// <returns>List&lt;Produsent&gt;</returns>
        public List<Produsent> findProdusent(string prod) {
            return (from p in produsentListe.Values
                    where p.getProdusent() == prod
                    select p).ToList();
        } //findProdusent

        /// <summary>
        /// Returnerer en liste over salgssteder basert på oppgitt verdi
        /// </summary>
        /// <param name="sted">Salgssted som skal finnes</param>
        /// <returns>List&lt;Salgssted&gt;</returns>
        public List<Salgssted> findSalgssted(string sted) {
            return (from s in salgsstedListe.Values
                    where s.getSalgssted() == sted
                    select s).ToList();
        } //findSalgssted
        #endregion

        #region COUNT - OPPTELLINGSMETODER
        /// <summary>
        /// Teller opp antallet lokomotiv og 
        /// returner verdien
        /// </summary>
        /// <returns>int</returns>
        public int countLokomotiv() {
            //returner antallet fra listen over tog
            return togListe.Count;
        } //countLokomotiv

        /// <summary>
        /// Teller opp antallet vogner og 
        /// returner verdien
        /// </summary>
        /// <returns>int</returns>
        public int countVogner() {
            //returner antallet fra listen over vogner
            return vognListe.Count;
        } //countVogner

        /// <summary>
        /// Teller opp antallet tilbehør og 
        /// returner verdien
        /// </summary>
        /// <returns>int</returns>
        public int countTilbehor() {
            //returner antallet fra listen over tilbehør
            return tilbehorListe.Count;
        } //countTilbehor

        /// <summary>
        /// Teller opp antallet ønsker og 
        /// returner verdien
        /// </summary>
        /// <returns>int</returns>
        public int countOnsker() {
            //henter ut antallet fra listen over ønsker
            int antall = onskeListe.Count;
            return antall;
        } //countOnsker

        /// <summary>
        /// Teller opp antallet land og 
        /// returner verdien
        /// </summary>
        /// <returns>int</returns>
        public int countLand() {
            //henter ut antallet fra listen over land
            int antall = landListe.Count; 
            return antall;
        } //countLand

        /// <summary>
        /// Teller opp antallet produsenter og 
        /// returner verdien
        /// </summary>
        /// <returns>int</returns>
        public int countProdusent() {
            //henter ut antallet fra listen over produsenter
            int antall = produsentListe.Count; 
            return antall;
        } //countProdusent

        /// <summary>
        /// Teller opp antallet salgssteder og 
        /// returner verdien
        /// </summary>
        /// <returns>int</returns>
        public int countSalgssted() {
            //henter ut antallet fra listen over salgssteder
            int antall = salgsstedListe.Count; 
            return antall;
        } //countSalgssted

        #endregion

        #region ARRAY'S FOR FYLLING AV COMBOBOX
        /// <summary>
        /// Returnerer en array fylt med land
        /// </summary>
        /// <returns>Land[]</returns>
        public Land[] returnLand() {
            return landListe.Values.ToArray();
        } //returnLand

        /// <summary>
        /// Returnerer en array fylt med produsentene
        /// </summary>
        /// <returns>Produsent[]</returns>
        public Produsent[] returnProdusent() {
            return produsentListe.Values.ToArray();
        } //returnProdusent

        /// <summary>
        /// Returnerer en array fylt med salgsstedene
        /// </summary>
        /// <returns>Salgssted[]</returns>
        public Salgssted[] returnSalgssted() {
            return salgsstedListe.Values.ToArray();
        } //returnSalgssted
        #endregion

        #region RETURNERING AV LISTER
        /// <summary>
        /// Returnerer en liste fylt med alle lokomotivene
        /// </summary>
        /// <returns>List&lt;Tog&gt;</returns>
        public List<Tog> returnTogListe() {
            return togListe.Values.ToList();
        } //returnTogListe

        /// <summary>
        /// Returnerer en liste fylt med alle vognene
        /// </summary>
        /// <returns>List&lt;Vogn&gt;</returns>
        public List<Vogn> returnVognListe() {
            return vognListe.Values.ToList();
        } //returnVognListe

        /// <summary>
        /// Returnerer en liste fylt med alt tilbehøret
        /// </summary>
        /// <returns>List&lt;Tilbehor&gt;</returns>
        public List<Tilbehor> returnTilbehorListe() {
            return tilbehorListe.Values.ToList();
        } //returnTilbehorListe

        /// <summary>
        /// Returnerer en liste fylt med alle ønskene
        /// </summary>
        /// <returns>List&lt;Onskeliste&gt;</returns>
        public List<Onskeliste> returnOnskeListe() {
            return onskeListe.Values.ToList();
        } //returnOnskeListe
        #endregion

        #region SLETTING
        /// <summary>
        /// Sletter toget med oppgitt ID
        /// </summary>
        /// <param name="id">ID til toget som skal slettes</param>
        public void delTog(int id) {
            //fjerner fra listen ønsket med gitt id
            togListe.Remove(id);
        } //delTog

        /// <summary>
        /// Sletter vognen med oppgitt ID
        /// </summary>
        /// <param name="id">ID til vognen som skal slettes</param>
        public void delVogn(int id) {
            //fjerner fra listen ønsket med gitt id
            vognListe.Remove(id);
        } //delVogn

        /// <summary>
        /// Sletter tilbehøret med oppgitt ID
        /// </summary>
        /// <param name="id">ID til tilbehøret som skal slettes</param>
        public void delTilbehor(int id) {
            //fjerner fra listen ønsket med gitt id
            tilbehorListe.Remove(id);
        } //delTilbehor

        /// <summary>
        /// Sletter ønsket med oppgitt ID
        /// </summary>
        /// <param name="id">ID til ønsket som skal slettes</param>
        public void delOnske(int id) {
            //fjerner fra listen ønsket med gitt id
            onskeListe.Remove(id); 
        } //delOnske

        /// <summary>
        /// Sletter land med oppgitt ID
        /// </summary>
        /// <param name="id">ID til land som skal slettes</param>
        public void delLand(int id) {
            bool kanSlettes = sjekkTogetsLand(id);
            //er landet i bruk?
            if (kanSlettes) {
                kanSlettes = sjekkVognensLand(id);
            } //if (!kanSlettes)
            //kan landet slettes?
            if (kanSlettes) {
                //fjerner fra listen land med gitt id
                landListe.Remove(id);
            } else { //er i bruk, gi feilmelding
                throw new Exception(
                    "Landet " + landListe[id].getLand() + " er i bruk, og kan ikke slettes!");
            } //if (kanSlettes) 
        } //delLand

        private bool sjekkTogetsLand(int id) {
            bool kanSlettes = true;
            int teller = 0;
            while (kanSlettes && teller < togListe.Values.Count) {
                if (togListe.Values[teller].getLand() == id) {
                    kanSlettes = false;
                } //if (togListe[teller].getLand() == id)
                //øk teller
                teller++;
            } //while
            return kanSlettes;
        } //sjekkTogetsLand

        private bool sjekkVognensLand(int id) {
            bool kanSlettes = true;
            int teller = 0;
            //ikke i bruk av lokomotiv, sjekk vognene
            while (kanSlettes && teller < vognListe.Values.Count) {
                if (vognListe.Values[teller].getLand() == id) {
                    kanSlettes = false;
                } //if (togListe[teller].getLand() == id)
                //øk teller
                teller++;
            } //while
            return kanSlettes;
        } //sjekkVognensLand

        /// <summary>
        /// Sletter produsent med oppgitt ID
        /// </summary>
        /// <param name="id">ID til produsent som skal slettes</param>
        public void delProdusent(int id) {
            bool kanSlettes = true;
            int teller = 0;
            //loop gjennom registrerte tilbehør for å se om produsent er i bruk
            while (kanSlettes && teller < tilbehorListe.Values.Count) {
                if (tilbehorListe.Values[teller].getProdusent() == id) {
                    kanSlettes = false;
                } //if (togListe[teller].getLand() == id)
                //øk teller
                teller++;
            } //while
            if (kanSlettes) {
                //fjerner fra listen produsent med gitt id
                produsentListe.Remove(id);
            } else {
                throw new Exception(
                    "Produsenten " + produsentListe.Values[id].getProdusent() + " er i bruk, og kan ikke slettes!");
            } //if (kanSlettes) 
        } //delProdusent

        /// <summary>
        /// Sletter salgssted med oppgitt ID
        /// </summary>
        /// <param name="id">ID til salgssted som skal slettes</param>
        public void delSalgssted(int id) {
            bool kanSlettes = true;
            int teller = 0;
            //loop gjennom registrerte ønsker for å se om salgssted kan slettes
            while (kanSlettes && teller < onskeListe.Values.Count) {
                if (onskeListe.Values[teller].getIdSted() == id) {
                    kanSlettes = false;
                } //if (togListe[teller].getLand() == id)
                //øk teller
                teller++;
            } //while
            if (kanSlettes) {
                //fjerner fra listen salgssted med gitt id
                salgsstedListe.Remove(id);
            } else {
                throw new Exception(
                    "Salgsstedet " + salgsstedListe.Values[id].getSalgssted() + " er i bruk, og kan ikke slettes!");
            } //if (kanSlettes)
        } //delSalgssted
        #endregion

        #region SØKEMETODER
        /// <summary>
        /// Søker gjennom alle listene etter modell(er) som matcher
        /// oppgitt modellnr. Kaster Exception dersom modellnr det ble
        /// søkt etter ikke finnes
        /// </summary>
        /// <param name="mNr">Modellnr som det skal søkes etter</param>
        /// <returns>Spesifikason | Exception</returns>
        public Spesifikasjon sokMnr(int mNr) {
            //finnes modellnr i tog?
            if (togListe.ContainsKey(mNr)) {
                Tog togObjekt = togListe[mNr];
                return togObjekt;
            } else if (vognListe.ContainsKey(mNr)) {
                //finnes modellnr i vogner?
                Vogn vognObjekt = vognListe[mNr];
                return vognObjekt;
            } else if (tilbehorListe.ContainsKey(mNr)) {
                //finnes modellnr i tilbehør?
                Tilbehor tilbehorObjekt = tilbehorListe[mNr];
                return tilbehorObjekt;
            } else { //modellnr finnes ikke
                throw new Exception("Modellnr som det ble søkt etter (" + mNr + "), finnes ikke.");
            } //if (togListe.ContainsKey(mNr))
        } //sokMnr

        /// <summary>
        /// Søker gjennom alle listene etter modell(er) som matcher
        /// oppgitt navn. Kaster Exception dersom navnet det ble søkt 
        /// etter ikke finnes
        /// </summary>
        /// <param name="navn">Modellens navn det skal søkes etter</param>
        /// <returns>Spesifikason | Exception</returns>
        public Spesifikasjon sokNavn(string navn) {
            //hent ut tog fra listen
            List<Tog> togObj = (from tog in togListe.Values
                          where tog.getNavn() == navn
                          select tog).ToList();
            //ble et tog med gitt navn funnet?
            if (togObj.Count > 0) {
                return togObj[0];
            } else { //navnet fantes ikke blant togene
                List<Vogn> vognObj = (from vogn in vognListe.Values
                                      where vogn.getNavn() == navn
                                      select vogn).ToList();
                //finnes navnet blant vognene`?
                if (vognObj.Count > 0) {
                    return vognObj[0];
                } else { //navnet fantes ikke blant vognene
                    List<Tilbehor> tilbehorObj = (from tilbehor in tilbehorListe.Values
                                          where tilbehor.getNavn() == navn
                                          select tilbehor).ToList();
                    //finnes navnet blant tilbehøret?
                    if (tilbehorObj.Count > 0) {
                        return tilbehorObj[0];
                    } //if (tilbehorObj.Count > 0)
                } //if (vognObj.Count > 0) 
            } //if (togObj.Count > 0)
            //navn søkt etter finnes ikke
            throw new Exception("Navnet det ble søkt etter (" + navn + "), finnes ikke.");
        } //sokNavn

        /// <summary>
        /// Søker gjennom alle listene etter modell(er) som matcher
        /// oppgitt type. Kaster Exception dersom typen søkt etter 
        /// ikke finnes
        /// </summary>
        /// <param name="type">Typen det skal søkes etter (string)</param>
        /// <returns>Spesifikason | Exception</returns>
        public Spesifikasjon sokType(string type) {
            //hent ut tog fra listen
            List<Tog> togObj = (from tog in togListe.Values
                                where tog.getType() == type
                                select tog).ToList();
            //ble et tog med gitt typen funnet?
            if (togObj.Count > 0) {
                return togObj[0];
            } else { //typen fantes ikke blant togene
                List<Vogn> vognObj = (from vogn in vognListe.Values
                                      where vogn.getType() == type
                                      select vogn).ToList();
                //finnes typen blant vognene`?
                if (vognObj.Count > 0) {
                    return vognObj[0];
                } else { //typen fantes ikke blant vognene
                    List<Tilbehor> tilbehorObj = (from tilbehor in tilbehorListe.Values
                                                  where tilbehor.getType() == type
                                                  select tilbehor).ToList();
                    //finnes typen blant tilbehøret?
                    if (tilbehorObj.Count > 0) {
                        return tilbehorObj[0];
                    } //if (tilbehorObj.Count > 0)
                } //if (vognObj.Count > 0) 
            } //if (togObj.Count > 0)
            //typen søkt etter finnes ikke
            throw new Exception("Typen det ble søkt etter (" + type + "), finnes ikke.");
        } //sokType

        #endregion

        #region UPDATE METODER
        /// <summary>
        /// Oppdater lokomotiv med gitt modellnr
        /// (alle verdier blir endret likegyldig om verdi er ny eller uforandret)
        /// </summary>
        /// <param name="mNr">Lokomotivets modellnr</param>
        /// <param name="navn"></param>
        /// <param name="type"></param>
        /// <param name="pris"></param>
        /// <param name="aarsmodell"></param>
        /// <param name="strl"></param>
        /// <param name="land"></param>
        public void updateLokomotiv(int mNr, string navn, string type, double pris,  int aarsmodell, double strl, string land) {
            //oppdaterer alle feltene for gjeldende lokomotiv
            togListe[mNr].setNavn(navn);
            togListe[mNr].setType(type);
            togListe[mNr].setPris(pris);
            togListe[mNr].setAarsmodell(aarsmodell);
            togListe[mNr].setStrl(strl);
            //hent listen over land
            List<Land> resultat = findLand(land);
            //hent ut det første landet og legg det inn (burde bare bli ett resultat)
            togListe[mNr].setIdLand(resultat[0].getIdLand());
        } //updateLokomotiv

        /// <summary>
        /// Oppdater vogn med gitt modellnr
        /// (alle verdier blir endret likegyldig om verdi er ny eller uforandret)
        /// </summary>
        /// <param name="mNr"></param>
        /// <param name="navn"></param>
        /// <param name="type"></param>
        /// <param name="pris"></param>
        /// <param name="epoke"></param>
        /// <param name="strl"></param>
        /// <param name="antall"></param>
        /// <param name="land"></param>
        public void updateVogner(int mNr, string navn, string type, double pris, string epoke, double strl, int antall, string land) {
            //oppdaterer alle feltene for gjeldende vogn
            vognListe[mNr].setNavn(navn);
            vognListe[mNr].setType(type);
            vognListe[mNr].setPris(pris);
            vognListe[mNr].setEpoke(epoke);
            vognListe[mNr].setStrl(strl);
            vognListe[mNr].setAntall(antall);
            //hent listen over land
            List<Land> resultat = findLand(land);
            //hent ut det første landet og legg det inn (burde bare bli ett resultat)
            vognListe[mNr].setIdLand(resultat[0].getIdLand());
        } //updateVogner

        /// <summary>
        /// Oppdater tilbehør med gitt modellnr
        /// (alle verdier blir endret likegyldig om verdi er ny eller uforandret)
        /// </summary>
        /// <param name="mNr"></param>
        /// <param name="navn"></param>
        /// <param name="type"></param>
        /// <param name="pris"></param>
        /// <param name="produsent"></param>
        public void updateTilbehor(int mNr, string navn, string type, double pris, string produsent) {
            //oppdaterer alle feltene for gjeldende tilbehør
            tilbehorListe[mNr].setNavn(navn);
            tilbehorListe[mNr].setType(type);
            tilbehorListe[mNr].setPris(pris);
            //hent listen over produsenter
            List<Produsent> resultat = findProdusent(produsent);
            //hent ut den første produsenten og legg det inn (burde bare bli ett resultat)
            tilbehorListe[mNr].setIdProd(resultat[0].getProdusentId());
        } //updateTilbehor

        /// <summary>
        /// Oppdaterer ønsket med gitt ID
        /// (alle verdier blir endret likegyldig om verdi er ny eller uforandret)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mNr"></param>
        /// <param name="navn"></param>
        /// <param name="pris"></param>
        /// <param name="salgssted"></param>
        public void updateOnsker(int id, int mNr, string navn, double pris, string salgssted) {
            //oppdaterer alle feltene for gjeldende ønske
            onskeListe[id].setMnr(mNr);
            onskeListe[id].setNavn(navn);
            onskeListe[id].setPris(pris);
            //hent listen over salgssteder
            List<Salgssted> resultat = findSalgssted(salgssted);
            //hent ut den første produsenten og legg det inn (burde bare bli ett resultat)
            onskeListe[id].setSalgssted(resultat[0].getIdSted());
        } //updateOnsker

        /// <summary>
        /// Oppdaterer antall vogner tilknyttet oppgitt modell
        /// </summary>
        /// <param name="mNr">Vognens modellnr</param>
        /// <param name="antall">Antall vogner som skal registreres</param>
        public void updateAntallVogner(int mNr, int antall) {
            //sjekk om vogn finnes
            Vogn finnes = findVogn(mNr);
            //finnes vogn?
            if (finnes == null) {
                throw
                    new Exception("Vogn med modellnr: " + mNr + " finnes ikke.");
            } //if (v == null)            
            vognListe[mNr].setAntall(antall);
        } //updateVogner

        /// <summary>
        /// Oppdaterer landet; gir landet nytt navn
        /// </summary>
        /// <param name="id">Landets ID</param>
        /// <param name="land">Nytt navn på landet</param>
        public void updateLand(int id, string land) {
            landListe[id].setLand(land);
        } //updateLand

        /// <summary>
        /// Oppdaterer produsenten; gir produsent nytt navn
        /// </summary>
        /// <param name="id">Produsentens ID</param>
        /// <param name="produsent">Nytt navn på produsenten</param>
        public void updateProdusent(int id, string produsent) {
            produsentListe[id].setProdusent(produsent);
        } //updateProdusent

        /// <summary>
        /// Oppdaterer salgsstedet; gir salgssted nytt navn
        /// </summary>
        /// <param name="id">Salgsstedets ID</param>
        /// <param name="salgssted">Nytt navn på salgsstedet</param>
        public void updateSalgssted(int id, string salgssted) {
            salgsstedListe[id].setSalgssted(salgssted);
        } //updateSalgssted

        #endregion

        /// <summary>
        /// Denne funksjonen lager et nytt snapshot.
        /// </summary>
        public void lagreAlt() {
            bambooEngine.TakeSnapshot();
        } //lagreAlt

        /// <summary>
        /// Skriver ut ønskelisten til en tekstfil (.txt)
        /// </summary>
        public bool skrivOnskeTilFil() {
            bool resultat = false;
            try {
                string innhold = "";
                string filSti = System.Environment.CurrentDirectory;
                filSti += @"\" + TXT_ONSKELISTE;
                //opprett en skriver for å skrive ønskeliste til fil
                StreamWriter skrivOnske = new StreamWriter(filSti);
                foreach (Onskeliste onske in onskeListe.Values) {
                    string sted = findSalgssted(onske.getIdSted());
                    innhold += onske.ToString() + sted + "\r\n";
                } //foreach
                //skriv innhold til fil
                skrivOnske.Write(innhold);
                //lukk skriveren
                skrivOnske.Close();
                resultat = true;
            } catch (Exception ex) {
                throw ex;
            } //try/catch
            return resultat;
        } //skrivOnskeTilFil

        /// <summary>
        /// Omgjør første tegn i gitt tekst til stor bokstav
        /// </summary>
        /// <param name="tekst">Teksten som skal få stor forbokstav</param>
        /// <returns>string</returns>
	    public string storForbokstav(string tekst) { 	    
            tekst = tekst.Substring(0, 1).ToUpper() + tekst.Substring(1).ToLower();         
		    return tekst;
	    } //storForbokstav
    } //Kontroll
} //namespace