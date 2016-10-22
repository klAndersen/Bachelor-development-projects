package dt.hin.android.kl_andersen;

//java-import
import java.io.IOException;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.net.URLEncoder;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;
import java.util.Map.Entry;
//android-import
import android.content.Context;
import android.content.SharedPreferences;
import android.database.sqlite.SQLiteException;
//google-import
import com.google.android.gcm.GCMRegistrar;

/**
 * Denne klassen tar for seg oppgaver tilknyttet tilkobling til server. <br />
 * Oppgaver som utføres er bl.a: <br />
 * - Registrering av enhet på server <br />
 * - Lagring av brukers enhet i databasen (SQLite) <br />
 * - Avregistrering av enhet på server <br />
 * - Sending av koordinater (latitude og longitude) <br />
 * - Sending av POST-request til server <br />
 * @author Knut Lucas Andersen
 */
public final class ServerTilkobling {
	private static SharedPreferences _deltPreferanse;
	/** Antall forsøk før enhet avslutter tilkobling til server **/
	private static final int MAX_TILKOBLINGSFORSOK = 5;
	/** Melding til server om at brukers posisjon ikke er utsendt **/
	private static final String IKKE_SEND_POSISJON = "999.0";
	/** Parameter i melding til server - verdi som sendes er enhetens navn **/
	private static final String PARAMETER_ENHETSNAVN = "enhetsNavn";
	/** Kobling til side på server - Registrering av enhet **/
	private static final String REGISTRER_ENHET = "/RegistrerEnhet";
	/** Kobling til side på server - Avregistrering av enhet **/
	private static final String AVREGISTRER_ENHET = "/AvregistrerEnhet";
	/** Parameter i melding til server - verdi som sendes er at enhet gikk offline **/
	private static final String PARAMETER_OFFLINE_MELDING = "enhetOffline";
	/** Kobling til side på server - Utsending av melding fra enhet til server **/
	private static final String SEND_MELDING_TIL_SERVER = "/MottaMeldingFraEnhet";
	/** Adressen til server. Her brukes localhost adressen til emulator **/
	//private static final String SERVER_URL = "http://10.0.2.2:8080/Karaktergivende_Server_KLA";
	/** Adressen til server. Her brukes Kark **/
	private static final String SERVER_URL = "http://kark.hin.no:8088/Karaktergivende_Server_KLA/";

	private ServerTilkobling() {
		throw new UnsupportedOperationException();
	} //konstruktør

	/**
	 * Funksjon som forsøker å registrere enhet på server. <br />
	 * Det forsøkes å koble til server fem ganger, og det returneres true 
	 * hvis tilkobling ble opprettet, og false hvis tilkobling til server feilet.
	 * @param context - Context
	 * @param registrationID - String: Enhetens registrationID
	 * @return boolean: True - tilkoblet server, false - ikke tilkoblet server
	 */
	protected static boolean registerOnServer(final Context context, final String registrationID) {
		String melding = "";
		String serverUrl = SERVER_URL + REGISTRER_ENHET;
		_deltPreferanse = Filbehandling.getSharedPreferances(context);
		Filbehandling.lagreEnhetsRegistrationID(context, registrationID);
		String defaultVerdi = context.getString(R.string.default_enhet_navn);
		String enhetsNavn = _deltPreferanse.getString(Filbehandling.ENHETSNAVN_NOKKEL, defaultVerdi);
		boolean sendPosisjon = _deltPreferanse.getBoolean(Filbehandling.SEND_POSISJON_NOKKEL, false);
		//fyller opp Map med verdier som skal sendes til server
		Map<String, String> parametere = new HashMap<String, String>();
		parametere.put(Filbehandling.REGISTRATIONID_NOKKEL, registrationID);
		parametere.put(PARAMETER_ENHETSNAVN, enhetsNavn);
		//skal brukers posisjon sendes?
		if (sendPosisjon) {
			float defValue = Float.parseFloat(IKKE_SEND_POSISJON);
			Float lat = _deltPreferanse.getFloat(Filbehandling.LATITUDE_NOKKEL, defValue),
					lon = _deltPreferanse.getFloat(Filbehandling.LONGITUDE_NOKKEL, defValue);
			parametere.put(Filbehandling.LATITUDE_NOKKEL, lat.toString());
			parametere.put(Filbehandling.LONGITUDE_NOKKEL, lon.toString());
		} else {
			parametere.put(Filbehandling.LATITUDE_NOKKEL, IKKE_SEND_POSISJON);
			parametere.put(Filbehandling.LONGITUDE_NOKKEL, IKKE_SEND_POSISJON);
		} //if (sendPosisjon)
		//forsøk å koble til server
		for (int i = 1; i <= MAX_TILKOBLINGSFORSOK; i++) {
			try {
				//opprett melding som inneholder hvilket forsøk vi er på
				FellesFunksjoner.visMelding(context, context.getString(R.string.server_registering, i, MAX_TILKOBLINGSFORSOK), false);
				//send post request til server med oppgitte parameter
				postOnServer(serverUrl, parametere);
				//sett at enhet ble registrert på server
				GCMRegistrar.setRegisteredOnServer(context, true);
				//opprett melding om at enhet ble registrert på server
				melding = context.getString(R.string.server_registered);
				FellesFunksjoner.visMelding(context, melding, false);
				long identifikator = DatabaseFunksjoner.registrerEnhetiDatabase(context, registrationID, enhetsNavn, true);
				Filbehandling.lagreEnhetsIdentifikator(context, identifikator);
				return true;
			} catch (IOException ex) {
				if (i == MAX_TILKOBLINGSFORSOK) {
					//opprett melding om at enhet ikke ble registrert på server 
					melding = context.getString(R.string.server_register_error, MAX_TILKOBLINGSFORSOK);
					break;
				} //if (i == MAX_TILKOBLINGSFORSOK)
			} catch (SQLiteException ex) {
				melding = context.getString(R.string.sqlite_exception) + ex.getMessage();
			} catch (Exception ex) {
				ex.printStackTrace();
				melding = context.getString(R.string.unknown_error_exception);
			} //try/catch
		} //for
		FellesFunksjoner.visMelding(context, melding, true);
		return false;
	} //registerOnServer

