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
    /// This is the abstract parent class to the classes Folders and Files.
    /// Contains all the common shared methods for getting and setting values.
    /// </summary>
    public abstract class FilesAndFolders {
        #region VARIABLES
        //string variables
        private string name; //the name of the folder/file
        private string parentFolder; //the name of the parent folder
        //numeric variables
        private long size; //the folder/files current size
        //diskSize is not added in the application, but left here for future implementation
        private long diskSize; //the actual size it takes on the drive (not implemented)
        //boolean variable 
        private bool toMove; //is this folder/file set to be moved?
        private bool toDelete; //is this folder/file set to be deleted?
        //DateTime variables
        private DateTime dateCreated; //the date the folder/file was created
        private DateTime dateModified; //the date the folder/file was last modified
        private DateTime lastAccessed; //the date the folder/file was last accessed
        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// The constructor setting the shared values for Files and Folders
        /// </summary>
        /// <param name="name">Name of the folder/file</param>
        /// <param name="parentFolder">Name of the parent folder</param>
        /// <param name="size">The size</param>
        /// <param name="toMove">Is the folder/file to be moved?</param>
        /// <param name="toDelete">Is this folder/file to be deleted?</param>
        /// <param name="dateCreated">The date the folder/file was created</param>
        /// <param name="dateModified">The date the folder/file was last modified</param>
        /// <param name="lastAccessed">The date the folder/file was last accessed</param>
        protected FilesAndFolders(string name, string parentFolder, long size, bool toMove, bool toDelete,
                        DateTime dateCreated, DateTime dateModified, DateTime lastAccessed) {
            setName(name);
            setParentFolder(parentFolder);
            setSize(size);
            setToMove(toMove);
            setToDelete(toDelete);
            setDateCreated(dateCreated);
            setDateModified(dateModified);
            setLastAccessed(lastAccessed);
        } //constructor
        #endregion

        #region GET METHODS
        /// <summary>
        /// Gets the name of the object. 
        /// Files: The name
        /// Folders: The full path
        /// </summary>
        /// <returns>string</returns>
        public string getName() {
            return name;
        }

        /// <summary>
        /// Gets the name of the objects parent directory/folder
        /// (returns null if the folder is "root")
        /// </summary>
        /// <returns>string</returns>
        public string getParentFolder() {
            return parentFolder;
        }

        /// <summary>
        /// Gets the size of the object
        /// </summary>
        /// <returns>long</returns>
        public long getSize() {
            return size;
        }

        /// <summary>
        /// Gets the objects size on disk (not implemented in constructor)
        /// </summary>
        /// <returns>long</returns>
        public long getDiskSize() {
            return diskSize;
        }

        /// <summary>
        /// Gets if the object is to be moved
        /// </summary>
        /// <returns>bool</returns>
        public bool getToMove() {
            return toMove;
        }
        /// <summary>
        /// Gets if the object is to be deleted
        /// </summary>
        /// <returns>bool</returns>
        public bool getToDelete() {
            return toDelete;
        }

        /// <summary>
        /// Gets the date and time the object was created
        /// </summary>
        /// <returns>DateTime</returns>
        public DateTime getDateCreated() {
            return dateCreated;
        }

        /// <summary>
        /// Gets the date and time the object was modified
        /// </summary>
        /// <returns>DateTime</returns>
        public DateTime getDateModified() {
            return dateModified;
        }

        /// <summary>
        /// Gets the date and time the object was last accessed
        /// </summary>
        /// <returns>DateTime</returns>
        public DateTime getLastAccessed() {
            return lastAccessed;
        }
        #endregion

        #region SET METHODS
        /// <summary>
        /// Sets the name of the object
        /// </summary>
        /// <param name="name">The name of the object</param>
        public void setName(string name) {
            this.name = name;
        }

        /// <summary>
        /// Sets the objects parent folder 
        /// (if "root" folder, then parentFolder = null)
        /// </summary>
        /// <param name="parentFolder">The name of parent</param>
        public void setParentFolder(string parentFolder) {
            this.parentFolder = parentFolder;
        }

        /// <summary>
        /// Sets the size of the object
        /// </summary>
        /// <param name="size">The objects size</param>
        public void setSize(long size) {
            this.size = size;
        }

        /// <summary>
        /// Sets the objects size on disk (not implemented in constructor)
        /// </summary>
        /// <param name="diskSize">The objects size on disk</param>
        public void setDiskSize(long diskSize) {
            this.diskSize = diskSize;
        }

        /// <summary>
        /// Sets if the object is to be moved
        /// </summary>
        /// <param name="toMove">Is the object to be moved?</param>
        public void setToMove(bool toMove) {
            this.toMove = toMove;
        }

        /// <summary>
        /// Sets if the object is to be deleted
        /// </summary>
        /// <param name="toDelete">Is the object to be deleted?</param>
        public void setToDelete(bool toDelete) {
            this.toDelete = toDelete;
        }

        /// <summary>
        /// Sets the date and time the object was created
        /// </summary>
        /// <param name="dateCreated">The DateTime the object was created</param>
        public void setDateCreated(DateTime dateCreated) {
            this.dateCreated = dateCreated;
        }

        /// <summary>
        /// Sets the date and time the object was modified
        /// </summary>
        /// <param name="dateModified">The DateTime the object was modified</param>
        public void setDateModified(DateTime dateModified) {
            this.dateModified = dateModified;
        }

        /// <summary>
        /// Sets the date and time the object was last accessed
        /// </summary>
        /// <param name="lastAccessed">The DateTime the object was last accessed</param>
        public void setLastAccessed(DateTime lastAccessed) {
            this.lastAccessed = lastAccessed;
        }
        #endregion
    } //FilesAndFolders
} //namespace