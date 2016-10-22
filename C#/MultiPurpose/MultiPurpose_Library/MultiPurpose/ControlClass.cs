using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

namespace MultiPurpose {
    /// <summary>
    /// 
    /// </summary>
    public class ControlClass {
        Database db = new Database();
        /// <summary>
        /// The variable to be used to send and retrieve data
        /// from the ControlClass and other classes in the library
        /// </summary>
        public static readonly ControlClass controlInstance = new ControlClass();

        static ControlClass() {
        } //constructor

        #region AUTOGENERATE
        /// <summary>
        /// Returns (end - start) number of lines.
        /// Only 'text' is returned and generated X times.
        /// </summary>
        /// <param name="text">Text to be printed</param>
        /// <param name="start">Startnumber</param>
        /// <param name="end">End number</param>
        /// <returns>string</returns>
        public string printTextOnly(string text, int start, int end) {
            return AutoGenerate.printTextOnly(text,start,end);
        } //printTextOnly

        /// <summary>
        /// Forwards the values, wether to be increased one-by-one 
        /// or increased by a jump value
        /// </summary>
        /// <param name="text">Text to be printed</param>
        /// <param name="start">Startnumber</param>
        /// <param name="end">End number</param>
        /// <param name="jump">Increasement of number</param>
        /// <returns>string</returns>
        public string printNumericText(string text, int start, int end, int jump) {
            return AutoGenerate.printNumericText(text, start, end, jump);
        } //printNumericText

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
        public string printNumericStartEnd(string startText, int start, int end, int jump, string endText) {
            return AutoGenerate.printNumericStartEnd(startText, start, end, jump, endText);
        } //printNumericStartEnd

        /// <summary>
        /// Prints the startText and endText
        /// Adds a newline at the end of each line
        /// </summary>
        /// <param name="startText">Text to be printed</param>
        /// <param name="start">Startnumber</param>
        /// <param name="end">End number</param>
        /// <param name="endText">The text to be added at the end</param>
        /// <returns>string</returns>
        public string printTextStartEnd(string startText, int start, int end, string endText) {
            return AutoGenerate.printTextStartEnd(startText,start, end,endText);
        } //printTextStartEnd
        #endregion

        #region CREATE README

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
        public void saveReadMeToFile(string filePath, string title, string vNo, string content,
                                    bool incAsteric, bool overwrite, bool addVersion) {
            CreateReadMe.saveToFile(filePath, title, vNo, content, incAsteric, overwrite, addVersion);
        } //saveReadMe

        /// <summary>
        /// Creates an example text to display to the user
        /// </summary>
        /// <returns>string</returns>
        public string returnReadMeExample() {
            return CreateReadMe.createExample();
        } //returnExample

        /// <summary>
        /// Returns a line of asterics (*)
        /// </summary>
        /// <returns>string</returns>
        public string createStars() {
            return CreateReadMe.createStars();
        } //createStars

        #endregion

        #region DATABASE
        /// <summary>
        /// Opens a connection to the MySQL server
        /// </summary>
        /// <param name="user">The users username</param>
        /// <param name="pwd">The users password</param>
        /// <param name="port">The port to connect to</param>
        /// <param name="server">The server to connect to</param>
        public bool connectToMySQL(string user, string pwd, string port, string server) {
            return db.connectToDB(server, port, user, pwd);
        } //connectToMySQL

        /// <summary>
        /// Connect to another database
        /// </summary>
        /// <param name="query">Query containing the change of database</param>
        public void changeDatabase(string query) {
            db.changeDatabase(query);
        } //changeDatabase

        /// <summary>
        /// Returns a List&lt;Objects&gt; containg the values from the query
        /// </summary>
        /// <param name="query">The query to be run</param>
        /// <returns>List&lt;Objects&gt;</returns>
        public List<Object> returnListOfObjects(string query) {
            return db.returnListOfObjects(query);
        } //returnListOfObjects

        /// <summary>
        /// Returns a DataTable filled with the result from the 
        /// query
        /// </summary>
        /// <param name="query">The query to be excecuted</param>
        /// <returns>DataTable</returns>
        public DataTable returnDataTable(string query) {
            return db.returnDataTable(query);
        } //returnDataTable

        /// <summary>
        /// Get the database status
        /// </summary>
        /// <returns>string</returns>
        public string getDatabaseStatus() {
            return db.getStatus();
        } //getDatabaseStatus

        /// <summary>
        /// Set the status of database
        /// </summary>
        /// <param name="status">The status to be set</param>
        public void setDataBaseStatus(string status) {
            db.setStatus(status);
        } //setDataBaseStatus

        /// <summary>
        /// Closes the connection to the MySQL server
        /// </summary>
        public void disconnectMySQL() {
            db.closeConnection();
        } //disconnectMySQL
        #endregion
    } //ConstrolClass
} //namespace