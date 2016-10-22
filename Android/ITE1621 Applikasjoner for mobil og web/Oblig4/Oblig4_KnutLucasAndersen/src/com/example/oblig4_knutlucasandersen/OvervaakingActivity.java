package com.example.oblig4_knutlucasandersen;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Toast;

/**
 * Dette er hovedaktiviteteten til applikasjonen. <br />
 * Den fremviser to Buttons og en TextView, og kaller 
 * på sub-aktiviteten OvervaakningsListeActivity <br /> 
 * som viser en liste basert på brukers valg.
 * @author Knut Lucas Andersen
 */
public class OvervaakingActivity extends Activity {
	public final static String LISTE_SOM_SKAL_VISES = "com.example.oblig4_knutlucasandersen.VALGT_LISTE";

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_overvaaking);
	} //onCreate

	/******************************* MENYVALG FOR BRUKER *******************************/
	public void visTemperaturListe(View v) {
		try {
			//forsøk å opprette melding (intent)
			Intent intent = new Intent(this, OvervaakningsListeActivity.class);
			//legg ved id for temperaturliste (listen som skal vises) 
			intent.putExtra(LISTE_SOM_SKAL_VISES, OvervaakningsListeActivity.VIS_TEMPERATUR_LISTE);
			//start aktiviteten
			startActivity(intent);
		} catch (Exception ex) {
			//hent ut ledetekst til feilmelding
			String feilmelding = getString(R.string.uknown_error_exception);
			//vis feilmelding til bruker
			Toast.makeText(this, feilmelding + ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //visTemperaturListe

	public void visLinkListe(View v) {
		try {
			//forsøk å opprette melding (intent)
			Intent intent = new Intent(this, OvervaakningsListeActivity.class);
			//legg ved id for linkliste (listen som skal vises) 
			intent.putExtra(LISTE_SOM_SKAL_VISES, OvervaakningsListeActivity.VIS_LINK_LISTE);
			//start aktiviteten
			startActivity(intent);
		} catch (Exception ex) {
			//hent ut ledetekst til feilmelding
			String feilmelding = getString(R.string.uknown_error_exception);
			//vis feilmelding til bruker
			Toast.makeText(this, feilmelding + ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //visLinkListe

	/******************************* ACTIONBAR *******************************/
	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.overvaaking, menu);
		return true;
	} //onCreateOptionsMenu

	@Override
	public boolean onPrepareOptionsMenu(Menu menu) {
		super.onPrepareOptionsMenu(menu);	    
		ActionBarFunksjoner.onPrepareOptionsMenu(this, menu);
		return true;
	} //onPrepareOptionsMenu

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		//hent ut valgt element
		boolean resultat = ActionBarFunksjoner.onOptionsItemSelected(this, item);
		if (!resultat) {
			//intet element ble valgt, bruk arv
			super.onOptionsItemSelected(item);
		} //if (!resultat)
		return resultat;
	} //onOptionsItemSelected

	/******************************* MÅLESTASJON ****************************/
	public void leggTilMaalestasjon(MenuItem item) {
		ActionBarFunksjoner.leggTilMaalestasjon(this);
	} //leggTilMaalestasjon

	public void fjernMaalestasjon(MenuItem item) {
		ActionBarFunksjoner.fjernMaalestasjon(this);
	} //fjernMaalestasjon

	/******************************* SERVICE *******************************/
	public void startService(MenuItem item) {
		ActionBarFunksjoner.startService(this);
	} //startService

	public void stopService(MenuItem item) {
		ActionBarFunksjoner.stopService(this);
	} //stopService

	/******************************* TEMPERATURGRENSER  *******************************/
	public void settOvreTemperaturGrense(MenuItem item) {
		ActionBarFunksjoner.settOvreTemperaturGrense(this);
	} //settOvreTemperaturGrense

	public void settNedreTemperaturGrense(MenuItem item) {
		ActionBarFunksjoner.settNedreTemperaturGrense(this);
	} //settNedreTemperaturGrense
} //OvervaakingActivity