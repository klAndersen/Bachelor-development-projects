package com.example.oblig4_knutlucasandersen;

//java-import
import java.lang.ref.WeakReference;
import java.util.ArrayList;
import java.util.Locale;
//android-import
import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.CheckedTextView;
import android.widget.ListView;
import android.widget.Toast;

/**
 * Dette er en sub-activity som tar for seg håndtering av å registrere nye målestasjoner. <br />
 * Målestasjonene hentes fra en RAW tekstfil (res/raw/), men et alternativ for en reell applikasjon <br />
 * ville vært å lagt stedsnavn i en database, og delt opp opplisting i f.eks. fylker/kommuner -> steder, med 
 * søkemulighet.
 * @author Knut Lucas Andersen
 */
public class LeggTilMaalestasjonActivity extends Activity {
	private ArrayList<Maalestasjon> alleMaalestasjonSteder;
	private static ArrayList<Maalestasjon> nedlastedeMaalestasjoner;
	private static ArrayAdapter<String> adapter;
	private CheckedTextView chkVelgAlle;
	private static ListView lvMaalestasjoner;
	private static int indeks;
	private static TraadHandler mTraadHandler;
	private static Context context;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_vis_maalestasjoner);
		fyllListView();
	} //onCreate

	/**
	 * Fyller ListView med målestasjoner basert på parsing fra xml. <br /> 
	 * ListView har mulighet for å selektere en eller alle.
	 */
	private void fyllListView() {
		try {
			//les inn målestasjoner fra raw-fil
			alleMaalestasjonSteder = Filbehandling.lesFraRawFil(this);
			//finn listview via ressurs id
			lvMaalestasjoner = (ListView) findViewById(R.id.lvChooseObservationStations);
			//hent ut sted fra intent og finn indeks
			Intent intent = getIntent();
			String sted = intent.getStringExtra(ActionBarFunksjoner.VIS_LISTE_FOR_AA_LEGGE_TIL_MAALESTASJON);
			indeks = finnStedIArrayList(sted);
			//ble en indeks for sted funnet
			if (indeks > -1) {
				//siden de fleste metodene knyttet til tråden er static, 
				//og opplisting i listview ble transparent, opprett objekt av context 
				//for å ta vare på Aktivitetens context (se også kommentar i LastNedMaalestasjoner.onPostExecute())
				context = this;
				nedlastedeMaalestasjoner = new ArrayList<Maalestasjon>();
				new LastNedMaalestasjoner(this).execute();
			} //if (indeks  -1)
		} catch (Exception ex) {
			//en feil oppsto, vis feilmelding
			String feilmelding = getString(R.string.uknown_error_exception);
			Toast.makeText(this, feilmelding + ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //fyllListView

	private int finnStedIArrayList(String sted) {
		int indeks;
		String msNavn;		
		Locale lokal = new Locale("nb_NO");
		for(int i = 0; i < alleMaalestasjonSteder.size(); i++) {
			msNavn = alleMaalestasjonSteder.get(i).getStedsNavn().toUpperCase(lokal);
			//er sted funnet
			if(sted.toUpperCase(lokal).equals(msNavn)) {
				indeks = i;
				//returner posisjon
				return indeks;
			} //if(sted.toUpperCase(lokal).equals(msNavn))
		} //for		
		//sted ikke funnet
		return -1;
	} //finnStedIArrayList

	public void avhukAlleTitler(View v) {
		try {
			//finn checkbox via ressurs id
			chkVelgAlle = (CheckedTextView) findViewById(R.id.chktvChooseAll);
			//skift tilstand på checkbox
			chkVelgAlle.toggle();
			//hent ut tilstanden til checkbox (checkedunchecked)
			boolean hukAv = chkVelgAlle.isChecked();
			//loop gjennom alle titler i listview
			for (int i = 0; i < lvMaalestasjoner.getCount(); i++) {
				//selekter/de-selekter tittel
				lvMaalestasjoner.setItemChecked(i, hukAv);
			} //for
		} catch (Exception ex) {
			//en feil oppsto, vis feilmelding
			String feilmelding = getString(R.string.uknown_error_exception);
			Toast.makeText(this, feilmelding + ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //avhukAlleTitler

	public void lagreEndringer(View v) {
		try {
			//opprett arraylist som skal inneholde valgte kategoerier som skal logges
			ArrayList<Maalestasjon> brukersValgteMaalestasjoner = Filbehandling.lesFraFil(this, false);
			//loop gjennom listviews og brukers valgte kategorier
			for (int i = 0; i < lvMaalestasjoner.getCount(); i++) {
				//er gjeldende item valgt
				if (lvMaalestasjoner.isItemChecked(i)) {
					if (brukersValgteMaalestasjoner.size() > 0) {
						loopGjennomEksisterendeMaalestasjoner(brukersValgteMaalestasjoner, i);
					} else {
						//legg til verdien i arraylist
						brukersValgteMaalestasjoner.add(nedlastedeMaalestasjoner.get(i));						
					} //if (brukersValgteMaalestasjoner.size() > 0)
				} //if (lvMaalestasjoner.isItemChecked(i))
			} //for
			//skriv verdier til fil
			Filbehandling.skrivTilFil(this, brukersValgteMaalestasjoner, false);
			this.finish();
		} catch (Exception ex) {
			//en feil oppsto, vis feilmelding
			String feilmelding = getString(R.string.uknown_error_exception);
			Toast.makeText(this, feilmelding + ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //lagreEndringer

	private void loopGjennomEksisterendeMaalestasjoner(ArrayList<Maalestasjon> ms, int indeks) {
		boolean finnes = false;
		for (int j = 0; j < ms.size(); j++) {
			//hent ut målestasjonnr
			int nyID = nedlastedeMaalestasjoner.get(indeks).getMaalestasjonNr();
			int eksisterendeID = ms.get(j).getMaalestasjonNr();
			//er målestasjonen registrert fra før?
			if (nyID == eksisterendeID) {
				finnes = true;
			} //if (nyID != eksisterendeID) 
		} //for
		if (!finnes) {
			//legg til verdien i arraylist
			ms.add(nedlastedeMaalestasjoner.get(indeks));
		} //if(!finnes)
	} //loopGjennomEksisterendeMaalestasjoner

	/**
	 * AsyncTask Klasse som tar for seg nedlasting av målestasjoner. <br /> 
	 * Informasjon som nedlastes er målestasjonnr, stedsnavn og url. <br /> 
	 * Fremvist informasjon er kun stedsnavnet.
	 * @author Knut Lucas Andersen
	 */
	private static class LastNedMaalestasjoner extends AsyncTask<Void,Void,Boolean> {
		//oppretter en WeakReference for å unngå minnelekkasjer
		private final WeakReference<LeggTilMaalestasjonActivity> mAsyncTask; 

		public LastNedMaalestasjoner(LeggTilMaalestasjonActivity aktivitet) {
			mTraadHandler = new TraadHandler(this);
			mAsyncTask = new WeakReference<LeggTilMaalestasjonActivity>(aktivitet);
		} //konstruktør

		@Override
		public Boolean doInBackground(Void... params) {
			return startNedlasting();
		} //doInBackground

		private boolean startNedlasting() {
			NedlastningsBehandler.lastNedMaalestasjoner(mAsyncTask.get().getApplicationContext(), mTraadHandler, nedlastedeMaalestasjoner, indeks);
			return true;
		} //startNedlasting

		@Override
		public void onPostExecute(Boolean result) {
			if(result) {
				//tråd er ferdig, vis målestasjoner
				//kaller her på objektet context pga. dersom jeg brukte mAsyncTask
				//så ble teksten transparent
				updateDisplay(context);
			} //if(result)
		} //onPostExecute

		public void handleMessage(Message msg) {
			visMessageToast(mAsyncTask.get().getApplicationContext(), msg);
		} //handleMessage
	} //LastNedMaalestasjoner

	/**
	 * Funksjon som oppdaterer ListView etter AsyncTask er ferdig med nedlasting. <br /> 
	 * Resultat som fremvises er stedsnavnet til målestasjonen(e). 
	 * @param context - Context
	 */
	private static void updateDisplay(Context context) {
		ArrayList<String> stedsNavn = new ArrayList<String>();
		for (int i = 0; i < nedlastedeMaalestasjoner.size(); i++) { 
			stedsNavn.add(nedlastedeMaalestasjoner.get(i).getStedsNavn());
		} //for
		//koble adapter og arraylist
		adapter = new ArrayAdapter<String>(context, android.R.layout.simple_list_item_multiple_choice, stedsNavn);
		//koble adapter til listview
		lvMaalestasjoner.setAdapter(adapter);
	} //updateDisplay

	/**
	 * Funksjon som viser meldinger sendt fra AsyncTask tråd i Toast.
	 * @param context - Context
	 * @param msg - Message
	 */
	protected static void visMessageToast(Context context, Message msg) {
		Toast.makeText(context, msg.obj.toString(), Toast.LENGTH_LONG).show();
	} //visMessageToast

	/**
	 * Handler som mottar beskjeder fra tråden. <br /> 
	 * Klassen er static og benytter seg av WeakReference for å unngå 
	 * minnelekkasjer.
	 * @author Knut Lucas Andersen
	 */
	private static class TraadHandler extends Handler {
		//oppretter en WeakReference for å unngå minnelekkasjer
		private final WeakReference<LastNedMaalestasjoner> mAsyncTask; 

		public TraadHandler(LastNedMaalestasjoner aSyncTask) {
			mAsyncTask = new WeakReference<LastNedMaalestasjoner>(aSyncTask);
		} //konstruktør

		@Override
		public void handleMessage(Message msg) {
			LastNedMaalestasjoner aSyncTask = mAsyncTask.get();
			//er tråd aktiv
			if (aSyncTask != null) {
				aSyncTask.handleMessage(msg);
			} //if (aSyncTask != null) 
		} //handleMessage
	} //TraadHandler
} //LeggTilMaalestasjonActivity