package com.example.oblig1_android_knutlucasandersen;

/**
 * This class takes care of conversion related to Time.
 * <br>The internal options are:
 * <br>- Milliseconds
 * <br>- Seconds
 * <br>- Minutes
 * <br>- Hours
 * <br>- Days
 * <br>- Weeks
 * <br>- Months
 * <br>- Years
 * <br>- Decades
 * <br>- Centuries
 * @author Knut Lucas Andersen
 */
public class ConvertTime {
	//constants for this types conversions
	private final static int MILLISECONDS = 0;
	private final static int SECONDS = 1;	
	private final static int MINUTES = 2;
	private final static int HOURS = 3;
	private final static int DAYS = 4;
	private final static int WEEKS = 5;
	private final static int MONTHS = 6;
	private final static int YEARS = 7;
	private final static int DECADES = 8;
	private final static int CENTURIES = 9;

	/**
	 * This method picks out the conversion based on given parameters, and returns a double 
	 * containing the converted value.
	 * <br>
	 * <br>Due note that not all methods in this class is finished, there are some still lacking.
	 * <br>The following methods are incomplete/unfinished:
	 * <br>- ConvertDays
	 * <br>- ConvertWeeks
	 * <br>- ConvertMonths
	 * <br>- ConvertYears
	 * 
	 * @param convertFrom (integer) Numeric value for the conversion value (i.e: 0 = Milliseconds)
	 * @param convertTo  (integer) Numeric value for the conversion value (i.e: 1 = Seconds) 
	 * @param conversionValue (double) The actual value to be converted (user input)
	 * @return (double) The converted value based on the options/input from above parameters
	 */
	public static double ConversionSelection(int convertFrom, int convertTo, double conversionValue) {
		double result = 0;
		switch(convertTo) {
		case MILLISECONDS:
			//convert to milliseconds
			result = ConvertMilliseconds(convertFrom, conversionValue);			
			break;			
		case SECONDS:
			//convert to seconds 
			result = ConvertSeconds(convertFrom, conversionValue);
			break;
		case MINUTES:
			//convert to minutes 
			result = ConvertMinutes(convertFrom, conversionValue);			
			break;
		case HOURS:
			//convert to hours
			result = ConvertHours(convertFrom, conversionValue);			
			break;
		case DAYS:
			//convert to days 
			result = ConvertDays(convertFrom, conversionValue);			
			break;
		case WEEKS: 
			//convert to weeks
			result = ConvertWeeks(convertFrom, conversionValue);			
			break;
		case MONTHS:
			//convert to months
			result = ConvertMonths(convertFrom, conversionValue);			
			break;
		case YEARS:
			//convert to years
			result = ConvertYears(convertFrom, conversionValue);			
			break;
		case DECADES:
			//convert to decades
			result = ConvertDecades(convertFrom, conversionValue);			
			break;
		case CENTURIES:
			//convert to centuries
			result = ConvertCenturies(convertFrom, conversionValue);			
			break;
		}//switch
		return result;
	} //ConversionSelection

	private static double ConvertMilliseconds(int convertFrom, double conversionValue) {
		double result = 0;
		switch(convertFrom) {
		case MILLISECONDS:
			//no need for conversion
			result = conversionValue;
			break;			
		case SECONDS:
			//convert from seconds to milliseconds 
			result = conversionValue * 1000;
			break;
		case MINUTES:
			//convert from minutes to milliseconds 
			result = conversionValue * 60 * 1000;
			break;
		case HOURS:
			//convert from hours to milliseconds 
			result = conversionValue * 3600 * 1000;
			break;
		case DAYS:
			//convert from days to milliseconds
			//(1 day = 24h)
			result = conversionValue * 24 * 3600 * 1000;
			break;
		case WEEKS: 
			//convert from weeks to milliseconds 
			//(1 week = 7 days, 1 day = 24h)
			result = conversionValue * 7 * 24 * 3600 * 1000;
			break;
		case MONTHS:
			//convert from months to milliseconds 
			//(1month = 4 weeks, 1 week = 7 days, 1 day = 24h)
			result = conversionValue * 4 * 7 * 24 * 3600 * 1000;
			break;
		case YEARS:
			//convert from years to milliseconds
			//(1 year = 12 months, 1 months = 4 weeks, 1 week = 7 days, 1 day = 24h)
			result = conversionValue * 12 * 4 * 7 * 24 * 3600 * 1000;
			break;
		case DECADES:
			//convert from decades to milliseconds
			//(1 year = 12 months, 1 months = 4 weeks, 1 week = 7 days, 1 day = 24h)
			result = conversionValue * 10 * 12 * 4 * 7 * 24 * 3600 * 1000;
			break;
		case CENTURIES:
			//convert from centuries to milliseconds
			//(1 year = 12 months, 1 months = 4 weeks, 1 week = 7 days, 1 day = 24h)
			result = conversionValue * 100 * 12 * 4 * 7 * 24 * 3600 * 1000;
			break;
		}//switch
		return result;
	} //ConvertMilliseconds

