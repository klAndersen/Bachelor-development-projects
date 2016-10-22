/**
Dette er et program skrevet i C++ basert på obligatorisk oppgave 2 
i faget ITE1546 - Programmering i C++.
Oppgaven baserte seg på å lage et menybasert billettsystem til en kino.
Programmet skal kun registrere en forestilling, vise og selge ledige seter, 
vise inntekter til forestilling og registrere en ny forestilling.
Data skal lagres i en tekstfil, men dersom ny forestilling opprettes, 
slettes alle tidligere data.
----------------------------------------------------------------
@Author: Knut Lucas Andersen
@Date: 27.02.2013
@Place: Narvik
*/
#include <iostream>
#include <fstream>
#include <string>
#include <iomanip>
#include <sstream>
using namespace std;

#pragma region FUNKSJONSPROTOTYPER
//lesing fra fil
void lesFraFil();
void hentForestillingFraFil(ifstream &innfil);
void hentInntektFraFil(ifstream &innfil);
int antallLinjerITekstfil(ifstream &innfil);
//meny
void visMeny();
void hentValgtFunksjon(int menyValg);
//opprett ny forestilling
void nyForestilling();
string navngiForestilling();
void skrivTilFil(bool nullstill);
//billettsalg
void selgBilletter();
int oppgiAntallBilletterRedusertpris();
int oppgiAntallBilletterFullpris();
int *oppgiRadOgSeteNr(int antBilletter);
bool erSeterLedig(int antallBilletter, int radNr, int seteNr);
char oppsummeringSalg(int radNr, int seteNr, int antallBilletter, double totalPris);
void lagreBillettSalg(int radNr, int seteNr, int antallBilletter);
//ledige seter
void visLedigeSeter();
//oppsummering salg for eksisterende forestilling
void visBilettInntekter();
//konvertering til string
string konverterIntegerTilString(int tall);
string konverterDoubleTilString(double tall);
//konvertering til tall
double konverterStringTilDouble(string tall);
#pragma endregion

#pragma region KONSTANTER
//navn på fil som inneholder forestilling
const string FORESTILLINGSFIL = "Forestillling.txt";
const string INNTEKTSFIL = "Inntekt.txt";
//antall rader i salen forestilling skal vises
const int ANTALL_RADER = 15;
//antall seter pr rad i salen forestilling skal vises
const int ANTALL_SETER_PR_RAD = 30;
//priser for forestilling
const double FULL_PRIS = 90;
const double REDUSERT_PRIS = 60;
//sete ledig/ikke ledig
const char LEDIG_SETE = '*';
const char OPPTATT_SETE = '#';
#pragma endregion

#pragma region VARIABLER
//to-dimensjonal array som inneholder setene
char kinoSeter[ANTALL_RADER][ANTALL_SETER_PR_RAD];
double inntektArray[ANTALL_RADER];
//variabel for forestillingens navn med default tekst 
string forestillingsNavn = "Ingen forestilling registrert";
//boolsk verdi som forteller om forestilling finnes
//brukes for kontroll ved utskrift av resultater tilknyttet forestilling
bool finnesForestilling = false;
#pragma endregion 

int main() {
	//settes til -1 pga 0 avslutter programmet
	int menyValg = -1;
	//løkke som kjører så lenge brukers input ikke er null
	//null avslutter programmet
	lesFraFil();
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
			cout << "\nDu skrev inn et ugyldig valg. Gyldig valg er et tall mellom 0 - 4." << endl << endl;
		} else {
			//tall ble skrevet inn
			hentValgtFunksjon(menyValg);
		} //if(cin.fail())
	} //while
	return 0;
} //main

#pragma region LES & BEHANDLE FILINNHOLD

/**
Det første denne funksjonen gjør er å opprette en tekstfil, 
og hvis tekstfil finnes, så gjør den intet. 
Dette er i hovedsak for å unngå feilmelding 
tilknyttet at fil ikke finnes og dermed feilmelding om at 
fil ikke kan åpnes.	Dersom fil finnes leses innholdet.
*/
void lesFraFil() {
	ifstream innfilForestilling, innfilInntekt;
	//for å unngå feil tilknyttet til at fil ikke eksisterer
	//opprett fil; og hvis fil eksisterer, legg til flagg med append
	//som gjør at innhold ikke slettes 
	//(hovedsaklig for at bruker skal slippe å opprette tekstfilen)
	ofstream opprettFil (FORESTILLINGSFIL, ios::app);
	ofstream opprettFil2 (INNTEKTSFIL, ios::app);
	//lukk "filbehandlingen"
	opprettFil.close();
	opprettFil2.close();
	//konverter filnavn til c-string og åpne fil
	innfilForestilling.open(FORESTILLINGSFIL.c_str());
	innfilInntekt.open(INNTEKTSFIL.c_str());
	//ble filen åpnet?
	if (innfilForestilling.fail()) {
		cout << "Kan ikke " << (char)134 << "pne fil: " << FORESTILLINGSFIL << "!" << endl;
	} else if (innfilInntekt.fail()) {
		cout << "Kan ikke " << (char)134 << "pne fil: " << INNTEKTSFIL << "!" << endl;
	} else { //fil ble åpnet
		//les og hent ut innhold fra fil		
		hentInntektFraFil(innfilInntekt);
		hentForestillingFraFil(innfilForestilling);
		//lukk filen		
		innfilInntekt.close();
		innfilForestilling.close();
	} //if (innfilForestilling.fail())
} //lesFraFil

