package com.example.oblig4_knutlucasandersen;

//java-import
import java.util.ArrayList;
//android-import
import android.app.Activity;
import android.app.FragmentManager;
import android.content.Intent;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.ArrayAdapter;

/**
 * Sub-aktivitet av klassen OvervaakingActivity. <br />
 * Denne kaller på et listfragment (OvervaakningsListeFragment) <br />
 * som fremviser en liste over målestasjoner og temperaturer <br />
 * eller viser en liste over linker til yr.no. <br />
 * Grunnen til dette kan du se her: <a href="http://om.yr.no/verdata/vilkar/">Yr.no Vilkår</a>
 * @author Knut Lucas Andersen
 */
public class OvervaakningsListeActivity extends Activity {
	public final static int VIS_TEMPERATUR_LISTE = 0;
	public final static int VIS_LINK_LISTE = 1;
	public final static int LEGG_TIL_NY_MAALESTASJON = 2; 

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_overvaakingsliste);
		//opprett fragment
		opprettFragment();
	} //onCreate

	private void opprettFragment() {
		//opprett referanse til fragment
		FragmentManager fm = getFragmentManager();
		//opprett et objekt av fragmentet som viser listen
		OvervaakningsListeFragment listeFragment = (OvervaakningsListeFragment)fm.findFragmentById(R.id.OvervaakningsListeFragment);
		//opprett intent for å hente ut melding
		Intent intent = getIntent();
		//hent verdien som meldingen inneholder
		int listeSomSkalVises = intent.getIntExtra(OvervaakingActivity.LISTE_SOM_SKAL_VISES, 0);
		switch (listeSomSkalVises) {
		case VIS_TEMPERATUR_LISTE:
			opprettAdapterForTemperatur(listeFragment);
			break;
		case VIS_LINK_LISTE:
			opprettAdapterForLinkVisning(listeFragment);
			break;
		case LEGG_TIL_NY_MAALESTASJON:
			opprettAdapterForAaLeggeTilNyMaalestasjon(listeFragment);
			break;
		} //switch
	} //opprettFragment

	private void opprettAdapterForTemperatur(OvervaakningsListeFragment listeFragment) {
		//hent ut målestasjoner fra fil
		ArrayList<Maalestasjon> visningsListe = Filbehandling.lesFraFil(this, true);
		//opprett en arrayadapter som kobler sammen arraylist og listfragment
		ArrayAdapter<Maalestasjon> adapter = new ArrayAdapter<Maalestasjon>(this, android.R.layout.simple_list_item_1, visningsListe);
		//meld ifra om at data har blitt lagt til
		adapter.notifyDataSetChanged();
		//koble adapter til listfragment
		listeFragment.setListAdapter(adapter);
	} //opprettAdapterForTemperatur

	private void opprettAdapterForLinkVisning(OvervaakningsListeFragment listeFragment) {
		ArrayList<Maalestasjon> ms = Filbehandling.lesFraFil(this, false);
		ArrayList<String> linkListe = new ArrayList<String>();
		linkListe.add(getString(R.string.tvRequiredText));
		//loop gjennom brukers valgte og hent ut URL
		for (int i = 0; i < ms.size(); i++) {
			linkListe.add(ms.get(i).getMaalestasjonURL());
		} //for
		//opprett en arrayadapter som kobler sammen arraylist og listfragment
		ArrayAdapter<String> adapter = new ArrayAdapter<String>(this, android.R.layout.simple_list_item_1, linkListe);
		//meld ifra om at data har blitt lagt til
		adapter.notifyDataSetChanged();
		//koble adapter til listfragment
		listeFragment.setListAdapter(adapter);
	} //opprettAdapterForLinkVisning

	private void opprettAdapterForAaLeggeTilNyMaalestasjon(OvervaakningsListeFragment listeFragment) {
		ArrayList<Maalestasjon> ms = Filbehandling.lesFraRawFil(this);
		ArrayList<String> stedsListe = new ArrayList<String>();
		//loop gjennom brukers valgte og hent ut URL
		for (int i = 0; i < ms.size(); i++) {
			stedsListe.add(ms.get(i).getStedsNavn());
		} //for
		//opprett en arrayadapter som kobler sammen arraylist og listfragment
		ArrayAdapter<String> adapter = new ArrayAdapter<String>(this, android.R.layout.simple_list_item_1, stedsListe);
		//meld ifra om at data har blitt lagt til
		adapter.notifyDataSetChanged();
		//koble adapter til listfragment
		listeFragment.setListAdapter(adapter);
	} //opprettAdapterForAaLeggeTilNyMaalestasjon

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
} //OvervaakningsListeActivity