#include "filbehandling.h"
#include "konvertering.h"

//filnavn for matvaretabell
const string Filbehandling::MATVARE_TABELL_FILNAVN = "Matvaretabellen_2012_csv.csv";
//filnavn for eksisterende oppskrifter
const string Filbehandling::EKSISTERENDE_OPPSKRIFTER_FILNAVN = "Eksisterende oppskrifter.txt";
//initialiserer vector ---> verdi settes i Filbehandling::lesFraMatvareTabell()
vector<MatvareTabell> *Filbehandling::matVektor = NULL;

Filbehandling::Filbehandling() {
} //konstruktør

Filbehandling::~Filbehandling() {
	//sletter peker
	delete matVektor;
	matVektor = 0;
} //destruktør

/*
Denne funksjonen leser inn innholdet fra en Excel-fil som er av 
typen .csv (semikolonn-separert). Innholdet i filen er rundt 
1300 forskjellige matvarerer med forskjellige verdier.
Verdiene brukes for å vise/beregne matvarens ernæringsinformasjon.

Funksjonen leser inn filens innhold og via klassen MatvareTabell 
så gjøres filens innhold om til objekter som legges i en vector.
vectoren<MatvareTabell> er en peker, grunnen til dette er for å 
forbedre søketiden ved uthenting av data basert på gitte kriterier.

Eksempelvis kan her nevnes søk på matvareID ved oppretting av ny 
oppskrift/rett.
----------------------------------------------------------------
@return: *vector<MatvareTabell> - Peker til vector fylt med objekter 
av MatvareTabell
*/
vector<MatvareTabell> *Filbehandling::lesFraMatvareTabell() {	
	ifstream innfil;
	string vareNavn = "";
	string filInnhold = "";
	double matvareID = 0, 
		vann = 0,
		kiloJoule = 0, 
		fett = 0,
		kolesterol = 0,
		karbohydrat = 0, 
		kostfiber = 0, 
		protein = 0, 
		vitaminA = 0,
		vitaminD = 0,
		vitaminE = 0,
		vitaminC = 0;
	//åpne filen
	innfil.open(MATVARE_TABELL_FILNAVN.c_str());
	//opprett vector som dynamisk peker
	matVektor = new vector<MatvareTabell>();
	if (innfil.fail()) {
		cout << "Kan ikke " << (char)134 << "pne fil: " << MATVARE_TABELL_FILNAVN << "!" << endl;
	} else { //fil ble åpnet
		//hopp over de første to linjene, siden de er uten innhold
		getline(innfil, filInnhold, '\n');
		getline(innfil, filInnhold, '\n');		
		//loop gjennom filens innhold
		while(!innfil.eof()) {
			getline(innfil, filInnhold, ';');
			matvareID = Konvertering::konverterStringTilDouble(filInnhold);
			getline(innfil, filInnhold, ';');
			vareNavn = filInnhold;
			getline(innfil, filInnhold, ';');
			vann = Konvertering::konverterStringTilDouble(filInnhold);
			getline(innfil, filInnhold, ';');
			kiloJoule = Konvertering::konverterStringTilDouble(filInnhold);
			getline(innfil, filInnhold, ';');
			fett = Konvertering::konverterStringTilDouble(filInnhold);
			getline(innfil, filInnhold, ';');
			kolesterol = Konvertering::konverterStringTilDouble(filInnhold);
			getline(innfil, filInnhold, ';');
			karbohydrat = Konvertering::konverterStringTilDouble(filInnhold);
			getline(innfil, filInnhold, ';');
			kostfiber = Konvertering::konverterStringTilDouble(filInnhold);
			getline(innfil, filInnhold, ';');
			protein = Konvertering::konverterStringTilDouble(filInnhold);
			getline(innfil, filInnhold, ';');
			vitaminA = Konvertering::konverterStringTilDouble(filInnhold);
			getline(innfil, filInnhold, ';');
			vitaminD = Konvertering::konverterStringTilDouble(filInnhold);
			getline(innfil, filInnhold, ';');
			vitaminE = Konvertering::konverterStringTilDouble(filInnhold);
			//OBS! Siden dette er siste verdi, så er skilletegn her newline
			getline(innfil, filInnhold, '\n');
			vitaminC = Konvertering::konverterStringTilDouble(filInnhold);
			//opprett objekt av matvaren
			MatvareTabell matvare(matvareID, vareNavn,  vann,  kiloJoule,  fett,  kolesterol, karbohydrat,  
				kostfiber,  protein,  vitaminA,  vitaminD,  vitaminE,  vitaminC);
			//legg objektet i vector og sjekk at minnet ble allokert
			matVektor -> push_back(matvare);
			assert(matVektor != 0);
		} //while
		//lukk filen
		innfil.close();
	} //if (innfil.fail())
	return matVektor;
} //lesFraMatvareTabell