	/**
	 * Siden bruker har mulighet til å motta posisjonsdata etter applikasjon er avsluttet, 
	 * så vil ikke funksjonen {@link #registerOnServer(Context, String)} kalles. <br />
	 * Dette kan føre til at bruker ikke mottar posisjonsdata via server (eksempel hvis server er nede). <br />
	 * Derfor sender denne funksjonen "dummy data" til server for å gjenopprette tilkobling.
	 * @param context - Context
	 * @param registrationID - String: Enhetens registrationID
	 */
	protected static void gjenopprettTilkoblingTilServer(Context context, String registrationID) {
		try {
			String serverUrl = SERVER_URL + SEND_MELDING_TIL_SERVER;
			String defaultVerdi = context.getString(R.string.default_enhet_navn);
			_deltPreferanse = Filbehandling.getSharedPreferances(context);
			//fyller opp Map med verdier som skal sendes til server
			Map<String, String> parametere = new HashMap<String, String>();
			parametere.put(Filbehandling.REGISTRATIONID_NOKKEL, registrationID);
			parametere.put(PARAMETER_ENHETSNAVN, _deltPreferanse.getString(Filbehandling.ENHETSNAVN_NOKKEL, defaultVerdi));
			Float ikkeSend = Float.parseFloat(IKKE_SEND_POSISJON);
			parametere.put(Filbehandling.LATITUDE_NOKKEL, ikkeSend.toString());
			parametere.put(Filbehandling.LONGITUDE_NOKKEL, ikkeSend.toString());
			postOnServer(serverUrl, parametere);
		} catch (IOException ex) {
			String feilmelding = ex.getMessage();
			Filbehandling.oppdaterSystemLogg(context, feilmelding);
			FellesFunksjoner.visMelding(context, feilmelding, true);
		} //try/catch
	} //gjenopprettTilkoblingTilServer

	/**
	 * Funksjon som avregistrerer enhet fra server.
	 * @param context - Context
	 * @param registrationID - String: Enhetens registrationID
	 */
	protected static void unregisterFromServer(final Context context, final String registrationID) {
		try {
			//opprett url og legg ved registrationID som parameter
			String serverUrl = SERVER_URL + AVREGISTRER_ENHET;
			Map<String, String> params = new HashMap<String, String>();
			params.put(Filbehandling.REGISTRATIONID_NOKKEL, registrationID);
			//fosøk å avregistrere ved å sende post til server
			postOnServer(serverUrl, params);
			//sett av enhet ble avregistrert
			GCMRegistrar.setRegisteredOnServer(context, false);
			//vis melding til bruker om suksessfull avregistrering
			String melding = context.getString(R.string.server_unregistered);
			FellesFunksjoner.visMelding(context, melding, false);
		} catch (IOException ex) {
			//Enhet er avregistrert fra GCM, men fortsatt registrert på server.
			//Hvis server forsøker å sende en melding til enhet, vil server få 
			//en "NotRegistered" feilmelding, og bør avregistrere enhet
			String melding = context.getString(R.string.server_unregister_error, ex.getMessage());
			FellesFunksjoner.visMelding(context, melding, true);
		} //try/catch
	} //unregisterFromServer

