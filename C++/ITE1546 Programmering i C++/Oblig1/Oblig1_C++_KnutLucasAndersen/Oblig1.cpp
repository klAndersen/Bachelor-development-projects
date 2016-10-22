/**
Dette er et program skrevet i C++ basert på obligatorisk oppgave 1 
i faget ITE1546 - Programmering i C++ som baserte seg på å lage et 
menybasert system som beregner forskjellige tallmengder med en 
tilhørende ekstra oppgave for å konvertere en tallverdi mellom 
0 - 1000 fra heltall til tekst.
----------------------------------------------------------------
@Author: Knut Lucas Andersen
@Date: 25.01.2013
@Place: Narvik
*/

#include <iostream> //nødvendig for bruk av cin og cout
#include <iomanip> //string manipulasjon
#include <string> //bruk av string
#include <sstream> //bruk av konvertering fra tall til string
#include <cmath> //matematiske operasjoner og operatorer
#define PI 3.14159 //definering av "PI"
using namespace std;

//deklarering av funksjonsprototyper
void visMeny();
void hentValgtFunksjon(int);
void beregnKuleVolum();
void beregnAnnengradslikning();
void beregnGeografiskAvstand();
void beregnNedbetalingsplan();
void skrivUtNedbetalingsplan(double, double, double, double);
void skrivUtKonvertertTallTilTekst();
string hentKonvertTallString(string, string[], int);
string konverterTallTilTekst(string tallTilString, int antSiffer, int antSubstrTall, int posisjon, string arrayTalltekst[]);
string returnerTiTallKonvertertTilTekst(string tallTilString, int posisjon, string arrayTalltekst[]);

int main() {
	//settes til -1 pga 0 avslutter programmet
	int menyValg = -1;
	//løkke som kjører så lenge brukers input ikke er null
	//null avslutter programmet
	while (menyValg != 0) {
		//skriv ut/vis menyvalg til bruker
		visMeny();		
		//hva ønsker bruker?
		cin >> menyValg;
		//kontroller at bruker skrev inn et tall
		if (cin.fail()) {
			//bruker skrev ikke inn et tall, sett cin fra fail til good status
			//(klar for ny input)
			cin.clear();
			//tøm cin for innhold
			cin.ignore();
			//vis feilmelding til bruker
			cout << "\nDu skrev inn en ugyldig verdi. Gyldig verdi er et tall mellom 0 - 5." << endl << endl;
		} else {
			//tall ble skrevet inn
			hentValgtFunksjon(menyValg);
		} //if(cin.fail())
	} //while

	return 0;
} //main

void visMeny() {
	string meny = "**** HOVEDMENY ****\n";
	meny += "1: Beregn kulevolum\n";
	meny += "2: Beregn annengradslikning\n" ;
	meny += "3: Beregn avstand mellom to geografiske punkter\n";
	meny += "4: Beregn nedbetalingsplan\n";
	meny += "5: Konverter tall til tekst\n";
	meny += "0: Avslutt programmet\n";
	cout << meny << endl;
	cout << "Skriv inn menyvalg alternativ (0-5): ";
} //visMeny

void hentValgtFunksjon(int menyValg) {
	switch (menyValg) {
	case 0:
		//siden null avslutter programmet, stopp her
		break;
	case 1:
		//beregn volumet til en kule basert på oppgitt radius
		beregnKuleVolum();
		break;
	case 2:
		//beren en annengradslikning basert på oppgitte verdier
		beregnAnnengradslikning();
		break;
	case 3:
		//beregn geografisk avstand basert på 
		//oppgitte verdier og Haversines formel
		beregnGeografiskAvstand();
		break;
	case 4:
		//beregn og skriv ut en nedbetalingsplan 
		//for et annuitetslån basert på oppgitte verdier
		beregnNedbetalingsplan();
		break;
	case 5:
		//konverter oppgitt tall til tekst
		//eks: 1000 gir utskriften "EtTusen"
		skrivUtKonvertertTallTilTekst();
		break;
	default:
		//bruker skrev inn en verdi utenfor gyldig rekkevidde (0-5), gi feilmelding
		//dobbel endl for å få luft mellom feilmelding og menytekst
		cout << "\nDu skrev inn " << menyValg << ". Gyldig verdi er et tall mellom 0 - 5." << endl << endl;
		break;
	} //switch
} //hentValgtFunksjon

