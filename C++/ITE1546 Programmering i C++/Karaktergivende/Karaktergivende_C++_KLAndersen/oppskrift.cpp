#include "oppskrift.h"
#include "filbehandling.h"
#include "konvertering.h"

//menyvalg: hent rettermed mer enn x (bestemmes av bruker) g protein
const int Oppskrift::HENT_RETTER_MED_MER_ENN_X_GRAM_PROTEIN = 3;
//menyvalg: hent rette rmed mer enn x (bestemmes av bruker) g fett
const int Oppskrift::HENT_RETTER_MED_MER_ENN_X_GRAM_FETT = 4;
//menyvalg: hent retter med mer enn x (bestemmes av bruker) g karbohydrater
const int Oppskrift::HENT_RETTER_MED_MER_ENN_X_GRAM_KARBOHYDRATER = 5;
//menyvalg: hent retter med mer enn x (bestemmes av bruker) g kalorier
const int Oppskrift::HENT_RETTER_MED_MER_ENN_X_GRAM_KALORIER = 6;
//menyvalg: hent retter med som overskrider daglig inntak av vitaminA
const int Oppskrift::HENT_RETTER_MED_MYE_VITAMIN_A = 7;
//menyvalg: hent retter med som overskrider daglig inntak av vitaminD
const int Oppskrift::HENT_RETTER_MED_MYE_VITAMIN_D = 8;
//menyvalg: hent retter med som overskrider daglig inntak av kolesterol
const int Oppskrift::HENT_RETTER_MED_MYE_KOLESTEROL = 9;
//verdi er hentet fra o.oolco.com
const double Oppskrift::DAGLIG_ANBEFALT_INNTAK_KOLESTEROL = 300;
//verdi er hentet fra helsedirektoratets publikasjon, alder: 14-60
const double Oppskrift::DAGLIG_ANBEFALT_INNTAK_VITAMIN_A = 900;
//verdi er hentet fra helsedirektoratets publikasjon, alder: 14-60
const double Oppskrift::DAGLIG_ANBEFALT_INNTAK_VITAMIN_D = 7.5;
//verdi er hentet fra helsedirektoratets publikasjon, alder: 14-60
const double Oppskrift::DAGLIG_ANBEFALT_INNTAK_VITAMIN_E = 10;
//verdi er hentet fra helsedirektoratets publikasjon, alder: 14-60
const double Oppskrift::DAGLIG_ANBEFALT_INNTAK_VITAMIN_C = 75;

Oppskrift::Oppskrift() {
	//initialiser vector
	registrerteOppskrifter = Filbehandling::lesInnOppskriftsTitler();
} //konstruktør

Oppskrift::Oppskrift(string oppskriftsNavn, double totalMengde) {
	_oppskriftsNavn = oppskriftsNavn;
	_totalMengde = totalMengde;
} //konstruktør

Oppskrift::Oppskrift(double matvareID, double gram, double proteiner) {
	_matvareID = matvareID;
	_gram = gram;
	_proteiner = proteiner;
} //konstruktør

#pragma region UTSKRIFT AV EKSISTERENDE OPPSKRIFTER
/*
Denne funksjonen skriver ut alle registrerte oppskrifter på skjerm, 
og gir bruker muligheten til å skrive ut valgt oppskrifts ingredienser.
Dersom ingen oppskrifter finnes, får bruker melding om dette.
------------------------------------------------------------
@param *matvareTabell: vector<MatvareTabell>- Dynamisk peker til vector fylt 
med alle matvarer fra matvaretabellen
*/
void Oppskrift::skrivUtRegistrerteOppskrifter(vector<MatvareTabell> *matvareTabell) {
	//finnes det oppskrifter?
	if (registrerteOppskrifter.size() > 0) {
		string input = "",
			ledetekst = "",
			oppskriftsNavn = "";
		int visInnhold = -1;
		cout << "--------- F" << char(155) << "lgende oppskrifter er registrert ---------" << endl;
		//looper gjennom vector og skriver ut alle oppskriftene
		for (unsigned int i = 0; i < registrerteOppskrifter.size(); i++) {
			oppskriftsNavn = registrerteOppskrifter.at(i);
			//fjerner filtypen fra navnet (.txt)
			oppskriftsNavn.erase(oppskriftsNavn.length()-4, 4);
			cout << (i+1) << ": " << oppskriftsNavn << endl;
		} //for
		cout << endl;
		//skriv ut ledetekst som spør bruker om oppskrifts ingredienser skal skrives ut
		ledetekst = skrivUtLedetekstTilOppskriftsRapport();
		while (visInnhold != 0) {
			cout << ledetekst;
			cin >> input;
			visInnhold = Konvertering::konverterStringTilInteger(input);
			while (visInnhold == Konvertering::INPUT_ER_IKKE_ET_TALL) {
				cout << endl;
				cout << ledetekst;
				cin >> input;
				visInnhold = Konvertering::konverterStringTilInteger(input);
			} //while
			cout << endl;
			//er valgt oppskriftsnr innenfor intervallet?
			if (visInnhold > registrerteOppskrifter.size()) {
				cout << "Oppgitt nummer: " << visInnhold << " finnes ikke." << endl << endl;
			} else if (visInnhold != 0) {
				skrivUtValgtOppskriftsInnhold(visInnhold, matvareTabell);
			} //if(visInnhold != 0)
		} //while
	} else {
		cout << "--- Ingen oppskrifter er registrert ---" << endl;
	} //if (registrerteOppskrifter.size() > 0)
} //skrivUtRegistrerteOppskrifter