/**
Denne funksjonen henter det faktiske innholdet fra filen. Den gjør et kall på funksjonen
antallLinjerITekstfil for å sjekke om fil faktisk har innhold. Dersom fil har innhold 
starter funksjonen med å lese inn radene (dette er de første 15 linjene).
Etter at alle rader er lest inn, så  leses forestillingens navn inn.
----------------------------------------------------------------------
@innfil: referanse til et objekt av ifstream; objektet som åpner filen
*/
void hentForestillingFraFil(ifstream &innfil) {
	string filInnhold;
	int teller = antallLinjerITekstfil(innfil);		
	//har tekstfil innhold?
	if (teller > 0) {
		//nullstill teller
		teller = 0;
		//sett at forestilling finnes
		finnesForestilling = true;
		//siden fil ble lukket i funksjonen antallLinjerITekstfil(innfil)
		//må filen åpnes en gang til ---> siden fil allerede har blitt åpnet 
		//uten problemer, sjekkes det ikke etter innfil.fail() her					
		innfil.open(FORESTILLINGSFIL.c_str());
		//start å lese det faktiske innholdet fra filen
		while (!innfil.eof()) {				
			//les linje for linje fra tekstfil, adskilt med linjeskift
			getline(innfil, filInnhold, '\n');				
			if (teller < ANTALL_RADER) {					
				//les inn hvert sete
				for (int j = 0; j < ANTALL_SETER_PR_RAD; j++) {
					//legg til et og et sete på tilhørende rad
					kinoSeter[teller][j] = (char) filInnhold[j];				
				} //for
				//hvis alle radene er lest inn, les forestillingens navn
			} else if (teller = (ANTALL_RADER + 1)) {
				forestillingsNavn = filInnhold;
			} //if (teller < ANTALL_RADER)
			//øk teller
			teller++;
		} //while
	} //if (teller > 0)
} //hentForestillingFraFil

/**
Denne funksjonen henter det faktiske innholdet fra filen. Den gjør et kall på funksjonen
antallLinjerITekstfil for å sjekke om fil faktisk har innhold. Dersom fil har innhold 
starter funksjonen med å lese inn inntektene for hver rad
----------------------------------------------------------------------
@innfil: referanse til et objekt av ifstream; objektet som åpner filen
*/
void hentInntektFraFil(ifstream &innfil) {
	string filInnhold;
	int teller = antallLinjerITekstfil(innfil);		
	//har tekstfil innhold?
	if (teller > 0) {
		//nullstill teller
		teller = 0;
		//siden fil ble lukket i funksjonen antallLinjerITekstfil(innfil)
		//må filen åpnes en gang til ---> siden fil allerede har blitt åpnet 
		//uten problemer, sjekkes det ikke etter innfil.fail() her					
		innfil.open(INNTEKTSFIL.c_str());
		//start å lese det faktiske innholdet fra filen
		while (!innfil.eof()) {				
			//les linje for linje fra tekstfil, adskilt med linjeskift
			getline(innfil, filInnhold, '\n');						
			//legg til inntekten på gjeldende rad i array
			inntektArray[teller] = konverterStringTilDouble(filInnhold);
			//øk teller
			teller++;
		} //while
	} else { //fil har ikke innhold
		//fyll array med null (0)
		for (int i = 0; i < ANTALL_RADER; i++) {
				inntektArray[i] = 0;
		} //for
	} //if (teller > 0)
} //hentInntektFraFil

/**
Denne funksjonen looper gjennom innholdet i filen og teller opp antall linjer.
Hovedsaklig er funksjonens eneste formål å returnere en verdi som tilsier om 
filen har innhold (nevnelig antall linjer).
------------------------------------------------------------------------------
@innfil: referanse til et objekt av ifstream; objektet som åpner filen
@return: integer (antall linjer i fil, hvis tom returneres 0)
*/
int antallLinjerITekstfil(ifstream &innfil) {
	string filInnhold = "";
	//initialiserer en teller som teller antall linjer i tekstfil
	//starter på -1 pga while løkken kjøres minst en gang
	//og teller skal telle opp antall linjer i tekstfil for å kontrollere om den er tom
	int teller = -1;		
	//loop gjennom filens innhold for å se om det er noe der
	while (!innfil.eof()) {
		//les linje for linje fra tekstfil, adskilt med linjeskift
		getline(innfil, filInnhold, '\n');
		//øk teller for hver linje i fil
		teller++;
	} //while
	//siden slutten av filen er nådd, så lukkes filen her
	innfil.close();
	//returner antall linjer i fil
	return teller;
} //antallLinjerITekstfil
#pragma endregion Funksjoner tilknyttet innlesning og uthenting av filens innhold