void beregnKuleVolum() {	
	double radius = 0;
	double volum = 0;
	//spør bruker etter radius
	cout << "\nOppgi kulens radius: ";
	//les brukers input
	cin >> radius;
	//ble numerisk skrevet inn?
	if (cin.fail()) {
		//ikke numerisk verdi, sett cin fra fail til good status
		//(klar for ny input)
		cin.clear();
		//tøm cin for innhold
		//http://www.cplusplus.com/reference/istream/istream/ignore/
		cin.ignore();
		cout << "Ugyldig verdi oppgitt.\nRadius kan kun best" << (char)134 << " av siffer (0-9) og punktum (.) som skilletegn." << endl << endl;
	} else {
		//numerisk verdi innskrevet, beregn volum
		//formel: 4PIr^3 / 3
		volum = (4*PI*(pow(radius, 3))) / (3);
		//vis resultat til bruker
		cout << "Kulens volum er " << volum << "!" << endl << endl;
	} //if (cin.fail()) 
} //beregnKuleVolum

void beregnAnnengradslikning() {
	double a, b, c;	
	cout << "\nOppgi verdi for a, b og c, adskilt med mellomrom (eksempel: 1 3 9): ";
	cin >> a >> b >> c;
	if (cin.fail()) {
		cin.clear();
		//kunne her skrevet cin.ignore(6), men da risikerer man å måtte trykke enter x ganger
		//dersom f.eks. første tegn er et tall...
		//fant derfor ut at det heller var bedre at menyen ble skrivd ut 
		//en gang for mye med feilmelding om feil alternativ
		cin.ignore();
		//vis feilmelding til bruker
		cout << "\nEn eller flere av verdier innskrevet er ugyldig." << endl;
		cout << "Du m" << (char)134 << " oppgi verdi for a, b og c og verdi m" << (char)134 << "v" << (char)145 << "re et siffer mellom 0-9" << endl << endl;
	} else {
		//alle verdier innskrevet er tall
		double sumKvadratrot, sumPosLikning, sumNegLikning;
		//beregn det som står under kvadratroten
		sumKvadratrot = pow(b, 2) - (4 * a * c);
		//er summen under kvadratrot positiv?
		if (sumKvadratrot > 0) {
			//beregn selve kvadratroten
			sumKvadratrot = sqrt(sumKvadratrot);
			//formel1: ( -b + kvadratrot( b^2 - 4ac ) ) / 2a		
			sumPosLikning = (-b + sumKvadratrot) / (2 * a);
			//formel2: ( -b - kvadratrot( b^2 - 4ac ) ) / 2a
			sumNegLikning = (-b - sumKvadratrot) / (2 * a);
			//vis resulatetene til bruker med formel (hovedsaklig for visning av positiv og negativ sqrt)
			cout << "\nX1) ( -b + kvadratrot( b^2 - 4ac ) ) / (2a) = " << sumPosLikning << endl;
			cout << "\nX2) ( -b - kvadratrot( b^2 - 4ac ) ) / (2a) = " << sumNegLikning << endl << endl;
		} else {
			//minus under kvadratrot (komplekse tall), gi tilbakemelding til bruker
			cout << "\nUtregning resulterer i minus under kvadratrot (" << sumKvadratrot << ")." << endl;
			cout << "Kan ikke regne ut annengradslikningen." << endl << endl;
		} //if (sumKvadratrot > 0) 
	} //if (cin.fail())
} //beregnAnnengradslikning