/*
Grunnet problemene med særnorske bokstaver, og mengden av nettopp
disse i denne ledeteksten, så valgte jeg å legge denne i en egen 
funksjon for å forbedre lesbarheten i funksjonen hvor denne kalles.
-----------------------------------------------------------------
@return: string - ledetekst som fremvises ved opplisting av oppskrifter
*/
string Oppskrift::skrivUtLedetekstTilOppskriftsRapport() {
	string ledetekst = "Skriv inn nummer p";
	ledetekst += char(134);
	ledetekst += " oppskrift ";
	//inneholder vector mer enn en oppskrift?
	if (registrerteOppskrifter.size() > 1) {
		ledetekst += "(1 - ";
		ledetekst += Konvertering::konverterIntegerTilString(registrerteOppskrifter.size());
		ledetekst += ")";
	} //if (registrerteOppskrifter.size() > 1)
	ledetekst += " for ";
	ledetekst += char(134); 
	ledetekst +=" se innhold. \nTrykk 0 for ";
	ledetekst += char(134); 
	ledetekst += " g";
	ledetekst += char(134);  
	ledetekst += " tilbake til hovedmeny.\n";
	ledetekst += "Ditt valg: ";
	return ledetekst;
} //skrivUtLedetekstTilOppskriftsRapport

/*
Denne funksjonen skriver ut innholdet (ingrediensene og ernæringsinnholdet)
til valgt oppskrift. Den bruker oversendt indeks for å hente ut valgt oppskrift. 
Korrekt indeks for uthenting fra vector gjøres i denne funksjonen, derfor trengs 
det ikke å endre indeks før den sendes over. Utskriften inneholder følgende:
- Matvarens/ingrediensens navn
- Antall gram pr matvare/ingrediens
- Antall gram proteiner pr matvare/ingrediens
- Antall gram fett pr matvare/ingrediens
- Antall gram karbohydrater pr matvare/ingrediens
---------------------------------------------------------------------------
@param indeks: int - nummer på valgt oppskrift
@param *matvareTabell: vector<MatvareTabell>- Dynamisk peker til vector fylt 
med alle matvarer fra matvaretabellen
*/
void Oppskrift::skrivUtValgtOppskriftsInnhold(int indeks, vector<MatvareTabell> *matvareTabell) {
	string navn = "";
	int matvareIndeks;
	double matvareID = 0,
		antGram = 0,
		totGram = 0,
		fett = 0,
		totFett = 0,
		proteiner = 0,
		totProteiner = 0,
		karbohydrater = 0,
		totKarbodyhdrater = 0;
	//trekk fra 1 for å få korrekt indeks
	indeks--;
	vector<Oppskrift> oppskrift = Filbehandling::lesInnEnOppskrift(registrerteOppskrifter.at(indeks));
	navn = registrerteOppskrifter.at(indeks);
	//fjern filtype fra oppskriftens navn (.txt)
	navn.erase(navn.length()-4, 4);
	cout << "Oppskriften \"" << navn << "\" inneholder: " << endl;
	cout << "--------------------------------------------------------------------------------";
	cout << left << setw(30) << "Matvare" << left << setw(10) << "Gram" << left << setw(15) << "Proteiner";
	cout << left << setw(10) << "Fett" << right << setw(10) << "Karbohydrater" << endl;
	cout << "--------------------------------------------------------------------------------";
	//loop gjennom matvarer/ingredienser og skriv ut innholdet 
	for (unsigned int i = 0; i < oppskrift.size(); i++) {
		//hent ut verdiene som er lagret på fil
		matvareID = oppskrift.at(i).getMatvareID();
		antGram = oppskrift.at(i).getGram();
		proteiner = oppskrift.at(i).getProteiner();
		//finn matvarens indeks/posisjon i vector
		finnOppgittMatvareID(matvareTabell, matvareIndeks, matvareID);
		//hent ut og beregn verdier basert på matvaretabellens innhold
		navn = matvareTabell ->at(matvareIndeks).getMatvare();
		fett = matvareTabell ->at(matvareIndeks).getFett();
		fett = beregnNaeringsinnhold(antGram, fett);
		karbohydrater = matvareTabell ->at(matvareIndeks).getKarbohydrat();
		karbohydrater = beregnNaeringsinnhold(antGram, karbohydrater);
		totGram += antGram;
		totFett += fett;
		totProteiner += proteiner;
		totKarbodyhdrater += karbohydrater;
		//skriv ut informasjon på skjerm
		cout << left << setw(30) << navn << left << setw(10) << antGram << left << setw(15) << proteiner;
		cout << left << setw(10) << fett << right << setw(10) << karbohydrater << endl;
	} //for
	//oppsummering som viser rettens totalinnhold
	cout << "--------------------------------------------------------------------------------";
	cout << left << setw(30) << "Totalt: " << left << setw(10) << totGram << left << setw(15) << totProteiner;
	cout << left << setw(10) << totFett << right << setw(10) << totKarbodyhdrater << endl;
	cout << "--------------------------------------------------------------------------------";
	cout << endl;
} //skrivUtValgtOppskriftsInnhold
#pragma endregion

