package com.example.oblig2A_knutlucasandersen;

import java.util.ArrayList;

import android.app.Activity;
import android.app.FragmentManager;
import android.os.Bundle;
import android.view.Menu;
import android.widget.ArrayAdapter;
import android.widget.Toast;

/**
 * Denne klassen viser en liste over alle registrerte boktitler.
 * Avhengig/benytter seg av BokListeFragment klassen.
 * @author Knut Lucas Andersen
 */
public class BokListeActivity extends Activity {

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_bokliste);
		try {
			//referanse til fragmentet
			FragmentManager fm = getFragmentManager();
			//opprett objekt av klassen som skal inneholde listen
			BokListeFragment listFragment = (BokListeFragment)fm.findFragmentById(R.id.BokListeFragment);
			//arraylist som inneholder bøkene
			ArrayList<String> bokListe = Filbehandling.lesFraFil(this);
			//opprett arrayadapter som kobler sammen arraylist og listfragment
			ArrayAdapter<String> adapter = new ArrayAdapter<String>(this, android.R.layout.simple_list_item_1, bokListe);
			//fortell adapter at bøker har blitt lagt til
			adapter.notifyDataSetChanged();
			//legg til adapter til listfragment slik at den kan vise bøkene
			listFragment.setListAdapter(adapter);
		} catch (Exception ex) {
			Toast.makeText(this, "En feil oppsto:\n" + ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //onCreate

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.main, menu);
		return true;
	} //onCreateOptionsMenu
} //BokListeActivity