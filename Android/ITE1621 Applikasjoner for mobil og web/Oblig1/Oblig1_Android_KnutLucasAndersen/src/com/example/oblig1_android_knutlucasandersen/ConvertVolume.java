package com.example.oblig1_android_knutlucasandersen;

/**
 * This class takes care of conversion related to Volume.
 * <br>The internal options are:
 * <br>- Cubic millimeters
 * <br>- Cubic centimeters
 * <br>- Cubic decimeters
 * <br>- Cubic kilometers
 * <br>- Cubic incehs
 * <br>- Cubic feet
 * <br>- Cubic yards
 * <br>- Milli liters
 * <br>- Centi liters
 * <br>- Deci liters
 * <br>- Liters
 * @author Knut Lucas Andersen
 */
public class ConvertVolume {
	//constants for this types conversions
	private final static int CUBIC_MILLIMETERS = 0;
	private final static int CUBIC_CENTIMETERS = 1;
	private final static int CUBIC_DECIMETERS = 2;
	private final static int CUBIC_KILOMETERS = 3;
	private final static int CUBIC_INCHES = 4;
	private final static int CUBIC_FEET = 5;
	private final static int CUBIC_YARDS = 6;
	private final static int MILLILITERS = 7;
	private final static int CENTILITERS = 8;
	private final static int DECILITERS = 9;
	private final static int LITERS = 10;

	/**
	 * This method picks out the conversion based on given parameters, and returns a double 
	 * containing the converted value. Throws an exception if the user attempts to convert a cubic 
	 * to liters or vica versa.
	 * 
	 * @param convertFrom (integer) Numeric value for the conversion value (i.e: 0 = Cubic millimeters)
	 * @param convertTo  (integer) Numeric value for the conversion value (i.e: 1 = Cubic centimeters) 
	 * @param conversionValue (double) The actual value to be converted (user input)
	 * @return (double) The converted value based on the options/input from above parameters
	 */
	public static double ConversionSelection(int convertFrom, int convertTo, double conversionValue) throws Exception {
		double result = 0;
		switch(convertTo) {
		case CUBIC_MILLIMETERS:
			//convert to cubic millimeters
			result = ConvertCubicMillimeters(convertFrom, conversionValue);
			break;
		case CUBIC_CENTIMETERS:
			//convert to cubic centimeters
			result = ConvertCubicCentimeters(convertFrom, conversionValue);
			break;
		case CUBIC_DECIMETERS:
			//convert to cubic decimeters
			result = ConvertCubicDecimeters(convertFrom, conversionValue);
			break;
		case CUBIC_KILOMETERS:
			//convert to cubic kilometers
			result = ConvertCubicKilometers(convertFrom, conversionValue);
			break;
		case CUBIC_INCHES:
			//convert to convert to cubic inches
			result = ConvertCubicInches(convertFrom, conversionValue);
			break;
		case CUBIC_FEET:
			//convert to convert to cubic feet
			result = ConvertCubicFeet(convertFrom, conversionValue);
			break;
		case CUBIC_YARDS:
			//convert to convert to cubic yards
			result = ConvertCubicYards(convertFrom, conversionValue);
			break;
		case MILLILITERS:
			//convert to milliliters
			result = ConvertMilliliters(convertFrom, conversionValue);
			break;
		case CENTILITERS:
			//convert to centiliters
			result = ConvertCentiliters(convertFrom, conversionValue);
			break;		
		case DECILITERS:
			//convert to deciliters
			result = ConvertDeciliters(convertFrom, conversionValue);
			break;
		case LITERS:
			//convert to liters
			result = ConvertLiters(convertFrom, conversionValue);
			break;
		}//switch
		return result;
	} //ConversionSelection