#pragma region OPPRETT/REGISTRER NY OPPSKRIFT
/*
Denne funksjonen oppretter en oppskrift.
Den ber bruker om å oppgi matvareID, antall gram og navn på oppskrift.
Det kontrolleres om input er gyldig og om matvare ID eksisterer i 
matvaretabellen. Hvis alt er ok, så skrives oppskrift til fil og 
filnavn legges til i en vector<string> som tar vare på filnavne til 
alle eksisterende/opprettede oppskrifter.
--------------------------------------------------------------------
@param antIngredienser: int - Antall ingredienser/matvarer som skal brukes 
@param *matvareTabell: vector<MatvareTabell>- Dynamisk peker til vector fylt 
med alle matvarer fra matvaretabellen
*/
void Oppskrift::opprettOppskrift(int antIngredienser, vector<MatvareTabell> *matvareTabell) {
	int indeks = -1;
	string filnavn;
	double matvareID = 0, 
		fett = 0,
		antGram = 0, 
		protein = 0,
		karbohydrater = 0,
		totFett = 0,
		totProtein = 0,
		totKarbohydrater = 0;
	vector<Oppskrift> nyOppskrift;
	//spør bruker etter matvarer basert på oppgitt antall ingredienser
	for (int i = 0; i < antIngredienser; i++) {
		//spør bruker etter matvareID og antall gram
		matvareID = oppgiMatvareID(matvareTabell, indeks);
		antGram = oppgiAntallGram();
		cout << endl;
		//hent ut protein fra vector, beregn proteiner for denne matvaren
		//og totalt antall proteiner for retten/oppskriften
		protein = matvareTabell->at(indeks).getProtein();
		protein = beregnNaeringsinnhold(antGram, protein);
		totProtein += protein;
		//gjør det samme for fett
		fett = matvareTabell->at(indeks).getFett();
		fett = beregnNaeringsinnhold(antGram, fett);
		totFett += fett;
		//gjør det samme karbohydrater
		karbohydrater = matvareTabell->at(indeks).getKarbohydrat();
		karbohydrater = beregnNaeringsinnhold(antGram, karbohydrater);
		totKarbohydrater += karbohydrater;
		cout << "Totalt antall proteiner for retten er n" << char(134) << ": " << totProtein << "g." << endl;
		cout << "Totalt antall fett for retten er n" << char(134) << ": " << totFett << "g." << endl;
		cout << "Totalt antall karbohydrater for retten er n" << char(134) << ": " << totKarbohydrater << "g." << endl << endl;
		//opprett objekt av denne matvaren, og legg det i vector
		Oppskrift rett(matvareID, antGram, protein);
		nyOppskrift.push_back(rett);
	} //for
	//siden oppskriftens navn kan ha mellomrom/space, 
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
	//hent navn på oppskriften
	filnavn = navngiOppskrift();
	//skriv oppskrift til fil
	Filbehandling::skrivOppskriftTilFil(filnavn, nyOppskrift);
	//oppdater vector som innehar oversikten over oppskrifter
	registrerteOppskrifter.push_back(filnavn);
	cout << "Oppskriften ble lagret til f" << char(155) << "lgende fil: \"" << filnavn << "\"." << endl;
} //opprettOppskrift

