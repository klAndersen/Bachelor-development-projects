/*
 * Dette er et program som skal registrere fangst og gjenfangst av dyr. Kun 2 dyr som
 * blir registrert i dette programmet; Gaupe og Hare. Videre skal programmet 
 * skrive ut informasjon om alt som er registrert på et dyr, man skal kunne søke på
 * år for gjenfangst se hvor mange dyr som ble gjenfanget, sjekke typen hare som er 
 * registrert og skrive ut en usortert rapport (med skrive ut menes da det på skjerm).
 * Programmet leser og lagrer til tekstfil av typen .txt
 */
import java.io.*;
import java.util.*;
class Oblig2_java {
	public static void main(String[] args) {		
		Meny lagMeny = new Meny(); //lager nytt objekt av klassen Meny
		lagMeny.visMeny();
	} //main
} //klassen Oblig2_java

class Meny {
	//lag et nytt objekt av klassen Fangstkontroll
	private FangstKontroll kontroll = new FangstKontroll();	
	Scanner scan = new Scanner(System.in); //lag et nytt objekt av klassen Scanner
	public void visMeny() {		
		int valg = 0;	
		char dyreType = 0;
		boolean avslutt = true;
		try {
		lesHarer(); //leser og fyller ArrayList med verdiene fra fil ved oppstart
		lesGaupe(); //leser og fyller ArrayList med verdiene fra fil ved oppstart	
		while (avslutt){			
		System.out.println(	"1: Registrer et nytt dyr\n" +
							"2: Registrer en gjenfangst\n" +
							"3: Søk på dyr (via id)\n" +
							"4: Søk etter antall gjenfangster (på et år)\n" +
							"5: Oversikt over haretyper\n" +
							"6: Vis rapport over dyr som er fanget\n" +
							"7: Avslutt programmet\n"); //skriver ut/viser menyvalget			
		System.out.println("Velg et alternativ ut fra menyen:"); //Be om et valg		
		valg = scan.nextInt(); //les valget fra brukeren			
			switch(valg) { //gi bruker ønsket meny-valg
			case 1: 
				do {
					System.out.println("Hvilket dyr er fanget?\n" +
						"(H for Hare, G for Gaupe)");  //Be om valg av dyr
					//gjør om char til stor bokstav
					dyreType = Character.toUpperCase(scan.next().charAt(0));
					if (dyreType == 'H'){ //Er dyret en hare...
						nyHare();
					} else if (dyreType == 'G') { //...eller en gaupe?
						nyGaupe();
					}
				} while (dyreType != 'H' && dyreType != 'G');
			break;
			case 2: 
				do {
					System.out.println("Hvilket dyr er gjenfanget?\n" +
							"(H for Hare, G for Gaupe)");  //Be om valg av dyr
					//gjør om char til stor bokstav
					dyreType = Character.toUpperCase(scan.next().charAt(0));
					if (dyreType == 'H'){ //Er dyret en hare...
						gjenfangstHare();
					} else if (dyreType == 'G') { //...eller en gaupe?
						gjenfangstGaupe();
					} //if
					} while (dyreType != 'H' && dyreType != 'G');				
			break;
			case 3: finnDyr();
			break;	
			case 4: finnFangstAar();
			break;
			case 5: antHarer();
			break;
			case 6: kontroll.lagRapport();
			break;
			default: avslutt = false; //avslutter løkka go programmet
			} //switch			
		} //løkka
		} catch (InputMismatchException e){
			//input er noe annet enn de oppgitte tallene (f.eks bokstaver)
			System.out.println("Ugyldig alternativ. De forskjellige menyvalgene er "
					+ "et tall mellom 1 - 7. Programmet avsluttes.");
		} // try/catch
		scan.close(); //lukker Scanneren
	} //visMeny()
	/***********METODER FOR HARE*************/
	public void nyHare(){
		String dyr = "haren"; //forteller hvilket dyr som skal legges inn i finn-Funksjonene
		char hareType;
		char f;
		String farge;		
		String idDyr = kontroll.lagHareId(); //kaller på funksjonen for å lage ny id
		System.out.println("Harens id: " + idDyr); //skriver ut hvilken id dyret ble tildelt
		char kjonn = oppgiKjonn(dyr);
		double lengde = oppgiLengde(dyr);
		double vekt = oppgiVekt(dyr);
		do { //kjøres minst en gang
			System.out.println("Hva slags individ er haren? (V/S):");
			hareType = Character.toUpperCase(scan.next().charAt(0)); //gjør om til stor bokstav
			//kjøres så lenge det ikke er inntastet en V eller S
		} while(hareType != 'V' && hareType != 'S'); 	
		do { //kjøres minst en gang
		System.out.println("Skriv inn fargen på haren (H/B):");
		f = Character.toUpperCase(scan.next().charAt(0));
		//kjøres så lenge det ikke er inntastet en H eller B
		} while(f != 'H' && f != 'B');
		farge = finnFarge(f); //finn fargen til haren
		String sted = oppgiSted(dyr);
		String dato = oppgiDato(dyr);
		//legger haren inn i ArrayList
		kontroll.opprettNyHare(idDyr, kjonn, lengde, vekt, hareType, farge, sted, dato);
		skrivHarer(); //kaller på skrivHare for å skrive til fil
	} //nyHare
	public void gjenfangstHare(){
		String dyr = "hare"; //forteller hvilket dyr som skal legges inn i finn-Funksjonene
		String idDyr;
		String farge;
		char f;
		int j = 0;		
		System.out.println("Skriv inn harens id:");
		idDyr = scan.next().toUpperCase();
		j = kontroll.sokHare(idDyr); //søk etter dyret med gitt id
		if (j >= 0) { //finnes id'n...
			double lengde = oppgiLengde(dyr);
			double vekt = oppgiVekt(dyr);
			do{ //kjøres minst en gang
				System.out.println("Skriv inn fargen på haren (H/B):");
				f = Character.toUpperCase(scan.next().charAt(0));
				//kjøres så lenge det ikke er inntastet en H eller B
			} while(f != 'H' && f != 'B');
			String sted = oppgiSted(dyr);
			String dato = oppgiDato(dyr);
			farge = finnFarge(f); //finn fargen til haren
			//legger haren inn i ArrayList
			kontroll.opprettNyGjenfangstHare(idDyr, lengde, vekt, farge, sted, dato);
			lagreGjenfangstHare(); //kaller på lagreGjenfangstHare for å skrive til fil
		} else { //...fant ikke id'n...
			System.out.println("Fant ikke " + dyr + ". Id'n du søkte på var: " + idDyr.toUpperCase());
			System.out.println(); //blank linje
		} //if
	} //gjenfangstHare
	public String finnFarge(char f){
		String farge;
		if (f == 'H') { //hvis bokstaven er H...
			farge = "Hvit"; //...så er haren hvit
		} else { //...bokstaven er B...
			farge = "Brun"; //...så haren er brun
		} //if
		return farge; //returner fargen
	} //finnFarge
	public void antHarer(){
		kontroll.antHarer(); //kaller på metoden for å finne antall harer fra FangstKontroll
	} //antHarer
	public void lesHarer() {
		//sjekker om innlastning av tekstfil er ok
		boolean sjekk = kontroll.lesHareFraFil();
		if(!sjekk) { //ble fil lest?
			System.out.println("Klarte ikke å lese inn filen for harer.");
			System.out.println(); //blank linje
		} //if
		boolean sjekk2 = kontroll.lesHareGjenfangst();
		if(!sjekk2) { 
			System.out.println("Klarte ikke å lese inn filen for gjenfangst av harer.");
			System.out.println(); //blank linje
		} //if
	} //lesHarer
	public void skrivHarer() {
		boolean sjekk = kontroll.skrivHareTilFil();
		if (sjekk){ //ble det lagret til fil?
			System.out.println("Fangsten er nå lagret på fil.");
			System.out.println(); //blank linje
		} else { //klarte ikke lagre
			System.out.println("Klarte ikke å lagre data");
			System.out.println(); //blank linje
		} //if
	} //skrivHarer
	public void lagreGjenfangstHare(){
		boolean sjekk = kontroll.skrivHareGjenfangst();
		if (sjekk){ //ble det lagret til fil?
			System.out.println("Gjenfangst er nå lagret på fil.");
			System.out.println(); //blank linje
		} else { //klarte ikke lagre
			System.out.println("Klarte ikke å lagre data");
			System.out.println(); //blank linje
		}
	} //lagreGjenfangstHare
	/***********METODER FOR GAUPE*************/
	public void nyGaupe() {
		String dyr = "gaupen"; //forteller hvilket dyr som skal legges inn i finn-Funksjonene
		double gaupeOre = 0;
		boolean ok = false;
		String idDyr = kontroll.lagGaupeId(); //kaller på funksjonen for å lage ny id
		System.out.println("Gaupens id: " + idDyr);	//skriver ut hvilken id dyret ble tildelt
		char kjonn = oppgiKjonn(dyr);
		double lengde = oppgiLengde(dyr);
		double vekt = oppgiVekt(dyr);		
		do { //kjøres minst en gang
			String input;
			System.out.println("Skriv inn lengden på gaupens øretust(bruk , som desimaltegn):");	
			try {		
			input = scan.next();
			gaupeOre = Double.parseDouble(input); //prøv å omgjøre til double
			ok = true;
			} catch(NumberFormatException e) { //klarte ikke å omgjøre til double
				System.out.println("Feil ved omforming av tall " +
				"(Bruk punktum som skilletegn (eks: 2.5).");
			} // try/catch
			} while(!ok);
		String sted = oppgiSted(dyr);
		String dato = oppgiDato(dyr);
		//legger gaupen inn i ArrayList
		kontroll.opprettNyGaupe(idDyr, kjonn, lengde, vekt, gaupeOre, sted, dato);
		skrivGaupe(); //kaller på skrivGaupe for å skrive til fil
	} //nyGaupe
	public void gjenfangstGaupe(){
		String dyr = "gaupen"; //forteller hvilket dyr som skal legges inn i finn-Funksjonene
		String idDyr;
		int j = 0;
		System.out.println("Skriv inn gaupens id:");
		idDyr = scan.next().toUpperCase();
		j = kontroll.sokGaupe(idDyr); //søk etter dyret med gitt id
		if (j >= 0) { //finnes id'n...
			double lengde = oppgiLengde(dyr);
			double vekt = oppgiVekt(dyr);
			String sted = oppgiSted(dyr);
			String dato = oppgiDato(dyr);
			//legger gaupen inn i ArrayList
			kontroll.nyGjenfangstGaupe(idDyr, lengde, vekt, sted, dato);
			lagreGaupeGjenfangst();  //kaller på lagreGaupeGjenfangst for å skrive til fil
		} else {  //...fant ikke id'n...
			System.out.println("Fant ikke " + dyr + ". Id'n du søkte på var: " + idDyr.toUpperCase());
			System.out.println(); //blank linje
		} //if
	}  //gjenfangstGaupe
	public void lesGaupe(){
		//sjekker om innlastning av tekstfil er ok
		boolean sjekk = kontroll.lesGaupeFraFil();
		if(!sjekk) { //ble fil lest?
			System.out.println("Klarte ikke å lese inn filen for gauper.");
			System.out.println(); //blank linje
		} //if
		boolean sjekk2 = kontroll.lesGjenfangstGaupe();
		if(!sjekk2) { //ble fil lest?
			System.out.println("Klarte ikke å lese inn filen for gjenfangst av harer.");
			System.out.println(); //blank linje
		} //if
	} //lesGaupe
	public void skrivGaupe(){
		boolean sjekk = kontroll.skrivGaupeTilFil();
		if (sjekk){ //ble det lagret til fil?
			System.out.println("Fangsten er nå lagret på fil.");
			System.out.println(); //blank linje
		} else { //ble det lagret til fil?
			System.out.println("Klarte ikke å lagre data");
			System.out.println(); //blank linje
		} //if
	} //skrivGaupe
	public void lagreGaupeGjenfangst(){
		boolean sjekk = kontroll.skrivGaupeGjenfangst();
		if (sjekk){ //ble det lagret til fil?
			System.out.println("Gjenfangst er nå lagret på fil.");
			System.out.println(); //blank linje
		} else { //klarte ikke lagre
			System.out.println("Klarte ikke å lagre data");
			System.out.println();  //blank linje
		} //if
	} //lagreGaupeGjenfangst
	/****POLYMORFENDE METODER****(BRUKES AV BEGGE KLASSENE)*****/
	public double oppgiLengde(String dyr) {
		boolean ok = false;
		double lengde = 0;
		do { //kjøres minst en gang
			String input;
			System.out.println("Skriv inn " + dyr + "s lengde:");	
			try {		
			input = scan.next(); //les input
			lengde = Double.parseDouble(input); //prøv å omgjøre til double
			ok = true; //input var korrekt
			} catch(NumberFormatException e) { //klarte ikke omgjøre til double
				System.out.println("Feil ved omforming av tall " +
						"(Bruk punktum som skilletegn (eks: 2.5).");
			} // try/catch
			}while(!ok); //kjøres så lenge input ikke er ok
		return lengde; //returner lengde
	} //oppgiLengde
	public double oppgiVekt(String dyr){
		boolean ok = false;
		double vekt = 0;
		do { //kjøres minst en gang
			String input;
			System.out.println("Skriv inn " + dyr + "s vekt:");
			try {
				input = scan.next(); //les input
				vekt = Double.parseDouble(input); //prøv å omgjøre til double
				ok = true; //input var korrekt
			} catch(NumberFormatException e) { //klarte ikke omgjøre til double
				System.out.println("Feil ved omforming av tall " +
				"(Bruk punktum som skilletegn (eks: 2.5).");
			} // try/catch			
		}while(!ok); //kjøres så lenge input ikke er ok
		return vekt; //returner vekt
	} //oppgiVekt
	public char oppgiKjonn(String dyr) {
		char kjonn;
		do { //kjøres minst en gang
			System.out.println("Skriv inn " + dyr + "s kjønn (M/F):");
			//les input og gjør om til stor bokstav:
			kjonn = Character.toUpperCase(scan.next().charAt(0));
			} while (kjonn != 'M' &&  kjonn != 'F');
		return kjonn; //returner kjønn
	} //oppgiKjonn
	public String oppgiSted(String dyr){
		System.out.println("Skriv inn stedet der " + dyr + " ble fanget:");
		String sted = scan.next();  //les input
		return sted; //returner sted
	}
	public String oppgiDato(String dyr){		
		String dato = null;
		char pkt = 0, pkt2 = 0; //char for sjekk av punktum
		int dag, mnd, aar; //skal sjekke dag, mnd og år
		boolean ok = false;		
		do {
			try {
				System.out.println("Skriv inn datoen " + dyr + " ble fanget(DD.MM.YYYY):");
				dato = scan.next(); //les input
				dag = Integer.parseInt(dato.substring(0, 2)); //omgjør 2 første tegnene til heltall
				mnd = Integer.parseInt(dato.substring(3, 5)); //omgjør det 4 og 5 tegnet til heltall
				aar = Integer.parseInt(dato.substring(6)); //omgjør det 7 tegnet og ut til heltall
				pkt = dato.charAt(2); //leser det 3 tegnet
				pkt2 = dato.charAt(5); //leser det 6 tegnet
				if (dato.length() > 9 && dato.length() < 11) { //er lengden på året korrekt?
					if (dag < 32 && dag > 0) { //er dagen mellom 1 - 31?
						if (pkt == '.') { //er 1 skilletegn et punktum?
							if (mnd > 0 && mnd < 13) { //er måneden mellom 1 -12?
								if (pkt2 == '.') { // er 2 skilletegn et punktum? 
									if (aar > 1979 && aar < 2100) { //er året gyldig?
										ok = true; //input er ok
									} else { //ugyldig år (utenfor satt radius)
										System.out.println("Feil år innskrevet. Året må være mellom 1980 - 2099");
									}//if(aar)
								} else { //ikke punktum
									System.out.println("Skilletegn må være punktum.");
								}//if(pkt2)
							} else { //ugyldig måned
								System.out.println("Måneden innskrevet er ugyldig. Gyldig måned er 1-12.");
							} //if(mnd)
						} else { //ikke punktum
						System.out.println("Skilletegn må være punktum.");
						} //if(pkt)
					} else { //ugyldig dag
					System.out.println("Dagen innskrevet er ugyldig. Må være mellom 1-31.");
					} //if(dag)
				} else { //ikke korrekt lengde på datoen innskrevet
					System.out.println("Datoen skrevet inn er ikke korrekt.");
				} //if(dato)
			} catch (StringIndexOutOfBoundsException e) { //for kort tekst er skrevet inn
				System.out.println("Datoen skrevet inn er ikke korrekt.");
			} catch (NumberFormatException e) { //klarte ikke omgjøring fra String til heltall
				System.out.println("Datoen må bestå tall (01-31) og punktum som skilletegn.");
			} catch (Exception e) { //oppsto en (uventet) feil
				System.out.println("En feil oppsto, vennligst prøv igjen.");
			} //try/catch
		} while(!ok);
		return dato; //returner dato
	} //oppgiDato	
	public void finnDyr() {
		String idDyr;
		char dyr;
		System.out.println("Skriv inn dyrets id:");
		idDyr = scan.next().toUpperCase(); //les input
		dyr = idDyr.charAt(0); //sjekk første bokstav
		if (dyr == 'H') { //er dyret en hare...
			kontroll.sokPaaHare(idDyr);
		} else { //...eller en gaupe?
			kontroll.sokPaaGaupe(idDyr);
		} //if
	} //finnDyr
	public void finnFangstAar() {
		String Aar;
		boolean sokeAar = false;
		do { //kjøres minst en gang		
		System.out.println("Skriv inn gjenfangståret:");
		Aar = scan.next(); //les input
		if (Aar.length() > 3 && Aar.length() < 5) { //er lengden på året innskrevet korrekt?
			sokeAar = true;
			kontroll.antGjenfangst(Aar);
		} else { //feil lengde
			System.out.println("Året det søkes på må være 4 tegn.");
		}//if
		} while(!sokeAar); //kjøres så lenge året det søkes på er ukorrekt
	} //finnFangstAar
} //klassen Meny

