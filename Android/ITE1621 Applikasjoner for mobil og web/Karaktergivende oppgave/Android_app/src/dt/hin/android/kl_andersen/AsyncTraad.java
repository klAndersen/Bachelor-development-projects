package dt.hin.android.kl_andersen;

//java-import
import java.lang.ref.WeakReference;
//android-import
import android.app.Activity;
import android.content.Context;
import android.content.SharedPreferences;
import android.database.sqlite.SQLiteException;
import android.os.AsyncTask;
import android.os.Message;
import android.widget.Toast;
//google-import
import com.google.android.gcm.GCMRegistrar;
import com.google.android.maps.GeoPoint;

/**
 * Klasse som benytter seg av AsyncTask&ltVoid,Void,Void&gt;. <br/>
 * Denne klassen brukes for å: <br />
 * - Avregistrere enhet dersom tilkobling til server feilet <br />
 * - Gjenopprette tilkobling til server (hvis posisjonsdata mottas etter app avsluttes) <br />
 * - Sende enhetens koordinater til server <br />
 * - Lagre enhets koordinater i databasen (SQLite) - gjøres ved sending <br />
 * - Sletting fra databasen (SQLite) <br />
 * @author Knut Lucas Andersen
 */
public final class AsyncTraad extends AsyncTask<Void, Void, Void> {
	/** Sending av melding via Toast fra AsyncTask tråd **/
	protected final static int SEND_MELDING_VIA_TOAST = 1001;
	/** Starter avregistrering fra server **/
	protected final static int AVREGISTRERING = 0;
	/** Kobler til server - brukes ved oppstart hvis bruker mottar posisjonsdata etter applikasjon avsluttes **/
	protected final static int GJENOPRETT_SERVER_TILKOBLING = 1;
	/** Sender brukers posisjon til server **/
	protected final static int SEND_POSISJON = 2;
	/** Starter sletting fra database - sletter kun det bruker ikke vil beholde **/
	protected static final int SLETT_ALT_FRA_DATABASE = 3;
	/** Sletting av bestemte elementer fra databasen **/
	protected static final int SLETT_SPESIFISERTE_RADER = 4;
	private final int _oppgave;
	private final String _registrationID;	
	//oppretter en WeakReference for å unngå minnelekkasjer
	private final WeakReference<Activity> _mAsyncTask;

	/**
	 * Konstruktør for start av en async tråd. <br />
	 * Oppgaven som utføres avhenger av oversendt verdi (oppgave).
	 * @param aktivitet - Activity: Aktiviteten som kaller tråden
	 * @param registrationID - String: Enhetens registrationID
	 * @param oppgave - int: Oppgaven som skal utføres
	 */
	public AsyncTraad(Activity aktivitet, String registrationID, int oppgave) {		
		_registrationID = registrationID;
		_mAsyncTask = new WeakReference<Activity>(aktivitet);
		_oppgave = oppgave;
	} //konstruktør

	@Override
	public Void doInBackground(Void... params) {
		try {
			switch (_oppgave) {
			case AVREGISTRERING:
				avRegistrerEnhetFraServer();
				break;
			case GJENOPRETT_SERVER_TILKOBLING:
				gjenopprettTilkoblingTilServer();
				break;
			case SEND_POSISJON:			
				sendKoordinaterTilServer();
				break;
			case SLETT_ALT_FRA_DATABASE:
				startSlettingFraDatabase();
				break;
			case SLETT_SPESIFISERTE_RADER:
				startSpesifisertSletting();
				break;
			} //switch
		} catch (Exception ex) {
			ex.printStackTrace();
			Context context = _mAsyncTask.get().getApplicationContext();
			String feilmelding = context.getString(R.string.unknown_error_exception) + ex.getMessage();
			opprettMelding(feilmelding);
		} //try/catch
		return null;
	} //doInBackground

	/**
	 * Dersom denne funksjonen igangsettes, betyr det at forsøk på å registrere på server feilet,
	 * så enhet må avregistreres fra GCM - applikasjon vil forsøke å registrere seg igjen når 
	 * den er restartet. GCM vil sende en unregistered callback, men GCMIntentService.onUnregistered()
	 * vil ignorere den
	 */
	private void avRegistrerEnhetFraServer() {
		Context context = _mAsyncTask.get().getApplicationContext();
		boolean registered = ServerTilkobling.registerOnServer(context, _registrationID);
		if (!registered) {
			GCMRegistrar.unregister(context);
		} //if (!registered)
	} //avRegistrerEnhetFraServer

	/**
	 * @see ServerTilkobling#gjenopprettTilkoblingTilServer(Context, String)
	 */
	private void gjenopprettTilkoblingTilServer() {
		if (!_registrationID.equals("")) {
			Context context = _mAsyncTask.get().getApplicationContext();
			ServerTilkobling.gjenopprettTilkoblingTilServer(context, _registrationID);
		} //if (!_registrationID.equals(""))
	} //gjenopprettTilkoblingTilServer