	private static double ConvertSeconds(int convertFrom, double conversionValue) {
		double result = 0;
		switch(convertFrom) {
		case MILLISECONDS:
			//convert from milliseconds to seconds
			result = conversionValue * 0.001;
			break;			
		case SECONDS:
			//no need for conversion
			result = conversionValue;
			break;
		case MINUTES:
			//convert from minutes to seconds
			result = conversionValue * 60;
			break;
		case HOURS:
			//convert from	hours to seconds 
			result = conversionValue * 3600; 
			break;
		case DAYS:
			//convert from days to seconds 
			result = conversionValue * 86400;
			break;
		case WEEKS: 
			//convert from weeks seconds
			result = conversionValue * 604800;
			break;
		case MONTHS:
			//convert from	months to secons 
			result = conversionValue * 2629743.83; 
			break;
		case YEARS:
			//convert from years to seconds
			result = conversionValue * 31556926;
			break;
		case DECADES:
			//convert from decades to seconds 
			result = conversionValue * 315569260;
			break;
		case CENTURIES:
			//convert from centuries to seconds
			result = conversionValue * (3.1556926 * 1000000000);
			break;
		}//switch
		return result;
	} //ConvertSeconds

	private static double ConvertMinutes(int convertFrom, double conversionValue) {
		double result = 0;
		switch(convertFrom) {
		case MILLISECONDS:
			//convert from milliseconds to minutes
			result = conversionValue * (1.66666667 / 100000);
			break;			
		case SECONDS:
			//convert from seconds to minutes
			result = conversionValue * 0.0166666667; 
			break;
		case MINUTES:
			//no need for conversion
			result = conversionValue;
			break;
		case HOURS:
			//convert from hours to minutes
			result = conversionValue * 60;
			break;
		case DAYS:
			//convert from days to minutes
			result = conversionValue * 1440;
			break;
		case WEEKS: 
			//convert from weeks to minutes
			result = conversionValue * 10080;
			break;
		case MONTHS:
			//convert from months to minutes
			result = conversionValue * 43829.0639;
			break;
		case YEARS:
			//convert from yars to minutes
			result = conversionValue * 525948.766;
			break;
		case DECADES:
			//convert from decades to minutes
			result = conversionValue * 5259487.66;
			break;
		case CENTURIES:
			//convert from centuries to minutes
			result = conversionValue * 52594876.6;
			break;
		}//switch
		return result;
	} //ConvertMinutes

	private static double ConvertHours(int convertFrom, double conversionValue) {
		double result = 0;
		switch(convertFrom) {
		case MILLISECONDS:
			//convert from milliseconds to hours
			result = conversionValue * (2.77777778 / 10000000);
			break;			
		case SECONDS:
			//convert from seconds to hours
			result = conversionValue * 0.000277777778;
			break;
		case MINUTES:
			//convert from minutes to hours
			result = conversionValue * 0.0166666667;
			break;
		case HOURS:
			//no need for conversion
			result = conversionValue;
			break;
		case DAYS:
			//convert from days to hours
			result = conversionValue * 24;
			break;
		case WEEKS: 
			//convert from weeks to hours
			result = conversionValue * 168;
			break;
		case MONTHS:
			//convert from months to hours
			result = conversionValue *  730.484398;
			break;
		case YEARS:
			//convert from years to hours	
			result = conversionValue * 8765.81277;
			break;
		case DECADES:
			//convert from decades to hours
			result = conversionValue * 87658.1277;
			break;
		case CENTURIES:
			//convert from centuries to hours
			result = conversionValue * 876581.277;
			break;
		}//switch
		return result;
	} //ConvertHours

	private static double ConvertDays(int convertFrom, double conversionValue) {
		double result = 0;
		switch(convertFrom) {
		case MILLISECONDS:
			//convert from milliseconds to days
			result = conversionValue * (1.15740741 / 100000000);
			break;			
		case SECONDS:
			//convert from 
			break;
		case MINUTES:
			//convert from 		
			break;
		case HOURS:
			//convert from		
			break;
		case DAYS:
			//no need for conversion
			result = conversionValue;
			break;
		case WEEKS: 
			//convert from		
			break;
		case MONTHS:
			//convert from	
			break;
		case YEARS:
			//convert from		
			break;
		case DECADES:
			//convert from decades to days
			result = conversionValue * 3652.42199;
			break;
		case CENTURIES:
			//convert from centuries to days
			result = conversionValue * 36524.2199;
			break;
		}//switch
		return result;
	} //ConvertDays