/*
Leser inn listen over registrerte oppskrifter.
Listen er navnet på filene med gitt rett/oppskrift, 
som legges i en vector<string>. vectoren kan da brukes 
for å lese inn en, flere eller alle eksisterende 
oppskrifter.
----------------------------------------------------
@return: vector<string> - Filnavn på eksisterende oppskrifter
*/
vector<string> Filbehandling::lesInnOppskriftsTitler() {
	ifstream innfil;
	string oppskrift = "";
	vector<string> registrerteOppskrifter;
	//opprett fil hvis fil ikke eksisterer
	opprettFil(EKSISTERENDE_OPPSKRIFTER_FILNAVN);
	innfil.open(EKSISTERENDE_OPPSKRIFTER_FILNAVN.c_str());
	//ble filen åpnet?
	if (innfil.fail()) {
		cout << "Kan ikke " << (char)134 << "pne fil: " << EKSISTERENDE_OPPSKRIFTER_FILNAVN << "!" << endl;
	} else { //fil ble åpnet
		int teller = antallLinjerITekstfil(innfil);
		//har tekstfil innhold?
		if (teller > 0) {
			//siden fil ble lukket i funksjonen antallLinjerITekstfil(innfil)
			//må filen åpnes en gang til ---> siden fil allerede har blitt åpnet 
			//uten problemer, sjekkes det ikke etter innfil.fail() her					
			innfil.open(EKSISTERENDE_OPPSKRIFTER_FILNAVN.c_str());
			//les innhold fra fil og legg det i vector
			while (!innfil.eof()) {
				getline(innfil, oppskrift, '\n');
				registrerteOppskrifter.push_back(oppskrift);
			} //while
			innfil.close();
		} //if (teller > 0)
	} //if (innfil.fail())
	//finnes det oppskrifter?
	if (registrerteOppskrifter.size() > 0) {
		//hent ut størrelsen på vector
		unsigned int vectorStrl = registrerteOppskrifter.size() - 1;
		//har vector innhold, og er siste element blankt (eks: newline)?
		if (registrerteOppskrifter.at(vectorStrl) == "") {
			//siste element er blankt, slett det fra vector
			registrerteOppskrifter.erase(registrerteOppskrifter.begin() + vectorStrl);
		} //if (registrerteOppskrifter.at(vectorStrl) == "")
	} //if (registrerteOppskrifter.size() > 0)
	return registrerteOppskrifter;
} //lesInnOppskriftsTitler

/*
Leser inn innholdet fra en tekstfil som inneholder en oppskrift.
Oppskriften legges i en vector<Oppskrift> som da vil inneholde 
følgende verdier:
- Matvaren(es) ID
- Matvarens mengde (i gram)
- Matvarens proteinmengde (i gram)
--------------------------------------------------
@param filnavn: string - navnet på filen det skal leses fra
@return: vector<Oppskrift>(matvareID, gram, protein)
(dersom fil er tom, returnes en tom vector)
*/
vector<Oppskrift> Filbehandling::lesInnEnOppskrift(string filnavn) {
	ifstream innfil;
	vector<Oppskrift> oppskriftsIngredienser;
	innfil.open(filnavn.c_str());
	//ble filen åpnet?
	if (innfil.fail()) {
		cout << "Kan ikke " << (char)134 << "pne fil: " << filnavn << "!" << endl;
	} else { //fil ble åpnet
		int teller = antallLinjerITekstfil(innfil);
		//har tekstfil innhold?
		if (teller > 0) {
			string filInnhold = "";
			double matvareID = 0, 
				antGram = 0,
				antProteiner = 0;
			//siden fil ble lukket i funksjonen antallLinjerITekstfil(innfil)
			//må filen åpnes en gang til ---> siden fil allerede har blitt åpnet 
			//uten problemer, sjekkes det ikke etter innfil.fail() her					
			innfil.open(filnavn.c_str());
			//les innhold fra fil
			while (!innfil.eof()) {
				getline(innfil, filInnhold, ';');
				//pga at skriving til fil avslutter med newline, så blir det en ekstra linje
				//i slutten på filen, noe som gjør at filInnhold settes lik blank ("")
				//dette fører til at konvertering returnerer -999 (kunne ikke konvertere string til double)
				//derfor sjekker jeg her på om faktisk er en verdi som leses inn
				//(i tillegg sikrer det mot feil som f.eks. at filen er tom/innhold har blitt slettet)
				if (filInnhold != "") {
					matvareID = Konvertering::konverterStringTilDouble(filInnhold);
					getline(innfil, filInnhold, ';');
					antGram = Konvertering::konverterStringTilDouble(filInnhold);
					getline(innfil, filInnhold, '\n');
					antProteiner = Konvertering::konverterStringTilDouble(filInnhold);
					//oppretter objekt og legger det inn i vector
					Oppskrift rett(matvareID, antGram, antProteiner);
					oppskriftsIngredienser.push_back(rett);
				} //if (filInnhold != "")
			} //while
			innfil.close();
		} //if (teller > 0)
	} //if (innfil.fail())
	return oppskriftsIngredienser;
} //lesInnEnOppskrift

