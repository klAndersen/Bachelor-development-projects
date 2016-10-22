package com.example.oblig2A_knutlucasandersen;

import android.app.Activity;
import android.app.ListFragment;
import android.content.Intent;
import android.view.View;
import android.widget.ListView;
import android.widget.Toast;

/**
 * Fragment for fremvisning/opplisting av registrerte boktitler.
 * @author Knut Lucas Andersen
 */
public class BokListeFragment extends ListFragment {	

	@Override
	public void onListItemClick(ListView l, View v, int position, long id) {
		super.onListItemClick(l, v, position, id);		

		try {
			//string som inneholder pakkenavnet som oversendes med intent
			String pakkeNavn = getString(R.string.delA_package_name);
			pakkeNavn += getString(R.string.activity_vis_tittel_liste);
			//hent ut tittel fra listview basert på oversendt posisjon
			String valgtTittel = l.getItemAtPosition(position).toString();
			//opprett intent som skal returneres
			Intent resultat = new Intent();
			//legg ved package og tittel
			resultat.putExtra(pakkeNavn, valgtTittel);
			//sett at resultatet blir oversendt sammen med selve intentet
			getActivity().setResult(Activity.RESULT_OK, resultat);
			//fullfør operasjonen
			getActivity().finish();
		} catch (Exception ex) {
			//en feil oppsto, vis feilmelding
			Toast.makeText((BokListeActivity)getActivity(), ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //onListItemClick
} //BokListeFragment