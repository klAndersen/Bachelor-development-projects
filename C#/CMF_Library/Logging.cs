/*
 * CleanMyFolder - a program for scanning and listing up files that 
 * takes up a lot of space on the harddrive, and gives the user the choice 
 * to move or delete the folders and files.
 * Copyright (C) 2012  Knut Lucas Andersen
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Text;
using System.IO;

namespace CMF_Library {
    /// <summary>
    /// This is the logging class and handles everything thats connected to logging.
    /// Logging class logs all from scan started to events as moving, deleting 
    /// and other operations the application does.
    /// </summary>
    internal static class Logging {
        #region VARIABLES
        //the name of the log file
        private const string LOG_FILENAME = "Log.txt";
        //the title for the scan started
        private const string START_SCAN = "STARTED SCANNING: ";
        //titles connected to operations for moving and deleting files
        private const string MOVE_FILES = "FOLLOWING FILES WERE MOVED FROM: ";
        private const string FILES_RECYCLE_BIN = "FOLLOWING FILES WERE MOVED TO RECYCLE BIN";
        private const string DELETE_FILES_PERMANENTLY = "FOLLOWING FILES WERE DELETED PERMANENTLY";
        //titles connected to operations for moving and deleting
        private const string MOVE_FOLDERS = "FOLDER AND ITS CONTENT WAS MOVED FROM: ";
        private const string FOLDERS_RECYCLE_BIN = "FOLDER AND ITS CONTENT WAS MOVED TO RECYCLE BIN";
        private const string DELETE_FOLDERS_PERMANENTLY = "FOLDER AND ITS CONTENT WAS DELETED PERMANENTLY";
        //the content to be written to file
        private static string logContent;
        //the path the operation is progressing
        private static string operationPath;
        //the length of the title to be logged
        private static int titleLength;
        #endregion

        #region WRITE METHODS
        /// <summary>
        /// Logging method writes to the log file. It adds symbols, title and the 
        /// current date and time the operation started/ended.
        /// </summary>
        /// <param name="operation">The value for the operation in progress 
        /// (0 = Scan, 1 = Move files, 2 = Move files to recycle bin, 
        /// 3 = Delete files permanently, 4 = Move folders, 
        /// 5 = Move folders to Recycle bin, 6 = Delete folders permanently)</param>
        /// <param name="dateDescription">The text to be set before the date which appears in the log</param>
        internal static void writeStatusToLog(int operation, string dateDescription) {
            try {
                //get the path to where the log file is
                string logFile = getLogsPath();
                //get the title
                string logText = returnTitle(operation);
                //add the date and time
                logText += dateDescription + DateTime.Now.ToString();
                //is the operation being logged other then a scan?
                if (operation > 0) { 
                    logText += "\r\n\r\n"; //add 2x newline
                } else { //operation is a scan
                    logText += "\r\n"; //add one newline
                } //if (operation > 0)
                //write the status to the log
                writeLog(logFile, logText); 
            } catch (Exception ex) {
                throw ex;
            } //try/catch
        } //writeStatusToLog

        /// <summary>
        /// This method writes content to the logfile. 
        /// The content to be written is set in the method setContent(string content).
        /// </summary>
        internal static void writeProgressToLog() {
            try {
                //get the log files location
                string logFile = getLogsPath(); 
                //add the content
                string logText = getContent() + "\r\n"; 
                //write the content to log
                writeLog(logFile, logText);
            } catch (Exception ex) {
                throw ex;
            } //try/catch
        } //writeProgressToLog

        private static void writeLog(string logFile, string logText) {
            try {
                string fileContent = "";
                //does the log file exists?
                if (File.Exists(logFile)) {
                    //create a reader to read the log
                    StreamReader readLog = new StreamReader(logFile);
                    //get the content in the log
                    fileContent = readLog.ReadToEnd();
                    //close the reader
                    readLog.Close();
                } //if (!(File.Exists(logFile)))
                //create a writer to write content to log
                StreamWriter writeLog = new StreamWriter(logFile);
                //write the newest content to log
                writeLog.Write(logText);
                //write the old content back to the log
                writeLog.Write(fileContent);
                //close the writer
                writeLog.Close();
            } catch (Exception ex) {
                throw ex;
            } //try/catch
        } //writeLog
        #endregion

