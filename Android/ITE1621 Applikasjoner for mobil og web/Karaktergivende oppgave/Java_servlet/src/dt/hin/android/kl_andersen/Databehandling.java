package dt.hin.android.kl_andersen;

import java.sql.SQLException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpSession;

/**
 * Klasse som tar for seg oppgaver som: <br />
 * - Innlogging av bruker <br />
 * - Registrering av enheter <br />
 * - Avregistrering av enheter <br />
 * - Oppdatering av registrationID <br />
 * - Opplisting av alle registrerte enheter <br />
 * - Oppretting av session for innlogget bruker på webside <br />
 * @author Knut Lucas Andersen
 */
public final class Databehandling {
	/** Etter 5 minutt med inaktivitet blir bruker logget ut **/
	private static final int MAX_AKTIV_INNLOGGINGS_TIDSINTERVALL = 300;
	/** Verdi som settes i Session-objektet **/
	private static final String INLOGGET_PARAMETER = "Innlogget";
	private static HttpSession _session;
	private static String _tilbakeMelding;
	private static Enhet _enhet;	
	private static ArrayList<Enhet> onlineEnheter = new ArrayList<Enhet>();

	private Databehandling() {
		throw new UnsupportedOperationException();
	} //konstruktør

	/**
	 * Forsøker å logge inn bruker. <br />
	 * Tilgangsmodifier er satt til protected for å sørge for at 
	 * kun servlets og klasser innenfor dette prosjektet får 
	 * tilgang til funksjonen.
	 * @param username - String: Brukernavn
	 * @param pwd - String: Brukers passord
	 * @return boolean: True - bruker ble innlogget, <br />
	 * false - bruker ble ikke innlogget
	 */
	protected static boolean loggInnBruker(String username, String pwd) {
		boolean innlogget = false;
		try {
			innlogget = Database.loggInnBruker(username, pwd);
		} catch (ClassNotFoundException ex) {
			ex.printStackTrace();
		} catch (SQLException ex) { 
			ex.printStackTrace();
		} //try/catch
		return innlogget;
	} //loggInnBruker

	/**
	 * Registrerer en ny enhet.
	 * @param registrationID - String: Enhetens registreringsID
	 * @param enhetsNavn
	 */
	public static void registrerEnhet(String registrationID, String enhetsNavn, String latitude, String longitude) {
		//bruker synchronized for å sørge for at kun enhet blir behandlet om gangen
		synchronized (onlineEnheter) {
			try {
				double lat = Double.parseDouble(latitude);
				double lon = Double.parseDouble(longitude);
				//sjekk at enhet med gitt registrationID ikke allerede er registrert
				int indeks = finnEnhetsIndeks(registrationID);
				if (indeks == -1) {
					//sjekk også at enhet ikke allerede er registrert i databasen
					_enhet = Database.erEnhetRegistrert(registrationID, lat, lon);
					if (_enhet != null) {
						onlineEnheter.add(_enhet);
					} else {
						//datoformat for dagens dato (for innlegging i database)
						SimpleDateFormat datoFormatering = new SimpleDateFormat("yyyy-MM-dd");
						//formater dagens dato til string
						String dagensDato = datoFormatering.format(new Date());
						//registrer enhet på server
						_enhet = new Enhet(registrationID, enhetsNavn, lat, lon, dagensDato);
						onlineEnheter.add(_enhet);			
						Database.insertIntoTblKlienter(registrationID, enhetsNavn, dagensDato);
					} //if (enhet != null)
				} //if (indeks == -1)
			} catch (NullPointerException ex) {
				ex.printStackTrace();
			} catch (NumberFormatException ex) {
				ex.printStackTrace();
			} catch (ClassNotFoundException ex) {
				ex.printStackTrace();
			} catch (SQLException ex) { 
				ex.printStackTrace();
			} //try/catch
		} //synchronized
	} //registrerEnhet

	/**
	 * Funksjon som kopler fra selekterte enheter og sender en melding 
	 * om at enheten er frakoplet server.
	 * @param valgteEnheter - String[]: Array som inneholder registrationID
	 * til enheter som skal frakobles
	 */
	public static void kobleFraEnheter(String[] valgteEnheter) {
		//melding som skal vises til bruker
		String melding = "Du har blitt frakoblet TracknHide's server!";
		SendMeldinger sendMelding = new SendMeldinger();
		for (int i = 0; i < valgteEnheter.length; i++) {
			//send melding til enheter at om valgte enheter har gått offline
			sendMelding.opprettEnhetGikkOffline(valgteEnheter[i]);
			//send melding som forteller enheten at den har blitt frakoblet
			sendMelding.sendKobletFraServerMelding(valgteEnheter[i], melding);
			avRegistrerEnhet(valgteEnheter[i]);			
		} //for
	} //kobleFraEnheter

