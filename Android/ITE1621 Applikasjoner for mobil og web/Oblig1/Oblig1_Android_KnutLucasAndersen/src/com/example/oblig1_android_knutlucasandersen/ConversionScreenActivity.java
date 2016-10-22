package com.example.oblig1_android_knutlucasandersen;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.TextView;

/**
 * This class is the activity that contains the screen where the actual 
 * conversion takes place. Here the user can choose which value to convert 
 * from and to, and what value (input) the user wishes to see converted.
 * This class relies on the fragment ConversionScreenFragment.
 * @author Knut Lucas Andersen
 */
public class ConversionScreenActivity extends Activity {
	//constants for the different conversion options
	public final static int AREA_CONVERSION = 0;
	public final static int DISTANCE_CONVERSION = 1;
	public final static int MASS_CONVERSION = 2;
	public final static int TEMPERATURE_CONVERSION = 3;
	public final static int TIME_CONVERSION = 4;
	public final static int VOLUME_CONVERSION = 5;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_result);
	} //onCreate

	/**
	 * This method is related to when the user pushes/clicks the button to 
	 * convert the input value. If no value is entered, an error message will 
	 * be displayed telling the user to enter a numeric value.
	 * The settings for the buttong and the corresponding componentes are set 
	 * in the files named *_screen_fragment.xml where * is the name of the 
	 * different conversion options. 
	 * @param v View
	 */
	public void ConvertValue(View v) {
		String resultText;
		//create an object of EditText
		EditText editText = (EditText)findViewById(R.id.enterConversionValue);
		//get the content of the EditText
		String content = editText.getText().toString();
		//has input been entered?
		if (content.equals("")) {
			//display an error message to the user
			resultText = getString(R.string.no_value_entered);
		} else { //numeric value entered
			//convert the content to double 
			//(this should go fine, since the EditText only accepts numeric and decimal values)
			double result = 0, convValue = Double.parseDouble(content);		
			//use the intent once more to get the message; what conversion is in progress?
			Intent intent = getIntent();
			//get the value for chosen conversion
			int choice = intent.getIntExtra(ConversionActivity.CONVERSION_CHOICE, 0);
			try {
			//which conversion (and calculation) does the user want?
			switch(choice) {
			case AREA_CONVERSION:
				result = ConvertArea(convValue);
				break;
			case DISTANCE_CONVERSION:
				result = ConvertDistance(convValue);
				break;
			case MASS_CONVERSION:
				result = ConvertMass(convValue);
				break;
			case TEMPERATURE_CONVERSION:
				result = ConvertTemperature(convValue);
				break;
			case TIME_CONVERSION:
				result = ConvertTime(convValue);
				break;
			case VOLUME_CONVERSION:
				result = ConvertVolume(convValue);
				break;
			} //switch
			//get the string to display to the user
			resultText = getString(R.string.tvResult) + " " + Double.toString(result);
			} catch (Exception ex) {
				resultText = ex.getMessage();
			}
		} //if (!content.equals(""))
		//create an object of the TextView to display the result to the user
		TextView tvResultat = (TextView) findViewById(R.id.tvConversionResult);		
		//make the TextView visible (hidden by default in xml)
		tvResultat.setVisibility(0);
		//put the string into the TextView so the user can see the converted value
		//or get an error message if no value was entered
		tvResultat.setText(resultText);
	} //ConvertValue

	/**
	 * This method takes the values from the drop-downs and the 
	 * input from the user and sending it to the class ConvertArea.
	 * The difference in this methods and the others are that the 
	 * spinners are based on each conversion option, and the same 
	 * goes for the corresponding class with same name as this function.
	 * @param inputValue (double) Value entered by the user
	 * @return double The result of the conversion
	 */
	private double ConvertArea(double inputValue) {
		//create an object of the drop-down
		Spinner spinner1 = (Spinner)findViewById(R.id.spinnerArea1);
		Spinner spinner2 = (Spinner)findViewById(R.id.spinnerArea2);
		//get the chosen items
		int convertFrom = (int) spinner1.getSelectedItemId();
		int convertTo = (int) spinner2.getSelectedItemId();
		//return the converted result
		return ConvertArea.ConversionSelection(convertFrom, convertTo, inputValue);
	} //ConvertArea

	/**
	 * This method takes the values from the drop-downs and the 
	 * input from the user and sending it to the class ConvertDistance.
	 * The difference in this methods and the others are that the 
	 * spinners are based on each conversion option, and the same 
	 * goes for the corresponding class with same name as this function.
	 * @param inputValue (double) Value entered by the user
	 * @return double The result of the conversion
	 */
	private double ConvertDistance(double inputValue) {
		//create an object of the drop-down
		Spinner spinner1 = (Spinner)findViewById(R.id.spinnerDistance1);
		Spinner spinner2 = (Spinner)findViewById(R.id.spinnerDistance2);
		//get the chosen items
		int convertFrom = (int) spinner1.getSelectedItemId();
		int convertTo = (int) spinner2.getSelectedItemId();
		//return the converted result
		return ConvertDistance.ConversionSelection(convertFrom, convertTo, inputValue);
	} //ConvertDistance

	/**
	 * This method takes the values from the drop-downs and the 
	 * input from the user and sending it to the class ConvertMass.
	 * The difference in this methods and the others are that the 
	 * spinners are based on each conversion option, and the same 
	 * goes for the corresponding class with same name as this function.
	 * @param inputValue (double) Value entered by the user
	 * @return double The result of the conversion
	 */
	private double ConvertMass(double inputValue) {
		//create an object of the drop-down
		Spinner spinner1 = (Spinner)findViewById(R.id.spinnerMass1);
		Spinner spinner2 = (Spinner)findViewById(R.id.spinnerMass2);
		//get the chosen items
		int convertFrom = (int) spinner1.getSelectedItemId();
		int convertTo = (int) spinner2.getSelectedItemId();
		//return the converted result
		return ConvertMass.ConversionSelection(convertFrom, convertTo, inputValue);
	} //ConvertMass

	/**
	 * This method takes the values from the drop-downs and the 
	 * input from the user and sending it to the class ConvertTemperature.
	 * The difference in this methods and the others are that the 
	 * spinners are based on each conversion option, and the same 
	 * goes for the corresponding class with same name as this function.
	 * @param inputValue (double) Value entered by the user
	 * @return double The result of the conversion
	 */
	private double ConvertTemperature(double inputValue) {
		//create an object of the drop-down
		Spinner spinner1 = (Spinner)findViewById(R.id.spinnerTemperature1);
		Spinner spinner2 = (Spinner)findViewById(R.id.spinnerTemperature2);
		//get the chosen items
		int convertFrom = (int) spinner1.getSelectedItemId();
		int convertTo = (int) spinner2.getSelectedItemId();
		//return the converted result
		return ConvertTemperature.ConversionSelection(convertFrom, convertTo, inputValue);
	} //ConvertTemperature

	/**
	 * This method takes the values from the drop-downs and the 
	 * input from the user and sending it to the class ConvertTime.
	 * The difference in this methods and the others are that the 
	 * spinners are based on each conversion option, and the same 
	 * goes for the corresponding class with same name as this function.
	 * @param inputValue (double) Value entered by the user
	 * @return double The result of the conversion
	 */
	private double ConvertTime(double inputValue) {
		//create an object of the drop-down
		Spinner spinner1 = (Spinner)findViewById(R.id.spinnerTime1);
		Spinner spinner2 = (Spinner)findViewById(R.id.spinnerTime2);
		//get the chosen items
		int convertFrom = (int) spinner1.getSelectedItemId();
		int convertTo = (int) spinner2.getSelectedItemId();
		//return the converted result
		return ConvertTime.ConversionSelection(convertFrom, convertTo, inputValue);
	} //ConvertTime

	/**
	 * This method takes the values from the drop-downs and the 
	 * input from the user and sending it to the class ConvertVolume.
	 * The difference in this methods and the others are that the 
	 * spinners are based on each conversion option, and the same 
	 * goes for the corresponding class with same name as this function.
	 * It also throws an exception if the user tries to convert cubic 
	 * measurement into liter measurement or vica versa.
	 * @param inputValue (double) Value entered by the user
	 * @return double The result of the conversion
	 */
	private double ConvertVolume(double inputValue) throws Exception {
		//create an object of the drop-down
		Spinner spinner1 = (Spinner)findViewById(R.id.spinnerVolume1);
		Spinner spinner2 = (Spinner)findViewById(R.id.spinnerVolume2);
		//get the chosen items
		int convertFrom = (int) spinner1.getSelectedItemId();
		int convertTo = (int) spinner2.getSelectedItemId();
		//return the converted result
		return ConvertVolume.ConversionSelection(convertFrom, convertTo, inputValue);
	} //ConvertVolume
} //ConversionScreenActivity