        #region CREATE TITLE
        private static string createTitle(string title) {
            string returnTitle;
            string sign = "-";
            returnTitle = "\r\n"; //add a newline
            //add symbols
            returnTitle += addSymbols(sign);
            //add the symbols and title
            returnTitle += title + "\r\n";
            //add symbols
            returnTitle += addSymbols(sign);
            return returnTitle;
        } //createTitle

        private static string addSymbols(string sign) {
            string symbols = "";
            //add enough symbols to match the length of the title string
            for (int i = 0; i < getTitleLength(); i++) {
                symbols += sign;
            } //for
            //add newline
            symbols += "\r\n";
            return symbols;
        } //addSymbols

        /// <summary>
        /// Returns the title. The operation is an int telling which 
        /// operation was started.
        /// </summary>
        /// <param name="operation">
        /// 0 = Scan, 1 = Move files, 2 = Move files to recycle bin, 
        /// 3 = Delete files permanently, 4 = Move folders, 
        /// 5 = Move folders to Recycle bin, 6 = Delete folders permanently
        /// </param>
        /// <returns>string</returns>
        private static string returnTitle(int operation) {
            string title = "";
            string titleContent = "";
            switch (operation) {
                case 0: //the operation is scan
                    title = START_SCAN + getOperationPath();
                    setLengthTitle(title.Length);
                    titleContent += createTitle(title);
                    break;
                case 1: //the operation is moving files
                    title = MOVE_FILES + getOperationPath();
                    setLengthTitle(title.Length);
                    titleContent += createTitle(title);
                    break;
                case 2: //the operation is deletion of files (Recycle bin)
                    setLengthTitle(FILES_RECYCLE_BIN.Length);
                    titleContent += createTitle(FILES_RECYCLE_BIN);
                    break;
                case 3: //the operation is deletion of files(permanently)
                    setLengthTitle(DELETE_FILES_PERMANENTLY.Length);
                    titleContent += createTitle(DELETE_FILES_PERMANENTLY);
                    break;
                case 4: //the operation is moving folders
                    title = MOVE_FOLDERS + getOperationPath();
                    setLengthTitle(title.Length);
                    titleContent += createTitle(title);
                    break;
                case 5: //the operation is deletion of folders (Recycle bin)
                    setLengthTitle(FOLDERS_RECYCLE_BIN.Length);
                    titleContent += createTitle(FOLDERS_RECYCLE_BIN);
                    break;
                case 6: //the operation is deletion of folders (permanently)
                    setLengthTitle(DELETE_FOLDERS_PERMANENTLY.Length);
                    titleContent += createTitle(DELETE_FOLDERS_PERMANENTLY);
                    break;
            } //switch
            return titleContent;
        } //returnTitle
        #endregion
        
        #region GET METHODS
        /// <summary>
        /// Gets the location of where the log file exists
        /// </summary>
        /// <returns>string</returns>
        private static string getLogsPath() {
            return Environment.CurrentDirectory + @"\" + LOG_FILENAME;
        } //getLogsPath

        private static int getTitleLength() {
            return titleLength;
        } //getTitleLength

        /// <summary>
        /// Gets the content to be written to the logfile
        /// </summary>
        /// <returns>string</returns>
        private static string getContent() {
            return logContent;
        } //getContent

        /// <summary>
        /// Gets the location that currently is being processed 
        /// (moved, deleted, scanned).
        /// </summary>
        /// <returns>string</returns>
        private static string getOperationPath() {
            return operationPath;
        } //getOperationPath
        #endregion

        #region SET METHODS
        /// <summary>
        /// Sets the current text to be written to the logfile.
        /// Can be one line or multiple lines seperated by "\r\n".
        /// </summary>
        /// <param name="logContent">Text to be written to log</param>
        internal static void setContent(string logContent) {
            Logging.logContent = logContent;
        } //setContent

        /// <summary>
        /// Sets the location that currently is being processed (moved, deleted, scanned).
        /// Note: This is the path from which the operation started.
        /// </summary>
        /// <param name="operationPath">The path currently being processed</param>
        internal static void setOperationPath(string operationPath) {
            Logging.operationPath = operationPath;
        } //setOperationPath

        private static void setLengthTitle(int titleLength) {
            Logging.titleLength = titleLength;
        } //setLengthTitle
        #endregion
    } //Logging
} //namespace