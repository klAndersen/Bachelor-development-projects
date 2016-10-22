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
	private Database db = new Database();	
	private static final int MAX_KOLONNER = 1000; //antall verdier som kan vises/legges i JTable
	private static final int MAX_RADER = 4;
	private ArrayList<Hare> HareTabell = db.hentHare();
	private ArrayList <Gaupe> GaupeTabell = db.hentGaupe();		

	/***METODER FOR HARE***/
	public String lagHareId() { // metode for å lage en id for hare						 
		int j =  HareTabell.size(); //setter j's verdi til ArrayList strl
		j++; //øker j
		String id = "H" + j; //lager id til dyret		
		return id; //returner id'n
	} //lagHareId

	public String lagreHare(char kjonn, double lengde, double vekt, String sted, String dato, char hType, String farge) {
		String sjekk = null;
		String id = lagHareId(); //lag ny id for haren
		try {					
			//lager et nytt objekt av klassen Hare
			Hare regNyHare = new Hare(id, kjonn, lengde, vekt, sted, dato, hType, farge);
			HareTabell.add(regNyHare); //legger haren inn i ArrayList		
			sjekk = db.nyHare(id, kjonn, lengde, vekt, sted, dato, hType, farge); //lagrer i databasen
			db.kobleNed(); //kobler ned forbindelsen til databasen
		} catch (SQLException e) {
			lagMessageDialog("Kan ikke koble ned forbindelse til database.", "Lukk forbindelse", 0);				
		} // try/catch
		return sjekk;
	} //lagreHare

	public String lagreGjenfangstHare(String id, double lengde, double vekt, String sted, String dato, String farge) {	
		String sjekk = null;
		try {
			sjekk = db.nyGjenfangstHare(id, lengde, vekt, sted, dato, farge);
			db.kobleNed(); //kobler ned forbindelsen til databasen
		} catch (SQLException e) {			
			lagMessageDialog("Kan ikke koble ned forbindelse til database.", "Lukk forbindelse", 0);
		} // try/catch
		return sjekk;
	} //lagreGjenfangstHare

	/***METODER FOR GAUPE***/
	public String lagGaupeId() { // metode for å lage en id for gaupe						 
		int j = GaupeTabell.size(); //setter j's verdi til ArrayList strl
		j++; //øker j
		String id = "G" + j; //lager id til dyret		
		return id; //returner id'n
	} //lagGaupeId	

	public String lagreGaupe(char kjonn, double lengde, double vekt, String sted, String dato, double ore) {
		String sjekk = null;
		String id = lagGaupeId();		
		try {
			//lager et nytt objekt av klassen Hare
			Gaupe regNyGaupe = new Gaupe(id, kjonn, lengde, vekt, sted, dato, ore);
			GaupeTabell.add(regNyGaupe); //legger haren inn i ArrayList		
			sjekk = db.nyGaupe(id, kjonn, lengde, vekt, sted, dato,ore); //lagrer i databasen
			db.kobleNed(); //kobler ned forbindelsen til databasen
		} catch (SQLException e) {
			lagMessageDialog("Kan ikke koble ned forbindelse til database.", "Lukk forbindelse", 0);
		} // try/catch
		return sjekk;
	} //lagreGaupe

	public String lagreGjenfangstGaupe(String id, double lengde, double vekt, String sted, String dato) {		
		String sjekk = null;
		try {
			sjekk = db.nyGjenfangstGaupe(id, lengde, vekt, sted, dato);
			db.kobleNed(); //kobler ned forbindelsen til databasen
		} catch (SQLException e) {
			lagMessageDialog("Kan ikke koble ned forbindelse til database.", "Lukk forbindelse", 0);	
		} // try/catch
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
				return false;   //bruker får ikke editere innhold i tabellen
			} //isCellEditable
		}; //JTable dyretabell
		//navngi kolonnene
		dyretabell.getColumnModel().getColumn(0).setHeaderValue("Id"); 
		dyretabell.getColumnModel().getColumn(1).setHeaderValue("Kjønn");
		dyretabell.getColumnModel().getColumn(2).setHeaderValue("Type");
		dyretabell.getColumnModel().getColumn(3).setHeaderValue("Dato");
		dyretabell.getTableHeader().resizeAndRepaint(); //endrer størrelse og navngivning til nye navn		
		try {			
			res = db.skrivUtFangst(); //hent resultatet
			while (res.next()) {
				String idDyr = res.getString("idDyr"); //henter resultat
				String kjonn = res.getString("kjonn");
				String dato = res.getString("dato");
				//sjekk hvilken type dyret er basert på idDyr
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
				i++; //øk teller
			} //while
		} catch (Exception e) {
			dyretabell.setValueAt("Klarte ikke fylle tabell", 0, 0); //legg inn en feilmelding i første rad
			throw e; //kast feilen videre
		} // try/catch
		return dyretabell;
	} //fyllJTable

	/***METODE FOR VISNING AV showMessageDialog***/
	public void lagMessageDialog(String melding, String tittel, int msg_nr) {
		//har lagd en felles funksjon som sender ut en showMessageDialog,
		//msg_nr er en integer som bestemmer hvilket ikon som vises:
		//0 = ERROR, 1 = INFORMATION, 2 = WARNING, 3 = QUESTION, 4 =PLAIN 
		showMessageDialog(null, melding, tittel, msg_nr);
	} //lagMessageDialog	

	/***METODER FOR SJEKK AV ID***/
	public boolean sjekkId(String id, char type) { 
		boolean sjekk = false;
		String tittel = "Ikke utfylt ID"; //tittel for lagMessageDialog
		if(id.equals("")) { //er feltet blankt?
			lagMessageDialog("Dyrets id må skrives inn.", tittel, 0);
		} else if (id.length() < 2) { //for lite er skrevet inn, må minst være 2 tegn - eks: H1
			lagMessageDialog("Innskrevet id er for kort, må minst være to tegn (Eks: H1).", tittel, 0);
		} else { //noe er skrevet inn
			sjekk = sokPaaId(id, type); //søk etter oppgitt id		
		} //if(id.equals(""))
		return sjekk;
	} //sjekkId

	public boolean sokPaaId(String inputId, char dyr) {		
		char typeDyr = inputId.charAt(0); //hent ut første tegn innskrevet
		String tittel = "Feil ID";  //tittel for lagMessageDialog
		int j = -1; //settes til -1 grunnet den blir brukt som indeks for plassering
		boolean funnet = false;
		if (typeDyr != 'H' && typeDyr != 'G') { //er korrekt id oppgitt?			
			lagMessageDialog( "Id'n oppgitt: " + inputId +" er ikke gyldig. Gyldig startid er enten G eller H.",
					"Ugyldig Id",0); //feil id, gi/vis feilmelding
		} else { //id innskrevet er korrekt
			if (dyr == 'H') { //er dyret en hare...
				if (typeDyr == 'G') { //sjekk at det ikke søkes etter gauper
					lagMessageDialog("Du kan kun registrere gjenfangst av harer her." , tittel, 2);
				} else {
					j = sokHare(inputId); //start søk		
					funnet = sokeResultatId(inputId, j);
				} // if (typeInput == 'G') {
			} else { //...eller en gaupe?			
				if (typeDyr == 'H') { //sjekk at det ikke søkes etter harer
					lagMessageDialog("Du kan kun registrere gjenfangst av gauper her." , tittel, 2);
				} else {
					j = sokGaupe(inputId); //start søk
					funnet = sokeResultatId(inputId,j);
				} // if (typeInput == 'H') {
			} //if (dyr == 'H')
		} //if (dyr != 'H' && dyr != 'G')
		return funnet;
	} //sokPaaId

	public int sokGaupe(String idDyr) {		
		int j;
		for (int i = 0; i < GaupeTabell.size(); i++) {
			if (idDyr.toUpperCase().equals(GaupeTabell.get(i).getId())) { //er søk lik id i ArrayList?
				j = i;
				return j; //returner posisjon
			} //if
		} //for (int i = 0; i < GaupeTabell.size(); i++)
		return -1; //dyr ikke funnet
	} //sokGaupe

	public int sokHare(String idDyr){		
		int j;
		for(int i = 0; i < HareTabell.size(); i++) {
			if(idDyr.toUpperCase().equals(HareTabell.get(i).getId())) { //er søk lik id i ArrayList?
				j = i;
				return j; //returner posisjon
			} //if
		} //for(int i = 0; i < HareTabell.size(); i++)		
		return -1; //dyr ikke funnet
	} //sokHare

	public boolean sokeResultatId(String inputId, int j) {
		boolean funnet = false;
		if (j >= 0) { //finnes id'n?
			funnet = true;
		} else { //fant ikke id'n
			lagMessageDialog( "Id: " + inputId + " ble ikke funnet.","Ukjent ID", 0);			
		} //if (j >= 0)
		return funnet;
	} //sokeResultatId

	/***SJEKK METODER***/
	public boolean sjekkLengde(String lengde) {
		boolean sjekk = false;
		String tittel = "Ikke utfylt lengde";  //tittel for lagMessageDialog
		try {
			if (lengde.equals("")) { //er lengde-feltet blankt?
				lagMessageDialog("Lengden må skrives inn.", tittel, 0);
			} else {
				double l = Double.parseDouble(lengde); //prøv å omgjøre verdien til en double
				if (l > 0) { //er verdien større enn 0?
					sjekk = true;
				} else {
					lagMessageDialog("Lengde innskrevet er 0, verdi må være større enn 0." , tittel, 0);
				} //if (l > 0)
			} //if (lengde.equals(""))
		} catch (NumberFormatException e) { //ikke skrevet inn tall eller brukt punktum
			lagMessageDialog("Lengde må være et tall, med punktum (.) som desimaltegn.", tittel, 0);
		} catch(Exception e) { //noe gikk galt
			lagMessageDialog("En feil oppsto: " + e, "Feil", 0);
		} // try/catch
		return sjekk;
	} //sjekkLengde

	public boolean sjekkVekt(String vekt) {
		boolean sjekk = false;
		String tittel = "Ikke utfylt vekt";  //tittel for lagMessageDialog
		try {
			if (vekt.equals("")) { //er vekt-feltet blankt?
				lagMessageDialog("Vekten må skrives inn.",tittel , 0);
			} else {
				double v = Double.parseDouble(vekt);  //prøv å omgjøre verdien til en double
				if (v > 0) { //er verdien større enn 0?
					sjekk = true;
				} else {
					lagMessageDialog("Verdi innskrevet er 0, verdi må være større enn 0." , tittel, 0);
				} //if (v > 0)
			} //if (vekt.equals(""))
		} catch (NumberFormatException e) { //ikke skrevet inn tall,eller brukt punktum
			lagMessageDialog("Vekt må være et tall, med punktum (.) som desimaltegn.", tittel, 0);			
		} catch(Exception e) { //noe gikk galt
			lagMessageDialog("En feil oppsto: " + e, "Feil", 0);
		} // try/catch
		return sjekk;
	} //sjekkVekt

	public boolean sjekkOre (String oreLengde) {
		boolean sjekk = false;
		String tittel = "Ikke utfylt ørelengde";  //tittel for lagMessageDialog
		try {
			if (oreLengde.equals("")) { //er vekt-feltet blankt?
				lagMessageDialog("Ørelengden må skrives inn.", tittel, 0);
			} else {
				double ore = Double.parseDouble(oreLengde); //prøv å omgjøre verdien til en double
				if (ore > 0) { //er verdien større enn 0?
					sjekk = true;
				} else {
					lagMessageDialog("Ørelengden innskrevet er 0, verdi må være større enn 0.", tittel, 0);
				} //if (ore > 0)
			} //if (ore.equals(""))
		} catch (NumberFormatException e) { //ikke skrevet inn tall eller brukt punktum			
			lagMessageDialog("Ørelengden må være et tall, med punktum (.) som desimaltegn.", tittel, 0);
		} catch(Exception e) { //noe gikk galt			
			lagMessageDialog("En feil oppsto: " + e, "Feil", 0);
		} // try/catch
		return sjekk;
	} //sjekkOre

	public boolean sjekkSted(String sted) {
		boolean sjekk = false;
		String tittel =  "Ikke utfylt sted";  //tittel for lagMessageDialog
		if (sted.equals("")) { //er sted-feltet blankt?			
			lagMessageDialog("Sted for fangst må skrives inn.", tittel, 0);
		} else if (sted.length() < 2 || sted.toUpperCase().charAt(0) !='S') { //er det minst to tegn og S som forbokstav?			
			lagMessageDialog("Ugyldig sted skrevet inn, må minst være 2 tegn og starte på S.", tittel, 0);
		} else { //gyldig sted innskrevet
			sjekk = true;
		} //if (sted.equals("")) {
		return sjekk;
	} //sjekkSted

	public boolean sjekkDato(String d) {		
		boolean sjekk = false;
		char pkt = 0, pkt2 = 0; //char for sjekk av punktum		
		String feil1 = "Feil skilletegn";  //tittel for lagMessageDialog
		String feil2 = "Feil dato";  //tittel for lagMessageDialog
		int dag, mnd, aar; //skal sjekke dag, mnd og år
		try {							
			dag = Integer.parseInt(d.substring(0, 2)); //omgjør 2 første tegnene til heltall
			mnd = Integer.parseInt(d.substring(3, 5)); //omgjør det 4 og 5 tegnet til heltall
			aar = Integer.parseInt(d.substring(6)); //omgjør det 7 tegnet og ut til heltall
			pkt = d.charAt(2); //leser det 3 tegnet
			pkt2 = d.charAt(5); //leser det 6 tegnet
			if (d.length() > 9 && d.length() < 11) { //er lengden på året korrekt?
				if (dag < 32 && dag > 0) { //er dagen mellom 1 - 31?
					if (pkt == '.') { //er 1 skilletegn et punktum?
						if (mnd > 0 && mnd < 13) { //er måneden mellom 1 -12?
							if (pkt2 == '.') { // er 2 skilletegn et punktum? 
								if (aar > 1979 && aar < 2100) { //er året gyldig?			
									sjekk = true;
								} else { //ugyldig år (utenfor satt radius)									
									lagMessageDialog("Ugyldig år innskrevet. Året må være mellom 1980 - 2099", "Feil år", 0);
								}//if(aar)
							} else { //ikke punktum
								lagMessageDialog("Skilletegn må være punktum.", feil1, 0);
							}//if(pkt2)
						} else { //ugyldig måned							
							lagMessageDialog("Ugyldig måned innskrevet. Gyldig måned er 1-12.", "Feil måned", 0);
						} //if(mnd)
					} else { //ikke punktum						
						lagMessageDialog("Skilletegn må være punktum.", feil1, 0);
					} //if(pkt)
				} else { //ugyldig dag					
					lagMessageDialog("Ugyldig dag innskrevet. Gyldig dag er 1-31.", "Feil dag", 0);
				} //if(dag)
			} else { //ikke korrekt lengde på datoen innskrevet
				lagMessageDialog("Datoen skrevet inn er ikke korrekt.", feil2, 0);
			} //if(dato)
		} catch (StringIndexOutOfBoundsException e) { //for kort tekst er skrevet inn			
			lagMessageDialog("Datoen skrevet inn er ikke korrekt.", feil2, 0);
		} catch (NumberFormatException e) { //klarte ikke omgjøring fra String til heltall			
			lagMessageDialog( "Datoen må bestå tall (01-31) og punktum som skilletegn.", feil2, 0);
		} catch (Exception e) { //oppsto en (uventet) feil			
			lagMessageDialog("En feil oppsto, vennligst prøv igjen", feil2, 0);
		} //try/catch
		return sjekk; 
	} //oppgiDato	
} //Kontroll