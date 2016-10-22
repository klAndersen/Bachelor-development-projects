package oblig_244;
/**
 * Denne klassen kontrollerer dataene som bruker har skrevet inn, 
 * og sendes da videre til lagring eller gir tilbakemelding/feilmelding 
 * dersom noe ikke stemmer/er feil innskrevet
 */
import static javax.swing.JOptionPane.*;
import java.sql.*;
import java.util.*;
import javax.swing.*;
import javax.swing.table.*;

public class Kontroll {
	private static Kontroll kontrollIinstans; //static slik at den kalles ved oppstart
	private static Database db; //objekt av klassen Database
	private static final int MAX_KOLONNER = 1000; //antall kolonner som kan vises/legges i JTable
	private static final int MAX_RADER = 4; //antall rader som kan vises/legges i JTable
	private static ArrayList<Hare> HareTabell; //ArrayList for Harer
	private static ArrayList <Gaupe> GaupeTabell; //ArrayList for Gauper

	private  Kontroll() {	} //tom konstrukt�r, satt som private s� ingen f�r tilgang til den		

	/**
	 * Fikk tipset av Knut W. Hansson om � bruke Singleton.
	 * Singleton s�rger for at kun et objekt av klassen det opprettes i er tilgjengelig.
	 * Kilde:
	 * http://www.javabeginner.com/learn-java/java-singleton-design-pattern
	 * Denne metoden bruker static fordi da kallens den direkte ved oppstart, og da er 
	 * objektets verdi null, og hver gang programmet kaller p� den, returneres det samme 
	 * objektet. Synchronized forhindrer at to klasser kaller p� metoden samtidig.
	 */
	public static synchronized Kontroll getKontrollIinstans(String bruker, String pwd) {
		if (kontrollIinstans == null) { //hva er kontrollinstans verdi?
			try {
				kontrollIinstans = new Kontroll(); //opprett et nytt objekt av kontrollinstans			
				db =  getDatabase(bruker,pwd); //fors�k � koble til databasen
				HareTabell = db.hentHare(); //fyll ArrayList med Hare(r)
				GaupeTabell = db.hentGaupe(); //fyll ArrayList med Gaupe(r)
				lagMessageDialog("Du er n� tilkoblet databasen, og kan starte registrering.","Tilkobling suksessfull",1);
			} catch (NullPointerException e) { //feil brukernavn/passord, s� finner ikke databasen
				lagMessageDialog("Grunnet tilkobling til database feilet, vil data ikke bli lagret.\nSjekk at brukernavn/passord er korrekt.",
						"Tilkobling",0);
				kontrollIinstans = null; //nullstiller instans, slik at nytt p�logginsfors�k er mulig
			} catch (Exception e) {
				lagMessageDialog("En feil oppsto, feilen var:\n " + e,"Feil",0);
				kontrollIinstans = null; //nullstiller instans, slik at nytt p�logginsfors�k er mulig
			} // try/catch
		} //if (kontrollIinstans == null)
		return kontrollIinstans;
	} //getKontrollIinstans

	/**
	 * Denne metoden forhindrer kloning/duplisering av objektet, 
	 * om det skjer, kastes da en CloneNotSupportedException
	 */
	public Object clone() throws CloneNotSupportedException {
		throw new CloneNotSupportedException();
	} //CloneNotSupportedException

	public static Database getDatabase(String bruker, String pwd) {
		try {			
			db = new Database(bruker, pwd);	//kobler til databasen
			return db; //returner resultatet av tilkoblingen
		} catch (Exception e) { //klarte ikke koble til databasen
			lagMessageDialog("F�r ikke forbindelse med databasen.","Tilkobling", 0);
		} // try/catch
		return null;
	} //getDatabase

	public void lukkTilkobling() {
		try {
			db.kobleNed(); //lukk tilkobling
		} catch (SQLException e) { //klarte ikke lukke forbindelsen til databasen
			lagMessageDialog("Klarte ikke � lukke forbindelse.","Lukking", 0);
		} // try/catch
	} //lukkTilkobling

	/***METODER FOR HARE***/
	public String lagHareId() { // metode for � lage en id for hare						 
		int j =  HareTabell.size(); //setter j's verdi til ArrayList strl
		j++; //�ker j
		String id = "H" + j; //lager id til dyret		
		return id; //returner id'n
	} //lagHareId

