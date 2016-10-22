package dt.hin.android.kl_andersen;

//java-import
import java.text.ParseException;
import java.util.ArrayList;
//android-import
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.database.sqlite.SQLiteException;
import android.widget.Toast;
//google-import
import com.google.android.maps.GeoPoint;

/**
 * Klasse som inneholder funksjoner som brukes på flere plasser. <br />
 * For å slippe for mye redundans og gjentakende kode er denne koden lagt inn her.<br /><br />
 * <u>Funksjoner klassen inneholder er:</u> <br /><br />
 * protected {@linkplain #initialiserFunksjoner(Context, SharedPreferences)} <br />
 * protected {@linkplain #opprettHistorikkRute(Enhet)} <br />
 * protected {@linkplain #fjernHistorikkRuter()} <br />
 * protected {@linkplain #leggTilKoordinater(long, String, String, GeoPoint, boolean)} <br />
 * protected {@linkplain #finnEnhetsIndeks(String)} <br /> 
 * protected {@linkplain #fjernOfflineEnhet(String)} <br />
 * protected {@linkplain #slettAlleRuterFraDatabase()} <br />
 * protected {@linkplain #slettIdentifikator()} <br />
 * protected {@linkplain #visMelding(Context, String, boolean)} <br />
 * protected {@linkplain #getOnlineEnheter()} <br />
 * protected {@linkplain #getEksisterendeRuteID(String)} <br />
 * protected {@linkplain #getOnlineEnheterArray()} <br />
 * protected {@linkplain #getSlettingFerdig()} <br />
 * private {@linkplain #opprettOgRegistrerEnhet(long, String, String, GeoPoint, boolean)} <br />
 * private {@linkplain #kringkastMelding(Context, String)} <br />
 * private {@linkplain #setSlettingFerdig(boolean)} <br />
 * @author Knut Lucas Andersen
 */
public final class FellesFunksjoner {
	private static boolean _slettingFerdig; 
	private static Context _context;
	private static ArrayList<Enhet> _onlineEnheter;
	private static SharedPreferences _deltPreferanse;

	private FellesFunksjoner() {
		throw new UnsupportedOperationException();
	} //konstruktør
	
	/**
	 * Denne skal kun kalles ved oppstart for å initialisere variablene!
	 * Kall skal gjøres i onCreate og må ikke kalles andre plasser, siden verdier vil bli nullstilt.
	 * @param context - Context
	 * @param deltPreferanse - SharedPreferences
	 */
	protected static void initialiserFunksjoner(Context context, SharedPreferences deltPreferanse) {
		_context = context;
		_slettingFerdig = true;
		_onlineEnheter = new ArrayList<Enhet>();
		_deltPreferanse = deltPreferanse;
	} //initialiserFunksjoner

	/**
	 * Starter tegning på kartet av en lagret rute. <br />
	 * Ruten som tegnes er basert på oversendt enhet.
	 * @param gammelRuteEnhet - Enhet: Enhetens viss rute skal tegnes på kartet
	 * @see Enhet
	 */
	protected static void opprettHistorikkRute(Enhet gammelRuteEnhet) {
		_onlineEnheter.add(gammelRuteEnhet);
		//finn denne rutens plass i arraylist
		int indeksRute = _onlineEnheter.size() - 1;
		//finn indeksen til de siste koordinatene i arraylist
		int indeksKoordinat = _onlineEnheter.get(indeksRute).getKoordinater().size() - 1;
		GeoPoint koordinat =  _onlineEnheter.get(indeksRute).getKoordinater().get(indeksKoordinat);
		//tegn ruten på kartet og vis den til bruker
		MainMapActivity.gaaTilLokasjon(koordinat);
	} //opprettHistorikkRute

	/**
	 * Fjerner historikk ruter fra ArrayList og kartet.
	 */
	protected static void fjernHistorikkRuter() {
		String gjeldendeID = "";
		for (int i = _onlineEnheter.size() - 1; i > -1; i--) {
			//hent ut registrationID og sjekk om den er null 
			//(historikk ruter har ikke registrationID)
			gjeldendeID = _onlineEnheter.get(i).getRegistrationID();
			if (gjeldendeID == null) {
				_onlineEnheter.remove(i);
			} //if (gjeldendeID.equals(null))
		} //for
		MainMapActivity.invaliderMapView();
	} //fjernHistorikkRuter