void beregnGeografiskAvstand() {
	//d = 2r arcsin( sqrt (sin((x2-x1) / 2)^2 + cos(x1)cos(x2)sin((y2-y1) / 2)^2 )
	//http://wordpress.mrreid.org/wp-content/uploads/2011/12/haversine.pdf
	//det kunne her vært bedre navngivning på x og y, men siden disse var grader,
	//fant jeg ikke på noe bedre
	double d, x1, x2, y1, y2, sinX, sinY, cosX;
	double jordasRadius = 6371;
	cout << "\nOppgi grader for omr" << (char)134 << "de 1 adskilt med mellomrom (eks: 46.53 -88.23): " << endl;
	cin >> x1 >> y1;
	//er numeriske verdier oppgitt?
	if (cin.fail()) {
		//ikke numeriske verdier, tøm og klargjør cin
		cin.clear();
		cin.ignore();
		//gi feilmelding
		cout << "\nUgyldig verdi innskrevet. Verdi m" << (char)134 << " best" << (char)134 << " av siffer (0-9) og punktum (.) som skilletegn" << endl << endl;
	} else {
		//numeriske verdier oppgitt, spør etter grader fra det andre området
		cout << "\nOppgi grader for omr" << (char)134 << "de 2 adskilt med mellomrom (eks: 46.53 -88.23): " << endl;
		cin >> x2 >> y2;
		//er numeriske verdier oppgitt?
		if (cin.fail()) {
			//ikke numeriske verdier, tøm og klargjør cin
			cin.clear();
			cin.ignore();
			//gi feilmelding
			cout << "\nUgyldig verdi innskrevet. Verdi m" << (char)134 << " best" << (char)134 << " av siffer (0-9) og punktum (.) som skilletegn" << endl << endl;
		} else {
			//numeriske verdier oppgitt
			//konverter grader til radianer
			x1 = x1 * (PI / 180);			
			x2 = x2 * (PI / 180);
			y1 = y1 * (PI / 180);
			y2 = y2 * (PI / 180);
			//beregn verdier under kvadratroten for første sinus
			//formel: sin((x2-x1) / 2)^2
			sinX = pow(sin((x2 - x1) / 2), 2);
			//beregn verdier under kvadratroten for cosinus
			//formel: cos(x1)cos(x2)
			cosX = cos(x1) * cos(x2);
			//beregn verdier under kvadratroten for den andre sinus
			//formel: sin((y2-y1) / 2)^2
			sinY = pow(sin((y2 - y1) / 2), 2);
			//multipliser sammen cosinus og sinus
			//formel: cos(x1)cos(x2)sin((y2-y1) / 2)^2
			cosX = cosX * sinY;
			//legg sammen den første sinus og resulatet av multiplikasjonen med cosinus
			//formel: sin((x2-x1) / 2)^2 + cos(x1)cos(x2)sin((y2-y1) / 2)^2
			sinX = sinX + cosX;
			//regn ut den geografiske avstanden
			d = 2 * jordasRadius * asin ( sqrt(sinX) );
			//skriv ut resultatet til bruker
			cout << "\nAvstanden mellom omr" << (char)134 << "dene er: " << d << " km." << endl << endl;
		} //if (cin.fail())
	} //if (cin.fail())
} //beregnGeografiskAvstand

void beregnNedbetalingsplan() {
	double startLaan, terminRente, antAar, tempVariabel, terminBelop, renteProsent;
	//skriv ut beskjed/"bruksanvisning" til bruker
	cout << "\nHer kan du beregne en nedbetalingsplan for et annuitetsl" << (char)134 << "n." << endl;
	cout << "Oppgi startl" << (char)134 << "n, rente (i %) og antall " << (char)134 << "r adskilt med mellomrom\n(eksempel: 200000 3.6 12): ";
	//les input fra bruker
	cin >> startLaan >> terminRente >> antAar;
	if (cin.fail()) {
		//ikke numerisk verdi
		cin.clear();
		cin.ignore();
		//feilmelding til bruker
		cout << "Ugyldig verdi innskrevet. Verdier m" << (char)134 << " v" << (char)145 << "re siffer (0-9) og best" << (char)134 << " av punktum (.) som skilletegn." << endl << endl;
	} else {
		//verdier innskrevet er numeriske
		renteProsent = 1 + (terminRente/100);
		//regner ut renteprosenten^antAar 
		//(eks: 1.04^3 hvor renta er 4% og antall år er 3)
		//(kalles tempVariabel siden den kun brukes for mellomregning)
		tempVariabel = pow(renteProsent, antAar);
		//regner ut selve terminbeløpet
		terminBelop = startLaan * ((tempVariabel * (terminRente/100)) / (tempVariabel - 1));
		//regner ut totalt hvor mye en betaler i renter
		//avdragRenter = antAar * terminBelop - startLaan; //DEBUG FORMÅL
		//skriv ut resultatet til bruker 
		skrivUtNedbetalingsplan(startLaan, terminRente, antAar, terminBelop);		
	} //if (cin.fail())
} //beregnNedbetalingsplan

