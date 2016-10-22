#include "matvareTabell.h"

MatvareTabell::MatvareTabell() {
} //konstruktør

MatvareTabell::MatvareTabell(double matvareID, string matvare, double vann, double kiloJoule, double fett, double kolesterol,
	double karbohydrat, double kostfiber, double protein, double vitaminA, double vitaminD, double vitaminE, double vitaminC) {
		_matvareID = matvareID;
		_matvare = matvare;
		_vann = vann;
		_kiloJoule = kiloJoule;
		_fett = fett;
		_kolesterol = kolesterol;
		_karbohydrat = karbohydrat;
		_kostfiber = kostfiber;
		_protein = protein;
		_vitaminA = vitaminA;
		_vitaminD = vitaminD;
		_vitaminE = vitaminE;
		_vitaminC = vitaminC;
} //konstruktør

double MatvareTabell::getMatvareID() {
	return _matvareID;
} //getMatvareID

string MatvareTabell::getMatvare() {
	return _matvare;
} //getMatvare

double MatvareTabell::getVann() {
	return _vann;
} //getVann

double MatvareTabell::getKiloJoule() {
	return _kiloJoule;
} //getKiloJoule

double MatvareTabell::getFett() {
	return _fett;
} //getFett

double MatvareTabell::getKolesterol() {
	return _kolesterol;
} //getKolesterol

double MatvareTabell::getKarbohydrat() {
	return _karbohydrat;
} //getKarbohydrat

double MatvareTabell::getKostfiber() {
	return _kostfiber;
} //getKostfiber

double MatvareTabell::getProtein() {
	return _protein;
} //getProtein

double MatvareTabell::getVitaminA() {
	return _vitaminA;
} //getVitaminA

double MatvareTabell::getVitaminD() {
	return _vitaminD;
} //getVitaminD

double MatvareTabell::getVitaminE() {
	return _vitaminE;
} //getVitaminE

double MatvareTabell::getVitaminC() {
	return _vitaminC;
} //getVitaminC