package com.example.oblig4_knutlucasandersen;

//java-import
import java.io.IOException;
import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.net.URLConnection;
import java.util.ArrayList;
//xml-import
import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.ParserConfigurationException;
//xml og dom-import
import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.NodeList;
import org.xml.sax.SAXException;
//android-import
import android.app.Activity;
import android.content.Context;
import android.content.SharedPreferences;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.os.Handler;
import android.os.Message;

/**
 * Klasse som tar for seg nedlasting og parsing av xml-data fra yr.no. <br />
 * Etter fullført nedlasting settes nytt tidspunkt for neste nedlasting.
 * @author Knut Lucas Andersen
 */
public class NedlastningsBehandler {
	public final static int SEND_MELDING_VIA_TOAST = 1001;

	/**
	 * Funksjon som sender en melding til Gui. <br />
	 * Meldingen som sendes er en String som skal fremvises via en Toast.
	 * @param handler - Handler som håndterer melding som sendes
	 * @param melding - String: Melding som skal sendes
	 */
	public static void sendMeldingTilGuiTraad(Handler handler, String melding) {
		Message msg = new Message();
		//sett argument, objekt som skal sendes og handler som skal motta melding
		msg.arg1 = SEND_MELDING_VIA_TOAST;
		msg.obj = (Object)melding;
		msg.setTarget(handler);
		//send melding til handler
		msg.sendToTarget();
	} //sendMeldingTilGuiTraad

	/**
	 * Denne funksjonen sjekker om bruker er tilkoblet internett. <br />
	 * Hvis bruker er tilkoblet internet, så startes nedlasting av værdata fra yr.no <br />
	 * Hvis bruker ikke er tilkoblet internett, får bruker feilmelding.
	 * @param context - Context
	 * @param handler - Handler
	 */
	public static void startNedlastingAvVaerData(Context context, Handler handler) {
		//Sjekk om tilkobling til nettverk eksisterer
		ConnectivityManager tilkoblingsManager = (ConnectivityManager) context.getSystemService(Context.CONNECTIVITY_SERVICE);
		NetworkInfo nettverksInfo = tilkoblingsManager.getActiveNetworkInfo();
		//er bruker tilkoblet nettverket?
		if (nettverksInfo != null && nettverksInfo.isConnected()) {
			hentVaerDataFraYr(context, handler);
		} else {
			//bruker er ikke tilkoblet, vis feilmelding
			String feilmelding = context.getResources().getString(R.string.no_internet_connection_found);
			sendMeldingTilGuiTraad(handler, feilmelding);
		} //if (nettverksInfo != null && nettverksInfo.isConnected())
	} //erTilkobletInternett

