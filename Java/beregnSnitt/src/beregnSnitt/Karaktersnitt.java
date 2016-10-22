package beregnSnitt;

/**
 * BeregnSnitt regner ut karaktersnitt iforhold til høgskole karakterer. Karaktersnitt beregnes her.
 * Copyright (C) 2011  Knut Lucas Andersen
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

public class Karaktersnitt {
	//poeng/verdi for karakterene
	private final static int A = 5;
	private final static int B = 4;
	private final static int C = 3;
	private final static int D = 2;
	private final static int E = 1;	
	private static double poeng;

	/**
	 * Formel for utregning er som følger:
	 * Studiepoeng * Karakteren = sumKarakterer
	 * sumKarakterer deles så på summen av studiepoeng
	 * sumStudiepoeng (hvis bachelor) er 180.
	 * sumKarakter/sumStudiepoeng = snitt.
	 * 
	 * Dersom du har karakterer med egen stp - verdi, beregnes dette
	 * i egen prosedyre. Det samme gjelder om du kun har et stp for alle
	 * fag/kurs, skriv inn antallet så regnes dette ut i egen 
	 * prosedyre.
	 * @param antall
	 * @param karakter
	 * @param stp
	 * @param antA
	 * @param antB
	 * @param antC
	 * @param antD
	 * @param antE
	 * @param studPoeng
	 * @param sumStudPoeng
	 */
	public static void beregnSnitt(int antA, int antB, int antC, int antD, int antE, double stp, double sumStp, 
			int antall, char karakter, double stp2) {
		double sumTotal, sumSnitt, sumA, sumB, sumC, sumD, sumE;
		sumA = regnUtA(antA, stp); //hent summen for karakteren A
		sumB = regnUtB(antB, stp);		
		sumC = regnUtC(antC, stp);
		sumD = regnUtD(antD, stp);
		sumE = regnUtE(antE, stp);
		beregnEn(karakter, stp2); //regner ut en spesifikk karakter med tilhørende studiepoeng
		if (antall == 0) { //skal flere karakterer beregnes?
			sumTotal = sumA + sumB + sumC + sumD + sumE; //regner ut summen av karakterene
			sumTotal += getStp(); //legg til poeng (hvis snitt utenom er lagt til)		
			sumSnitt = sumTotal/sumStp; //regn ut snitt
			setStp(sumSnitt); //sett snittet			
		} //if (antall == 0) 
	} //beregnSnitt

	private static void beregnEn(char karakter, double stp) {
		double sum = 0;

		switch(karakter) { //hvilken karakter skal beregnes?
		case 'A':
			sum = regnUtA(1, stp); //regn ut EN karakter basert på tilhørende studiepoeng
			setStp(sum + getStp()); //legg til og sett ny studiepoeng sum
			break;
		case 'B':
			sum = regnUtB(1, stp);
			setStp(sum + getStp());
			break;
		case 'C':
			sum = regnUtC(1, stp);
			setStp(sum + getStp());
			break;
		case 'D':
			sum = regnUtD(1, stp);
			setStp(sum + getStp());
			break;
		case 'E':
			sum = regnUtE(1, stp);
			setStp(sum + getStp());
			break;			
		} //switch
	} //beregnEn	

	//***UTREGNINGSMETODER** *//
	private static double regnUtA(int antallA, double stp) {
		double poeng = 0;
		poeng = A * stp; //regn ut poeng;
		poeng = poeng * antallA; //regn ut totalpoengsum for karakteren;
		return poeng; //returner poengsummen for denne karakteren;
	} //regnUtA

	private static double regnUtB(int antallB, double stp) {
		double poeng = 0;
		poeng = B * stp;
		poeng = poeng * antallB;
		return poeng;
	} //regnUtB

	private static double regnUtC(int antallC, double stp) {
		double poeng = 0;
		poeng = C * stp;
		poeng = poeng * antallC;
		return poeng;
	} //regnUtC

	private static double regnUtD(int antallD, double stp) {
		double poeng = 0;
		poeng = D * stp;
		poeng = poeng * antallD;
		return poeng;
	} //regnUtD

	private static double regnUtE(int antallE, double stp) {
		double poeng = 0;
		poeng = E * stp;
		poeng = poeng * antallE;
		return poeng;
	} //regnUtE

	//***GET & SET - METODER ***//
	public static double getStp() {
		return poeng;
	} //getPoeng

	public static void setStp(double poeng) {
		Karaktersnitt.poeng = poeng;
	} //setPoeng
} //Karaktersnitt