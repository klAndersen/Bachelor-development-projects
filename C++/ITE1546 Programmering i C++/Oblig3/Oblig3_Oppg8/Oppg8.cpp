#include <iostream>
#include <fstream>
#include <string>
using namespace std;

//funksjonsprototyper
void lesFraFil();

//konstanter
const string FILNAVN = "trekanter.txt";

int main() {
	lesFraFil();
	return 0;
} //main

void lesFraFil() {
		ifstream innFil;
	//konverter filnavn til c-string og �pne fil
	innFil.open(FILNAVN.c_str());
	//ble filen �pnet?
	if (innFil.fail()) {
		cout << "Kan ikke " << (char)134 << "pne fil: " << FILNAVN << "!" << endl;
	} else { //fil ble �pnet
		//les og hent ut innhold fra fil		
		hentInnhold(innFil);
		//lukk filen
		innFil.close();
	} //if (innFil.fail())
} //lesFraFil

void hentInnhold(ifstream innFil) {
		string filInnhold;
		//start � lese det faktiske innholdet fra filen
		while (!innFil.eof()) {				
			//les linje for linje fra tekstfil, adskilt med tab
			getline(innFil, filInnhold, '\t');
		} //while
} //