package com.example.oblig3_knutlucasandersen_del1;

//java-import
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.Locale;
//android-import
import android.content.BroadcastReceiver;
import android.content.ContentResolver;
import android.content.ContentValues;
import android.content.Context;
import android.content.Intent;
import android.net.wifi.WifiManager;
import android.os.Bundle;
import android.telephony.SmsMessage;
import android.telephony.TelephonyManager;
import android.widget.Toast;

/**
 * BroadcastReceiver som plukker opp handlinger fra systemet.<br />
 * Deretter blir handlingen logget hvis bruker har satt at valgt 
 * element skal loggføres.
 * @author Knut Lucas Andersen
 */
public class Kringkastningsmottaker extends BroadcastReceiver {
	//konstanter for indeks til eksisterende kategorier
	private final int TELEFONI = 0;
	private final int NETTVERK = 1;
	private final int POSISJONERING = 2;
	private final int SMS = 3;

	@Override
	public void onReceive(Context context, Intent mottaker) {
		try {
			//les verdier fra fil - hva ønsker bruker å logge?
			ArrayList<Boolean> loggListe = Filbehandling.lesFraFil(context);
			//hent ut kategorien for hendelsen som BroadCastReceiver har plukket opp
			int kategori = hentIndeksForKategori(context, mottaker);
			//hvilken kategori er "plukket opp"?
			switch (kategori) {
			case TELEFONI:
				//skal denne kategorien logges?
				if (loggListe.get(kategori) == true) {
					//logg kategorien
					loggTelefoni(context, kategori, mottaker);
				} //if (loggListe.get(kategori) == true)
				break;				
			case NETTVERK:
				//skal denne kategorien logges?
				if (loggListe.get(kategori) == true) {
					loggNettverk(context, kategori, mottaker);
				} //if (loggListe.get(kategori) == true)
			case POSISJONERING:
				//skal denne kategorien logges?
				if (loggListe.get(kategori) == true) {
					loggPosisjonering(context,kategori, mottaker);
				} //if (loggListe.getkategori) == true)
			case SMS:
				//skal denne kategorien logges?
				if (loggListe.get(kategori) == true) {
					loggSMS(context, kategori, mottaker);
				} //if (loggListe.getkategori) == true)
			default:
				break;
			} //switch			
		} catch (Exception ex) {
			//en feil oppsto, vis feilmelding
			Toast.makeText(context, 
					"En feil oppsto under logging til databasen.\nFeilen var: " + ex.getMessage(), 
					Toast.LENGTH_LONG).show();
		} //try/catch
	} //onReceive

