package com.example.oblig1_android_knutlucasandersen;

/**
 * This class takes care of conversion related to Mass.
 * <br>
 * The internal options are:
 * <br>- Microgram
 * <br>- Milligram
 * <br>- Centigram
 * <br>- Decigram
 * <br>- Gram
 * <br>- Decagram
 * <br>- Kilogram
 * <br>- Ounces
 * <br>- Pounds
 * @author Knut Lucas Andersen
 */
public class ConvertMass {
	//constants for this types conversions
	private final static int MICROGRAM = 0;
	private final static int MILLIGRAM = 1;
	private final static int CENTIGRAM = 2;
	private final static int DECIGRAM = 3;
	private final static int GRAM = 4;
	private final static int DECAGRAM = 5;
	private final static int KILOGRAM = 6;
	private final static int OUNCES = 7;
	private final static int POUNDS = 8;
	
	/**
	 * This method picks out the conversion based on given parameters, and returns a double 
	 * containing the converted value
	 * 
	 * @param convertFrom (integer) Numeric value for the conversion value (i.e: 0 = Microgram)
	 * @param convertTo  (integer) Numeric value for the conversion value (i.e: 1 = Milligram) 
	 * @param conversionValue (double) The actual value to be converted (user input)
	 * @return (double) The converted value based on the options/input from above parameters
	 */
	public static double ConversionSelection(int convertFrom, int convertTo, double conversionValue) {
		double result = 0;
		switch(convertTo) {
		case MICROGRAM:
			//convert to microgram
			result = ConvertMicrogram(convertFrom, conversionValue);
			break;
		case MILLIGRAM:
			//convert to milligram
			result = ConvertMilligram(convertFrom, conversionValue);
			break;
		case CENTIGRAM:
			//convert to centigram
			result = ConvertCentigram(convertFrom, conversionValue);
			break;
		case DECIGRAM:
			//convert to 
			result = ConvertDecigram(convertFrom, conversionValue);
			break;
		case GRAM:
			//convert to gram
			result = ConvertGram(convertFrom, conversionValue);
			break;
		case DECAGRAM:
			//convert to decagram
			result = ConvertDecaGram(convertFrom, conversionValue);
			break;
		case KILOGRAM:
			//convert to kilogram
			result = ConvertKilogram(convertFrom, conversionValue);
			break;
		case OUNCES:
			//convert to ounces
			result = ConvertOunces(convertFrom, conversionValue);
			break;
		case POUNDS:
			//convert to pounds
			result = ConvertPounds(convertFrom, conversionValue);
			break;
		}//switch
		return result;
	} //ConversionSelection
	
	private static double ConvertMicrogram(int convertFrom, double conversionValue) {
		double result = 0;
		switch(convertFrom) {
		case MICROGRAM:
			//no need for conversion
			result = conversionValue; 
			break;
		case MILLIGRAM:
			//convert from milligram to microgram
			result = conversionValue * 1000;
			break;
		case CENTIGRAM:
			//convert from centigram to microgram
			result = conversionValue * 10000;
			break;
		case DECIGRAM:
			//convert from decigram to microgram
			result = conversionValue * 100000;
			break;
		case GRAM:
			//convert from gram to microgram
			result = conversionValue * 1000000;
			break;
		case DECAGRAM:
			//convert from decagram to microgram
			result = conversionValue * 10000000;
			break;
		case KILOGRAM:
			//convert from kilogram to microgram
			result = conversionValue * 1000000000;
			break;
		case OUNCES:
			//convert from ounces to microgram
			result = conversionValue * 28349523.1;
			break;
		case POUNDS:
			//convert from  pounds to microgram
			result = conversionValue * 453592370;
			break;
		}//switch
		return result;
	} //ConvertMicrogram
	
	private static double ConvertMilligram(int convertFrom, double conversionValue) {
		double result = 0;
		switch(convertFrom) {
		case MICROGRAM:
			//convert from microgram to milligram
			result = conversionValue / 1000;
			break;
		case MILLIGRAM:
			//no need for conversion
			result = conversionValue; 
			break;
		case CENTIGRAM:
			//convert from centigram to milligram
			result = conversionValue * 10;
			break;
		case DECIGRAM:
			//convert from decigram to milligram
			result = conversionValue * 100;
			break;
		case GRAM:
			//convert from gram to milligram
			result = conversionValue * 1000;
			break;
		case DECAGRAM:
			//convert from decagram to milligram
			result = conversionValue * 10000;
			break;
		case KILOGRAM:
			//convert from kilogram to milligram
			result = conversionValue * 1000000;
			break;
		case OUNCES:
			//convert from ounces to milligram
			result = conversionValue * 28349.5231;
			break;
		case POUNDS:
			//convert from  pounds to milligram
			result = conversionValue * 453592.37;
			break;
		}//switch
		return result;
	} //ConvertMilligram
	
