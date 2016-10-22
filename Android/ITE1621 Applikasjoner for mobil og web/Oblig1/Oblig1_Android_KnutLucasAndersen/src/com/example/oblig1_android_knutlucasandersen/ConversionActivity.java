package com.example.oblig1_android_knutlucasandersen;

import java.util.ArrayList;

import android.app.Activity;
import android.app.FragmentManager;
import android.os.Bundle;
import android.view.Menu;
import android.widget.ArrayAdapter;

/**
 * The main activity, this is where the application starts up.
 * This class relies on ConversionListFragment.
 * @author Knut Lucas Andersen
 */
public class ConversionActivity extends Activity {
	public final static String CONVERSION_CHOICE ="com.example.oblig1_android_knutlucasandersen.CHOICE";

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_conversion);

		//create a reference to the fragments
		FragmentManager fm = getFragmentManager();
		////create an object of the class that creates a list
		ConversionListFragment listFragment = (ConversionListFragment)fm.findFragmentById(R.id.ConversionListFragment);
		//create an array that keeps the conversion options,
		//the values are retrieved from a string-array in the string language-xml
		String[] convArray = getResources().getStringArray(R.array.conversions_array);
		//create an arraylist that stores the convertable options
		ArrayList<String> conversionList = new ArrayList<String>();
		//create an arrayadapter that connects the arraylist and the listfragment
		ArrayAdapter<String> adapter = new ArrayAdapter<String>(this, android.R.layout.simple_list_item_1, conversionList);
		//loop through the values in the array
		for (int i = 0; i < convArray.length; i++) {
			//add the value to the arraylist
			conversionList.add(convArray[i]);
			//notify that data has been added
			adapter.notifyDataSetChanged();
		} //for
		//connect the adapter to the listfragment
		listFragment.setListAdapter(adapter);
	} //onCreate

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.activity_conversion, menu);
		return true;
	} //onCreateOptionsMenu

} //ConversionActivity