/*
Skriver en ny oppskrift til fil.
Verdiene som skrives til fil er ingrediensen til 
den nye oppskriften; hver matvares ID, antall gram av hver 
matvare og antall proteiner pr matvare.
Hver matvare blir skrivd på en linje.
--------------------------------------------------
Eksempel: 
Sett at det registreres 2 ingredienser.
Da blir filens innhold seende slik ut;
1. linje: "matvareID; antGram; antProteiner \n"
2. linje: "matvareID; antGram; antProteiner \n"
--------------------------------------------------
@param filnavn: string - navnet på filen det skal skrives til
@param nyOppskrift: vector<Oppskrift>(matvareID, gram, protein)
*/
void Filbehandling::skrivOppskriftTilFil(string &filnavn, vector<Oppskrift> nyOppskrift) {
	ofstream utfil;
	double matvareID,
		antGram,
		antProteiner;
	string filInnhold = "";
	//loop gjennom matvarene/ingredienser og legg de i string variabel
	for (unsigned int i = 0; i < nyOppskrift.size(); i++) {
		matvareID =	nyOppskrift.at(i).getMatvareID();
		antGram = nyOppskrift.at(i).getGram();
		antProteiner = nyOppskrift.at(i).getProteiner();
		//legg hvert objekt av oppskrift på en linje adskilt med semikolonn
		filInnhold += Konvertering::konverterDoubleTilString(matvareID) + ";";
		filInnhold += Konvertering::konverterDoubleTilString(antGram) + ";";
		//legg til newline på slutten
		filInnhold += Konvertering::konverterDoubleTilString(antProteiner) + "\n";
	} //for
	filnavn += ".txt";
	utfil.open(filnavn.c_str());
	//skriv innhold til fil og lukk den
	utfil << filInnhold;
	utfil.close();
	//oppdater listen over eksisterende oppskrifter
	utfil.open(EKSISTERENDE_OPPSKRIFTER_FILNAVN.c_str(), ios::app);
	utfil << filnavn << "\n";
	utfil.close();
} //skrivOppskriftTilFil

/*
Oppretter fil dersom fil ikke finnes.
For å unngå at filinnhold slettes hvis fil eksisterer, 
brukes ios::app.
--------------------------------------------------
@param filnavn: string - navnet på filen som skal opprettes
*/
void Filbehandling::opprettFil(string filnavn) {
	ofstream opprettFil(filnavn, ios::app);
	opprettFil.close();
} //opprettFil

/*
Denne funksjonen looper gjennom innholdet i filen og teller opp antall linjer.
Hovedsaklig er funksjonens eneste formål å returnere en verdi som tilsier om 
filen har innhold (nevnelig antall linjer).
------------------------------------------------------------------------------
@param innfil: referanse til et objekt av ifstream; objektet som åpner filen
@return: integer (antall linjer i fil, hvis tom returneres 0)
*/
int Filbehandling::antallLinjerITekstfil(ifstream &innfil) {
	string filInnhold = "";
	//initialiserer en teller som teller antall linjer i tekstfil
	//starter på -1 pga while løkken kjøres minst en gang og teller skal
	// telle opp antall linjer i tekstfil for å kontrollere om den er tom
	int teller = -1;		
	//loop gjennom filens innhold for å se om det er noe der
	while (!innfil.eof()) {
		//les linje for linje fra tekstfil, adskilt med linjeskift
		getline(innfil, filInnhold, '\n');
		//øk teller for hver linje i fil
		teller++;
	} //while
	//lukk filen og returner resultatet
	innfil.close();
	return teller;
} //antallLinjerITekstfil