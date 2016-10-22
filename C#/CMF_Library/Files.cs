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
    /// The class that creates objects of Files, inherits from FilesAndFolders. 
    /// Not inheritable.
    /// </summary>
    public sealed class Files : FilesAndFolders {
        private string extension; //the extension of the file (i.e: .TXT)

        /// <summary>
        /// The constructor setting the values for the Folders object
        /// </summary>
        /// <param name="fileName">The name of the file</param>
        /// <param name="extension">The type/extension of the file (i.e: .TXT)</param>
        /// <param name="parentFolder">The name of the parent folder</param>
        /// <param name="size">The size of the file</param>
        /// <param name="toMove">Is the file to be moved?</param>
        /// <param name="toDelete">Is the file to be deleted?</param>
        /// <param name="dateCreated">The date the file was created</param>
        /// <param name="dateModified">The date the file was last modified</param>
        /// <param name="lastAccessed">The date the file was last accessed</param>
        public Files(string fileName, string extension, string parentFolder, long size, bool toMove, bool toDelete,
                DateTime dateCreated, DateTime dateModified, DateTime lastAccessed)
            : base(fileName, parentFolder, size, toMove, toDelete, dateCreated, dateModified, lastAccessed) {
            setExtension(extension);
        } //constructor

        /// <summary>
        /// Gets the files extension/type
        /// </summary>
        /// <returns>string</returns>
        public string getExtension() {
            return extension;
        } //getExtension

        /// <summary>
        /// Sets the extension type
        /// </summary>
        /// <param name="extension">The files extension/type</param>
        public void setExtension(string extension) {
            this.extension = extension;
        } //setExtension
    } //Files
} //namespace