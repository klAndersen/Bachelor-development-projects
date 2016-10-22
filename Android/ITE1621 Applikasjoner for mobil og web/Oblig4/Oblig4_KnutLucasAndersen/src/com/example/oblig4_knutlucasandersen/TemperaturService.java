package com.example.oblig4_knutlucasandersen;

//java-import
import java.lang.ref.WeakReference;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Locale;
import java.util.Timer;
import java.util.TimerTask;
//android-import
import android.app.Activity;
import android.app.Service;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Handler;
import android.os.HandlerThread;
import android.os.IBinder;
import android.os.Looper;
import android.os.Message;
import android.os.Process;
import android.widget.Toast;

/**
 * Grunnlaget for klassen er basert på 
 * <a href="http://developer.android.com/guide/components/services.html#LifecycleCallbacks">Android Developer: Services</a>. <br />
 * Dette er en Service som tar for seg nedlasting av værdata i bakgrunnen. <br />
 * Den starter opp en Timer som ved første gangs oppstart leser inn tidspunkt fra SharedPreference, <br />
 * og bruker dette som utgangspunkt for hvor lang tid det tar før første nedlasting skjer. <br />
 * Deretter skjer neste nedlasting basert på satt intervall <br />
 * (intervallet varierer etter brukers ønske for hvor ofte nedlasting skal skje).
 * @author Knut Lucas Andersen
 */
public class TemperaturService extends Service {
	// interface for clients that bind
	private IBinder mBinder;
	// indicates whether onRebind should be used
	private boolean mAllowRebind;
	private Looper mServiceLooper;
	private ServiceHandler mServiceHandler;
	private Timer timer;
	private Context context;
	private Intent restartServiceIntent;

	@Override
	public void onCreate() {
		context = getApplicationContext();
		//opprett en ny tråd som kjører i bakgrunnen
		HandlerThread traad = new HandlerThread("ServiceNedlasting", Process.THREAD_PRIORITY_BACKGROUND);
		traad.start();
		//hent HandlerThread's Looper slik at "vår" Handler kan bruke den 
		mServiceLooper = traad.getLooper();
		mServiceHandler = new ServiceHandler(this, mServiceLooper);
		startTimer();
	} //onCreate

	private void startTimer() {
		try {
			timer = new Timer();
			//opprett Sharedpreferance for uthenting av verdier
			int modus = Activity.MODE_PRIVATE;
			SharedPreferences deltPreferanse = getApplicationContext().getSharedPreferences(Filbehandling.DELT_TEMPERATUR_PREFERANSE, modus);
			//hent ut tidsintervall for nedlasting
			int defaultIntervall = getApplicationContext().getResources().getInteger(R.integer.default_time_interval_download);
			int tidsIntervall = deltPreferanse.getInt(Filbehandling.TIDSINTERVALL_NEDLASTING, defaultIntervall);
			//hent ut tidspunkt for neste nedlasting
			String defaultTidspunkt = ActionBarFunksjoner.returnNesteNedlastingsTidspunkt(tidsIntervall);
			String tidspunkt = deltPreferanse.getString(Filbehandling.OPPDATERINGS_TIDSPUNKT_NOKKEL, defaultTidspunkt);
			//forsinkelse før timer starter
			long forsinkelse = hentForsinkelseForTimer(tidspunkt);
			//tid før neste oppdatering
			long nesteOppdatering = tidsIntervall * 3600 * 1000;
			//start opp Timer og sett at den repeteres med oppgitte intervaller
			timer.scheduleAtFixedRate(new TimerTaskClass(), forsinkelse, nesteOppdatering);
		} catch (Exception ex) {
			//en uventet feil oppsto
			ex.printStackTrace();
		} //try/catch
	} //startTimer

