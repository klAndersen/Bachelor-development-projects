package dt.hin.android.kl_andersen;

//android-import
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import android.database.sqlite.SQLiteException;
import android.support.v4.app.NotificationCompat;
//google-import
import com.google.android.gcm.GCMBaseIntentService;
import com.google.android.gcm.GCMRegistrar;
import com.google.android.maps.GeoPoint;

/**
 * IntentService som håndterer GCM meldinger og registrering på server. <br />
 * Dette gjelder tilkobling, frakobling, feilmeldinger og visning av 
 * notifications (ny melding fra GCM/Server).
 * @author Knut Lucas Andersen
 */
public class GCMIntentService extends GCMBaseIntentService {
	/** Notification ID til notification som ble mottatt **/
	private static final int NOTIFICATION_ID = 333;
	/** Prosjektets API nøkkel fra Google **/
	private static final String SENDER_ID = "379717811198";
	/** Parameter i melding fra server - verdi som mottas er enhetens navn **/
	private static final String PARAMETER_ENHETSNAVN = "enhetsNavn";
	/** Parameter i melding fra server - verdi som mottas er at enhet gikk offline **/
	private static final String PARAMETER_ENHET_GIKK_OFFLINE = "enhetOffline";

	public GCMIntentService() {
		super(SENDER_ID);
	} //konstruktør

	@Override
	public void onError(Context context, String errorId) {
		String melding = getString(R.string.gcm_error, errorId);
		FellesFunksjoner.visMelding(context, melding, true);
	} //onError

	@Override
	protected boolean onRecoverableError(Context context, String errorId) {
		String melding = getString(R.string.gcm_recoverable_error, errorId);
		FellesFunksjoner.visMelding(context, melding, true);
		return super.onRecoverableError(context, errorId);
	} //onRecoverableError

	@Override
	protected void onRegistered(Context context, String registrationID) {
		String melding = getString(R.string.gcm_registered);
		FellesFunksjoner.visMelding(context, melding, false);
		//enhet ble registrert på GCM server, forsøk å registrere enhet på vår server
		ServerTilkobling.registerOnServer(context, registrationID);
	} //onRegistered

	@Override
	protected void onUnregistered(Context context, String registrationID) {
		String melding = getString(R.string.gcm_unregistered);
		FellesFunksjoner.visMelding(context, melding, false);
		//er bruker registrert på server?
		if (GCMRegistrar.isRegisteredOnServer(context)) {
			ServerTilkobling.unregisterFromServer(context, registrationID);
		} //if (GCMRegistrar.isRegisteredOnServer(context))
	} //onUnregistered

	@Override
	protected void onMessage(Context context, Intent intent) {
		String melding = "", 
				latitude = "",
				longitude = "",
				enhetsNavn = "",
				registrationID = "";
		//uthenting av meldinger - hva slags melding er mottatt?
		if (intent.getStringExtra(context.getString(R.string.readable_message)) != null) {
			melding = getString(R.string.gcm_message);
			melding += intent.getStringExtra(context.getString(R.string.readable_message));
			FellesFunksjoner.visMelding(context, melding, true);
			opprettNotification(context, melding);
		} else {
			if (intent.getStringExtra(PARAMETER_ENHET_GIKK_OFFLINE) != null) {				
				registrationID = intent.getStringExtra(PARAMETER_ENHET_GIKK_OFFLINE);
				FellesFunksjoner.fjernOfflineEnhet(registrationID);
			} else {
				//sjekk at verdi er oversendt og hent ut verdiene
				if (intent.getStringExtra(Filbehandling.REGISTRATIONID_NOKKEL) != null) {
					registrationID = intent.getStringExtra(Filbehandling.REGISTRATIONID_NOKKEL);
				} //if (intent.getStringExtra(PARAMETER_REGISTRATION_ID) != null)
				if (intent.getStringExtra(PARAMETER_ENHETSNAVN) != null) {
					enhetsNavn = intent.getStringExtra(PARAMETER_ENHETSNAVN);
				} //if (intent.getStringExtra(PARAMETER_ENHETSNAVN) != null)
				if (intent.getStringExtra(Filbehandling.LATITUDE_NOKKEL) != null) {
					latitude = intent.getStringExtra(Filbehandling.LATITUDE_NOKKEL);
				} //if (intent.getStringExtra(Filbehandling.LATITUDE_NOKKEL) != null)
				if (intent.getStringExtra(Filbehandling.LONGITUDE_NOKKEL) != null) {
					longitude = intent.getStringExtra(Filbehandling.LONGITUDE_NOKKEL);
				} //if (intent.getStringExtra(Filbehandling.LONGITUDE_NOKKEL) != null)			
				lagreKoordinater(context, registrationID, enhetsNavn, latitude, longitude);
			} //if (intent.getStringExtra(PARAMETER_ENHET_GIKK_OFFLINE) != null)
		} //if (intent.getStringExtra(LESBAR_MELDING) != null)
	} //onMessage

