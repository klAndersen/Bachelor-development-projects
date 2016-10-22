package com.example.oblig3_knutlucasandersen_del1;

//java-import
import java.util.ArrayList;
//android-import
import android.app.Activity;
import android.os.Bundle;
import android.view.Menu;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.CheckedTextView;
import android.widget.ListView;
import android.widget.Toast;

/**
 * Dette er hovedklassen som blir vist når bruker starter applikasjonen. <br />
 * Det vises frem en CheckBox og ListView (med CheckBoxes) som gir  <br />
 * bruker muligheter til å velge hvilke kategorier som skal logges. <br />
 * Endringer skrives til fil med verdien true/false.
 * @author Knut Lucas Andersen
 */
public class InnstillingActivity extends Activity {
	//konstanter
	public final String LOGG_PREFERANSE = "SYSTEM_LOGG_KATEGORIER";
	//variabler
	private ArrayList<String> kategoriListe;
	private ArrayAdapter<String> adapter;
	private CheckedTextView chkVelgAlle;
	private ListView lvKategorier;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_logger);
		fyllListView();
	} //onCreate

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.logger, menu);
		return true;
	} //onCreateOptionsMenu

	/**
	 * Fyller ListView med eksisterende kategorier fra xml. <br />
	 * Deretter leses det inn fra fil hvilke kategorier bruker har 
	 * valgt skal logges. <br /> 
	 * Ved første gangs oppstart er alle selektert.
	 */
	private void fyllListView() {
		try {
			kategoriListe = new ArrayList<String>();
			//finn listview via ressurs id
			lvKategorier = (ListView) findViewById(R.id.lvVelgKategorier);
			//fyll array med kategorier
			String[] kategoriArray = getResources().getStringArray(R.array.array_kategorier);
			//koble adapter og arraylist
			adapter = new ArrayAdapter<String>(this, android.R.layout.simple_list_item_multiple_choice, kategoriListe);
			//loop gjennom kategoriene i array
			for (int i = 0; i < kategoriArray.length; i++) {
				//legg til verdien i arraylist
				kategoriListe.add(kategoriArray[i]);
				//oppdater adapter
				adapter.notifyDataSetChanged();
			} //for			
			//koble adapter til listview
			lvKategorier.setAdapter(adapter);
			//opprett en arraylist som inneholder verdiene for hvilke hendelser som skal logges
			ArrayList<Boolean> loggListe = Filbehandling.lesFraFil(this);
			//loop gjennom listview
			for (int i = 0; i < loggListe.size(); i++) {
				//sett kategori til verdi basert på hva som er tidligere registrert
				lvKategorier.setItemChecked(i, loggListe.get(i));
			} //for
		} catch (Exception ex) {
			Toast.makeText(this, "Feil oppsto under fylling av ListView:\n" + ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //fyllListView

	public void avhukAlleTitler(View v) {
		try {
			//finn checkbox via ressurs id
			chkVelgAlle = (CheckedTextView) findViewById(R.id.chktvVelgAlle);
			//skift tilstand på checkbox
			chkVelgAlle.toggle();
			//hent ut tilstanden til checkbox (checked/unchecked)
			boolean hukAv = chkVelgAlle.isChecked();
			//loop gjennom alle titler i listview
			for (int i = 0; i < lvKategorier.getCount(); i++) {
				//selekter/de-selekter tittel
				lvKategorier.setItemChecked(i, hukAv);
			} //for
		} catch (Exception ex) {
			//en feil oppsto, vis feilmelding
			Toast.makeText(this, "En feil oppsto:\n" + ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //avhukAlleTitler

	public void lagreEndringer(View v) {
		try {
			//opprett arraylist som skal inneholde valgte kategoerier som skal logges
			ArrayList<Boolean> loggListe = new ArrayList<Boolean>();
			//loop gjennom listviews kategorier
			for (int i = 0; i < lvKategorier.getCount(); i++) {
				//legg til verdien i arraylist
				loggListe.add(lvKategorier.isItemChecked(i));
			} //for
			//skriv verdier til fil
			Filbehandling.skrivTilFil(this, loggListe);
		} catch (Exception ex) {
			//en feil oppsto, vis feilmelding
			Toast.makeText(this, "En feil oppsto:\n" + ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //lagreEndringer
} //InnstillingActivity