	private static double ConvertCentigram(int convertFrom, double conversionValue) {
		double result = 0;
		switch(convertFrom) {
		case MICROGRAM:
			//convert from microgram to centigram
			result = conversionValue / 10000;
			break;
		case MILLIGRAM:
			//convert from milligram to centigram
			result = conversionValue * 0.1;
			break;
		case CENTIGRAM:
			//no need for conversion
			result = conversionValue; 
			break;
		case DECIGRAM:
			//convert from decigram to centigram
			result = conversionValue * 10;
			break;
		case GRAM:
			//convert from gram to centigram
			result = conversionValue * 100;
			break;
		case DECAGRAM:
			//convert from decagram to centigram
			result = conversionValue * 1000;
			break;
		case KILOGRAM:
			//convert from kilogram to centigram
			result = conversionValue * 100000;
			break;
		case OUNCES:
			//convert from ounces to centigram
			result = conversionValue * 2834.95231;
			break;
		case POUNDS:
			//convert from  pounds to centigram
			result = conversionValue * 45359.237;
			break;
		}//switch
		return result;
	} //ConvertCentigram
	
	private static double ConvertDecigram(int convertFrom, double conversionValue) {
		double result = 0;
		switch(convertFrom) {
		case MICROGRAM:
			//convert from microgram to decigram
			result = conversionValue / 100000;
			break;
		case MILLIGRAM:
			//convert from milligram to decigram
			result = conversionValue * 0.01;
			break;
		case CENTIGRAM:
			//convert from centigram to decigram
			result = conversionValue * 0.1;
			break;
		case DECIGRAM:
			//no need for conversion
			result = conversionValue; 
			break;
		case GRAM:
			//convert from gram to decigram
			result = conversionValue * 10;
			break;
		case DECAGRAM:
			//convert from decagram to decigram
			result = conversionValue * 100;
			break;
		case KILOGRAM:
			//convert from kilogram to decigram
			result = conversionValue * 10000;
			break;
		case OUNCES:
			//convert from ounces to decigram
			result = conversionValue * 283.495231;
			break;
		case POUNDS:
			//convert from  pounds to decigram
			result = conversionValue * 4535.9237;
			break;
		}//switch
		return result;
	} //ConvertDecigram
	
	private static double ConvertGram(int convertFrom, double conversionValue) {
		double result = 0;
		switch(convertFrom) {
		case MICROGRAM:
			//convert from microgram to gram
			result = conversionValue / 1000000;
			break;
		case MILLIGRAM:
			//convert from milligram to gram
			result = conversionValue / 1000;
			break;
		case CENTIGRAM:
			//convert from centigram to gram
			result = conversionValue * 0.01;
			break;
		case DECIGRAM:
			//convert from decigram to gram
			result = conversionValue * 0.1;
			break;
		case GRAM:
			//no need for conversion
			result = conversionValue; 
			break;
		case DECAGRAM:
			//convert from decagram to gram
			result = conversionValue * 10;
			break;
		case KILOGRAM:
			//convert from kilogram to gram
			result = conversionValue * 1000;
			break;
		case OUNCES:
			//convert from ounces to gram
			result = conversionValue * 28.3495231;
			break;
		case POUNDS:
			//convert from  pounds to gram
			result = conversionValue * 453.59237;
			break;
		}//switch
		return result;
	} //ConvertGram
	
