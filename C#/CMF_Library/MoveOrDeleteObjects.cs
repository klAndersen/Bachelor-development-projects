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
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
//importing this to be able to move/delete folders and files
//and show progress dialogs when files/folders are moved/deleted
using Microsoft.VisualBasic.FileIO;

namespace CMF_Library {
    /// <summary>
    /// This class handles operations related to moving and deleting 
    /// folders and files
    /// </summary>
    internal static class MoveOrDeleteObjects {

        #region MOVE TO RECYCLE BIN
        /// <summary>
        /// This method moves the file(s) to the recycle bin. It shows only 
        /// error messages and does not ask if user wants to delete the file(s) 
        /// since this is already chosen when this method is called.
        /// </summary>
        /// <param name="filePath">The path to the file thats to be deleted</param>
        internal static void moveFilesToRecycleBin(string filePath) {
            try {
                //moves the file to the recycle bin, showing only errors
                FileSystem.DeleteFile(filePath, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
            } catch (Exception ex) {
                throw ex;
            } //try/catch
        } //moveFilesToRecycleBin

        /// <summary>
        /// This method moves the folder(s) to the recycle bin. It shows only 
        /// error messages and does not ask if user wants to delete the folder(s) 
        /// since this is already chosen when this method is called.
        /// </summary>
        /// <param name="folderPath">The path to the folder thats to be deleted</param>
        internal static void moveFoldersToRecycleBin(string folderPath) {
            try {
                //moves the folder to the recycle bin, showing only errors
                FileSystem.DeleteDirectory(folderPath, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
            } catch (Exception ex) {
                throw ex;
            } //try/catch
        } //moveFoldersToRecycleBin
        #endregion

        #region DELETE PERMANENTLY
        /// <summary>
        /// This method deletes the file(s) permanently. It shows only 
        /// error messages and does not ask if user wants to delete the file(s) 
        /// since this is already chosen when this method is called.
        /// </summary>
        /// <param name="filePath">The path where the file(s) exists</param>
        internal static void deleteFilePermanently(string filePath) {
            try {
                FileSystem.DeleteFile(filePath, UIOption.OnlyErrorDialogs, RecycleOption.DeletePermanently);
            } catch (Exception ex) {
                throw ex;
            } //try/catch
        } //deleteFilePermanently

        /// <summary>
        /// This method deletes the folder(s) permanently. It shows only 
        /// error messages and does not ask if user wants to delete the folder(s) 
        /// since this is already chosen when this method is called.
        /// </summary>
        /// <param name="folderPath">The path where the folder(s) exists</param>
        internal static void deleteFolderPermanently(string folderPath) {
            try {
                FileSystem.DeleteDirectory(folderPath, UIOption.OnlyErrorDialogs, RecycleOption.DeletePermanently);
            } catch (Exception ex) {
                throw ex;
            } //try/catch
        } //deleteFolderPermanently
        #endregion
        
        #region MOVE FILES
        /// <summary>
        /// This method moves a file. To move multiple files a surrounding loop
        /// will be needed.
        /// </summary>
        /// <param name="pathFrom">The path where the file(s) exists</param>
        /// <param name="pathTo">The path the file(s) is to be moved to</param>
        internal static void moveFile(string pathFrom, string pathTo) {
            try {
                string fileName = Path.GetFileName(pathFrom);
                pathTo += @"\" + fileName;
                FileSystem.MoveFile(pathFrom, pathTo, UIOption.AllDialogs);
            } catch (Exception ex) {
                throw ex;
            } //try/catch
        } //moveFile

        private static void moveFilesInList(string pathTo, string parentFolder, List<Files> fileList) {
            try {
                foreach (Files file in (from f in fileList
                                        where f.getParentFolder() == parentFolder
                                        select f)) {
                    //set the location where the file existed
                    string pathFrom = file.getParentFolder() + @"\" + file.getName() + file.getExtension();
                    //get the name and filetype of the current file and get where it shall be moved to
                    string logContent = "Filename: " + file.getName() + file.getExtension() + " was moved to: " + pathTo;
                    //set the content to be logged
                    Logging.setContent(logContent);
                    //move the file(s)
                    moveFile(pathFrom, pathTo);
                    //log that the file was moved
                    Logging.writeProgressToLog();
                } //foreach
            } catch (Exception ex) {
                throw ex;
            } //try/catch
        } //moveFilesInList
        #endregion

        #region MOVE FOLDERS
        /// <summary>
        /// This method moves the whole folder, with all content 
        /// (files and subfolders) to a new location.
        /// </summary>
        /// <param name="pathFrom">The path where the folder exists</param>
        /// <param name="pathTo">The path the folder is to be moved to</param>
        internal static void moveWholeFolder(string pathFrom, string pathTo) {
            try {
                //get the name of the folder
                string folderName = Path.GetFileName(pathFrom);
                //add the folders name to the location its being moved to
                pathTo += @"\" + folderName;
                //move the folder and its content
                FileSystem.MoveDirectory(pathFrom, pathTo, UIOption.AllDialogs);
            } catch (Exception ex) {
                throw ex;
            } //try/catch
        } //moveWholeFolder

        /// <summary>
        /// This method moves the folder and its content (subfolders and files). 
        /// This only moves the content shown "on-screen" based on the scan criterias when 
        /// scan started. Files not matching the scan criterias are left at its current location.
        /// </summary>
        /// <param name="pathFrom">The folder were the files and subfolders are to be moved from</param>
        /// <param name="pathTo">The location were the files and subfolders are to be moved to</param>
        /// <param name="subFolderList">List&lt;Folders&gt; with subfolders (do not include the point to move from)</param>
        /// <param name="moveFileList">List&lt;Files&gt; with files to be moved</param>
        internal static void moveFoldersContent(string pathFrom, string pathTo, List<Folders> subFolderList, List<Files> moveFileList) {
            try {
                //get the name of the parent folder
                string parent = Path.GetFileName(pathFrom);
                //create a string containing the path where the folders are to be created
                string createFolderAt = pathTo + @"\" + parent;
                //create the new folder
                MoveOrDeleteObjects.createNewFolders(createFolderAt);
                //move the files to the new folder
                moveFilesInList(createFolderAt, pathFrom, moveFileList);
                //move the subfolders to the new folder
                moveSubFolders(createFolderAt, pathFrom, subFolderList, moveFileList);
            } catch (Exception ex) {
                throw ex;
            } //try/catch
        } //moveFoldersContent

        private static void moveSubFolders(string pathTo, string parentFolder, List<Folders> subFolderList, List<Files> moveFileList) {
            try {
                foreach (Folders subFolder in (from sub in subFolderList
                                               where sub.getParentFolder() == parentFolder
                                               select sub)) {
                    //get the name of the child folder
                    string child = Path.GetFileName(subFolder.getName());
                    //create the string for the location to create the new folder
                    string createFolderAt = pathTo + @"\" + child;
                    //get total files (both scanned and total in folder)
                    int numFilesScanned = ControlClass.controlClass.countFilesScanned(subFolder.getName(), subFolderList);
                    int numFilesTotal = ControlClass.controlClass.countTotalNumberOfFiles(subFolder.getName(), subFolderList);
                    //is the number of files and the total number including files in subfolders equal to the scanned amount?
                    if (numFilesTotal == numFilesScanned && 
                            subFolder.getScannedNumberOfFiles() == subFolder.getTotalNumberOfFiles()) {
                        //the content to be logged
                        string logContent = "Folder at " + subFolder.getName()  + " was moved to " + pathTo;
                        ControlClass.controlClass.setContent(logContent);
                        //move the whole folder and its content
                        moveWholeFolder(subFolder.getName(), pathTo);
                        //log the folder that was moved
                        ControlClass.controlClass.writeProgressToLog();
                    } else { //only parts of the chosen folders content is being moved
                        //create the folder
                        MoveOrDeleteObjects.createNewFolders(createFolderAt);
                        //move the files to the new folder
                        moveFilesInList(createFolderAt, subFolder.getName(), moveFileList);
                        //move the folders subfolders
                        moveSubFolders(createFolderAt, subFolder.getName(), subFolderList, moveFileList);
                    } //if (numFilesTotal == numFilesScanned ...)
                } //foreach
            } catch (Exception ex) {
                throw ex;
            } //try/catch
        } //moveSubFolders

        private static void createNewFolders(string createPath) {
            try {
                //create a new folder at the chosen path
                Directory.CreateDirectory(createPath);
            } catch (Exception ex) {
                throw ex;
            } //try/catch
        } //createNewFolders
        #endregion
    } //MoveOrDeleteObjects
} //namespace