class FangstKontroll {
	//Tekstfiler hvor informasjonen lagres
	private final String HAREFIL = "HareFangst.txt";
	private final String HAREGJENFANGST = "HareGjenfangst.txt";
	private final String GAUPEFIL = "GaupeFangst.txt";
	private final String GAUPEGJENFANGST = "GaupeGjenfangst.txt";
	//ArrayList hvor dyrene legges inn under kjøring	
	ArrayList <Hare> nyHare = new ArrayList<Hare>();
	ArrayList <Gaupe> nyGaupe = new ArrayList<Gaupe>();
	ArrayList <Gjenfangst> fangetHare = new ArrayList<Gjenfangst>();
	ArrayList <Gjenfangst> fangetGaupe = new ArrayList<Gjenfangst>();
	/***********METODER FOR HARE*************/
	public String lagHareId() { // metode for å lage en id for gaupe
		String id;
		int j = 0; //initialiserer variabelen
		j = nyHare.size(); //sjekker strl på ArrayList
		j++; //øker j
		id = "H" + j; //lager id til dyret
		return id; //returner id'n
	} //lagHareId
	public void opprettNyHare(String idDyr,char kjonn, double lengde, double vekt, 
			char hareType, String farge, String sted, String dato) {
		//lager et nytt objekt av klassen Hare
		Hare regNyHare = new Hare(idDyr, kjonn, lengde, vekt, hareType, 
					farge, sted, dato);
		nyHare.add(regNyHare); //legger gaupen inn i ArrayList
	} //opprettNyHare
	public void opprettNyGjenfangstHare(String idDyr, double lengde, double vekt, 
			String farge, String sted, String dato){
		//oppretter nytt objekt av klassen Gjenfangst
		Gjenfangst fangHare = new Gjenfangst(idDyr, lengde, vekt, 
				farge, sted, dato);
		fangetHare.add(fangHare); //legger gjenfangst inn i ArrayList
	} //opprettNyGjenfangstHare
	public void antHarer() {
		int antVanlig = 0;
		int antSor = 0;
		for (int i = 0; i < nyHare.size(); i++){			
			if (nyHare.get(i).getHareType() == ('V')){ //er det vanlig hare...			
				antVanlig ++; //øk antall vanlige
			} else { //...eller sør-hare?
				antSor++; //øk antall sør-harer
			} //if
		} //for-løkka
		//skriv ut resultatet
		System.out.println("Antall vanlige harer: " + antVanlig);
		System.out.println("Antall sør-harer: " + antSor);
		System.out.println(); //skriver ut blank linje
	} //antHarer
	public int sokHare(String idDyr){		
		int j;
		for(int i = 0; i < nyHare.size(); i++) {
			if(idDyr.toUpperCase().equals(nyHare.get(i).getId())) { //er søk lik id i ArrayList?
				j = i;
				return j; //returner posisjon
			} //if
		} //for		
		return -1; //dyr ikke funnet
	} //sokHare
	public void sokPaaHare(String idDyr){
		int j = sokHare(idDyr);  //kaller på sokemetoden 
		int teller = 0; //teller antall "bomskudd" under søk på gjenfangst		
		if (j >= 0) { //sjekker verdien fra sokHare, hvis j er 0 eller større...
			System.out.println("Id: " + nyHare.get(j).getId());
			System.out.println("Kjønn: " + nyHare.get(j).getKjonn());
			System.out.println("Lengde: " + nyHare.get(j).getLengde());
			System.out.println("Vekt: " + nyHare.get(j).getVekt());
			System.out.println("Farge: " + nyHare.get(j).getFarge());
			System.out.println("Haretype: " + nyHare.get(j).getHareType());
			System.out.println("Sted: " + nyHare.get(j).getSted());
			System.out.println("Dato: " + nyHare.get(j).getDato());	
			System.out.println();  //skriver ut blank linje				 
			for (int i = 0; i < fangetHare.size(); i++) { //løkke som går gjennom hele ArrayList
				if (nyHare.get(j).getId().equals(fangetHare.get(i).getId())){
					//sjekker om id'n fra nyGaupe også er i gjenfangst...
					System.out.println("Gjenfangst:"); //...skriv ut gjenfangsten(e)
					System.out.println("Farge: " + fangetHare.get(i).getFarge());
					System.out.println("Lengde: " + fangetHare.get(i).getLengde());
					System.out.println("Vekt: " + fangetHare.get(i).getVekt());
					System.out.println("Sted: " + fangetHare.get(i).getSted());
					System.out.println("Dato: " + fangetHare.get(i).getDato());
					System.out.println();  //skriver ut blank linje	
				} else { //ingen gjenfangst				
				teller++; //øk teller
				} //if
			} //for-løkka
			if (teller == fangetHare.size()) { //er teller lik gjenfangst-tabellen? 
				System.out.println("Ingen gjenfangst er registrert.");	
				System.out.println();  //skriver ut blank linje	
			} //if (teller)
		} else { //ingen treff på id, gi tilbakemelding	
			System.out.println("Fant ikke dyret. Id'n du søkte på var: " + idDyr.toUpperCase());
		} //if (j >= 0)
	} //sokPaaHare	
	public boolean lesHareFraFil() {
		boolean sjekk = true;		
		StringTokenizer innhold;
		try {			
		//starter å lese fra disk
		FileReader lesFil = new FileReader(HAREFIL);
		//legger informasjonen i buffer for hurtigere tilgang
		BufferedReader leser = new BufferedReader(lesFil);		
		String filInnhold;		
		while(leser.ready()) {	//kjør løkken til filen tar slutt		
			filInnhold = leser.readLine();	//leser hver linje i filen		
			innhold = new StringTokenizer(filInnhold);
			while (innhold.hasMoreTokens()) {			
				String idDyr = innhold.nextToken(" ");
				String kjonn = innhold.nextToken();
				String lengde = innhold.nextToken();				
				String vekt = innhold.nextToken();				
				String hareType = innhold.nextToken();				
				String farge = innhold.nextToken();				
				String sted = innhold.nextToken();
				String dato = innhold.nextToken();				
				//omgjør variablene til "korrekt" datatype
				char k = Character.toUpperCase(kjonn.charAt(0)); //omgjør til char
				char hType  = Character.toUpperCase(hareType.charAt(0)); //omgjør til char
				double l  = Double.parseDouble(lengde); //omgjør til double
				double v = Double.parseDouble(vekt); //omgjør til double
				opprettNyHare(idDyr, k, l, v, hType, farge, sted, dato); //legg inn i ArrayList				
			} //indre løkke
		} //ytre løkke		
		leser.close();//lukker lesningen
		} catch (NoSuchElementException e) { //oppstår hvis det ikke er noen flere linjer i filen			
		} catch (FileNotFoundException e) { //fant ikke filen
			System.out.println("Fant ikke filen: " + HAREFIL);
		} catch (Exception e) {
			System.out.println(e);
			sjekk = false;
		} //try/catch
		return sjekk;
	} //lesHareFraFil
	public boolean lesHareGjenfangst() {
		boolean sjekk = true;		
		StringTokenizer innhold;
		String filInnhold;
		try {			
		//starter å lese fra disk
		FileReader lesFil = new FileReader(HAREGJENFANGST);
		//legger informasjonen i buffer for hurtigere tilgang
		BufferedReader leser = new BufferedReader(lesFil);				
		while(leser.ready()) {	//kjør løkken til filen tar slutt		
			filInnhold = leser.readLine(); //les en og en linje
			innhold = new StringTokenizer(filInnhold);
			while (innhold.hasMoreTokens()) {			
				String idDyr = innhold.nextToken(" ");
				String lengde = innhold.nextToken();				
				String vekt = innhold.nextToken();					
				String farge = innhold.nextToken();				
				String sted = innhold.nextToken();
				String dato = innhold.nextToken();				
				//omgjør variablene til "korrekt" datatype
				double l  = Double.parseDouble(lengde); // omgjør til double
				double v = Double.parseDouble(vekt); // omgjør til double
				opprettNyGjenfangstHare(idDyr, l, v, farge, sted, dato); //legg inn i ArrayList				
			} //indre løkke
		} //ytre løkke
		leser.close();	//lukker lesningen
		} catch (NoSuchElementException e) { //oppstår hvis det ikke er noen flere linjer i filen
		} catch (FileNotFoundException e) { //fant ikke filen
			System.out.println("Fant ikke filen: " + HAREGJENFANGST);
		} catch (Exception e) {
			System.out.println(e);
			sjekk = false;
		} // try/catch
		return sjekk;
	} //lesHareGjenfangst
	public boolean skrivHareTilFil() {
		boolean sjekk = true;
		int teller = 0;		
		try {
		//klargjør for skriving til fil			
		FileWriter skrivFil = new FileWriter(HAREFIL,true);
		PrintWriter skriver = new PrintWriter(new BufferedWriter(skrivFil));
		while((teller < nyHare.size()) && nyHare.size() != 0){
			//opprett en variabel for idDyr som skal sjekkes
			String idDyr = nyHare.get(teller).getId();
			if(!sjekkDyreID(idDyr,HAREFIL)){ //er haren skrevet til fil?
				//skriver alt på en linje inn i tekstfil med (blanke) mellomrom
				skriver.println(nyHare.get(teller).getId() + " " 
				+ nyHare.get(teller).getKjonn() + " " 
				+ nyHare.get(teller).getLengde().toString() + " " 
				+ nyHare.get(teller).getVekt().toString() + " " 
				+ nyHare.get(teller).getHareType() + " " 
				+ nyHare.get(teller).getFarge() + " " 
				+ nyHare.get(teller).getSted() + " "
				+ nyHare.get(teller).getDato());			
			} //if		
		teller++; //øk teller
		} //løkke
		skriver.close(); //lukk skriver
		} catch (Exception e) { //noe gikk galt			
			sjekk = false;
		} //try/catch
		return sjekk;
	} //skrivHareTilFil
	public boolean skrivHareGjenfangst(){
		boolean sjekk = true;
		int teller = 0;
		try {
		//klargjør for skriving til fil			
		FileWriter skrivFil = new FileWriter(HAREGJENFANGST,true);
		PrintWriter skriver = new PrintWriter(new BufferedWriter(skrivFil));
		while((teller < fangetHare.size()) && fangetHare.size() != 0){
			String dato = fangetHare.get(teller).getDato();
			if(!sjekkDato(dato,HAREGJENFANGST)){ //finnes gjenfangst fra før?
				//skriver alt på en linje inn i tekstfil med (blanke) mellomrom
				skriver.println(fangetHare.get(teller).getId() + " " 
						+ fangetHare.get(teller).getLengde().toString() + " " 
						+ fangetHare.get(teller).getVekt().toString() + " " 
						+ fangetHare.get(teller).getFarge() + " "
						+ fangetHare.get(teller).getSted() + " " 
						+ fangetHare.get(teller).getDato());
			} //if
		teller++; //øk teller
		} //løkka
		skriver.close(); //lukker skriveren
		} catch (Exception e) {
			sjekk = false;
		} //try/catch
		return sjekk;
	} //skrivHareGjenfangst
	/***********METODER FOR GAUPE*************/
	public String lagGaupeId(){ // metode for å lage en id for gaupe
		String id;		
		int j = 0; //initialiserer variabelen
		j = nyGaupe.size(); //sjekker strl på ArrayList
		j++; //øker j
		id = "G" + j; //lager id til dyret
		return id; //returner id'n
	} //lagGaupeId
	public void opprettNyGaupe(String idDyr, char kjonn, double lengde, double vekt, 
			double gaupeOre, String sted, String dato) {
		//lager et nytt objekt av klassen Gaupe
		Gaupe regNyGaupe = new Gaupe(idDyr, kjonn, lengde, vekt, gaupeOre, sted, dato);
		nyGaupe.add(regNyGaupe); //legger gaupen inn i ArrayList
	} //opprettNyGaupe
	public void nyGjenfangstGaupe(String idDyr,double lengde, double vekt,String sted, String dato){
		//oppretter nytt objekt av klassen Gjenfangst
		Gjenfangst fangGaupe = new Gjenfangst(idDyr, lengde, vekt, sted, dato); 
		fangetGaupe.add(fangGaupe); //legger gjenfangst inn i ArrayList
	} //nyGjenfangstGaupe
	public int sokGaupe(String idDyr){		
		int j;
		for(int i = 0; i < nyGaupe.size(); i++) {
			if(idDyr.toUpperCase().equals(nyGaupe.get(i).getId())) { //er søk lik id i ArrayList?
				j = i;
				return j; //returner posisjon
			} //if
		} //for
		return -1; //dyr ikke funnet
	} //sokGaupe
	public void sokPaaGaupe(String idDyr){		
		int j = sokGaupe(idDyr); //kaller på sokemetoden
		int teller = 0; //teller antall "bomskudd" under søk på gjenfangst		
		if (j >= 0){ //sjekker verdien fra sokGaupe, hvis j er 0 eller større...
			System.out.println("Id: " + nyGaupe.get(j).getId());
			System.out.println("Kjønn: " + nyGaupe.get(j).getKjonn());
			System.out.println("Lengde: " + nyGaupe.get(j).getLengde());
			System.out.println("Vekt: " + nyGaupe.get(j).getVekt());
			System.out.println("Øre: " + nyGaupe.get(j).getGaupeOre());
			System.out.println("Sted: " + nyGaupe.get(j).getSted());
			System.out.println("Dato: " + nyGaupe.get(j).getDato());	
			System.out.println(); //skriver ut blank linje	
			for (int i = 0; i < fangetGaupe.size(); i++) { //løkke som går gjennom hele ArrayList
				//sjekker om id'n fra nyGaupe også er i gjenfangst...
				if (nyGaupe.get(j).getId().equals(fangetGaupe.get(i).getId())){					
					System.out.println("Gjenfangst:"); //...skriv ut gjenfangsten(e)
					System.out.println("Dato: " + fangetGaupe.get(i).getDato());
					System.out.println("Lengde: " + fangetGaupe.get(i).getLengde());
					System.out.println("Vekt: " + fangetGaupe.get(i).getVekt());
					System.out.println("Sted: " + fangetGaupe.get(i).getSted());					
					System.out.println();  //skriver ut blank linje	
				} else { //ingen gjenfangst					
					teller++; // øk teller
				} //if
			} //for-løkka			
			if (teller == fangetGaupe.size()) { //er teller lik gjenfangst-tabellen? 
				System.out.println("Ingen gjenfangst er registrert.");	
				System.out.println();  //skriver ut blank linje	
			} //if (teller)
		} else { //ingen treff på id, gi tilbakemelding			
			System.out.println("Fant ikke dyret. Id'n du søkte på var: " 
					+ idDyr.toUpperCase());
			System.out.println(); //skriv ut blank linje
		} //if (j >= 0)
	} //sokPaaGaupe
	public boolean lesGaupeFraFil() {
		boolean sjekk = true;		
		StringTokenizer innhold;
		try {			
		//starter å lese fra disk
		FileReader lesFil = new FileReader(GAUPEFIL);
		//legger informasjonen i buffer for hurtigere tilgang
		BufferedReader leser = new BufferedReader(lesFil);		
		String filInnhold;		
		while(leser.ready()) {	//kjør løkken til filen tar slutt		
			filInnhold = leser.readLine(); //leser hver linje i filen			
			innhold = new StringTokenizer(filInnhold);
			while (innhold.hasMoreTokens()) {			
				String idDyr = innhold.nextToken(" ");
				String kjonn = innhold.nextToken();
				String lengde = innhold.nextToken();				
				String vekt = innhold.nextToken();				
				String gaupeOre = innhold.nextToken();				
				String sted = innhold.nextToken();
				String dato = innhold.nextToken();				
				//omgjør variablene til "korrekt" datatype
				char k = Character.toUpperCase(kjonn.charAt(0)); //omgjør til char
				double l  = Double.parseDouble(lengde); //omgjør til double
				double v = Double.parseDouble(vekt); //omgjør til double
				double g = Double.parseDouble(gaupeOre); //omgjør til double
				opprettNyGaupe(idDyr, k, l, v, g, sted, dato); //legg inn i ArrayList			
			} //indre løkke
		} //ytre løkke		
		leser.close(); //lukker lesningen
		} catch (NoSuchElementException e) { //oppstår hvis det ikke er noen flere linjer i filen
		} catch (FileNotFoundException e) { //fant ikke filen
			System.out.println("Fant ikke filen: " + GAUPEFIL);
		} catch (Exception e) { //noe gikk galt
			System.out.println(e);
			sjekk = false;
		} //try/catch
		return sjekk;
	} //lesGaupeFraFil
	public boolean lesGjenfangstGaupe() {
		boolean sjekk = true;		
		StringTokenizer innhold;
		String filInnhold;
		try {			
		//starter å lese fra disk
		FileReader lesFil = new FileReader(GAUPEGJENFANGST);
		//legger informasjonen i buffer for hurtigere tilgang
		BufferedReader leser = new BufferedReader(lesFil);				
		while(leser.ready()) { //kjør løkken til filen tar slutt			
			filInnhold = leser.readLine(); //les en og en linje	
			innhold = new StringTokenizer(filInnhold);
			while (innhold.hasMoreTokens()) {			
				String idDyr = innhold.nextToken(" ");
				String lengde = innhold.nextToken();				
				String vekt = innhold.nextToken();							
				String sted = innhold.nextToken();
				String dato = innhold.nextToken();				
				//omgjør variablene til "korrekt" datatype
				double l  = Double.parseDouble(lengde); //omgjør til double
				double v = Double.parseDouble(vekt); //omgjør til double
				nyGjenfangstGaupe(idDyr, l, v, sted, dato);	//legg inn i ArrayList			
			} //indre løkke
		} //ytre løkke		
		leser.close();//lukker lesningen
		} catch (NoSuchElementException e) { //oppstår hvis det ikke er noen flere linjer i filen
		} catch (FileNotFoundException e) { //fant ikke filen
			System.out.println("Fant ikke filen: " + GAUPEGJENFANGST);
		} catch (Exception e) { //noe gikk galt
			System.out.println(e);
			sjekk = false;
		} //try/catch
		return sjekk;
	} //lesGjenfangstGaupe
	public boolean skrivGaupeTilFil() {
		boolean sjekk = true;
		int teller = 0;		
		try {
		//klargjør for skriving til fil			
		FileWriter skrivFil = new FileWriter(GAUPEFIL,true);
		PrintWriter skriver = new PrintWriter(new BufferedWriter(skrivFil));
		while((teller < nyGaupe.size()) && nyGaupe.size() != 0){
			//opprett en variabel for idDyr som skal sjekkes
			String idDyr = nyGaupe.get(teller).getId();
			//sjekk om id'n fins i filen
			if(!sjekkDyreID(idDyr,GAUPEFIL)){//finnes dyret fra før?
				//skriver alt på en linje inn i tekstfil med (blanke) mellomrom
				skriver.println(nyGaupe.get(teller).getId() + " " 
				+ nyGaupe.get(teller).getKjonn() + " " 
				+ nyGaupe.get(teller).getLengde().toString() + " " 
				+ nyGaupe.get(teller).getVekt().toString() + " "  
				+ nyGaupe.get(teller).getGaupeOre() + " " 
				+ nyGaupe.get(teller).getSted() + " " 
				+ nyGaupe.get(teller).getDato());			
			} //if		
		teller++; //øk teller
		} //løkka
		skriver.close(); //lukk skriver
		} catch (Exception e) { //noe gikk galt			
			sjekk = false;
		} //try/catch
		return sjekk;
	} //skrivGaupeTilFil	
	public boolean skrivGaupeGjenfangst(){
		boolean sjekk = true;
		int teller = 0;
		try {
		//klargjør for skriving til fil			
		FileWriter skrivFil = new FileWriter(GAUPEGJENFANGST,true);
		PrintWriter skriver = new PrintWriter(new BufferedWriter(skrivFil));
		while((teller < fangetGaupe.size()) && fangetGaupe.size() != 0){
			String dato = fangetGaupe.get(teller).getDato();
			if(!sjekkDato(dato,GAUPEGJENFANGST)){ //finnes datoen fra før?
				//skriver alt på en linje inn i tekstfil med (blanke) mellomrom
				skriver.println(fangetGaupe.get(teller).getId() + " " 
						+ fangetGaupe.get(teller).getLengde().toString() + " " 
						+ fangetGaupe.get(teller).getVekt().toString() + " " 
						+ fangetGaupe.get(teller).getSted() + " " 
						+ fangetGaupe.get(teller).getDato());
			}
		teller++; //øk teller
		} //while
		skriver.close(); // lukk skriver
		} catch (Exception e) { //noe gikk galt
			sjekk = false;
		} //try/catch
		return sjekk;
	} //skrivGaupeGjenfangst
	/****POLYMORFENDE METODER****(BRUKES AV BEGGE KLASSENE)*****/
 	public void antGjenfangst(String sokeAar) {
		int antHarer = 0; //teller for harer
		int antGauper = 0; //teller for gauper
		for (int i = 0; i < fangetHare.size(); i++) {
			String fangstAar = fangetHare.get(i).getDato();
			//årstall har plassering 6 og ut i dato
			if (sokeAar.equals(fangstAar.substring(6))) {//sjekk om søke-år er lik år i tabell
				antHarer++; //øk antall harer
			} //if			
		} //for
		for (int i = 0; i < fangetGaupe.size(); i++) {
			String fangstAar = fangetGaupe.get(i).getDato();
			//årstall har plassering 6 og ut i dato
			if (sokeAar.equals(fangstAar.substring(6))) {//sjekk om søke-år er lik år i tabell
				antGauper++; //øk antall gauper
			} //if
		} //for
			if (antHarer > 0 || antGauper > 0) { //Sjekk om antall fanget er større enn 0...
				System.out.println("Antall harer fanget i " + sokeAar + " er: " + antHarer);
				System.out.println("Antall gauper fanget i " + sokeAar + " er: " + antGauper);
				System.out.println();
			} else { //...ingen fanget, gi tilbakemelding
				System.out.println("Ingen dyr fanget i " + sokeAar + ".");
				System.out.println(); //skriver ut blank linje
			} //if(antfanget)
	} //antGjenfangst
	public void lagRapport() {
		int teller = 1; //nummerering for dyra
		//kjører gjennom løkka og skriver ut alt fra ArrayList
		for (int i = 0; i < nyHare.size(); i++) {
			System.out.println("Hare nr " + teller + "   " + nyHare.get(i).getHareType() + "   " 
				+ nyHare.get(i).getKjonn() + "   " + nyHare.get(i).getSted() + "   "
				+ nyHare.get(i).getDato() + "   " + nyHare.get(i).getVekt() + " kg   "
				+ nyHare.get(i).getLengde() + " cm   " + nyHare.get(i).getFarge());
			teller++;//øk teller
		} //for				
		teller = 1; //"nullstill" teller			
		System.out.println(); // legg inn blankt mellomrom for økt lesbarhet	
		//kjører gjennom løkka og skriver ut alt fra ArrayList
		for(int j = 0; j < nyGaupe.size(); j++){
			System.out.println("Gaupe nr " + teller + "   " + nyGaupe.get(j).getKjonn() + "   "
				+ nyGaupe.get(j).getSted() + "   " + nyGaupe.get(j).getDato() + "   " 
				+ nyGaupe.get(j).getVekt() + " kg   " + nyGaupe.get(j).getLengde() + " cm");			
			teller++;//øk teller
		} //for	
	System.out.println(); // legg inn blankt mellomrom for økt lesbarhet
	}//lagRapport
 	/* Disse 2 sjekk metodene, sjekkDyreId og sjekkDato
 	 * leser gjennom tekstfilen og sjekker
 	 * om det som skal skrives inn ikke eksisterer 
 	 * (siden tabellen fylles med det som ligger på fil)
 	 * Dette for å unngå dobbellagring/redundans */
 	public boolean sjekkDyreID(String idDyr, String filnavn){
		StringTokenizer innhold;
		String filInnhold;
		int teller = 0;
		boolean funnet = false;
		try {			
		//starter å lese fra disk
		FileReader lesFil = new FileReader(filnavn);
		//legger informasjonen i buffer for hurtigere tilgang
		BufferedReader leser = new BufferedReader(lesFil);
		//kjør løkken til filen tar slutt
		while(leser.ready()) {			
			filInnhold = leser.readLine();			
			innhold = new StringTokenizer(filInnhold);
			//så lenge filen har mer innhold
			while (innhold.hasMoreTokens()) {
				//leser hver linje
				 String id = innhold.nextToken(" ");				 
				if (id.equals(idDyr)){//sjekker om linjen er lik id'n som skal skrives til fil
					funnet = true;
				} //if
				teller++;
			} //indre løkke
		} //ytre løkke				
		leser.close(); //lukker lesningen
		} catch (Exception e) {
			System.out.println(e);
		}
		return funnet;
	}	
	public boolean sjekkDato(String dato, String filnavn){		
		StringTokenizer innhold;
		String filInnhold;
		int teller = 0;
		boolean funnet = false;
		try {			
		//starter å lese fra disk
		FileReader lesFil = new FileReader(filnavn);
		//legger informasjonen i buffer for hurtigere tilgang
		BufferedReader leser = new BufferedReader(lesFil);
		//kjør løkken til filen tar slutt
		while(leser.ready()) {			
			filInnhold = leser.readLine();			
			innhold = new StringTokenizer(filInnhold);
			//så lenge filen har mer innhold
			while (innhold.hasMoreTokens()) {
				//leser hver linje
				 String d = innhold.nextToken(" ");				 
				if (d.equals(dato)){//sjekker om linjen er lik id'n som skal skrives til fil
					funnet = true;
				} //if	
				teller++;
			} //indre løkke
		} //ytre løkke		
		//lukker lesningen
		leser.close(); //lukker lesningen
		} catch (Exception e) {
			System.out.println(e);
		} //try/catch
		return funnet;
	} //sjekkDato
} //klassen Fangstkontroll