	/**
	 * Denne funksjonen plukker opp hvilken handling (action) som BroadcastReceiver fanget opp.<br />
	 * Deretter sjekkes det på hvilken kategori oppfanget handling (action) hører til.<br />
	 * Hvis kategori ble funnet, returneres kategoriens indeks (plass i string-array).<br />
	 * Se også funksjonens interne forklaring.
	 * @param context
	 * @param mottaker Intent som er fanget opp av BroadcastReceiver
	 * @return integer - Indeks til gitt kategoris plass i string-array (-1 hvis ikke funnet)
	 */
	private int hentIndeksForKategori(Context context, Intent mottaker) {
		//Kort intern oppsummering:
		//plukk ut handlingen (action) som kringkaster mottok/fanget opp
		//finn ut hvilken handling (action) som skjedde { Eks: Telefonanrop } 
		//basert på registrerte kategorierer, plukk ut id'n som stemmer overens med action
		//registrerte kategoriers action finnes i strings.xml og kan derfor brukes til sammenligning
		//returner indeksen som deretter kan brukes for å loggføre korrekt kategori
		//intent kan og brukes siden for å hente ut flere detaljer tilknyttet loggføring

		//satt til -1 for at rett kategori indeks skal returneres (-1: kategori ikke funnet)
		int indeks = -1;
		//variabel som leser inn om det er et innkommende anrop
		boolean innkommendeAnrop = false;
		//hent hendelsen (action) som ble fanget opp
		String hendelse = mottaker.getAction();
		//er hendelsen noe som påvirker telefonens status?
		if (hendelse.equals(context.getString(R.string.phone_state))) {
			//hendelsen påvirker telefonens status, hent ut verdi som tilsier om det 
			//er et innkommende anrop
			innkommendeAnrop = (mottaker.getStringExtra(TelephonyManager.EXTRA_STATE).equals(TelephonyManager.EXTRA_STATE_RINGING));
		} //if (hendelse.equals(context.getString(R.string.phone_state)))
		//hvilken hendelse ble mottatt/plukket opp?
		if ( (hendelse.equals(context.getString(R.string.phone_state)) && innkommendeAnrop) ||
				hendelse.equals(context.getString(R.string.utgaaende_anrop))) {
			//hendelsen som ble plukket opp hører innunder kategorien telefoni
			indeks = TELEFONI;
		} else if (hendelse.equals(context.getString(R.string.wifi_endret1)) 
				|| hendelse.equals(context.getString(R.string.wifi_endret2))) {
			//hendelsen som ble plukket opp hører innunder kategorien nettverk 
			indeks = NETTVERK;
		} else if (hendelse.equals(context.getString(R.string.gps_status))) {
			//hendelsen som ble plukket opp hører innunder kategorien posisjonering
			indeks = POSISJONERING;
		} else if (hendelse.equals(context.getString(R.string.sms_mottatt))) {
			//hendelsen som ble plukket opp hører innunder kategorien sms
			indeks = SMS;
		} //if (hendelse.equals(...)))
		return indeks;
	} //hentIndeksForKategori

	/**
	 * Denne metoden logger hendelser (actions) tilknyttet telefonanrop.<br />
	 * Loggføring skiller mellom innkommende og utgående anrop. <br />
	 * Verdiene blir så oversendt til metoden: <br />
	 * {@link #insertLoggTilDatabase(Context context, String kategori, String tekst, String detaljer)}
	 * @param context
	 * @param indeks - Integer: kategoriens indeks/plass i string-array
	 * @param mottaker - Intent fanget opp av BroadcastReceiver
	 * @throws Exception Kaster feil tilknyttet insetting i database eller oppfanging av action
	 */
	private void loggTelefoni(Context context, int indeks, Intent mottaker)  throws Exception {
		String detaljer = "";
		//hent ut kategorien på gitt indeks fra string array'n
		String kategori = context.getResources().getStringArray(R.array.array_kategorier)[indeks];
		//hent ut hendelsen (innkommende/utgående anrop)
		String tekst = mottaker.getAction();
		boolean innkommendeAnrop = (mottaker.getStringExtra(TelephonyManager.EXTRA_STATE).equals(TelephonyManager.EXTRA_STATE_RINGING));
		//hva slags anrop er det?
		if (innkommendeAnrop) {
			//sett at det er et innkommende anrop
			tekst = "Innkommende anrop";
			//telefonnummeret relatert til anropet
			detaljer = mottaker.getStringExtra(TelephonyManager.EXTRA_INCOMING_NUMBER);			
		} else if (tekst.equals(context.getString(R.string.utgaaende_anrop))) {
			//sett at det er et utgående anrop
			tekst = "Utgående anrop";
			//telefonnummeret relatert til anropet
			detaljer = mottaker.getStringExtra(Intent.EXTRA_PHONE_NUMBER);
		} //if (tekst.equals(...)))		
		//send over verdiene som skal lagres i databasen
		insertLoggTilDatabase(context, kategori, tekst, detaljer);
	} //loggTelefoni

