package dt.hin.android.kl_andersen;

//java-import
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import java.util.Locale;
//android-import
import android.app.AlertDialog;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.location.Address;
import android.location.Criteria;
import android.location.Geocoder;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.os.Bundle;
import android.text.Html;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ListView;
import android.widget.Toast;
//google-import
import com.google.android.gcm.GCMRegistrar;
import com.google.android.maps.GeoPoint;
import com.google.android.maps.MapActivity;
import com.google.android.maps.MapController;
import com.google.android.maps.MapView;
import com.google.android.maps.MyLocationOverlay;
import com.google.android.maps.Overlay;

/**
 * Aktiviteten som vises til bruker ved oppstart. <br />
 * Det er her Google Maps lastes inn og ruter blir vist.
 * @author Knut Lucas Andersen
 */
public class MainMapActivity extends MapActivity implements LocationListener {
	/** Brukes av MapOverlay for å sette hvor på kartet bruker trykket **/
	public static int skjermX, skjermY;
	private GeoPoint koordinater;
	private MapOverlay mapOverlay;
	private static Context context;
	private static MapView mapView;
	private static AlertDialog alert;
	private List<Overlay> overlayList;
	private static String bestProvider;
	private static boolean isStreetView;
	private static AsyncTraad mAsyncTraad;
	private static MapController controller;
	private static LocationManager locationManager;
	private static SharedPreferences deltPreferanse;
	private static LocationListener myLocationListener;

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		try {
			setContentView(R.layout.activity_main);
			//hent ut verdier for å sjekke om bruker er tilkoblet internett
			ConnectivityManager tilkoblingsManager = (ConnectivityManager) getSystemService(Context.CONNECTIVITY_SERVICE);
			NetworkInfo nettverksInfo = tilkoblingsManager.getActiveNetworkInfo();
			if (nettverksInfo != null && nettverksInfo.isConnected()) {
				opprettMapViewOgObjekter();
			} else {
				//bruker er ikke tilkoblet, vis feilmelding
				String feilmelding = getString(R.string.no_internet_connection_found);
				Toast.makeText(this, feilmelding, Toast.LENGTH_LONG).show();
				Filbehandling.oppdaterSystemLogg(this, feilmelding);
			} //if (nettverksInfo != null && nettverksInfo.isConnected())
		} catch (SecurityException ex) {
			//denne feilen oppstår dersom en tillatelse mangler i AndroidManifest
			ex.printStackTrace();
		} catch (IllegalArgumentException ex) {
			String feilmelding = "";
			if (bestProvider == null) {
				//ingen tilbydere/providers tilgjengelig
				feilmelding = getString(R.string.illegal_argument_exception);
				Filbehandling.oppdaterSystemLogg(this, feilmelding);
			} //if (bestProvider == null)
			Toast.makeText(this, feilmelding + ex.getMessage(), Toast.LENGTH_LONG).show();
		} catch (NullPointerException ex) {
			ex.printStackTrace();
			String feilmelding = getString(R.string.nullpointer_error_exception);
			Toast.makeText(this, feilmelding + ex.getMessage(), Toast.LENGTH_LONG).show();
		} catch (RuntimeException ex) {
			ex.printStackTrace();
			//denne feilen dukker opp f.eks. hvis den feiler på å opprette grensesnitt
			String feilmelding = getString(R.string.runtime_error_exception);
			Toast.makeText(this, feilmelding + ex.getMessage(), Toast.LENGTH_LONG).show();
		} catch (Exception ex) {
			ex.printStackTrace();
			String feilmelding = getString(R.string.unknown_error_exception);
			Toast.makeText(this, feilmelding + ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //onCreate

	private void opprettMapViewOgObjekter() {
		//oppretter objekt av MapView
		mapView = (MapView) findViewById(R.id.mapview);
		//bruk zoom som følger med google maps
		mapView.setBuiltInZoomControls(true);
		context = this;
		isStreetView = !mapView.isSatellite();
		myLocationListener = this;
		deltPreferanse = Filbehandling.getSharedPreferances(this);
		setZoomNivaa();
		FellesFunksjoner.initialiserFunksjoner(context, deltPreferanse);
		initialiserKart();
		//opprett locationlistener, sett verdier og start requestUpdates()
		oppdaterLocationListener();
		//koble til GCM
		opprettTilkoblingTilGCM();
	} //opprettMapViewOgObjekter

	public static void setZoomNivaa() {
		int defaultZoom = context.getResources().getInteger(R.integer.default_zoom);
		defaultZoom = deltPreferanse.getInt(Filbehandling.ZOOM_VERDI_NOKKEL, defaultZoom);
		controller = mapView.getController();
		controller.setZoom(defaultZoom);
	} //settZoomNivaa

	private void initialiserKart() {
		//koordinater skal ikke sendes før bruker har initialisert (via actionbar)
		Filbehandling.lagreUtsendingAvKoordinater(this, false);
		locationManager = (LocationManager)getSystemService(Context.LOCATION_SERVICE);
		//opprett mapoverlay og objekt som skal vises (mylocationoverlay)
		mapOverlay = new MapOverlay(context);		
		overlayList = mapView.getOverlays();
		overlayList.add(mapOverlay);
		MyLocationOverlay myLocationOverlay = new MyLocationOverlay(this, mapView);
		overlayList.add(myLocationOverlay);		
		myLocationOverlay.enableMyLocation();
		//opprett criteria for den beste tilbyderen
		Criteria criteria = new Criteria();
		criteria.setAccuracy(Criteria.ACCURACY_FINE);
		criteria.setPowerRequirement(Criteria.POWER_LOW);
		criteria.setAltitudeRequired(false);
		criteria.setBearingRequired(false);
		criteria.setSpeedRequired(false);
		criteria.setCostAllowed(true);
		bestProvider = locationManager.getBestProvider(criteria, true);
	} //initialiserKart

	protected static void opprettTilkoblingTilGCM() {
		//hent denne enhetens registreringsID
		final String registrationID = GCMRegistrar.getRegistrationId(context);
		//er enhet registrert fra før?
		if (registrationID.equals("")) {
			//enhet ikke registrert, hent registrationID for denne enheten
			GCMRegistrar.register(context, context.getString(R.string.sender_id));
		} else {
			//er enhet registrert på server?
			if (GCMRegistrar.isRegisteredOnServer(context)) {
				String feilmelding = context.getString(R.string.already_registered);
				Filbehandling.oppdaterSystemLogg(context, feilmelding);
				Toast.makeText(context, feilmelding, Toast.LENGTH_LONG).show();
				//registrert på server, hent ut brukers registrationID fra sharedpreferences
				String regID = deltPreferanse.getString(Filbehandling.REGISTRATIONID_NOKKEL, "");
				//sjekker for sikkerhets skyld at det er en verdi registrert i SP
				if (!regID.equals("")) {
					//se utdypende kommentar i ServerTilkobling.gjenopprettServerTilkobling()
					//kort forklart: forsikrer at bruker er tilkoblet vår server
					mAsyncTraad = new AsyncTraad((MainMapActivity) context, regID, AsyncTraad.GJENOPRETT_SERVER_TILKOBLING);
					mAsyncTraad.execute();
				} //if (!regID.equals(""))
			} else {
				//siden det er nødvendig å kansellere tråd via onDestroy() brukes Asynctask
				mAsyncTraad = new AsyncTraad((MainMapActivity) context, registrationID, AsyncTraad.AVREGISTRERING);
				mAsyncTraad.execute();
			} //if (GCMRegistrar.isRegisteredOnServer(context))
		} //if (registrationID.equals(""))
	} //opprettTilkoblingTilGCM

	/**
	 * Henter ut verdier for oppdatering i tid og avstand fra SharedPreferences
	 * og setter disse verdiene via LocationManager.requestLocationUpdates(...).
	 * @see Filbehandling
	 * @see SharedPreferences
	 * @see LocationManager#requestLocationUpdates(long, float, Criteria, android.app.PendingIntent)
	 */
	public static void oppdaterLocationListener() {
		//hent ut verdier for hvor ofte data skal sendes (i minutter)
		float defValue = context.getResources().getInteger(R.integer.default_minutt);
		float minutter = deltPreferanse.getFloat(Filbehandling.OPPDATERINGSINTERVALL_MINUTT_NOKKEL, defValue);
		//konverter minutter til millisekunder
		minutter = minutter * 60 * 1000;
		long milliSekunder = (long)minutter;
		//hent ut verdier for hvor ofte data skal sendes (i meter)
		defValue = context.getResources().getInteger(R.integer.default_meter);
		float meter = deltPreferanse.getFloat(Filbehandling.OPPDATERINGSINTERVALL_METER_NOKKEL, defValue);
		//registrer for oppdateringer
		locationManager.requestLocationUpdates(bestProvider, milliSekunder, meter, myLocationListener);
	} //oppdaterLocationListener

	@Override
	public void onLocationChanged(Location location) {
		//Kjøres når enten tiden (X sekunder) løper ut eller forflytningen > X meter
		Double latitude = location.getLatitude() * 1E6;
		Double longitude = location.getLongitude() * 1E6;
		koordinater = new GeoPoint(latitude.intValue(), longitude.intValue());
		controller = mapView.getController();
		controller.setCenter(koordinater);
		controller.animateTo(koordinater);
		controller.setZoom(mapView.getZoomLevel());
		//lagre den siste posisjonen
		Filbehandling.lagreSisteLatitude(this, Float.parseFloat(latitude.toString()));
		Filbehandling.lagreSisteLongitude(this, Float.parseFloat(longitude.toString()));
		sendKoordinaterTilServer();
		mapView.invalidate();
	} //onLocationChanged

	private void sendKoordinaterTilServer() {
		String registrationID = deltPreferanse.getString(Filbehandling.REGISTRATIONID_NOKKEL, "");
		if (!registrationID.equals("")) {			
			//start utsending av koordinater ved å bruke AsyncThread-klassen
			mAsyncTraad = new AsyncTraad(this, registrationID, AsyncTraad.SEND_POSISJON);
			mAsyncTraad.execute();
		} //if (!registrationID.equals(""))
	} //sendKoordinaterTilServer

	/**
	 * Oppretter en Pop-up meny ved bruk av AlertDialog.Builder med følgende menyvalg: <br />
	 * - Vis adresse <br />
	 * - Endre visning (veksle mellom streetview og sateliteview) <br />
	 * - Finn min lokasjon (setter visning til brukers siste posisjon) <br />
	 * - Vis online brukere <br />
	 * - Avbryt
	 * @see AlertDialog.Builder
	 */
	public static void opprettPopupDialog() {
		try {
			String endreVisningTekst = "";
			//opprett en AlertDialog som ser ut som en pop-up meny
			final AlertDialog.Builder popUp = new AlertDialog.Builder(context);
			//bruker html for å sette ny farge på teksten (hvit farge)
			popUp.setTitle(Html.fromHtml("<font color='#FFFFFF'>Alternativer</font>"));
			LayoutInflater inflater = LayoutInflater.from(context);
			View view = inflater.inflate(R.layout.popup_dialog, null); 		
			//siden bruker kan endre visning, hent ut knappen for endring av visning
			Button btnEndreVisning = (Button) view.findViewById(R.id.btnEndreVisning);
			//hvilken view er aktiv på kartet?
			if (isStreetView) {
				endreVisningTekst = context.getString(R.string.btnEndreVisning_1);
			} else {
				endreVisningTekst = context.getString(R.string.btnEndreVisning_2);
			} //if (isStreetView)
			//sett den korrekte teksten på knappen
			btnEndreVisning.setText(endreVisningTekst);
			popUp.setView(view);
			alert = popUp.create();
			alert.show();
		} catch (NullPointerException ex) {
			ex.printStackTrace();
			String feilmelding = context.getString(R.string.nullpointer_error_exception);
			Toast.makeText(context, feilmelding, Toast.LENGTH_LONG).show();
		} catch (Exception ex) {
			String feilmelding = context.getString(R.string.unknown_error_exception);
			Toast.makeText(context, feilmelding + ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //opprettPopupDialog

	public void onClickVisAdresse(View v) {
		try {
			String visning = "";
			Geocoder geoCode = new Geocoder(getBaseContext(), Locale.getDefault());
			GeoPoint beroringsPunkt = MapOverlay.getBeroringsPunkt();
			//finn adressen til punktet som ble berørt
			List<Address> adresseListe = geoCode.getFromLocation(beroringsPunkt.getLatitudeE6() / 1E6, beroringsPunkt.getLongitudeE6() / 1E6, 1);
			if (adresseListe.size() > 0) {
				for (int i = 0; i < adresseListe.get(0).getMaxAddressLineIndex(); i++) {
					visning += adresseListe.get(0).getAddressLine(i) + "\n";
				} //for
			} //if (adresseListe.size() > 0)
			/**
			 * Siden uthenting av adresse kan være et behov, så lagres adressen i loggen.
			 * Men, dersom logging er slått av, så ville ikke adressen blitt lagret.
			 * Derfor sjekkes det her om bruker vil lagre systemlogger. Dersom bruker ikke 
			 * vil lagre systemlogger, slå den på bare for at adressen skal lagres i logg, 
			 * for deretter å slå av logging igjen etter brukers ønske.
			 */
			boolean lagreLogg = deltPreferanse.getBoolean(Filbehandling.LOGG_MELDINGER_NOKKEL, false);
			if (!lagreLogg) {
				//slå på logging for at adressen skal lagres i logg
				Filbehandling.lagreValgLoggingAvMeldinger(context, true);
				Filbehandling.oppdaterSystemLogg(context, visning);
				//slå av logging igjen 
				Filbehandling.lagreValgLoggingAvMeldinger(context, false);
			} else {
				Filbehandling.oppdaterSystemLogg(context, visning);
			} //if (!lagreLogg)
			if (!visning.equals("")) {
				visning += "\nAdressen ble lagret i logg.";
			} //if (!visning.equals(""))
			Toast.makeText(this, visning, Toast.LENGTH_LONG).show();
			//lukker alert
			onClickAvbryt(v);
		} catch (IOException ex) {
			ex.printStackTrace();
			String feilmelding = getString(R.string.ioexception_adress_error);
			Filbehandling.oppdaterSystemLogg(this, feilmelding);
			Toast.makeText(this, feilmelding + ex.getMessage(), Toast.LENGTH_LONG).show();
		} catch (Exception ex) {
			String feilmelding = getString(R.string.unknown_error_exception);
			Toast.makeText(this, feilmelding + ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //onClickVisAdresse

	public void onClickEndreVisning(View v) {
		try {
			//sett korrekt visning
			mapView.setSatellite(isStreetView);
			isStreetView = !isStreetView;
			//lukk alertdialog
			onClickAvbryt(v);
			//oppdater mapview
			mapView.invalidate();
		} catch (NullPointerException ex) {
			ex.printStackTrace();
			String feilmelding = getString(R.string.nullpointer_error_exception);
			Toast.makeText(this, feilmelding, Toast.LENGTH_LONG).show();
		} catch (Exception ex) {
			String feilmelding = getString(R.string.unknown_error_exception);
			Toast.makeText(this, feilmelding + ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //onClickEndreVisning

	public void onClickFinnMinLokasjon(View v) {
		try {
			//hent ut siste registrerte posisjon
			Location location = locationManager.getLastKnownLocation(bestProvider);
			if (location != null) {
				Double latitude = location.getLatitude() * 1E6;
				Double longitude = location.getLongitude() * 1E6;
				koordinater = new GeoPoint(latitude.intValue(), longitude.intValue());
				//lagre den siste posisjonen i sharedpreferences
				Filbehandling.lagreSisteLatitude(this, Float.parseFloat(latitude.toString()));
				Filbehandling.lagreSisteLongitude(this, Float.parseFloat(longitude.toString()));
				gaaTilLokasjon(koordinater);
			} else {
				Toast.makeText(this, "Kunne ikke hente din lokasjon...", Toast.LENGTH_LONG).show();
			} //if (location != null) 
			//lukk alertdialog
			onClickAvbryt(v);
		} catch (NullPointerException ex) {
			ex.printStackTrace();
			String feilmelding = getString(R.string.nullpointer_error_exception);
			Toast.makeText(this, feilmelding, Toast.LENGTH_LONG).show();
		} catch (Exception ex) {
			String feilmelding = getString(R.string.unknown_error_exception);
			Toast.makeText(this, feilmelding + ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //onClickFinnMinLokasjon

	public void onClickVisOnlineBrukere(View v) {
		try {
			//lukk det forrige dialogvinduet
			onClickAvbryt(v);
			//hent listen med online enheter
			final ArrayList<Enhet> onlineEnheter = FellesFunksjoner.getOnlineEnheter();
			String[] onlineArray = FellesFunksjoner.getOnlineEnheterArray();
			//finnes det enheter online?
			if (onlineArray != null) {
				//opprett en AlertDialog som ser ut som en pop-up meny
				final AlertDialog.Builder visOnline = new AlertDialog.Builder(context);
				//bruker html for å sette ny farge på teksten (hvit farge)
				visOnline.setTitle(Html.fromHtml("<font color='#FFFFFF'>Online brukere</font>"));
				LayoutInflater inflater = LayoutInflater.from(context);
				View view = inflater.inflate(R.layout.popup_online_brukere, null); 	
				final ListView lvOnlineBrukere = (ListView) view.findViewById(R.id.lvOnlineBrukere);
				//koble adapter og array
				ArrayAdapter<String>adapter = new ArrayAdapter<String>(this, android.R.layout.simple_list_item_1, onlineArray);
				//koble adapter til listview
				lvOnlineBrukere.setAdapter(adapter);
				lvOnlineBrukere.setOnItemClickListener(new OnItemClickListener() {
					@Override
					public void onItemClick(AdapterView<?> parent, View v, int position, long id) {
						//hent ut koordinatene til valgt bruker fra ArrayList
						ArrayList<GeoPoint> koordinatListe = onlineEnheter.get(position).getKoordinater();
						int strl = koordinatListe.size() - 1;
						gaaTilLokasjon(koordinatListe.get(strl));
						//lukk dialogvinduet
						onClickAvbryt(v);
					} //onItemClick
				}); //setOnItemClickListener
				visOnline.setView(view);
				alert = visOnline.create();
				alert.show();
			} else {
				Toast.makeText(context, "Ingen enheter er online...", Toast.LENGTH_LONG).show();
				//lukk dialogvinduet
				onClickAvbryt(v);
			} //if (onlineArray != null)			
		} catch (Exception ex) {
			String feilmelding = getString(R.string.unknown_error_exception);
			Toast.makeText(this, feilmelding + ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //onClickVisOnlineBrukere

	/**
	 * Går til valgt lokasjon basert på oversendt GeoPoint.
	 * @param punkt - GeoPoint: Punktet på kartet som skal vises til bruker
	 */
	public static void gaaTilLokasjon(GeoPoint punkt) {
		controller = mapView.getController();
		controller.setCenter(punkt);
		controller.animateTo(punkt);
		controller.setZoom(mapView.getZoomLevel());
		mapView.invalidate();
	} //gaaTilLokasjon

	public void onClickAvbryt(View v) {
		//lukk AlertDialog
		alert.dismiss();
	} //onClickAvbryt

	/**
	 * For at kartet skal oppdateres realtime, så kreves kall på {@link MapView#invalidate()}. <br />
	 * Men, siden bruker kan stå stille for å følge andre enheter, så kalles det her på 
	 * {@link MapView#postInvalidate()}, <br /> siden denne funksjonen kalles når bruker mottar nye 
	 * posisjonsoppdateringer, i klassen FellesFunksjoner.
	 * @see FellesFunksjoner
	 * @see MapView#invalidate()
	 * @see MapView#postInvalidate()
	 */
	public static void invaliderMapView() {		
		mapView.postInvalidate();
	} //invaliderMapView

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.main_map, menu);
		return true;
	} //onCreateOptionsMenu	

	@Override
	public boolean onPrepareOptionsMenu(Menu menu) {
		super.onPrepareOptionsMenu(menu);
		return true;
	} //onPrepareOptionsMenu

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		Intent intent = null;
		String melding = "";
		switch(item.getItemId()) {
		case R.id.action_send_posisjon:
			//start utsending av brukers koordinater
			Filbehandling.lagreUtsendingAvKoordinater(this, true);
			melding = getString(R.string.startet_sending_av_posdata_melding);
			Toast.makeText(this, melding, Toast.LENGTH_LONG).show();
			Filbehandling.oppdaterSystemLogg(this, melding);
			//hent ut siste registrerte posisjon
			Location location = locationManager.getLastKnownLocation(bestProvider);
			if (location != null) {
				Double latitude = location.getLatitude() * 1E6;
				Double longitude = location.getLongitude() * 1E6;
				//lagre den siste posisjonen i sharedpreferences
				Filbehandling.lagreSisteLatitude(this, Float.parseFloat(latitude.toString()));
				Filbehandling.lagreSisteLongitude(this, Float.parseFloat(longitude.toString()));
				sendKoordinaterTilServer();
			} //if (location != null)
			return true;
		case R.id.action_stopp_send_posisjon:
			//stopp utsending av brukers koordinater
			Filbehandling.lagreUtsendingAvKoordinater(this, false);
			melding = getString(R.string.stoppet_sending_av_posdata_melding);
			Toast.makeText(this, melding, Toast.LENGTH_LONG).show();
			Filbehandling.oppdaterSystemLogg(this, melding);
			return true;
		case R.id.activity_systemlogg:
			intent = new Intent(this, SystemloggActivity.class);
			startActivity(intent);
			return true;
		case R.id.action_settings:
			//hent og sett gjeldende zoom-nivå
			Filbehandling.lagreDefaultZoomVerdi(context, mapView.getZoomLevel());
			intent = new Intent(this, InnstillingsActivity.class);
			startActivity(intent);
			return true;
		default:
			return super.onOptionsItemSelected(item);
		} //switch
	} //onOptionsItemSelected

	public void visBrukersRuter(MenuItem item) {
		//opprett intent
		Intent intent = new Intent(context, VisRuterActivity.class);
		intent.putExtra(VisRuterActivity.VIS_RUTER, VisRuterActivity.VIS_EGNE_RUTER);
		//start aktivitet
		context.startActivity(intent);
	}//visBrukersRuter

	public void visAndresRuter(MenuItem item) {
		//opprett intent
		Intent intent = new Intent(context, VisRuterActivity.class);
		intent.putExtra(VisRuterActivity.VIS_RUTER, VisRuterActivity.VIS_ANDRES_RUTER);
		//start aktivitet
		context.startActivity(intent);
	}//visAndresRuter

	public void visAlleRuter(MenuItem item) {
		//opprett intent
		Intent intent = new Intent(context, VisRuterActivity.class);
		intent.putExtra(VisRuterActivity.VIS_RUTER, VisRuterActivity.VIS_ALLE_RUTER);
		//start aktivitet
		context.startActivity(intent);
	}//visAlleRuter

	public void fjernHistorikkRuter(MenuItem item) {
		FellesFunksjoner.fjernHistorikkRuter();
	} //fjernHistorikkRuter

	@Override
	protected void onPause() {
		super.onPause();
		locationManager.removeUpdates(myLocationListener);
	} //onPause

	@Override
	protected void onResume() {
		super.onResume();
		oppdaterLocationListener();
	} //onResume

	@Override
	protected void onDestroy() {
		String registrationID = deltPreferanse.getString(Filbehandling.REGISTRATIONID_NOKKEL, "");
		mAsyncTraad = new AsyncTraad(this, registrationID, AsyncTraad.SLETT_ALT_FRA_DATABASE);
		mAsyncTraad.execute();
		//er sletting fra databasen i gang?
		if (FellesFunksjoner.getSlettingFerdig()) {
			//sletting er fullført, sjekk at async ikke gjør andre ting
			if (mAsyncTraad != null) {
				mAsyncTraad.cancel(true);
			} // if (mAsyncTraad != null)
		} //if (FellesFunksjoner.getSlettingFerdig())		
		Filbehandling.lagreUtsendingAvKoordinater(this, false);
		locationManager.removeUpdates(myLocationListener);
		//hvis bruker ikke vil fortsette å motta posisjonsdata, avregistrer fra GCM og server
		String nokkel = Filbehandling.MOTTA_POSISJONSDATA_ETTER_AVSLUTNING_NOKKEL;
		boolean mottaPosData = deltPreferanse.getBoolean(nokkel, false);
		if (!mottaPosData) {
			GCMRegistrar.unregister(this);
		} //if (!mottaPosData)
		GCMRegistrar.onDestroy(getApplicationContext());
		super.onDestroy();
	} //onDestroy

	@Override
	protected boolean isRouteDisplayed() {
		return false;
	} //isRouteDisplayed

	@Override
	public void onProviderDisabled(String provider) {
	} //onProviderDisabled

	@Override
	public void onProviderEnabled(String provider) {
	} //onProviderEnabled

	@Override
	public void onStatusChanged(String provider, int status, Bundle extras) {
	} //onStatusChanged	
} //MainMapActivity