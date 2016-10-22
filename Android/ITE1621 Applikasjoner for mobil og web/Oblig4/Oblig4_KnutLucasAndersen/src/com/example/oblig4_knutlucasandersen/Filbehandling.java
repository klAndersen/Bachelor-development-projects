package com.example.oblig4_knutlucasandersen;

//java-import
import java.io.BufferedReader;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.util.ArrayList;
import java.util.StringTokenizer;
//android-import
import android.app.Activity;
import android.content.Context;
import android.content.SharedPreferences;
import android.content.res.Resources;
import android.widget.Toast;

/**
 * Denne klassen tar for seg operasjoner tilknyttet filbehandling. <br />
 * Dette gjelder områder som oppretting, lesing og skriving til fil. <br />
 * @author Knut Lucas Andersen
 */
public class Filbehandling {
	public static final String DELT_TEMPERATUR_PREFERANSE = "temperaturPreferanse";
	public static final String OVRE_TEMPERATUR_NOKKEL = "ovreTempGrense";
	public static final String NEDRE_TEMPERATUR_NOKKEL = "nedreTempGrense";
	public static final String OPPDATERINGS_TIDSPUNKT_NOKKEL = "oppdateringsTidspunkt";
	public static final String TIDSINTERVALL_NEDLASTING = "tidsintervallNedlasting";

	/**
	 * Oppretter en fil på gitt lokasjon basert på oversendt 
	 * Context (context.getFilesDir()) med oppgitt filnavn.
	 * @param context - Context
	 * @param filnavn - String: Filen som det skal leses fra/skrives til
	 */
	private static void opprettFil(Context context, String filnavn) {
		@SuppressWarnings("unused")
		File file = new File(context.getFilesDir(), filnavn);
	} //opprettFil

