/*
--------------------------------------------------------------------------------------
Dette er et program som er utviklet basert på en karaktergivende programmeringsoppgave. 
Oppgaven ble gitt i faget ITE1546 - Programmering i C++.
--------------------------------------------------------------------------------------
Oppgaven baserte seg på å utvikle et system som:

- Er menybasert
- Leser inn matvarers ernæringsinformasjon fra en Excel-fil ved oppstart
- Leser inn en liste over over eksisterende oppskrifter
- Skriver nye oppskrifter til egen fil (ny fil for hver oppskrift)
- Kalkulerer ernæringsinformasjon til oppgitt oppskrift basert på matvarene brukt
- Skriver ut rapporter basert på følgende kriterier:
	--> Alle registrerte retter/oppskrifter listes opp på skjermen.
	--> Retter med mer enn x (bestemmes av bruker) g protein, fett 
		eller karbohydrater per 100g ferdig rett
	--> Retter med mer enn x (bestemmes av bruker) kalorier per 100g ferdig rett
	--> Retter med mye D-vitamin (i forhold til daglig anbefalt inntak)
	--> Retter med mye A-vitamin (i forhold til daglig anbefalt inntak)
	--> Retter med mye kolesterol (i forhold til daglig anbefalt inntak)
--------------------------------------------------------------------------------------
Kilden for matvaretabellen er hentet fra:
Matvaretabellen 2012. Mattilsynet, Helsedirektoratet og Universitetet i Oslo.					
http://www.matvaretabellen.no
http://www.matportalen.no/verktoy/matvaretabellen/#tabs-1-5-anchor
--------------------------------------------------------------------------------------
Kilde for daglig inntak av vitaminer
http://helsedirektoratet.no/publikasjoner/norske-anbefalinger-for-ernering-og-fysisk-aktivitet/Publikasjoner/norske-anbefalinger-for-ernering-og-fysisk-aktivitet.pdf
--------------------------------------------------------------------------------------
Kilde for daglig inntak av kolesterol
http://o.oolco.com/administrerende-kolesterol/kolesterol-og-mat
--------------------------------------------------------------------------------------
@Author: Knut Lucas Andersen
@Date: 30.04.2013
@Place: Narvik
--------------------------------------------------------------------------------------
*/

#include <iostream>
#include <vector>
#include <string>
#include <new>
#include <cstdlib>
#include "filbehandling.h"
#include "oppskrift.h"
#include "matvareTabell.h"
#include "konvertering.h"

using namespace std;

//viser menyen til bruker
void visMeny();
//henter brukers ønskede menyvalg
void hentMenyValg(int menyValg);
//bruker vil registrere en ny oppskrift
void opprettNyOppskrift();

//variabel som tar vare på registrerte oppskrifter (navnene)
Oppskrift oppskrift;
//opprett peker til vector som inneholder matvare-informasjonen
vector<MatvareTabell> *matvareTabell;

int main() {
	try {
		int valg = -1;
		string input = "";
		Filbehandling filBehandling;
		matvareTabell = filBehandling.lesFraMatvareTabell();
		//løkke som kjører frem til bruker avslutter programmet
		while (valg != 0) {
			visMeny();
			//les og (forsøk) konverter input til integer
			cin >> input;
			valg = Konvertering::konverterStringTilInteger(input);
			//"luft"
			cout << endl;
			//var det et tall som ble skrevet inn et tall?
			if (valg == Konvertering::INPUT_ER_IKKE_ET_TALL) {
				cout << "Ugyldig menyvalg. Gyldige valg er 0 - 9, og menyvalg m" << char(134) << " v" << char(145) << "re et siffer (0 - 9)!" << endl << endl;
			} else { //gyldig menyvalg valgt
				hentMenyValg(valg);
			} //if (valg == Konvertering::INPUT_ER_IKKE_ET_TALL)
		} //while
	} catch (bad_alloc) {
		cout << "Kunne ikke allokere minnet!" << endl;
		exit(1);
	} //try/catch
	return 0;
} //main			