	public boolean lagreHare(char kjonn, double lengde, double vekt, String sted, String dato, char hType, String farge) 
	throws Exception {
		boolean sjekk = false;
		String id = lagHareId(); //lag ny id for haren
		try {
			//lager et nytt objekt av klassen Hare
			Hare regNyHare = new Hare(id, kjonn, lengde, vekt, sted, dato, hType, farge);
			HareTabell.add(regNyHare); //legger haren inn i ArrayList		
			sjekk = db.nyHare(id, kjonn, lengde, vekt, sted, dato, hType, farge); //lagrer i databasen
		} catch (NullPointerException e) {
			throw e;
		} catch (SQLException e) {
			throw e;
		} // try/catch
		return sjekk;
	} //lagreHare

	public boolean lagreGjenfangstHare(String id, double lengde, double vekt, String sted, String dato, String farge)
	throws Exception {
		boolean sjekk = false;
		try {
			sjekk = db.nyGjenfangstHare(id, lengde, vekt, sted, dato, farge);
		} catch (NullPointerException e) {
			throw e;
		} catch (SQLException e) {
			throw e;
		} // try/catch
		return sjekk;
	} //lagreGjenfangstHare

	/***METODER FOR GAUPE***/
	public String lagGaupeId() { // metode for � lage en id for gaupe						 
		int j = GaupeTabell.size(); //setter j's verdi til ArrayList strl
		j++; //�ker j
		String id = "G" + j; //lager id til dyret		
		return id; //returner id'n
	} //lagGaupeId	

	public boolean lagreGaupe(char kjonn, double lengde, double vekt, String sted, String dato, double ore)
	throws Exception {
		boolean sjekk = false;
		String id = lagGaupeId();		
		try {
			//lager et nytt objekt av klassen Hare
			Gaupe regNyGaupe = new Gaupe(id, kjonn, lengde, vekt, sted, dato, ore);
			GaupeTabell.add(regNyGaupe); //legger haren inn i ArrayList		
			sjekk = db.nyGaupe(id, kjonn, lengde, vekt, sted, dato,ore); //lagrer i databasen
		} catch (NullPointerException e) {
			throw e;
		} catch (SQLException e) {
			throw e;
		}
		return sjekk;
	} //lagreGaupe

	public boolean lagreGjenfangstGaupe(String id, double lengde, double vekt, String sted, String dato)		
	throws Exception {
		boolean sjekk = false;
		try {
			sjekk = db.nyGjenfangstGaupe(id, lengde, vekt, sted, dato);
		} catch (NullPointerException e) {
			throw e;
		} catch (SQLException e) {
			throw e;
		}
		return sjekk;
	} //lagreGjenfangstGaupe

	/***FYLLING AV JTABLE***/
	public JTable fyllJTable() throws Exception {
		//lag et nytt objekt av default oppsettet for tabell-modellen	
		DefaultTableModel model = new DefaultTableModel(MAX_KOLONNER, MAX_RADER);
		int i = 0; //tabellplassering
		String type = null; //type dyr (hare/gaupe)
		ResultSet res =null; 
		JTable dyretabell = new JTable(model) {
			/**
			 * 
			 */
			private static final long serialVersionUID = 1L;

			public boolean isCellEditable(int rad, int kolonne) {
				return false;   //bruker f�r ikke editere innhold i tabellen
			} //isCellEditable
		}; //JTable dyretabell
		//navngi kolonnene
		dyretabell.getColumnModel().getColumn(0).setHeaderValue("Id"); 
		dyretabell.getColumnModel().getColumn(1).setHeaderValue("Kj�nn");
		dyretabell.getColumnModel().getColumn(2).setHeaderValue("Type");
		dyretabell.getColumnModel().getColumn(3).setHeaderValue("Dato");
		dyretabell.getTableHeader().resizeAndRepaint(); //endrer st�rrelse og navngivning til nye navn		
		try {			
			res = db.skrivUtFangst(); //hent resultatet
			while (res.next()) {
				String idDyr = res.getString("idDyr"); //henter resultat
				String kjonn = res.getString("kjonn");
				String dato = res.getString("dato");
				//sjekk hvilken type dyret er basert p� idDyr
				if (idDyr.charAt(0) == 'G') { //er det en Gaupe...
					type = "Gaupe";
				} else { //...eller en Hare
					type = "Hare";
				} //if (idDyr.charAt(0) == 'G')
				//setter inn verdier i dyretabell -> verdi, tabellplassering og nummerering
				dyretabell.setValueAt(idDyr, i, 0); 
				dyretabell.setValueAt(kjonn, i, 1);
				dyretabell.setValueAt(type, i, 2);
				dyretabell.setValueAt(dato, i, 3);
				i++; //�k teller
			} //while
		} catch (Exception e) {			
			throw e; //kast feilen videre
		} // try/catch
		return dyretabell;
	} //fyllJTable

