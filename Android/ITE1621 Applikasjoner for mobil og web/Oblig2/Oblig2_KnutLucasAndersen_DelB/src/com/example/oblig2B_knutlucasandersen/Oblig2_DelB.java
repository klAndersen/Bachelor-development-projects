package com.example.oblig2B_knutlucasandersen;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.Menu;
import android.view.View;
import android.widget.TextView;
import android.widget.Toast;

/**
 * Dette er hovedklassen som benytter seg av aktiviteter fra 
 * Oblig2_KnutLucasAndersen_DelA.
 * Aktivitetene som kalles herfra er boklisten og muligheten 
 * for å registrere en ny tittel. 
 * @author Knut Lucas Andersen
 */
public class Oblig2_DelB extends Activity {
	private final int DEL_B_REQUEST_TITLE = 1001;

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
			//variabler for pakke - og aktivitet som skal startes			
			String eksternPakkeNavn = getString(R.string.delA_package_name);
			String aktivitet = eksternPakkeNavn + getString(R.string.activity_vis_tittel_liste);
			//opprett intent som kaller på bokliste aktiviteten
			Intent intent = new Intent(aktivitet);
			//start aktiviteten			
			this.startActivityForResult(intent, DEL_B_REQUEST_TITLE);
		} catch (Exception ex) {
			//en feil oppsto, vis feilmelding
			Toast.makeText(this, ex.getMessage(), Toast.LENGTH_LONG).show();			
		} //try/catch
	} //visListe

	@Override
	public void onActivityResult(int requestCode, int resultCode, Intent data) {
		super.onActivityResult(requestCode, resultCode, data);
		try {
			//hvilken aktivitet kalte på resultatet?
			switch(requestCode) {
			//ble tittel fra bokliste etterspurt?
			case DEL_B_REQUEST_TITLE: 
				//opprett et objekt av TextView
				TextView tvValgtTittel = (TextView) findViewById(R.id.tvValgtTittel);
				//ble resultat oversendt?
				if (resultCode == Activity.RESULT_OK) {
					//ble data oversendt?
					if (data!= null) {
						//hent ut resultatet
						String pakkeNavn = getString(R.string.delA_package_name);
						pakkeNavn += getString(R.string.activity_vis_tittel_liste);
						//opprett string som inneholder resultatet
						String resultat = data.getStringExtra(pakkeNavn);
						//ble et resultat hentet ut?
						if (!resultat.equals(null)) {
							//oppretter stringens innhold på nytt ved å legge til tekst foran for bedre visning
							//dette må gjøres her for hvis ikke vil if-test feile siden stringen vil inneholde
							//teksten som står foran resultatet
							resultat = getString(R.string.tvReturnertTittel) + data.getStringExtra(pakkeNavn);
							tvValgtTittel.setText(resultat);
						} //if (!result.equals(null)) 
					} //if (data!= null)
				} //if (resultCode == Activity.RESULT_OK)
				break;
			} //switch(requestCode)
		} catch (Exception ex) {
			Toast.makeText(this, "Kunne ikke hente ut tittel. Feilen som oppsto var:\n" + ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //onActivityResult


	public void registrerNyBok(View v) {
		try {
			//variabler for pakke - og aktivitet som skal startes
			String eksternPakkeNavn = getString(R.string.delA_package_name);
			String aktivitet = eksternPakkeNavn + getString(R.string.activity_ny_tittel);
			//opprett intent som kaller på aktiviteten for registrering av ny bok
			Intent intent = new Intent(aktivitet);
			//start aktiviteten
			this.startActivity(intent);
		} catch (Exception ex) {
			//en feil oppsto, vis feilmelding
			Toast.makeText(this, ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //registrerNyBok
} //Oblig2_DelB