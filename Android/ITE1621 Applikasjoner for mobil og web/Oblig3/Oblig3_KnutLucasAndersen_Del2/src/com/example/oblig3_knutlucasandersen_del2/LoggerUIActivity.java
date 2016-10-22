package com.example.oblig3_knutlucasandersen_del2;

//java-import
import java.util.ArrayList;
//android-import
import android.os.Bundle;
import android.app.Activity;
import android.app.FragmentManager;
import android.view.Menu;
import android.widget.ArrayAdapter;

/**
 * Hovedaktiviteten som starter applikasjonen. <br />
 * Den starter opp en fragment kalt LoggUIFragment.
 * @author Knut Lucas Andersen
 */
public class LoggerUIActivity extends Activity {
	public final static String ID_VALGT_KATEGORI= "com.example.oblig3_knutlucasandersen_del2.LOGG_KATEGORI";

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_logger_ui);
		//opprett fragment
		opprettFragment();
	} //onCreate

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.logger_ui, menu);
		return true;
	} //onCreateOptionsMenu

	private void opprettFragment() {
		//opprett referanse til fragment
		FragmentManager fm = getFragmentManager();
		//opprett et objekt av fragmentet som viser listen
		LoggUIFragment loggUIFragment = (LoggUIFragment)fm.findFragmentById(R.id.LoggUIFragment);
		//opprett en array som inneholder kategoriene,
		//verdiene er hentet fra en string-array i strings.xml
		String[] kategoriArray = getResources().getStringArray(R.array.array_kategorier);
		//opprett en arraylist som inneholder kategoriene
		ArrayList<String> kategoriListe = new ArrayList<String>();
		//opprett en arrayadapter som kobler sammen arraylist og listfragment
		ArrayAdapter<String> adapter = new ArrayAdapter<String>(this, android.R.layout.simple_list_item_1, kategoriListe);
		//loop gjennom verdiene i array
		for (int i = 0; i < kategoriArray.length; i++) {
			//legg til array's verdi i arraylist
			kategoriListe.add(kategoriArray[i]);
			//meld ifra om at data har blitt lagt til
			adapter.notifyDataSetChanged();
		} //for
		//koble adapter til listfragment
		loggUIFragment.setListAdapter(adapter);
	} //opprettFragment
} //LoggerUIActivity