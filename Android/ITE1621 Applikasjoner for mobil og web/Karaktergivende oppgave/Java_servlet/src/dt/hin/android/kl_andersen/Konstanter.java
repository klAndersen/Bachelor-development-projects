package dt.hin.android.kl_andersen;

/**
 * Klasse som inneholder konstanter som brukes over flere servlets/klasser.
 * @author Knut Lucas Andersen
 */
public final class Konstanter {
	
	private Konstanter() {
		throw new UnsupportedOperationException();
	} //konstruktør
	
	/** Konstant som tilsier at bruker vil motta lokasjonsdata, - men ikke sende egne **/
	public static final String IKKE_SEND_POSISJON = "999.0";
	
	/** API-prosjekt nøkkel fra code.google.com/apis/console **/
	static final String API_PROJECT_KEY = "AIzaSyCWtVO-yHkERZ420iKYGLRTXm7akvojfDg";
	
	/** Konstant for uthenting av parameter: Meldingstype (hva slags melding som skal sendes) **/
	static final String MELDINGSTYPE = "meldingsType";
	
	/** Konstant for uthenting av parameter: 
	 * Koordinater (tilsier at melding som skal sendes er enhets posisjon) **/
	static final String SEND_KOORDINATER_MELDING = "messageCoordinates";
	
	/** Konstant for uthenting av parameter: Melding skal sendes til enheter fra websiden **/
	static final String SEND_MELDING_TIL_ENHETER = "message";
	
	/** Konstant for uthenting av parameter: Identifisering/emne til melding som sendes ut **/
	static final String MELDINGS_EMNE = "meldingsEmne";
	
	/** Konstant for uthenting av parameter: melding som skal sendes **/
	static final String MELDINGS_INNHOLD = "melding";
	
	/** Konstant for uthenting av parameter: registrationID (fra enhet) **/
	static final String PARAMETER_REGISTRATION_ID = "registrationID";

	/** Konstant for uthenting av parameter: latitude **/
	static final String PARAMETER_LATITUDE = "latitude";
	
	/** Konstant for uthenting av parameter: longitude **/
	static final String PARAMETER_LONGITUDE = "longitude";

	/** Konstant for uthenting av parameter: Enhetens navn **/
	static final String PARAMETER_ENHETSNAVN = "enhetsNavn";
	
	/** Melding som sendes ut til alle enheter når en enhet går offline 
	 	(for å stoppe tegning av denne ruten) **/
	static final String ENHET_GIKK_OFFLINE = "enhetOffline";
} //Konstanter