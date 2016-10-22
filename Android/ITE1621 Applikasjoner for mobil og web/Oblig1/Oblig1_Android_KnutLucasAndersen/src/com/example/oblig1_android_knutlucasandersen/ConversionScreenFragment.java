package com.example.oblig1_android_knutlucasandersen;

import android.app.Fragment;
import android.content.Intent;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

public class ConversionScreenFragment extends Fragment {

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
		//initialization of the view
		View viewToDisplay = null;
		//get the message
		Intent intent = ((ConversionScreenActivity) getActivity()).getIntent();
		//get the value the message contains (what the user wants to convert)
		int choice = intent.getIntExtra(ConversionActivity.CONVERSION_CHOICE, 0);
		//what conversion screen is to be displayed?
		switch(choice) {
		case ConversionScreenActivity.AREA_CONVERSION:
			//show area conversionscreen
			viewToDisplay = inflater.inflate(R.layout.area_screen_fragment, container, false);
			break;
		case ConversionScreenActivity.DISTANCE_CONVERSION:
			//show distance conversionscreen
			viewToDisplay = inflater.inflate(R.layout.distance_screen_fragment, container, false);
			break;
		case ConversionScreenActivity.MASS_CONVERSION:
			//show mass conversionscreen
			viewToDisplay = inflater.inflate(R.layout.mass_screen_fragment, container, false);
			break;
		case ConversionScreenActivity.TEMPERATURE_CONVERSION:
			//show temperature conversionscreen			
			viewToDisplay = inflater.inflate(R.layout.temperature_screen_fragment, container, false);
			break;
		case ConversionScreenActivity.TIME_CONVERSION:
			//show time conversionscreen
			viewToDisplay = inflater.inflate(R.layout.time_screen_fragment, container, false);
			break;
		case ConversionScreenActivity.VOLUME_CONVERSION:
			//show volume conversionscreen
			viewToDisplay = inflater.inflate(R.layout.volume_screen_fragment, container, false);
			break;
		} //switch
		return viewToDisplay;
	} //onCreateView
} //ConversionScreenFragment