	/**
	 * Funksjon som kobler til og starter nedlasting av data. <br />
	 *  Nedlastede data blir deretter oversendt til  
	 * {@link #lesTemperaturerFraXMLDokument(InputStream) lesTemperaturerFraXMLDokument} 
	 * og lagt til i ArrayList&lt;Maalestasjon&gt;. <br />
	 * Etter data er ferdig nedlastet, blir resultatet skrivd til fil.
	 * @param context - Context
	 * @param handler - Handler
	 */
	private static void hentVaerDataFraYr(Context context, Handler handler) {
		try {			
			URL url;
			String hyperlenke = "", 
					forrigeLenke = "";
			//hent ut brukers valgte målestasjoner
			ArrayList<Maalestasjon> brukersMaalestasjoner = Filbehandling.lesFraFil(context, false);
			//arraylist som skal ta vare på innleste temperaturer fra nedlasting
			ArrayList<Maalestasjon> tempListe = new ArrayList<Maalestasjon>();
			//loop gjennom listen over målestasjoner bruker vil følge
			for (int i = 0; i < brukersMaalestasjoner.size(); i++) {
				//url til siden som temperatur skal hentes fra
				forrigeLenke = hyperlenke;
				hyperlenke = brukersMaalestasjoner.get(i).getMaalestasjonURL();
				//er det samme lenke for gjeldende målestasjon?
				if (!forrigeLenke.equals(hyperlenke)) {
					url = new URL(hyperlenke);			
					//opprett tilkobling og åpne tilgang
					URLConnection connection;
					connection = url.openConnection();
					HttpURLConnection httpConnection = (HttpURLConnection)connection;
					//hent ut responskoden for tilkobling
					int responseCode = httpConnection.getResponseCode();
					//ble tilkobling opprettet og resultat hentet ut?
					if (responseCode == HttpURLConnection.HTTP_OK) {
						//les data fra HTTP og legg det i arraylist
						InputStream inputStream = httpConnection.getInputStream();
						lesTemperaturerFraXMLDokument(inputStream, tempListe);
					} //if (responseCode == HttpURLConnection.HTTP_OK)
				} //if (!forrigeLenke.equals(hyperlenke))
			} //for
			//loop gjennom listen med temperaturer og sammenlign den med listen 
			//over målestasjoner bruker vil følge
			for (int i = (tempListe.size() - 1); i > -1; i--) {
				boolean finnes = loopGjennomEksisterendeMaalestasjoner(tempListe, brukersMaalestasjoner, i);
				if (!finnes) {
					tempListe.remove(i);
				} //if (!finnes)
			} //for
			//skriv data til fil og sett neste nedlastingstidspunkt
			Filbehandling.skrivTilFil(context, tempListe, true);
			settNesteNedlastingsTidspunkt(context);
			sjekkTemperatur(context, handler, tempListe);
		} catch (MalformedURLException ex) {
			//URL-feil
			ex.printStackTrace();
		} catch (SAXException ex) {
			//feil/advarsler fra xml-parser
			ex.printStackTrace();
		} catch (IOException ex) {
			//feil under tilkobling til HTTP
			ex.printStackTrace();
		} catch (ParserConfigurationException ex) {
			//konfigurasjonsfeil under parsing av xml
			ex.printStackTrace();
		} catch (Exception ex) {
			//uventet feil oppsto, vis feilmelding til bruker
			String feilmelding = context.getString(R.string.uknown_error_exception);
			sendMeldingTilGuiTraad(handler, feilmelding + ex.getMessage());
		} //try/catch
	} //hentVaerDataFraYr

	private static boolean loopGjennomEksisterendeMaalestasjoner(ArrayList<Maalestasjon> tempListe, ArrayList<Maalestasjon> brukerListe, int indeks) {
		boolean finnes = false;
		for (int j = 0; j < brukerListe.size(); j++) {
			//hent ut målestasjonnr
			int nyID = tempListe.get(indeks).getMaalestasjonNr();
			int eksisterendeID = brukerListe.get(j).getMaalestasjonNr();
			//er målestasjonen registrert fra før?
			if (nyID == eksisterendeID) {
				finnes = true;
			} //if (nyID != eksisterendeID) 
		} //for
		return finnes;
	} //loopGjennomEksisterendeMaalestasjoner

