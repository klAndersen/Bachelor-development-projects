#include <iostream>
#include <string>
#include <sstream>

using namespace std;

#ifndef KONVERTERING_H

#define KONVERTERING_H
/*
Klasse som tar for seg konvertering av 
string til tall og vica versa. 
Dersom string ikke kan konverteres, 
returneres en konstant med default verdi.
-----------------------------------------
@Author: Knut Lucas Andersen
*/
const class Konvertering {
public:
	//tom konstruktør
	Konvertering();
	//konverter integer til string
	static string konverterIntegerTilString(int tall);
	//konverter double til string
	static string konverterDoubleTilString(double tall);
	//konverter string til integer
	static int konverterStringTilInteger(string tall);
	//konverter string til double
	static double konverterStringTilDouble(string tall);
	//konstant som tilsier at input (string) ikke har tallverdi
	static const int INPUT_ER_IKKE_ET_TALL;
}; //Konvertering

#endif