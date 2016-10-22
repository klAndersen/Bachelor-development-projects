package com.example.oblig4_knutlucasandersen;

//java-import
import java.util.Calendar;
import java.util.TimeZone;
//android-import
import android.app.Activity;
import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.EditText;
import android.widget.Toast;

/**
 * Hjelpeklasse for fellesfunksjoner som er brukes av ActionBar. <br />
 * Klassen ble opprettet for å minske felles funksjoner over Activity'ene som applikasjonen bruker. <br /><br />
 * Funksjoner klassen innholder er: <br />
 * - public {@link #onPrepareOptionsMenu(Context, Menu) onPrepareOptionsMenu} <br />
 * - public {@link #onOptionsItemSelected(Context, MenuItem) onOptionsItemSelected} <br />
 * - public {@link #leggTilMaalestasjon(Context) leggTilMaalestasjon} <br />
 * - public {@link #fjernMaalestasjon(Context) fjernMaalestasjon} <br />
 * - public {@link #startService(Context) startService} <br />
 * - public {@link #stopService(Context) stopService} <br />
 * - public {@link #settOvreTemperaturGrense(Context) settOvreTemperaturGrense} <br />
 * - public {@link #settNedreTemperaturGrense(Context) settNedreTemperaturGrense} <br />
 * - private {@link #visInputDialog(Context, String, String, int) visInputDialog} <br />
 * - private {@link #lagreTemperaturGrense(Context, int, String) lagreTemperaturGrense} <br />
 * - private {@link #settNyttTidsintervall(Context, int) settNyttTidsintervall} <br />
 * - public {@link #returnNesteNedlastingsTidspunkt(int) returnDefaultTidspunkt} <br />
 * @author Knut Lucas Andersen
 */
public class ActionBarFunksjoner {
	public final static String RESTART_AV_SERVICE = "serviceRestarter";
	public final static String VIS_LISTE_FOR_AA_LEGGE_TIL_MAALESTASJON = "leggTilNyMaalestasjon";
	private final static int LAGRE_OVRE_TEMPERATUR_GRENSE = 0;
	private final static int LAGRE_NEDRE_TEMPERATUR_GRENSE = 1;	
	private static boolean serviceRestarter = false;

	/**
	 * Denne funksjonen henter ut verdi (hvis satt) og setter valgt verdi til selektert <br /> 
	 * (listen over radiobuttons for tidsintervall).
	 * @param context - Context
	 * @param menu
	 */
	public static void onPrepareOptionsMenu(Context context, Menu menu) {
		//opprett et objekt av SharedPreference
		int modus = Activity.MODE_PRIVATE;
		SharedPreferences deltPreferanse = context.getSharedPreferences(Filbehandling.DELT_TEMPERATUR_PREFERANSE, modus);
		//hent ut default tidsintervall verdi fra integers.xml
		int defaultVerdi = context.getResources().getInteger(R.integer.default_time_interval_download);
		//hent ut tidsintervall fra sharedpreferences
		int tidsIntervall = deltPreferanse.getInt(Filbehandling.TIDSINTERVALL_NEDLASTING, defaultVerdi);		
		//hvilket tidsintervall er satt?
		switch (tidsIntervall) {
		//tidsintervall er satt til hver time
		case 1:
			//finn menuitem og sett den til checked/valgt
			menu.findItem(R.id.action_settings_download_interval_2h);//R.id.action_settings_download_interval_1h).setChecked(true);
			break;
		case 2:
			menu.findItem(R.id.action_settings_download_interval_2h).setChecked(true);
			break;
		case 4:
			menu.findItem(R.id.action_settings_download_interval_4h).setChecked(true);
			break;
		case 8:
			menu.findItem(R.id.action_settings_download_interval_8h).setChecked(true);
			break;
		case 12:
			menu.findItem(R.id.action_settings_download_interval_12h).setChecked(true);
			break;
		case 24:
			menu.findItem(R.id.action_settings_download_interval_24h).setChecked(true);
			break;
		} //switch		
	} //onPrepareOptionsMenu

