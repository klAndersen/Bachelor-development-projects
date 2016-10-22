package dt.hin.android.kl_andersen;

//java-import
import java.io.BufferedReader;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.Locale;
//android-import
import android.content.Context;
import android.content.SharedPreferences;
import android.content.res.Resources;
import android.widget.Toast;

/**
 * Klasse som leser og skriver til SharedPreferences. <br />
 * Funksjonen {@link #getSharedPreferances(Context) } returnerer objektet til 
 * SharedPreferences <br /> slik at man slipper å opprette objektet på nytt. <br /><br />
 * Klassen oppretter og skriver også til en loggfil for følgende blir tatt med: <br />
 * - Meldinger fra GCM <br />
 * - Resultat av tilkobling/frakobling fra server <br />
 * - Diverse feilmeldinger (mottak av meldinger og tilkoblingsfeil) <br />
 * - Adresse(r) (via "Vis Adresse" i pop-up dialog) <br />
 * @author Knut Lucas Andersen
 */
public final class Filbehandling {
	protected static final String LATITUDE_NOKKEL = "latitude";
	protected static final String LONGITUDE_NOKKEL = "longitude";
	protected static final String RUTEFARGE_NOKKEL = "ruteFarge";
	protected static final String ENHETSNAVN_NOKKEL = "enhetsNavn";
	protected static final String ZOOM_VERDI_NOKKEL = "settZoomVerdi";
	protected static final String ENHETS_IDENTIFIKATOR = "pk_TblEnhet";
	protected static final String SEND_POSISJON_NOKKEL = "sendPosisjon";
	protected static final String LOGG_MELDINGER_NOKKEL = "loggMeldinger";
	protected static final String REGISTRATIONID_NOKKEL = "registrationID";
	protected static final String LAGRE_EGEN_RUTE_NOKKEL = "lagreMinPosisjon";
	protected static final String VIS_NOTIFICATIONS_NOKKEL = "visNotifications";
	protected static final String LAGRE_ANDRES_RUTE_NOKKEL = "lagreAndresPosisjon";
	protected static final String VIS_UTVALGTE_ENHETER_NOKKEL = "visUtvalgeEnheter";
	protected static final String DELT_INNSTILLING_PREFERANSE = "innstillingsPreferanse";
	protected static final String OPPDATERINGSINTERVALL_METER_NOKKEL = "oppdateringMeter";
	protected static final String OPPDATERINGSINTERVALL_MINUTT_NOKKEL = "oppdateringMinutt";
	protected static final String MOTTA_POSISJONSDATA_ETTER_AVSLUTNING_NOKKEL = "mottaPosisjonsdata";
	
	private Filbehandling() {
		throw new UnsupportedOperationException();
	} //konstruktør

	/**
	 * Returnerer et objekt av SharedPreferences som inneholder brukers preferanser/innstillinger 
	 * for kart, rute(r) og fremvisning.
	 * @param context - Context
	 * @return SharedPreferences: Objekt som inneholder innstillinger for InnstillingsActivity
	 */
	protected static SharedPreferences getSharedPreferances(Context context) {
		//sett at det er kun denne applikasjonen som skal bruke oppgitt sharedpreference
		int modus = Context.MODE_PRIVATE;
		//opprett og hent preferansen
		SharedPreferences deltPreferanse = context.getSharedPreferences(DELT_INNSTILLING_PREFERANSE, modus);
		return deltPreferanse;
	} //getSharedPreferances
	
	/**
	 * Lagrer verdien for enhetens identifikator (primærnøkkel i databasen).
	 * @param context
	 * @param identifikator - long: Enhetens identifikator
	 */
	protected static void lagreEnhetsIdentifikator(Context context, long identifikator) {
		//opprett en editor for å redigere sharedpreference
		SharedPreferences.Editor spEditor = getSharedPreferances(context).edit();
		//lagre verdier i sharedpreference
		spEditor.putLong(ENHETS_IDENTIFIKATOR, identifikator);
		spEditor.commit();
	} //lagreEnhetsIdentifikator
	
