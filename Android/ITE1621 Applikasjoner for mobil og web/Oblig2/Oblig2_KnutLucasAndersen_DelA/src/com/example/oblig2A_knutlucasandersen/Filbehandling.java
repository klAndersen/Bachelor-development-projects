package com.example.oblig2A_knutlucasandersen;

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
 * Denne klassen tar for seg operasjoner tilknyttet filbehandling.
 * Dette gjelder omr�der som oppretting, lesing og skriving til fil.
 * @author Knut Lucas Andersen
 */
public class Filbehandling {

	/**
	 * Oppretter en fil p� gitt lokasjon basert p� oversendt 
	 * Context (context.getFilesDir()) med oppgitt filnavn
	 * @param context
	 * @param filnavn
	 */
	private static void opprettFil(Context context, String filnavn) {
		@SuppressWarnings("unused")
		File file = new File(context.getFilesDir(), filnavn);
	} //opprettFil

	/**
	 * Oppretter en ArrayList<String> basert p� filinnhold.
	 * Dersom fil ikke finnes, blir denne opprettet.
	 * Dersom filen er tom, returneres kun en string som 
	 * forteller at ingen b�ker er registrert
	 * @param context
	 * @return ArrayList<String> - filens innhold
	 */
	public static ArrayList<String> lesFraFil(Context context) {
		String enLinje;
		//arrayliste som tar vare p� b�ker lagret
		ArrayList<String> bokListe = new ArrayList<String>();
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
				//fil er tom, hent ut "feilmelding" fra ressursfil
				enLinje = res.getString(R.string.tomFil);
				//legg til "feilmelding" i arraylist
				bokListe.add(enLinje);
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
					bokListe.add(enLinje);
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
		return bokListe;
	} //lesFraFil

	/**
	 * Skriver til fil det som oversendt (det ArrayList<String> inneholder).
	 * Dersom fil ikke finnes, blir denne opprettet.
	 * @param context
	 * @param bokListe
	 */
	public static void skrivTilFil(Context context, ArrayList<String> bokListe) {
		try {
			//objekt av ressursklassen
			Resources res = context.getResources();
			//string som inneholder "feilmelding" (dersom intet er registrert)
			String intetRegistrert = res.getString(R.string.tomFil);
			//hent ut indeks for "feilmelding"
			//(vil ligge p� indeks:0 dersom n�v�rende registrering er f�rste tittel som registreres) 
			int indeksTomFil = bokListe.indexOf(intetRegistrert);
			//er feilmelding i array?
			if (indeksTomFil != -1) {
				//fjern feilmelding fra array				
				bokListe.remove(indeksTomFil);
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
			for (int i = 0; i < bokListe.size(); i++) {
				//skriv innhold til fil separert med newline
				outputStreamSkriver.write(bokListe.get(i) + '\n');
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