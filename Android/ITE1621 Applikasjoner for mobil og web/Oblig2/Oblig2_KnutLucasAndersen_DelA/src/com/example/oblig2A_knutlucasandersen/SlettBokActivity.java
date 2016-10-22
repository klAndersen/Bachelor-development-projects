package com.example.oblig2A_knutlucasandersen;

import java.util.ArrayList;

import android.app.Activity;
import android.os.Bundle;
import android.view.Menu;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.CheckedTextView;
import android.widget.ListView;
import android.widget.Toast;

/**
 * Denne klassen tar for seg sletting av registrerte boktitler.
 * Man kan velge mellom å slette en eller alle registrerte titler
 * @author Knut Lucas Andersen
 */
public class SlettBokActivity extends Activity {
	private ArrayList<String> bokListe;
	private ArrayAdapter<String> adapter;
	private CheckedTextView chkVelgAlle;
	private ListView lvBokTitler;	

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_slett_bok);
		//fyll listview med boktitler
		fyllListView();		
	} //onCreate

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.main, menu);
		return true;
	} //onCreateOptionsMenu

	private void fyllListView() {
		try {
			//finn listview via ressurs id
			lvBokTitler = (ListView) findViewById(R.id.lvBokTitler);
			//fyll arraylist med titler
			bokListe = Filbehandling.lesFraFil(this);
			//koble adapter og arraylist
			adapter = new ArrayAdapter<String>(this, android.R.layout.simple_list_item_multiple_choice, bokListe);
			//koble adapter til listview
			lvBokTitler.setAdapter(adapter);
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
			for (int i = 0; i < lvBokTitler.getCount(); i++) {
				//selekter/de-selekter tittel
				lvBokTitler.setItemChecked(i, hukAv);
			} //for
		} catch (Exception ex) {
			//en feil oppsto, vis feilmelding
			Toast.makeText(this, "Feil oppsto under selektering av titler:\n" + ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //avhukAlleTitler

	public void slettBoker(View v) {
		try {			
			//start bakerst fra arraylist og gå fremover for å fjerne fra arraylist
			//(bokListe.size() - 1) pga da stemmer størrelsen overens med antall elementer
			//EKS: hvis bokListe.size() = 3, - så går den først innom element 2, element 1, 
			//og tilslutt element 0
			for (int i = (bokListe.size() - 1); i > -1; i--) {
				//er gjeldende tittel selektert for sletting?
				if (lvBokTitler.isItemChecked(i)) {
					//fjern tittel fra arraylist
					bokListe.remove(i);					
				} //if (lvBokTitler.isItemChecked(i))
			} //for
			//skriv endringer til fil
			Filbehandling.skrivTilFil(this, bokListe);
			//oppdater adapter med resterende titler
			adapter = new ArrayAdapter<String>(this, android.R.layout.simple_list_item_multiple_choice, bokListe);
			//oppdater listview med resterende titler
			lvBokTitler.setAdapter(adapter);
		} catch (Exception ex) {
			//en feil oppsto, vis feilmelding
			Toast.makeText(this, "Feil oppsto under fylling av sletting av titler:\n" + ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //slettBoker
} //SlettBokActivity