	/**
	 * Denne funksjonen parser XML. <br />
	 * InputStream som sendes over blir omgjort til DOM og deretter parset. <br />
	 * Verdiene som hentes ut er målestasjonsnr, stedsnavn og temperatur. <br /> 
	 * Disse verdiene blir lagt til i ArrayList&lt;Maalestasjon&gt;.
	 * @param inputStream - InputStream: Input fra HttpURLConnection.getInputStream()
	 * @param ms - ArrayList&lt;Maalestasjon&gt;: ArrayList som skal fylles med temperaturer
	 * @throws SAXException
	 * @throws IOException
	 * @throws ParserConfigurationException
	 * @throws NullPointerException
	 */
	private static void lesTemperaturerFraXMLDokument(InputStream inputStream, ArrayList<Maalestasjon> ms) 
			throws SAXException, IOException, ParserConfigurationException, NullPointerException {
		//opprett objekt for å lese DOM fra XML
		DocumentBuilderFactory dbf = DocumentBuilderFactory.newInstance();
		DocumentBuilder db = dbf.newDocumentBuilder();
		//les innhold fra xml-dokument
		Document dom = db.parse(inputStream);
		Element xmlElement = dom.getDocumentElement();
		//opprett liste over elementer med tagg-navn 'observations'
		NodeList nodeListeObservations = xmlElement.getElementsByTagName("observations");
		NodeList nodeListeWeatherstations = xmlElement.getElementsByTagName("weatherstation");
		//inneholder nodelisten elementer?
		if ((nodeListeObservations != null && nodeListeObservations.getLength() > 0)
				&& (nodeListeWeatherstations != null && nodeListeWeatherstations.getLength() > 0)) {
			//loop gjennom nodelisten
			for (int i = 0; i < nodeListeWeatherstations.getLength(); i++) {	
				//hent ut gjeldende værstasjon element
				Element weatherstation = (Element)nodeListeWeatherstations.item(i);
				//har værstasjonen noder?
				if (weatherstation.hasChildNodes()) {
					//værstasjonen har barn, hent ut temperatur elementet
					Element temperature = (Element)weatherstation.getElementsByTagName("temperature").item(0);
					//hent ut attributter som inneholder verdiene vi vil vise til bruker
					int stasjonsNr = Integer.parseInt(weatherstation.getAttribute("stno"));
					String stedsNavn = weatherstation.getAttribute("name");
					float temperatur = Float.parseFloat(temperature.getAttribute("value"));					
					//legg inn verdiene i objektet
					ms.add(new Maalestasjon(stasjonsNr, stedsNavn, temperatur));
				} //if (weatherstation.hasChildNodes())
			} //for			
		} //if (nodeListe != null && nodeListe.getLength() > 0 ...)
	} //lesTemperaturerFraXMLDokument

	/**
	 * Denne funksjonen kobler til og igangsetter nedlasting ved suksesfull tilkobling til internett. <br />
	 * Nedlastet informasjon blir videresent til 
	 * {@link #lesMaalestasjonerFraXMLDokument(InputStream, ArrayList, String) lesMaalestasjonerFraXMLDokument} <br />
	 * og deretter lagt inn i ArrayList&lt;Maalestasjon&gt;.
	 * @param context - Context
	 * @param handler - Handler: Handler som håndterer meldinger til tråden
	 * @param ms - ArrayList&lt;Maalestasjon&gt;: ArrayList som skal fylles med målestasjoner som skal fremvises
	 * @param indeks - int: Indeks til valgt sted i ArrayList
	 */
	public static void lastNedMaalestasjoner(Context context, Handler handler, ArrayList<Maalestasjon> ms, int indeks) {
		try {
			ArrayList<Maalestasjon> listeOverMaalestasjoner = Filbehandling.lesFraRawFil(context);
			//Sjekk om tilkobling til nettverk eksisterer
			ConnectivityManager tilkoblingsManager = (ConnectivityManager) context.getSystemService(Context.CONNECTIVITY_SERVICE);
			NetworkInfo nettverksInfo = tilkoblingsManager.getActiveNetworkInfo();
			//er bruker tilkoblet nettverket?
			if (nettverksInfo != null && nettverksInfo.isConnected()) {
				String hyperlenke = listeOverMaalestasjoner.get(indeks).getMaalestasjonURL();
				URL url;
				url = new URL(hyperlenke);			
				//opprett tilkobling og åpne tilgang
				URLConnection connection;
				connection = url.openConnection();
				HttpURLConnection httpConnection = (HttpURLConnection)connection;
				//hent ut responskoden for tilkobling
				int responseCode = httpConnection.getResponseCode();
				//ble tilkobling opprettet og resultat hentet ut?
				if (responseCode == HttpURLConnection.HTTP_OK) {
					//les data fra HTTP og legg det i arraylist
					InputStream inputStream = httpConnection.getInputStream();
					lesMaalestasjonerFraXMLDokument(inputStream, ms, hyperlenke);
				} //if (responseCode == HttpURLConnection.HTTP_OK)
			} else {
				//bruker er ikke tilkoblet, vis feilmelding
				String feilmelding = context.getString(R.string.no_internet_connection_found);
				sendMeldingTilGuiTraad(handler, feilmelding);
			} //if (nettverksInfo != null && nettverksInfo.isConnected())
		} catch (MalformedURLException ex) {
			//URL-feil
			ex.printStackTrace();
		} catch (NullPointerException ex) {
			//URL-feil
			ex.printStackTrace();
		} catch (SAXException ex) {
			//feil/advarsler fra xml-parser
			ex.printStackTrace();
		} catch (IOException ex) {
			//feil under tilkobling til HTTP
			ex.printStackTrace();
		} catch (ParserConfigurationException ex) {
			//konfigurasjonsfeil under parsing av xml
			ex.printStackTrace();
		} catch (Exception ex) {
			String feilmelding = context.getString(R.string.uknown_error_exception);
			sendMeldingTilGuiTraad(handler, feilmelding + ex.getMessage());
		} //try/catch
	} //lastNedMaalestasjoner

