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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//import the Library
using CMF_Library;

namespace CleanMyFolder {
    /// <summary>
    /// This form shows a window similar to the first 
    /// window shown when rightclicking to view 
    /// file properties with Windows Explorer.
    /// </summary>
    public partial class frmProperties : Form {
        //list containing folders and subfolder(s)
        private List<Folders> folderList;
        //object of the folder
        private Folders folder;
        //the number of subfolders this folder contains
        private int numSubFolders;
        //show/hide total number of files and subfolders
        private bool showContent; 

        #region CONSTRUCTOR AND FORMLOAD
        public frmProperties() {
            InitializeComponent();
            //create seperators
            createSeperator(lblSeperator1);
            createSeperator(lblSeperator2);
            createSeperator(lblSeperator3);
        } //constructor

        private void createSeperator(Label lbl) {
            lbl.AutoSize = false;
            lbl.Height = 2;
            lbl.BorderStyle = BorderStyle.Fixed3D;
        } //createSeperator

        private void frmFolderProperties_Load(object sender, EventArgs e) {
            fillLabels();
            try {
                //get the helpfile
                string helpFile = "CMF_HELP.chm";
                //set the namespace
                hpHelper.HelpNamespace = helpFile;
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            } //try/catch
        } //frmFolderProperties_Load
        #endregion

        #region LABELS AND FOLDER-INFORMATION
        private void fillLabels() {
            //get the folder
            folder = getFolder();
            //is the chosen folder a drive?
            if (isDrive(folder.getName())) {
                showDriveProperties(folder.getName());
            } else { //not a drive, show folders information
                //get the name of the parent folder
                string location = System.IO.Directory.GetParent(folder.getName()).ToString();
                //makes the whole path visible in case its longer then the size of the form
                tipProperties.SetToolTip(lblLocation, location);
                //name and location
                lblFolderName.Text = System.IO.Path.GetFileName(folder.getName());
                lblLocation.Text = location;
                //show folders content and size
                showFoldersContentAndSize();
                //date values
                lblDateCreated.Text = folder.getDateCreated().ToString();
                lblDateModified.Text = folder.getDateModified().ToString();
                lblLastAccessed.Text = folder.getLastAccessed().ToString();
            } //if (!isDrive(folder.getName()))
        } //fillLabels

        private void showFoldersContentAndSize() {
            lblContains.Text = "";
            showContent = getShowContent();
            //get the number of scanned files
            int countAllFiles = folder.getScannedNumberOfFiles();
            //is the label with content to be shown?
            if (showContent) {
                string countSubFolders = getSubFolders().ToString();
                lblContains.Text = countSubFolders + " folders, " + countAllFiles.ToString() + " files.";
            } //if (showContent)            
            lblNumFilesScan.Text = countAllFiles.ToString();
            lblTotalNumFiles.Text = folder.getTotalNumberOfFiles().ToString();
            //convert the folders size
            ControlClass.controlClass.convertSizeValue(folder.getSize());
            //get the converted size
            string convertedSize = ControlClass.controlClass.getSizeConverted().ToString();
            //get the size parameter
            convertedSize += " " + ControlClass.controlClass.getSizeParameter().ToString();
            //get the original size of the folder
            string originalSize = folder.getSize().ToString() + " bytes";
            lblSizeScanned.Text = convertedSize + " (" + originalSize + ")";
        } //showFoldersSize

        private bool isDrive(string folderName) {
            //get the drives
            string[] drives = Environment.GetLogicalDrives();
            //loop through the drives
            foreach (string drive in drives) {
                //is the chosen folder a drive?
                if (drive.Equals(folderName)) {
                    return true;
                } //if (drive.Equals(folderName))
            } //foreach
            return false;
        } //isDrive

        private void showDriveProperties(string folderName) {
            //get the drives information
            System.IO.DriveInfo driveInfo = new System.IO.DriveInfo(folderName);
            //show name and location
            lblFolderName.Text = driveInfo.Name;
            lblLocation.Text = driveInfo.RootDirectory.ToString();
            //show drive content and size
            showFoldersContentAndSize();
            //show date values
            lblDateCreated.Text = driveInfo.RootDirectory.CreationTime.ToString();
            lblDateModified.Text = driveInfo.RootDirectory.LastWriteTime.ToString();
            lblLastAccessed.Text = driveInfo.RootDirectory.LastAccessTime.ToString();
        } //showDriveProperties
        #endregion