	/**
	 * Avregistrerer en enhet.
	 * @param registrationID - String: Enhetens registreringsID
	 */
	public static void avRegistrerEnhet(String registrationID) {
		synchronized (onlineEnheter) {
			try {
				if (onlineEnheter.size() > 0) {
					int indeks = finnEnhetsIndeks(registrationID);
					if (indeks != -1) {
						onlineEnheter.remove(indeks);
						Database.slettEnhet(registrationID);
					} //if (indeks != -1)
				} //if (onlineEnheter.size() > 0)
			} catch (ClassNotFoundException ex) { 
				ex.printStackTrace();
			} catch (SQLException ex) {
				ex.printStackTrace();
			} catch (Exception ex) {
				ex.printStackTrace();
			} //try/catch
		} //synchronized
	} //avRegistrerEnhet

	/**
	 * Looper gjennom ArrayList&lt;Enhet&gt; for å finne enhets indeks/posisjon 
	 * i ArrayList.
	 * @param registrationID - String: Enhets registrationID
	 * @return int: Indeks/posisjon til enhet
	 */
	private static int finnEnhetsIndeks(String registrationID) {
		int indeks = -1,
				teller = 0;
		boolean funnet = false;
		String gjeldendeID = "";
		//loop gjennom arraylist til ID er funnet eller alle elementer er sjekket
		while (!funnet && teller < onlineEnheter.size()) {
			gjeldendeID = onlineEnheter.get(teller).getRegistrationID();
			if (gjeldendeID.equals(registrationID)) {
				funnet = true;
				indeks = teller;
			} //if (gjeldendeID.equals(registrationID))
			teller++;
		} //while
		return indeks;
	} //finnEnhetsIndeks

	/**
	 * Oppdater registrationID til en enhet.
	 * @param gammeReglID - String: Enhetens orginale registrationID
	 * @param nyRegID - String: Enhetens nye registrationID
	 */
	public static void oppdaterRegistrationID(String gammelRegID, String nyRegID) {		
		synchronized (onlineEnheter) {
			try {
				int indeks = finnEnhetsIndeks(gammelRegID);
				String enhetsNavn = onlineEnheter.get(indeks).getEnhetsnavn();
				double latitude = onlineEnheter.get(indeks).getLatitude();
				double longitude = onlineEnheter.get(indeks).getLongitude();
				String dagensDato = onlineEnheter.get(indeks).getRegistrertDato();
				onlineEnheter.remove(indeks);
				_enhet = new Enhet(nyRegID, enhetsNavn, latitude, longitude, dagensDato);
				onlineEnheter.add(_enhet);
				Database.updateRegistrationID(gammelRegID, nyRegID);
			} catch (ClassNotFoundException ex) {
				ex.printStackTrace();
			} catch (SQLException ex) { 
				ex.printStackTrace();
			} //try/catch
		} //synchronized
	} //oppdaterRegistrationID

	/**
	 * Oppdater koordinatene til de sist oversendte. <br />
	 * Dersom enhet har blitt avregistrert fra server (eller server har vært nede), 
	 * så sjekkes det i tillegg om enhet er registrert på server. <br />
	 * @param regID - String: Enhetens registrationID
	 * @param enhetsNavn - String: Enhetens navn
	 * @param latitude - String: Enhetens latitude
	 * @param longitude - String: Enhetens longitude
	 */
	public static void oppdaterKoordinater(String regID, String enhetsNavn, String latitude, String longitude) {
		synchronized (onlineEnheter) {
			try {
				double lat = Double.parseDouble(latitude);
				double lon = Double.parseDouble(longitude);
				int indeks = finnEnhetsIndeks(regID);
				//er denne enheten registrert på server?
				if (indeks == -1) {
					//ikke registrert på server, registrer enhet
					registrerEnhet(regID, enhetsNavn, latitude, longitude);
				} else {
					onlineEnheter.get(indeks).setLatitude(lat);
					onlineEnheter.get(indeks).setLongitude(lon);
					onlineEnheter.get(indeks).setEnhetsNavn(enhetsNavn);
				} //if (indeks == -1)
			} catch (NullPointerException ex) {
				ex.printStackTrace();
			} catch (NumberFormatException ex) {
				ex.printStackTrace();
			} //try/catch
		} //synchronized
	} //oppdaterKoordinater

	/**
	 * Setter enhet som offline, men fortsetter å la enhet være tilkoblet server. <br />
	 * Dette siden bruker kan velge å motta posisjonsdata etter applikasjon avsluttes.
	 * @param regID - String: Enhetens registrationID
	 */
	public static void setEnhetSomOffline(String regID) {
		synchronized (onlineEnheter) {
			try {
				int indeks = finnEnhetsIndeks(regID);
				//er denne enheten registrert på server?
				if (indeks != -1) {
					double lat = Double.parseDouble(Konstanter.IKKE_SEND_POSISJON);
					double lon = Double.parseDouble(Konstanter.IKKE_SEND_POSISJON);
					onlineEnheter.get(indeks).setLatitude(lat);
					onlineEnheter.get(indeks).setLongitude(lon);
				} //if (indeks != -1) 
			} catch(Exception ex) {
				ex.printStackTrace();
			} //try/catch
		} //synchronized
	} //setEnhetSomOffline