#pragma region MENY

void visMeny() {
	string meny = "**** HOVEDMENY ****\n\n";
	meny += "Aktuell forestilling: " + forestillingsNavn;
	meny += "\n1. Ny forestilling\n" ;
	meny += "2. Kj";
	meny += char(155);
	meny +="p billetter\n";
	meny += "3. Vis ledige seter\n";
	meny += "4. Vis billettinntekter for denne forestillinga\n";
	meny += "0: Avslutt programmet\n";
	cout << meny << endl;
	cout << "Skriv inn " << (char)155 << "nsket alternativ (0-4): ";
} //visMeny

void hentValgtFunksjon(int menyValg) {
	//"luft"
	cout << endl;
	switch (menyValg) {
	case 0:
		//siden null avslutter programmet, stopp her
		break;
	case 1:
		//opprett en ny forestilling
		nyForestilling();
		break;
	case 2:
		if (finnesForestilling) {
			//selg billetter
			selgBilletter();
		} else {
			cout << "Ingen forestilling registrert." << endl;
			cout << "Billetter kan ikke selges f" << char(155) << "r forestilling er opprettet." << endl << endl;
		} //if (finnesForestilling)
		break;
	case 3:
		//vis forestillingens ledige seter
		visLedigeSeter();
		break;
	case 4:
		//vis denne forestillingens inntekter
		visBilettInntekter();
		break;
	default:
		//bruker skrev inn en verdi utenfor gyldig rekkevidde (0-4), gi feilmelding
		//dobbel endl for å få luft mellom feilmelding og menytekst
		cout << "Du skrev inn " << menyValg << ". Gyldig valg er et tall mellom 0 - 4." << endl << endl;
		break;
	} //switch
} //hentValgtFunksjon

#pragma endregion Oppretting av meny og uthenting av menyvalg

void nyForestilling() {
	//innhold i fil skal slettes
	bool nullstill = true;
	//siden forestillingens navn kan ha mellomrom/space, 
	//så brukes getline som henter ut hele linja.
	//men, et problem som oppstår er at den leser newline (\n), og 
	//"tror" at bruker ønsker å hoppe over input derfor 
	//brukes det her cin.ignore()
	//funksjonen tilsier da at den skal hoppe over x antall tegn
	//(her: numeric_limits<streamsize>::max() som er størrelsen på I/O Buffer) 
	//eller til den møter newline (\n)
	//http://www.velocityreviews.com/forums/t289313-cin-and-cin-getline.html
	//http://www.cplusplus.com/forum/general/1477/
	cin.ignore(numeric_limits<streamsize>::max(),'\n');
	//hent navn på forestilling
	forestillingsNavn = navngiForestilling();
	//hvis intet navn på forestilling ble skrivd inn
	while (forestillingsNavn == "") {
		//input var tomt, gi feilmelding 
		cout << "\nForestilling m" << char(134) << " ha et navn."<< endl;
		//spør på nytt etter forestillingens navn
		forestillingsNavn = navngiForestilling();
	} //while
	//sett opp ny forestilling og skriv til fil
	skrivTilFil(nullstill);
	//les inn den nye forestillingen
	lesFraFil();
	//"luft"
	cout << endl;
} //nyForestilling

/**
Denne funksjonen spør bruker etter navn på forestilling og
returnerer resultatet. For innlesning brukes getline for å 
inkludere mellomrom/space. 
NB! Det sjekkes ikke etter om input er tomt/blankt.
----------------------------------------------------------
@return: string som inneholder navn på forestilling
*/
string navngiForestilling() {
	string input = "";
	//spør etter navn på forestilling
	cout << "Skriv inn navn p" << char(134) <<" forestilling: ";
	//tømmer cin for tidligere input
	cin.clear();
	//les inn navn på forestilling
	getline(cin, input);
	return input;
} //navngiForestilling

