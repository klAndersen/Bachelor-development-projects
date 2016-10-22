package com.example.oblig4_knutlucasandersen;

/**
 * Klasse som objektifiser målestasjonene. <br /> 
 * Tanken er at klassen skal brukes for lettere lagre  
 * og hente ut valgte målestasjoner fra fil.
 * @author Knut Lucas Andersen
 */
public class Maalestasjon {
	private int _stasjonsNr;
	private String _stedsNavn;
	private String _url;
	private float _temperatur;

	/**
	 * Konstruktør for oppretting av liste over eksisterende målestasjoner
	 * @param stasjonsNr - int: Målestasjonens id/nr
	 * @param stedsNavn - String: Stedet målestasjonen finnes på 
	 * @param url - String: URL-adressen til målestasjonen
	 */
	public Maalestasjon(int stasjonsNr, String stedsNavn, String url) {
		_stasjonsNr = stasjonsNr;
		_stedsNavn = stedsNavn;
		_url = url;
	} //konstruktør

	/**
	 * Konstruktør for oppretting av liste over valgte målestasjoners 
	 * temperaturer
	 * @param stasjonsNr - int: Målestasjonens id/nr
	 * @param stedsNavn - String: Stedet målestasjonen finnes på 
	 * @param temperatur - double: Sist registrerte temperatur
	 */
	public Maalestasjon(int stasjonsNr, String stedsNavn, float temperatur) {
		_stasjonsNr = stasjonsNr;
		_stedsNavn = stedsNavn;
		_temperatur = temperatur;
	} //konstruktør

	/**
	 * Returnerer målestasjonsnummeret
	 * @return int: Målestasjonens id/nr
	 */
	public int getMaalestasjonNr() {
		return _stasjonsNr;
	}

	/**
	 * Returnerer stedsnavnet til hvor målestasjonen befinner seg. <br />
	 * Brukes til opplisting.
	 * @return String: Stedsnavnet til målestasjonen
	 */
	public String getStedsNavn() {
		return _stedsNavn;
	}

	/**
	 * Returnerer URL-adressen til målestasjonen. <br />
	 * Brukes for å hente data tilknyttet valgt målestasjon
	 * @return String: URL-adressen til målestasjonen
	 */
	public String getMaalestasjonURL() {
		return _url;
	}

	/**
	 * Returnerer den sist registrerte temperaturen til valgt målestasjonen
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