void visMeny() {
	string meny = "************************ HOVEDMENY ****************************\n\n";
	meny += "1: Registrer en ny oppskrift. \n";
	meny += "2: List opp alle registrerte oppskrifter.\n";
	meny += "3: List opp retter med mer enn X g protein pr 100g ferdig rett.\n";
	meny += "4: List opp retter med mer enn X g fett pr 100g ferdig rett.\n";
	meny += "5: List opp retter med mer enn X g karbohydrater pr 100g ferdig rett.\n";
	meny += "6: List opp retter med mer enn X g kalorier pr 100g ferdig rett.\n";
	meny += "7: List opp retter med mye A-vitamin (i forhold til daglig anbefalt inntak).\n";
	meny += "8: List opp retter med mye D-vitamin (i forhold til daglig anbefalt inntak).\n";
	meny += "9: List opp retter med mye kolesterol (i forhold til daglig anbefalt inntak).\n";
	meny += "0: Avslutt programmet.\n\n";
	meny += "Kilden for matvaretabellen:\n";
	meny += "Matvaretabellen 2012.\n";
	meny += "Mattilsynet, Helsedirektoratet og Universitetet i Oslo.\n";
	meny += "http://www.matvaretabellen.no\n\n";
	cout << meny << endl;
	cout << "Skriv inn " << (char)155 << "nsket menyvalg (0-9): ";
} //visMeny

void hentMenyValg(int menyValg) {
	switch(menyValg) {
	case 0:
		//programmet skal avsluttes, ikke gjør noe
		break;
	case 1:
		//opprett ny oppskrift
		opprettNyOppskrift();
		break;
	case 2:
		//skriv ut alle eksisterende oppskrifter
		oppskrift.skrivUtRegistrerteOppskrifter(matvareTabell);
		break;
	case 3:
		//list opp retter med mer enn X g protein pr 100g ferdig rett
		//siden dette menyvalget kaller på samme funksjon som menyvalg 4, 5 og 6
		//faller den gjennom disse casene frem til case 6
	case 4:
		//list opp retter med mer enn X g fett pr 100g ferdig rett
	case 5:
		//list opp retter med mer enn X g karbohydrater pr 100g ferdig rett
	case 6:
		//list opp retter med mer enn X g kalorier pr 100g ferdig rett
		oppskrift.oppskrifterMedInnholdStorreEnnInput(menyValg, matvareTabell);
		break;
	case 7:
		//list opp retter med A-vitamin (i forhold til daglig anbefalt inntak)
		//siden dette menyvalget kaller på samme funksjon som menyvalg 8 og 9
		//faller den gjennom disse casene frem til case 9
	case 8:
		//list opp retter med D-vitamin (i forhold til daglig anbefalt inntak)
	case 9:
		//list opp retter med mye kolesterol (i forhold til daglig anbefalt inntak)
		oppskrift.oppskrifterMedInnholdOverDagligInntak(menyValg, matvareTabell);
		break;
	default:
		//innskrevet menyvalg er ikke et oppført alternativ
		cout << "Du skrev inn " << menyValg << ". Gyldig menyvalg er et tall mellom 0 - 9." << endl;
		break;
	} //switch
	//"luft"
	cout << endl;
} //hentMenyValg

void opprettNyOppskrift() {
	string input = "";
	int antIngredienser = 0;
	//spør bruker hvor mange ingredienser som skal registreres
	string ledetekst = "Oppgi antall ingredienser (siffer 0-9): ";
	cout << ledetekst;
	cin >> input;
	antIngredienser = Konvertering::konverterStringTilInteger(input);
	//ble et gyldig tall skrevet inn?
	while (antIngredienser == Konvertering::INPUT_ER_IKKE_ET_TALL) {
		cout << ledetekst;
		cin >> input;
		antIngredienser = Konvertering::konverterStringTilInteger(input);
	} //while
	cout << endl;
	oppskrift.opprettOppskrift(antIngredienser, matvareTabell);
} //opprettNyOppskrift