	/**
	 * Lagrer bruker sin enhets registrationID.
	 * @param context - Context
	 * @param registrationID - String: Bruker sin enhets registrationID
	 */
	protected static void lagreEnhetsRegistrationID(Context context, String registrationID) {
		//opprett en editor for å redigere sharedpreference
		SharedPreferences.Editor spEditor = getSharedPreferances(context).edit();
		//lagre verdier i sharedpreference
		spEditor.putString(REGISTRATIONID_NOKKEL, registrationID);
		spEditor.commit();
	} //lagreEnhetsRegistrationID

	/**
	 * Setter om koordinater/posisjon skal sendes eller ikke.
	 * @param context - Context
	 * @param sendPosisjon - boolean: True - send koordinater, false - ikke send koordinater
	 */
	protected static void lagreUtsendingAvKoordinater(Context context, boolean sendPosisjon) {
		//opprett en editor for å redigere sharedpreference
		SharedPreferences.Editor spEditor = getSharedPreferances(context).edit();
		//lagre verdier i sharedpreference
		spEditor.putBoolean(SEND_POSISJON_NOKKEL, sendPosisjon);
		spEditor.commit();
	} //lagreUtsendingAvKoordinater

	/**
	 * Denne enhetens siste latitude.
	 * @param context - Context
	 * @param sisteLatitude - double: Sist registrerte latitude
	 */
	protected static void lagreSisteLatitude(Context context, float sisteLatitude) {
		//opprett en editor for å redigere sharedpreference
		SharedPreferences.Editor spEditor = getSharedPreferances(context).edit();
		//lagre verdier i sharedpreference
		spEditor.putFloat(LATITUDE_NOKKEL, sisteLatitude);
		spEditor.commit();
	} //lagreSisteLatitude

	/**
	 * Denne enhetens siste longitude.
	 * @param context - Context
	 * @param sisteLongitude - double: Sist registrerte longitude
	 */
	protected static void lagreSisteLongitude(Context context, float sisteLongitude) {
		//opprett en editor for å redigere sharedpreference
		SharedPreferences.Editor spEditor = getSharedPreferances(context).edit();
		//lagre verdier i sharedpreference
		spEditor.putFloat(LONGITUDE_NOKKEL, sisteLongitude);
		spEditor.commit();
	} //lagreSisteLongitude
	
	/**
	 * Denne funksjonen lagrer hvor mange minutt det skal gå før neste oppdatering/sending 
	 * av bruker gjeldende posisjon. <br />
	 * Verdien lagres i SharedPreferences. <br />
	 * @param context - Context
	 * @param minutt - float: Antall minutt mellom hver oppdatering/sending av 
	 * brukers posisjonsdata
	 */
	protected static void lagreMinuttOppdatering(Context context, float minutt) {
		//opprett en editor for å redigere sharedpreference
		SharedPreferences.Editor spEditor = getSharedPreferances(context).edit();
		//lagre verdier i sharedpreference
		spEditor.putFloat(OPPDATERINGSINTERVALL_MINUTT_NOKKEL, minutt);
		spEditor.commit();
	} //lagreMinuttOppdatering

	/**
	 * Denne funksjonen lagrer hvor mange meter bruker skal forflytte seg før 
	 * neste oppdatering/sending av brukers gjeldende posisjon. <br />
	 * Verdien lagres i SharedPreferences. <br />
	 * @param context - Context
	 * @param meter - float: Antall meter mellom hver oppdatering/sending av 
	 * brukers posisjonsdata
	 */
	protected static void lagreMeterOppdatering(Context context, float meter) {
		//opprett en editor for å redigere sharedpreference
		SharedPreferences.Editor spEditor = getSharedPreferances(context).edit();
		//lagre verdier i sharedpreference
		spEditor.putFloat(OPPDATERINGSINTERVALL_METER_NOKKEL, meter);
		spEditor.commit();
	} //lagreMeterOppdatering

	/**
	 * Denne funksjonen lagrer navnet på brukers enhet (navn som vises på kartet). <br />
	 * Verdien lagres i SharedPreferences. <br />
	 * @param context - Context
	 * @param enhetsNavn - String: Navn på enhet
	 */
	protected static void lagreEnhetsnavn(Context context, String enhetsNavn) {
		//opprett en editor for å redigere sharedpreference
		SharedPreferences.Editor spEditor = getSharedPreferances(context).edit();
		//lagre verdier i sharedpreference
		spEditor.putString(ENHETSNAVN_NOKKEL, enhetsNavn);
		spEditor.commit();
	} //lagreEnhetsnavn