void skrivUtNedbetalingsplan(double startLaan, double terminRente, double antAar, double terminBelop) {
	double avdragRenter, restGjeld, terminAvdrag;
	//luft
	cout << endl;
	//skriver ut tittel/ledetekst/oversikt adskilt med tabulator
	cout << left << setw(10) << "Aar" << left << setw(15) << "Terminbelop";
	cout << left << setw(15) << "Renter" << left << setw(15) << "Avdrag";
	cout << right << "Restgjeld" << endl;
	//linje for "pynt" - hovedsaklig for å gi litt luft for bedre lesbarhet
	cout << "----------------------------------------------------------------" << endl;
	//beregner verdier for det første året
	avdragRenter = (startLaan * terminRente) / 100;
	terminAvdrag = terminBelop - avdragRenter;
	restGjeld = startLaan - terminAvdrag;
	//løkke som kjører like lenge som antall år oppgitt
	for (int aar = 1; aar <= antAar; aar++) {
		//er kun interessert i vise de to første desimaltallene for hvert beløp
		cout << fixed << setprecision(2);
		//skriver ut for ett og ett år de forskjellige beløpene
		cout << left << setw(10) << aar << left << setw(15) << terminBelop;
		cout << left << setw(15) << avdragRenter << left << setw(15) << terminAvdrag;
		cout << right << restGjeld << endl;
		//utregning for neste års avdrag
		avdragRenter = (restGjeld * terminRente) / 100;
		terminAvdrag = terminBelop - avdragRenter;
		restGjeld = restGjeld - terminAvdrag;
	} //for
	//linjeskift for luft
	cout << endl << endl; 
} //skrivUtNedbetalingsplan

void skrivUtKonvertertTallTilTekst() {
	int tallVerdi;
	string utskrift, tallTilString;
	ostringstream konverterTilString;
	//array med tekstverdier for tall som skal konverteres
	string arrayTalltekst[] = {
		"Null",
		"En", "To","Tre","Fire","Fem",
		"Seks","Sju","Aatte","Ni","Ti",
		"Ellve", "Tolv", "Tretten", "Fjorten", 
		"Femten", "Seksten", "Sytten", "Atten", 
		"Nitten", "Tjue", "Tretti", "Foerti", 
		"Femti", "Seksti", "Sytti", "Aatti", 
		"Nitti", "EtHundre", "Hundre", 
		"EtTusen", "Tusen"
	};
	//be bruker om input
	cout << "\nSkriv inn ett heltall mellom 0 - 9999, s" << (char)134 << " vil jeg konvertere det til tekst: " << endl;
	cin >> tallVerdi;
	if (cin.fail()) {
		//ikke heltall innskrevet, tøm cin
		cin.clear();
		cin.ignore();
		//feilmelding
		utskrift = "Verdi innskrevet er ikke et heltall. Kan ikke konvertere verdi til tekst.";
	} else if (tallVerdi > 9999) {
		//konverter verdien her for å få skrivd ut teksten
		//feiler delvis ved bruk av tallverdien
		konverterTilString << tallVerdi;
		tallTilString = konverterTilString.str();
		//tallverdi over gitt mengde, gi "feilmelding"
		utskrift = "Verdi oppgitt (" + tallTilString;
		utskrift += ") er for høy. Kan kun konvertere siffer mellom 0 - 9999";
	} else {
		//numerisk verdi innskrevet, konverter verdien
		konverterTilString << tallVerdi;
		tallTilString = konverterTilString.str();
		//hent ut antall tegn
		int antSiffer = tallTilString.size();
		//vis bruker tallet som ble skrevet inn og hva verdien er som tekst/string
		utskrift = "Tall innskrevet (" + tallTilString + ") konvertert til tekst er: " 
			+ hentKonvertTallString(tallTilString, arrayTalltekst, antSiffer);
	} //if (cin.fail())
	//melding som vises til bruker
	cout << utskrift << endl << endl;
} //skrivUtKonvertertTallTilTekst