        #region BUTTONS
        private void cmdOpen_Click(object sender, EventArgs e) {
            //if possible, open the chosen folder...
            try {
                //get name of folder
                string folderName = folder.getName();
                //open the folder
                System.Diagnostics.Process.Start(folderName);
            } catch (Win32Exception ex) {
                MessageBox.Show(ex.Message, "Can't open folder");
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Can't open folder");
            } // try/catch
        } //cmdOpen_Click

        private void cmdClose_Click(object sender, EventArgs e) {
            if (bwCountFiles.WorkerSupportsCancellation) {
                //cancel the asynchronous operation
                bwCountFiles.CancelAsync();
                bwCountFiles.Dispose();
            } //if (bwCountFiles.WorkerSupportsCancellation)
            this.Close();
        } //cmdClose_Click
        #endregion

        #region BACKGROUNDWORKER
        internal void startScan() {
            //is the backgroundworker available?
            if (!bwCountFiles.IsBusy) {
                bwCountFiles.WorkerReportsProgress = true;
                bwCountFiles.WorkerSupportsCancellation = true;
                //start the operation
                bwCountFiles.RunWorkerAsync();
            } //if (!bwCountFiles.IsBusy)
        } //startScan

        private void bwCountFiles_DoWork(object sender, DoWorkEventArgs e) {
            BackgroundWorker worker = sender as BackgroundWorker;
            //get the list of folders
            folderList = getList();
            //get the folders
            folder = getFolder();
            //start counting number of files scanned
            ControlClass.controlClass.countFilesScanned(folder.getName(), folderList, worker);
        } //bwCountFiles_DoWork

        private void bwCountFiles_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            //get the number of subfolders
            string countSubFolders = getSubFolders().ToString();
            //get the amount of files which has been calculated
            string countAllFilesScanned = ControlClass.controlClass.getProgress();
            //show the current result in the label
            lblContains.Text = countSubFolders + " folders, " + countAllFilesScanned + " files.";
        } //bwCountFiles_ProgressChanged

        private void bwCountFiles_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            try {
                //get the folder
                folder = getFolder();
                //get the final result of the count and convert it to integer
                int countAllFiles = int.Parse(ControlClass.controlClass.getProgress());
                //add this folders amount of files to the count
                countAllFiles += folder.getScannedNumberOfFiles();
                //get the number of subfolders
                string countSubFolders = getSubFolders().ToString(); 
                //show the label
                lblContains.Text = countSubFolders + " folders, " + countAllFiles.ToString() + " files.";
                //release the backgroundworker
                bwCountFiles.Dispose();
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error");
            } //try/catch
        } //bwCountFiles_RunWorkerCompleted
        #endregion

        #region GET METHODS
        private Folders getFolder() {
            return folder;
        } //getFolder

        private List<Folders> getList() {
            return folderList;
        } //getList

        private int getSubFolders() {
            return numSubFolders;
        } //getSubFolders

        private bool getShowContent() {
            return showContent;
        } //getShowContains
        #endregion

        #region SET METHODS
        /// <summary>
        /// Set the folder to view information about
        /// </summary>
        /// <param name="folder">Folder to be shown</param>
        internal void setFolder(Folders folder) {
            this.folder = folder;
        } //setFolder

        /// <summary>
        /// Set the number of subfolders the displayed folder contains
        /// </summary>
        /// <param name="numSubFolders">Number of subfolders</param>
        internal void setSubFolders(int numSubFolders) {
            this.numSubFolders = numSubFolders;
        } //setSubFolders

        /// <summary>
        /// Set the list containing the scanned folders
        /// </summary>
        /// <param name="folderList">List&lt;Folders&gt;</param>
        internal void setList(List<Folders> folderList) {
            this.folderList = folderList;
        } //setList

        /// <summary>
        /// Sets if the label which displays the folders content
        /// (subfolders and files) is to be shown
        /// </summary>
        /// <param name="showContent">True = Don't show (calculate files based on subfolders), False = Show (no subfolders)</param>
        internal void setShowContent(bool showContent) {
            this.showContent = showContent;
        } //setShowContent
        #endregion
    } //frmFolderProperties
} //namespace