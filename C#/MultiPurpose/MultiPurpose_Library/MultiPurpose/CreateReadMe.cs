using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//imports
using System.IO;

namespace MultiPurpose {
    internal static class CreateReadMe {
        private const int NUMBER_OF_STARS = 78;
        
        /// <summary>
        /// Creates and saves the added content to a textfile.
        /// </summary>
        /// <param name="filePath">The path where the ReadMe is to be saved</param>
        /// <param name="title">The ReadMe's title</param>
        /// <param name="vNo">The version number</param>
        /// <param name="content">The ReadMe's content</param>
        /// <param name="incAsteric">Is asteric to be added?</param>
        /// <param name="overwrite">Is file to be overwritten (if exists)?</param>
        /// <param name="addVersion">Is version number to be added?</param>
        internal static void saveToFile(string filePath, string title, string vNo, string content,
                                    bool incAsteric, bool overwrite, bool addVersion) {
		    bool saved = false;
		    try {		
			    //shall excisting file be overwritten?
                if (overwrite) { 
				    //start saving progress
                    saved = writeToFile(filePath, title, vNo, content, incAsteric, addVersion);
			    } else { //create a new file
                    //does a file with same name exist?
                    if (File.Exists(filePath)) { 
					    throw
                            new Exception("A file by that name already exists.\n"
							    + "To overwrite select 'Overwrite file?', or change filename.\n"
							    + "Current changes has not been saved.");
				    } else { //no file exist with this name
                        saved = writeToFile(filePath, title, vNo, content, incAsteric, addVersion);
				    } //if (fileExcists)
			    } //if (overwrite)
			    //was the document saved?
                if (!saved) { 
                    throw 
                        new Exception("Couldn't save the document.");
			    } //if (!saved)
		    } catch(Exception ex) {
			    throw ex;
		    } //try/catch
        } //saveToFile

	    private static bool writeToFile(string filename, string title, string vNo, string content, bool asteric, bool addVersion) {
		    bool saved = true;		
		    try {
			    //prepare to write to file
                StreamWriter writeFile = new StreamWriter(filename);
                string[] contentLines = content.Split('\n');
                //is the title to be surrounded by asteric signs?
                if (asteric) {
                    //write a row of stars
                    writeFile.WriteLine(createStars());
                    //write stars + title + stars
                    writeFile.WriteLine(getTitleLength(title));
                    //write a row of stars
                    writeFile.WriteLine(createStars());
                } else { //write title only
                    //write only the title
                    writeFile.WriteLine(title); 
			    } //if (asteric)
                //is version no. to be added?			
                if (addVersion) {
                    writeFile.WriteLine("Current Version: " + vNo);
                    //add an empty line to the file
                    writeFile.WriteLine();
                    //as long as there is text/words
                    foreach (string word in contentLines) {
                        //write the line to the txt-file
                        writeFile.WriteLine(word); 
				    }//while
			    } else { //version no. not to be added
                    foreach (string word in contentLines) {
                        //write the line to the txt-file
                        writeFile.WriteLine(word); 		
				    }//while
			    } //if (addVersion)
                //close the writer
                writeFile.Close(); 
		    } catch (Exception ex) {
			    saved = false;
                throw ex;
		    } //try/catch
		    return saved;
	    } //writeToFile

        #region TITLE AND ASTERICS

	    private static string getTitleLength(string title) {
		    string editTitle;
		    //number of stars subtracted by number of elements in the title-string		
            int length = NUMBER_OF_STARS - title.Length;
		    //divide the length by 2 (half amount of stars on each side of title)		
            length = length / 2; 
		    //fill editTitle with stars
            editTitle = addDynamicStars(length); 
		    //add title after stars followed by new row of stars
            editTitle += title + addDynamicStars(length);
            //return the edited title
            return editTitle; 
	    } //getTitleLength

        private static string addDynamicStars(int numStars) {
		    string asteric = "";
		    for (int i = 0; i < numStars; i++) {
			    asteric += "*";
		    } //for
		    return asteric;
	    } //addDynamicStars

        /// <summary>
        /// Creates a line of asterics (*)
        /// </summary>
        /// <returns>string</returns>
        internal static string createStars() {
		    string asteric = "*";
		    for (int i = 0; i < NUMBER_OF_STARS; i++) {
			    asteric += "*";
		    } //for		
		    return asteric;
	    } //createStars

        #endregion

        /// <summary>
        /// Creates an example text to display to the user
        /// </summary>
        /// <returns>string</returns>
        internal static string createExample() {
		    string title = "";
		    string txt = "";
		    title = createStars();
		    title += "\n" + getTitleLength("Example") + "\n";
		    title += createStars();
            txt = "\nCurrent Version: 1.5 \n\nThis is an example text to show how it will look with asteric\n";
		    txt += createStars();
		    txt += "\nThis is a line coming after the 'Insert asterics(*)' is used";
		    return title + txt;
	    } //createExample
    } //CreateReadMe
} //namespace