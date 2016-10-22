#include <iostream>
#include <vector>
#include <string>
#include <fstream>
#include "Pixel.h";

using namespace std;

//funksjonsprototyper
void visMeny();
void hentMenyValg(vector<Pixel> &pixelTabell, int width, int valg);
void settMerke(vector<Pixel> &pixelTabell, int _row, int _col, int _width, int _rowwidth);
void greyScale(vector<Pixel>&pixelTabell);
void changeColor(vector<Pixel> &pixelTabell, int component, int color);
void newColorRed(vector<Pixel> &pixelTabell, int color);
void newColorGreen(vector<Pixel> &pixelTabell, int color);
void newColorBlue(vector<Pixel> &pixelTabell, int color);
void skrivNyBitmapTilFil(string innfilnavn, unsigned int offset, unsigned int width, vector<Pixel> &pixelTabell);

int main() {
	fstream infile;
	string innfilnavn = "figur1.bmp";	
	unsigned int offset; //Angir starten på pikslene
	unsigned int width; //Bildets bredde
	unsigned int height; //Bildets høyde
	unsigned char header[54]; //Kan holde på hele headeren
	unsigned char enPixel[3]; //Bestående av 3 byte
	//En tabell som vil inneholde alle pikslene i fila:
	vector<Pixel> pixelTabell;
	//Åpner bitmapfila m.m.
	infile.open(innfilnavn.c_str(), ios::binary | ios::in);
	if (infile.fail()) {
		cout << "Feil ved åpning av fil" << endl;
	} else {
		//Leser hele headeren slik at den kan brukes i ny fil:
		infile.read((char *)header,54);
		//Søker frem til 10. byte (fra start):
		infile.seekg(10, ios::beg);
		infile.read((char*)&offset,4);
		infile.seekg(18, ios::beg);
		infile.read((char*)&width,4); //i antall piksler
		infile.read((char*)&height,4); //i antall piksler
		//Plasserer filpeker til starten av pikseldata:
		infile.seekg(offset);
		//NB!Bredden (i antall piksler) på bildet skal være delelig på 4.
		//Dersom bredden på bildet er delelig på 4 vil stuffing være lik 0,
		//ellers vil den bli 1,2 eller 3.
		int stuffing = width % 4;
		//pix holder rede på antall leste piksler per linje
		int pix=0;
		//Leser pikslene (3 byte per piksel):
		for(int i=0; i < (int)(height*width); i++){
			//Leser tre bytes (en piksel) i slengen:
			infile.read((char *)enPixel,3);
			//Øker antall piksler for denne linja med 1:
			pix++;
			//Når antall piksler er lik bredden sjekker vi for evt. stuffing:
			if((pix == width)){
				//p settes lik filpekeren
				int p = infile.tellp();
				//Søker forbi evt. stuffbytes:
				infile.seekg(p + stuffing);
				pix=0;
			} //if
			//Opprett et nytt pikselobjekt og legg i vektor:
			Pixel nyPixel((int)enPixel[2], (int)enPixel[1], (int)enPixel[0]);
			pixelTabell.push_back(nyPixel);
		} //for
		int valg = -1;
		while (valg != 0) {			
			visMeny();
			cin >> valg;
			hentMenyValg(pixelTabell, width, valg);
		} //while
		/*
		settMerke(pixelTabell, 10, 10, 50, width);
		changeColor(pixelTabell, 0, 255);
		greyScale(pixelTabell);
		*/
		skrivNyBitmapTilFil(innfilnavn, offset, width, pixelTabell);
	} //if (infile.fail())
	return 0;
} //main

void visMeny() {
	cout << "****** Bitmapredigering ******" << endl;
	cout << "Hva vil du gj" << char(155) << "re med bildet?" << endl;
	cout << "1: Sette inn en svart firkant p" << char(134) << " oppgitt posisjon" << endl;
	cout << "2: Endre fargen p" << char(134) << " pixlene" << endl;
	cout << "3: Sette alle pixler til gr" << char(134) << " basert p" << char(134) << " pixlenes gjennomsnittsverdi" << endl;
	cout << "0: Avslutt programmet" << endl;	
} //visMeny

void hentMenyValg(vector<Pixel> &pixelTabell, int width, int valg) {
		switch(valg) {
		case 0: 
			break;
		case 1:
			int rad, kolonne, vidde;
			cout << "Skriv inn rad, kolonne og vidde på firkant adskilt med mellomrom:" << endl;
			cin >> rad >> kolonne >> vidde;
			settMerke(pixelTabell, rad, kolonne, vidde, width);
			break;
		case 2:
			int component, color;
			cout << "Skriv inn komponent (0 = R, 1 = G, 2 = B) og fargekoden (0-255) " << endl;
			cout << "adskilt med mellomrom:";
			cin >> component >> color;
			changeColor(pixelTabell, component, color);
			break;
		case 3:
			greyScale(pixelTabell);
			break;
		default:
			cout << "Ugyldig valg. Gyldige valg er et siffer (0-3)." << endl << endl;
			break;
		} //switch
		//luft
		cout << endl;
} //hentMenyValg

