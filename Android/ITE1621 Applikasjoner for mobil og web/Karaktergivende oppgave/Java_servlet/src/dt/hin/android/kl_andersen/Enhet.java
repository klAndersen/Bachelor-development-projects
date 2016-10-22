package dt.hin.android.kl_andersen;

/**
 * Klasse som objektifiserer enhetene som kobler seg til Server.
 * @author Knut Lucas Andersen
 */
public class Enhet {
	private String _registrationID;
	private String _enhetsNavn;
	private double _latitude;
	private double _longitude;
	private String _registreringsDato;

	/**
	 * Konstruktør for oppretting av online enhet på server
	 * @param registrationID - String: Enhetens registrationID
	 * @param enhetsNavn - String: Enhetens navn
	 * @param latitude - double: Enhetens latitude
	 * @param longitude - double: Enhetens longitude
	 * @param registreringsDato - String: Dato for førstegangsregistrering
	 */
	public Enhet(String registrationID, String enhetsNavn, double latitude, double longitude, String registreringsDato) {
		_registrationID = registrationID;
		_enhetsNavn = enhetsNavn;
		_latitude = latitude;
		_longitude = longitude;
		_registreringsDato = registreringsDato;
	} //konstruktør

	public String getRegistrationID() {
		return _registrationID;
	} //getRegistrationID

	public String getEnhetsnavn() {
		return _enhetsNavn;
	} //getEnhetsnavn

	public String getRegistrertDato() {
		return _registreringsDato;
	} //getRegistrertDato

	public double getLatitude() {
		return _latitude;
	} //getLatitude

	public double getLongitude() {
		return _longitude;
	} //getLongitude
	
	public void setEnhetsNavn(String enhetsNavn) {
		_enhetsNavn = enhetsNavn;
	} //setEnhetsNavn

	public void setLatitude(double latitude) {
		_latitude = latitude;
	} //setLatitude

	public void setLongitude(double longitude) {
		_longitude = longitude;
	} //setLongitude
} //Enhet