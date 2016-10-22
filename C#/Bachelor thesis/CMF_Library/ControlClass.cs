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
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;

namespace CMF_Library {
    /// <summary>
    /// The class "communicating" between the userinterface and the library.
    /// </summary>
    public class ControlClass {
        #region VARIABLES
        /// <summary>
        /// Readonly object of the ControlClass used as "communicator"
        /// </summary>
        public static readonly ControlClass controlClass = new ControlClass();
        //variable related to the current progress in scan
        private static string progress;
        //constant for avoiding a folder which contains offline synchronisation files
        private const string CSC_FOLDER = @"C:\Windows\CSC";
        //a list related to errors that occurs under scanning/reading folders/files
        private List<string> exceptionList = new List<string>();
        //variables related to folders/directories
        private long currentFileSize; //incremental variable for the total filesize in a folder
        private int numFiles; //incremental variabel for the total number of files in a folder
        private static List<Folders> foldersRead = new List<Folders>();
        //variables related to files
        private List<Files> filesRead = new List<Files>();
        //variables for size convertion
        private Enum sizeParameter; //variable for what size to display
        private double sizeConverted; //variable for the converted size
        #endregion

        #region CONSTRUCTOR AND DESTRUCTOR
        private ControlClass() {
        } //constructor

        /// <summary>
        /// The destructor of the ControlClass. Empties all this class lists.
        /// </summary>
        ~ControlClass() {
            clearLists();
        } //destructor

        private void clearLists() {
            exceptionList.Clear();
            foldersRead.Clear();
            filesRead.Clear();
        } //clearLists
        #endregion

        #region START SCAN METHODS
        /// <summary>
        /// Starts a fullscan, without any criterias. Loops through the 
        /// folders, then the files and finally calculates the size of the 
        /// folders in the list (does not include hidden or system files).
        /// </summary>
        /// <param name="path">The directory to be scanned</param>
        /// <param name="bw">Active backgroundworker</param>
        /// <param name="scanSub">Scan subfolders?</param>
        public void startFullScan(string path, BackgroundWorker bw, bool scanSub) {
            try {
                clearLists();
                //create an array over the folders to be scanned
                string[] directories = Directory.GetDirectories(path);
                //adds the root folder
                addOneFolder(path);
                //is subfolders to be included in scan?
                if (scanSub) {
                    //scans multiple folders and sub-folders
                    recursiveFunctionFolders(path, bw);
                } //if (scanSub)
                //start the filescan
                recursiveFunctionFiles(bw);
                //set the size of the folders
                setSizeOfFolders(bw);
            } catch (UnauthorizedAccessException ex) {
                //record that this folder is not accessible
                recordExceptions(ex.Message);
            } catch (Exception ex) {
                //record the folder that caused the exception
                recordExceptions(ex.Message);
            } //try/catch
        } //startFullScan

        /// <summary>
        /// This method starts a specified scan. It first loops through the folders
        /// then the files matching searchcriteria and finally calculates the size 
        /// of the scanned folders (does not include hidden or system files).
        /// </summary>
        /// <param name="path">The directory to be scanned</param>
        /// <param name="searchpattern">Filetypes to be searced for</param>
        /// <param name="bw">Object of the BackgroundWorker from the userinterface</param>
        /// <param name="fileSize">The minimum file size to scan for</param>
        /// <param name="criterie1">Value for searching for date (0 = older then, 1 = newer then, -1 = no date scan)</param>
        /// <param name="criterie2">Value for datecriteria (0 = CreationTime, 1 = Last modified, 2 = Last accessed)</param>
        /// <param name="date">The date value to search for</param>
        /// <param name="scanSub">Scan subfolders?</param>
        public void startSpecifiedScan(string path, string[] searchpattern, BackgroundWorker bw, long fileSize,
                                    int criterie1, int criterie2, DateTime date, bool scanSub) {
            try {
                clearLists();
                //adds the root folder
                addOneFolder(path);
                //is subfolders to be included in scan?
                if (scanSub) {
                    //scan sub-folders
                    recursiveFunctionFolders(path, bw);
                } //if (scanSub)
                //start the filescan
                for(int i = 0; i < searchpattern.Length; i++) {
                    recursiveFunctionFiles(searchpattern[i], bw, fileSize, criterie1, criterie2, date);
                } //foreach
                //set the size of the folders
                setSizeOfFolders(bw);
            } catch (UnauthorizedAccessException ex) {
                //record that this folder is not accessible
                recordExceptions(ex.Message);
            } catch (Exception ex) {
                //record the folder that caused the exception
                recordExceptions(ex.Message);
            } //try/catch
        } //startSpecifiedScan