	/**
	 * Legger til mottatte koordinater i ArrayList&lt;Enhet&gt; som inneholder alle enheter som er online. <br />
	 * I tillegg oppdateres enhetens navn, i tilfelle den har blitt endret siden siste oppdatering. <br />
	 * Dersom enheten hvis koordinater mottas ikke ligger i ArrayList&lt;Enhet&gt;, blir den lagt til.<br />
	 * @param identifikator - long: Enhetens identifikator (primærnøkkel i databasen)
	 * @param registrationID - String: Enhetens registrationID
	 * @param enhetsNavn - String: Enhetens navn
	 * @param koordinater - GeoPoint: Enhetens koordinater
	 * @param egenEnhet - boolean: Er dette brukers enhet?
	 */
	protected static void leggTilKoordinater(long identifikator, String registrationID, String enhetsNavn, GeoPoint koordinater, boolean egenEnhet) {
		//finn denne enhetens plass i listen
		int indeks = finnEnhetsIndeks(registrationID);
		//er denne enhet lagt til i listen?
		if (indeks == -1) {
			Enhet enhet = opprettOgRegistrerEnhet(identifikator, registrationID, enhetsNavn, koordinater, egenEnhet);
			if (enhet != null) {
				//legg til enheten i listen
				_onlineEnheter.add(enhet);
			} //if (enhet != null)
		} else {
			//legg til enhetens koordinater i listen og oppdater enhetsnavn
			_onlineEnheter.get(indeks).addGeoPoint(koordinater);
			_onlineEnheter.get(indeks).setEnhetsNavn(enhetsNavn);
		} //if (indeks == -1)
		//oppdater kart så ny enhet vises
		MainMapActivity.invaliderMapView();
	} //leggTilKoordinater

	/**
	 * Looper gjennom ArrayList&lt;Enhet&gt; for å finne enhets  
	 * indeks/posisjon i ArrayList.
	 * @param registrationID - String: Enhets registrationID
	 * @return int: Indeks/posisjon til enhet
	 */
	protected static int finnEnhetsIndeks(String registrationID) {
		int indeks = -1,
				teller = 0;
		boolean funnet = false;
		String gjeldendeID = "";
		//loop gjennom arraylist til ID er funnet eller alle elementer er sjekket
		while (!funnet && teller < _onlineEnheter.size()) {
			gjeldendeID = _onlineEnheter.get(teller).getRegistrationID();
			if (gjeldendeID != null && gjeldendeID.equals(registrationID)) {
				funnet = true;
				indeks = teller;
			} //if (gjeldendeID.equals(registrationID))
			teller++;
		} //while
		return indeks;
	} //finnEnhetsIndeks