	/**
	 * Denne funksjonen sjekker hvilket alternativ som ble klikket i ActionBar 
	 * (RadioButton listen for tidsintervall). <br />
	 * Deretter setter den valgt verdi til selektert (checked) og lagrer tidsintervall 
	 * og tidspunkt for neste nedlasting i SharedPreferences.
	 * @param context - Context
	 * @param item
	 * @return boolean: True - verdi satt/endret, False - ikke innen gitte parametre
	 */
	public static boolean onOptionsItemSelected(Context context, MenuItem item) {
		//hvilket alternativ ble klikket/valgt?
		switch (item.getItemId()) {
		//elementet som ble klikket er tidsintervall for nedlasting
		case R.id.action_settings_download_interval_1h:
			//sett at denne ble valgt
			item.setChecked(true);
			//sett tidsintervallet
			settNyttTidsintervall(context, 1);
			return true;
		case R.id.action_settings_download_interval_2h:
			item.setChecked(true);
			settNyttTidsintervall(context, 2);
			return true;
		case R.id.action_settings_download_interval_4h:
			item.setChecked(true);
			settNyttTidsintervall(context, 4);
			return true;
		case R.id.action_settings_download_interval_8h:
			item.setChecked(true);
			settNyttTidsintervall(context, 8);
			return true;
		case R.id.action_settings_download_interval_12h:
			item.setChecked(true);
			settNyttTidsintervall(context, 12);
			return true;
		case R.id.action_settings_download_interval_24h:
			item.setChecked(true);
			settNyttTidsintervall(context, 24);
			return true;
		default:
			return false;
		} //switch
	} //onOptionsItemSelected

	/******************************* MÅLESTASJON ****************************/
	/**
	 * Starter activity for å legge til målestasjoner nye målestasjoner 
	 * som bruker kan laste ned temperaturverdier til
	 * @param context - Context
	 */
	public static void leggTilMaalestasjon(Context context) {
		//opprett intent
		Intent intent = new Intent(context, OvervaakningsListeActivity.class);
		//legg ved id for stedsliste (listen som skal vises)
		intent.putExtra(OvervaakingActivity.LISTE_SOM_SKAL_VISES, OvervaakningsListeActivity.LEGG_TIL_NY_MAALESTASJON);
		intent.putExtra(VIS_LISTE_FOR_AA_LEGGE_TIL_MAALESTASJON, true);
		//start aktivitet
		context.startActivity(intent);
	} //leggTilMaalestasjon

	/**
	 * Starter activity for å fjerne registrerte målestasjoner som bruker følger
	 * @param context - Context
	 */
	public static void fjernMaalestasjon(Context context) {
		//opprett intent
		Intent intent = new Intent(context, FjernMaalestasjonerActivity.class);
		//start aktivitet
		context.startActivity(intent);
	} //fjernMaalestasjon

	/******************************* SERVICE *******************************/

