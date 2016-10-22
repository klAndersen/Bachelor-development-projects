#include <iostream>
#include <fstream>
#include <string>
#include <cassert>
#include <vector>
#include "matvareTabell.h"
#include "oppskrift.h"

using namespace std;

#ifndef FILBEHANDLING_H

#define FILBEHANDLING_H
/*
Som navnet tilsier, er dette en klasse som tar for seg filbehandling.
Med dette menes at klassen leser og skriver til fil, og tar for seg 
andre nødvendige filoperasjoner.
--------------------------------------------------------------------
@Author: Knut Lucas Andersen
*/
class Filbehandling { 
public:
	//konstruktør
	Filbehandling();
	//destruktør for opprydding
	~Filbehandling();
	//returner en peker til vector fylt med verdiene fra filen "Matvaretabellen_2012.csv"
	vector<MatvareTabell> *lesFraMatvareTabell();
	//inneholder navn på alle registrerte oppskrifter
	static vector<string> lesInnOppskriftsTitler();
	//leser inn ingrediensene fra en fil
	static vector<Oppskrift> lesInnEnOppskrift(string filnavn);
	//skriver en ny oppskrift til fil
	static void skrivOppskriftTilFil(string &filnavn, vector<Oppskrift> nyOppskrift);
private:
	//funksjon som oppretter fil, i tilfelle fil ikke finnes
	static void opprettFil(string filnavn);
	//leser antall linjer i fil for å sjekke om fil er tom
	static int antallLinjerITekstfil(ifstream &innfil);
	//fil som inneholder matvaretabellen
	static const string MATVARE_TABELL_FILNAVN;
	//fil som inneholder liste over oppførte oppskrifter ("innholdsliste")
	static const string EKSISTERENDE_OPPSKRIFTER_FILNAVN;
	//peker som inneholder verdiene til alle matvarene
	static vector<MatvareTabell> *matVektor;
}; //Filbehandling

#endif