void skrivTilFil(bool nullstill) {
	ofstream utfilForestilling, utfilInntekt;
	string filInnholdForestilling = "";
	string filInnholdInntekt = "0";
	if (nullstill) {
		//løkker som setter alle seter på alle rader til ledig
		for (int i = 0; i < ANTALL_RADER; i++) {
			for (int j = 0; j < ANTALL_SETER_PR_RAD; j++) {
				filInnholdForestilling += LEDIG_SETE;
			} //indre for
			//legg til linjeskift på slutten av hver rad
			filInnholdForestilling += "\n";
		} //ytre for
		//setter at alle radene har null inntekt
		for (int i = 0; i < ANTALL_RADER; i++) {
			filInnholdInntekt += "0\n";
		} //for
		//legg til forestillingens navn på slutten av filen
		filInnholdForestilling += forestillingsNavn;
	} else {
		//løkker som looper gjennom og henter ut innhold fra array
		for (int i = 0; i < ANTALL_RADER; i++) {
			for (int j = 0; j < ANTALL_SETER_PR_RAD; j++) {
				filInnholdForestilling += kinoSeter[i][j];
			} //indre for
			//legg til linjeskift på slutten av hver rad
			filInnholdForestilling += "\n";
		} //ytre for
		filInnholdForestilling += forestillingsNavn;
		//loop gjennom alle radene
		for (int i = 0; i < ANTALL_RADER; i++) {			
			//hent ut og konverter inntekt til double
			filInnholdInntekt += konverterDoubleTilString(inntektArray[i]) + "\n";
		} //for
	} //if (nullstill)
	//åpne filen det skal skrives til
	utfilForestilling.open(FORESTILLINGSFIL.c_str());
	utfilInntekt.open(INNTEKTSFIL.c_str());
	//skriv innhold til fil
	utfilForestilling << filInnholdForestilling;
	utfilInntekt << filInnholdInntekt;
	//lukk filen
	utfilForestilling.close();
	utfilInntekt.close();
} //skrivTilFil

#pragma region SALG AV BILLETTER
void selgBilletter() {
	char bekreftKjop;
	double totalPris;	
	int antFullpris, antRedPris, antallBilletter, radNr, seteNr;
	//hent antall billetter som skal selges (fullpris og redusert pris)
	antFullpris = oppgiAntallBilletterFullpris();
	//hvis antall biletter til fullpris ikke var korrekt
	//be bruker på nytt om å skrive den inn på nytt
	while (antFullpris == -1) {
		antFullpris = oppgiAntallBilletterFullpris();
	} //while
	antRedPris = oppgiAntallBilletterRedusertpris();
	//hvis antall biletter til redusert pris ikke var korrekt
	//be bruker på nytt om å skrive den inn på nytt
	while (antRedPris == -1) { 		
		antRedPris = oppgiAntallBilletterRedusertpris();
	} //while
	//legg sammen den totale prisen for billettene
	totalPris = (FULL_PRIS * antFullpris) + (REDUSERT_PRIS * antRedPris);
	//legg sammen antall billetter
	antallBilletter = antFullpris + antRedPris;
	//hent en array som inneholder radnr og setenr 
	//(hvis feil inneholder den -1)
	int *plassArray = oppgiRadOgSeteNr(antallBilletter);
	radNr = plassArray[0];
	seteNr = plassArray[1];
	//oppsto det feil/ble salg avbrutt?
	if (radNr == -1 || seteNr == -1) {
		//salg avbrutt, gi tilbakemelding til bruker
		cout << "\n------ Salg av billetter ble avbrutt. ------" << endl << endl;
	} else { //alt ok, bekreft salg
		//hent bekreftelse på kjøp (J/j = salg, n/N avbrutt salg)
		bekreftKjop = oppsummeringSalg(radNr, seteNr, antallBilletter, totalPris);
		//hva er resultatet - ble billetter solgt eller ble salg avbrutt?
		switch (bekreftKjop) {
		case 'j':
		case 'J':
				//-1 pga array starter fra 0, mens bruker oppgir verdi fra 1
				radNr--;
				seteNr--;
				inntektArray[radNr] = totalPris;
				lagreBillettSalg(radNr, seteNr, antallBilletter);
				break;
		case 'n':
		case 'N':
			//kjøp ble avbrutt, vis bekreftelse til bruker
			cout << "\n------ Salg av billetter ble avbrutt. ------" << endl << endl;
			break;
		} //switch
	} //if (radNr == -1 || seteNr == -1)
} //selgBilletter

#pragma region OPPGI ANTALL BILLETTER
/**
Ber bruker om å oppgi antall billetter som skal selges til fullpris.
Hvis ikke et heltall oppgis, vil funksjonen feile og -1 returneres.
Ved korrekt input (heltall) returneres tallet som skrives inn.
------------------------------------------------------------------
@return: integer -1 eller antall oppgitt
*/
int oppgiAntallBilletterFullpris() {
	int antFullpris = -1;
	//spør etter antall billetter til full og redusert pris
	cout << "Oppgi antall billetter til full pris (" << FULL_PRIS << " kr): ";
	cin >> antFullpris;
	if (cin.fail()) {
		//bruker skrev ikke inn et tall, sett cin fra fail til good status
		//(klar for ny input)
		cin.clear();
		//tøm cin for innhold
		cin.ignore();
		//vis feilmelding
		cout << "Ikke en gyldig verdi innskrevet. Antall m" << char(134) << " v" << (char)145 << "re et siffer (0-9)." << endl;
	} //if (cin.fail())
	return antFullpris;
} //oppgiAntallBilletterFullpris

