package com.example.oblig1_android_knutlucasandersen;

/**
 * This class takes care of conversion related to Distance.
 * <br>The internal options are:
 * <br>- Angstroms
 * <br>- Microns
 * <br>- Millimeters
 * <br>- Centimeters
 * <br>- Meters
 * <br>- Kilometers
 * <br>- Inches
 * <br>- Feet
 * @author Knut Lucas Andersen
 */
public class ConvertDistance {
	//constants for this types conversions
	private final static int ANGSTROMS = 0;
	private final static int MICRONS = 1;
	private final static int MILLIMETERS = 2;
	private final static int CENTIMETERS = 3;
	private final static int DECIMETERS = 4;
	private final static int METERS = 5;
	private final static int KILOMETERS = 6;
	private final static int INCHES = 7;
	private final static int FEET = 8;

	/**
	 * This method picks out the conversion based on given parameters, and returns a double 
	 * containing the converted value
	 * 
	 * @param convertFrom (integer) Numeric value for the conversion value (i.e: 0 = Angstroms)
	 * @param convertTo  (integer) Numeric value for the conversion value (i.e: 1 = Microns) 
	 * @param conversionValue (double) The actual value to be converted (user input)
	 * @return (double) The converted value based on the options/input from above parameters
	 */
	public static double ConversionSelection(int convertFrom, int convertTo, double conversionValue) {
		double result = 0;
		switch(convertTo) {
		case ANGSTROMS:
			//convert to angstroms
			result = ConvertAngstrom(convertFrom, conversionValue);
			break;		
		case MICRONS:
			//convert to microns
			result = ConvertMicrons(convertFrom, conversionValue);
			break;
		case MILLIMETERS:
			//convert to millimeters
			result = ConvertMillimeters(convertFrom, conversionValue);
			break;
		case CENTIMETERS:
			//convert frtoom centimeters
			result = ConvertCentimeters(convertFrom, conversionValue);
			break;
		case DECIMETERS:
			//convert to decimeters
			result = ConvertDecimeters(convertFrom, conversionValue);
			break;
		case METERS:
			//convert to meters
			result = ConvertMeters(convertFrom, conversionValue);
			break;
		case KILOMETERS:
			//convert to kilometers
			result = ConvertKilometers(convertFrom, conversionValue);
			break;
		case INCHES:
			//convert to inches
			result = ConvertInches(convertFrom, conversionValue);
			break;
		case FEET:
			//convert to feet
			result = ConvertFeet(convertFrom, conversionValue);
			break;
		}//switch
		return result;
	} //ConversionSelection

	private static double ConvertAngstrom(int convertFrom, double conversionValue) {
		double result = 0;
		switch(convertFrom) {
		case ANGSTROMS:
			//no need for conversion
			result = conversionValue;
			break;		
		case MICRONS:
			//convert from microns to angstrom
			result = conversionValue * 10000;
			break;
		case MILLIMETERS:
			//convert from millimeters to angstrom
			result = conversionValue * 10000000;
			break;
		case CENTIMETERS:
			//convert from centimeters to angstrom
			result = conversionValue * 100000000;
			break;
		case DECIMETERS:
			//convert from decimeters to angstrom
			result = conversionValue * 1000000000;
			break;
		case METERS:
			//convert from meters to angstrom
			result = conversionValue * 10000000000L;
			break;
		case KILOMETERS:
			//convert from kilometers to angstrom
			result = conversionValue * 10000000000000L;
			break;
		case INCHES:
			//convert from inches to angstrom
			result = conversionValue * 254000000;
			break;
		case FEET:
			//convert from feet to angstrom
			result = conversionValue * 3048000000L;
			break;
		}//switch
		return result;
	} //ConvertAngstrom

	private static double ConvertMicrons(int convertFrom, double conversionValue) {
		double result = 0;
		switch(convertFrom) {
		case ANGSTROMS:
			//convert from angstroms to microns
			result = conversionValue / 10000;
			break;		
		case MICRONS:
			//no need for conversion
			result = conversionValue;
			break;
		case MILLIMETERS:
			//convert from millimeters to microns
			result = conversionValue * 1000;
			break;
		case CENTIMETERS:
			//convert from centimeters to microns
			result = conversionValue * 10000;
			break;
		case DECIMETERS:
			//convert from decimeters to microns
			result = conversionValue * 100000;
			break;			
		case METERS:
			//convert from meters to microns
			result = conversionValue * 1000000;
			break;
		case KILOMETERS:
			//convert from kilometers to microns
			result = conversionValue * 1000000000;
			break;
		case INCHES:
			//convert from inches to microns
			result = conversionValue * 25400;
			break;
		case FEET:
			//convert from feet to microns
			result = conversionValue * 304800;
			break;
		}//switch
		return result;
	} //ConvertMicrons

