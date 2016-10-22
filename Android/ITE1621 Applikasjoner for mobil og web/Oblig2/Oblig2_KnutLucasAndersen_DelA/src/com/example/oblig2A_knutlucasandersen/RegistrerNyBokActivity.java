package com.example.oblig2A_knutlucasandersen;

import java.util.ArrayList;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.Menu;
import android.view.View;
import android.widget.EditText;
import android.widget.Toast;

/**
 * Denne klassen tar for seg registrering av nye boktitler.
 * @author Knut Lucas Andersen
 */
public class RegistrerNyBokActivity extends Activity {

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_registrer_ny_bok);
	} //onCreate

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.main, menu);
		return true;
	} //onCreateOptionsMenu

	public void registrerNyTittel(View v) {
		try {
			//opprett en arraylist som leser inn eksisterende innhold fra fil
			ArrayList<String> bokListe = Filbehandling.lesFraFil(this);
			//opprett objekt av EditText som inneholder boktittel
			EditText inputBoktittel = (EditText) findViewById(R.id.inputBoktittel);
			//hent ut tittelen
			String tittel = inputBoktittel.getText().toString();
			//er en tittel innskreven?
			if (!tittel.equals("")) {
				//legg til gjeldende boktittel i arraylist
				bokListe.add(tittel);
				//skriv arraylist's innhold til fil
				Filbehandling.skrivTilFil(this, bokListe);
				//gi tilbakemelding til bruker
				Toast.makeText(this, "Tittelen '" + tittel + "' ble lagret." , Toast.LENGTH_LONG).show();
				inputBoktittel.setText("");
				opprettKringkastning(tittel);
			} //if (!tittel.equals("")) 
		} catch (Exception ex) {
			Toast.makeText(this, "Feil oppsto under registrering:\n" + ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //registrerNyTittel

	private void opprettKringkastning(String tittel) {
		//opprett string som inneholder pakke - og aktivitetsnavn
		String pakkeNavn = getString(R.string.delB_package_name);
		pakkeNavn += getString(R.string.activity_ny_tittel);
		//opprett intent som skal videresende broadcastet
		Intent kringkastning = new Intent();
		kringkastning.setAction(pakkeNavn);
		//legg ved pakkenavn og tittel på boken som ble opprettet
		kringkastning.putExtra(pakkeNavn, tittel);
		//skal inkludere pakker som er stoppet 
		//kilde: http://stackoverflow.com/questions/9783704/broadcast-receiver-onreceive-never-called
		kringkastning.addFlags(Intent.FLAG_INCLUDE_STOPPED_PACKAGES);
		//send broadcastet		
		sendBroadcast(kringkastning);
	} //opprettKringkastning
} //RegistrerNyBokActivity