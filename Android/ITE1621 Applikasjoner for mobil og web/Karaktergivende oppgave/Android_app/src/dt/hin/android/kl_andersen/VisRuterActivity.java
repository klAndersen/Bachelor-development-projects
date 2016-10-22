package dt.hin.android.kl_andersen;

//java-import
import java.util.ArrayList;
//android-import
import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.Toast;

/**
 * Sub-aktivitet som fremviser lagrede ruter og gir bruker valget 
 * mellom å vise disse på kartet eller slette opplistede ruter.
 * @author Knut Lucas Andersen
 */
public class VisRuterActivity extends Activity {
	public static final String VIS_RUTER = "visRuter"; 
	public static final int VIS_ALLE_RUTER = 0;
	public static final int VIS_EGNE_RUTER = 1;
	public static final int VIS_ANDRES_RUTER = 2;
	private static ListView lvVisning;
	private static int valgtVisning = -1;
	private static ArrayAdapter<Enhet> adapter;
	private static ArrayList<Enhet> ruteListe;
	private static Context context;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_vis_ruter);
		context = this;
		ruteListe = new ArrayList<Enhet>();
		opprettQuery();
	} //onCreate

	public void slettRuter(View v) {
		if (ruteListe.size() > 0) {
			//tøm arraylist for innhold
			ruteListe.clear();
			//start slettingen fra databasen
			AsyncTraad asyncTraad = new AsyncTraad(this, null, AsyncTraad.SLETT_SPESIFISERTE_RADER);
			asyncTraad.execute();
		} //if (ruteListe.size() > 0)
	} //slettRuter

	/**
	 * Oppdaterer ListView til å vise det siste innholdet fra ArrayList&lt;Enhet&gt;.
	 */
	public static void updateListView() {
		//oppdater listview
		adapter = new ArrayAdapter<Enhet>(context, android.R.layout.simple_list_item_1, ruteListe);
		lvVisning.setAdapter(adapter);
	} //updateListView

	/**
	 * Returnerer den valgte visningen.
	 * @return int: Aktiv visning 
	 */
	public final static int getValgtVisning() {
		return valgtVisning;
	} //getValgtVisning

	/**
	 * Oppretter spørring som skal brukes for å hente ut ruter.
	 */
	private void opprettQuery() {
		//variabler som brukes i spørringen
		String tblEnhet = LoggingContentProvider.DB_TBLENHET;
		String pkEnhet = LoggingContentProvider.TBLENHET_PRIMARY_KEY;
		String egenEnhet = LoggingContentProvider.TBLENHET_EGEN_ENHET;
		String tblKoordinater = LoggingContentProvider.DB_TBLKOORDINATER;
		String fkEnhet = LoggingContentProvider.TBLKOORDINATER_FOREIGN_KEY;
		String ruteTidspunkt = LoggingContentProvider.TBLKOORDINATER_RUTETIDSPUNKT;
		String query = "SELECT * FROM " 
				+ tblEnhet + ", " + tblKoordinater;
		//hent visningen bruker vil se
		Intent intent = getIntent();
		valgtVisning = intent.getIntExtra(VIS_RUTER, VIS_ALLE_RUTER);
		switch(valgtVisning) {
		case VIS_ALLE_RUTER:
			query += " WHERE " + tblKoordinater + "." + fkEnhet + " = " + tblEnhet + "." + pkEnhet
			+ " ORDER BY " + tblEnhet + "." + pkEnhet + ", " + tblKoordinater + "." + ruteTidspunkt + " ASC";
			break;
		case VIS_EGNE_RUTER:
			query += " WHERE " + tblKoordinater + "." + fkEnhet + " = " + tblEnhet + "." + pkEnhet
			+ " AND " + tblEnhet + "." + egenEnhet + " = " + 1
			+ " ORDER BY " + tblEnhet + "." + pkEnhet + ", " + tblKoordinater + "." + ruteTidspunkt + " ASC";
			break;
		case VIS_ANDRES_RUTER:
			query += " WHERE " + tblKoordinater + "." + fkEnhet + " = " + tblEnhet + "." + pkEnhet
			+ " AND " + tblEnhet + "." + egenEnhet + " = " + 0
			+ " ORDER BY " + tblEnhet + "." + pkEnhet + ", " + tblKoordinater + "." + ruteTidspunkt + " ASC";
			break;
		} //switch
		fyllListView(query);
	} //opprettQuery

	/**
	 * Fyller ListView med ruter fra database basert på oversendt query.
	 * @param query - String: Spørringen som skal kjøres mot databasen
	 */
	private void fyllListView(String query) {
		try {
			//hent ruter fra databasen
			ruteListe = DatabaseFunksjoner.hentLagredeRuter(query);
			if (ruteListe.size() > 0) {
				int ruteID = 0;
				long pkEnhet = 0;
				//hent ut enheter som er online
				ArrayList<Enhet> onlineEnheter = FellesFunksjoner.getOnlineEnheter();
				//loop gjennom ruter uthentet fra database
				for (int i = ruteListe.size() - 1; i > -1; i--) {
					//siden historikk ruter får identifikator med negativ verdi, 
					//gang den med -1 for å få positiv verdi
					pkEnhet = ruteListe.get(i).getIdentifikator() * (-1);
					ruteID = ruteListe.get(i).getRuteID();
					//loop gjennom enheter som er online
					for (int j = 0; j < onlineEnheter.size(); j++) {
						//finnes historikk ruten blant online enheters ruter?
						if (pkEnhet == onlineEnheter.get(j).getIdentifikator() && ruteID == onlineEnheter.get(j).getRuteID()) {
							//denne ruten er en online rute, fjern den fra listen
							ruteListe.remove(i);
						} //if (pkEnhet == onlineEnheter.get(j).getIdentifikator() ...)
					} //indre for
				} //ytre for
				//opprett og fyll listview med innhold
				lvVisning = (ListView) findViewById(R.id.lvVisning);
				adapter = new ArrayAdapter<Enhet>(this, android.R.layout.simple_list_item_1, ruteListe);
				lvVisning.setAdapter(adapter);
				//knytt lytter til listview
				lvVisning.setOnItemClickListener(new OnItemClickListener() {
					@Override
					public void onItemClick(AdapterView<?> parent, View v, int position, long id) {
						//sett nytt navn og at dette ikke er brukers enhet
						String nyttNavn = "Historikk: " + ruteListe.get(position).getEnhetsnavn();
						ruteListe.get(position).setEnhetsNavn(nyttNavn);
						ruteListe.get(position).setEgenEnhetFalse();
						//opprett og tegn ruta
						FellesFunksjoner.opprettHistorikkRute(ruteListe.get(position));
						//avslutt rutevisning
						finish();
					} //onItemClick
				}); //setOnItemClickListener
			} else {
				Toast.makeText(this, "Ingen ruter registrert.", Toast.LENGTH_LONG).show();
			} // if (ruteListe.size() > 0)
		} catch (Exception ex) {
			String feilmelding = getString(R.string.unknown_error_exception);
			Toast.makeText(this, feilmelding + ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //fyllListView
} //VisRuterActivity