/*
Denne funksjonen ber bruker om å oppgi gjeldende ingrediens sin matvareID.
Dersom input ikke er tallverdi, blir bruker bedt om å skrive inn ID på nytt.
Når gyldig ID er innskrevet, sjekkes det mot vector<MatvareTabell> om oppgitt 
ID finnes. Dersom oppgitt ID ikke finnes, blir bruker bedt om å skrive inn ID 
på nytt. Hvis ID finnes, får bruker melding om dette og ID returneres.
-----------------------------------------------------------------------------
@param *matvareTabell: vector<MatvareTabell> - Dynamisk peker til vector fylt 
med alle matvarer fra matvaretabellen
@param &indeks: int - referanseoverføring for å finne matvarens posisjonen i vector
@return: double - valgt matvares ID
*/
double Oppskrift::oppgiMatvareID(vector<MatvareTabell> *matvareTabell, int &indeks) {
	string input = "";
	double matvareID = 0;
	bool idFunnet = false;
	//spør bruker etter matvareID
	string ledetekst = "Skriv inn matvareID (siffer 0-9, bruk punktum som desimaltegn).\n";
	ledetekst += "MatvareID: ";
	cout << ledetekst;
	cin >> input;
	//konverter input til double (kjør løkke til gyldig tallverdi er innskrevet)
	matvareID = Konvertering::konverterStringTilDouble(input);
	while (matvareID == Konvertering::INPUT_ER_IKKE_ET_TALL) {
		cout << ledetekst;
		cin >> input;
		matvareID = Konvertering::konverterStringTilDouble(input);
	} //while
	idFunnet = finnOppgittMatvareID(matvareTabell, indeks, matvareID);
	//ble den oppgitte matvareID'n funnet?
	if (!idFunnet) {
		cout << endl;
		//oppgitt matvareID ikke funnet - be bruker om å skrive inn ID på nytt
		cout << "Beklager. MatvareID'n du oppga (" << matvareID << ") ble desverre ikke funnet." << endl;
		cout << "Vennligst oppgi matvareID p" << char(134) << " nytt." << endl << endl;
		matvareID = oppgiMatvareID(matvareTabell, indeks);
	} else {
		//fant matvaren, skriv ut id og varenavn slik at bruker vet at den ble funnet
		cout << "Fant matvare med ID (" << matvareID << "): " << matvareTabell->at(indeks).getMatvare() << endl << endl;
	} //if (!idFunnet)
	return matvareID;
} //oppgiMatvareID

/*
Looper gjennom alle matvarene som finnes i vector<MatvareTabell>.
Dersom matvarens ID blir funnet returneres true, hvis ikke false.
Det brukes referanseoverføring for å ta vare på matvarens indeks i 
vector<MatvareTabell>.
------------------------------------------------------------------------------
@param *matvareTabell: vector<MatvareTabell> - Dynamisk peker til vector fylt 
med alle matvarer fra matvaretabellen
@param &indeks: int - referanseoverføring for å finne matvarens posisjonen i vector
@param matvareID: double - ID til matvaren som skal finnes
@return: bool - true: id funnet, false: id ikke funnet
*/
bool Oppskrift::finnOppgittMatvareID(vector<MatvareTabell> *matvareTabell, int &indeks, const double matvareID) {
	bool idFunnet = false;
	unsigned int teller = 0;
	while (!idFunnet && teller < matvareTabell->size()) {
		//er matvareID funnet?
		if (matvareTabell->at(teller).getMatvareID() == matvareID) {
			indeks = teller;
			idFunnet = true;
		} //if (matvareTabell ->at(teller).getMatvareID() == matvareID)
		teller++;
	} //while
	return idFunnet;
} //finnOppgittMatvareID