	/**
	 * Lagrer koordinater i databasen (SQLite) og legger de til i ArrayList over online enheter.
	 * @param context - Context
	 * @param registrationID - String: Enhetens registrationID
	 * @param enhetsNavn - String: Enhetens navn
	 * @param latitude - String: Mottatt latitude fra server
	 * @param longitude - String: Mottatt longitude fra server
	 */
	private void lagreKoordinater(Context context, String registrationID, String enhetsNavn, String latitude, String longitude) {
		try {
			Double lat = Double.parseDouble(latitude);
			Double lon = Double.parseDouble(longitude);
			GeoPoint koordinater = new GeoPoint(lat.intValue(), lon.intValue());
			//hent ut indeks for denne enheten for å se om den er registrert
			int indeks = FellesFunksjoner.finnEnhetsIndeks(registrationID);
			long identifikator = -1;
			if (indeks > identifikator) {
				//enhet registrert, hent identifikator
				identifikator = FellesFunksjoner.getOnlineEnheter().get(indeks).getIdentifikator();
			} else {
				//registrer enhet i database
				identifikator = DatabaseFunksjoner.registrerEnhetiDatabase(context, registrationID, enhetsNavn, false);
			} //if (indeks > identifikator)
			FellesFunksjoner.leggTilKoordinater(identifikator, registrationID, enhetsNavn, koordinater, false);
			DatabaseFunksjoner.registrerKoordinateriDatabase(context, identifikator, registrationID, lat, lon);
		} catch (NumberFormatException ex) {
			String feilmelding = context.getString(R.string.number_format_exception2);
			FellesFunksjoner.visMelding(context, feilmelding + ex.getMessage(), true);
		} catch (SQLiteException ex) {
			String feilmelding = context.getString(R.string.sqlite_exception) + ex.getMessage();
			FellesFunksjoner.visMelding(context, feilmelding, true);
		} catch (Exception ex) { 
			ex.printStackTrace();
			String feilmelding = context.getString(R.string.unknown_error_exception);
			FellesFunksjoner.visMelding(context, feilmelding + ex.getMessage(), true);
		} //try/catch
	} //lagreKoordinater

	/**
	 * Oppretter en notification som inneholder meldingen som ble sendt fra GCM.
	 * @param context - Context
	 * @param melding - String: Meldingen som ble sendt
	 */
	private void opprettNotification(Context context, String melding) {
		int ikon = R.drawable.icon_notification;
		//notificationmanager som håndterer notifications (hører innunder system service)
		NotificationManager notificationManager = (NotificationManager) context.getSystemService(Context.NOTIFICATION_SERVICE);
		//bruker NotificationCompat.Builder siden API < 11
		NotificationCompat.Builder notification = new NotificationCompat.Builder(context)
		.setContentTitle(context.getString(R.string.app_name))
		.setContentText(melding)
		.setSmallIcon(ikon);
		//oppretter intent 
		Intent notificationIntent = new Intent(context, MainMapActivity.class);
		//sørg for at intent ikke starter ny aktivitet
		notificationIntent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP | Intent.FLAG_ACTIVITY_SINGLE_TOP);     
		//opprett pendingintent
		PendingIntent notifyIntent = PendingIntent.getActivity(context, 0, notificationIntent, PendingIntent.FLAG_UPDATE_CURRENT);
		//legg pendingintent inn i notification
		notification.setContentIntent(notifyIntent);
		//oppretter notification objekt og sender det til notificationmanager
		notificationManager.notify(NOTIFICATION_ID, notification.build());
	} //opprettNotification
} //GCMIntentService