	private static double ConvertMillimeters(int convertFrom, double conversionValue) {
		double result = 0;
		switch(convertFrom) {
		case ANGSTROMS:
			//convert from angstroms to millimeters (10^-7)
			result = conversionValue / 10000000;
			break;		
		case MICRONS:
			//convert from microns to millimeters
			result = conversionValue / 1000;
			break;
		case MILLIMETERS:
			//no need for conversion
			result = conversionValue;
			break;
		case CENTIMETERS:
			//convert from centimeters to millimeters
			result = conversionValue * 10;
			break;
		case DECIMETERS:
			//convert from decimeters to millimeters
			result = conversionValue * 100;
			break;
		case METERS:
			//convert from meters to millimeters
			result = conversionValue * 1000;
			break;
		case KILOMETERS:
			//convert from kilometers to millimeters  
			result = conversionValue * 1000000;
			break;
		case INCHES:
			//convert from inches to millimeters  
			result = conversionValue * 25.4; 
			break;
		case FEET:
			//convert from feet to millimeters
			result = conversionValue * 304.8;
			break;
		}//switch
		return result;
	} //ConvertMillimeters

	private static double ConvertCentimeters(int convertFrom, double conversionValue) {
		double result = 0;
		switch(convertFrom) {
		case ANGSTROMS:
			//convert from angstroms to centimeters
			result = conversionValue / 100000000;
			break;		
		case MICRONS:
			//convert from microns to centimeters
			result = conversionValue / 10000;
			break;
		case MILLIMETERS:
			//convert from millimeters to centimeters
			result = conversionValue / 10;
			break;
		case CENTIMETERS:
			//no need for conversion
			result = conversionValue;
			break;
		case DECIMETERS:
			//convert from decimeters to centimeters
			result = conversionValue * 10;
			break;
		case METERS:
			//convert from meters to centimeters
			result = conversionValue * 100;
			break;
		case KILOMETERS:
			//convert from kilometers to centimeters
			result = conversionValue * 100000;
			break;
		case INCHES:
			//convert from inches to centimeters
			result = conversionValue * 2.54;
			break;
		case FEET:
			//convert from feet to centimeters
			result = conversionValue * 30.48;
			break;
		}//switch
		return result;
	} //ConvertCentimeters

	private static double ConvertDecimeters(int convertFrom, double conversionValue) {
		double result = 0;
		switch(convertFrom) {
		case ANGSTROMS:
			//convert from angstrom to decimeters
			result = conversionValue / 1000000000;
			break;		
		case MICRONS:
			//convert from microns to decimeters
			result = conversionValue / 100000;
			break;
		case MILLIMETERS:
			//convert from millimeters to decimeters
			result = conversionValue / 100;
			break;
		case CENTIMETERS:
			//convert from centimeters to decimeters
			result = conversionValue / 10;
			break;
		case DECIMETERS:
			//no need for conversion
			result = conversionValue; 
			break;
		case METERS:
			//convert from meters to decimeters
			result = conversionValue * 10;
			break;
		case KILOMETERS:
			//convert from kilometers to decimeters
			result = conversionValue * 10000;
			break;
		case INCHES:
			//convert from inches to decimeters
			result = conversionValue * 0.254;
			break;			
		case FEET:
			//convert from feet to decimeters
			result = conversionValue * 3.048;
			break;
		}//switch
		return result;
	} //ConvertDecimeters