/*
Denne funksjonen spør bruker etter hvor mange gram som skal brukes 
av valgt matvare. Dersom input feiler, blir bruker bedt om å skrive 
inn verdien på ny.
----------------------------------------------------------------
@return: double - mengden av gitt matvare i gram
*/
double Oppskrift::oppgiAntallGram() {
	string input = "";
	double antGram = 0;
	string ledetekst = "Skriv inn antall gram (siffer 0-9, bruk punktum som desimaltegn).\n";
	ledetekst += "Antall gram: ";
	cout << ledetekst;
	cin >> input;
	antGram = Konvertering::konverterStringTilDouble(input);
	while (antGram == Konvertering::INPUT_ER_IKKE_ET_TALL) {
		cout << ledetekst;
		cin >> input;
		antGram = Konvertering::konverterStringTilDouble(input);
	} //while
	return antGram;
} //oppgiAntallGram

/*
Denne funksjonen beregner ernæringsinnholdet for gitt matvare.
Den deler ernæringsinnholdet på 100, siden oppgitte verdier i matvare 
tabellen er pr. 100g gram, og summen av divisjonen blir deretter 
multiplisert med den totale mengden for gitt matvare.
--------------------------------------------------------------------
@param antGram: double - mengden (i gram) av gitt matvare
@param ernaering: double - ernæring (i gram) av gitt matvare
@return: double - gjeldende ingrediens ernæringsinnhold i gram
*/
double Oppskrift::beregnNaeringsinnhold(double antGram, double ernaering) {
	double antErnaering = 0;
	//siden tabell er pr. 100g, omgjør ernæring til 1g
	antErnaering = ernaering / 100;
	//beregn antallet ernæring for denne matvaren
	antErnaering = antGram * antErnaering;
	return antErnaering;
} //beregnNaeringsinnhold

/*
Denne funksjonen spør bruker etter navn på oppskrift og
returnerer resultatet. For innlesning brukes getline for å 
inkludere mellomrom/space. Dersom oppskriftsnavn er blankt, 
blir bruker bedt om å skrive inn navn på nytt. Det kontrolleres 
også at innskrevet navn ikke eksisterer fra før.
----------------------------------------------------------
@return: string - navn på den nye oppskriften
*/
string Oppskrift::navngiOppskrift() {
	unsigned int teller = 0;
	bool finnesOppskrift = false;
	string navnPaaOppskrift = "";
	//spør etter navn på oppskrift
	cout << "Skriv inn navn p" << char(134) <<" oppskrift: ";
	//tømmer cin for tidligere input
	cin.clear();
	//les inn navn på oppskrift
	getline(cin, navnPaaOppskrift);
	//hvis intet navn på oppskrift ble skrivd inn
	while (navnPaaOppskrift == "") {
		//input var tomt, gi feilmelding 
		cout << "\nOppskriften m" << char(134) << " ha et navn."<< endl;
		//spør på nytt etter oppskriftens
		navnPaaOppskrift = navngiOppskrift();
	} //while
	//loop gjennom eksisterende filnavn for å sjekke at det ikke finnes 
	//oppskrifter med samme navn
	while (teller < registrerteOppskrifter.size() && !finnesOppskrift) {
		string temp = registrerteOppskrifter.at(teller);
		//fjern filtypen fra navnet
		temp.erase(temp.length()-4, 4);
		if (temp == navnPaaOppskrift) {
			finnesOppskrift = true;
		} //if (temp == navnPaaOppskrift)
		teller++;
	} //while
	//ble oppskrift med samme navn funnet?
	if (finnesOppskrift) {
		cout << "Beklager. Oppgitt navn: \"" << navnPaaOppskrift << "\" finnes fra f" << char(155) << "r." << endl;
		cout << "Vennligst oppgi et nytt navn p" << char(134) << " oppskriften." << endl << endl;
		navnPaaOppskrift = navngiOppskrift();
	} //if (finnesOppskrift)
	return navnPaaOppskrift;
} //navngiOppskrift

#pragma endregion Funksjoner tilknyttet oppretting av ny oppskrift + søk etter matvareID

