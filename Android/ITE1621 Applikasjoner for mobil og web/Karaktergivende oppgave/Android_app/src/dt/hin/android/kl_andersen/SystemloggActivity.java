package dt.hin.android.kl_andersen;

//java-import
import java.util.ArrayList;
//android-import
import android.app.Activity;
import android.os.Bundle;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.Toast;

/**
 * Klasse som leser inn og fremviser systemlogg,
 * og mulighet til å slett logg. <br />
 * Systemloggen inneholder hendelser som: <br />
 * - Feilmeldinger <br />
 * - Registrering på server/GCM <br />
 * - Avregistrering på server/GCM <br />
 * - Mottak av meldinger fra server/GCM <br />
 * @author Knut Lucas Andersen
 */
public class SystemloggActivity extends Activity {
	private ArrayList<String> loggListe;
	private ArrayAdapter<String> adapter;
	private ListView lvSystemlogg;

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_systemlogg);
		fyllListView();
	} //onCreate

	private void fyllListView() {
		try {
			//les inn målestasjoner fra raw-fil
			loggListe = Filbehandling.lesLoggFraFil(this);
			//finn listview via ressurs id
			lvSystemlogg = (ListView) findViewById(R.id.lvSystemlogg);
			//koble adapter og arraylist
			adapter = new ArrayAdapter<String>(this, android.R.layout.simple_list_item_1, loggListe);
			//koble adapter til listview
			lvSystemlogg.setAdapter(adapter);
		} catch (Exception ex) {
			//en feil oppsto, vis feilmelding
			String feilmelding = getString(R.string.unknown_error_exception);
			Toast.makeText(this, feilmelding + ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //fyllListView
	
	public void slettSystemlogg(View V) {
		//slett filens innhold og fremvis den tømte loggen
		Filbehandling.slettLoggensInnhold(this);
		fyllListView();
	} //slettSystemlogg
} //SystemloggActivity