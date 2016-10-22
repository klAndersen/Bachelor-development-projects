using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiPurpose {
    /// <summary>
    /// A class that auto-generates text. Made static to 
    /// get direct contact with the functions.
    /// </summary>
    internal static class AutoGenerate {

        #region PRINTING THE TEXT WITH NUMERIC VALUES
        /// <summary>
        /// Forwards the values, wether to be increased one-by-one 
        /// or increased by a jump value
        /// </summary>
        /// <param name="text">Text to be printed</param>
        /// <param name="start">Startnumber</param>
        /// <param name="end">End number</param>
        /// <param name="jump">Increasement of number</param>
        /// <returns>string</returns>
        internal static string printNumericText(string text, int start, int end, int jump) {
            string print = null;
            //is number to be increased one by one?
            if (jump == 0) { 
                print = numericText(text, start, end);
            } else { //text printed shall have increased jump
                print = jumpNumericText(text, start, end, jump);
            } //if (jump == 0)
            return print;
        } //printNumericText

        private static string numericText(string text, int start, int end) {
            //reset (previous) text		
            string print = ""; 
            for (int i = start; i <= end; i++) {
                print += text + i + "\n";
            } //for
            //return the text
            return print; 
        } //numericText

        private static string jumpNumericText(string text, int start, int end, int jump) {
            string print = ""; //reset (previous) text		
            for (int i = start; i <= end; i += jump) {
                print += text + i + "\n";
                //System.out.println(i);
            } //for
            //return the text
            return print; 
        } //jumpNumericText	
        #endregion

        #region PRINTING THE STARTING TEXT ONLY
        /// <summary>
        /// Returns (end - start) number of lines.
        /// Only 'text' is returned and generated X times.
        /// </summary>
        /// <param name="text">Text to be printed</param>
        /// <param name="start">Startnumber</param>
        /// <param name="end">End number</param>
        /// <returns>string</returns>
        internal static string printTextOnly(string text, int start, int end) {
            //reset (previous) text
            string print = ""; 
            for (int i = start; i <= end; i++) {
                print += text + "\n";
            } //for
            return print;
        } //printTextOnly
        #endregion

        #region PRINTING THE TEXT WITH START/END AND NUMERIC VALUES
        /// <summary>
        /// Prints the starttext, the number (from start to end) and then the endText
        /// Adds a newline at the end of each line
        /// </summary>
        /// <param name="startText">Text to be printed</param>
        /// <param name="start">Startnumber</param>
        /// <param name="end">End number</param>
        /// <param name="jump">Increasement of number</param>
        /// <param name="endText">The text to be added at the end</param>
        /// <returns>string</returns>
        internal static string printNumericStartEnd(string startText, int start, int end, int jump, string endText) {
            string print = "";
            //shall the printed text go from start to end?
            if (jump == 0) { 
                for (int i = start; i <= end; i++) {
                    print += startText + i + endText + "\n";
                } //for
            } else { //printed text is to have a numeric increase
                for (int i = start; i <= end; i += jump) {
                    print += startText + i + endText + "\n";
                } //for
            } //if (jump == 0)
            return print;
        } //printNumericStartEnd
        #endregion

        #region PRINTING THE TEXT ONLY WITH START/END
        /// <summary>
        /// Prints the startText and endText
        /// Adds a newline at the end of each line
        /// </summary>
        /// <param name="startText">Text to be printed</param>
        /// <param name="start">Startnumber</param>
        /// <param name="end">End number</param>
        /// <param name="endText">The text to be added at the end</param>
        /// <returns>string</returns>
        internal static string printTextStartEnd(string startText, int start, int end, string endText) {
            //reset (previous) text
            string print = ""; 
            for (int i = start; i <= end; i++) {
                print += startText + endText + "\n";
            } //for
            return print;
        } //printTextStartEnd
        #endregion
    } //AutoGenerate
} //namespace