	/**
	 * Sender brukers koordinater til server.
	 * @param context - Context
	 * @param registrationID - String: Enhetens registrationID
	 */
	protected static void sendKoordinaterTilServer(Context context, String registrationID) {
		try {
			String serverUrl = SERVER_URL + SEND_MELDING_TIL_SERVER;
			String defaultVerdi = context.getString(R.string.default_enhet_navn);
			_deltPreferanse = Filbehandling.getSharedPreferances(context);
			//fyller opp Map med verdier som skal sendes til server
			Map<String, String> parametere = new HashMap<String, String>();
			parametere.put(Filbehandling.REGISTRATIONID_NOKKEL, registrationID);
			parametere.put(PARAMETER_ENHETSNAVN, _deltPreferanse.getString(Filbehandling.ENHETSNAVN_NOKKEL, defaultVerdi));
			float defValue = Float.parseFloat(IKKE_SEND_POSISJON);
			Float lat = _deltPreferanse.getFloat(Filbehandling.LATITUDE_NOKKEL, defValue),
					lon = _deltPreferanse.getFloat(Filbehandling.LONGITUDE_NOKKEL, defValue);
			parametere.put(Filbehandling.LATITUDE_NOKKEL, lat.toString());
			parametere.put(Filbehandling.LONGITUDE_NOKKEL, lon.toString());
			postOnServer(serverUrl, parametere);
		} catch (IOException ex) {
			String feilmelding = ex.getMessage();
			Filbehandling.oppdaterSystemLogg(context, feilmelding);
			FellesFunksjoner.visMelding(context, feilmelding, true);
		} //try/catch
	} //sendKoordinaterTilServer

	/**
	 * Sender melding til server om at bruker har avsluttet applikasjonen (gått "offline").
	 * @param context - Context
	 * @param registrationID - String: Enhetens registrationID
	 */
	protected static void sendOfflineMeldingTilServer(Context context, String registrationID) {
		try {
			String serverUrl = SERVER_URL + SEND_MELDING_TIL_SERVER;
			Map<String, String> parametere = new HashMap<String, String>();
			parametere.put(PARAMETER_OFFLINE_MELDING, PARAMETER_OFFLINE_MELDING);
			parametere.put(Filbehandling.REGISTRATIONID_NOKKEL, registrationID);			
			postOnServer(serverUrl, parametere);
		} catch (IOException ex) {
			String feilmelding = ex.getMessage();
			Filbehandling.oppdaterSystemLogg(context, feilmelding);
			FellesFunksjoner.visMelding(context, feilmelding, true);
		} //try/catch
	} //sendOfflineMeldingTilServer

	/**
	 * Funksjon som forsøker å opprette tilkobling til oversendt {@linkplain #SERVER_URL}. <br />
	 * Funksjonen tar oversendte parameter og bruker StringBuilder for å opprette en 
	 * string med parameterverdier, <br /> i tillegg til at parameter blir kodet via URLEncoder. <br />
	 * Tilkobling opprettes via HttpURLConnection, hvor etter fullført post status sjekkes. <br />
	 * Dersom resultatet av post resulterte i en feil, kastes IOException.
	 * @param serverUrl - String: URL-adressen på server som skal motta post requesten
	 * @param parametere - Map&lt;String, String&gt;: Parametere som skal sendes til server
	 * @throws IOException
	 * @see StringBuilder
	 * @see URLEncoder
	 * @see HttpURLConnection
	 */
	private static void postOnServer(String serverUrl, Map<String, String> parametere) throws IOException {
		URL url;
		try {
			//opprett url
			url = new URL(serverUrl);
		} catch (MalformedURLException ex) {
			throw new IllegalArgumentException("Ugyldig URL: " + serverUrl);
		} //try/catch
		//opprett objekt av stringbuilder for å konstruere parameterne som skal sendes med post
		StringBuilder stringBuilder = new StringBuilder();
		//opprett iterator for å loope gjennom Map's innhold
		Iterator<Entry<String, String>> iterator = parametere.entrySet().iterator();
		//så lenge iterator har innhold
		while (iterator.hasNext()) {
			//hent ut neste parameter
			Entry<String, String> parameter = iterator.next();
			//hent ut nøkkel og verdien og kod parameterne
			String nokkel = URLEncoder.encode(parameter.getKey(), "UTF-8");
			String verdi = URLEncoder.encode(parameter.getValue(), "UTF-8");
			//bygg paramter-strengen som skal sendes
			//eks: regID=12r45jxH90l
			stringBuilder.append(nokkel) .append('=').append(verdi);
			//er det flere parametere? isåfall legg til '&' (skilletegn på url-parametere)
			if (iterator.hasNext()) {
				stringBuilder.append('&');
			} //if (iterator.hasNext())
		} //while
		String parameterVerdier = stringBuilder.toString();
		byte[] bytes = parameterVerdier.getBytes();
		HttpURLConnection tilkobling = null;
		try {
			//forsøk å koble til den oversendte url-adressen
			tilkobling = (HttpURLConnection) url.openConnection();
			tilkobling.setDoOutput(true);
			tilkobling.setUseCaches(false);
			tilkobling.setFixedLengthStreamingMode(bytes.length);
			tilkobling.setRequestMethod("POST");
			tilkobling.setRequestProperty("Content-Length", Integer.toString(parameterVerdier.length()));
			//sender post request til httpurlconnection
			OutputStream out = tilkobling.getOutputStream();
			out.write(bytes);
			out.close();
			//hva er resultatet/status på tilkobling?
			int status = tilkobling.getResponseCode();
			if (status != 200) {
				throw new IOException("Kunne ikke sende POST til server. \nStatus: " + status);
			} //if (status != 200)
		} finally {
			//lukk tilkobling
			if (tilkobling != null) {
				tilkobling.disconnect();
			} //if (tilkobling != null)
		} //try/catch
	} //postOnServer
} //ServerTilkobling