package com.example.oblig3_knutlucasandersen_del1;

//java-import
import java.io.File;
import java.io.BufferedReader;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.util.ArrayList;
//android-import
import android.content.Context;
import android.content.res.Resources;
import android.widget.Toast;

/**
 * Denne klassen tar for seg operasjoner tilknyttet filbehandling. <br />
 * Dette gjelder omr�der som oppretting, lesing og skriving til fil. <br />
 * Denne klassen er tilknyttet prosjektet Oblig3 og baserer seg 
 * hovedsaklig p� � skrive boolske variabler til fil.  <br />
 * Disse variablene er tilknyttet hvilke hendelser som bruker �nsker � logge.
 * @author Knut Lucas Andersen
 */
public class Filbehandling {

	/**
	 * Oppretter en fil p� gitt lokasjon basert p� oversendt 
	 * Context (context.getFilesDir()) med oppgitt filnavn.
	 * @param context
	 * @param filnavn
	 */
	private static void opprettFil(Context context, String filnavn) {
		@SuppressWarnings("unused")
		File file = new File(context.getFilesDir(), filnavn);
	} //opprettFil

	/**
	 * Oppretter en ArrayList<String> basert p� filinnhold. <br />
	 * Dersom fil ikke finnes, blir denne opprettet. <br />
	 * Dersom filen er tom, blir alle registrerte kategorier 
	 * satt til true (alt skal logges - default setting)
	 * @param context
	 * @return ArrayList<Boolean> - filens innhold
	 */
	public static ArrayList<Boolean> lesFraFil(Context context) {
		String enLinje;
		//arrayliste som inneholder verdier for om kategori skal logges
		ArrayList<Boolean> loggListe = new ArrayList<Boolean>();
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
			if (fil.length() == 0) {
				//fil er tom, hent ut antall kategorier
				int antallKategorier = res.getStringArray(R.array.array_kategorier).length;
				//fyll arraylist med true (alle kategorier skal logges - default setting)	
				for (int i = 0; i < antallKategorier; i++) {
					loggListe.add(true);
				} //for
			} else { //fil har innhold
				//fors�k � �pne og lese fra fil
				FileInputStream filInputStream = context.openFileInput(filnavn);
				InputStreamReader inputStreamLeser = new InputStreamReader(filInputStream);
				BufferedReader bufferLeser = new BufferedReader(inputStreamLeser);
				//les f�rste linje fra filen
				enLinje = bufferLeser.readLine();				
				//s� lenge fil har innhold...
				while (enLinje != null) {
					//legg til innlest linje i arraylist
					loggListe.add(Boolean.valueOf(enLinje));
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
			Toast.makeText(context, "Feil oppsto under lesing: \n" + ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
		return loggListe;
	} //lesFraFil

	/**
	 * Skriver til fil det som oversendt (det ArrayList<Boolean> inneholder). <br />
	 * Dersom fil ikke finnes, blir denne opprettet.
	 * @param context
	 * @param loggListe
	 */
	public static void skrivTilFil(Context context, ArrayList<Boolean> loggListe) {
		try {
			//objekt av ressursklassen
			Resources res = context.getResources();
			//string som inneholder "feilmelding" (dersom intet er registrert)
			String intetRegistrert = res.getString(R.string.tomFil);
			//hent ut indeks for "feilmelding"
			//(vil ligge p� indeks:0 dersom n�v�rende registrering er f�rste registrering) 
			int indeksTomFil = loggListe.indexOf(intetRegistrert);
			//er feilmelding i array?
			if (indeksTomFil != -1) {
				//fjern feilmelding fra array				
				loggListe.remove(indeksTomFil);
			} //if (indeksTomFil != -1)			
			//hent filnavn fra ressurser
			String filnavn = res.getString(R.string.filnavn);
			//opprett et filobjekt
			File fil = context.getFileStreamPath(filnavn);
			//finnes filen?
			if (!fil.exists()) {
				//fil finnes ikke, opprett fil
				opprettFil(context, filnavn);
			} //if (!fil.exists())
			//fors�k � �pne filen og opprett skriver
			FileOutputStream fileOutputStrem = context.openFileOutput(filnavn, Context.MODE_PRIVATE);
			OutputStreamWriter outputStreamSkriver = new OutputStreamWriter(fileOutputStrem);
			//loop gjennom innhold i arraylist og skriv det til fil
			for (int i = 0; i < loggListe.size(); i++) {
				//skriv innhold til fil separert med newline
				String kategoriVerdi = loggListe.get(i).toString() + '\n';
				outputStreamSkriver.write(kategoriVerdi);
			} //for
			//lukk skrivere
			outputStreamSkriver.close();
			fileOutputStrem.close();
		} catch (Exception ex) {
			//en feil oppsto, vis feilmelding til bruker
			Toast.makeText(context, "Feil oppsto under skriving: \n" + ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //skrivTilFil
} //Filbehandling