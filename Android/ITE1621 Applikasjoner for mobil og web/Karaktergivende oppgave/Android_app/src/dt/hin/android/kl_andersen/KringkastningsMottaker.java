package dt.hin.android.kl_andersen;

//android-import
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.widget.Toast;

/**
 * BroadcastReceiver som mottar meldinger om: <br />
 * - Endringer i innstillinger <br />
 * - Meldinger fra GCM (lesbare - vises via Toast) <br />
 * @author Knut Lucas Andersen
 */
public class KringkastningsMottaker extends BroadcastReceiver {
	/** Pakkenavn for at enhet er ferdig med å starte opp **/
	private final String BOOT_COMPLETED = "android.intent.action.BOOT_COMPLETED";

	@Override
	public void onReceive(Context context, Intent intent) {
		try {
			String lesbarMelding = context.getString(R.string.readable_message);
			if (intent.getStringExtra(lesbarMelding) != null) {
				String melding = intent.getStringExtra(lesbarMelding);
				Toast.makeText(context, melding, Toast.LENGTH_LONG).show();
			} else if(BOOT_COMPLETED.equals(intent.getAction())) {
				MainMapActivity.opprettTilkoblingTilGCM();
			} else {
				//opprett string som inneholder pakke - og aktivitetsnavn
				String pakkeNavn = context.getString(R.string.package_name);
				pakkeNavn += context.getString(R.string.intent_innstillinger_endret);
				//hent ut verdi som ble broadcastet
				int endretInnstilling = intent.getIntExtra(pakkeNavn, -1);
				oppdaterGrensesnitt(context, endretInnstilling);
			} //if (intent.getStringExtra(lesbarMelding) != null)
		} catch (NullPointerException ex) {
			ex.printStackTrace();
		} catch (Exception ex) {
			ex.printStackTrace();
			//en feil oppsto, vis feilmelding
			String feilmelding = context.getString(R.string.exception_bcr_messages);
			Toast.makeText(context, feilmelding + ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //onReceive

	/**
	 * Oppdaterer grensesnitt og/eller verdier og komponenter basert på 
	 * endringer gjort i InnstillingActivity
	 * @param context - Context
	 * @param endretInnstilling - int: Innstillingen som er endret
	 */
	private void oppdaterGrensesnitt(Context context, final int endretInnstilling) {
		switch (endretInnstilling) {
		case InnstillingsActivity.NYTT_ENHETSNAVN:
			String enhetsNavn = Filbehandling.getSharedPreferances(context).getString(Filbehandling.ENHETSNAVN_NOKKEL, "");
			long identifikator = Filbehandling.getSharedPreferances(context).getLong(Filbehandling.ENHETS_IDENTIFIKATOR, 0);
			if (identifikator > 0) {
				//oppdater navnet i databasen
				DatabaseFunksjoner.updateDatabaseEnhetsNavn(context, identifikator, enhetsNavn);
			} //if (identifikator > 0)
		case InnstillingsActivity.NY_OPPDATERING_I_MINUTT:
			MainMapActivity.oppdaterLocationListener();
			break;
		case InnstillingsActivity.NY_OPPDATERING_I_METER:
			MainMapActivity.oppdaterLocationListener();
			break;
		case InnstillingsActivity.NYTT_ZOOM_NIVAA:
			MainMapActivity.setZoomNivaa();
		default:
			break;
		} //switch
	} //oppdaterGrensesnitt
} //KringkastningsMottaker