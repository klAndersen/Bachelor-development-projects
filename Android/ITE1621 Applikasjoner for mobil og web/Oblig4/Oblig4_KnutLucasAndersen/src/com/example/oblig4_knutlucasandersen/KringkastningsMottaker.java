package com.example.oblig4_knutlucasandersen;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.widget.Toast;

/**
 * BroadcastReceiver klasse som registrer når enhet er slått på. <br />
 * Når enhet er slått på, så startes TemperaturService.<br />
 * TemperaturService er servicen som tar for seg nedlasting av værdata.
 * @author Knut Lucas Andersen
 */
public class KringkastningsMottaker extends BroadcastReceiver {

	@Override
	public void onReceive(Context context, Intent intent) {
		try {			
			//opprett intent for starting av service
			intent = new Intent(context, TemperaturService.class);
			//start service
			context.startService(intent);
		} catch(Exception ex) {
			//hent ut ledetekst til feilmelding
			String feilmelding = context.getResources().getString(R.string.uknown_error_exception);
			//vis feilmelding til bruker
			Toast.makeText(context, feilmelding + ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //onReceive
} //KringkastningsMottaker