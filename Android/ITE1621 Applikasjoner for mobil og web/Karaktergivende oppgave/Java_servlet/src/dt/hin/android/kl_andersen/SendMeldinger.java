package dt.hin.android.kl_andersen;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import com.google.android.gcm.server.Constants;
import com.google.android.gcm.server.Message;
import com.google.android.gcm.server.MulticastResult;
import com.google.android.gcm.server.Result;
import com.google.android.gcm.server.Sender;

import static dt.hin.android.kl_andersen.Konstanter.*;

/**
 * Denne klassen tar for seg sending av meldinger til enheter som er online. <br />
 * Klassen er grovt basert samples-gcm-server og 
 * <a href="http://avilyne.com/?p=267">Android and GCM � Broadcast Yourself</a>
 * @author Knut Lucas Andersen
 */
@SuppressWarnings("serial")
public class SendMeldinger extends ForelderServlet {
	/** Hvor mange meldinger som kan sendes p� en gang via GCM **/
	private static final int MAX_ANTALL_UTSENDINGER = 1000;
	private ArrayList<String> onlineEnheter;
	private Message message;
	//Instanse av com.android.gcm.server.Sender som tar for seg sending 
	//av meldingen til GCM service
	private Sender sender;
	private boolean meldingSendesFraWeb = false;

	public SendMeldinger() {
		super();
		sender = new Sender(API_PROJECT_KEY);
	} //konstrukt�r