	/**
	 * Denne metoden logger nettverksrelaterte hendelser.
	 * --- Grunnet emulator ikke har Wifi så er denne metoden uprøvd --- <br />
	 * Verdiene blir så oversendt til metoden: <br />
	 * {@link #insertLoggTilDatabase(Context context, String kategori, String tekst, String detaljer)}
	 * @param context
	 * @param indeks - Integer: kategoriens indeks/plass i string-array
	 * @param mottaker - Intent fanget opp av BroadcastReceiver
	 * @throws Exception Kaster feil tilknyttet insetting i database eller oppfanging av action
	 */
	private void loggNettverk(Context context, int indeks, Intent mottaker)  throws Exception {
		String tekst = "", detaljer = "";
		//hent ut kategorien på gitt indeks fra string array'n
		String kategori = context.getResources().getStringArray(R.array.array_kategorier)[indeks];
		//hent ut tilstand/status til wifi
		int wifiStatus = mottaker.getIntExtra(WifiManager.EXTRA_WIFI_STATE, WifiManager.WIFI_STATE_UNKNOWN);
		//hvilken tilstand er wifi?
		switch(wifiStatus){
		case WifiManager.WIFI_STATE_DISABLED:
			tekst = "WIFI STATE DISABLED";
			detaljer = "WiFi (trådløs tilkobling) ble slått av.";
			break;
		case WifiManager.WIFI_STATE_DISABLING:
			tekst = "WIFI STATE DISABLING";
			detaljer = "WiFi (trådløs tilkobling) holder på å slåes av.";
			break;
		case WifiManager.WIFI_STATE_ENABLED:
			tekst = "WIFI STATE ENABLED";
			detaljer = "WiFi (trådløs tilkobling) ble slått på.";
			break;
		case WifiManager.WIFI_STATE_ENABLING:
			tekst = "WIFI STATE ENABLING";
			detaljer = "WiFi (trådløs tilkobling) holder på å slåes på.";
			break;
		case WifiManager.WIFI_STATE_UNKNOWN:
			tekst = "WIFI STATE UNKNOWN";
			detaljer = "WiFi (trådløs tilkobling) status er ukjent.";
			break;
		} //switch
		//send over verdiene som skal lagres i databasen
		insertLoggTilDatabase(context, kategori, tekst, detaljer);
	} //loggNettverk

	/**
	 * Denne metoden logger posisjonering og lokaliseringsbaserte hendelser. <br />
	 * --- Grunnet emulator ikke har gps så er denne metoden uprøvd --- <br />
	 * Verdiene blir så oversendt til metoden: <br />
	 * {@link #insertLoggTilDatabase(Context context, String kategori, String tekst, String detaljer)}
	 * @param context
	 * @param indeks - Integer: kategoriens indeks/plass i string-array
	 * @param mottaker - Intent fanget opp av BroadcastReceiver
	 * @throws Exception Kaster feil tilknyttet insetting i database eller oppfanging av action
	 */
	private void loggPosisjonering(Context context, int indeks, Intent mottaker)  throws Exception {
		String tekst = "", detaljer = "";
		//hent ut kategorien på gitt indeks fra string array'n
		String kategori = context.getResources().getStringArray(R.array.array_kategorier)[indeks];
		//hent ut status for om gps ble slått på/av (false som default retur verdi)
		boolean gpsAktiv = mottaker.getBooleanExtra(context.getString(R.string.gps_intent_enabled), false);
		//ble gps slått på?
		if (gpsAktiv) {
			detaljer = "GPS ble aktivert.";
		} else { //gps ble slått av
			detaljer = "GPS ble slått av.";
		} //if (gpsAktiv)		
		//send over verdiene som skal lagres i databasen
		insertLoggTilDatabase(context, kategori, tekst, detaljer);
	} //loggPosisjonering