	/**
	 * Denne funksjonen lagrer verdien for hvilken farge bruker ønsker at sin rute 
	 * skal farges med. <br />
	 * Verdien lagres i SharedPreferences. <br />
	 * @param context - Context
	 * @param fargeIndeks - Integer: Indeks for fargens posisjon i array <br />
	 * (array hentes ut fra en string-array i strings.xml og legges i en Spinner)
	 */
	protected static void lagreEnhetsfarge(Context context, int fargeIndeks) {
		//opprett en editor for å redigere sharedpreference
		SharedPreferences.Editor spEditor = getSharedPreferances(context).edit();
		//lagre verdier i sharedpreference
		spEditor.putInt(RUTEFARGE_NOKKEL, fargeIndeks);
		spEditor.commit();
	} //lagreEnhetsfarge
	
	/**
	 * Setter default zoom verdi på kart.
	 * @param context - Context
	 * @param zoomVerdi - int: Verdien bruker vil ha for default zoom
	 */
	protected static void lagreDefaultZoomVerdi(Context context, int zoomVerdi) {
		//opprett en editor for å redigere sharedpreference
		SharedPreferences.Editor spEditor = getSharedPreferances(context).edit();
		//lagre verdier i sharedpreference
		spEditor.putInt(ZOOM_VERDI_NOKKEL, zoomVerdi);
		spEditor.commit();
	} //lagreDefaultZoomVerdi
	
	/**
	 * Lagrer brukers valg tilknyttet mottak av posisjonsdata. <br />
	 * Dette gir bruker mulighet til å velge om posisjonsdata fortsatt skal mottas etter 
	 * applikasjonen avsluttes.
	 * @param context - Context
	 * @param mottaPosisjonsdata - boolean: True - bruker vil motta posisjonsdata etter avslutning, <br /> 
	 * false - bruker vil ikke motta posisjonsdata etter avslutning; avregistrer enhet fra GCM
	 */
	protected static void lagreMottakAvPosisjonsdata(Context context, boolean mottaPosisjonsdata) {
		//opprett en editor for å redigere sharedpreference
		SharedPreferences.Editor spEditor = getSharedPreferances(context).edit();
		//lagre verdier i sharedpreference
		spEditor.putBoolean(MOTTA_POSISJONSDATA_ETTER_AVSLUTNING_NOKKEL, mottaPosisjonsdata);
		spEditor.commit();
	} //lagreMottakAvPosisjonsdata

	/**
	 * Denne funksjonen lagrer verdien for om bruker ønsker å logge egen rute. <br />
	 * Hensikten er at bruker kan kunne se egen rute i offline-modus. <br />
	 * Verdien lagres i SharedPreferences. <br />
	 * @param context - Context
	 * @param lagreMinPosisjon - Boolean: True = logg brukers rute, <br />
	 * false = ikke logg brukers rute
	 */
	protected static void lagreLoggForEgenRute(Context context, boolean lagreMinPosisjon) {
		//opprett en editor for å redigere sharedpreference
		SharedPreferences.Editor spEditor = getSharedPreferances(context).edit();
		//lagre verdier i sharedpreference
		spEditor.putBoolean(LAGRE_EGEN_RUTE_NOKKEL, lagreMinPosisjon);
		spEditor.commit();
	} //lagreLoggForEgenRute

	/**
	 * Denne funksjonen lagrer verdien for om bruker ønsker å logge andre brukeres rute. <br />
	 * Hensikten er at bruker kan kunne se rute i offline-modus. <br />
	 * Verdien lagres i SharedPreferences. <br />
	 * @param context - Context
	 * @param lagreAndresPosisjon - Boolean: True = logg andre brukeres rute, <br /> 
	 * false = ikke logg andre brukeres rute
	 */
	protected static void lagreLoggForAndresRute(Context context, boolean lagreAndresPosisjon) {
		//opprett en editor for å redigere sharedpreference
		SharedPreferences.Editor spEditor = getSharedPreferances(context).edit();
		//lagre verdier i sharedpreference
		spEditor.putBoolean(LAGRE_ANDRES_RUTE_NOKKEL, lagreAndresPosisjon);
		spEditor.commit();
	} //lagreLoggForAndresRute
	