	private static double ConvertDecaGram(int convertFrom, double conversionValue) {
		double result = 0;
		switch(convertFrom) {
		case MICROGRAM:
			//convert from microgram to decagram
			result = conversionValue / 10000000; 
			break;
		case MILLIGRAM:
			//convert from milligram to decagram
			result = conversionValue / 10000;
			break;
		case CENTIGRAM:
			//convert from centigram to decagram
			result = conversionValue / 1000;
			break;
		case DECIGRAM:
			//convert from decigram to decagram
			result = conversionValue * 0.01;
			break;
		case GRAM:
			//convert from gram to decagram
			result = conversionValue * 0.1;
			break;
		case DECAGRAM:
			//no need for conversion
			result = conversionValue; 
			break;
		case KILOGRAM:
			//convert from kilogram to decagram
			result = conversionValue * 100;
			break;
		case OUNCES:
			//convert from ounces to decagram
			result = conversionValue * 2.83495231;
			break;
		case POUNDS:
			//convert from  pounds to decagram	
			result = conversionValue * 45.359237;
			break;
		}//switch
		return result;
	} //ConvertDecaGram
	
	private static double ConvertKilogram(int convertFrom, double conversionValue) {
		double result = 0;
		switch(convertFrom) {
		case MICROGRAM:
			//convert from microgram to kilogram
			result = conversionValue / 1000000000;
			break;
		case MILLIGRAM:
			//convert from milligram to kilogram
			result = conversionValue / 1000000;
			break;
		case CENTIGRAM:
			//convert from centigram to kilogram
			result = conversionValue / 100000;
			break;
		case DECIGRAM:
			//convert from decigram to kilogram
			result = conversionValue / 10000;
			break;
		case GRAM:
			//convert from gram to kilogram
			result = conversionValue * 1000;
			break;
		case DECAGRAM:
			//convert from decagram to kilogram
			result = conversionValue * 0.01;
			break;
		case KILOGRAM:
			//no need for conversion
			result = conversionValue; 
			break;
		case OUNCES:
			//convert from ounces to kilogram
			result = conversionValue * 0.0283495231;
			break;
		case POUNDS:
			//convert from pounds to kilogram		
			result = conversionValue * 0.45359237;
			break;
		}//switch
		return result;
	} //ConvertKilogram
	
	private static double ConvertOunces(int convertFrom, double conversionValue) {
		double result = 0;
		switch(convertFrom) {
		case MICROGRAM:
			//convert from microgram to ounces
			result = conversionValue * (3.52739619 / 100000000);
			break;
		case MILLIGRAM:
			//convert from milligram to ounces
			result = conversionValue * (3.52739619  / 100000);
			break;
		case CENTIGRAM:
			//convert from centigram to ounces
			result = conversionValue * 0.000352739619;
			break;
		case DECIGRAM:
			//convert from decigram to ounces
			result = conversionValue * 0.00352739619;
			break;
		case GRAM:
			//convert from gram to ounces
			result = conversionValue * 0.0352739619;
			break;
		case DECAGRAM:
			//convert from decagram to ounces
			result = conversionValue * 0.352739619;
			break;
		case KILOGRAM:
			//convert from kilogram to ounces
			result = conversionValue * 35.2739619;
			break;
		case OUNCES:
			//no need for conversion
			result = conversionValue; 
			break;
		case POUNDS:
			//convert from pounds to ounces
			result = conversionValue * 16;
			break;
		}//switch
		return result;
	} //ConvertOunces
	
	private static double ConvertPounds(int convertFrom, double conversionValue) {
		double result = 0;
		switch(convertFrom) {
		case MICROGRAM:
			//convert from microgram to pounds  
			result = conversionValue * (2.20462262 / 1000000000);
			break;
		case MILLIGRAM:
			//convert from milligram to pounds
			result = conversionValue * (2.20462262 / 1000000);
			break;
		case CENTIGRAM:
			//convert from centigram to pounds
			result = conversionValue * (2.20462262 / 100000);
			break;
		case DECIGRAM:
			//convert from decigram to pounds
			result = conversionValue * 2.20462262;
			break;
		case GRAM:
			//convert from gram to pounds
			result = conversionValue * 0.00220462262;
			break;
		case DECAGRAM:
			//convert from decagram to pounds
			result = conversionValue * 0.0220462262;
			break;
		case KILOGRAM:
			//convert from kilogram to pounds
			result = conversionValue * 2.20462262;
			break;
		case OUNCES:
			//convert from ounces to pounds
			result = conversionValue * 0.0625;
			break;
		case POUNDS:
			//no need for conversion
			result = conversionValue;  
			break;
		}//switch
		return result;
	} //ConvertPounds
} //ConvertMass