package com.example.oblig2B_knutlucasandersen;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.widget.Toast;

/**
 * Dette er en Broadcasting klasse som fungerer som en mottaker.
 * Den har kun en metode; onReceive som viser en toast hver gang 
 * en ny bok tittel blir registrert.
 * @author Knut Lucas Andersen
 */
public class Kringkastningsmottaker extends BroadcastReceiver {

	@Override
	public void onReceive(Context context, Intent mottaker) {
		try {
			//opprett string som inneholder pakke - og aktivitetsnavn
			String pakkeNavn = context.getString(R.string.delB_package_name);
			pakkeNavn += context.getString(R.string.activity_ny_tittel);
			//opprett string som starter med "notification"
			String melding = context.getString(R.string.resultat_melding);
			//legg til melding som ble broadcastet
			melding += mottaker.getStringExtra(pakkeNavn);
			//vis melding til bruker
			Toast.makeText(context, melding, Toast.LENGTH_LONG).show();
		} catch (Exception ex) {
			//en feil oppsto, vis feilmelding
			Toast.makeText(context, "En feil oppsto under henting av melding:\n" + ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //onReceive
} //Kringkastningsmottaker