	/**
	 * Lagrer valg for om bruker vil se all slags meldinger. <br />
	 * Hensikten er at bruker skal slippe å bli "spammet" med påloggings - og  <br />
	 * frakoblingsmeldinger når bruker starter/avslutter applikasjonen. <br />
	 * Men, feilmeldinger og meldinger sendt fra server/gcm vil fortsatt vises.
	 * @param context - Context
	 * @param visNotifications - Booleean: True - vis alle meldinger, <br />
	 * False - vis kun feilmeldinger og meldinger fra server/gcm
	 */
	protected static void lagreValgVisningAvNotifications(Context context, boolean visNotifications) {
		//opprett en editor for å redigere sharedpreference
		SharedPreferences.Editor spEditor = getSharedPreferances(context).edit();
		//lagre verdier i sharedpreference
		spEditor.putBoolean(VIS_NOTIFICATIONS_NOKKEL, visNotifications);
		spEditor.commit();
	} //lagreValgVisningAvNotifications

	/**
	 * Lagrer valg for om bruker vil opprette og lagre en systemlogg. <br />
	 * Systemloggen inneholder hendelser som skjer mens applikasjonen kjører, bl.a. 
	 * meldinger fra GCM, noen feilmeldinger og andre hendelser.
	 * @param context - Context
	 * @param loggMeldinger - Boolean: True - bruker vil logge hendelser, feilmeldinger og 
	 * andre meldinger fra bl.a. GCM <br />
	 * false - bruker vil ikke logge hendelser o.l. fra bl.a. GCM 
	 */
	protected static void lagreValgLoggingAvMeldinger(Context context, boolean loggMeldinger) {
		//opprett en editor for å redigere sharedpreference
		SharedPreferences.Editor spEditor = getSharedPreferances(context).edit();
		//lagre verdier i sharedpreference
		spEditor.putBoolean(LOGG_MELDINGER_NOKKEL, loggMeldinger);
		spEditor.commit();
	} //lagreValgLoggingAvMeldinger
	
	/**
	 * Denne funksjonen leser inn innholdet fra eksisterende loggfil, for 
	 * deretter å legge til mottatt melding og skrive den nye loggen til fil.
	 * @param context - Context
	 * @param melding - String: Meldingen som skal logges
	 */
	protected static void oppdaterSystemLogg(Context context, String melding) {
		SharedPreferences deltPreferanse = getSharedPreferances(context);
		boolean loggMeldinger = deltPreferanse.getBoolean(LOGG_MELDINGER_NOKKEL, false);
		//skal meldinger logges?
		if (loggMeldinger) {
			ArrayList<String> systemLogg = lesLoggFraFil(context);
			String logget =  "Registrert: " + hentTidspunktNaa() + "\n" + melding; 
			systemLogg.add(logget);
			skrivLoggTilFil(context, systemLogg);
		} //if (loggMeldinger)
	} //oppdaterSystemLogg

	private static String hentTidspunktNaa() {
		Locale locale = Locale.getDefault();
		SimpleDateFormat datoFormatering = new SimpleDateFormat("dd/MM-yyyy HH:mm:ss", locale);
		//returner den formaterte dato-stringen		
		return datoFormatering.format(new Date());
	} //hentTidspunktNaa