class Gjenfangst extends Dyr {
	private String farge;	 
	//konstruktør for gjenfanget hare
	public Gjenfangst(String idDyr, double lengde, double vekt, 
			String farge, String sted, String dato) {		
		super(idDyr, lengde, vekt, sted, dato);
		this.farge = farge;	
	}
	//konstruktør for gjenfanget gaupe
	public Gjenfangst(String idDyr,double lengde, double vekt, 
			String sted, String dato) {
		//kaller på verdiene og funksjonene fra klassen Dyr
		super(idDyr, lengde, vekt, sted, dato);
	}
	public String getFarge() {
		return farge;
	}
} //klassen Gjenfangst
class Hare extends Dyr {
	private char hareType;
	private String farge;
	//konstruktør
	public Hare(String idDyr,char kjonn, double lengde, double vekt, 
			char hareType, String farge, String sted, String dato) {
		//kaller på verdiene og funksjonene fra klassen Dyr
		super(idDyr, kjonn, lengde, vekt, sted, dato);
		this.hareType = hareType;
		this.farge = farge;
	}
	//GET-FUNKSJONER
	public char getHareType() {
		return hareType;
	}
	public String getFarge() {
		return farge;
	}
} //klassen Hare
class Gaupe extends Dyr {
	private double gaupeOre;
	//konstruktør
	public Gaupe(String idDyr,char kjonn, double lengde, double vekt, 
			double gaupeOre, String sted, String dato) {
		//kaller på verdiene og funksjonene fra klassen Dyr
		super(idDyr, kjonn, lengde, vekt, sted, dato);
		this.gaupeOre = gaupeOre;
	}
	//GET-FUNKSJON
	public double getGaupeOre() {
		return gaupeOre;
	}
} //klassen Gaupe
class Dyr {
	private String idDyr;
	private char kjonn;
	private double lengde;
	private double vekt;
	private String sted;
	private String dato;
	//konstruktør som oppretter felles verdier for alle dyr
	public Dyr (String idDyr,char kjonn, double lengde, 
				double vekt, String sted, String dato) {	
		this.idDyr = idDyr;
		this.kjonn = kjonn;
		this.lengde = lengde;
		this.vekt = vekt;
		this.sted = sted;
		this.dato = dato;		
	}
	//konstruktør som oppretter felles verdier klassen gjenfangst
	public Dyr (String idDyr, double lengde, 
				double vekt, String sted, String dato) {	
		this.idDyr = idDyr;
		this.lengde = lengde;
		this.vekt = vekt;
		this.sted = sted;
		this.dato = dato;		
	}			
	//GET-FUNKSJONER
	public String getId(){
		return idDyr;
	}
	public char getKjonn(){
		return kjonn;
	}	
	public Double getLengde(){
		return lengde;
	}	
	public Double getVekt(){
		return vekt;
	}	
	public String getSted(){
		return sted;
	}	
	public String getDato(){
		return dato;
	}
} //klassen Dyr