	/**
	 * Denne funksjonen oppretter en enhet basert på oversendte parametere. <br />
	 * Det sjekkes også her om denne enheten har vært pålogget i løpet av den siste tiden, <br />
	 * og hvis den har det, så hentes data fra tidligere rute ut og legges til før de mottatte
	 * koordinatene.
	 * @param identifikator - long: Enhetens identifikator (pimærnøkkel i databasen)
	 * @param registrationID - String: Enhetens registrationID
	 * @param enhetsNavn - String: Enhetens navn
	 * @param koordinater - GeoPoint: Denne enhetens koordinater
	 * @param egenEnhet - boolean: Er dette brukers enhet?
	 * @return Enhet: Objekt av klassen Enhet
	 * @see Enhet
	 */
	private static Enhet opprettOgRegistrerEnhet(long identifikator, String registrationID, String enhetsNavn, GeoPoint koordinater, boolean egenEnhet) {
		try {
			int ruteID = DatabaseFunksjoner.opprettRuteID(_context, identifikator);
			ArrayList<GeoPoint> koordinatListe = DatabaseFunksjoner.hentRuteKoordinater(_context, identifikator, ruteID);
			koordinatListe.add(koordinater);
			return new Enhet(identifikator, registrationID, enhetsNavn, egenEnhet, ruteID, koordinatListe);
		} catch (SQLiteException ex) {
			ex.printStackTrace();
			String feilmelding = _context.getString(R.string.sqlite_exception); 
			Toast.makeText(_context, feilmelding + ex.getMessage(), Toast.LENGTH_LONG).show();
		} catch (ParseException ex) {
			ex.printStackTrace();
			String feilmelding = _context.getString(R.string.parse_date_exception); 
			Toast.makeText(_context, feilmelding + ex.getMessage(), Toast.LENGTH_LONG).show();
		} catch (Exception ex) {
			String feilmelding = _context.getString(R.string.unknown_error_exception);
			Toast.makeText(_context, feilmelding + ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
		return null;
	} //opprettOgRegistrerEnhet

	/**
	 * Fjerner enhet med oppgitt registrationID fra ArrayList&lt;Enhet&gt;. <br />
	 * Hvis bruker ikke vil beholde andres ruter slettes ruten fra databasen.
	 * @param registrationID - String: Enhetens registrationID
	 */
	protected static void fjernOfflineEnhet(String registrationID) {
		//finn denne enhetens plass i listen
		int indeks = finnEnhetsIndeks(registrationID);
		//finnes enheten i listen?
		if (indeks > -1) {
			String enhetsNavn = _onlineEnheter.get(indeks).getEnhetsnavn();
			long identifikator = _onlineEnheter.get(indeks).getIdentifikator();
			_onlineEnheter.remove(indeks);
			boolean lagreAndresRuter = _deltPreferanse.getBoolean(Filbehandling.LAGRE_ANDRES_RUTE_NOKKEL, false);
			if (!lagreAndresRuter) {
				DatabaseFunksjoner.slettRuteFraDatabase(identifikator);
			} else {
				//oppdater enhetsnavn i databasen, i tilfelle det er endret
				DatabaseFunksjoner.updateDatabaseEnhetsNavn(_context, identifikator, enhetsNavn);
			} //if (!lagreAndresRuter)
		} //if (indeks > -1)
		//fjern offline enhet fra kartet
		MainMapActivity.invaliderMapView();
	} //fjernOfflineEnhet

	/**
	 * Funksjon som sjekker om bruker ønsker å ta vare på registrerte ruter. <br />
	 * Tanken er at denne funksjonen kun skal kalles når bruker avslutter applikasjonen. <br />
	 * Det sjekkes her om bruker vil ta vare på egne og andres ruter, og det bruker ikke vil ta 
	 * vare på blir slettet fra databasen.<br />
	 */
	protected static void slettAlleRuterFraDatabase() {		
		setSlettingFerdig(false);
		//variabler for tabeller og lagring
		String tblKoordinater = LoggingContentProvider.DB_TBLKOORDINATER;
		String fkEnhet = LoggingContentProvider.TBLKOORDINATER_FOREIGN_KEY;
		long brukersIdentifikator = _deltPreferanse.getLong(Filbehandling.ENHETS_IDENTIFIKATOR, 0);
		boolean lagreEgenRute = _deltPreferanse.getBoolean(Filbehandling.LAGRE_EGEN_RUTE_NOKKEL, false);
		boolean lagreAndresRuter = _deltPreferanse.getBoolean(Filbehandling.LAGRE_ANDRES_RUTE_NOKKEL, false);
		//ønsker bruker å ta vare på egen og andres ruter?
		if (!lagreEgenRute && !lagreAndresRuter) {
			LoggingContentProvider.dropDatabase(1);
			slettIdentifikator();
		} else if (!lagreEgenRute) {
			if (brukersIdentifikator > -1) {
				//slett ruten(e) fra tblkoordinater
				String where = fkEnhet + " = " + brukersIdentifikator;				
				LoggingContentProvider.delete(tblKoordinater, where, null);
			} //if (brukersIdentifikator > -1)
		} else if (!lagreAndresRuter) {
			if (brukersIdentifikator > -1) {
				//slett andre brukeres ruter
				DatabaseFunksjoner.slettAndresRuterFraDatabasen(brukersIdentifikator);
			} //if (brukersIdentifikator > -1)
		} //if (!lagreEgenRute && !lagreAndresRuter)
		setSlettingFerdig(true);
	} //slettAlleRuterFraDatabase

	/**
	 * Sletter brukers identifikator fra SharedPreferences
	 */
	protected static void slettIdentifikator() {
		SharedPreferences.Editor spEditor = _deltPreferanse.edit();
		spEditor.remove(Filbehandling.ENHETS_IDENTIFIKATOR);
		spEditor.commit();
	} //slettIdentifikator

	/**
	 * Videresender mottatte meldinger via Intent og Broadcast for 
	 * å fremvises på grensesnittet i Toast. <br />
	 * I tillegg blir meldingene logget.
	 * @param context - Context
	 * @param melding - String: Meldingen som ble sendt
	 * @param skalVises - boolean: True - meldingen skal vises, (via Toast)<br />
	 * false - meldingen skal ikke vises (via Toast)
	 */
	protected static void visMelding(Context context, String melding, boolean skalVises) {
		//vil bruker se til/frakoblingsmeldinger?
		boolean visMeldinger = _deltPreferanse.getBoolean(Filbehandling.VIS_NOTIFICATIONS_NOKKEL, true);
		Filbehandling.oppdaterSystemLogg(context, melding);
		//er dette en melding som skal vises? (feilmelding, gcm-melding, el.l.)
		if (skalVises) {
			kringkastMelding(context, melding);
		} else if (visMeldinger) {
			//hvis bruker vil se all slags meldinger, vis dem
			kringkastMelding(context, melding);
		} //if (skalVises)
	} //visMelding

	private static void kringkastMelding(Context context, String melding) {
		Intent intent = new Intent(context.getString(R.string.display_message));
		intent.putExtra(context.getString(R.string.readable_message), melding);
		context.sendBroadcast(intent);
	} //kringkastMelding

	/**
	 * Returnerer en ArrayList med alle enheter som er online
	 * @return ArrayList&lt;Enhet&gt;: Enheter som er online
	 */
	protected static ArrayList<Enhet> getOnlineEnheter() {
		return _onlineEnheter;
	} //getOnlineEnheter

	/**
	 * Henter enhetens gjeldende ruteID.
	 * @param registrationID - String: Enhetens registrationID
	 * @return int: Enhetens ruteID
	 */
	protected static int getEksisterendeRuteID(String registrationID) {
		int indeks = finnEnhetsIndeks(registrationID);
		return _onlineEnheter.get(indeks).getRuteID();
	} //getEksisterendeRuteID

	/**
	 * Returner en array fylt med navn på enheter som er online.
	 * @return String[] || null
	 */
	protected static String[] getOnlineEnheterArray() {
		//finnes det enheter som er online
		if (_onlineEnheter.size() > 0) {
			String[] onlineArray = new String[_onlineEnheter.size()];
			for (int i = 0; i < _onlineEnheter.size(); i++) {
				onlineArray[i] = _onlineEnheter.get(i).getEnhetsnavn();
			} //for
			return onlineArray;
		} else {
			return null;
		} //if (_onlineEnheter.size() > 0)
	} //getOnlineEnheterArray

	/**
	 * Returnerer verdi for om sletting fra database er fullført.
	 * @return boolean: True - sletting fullført, <br />
	 * False - sletting ikke fullført
	 */
	protected static boolean getSlettingFerdig() {
		return _slettingFerdig;
	} //getSlettingFerdig

	/**
	 * Setter verdi for om sletting av database er fullført.
	 * @param slettingFerdig - boolean: True - sletting fullført, <br />
	 * False - sletting ikke fullført
	 */
	private static void setSlettingFerdig(boolean slettingFerdig) {
		_slettingFerdig = slettingFerdig;
	} //setSlettingFerdig
} //FellesFunksjoner