/**
Ber bruker om å oppgi antall billetter som skal selges til redusert pris.
Hvis ikke et heltall oppgis, vil funksjonen feile og -1 returneres.
Ved korrekt input (heltall) returneres tallet som skrives inn.
------------------------------------------------------------------
@return: integer -1 eller antall oppgitt
*/
int oppgiAntallBilletterRedusertpris() {
	int antRedPris = -1;
	cout << "Oppgi antall billetter til redusert pris (" << REDUSERT_PRIS << " kr): ";
	cin >> antRedPris;
	if (cin.fail()) {
		//bruker skrev ikke inn et tall, sett cin fra fail til good status
		//(klar for ny input)
		cin.clear();
		//tøm cin for innhold
		cin.ignore();
		//vis feilmelding
		cout << "Ikke en gyldig verdi innskrevet. Antall m" << char(134) << " v" << (char)145 << "re et siffer (0-9)." << endl;
	} //if (cin.fail())
	return antRedPris;
} //oppgiAntallBilletterRedusertpris
#pragma endregion Funksjoner som ber bruker om å oppgi antall billetter til gitt prisklasse

#pragma region OPPGI RAD, SETE OG KONTROLLER LEDIGHET
/**
Dette er en ganske stor funksjon, som tar for seg input behandling av 
ønsket rad og sete. Videre utføres også en kontroll om valgt sete på valgt 
rad er ledig. 

Dersom input feiler, får bruker en feilmelding i tillegg til
ny mulighet til å legge inn korrekte data. Etter at korrekt data 
(nevnelig radnr og setenr) er oppgitt, utføres en kontroll via funksjonen 
erSeterLedig(); som kort sagt sier om seter opptatt/ledig. 

Dersom seter ikke er ledig, får bruker tre alternativer;
1) Utskrift som viser ledige seter ( via funksjonen visLedige() )
2) Mulighet for nytt forsøk på å legge inn nytt rad/sete nummer
3) Avbryte salget, som fyller pointer array med -1
-----------------------------------------------------------------------
@param antallBilletter: integer totalt antall billetter som skal selges
@return: pointer array som inneholder enten:
1) verdi for valgt rad og sete(r)
2) -1 som betyr at salg ble avbrutt
*/
int *oppgiRadOgSeteNr(int antallBilletter) {
	bool avbryt = false;
	int radNr, seteNr;
	int antElementer = 2;
	int *plassArray = new int[antElementer];
	//spør etter hvilken rad og sete(r) som skal selges
	cout << "\nOppgi radnr og startsete adskilt med mellomrom (eks: 4 10) eller " << endl;
	cout << "skriv inn (99 99) for " << char(134) << " avslutte: ";
	cin >> radNr >> seteNr;	
	#pragma region WHILE LØKKE FOR FEIL I INPUT
	//dersom input feiler eller er in-korrekt, kjør løkka til rett input er innskreven
	while ((cin.fail() || (radNr > ANTALL_RADER || radNr < 1) || (seteNr > ANTALL_SETER_PR_RAD || seteNr < 1) 
					  || ((seteNr + antallBilletter - 1) > ANTALL_SETER_PR_RAD)) && !avbryt) {
		//hva forårsaket feilen?
		if (cin.fail()) {
			//bruker skrev ikke inn et tall, sett cin fra fail til good status
			//(klar for ny input)
			cin.clear();
			//tøm cin for innhold
			cin.ignore();
			//vis feilmelding
			cout << "En eller flere av verdier innskrevet er ugyldig." << endl;
			cout << "Rad og setenr kan kun best" << char(134) << " av siffer (0-9)." << endl << endl;
		} else if (radNr == 99 && seteNr == 99) {
			avbryt = true;			
		} else if (radNr > ANTALL_RADER || radNr < 1) { 
			//radnr er ikke innenfor rett intervall, vis feilmelding
			cout << "Radnummer m" << char(134) << " v" << (char)145 << "re et siffer mellom 1 - " << konverterIntegerTilString(ANTALL_RADER) << "." << endl << endl;
		} else if (seteNr > ANTALL_SETER_PR_RAD || seteNr < 1) {
			//setenr er ikke innenfor rett intervall, vis feilmelding
			cout << "Setenummer m" << char(134) << " v" << (char)145 << "re et siffer mellom 1 - " << konverterIntegerTilString(ANTALL_SETER_PR_RAD) << "." << endl << endl;
		} else if ((seteNr + antallBilletter - 1) > ANTALL_SETER_PR_RAD) {
			//ikke nok ledige seter på ønsket rad
			cout << "Ikke nok tilgjengelige seter p" << char(134) << " valgt rad." << endl << endl;
		} //if (cin.fail())
		//er salg avbrutt?
		if (!avbryt) {
			//spør bruker på nytt etter rad - og setenr
			cout << "\nOppgi radnr og startsete adskilt med mellomrom (eks: 4 10) eller " << endl;
			cout << "skriv inn (99 99) for " << char(134) << " avslutte: ";
			cin >> radNr >> seteNr;
		} //if (!avbryt)
	} //while
	#pragma endregion
	//er salg avbrutt
	if (!avbryt) {
		//legg inn radNr og seteNr
		plassArray[0] = radNr;
		plassArray[1] = seteNr;
		//variabel for brukers valg hvis valgt(e) sete(r) er opptatt
		int valg = 0;
		//er valgte seter ledige?
		bool ledig = erSeterLedig(antallBilletter, radNr, seteNr);
		if (!ledig) {
			//sete(r) er ikke ledig, spør bruker hva som skal gjøres
			cout << "\nEt eller flere av valgte seter er opptatt!" << endl << endl;
			cout << "Trykk 1 for " << char(134) << " vise ledige seter." << endl;
			cout << "Trykk 2 for " << char(134) << " legge inn annen rad/sete." << endl;
			cout << "Trykk 3 for " << char(134) << " avbryte kj" << char(155) << "pet." << endl << endl;
			cout << "Ditt valg: ";
			//les inn valg
			cin >> valg;
			//hvis input feiler, spør bruker igjen
			while (cin.fail() || valg < 1 || valg > 3) {
				cout << "\nTrykk 1 for " << char(134) << " vise ledige seter." << endl;
				cout << "Trykk 2 for " << char(134) << " legge inn annen rad/sete." << endl;
				cout << "Trykk 3 for " << char(134) << " avbryte kj" << char(155) << "pet" << endl << endl;
				cout << "Ditt valg: ";
				cin >> valg;
			} //while
			switch (valg) {
			case 1:
				//vis ledige seter og....
				cout << endl; //luft
				visLedigeSeter();			
			case 2:
				//...la bruker forsøke på ny å legge til rad og seter
				plassArray = oppgiRadOgSeteNr(antallBilletter);
				break;
			case 3:
				//bruker ønsker å avbryte, legg inn verdien -1 som tegn på avbryting...
				plassArray[0] = -1;
				plassArray[1] = -1;
				break;
			} //switch
		} //if (!ledig)	
	} else {
		//bruker ønsker å avbryte, legg inn verdien -1 som tegn på avbryting...
		plassArray[0] = -1;
		plassArray[1] = -1;
	} //if (!avbryt)
	return plassArray;
} //*oppgiRadOgSeteNr