        private void addOneFolder(string path) {
            //get the current folders information
            DirectoryInfo dirInfo = new DirectoryInfo(path); 
            int numberOfFiles = 0;
            foreach (FileInfo fileInfo in dirInfo.GetFiles()) {
                //is the file(s) this folder contains a system or hidden file?
                if (((fileInfo.Attributes & FileAttributes.System) != FileAttributes.System)
                                    && ((fileInfo.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)) {
                    //increase number of files
                    numberOfFiles++;
                } //if (((fileInfo.Attributes & FileAttributes.System)
            } //foreach
            //create a new Folder object - parent set to null since this has no parent ("root" for search)
            Folders folders = new Folders(dirInfo.FullName, 0, false, false, dirInfo.CreationTime, dirInfo.LastWriteTime,
                                            dirInfo.LastAccessTime, null, numberOfFiles, 0);
            //record that the folder were accessed successfully
            recordSuccessFolderAccess(folders);
        } //addOneFolder
        #endregion

        #region RECURSE THROUGH FOLDERS AND FILES
        private void recursiveFunctionFolders(string path, BackgroundWorker bw) {
            //is the scan cancelled?
            if (bw.CancellationPending) {
                bw.CancelAsync();
            } else { //continue scanning
                DirectoryInfo dirInfo;
                try {
                    foreach (string dir in Directory.GetDirectories(path)) {
                        //get the current folders information
                        dirInfo = new DirectoryInfo(dir);
                        //is the current directory a systemfolder, hidden or the CSC folder?
                        if (((dirInfo.Attributes & FileAttributes.System) != FileAttributes.System)
                                && ((dirInfo.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                            && !dirInfo.FullName.Equals(CSC_FOLDER)) {
                            //get the number of files this contains
                            int numberOfFiles = dirInfo.GetFiles().Length;
                            //create a new Folders object
                            Folders folders = new Folders(dirInfo.FullName, 0, false, false, dirInfo.CreationTime, 
                                            dirInfo.LastWriteTime, dirInfo.LastAccessTime, path, numberOfFiles, 0);
                            //enable reports
                            bw.ReportProgress(0);
                            //set new progress text
                            progress = "Scanning folders: " + path;
                            setProgress(progress);
                            //record that the folder were accessed successfully
                            recordSuccessFolderAccess(folders);
                            //scan the subfolders
                            recursiveFunctionFolders(dir, bw);
                        } //if (((dirInfo.Attributes & FileAttributes.System) ...)
                    } //foreach
                } catch (UnauthorizedAccessException ex) {
                    //record that this folder is not accessible
                    recordExceptions(ex.Message);
                } catch (Exception ex) {
                    //record the folder that caused the exception
                    recordExceptions(ex.Message);
                } //try/catch
            } //if (bw.CancellationPending)
        } //recursiveFunctionFolders

        private void recursiveFunctionFiles(BackgroundWorker bw) {
            //get the list of folders
            List<Folders> folderList = returnListOfFolders();
            try {
                //variable for the size of files the folder contains
                long size = 0;
                //variable for the number of files the folder contains
                int numScanFiles = 0;
                //loop through the folders thats been scanned
                foreach (Folders folder in folderList) {
                    //is the scan cancelled?
                    if (bw.CancellationPending) {
                        bw.CancelAsync();
                    } else { //continue to scan files
                        //initialize size for "new round"
                        setCurrentFileSize(0);
                        //initalize number of files for "new round"
                        setNumFiles(0);
                        //get the files
                        foreach (string file in Directory.GetFiles(folder.getName())) {
                            //get the currents file information
                            FileInfo fileInfo = new FileInfo(file);
                            //is the current file a systemfile or hidden?
                            if (((fileInfo.Attributes & FileAttributes.System) != FileAttributes.System)
                                    && ((fileInfo.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)) {
                                //enables reports
                                bw.ReportProgress(0);
                                progress = "Scanning files: " + folder.getName() + fileInfo.Name;
                                setProgress(progress);
                                //add the file to the list
                                addFiles(folder, fileInfo);
                            } //if (((fileInfo.Attributes & FileAttributes.System ...)
                        } //inner foreach
                        //get the number of files
                        numScanFiles = getNumFiles();
                        //set the scanned number of files this folder contains
                        folder.setScannedNumberOfFiles(numScanFiles);
                        //add this files size
                        size = getCurrentFileSize();
                        //set the folder size based on the files it currently contains
                        folder.setSize(size); 
                    } //if (bw.CancellationPending)
                } //outer foreach
            } catch (UnauthorizedAccessException ex) {
                //record the file that failed to be accessed
                recordExceptions(ex.Message);
            } catch (Exception ex) {
                //record the exception
                recordExceptions(ex.Message);
            } //try/catch
        } //recursiveFunctionFiles

        private void recursiveFunctionFiles(string searchpattern, BackgroundWorker bw, long fileSize, int criterie1, int criterie2, DateTime date) {
            //get the list of folders
            List<Folders> folderList = returnListOfFolders();
            try {
                //variable for the size of files the folder contains
                long size = 0;
                //variable for the number of files the folder contains
                int numScanFiles = 0;
                //loop through the folders thats been scanned
                foreach (Folders folder in folderList) {
                    //is the scan cancelled?
                    if (bw.CancellationPending) {
                        bw.CancelAsync();
                    } else { //continue to scan files
                        //initialize size for "new round"
                        setCurrentFileSize(0);
                        //initalize number of files for "new round"
                        setNumFiles(0);
                        //get the files
                        loopThroughFiles(folder, searchpattern, fileSize, criterie1, criterie2, date, bw);
                        //get the number of files
                        numScanFiles = getNumFiles();
                        //set the scanned number of files this folder contains
                        folder.setScannedNumberOfFiles(numScanFiles);
                        //add this files size
                        size = getCurrentFileSize();
                        //add the folders size
                        size += folder.getSize();
                        //set the new size for the folder
                        folder.setSize(size); 
                    } //if (bw.CancellationPending)
                } //outer foreach
            } catch (UnauthorizedAccessException ex) {
                //record the file that failed to be accessed
                recordExceptions(ex.Message);
            } catch (Exception ex) {
                //record the exception
                recordExceptions(ex.Message);
            } //try/catch
        } //recursiveFunctionFiles

        private void loopThroughFiles(Folders folder, string searchpattern, long fileSize, int criterie1, int criterie2, DateTime date, BackgroundWorker bw) {
            //loop through the files of the current folder
            foreach (string file in Directory.GetFiles(folder.getName(), searchpattern)) {
                //get the currents file information
                FileInfo fileInfo = new FileInfo(file);
                //is the current file a systemfile or hidden?
                if (((fileInfo.Attributes & FileAttributes.System) != FileAttributes.System)
                        && ((fileInfo.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)) {
                    //is this file bigger then the size scanned for?
                    if (fileInfo.Length > fileSize) {
                        //check this files date
                        checkDate(folder, fileInfo, criterie1, criterie2, date);
                        //enables reports
                        bw.ReportProgress(0);
                        progress = "Scanning files: " + folder.getName() + fileInfo.Name;
                        setProgress(progress);
                    } //if (fileInfo.Length > fileSize)
                } //if (((fileInfo.Attributes & FileAttributes.System) ...)
            } //foreach
        } //loopThroughFiles

        private void checkDate(Folders folder, FileInfo fileInfo, int criterie1, int criterie2, DateTime date) {
            //is date included in scan?
            if (criterie1 == -1) {
                //add the file to the list
                addFiles(folder, fileInfo);
            } else if (criterie1 == 0) { //is the scan looking for files older then?
                dateOlderThen(fileInfo, folder, criterie2, date);
            } else if (criterie1 == 1) { //is the scan looking for files newer then?
                dateNewerThen(fileInfo, folder, criterie2, date);
            } //if (criterie1 == -1)
        } //checkDate

        private void dateOlderThen(FileInfo fileInfo, Folders folder, int criteria, DateTime date) {
            //is the scan looking for date of creation?
            if (criteria == 0) {
                if (fileInfo.CreationTime.Date < date.Date) {
                    //add the file to the list
                    addFiles(folder, fileInfo);
                } //if (fileInfo.CreationTime.Date < date.Date)
            } else if (criteria == 1) { //is the scan looking for the last time it was modified?
                if (fileInfo.LastWriteTime.Date < date.Date) {
                    //add the file to the list
                    addFiles(folder, fileInfo);
                } //if (fileInfo.CreationTime.Date < date.Date)
            } else if (criteria == 2) { //is the scan looking for the last time it was accessed?
                if (fileInfo.LastAccessTime.Date < date.Date) {
                    //add the file to the list
                    addFiles(folder, fileInfo);
                } //if (fileInfo.CreationTime.Date < date.Date)
            } //if (criteria == 0)
        } //dateOlderThen

        private void dateNewerThen(FileInfo fileInfo, Folders folder, int criteria, DateTime date) {
            //is the scan looking for date of creation?
            if (criteria == 0) {
                if (fileInfo.CreationTime.Date > date.Date) {
                    //add the file to the list
                    addFiles(folder, fileInfo);
                } //if (fileInfo.CreationTime.Date > date.Date)
            } else if (criteria == 1) { //is the scan looking for the last time it was modified?
                if (fileInfo.LastWriteTime.Date > date.Date) {
                    //add the file to the list
                    addFiles(folder, fileInfo);
                } //if (fileInfo.CreationTime.Date > date.Date)
            } else if (criteria == 2) { //is the scan looking for the last time it was accessed?
                if (fileInfo.LastAccessTime.Date > date.Date) {
                    //add the file to the list
                    addFiles(folder, fileInfo);
                } //if (fileInfo.CreationTime.Date > date.Date)
            } //if (criteria == 0)
        } //dateNewerThen

        private void addFiles(Folders folder, FileInfo fileInfo) {
            //gets the filename without the extension
            string fileName = Path.GetFileNameWithoutExtension(fileInfo.Name);
            //create a new Files object
            Files files = new Files(fileName, fileInfo.Extension, folder.getName(), fileInfo.Length,
                                false, false, fileInfo.CreationTime, fileInfo.LastWriteTime, fileInfo.LastAccessTime);
            //get the current filesize
            currentFileSize = getCurrentFileSize();
            //add this files size
            currentFileSize += fileInfo.Length;
            //set the size of this file
            setCurrentFileSize(currentFileSize);
            //get the current number of files
            numFiles = getNumFiles();
            //add one more file
            numFiles++;
            //set new number of files
            setNumFiles(numFiles);
            //record that the file was accessed successfully
            recordSuccessFileAccess(files);
        } //addFiles
        #endregion

        #region ADD FOLDERS, FILES AND EXCEPTIONS TO LISTS
        /// <summary>
        /// This method records the folders that were accessed successfully
        /// </summary>
        /// <param name="success">The Folders object that was accessed</param>
        private void recordSuccessFolderAccess(Folders success) {
            foldersRead.Add(success); 
        } //recordSuccessFolderAccess

        /// <summary>
        /// This method records the files that were accessed successfully
        /// </summary>
        /// <param name="success">The Files object that was accessed</param>
        private void recordSuccessFileAccess(Files success) {
            filesRead.Add(success);
        } //recordSuccessFileAccess

        /// <summary>
        /// This method records the exception/error occuring. 
        /// Only adds exception to list if it's not recorded.
        /// </summary>
        /// <param name="error">The error message (i.e: ex.Message where ex is Exception)</param>
        private void recordExceptions(string error) {
            //is this error already recorded?
            if (!exceptionList.Contains(error)) {
                exceptionList.Add(error);
            } //if (exceptionList.ContainsValue(error))
        } //recordExceptions
        #endregion

        #region CALCULATE FOLDERS SIZE
        private static void setSizeOfFolders(BackgroundWorker bw) {
            long size = 0;
            var enumerator = foldersRead.GetEnumerator();
            while (enumerator.MoveNext()) {
                Folders folder = enumerator.Current;
                //enable reports
                bw.ReportProgress(0);
                //set current text and progress
                progress = "Calculating size of folder: " + folder.getName();
                setProgress(progress);
                //get the size of the subfolder(s)
                size = getChildSize(folder.getName(), bw);
                //add this folders size
                size += folder.getSize();
                //set the new size
                folder.setSize(size);
            } //foreach
        } //setSizeOfFolders

        private static long getChildSize(string parent, BackgroundWorker bw) {
            long size = 0;
            //is the scan cancelled?
            if (bw.CancellationPending) {
                bw.CancelAsync();
            } else { //continue to calculate sizes
                //get the folder(s) which has this parent as parentfolder                
                List<Folders> newList = (from s in foldersRead
                                         where s.getParentFolder() == parent
                                         select s).ToList();
                var enumerator = newList.GetEnumerator();
                while (enumerator.MoveNext()) {
                    Folders folder = enumerator.Current;
                    //get the size of this folders children
                    size += getChildSize(folder.getName(), bw);
                    //add this folders size
                    size += folder.getSize();
                } //while
            } //if (bw.CancellationPending) 
            return size;
        } //getChildSize
        #endregion

        #region RETURN LISTS
        /// <summary>
        /// This function returns a list of the folders that were accessed
        /// </summary>
        /// <returns>List&lt;Folders&gt;</returns>
        public List<Folders> returnListOfFolders() {
            return foldersRead;
        } //returnListOfFolders

        /// <summary>
        /// This function returns a list of the files that were accessed
        /// </summary>
        /// <returns>List&lt;Files&gt;</returns>
        public List<Files> returnListOfFiles() {
            return filesRead;
        } //returnListOfFiles

        /// <summary>
        /// This function returns a list of the exceptions/errors that occured 
        /// during the scan.
        /// </summary>
        /// <returns>List&lt;string&gt;</returns>
        public List<string> returnListOfErrors() {
            return exceptionList;
        } //returnListOfErrors

        /// <summary>
        /// Returns a List&lt;Files&gt; based on the parent folder.
        /// </summary>
        /// <param name="parentFolder">The files parent folder</param>
        /// <param name="filesList">List&lt;Files&gt; containing the scanned files</param>
        /// <returns>List&lt;Files&gt;</returns>
        public List<Files> returnFilesToBeShown(List<Files> filesList, string parentFolder) {
            return (from file in filesList
                    where file.getParentFolder() == parentFolder
                    select file).ToList();
        } //returnFilesToBeShown

        /// <summary>
        /// Returns a List&lt;Files&gt; based on the parent folder. It also has a boolean 
        /// value for on size; ascending or descending.
        /// </summary>
        /// <param name="parentFolder">The files parent folder</param>
        /// <param name="filesList">List&lt;Files&gt; containing the scanned files</param>
        /// <param name="sortAsc">Sort the list ascending (true) or descending (false)</param>
        /// <returns>List&lt;Files&gt;</returns>
        public List<Files> returnFilesToBeShown(List<Files> filesList, string parentFolder, bool sortAsc) {
            //is the list to be sorted on ascending size?
            if (sortAsc) {
                return (from file in filesList
                        where file.getParentFolder() == parentFolder
                        orderby file.getSize() ascending
                        select file).ToList();
            } else { //sort the list on descending size
                return (from file in filesList
                        where file.getParentFolder() == parentFolder
                        orderby file.getSize() descending
                        select file).ToList();
            } //if (sortAsc)
        } //returnFilesToBeShown
        #endregion

        #region COUNT METHODS
        /// <summary>
        /// Counts the total number of scanned files this folder (the parent) 
        /// and the subfolders contains.It is not comparable to results in 
        /// Windows Explorer as this is based on the number of scanned files.
        /// As the scan excludes hidden and system folders/files, the number 
        /// will not be accurate even if full scan without criterias is in action.
        /// </summary>
        /// <param name="parent">Name of the folder to start the count</param>
        /// <param name="folderList">List&lt;Folders&gt; containing the folders</param>
        /// <returns>int</returns>
        public int countFilesScanned(string parent, List<Folders> folderList) {
            int count = 0;
            foreach (Folders f in (from folder in folderList
                                    where folder.getParentFolder() == parent
                                    select folder)) {
                count += countFilesScanned(f.getName(), folderList);
                count += f.getScannedNumberOfFiles();
            } //foreach
            return count;
        } //countFilesScanned

        /// <summary>
        /// Counts the total number of scanned files this folder (the parent) 
        /// and the subfolders contains.It is not comparable to results in 
        /// Windows Explorer as this is based on the scanned files.
        /// As the scan excludes hidden and system folders/files, the number
        /// will not be accurate even if full scan without criterias is in action. 
        /// Has a backroundworker for the more time consuming calculations for folders 
        /// with greater sizes.
        /// </summary>
        /// <param name="parent">Name of the folder to start the count</param>
        /// <param name="folderList">List&lt;Folders&gt; containing the folders</param>
        /// <param name="bw"></param>
        /// <returns>int</returns>
        public int countFilesScanned(string parent, List<Folders> folderList, BackgroundWorker bw) {
            int count = 0;
            //is the scan cancelled?
            if (bw.CancellationPending) {
                bw.CancelAsync();
            } else {  //scan isn't cancelled
                //continue to count files
                foreach (Folders f in (from folder in folderList
                                       where folder.getParentFolder() == parent
                                       select folder)) {
                    count += countFilesScanned(f.getName(), folderList, bw);
                    count += f.getScannedNumberOfFiles();
                    //enable reports
                    bw.ReportProgress(0);
                    //set the progress to the current count
                    setProgress(count.ToString());
                } //foreach
            } //if (bw.CancellationPending)
            return count;
        } //countFilesScanned

        /// <summary>
        /// Counts the total number of files this folder (parent) and the subfolders contains.
        /// The difference between this and countFilesScanned is that this returns the total 
        /// number files this and its subfolders contains. But, it will not be 100% accurate 
        /// as its based on the scanned values, where the system and hidden folders are 
        /// excluded from the scan.
        /// </summary>
        /// <param name="parent">Name of the folder to start counter</param>
        /// <param name="folderList">List&lt;Folders&gt; containing the folders</param>
        /// <returns>int</returns>
        public int countTotalNumberOfFiles(string parent, List<Folders> folderList) {
            int count = 0;
            foreach (Folders f in (from folder in folderList
                                   where folder.getParentFolder() == parent
                                   select folder)) {
                count += countTotalNumberOfFiles(f.getName(), folderList);
                count += f.getTotalNumberOfFiles();
            } //foreach
            return count;
        } //countTotalNumberOfFiles

        /// <summary>
        /// Counts the number of files to be shown/displayed.
        /// </summary>
        /// <param name="parentFolder">The files parent folder</param>
        /// <param name="filesList">List&lt;Files&gt; containing the scanned files</param>
        /// <returns></returns>
        public int countFilesToBeShown(List<Files> filesList, string parentFolder) {
            return returnFilesToBeShown(filesList, parentFolder).Count;
        } //countFilesToBeShown

        /// <summary>
        /// Counts the number of files to be moved
        /// </summary>
        /// <param name="filesList">List&lt;Files&gt; containing the scanned files</param>
        /// <returns>int</returns>
        public int countFilesToMove(List<Files> filesList) {
            return (from file in filesList
                    where file.getToMove() == true
                    select file).ToList().Count;
        } //countFilesToMove

        /// <summary>
        /// Counts the number of files to be deleted
        /// </summary>
        /// <param name="filesList">List&lt;Files&gt; containing the scanned files</param>
        /// <returns>int</returns>
        public int countFilesToDelete(List<Files> filesList) {
            return (from file in filesList
                    where file.getToDelete() == true
                    select file).ToList().Count();
        } //countFilesToDelete
        #endregion

        #region MOVE AND DELETE FILES
        /// <summary>
        /// Moves the file(s) to a new location
        /// </summary>
        /// <param name="pathFrom">The path were the file(s) exists</param>
        /// <param name="pathTo">The path to move the file(s) to</param>
        public void moveFiles(string pathFrom, string pathTo) {
            try {
                MoveOrDeleteObjects.moveFile(pathFrom, pathTo);
            } catch (Exception ex) {
                throw ex;
            } //try/catch
        } //moveFiles

        /// <summary>
        /// Moves the file(s) to the recycle bin
        /// </summary>
        /// <param name="filePath">The path to the file(s) thats to be deleted</param>
        public void moveFilesToRecycleBin(string filePath) {
            try {
                MoveOrDeleteObjects.moveFilesToRecycleBin(filePath);
            } catch (Exception ex) {
                throw ex;
            } //try/catch
        } //moveFilesToRecycleBin

        /// <summary>
        /// Deletes the file(s) permanently.
        /// </summary>
        /// <param name="filePath">The path to the file(s) thats to be deleted</param>
        public void deleteFilesPermanently(string filePath) {
            try {
                MoveOrDeleteObjects.deleteFilePermanently(filePath);
            } catch (Exception ex) {
                throw ex;
            } //try/catch
        } //deleteFilesPermanently
        #endregion

        #region  MOVE AND DELETE FOLDERS
        /// <summary>
        /// Move the whole folder and all its content (files and subfolders).
        /// </summary>
        /// <param name="pathFrom">The path where the folder exists</param>
        /// <param name="pathTo">The path the folder is to be moved to</param>
        public void moveWholeFolder(string pathFrom, string pathTo) {
            try {
                //move the whole folder and its content
                MoveOrDeleteObjects.moveWholeFolder(pathFrom, pathTo);
            } catch (Exception ex) {
                throw ex;
            } //try/catch
        } //moveWholeFolder

        /// <summary>
        /// Copies the folder (and subfolders) and moves the displayed 
        /// files to the new location
        /// </summary>
        /// <param name="pathFrom">The path were the folder exists</param>
        /// <param name="pathTo">The path to move the folders to</param>
        /// <param name="subFolderList">List&lt;Folders&gt; with subfolders (do not include the point to move from)</param>
        /// <param name="moveFileList">List&lt;Files&gt; with files to be moved</param>
        public void moveFoldersContent(string pathFrom, string pathTo, List<Folders> subFolderList, List<Files> moveFileList) {
            try {
                //move the subfolders and the files
                MoveOrDeleteObjects.moveFoldersContent(pathFrom, pathTo, subFolderList, moveFileList);
            } catch (Exception ex) {
                throw ex;
            } //try/catch
        } //moveFoldersContent

        /// <summary>
        /// Moves the folder and its content to the recycle bin
        /// </summary>
        /// <param name="folderPath">Path to the folder to be deleted</param>
        public void moveFolderToRecycleBin(string folderPath) {
            try {
                MoveOrDeleteObjects.moveFoldersToRecycleBin(folderPath);
            } catch (Exception ex) {
                throw ex;
            } //try/catch
        } //moveFolderToRecycleBin

        /// <summary>
        /// Deletes the folder(s) permanently.
        /// </summary>
        /// <param name="folderPath">Path to folder to be deleted</param>
        public void deleteFoldersPermanently(string folderPath) {
            try {
                MoveOrDeleteObjects.deleteFolderPermanently(folderPath);
            } catch (Exception ex) {
                throw ex;
            } //try/catch
        } //deleteFoldersPermanently
        #endregion

        #region SIZE CONVERTION
        /// <summary>
        /// This method converts the current folder/files size. The lowest value is Bytes
        /// and the current greatest is TerraBytes. This method takes the folder/files 
        /// size value and converts upwards as long as the value is greater than one.
        /// (i.e: if filesize = 1000 bytes, then the converted value displayed is 1 Kb)
        /// </summary>
        /// <param name="sizeValue">The original size of the folder/file</param>
        public void convertSizeValue(long sizeValue) {
            //set default values
            sizeConverted = sizeValue;
            sizeParameter = SizeEnum.bytes;
            //is the size convertable to kilobytes?
            if (convertToKB(sizeValue) >= 1) {
                //convert the size to kilobytes
                sizeConverted = convertToKB(sizeValue);
                //set the text value to kb (kilobytes)
                sizeParameter = SizeEnum.Kb;
                //is the size convertable to megabytes?
                if (convertToMB(sizeValue) >= 1) {
                    sizeConverted = convertToMB(sizeValue);
                    sizeParameter = SizeEnum.Mb;
                    //is the size convertable to gigabytes?
                    if (convertToGB(sizeValue) >= 1) {
                        sizeConverted = convertToGB(sizeValue);
                        sizeParameter = SizeEnum.Gb;
                        //is the size convertable to Terrabytes?
                        if (convertToTB(sizeValue) >= 1) {
                            sizeConverted = convertToTB(sizeValue);
                            sizeParameter = SizeEnum.Tb;
                        } //if (convertToTB(sizeValue) >= 1)
                    } //if (convertToGB(value) >= 1)
                } //if (convertToMB(value) >= 1)
            } //if (convertToKB(sizeValue) >= 1)
            //set the converted size
            setConvertedSize(sizeConverted);
            //set the size parameter
            setSizeParameter(sizeParameter);
        } //convertSizeValue

        private double convertToKB(double size) {
            //converts size to kilobytes
            size = size / 1024;
            size = Math.Round(size, 2);
            return size;
        } //convertToKB

        private double convertToMB(double size) {
            //converts size to megabytes
            size = convertToKB(size) / 1024;
            size = Math.Round(size, 2);
            return size;
        } //convertToMB

        private double convertToGB(double size) {
            //converts size to gigabytes
            size = convertToMB(size) / 1024;
            size = Math.Round(size, 2);
            return size;
        } //convertToGB

        private double convertToTB(double size) {
            //converts size to terrabytes
            size = convertToGB(size) / 1024;
            size = Math.Round(size, 2);
            return size;
        } //convertToTB
        #endregion

        #region LOGGING
        /// <summary>
        /// Starts to write the title to the log. If content is to be added after this title, 
        /// this write method needs to be called after the content has been written to the log file.
        /// </summary>
        /// <param name="operation">
        /// The value for the operation in progress 
        /// (0 = Scan, 1 = Move files, 2 = Move files to recycle bin, 
        /// 3 = Delete files permanently, 4 = Move folders, 
        /// 5 = Move folders to recycle bin, 6 = Delete folders permanently)
        /// </param>
        /// <param name="dateDescription">The text to be set before the date in the log</param>
        public void writeStatusToLog(int operation, string dateDescription) {
            try {
                Logging.writeStatusToLog(operation, dateDescription);
            } catch (Exception ex) {
                throw ex;
            } //try/catch
        } //writeStatusToLog

        /// <summary>
        /// This method writes content to the logfile. 
        /// The content written is set in setContent(string content).
        /// </summary>
        public void writeProgressToLog() {
            try {
                Logging.writeProgressToLog();
            } catch (Exception ex) {
                throw ex;
            } //try/catch
        } //writeProgressToLog

        /// <summary>
        /// Sets the current text to be written to the logfile.
        /// Can be one line or multiple lines seperated by "\r\n".
        /// </summary>
        /// <param name="logContent">Text to be written to log</param>
        public void setContent(string logContent) {
            Logging.setContent(logContent);
        } //setContent

        /// <summary>
        /// Sets the path that currently is being processed (moved, deleted, scanned).
        /// </summary>
        /// <param name="operationPath">The path currently being processed</param>
        public void setOperationPath(string operationPath) {
            Logging.setOperationPath(operationPath);
        } //setOperationPath
        #endregion

        #region GET METHODS
        /// <summary>
        /// Gets the current progress of what the 
        /// backgroundworker is currently doing.
        /// </summary>
        /// <returns>string</returns>
        public string getProgress() {
            return progress;
        } //getProgress

        /// <summary>
        /// Gets the enum value for the current converted size.
        /// It's value is based on the SizeEnum 
        /// (bytes, Kb, Mb, Gb, Tb).
        /// </summary>
        /// <returns>Enum</returns>
        public Enum getSizeParameter() {
            return sizeParameter;
        } //getsizeParameter

        /// <summary>
        /// Gets the converted size for the folder/file.
        /// </summary>
        /// <returns>double</returns>
        public double getSizeConverted() {
            return sizeConverted;
        } //getSizeConverted

        private long getCurrentFileSize() {
            return currentFileSize;
        } //getCurrentFileSize

        private int getNumFiles() {
            return numFiles;
        } //getNumFiles
        #endregion

        #region SET METHODS
        /// <summary>
        /// Sets the progress text of what the 
        /// backgroundworker currently is doing. 
        /// </summary>
        /// <param name="progress">The operation/task done by backgroundworker</param>
        public static void setProgress(string progress) {
            ControlClass.progress = progress;
        } //setProgress

        private void setCurrentFileSize(long currentFileSize) {
            this.currentFileSize = currentFileSize;
        } //setCurrentFileSize

        private void setNumFiles(int numFiles) {
            this.numFiles = numFiles;
        } //setNumFiles

        private void setSizeParameter(Enum sizeParameter) {
            this.sizeParameter = sizeParameter;
        } //setSizeParameter

        private void setConvertedSize(double sizeConverted) {
            this.sizeConverted = sizeConverted;
        } //setConvertedSize
        #endregion
    } //ControlClass
} //namespace