#pragma region LIST OPP OPPSKRIFTER MED MENGDE > X G PR HUNDRE GRAM RETT
/*
Denne funksjonen henter ut oppskrifter som inneholder en ernæringstype 
høyere eller lik verdi oppgitt av bruker. Parameteren type er selve 
menyvalget, altså hvilken av verdiene som skal sammenlignes og eventuelt 
opplistes blant de eksisterende oppskriftene.
Kort oppsummert: hent ut oppskrifter som inneholder mer enn X gram ernæringstype
----------------------------------------------------------------------
@param type: int - ernæringenstypen (menyvalget) som skal hentes/sammenlignes
@param *matvareTabell: vector<MatvareTabell> - Dynamisk peker til vector fylt 
med alle matvarer fra matvaretabellen
*/
void Oppskrift::oppskrifterMedInnholdStorreEnnInput(int type, vector<MatvareTabell> *matvareTabell) {
	string ledetekst = "",
		verdiSomSkalFinnes = "";
	switch(type) {
	case HENT_RETTER_MED_MER_ENN_X_GRAM_PROTEIN:
		verdiSomSkalFinnes = "protein";
		ledetekst = "Oppgi hvor mange gram proteiner retten(e) skal inneholde pr 100g ferdig rett.\n";
		ledetekst += "Antall gram proteiner (siffer 0-9, punktum som desimaltegn): ";
		break;
	case HENT_RETTER_MED_MER_ENN_X_GRAM_FETT:
		verdiSomSkalFinnes = "fett";
		ledetekst = "Oppgi hvor mange gram fett retten(e) skal inneholde pr 100g ferdig rett.\n";
		ledetekst += "Antall gram fett (siffer 0-9, punktum som desimaltegn): ";
		break;
	case HENT_RETTER_MED_MER_ENN_X_GRAM_KARBOHYDRATER:
		verdiSomSkalFinnes = "karbohydrat";
		ledetekst = "Oppgi hvor mange gram karbohydrater retten(e) skal inneholde pr 100g ferdig rett.\n";
		ledetekst += "Antall gram karbohydrater (siffer 0-9, punktum som desimaltegn): ";
		break;
	case HENT_RETTER_MED_MER_ENN_X_GRAM_KALORIER:
		verdiSomSkalFinnes = "kalorier";
		ledetekst = "Oppgi hvor mange gram kalorier retten(e) skal inneholde pr 100g ferdig rett.\n";
		ledetekst += "Antall gram kalorier (siffer 0-9, punktum som desimaltegn): ";
		break;
	} //switch
	//hent verdier og skriv ut resultatet
	hentOppskrifterMedMengdeX(type, ledetekst, verdiSomSkalFinnes, matvareTabell);
} //oppskrifterMedInnholdStorreEnnInput