	/***METODE FOR VISNING AV showMessageDialog***/
	public static void lagMessageDialog(String melding, String tittel, int msg_nr) {
		//har lagd en felles funksjon som sender ut en showMessageDialog,
		//msg_nr er en integer som bestemmer hvilket ikon som vises:
		//0 = ERROR, 1 = INFORMATION, 2 = WARNING, 3 = QUESTION, 4 =PLAIN 
		showMessageDialog(null, melding, tittel, msg_nr);
	} //lagMessageDialog	

	/***METODER FOR SJEKK AV ID***/
	public static boolean sjekkId(String id, char type) { 
		boolean sjekk = false;
		String tittel = "Finn ID"; //tittel for lagMessageDialog
		try {
			if(id.equals("")) { //er feltet blankt?
				lagMessageDialog("Dyrets id m� skrives inn.", tittel, 0);
			} else if (id.length() < 2) { //for lite er skrevet inn, m� minst v�re 2 tegn - eks: H1
				lagMessageDialog("Innskrevet id er for kort, m� minst v�re to tegn (Eks: H1).", tittel, 0);
			} else { //noe er skrevet inn
				sjekk = sokPaaId(id, type); //s�k etter oppgitt id		
			} //if(id.equals(""))
		} catch (NullPointerException e) { //ingen aktiv databasetilkobling
			lagMessageDialog("Databasetilkobling er ikke aktiv.\nDu kreves en aktiv tilkobling for � kunne s�ke p� ID.", tittel, 0);
		} catch (Exception e) { //uventet feil
			lagMessageDialog("En feil oppsto under s�k:\n"+ e, tittel, 0);
		} // try/catch
		return sjekk;
	} //sjekkId

	public static boolean sokPaaId(String inputId, char dyr) {		
		char typeDyr = inputId.charAt(0); //hent ut f�rste tegn innskrevet
		String tittel = "Feil ID";  //tittel for lagMessageDialog
		int j = -1; //settes til -1 grunnet den blir brukt som indeks for plassering
		boolean funnet = false;
		if (typeDyr != 'H' && typeDyr != 'G') { //er korrekt id oppgitt?			
			lagMessageDialog( "Id'n oppgitt: " + inputId +" er ikke gyldig. Gyldig startid er enten G eller H.",
					"Ugyldig Id",0); //feil id, gi/vis feilmelding
		} else { //id innskrevet er korrekt
			if (dyr == 'H') { //er dyret en hare...
				if (typeDyr == 'G') { //sjekk at det ikke s�kes etter gauper
					lagMessageDialog("Du kan kun registrere gjenfangst av harer her." , tittel, 2);
				} else {
					j = sokHare(inputId); //start s�k		
					funnet = sokeResultatId(inputId, j);
				} // if (typeInput == 'G') {
			} else { //...eller en gaupe?			
				if (typeDyr == 'H') { //sjekk at det ikke s�kes etter harer
					lagMessageDialog("Du kan kun registrere gjenfangst av gauper her." , tittel, 2);
				} else {
					j = sokGaupe(inputId); //start s�k
					funnet = sokeResultatId(inputId,j);
				} // if (typeInput == 'H')
			} //if (dyr == 'H')
		} //if (dyr != 'H' && dyr != 'G')
		return funnet;
	} //sokPaaId