	private static double ConvertCubicMillimeters(int convertFrom, double conversionValue) throws Exception {
		double result = 0;
		switch(convertFrom) {
		case CUBIC_MILLIMETERS:
			//no need for conversion
			result = conversionValue;
			break;
		case CUBIC_CENTIMETERS:
			//convert from cubic centimeters to cubic millimeters
			result = conversionValue * 1000;
			break;
		case CUBIC_DECIMETERS:
			//convert from cubic decimeters to cubic millimeters
			result = conversionValue * 1000000;
			break;
		case CUBIC_KILOMETERS:
			//convert from cubic kilometers to cubic millimeters
			result = conversionValue * 1000000000000000000L;
			break;
		case CUBIC_INCHES:
			//convert from cubic inches to cubic millimeters
			result = conversionValue * 16387.064;
			break;
		case CUBIC_FEET:
			//convert from cubic feet to cubic millimeters
			result = conversionValue * 28316846.6;
			break;
		case CUBIC_YARDS:
			//convert from cubic yards to cubic millimeters¨
			result = conversionValue * 764554858;
			break;
		default:
			//throw an exception telling the user this conversion 
			//cannot be completed
			throw new Exception("Can't convert from cubic to liters.");			
		}//switch
		return result;
	} //ConvertCubicMillimeters

	private static double ConvertCubicCentimeters(int convertFrom, double conversionValue) throws Exception {
		double result = 0;
		switch(convertFrom) {
		case CUBIC_MILLIMETERS:
			//convert from cubic millimeters to cubic centimeters
			result = conversionValue / 1000;
			break;
		case CUBIC_CENTIMETERS:
			//no need for conversion
			result = conversionValue;
			break;
		case CUBIC_DECIMETERS:
			//convert from cubic decimeters to cubic centimeters
			result = conversionValue * 1000; 
			break;
		case CUBIC_KILOMETERS:
			//convert from cubic kilometers to cubic centimeters
			result = conversionValue * 1000000000000000L;
			break;
		case CUBIC_INCHES:
			//convert from cubic inches to cubic centimeters
			result = conversionValue * 16.387064;
			break;
		case CUBIC_FEET:
			//convert from cubic feet to cubic centimeters
			result = conversionValue * 28316.8466;
			break;
		case CUBIC_YARDS:
			//convert from cubic yards to cubic centimeters
			result = conversionValue * 764554.858;
			break;
		default:
			//throw an exception telling the user this conversion 
			//cannot be completed
			throw new Exception("Can't convert from cubic to liters.");		
		}//switch
		return result;
	} //ConvertCubicCentimeters

	private static double ConvertCubicDecimeters(int convertFrom, double conversionValue) throws Exception {
		double result = 0;
		switch(convertFrom) {
		case CUBIC_MILLIMETERS:
			//convert from cubic millimeters to cubic decimeters
			result = conversionValue / 1000000;
			break;
		case CUBIC_CENTIMETERS:
			//convert from cubic centimeters to cubic decimeters
			result = conversionValue * 1000;
			break;
		case CUBIC_DECIMETERS:
			//no need for conversion
			result = conversionValue;
			break;
		case CUBIC_KILOMETERS:
			//convert from cubic kilometers to cubic decimeters 
			result = conversionValue * 1000000000000L;
			break;
		case CUBIC_INCHES:
			//convert from cubic inches to cubic decimeters
			result = conversionValue * 0.016387064;
			break;
		case CUBIC_FEET:
			//convert from cubic feet to cubic decimeters 
			result = conversionValue * 28.3168466;
			break;
		case CUBIC_YARDS:
			//convert from cubic yards to cubic decimeters
			result = conversionValue * 764.554858;
			break;
		default:
			//throw an exception telling the user this conversion 
			//cannot be completed
			throw new Exception("Can't convert from cubic to liters.");		
		}//switch
		return result;
	} //ConvertCubicDecimeters