	/**
	 * Henter alle registrerte enheter.
	 * @return ArrayList&lt;Enhet&gt;: ArrayList med alle registrerte enheter
	 */
	public static ArrayList<Enhet> hentAlleEnheter() {
		synchronized (onlineEnheter) {
			return new ArrayList<Enhet>(onlineEnheter);
		} //synchronized
	} //hentAlleEnheter

	/**
	 * Henter alle registrerte enheters RegistrationID
	 *  @return ArrayList&lt;String&gt;: ArrayList med alle registrerte enheters registrationID
	 */
	public static ArrayList<String> hentAlleEnhetersRegID() {
		synchronized (onlineEnheter) {
			ArrayList<String> regIDListe = new ArrayList<String>(onlineEnheter.size());
			for (int i = 0; i < onlineEnheter.size(); i++) {
				regIDListe.add(onlineEnheter.get(i).getRegistrationID());
			} //for
			return regIDListe;
		} //synchronized
	} //hentAlleEnhetersRegID

	/**
	 * Returnerer antall enheter som er online.
	 * @return int: Antall enheter online
	 */
	public static int getAntallOnline() {
		return onlineEnheter.size();
	} //getAntallOnline

	/**
	 * Returnerer objektet av Session.
	 * @return HttpSession: Objekt av session
	 */
	public static HttpSession getSessionObjekt() {
		return _session;
	} //getSessionInnlogget

	/**
	 * Rturnerer brukernavnet som er knyttet til session - objektet. <br />
	 * Hvis session-objektet er null (ingen aktiv session)
	 * returneres null.
	 * @return String: Innlogget brukers brukernavn || null
	 */
	public static String getSessionBrukernavn() {
		String brukernavn = "";
		try {
			_session = getSessionObjekt();
			//er session opprettet?
			if (_session == null) {
				brukernavn = null;
			} else {
				brukernavn = (String) _session.getAttribute(INLOGGET_PARAMETER);
			} //if (_session == null)
		} catch (IllegalStateException ex) {
			//denne feilen oppstår dersom det gjøres kall på en session 
			//som allerede er invalidert (eks: timout pga inaktivitet)
			brukernavn = null;
		} //try/catch
		return brukernavn;
	} //getSessionBrukernavn

	/**
	 * Oppretter et nytt session objekt basert på oversendt HttpServletRequest.
	 * @param request - HttpServletRequest
	 */
	public static void setSessionObjekt(HttpServletRequest request) {
		_session = request.getSession(true);
		_session.setMaxInactiveInterval(MAX_AKTIV_INNLOGGINGS_TIDSINTERVALL);
	} //setSessionObjekt

	/**
	 * Setter attributtetverdien til aktiv session. <br />
	 * Verdien som skal settes er innlogget brukers brukernavn.
	 * @param brukernavn - String: Innlogget brukers brukernavn
	 */
	public static void setSessionBrukernavn(String brukernavn) {
		_session.setAttribute(Databehandling.INLOGGET_PARAMETER, brukernavn);
	} //setSessionBrukernavn

	/**
	 * Avslutter aktiv session. <br />
	 * Hvis session - objektet eksisterer, blir det invalidert og satt til null.
	 */
	public static void avsluttSession() {
		try {
			_session = getSessionObjekt();
			//er et session-objekt startet og opprettet?
			if (_session != null && !_session.isNew()) {
				//slett session-objektet
				_session.invalidate();
				_session = null;
			} //if (!_session.isNew())
		} catch (IllegalStateException ex) {
			//denne feilen oppstår dersom session
			//allerede er invalidert (_session.isNew())
			_session = null;
		} //try/catch
	} //avsluttSession

	/**
	 * Returnerer en streng som inneholder en tilbakemelding  til bruker. <br />
	 * Denne meldingen vises kun på server, og skal ikke brukes for å sende 
	 * meldinger til enheter som er online! <br />
	 * Bruksområde er f.eks. ved feil under innlogging, som forteller bruker 
	 * at bruker ikke ble pålogget.
	 * @return String: tilbakemelding til bruker 
	 */
	public static String getTilbakemelding() {
		return _tilbakeMelding;
	} //getTilbakemelding

	/**
	 * Setter tilbakemelding som skal vises til bruker. <br />
	 * Skal kun brukes på server, og ikke til å sette meldinger som 
	 * skal vises til online enheter!
	 * @param tilbakeMelding - String: tilbakemelding som skal vises
	 */
	public static void setTilbakemelding(String tilbakeMelding) {
		_tilbakeMelding = tilbakeMelding;
	} //setTilbakemelding
} //Databehandling