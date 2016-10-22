package dt.hin.android.kl_andersen;

//java-import
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.Locale;
//google-import
import com.google.android.maps.GeoPoint;

/**
 * Klasse som objektifiserer enhetene (brukerne) som vises på kartet. <br />
 * Klassene har to konstruktører, den ene for registrering av enheter som er online, <br />
 * den andre for oppretting av ruter lagret i databasen (SQLite).
 * @author Knut Lucas Andersen
 */
public class Enhet {
	/** Datoformatet som rutetidspunkt blir lagret som i databasen (SQLite) **/
	private final String DATO_TID_FORMAT = "yyyy-MM-dd HH:mm:ss";
	private long _identifikator;
	private String _registrationID;
	private String _enhetsNavn;
	private boolean _egenEnhet;
	private int _ruteID;
	private String _ruteTidspunkt;
	private ArrayList<GeoPoint> _koordinatListe;

	/**
	 * Konstruktør for oppretting av en enhet.
	 * @param identifikator - long: ID som identfiserer enhet (enhetens primærnøkkel i databasen)
	 * @param registrationID - String: Enhetens registrationID
	 * @param enhetsNavn - String: Enhetens navn
	 * @param egenEnhet - boolean: Er dette brukers enhet? Sett verdi til True. <br />
	 * Ikke brukers enhet? Sett verdi til False.
	 * @param ruteID - int: RuteID for denne enhetens rute
	 * @param koordinatListe - ArrayList&lt;GeoPoint&gt;: ArrayList som inneholder denne enhetens koordinater
	 */
	public Enhet(long identifikator, String registrationID, String enhetsNavn, boolean egenEnhet, int ruteID, ArrayList<GeoPoint> koordinatListe) {
		_identifikator = identifikator;
		_registrationID = registrationID;
		_enhetsNavn = enhetsNavn;
		_egenEnhet = egenEnhet;
		_ruteID = ruteID;
		_koordinatListe = new ArrayList<GeoPoint>(koordinatListe);
	} //konstruktør

	/**
	 * Konstruktør for oppretting av enhet. <br />
	 * Bruksområde er hovedsaklig tenkt for fremvisning/opplisting og generering av lagrede ruter fra databasen
	 * @param identifikator - long: ID som identfiserer enhet (enhetens primærnøkkel i databasen)
	 * @param registrationID - String: Enhetens registrationID
	 * @param enhetsNavn - String: Enhetens navn
	 * @param egenEnhet - boolean: Er dette brukers enhet? Sett verdi til True. <br />
	 * Ikke brukers enhet? Sett verdi til False.
	 * @param ruteID - int: RuteID for denne enhetens rute
	 * @param koordinatListe - ArrayList&lt;GeoPoint&gt;: ArrayList som inneholder denne enhetens koordinater
	 */
	public Enhet(long identifikator, String registrationID, String enhetsNavn, boolean egenEnhet, int ruteID, String ruteTidspunkt, ArrayList<GeoPoint> koordinatListe) {
		_identifikator = identifikator;
		_registrationID = registrationID;
		_enhetsNavn = enhetsNavn;
		_egenEnhet = egenEnhet;
		_ruteID = ruteID;
		_ruteTidspunkt = ruteTidspunkt;
		_koordinatListe = new ArrayList<GeoPoint>(koordinatListe);
	} //konstruktør

	public long getIdentifikator() {
		return _identifikator;
	} //getIdentifikator

	public String getRegistrationID() {
		return _registrationID;
	} //getRegistrationID

	public String getEnhetsnavn() {
		return _enhetsNavn;
	} //getEnhetsnavn

	public boolean getEgenEnhet() {
		return _egenEnhet;
	} //getEgenEnhet

	public int getRuteID() {
		return _ruteID;
	} //getRuteID

	/**
	 * Kun tilgjengelig ved oppretting av ruteliste 
	 * og ruter basert på lagrede data.
	 * @return String: Tidspunkt da ruten ble opprettet
	 */
	public String getRuteTidspunkt() {
		return _ruteTidspunkt;
	} //getRuteTidspunkt

	/**
	 * Returnerer alle koordinatene til denne enheten i en ArrayList.
	 * @return ArrayList&lt;GeoPoint&gt;
	 */
	public ArrayList<GeoPoint> getKoordinater() {
		return _koordinatListe;
	} //getKoordinater

	/**
	 * Gir mulighet for å oppdatere/endre tilkoblet brukers navn.
	 * @param enhetsNavn - String: Enhetens nye navn
	 */
	public void setEnhetsNavn(String enhetsNavn) {
		_enhetsNavn = enhetsNavn;
	} //setEnhetsNavn
	
	/**
	 * Brukes ved tegning av lagrede ruter for å unngå at eksisterende rute 
	 * ikke blir tegnet på kartet. <br /> 
	 * Denne setter egenEnhet til false, likegyldig hva tidligere verdi var.
	 */
	public void setEgenEnhetFalse() {
		_egenEnhet = false;
	} //setEgenEnhetFalse

	/**
	 * Registrerer/legger til denne enhetens siste mottatte koordinat
	 * @param koordinat - GeoPoint: Siste mottatte koordinat
	 */
	public void addGeoPoint(GeoPoint koordinat) {
		_koordinatListe.add(koordinat);
	} //addGeoPoint

	@Override
	public String toString() {
		String utskrift = "";
		try {
			//konverter eksisterende tidspunkt til dato
			Locale local = Locale.getDefault();
			SimpleDateFormat formatering = new SimpleDateFormat(DATO_TID_FORMAT, local);
			Date tidspunkt = (Date) formatering.parse(getRuteTidspunkt());
			//omgjør dato til kalender
			Calendar kalender = Calendar.getInstance();
			kalender.setTime(tidspunkt);
			//konverter dato-strengen ved å bruke calendar (09/05-2013 18:00:00)
			String tid = String.format("%02d", kalender.get(Calendar.DATE)) 
					+ "/" + String.format("%02d", (kalender.get(Calendar.MONTH) + 1)) 
					+ "-" + kalender.get(Calendar.YEAR) + " " 
					+ String.format("%02d", kalender.get(Calendar.HOUR)) 
					+ ":" + String.format("%02d", kalender.get(Calendar.MINUTE)) 
					+ ":" + String.format("%02d", kalender.get(Calendar.SECOND));
			//opprett utskriften og returner den
			utskrift = "Rute registrert: " + tid + "\n"
					+"Enhetsnavn: " + getEnhetsnavn() + ", ";
			if (getEgenEnhet()) {
				utskrift += "Egen rute";
			} else {
				utskrift += "Annen brukers rute";
			} //if (_egenEnhet)
		} catch (ParseException ex) {
			ex.printStackTrace();
		} catch (Exception ex) {
			ex.printStackTrace();
		} //try/catch
		return utskrift;
	} //toString
} //Enhet