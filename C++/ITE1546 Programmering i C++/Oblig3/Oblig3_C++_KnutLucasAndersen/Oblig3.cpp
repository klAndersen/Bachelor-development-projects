#include <iostream>
#include <cmath>
#include <string>
#include <fstream>
#include <sstream>
using namespace std;

//funksjonsprototyper
void visLedetekst(int indeks);
double beregnTrekantsArealViaHeronsFormel(double a, double b, double c);
double totalAreal(double trekanter[], const int MAKS_BEREGNINGER);
void skrivTilFil();
string konverterDoubleTilString(double verdi);

//konstanter
const int MAKS_BEREGNINGER = 5;
const string FILNAVN = "trekanter.txt";

//arrays
double arrayA[MAKS_BEREGNINGER];
double arrayB[MAKS_BEREGNINGER];
double arrayC[MAKS_BEREGNINGER];
double trekanter[MAKS_BEREGNINGER];

int main() {
	//omgjort til ren for l�kke etter beskrivelse i oppg. 6
	for (int i = 0; i < MAKS_BEREGNINGER; i ++) {
		//be om input og oversend i som indeks/elementplassering
		visLedetekst(i);
	} //for
	double totAreal = totalAreal(trekanter, MAKS_BEREGNINGER);
	cout << "Totalarealet for alle trekantene er: " << totAreal << endl;
	//skriv resultatet til fil
	skrivTilFil();
	return 0;
} //main

void visLedetekst(int indeks) {
	double areal, a, b, c;
	//forklaring til bruker
	cout << "Dette programmet vil sp�rre etter lengden p� sidene til et antall trekanter og beregne og skrive ut arelalet til disse." << endl;
	cout << "Informasjonen vil ogs� skrives til en fil." << endl;
	//sp�r etter input
	cout << "Skriv inn verdien for a: ";
	cin >> a;
	//sp�r etter input
	cout << "Skriv inn verdien for b: ";
	cin >> b;
	//sp�r etter input
	cout << "Skriv inn verdien for c: ";
	cin >> c;
	//legg variabler i array
	arrayA[indeks] = a;
	arrayB[indeks] = b;
	arrayC[indeks] = c;
	//beregn arealet
	areal = beregnTrekantsArealViaHeronsFormel(a, b, c);
	//legg arealet inn i array p� gitt indeks/plassering
	trekanter[indeks] = areal;
	//vis resultatet til bruker
	cout << "Trekantens areal er: " << areal << endl;
} //visLedetekst

double beregnTrekantsArealViaHeronsFormel(double a, double b, double c) {
	double areal, s, underSqrVerdi;
	//beregn s
	s = (a + b + c);
	s = s * 0.5;	
	//beregn verdien under kvadratroten
	underSqrVerdi = s*(s - a)*(s - b)*(s - c);
	//beregn arealet
	areal = sqrt(underSqrVerdi);
	//returner resultatet
	return areal;
} //beregnTrekantsArealViaHeronsFormel

double totalAreal(double trekanter[], const int MAKS_BEREGNINGER) {
	double totAreal = 0;
	for (int i = 0; i < MAKS_BEREGNINGER; i++) {
		totAreal += trekanter[i];
	} //for
	return totAreal;
} //totalAreal

void skrivTilFil() {
	ofstream utfilTrekanter;
	string filInnhold = "";
	//oppretter fil hvis fil ikke finnes
	ofstream opprettFil (FILNAVN, ios::app);
	//lukker fila
	opprettFil.close();
	//l�kke som looper gjennom og henter ut innhold fra array
	for (int i = 0; i < MAKS_BEREGNINGER; i++) {
		//konverter og legg til verdi i string variabel
		filInnhold += konverterDoubleTilString(arrayA[i]) + "\t";
		filInnhold += konverterDoubleTilString(arrayB[i]) + "\t";
		filInnhold += konverterDoubleTilString(arrayC[i]) + "\t";
		filInnhold += konverterDoubleTilString(trekanter[i]) + "\n";
	} //ytre for
	//�pne filen det skal skrives til
	utfilTrekanter.open(FILNAVN.c_str());
	//skriv innhold til fil
	utfilTrekanter << filInnhold;
	//lukk filen
	utfilTrekanter.close();
} //skrivTilFil

string konverterDoubleTilString(double verdi) {
	string resultat;
	ostringstream konverterTilString;
	//les inn tallverdi som skal konverteres
	konverterTilString << verdi;
	//konverter tallet til string
	resultat = konverterTilString.str();
	//t�m konverterer for innhold
	konverterTilString.clear();
	konverterTilString.str("");
	//returner den konverterte stringen
	return resultat;
} //konverterDoubleTilString