string hentKonvertTallString(string tallTilString, string arrayTalltekst[], int antSiffer) {
	//tekst som skal returneres
	string utskrift = "";
	//hvor mange tegn som skal hentes ut fra konvertert string
	int antSubstrTall = 1;
	//looper gjennom alle sifrene som ble skrevet inn
	//-1 pga de to siste sifferene konverteres til ett tall (00-99)
	for (int i = 0; i < antSiffer-1; i++) {
		//hvilket siffer (og posisjon i substring) skal konverteres?
		switch (i) {
		case 0:
			//er det kun to siffer (00-99)?
			if (antSiffer == 2) {
				//sett at begge tegn skal hentes ut
				antSubstrTall = 2;
			} //if (antSiffer == 2)
			//hent resultatet som inneholder gjeldende tall konvertert til tekst
			utskrift = konverterTallTilTekst(tallTilString, antSiffer, antSubstrTall, i, arrayTalltekst);
			break;
		case 1: 
			//er det kun tre siffer (0-999)?
			if (antSiffer == 3) {
				//siden plass 1 tilsvarer titallet, hent ut begge sifferene
				antSubstrTall = 2;
			} //if (antSiffer == 3)
			//hent resultatet som inneholder gjeldende tall konvertert til tekst
			utskrift += konverterTallTilTekst(tallTilString, antSiffer, antSubstrTall, i, arrayTalltekst);
			break;
		case 2:
			//siden det kun er titall igjen på dette punktet, hent ut begge sifferene
			antSubstrTall = 2;
			//hent resultatet som inneholder gjeldende tall konvertert til tekst
			utskrift += konverterTallTilTekst(tallTilString, antSiffer, antSubstrTall, i, arrayTalltekst);
			break;
		} //switch
	} //for
	return utskrift;
} //hentKonvertTallString