	/**
	 * Oppretter en ArrayList&lt;Maalestasjon&gt; basert på filinnhold. <br />
	 * Dersom fil ikke finnes, blir denne opprettet. <br />
	 * @param context - Context
	 * @param lesTemperaturer - Boolean: Hvis true, skal temperaturliste hentes, 
	 * hvis false skal brukers valgte målestasjoner hentes
	 * @return ArrayList&lt;Maalestasjon&gt; - filens innhold
	 */
	public static ArrayList<Maalestasjon> lesFraFil(Context context, boolean lesTemperaturer) {
		String enLinje;
		Maalestasjon maalestasjon = null;
		//arrayliste som skal inneholde målestasjonens verdier
		ArrayList<Maalestasjon> maalestasjonsListe = new ArrayList<Maalestasjon>();
		try {
			//objekt av ressursklassen
			Resources res = context.getResources();
			//hent filnavn via ressurser
			String filnavn = "";
			//hvilken fil skal det leses fra?
			if (lesTemperaturer) {
				filnavn = res.getString(R.string.filename_saved_temperatures);
			} else {
				filnavn = res.getString(R.string.filename_saved_stations);
			} //if (lesTemperaturer)
			//opprett et filobjekt
			File fil = context.getFileStreamPath(filnavn);
			//finnes filen?
			if (!fil.exists()) {
				//fil finnes ikke, opprett fil
				opprettFil(context, filnavn);
			} //if (!fil.exists())
			//har filen innhold?
			if (fil.length() != 0) {
				//forsøk å åpne og lese fra fil
				FileInputStream filInputStream = context.openFileInput(filnavn);
				InputStreamReader inputStreamLeser = new InputStreamReader(filInputStream);
				BufferedReader bufferLeser = new BufferedReader(inputStreamLeser);
				//les første linje fra filen
				enLinje = bufferLeser.readLine();				
				//så lenge fil har innhold...
				while (enLinje != null) {
					//opprett objekt av stringtokenizer for å dele opp linja
					StringTokenizer stringToken = new StringTokenizer(enLinje);
					//hent ut verdiene med stringtokenizer
					String stno = stringToken.nextToken(",");
					String sted = stringToken.nextToken(",");
					String verdi = stringToken.nextToken();
					//konverter stasjonsnr til integer
					int msNr = Integer.parseInt(stno);
					//er det temperaturer som skal hentes fra fil?
					if (lesTemperaturer) {
						float temperatur = Float.parseFloat(verdi);
						maalestasjon = new Maalestasjon(msNr, sted, temperatur);
					} else { 
						//skal hente ut liste over brukers valgte målesatsjoner
						maalestasjon = new Maalestasjon(msNr, sted, verdi);
					} //if (lesTemperaturer)
					//legg til målestasjon i arraylist
					maalestasjonsListe.add(maalestasjon);
					//les neste linje fra fil
					enLinje = bufferLeser.readLine();
				} //while
				//lukk leserne
				inputStreamLeser.close();
				bufferLeser.close();
				filInputStream.close();
			} //if (fil.length() == 0) 
		} catch (Exception ex) {
			//objekt av ressursklassen
			Resources res = context.getResources();
			//en feil oppsto, vis feilmelding til bruker
			Toast.makeText(context, res.getString(R.string.read_from_file_exception) + ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
		return maalestasjonsListe;
	} //lesFraFil

	/**
	 * Funksjon som leser fra en raw fil. <br />
	 * Raw filen ligger i mappen res/raw og inneholder "alle" 
	 * steder som man kan lese temperaturer fra.
	 * @param context - Context
	 * @return ArrayList&lt;Maalestasjon&gt; - filens innhold
	 */
	public static ArrayList<Maalestasjon> lesFraRawFil(Context context) {
		ArrayList<Maalestasjon> stedListe = new ArrayList<Maalestasjon>();
		try {
			InputStream inputStream = context.getResources().openRawResource(R.raw.places_to_observe);
			//enkoding er funnet via Properties til raw-fil under Encoding
			InputStreamReader inputStreamLeser = new InputStreamReader(inputStream, "cp-1252");
			BufferedReader bufferLeser = new BufferedReader(inputStreamLeser);
			String enLinje = bufferLeser.readLine();
			while (enLinje != null) {
				//opprett objekt av stringtokenizer for å dele opp linja
				StringTokenizer stringToken = new StringTokenizer(enLinje);
				//hent ut verdiene med stringtokenizer
				String stno = stringToken.nextToken(",");
				String sted = stringToken.nextToken(",");
				String url = stringToken.nextToken();
				//konverter stasjonsnr til integer
				int msNr = Integer.parseInt(stno);
				//legg til målestasjon i arraylist
				stedListe.add(new Maalestasjon(msNr, sted, url));
				//les neste linje fra fil
				enLinje = bufferLeser.readLine();
			} //while
			//lukk leserne
			inputStreamLeser.close();
			bufferLeser.close();
			inputStream.close();
		} catch (Exception ex) {
			//objekt av ressursklassen
			Resources res = context.getResources();
			//en feil oppsto, vis feilmelding til bruker
			Toast.makeText(context, res.getString(R.string.read_from_file_exception) + ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
		return stedListe;
	} //lesFraRawFil

	/**
	 * Skriver til fil det som oversendt (det ArrayList&lt;Maalestasjon&gt; inneholder). <br />
	 * Dersom fil ikke finnes, blir denne opprettet.
	 * @param context - Context
	 * @param maalestasjonsListe - ArrayList&lt;Maalestasjon&gt;: Inneholder målestasjonen(es) verdier som skal skrives til fil
	 * @param skrivTemperaturer - Boolean: Hvis true, skriv sist nedlastede temperaturer til fil, <br />
	 * hvis false, skriv brukers valgte målestasjoner til fil
	 */
	public static void skrivTilFil(Context context, ArrayList<Maalestasjon> maalestasjonsListe, boolean skrivTemperaturer) {
		try {
			//objekt av ressursklassen
			Resources res = context.getResources();		
			//hent filnavn fra ressurser
			String filnavn = "";
			//hvilken fil skal det skrives til?
			if (skrivTemperaturer) {
				filnavn = res.getString(R.string.filename_saved_temperatures);
			} else {
				filnavn = res.getString(R.string.filename_saved_stations);
			} //if (lesTemperaturer)
			//opprett et filobjekt
			File fil = context.getFileStreamPath(filnavn);
			//finnes filen?
			if (!fil.exists()) {
				//fil finnes ikke, opprett fil
				opprettFil(context, filnavn);
			} //if (!fil.exists())
			//forsøk å åpne filen og opprett skriver
			FileOutputStream fileOutputStrem = context.openFileOutput(filnavn, Context.MODE_PRIVATE);
			OutputStreamWriter outputStreamSkriver = new OutputStreamWriter(fileOutputStrem);
			//loop gjennom innhold i arraylist og skriv det til fil
			for (int i = 0; i < maalestasjonsListe.size(); i++) {
				//skriv innhold til fil, separert med newline og space mellom hver del
				//Eksempel: 32060 Gvarv http://www.yr.no/..... 
				String verdier = maalestasjonsListe.get(i).getMaalestasjonNr() + ", " 
						+ maalestasjonsListe.get(i).getStedsNavn() + ", ";
				//skal temperaturer lagres?
				if (skrivTemperaturer) {
					verdier += maalestasjonsListe.get(i).getTemperatur();
				} else {
					//brukers valgte målestasjoner skal lagres
					verdier += maalestasjonsListe.get(i).getMaalestasjonURL(); 
				} //if (skrivTemperaturer)
				//legg til linjeskift/newline på slutten
				verdier += '\n';				
				outputStreamSkriver.write(verdier);
			} //for
			//lukk skrivere
			outputStreamSkriver.close();
			fileOutputStrem.close();
		} catch (Exception ex) {
			//objekt av ressursklassen
			Resources res = context.getResources();
			//en feil oppsto, vis feilmelding til bruker
			Toast.makeText(context, res.getString(R.string.write_to_file_exception) + ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //skrivTilFil

	/**
	 * Denne funksjonen skriver verdier til SharedPreferences. <br /> 
	 * Verdiene som skrives er øvre temperaturgrense, nedre temperaturgrense, <br />
	 * og tidspunkt for neste nedlasting av xml-fil fra yr.no
	 * @param context - Context
	 * @param ovreTempGrense - float: Den øvre temperaturgrensen
	 * @param nedreTempGrense - float: Den nedre temperaturgrensen
	 * @param tidspunkt - String: Tidspunkt for neste nedlasting av temperaturdata
	 * @param intervall - int: Hvor ofte nedlasting skal skje
	 */
	public static void skrivTilSharedPreferences(Context context, float ovreTempGrense, float nedreTempGrense, String tidspunkt, int intervall) {
		//sett at det er kun denne applikasjonen som skal bruke oppgitt sharedpreference
		int modus = Activity.MODE_PRIVATE;
		//opprett og hent preferansen
		SharedPreferences deltPreferanse = context.getSharedPreferences(DELT_TEMPERATUR_PREFERANSE, modus);
		//opprett en editor for å redigere sharedpreference
		SharedPreferences.Editor spEditor = deltPreferanse.edit();
		//lagre verdier i sharedpreference
		spEditor.putFloat(OVRE_TEMPERATUR_NOKKEL, ovreTempGrense);
		spEditor.putFloat(NEDRE_TEMPERATUR_NOKKEL, nedreTempGrense);
		spEditor.putString(OPPDATERINGS_TIDSPUNKT_NOKKEL, tidspunkt);
		spEditor.putInt(TIDSINTERVALL_NEDLASTING, intervall);
		//lagre endringene
		spEditor.commit();
	} //skrivTilSharedPreferences
} //Filbehandling