	private static double ConvertCubicKilometers(int convertFrom, double conversionValue) throws Exception {
		double result = 0;
		switch(convertFrom) {
		case CUBIC_MILLIMETERS:
			//convert from cubic millimeters to cubic kilometers
			result = conversionValue / 1000000000000000000L;
			break;
		case CUBIC_CENTIMETERS:
			//convert from cubic centimeters to cubic kilometers
			result = conversionValue / 1000000000000000L;
			break;
		case CUBIC_DECIMETERS:
			//convert from cubic decimeters to cubic kilometers
			result = conversionValue / 1000000000000L;
			break;
		case CUBIC_KILOMETERS:
			//no need for conversion
			result = conversionValue;
			break;
		case CUBIC_INCHES:
			//convert from cubic inches to cubic kilometers 
			result = conversionValue * (1.6387064 / 100000000000000L);
			break;
		case CUBIC_FEET:
			//convert from cubic feet to cubic kilometers 
			result = conversionValue * (2.83168466 / 1000000000000L);
			break;
		case CUBIC_YARDS:
			//convert from cubic yards to cubic kilometers 
			result = conversionValue * (7.64554858 / 10000000000L);
			break;
		default:
			//throw an exception telling the user this conversion 
			//cannot be completed
			throw new Exception("Can't convert from cubic to liters.");		
		}//switch
		return result;
	} //ConvertCubicKilometers

	private static double ConvertCubicInches(int convertFrom, double conversionValue) throws Exception {
		double result = 0;
		switch(convertFrom) {
		case CUBIC_MILLIMETERS:
			//convert from cubic millimeters to cubic inches
			result = conversionValue * (6.10237441 / 100000);
			break;
		case CUBIC_CENTIMETERS:
			//convert from cubic centimeters to cubic inches
			result = conversionValue * 0.0610237441;
			break;
		case CUBIC_DECIMETERS:
			//convert from cubic decimeters to cubic inches
			result = conversionValue * 61.0237441;
			break;
		case CUBIC_KILOMETERS:
			//convert from cubic kilometers to cubic inches 
			result = conversionValue * (6.10237441 * 10000000000000L); 
			break;
		case CUBIC_INCHES:
			//no need for conversion
			result = conversionValue;
			break;
		case CUBIC_FEET:
			//convert from cubic feet to cubic inches
			result = conversionValue * 1728;
			break;
		case CUBIC_YARDS:
			//convert from cubic yards to cubic inches
			result = conversionValue * 46656;
			break;
		default:
			//throw an exception telling the user this conversion 
			//cannot be completed
			throw new Exception("Can't convert from cubic to liters.");		
		}//switch
		return result;
	} //ConvertCubicInches

	private static double ConvertCubicFeet(int convertFrom, double conversionValue) throws Exception {
		double result = 0;
		switch(convertFrom) {
		case CUBIC_MILLIMETERS:
			//convert from cubic millimeters to cubic feet
			result = conversionValue * (3.53146667 / 100000000);
			break;
		case CUBIC_CENTIMETERS:
			//convert from cubic centimeters to cubic feet
			result = conversionValue * (3.53146667 / 100000);
			break;
		case CUBIC_DECIMETERS:
			//convert from cubic decimeters to cubic feet
			result = conversionValue * 0.0353146667;
			break;
		case CUBIC_KILOMETERS:
			//convert from cubic kilometers to cubic feet
			result = conversionValue * (3.53146667 * 10000000000L);
			break;
		case CUBIC_INCHES:
			//convert from cubic inches to cubic feet
			result = conversionValue * 0.000578703704;
			break;
		case CUBIC_FEET:
			//no need for conversion
			result = conversionValue;
			break;
		case CUBIC_YARDS:
			//convert from cubic yards to cubic feet
			result = conversionValue * 27;
			break;
		default:
			//throw an exception telling the user this conversion 
			//cannot be completed
			throw new Exception("Can't convert from cubic to liters.");		
		}//switch
		return result;
	} //ConvertCubicFeet