/*
Her blir bruker bedt om input (oppgi mengden i gram) og deretter leses 
oppskriftene inn og ingredienser/matvarers verdier blir hentet ut, 
akkumelert og sammenlignet med verdi oppgitt av bruker. Dersom verdi 
er lik eller høyere verdi oppgitt fra bruker, blir den lagt inn i en 
vector<Oppskrift> og til slutt sjekkes det om det fantes oppskrifter 
som matcher og resultat skrives ut på skjerm.
---------------------------------------------------------------------------
@param type: int - ernæringenstypen (menyvalget) som skal hentes/sammenlignes
@param ledetekst: string - Tekst som forteller bruker hva som skal gjøres
@param verdiSomSkalFinnes: string - beskriver hva det letes etter (eks: protein)
@param *matvareTabell: vector<MatvareTabell> - Dynamisk peker til vector fylt 
med alle matvarer fra matvaretabellen
*/
void Oppskrift::hentOppskrifterMedMengdeX(int type, string ledetekst, string verdiSomSkalFinnes, vector<MatvareTabell> *matvareTabell) {
	string input = "";
	int indeks = -1;
	double totMengde = 0, 
		matvareID = 0;
	vector<Oppskrift> temp,
		match;
	double mengdeX = 0;
	//be bruker om input og sjekk at input er korrekt
	cout << ledetekst;
	cin >> input;
	mengdeX = Konvertering::konverterStringTilDouble(input);
	cout << endl;
	//hvis input ikke er korrekt, be bruker skrive inn verdi på nytt
	while (mengdeX == Konvertering::INPUT_ER_IKKE_ET_TALL) {
		cout << ledetekst;
		cin >> input;
		mengdeX = Konvertering::konverterStringTilDouble(input);
		cout << endl;
	} //while
	//loop gjennom oppskriftsfiler 
	for (unsigned int i = 0; i < registrerteOppskrifter.size(); i++) {
		//hent ut oppskriften fra fil og beregn rettens mengde
		temp = Filbehandling::lesInnEnOppskrift(registrerteOppskrifter.at(i));
		for (unsigned int j = 0; j < temp.size(); j++) {
			//hent ut gjeldende ingrediens sin ID og finn indeksen i vector
			matvareID = temp.at(j).getMatvareID();
			finnOppgittMatvareID(matvareTabell, indeks, matvareID);
			//hvilken ernæringstype skal det sjekkes mot?
			switch (type) {
			case HENT_RETTER_MED_MER_ENN_X_GRAM_PROTEIN:
				//henter protein rett ut fra oppskriftsfil
				totMengde += temp.at(j).getProteiner();
				break;
			case HENT_RETTER_MED_MER_ENN_X_GRAM_FETT:
				//hent ut verdien fra matvaretabellen og del verdien på 100 for å få verdi for 1g
				//deretter multipliser verdien med mengden til gjeldende matvare/ingrediens
				totMengde += (matvareTabell->at(indeks).getFett() / 100) * temp.at(j).getGram();
				break;
			case HENT_RETTER_MED_MER_ENN_X_GRAM_KARBOHYDRATER:
				totMengde += (matvareTabell->at(indeks).getKarbohydrat() / 100) * temp.at(j).getGram();
				break;
			case HENT_RETTER_MED_MER_ENN_X_GRAM_KALORIER:
				totMengde += (matvareTabell->at(indeks).getKiloJoule() / 100) * temp.at(j).getGram();
				break;
			} //switch
		} //indre for
		//er den totale mengden det bruker ønsker å finne?
		if (totMengde >= mengdeX) {
			Oppskrift oppskrift(registrerteOppskrifter.at(i), totMengde);
			match.push_back(oppskrift);
		} //if (totMengde >= mengdeX) 
		//nullstill verdien for neste runde
		totMengde = 0;
	} //ytre for
	//ble oppskrifter funnet?
	if (match.size() == 0) {
		cout << "Fant ingen oppskrifter med " << verdiSomSkalFinnes << "mengde st" << char(155) << "rre enn " << mengdeX << "g." << endl;
	} else { 
		string navn = "";
		//oppskrift ble funnet, skriv ut oppskriftene
		cout << "F" << char(155) << "lgende oppskrifter inneholder " << mengdeX << "g (eller mer) " << verdiSomSkalFinnes << " pr 100g ferdig rett:" << endl;
		cout << "--------------------------------------------------------------------------------";
		for (unsigned int i = 0; i < match.size(); i++) {
			navn = match.at(i).getOppskriftsNavn();
			navn.erase(navn.length()-4, 4);
			cout << "Oppskrift: \"" << navn << "\" med " << verdiSomSkalFinnes << "mengde " << match.at(i).getTotalMengde() << "g." << endl;
		} //for
	} //if (match.size() == 0)
} //hentOppskrifterMedMengdeX
#pragma endregion

#pragma region LIST OPP OPPSKRIFTER SOM OVERSKRIDER ANBEFALT DAGLIG INNTAK
/*
Denne funksjonen går gjennom registrerte oppskrifter og sjekker om de overskrider 
anbefalt daglig inntak av vitaminer eller kolesterol. Hvilken det sjekkes mot avhenger 
av ernæringstypen som blir oversendt. Til slutt skrives resultatet ut på skjerm.
Kort oppsummert: Hent ut oppskrifter som overskrider daglig inntak av ernæringstype
----------------------------------------------------------------------------------
@param type: int - ernæringenstypen (menyvalget) som skal hentes/sammenlignes
@param *matvareTabell: vector<MatvareTabell> - Dynamisk peker til vector fylt 
med alle matvarer fra matvaretabellen
*/
void Oppskrift::oppskrifterMedInnholdOverDagligInntak(int type, vector<MatvareTabell> *matvareTabell) {
	vector<Oppskrift> match;
	string mengde = "",
		verdiSomSkalFinnes = "";
	string utskrift = "F";
	utskrift += char(155);
	utskrift += "lgende oppskrifter inneholder verdier over daglig anbefalt inntak av ";
	//hvilken ernæringstype skal sjekkes mot daglig inntak
	switch(type) {
	case HENT_RETTER_MED_MYE_VITAMIN_A:
		mengde = "RAE";
		verdiSomSkalFinnes = "VitaminA";
		match = returnerOppskrifterSomOverskriderDAI(type, DAGLIG_ANBEFALT_INNTAK_VITAMIN_A, matvareTabell);
		break;
	case HENT_RETTER_MED_MYE_VITAMIN_D:
		mengde = "µg";
		verdiSomSkalFinnes = "VitaminD";
		match = returnerOppskrifterSomOverskriderDAI(type, DAGLIG_ANBEFALT_INNTAK_VITAMIN_D, matvareTabell);
		break;
	case HENT_RETTER_MED_MYE_KOLESTEROL:
		mengde = "mg";
		verdiSomSkalFinnes = "kolesterol";
		match = returnerOppskrifterSomOverskriderDAI(type, DAGLIG_ANBEFALT_INNTAK_KOLESTEROL, matvareTabell);
		break;
	} //switch
	//er det noen oppskrifter hvis innhold er over daglig inntak?
	if (match.size() == 0) {
		cout << "Fant ingen oppskrifter hvis verdier er over anbefalt daglig inntak." << endl;
	} else { //fant oppskrifter
		string navn = "";
		cout << utskrift << verdiSomSkalFinnes << endl;
		cout << "---------------------------------------------------------------------------" << endl;
		for (unsigned int i = 0; i < match.size(); i++) {
			navn = match.at(i).getOppskriftsNavn();
			navn.erase(navn.length()-4, 4);
			cout << "Oppskrift: \"" << navn << "\" med total mengde ";
			cout << verdiSomSkalFinnes << " " << match.at(i).getTotalMengde() << mengde << "." << endl;
		} //for
	} //if (match.size() == 0)
} //oppskrifterMedInnholdOverDagligInntak