	@Override
	protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		String lat = "",
				lon = "",
				regID = "",
				emne = "",
				melding = "",
				enhetsNavn = "",
				utsendelsesType = "";
		try {
			meldingSendesFraWeb = false;
			onlineEnheter = Databehandling.hentAlleEnhetersRegID();
			//fors�k � hente ut utsendelsesType - hvis tom, 
			//sjekk om det er oversendt som attributt 
			utsendelsesType = hentParametere(request, MELDINGSTYPE, "");
			if (utsendelsesType.equals("")) {
				utsendelsesType = (String) request.getAttribute(MELDINGSTYPE);  
			} //if (utsendelsesType.equals("")) {
			//er det utsending av koordinater? is�fall, hent verdiene
			if (utsendelsesType.equals(SEND_KOORDINATER_MELDING)) {
				emne = (String) request.getAttribute(MELDINGS_EMNE);
				regID = hentParametere(request, PARAMETER_REGISTRATION_ID);
				enhetsNavn = hentParametere(request, PARAMETER_ENHETSNAVN);
				lat = hentParametere(request, PARAMETER_LATITUDE);
				lon = hentParametere(request, PARAMETER_LONGITUDE);
				opprettKoordinatMelding(emne, regID, enhetsNavn, lat, lon);
			} else if (utsendelsesType.equals(ENHET_GIKK_OFFLINE)) {
				emne = (String) request.getAttribute(MELDINGS_EMNE);
				regID = hentParametere(request, PARAMETER_REGISTRATION_ID);
				opprettEnhetGikkOffline(regID);
			} else if (utsendelsesType.equals(SEND_MELDING_TIL_ENHETER)){
				//vanlig melding som skal sendes ut
				meldingSendesFraWeb = true;
				melding = hentParametere(request, MELDINGS_INNHOLD);
				emne = hentParametere(request, MELDINGS_EMNE);
				opprettMeldingTilEnheter(emne, melding);
				//sender over verdiene slik at de fortsatt vises i meldingsfeltet
				request.setAttribute(MELDINGS_EMNE, emne);
				request.setAttribute(MELDINGS_INNHOLD, melding);
				//videresend bruker tilbake til sendmelding.jsp
				request.getRequestDispatcher("sendmelding.jsp").forward(request, response);
			} //if (utsendelsesType.equals(SEND_KOORDINATER_MELDING))
		} catch (Exception ex) {
			ex.printStackTrace();
		} //try/catch
	} //doPost

	/**
	 * Oppretter melding som inneholder koordinatene til en enhet. <br />
	 * Alle enheter som er online (foruten sender av koordinatene) vil motta 
	 * meldingen.
	 * @param emne - String: Meldingens emne
	 * @param regID - String: Senders registrationID
	 * @param lat - String: Senders latitude
	 * @param lon - String: Senders longitude
	 */
	private void opprettKoordinatMelding(String emne, String regID, String enhetsNavn, String lat, String lon) {
		//fjern enhet som sendte koordinatene, vil kun sende til andre p�loggede enheter
		onlineEnheter.remove(regID);
		if (onlineEnheter.size() > 0) {
			//Objektet som tar vare p� dataene som skal sendes
			message = new Message.Builder()
			.timeToLive(30)
			.delayWhileIdle(true)
			.addData(PARAMETER_REGISTRATION_ID, regID)
			.addData(PARAMETER_ENHETSNAVN, enhetsNavn)
			.addData(PARAMETER_LATITUDE, lat)
			.addData(PARAMETER_LONGITUDE, lon)
			.build();
			sendMeldingerTilEnhet();
		} //if (onlineEnheter.size() > 0)
	} //opprettKoordinatMelding

	/**
	 * Oppretter en melding om at en enhet gikk offline (koblet seg fra server). <br />
	 * Meldingen blir sendt til alle enheter som fortsatt er online.
	 * @param emne - String: Meldingens emne
	 * @param regID - String: RegistrationID'n til enheten som gikk offline
	 */
	protected void opprettEnhetGikkOffline(String regID) {
		//kaller p� denne her i tilfelle denne funksjonen blir kalt
		//pga. enhet(er) har blitt frakoblet server av administrator 
		onlineEnheter = Databehandling.hentAlleEnhetersRegID();
		//s�rger for at enhet ikke mottar offline melding 
		//(i tilfelle forsinkelse under oppdatering av av faktiske online enheter)
		onlineEnheter.remove(regID);
		//er det fortsatt enheter online?
		if (onlineEnheter.size() > 0) {
			//Objektet som tar vare p� dataene som skal sendes
			message = new Message.Builder()
			.timeToLive(30)
			.delayWhileIdle(true)
			.addData(ENHET_GIKK_OFFLINE, regID)
			.build();
			sendMeldingerTilEnhet();
		} //if (onlineEnheter.size() > 0)
	} //opprettEnhetGikkOffline

	/**
	 * Oppretter en melding som sendes ut til enhet som ble frakoblet server.
	 * @param regID - String: Enhetens registrationID
	 * @param melding - String: Meldingen som skal sendes
	 */
	protected void sendKobletFraServerMelding(String regID, String melding) {
		try {
			//Oppretter eget objektet her siden melding kun skal sendes til en bruker
			Message message = new Message.Builder()
			.timeToLive(30)
			.delayWhileIdle(true)
			.addData(SEND_MELDING_TIL_ENHETER, melding)
			.build();
			//oppretter arraylist og legger ved regID
			ArrayList<String> utsendingsListe = new ArrayList<String>();
			utsendingsListe.add(regID);
			//Hent resultatet av masseutsendingen via GCM 
			sender.send(message, utsendingsListe, 1);
		} catch (IOException ex) {
			ex.printStackTrace();
		} catch (Exception ex) {
			ex.printStackTrace();
		} //try/catch
	} //sendKobletFraServerMelding

	/**
	 * Oppretter en melding som sendes til enhetene. <br />
	 * Dette er en melding som kan leses p� enhet.
	 * @param emne - String: Meldingens emne
	 * @param melding - String: Meldingens innhold
	 */
	private void opprettMeldingTilEnheter(String emne, String melding) {
		//Objektet som tar vare p� dataene som skal sendes
		message = new Message.Builder()
		//hvis flere meldinger sendes med samme tittel/emne til enheter, og hvis 
		//enheten var offline n�r tidligere meldinger ble sendt, s� s�rger 
		//.collapseKey(...) for at brukeren kun mottar den siste meldingen
		//med gitt tittel
		.collapseKey(emne)
		.timeToLive(30)
		.delayWhileIdle(true)
		.addData(SEND_MELDING_TIL_ENHETER, melding)
		.build();
		sendMeldingerTilEnhet();
	} //opprettMeldingTilEnheter

	/**
	 * Sender meldinger til enhet. <br />
	 * Dersom antallet enheter online er st�rre enn grensen for hva GCM kan 
	 * h�ndtere (pr. dags dato 1000 enheter), <br /> 
	 * blir meldingsutsendelsen splittet opp. <br />
	 * Melding sendes f�rst til de f�rste 1000, deretter 
	 * til de resterende. <br />
	 * Melding sendes ut ved bruk av tr�dklassen MultiUtsendingAvMeldinger .
	 */
	private void sendMeldingerTilEnhet() {
		//skal det sendes ut melding til flere enn 1000 enheter?
		if (onlineEnheter.size() > MAX_ANTALL_UTSENDINGER) {
			//hent ut totalt antall p�loggede enheter
			int antallOnline = onlineEnheter.size();
			//opprett tom arraylist med plass til alle enheter som er online
			ArrayList<String> utsendingsListe = new ArrayList<String>(antallOnline);
			int teller = 0,
					antLagtTil = 0;
			for (String enhet : onlineEnheter) {
				//er 1000 enheter lagt til i arraylist/er alle enheter lagt til for utsending
				if (antLagtTil == MAX_ANTALL_UTSENDINGER || teller == antallOnline) {
					//opprett tr�d som tar for seg utsending av melding til enhetene
					MultiUtsendingAvMeldinger traad = new MultiUtsendingAvMeldinger(utsendingsListe);
					traad.start();
				} //if (antLagtTil == MAX_ANTALL_UTSENDINGER || teller == antallOnline)
				utsendingsListe.add(enhet);
				antLagtTil = utsendingsListe.size();
				teller++;
			} //for
		} else {
			//opprett tr�d som tar for seg utsending av melding til enhetene
			MultiUtsendingAvMeldinger traad = new MultiUtsendingAvMeldinger(onlineEnheter);
			traad.start();
		} //if (onlineEnheter.size() > MAX_ANTALL_UTSENDINGER)
	} //sendMeldingerTilEnhet

	/**
	 * Denne funksjonen tar for seg selve utsendingen av meldingen, etterfulgt av kontroll om alt gikk bra. <br />
	 * Funksjonen er synchronized for � s�rge for at meldinger sendes ut i korrekt rekkef�lge. <br />
	 * Dette gjelder spesielt ved utsending av koordinater.
	 * @param utsendingsListe - ArrayList&lt;String&gt;: RegistrationID's til enheter som skal motta melding
	 */
	private synchronized void sendMeldinger(ArrayList<String> utsendingsListe) {		
		try {
			//Hent resultatet av masseutsendingen via GCM 
			MulticastResult result = sender.send(message, utsendingsListe, utsendingsListe.size());
			//opprett liste over resultatene og sjekk resultatet
			List<Result> resultatListe = result.getResults();
			for (int i = 0; i < utsendingsListe.size(); i++) {
				//hent ut parameterne
				String registrationID = utsendingsListe.get(i);
				Result resultat = resultatListe.get(i);
				/*
				 * Hent ut meldingens ID og sjekk etter f�lgende:
				 * 	- Hvis meldingsID er null, s� betyr det at det var en feil; 
				 * 	  kall p� funksjonen getErrorCodeName()
				 * 	- Hvis meldingsID ikke er null, betyr det at melding ble sendt; 
				 * 	  kall p� getCanonicalRegistrationId()
				 * 		-> Hvis getCanonicalRegistrationId() returnerer null, 
				 * 		   trenger en ikke gj�re noe, hvis den har returverdi;
				 * 		   oppdater registrationID
				 * 
				 * Kilde: http://developer.android.com/reference/com/google/android/gcm/server/Result.html
				 */
				String meldingsID = resultat.getMessageId();
				if (meldingsID != null) {
					String canonicalRegId = resultat.getCanonicalRegistrationId();
					if (canonicalRegId != null) {
						//samme enhet har flere registrationID'r, oppdater ArrayList
						Databehandling.oppdaterRegistrationID(registrationID, canonicalRegId);
					} //if (canonicalRegId != null)
				} else {
					String feilKode = resultat.getErrorCodeName();
					if (feilKode.equals(Constants.ERROR_NOT_REGISTERED)) {
						//enhet er ikke lenger tilkoblet GCM, fjern den fra server
						Databehandling.avRegistrerEnhet(registrationID);
					} else {
						if (meldingSendesFraWeb) {
							//en mer kritisk feil oppsto, vis melding til administrator
							String tilbakemelding = "<font color='red'>Feil under sending av melding til " 
									+ registrationID + ": " + feilKode + "</font>";
							Databehandling.setTilbakemelding(tilbakemelding);
						} //if (meldingSendesFraWeb)
					} //if (feilKode.equals(Constants.ERROR_NOT_REGISTERED))
				} //if (meldingsID != null)
			}//for
		} catch (IOException ex) {
			ex.printStackTrace();
		} catch (Exception ex) {
			ex.printStackTrace();
		} //try/catch
	} //sendMeldinger

	/**
	 * Klasse som oppretter en tr�d. <br />
	 * Tr�den(e) som opprettes ved bruk av klassen tar for seg utsending av melding 
	 * til flere enheter.
	 * @author Knut Lucas Andersen
	 */
	private class MultiUtsendingAvMeldinger extends Thread implements Runnable {
		private final ArrayList<String> _utsendingsListe; 

		/**
		 * Konstrukt�r som mottar ArrayList. <br />
		 * ArrayList inneholder enhetene som skal motta meldingen.
		 * @param utsendingsListe - ArrayList&lt;String&gt;
		 */
		public MultiUtsendingAvMeldinger(ArrayList<String> utsendingsListe) {
			this._utsendingsListe = utsendingsListe;
		} //konstrukt�r

		public void run() {
			sendMeldinger(_utsendingsListe);
		} //run
	} //MultiUtsendingAvMeldinger
} //SendMeldinger