	private static double ConvertCubicYards(int convertFrom, double conversionValue) throws Exception {
		double result = 0;
		switch(convertFrom) {
		case CUBIC_MILLIMETERS:
			//convert from cubic millimeters to cubic yards
			result = conversionValue * (1.30795062 / 1000000000);
			break;
		case CUBIC_CENTIMETERS:
			//convert from cubic centimeters to cubic yards
			result = conversionValue * (1.30795062 / 1000000);
			break;
		case CUBIC_DECIMETERS:

			break;
		case CUBIC_KILOMETERS:
			//convert from cubic kilometers to cubic yards 
			result = conversionValue * (1.30795062 * 1000000000);
			break;
		case CUBIC_INCHES:
			//convert from cubic inches to cubic yards
			result = conversionValue * (2.14334705 / 100000);
			break;
		case CUBIC_FEET:
			//convert from cubic feet to cubic yards 
			result = conversionValue * 0.037037037;
			break;
		case CUBIC_YARDS:
			//no need for conversion
			result = conversionValue;
			break;
		default:
			//throw an exception telling the user this conversion 
			//cannot be completed
			throw new Exception("Can't convert from cubic to liters.");		
		}//switch
		return result;
	} //ConvertCubicYards

	private static double ConvertMilliliters(int convertFrom, double conversionValue) throws Exception {
		double result = 0;
		switch(convertFrom) {
		case MILLILITERS:
			//no need for conversion
			result = conversionValue;
			break;
		case CENTILITERS:
			//convert from centiliters to milliliters
			result = conversionValue * 10;
			break;		
		case DECILITERS:
			//convert from deciliters to milliliters
			result = conversionValue * 100;
			break;
		case LITERS:
			//convert from liters to milliliters
			result = conversionValue * 1000;
			break;
		default:
			//throw an exception telling the user this conversion 
			//cannot be completed
			throw new Exception("Can't convert from liters to cubic.");		
		}//switch
		return result;
	} //ConvertMilliliters

	private static double ConvertCentiliters(int convertFrom, double conversionValue) throws Exception {
		double result = 0;
		switch(convertFrom) {
		case MILLILITERS:
			//convert from milliliters to centiliters
			result = conversionValue * 0.1;
			break;
		case CENTILITERS:
			//no need for conversion
			result = conversionValue;
			break;		
		case DECILITERS:
			//convert from deciliters to centiliters
			result = conversionValue * 10;
			break;
		case LITERS:
			//convert from liters to centiliters
			result = conversionValue * 100;
			break;
		default:
			//throw an exception telling the user this conversion 
			//cannot be completed
			throw new Exception("Can't convert from liters to cubic.");		
		}//switch
		return result;
	} //ConvertCentiliters

	private static double ConvertDeciliters(int convertFrom, double conversionValue) throws Exception {
		double result = 0;
		switch(convertFrom) {
		case MILLILITERS:
			//convert from milliliters to deciliters
			result = conversionValue * 0.01;
			break;
		case CENTILITERS:
			//convert from centiliters to deciliters
			result = conversionValue * 0.1;
			break;	
		case DECILITERS:
			//no need for conversion
			result = conversionValue;
			break;
		case LITERS:
			//convert from liters to deciliters
			result = conversionValue * 10;
			break;
		default:
			//throw an exception telling the user this conversion 
			//cannot be completed
			throw new Exception("Can't convert from liters to cubic.");		
		}//switch
		return result;
	} //ConvertDeciliters

	private static double ConvertLiters(int convertFrom, double conversionValue) throws Exception {
		double result = 0;
		switch(convertFrom) {
		case MILLILITERS:
			//convert from milliliters to liters 
			result = conversionValue *  0.001;
			break;
		case CENTILITERS:
			//convert from centiliters to liters
			result = conversionValue * 0.01;
			break;		
		case DECILITERS:
			//convert from deciliters to liters
			result = conversionValue * 0.1;
			break;
		case LITERS:
			//no need for conversion
			result = conversionValue;
			break;
		default:
			//throw an exception telling the user this conversion 
			//cannot be completed
			throw new Exception("Can't convert from liters to cubic.");		
		}//switch
		return result;
	} //ConvertLiters
} //ConvertVolume