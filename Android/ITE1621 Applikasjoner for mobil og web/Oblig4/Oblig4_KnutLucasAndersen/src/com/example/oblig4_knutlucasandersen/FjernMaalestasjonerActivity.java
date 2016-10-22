package com.example.oblig4_knutlucasandersen;

//java-import
import java.util.ArrayList;
//android-import
import android.app.Activity;
import android.os.Bundle;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.CheckedTextView;
import android.widget.ListView;
import android.widget.Toast;

/**
 * Sub-Activity som tar for seg fjerning av registrerte målestasjoner.
 * @author Knut Lucas Andersen
 */
public class FjernMaalestasjonerActivity extends Activity {
	private ArrayList<Maalestasjon> maalestasjonListe;
	private ArrayList<String> stedsNavn;
	private ArrayAdapter<String> adapter;
	private CheckedTextView chkVelgAlle;
	private ListView lvMaalestasjon;
	private Button btnSave;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_vis_maalestasjoner);
		//default tekst for knapp er lagring; sett knappetekst til fjerning
		btnSave = (Button) findViewById(R.id.btnSave);
		btnSave.setText(getString(R.string.btnDelete));
		fyllListView();
	} //onCreate

	/**
	 * Fyller ListView med eksisterende kategorier fra xml. <br />
	 * Deretter leses det inn fra fil hvilke kategorier bruker har 
	 * valgt skal logges. <br /> 
	 * Ved første gangs oppstart er alle selektert.
	 */
	private void fyllListView() {
		try {
			//les inn eksisterende målestasjoner fra fil
			maalestasjonListe = Filbehandling.lesFraFil(this, false);
			stedsNavn = new ArrayList<String>();
			//finn listview via ressurs id
			lvMaalestasjon = (ListView) findViewById(R.id.lvChooseObservationStations);
			//loop gjennom og fyll opp arraylist med stedsnavn
			for (int i = 0; i < maalestasjonListe.size(); i++) {
				String sted = maalestasjonListe.get(i).getStedsNavn();
				stedsNavn.add(sted);
			} //for
			//koble adapter og arraylist
			adapter = new ArrayAdapter<String>(this, android.R.layout.simple_list_item_multiple_choice, stedsNavn);
			//koble adapter til listview
			lvMaalestasjon.setAdapter(adapter);
		} catch (Exception ex) {
			String feilmelding = getString(R.string.uknown_error_exception);
			Toast.makeText(this, feilmelding + ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //fyllListView

	public void avhukAlleTitler(View v) {
		try {
			//finn checkbox via ressurs id
			chkVelgAlle = (CheckedTextView) findViewById(R.id.chktvChooseAll);
			//skift tilstand på checkbox
			chkVelgAlle.toggle();
			//hent ut tilstanden til checkbox (checked/unchecked)
			boolean hukAv = chkVelgAlle.isChecked();
			//loop gjennom alle titler i listview
			for (int i = 0; i < lvMaalestasjon.getCount(); i++) {
				//selekter/de-selekter tittel
				lvMaalestasjon.setItemChecked(i, hukAv);
			} //for
		} catch (Exception ex) {
			//en feil oppsto, vis feilmelding
			Toast.makeText(this, "En feil oppsto:\n" + ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //avhukAlleTitler

	public void lagreEndringer(View v) {
		try {
			//start bakerst fra arraylist og gå fremover for å fjerne fra arraylist
			//(maalestasjonListe.size() - 1) pga da stemmer størrelsen overens med antall elementer
			//EKS: hvis maalestasjonListe.size() = 3, - så går den først innom element 2, element 1, 
			//og tilslutt element 0
			for (int i = (maalestasjonListe.size() - 1); i > -1; i--) {
				//hvis stedet er valgt, fjern det fra arraylist
				if (lvMaalestasjon.isItemChecked(i)) {
					maalestasjonListe.remove(i);
					stedsNavn.remove(i);
				} //if (lvMaalestasjon.isItemChecked(i))
			} //for
			//skriv verdier til fil
			Filbehandling.skrivTilFil(this, maalestasjonListe, false);
			//oppdater adapter med resterende stedsnavn
			adapter = new ArrayAdapter<String>(this, android.R.layout.simple_list_item_multiple_choice, stedsNavn);
			//oppdater listview med resterende stedsnavn
			lvMaalestasjon.setAdapter(adapter);
		} catch (Exception ex) {
			//en feil oppsto, vis feilmelding
			String feilmelding = getString(R.string.uknown_error_exception);
			Toast.makeText(this, feilmelding + ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //lagreEndringer
} //FjernMaalestasjonerActivity