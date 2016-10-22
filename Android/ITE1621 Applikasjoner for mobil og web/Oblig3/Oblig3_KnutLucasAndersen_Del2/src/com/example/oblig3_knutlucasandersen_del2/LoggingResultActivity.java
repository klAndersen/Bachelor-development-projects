package com.example.oblig3_knutlucasandersen_del2;

import android.app.Activity;
import android.content.ContentResolver;
import android.content.Intent;
import android.database.Cursor;
import android.net.Uri;
import android.os.Bundle;
import android.view.Menu;
import android.widget.TextView;
import android.widget.Toast;

/**
 * Dette er en sub-aktivitet som fremviser all logg registrert på valgt kategori. <br />
 * Dersom intet er loggført, vises en tekst i TextView om at intet er loggført.
 * @author Knut Lucas Andersen
 */
public class LoggingResultActivity extends Activity {

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_logger_result);
		fyllTextView();
	} //onCreate

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.logger_ui, menu);
		return true;
	} //onCreateOptionsMenu

	/**
	 * Denne metoden fyller opp en TextView med resultatene som returneres fra databasen. <br />
	 * Dersom intet er loggført eller at eksisterende logg er eldre enn 1 dag (24t), vil kun teksten <br />
	 * "Ingen hendelser er loggført." vises.
	 */
	private void fyllTextView() {
		try {
			//variabler som holder på tabellverdier
			String kategori, dato, tidspunkt, tekst, detaljer;
			//hent verdi fra strings.xml
			kategori = getString(R.string.LOGG_KATEGORI);			
			dato = getString(R.string.LOGG_DATO);
			tidspunkt = getString(R.string.LOGG_TIDSPUNKT);
			tekst = getString(R.string.LOGG_TEKST);
			detaljer = getString(R.string.LOGG_DETALJER);
			//opprett intent som skal hente ut valgt kategori
			Intent kategoriID = getIntent(); 
			//hent ut verdien fra intent, returnerer -1 hvis den feiler på å hente id
			int indeks = kategoriID.getIntExtra(LoggerUIActivity.ID_VALGT_KATEGORI, -1);
			//ble indeks hentet ut?
			if (indeks > -1) {
				//hent ut kategorien fra string-array og hent kun logger som er mindre enn en dag gammel (24t)
				String where = kategori + "='" + getResources().getStringArray(R.array.array_kategorier)[indeks] + "'"
						+ " and " + dato + ">= datetime('now','-1 day')";			
				//opprett objekt som inneholder resultatet av spørring mot databasen
				Cursor resultatCursor = hentResultaterFraDatabase(where);
				//ble et resultat fra databasen returnert og er det minst en rad i resultatet?
				if (resultatCursor != null && resultatCursor.getCount() > 0) {
					//opprett objekt av textview
					TextView tvResultat = (TextView)findViewById(R.id.tvResultat);
					//siden cursor har et resultat, legg kategorien inn i textview
					String loggInnhold = "Logg for " + getResources().getStringArray(R.array.array_kategorier)[indeks] + ":\n\n"; 
					//legg loggTittel i textview
					tvResultat.setText(loggInnhold);
					//hent kolonne ID for de forskjellige verdiene
					int KATEGORI_DATO = resultatCursor.getColumnIndexOrThrow(dato);
					int KATEGORI_TIDSPUNKT = resultatCursor.getColumnIndexOrThrow(tidspunkt);
					int KATEGORI_TEKST = resultatCursor.getColumnIndexOrThrow(tekst);
					int KATEGORI_DETALJER = resultatCursor.getColumnIndexOrThrow(detaljer);
					//blank ut variabel 
					loggInnhold = "";
					//loop gjennom innhold i cursor
					while (resultatCursor.moveToNext()) {
						//hent ut verdier fra cursor
						dato = resultatCursor.getString(KATEGORI_DATO);
						tidspunkt = resultatCursor.getString(KATEGORI_TIDSPUNKT);
						tekst = resultatCursor.getString(KATEGORI_TEKST);
						detaljer = resultatCursor.getString(KATEGORI_DETALJER);
						//hent innhold fra textview
						loggInnhold = tvResultat.getText().toString();
						//legg til neste del av loggen - avslutter string med dobbelt linjeskift for luft mellom loggene
						loggInnhold += "Dato: " + dato + "\nTidspunkt: " + tidspunkt + "\nDetalj:\n" + detaljer + "\nTekst:\n" + tekst + "\n\n";
						//legg loggen tilbake i textview
						tvResultat.setText(loggInnhold);
					} //while					
				} //if (resultatCursor != null)
				//lukk cursor
				resultatCursor.close();
			} //if (indeks > -1)
		} catch (Exception ex) {
			//en feil oppsto, vis feilmelding
			Toast.makeText(this, "En feil oppsto under fremvisning av loggen:\n" + ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //fyllTextView

	/**
	 * Funksjon som kjører spørring mot databasen ved bruk av ContentResolver. <br />
	 * Videre benyttes verdier fra strings.xml for uthenting av navn på kolonner i database.<br />
	 * Funksjonen returnerer en Cursor som inneholder resultatet av spørringen, <br /> 
	 * men hvis spørring feiler, så returneres null.
	 * @param where - String parameter som inneholder where klasul (sql-spørring)
	 * @return Cursor - Resultatet av spørringen eller null (hvis spørring feiler)
	 */
	private Cursor hentResultaterFraDatabase(String where) {
		Cursor resultatCursor = null;
		try {
			//variabler som holder på tabellverdier
			String dato, tidspunkt, tekst, detaljer, uriString;
			//hent verdi fra strings.xml
			dato = getString(R.string.LOGG_DATO);
			tidspunkt = getString(R.string.LOGG_TIDSPUNKT);
			tekst = getString(R.string.LOGG_TEKST);
			detaljer = getString(R.string.LOGG_DETALJER);
			//string som inneholder URI-string -> basert på uri fra del1
			//hentes ut fra strings.xml
			uriString = getString(R.string.URI_STRING);
			//opprett objekt av contentresolver
			ContentResolver cr = getContentResolver();
			//opprett array som inneholder verdiene som skal hentes ut
			String[] resultatArray = new String[] {
					dato, 
					tidspunkt,
					tekst, 
					detaljer
			};
			//"plassholdere"
			String[] whereArgs = null;
			String order = null;
			//opprett objekt av URI som inneholder stringen
			Uri loggUri = Uri.parse(uriString);
			//hent resultatet fra databasen
			resultatCursor = cr.query(loggUri, resultatArray, where, whereArgs, order);
		} catch (Exception ex) {
			//en feil oppsto, vis feilmelding
			Toast.makeText(this, "Feil oppsto under uthenting fra database:\n" + ex.getMessage(), Toast.LENGTH_LONG).show();			
		} //try/catch
		//returner resultatet
		return resultatCursor;
	} //hentResultaterFraDatabase
} //LoggingResultActivity