/**
Dette er en funksjon som returnerer en boolsk verdi basert på 
oppgitt rad og sete. Dersom seter oppgitt er ledige, returneres 
true. Dersom et eller flere seter er opptatt returneres false.
---------------------------------------------------------------
@param antallBilletter: totalt antall billetter som skal selges
@param radNr: raden som setene finnes på
@param seteNr: "startsete" som skal selges 
Eksempel: Hvis 3 billetter skal selges på rad 1, 
og ønsket sete er 1, 2 og 3, så oppføres seteNr = 1.
Resten beregnes i egne algoritmer i de forskjellige funksjoner.
@return: true: valgte seter ER ledige. false: valgte seter er IKKE ledige
*/
bool erSeterLedig(int antallBilletter, int radNr, int seteNr) {
	bool seteLedig = true;
	//trekk fra 1 pga array starter på 0
	int i = seteNr - 1;
	//hvor lenge løkka skal kjøre; -1 pga array starter på 0
	int maxLoop = seteNr + antallBilletter - 1;
	//så lenge ingen seter er opptatt og seter gjenstår å sjekkes...
	while (seteLedig && i < maxLoop) {
		//hent ut innhold fra array (-1 pga array starter på 0)
		char innhold = kinoSeter[radNr-1][i];
		//er setet opptatt?
		if (innhold == OPPTATT_SETE) {
			seteLedig = false;
		} //if (innhold == OPPTATT_SETE)
		//øk teller
		i++;
	} //while
	//returner resultatet
	return seteLedig;
} //erSeterLedig
#pragma endregion Funksjon som ber bruker oppgi rad og sete, og i tillegg utføres kontroll av input & ledighet

