package com.example.oblig3_knutlucasandersen_del2;

import android.app.ListFragment;
import android.content.Intent;
import android.view.View;
import android.widget.ListView;
import android.widget.Toast;

/**
 * Dette fragmentet er avhengig/en del av klassen LoggerUIActivity. <br />
 * Den fremviser en liste med alle registrerte kategorier. <br />
 * Ved klikk på en kategori, så fremvises loggen for valgt kategori.
 * @author Knut Lucas Andersen
 */
public class LoggUIFragment extends ListFragment {

	public void onListItemClick(ListView l, View v, int position, long id) {
		super.onListItemClick(l, v, position, id);

		try {
			//forsøk å opprette meldingen(intent)
			Intent intent = new Intent((LoggerUIActivity)getActivity(), LoggingResultActivity.class);
			//send/legg ved ID (for valgt kategori) 
			intent.putExtra(LoggerUIActivity.ID_VALGT_KATEGORI, (int)id);
			//start aktiviteten
			startActivity(intent);
		} catch (Exception ex) {
			//en feil oppsto, vis feilmelding
			Toast.makeText((LoggerUIActivity)getActivity(), ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //onListItemClick
} //LoggUIFragment