	/**
	 * Denne funksjonen parser XML. <br />
	 * InputStream som sendes over blir omgjort til DOM og deretter parset. <br />
	 * Verdiene som hentes ut er målestasjonsnr og stedsnavn (URL legges til). <br /> 
	 * Disse verdiene blir lagt til i ArrayList&lt;Maalestasjon&gt;.
	 * @param inputStream - InputStream: Input fra HttpURLConnection.getInputStream()
	 * @param ms - ArrayList&lt;Maalestasjon&gt;: ArrayList som skal fylles med nedlastede målestasjoner
	 * @param url - String: URL-adressen hvor målestasjoner lastes ned fra
	 * @throws SAXException
	 * @throws IOException
	 * @throws ParserConfigurationException
	 * @throws NullPointerException
	 */
	private static void lesMaalestasjonerFraXMLDokument(InputStream inputStream, ArrayList<Maalestasjon> ms, String url) 
			throws SAXException, IOException, ParserConfigurationException, NullPointerException {
		//opprett objekt for å lese DOM fra XML
		DocumentBuilderFactory dbf = DocumentBuilderFactory.newInstance();
		DocumentBuilder db = dbf.newDocumentBuilder();
		//les innhold fra xml-dokument
		Document dom = db.parse(inputStream);
		Element xmlElement = dom.getDocumentElement();
		//opprett liste over elementer med tagg-navn 'observations'
		NodeList nodeListeObservations = xmlElement.getElementsByTagName("observations");
		NodeList nodeListeWeatherstations = xmlElement.getElementsByTagName("weatherstation");
		//inneholder nodelisten elementer?
		if ((nodeListeObservations != null && nodeListeObservations.getLength() > 0)
				&& (nodeListeWeatherstations != null && nodeListeWeatherstations.getLength() > 0)) {
			//loop gjennom nodelisten
			for (int i = 0; i < nodeListeWeatherstations.getLength(); i++) {	
				//hent ut gjeldende værstasjon element
				Element weatherstation = (Element)nodeListeWeatherstations.item(i);
				//har værstasjonen noder?
				if (weatherstation.hasChildNodes()) {
					//hent ut attributter som inneholder verdiene vi vil vise til bruker
					int stasjonsNr = Integer.parseInt(weatherstation.getAttribute("stno"));
					String stedsNavn = weatherstation.getAttribute("name");					
					//legg inn verdiene i objektet
					ms.add(new Maalestasjon(stasjonsNr, stedsNavn, url));
				} //if (weatherstation.hasChildNodes())
			} //for			
		} //if (nodeListe != null && nodeListe.getLength() > 0 ...)
	} //lesMaalestasjonerFraXMLDokument

