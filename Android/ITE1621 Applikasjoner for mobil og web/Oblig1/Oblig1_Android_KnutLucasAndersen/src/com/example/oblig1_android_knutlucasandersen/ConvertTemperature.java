package com.example.oblig1_android_knutlucasandersen;

/**
 * This class takes care of conversion related to Temperature.
 * <br>The internal options are:
 * <br>- Celcius
 * <br>- Fahrenheit
 * <br>- Kelvin
 * @author Knut Lucas Andersen
 */
public class ConvertTemperature {
	//constants for this types conversions
	private final static int CELCIUS = 0;
	private final static int FAHRENHEIT = 1;
	private final static int KELVIN = 2;
	
	/**
	 * This method picks out the conversion based on given parameters, and returns a double 
	 * containing the converted value
	 * 
	 * @param convertFrom (integer) Numeric value for the conversion value (i.e: 0 = Celcius)
	 * @param convertTo  (integer) Numeric value for the conversion value (i.e: 1 = Fahrenheit) 
	 * @param conversionValue (double) The actual value to be converted (user input)
	 * @return (double) The converted value based on the options/input from above parameters
	 */
	public static double ConversionSelection(int convertFrom, int convertTo, double conversionValue) {
		double result = 0;
		switch(convertTo) {
		case CELCIUS:
			//convert to celcius
			result = ConvertCelcius(convertFrom, conversionValue);
			break;
		case FAHRENHEIT:
			//convert to fahrenheit
			result = ConvertFahrenheit(convertFrom, conversionValue);
			break;
		case KELVIN:
			//convert to kelvin
			result = ConvertKelvin(convertFrom, conversionValue);
			break;
		}//switch
		return result;
	} //ConversionSelection
	
	private static double ConvertCelcius(int convertFrom, double conversionValue) {
		double result = 0;
		switch(convertFrom) {
		case CELCIUS:
			//no need for conversion
			result = conversionValue;
			break;
		case FAHRENHEIT:
			//convert from fahrenheit to celcius
			result = conversionValue * (-17.2222222);
			break;
		case KELVIN:
			//convert from kelvin to celcius
			result = conversionValue * (-272.15);
			break;
		}//switch
		return result;
	} //ConvertCelcius
	
	private static double ConvertFahrenheit(int convertFrom, double conversionValue) {
		double result = 0;
		switch(convertFrom) {
		case CELCIUS:
			//convert from celcius to fahrenheit
			result = conversionValue * 33.8;
			break;
		case FAHRENHEIT:
			//no need for conversion
			result = conversionValue;
			break;
		case KELVIN:
			//convert from kelvin to fahrenheit
			result = conversionValue * (-457.87);
			break;
		}//switch
		return result;
	} //ConvertFahrenheit
	
	private static double ConvertKelvin(int convertFrom, double conversionValue) {
		double result = 0;
		switch(convertFrom) {
		case CELCIUS:
			//convert from celcius to kelvin
			result = conversionValue * 274.15;
			break;
		case FAHRENHEIT:
			//convert from fahrenheit to kelvin
			result = conversionValue * 255.927778;
			break;
		case KELVIN:
			//no need for conversion
			result = conversionValue;
			break;
		}//switch
		return result;
	} //ConvertKelvin
} //ConvertTemperature