/**
Denne funksjonen oppdaterer array og skriver endringene til fil 
via funksjonen skrivTilFil().
-------------------------------------------------------------------
@param radNr: raden som setene som selges tilhører
@param seteNr: "startsete" som skal selges 
Eksempel: Hvis 3 billetter skal selges på rad 1, 
og ønsket sete er 1, 2 og 3, så oppføres seteNr = 1.
Dette regnes ut inni funksjonen
@param antallBilletter: totalt antall billetter som skal selges
*/
void lagreBillettSalg(int radNr, int seteNr, int antallBilletter) {
	//oppdater array basert på antall solgte billetter
	if (antallBilletter == 1) {
		//kun en billett som ble solgt
		kinoSeter[radNr][seteNr] = OPPTATT_SETE;
	} else { //flere billetter solgt			
		//opprett variabel for hvor lenge løkka skal gå
		int maxSeterSolgt = seteNr + antallBilletter;
		//løkke som oppdaterer array og setter seter til solgt
		for (int i = seteNr; i < maxSeterSolgt; i++) {
			kinoSeter[radNr][i] = OPPTATT_SETE;
		} //for		
	} //if (antallBilletter == 1)
	//kjøp ble bekreftet, skriv endringer til fil
	skrivTilFil(false);
	//vis bekreftelse på salg til bruker
	cout << "\n------ Salg av billetter er registrert. ------" << endl << endl;
} //lagreBillettSalg

/**
Denne funksjonen skriver ut en oppsummering på gjeldende billettsalg til bruker.
I tillegg ber funksjonen bruker om å bekrefte salg (j/J) eller avbryte (n/N).
--------------------------------------------------------------------------------
@param radNr: integer - Valgt rad for billettsalg
@param seteNr: integer - Valgt sete ("startsete" hvis flere billetter)
@param antallBilletter: integer - totalt antall billetter som skal selges
@param totalPris: den totale prisen for salget
@return: char med verdi 'j', 'J', 'n' eller 'N'
*/
char oppsummeringSalg(int radNr, int seteNr, int antallBilletter, double totalPris) {
	char bekreftKjop;
	string oppsummering, bekreftelse;
	//string som oppsummerer detaljer tilknyttet billettsalget
	oppsummering = "\nPrisen for billetten(e) p";
	oppsummering += char(134);
	oppsummering += " rad " + konverterIntegerTilString(radNr);	
	oppsummering +=  " sete " + konverterIntegerTilString(seteNr);
	//hvis det er salg av flere seter, legg til sluttsete
	if (antallBilletter > 1) {
		//salg av flere seter; finn max og trekk fra en pga seteNr er inkl. i kjøpet
		oppsummering +=  " - " + konverterIntegerTilString(seteNr + antallBilletter - 1);
	} //if (antallBilletter > 1)
	oppsummering += " er " + konverterDoubleTilString(totalPris) + " kr. ";
	//string som ber bruker om å bekrefte eller avbryte salget
	bekreftelse = "Trykk 'j' eller 'J' for ";
	bekreftelse += char(134);
	bekreftelse += " bekrefte billettsalg, eller trykk 'n' eller 'N' for ";
	bekreftelse += char(134);
	bekreftelse += " avbryte: ";
	//spør etter bekreftelse på kjøpet
	cout << oppsummering << endl;
	cout << bekreftelse;
	cin >> bekreftKjop;
	//ble kjøp bekreftet/avbrutt?
	while (bekreftKjop != 'j' && bekreftKjop != 'J' && bekreftKjop != 'n' && bekreftKjop != 'N') {
		//kjøp ikke bekreftet/avbrutt, spør igjen
		cout << oppsummering << endl;
		cout << bekreftelse;
		cin >> bekreftKjop;
	} //while
	return bekreftKjop;
} //oppsummeringSalg

#pragma endregion Funksjoner tilknyttet salg av billetter

/**
Denne funksjonen viser ledige seter forutsatt at det finnes en 
registrert forestilling. Dersom forestilling ikke finnes, gis det 
en feilmelding som forteller at forestilling må settes opp før 
seteinformasjon kan vises.
--------------------------------------------------------------
Dersom forestilling finnes listes ledige og opptatte seter opp 
med følgende utseende ('#' = opptatt):

		012345678901234567890123456789
Rad 1	******************************
Rad2	*****************##***********
Rad 3	******************************
Rad 4	******************************
Rad 5	**####************************
Rad 6	******###*********************
Rad 7	****************#*************
Rad 8	******************************
Rad 9	*********#######**************
Rad 10	******************************
Rad 11	******************************
Rad 12	******************************
Rad 13	*******************#######****
Rad 14	******************************
Rad 15	******************************
*/
void visLedigeSeter() {
	//finnes det en oppsatt/registrert forestilling?
	if (finnesForestilling) {
		//forestilling finnes, skriv ut seteinformasjon
		int rad = 0;
		string enLinje = "";
		//tekst som skal vises over og på siden av setene
		string beskrivelsesTekst = "012345678901234567890123456789";
		//skriv ut teksten som skal vises over setene
		cout << left << setw(10) << " " << beskrivelsesTekst << endl;
		//for hver rad i array...
		for (int i = 0; i < ANTALL_RADER; i++) {
			//siden første plass = 0, men første rad = 1
			//sett verdien til i + 1 (i = 0 gir da 0 + 1 = 1) 
			rad = i + 1;
			//legg til rad med tilhørende radnr konverert til string
			beskrivelsesTekst = "Rad " + konverterIntegerTilString(rad);
			//for hvert sete i array....
			for (int j = 0; j < ANTALL_SETER_PR_RAD; j++) {
				//hent ut alle setene på gitt rad
				enLinje += kinoSeter[i][j];
			} // indre for
			//skriv ut raden og setene på gjeldende rad
			cout << left << setw(10)  << beskrivelsesTekst<< enLinje << endl;
			//fjern gjeldende rad fra string objektet
			enLinje = "";
		} //ytre for
	} else { //ingen forestilling registrert
		//gi "feilmelding" til bruker
		cout << "\nIngen forestilling er registrert." << endl;
		cout << "Ledige seter kan ikke vises f" << char(155) << "r en forestilling er opprettet." << endl;
	} //if (finnesForestilling)
	//"luft"
	cout << endl;
} //visLedigeSeter