	private static double ConvertWeeks(int convertFrom, double conversionValue) {
		double result = 0;
		switch(convertFrom) {
		case MILLISECONDS:
			//convert from milliseconds to weeks
			result = conversionValue * (1.65343915 / 1000000000);
			break;			
		case SECONDS:
			//convert from 
			break;
		case MINUTES:
			//convert from 		
			break;
		case HOURS:
			//convert from		
			break;
		case DAYS:
			//convert from 		
			break;
		case WEEKS: 
			//no need for conversion
			result = conversionValue;	
			break;
		case MONTHS:
			//convert from	
			break;
		case YEARS:
			//convert from		
			break;
		case DECADES:
			//convert from decades to weeks
			result = conversionValue * 521.77457;
			break;
		case CENTURIES:
			//convert from centuries to weeks
			result = conversionValue * 5217.7457; 
			break;
		}//switch
		return result;
	} //ConvertWeeks

	private static double ConvertMonths(int convertFrom, double conversionValue) {
		double result = 0;
		switch(convertFrom) {
		case MILLISECONDS:
			//convert from milliseconds to months
			result = conversionValue * (3.80265176 / 10000000000L);
			break;			
		case SECONDS:
			//convert from 
			break;
		case MINUTES:
			//convert from 		
			break;
		case HOURS:
			//convert from		
			break;
		case DAYS:
			//convert from 		
			break;
		case WEEKS: 
			//convert from		
			break;
		case MONTHS:
			//no need for conversion
			result = conversionValue;
			break;
		case YEARS:
			//convert from		
			break;
		case DECADES:
			//convert from decades to months
			result = conversionValue * 120;
			break;
		case CENTURIES:
			//convert from centuries to months
			result = conversionValue * 1200;
			break;
		}//switch
		return result;
	} //ConvertMonths

	private static double ConvertYears(int convertFrom, double conversionValue) {
		double result = 0;
		switch(convertFrom) {
		case MILLISECONDS:
			//convert from milliseconds to years
			result = conversionValue * (3.16887646 / 100000000000L);
			break;			
		case SECONDS:
			//convert from 
			break;
		case MINUTES:
			//convert from 		
			break;
		case HOURS:
			//convert from		
			break;
		case DAYS:
			//convert from 		
			break;
		case WEEKS: 
			//convert from		
			break;
		case MONTHS:
			//convert from	
			break;
		case YEARS:
			//no need for conversion
			result = conversionValue;	
			break;
		case DECADES:
			//convert from decades to years
			result = conversionValue * 10;
			break;
		case CENTURIES:
			//convert from centuries to years
			result = conversionValue * 100;
			break;
		}//switch
		return result;
	} //ConvertYears

	private static double ConvertDecades(int convertFrom, double conversionValue) {
		double result = 0;
		switch(convertFrom) {
		case MILLISECONDS:
			//convert from milliseconds to decades
			result = conversionValue * (3.16887646 / 1000000000000L);
			break;			
		case SECONDS:
			//convert from seconds to decades
			result = conversionValue * (3.16887646  / 1000000000);
			break;
		case MINUTES:
			//convert from 	
			result = conversionValue * (1.90132588 / 10000000);
			break;
		case HOURS:
			//convert from	
			result = conversionValue * (1.14079553 / 100000);
			break;
		case DAYS:
			//convert from 	
			result = conversionValue * 0.000273790926;
			break;
		case WEEKS: 
			//convert from	
			result = conversionValue * 0.00191653649;
			break;
		case MONTHS:
			//convert from	
			result = conversionValue * 0.00833333333;
			break;
		case YEARS:
			//convert from
			result = conversionValue * 0.1;
			break;
		case DECADES:
			//no need for conversion
			result = conversionValue;
			break;
		case CENTURIES:
			//convert from centuries to decades
			result = conversionValue * 10;
			break;
		}//switch
		return result;
	} //ConvertDecades

	private static double ConvertCenturies(int convertFrom, double conversionValue) {
		double result = 0;
		switch(convertFrom) {
		case MILLISECONDS:
			//convert from milliseconds to centuries
			result = conversionValue * (3.16887646 / 10000000000000L);
			break;			
		case SECONDS:
			//convert from seconds to centuries
			result = conversionValue * (3.16887646 / 10000000000L);
			break;
		case MINUTES:
			//convert from minutes to centuries
			result = conversionValue * (1.90132588 / 100000000);
			break;
		case HOURS:
			//convert from hours to centuries
			result = conversionValue * (1.14079553 / 1000000);
			break;
		case DAYS:
			//convert from days to centuries
			result = conversionValue * (2.73790926 / 100000);
			break;
		case WEEKS: 
			//convert from weeks to centuries
			result = conversionValue * 0.000191653649;
			break;
		case MONTHS:
			//convert from months to centuries
			result = conversionValue * 0.000833333333;
			break;
		case YEARS:
			//convert from	years to centuries
			result = conversionValue * 0.01;
			break;
		case DECADES:
			//convert from decades to centuries
			result = conversionValue * 0.1;
			break;
		case CENTURIES:
			//no need for conversion
			result = conversionValue;
			break;
		}//switch
		return result;
	} //ConvertCenturies
} //ConvertTime