package com.example.oblig1_android_knutlucasandersen;

import android.app.ListFragment;
import android.content.Intent;
import android.util.Log;
import android.view.View;
import android.widget.ListView;
import android.widget.Toast;

/**
 * This class extends the ListFragment to create a list 
 * displaying the different options/types the user can 
 * convert values from/to.
 * @author Knut Lucas Andersen
 */
public class ConversionListFragment extends ListFragment {

	/**
	 * Attempts to send an intent which contains the value (integer) for which 
	 * conversion the user wants to use.
	 * @param l ListView
	 * @param v View
	 * @param position int
	 * @param id long
	 */
	@Override
	public void onListItemClick(ListView l, View v, int position, long id) {
		super.onListItemClick(l, v, position, id);		

		try {
			//attempt to create a message (intent)
			Intent intent = new Intent((ConversionActivity)getActivity(), ConversionScreenActivity.class);
			//send the ID (the conversion the user wants) 
			intent.putExtra(ConversionActivity.CONVERSION_CHOICE, (int)id);
			//start the activity
			startActivity(intent);
		} catch (Exception ex) {
			//some exception occured, show the error message in LogCat
			Log.i("Exception", ex.getMessage());
			//show the error message to the user with Toast
			Toast.makeText((ConversionActivity)getActivity(), ex.getMessage(), Toast.LENGTH_LONG).show();
		} //try/catch
	} //onListItemClick
} //ConversionList