/**
Funksjon som lager en svart firkant basert på oppgitte verdier.
----------------------------------------------------------
@param pixelTabell: referanse til vector som inneholder (de endrede) pixlene
@param _row: rad (antatt å være 1-basert!)
@param _col: kolonne (antatt å være 1-basert!)
@param _width: Bredden på bildet i antall pixler
@param _rowwidth: Totale bredden for bildet
*/
void settMerke(vector<Pixel> &pixelTabell, int _row, int _col, int _width, int _rowwidth) {
	// _row og _col er antatt å være 1-basert!
	// _width er bredden på bildet i antall piksler.
	// idx representerer indeks i tabell:
	int idx;
	//sr=start row, sc=start column
	//er=end row, ec=end column
	int sr = _row, sc=_col, er=sr+_width, ec=sc+_width;
	for (int i=sr; i < er; i++) {
		for (int j=sc; j < ec; j++) {
			//Finner indeks i tabell som svarer til
			//gitt rad (i) og kolonne (j):
			idx = (i - 1) * _rowwidth + (j - 1);
			pixelTabell[idx].edit(0,0,0);
		} //indre for
	} //ytre for
} //settMerke

/**
Funksjon som gjør alle pixler grå.
Gråfargen den endres til er basert på gjennomsnittsverdien 
til hver pixels RGB.
----------------------------------------------------------
@param pixelTabell: referanse til vector som inneholder (de endrede) pixlene
*/
void greyScale(vector<Pixel>&pixelTabell) {
	double r, g, b;
	double gjennomsnitt;
	//loop gjennom vector og hent ut verdier
	for(int i=0; i < (int)pixelTabell.size(); i++) {
		r = pixelTabell[i].getR();
		g = pixelTabell[i].getG();
		b = pixelTabell[i].getB();
		//beregn gjennomsnitt og legg resultat tilbake i vector
		gjennomsnitt = (r + g + b) / 3;
		pixelTabell[i].edit(gjennomsnitt, gjennomsnitt, gjennomsnitt);
	} //for
} //greyScale

/**
Funksjon som endrer fargen på pixler. Fargen som endres er basert 
på component (0 = R, 1 = G, 2 = B) og color (0-255) som er fargen 
pixelen endres til.
----------------------------------------------------------
@param pixelTabell: referanse til vector som inneholder (de endrede) pixlene
@param component: pixel som skal endres (0 = R, 1 = G, 2 = B)
@param color: fargen som pixel skal endres til (0-255)
*/
void changeColor(vector<Pixel> &pixelTabell, int component, int color) {
	switch(component) {
	case 0:
		//funksjon som looper gjennom vector og setter pixelfarge til oppgitt farge
		newColorRed(pixelTabell, color);
		break;
	case 1:
		newColorGreen(pixelTabell, color);
		break;
	case 2:
		newColorBlue(pixelTabell, color);
		break;
	default:
		cout << "Ugyldig valg. Fargekomponent er et siffer (0 = 1, 1 = G, 2 = B)." << endl;
		break;
	} //switch
} //changeColor

void newColorRed(vector<Pixel> &pixelTabell, int color) {
	int r, g, b;
	for(int i=0; i < (int)pixelTabell.size(); i++) {
		r = color;
		g = pixelTabell[i].getG();
		b = pixelTabell[i].getB();
		pixelTabell[i].edit(r, g, b);
	} //for
} //newColorRed

void newColorGreen(vector<Pixel> &pixelTabell, int color) {
	int r, g, b;
	for(int i=0; i < (int)pixelTabell.size(); i++) {
		r = pixelTabell[i].getR();
		g = color;
		b = pixelTabell[i].getB();
		pixelTabell[i].edit(r, g, b);
	} //for
} //newColorGreen

void newColorBlue(vector<Pixel> &pixelTabell, int color) {
	int r, g, b;
	for(int i=0; i < (int)pixelTabell.size(); i++) {
		r = pixelTabell[i].getR();
		g = pixelTabell[i].getG();
		b = color;
		pixelTabell[i].edit(r, g, b);
	} //for
} //newColorBlue

/**
Denne funksjonen skriver til fil resultatet av endringene gjort med 
den orginale filen.
----------------------------------------------------------
@param innfilnavn: Navn på orginal fil
@param offset: Filens offset
@param width: Filens vidde
@param pixelTabell: referanse til vector som inneholder (de endrede) pixlene
*/
void skrivNyBitmapTilFil(string innfilnavn, unsigned int offset, unsigned int width, vector<Pixel> &pixelTabell) {
	fstream infile;
	string utfilnavn = "figur1-ny.bmp";
	infile.open(innfilnavn.c_str(), ios::binary | ios::in);
	unsigned char header[54]; //Gir plass til headeren.
	//Leser hele headeren slik at den kan brukes i ny fil:
	infile.read((char *)header,54);
	//Skriver manipulert innhold til ny fil:
	fstream outfile;
	outfile.open(utfilnavn.c_str(), ios::binary | ios::out);
	//Skriver headeren:
	outfile.write((char *)header, 54);
	//"Hjelpebytes":
	unsigned char utdata[3];
	unsigned char stuffBytes[3] = {0, 0, 0};
	int stuffing = width % 4;
	//Plasserer skrivepeker (byte 54):
	outfile.seekp(offset, ios::beg);
	//pix holder rede på antall skrevne piksler per linje
	int pix = 0;
	//Skriver alle pikslene fra tabellen til den nye fila:
	for(int j=0; j < (int)pixelTabell.size(); j++) {
		utdata[0] = pixelTabell[j].getB();
		utdata[1] = pixelTabell[j].getG();
		utdata[2] = pixelTabell[j].getR();
		outfile.write((char *)utdata, 3);
		pix++;
		//Legger til evt. stuffing:
		if((pix == width)){
			outfile.write((char *)stuffBytes, stuffing);
			pix=0;
		} //if
	} //for
	infile.close();
	outfile.close();
} //skrivNyBitmapTilFil