	/**
	 * Oppretter en ArrayList&lt;String&gt som inneholder systemloggen. <br />
	 * Dersom fil ikke finnes, blir denne opprettet. <br />
	 * Dersom filen er tom, returneres kun en blank linje.
	 * @param context - Context
	 * @return ArrayList&lt;String&gt: filens innhold
	 */
	protected static synchronized ArrayList<String> lesLoggFraFil(Context context) {
		String enLinje;
		//arrayliste som tar vare på logg
		ArrayList<String> loggListe = new ArrayList<String>();
		try {
			//objekt av ressursklassen
			Resources res = context.getResources();
			//hent filnavn via ressurser
			String filnavn = res.getString(R.string.filnavn);
			//opprett et filobjekt
			File fil = context.getFileStreamPath(filnavn);
			//finnes filen?
			if (!fil.exists()) {
				//fil finnes ikke, opprett fil
				opprettFil(context, filnavn);
			} //if (!fil.exists())
			//har filen innhold?
			if (fil.length() > 0) {
				//forsøk å åpne og lese fra fil
				FileInputStream filInputStream = context.openFileInput(filnavn);
				InputStreamReader inputStreamLeser = new InputStreamReader(filInputStream);
				BufferedReader bufferLeser = new BufferedReader(inputStreamLeser);
				//les første linje fra filen
				enLinje = bufferLeser.readLine();		
				//så lenge fil har innhold...
				while (enLinje != null) {
					//legg til innlest linje i arraylist
					loggListe.add(enLinje);
					//les neste linje fra fil
					enLinje = bufferLeser.readLine();
				} //while
				//lukk leserne
				inputStreamLeser.close();
				bufferLeser.close();
				filInputStream.close();
			} //if (fil.length() == 0) 
		} catch (Exception ex) {
			//en feil oppsto, vis feilmelding til bruker
			String feilmelding = context.getString(R.string.read_from_file_exception);
			Toast.makeText(context, feilmelding + ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
		return loggListe;
	} //lesLoggFraFil

	/**
	 * Skriver TracknHide's systemlogg til fil. <br />
	 * Dersom fil ikke finnes, blir denne opprettet.
	 * @param context - Context
	 * @param loggListe - ArrayList&lt;String&gt: Systemloggen
	 */
	protected static synchronized void skrivLoggTilFil(Context context, ArrayList<String> loggListe) {
		try {
			//objekt av ressursklassen
			Resources res = context.getResources();
			//hent filnavn fra ressurser
			String filnavn = res.getString(R.string.filnavn);
			//opprett et filobjekt
			File fil = context.getFileStreamPath(filnavn);
			//finnes filen?
			if (!fil.exists()) {
				//fil finnes ikke, opprett fil
				opprettFil(context, filnavn);
			} //if (!fil.exists())
			//forsøk å åpne filen og opprett skriver
			FileOutputStream fileOutputStream = context.openFileOutput(filnavn, Context.MODE_PRIVATE);
			OutputStreamWriter outputStreamSkriver = new OutputStreamWriter(fileOutputStream);
			//loop gjennom innhold i arraylist og skriv det til fil
			for (int i = 0; i < loggListe.size(); i++) {
				//skriv innhold til fil separert med newline
				outputStreamSkriver.write(loggListe.get(i) + '\n');
			} //for
			//lukk skrivere
			outputStreamSkriver.close();
			fileOutputStream.close();
		} catch (Exception ex) {
			//en feil oppsto, vis feilmelding til bruker
			String feilmelding = context.getString(R.string.write_to_file_exception);
			Toast.makeText(context, feilmelding + ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //skrivLoggTilFil

	/**
	 * Sletter innhold i systemloggen til TracknHide ved å skrive blankt ("") til fil.
	 * @param context - Context
	 */
	protected static void slettLoggensInnhold(Context context) {
		try {
			//objekt av ressursklassen
			Resources res = context.getResources();
			//hent filnavn fra ressurser
			String filnavn = res.getString(R.string.filnavn);
			//opprett et filobjekt
			File fil = context.getFileStreamPath(filnavn);
			//finnes filen?
			if (!fil.exists()) {
				//fil finnes ikke, opprett fil
				opprettFil(context, filnavn);
			} //if (!fil.exists())
			//forsøk å åpne filen og opprett skriver
			FileOutputStream fileOutputStream = context.openFileOutput(filnavn, Context.MODE_PRIVATE);
			OutputStreamWriter outputStreamSkriver = new OutputStreamWriter(fileOutputStream);
			//skriv blankt til fil (sletter innhold)
			outputStreamSkriver.write("");
			//lukk skrivere
			outputStreamSkriver.close();
			fileOutputStream.close();
		} catch (Exception ex) {
			//en feil oppsto, vis feilmelding til bruker
			String feilmelding = context.getString(R.string.write_to_file_exception);
			Toast.makeText(context, feilmelding + ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //slettLoggensInnhold
	
	/**
	 * Oppretter en fil på gitt lokasjon basert på oversendt 
	 * Context (context.getFilesDir()) med oppgitt filnavn
	 * @param context
	 * @param filnavn
	 */
	private static void opprettFil(Context context, String filnavn) {
		@SuppressWarnings("unused")
		File file = new File(context.getFilesDir(), filnavn);
	} //opprettFil
} //Filbehandling