/**
Denne funksjonen tar en streng som inneholder tallet innskrevet konvertert til string, 
antall siffer innskrevet for å finne ut hvilken tallverdi som skal skrives ut, hvor mange tegn 
som skal hentes ut fra strengen, posisjonen sifferet skal hentes fra og til slutt array som 
inneholder tekstverdiene for tallmengden
------------------------------------------------------------------------------------------------
Forklaring til variabler:
@tallString: Innskrevet tall konvertert til string
@antSiffer: Antall siffer som er innskrevet (eks: 1000 = 4 siffer)
@antSubstrTall: Antall tegn fra stringen som skal hentes ut
@posisjon: hvilken del av tallString som skal hentes ut og omgjøres til tekst
@arrayTalltekst[]: Array som inneholder tallkonversjonen; (eks: Ti, Tusen, osv)
@return string: String som inneholder en tekst basert på gjeldende tall oversendt
*/
string konverterTallTilTekst(string tallTilString, int antSiffer, int antSubstrTall, int posisjon, string arrayTalltekst[]) {
	int siffer = 0;
	string utskrift = "";
	//hent ut siffer med substring
	istringstream konverterTilSiffer (tallTilString.substr(posisjon, antSubstrTall));
	//konverter string til et heltall
	konverterTilSiffer >> siffer;
	//hvilken tallverdi og posisjon er det snakk om...
	//er tallet tusen?
	if (antSiffer == 4 && posisjon == 0) {
		//er dette tallet null?
		if (siffer == 0) {
			//tallet er null, returner blank utskrift
			utskrift = "";
		} else if (siffer == 1) {
			//tallet er en, hent ut spesifikk verdi ("EtTusen")
			utskrift = arrayTalltekst[30];
		} else {
			//tallet er av en verdi mellom 2xxx - 9xxxx
			utskrift = arrayTalltekst[siffer] + arrayTalltekst[31];
		} //if (siffer == 0)
	//er tallet av verdien hundre?
	} else if ((antSiffer == 3 && posisjon == 0) || antSiffer == 4 && posisjon == 1) {
		//er dette tallet null?
		if (siffer == 0) {
			//tallet er null, returner blank utskrift
			utskrift = "";
		} else if (siffer == 1) {
			//tallet er 1, hent ut spesifikk verdi ("EtHundre")
			utskrift = arrayTalltekst[28];
		} else {
			//tallet er av en verdi mellom 2xx - 9xx
			utskrift = arrayTalltekst[siffer] + arrayTalltekst[29];
		} //if (siffer == 0)
	//er tallet av typen titall?
	} else if ((antSiffer == 2 && posisjon == 0) || (antSiffer == 3 && posisjon == 1) || antSiffer == 4 && posisjon == 2) {
		//er dette tallet null?
		if (siffer == 0) {
			//sett utskrift til blank tekst
			utskrift = "";
		} else if (siffer < 21) {
			//er det kun et titall som er skrevet inn (00-99)?
			if (antSiffer > 2) {
				//verdi innskrevet > 99, legg til "Og" (Eks: "EtHundreOgNitten")
				utskrift = "Og";
			} //if (antSiffer > 2) 
			//tallet er mellom 1-20, hent ut verdi fra array
			utskrift += arrayTalltekst[siffer];
		} else {
			//er det kun et titall som er skrevet inn (00-99)?
			if (antSiffer > 2) {
				//verdi innskrevet > 99, legg til "Og" (Eks: "EtHundreOgNitten")
				utskrift = "Og";
			} //if (antSiffer > 2) 
			//siden tallet er >= 21, hent ut konvertert tekst fra egen funksjon
			utskrift += returnerTiTallKonvertertTilTekst(tallTilString, posisjon, arrayTalltekst);
		} //if (siffer == 0)
	} //if (antSiffer == 4 && posisjon == 0)
	return utskrift;
} //konverterTallTilTekst

/**
Siden innholdet i denne funksjonen tar en del plass (hovedsaklig switch for å finne verdien for ti-tallet), 
så valgte jeg å lage denne for seg selv. Den tar en streng som inneholder konvertert tallverdi, posisjonen 
som tallet/ene skal hentes ut fra og arrayen som inneholder tallenes tekstverdi.
----------------------------------------------------------------------------------------------------------
@tallTilString: 
@posisjon: 
@arrayTallTekst: 
@return string: 
*/
string returnerTiTallKonvertertTilTekst(string tallTilString, int posisjon, string arrayTalltekst[]) {
	string utskrift;	
	int siffer1, siffer2;
	//hent ut titallet fra konvertert string
	istringstream konverterTilSiffer (tallTilString.substr(posisjon, 1));
	//konverter string til et heltall
	konverterTilSiffer >> siffer1;
	//hent ut det andre sifferet (0) og øk posisjon med en, siden 
	//det er det siste tallet en skal ha tak i
	istringstream konverterTilSiffer2 (tallTilString.substr(posisjon + 1, 1));
	//konverter string til et heltall
	konverterTilSiffer2 >> siffer2;
	//hent ut verdien for titallet via switch som kombinerer titall og etterfølgende tall
	switch(siffer1) {
	case 2:
		utskrift = arrayTalltekst[20];
		break;
	case 3:
		utskrift = arrayTalltekst[21];
		break;
	case 4:
		utskrift = arrayTalltekst[22];
		break;
	case 5:
		utskrift = arrayTalltekst[23];
		break;
	case 6: 
		utskrift = arrayTalltekst[24];
		break;
	case 7:
		utskrift = arrayTalltekst[25];
		break;
	case 8:
		utskrift = arrayTalltekst[26];
		break;
	case 9:
		utskrift = arrayTalltekst[27];
		break;
	} //switch
	//er det siste sifferet større enn null?
	if (siffer2 > 0) {
		//større enn null, legg til det siste tallet bakerst
		 utskrift += arrayTalltekst[siffer2];
	} //if (siffer2 > 0) {
	return utskrift;
} //returnerTiTallKonvertertTilTekst