	private static double ConvertMeters(int convertFrom, double conversionValue) {
		double result = 0;
		switch(convertFrom) {
		case ANGSTROMS:
			//convert from angstrom to meters
			result = conversionValue / 10000000000L;
			break;		
		case MICRONS:
			//convert from microns to meters
			result = conversionValue / 1000000;
			break;
		case MILLIMETERS:
			//convert from millimeters to meters
			result = conversionValue / 1000;
			break;
		case CENTIMETERS:
			//convert from centimeters to meters
			result = conversionValue / 100;
			break;
		case DECIMETERS:
			//convert from decimeters to meters
			result = conversionValue / 10;
			break;
		case METERS:
			//no need for conversion
			result = conversionValue;
			break;
		case KILOMETERS:
			//convert from kilometers to meters
			result = conversionValue * 1000;
			break;
		case INCHES:
			//convert from inches to meters
			result = conversionValue * 0.0254;
			break;
		case FEET:
			//convert from feet to meters
			result = conversionValue * 0.3048;
			break;
		}//switch
		return result;
	} //ConvertMeters

	private static double ConvertKilometers(int convertFrom, double conversionValue) {
		double result = 0;
		switch(convertFrom) {
		case ANGSTROMS:
			//convert from angstrom to kilometers
			result = conversionValue / 10000000000000L; 
			break;		
		case MICRONS:
			//convert from microns to kilometers
			result = conversionValue / 1000000000;
			break;
		case MILLIMETERS:
			//convert from millimeters to kilometers
			result = conversionValue / 1000000;
			break;
		case CENTIMETERS:
			//convert from centimeters to kilometers
			result = conversionValue / 100000;
			break;
		case DECIMETERS:
			//convert from decimeters to kilometers
			result = conversionValue / 10000;
			break;
		case METERS:
			//convert from meters to kilometers
			result = conversionValue / 1000;
			break;
		case KILOMETERS:
			//no need for conversion
			result = conversionValue;
			break;
		case INCHES:
			//convert from inches to kilometers
			result = conversionValue * (2.54 / 100000);
			break;
		case FEET:
			//convert from feet to kilometers
			result = conversionValue * (3.048 / 10000);
			break;
		}//switch
		return result;
	} //ConvertKilometers

	private static double ConvertInches(int convertFrom, double conversionValue) {
		double result = 0;
		switch(convertFrom) {
		case ANGSTROMS:
			//convert from angstrom to inches
			result = conversionValue * (3.93700787 / 1000000000);
			break;		
		case MICRONS:
			//convert from microns to inches  
			result = conversionValue * (3.93700787 / 100000);
			break;
		case MILLIMETERS:
			//convert from millimeters to inches  
			result = conversionValue * 0.0393700787;
			break;
		case CENTIMETERS:
			//convert from centimeters to inches
			result = conversionValue * 0.393700787;
			break;
		case DECIMETERS:
			//convert from decimeters to inches 
			result = conversionValue * 3.93700787;
			break;
		case METERS:
			//convert from meters to inches 
			result = conversionValue * 39.3700787;
			break;
		case KILOMETERS:
			//convert from kilometers to inches
			result = conversionValue * 39370.0787;
			break;
		case INCHES:
			//no need for conversion
			result = conversionValue;
			break;
		case FEET:
			//convert from feet to inches
			result = conversionValue * 12;
			break;
		}//switch
		return result;
	} //ConvertInches

	private static double ConvertFeet(int convertFrom, double conversionValue) {
		double result = 0;
		switch(convertFrom) {
		case ANGSTROMS:
			//convert from angstrom to feet
			result = conversionValue * (3.2808399 / 1000000000); 
			break;		
		case MICRONS:
			//convert from microns to feet
			result = conversionValue * (3.2808399 / 1000000);
			break;
		case MILLIMETERS:
			//convert from millimeters to feet
			result = conversionValue * 0.0032808399;
			break;
		case CENTIMETERS:
			//convert from centimeters to feet
			result = conversionValue * 0.032808399;
			break;
		case DECIMETERS:
			//convert from decimeters to feet
			result = conversionValue * 0.32808399;
			break;
		case METERS:
			//convert from meters to feet
			result = conversionValue * 3.2808399;
			break;
		case KILOMETERS:
			//convert from kilometers to feet
			result = conversionValue * 3280.8399;
			break;
		case INCHES:
			//convert from inches to feet
			result = conversionValue * 0.0833333333;
			break;
		case FEET:
			//no need for conversion
			result = conversionValue;
			break;
		}//switch
		return result;
	} //ConvertFeet
} //ConvertDistance