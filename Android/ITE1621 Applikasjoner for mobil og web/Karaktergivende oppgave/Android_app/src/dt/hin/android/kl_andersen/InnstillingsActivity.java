package dt.hin.android.kl_andersen;

//android-import
import android.app.Activity;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemSelectedListener;
import android.widget.ArrayAdapter;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

/**
 * Sub-aktivitet som viser innstillinger. <br />
 * Innstillinger tilgjengelig her er: <br />
 * - Setting av enhetsnavn (navn som vises på kart)
 * - Setting av hvor ofte posisjonsdata skal sendes (i minutter) <br />
 * - Setting av hvor ofte posisjonsdata skal sendes (i meter) <br />
 * - Setting av hvilken farge brukers rute skal vises i på kartet <br />
 * - Setting av default zoom nivå på kartet <br />
 * - Setting av om posisjonsdata skal mottas etter applikasjon er avsluttet <br />
 * - Setting av om egne posisjonsdata (ruter) skal lagres <br />
 * - Setting av om andres posisjonsdata (ruter) skal lagres <br />
 * - Setting av om til/frakoblingsmeldinger skal vises ved oppstart og avslutning <br />
 * @author KnutLucas Andersen
 */
public class InnstillingsActivity extends Activity {
	/** Nytt enhetsnavn er opprettet **/
	public static final int NYTT_ENHETSNAVN = 100;
	/** Ny oppdatering for sending av posisjonsdata i minutter er satt **/
	public static final int NY_OPPDATERING_I_MINUTT = 101;
	/** Ny oppdatering for sending av posisjonsdata i meter er satt **/
	public static final int NY_OPPDATERING_I_METER = 102;
	/** Ny verdi for default zoom nivå er satt **/
	public static final int NYTT_ZOOM_NIVAA = 103;
	/** Høyeste verdi for zoom nivå **/
	private static final int MAX_ZOOM = 21;
	private static SharedPreferences deltPreferanse;
	//gui-komponenter
	private Spinner spinnerFarge;
	private Spinner spinnerZoom;
	private TextView tvInputMeter;
	private TextView tvInputMinutt;
	private TextView tvMinEnhetsnavn;
	private CheckBox chkLoggMeldinger;
	private CheckBox chkMottaPosisjonsdata;
	private CheckBox chkLagreMinePosisjonsdata;
	private CheckBox chkLagreAndresPosisjonsdata;
	private CheckBox chkVisNotifications;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		try {
			setContentView(R.layout.activity_innstilling);
			//opprett objekt av sharedpreferences
			deltPreferanse = Filbehandling.getSharedPreferances(this);
			settVerdierTextView();
			settVerdierCheckbox();
			settVerdieFargeSpinner();
			settVerdiZoomSpinner();
		} catch (RuntimeException ex) {
			//denne feilen dukker opp f.eks. hvis den feiler på å opprette grensesnitt
			String feilmelding = getString(R.string.runtime_error_exception);;
			Toast.makeText(this, feilmelding + ex.getMessage(), Toast.LENGTH_LONG).show();
		} catch (Exception ex) {
			String feilmelding = getString(R.string.unknown_error_exception);
			Toast.makeText(this, feilmelding + ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //onCreate

	private void settVerdierTextView() {
		//hent ut minutt fra sharedpreferences
		float defaultMinutt = getResources().getInteger(R.integer.default_minutt);		
		float minutt = deltPreferanse.getFloat(Filbehandling.OPPDATERINGSINTERVALL_MINUTT_NOKKEL, defaultMinutt);
		tvInputMinutt = (TextView) findViewById(R.id.tvInputMinutt); 
		tvInputMinutt.setText(minutt + getString(R.string.tvInputMinutt));
		//hent ut meter fra sharedpreferences
		float defaultMeter = getResources().getInteger(R.integer.default_meter);
		float meter = deltPreferanse.getFloat(Filbehandling.OPPDATERINGSINTERVALL_METER_NOKKEL, defaultMeter);
		tvInputMeter = (TextView) findViewById(R.id.tvInputMeter);
		tvInputMeter.setText(meter + getString(R.string.tvInputMeter));
		//hent ut enhetsnavnet fra sharedpreferences
		String defValue = getString(R.string.default_enhet_navn);
		String navn = deltPreferanse.getString(Filbehandling.ENHETSNAVN_NOKKEL, defValue);
		tvMinEnhetsnavn = (TextView) findViewById(R.id.tvMinEnhetsnavn);
		tvMinEnhetsnavn.setText(navn);
	} //settVerdierTextView

	private void settVerdieFargeSpinner() {
		//hent ut indeks for valgt element og sett valgt element i spinner
		int defaultVerdi = 0;
		int indeks = deltPreferanse.getInt(Filbehandling.RUTEFARGE_NOKKEL, defaultVerdi);
		spinnerFarge = (Spinner) findViewById(R.id.spinnerFarge);
		spinnerFarge.setSelection(indeks);
		//opprett lytter
		spinnerFarge.setOnItemSelectedListener(new OnItemSelectedListener() {
			@Override
			public void onItemSelected(AdapterView<?> arg0, View view, int position, long id) {
				//hent ut indeks for valgt element og lagre indeks i sharedpreferences
				int item = spinnerFarge.getSelectedItemPosition();
				Filbehandling.lagreEnhetsfarge(getBaseContext(), item);
			} //onItemSelected
			@Override
			public void onNothingSelected(AdapterView<?> arg0) { 
				//siden intet er valgt, ikke gjør noe
			} //onNothingSelected
		}); //setOnItemSelectedListener
	} //settVerdieFargeSpinner

	private void settVerdiZoomSpinner() {
		//opprett og fyll en array med verdiene som skal vises i spinner
		String[] zoomArray = new String[MAX_ZOOM]; 
		for (int i = 0; i < MAX_ZOOM; i++) {
			zoomArray[i] = "" + (i + 1);
		} //for
		//hent default verdi og finn spinner fra layout
		int defaultVerdi = this.getResources().getInteger(R.integer.default_zoom);
		int indeks = deltPreferanse.getInt(Filbehandling.ZOOM_VERDI_NOKKEL, defaultVerdi);
		//siden verdien som ligger i sharedpreferences er den faktiske zoomverdien, trekk fra 1
		indeks--;
		spinnerZoom = (Spinner) findViewById(R.id.spinnerZoom);
		//koble spinner og array via adapter, og opprett en lytter til spinneren
		ArrayAdapter<String> adapter = new ArrayAdapter<String>(this, android.R.layout.simple_spinner_item, zoomArray);
		spinnerZoom.setAdapter(adapter);
		spinnerZoom.setSelection(indeks);
		spinnerZoom.setOnItemSelectedListener(new OnItemSelectedListener() {
			@Override
			public void onItemSelected(AdapterView<?> arg0, View view, int position, long id) {
				//hent ut indeks for valgt element og lagre verdi i sharedpreferences
				int item = spinnerZoom.getSelectedItemPosition();
				int verdi = Integer.parseInt(spinnerZoom.getItemAtPosition(item).toString());
				Filbehandling.lagreDefaultZoomVerdi(getBaseContext(), verdi);
				opprettKringkastning(NYTT_ZOOM_NIVAA);
			} //onItemSelected
			@Override
			public void onNothingSelected(AdapterView<?> arg0) { 
				//siden intet er valgt, ikke gjør noe
			} //onNothingSelected
		}); //setOnItemSelectedListener
	} //settVerdiZoomSpinner

	private void settVerdierCheckbox() {
		//checkbox for om bruker vil motta posisjonsdata etter applikasjon avslutter
		boolean hukAv = deltPreferanse.getBoolean(Filbehandling.MOTTA_POSISJONSDATA_ETTER_AVSLUTNING_NOKKEL, false);
		chkMottaPosisjonsdata = (CheckBox) findViewById(R.id.chkMottaPosisjonsdata);
		chkMottaPosisjonsdata.setChecked(hukAv);
		//checkbox for lagring av egen rute
		hukAv = deltPreferanse.getBoolean(Filbehandling.LAGRE_EGEN_RUTE_NOKKEL, false);
		chkLagreMinePosisjonsdata = (CheckBox) findViewById(R.id.chkLagreMinePosisjonsdata);
		chkLagreMinePosisjonsdata.setChecked(hukAv);
		//checkbox for lagring av andres rute
		hukAv = deltPreferanse.getBoolean(Filbehandling.LAGRE_ANDRES_RUTE_NOKKEL, false);
		chkLagreAndresPosisjonsdata = (CheckBox) findViewById(R.id.chkLagreAndresPosisjonsdata);
		chkLagreAndresPosisjonsdata.setChecked(hukAv);
		//checkbox for varsling om endringer
		hukAv = deltPreferanse.getBoolean(Filbehandling.VIS_NOTIFICATIONS_NOKKEL, true);
		chkVisNotifications = (CheckBox) findViewById(R.id.chkVisNotifications);
		chkVisNotifications.setChecked(hukAv);
		//checkbox for lagring av meldinger (logg)
		hukAv = deltPreferanse.getBoolean(Filbehandling.LOGG_MELDINGER_NOKKEL, false);
		chkLoggMeldinger = (CheckBox) findViewById(R.id.chkLoggMeldinger);
		chkLoggMeldinger.setChecked(hukAv);
	} //settVerdierCheckbox

	public void endrePosisjonsoppdateringMinutt(View v) {
		//tekstverdier som skal vises i inputboks
		String tittel = getString(R.string.inputboks_minutt_oppdatering_tittel);
		String melding = getString(R.string.inputboks_minutt_oppdatering_melding);
		String posButton = getString(R.string.inputboks_minutt_oppdatering_posButton);
		String negButton = getString(R.string.btnAvbryt);
		//vis inputboks, - dersom nytt navn blir skrivd inn, blir textview oppdatert 
		visInputDialog(tittel, melding, posButton, negButton, NY_OPPDATERING_I_MINUTT);
	} //endrePosisjonsoppdateringMinutt

	public void endrePosisjonsoppdateringMeter(View v) {
		//tekstverdier som skal vises i inputboks
		String tittel = getString(R.string.inputboks_meter_oppdatering_tittel);
		String melding = getString(R.string.inputboks_meter_oppdatering_melding);
		String posButton = getString(R.string.inputboks_meter_oppdatering_posButton);
		String negButton = getString(R.string.btnAvbryt);
		//vis inputboks, - dersom nytt navn blir skrivd inn, blir textview oppdatert 
		visInputDialog(tittel, melding, posButton, negButton, NY_OPPDATERING_I_METER);
	} //endrePosisjonsoppdateringMeter

	public void endreMinEnhetsNavn(View v) {
		try {
			//tekstverdier som skal vises i inputboks
			String tittel = getString(R.string.inputboks_enhetsnavn_tittel);
			String melding = getString(R.string.inputboks_enhetsnavn_melding);
			String posButton = getString(R.string.inputboks_enhetsnavn_posButton);
			String negButton = getString(R.string.btnAvbryt);
			//vis inputboks, - dersom nytt navn blir skrivd inn, blir textview oppdatert 
			visInputDialog(tittel, melding, posButton, negButton, NYTT_ENHETSNAVN);
		} catch (Exception ex) {
			String feilmelding = getString(R.string.unknown_error_exception);
			Toast.makeText(this, feilmelding + ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //endreMinEnhetsNavn

	public void lagreMottakAvPosisjonsdata(View v) {
		boolean status = chkMottaPosisjonsdata.isChecked();
		Filbehandling.lagreMottakAvPosisjonsdata(this, status);
	} //lagreMottakAvPosisjonsdata

	public void lagreMinPosisjon(View v) {
		boolean status = chkLagreMinePosisjonsdata.isChecked();
		Filbehandling.lagreLoggForEgenRute(this, status);
	} //lagreMinPosisjon

	public void lagreAndresPosisjon(View v) {
		boolean status = chkLagreAndresPosisjonsdata.isChecked();
		Filbehandling.lagreLoggForAndresRute(this, status);
	} //lagreAndresPosisjon
	
	public void lagreValgVisningAvNotifications(View v) {
		boolean status = chkVisNotifications.isChecked();
		Filbehandling.lagreValgVisningAvNotifications(this, status);
	} //lagreValgVisningAvNotifications

	public void lagreValgLoggingAvMeldinger(View v) {
		boolean status = chkLoggMeldinger.isChecked();
		Filbehandling.lagreValgLoggingAvMeldinger(this, status);
	} //lagreValgLoggingAvMeldinger

	/**
	 * Oppretter en Dialog av typen AlertDialog. Hensikten er å fremvise en InputBoks 
	 * som ber bruker om input. Hva bruker blir bedt om avhenger av oversendt tittel, 
	 * melding og id. Input blir deretter oversendt til metode basert på oversendt id.
	 * @param tittel - Tittel på inputboks
	 * @param melding - Beskjed som forteller bruker hva som skal skrives inn
	 * @param posButton - Tekst på positiv knapp ("OK")
	 * @param negButton - Tekst på negativ knapp ("Cancel")
	 * @param id - Identifikator for hva som skal gjøres med input
	 */
	private void visInputDialog(String tittel, String melding, String posButton, String negButton, final int id) {		
		//opprett et Dialog objekt av typen AlertDialog
		AlertDialog.Builder inputBoks = new AlertDialog.Builder(this);
		//sett tittel og melding som skal vises
		inputBoks.setTitle(tittel);
		inputBoks.setMessage(melding);
		//opprett et tekstfelt for brukers input
		final EditText inputFelt = new EditText(this);
		//knytt tekstfelt til inputboksen
		inputBoks.setView(inputFelt);
		//opprett lytter til OK knappen
		inputBoks.setPositiveButton(posButton, new DialogInterface.OnClickListener() {
			@Override
			public void onClick(DialogInterface dialog, int whichButton) {
				//hent ut verdien som bruker skrev inn
				String input = inputFelt.getText().toString();
				if (!input.equals("")) {
					behandleInput(id, input);
				} //if (!input.equals(""))
			} //onClick
		});
		//opprett lytter til CANCEL knappen
		inputBoks.setNegativeButton(negButton, new DialogInterface.OnClickListener() {
			@Override
			public void onClick(DialogInterface dialog, int whichButton) {
				//input ble avbrutt
			} //onClick
		});
		//vis inputboks til bruker
		inputBoks.show();
	} //visInputDialog

	/**
	 * Behandler input basert på hva bruker skrev inn og hva som id tilsier 
	 * at skal gjøres med oversendt input.
	 * @param id - Identifikator for hva som skal gjøres med input
	 * @param input - Tekst skrivd inn fra bruker i AlertDialog: InputBoks
	 */
	private void behandleInput(int id, String input) {
		try {
			switch(id) {
			case NYTT_ENHETSNAVN:
				//lagrer enhetsnavn i sharedpreferences
				Filbehandling.lagreEnhetsnavn(this, input);
				//send melding om at enhetsnavn er oppdatert
				opprettKringkastning(NYTT_ENHETSNAVN);
				break;
			case NY_OPPDATERING_I_MINUTT:
				//lagre minutt i sharedpreferences
				float minutt = Float.parseFloat(input);
				Filbehandling.lagreMinuttOppdatering(this, minutt);
				//send melding om at minutt er oppdatert
				opprettKringkastning(NY_OPPDATERING_I_MINUTT);
				break;
			case NY_OPPDATERING_I_METER:
				//lagre meter i sharedpreferences
				float meter = Float.parseFloat(input);
				Filbehandling.lagreMeterOppdatering(this, meter);
				//send melding om at meter er oppdatert
				opprettKringkastning(NY_OPPDATERING_I_METER);
				break;
			default:
				break;
			} //switch
			//oppdater textview
			settVerdierTextView();
		} catch (NumberFormatException ex) {
			String feilmelding = getString(R.string.number_format_exception);
			Toast.makeText(this, feilmelding + ex.getMessage(), Toast.LENGTH_LONG).show();
		} catch (Exception ex) {
			String feilmelding = getString(R.string.unknown_error_exception);
			Toast.makeText(this, feilmelding + ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //behandleInput

	/**
	 * Funksjon som sender en intent om at innstillinger har blitt endret.
	 * @param endretInnstilling - final int: hvilken innstilling som er endret
	 */
	private void opprettKringkastning(final int endretInnstilling) {
		//opprett string som inneholder pakke - og aktivitetsnavn
		String pakkeNavn = getString(R.string.package_name);
		pakkeNavn += getString(R.string.intent_innstillinger_endret);
		//opprett intent som skal videresende broadcastet
		Intent kringkastning = new Intent();
		kringkastning.setAction(pakkeNavn);
		//legg ved pakkenavn og tittel på boken som ble opprettet
		kringkastning.putExtra(pakkeNavn, endretInnstilling);
		//send broadcastet		
		sendBroadcast(kringkastning);
	} //opprettKringkastning
} //InnstillingsActivity