	/**
	 * Sender brukers koordinater til server, hvis bruker har aktivert sending av 
	 * egne posisjonsdata.
	 */
	private void sendKoordinaterTilServer() {
		Context context = _mAsyncTask.get().getApplicationContext();
		try {			
			SharedPreferences deltPreferanse = Filbehandling.getSharedPreferances(context);
			boolean sendPosisjon = deltPreferanse.getBoolean(Filbehandling.SEND_POSISJON_NOKKEL, false);
			if (sendPosisjon) {
				ServerTilkobling.sendKoordinaterTilServer(context, _registrationID);
			} //if (sendPosisjon)
			//hent ut brukers identifikator
			long identifikator = deltPreferanse.getLong(Filbehandling.ENHETS_IDENTIFIKATOR, 0);
			String enhetsNavn = deltPreferanse.getString(Filbehandling.ENHETSNAVN_NOKKEL, context.getString(R.string.default_enhet_navn));
			//er brukers enhet registrert i databasen?
			if (identifikator == 0) {
				//brukers enhet er ikke registrert i databasen, registrer den
				identifikator = DatabaseFunksjoner.registrerEnhetiDatabase(context, _registrationID, enhetsNavn, true);
				Filbehandling.lagreEnhetsIdentifikator(context, identifikator);
			} //if (identifikator == -1)
			//lagre posisjon i database
			//hent ut brukers posisjon og konverter den til geopoint 
			//(trenger ikke 1E6, siden den beregningen er gjort i onLocationChanged) 
			int defValue = context.getResources().getInteger(R.integer.ikke_send_posisjon);
			Double latitude = (double) deltPreferanse.getFloat(Filbehandling.LATITUDE_NOKKEL, defValue);
			Double longitude = (double) deltPreferanse.getFloat(Filbehandling.LONGITUDE_NOKKEL, defValue);
			GeoPoint koordinater = new GeoPoint(latitude.intValue(), longitude.intValue());
			//legg til koordinater i listen over over online enheter og registrer koordinatene i databasen
			FellesFunksjoner.leggTilKoordinater(identifikator, _registrationID, enhetsNavn, koordinater, true);
			DatabaseFunksjoner.registrerKoordinateriDatabase(context, identifikator, _registrationID, latitude, longitude);
		} catch (SQLiteException ex) {
			String feilmelding = context.getString(R.string.unknown_error_exception) + ex.getMessage();
			opprettMelding(feilmelding);
		} catch (Exception ex) { 
			String feilmelding = context.getString(R.string.unknown_error_exception) + ex.getMessage();
			opprettMelding(feilmelding);
		} //try/catch
	} //sendKoordinaterTilServer

	/**
	 * Utgangspunktet er at denne blir kalt i onDestroy(), når applikasjon avsluttes. <br /> 
	 * Det sendes først en offline melding til server, etterfulgt av sletting fra databasen. <br />
	 * Grunnen til at det sendes en offline melding er pga bruker kan velge å fortsette å motta 
	 * posisjonsdata etter applikasjon er avsluttet. 
	 * Under sletting blir det sjekket hva bruker vil beholde, og de rutene bruker ikke vil beholde slettes.
	 */
	private void startSlettingFraDatabase() {
		Context context = _mAsyncTask.get().getApplicationContext();
		ServerTilkobling.sendOfflineMeldingTilServer(context, _registrationID);
		FellesFunksjoner.slettAlleRuterFraDatabase();
	} //startSlettingFraDatabase

	/**
	 * Sletter alle fremviste ruter på valgt visning. <br />
	 * Denne funksjonen kalles fra VisRuterActivity.
	 * @see VisRuterActivity
	 */
	private void startSpesifisertSletting() {
		Context context = _mAsyncTask.get().getApplicationContext();
		SharedPreferences deltPreferanse = Filbehandling.getSharedPreferances(context);
		long identifikator = deltPreferanse.getLong(Filbehandling.ENHETS_IDENTIFIKATOR, 0);
		final int sletting = VisRuterActivity.getValgtVisning();
		switch (sletting) {
		case VisRuterActivity.VIS_ALLE_RUTER:
			LoggingContentProvider.dropDatabase(1);
			FellesFunksjoner.slettIdentifikator();
			break;
		case VisRuterActivity.VIS_ANDRES_RUTER:
			if (identifikator > -1) {
				DatabaseFunksjoner.slettAndresRuterFraDatabasen(identifikator);
			} //if (identifikator > -1)
			break;
		case VisRuterActivity.VIS_EGNE_RUTER:
			if (identifikator > -1) {
				String fkEnhet = LoggingContentProvider.TBLKOORDINATER_FOREIGN_KEY;
				String where = fkEnhet + " = " + identifikator;
				//slett ruten(e) fra tblkoordinater
				LoggingContentProvider.delete(LoggingContentProvider.DB_TBLKOORDINATER, where, null);
			} //if (identifikator > -1)
			break;
		} //switch
	} //startSpesifisertSletting

	@Override
	public void onPostExecute(Void result) {
		String melding = "";
		//er oppgaven som ble startet sletting av ruter?
		if (_oppgave == SLETT_SPESIFISERTE_RADER) {
			//oppdater listview så bruker ser at elementene er slettet
			VisRuterActivity.updateListView();
			//fjern rutene fra kartet, siden historikken er slettet
			FellesFunksjoner.fjernHistorikkRuter();
			melding = "Sletting fullført.";
			opprettMelding(melding);
		} //if (_oppgave == SLETT_SPESIFISERTE_RADER)
	} //onPostExecute

	/**
	 * Oppretter en melding som vises på grensesnittet via Toast.
	 * @param melding - String: Melding som skal vises
	 */
	private void opprettMelding(String melding) {
		//opprett meldingsobjekt og sett argument
		Message msg = new Message();
		msg.arg1 = SEND_MELDING_VIA_TOAST;
		//sett og vis melding til bruker
		msg.obj = (Object)melding;
		handleMessage(msg);
	} //opprettMelding

	public void handleMessage(Message msg) {
		Context context = _mAsyncTask.get().getApplicationContext();
		Toast.makeText(context, msg.obj.toString(), Toast.LENGTH_LONG).show();
	} //handleMessage
} //AsyncTraad