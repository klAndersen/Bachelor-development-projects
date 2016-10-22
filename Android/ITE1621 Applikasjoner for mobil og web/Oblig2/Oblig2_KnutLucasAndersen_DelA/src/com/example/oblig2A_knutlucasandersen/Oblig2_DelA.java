package com.example.oblig2A_knutlucasandersen;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.Menu;
import android.view.View;
import android.widget.Toast;

/**
 * Hovedklassen som starter aktiviteten. Inneholder en meny 
 * bestående av knapper som starter sub-aktiviteter
 * @author Knut Lucas Andersen
 */
public class Oblig2_DelA extends Activity {

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_main);		
	} //onCreate

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.main, menu);
		return true;
	} //onCreateOptionsMenu

	public void visListe(View v) {
		try {
			//opprett intent som kaller på bokliste aktiviteten
			Intent intent = new Intent(this, BokListeActivity.class);
			//start aktiviteten
			startActivity(intent);
		} catch (Exception ex) {
			//en feil oppsto, vis feilmelding
			Toast.makeText(this, ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //visListe

	public void registrerNyBok(View v) {
		try {
			//opprett intent som kaller på registrering av ny bok aktiviteten
			Intent intent = new Intent(this, RegistrerNyBokActivity.class);
			//start aktiviteten
			startActivity(intent);
		} catch (Exception ex) {
			//en feil oppsto, vis feilmelding
			Toast.makeText(this, ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //registrerNyBok

	public void slettBok(View v) {
		try {
			//opprett intent som kaller på slettbok aktiviteten
			Intent intent = new Intent(this, SlettBokActivity.class);
			//start aktiviteten
			startActivity(intent);
		} catch (Exception ex) {
			//en feil oppsto, vis feilmelding
			Toast.makeText(this, ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //slettBok
} //Oblig2_DelA