	public static int sokGaupe(String idDyr) {		
		int j;
		for (int i = 0; i < GaupeTabell.size(); i++) {
			if (idDyr.toUpperCase().equals(GaupeTabell.get(i).getId())) { //er s�k lik id i ArrayList?
				j = i;
				return j; //returner posisjon
			} //if
		} //for (int i = 0; i < GaupeTabell.size(); i++)
		return -1; //dyr ikke funnet
	} //sokGaupe

	public static int sokHare(String idDyr){		
		int j;
		for(int i = 0; i < HareTabell.size(); i++) {
			if(idDyr.toUpperCase().equals(HareTabell.get(i).getId())) { //er s�k lik id i ArrayList?
				j = i;
				return j; //returner posisjon
			} //if
		} //for(int i = 0; i < HareTabell.size(); i++)		
		return -1; //dyr ikke funnet
	} //sokHare

	public static boolean sokeResultatId(String inputId, int j) {
		boolean funnet = false;
		if (j >= 0) { //finnes id'n?
			funnet = true;
		} else { //fant ikke id'n
			lagMessageDialog( "Id: " + inputId + " ble ikke funnet.","Ukjent ID", 0);			
		} //if (j >= 0)
		return funnet;
	} //sokeResultatId

	/***SJEKK METODER***/
	public static boolean sjekkLengde(String lengde) {
		boolean sjekk = false;
		String tittel = "Ikke utfylt lengde";  //tittel for lagMessageDialog
		try {
			if (lengde.equals("")) { //er lengde-feltet blankt?
				lagMessageDialog("Lengden m� skrives inn.", tittel, 0);
			} else {
				double l = Double.parseDouble(lengde); //pr�v � omgj�re verdien til en double
				if (l > 0) { //er verdien st�rre enn 0?
					sjekk = true;
				} else {
					lagMessageDialog("Lengde innskrevet er 0, verdi m� v�re st�rre enn 0." , tittel, 0);
				} //if (l > 0)
			} //if (lengde.equals(""))
		} catch (NumberFormatException e) { //ikke skrevet inn tall eller brukt punktum
			lagMessageDialog("Lengde m� v�re et tall, med punktum (.) som desimaltegn.", tittel, 0);
		} catch(Exception e) { //noe gikk galt
			lagMessageDialog("En feil oppsto: " + e, "Feil", 0);
		} // try/catch
		return sjekk;
	} //sjekkLengde

	public static boolean sjekkVekt(String vekt) {
		boolean sjekk = false;
		String tittel = "Ikke utfylt vekt";  //tittel for lagMessageDialog
		try {
			if (vekt.equals("")) { //er vekt-feltet blankt?
				lagMessageDialog("Vekten m� skrives inn.",tittel , 0);
			} else {
				double v = Double.parseDouble(vekt);  //pr�v � omgj�re verdien til en double
				if (v > 0) { //er verdien st�rre enn 0?
					sjekk = true;
				} else {
					lagMessageDialog("Verdi innskrevet er 0, verdi m� v�re st�rre enn 0." , tittel, 0);
				} //if (v > 0)
			} //if (vekt.equals(""))
		} catch (NumberFormatException e) { //ikke skrevet inn tall,eller brukt punktum
			lagMessageDialog("Vekt m� v�re et tall, med punktum (.) som desimaltegn.", tittel, 0);			
		} catch(Exception e) { //noe gikk galt
			lagMessageDialog("En feil oppsto: " + e, "Feil", 0);
		} // try/catch
		return sjekk;
	} //sjekkVekt

	public static boolean sjekkOre (String oreLengde) {
		boolean sjekk = false;
		String tittel = "Ikke utfylt �relengde";  //tittel for lagMessageDialog
		try {
			if (oreLengde.equals("")) { //er vekt-feltet blankt?
				lagMessageDialog("�relengden m� skrives inn.", tittel, 0);
			} else {
				double ore = Double.parseDouble(oreLengde); //pr�v � omgj�re verdien til en double
				if (ore > 0) { //er verdien st�rre enn 0?
					sjekk = true;
				} else {
					lagMessageDialog("�relengden innskrevet er 0, verdi m� v�re st�rre enn 0.", tittel, 0);
				} //if (ore > 0)
			} //if (ore.equals(""))
		} catch (NumberFormatException e) { //ikke skrevet inn tall eller brukt punktum			
			lagMessageDialog("�relengden m� v�re et tall, med punktum (.) som desimaltegn.", tittel, 0);
		} catch(Exception e) { //noe gikk galt			
			lagMessageDialog("En feil oppsto: " + e, "Feil", 0);
		} // try/catch
		return sjekk;
	} //sjekkOre