	/**
	 * Funksjon som setter neste tidspunkt for nedlasting av værdata. <br />
	 * Tidspunktet lagres i SharedPreferences. <br />
	 * Dette fordi at dersom enhet skrues av eller service stoppes over lengre tid, <br />
	 * så kan det være at neste gang service startes, så har tidspunkt for nedlasting passert. <br />
	 * Dette blir da sjekket ved neste oppstart av service.
	 * @param context - Context
	 */
	private static void settNesteNedlastingsTidspunkt(Context context) {
		//opprett et objekt av SharedPreference
		int modus = Activity.MODE_PRIVATE;
		SharedPreferences deltPreferanse = context.getSharedPreferences(Filbehandling.DELT_TEMPERATUR_PREFERANSE, modus);
		//hent ut eksisterende verdier fra sharedpreferences (øvre temp grense)
		int defaultOvreTempGrense = context.getResources().getInteger(R.integer.default_upper_value_temperature_limit);
		float ovreTempGrense = deltPreferanse.getFloat(Filbehandling.OVRE_TEMPERATUR_NOKKEL, defaultOvreTempGrense);
		//hent ut eksisterende verdier fra sharedpreferences (nedre temp grense)
		int defaultNedreTempGrense = context.getResources().getInteger(R.integer.default_lower_value_temperature_limit);
		float nedreTempGrense = deltPreferanse.getFloat(Filbehandling.NEDRE_TEMPERATUR_NOKKEL, defaultNedreTempGrense);
		//hent ut tidsintervall for nedlasting
		int defaultIntervall = context.getResources().getInteger(R.integer.default_time_interval_download);
		int tidsIntervall = deltPreferanse.getInt(Filbehandling.TIDSINTERVALL_NEDLASTING, defaultIntervall);
		//opprett tidspunkt for neste oppdatering
		String tidspunkt = ActionBarFunksjoner.returnNesteNedlastingsTidspunkt(tidsIntervall);
		//skriv endringen til fil
		Filbehandling.skrivTilSharedPreferences(context, ovreTempGrense, nedreTempGrense, tidspunkt, tidsIntervall);
	} //settNesteNedlastingsTidspunkt

	/**
	 * Funksjon som sjekker om temperaturer hentet fra siste nedlasting er over/under satt grense. <br />
	 * Hvis temperatur er over/under grense sendes en melding til Handler som inneholder sted, temperatur 
	 * og om temperatur var over eller under satt grense.
	 * @param context - Context
	 * @param handler - Handler
	 * @param ms - ArrayList&lt;Maalestasjon&gt;
	 */
	private static void sjekkTemperatur(Context context, Handler handler, ArrayList<Maalestasjon> ms) {
		String melding = "";
		char gradTegn = '\u00B0';
		//opprett et objekt av SharedPreference
		int modus = Activity.MODE_PRIVATE;
		SharedPreferences deltPreferanse = context.getSharedPreferences(Filbehandling.DELT_TEMPERATUR_PREFERANSE, modus);
		//hent ut eksisterende verdier fra sharedpreferences (øvre temp grense)
		int defaultOvreTempGrense = context.getResources().getInteger(R.integer.default_upper_value_temperature_limit);
		float ovreTempGrense = deltPreferanse.getFloat(Filbehandling.OVRE_TEMPERATUR_NOKKEL, defaultOvreTempGrense);
		//hent ut eksisterende verdier fra sharedpreferences (nedre temp grense)
		int defaultNedreTempGrense = context.getResources().getInteger(R.integer.default_lower_value_temperature_limit);
		float nedreTempGrense = deltPreferanse.getFloat(Filbehandling.NEDRE_TEMPERATUR_NOKKEL, defaultNedreTempGrense);
		//loop gjennom verdier som ble lagt til i array for å sjekke om temperatur er over/under gitt grense
		for (int i = 0; i < ms.size(); i++) {
			float temp = ms.get(i).getTemperatur();
			//er temperatur over øvre grense?
			if (temp > ovreTempGrense) {
				//vis melding med stedsnavn og temperatur registrert
				melding = context.getString(R.string.notification_upper_limit) + '\n'
						+ ms.get(i).getStedsNavn() + " " + ms.get(i).getTemperatur() 
						+ gradTegn + "C";
				sendMeldingTilGuiTraad(handler, melding);
			} else if (temp < nedreTempGrense) { //er temperatur under nedre grense?
				//vis melding med stedsnavn og temperatur registrert
				melding = context.getString(R.string.notification_lower_limit) + '\n'
						+ ms.get(i).getStedsNavn() + " " + ms.get(i).getTemperatur() 
						+ gradTegn + "C";
				sendMeldingTilGuiTraad(handler, melding);
			} //if (temp > ovreTempGrense)
		} //for
	} //sjekkTemperatur
} //NedlastningsBehandler