	/**
	 * Starter service (hvis denne ikke allerede kjører).
	 * @param context - Context
	 */
	public static void startService(Context context) {
		try {
			//opprett intent tilkoblet Service
			Intent serviceIntent = new Intent(context, TemperaturService.class);
			//start service
			context.startService(serviceIntent);
		} catch (Exception ex) {
			//hent ut ledetekst til feilmelding
			String feilmelding = context.getString(R.string.uknown_error_exception);
			//vis feilmelding til bruker
			Toast.makeText(context, feilmelding + ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //startService

	/**
	 * Stopper service (hvis den kjører). <br />
	 * Det sendes også over en boolsk verdi (serviceRestarter) som 
	 * brukes for å sjekke om det er en restart. <br/ > 
	 * Hvis det ikke er en restart (bruker stoppet service), 
	 * så vises melding om at nedlasting ikke skjer før service restartes.
	 * @param context - Context
	 */
	public static void stopService(Context context) {
		try {
			//opprett intent tilkoblet Service
			Intent serviceIntent = new Intent(context, TemperaturService.class);
			//send over verdi for om dette er en restart
			serviceIntent.putExtra(RESTART_AV_SERVICE, serviceRestarter);
			//send over melding (intent) til onStartCommand
			context.startService(serviceIntent);
			//stopp service
			context.stopService(serviceIntent);
			serviceRestarter = false;
		} catch (Exception ex) {
			//hent ut ledetekst til feilmelding
			String feilmelding = context.getString(R.string.uknown_error_exception);
			//vis feilmelding til bruker
			Toast.makeText(context, feilmelding + ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //stopService

	/******************************* TEMPERATURGRENSER  *******************************/

	/**
	 * Oppretter en InputBoks som spør bruker etter øvre temperaturgrense. 
	 * @param context - Context
	 */
	public static void settOvreTemperaturGrense(Context context) {
		try {
			String tittel, melding;
			//hent tittel fra strings.xml
			tittel = context.getString(R.string.inputdialog_title_upper_temperature);
			//hent melding fra strings.xml
			melding = context.getString(R.string.inputdialog_upper_temperature);
			//vis input boks til bruker
			visInputDialog(context, tittel, melding, LAGRE_OVRE_TEMPERATUR_GRENSE);
		} catch (Exception ex) {
			//hent ut ledetekst til feilmelding
			String feilmelding = context.getString(R.string.uknown_error_exception);
			//vis feilmelding til bruker
			Toast.makeText(context, feilmelding + ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //settOvreTemperaturGrense

	/**
	 * Oppretter en InputBoks som spør bruker etter nedre temperaturgrense. 
	 * @param context - Context
	 */
	public static void settNedreTemperaturGrense(Context context) {
		try {
			String tittel, melding;
			//hent tittel fra strings.xml
			tittel = context.getString(R.string.inputdialog_title_lower_temperature);
			//hent melding fra strings.xml
			melding = context.getString(R.string.inputdialog_lower_temperature);
			//vis input boks til bruker
			visInputDialog(context, tittel, melding, LAGRE_NEDRE_TEMPERATUR_GRENSE);
		} catch (Exception ex) {
			//hent ut ledetekst til feilmelding
			String feilmelding = context.getString(R.string.uknown_error_exception);
			//vis feilmelding til bruker
			Toast.makeText(context, feilmelding + ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //settNedreTemperaturGrense

	/**
	 * Oppretter en Dialog av typen AlertDialog. Hensikten er å fremvise en InputBoks 
	 * som ber bruker om input. Hva bruker blir bedt om avhenger av oversendt tittel, 
	 * melding og id. Input blir deretter oversendt til metode basert på oversendt id.
	 * @param context - Context
	 * @param tittel - Tittel på inputboks
	 * @param melding - Beskjed som forteller bruker hva som skal skrives inn
	 * @param id - Identifikator for hva som skal gjøres med input
	 */
	private static void visInputDialog(final Context context, String tittel, String melding, final int id) {		
		//opprett et Dialog objekt av typen AlertDialog
		AlertDialog.Builder inputBoks = new AlertDialog.Builder(context);
		//sett tittel og melding som skal vises
		inputBoks.setTitle(tittel);
		inputBoks.setMessage(melding);
		//opprett et tekstfelt for brukers input
		final EditText inputFelt = new EditText(context);
		//knytt tekstfelt til inputboksen
		inputBoks.setView(inputFelt);
		//opprett lytter til OK knappen
		inputBoks.setPositiveButton("Ok", new DialogInterface.OnClickListener() {
			public void onClick(DialogInterface dialog, int whichButton) {
				//hent ut verdien som bruker skrev inn
				String input = inputFelt.getText().toString();	
				lagreTemperaturGrense(context, id, input);
			} //onClick
		});
		//opprett lytter til CANCEL knappen
		inputBoks.setNegativeButton("Cancel", new DialogInterface.OnClickListener() {
			public void onClick(DialogInterface dialog, int whichButton) {
				//input ble avbrutt
			} //onClick
		});
		//vis inputboks til bruker
		inputBoks.show();
	} //visInputDialog

	/***
	 * Denne funksjonen registrer temperaturgrensen (enten øvre eller nedre) i SharedPreferences.
	 * @param context - Context
	 * @param id - int: Identifikator for om det er øvre eller nedre grense som skal lagres
	 * @param input - String: Verdien som skal lagres
	 */
	private static void lagreTemperaturGrense(Context context, final int id, String input) {
		try {
			//boolsk verdi for å sjekke at temperaturene ikke krysser hverandre
			//f.eks. at nedre temp. grense ikke er høyere enn øvre grense
			boolean kanRegistreres = true;
			float ovreTempGrense = 0, 
					nedreTempGrense = 0;
			//opprett et objekt av SharedPreference
			int modus = Activity.MODE_PRIVATE;
			SharedPreferences deltPreferanse = context.getSharedPreferences(Filbehandling.DELT_TEMPERATUR_PREFERANSE, modus);
			//hvilken temperaturgrense skal lagres?
			switch (id) {
			case LAGRE_OVRE_TEMPERATUR_GRENSE:
				//forsøk å konvertere input til integer
				ovreTempGrense = Float.parseFloat(input);
				//hent ut eksisterende verdier fra sharedpreferences
				int defaultNedreTempGrense = context.getResources().getInteger(R.integer.default_lower_value_temperature_limit);
				nedreTempGrense = deltPreferanse.getFloat(Filbehandling.NEDRE_TEMPERATUR_NOKKEL, defaultNedreTempGrense);
				//er temperatur for øvre grense høyere enn den for nedre?
				if (ovreTempGrense < nedreTempGrense) {
					//øvre grense er lavere enn nedre, sett at temperatur ikke kan registreres
					kanRegistreres = false;
				} //if (ovreTempGrense < nedreTempGrense) 
				break;
			case LAGRE_NEDRE_TEMPERATUR_GRENSE:
				//forsøk å konvertere input til integer
				nedreTempGrense = Float.parseFloat(input);
				//hent ut eksisterende verdier fra sharedpreferences
				int defaultOvreTempGrense = context.getResources().getInteger(R.integer.default_upper_value_temperature_limit);
				ovreTempGrense = deltPreferanse.getFloat(Filbehandling.OVRE_TEMPERATUR_NOKKEL, defaultOvreTempGrense);
				//er temperatur for øvre grense høyere enn den for nedre?
				if (ovreTempGrense < nedreTempGrense) {
					//øvre grense er lavere enn nedre, sett at temperatur ikke kan registreres
					kanRegistreres = false;
				} //if (ovreTempGrense < nedreTempGrense) 				
				break;				
			} //switch
			//kan temperatur registreres?
			if (kanRegistreres) {
				//hent ut verdi for default tidsintervall fra xml
				int defaultTidsintervall = context.getResources().getInteger(R.integer.default_time_interval_download);
				//hent ut tidsintervallet fra sharedpreferences
				int tidsIntervall = deltPreferanse.getInt(Filbehandling.TIDSINTERVALL_NEDLASTING, defaultTidsintervall);
				//hent ut tidpsunktet
				String tidspunkt = deltPreferanse.getString(Filbehandling.OPPDATERINGS_TIDSPUNKT_NOKKEL, returnNesteNedlastingsTidspunkt(tidsIntervall));
				//skriv endringen til fil
				Filbehandling.skrivTilSharedPreferences(context, ovreTempGrense, nedreTempGrense, tidspunkt, tidsIntervall);
				//tilbakemelding til bruker
				Toast.makeText(context, context.getString(R.string.new_temp_limit_set), Toast.LENGTH_LONG).show();
			} //if (kanRegistreres)
		} catch (NumberFormatException ex) {
			//hent ut ledetekst til feilmelding
			String feilmelding = context.getString(R.string.number_format_exception);
			//vis feilmelding til bruker
			Toast.makeText(context, feilmelding + ex.getMessage(), Toast.LENGTH_LONG).show();
		} catch (Exception ex) {
			//hent ut ledetekst til feilmelding
			String feilmelding = context.getString(R.string.uknown_error_exception);
			//vis feilmelding til bruker
			Toast.makeText(context, feilmelding + ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //lagreTemperaturGrense

	/******************************* TIDSINTERVALL *******************************/

	/***
	 * Denne funksjonen registrerer det nye tidsintervallet og tidspunktet for 
	 * nedlasting av værdata i SharedPreferences.
	 * @param context - Context
	 * @param tidsIntervall - int: Tidsintervallet for nedlasting
	 */
	private static void settNyttTidsintervall(Context context, int tidsIntervall) {
		try {
			//opprett et objekt av SharedPreference
			int modus = Activity.MODE_PRIVATE;
			SharedPreferences deltPreferanse = context.getSharedPreferences(Filbehandling.DELT_TEMPERATUR_PREFERANSE, modus);
			//hent ut eksisterende verdier fra sharedpreferences (øvre temp grense)
			int defaultOvreTempGrense = context.getResources().getInteger(R.integer.default_upper_value_temperature_limit);
			float ovreTempGrense = deltPreferanse.getFloat(Filbehandling.OVRE_TEMPERATUR_NOKKEL, defaultOvreTempGrense);
			//hent ut eksisterende verdier fra sharedpreferences (nedre temp grense)
			int defaultNedreTempGrense = context.getResources().getInteger(R.integer.default_lower_value_temperature_limit);
			float nedreTempGrense = deltPreferanse.getFloat(Filbehandling.NEDRE_TEMPERATUR_NOKKEL, defaultNedreTempGrense);
			//opprett tidspunkt for neste oppdatering
			String tidspunkt = returnNesteNedlastingsTidspunkt(tidsIntervall);
			//skriv endringen til fil
			Filbehandling.skrivTilSharedPreferences(context, ovreTempGrense, nedreTempGrense, tidspunkt, tidsIntervall);
			//gi tilbakemelding til bruker
			Toast.makeText(context, context.getString(R.string.new_time_interval_set) + " " + tidspunkt, Toast.LENGTH_LONG).show();
			//siden intervallet er endret; stopp service og restart for at timer skal kjøre på rett intervall
			serviceRestarter = true;
			stopService(context);
			startService(context);
		} catch (Exception ex) {
			//hent ut ledetekst til feilmelding
			String feilmelding = context.getString(R.string.uknown_error_exception);
			//vis feilmelding til bruker
			Toast.makeText(context, feilmelding + ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //settNyttTidsintervall

	/**
	 * Returnerer tidspunktet + oversendt tidsintervall når funksjonen kalles. <br /> 
	 * Bruksområde er bl.a. setting av nytt tidspunkt for nedlasting og default verdi <br />
	 * dersom intet nedlastingstidspunkt eksisterer.<br /><br /> 
	 * Eksempel: <br />
	 * Hvis klokkeslett når funksjonen kalles er 14:00 og tidsintervall = 1; så returneres 15:00.
	 * @param tidsIntervall - int: hvor ofte nedlasting skal skje
	 * @return String: (gjeldende tidspunkt + tidsintervall)
	 */
	public static String returnNesteNedlastingsTidspunkt(int tidsIntervall) {
		//hent ut tidssonen
		TimeZone tz = TimeZone.getDefault();
		//opprett et objekt av Kalender for uthenting av tidspunkt
		Calendar kalender = Calendar.getInstance();
		//sett tidssonen
		kalender.setTimeZone(tz);
		//hent ut verdien for time, minutt og sekund
		int time = kalender.get(Calendar.HOUR_OF_DAY) + tidsIntervall;
		//er satt tidsintervall større enn 24t (time fra 0-23)?
		if (time > 23) {
			//over 24t, trekk fra 24, siden timene går fra 0-23
			time = time - 24;
		} //if (time > 23)
		//opprett en string for tidspunktet
		String tidspunkt = time + ":" + kalender.get(Calendar.MINUTE) + ":" + kalender.get(Calendar.SECOND);
		//returner tidspunktet i form av en string
		return tidspunkt;
	} //returnDefaultTidspunkt
} //ActionBarFunksjoner