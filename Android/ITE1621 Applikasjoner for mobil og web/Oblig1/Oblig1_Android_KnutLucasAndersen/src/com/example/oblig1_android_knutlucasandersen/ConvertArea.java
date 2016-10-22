package com.example.oblig1_android_knutlucasandersen;

/**
 * This class takes care of conversion related to Area.
 * <br>The internal options are:
 * <br>- Square millimeters
 * <br>- Square centimeters
 * <br>- Square feet
 * <br>- Square yards
 * @author Knut Lucas Andersen
 */
public class ConvertArea {
	//constants for this types conversions
	private final static int SQUARE_MILLIMETERS = 0;
	private final static int SQUARE_CENTIMETERS = 1;
	private final static int SQUARE_FEET = 2;
	private final static int SQUARE_INCHES = 3;
	private final static int SQUARE_YARDS = 4;
	
	/**
	 * This method picks out the conversion based on given parameters, and returns a double 
	 * containing the converted value
	 * 
	 * @param convertFrom (integer) Numeric value for the conversion value (i.e: 0 = Square millimeters)
	 * @param convertTo  (integer) Numeric value for the conversion value (i.e: 1 = Square centimeters) 
	 * @param conversionValue (double) The actual value to be converted (user input)
	 * @return (double) The converted value based on the options/input from above parameters
	 */
	public static double ConversionSelection(int convertFrom, int convertTo, double conversionValue) {
		double result = 0;
		switch(convertTo) {
		case SQUARE_MILLIMETERS:
			//convert to square millimeters
			result = ConvertSquareMillimeters(convertFrom, conversionValue);
			break;
		case SQUARE_CENTIMETERS:
			//convert to square centimeters
			result = ConvertSquareCentimeters(convertFrom, conversionValue);
			break;
		case SQUARE_FEET:
			//convert to suare feet
			result = ConvertSquareFeet(convertFrom, conversionValue);
			break;
		case SQUARE_INCHES:
			//convert to square inches
			result = ConvertSquareInches(convertFrom, conversionValue);
			break;
		case SQUARE_YARDS:
			//convert to square yards
			result = ConvertSquareYards(convertFrom, conversionValue);
			break;
		} //switch
		return result;
	} //ConversionSelection
	
	private static double ConvertSquareMillimeters(int convertFrom, double conversionValue) {
		double result = 0;
		switch(convertFrom) {
		case SQUARE_MILLIMETERS:
			//no need for conversion
			result = conversionValue;
			break;
		case SQUARE_CENTIMETERS:
			//convert from square centimeters to square millimeters
			result = conversionValue * 100;
			break;
		case SQUARE_FEET:
			//convert from square feet to square millimeters
			result = conversionValue * 92903.04;
			break;
		case SQUARE_INCHES:
			//convert from square inches to square millimeters
			result = conversionValue * 645.16;
			break;
		case SQUARE_YARDS:
			//convert from square yards to square millimeters
			result = conversionValue * 836127.36;
			break;
		}//switch
		return result;
	} //ConvertSquareMillimeters
	
	private static double ConvertSquareCentimeters(int convertFrom, double conversionValue) {
		double result = 0;
		switch(convertFrom) {
		case SQUARE_MILLIMETERS:
			//convert from square millimeters to square centimeters
			result = conversionValue * 0.01;
			break;
		case SQUARE_CENTIMETERS:
			//no need for conversion
			result = conversionValue;
			break;
		case SQUARE_FEET:
			//convert from square feet to square centimeters
			result = conversionValue * 929.0304;
			break;
		case SQUARE_INCHES:
			//convert from square inches to square centimeters
			result = conversionValue * 6.4516;
			break;
		case SQUARE_YARDS:
			//convert from square yards to square centimeters
			result = conversionValue * 8361.2736;
			break;
		}//switch
		return result;
	} //ConvertSquareCentimeters
	
	private static double ConvertSquareFeet(int convertFrom, double conversionValue) {
		double result = 0;
		switch(convertFrom) {
		case SQUARE_MILLIMETERS:
			//convert from square millimeters to square feet 
			result = conversionValue * (1.07639104 / 100000);
			break;
		case SQUARE_CENTIMETERS:
			//convert from square centimeters to square feet
			result = conversionValue * 0.00107639104;
			break;
		case SQUARE_FEET:
			//no need for conversion
			result = conversionValue;
			break;
		case SQUARE_INCHES:
			//convert from square inches to square feet
			result = conversionValue * 0.00694444444;
			break;
		case SQUARE_YARDS:
			//convert from square yards to square feet
			result = conversionValue * 9;
			break;
		}//switch
		return result;
	} //ConvertSquareFeet
	
	private static double ConvertSquareInches(int convertFrom, double conversionValue) {
		double result = 0;
		switch(convertFrom) {
		case SQUARE_MILLIMETERS:
			//convert from square millimeters to square inches
			result = conversionValue * 0.0015500031;
			break;
		case SQUARE_CENTIMETERS:
			//convert from square centimeters to square inches
			result = conversionValue * 0.15500031;
			break;
		case SQUARE_FEET:
			//convert from square feet to square inches
			result = conversionValue * 144;
			break;
		case SQUARE_INCHES:
			//no need for conversion
			result = conversionValue;
			break;
		case SQUARE_YARDS:
			//convert from square yards to square inches
			result = conversionValue * 1296;
			break;
		}//switch
		return result;
	} //ConvertSquareInches
	
	private static double ConvertSquareYards(int convertFrom, double conversionValue) {
		double result = 0;
		switch(convertFrom) {
		case SQUARE_MILLIMETERS:
			//convert from square millimeters to square yards
			result = conversionValue * (1.19599005 / 1000000);
			break;
		case SQUARE_CENTIMETERS:
			//convert from square centimeters to square yards
			result = conversionValue * 0.000119599005;
			break;
		case SQUARE_FEET:
			//convert from square feet to square yards
			result = conversionValue * 0.111111111;
			break;
		case SQUARE_INCHES:
			//convert from square inches to square yards
			result = conversionValue * 0.000771604938;
			break;
		case SQUARE_YARDS:
			//no need for conversion
			result = conversionValue; 
			break;
		}//switch
		return result;
	} //ConvertSquareYards
} //ConvertArea