	public static boolean sjekkSted(String sted) {
		boolean sjekk = false;
		String tittel =  "Ikke utfylt sted";  //tittel for lagMessageDialog
		if (sted.equals("")) { //er sted-feltet blankt?			
			lagMessageDialog("Sted for fangst m� skrives inn.", tittel, 0);
		} else if (sted.length() < 2 || sted.toUpperCase().charAt(0) !='S') { //er det minst to tegn og S som forbokstav?			
			lagMessageDialog("Ugyldig sted skrevet inn, m� minst v�re 2 tegn og starte p� S.", tittel, 0);
		} else { //gyldig sted innskrevet
			sjekk = true;
		} //if (sted.equals(""))
		return sjekk;
	} //sjekkSted

	public static boolean sjekkDato(String d) {		
		boolean sjekk = false;
		char pkt = 0, pkt2 = 0; //char for sjekk av punktum		
		String feil1 = "Feil skilletegn";  //tittel for lagMessageDialog
		String feil2 = "Feil dato";  //tittel for lagMessageDialog
		int dag, mnd, aar; //skal sjekke dag, mnd og �r
		try {							
			dag = Integer.parseInt(d.substring(0, 2)); //omgj�r 2 f�rste tegnene til heltall
			mnd = Integer.parseInt(d.substring(3, 5)); //omgj�r det 4 og 5 tegnet til heltall
			aar = Integer.parseInt(d.substring(6)); //omgj�r det 7 tegnet og ut til heltall
			pkt = d.charAt(2); //leser det 3 tegnet
			pkt2 = d.charAt(5); //leser det 6 tegnet
			if (d.length() > 9 && d.length() < 11) { //er lengden p� �ret korrekt?
				if (dag < 32 && dag > 0) { //er dagen mellom 1 - 31?
					if (pkt == '.') { //er 1 skilletegn et punktum?
						if (mnd > 0 && mnd < 13) { //er m�neden mellom 1 -12?
							if (pkt2 == '.') { // er 2 skilletegn et punktum? 
								if (aar > 1979 && aar < 2100) { //er �ret gyldig?			
									sjekk = true;
								} else { //ugyldig �r (utenfor satt radius)									
									lagMessageDialog("Ugyldig �r innskrevet. �ret m� v�re mellom 1980 - 2099", "Feil �r", 0);
								}//if(aar)
							} else { //ikke punktum
								lagMessageDialog("Skilletegn m� v�re punktum.", feil1, 0);
							}//if(pkt2)
						} else { //ugyldig m�ned							
							lagMessageDialog("Ugyldig m�ned innskrevet. Gyldig m�ned er 1-12.", "Feil m�ned", 0);
						} //if(mnd)
					} else { //ikke punktum						
						lagMessageDialog("Skilletegn m� v�re punktum.", feil1, 0);
					} //if(pkt)
				} else { //ugyldig dag					
					lagMessageDialog("Ugyldig dag innskrevet. Gyldig dag er 1-31.", "Feil dag", 0);
				} //if(dag)
			} else { //ikke korrekt lengde p� datoen innskrevet
				lagMessageDialog("Datoen skrevet inn er ikke korrekt.\nGyldig datoformatet er: DD.MM.YYYY.", feil2, 0);
			} //if(dato)
		} catch (StringIndexOutOfBoundsException e) { //for kort tekst er skrevet inn			
			lagMessageDialog("Datoen skrevet inn er ikke korrekt.\nGyldig datoformatet er: DD.MM.YYYY.", feil2, 0);
		} catch (NumberFormatException e) { //klarte ikke omgj�ring fra String til heltall			
			lagMessageDialog( "Datoen m� best� tall (01-31) og punktum som skilletegn.", feil2, 0);
		} catch (Exception e) { //oppsto en (uventet) feil			
			lagMessageDialog("En feil oppsto, vennligst pr�v igjen", feil2, 0);
		} //try/catch
		return sjekk; 
	} //oppgiDato	
} //Kontroll