void visBilettInntekter() {
	int antSolgte = 0,		
		totaltAntSolgte = 0;
	double totalInntekt = 0;
	int antallSolgtePrRad[ANTALL_RADER]; 
	//loop gjennom rader og seter for å telle opp antall solgte
	for (int i = 0; i < ANTALL_RADER; i++) {
		for (int j = 0; j < ANTALL_SETER_PR_RAD; j++) {
			//er dette setet solgt?
			if (kinoSeter[i][j] == OPPTATT_SETE) {
				//øk teller for antall solgte
				antSolgte++;
			} //if (kinoSeter[i][j] == OPPTATT_SETE)
		} //indre for
		//legg til antallet i array
		antallSolgtePrRad[i] = antSolgte;
		//nullstill teller
		antSolgte = 0;
	} //ytre for
	//tittel linje
	cout << left << setw(20) << "Rad" << left << setw(20) << "Antall solgte" << right << setw(20) << "Inntekt pr rad" << endl;
	cout << "------------------------------------------------------------" << endl;
	//loop gjennom array og skriv ut innhold
	for (int i = 0; i < ANTALL_RADER; i++) {
		//(i+1) pga array starter på 0
		string radTekst = "Rad " + konverterIntegerTilString(i + 1);
		//skriver ut rad og radnr, antall solgte på gitt rad og inntekten på gitt rad
		cout << left << setw(31) << radTekst << left << setw(11) << antallSolgtePrRad[i] << right << setw(14) 
			<< inntektArray[i] << right << setw(4) << " kr." << endl;
		//legg til antall solgte og inntekt i totalen
		totaltAntSolgte += antallSolgtePrRad[i];
		totalInntekt += inntektArray[i];
	} //for
	cout << "------------------------------------------------------------" << endl;	
	//skriver ut totalt antall solgte billetter og total inntekt
	cout << left << setw(31) << "Total:" << left << setw(11) << totaltAntSolgte << right << setw(14)  << totalInntekt << right << setw(4) << " kr." << endl;
	cout << "------------------------------------------------------------" << endl << endl;	
} //visBilettInntekter

#pragma region KONVERTERING

/**
Denne funksjonen tar et heltall av typen integer og 
konverterer det til string.
---------------------------------------------------------
@param tall: integer tall som skal konverteres til string
@return: string resultatet av konverteringen
*/
string konverterIntegerTilString(int tall) {
	string resultat;
	ostringstream konverterTilString;
	//les inn tallverdi som skal konverteres
	konverterTilString << tall;
	//konverter tallet til string
	resultat = konverterTilString.str();
	//tøm konverterer for innhold
	konverterTilString.clear();
	konverterTilString.str("");
	//returner den konverterte stringen
	return resultat;
} //konverterIntegerTilString

/**
Denne funksjonen tar et heltall av typen double og 
konverterer det til string.
---------------------------------------------------------
@param tall: double tall som skal konverteres til string
@return: string resultatet av konverteringen
*/
string konverterDoubleTilString(double tall) {
	string resultat;
	ostringstream konverterTilString;
	//les inn tallverdi som skal konverteres
	konverterTilString << tall;
	//konverter tallet til string
	resultat = konverterTilString.str();
	//tøm konverterer for innhold
	konverterTilString.clear();
	konverterTilString.str("");
	//returner den konverterte stringen
	return resultat;
} //konverterDoubleTilString

/**
Denne funksjonen tar en verdi av typen string og 
konverterer det til double. Dersom verdi ikke kan konverteres
returneres null
---------------------------------------------------------
@param tall: string verdi (tall) som skal konverteres til double
@return: double || 0 hvis konvertering feiler
*/
double konverterStringTilDouble(string tall) {
	double resultat = 0;
	//les inn stringverdi som skal konverteres
	stringstream konverterTilDouble(tall);
	//kan verdien konverteres?
	if (!(konverterTilDouble >> resultat)) {
		//kunne ikke konverteres, sett verdi til null
		resultat = 0;
	} //if (!(konverterTilDouble >> resultat))
	return resultat;
} //konverterStringTilDouble

#pragma endregion Konvertering fra integer og double til String