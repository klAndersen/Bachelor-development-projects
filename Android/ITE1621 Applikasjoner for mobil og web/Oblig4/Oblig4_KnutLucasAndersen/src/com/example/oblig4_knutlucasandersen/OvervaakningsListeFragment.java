package com.example.oblig4_knutlucasandersen;

import android.app.ListFragment;
import android.content.Intent;
import android.view.View;
import android.widget.ListView;
import android.widget.Toast;

/**
 * Fragment som fremviser en liste som inneholder linker til yr.no <br />
 * eller valgte målestasjoner og deres temperaturer.
 * @author Knut Lucas Andersen
 */
public class OvervaakningsListeFragment extends ListFragment {

	@Override
	public void onListItemClick(ListView l, View v, int position, long id) {
		super.onListItemClick(l, v, position, id);

		Intent intent = getActivity().getIntent();
		String nokkel = ActionBarFunksjoner.VIS_LISTE_FOR_AA_LEGGE_TIL_MAALESTASJON;
		boolean nyMaalestasjon = intent.getBooleanExtra(nokkel, false);
		if (nyMaalestasjon) {
			visListeOverMaalestasjoner(l, position, nokkel);
		} //if (nyMaalestasjon)
	} //onListItemClick

	private void visListeOverMaalestasjoner(ListView l, int position, String nokkel) {
		try {
			Intent intent = new Intent(getActivity(), LeggTilMaalestasjonActivity.class);
			String verdi = l.getItemAtPosition(position).toString();
			intent.putExtra(nokkel, verdi);
			startActivity(intent);
		} catch (Exception ex) {
			//vis feilmelding til bruker
			Toast.makeText(getActivity(), ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //visListeOverMaalestasjoner
} //OvervaakningsListeFragment