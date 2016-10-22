package com.example.oblig4_knutlucasandersen;

/**
 * Klasse som objektifiser m�lestasjonene. <br /> 
 * Tanken er at klassen skal brukes for lettere lagre  
 * og hente ut valgte m�lestasjoner fra fil.
 * @author Knut Lucas Andersen
 */
public class Maalestasjon {
	private int _stasjonsNr;
	private String _stedsNavn;
	private String _url;
	private float _temperatur;

	/**
	 * Konstrukt�r for oppretting av liste over eksisterende m�lestasjoner
	 * @param stasjonsNr - int: M�lestasjonens id/nr
	 * @param stedsNavn - String: Stedet m�lestasjonen finnes p� 
	 * @param url - String: URL-adressen til m�lestasjonen
	 */
	public Maalestasjon(int stasjonsNr, String stedsNavn, String url) {
		_stasjonsNr = stasjonsNr;
		_stedsNavn = stedsNavn;
		_url = url;
	} //konstrukt�r

	/**
	 * Konstrukt�r for oppretting av liste over valgte m�lestasjoners 
	 * temperaturer
	 * @param stasjonsNr - int: M�lestasjonens id/nr
	 * @param stedsNavn - String: Stedet m�lestasjonen finnes p� 
	 * @param temperatur - double: Sist registrerte temperatur
	 */
	public Maalestasjon(int stasjonsNr, String stedsNavn, float temperatur) {
		_stasjonsNr = stasjonsNr;
		_stedsNavn = stedsNavn;
		_temperatur = temperatur;
	} //konstrukt�r

	/**
	 * Returnerer m�lestasjonsnummeret
	 * @return int: M�lestasjonens id/nr
	 */
	public int getMaalestasjonNr() {
		return _stasjonsNr;
	}

	/**
	 * Returnerer stedsnavnet til hvor m�lestasjonen befinner seg. <br />
	 * Brukes til opplisting.
	 * @return String: Stedsnavnet til m�lestasjonen
	 */
	public String getStedsNavn() {
		return _stedsNavn;
	}

	/**
	 * Returnerer URL-adressen til m�lestasjonen. <br />
	 * Brukes for � hente data tilknyttet valgt m�lestasjon
	 * @return String: URL-adressen til m�lestasjonen
	 */
	public String getMaalestasjonURL() {
		return _url;
	}

	/**
	 * Returnerer den sist registrerte temperaturen til valgt m�lestasjonen
	 * @return float: Sist registrerte temperatur
	 */
	public float getTemperatur() {
		return _temperatur;
	}

	/**
	 * Overrider toString() metoden og returnerer en string som 
	 * inneholder stedsnavn og temperatur med gradtegn
	 * @return String: Stedsnavn temperatur *C
	 */
	@Override
	public String toString() {
		char gradTegn = '\u00B0';
		return _stedsNavn + " " + _temperatur + " " +  gradTegn + "C";
	}
} //Maalestasjon