/*
Denne funksjonen looper gjennom eksisterende oppskriftsfiler og leser 
inn ingrediensenes matvare ID. ID'n blir deretter brukt for å hente ut 
verdiene til matvaren fra vector<MatvareTabell> som blir akkumelert. 
Deretter sjekkes det om totalverdien for gitt ernæringstype overskrider 
anbefalt daglig inntak. Hvis totalverdi overskrider anbefalt inntak, blir 
oppskriftens navn og totalverdi lagt inn i vector<Oppskrift>, som til 
slutt returneres når alle oppskrifter er gjennomgått.
------------------------------------------------------------------
@param type: int - ernæringenstypen (menyvalget) som skal hentes/sammenlignes
@param dagligInntak - double
@param *matvareTabell: vector<MatvareTabell> - Dynamisk peker til vector fylt 
med alle matvarer fra matvaretabellen
@return: vector<Oppskrift> - 
*/
vector<Oppskrift> Oppskrift::returnerOppskrifterSomOverskriderDAI(int type, const double dagligInntak, vector<MatvareTabell> *matvareTabell) {
	int indeks = -1;
	double matvareID = 0,
		totMengde = 0;
	vector<Oppskrift> temp,
		match;
	//loop gjennom oppskriftsfiler 
	for (unsigned int i = 0; i < registrerteOppskrifter.size(); i++) {
		//hent ut oppskriften fra fil og beregn rettens mengde
		temp = Filbehandling::lesInnEnOppskrift(registrerteOppskrifter.at(i));
		for (unsigned int j = 0; j < temp.size(); j++) {
			//hent ut gjeldende ingrediens sin ID og finn indeksen i vector
			matvareID = temp.at(j).getMatvareID();
			finnOppgittMatvareID(matvareTabell, indeks, matvareID);
			switch (type) {
			case HENT_RETTER_MED_MYE_VITAMIN_A:
				//hent ut verdien fra matvaretabellen og del verdien på 100 for å få verdi for 1g
				//deretter multipliser verdien med mengden til gjeldende matvare/ingrediens
				totMengde += (matvareTabell ->at(indeks).getVitaminA() / 100) * temp.at(j).getGram();
				break;
			case HENT_RETTER_MED_MYE_VITAMIN_D:
				totMengde += (matvareTabell ->at(indeks).getVitaminD() / 100) * temp.at(j).getGram();
				break;
			case HENT_RETTER_MED_MYE_KOLESTEROL:
				totMengde += (matvareTabell ->at(indeks).getKolesterol() / 100) * temp.at(j).getGram();
				break;
			} //switch
		} //indre for
		if (totMengde > dagligInntak) {
			Oppskrift oppskrift(registrerteOppskrifter.at(i), totMengde);
			match.push_back(oppskrift);
		} //if (totMengde > dagligInntak)
		//nullstill verdi
		totMengde = 0;
	} //ytre for
	return match;
} //returnerOppskrifterSomOverskriderDAI
#pragma endregion

double Oppskrift::getMatvareID() {
	return _matvareID;
} //getMatvareID

double Oppskrift::getGram() {
	return _gram;
} //getGram

double Oppskrift::getProteiner() {
	return _proteiner;
} //getProteiner

double Oppskrift::getTotalMengde() {
	return _totalMengde;
} //getTotalMengde

string Oppskrift::getOppskriftsNavn() {
	return _oppskriftsNavn;
} //getOppskriftsNavn