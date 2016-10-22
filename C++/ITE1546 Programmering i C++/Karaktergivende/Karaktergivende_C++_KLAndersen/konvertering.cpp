#include "konvertering.h"

//konstant som settes hvis konvertering til tall feiler
const int Konvertering::INPUT_ER_IKKE_ET_TALL = -999;

Konvertering::Konvertering() {
} //konstruktør

/*
Denne funksjonen tar et heltall av typen integer og 
konverterer det til string.
---------------------------------------------------------
@param tall: integer - tall som skal konverteres til string
@return: string - resultatet av konverteringen
*/
string Konvertering::konverterIntegerTilString(int tall) {
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

/*
Denne funksjonen tar et heltall av typen double og 
konverterer det til string.
---------------------------------------------------------
@param tall: double - tall som skal konverteres til string
@return: string - resultatet av konverteringen
*/
string Konvertering::konverterDoubleTilString(double tall) {
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

/*
Denne funksjonen tar en verdi av typen string og 
konverterer det til integer. Dersom verdi ikke kan 
konverteres returneres -999
---------------------------------------------------------
@param tall: string - verdi (tall) som skal konverteres til integer
@return: int || -999 hvis konvertering feiler
*/
int Konvertering::konverterStringTilInteger(string tall) {
	int resultat = 0;
	//les inn stringverdi som skal konverteres
	stringstream konverterTilInteger(tall);
	//kan verdien konverteres?
	if (!(konverterTilInteger >> resultat)) {
		//kunne ikke konverteres, sett verdi til -999
		resultat = INPUT_ER_IKKE_ET_TALL;
	} //if (!(konverterTilInteger >> resultat))
	return resultat;
} //konverterStringTilInteger

/*
Denne funksjonen tar en verdi av typen string og 
konverterer det til double. Dersom verdi ikke kan 
konverteres returneres -999
---------------------------------------------------------
@param tall: string - verdi (tall) som skal konverteres til double
@return: double || -999 hvis konvertering feiler
*/
double Konvertering::konverterStringTilDouble(string tall) {
	double resultat = 0;
	//les inn stringverdi som skal konverteres
	stringstream konverterTilDouble(tall);
	//kan verdien konverteres?
	if (!(konverterTilDouble >> resultat)) {
		//kunne ikke konverteres, sett verdi til null
		resultat = static_cast<double>(INPUT_ER_IKKE_ET_TALL);
	} //if (!(konverterTilDouble >> resultat))
	return resultat;
} //konverterStringTilDouble