	/**
	 * Denne metoden logger mottatte sms.  <br />
	 * Den henter ut nr'et som sms ble sendt fra og sms'ns innhold.<br />
	 * Ifølge <a href="http://stackoverflow.com/questions/990558/android-broadcast-receiver-for-sent-sms-messages">
	 * StackOverflow</a> 
	 * så går det ikke an å registrere/logge utgående sms.<br />
	 * Verdiene blir så oversendt til metoden: <br />
	 * {@link #insertLoggTilDatabase(Context context, String kategori, String tekst, String detaljer)}
	 * @param context
	 * @param indeks - Integer: kategoriens indeks/plass i string-array
	 * @param mottaker - Intent fanget opp av BroadcastReceiver
	 * @throws Exception Kaster feil tilknyttet insetting i database eller oppfanging av action
	 */
	private void loggSMS(Context context, int indeks, Intent mottaker)  throws Exception {
		String tekst = "", detaljer = "";
		SmsMessage sms = null;
		Bundle bundle = mottaker.getExtras();
		//hent ut kategorien på gitt indeks fra string array'n
		String kategori = context.getResources().getStringArray(R.array.array_kategorier)[indeks];
		if (bundle != null) {
			//legg data tilknyttet mottatt SMS i array
			Object[] smsExtra = (Object[]) bundle.get("pdus");
			sms = SmsMessage.createFromPdu((byte[])smsExtra[0]);
			//hent ut meldingen som ble sendt
			tekst = sms.getMessageBody().toString();
			//legg avsenders telefonnr i detaljer
			detaljer += "SMS fra " + sms.getOriginatingAddress();
			//send over verdiene som skal lagres i databasen
			insertLoggTilDatabase(context, kategori, tekst, detaljer);
		} //if (bundle != null)
	} //loggSMS

	/**
	 * Denne metoden oppretter et objekt av ContentValues, som legger inn 
	 * verdiene som skal lagres i databasen. Deretter blir ContentValues 
	 * objektet oversendt til LoggingContentProvider for lagring i selve 
	 * databasen.
	 * @param kategori - Kategorien til loggen (String)
	 * @param tekst - Tekst tilknyttet loggen (String)
	 * @param detaljer - Detaljer tilknyttet loggen (String)
	 * @throws Exception Kaster feil dersom insertion i database feiler
	 */
	private void insertLoggTilDatabase(Context context, String kategori, String tekst, String detaljer) throws Exception {		
		//opprett objekt som skal inneholde verdiene
		ContentValues verdier = new ContentValues();
		ContentResolver cr = context.getContentResolver();
		//legg inn verdier som skal logges
		verdier.put(LoggingContentProvider.LOGG_DATO, dagensDato());
		verdier.put(LoggingContentProvider.LOGG_TIDSPUNKT, gjeldendeTidspunkt());
		verdier.put(LoggingContentProvider.LOGG_KATEGORI, kategori);		
		verdier.put(LoggingContentProvider.LOGG_TEKST, tekst);		
		verdier.put(LoggingContentProvider.LOGG_DETALJER, detaljer);
		//lagre logg i databasen
		/*/DEBUGGING - sjekk av oversendte verdiers innhold
		Toast.makeText(context, 
				"dato: " + dagensDato() + "\ntidspunkt: " + gjeldendeTidspunkt() 
				+ "\nkategori: " + kategori + "\nDetalj&Tekst:\n" + tekst +"\n"+ detaljer, 
				Toast.LENGTH_LONG).show(); //*/
		cr.insert(LoggingContentProvider.LOGG_URI, verdier);
	} //insertLoggTilDatabase

	/**
	 * Denne funksjonen oppretter en string som inneholder datoen for logging.
	 * @return String - Dagens dato i form av dd/MM-yyyy
	 */
	private String dagensDato() {
		//datoformat for visning av dato
		SimpleDateFormat datoFormatering = new SimpleDateFormat("dd/MM-yyyy", Locale.UK);
		//variabel for dagens dato
		Date dato = new Date();
		//legger til datoformatering og dagens dato
		return datoFormatering.format(dato);		
	} //dagensDato

	/**
	 * Denne funksjonen oppretter en string som inneholder tidspunkt for logging.
	 * @return String - Tidspunkt i form av HH:mm:ss
	 */
	private String gjeldendeTidspunkt() {
		//datoformat for visning av dato
		SimpleDateFormat datoFormatering = new SimpleDateFormat("HH:mm:ss", Locale.UK);
		//variabel for dagens dato
		Date dato = new Date();		
		//legger til datoformatering og dagens dato
		return datoFormatering.format(dato);		
	} //dagensDato
} //Kringkastningsmottaker