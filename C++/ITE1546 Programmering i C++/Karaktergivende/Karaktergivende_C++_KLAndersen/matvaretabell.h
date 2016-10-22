#include <iostream>
#include <string>

using namespace std;

#ifndef MATVARETABELL_H

#define MATVARETABELL_H
/*
Denne klassen objektifiserer alt som ligger i filen Matvaretabell.
Den inneholder en konstruktør som initialiserer verdiene som finnes 
i filen MatvareTabell, og disse verdiene er tilgjengelig via get-funksjoner.
----------------------------------------------------------------------------
@Author: Knut Lucas Andersen
*/
class MatvareTabell {
public:
	//tom konstruktør
	MatvareTabell();
	//konstruktør som tar vare på alt som finnes i matvaretabell
	MatvareTabell(double matvareID, string matvare, double vann, double kiloJoule, double fett, double kolesterol,
		double karbohydrat, double kostfiber, double protein, double vitaminA, double vitaminD, double vitaminE, double vitaminC);
	double getMatvareID();
	string getMatvare();
	double getVann();
	double getKiloJoule();
	double getFett();
	double getKolesterol();
	double getKarbohydrat();
	double getKostfiber();
	double getProtein();
	double getVitaminA();
	double getVitaminD();
	double getVitaminE();
	double getVitaminC();
private:
	string _matvare;
	double _matvareID, 
		_vann,
		_kiloJoule, 
		_fett,
		_kolesterol, 
		_karbohydrat, 
		_kostfiber, 
		_protein, 
		_vitaminA,
		_vitaminD,
		_vitaminE,
		_vitaminC;
}; //MatvareTabell

#endif