	private long hentForsinkelseForTimer(String tidspunkt) {
		//sett forsinkelse til 30 sek
		//(dersom nedlasting først skjer lengre frem i tid, endres dette lenger ned)
		long forsinkelse = 30000;
		try {
			//datoformatering
			Locale locale = Locale.getDefault();
			SimpleDateFormat datoFormatering = new SimpleDateFormat("HH:mm:ss", locale);
			//konverter det registrerte tidspunktet (strengen) til Date
			Date konvertertTidspunkt = datoFormatering.parse(tidspunkt);
			//hent ut dagens dato og konverter det til kun time, minutt og sekund
			Date tidspunktNaa = new Date();			
			String tidNaa = datoFormatering.format(tidspunktNaa);
			tidspunktNaa = datoFormatering.parse(tidNaa);			
			//sammenlign tidspunktene 
			int forskjell = tidspunktNaa.compareTo(konvertertTidspunkt);
			//er det fortsatt tid igjen til neste nedlasting skal skje?
			if (forskjell < 0) {
				//hent ut antall millisekunder til orginalt nedlastingstidspunkt
				forsinkelse = konvertertTidspunkt.getTime() - tidspunktNaa.getTime();
			} //if (forskjell < 0)
		} catch (ParseException ex) {
			//kunne ikke parse/konvertere dato
			ex.printStackTrace();
		} catch (Exception ex) {
			ex.printStackTrace();
		} //try/catch
		return forsinkelse;
	} //hentForsinkelseForTimer

	@Override
	public int onStartCommand(Intent intent, int flags, int startId) {
		// For each start request, send a message to start a job and deliver the
		// start ID so we know which request we're stopping when we finish the job
		Message msg = mServiceHandler.obtainMessage();
		msg.arg1 = startId;
		mServiceHandler.sendMessage(msg);
		//hent ut intent som ble oversendt
		restartServiceIntent = intent;
		// If we get killed, after returning from here, restart
		return START_STICKY;
	} //onStartCommand

	@Override
	public IBinder onBind(Intent intent) {
		// A client is binding to the service with bindService()
		return mBinder;
	} //onBind

	@Override
	public boolean onUnbind(Intent intent) {
		// All clients have unbound with unbindService()
		return mAllowRebind;
	} //onUnbind

	@Override
	public void onRebind(Intent intent) {
		// A client is binding to the service with bindService(),
		// after onUnbind() has already been called
	} //onRebind

	@Override
	public void onDestroy() {
		// The service is no longer used and is being destroyed
		//stopp/avbryt timer
		timer.cancel();
		//henter ut om grunnen til at service avsluttes er pga restart
		//eksempel på restart er ved setting av nytt tidsintervall, noe som 
		//krever oppdatering/endring av timer.scheduleAtFixedRate(...)
		boolean serviceRestarter = restartServiceIntent.getBooleanExtra(ActionBarFunksjoner.RESTART_AV_SERVICE, false);
		//er det restart eller har bruker stoppet service?
		if (!serviceRestarter) {
			//melding til bruker for å fortelle at nedlasting ikke 
			//skjer før service startes opp igjen
			String melding = context.getString(R.string.service_on_destroy);
			Toast.makeText(context, melding, Toast.LENGTH_LONG).show();
		} //if (!serviceRestarter)
		super.onDestroy();
	} //onDestroy

	public void handleMessage(Message msg) {
		//meldinger til GUI-tråden kan håndteres her...
		if (msg.arg1 == NedlastningsBehandler.SEND_MELDING_VIA_TOAST) {
			Toast.makeText(context, msg.obj.toString(), Toast.LENGTH_LONG).show();
		} //if (msg.arg1 == NedlastningsBehandler.SEND_MELDING_VIA_TOAST)
	} //handleMessage

	/**
	 * Klasse som tar for seg oppgaver tilknyttet Timer og 
	 * TimerTask. <br />
	 * Klassens run metode starter nedlasting av værdata.
	 * @author Knut Lucas Andersen
	 */
	private class TimerTaskClass extends TimerTask {

		public TimerTaskClass() {
		} //konstruktør

		@Override
		public void run() {
			NedlastningsBehandler.startNedlastingAvVaerData(context, mServiceHandler);
		} //run
	} //TimerTaskClass

	/**
	 * Handler som mottar beskjeder fra tråden. <br />
	 * Klassen er static og benytter seg av WeakReference for å unngå 
	 * minnelekkasjer.
	 * @author Knut Lucas Andersen
	 */
	private static class ServiceHandler extends Handler {
		//oppretter en WeakReference for å unngå minnelekkasjer
		private final WeakReference<TemperaturService> mService; 

		public ServiceHandler(TemperaturService service, Looper looper) {
			super(looper);
			mService = new WeakReference<TemperaturService>(service);
		} //konstruktør

		@Override
		public void handleMessage(Message msg) {
			TemperaturService service = mService.get();
			//er service aktiv?
			if (service != null) {
				service.handleMessage(msg);
			} //if (service != null) 
		} //handleMessage
	} //ServiceHandler
} //TemperaturService