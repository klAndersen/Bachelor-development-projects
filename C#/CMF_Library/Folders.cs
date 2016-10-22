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

namespace CMF_Library {
    /// <summary>
    /// This class creates objects of Folders; inherits from FilesAndFolders. 
    /// Not inheritable.
    /// </summary>
    public sealed class Folders : FilesAndFolders {
        private int scannedNumberFiles; //the scanned number of files the folder contains
        private int totNumberFiles; //the total number of files the folder contains

        /// <summary>
        /// The constructor setting the values for the Folders object
        /// </summary>
        /// <param name="folderName">The name of the folder</param>
        /// <param name="size">The size of the folder</param>
        /// <param name="toMove">Is this folder to be moved?</param>
        /// <param name="toDelete">Is this folder to be deleted?</param>
        /// <param name="dateCreated">The date the folder was created</param>
        /// <param name="dateModified">The date the folder was last modified</param>
        /// <param name="lastAccessed">The date the folder was last accessed</param>
        /// <param name="parentFolder">The name of the parent folder</param>
        /// <param name="totNumberFiles">The number of files this folder contains</param>
        /// <param name="scannedNumberFiles">The number of files this folder contains after scan</param>
        public Folders(string folderName, long size, bool toMove, bool toDelete, DateTime dateCreated, 
                    DateTime dateModified, DateTime lastAccessed, string parentFolder, int totNumberFiles, int scannedNumberFiles)
            : base(folderName, parentFolder, size, toMove, toDelete, dateCreated, dateModified, lastAccessed) {
                setTotalNumberOfFiles(totNumberFiles);
                setScannedNumberOfFiles(scannedNumberFiles);
        } //constructor

        /// <summary>
        /// Gets the number of files this folder contains in total
        /// </summary>
        /// <returns>int</returns>
        public int getTotalNumberOfFiles() {
            return totNumberFiles;
        } //getNumFiles

        /// <summary>
        /// Gets the number of files this folder contains based on scan
        /// </summary>
        /// <returns>int</returns>
        public int getScannedNumberOfFiles() {
            return scannedNumberFiles;
        } //getNumFiles

        /// <summary>
        /// Set the total number of files this folder contains
        /// </summary>
        /// <param name="totNumberFiles">Number of files folder contains</param>
        public void setTotalNumberOfFiles(int totNumberFiles) {
            this.totNumberFiles = totNumberFiles;
        } //setTotalNumberOfFiles        

        /// <summary>
        /// Sets the number of files this folder contains after scan
        /// </summary>
        /// <param name="scannedNumberFiles">Number of files scanned</param>
        public void setScannedNumberOfFiles(int scannedNumberFiles) {
            this.scannedNumberFiles = scannedNumberFiles;
        } //setScannedNumberOfFiles
    } //Folders
} //namespace