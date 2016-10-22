#include <iostream>
#include <string>
#include <vector>
#include <iomanip>
#include "matvareTabell.h"

using namespace std;

#ifndef OPPSKRIFT_H

#define OPPSKRIFT_H
/*
Denne klassen håndterer oppretting av ny oppskrift, 
og utskrift av eksisterende (rapporter).
@Author: Knut Lucas Andersen
*/
class Oppskrift {
public:
	//konstruktør
	Oppskrift();
	//konstruktør for registrering av ny oppskrift
	Oppskrift(double matvareID, double gram, double proteiner);
	//konstruktør for uthenting av oppskrifter med mer enn X gram pr 100g ferdig rett
	Oppskrift(string oppskriftsNavn, double totalMengde);
	//skriver ut alle registrerte oppskrifter
	void skrivUtRegistrerteOppskrifter(vector<MatvareTabell> *matvareTabell);
	//oppretter en ny oppskrift, hvor antall matvarer = antIngredienser
	void opprettOppskrift(int antIngredienser, vector<MatvareTabell> *matvareTabell);
	//hent ut oppskrifter som inneholder mer enn X gram
	void oppskrifterMedInnholdStorreEnnInput(int type, vector<MatvareTabell> *matvareTabell);
	//hent ut oppskrifter som overskrider daglig inntak
	void oppskrifterMedInnholdOverDagligInntak(int type, vector<MatvareTabell> *matvareTabell);
	//returnerer matvareID
	double getMatvareID();
	//returnerer gram
	double getGram();
	//returnerer proteiner
	double getProteiner();
	//returnerer totalmengde til oppskrift med mer enn X g 100g rett
	double getTotalMengde();
	//returnerer navnet på oppskriften med mer enn X g pr 100g rett
	string getOppskriftsNavn();
private:
	//ber bruker om å oppgi navn på oppskriften
	string navngiOppskrift();
	//ber bruker om å oppgi antall gram
	static double oppgiAntallGram();
	//ber bruker om å oppgi matvareID og sjekker om ID finnes
	static double oppgiMatvareID(vector<MatvareTabell> *matvareTabell, int &indeks);
	//beregner ernæringsinnholdet på oppskrift som registreres
	static double beregnNaeringsinnhold(double antGram, double protein);
	//funksjon som returnerer ledeteksten som fremvises under opplisting av eksisterende oppskrifter
	string skrivUtLedetekstTilOppskriftsRapport();
	//skriver ut innholdet (matvarer/ingredienser) til valgt oppskrift
	void skrivUtValgtOppskriftsInnhold(int indeks, vector<MatvareTabell> *matvareTabell);
	//søker gjennom dynamisk peker etter oppgitt matvares ID og returnerer true hvis ID ble funnet
	static bool finnOppgittMatvareID(vector<MatvareTabell> *matvareTabell, int &indeks, const double matvareID);
	//funksjon som ber bruker oppgi mengde av gitt ernæringstype og henter matchende oppskrifter
	void hentOppskrifterMedMengdeX(int type, string ledetekst, string verdiSomSkalFinnes, vector<MatvareTabell> *matvareTabell);
	//returnerer oppskrifter som overskrider daglig inntak (resultatet baseres på typen som sendes over)
	vector<Oppskrift> returnerOppskrifterSomOverskriderDAI(int type, const double dagligInntak, vector<MatvareTabell> *matvareTabell);
	//menyvalg: hent rettermed mer enn x (bestemmes av bruker) g protein
	static const int HENT_RETTER_MED_MER_ENN_X_GRAM_PROTEIN;
	//menyvalg: hent rette rmed mer enn x (bestemmes av bruker) g fett
	static const int HENT_RETTER_MED_MER_ENN_X_GRAM_FETT;
	//menyvalg: hent retter med mer enn x (bestemmes av bruker) g karbohydrater
	static const int HENT_RETTER_MED_MER_ENN_X_GRAM_KARBOHYDRATER;
	//menyvalg: hent retter med mer enn x (bestemmes av bruker) g kalorier
	static const int HENT_RETTER_MED_MER_ENN_X_GRAM_KALORIER;
	//menyvalg: hent retter med som overskrider daglig inntak av vitaminA
	static const int HENT_RETTER_MED_MYE_VITAMIN_A;
	//menyvalg: hent retter med som overskrider daglig inntak av vitaminD
	static const int HENT_RETTER_MED_MYE_VITAMIN_D;
	//menyvalg: hent retter med som overskrider daglig inntak av vitaminE
	static const int HENT_RETTER_MED_MYE_VITAMIN_E;
	//menyvalg: hent retter med som overskrider daglig inntak av vitaminC
	static const int HENT_RETTER_MED_MYE_VITAMIN_C;
	//menyvalg: hent retter med som overskrider daglig inntak av kolesterol
	static const int HENT_RETTER_MED_MYE_KOLESTEROL;
	//konstant for daglig inntak av kolesterol
	static const double DAGLIG_ANBEFALT_INNTAK_KOLESTEROL;
	//konstant for daglig inntak av vitaminA
	static const double DAGLIG_ANBEFALT_INNTAK_VITAMIN_A;
	//konstant for daglig inntak av vitaminD
	static const double DAGLIG_ANBEFALT_INNTAK_VITAMIN_D;
	//konstant for daglig inntak av vitaminE
	static const double DAGLIG_ANBEFALT_INNTAK_VITAMIN_E;
	//konstant for daglig inntak av vitaminC
	static const double DAGLIG_ANBEFALT_INNTAK_VITAMIN_C;
	//vector som inneholder alle oppskriftene som er lagd
	vector<string> registrerteOppskrifter;
	string _oppskriftsNavn;
	double _matvareID;
	double _totalMengde;
	double _gram;
	double _proteiner;
}; //Oppskrift

#endif