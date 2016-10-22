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
    public partial class frmCleanMyFolder : Form {

        #region VARIABLE DECLARATION

        #region VARIABLES
        //default text for the path label
        private const string DEFAULT_TEXT = "Choose a path to scan...";
        //index for the columns for Move & Deletion checkboxes in datagridview
        private const int MOVE_INDEX = 6;
        private const int DELETE_INDEX = 7; 
        //constants for the different operations to be logged
        private const int SCAN_OPERATION = 0;
        private const int MOVE_FILES = 1;
        private const int MOVE_FILES_RECYCLE_BIN = 2;
        private const int DELETE_FILES_PERMANENTLY = 3;
        private const int MOVE_FOLDERS = 4;
        private const int MOVE_FOLDERS_RECYCLE_BIN = 5;
        private const int DELETE_FOLDERS_PERMANENTLY = 6;
        //lists for folders, files and exceptions (during scan)
        private List<Folders> folderList = null;
        private List<Files> filesList = null;
        private List<string> exceptionList = null;
        //root of the treeview
        private TreeNode root;
        //variable for formclosing to avoid call to clearAll()
        //that attempts to clear disposed controls
        private bool formClosing = false;
        //criterias for specified scan, made global because if they are 
        //called within the bacgroundworker they cause a CrossThreadException
        //the extensions to scan for
        private string[] searchpattern;
        //the size to scan for
        private long sizeScan; 
        //(older then/newer then/not included in scan)
        private int dateValue;
        //(date created/date modified/last accessed)
        private int dateType;
        //variable for size sorting in datagridview
        private bool sortSizeAsc = true;
        //variable for whether or not its a fullscan
        private bool fullScan = false;
        //variable for whether or not to include subfolders in scan
        private bool scanSub = false;
        //variable for whether or not to enable the folder deletion buttons
        private bool enableFolderDeletion = false;
        #endregion

        #region dateCriteriaArray
        //array containg the values for the date criterias
        private string[] dateCriteriaArray = {
                                                 "older then",
                                                 "newer then",
                                                 "date created",
                                                 "date modified",
                                                 "last accessed"
                                             };
        #endregion

        #region extensionArray
        //array containing filetypes to be searched for
        private string[][] extensionArray = {
                                                new string [] {
                                                    "All files", 
                                                    "*.*"
                                                },
                                                new string[] {
                                                    "Acrobat Reader (pdf)", 
                                                    "*.pdf"
                                                },
                                                new string[] {
                                                    "Compression (zip, rar)", 
                                                    "*.rar",
                                                    "*.zip"
                                                },
                                                new string[] {
                                                    "Excecutables", 
                                                    "*.exe"
                                                },
                                                new string[] {
                                                    "Movies", 
                                                    "*.avi", 
                                                    "*.mkv",
                                                    "*.mov",
                                                    "*.mp4",
                                                    "*.mpeg",
                                                    "*.mpg",
                                                    "*.vob"
                                                },
                                                new string[] {
                                                    "MS Office", 
                                                    "*.accdb",
                                                    "*.doc",
                                                    "*.pps",
                                                    "*.ppt",
                                                    "*.xls"
                                                },
                                                new string[] {
                                                    "Pictures", 
                                                    "*.bmp", 
                                                    "*.gif",
                                                    "*.jpg", 
                                                    "*.jpeg", 
                                                    "*.png"
                                                },
                                                new string[] {
                                                    "Sound/Music", 
                                                    "*.aac",
                                                    "*.aiff",
                                                    "*.m4p",
                                                    "*.mp3",
                                                    "*.wav", 
                                                    "*.wma"
                                                },
                                                new string[] {
                                                    "Temporary files", 
                                                    "*.tmp"
                                                },
                                                new string[] {
                                                    "Textfiles", 
                                                    "*.rtf",
                                                    "*.txt"
                                                }
                                            };
        #endregion

        #endregion

        #region INITIALIZATION AND FORM EVENTS
        public frmCleanMyFolder() {
            InitializeComponent();
            InitializeComboBoxes();
        } //constructor

        private void InitializeComboBoxes() {
            //fill the combobox with extensions
            for (int i = 0; i < extensionArray.Length; i++) {
                cmbExtension.Items.Add(extensionArray[i][0]);
            } //for
            foreach (SizeEnum size in Enum.GetValues(typeof(SizeEnum))) {
                cmbSize.Items.Add(size);
            } //foreach
            cmbDateValue.Items.Add(dateCriteriaArray[0]);
            cmbDateValue.Items.Add(dateCriteriaArray[1]);
            //fill the combobox with criterias for datetype
            for (int i = 2; i < dateCriteriaArray.Length; i++) {
                cmbDateType.Items.Add(dateCriteriaArray[i]);
            } //for
            //sets the chosen value to the first element
            cmbExtension.SelectedIndex = 0;
            cmbSize.SelectedIndex = 0;
            cmbDateValue.SelectedIndex = 0;
            cmbDateType.SelectedIndex = 0;
        } //InitializeComboBoxes

        private void frmCleanMyFolder_Load(object sender, EventArgs e) {
            dtpSetDate.MaxDate = DateTime.Today;
            //user can edit rows
            dgvFiles.ReadOnly = false;
            //set that the user can't edit any cells except the checkbox cells
            for (int i = 0; i < 6; i++) {
                dgvFiles.Columns[i].ReadOnly = true;
            } //for
            setDefaultText(true);
            disableResultScreen();
            try {
                //get the helpfile
                string helpFile = "CMF_HELP.chm";
                //set the namespace
                hpHelper.HelpNamespace = helpFile;
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            } //try/catch
        } //frmCleanMyFolder_Load

        private void setDefaultText(bool startUp) {
            lblFolderSize.Text = "";
            lblSizeEarned.Text = "";
            lblDriveSize.Text = "";
            lblChosenFolder.Text = "";
            //is the application starting?
            if (startUp) {
                addItemInformation("If any errors occur, they will be displayed here.");
            } //if (startUp)
        } //setDefaultText

        private void disableResultScreen() {
            //disable the views
            treeviewFolders.Enabled = false;
            dgvFiles.Enabled = false;
            //disable the buttons for folder options
            cmdOpenFolder.Enabled = false;
            cmdMoveFolder.Enabled = false;
            cmdMoveFolderRecycleBin.Enabled = false;
            cmdDelFoldersPermanently.Enabled = false;
            //disable the buttons for file options
            cmdOpenFile.Enabled = false;
            cmdMoveFiles.Enabled = false;
            cmdMoveFilesRecycleBin.Enabled = false;
            cmdDelFilesPermanently.Enabled = false;
        } //disableResultScreen

        private void frmCleanMyFolder_FormClosing(object sender, FormClosingEventArgs e) {
            //is the backgroundworker active?
            if (bwScan.IsBusy) {
                formClosing = true;
                //cancel the backgroundworker
                bwScan.CancelAsync();
            } //if (bwScan.IsBusy)
            //clear the lists
            clearLists();
        } //frmCleanMyFolder_FormClosing
        #endregion

        #region MENUSTRIP
        private void tsmStartFullscan_Click(object sender, EventArgs e) {
            //get a path to scan
            setPath();
            //prepare and start scan
            fullScan = true;
            scanSub = true;
            prepareToScan(fullScan);
        } //tsmStartFullscan_Click

        private void tsmSpecifiedScan_Click(object sender, EventArgs e) {
            //show the tabpage with the specified scan options
            tabWindows.SelectedIndex = 0;
        } //tsmSpecifiedScan_Click

        private void tsmExit_Click(object sender, EventArgs e) {
            //close the form and exit the program
            this.Close();
        } //tsmExit_Click

        private void tsmShowHelp_Click(object sender, EventArgs e) {
            try {
                //show the help
                System.Diagnostics.Process.Start(hpHelper.HelpNamespace);
            } catch (Win32Exception ex) {
                MessageBox.Show("Can't view helpfile.\n" + ex.Message, "Can't view help");
            } catch (Exception ex) {
                MessageBox.Show("Can't view helpfile.\n" + ex.Message, "Can't view help");
            } //try/catch
        } //tsmShowHelp_Click

        private void tsmOpenLog_Click(object sender, EventArgs e) {
            try {
                //the name & location of the log
                string logFile = Environment.CurrentDirectory + @"\" + "Log.txt";
                //open the logfile
                System.Diagnostics.Process.Start(logFile);
            } catch (Win32Exception ex) {
                MessageBox.Show(ex.Message, "Can't open log");
                addItemInformation("Failed to open log: " + ex.Message);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Can't open log");
                addItemInformation("Failed to open log: " + ex.Message);
            } // try/catch
        } //tsmOpenLog_Click

        private void tsmAbout_Click(object sender, EventArgs e) {
            frmAbout about = new frmAbout();
            about.ShowDialog(this);
        } //tsmAbout_Click
        #endregion

        #region SCAN MENU

        #region BUTTONS
        private void cmdSetPath_Click(object sender, EventArgs e) {
            setPath();
        } //cmdSetPath_Click

        private void setPath() {
            //set default text
            lblPath.Text = DEFAULT_TEXT;
            DialogResult dialog = fdbPath.ShowDialog();
            //was a path chosen?
            if (dialog == DialogResult.OK) {
                lblPath.Text = fdbPath.SelectedPath;
                //shows content in tooltip if the length is over the labels size
                tipLabel.SetToolTip(lblPath, lblPath.Text);
            } //if (dialog == DialogResult.OK)
        } //setPath

        private void cmdStartScan_Click(object sender, EventArgs e) {
            setDefaultText(false);
            prepareToScan(false);
        } //cmdStartScan_Click

        private void cmdReset_Click(object sender, EventArgs e) {
            //set default values
            lblPath.Text = DEFAULT_TEXT;
            chkIncludeSubFolders.Checked = true;
            txtExtension.Text = "";
            numericSize.Value = 0;
            dtpSetDate.Value = DateTime.Today;
            //uncheck the checkboxes
            chkSelectAll.Checked = false;
            chkIncludeExtension.Checked = false;
            chkIncludeSize.Checked = false;
            chkIncludeDate.Checked = false;
            //sets the chosen value to the first element
            cmbExtension.SelectedIndex = 0;
            cmbSize.SelectedIndex = 0;
            cmbDateValue.SelectedIndex = 0;
            cmbDateType.SelectedIndex = 0;
        } //cmdReset_Click

        private void prepareToScan(bool fullScan) {
            //is a path chosen?
            if (lblPath.Text.Equals(DEFAULT_TEXT)) {
                //is this a fullscan?
                if (!fullScan) {
                    //pressed scanbutton, show message to user
                    MessageBox.Show("You need to choose a path to be scanned.", "No path");
                    //show the folderbrowser
                    setPath();
                } //if (!fullScan)
            } else { //a path is chosen
                //get the location of the drive the os is installed on
                string OS_Drive = System.IO.Path.GetPathRoot(Environment.SystemDirectory);
                //is the path the same as where the OS is installed?
                if (lblPath.Text.Equals(OS_Drive)) {
                    //message to show to user
                    string message = "Preparing to scan " + OS_Drive + "\nThis may take a while. \nStart scan?";
                    //ask the user if scan is to be started
                    if (MessageBox.Show(message, "Start scan?", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                        startScan();
                    } //if (MessageBox.Show(...)
                } else { //"normal" folder, start scan
                    startScan();
                } //if (lblPath.Text.Equals(OS_Drive))
            } //if (lblPath.Text.Equals(DEFAULT_TEXT))
        } //prepareToScan

        private void startScan() {
            try {
                //is the backgroundworker available?
                if (!bwScan.IsBusy) {
                    //clear the previous scanned data
                    clearAll();
                    updateProgress("Starting scan...");
                    //if the scan sub is false (default value) get the 
                    //value of the checkbox
                    if (!scanSub) {
                        scanSub = chkIncludeSubFolders.Checked;
                    } //if (!scanSub)
                    //disable the views
                    treeviewFolders.Enabled = false;
                    dgvFiles.Enabled = false;
                    enableFolderDeletion = false;
                    //get the values to scan for
                    searchpattern = includeExtension();
                    sizeScan = includeSize();
                    dateValue = includeDateValue();
                    dateType = includeDateType();
                    //write to log that a scan was started, and which path was scanned
                    ControlClass.controlClass.setOperationPath(lblPath.Text);
                    ControlClass.controlClass.writeStatusToLog(SCAN_OPERATION, "Scanning started: ");
                    //add information to the listbox
                    addItemInformation("Started scanning: " + lblPath.Text);
                    // start the asynchronous operation
                    bwScan.RunWorkerAsync();
                } //if (!bwScan.IsBusy)
            } catch (Exception ex) {
                MessageBox.Show("Couldn't start scan: " + ex.Message, "Error");
                addItemInformation("Couldn't start scan: " + ex.Message);
                //enable the buttons so user can retry or scan a new path
                enableScanSettingButtons();
            } //try/catch
        } //startScan

        private void clearAll() {
            if (!formClosing) {
                disableResultScreen();
                //clear the list and views
                lblResult.Visible = false;
                lstInformation.Items.Clear();
                treeviewFolders.Nodes.Clear();
                dgvFiles.Rows.Clear();
                clearLists();
                enableScanSettingButtons();
                //add default text to listbox
                addItemInformation("If any errors occur, they will be displayed here.");
            } //if (!formClosing)
        } //clearAll

        private void clearLists() {
            //is the list empty?
            if (folderList != null) {
                folderList.Clear();
            } //if (folderList != null)
            //is the list empty?
            if (filesList != null) {
                filesList.Clear();
            } //if (filesList != null)
            //is the list empty?
            if (exceptionList != null) {
                exceptionList.Clear();
            } //if (exceptionList != null)
        } //clearLists

        private void enableScanSettingButtons() {
            cmdStartScan.Enabled = !cmdStartScan.Enabled;
            cmdReset.Enabled = !cmdReset.Enabled;
            cmdCancel.Enabled = !cmdCancel.Enabled;
        } //enableScanSettingButtons

        private void cmdCancel_Click(object sender, EventArgs e) {
            if (bwScan.WorkerSupportsCancellation) {
                //cancel the asynchronous operation
                bwScan.CancelAsync();
            } //if (bwScan.WorkerSupportsCancellation)
        } //cmdCancel_Click
        #endregion

        #region CHECKBOX DISABLED/ENABLED
        private void chkSelectAll_CheckedChanged(object sender, EventArgs e) {
            chkIncludeSubFolders.Checked = true;
            chkIncludeExtension.Checked = true;
            chkIncludeDate.Checked = true;
            chkIncludeSize.Checked = true;
            chkIncludeDate.Checked = true;
            chkIncludeDateType.Checked = true;
        } //chkSelectAll_CheckedChanged

        private void chkIncludeExtension_CheckedChanged(object sender, EventArgs e) {
            lblExtension.Enabled = !lblExtension.Enabled;
            cmbExtension.Enabled = !cmbExtension.Enabled;
            lblUserExtension.Enabled = !lblUserExtension.Enabled;
            txtExtension.Enabled = !txtExtension.Enabled;
        } //chkIncludeExtension_CheckedChanged

        private void chkIncludeSize_CheckedChanged(object sender, EventArgs e) {
            lblSize.Enabled = !lblSize.Enabled;
            numericSize.Enabled = !numericSize.Enabled;
            cmbSize.Enabled = !cmbSize.Enabled;
        } //chkIncludeSize_CheckedChanged

        private void chkIncludeDate_CheckedChanged(object sender, EventArgs e) {
            lblDateValue.Enabled = !lblDateValue.Enabled;
            cmbDateValue.Enabled = !cmbDateValue.Enabled;
            dtpSetDate.Enabled = !dtpSetDate.Enabled;
            chkIncludeDateType.Enabled = !chkIncludeDateType.Enabled;
            //is the date type option checked?
            if (chkIncludeDateType.Checked) {
                //uncheck it
                chkIncludeDateType.Checked = false;
            } //if (chkIncludeDateType.Checked) {
        } //chkIncludeDate_CheckedChanged

        private void chkIncludeDateType_CheckedChanged(object sender, EventArgs e) {
            cmbDateType.Enabled = !cmbDateType.Enabled;
        } //chkIncludeDateType_CheckedChanged
        #endregion

        #region INCLUDE SCAN CRITERIAS
        private string[] includeExtension() {
            string[] extension = null;
            //is extension (filetypes) included in the scan?
            if (chkIncludeExtension.Checked) {
                //has the user entered any in the textbox?
                if (txtExtension.Text.Equals("")) {
                    //get the index of the extension
                    int index = cmbExtension.SelectedIndex;
                    //get the number of elements in the array
                    int arraySize = extensionArray[index].Length;
                    //set the new size for the array
                    //( -1 because the first element is not added)
                    extension = new string[arraySize - 1];
                    //fill the array with extensions
                    for (int i = 1; i < arraySize; i++) {
                        //get the array position (start at 0)
                        int position = i - 1;
                        extension[position] = extensionArray[index][i];
                    } //for
                } else { //the user has entered an extension
                    extension = new string[] { txtExtension.Text };
                } //if (txtExtension.Text.Equals(""))
            } else { //does not want to search for specific filetypes
                //set the default value (*.*) - search for all filetypes
                extension = new string[] {"*.*"};
            } //if (chkIncludeExtension.Checked)
            return extension;
        } //includeExtension

        private void txtExtension_Enter(object sender, EventArgs e) {
            txtExtension.SelectAll();
        } //txtExtension_Enter

        private long includeSize() {
            long size = 0;
            //is size included in the scan?
            if (chkIncludeSize.Checked && numericSize.Value > 0) {
                //get the definitions for the size
                string kb = SizeEnum.Kb.ToString();
                string mb = SizeEnum.Mb.ToString();
                string gb = SizeEnum.Gb.ToString();
                string tb = SizeEnum.Tb.ToString();
                //which definition is set for the scan?
                if (cmbSize.Text.Equals(kb)) {
                    size = calculateSize(1);
                } else if (cmbSize.Text.Equals(mb)) {
                    size = calculateSize(2);
                } else if (cmbSize.Text.Equals(gb)) {
                    size = calculateSize(3);
                } else if (cmbSize.Text.Equals(tb)) {
                    size = calculateSize(4);
                } //if (cmbSize.Text.Equals(kb))
            } //if (chkIncludeSize.Checked && numericSize.Value > 0)
            return size;
        } //includeSize

        private long calculateSize(int numMultiply) {
            //get the minimum size of files
            long size = (long)numericSize.Value;
            for (int i = 0; i < numMultiply; i++) {
                //multiply the size to get the converted size
                size = size * 1024;
            } //for
            return size;
        } //calculateSize

        private int includeDateValue() {
            //is date included in the search?
            if (chkIncludeDate.Checked) {
                //get the value for old
                string older = dateCriteriaArray[0];
                //scan for date older then?
                if (cmbDateValue.Text.Equals(older)) {
                    return 0;
                } else { //scan for newer
                    return 1;
                } //if (cmbDateValue.Text.Equals(older))
            } //if (chkIncludeDate.Checked)
            return -1;
        } //includeDateValue

        private int includeDateType() {
            string modified = dateCriteriaArray[3];
            string accessed = dateCriteriaArray[4];
            //is details regarding date included in scan?
            if (chkIncludeDateType.Checked) {
                //scan for date based on modified?
                if (cmbDateType.Text.Equals(modified)) {
                    return 1;
                //scan for date based on last accessed?
                } else if (cmbDateType.Text.Equals(accessed)) {
                    return 2;
                } //if (cmbDateType.Text.Equals(created))
            } //if (chkIncludeDateType.Checked)
            return 0;
        } //includeDateType
        #endregion

        #endregion

        #region BACKGROUNDWORKER

        #region SCANNING & FILLING
        private void bwScan_DoWork(object sender, DoWorkEventArgs e) {
            BackgroundWorker worker = sender as BackgroundWorker;
            //do the first part of the scan job
            scanJob(worker, e);
            //were there any files found?
            if (filesList.Count > 0) {
                //start to fill up the treeview and datagridview
                fillUpViews(worker, e);
            } //if (filesList.Count > 0)
        } //bwScan_DoWork

        private void scanJob(BackgroundWorker worker, DoWorkEventArgs e) {
            //has the scan been cancelled?
            if (worker.CancellationPending) {
                cancelScan(e);
            } else { //scan still active
                string path = lblPath.Text;
                //was any of the criterias checked?
                if (!chkIncludeExtension.Checked && !chkIncludeSize.Checked && !chkIncludeDate.Checked || fullScan) {
                    //no criterias, start a full scan
                    ControlClass.controlClass.startFullScan(path, bwScan, scanSub);
                } else { //one or more criterias checked
                    //start a specified scan based on checked criterias
                    ControlClass.controlClass.startSpecifiedScan(path, searchpattern, worker, 
                                                        sizeScan, dateValue, dateType, dtpSetDate.Value, scanSub);
                } //if (!chkIncludeExtension.Checked ...)
                //get the lists of folders, files and exceptions/errors
                folderList = ControlClass.controlClass.returnListOfFolders();
                filesList = ControlClass.controlClass.returnListOfFiles();
                exceptionList = ControlClass.controlClass.returnListOfErrors();
                //has the scan been cancelled?
                if (worker.CancellationPending) { 
                    cancelScan(e);
                } else { //scan still active
                    //did any exceptions occur during scan?
                    if (exceptionList.Count > 0) {
                        //adds the failed accessed folders to the listbox
                        addItemInformation("Failed to accesss following folders:");
                        foreach (string error in exceptionList) {
                            addItemInformation(error);
                        } //foreach
                    } else { //no exceptions occured during the scan of folders/files
                        addItemInformation("All folders and files were scanned succsessfully!");
                    } //if (exceptionList.Count > 0)
                } //inner if (worker.CancellationPending)
            } //outer if (worker.CancellationPending)
        } //scanJob

        private void addItemInformation(string text) {
            //is invoke required?
            if (lstInformation.InvokeRequired) {
                Action<String> action = new Action<string>(addItemInformation);
                lstInformation.Invoke(action, text);
            } else { //no invoke needed, fill listbox
                lstInformation.Items.Add(text);
            } //if (lstInformation.InvokeRequired)
        } //addItemInformation

        private void fillUpViews(BackgroundWorker worker, DoWorkEventArgs e) {
            //has the scan been cancelled?
            if (worker.CancellationPending) {
                cancelScan(e);
            } else { //still active   
                //enable progress report and set the current work text
                worker.ReportProgress(0);
                string progress = "Filling treeview...";
                setProgress(progress);
                //set name of the root folder
                string parentFolder = folderList[0].getName();
                //set new value to root  
                root = new TreeNode(parentFolder);
                //does the root folder contain subfolders?
                if (folderList.Count > 1) {
                    //fill the treeview with folders
                    fillTreeView(parentFolder, root);
                } //if (folderList.Count > 1)
                //fill datagridview with the folders files
                filldgvFiles(parentFolder, false);
            } //if (worker.CancellationPending)
        } //fillUpViews

        private void cancelScan(DoWorkEventArgs e) {
            e.Cancel = true;
            return;
        } //cancelScan
        #endregion

        #region UPDATE PROGRESS
        private void bwScan_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            updateProgress(getProgress());
        } //bwScan_ProgressChanged

        private void updateProgress(string text) {
            //is invoke required?
            if (lblProgress.InvokeRequired) {
                Action<string> action = new Action<string>(updateProgress);
                lblProgress.Invoke(action, text);
            } else {
                lblProgress.Text = text;
            } //if (lblProgress.InvokeRequired)
        } //updateProgress

        private string getProgress() {
            return ControlClass.controlClass.getProgress();
        } //getProgress

        private static void setProgress(string progress) {
            ControlClass.setProgress(progress);
        } //setProgress
        #endregion

        #region BACKGROUNDWORKER COMPLETED
        private void bwScan_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            string result = "";
            //is the backgroundworker cancelled/aborted?
            if (e.Cancelled) {
                result = "Canceled!";
                //clear the contents
                clearAll();
                //change value to false in case next scan is specified scan
                fullScan = false;
                scanSub = false;
                setDefaultText(false);
                informUser("The scan was canceled.");
            } else if (e.Error != null) { //did an error occur?
                result = e.Error.Message;
                //change value to false in case next scan is specified scan
                fullScan = false;
                scanSub = false;
                setDefaultText(false);
                informUser(e.Error.Message);
                //enable the related buttons
                enableButtonsAfterFinish(false);
            } else { //the backgroundworker finished the job
                result = "Done scannning!";
                //dispose the resources used by the backgroundworker
                bwScan.Dispose();
                //were there files discovered?  
                if (filesList.Count > 0) {
                    //add the root with child nodes to the treeview
                    treeviewFolders.Nodes.Add(root);
                    //expand the root node only
                    root.Expand();
                    //show the window with treeview and datagridview
                    tabWindows.SelectedIndex = 1;
                    //enable the related buttons
                    enableButtonsAfterFinish(true);
                } else { //no files were found
                    enableButtonsAfterFinish(false);
                    setDefaultText(false);
                    informUser("No files matching criteria was found.");
                } //if (filesList.Count > 0)
            } //if (e.Cancelled)
            updateProgress(result);
        } //bwScan_RunWorkerCompleted

        private void informUser(string text) {
            lblResult.Visible = true;
            lblResult.Text = text;
            addItemInformation(text);
        } //informUser

        private void enableButtonsAfterFinish(bool scanFinished) {
            //enable the buttons on the scan menu
            enableScanSettingButtons();
            //was the scan completed?
            if (scanFinished) {
                //enable views
                treeviewFolders.Enabled = true;
                dgvFiles.Enabled = true;
                //was subfolders not included in the scan?
                if (!scanSub) {
                    //get the number of subfolders
                    int numSubFolders = countSubFolders(lblPath.Text);
                    //does the folder scanned contain subfolders?
                    if (numSubFolders == 0) {
                        //enable (if possible) the folder deletion buttons
                        enableDeleteFolderOptions();
                    } //if (numSubFolders == 0)
                } else { //subfolders included in the scan
                    //enable (if possible) the folder deletion buttons
                    enableDeleteFolderOptions();
                } //if (!scanSub)
                //enable options for folder
                disableAndEnableOptions(true);
            } else { //scan wasn't completed, disable options
                disableResultScreen();
            } //if (scanFinished)
            //set focus to the treeview
            treeviewFolders.Focus();
            //set scan subfolders to false in case this was a full scan
            scanSub = false;
        } //enableButtonsAfterFinish

        private void enableDeleteFolderOptions() {
            //was this a full scan?
            if ((!chkIncludeExtension.Checked && 
                    !chkIncludeSize.Checked && !chkIncludeDate.Checked) || fullScan) {
                //set fullScan to false in case next scan is specified scan
                fullScan = false;
                enableFolderDeletion = true;
            } //if (!chkIncludeExtension.Checked)
        } //enableDeleteFolderOptions

        private int countSubFolders(string path) {
            int numSubs = 0;
            try {
                //get the number of directories/folders the path contains
                numSubs = System.IO.Directory.GetDirectories(path).Length;
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error");
                addItemInformation(ex.Message);
            } //try/catch
            return numSubs;
        } //countSubFolders
        #endregion

        #endregion

        #region TREEVIEW

        #region FILLING treeview
        private void fillTreeView(string parentFolder, TreeNode root) {
            try {
                //get the parentFolders subfolder(s)
                List<Folders> newList = (from s in folderList
                                         where s.getParentFolder() == parentFolder
                                         select s).ToList();
                var enumerator = newList.GetEnumerator();
                while (enumerator.MoveNext()) {
                    Folders folder = enumerator.Current;
                    //get the folders size
                    long sizeValue = folder.getSize();
                    //is this folders size bigger then the size scanned for?
                    if (sizeValue > sizeScan) {
                        //add this folder to the treeview
                        addNewRoot(folder.getName(), root);
                    } //if (sizeValue > sizeScan)
                } //while
            } catch (StackOverflowException ex) {
                //may occur if i.e. infinte loop appears
                MessageBox.Show("StackOverflow: " + ex, "StackOverflow");
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error");
                addItemInformation("Error occured while filling treeview: " + ex.Message);
            } // try/catch    
        } //fillTreeView

        private void addNewRoot(string folderName, TreeNode root) {
            //create a new node containing the folders
            TreeNode newRoot = new TreeNode(folderName);
            //get the subfolders of current folder
            fillTreeView(folderName, newRoot);
            //add the children to the root
            root.Nodes.Add(newRoot);
        } //addNewRoot
        #endregion

        #region RIGHTCLICK treeview
        private void treeviewFolders_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e) {
            //is the rightmouse-button clicked?
            if (e.Button == MouseButtons.Right) {
                //set the selected node to the one that was rightclicked
                treeviewFolders.SelectedNode = e.Node;
                //enable the folder options
                disableAndEnableOptions(true);
            } //if (e.Button == MouseButtons.Right)
        } //treeviewFolders_NodeMouseClick

        private void ctmTreeView_Opening(object sender, CancelEventArgs e) {
            //get number of nodes in treeview
            int countNodes = treeviewFolders.GetNodeCount(true);
            //is there any nodes added to the treeview?
            if (countNodes > 0) {
                //enable the options
                tsmExpandClose.Enabled = true;
                tsmScan.Enabled = true;
                tsmOpenFolder.Enabled = true;
                tsmViewFoldersInfo.Enabled = true;
            } else { //no nodes, disable options
                tsmExpandClose.Enabled = false;
                tsmScan.Enabled = false;
                tsmOpenFolder.Enabled = false;
                tsmViewFoldersInfo.Enabled = false;
            } //if (countNodes > 0)
        } //ctmTreeView_Opening

        #region SCAN OPTIONS
        private void tsmFullScan_Click(object sender, EventArgs e) {
            //prepare and start scan
            fullScan = true;
            scanSub = true;
            string text = treeviewFolders.SelectedNode.Text;
            lblPath.Text = text;
            prepareToScan(fullScan);
        } //tsmFullScan_Click

        private void tsmScanFolderSpecified_Click(object sender, EventArgs e) {
            string text = treeviewFolders.SelectedNode.Text;
            lblPath.Text = text;
            //shows content in tooltip if length is over the labels size
            tipLabel.SetToolTip(lblPath, text);
            startScan();
        } //tsmScanFolderSpecified_Click

        private void tsmNewScan_Click(object sender, EventArgs e) {
            lblPath.Text = treeviewFolders.SelectedNode.Text;
            tipLabel.SetToolTip(lblPath, lblPath.Text);
            tabWindows.SelectedIndex = 0;
        } //tsmNewScan_Click
        #endregion

        #region EXPAND/CLOSE OPTIONS
        private void tsmExpandAll_Click(object sender, EventArgs e) {
            //expand all nodes in treeview
            treeviewFolders.ExpandAll();
        } //tsmExpandAll_Click

        private void tsmCloseAll_Click(object sender, EventArgs e) {
            //close all nodes in treeview
            treeviewFolders.CollapseAll();
        } //tsmCloseAll_Click

        private void tsmExpandSelected_Click(object sender, EventArgs e) {
            //expand the selected node and its child nodes
            treeviewFolders.SelectedNode.ExpandAll();
        } //tsmExpandSelected_Click

        private void tsmCloseSelected_Click(object sender, EventArgs e) {
            //close the selected node and its child nodes
            treeviewFolders.SelectedNode.Collapse();
        } //tsmCloseSelected_Click
        #endregion

        private void tsmOpenFolder_Click(object sender, EventArgs e) {
            //open the selected folder
            openFolder();
        } //tsmOpenFolder_Click

        private void openFolder() {
            //if possible, open the chosen folder...
            try {
                //get the folders name
                string folderName = treeviewFolders.SelectedNode.Text;
                //open the folder
                System.Diagnostics.Process.Start(folderName);
            } catch (Win32Exception ex) {
                MessageBox.Show(ex.Message, "Can't open folder");
                addItemInformation("Failed to open folder. Error: " + ex.Message);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Can't open folder");
                addItemInformation("Failed to open folder. Error: " + ex.Message);
            } // try/catch
        } //openFolder

        private void tsmViewFoldersInfo_Click(object sender, EventArgs e) {
            //get the name of the folder
            string folderName = treeviewFolders.SelectedNode.Text;
            //create an object of the folder
            Folders folder = folderList.Find(f => f.getName() == folderName);
            frmProperties properties = new frmProperties();
            //get the number of subfolders this folder contains
            int numSubFolders = treeviewFolders.SelectedNode.GetNodeCount(true);
            properties.setSubFolders(numSubFolders);
            //set that it's this folder thats to be displayed on frmProperties
            properties.setFolder(folder);
            properties.setList(folderList);
            //does this folder contain any subfolders?
            if (numSubFolders > 0) {
                //contains subfolders, start the scan
                properties.setShowContent(false);
                properties.startScan();
            } else { //no subfolders, show the content of folder
                properties.setShowContent(true);
            } //if (numSubFolders > 0)
            //show the property window
            properties.ShowDialog(this);
        } //tsmViewFoldersInfo_Click

        #region AFTERSELECT TreeView
        private void treeviewFolders_AfterSelect(object sender, TreeViewEventArgs e) {
            //get the name of the chosen folder
            string folderName = treeviewFolders.SelectedNode.Text;
            dgvFiles.Columns[0].HeaderText = "Contents of " + folderName;
            //create an object of Folders
            Folders folder = folderList.Find(f => f.getName() == folderName);
            //get the folders size
            long folderSize = folder.getSize();
            //update the drives size
            getDriveSize();
            //add the information about the currently chosen folders size
            fillLabelsWithSize(folderSize, lblFolderSize, "Current folders size: ");
            //get the drives
            string[] drives = Environment.GetLogicalDrives();
            string chosenFolder = "";
            //loop through the drives
            foreach (string drive in drives) {
                //is the chosen folder a drive?
                if (drive.Equals(folderName)) {
                    chosenFolder = drive;
                } //if (drive.Equals(folderName))
            } //foreach
            //was the chosen folder a drive?
            if (chosenFolder.Equals("")) {
                //not a drive, view drives name
                lblChosenFolder.Text = "Chosen folder: " + System.IO.Path.GetFileName(folderName);
            } else { //a drive was chosen
                //show the drives name
                lblChosenFolder.Text = "Chosen folder: " + chosenFolder;
            } //if (chosenFolder.Equals(""))
            //set the tooltip for this label to the labels text
            tipLabel.SetToolTip(lblChosenFolder, lblChosenFolder.Text);
            refreshDataGridView(false);
        } //treeviewFolders_AfterSelect

        private void getDriveSize() {
            //is there any folders left in treeview?
            if (treeviewFolders.GetNodeCount(true) > 0) {
                //get the name of the selected folder
                string folder = treeviewFolders.SelectedNode.Text;
                //get the information about the drive
                System.IO.DriveInfo driveInfo = new System.IO.DriveInfo(folder);
                //get the drives used space based on its size values
                long driveSize = driveInfo.TotalSize - driveInfo.AvailableFreeSpace;
                //add the information about space used on the drive
                fillLabelsWithSize(driveSize, lblDriveSize, "Space used on drive: ");
            } //if (treeviewFolders.GetNodeCount(true) > 0)
        } //getDriveSize

        private void fillLabelsWithSize(long sizeValue, Label lbl, string displayText) {
            //convert the size value
            ControlClass.controlClass.convertSizeValue(sizeValue);
            //get the converted size
            double sizeConverted = ControlClass.controlClass.getSizeConverted();
            //get the size parameter
            string sizeParameter = " " + ControlClass.controlClass.getSizeParameter().ToString();
            //display the text
            lbl.Text = displayText + sizeConverted.ToString() + sizeParameter;
        } //fillLabelsWithSize
        #endregion

        private void refreshDataGridView(bool sortSize) {
            try {
                //clear datagridview of previous entries
                dgvFiles.Rows.Clear();
                //get the name of the parentFolder
                string parentFolder = treeviewFolders.SelectedNode.Text;
                //fill the datagridview with the folders files
                filldgvFiles(parentFolder, sortSize);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error");
                addItemInformation("Failed to refresh datagridview: " + ex.Message);
            } //try/catch
        } //refreshDataGridView

        #endregion

        private void treeviewFolders_Enter(object sender, EventArgs e) {
            disableAndEnableOptions(true);
        } //treeviewFolders_Enter
        #endregion

        #region DATAGRIDVIEW

        #region FILLING datagridview WITH FILES
        private void filldgvFiles(string parentFolder, bool sortSize) {
            int rows = 0;
            List<Files>.Enumerator enumerator;
            //get the number of files to display
            int count = ControlClass.controlClass.countFilesToBeShown(filesList, parentFolder);
            //is there files to be shown?
            if (count > 0) {
                invokeAddingRowsFiles(count);
                //is the view to be sorted on filesize?
                if (sortSize) {
                    //sort the files on size
                    enumerator = ControlClass.controlClass.returnFilesToBeShown(filesList, parentFolder, sortSizeAsc).GetEnumerator();
                } else { //not to be sorted on filesize
                    enumerator = ControlClass.controlClass.returnFilesToBeShown(filesList, parentFolder).GetEnumerator();
                } //if (sortSize) 
                while (enumerator.MoveNext()) {
                    //get the current file
                    Files currentFile = enumerator.Current;
                    //convert the files size
                    ControlClass.controlClass.convertSizeValue(currentFile.getSize());
                    //get the converted size
                    double sizeConverted = ControlClass.controlClass.getSizeConverted();
                    //get the size parameter
                    Enum sizeParameter = ControlClass.controlClass.getSizeParameter();
                    //create a new row with the current file
                    fillRowsWithFiles(rows, currentFile, sizeConverted, sizeParameter);
                    //increase the counter
                    rows++;
                } //while
            } //if (count > 0)
        } //filldgvFiles

        private void invokeAddingRowsFiles(int numRows) {
            //is invoke required?
            if (dgvFiles.InvokeRequired) {
                Action<int> action = new Action<int>(invokeAddingRowsFiles);
                dgvFiles.Invoke(action, numRows);
            } else { //no invoke needed, add rows
                dgvFiles.Rows.Add(numRows);
            } //if (dgvFiles.InvokeRequired)
        } //invokeAddingRowsFiles

        private void fillRowsWithFiles(int rows, Files currentFile, double size, Enum sizeParameter) {
            //add the folders content
            invokeFilling(rows, 0, currentFile.getName() + currentFile.getExtension());
            //add the files extension
            invokeFilling(rows, 1, currentFile.getExtension());
            //add the files size and parameter (bytes, kb, mb, gb, tb)
            invokeFilling(rows, 2, size + " " + sizeParameter);
            //add the files date of creation
            invokeFilling(rows, 3, currentFile.getDateCreated()); 
            //add the date the file was last modified
            invokeFilling(rows, 4, currentFile.getDateModified()); 
            //add the date the file was last accessed
            invokeFilling(rows, 5, currentFile.getLastAccessed()); 
            //add the value for whether or not this file is to be moved
            invokeFilling(rows, 6, currentFile.getToMove());
            //add the value for whether or not this file is to be deleted
            invokeFilling(rows, 7, currentFile.getToDelete()); 
        } //fillRowsWithFiles

        private delegate void invokeFillingDelegate(int rows, int cells, object value);

        private void invokeFilling(int rows, int cells, object value) {
            //is invoke required?
            if (dgvFiles.InvokeRequired) {
                dgvFiles.Invoke(new invokeFillingDelegate(invokeFilling), rows, cells, value);
            } else { //no invoke needed, fill datagridview as usual
                dgvFiles.Rows[rows].Cells[cells].Value = value;
            } //if (dgvFiles.InvokeRequired)
        } //invokeFilling
        #endregion

        #region CLICK ON CELLS datagridview
        private void dgvFiles_Click(object sender, EventArgs e) {
            disableAndEnableOptions(false);
        } //dgvFiles_Click

        private void disableAndEnableOptions(bool isFolders) {
            //folder options
            cmdOpenFolder.Enabled = isFolders;
            cmdMoveFolder.Enabled = isFolders;
            //can the deletion buttons for folders be enabled?
            if (enableFolderDeletion) {
                cmdMoveFolderRecycleBin.Enabled = isFolders;
                cmdDelFoldersPermanently.Enabled = isFolders;
            } //if (enableFolderDeletion)
            //file options
            cmdOpenFile.Enabled = !isFolders;
            cmdMoveFiles.Enabled = !isFolders;
            cmdMoveFilesRecycleBin.Enabled = !isFolders;
            cmdDelFilesPermanently.Enabled = !isFolders;
        } //disableAndEnableOptions

        private void dgvFiles_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e) {
            try {
                //is the rightmouse-button clicked?
                if (e.Button == MouseButtons.Right) {
                    //set the selected cell to the one that was rightclicked
                    dgvFiles.CurrentCell = dgvFiles.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    disableAndEnableOptions(false);
                } //if (e.Button == MouseButtons.Right)
            } catch (ArgumentOutOfRangeException) {
                //in case user clicks the header, an ArgumentOutOfRangeException
                //is thrown. Since this only happens when header is rightclicked, 
                //I see no point in telling user not to click here
            } //try/catch
        } //dgvFiles_CellMouseDown

        private void dgvFiles_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            try {
                //does this cell contain a checkbox?
                if (e.ColumnIndex == MOVE_INDEX || e.ColumnIndex == DELETE_INDEX) {
                    //create objects of the current checkbox cells
                    DataGridViewCheckBoxCell chkMove = (DataGridViewCheckBoxCell)dgvFiles.Rows[e.RowIndex].Cells[MOVE_INDEX];
                    DataGridViewCheckBoxCell chkDelete = (DataGridViewCheckBoxCell)dgvFiles.Rows[e.RowIndex].Cells[DELETE_INDEX];
                    //check if the cell is already checked
                    bool moveValue = (bool)chkMove.FormattedValue;
                    bool deleteValue = (bool)chkDelete.FormattedValue;
                    //get the selected files name
                    string fileName = dgvFiles.Rows[e.RowIndex].Cells[0].Value.ToString();
                    //get the name without extension
                    fileName = System.IO.Path.GetFileNameWithoutExtension(fileName);
                    //update the values in the list based on current choices
                    updateList(fileName, (bool)chkMove.EditedFormattedValue, false);
                    updateList(fileName, (bool)chkDelete.EditedFormattedValue, true);
                    //check if something has changed
                    //has the option for move already been checked?
                    if (moveValue) {
                        //uncheck move
                        chkMove.Value = false;
                        //update list; set that this file is not to be moved
                        updateList(fileName, false, false);
                    } else if (deleteValue) { //is the option for deletion already checked?
                        //uncheck delete
                        chkDelete.Value = false;
                        //update list; set that this file is not to be deleted
                        updateList(fileName, false, true);
                    } //if (moveValue) 
                } //if (e.ColumnIndex == MOVE_INDEX || e.ColumnIndex == DELETE_INDEX)
            } catch (ArgumentOutOfRangeException) {
                //this exception is thrown if user clicks the header
                //as I don't see the point in telling the user not to click 
                //here, I catch the error and ignore it
            } //try/catch
        } //dgvFiles_CellContentClick

        private void dgvFiles_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e) {
            //is the header for the size clicked?
            if (e.ColumnIndex == 2) {
                //is the size to be sorted ascending?
                if (sortSizeAsc) {
                    refreshDataGridView(true);
                    sortSizeAsc = false;
                } else { //size to be sorted descending
                    refreshDataGridView(true);
                    sortSizeAsc = true;
                } //if (sortSizeAsc) 
            } //if (e.ColumnIndex == 2)
        } //dgvFiles_ColumnHeaderMouseClick
        #endregion

        #region RIGHTCLICK datagridview
        private void ctmDataGridView_Opening(object sender, CancelEventArgs e) {
            //is there any rows added to the datagridview?
            if (dgvFiles.Rows.Count >= 1) {
                //enable options
                tsmMove.Enabled = true;
                tsmDelete.Enabled = true;
                tsmOpenFile.Enabled = true;
            } else { //no rows, disable options
                tsmMove.Enabled = false;
                tsmDelete.Enabled = false;
                tsmOpenFile.Enabled = false;
            } //if (gridviewFiles.Rows.Count >= 1)
        } //ctmDataGridView_Opening

        private void tsmSelectMoveOne_Click(object sender, EventArgs e) {
            //get the row index of the selected file
            int rowIndex = dgvFiles.CurrentCell.RowIndex;
            DataGridViewCheckBoxCell chkMove = null;
            DataGridViewCheckBoxCell chkDelete = null;
            //set that this file is to be moved
            checkOneInDataGridView(chkMove, rowIndex, MOVE_INDEX, true, false);
            //set that this file is not to be deleted
            checkOneInDataGridView(chkDelete, rowIndex, DELETE_INDEX, false, true);
        } //tsmSelectMoveOne_Click

        private void tsmUnselectMoveOne_Click(object sender, EventArgs e) {
            //get the row index of the selected file
            int rowIndex = dgvFiles.CurrentCell.RowIndex;
            DataGridViewCheckBoxCell chkMove = null;
            //set that this file is not to be moved
            checkOneInDataGridView(chkMove, rowIndex, MOVE_INDEX, false, false);
        } //tsmUnselectMoveOne_Click

        private void tsmSelectAllMove_Click(object sender, EventArgs e) {
            //set all currently displayed files to be moved
            selectAllForMoving();
        } //tsmSelectAllMove_Click

        private void selectAllForMoving() {
            //set all files to be moved
            DataGridViewCheckBoxCell chkDelete = null;
            DataGridViewCheckBoxCell chkMove = null;
            //set that all displayed files are to be moved
            checkAllInDataGridView(chkMove, MOVE_INDEX, true, false);
            //set that none of the displayed files are to be deleted
            checkAllInDataGridView(chkDelete, DELETE_INDEX, false, true);
        } //selectAllForMoving

        private void tsmUnselectAllMove_Click(object sender, EventArgs e) {
            //set that no files are to be moved
            DataGridViewCheckBoxCell chkMove = null;
            checkAllInDataGridView(chkMove, MOVE_INDEX, false, false);
        } //tsmUnselectAllMove_Click

        private void tsmDeleteOne_Click(object sender, EventArgs e) {
            //get the row index of the selected file
            int rowIndex = dgvFiles.CurrentCell.RowIndex;
            DataGridViewCheckBoxCell chkMove = null;
            DataGridViewCheckBoxCell chkDelete = null;
            //set that this file is not to be moved
            checkOneInDataGridView(chkMove, rowIndex, MOVE_INDEX, false, false);
            //set that this file is to be deleted
            checkOneInDataGridView(chkDelete, rowIndex, DELETE_INDEX, true, true);
        } //tsmDeleteOne_Click

        private void tsmUncheckDelOne_Click(object sender, EventArgs e) {
            //set this file not to be deleted
            int rowIndex = dgvFiles.CurrentCell.RowIndex;
            DataGridViewCheckBoxCell chkDelete = null;
            checkOneInDataGridView(chkDelete, rowIndex, DELETE_INDEX, false, true);
        } //tsmUncheckDelOne_Click

        private void tsmSelectAllDel_Click(object sender, EventArgs e) {
            //set all currently displayed files to be deleted
            selectAllForDeletion();
        } //tsmSelectAllDel_Click

        private void selectAllForDeletion() {
            DataGridViewCheckBoxCell chkDelete = null;
            DataGridViewCheckBoxCell chkMove = null;
            //set that none of the displayed files are to be moved
            checkAllInDataGridView(chkMove, MOVE_INDEX, false, false);
            //set that all the displayed files are to be deleted
            checkAllInDataGridView(chkDelete, DELETE_INDEX, true, true);
        } //selectAllForDeletion

        private void tsmUncheckDelAll_Click(object sender, EventArgs e) {
            //set no files to be deleted
            DataGridViewCheckBoxCell chkDelete = null;
            checkAllInDataGridView(chkDelete, DELETE_INDEX, false, true);
        } //tsmUncheckDelAll_Click

        private void tsmOpenFile_Click(object sender, EventArgs e) {
            //if possible, open the chosen file
            openFile();
        } //tsmOpenFile_Click

        private void openFile() {
            try {
                //get the path...
                string file = treeviewFolders.SelectedNode.Text;
                //get the index of the chosen file
                int rowIndex = dgvFiles.CurrentCell.RowIndex;
                //add a backslash and the files name
                file += @"\" + dgvFiles.Rows[rowIndex].Cells[0].Value.ToString();
                //open the file
                System.Diagnostics.Process.Start(file);
            } catch (Win32Exception ex) {
                MessageBox.Show(ex.Message, "Can't open file");
                addItemInformation("Failed to open file: " + ex.Message);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Can't open file");
                addItemInformation("Failed to open file: " + ex.Message);
            } // try/catch
        } //openFile
        #endregion

        #region CHECK/UNCHECK CHECKBOXES datagridview
        /// <summary>
        /// This method checks or unchecks one checkbox in a row in the Datagridview.
        /// It checks/unchecks the row based on the rowIndex and the setValue.
        /// </summary>
        /// <param name="chkBox">Name of the DataGridViewCheckBoxCell</param>
        /// <param name="rowIndex">The row (index) where the checkbox exists</param>
        /// <param name="columnIndex">The column (index) where the checkbox exists</param>
        /// <param name="setValue">True = checked, False = unchecked</param>
        /// <param name="toDelete">True = Add/Remove delete from list, False = Add/Remove move from list</param>
        private void checkOneInDataGridView(DataGridViewCheckBoxCell chkBox, int rowIndex, int columnIndex, bool setValue, bool toDelete) {
            //get the datagridview checkbox
            chkBox = (DataGridViewCheckBoxCell)dgvFiles.Rows[rowIndex].Cells[columnIndex];
            //set new value to this checkbox
            chkBox.Value = setValue;
            //get this files name
            string fileName = dgvFiles.Rows[rowIndex].Cells[0].Value.ToString();
            //get the name without extension
            fileName = System.IO.Path.GetFileNameWithoutExtension(fileName);
            updateList(fileName, setValue, toDelete);
        } //checkOneInDataGridView

        /// <summary>
        /// This method checks or unchecks all the checkboxes in one row in the Datagridview.
        /// It checks/unchecks the rows based on the chkIndex and the setValue.
        /// </summary>
        /// <param name="chkBox">Name of the DataGridViewCheckBoxCell</param>
        /// <param name="chkIndex">The column (index) where the checkbox exists</param>
        /// <param name="setValue">True = checked, False = unchecked</param>
        /// <param name="toDelete">True = Add/Remove 'Delete' from list, False = Add/Remove 'Move' from list</param>
        private void checkAllInDataGridView(DataGridViewCheckBoxCell chkBox, int chkIndex, bool setValue, bool toDelete) {
            //for all rows (checkboxes) in the datagridview
            for (int i = 0; i < dgvFiles.Rows.Count; i++) {
                //get current checkbox
                chkBox = (DataGridViewCheckBoxCell)dgvFiles.Rows[i].Cells[chkIndex];
                //set new value to this checkbox
                chkBox.Value = setValue;
                string fileName = dgvFiles.Rows[i].Cells[0].Value.ToString();
                //get the name without extension
                fileName = System.IO.Path.GetFileNameWithoutExtension(fileName);
                updateList(fileName, setValue, toDelete);
            } //for
        } //checkAllInDataGridView

        /// <summary>
        /// Updates the content of the list based on the given values.
        /// </summary>
        /// <param name="fileName">The name of the file (used in List&lt;t&gt;.Find())</param>
        /// <param name="setValue">True = Set to be changed, False = Do nothing</param>
        /// <param name="toDelete">True = Add/Remove DELETE from list, False = Add/Remove MOVE from list</param>
        private void updateList(string fileName, bool setValue, bool toDelete) {
            //get the selected folder
            string parentFolder = treeviewFolders.SelectedNode.Text;
            //find the file object
            Files file = filesList.Find(f => f.getName() == fileName && f.getParentFolder() == parentFolder);
            //get the files index
            int index = filesList.IndexOf(file);
            //is the option connected to deletion?
            if (toDelete) {
                filesList[index].setToDelete(setValue);
            } else { //option connected to moving
                filesList[index].setToMove(setValue);
            } //if (toDelete)
            updateSizeEarned();
        } //updateList

        private void updateSizeEarned() {
            long sizeEarned = 0;
            foreach (Files file in filesList) {
                //is the current file to be moved or deleted?
                if (file.getToDelete() || file.getToMove()) {
                    sizeEarned += file.getSize();
                } //if (file.getToDelete() || file.getToMove())
            } //foreach
            //add the information about how much space will be 
            //earned after moving/deleting files
            fillLabelsWithSize(sizeEarned, lblSizeEarned, "Space that will be available: ");
        } //updateSizeEarned
        #endregion

        #endregion

        #region FOLDER OPTION - BUTTONS

        private void cmdOpenFolder_Click(object sender, EventArgs e) {
            //open the selected folder
            openFolder();
        } //cmdOpenFolder_Click

        #region MOVE FOLDERS
        private void cmdMoveFolder_Click(object sender, EventArgs e) {
            //move folder to a new location
            try {
                Folders moveFolder = folderList.Find(f => f.getName() == treeviewFolders.SelectedNode.Text);
                //the path to move the folder from
                string pathFrom = moveFolder.getName();
                //get the folders content
                int numFilesInFolder = moveFolder.getTotalNumberOfFiles();
                int numScannedFilesInFolder = moveFolder.getScannedNumberOfFiles();
                //get the content for this folders and the subfolders
                int numTotalForAllFolders = ControlClass.controlClass.countTotalNumberOfFiles(pathFrom, folderList);
                int numScannedForAllFolders = ControlClass.controlClass.countFilesScanned(pathFrom, folderList);
                //is all the folders content (files and subfolders files) displayed?
                if (numFilesInFolder > numScannedFilesInFolder || numTotalForAllFolders > numScannedForAllFolders || countSubFolders(pathFrom) > 0) {
                    //move the displayed content to a new location
                    moveFoldersContent(pathFrom, moveFolder);
                } else { //all content (files and subfolders files) is displayed
                    //move the whole folder and its content to a new location
                    moveWholeFolder(pathFrom, moveFolder);
                } //if (numFilesInFolder > numScannedFilesInFolder)
            } catch (OperationCanceledException) {
                operationCancelled("move");
                addItemInformation("The moving of folder was cancelled. Re-scan is recommended.");
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error");
                addItemInformation(ex.Message);
            } //try/catch
        } //cmdMoveFolder_Click

        /// <summary>
        /// Asks the user if he wants to rescan the chosen location to avoid errors 
        /// in case some of the folders/files were moved.
        /// </summary>
        /// <param name="operation">Lower caps description of cancelled operation</param>
        private void operationCancelled(string operation) {
            //message to display to the user
            string message = "The " + operation + " was cancelled.\n"
                    + "The currently displayed folders and files may be inaccurate.\n"
                    + "A re-scan of this folder and its subfolders are recommended to avoid errors.\n"
                    + "Do you want to re-scan this folder?";
            //ask the user if the user wants to re-scan the path
            if (MessageBox.Show(message, "Cancelled", MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) == DialogResult.Yes) {
                //start scanning the folder again
                startScan();
            } //if (MessageBox.Show)
        } //operationCancelled

        #region MOVE FOLDERS CONTENT

        private void moveFoldersContent(string pathFrom, Folders moveFolder) {
            //message to user
            string message = "The displayed content (files) is not all this folder contains.\n"
            + "A copy of this folder with displayed content will be created at chosen location.\n"
            + "Continue?";
            //does user still want to move this folder?
            if (MessageBox.Show(message, "Move?", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                fdbPath.Description = "Select a new location for the folder.";
                //enable possibility to create a new folder
                fdbPath.ShowNewFolderButton = true;
                DialogResult dialog = fdbPath.ShowDialog();
                //was a path chosen?
                if (dialog == DialogResult.OK) {
                    //the path to move the folder to
                    string pathTo = fdbPath.SelectedPath;
                    //get the subfolders
                    List<Folders> subFoldersToMove = getSubFolders(treeviewFolders.SelectedNode);
                    //get the files to be moved from this folder
                    List<Files> filesToBeMoved = (from files in filesList
                                                  where files.getParentFolder() == moveFolder.getName()
                                                  select files).ToList();
                    //get the files to be moved from the subfolders
                    foreach (Folders sub in subFoldersToMove) {
                        foreach (Files file in (from f in filesList
                                                where f.getParentFolder() == sub.getName()
                                                select f)) {
                            //add the file to the list
                            filesToBeMoved.Add(file);
                        } //foreach
                    }  //foreach
                    //move the folder and its content
                    ControlClass.controlClass.moveFoldersContent(pathFrom, pathTo, subFoldersToMove, filesToBeMoved);
                    //add information about the move to the listbox
                    addItemInformation("Moved folder from \"" + pathFrom + "\" to \"" + pathTo + "\"");
                    writeToLogAndRefreshViews(pathFrom, moveFolder);
                } //if (dialog == DialogResult.OK)
            } //if (MessageBox.Show(...)
        } //moveFoldersContent

        private List<Folders> getSubFolders(TreeNode parentNode) {
            //create a list and fill it with the selected folders subfolder(s)
            List<Folders> subFolders = new List<Folders>();
            getAllSubFolders(parentNode, subFolders);
            return subFolders;
        } //getSubFolders

        private void getAllSubFolders(TreeNode parentNode, List<Folders> nodes) {
            foreach (TreeNode childNode in parentNode.Nodes) {
                //find the folder
                Folders subFolder = folderList.Find(f => f.getName() == childNode.Text);
                //add it to the list
                nodes.Add(subFolder);
                //get the remaining subfolders
                getAllSubFolders(childNode, nodes);
            } //foreach
        } //getAllSubFolders
        #endregion

        private void moveWholeFolder(string pathFrom, Folders moveFolder) {
            fdbPath.Description = "Select a new location for the folder.";
            //enable possibility to create a new folder
            fdbPath.ShowNewFolderButton = true;
            DialogResult dialog = fdbPath.ShowDialog();
            //was a path chosen?
            if (dialog == DialogResult.OK) {
                //the path to move the folder to
                string pathTo = fdbPath.SelectedPath;
                //the content to be logged
                string logContent = "Folder was moved to " + pathTo;
                ControlClass.controlClass.setContent(logContent);
                //start to move the folder and its content
                ControlClass.controlClass.moveWholeFolder(pathFrom, pathTo);
                //log the folder that was moved
                ControlClass.controlClass.writeProgressToLog();
                //add information about the move to the listbox
                addItemInformation("Moved folder from: " + pathFrom + " to " + pathTo);
                writeToLogAndRefreshViews(pathFrom, moveFolder);
            } //if (dialog == DialogResult.OK)
        } //moveWholeFolder

        private void writeToLogAndRefreshViews(string pathFrom, Folders moveFolder) {
            //set the path that the files were moved from
            ControlClass.controlClass.setOperationPath(treeviewFolders.SelectedNode.Text);
            //write the status title to the log file
            ControlClass.controlClass.writeStatusToLog(4, "Moving completed: ");
            //update the treeview
            refreshTreeView(moveFolder);
        } //writeToLogAndRefreshViews
        #endregion

        private void cmdMoveFolderRecycleBin_Click(object sender, EventArgs e) {
            //move the folder to the Recycle bin
            try {
                Folders removeFolder = folderList.Find(f => f.getName() == treeviewFolders.SelectedNode.Text);
                string delPath = removeFolder.getName();
                ControlClass.controlClass.setContent(delPath);
                //move folder to recycle bin
                ControlClass.controlClass.moveFolderToRecycleBin(delPath);
                ControlClass.controlClass.writeProgressToLog();
                //write the status title to the log file
                ControlClass.controlClass.writeStatusToLog(5, "Deletion completed: ");
                //add information about the move to the listbox
                addItemInformation("Moved folder at: " + delPath + " to Recycle bin");
                //update the treeview
                refreshTreeView(removeFolder);
            } catch (OperationCanceledException) {
                operationCancelled("deletion");
                addItemInformation("The folder deletion was cancelled.");
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error");
                addItemInformation(ex.Message);
            } //try/catch
        } //cmdMoveFolderRecycleBin_Click

        private void cmdDelFoldersPermanently_Click(object sender, EventArgs e) {
            //delete the folder permanently (not restorable)
            try {
                //get the name of the folder
                string folderName = System.IO.Path.GetFileName(treeviewFolders.SelectedNode.Text);
                //message to display to the user
                string message = "This will delete " + folderName + " permanently.\n"
                            + "You will not be able to restore the folder.\n"
                            + "Do you want to delete the folder permanently?";
                //since this will delete the folder permanently, 
                //ask the user to confirm the action
                if (MessageBox.Show(message, "Delete permanently?",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) {
                    //get the folder to be deleted
                    Folders removeFolder = folderList.Find(f => f.getName() == treeviewFolders.SelectedNode.Text);
                    string delPath = removeFolder.getName();
                    ControlClass.controlClass.setContent(delPath);
                    //deletes permanently (no restorability from Recycle bin)
                    ControlClass.controlClass.deleteFoldersPermanently(delPath);
                    ControlClass.controlClass.writeProgressToLog();
                    //write the status title to the log file
                    ControlClass.controlClass.writeStatusToLog(6, "Deletion completed: ");
                    //add information about the move to the listbox
                    addItemInformation("Deleted folder at: " + delPath);
                    //update the treeview
                    refreshTreeView(removeFolder);
                } //if (MessageBox.Show(...)
            } catch (OperationCanceledException) {
                operationCancelled("deletion");
                addItemInformation("The folder deletion was cancelled.");
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error");
                addItemInformation(ex.Message);
            } //try/catch
        } //cmdDelFoldersPermanently_Click

        private void refreshTreeView(Folders removeFolder) {
            //update the drives size
            getDriveSize();
            //update the size earned
            updateSizeEarned();
            //remove the folder from the list
            folderList.Remove(removeFolder);
            //remove the selected node
            treeviewFolders.SelectedNode.Remove();
            //update the treeview
            treeviewFolders.Update();
            //get the number of nodes in the treeview
            int countNodes = treeviewFolders.GetNodeCount(true);
            //is there only one folder left in the treeview?
            if (countNodes == 1) {
                //get the name of the node
                string rootFolder = treeviewFolders.Nodes[0].Text;
                //get the number of files connected to this folder
                int numFiles = folderList.Find(f => f.getName() == rootFolder).getScannedNumberOfFiles();
                //does this folder have any scanned files?
                if (numFiles > 0) {
                    //update the datagridview
                    refreshDataGridView(false);
                    //set focus back to the treeview
                    treeviewFolders.Focus();
                } else { //no files connected to this folder
                    //clear the treeview
                    clearTreeViewAfterFolderAction();
                } //if (numFiles > 0)
                //is there any folders left in the treeview??
            } else if (countNodes > 1) {
                //update the datagridview
                refreshDataGridView(false);
                //set focus back to the treeview
                treeviewFolders.Focus();
            } else { //no folders left
                //clear the treeview
                clearTreeViewAfterFolderAction();
            } //if (countNodes == 1)
        } //refreshTreeView

        private void clearTreeViewAfterFolderAction() {
            setDefaultText(false);
            MessageBox.Show("No more files to display.", "Display");
            //clear the datagridview for rows
            dgvFiles.Rows.Clear();
            //disable buttons
            disableResultScreen();
            //view the first tabpage
            tabWindows.SelectedIndex = 0;
        } //clearTreeViewAfterFolderAction
        #endregion

        #region FILE OPTIONS - BUTTONS
        private void cmdOpenFile_Click(object sender, EventArgs e) {
            openFile();
        } //cmdOpenFile_Click

        private void cmdMoveFiles_Click(object sender, EventArgs e) {
            //moves file(s) to a new location
            try {
                int countFiles = ControlClass.controlClass.countFilesToMove(filesList);
                //does the list contain any files to be moved?
                if (countFiles >= 1) {
                    fdbPath.Description = "Select a folder to move the files to...";
                    //enable possibility to create a new folder
                    fdbPath.ShowNewFolderButton = true;
                    DialogResult dialog = fdbPath.ShowDialog();
                    //was a path chosen?
                    if (dialog == DialogResult.OK) {
                        string pathTo = fdbPath.SelectedPath;
                        //loop through all the files set to be moved
                        foreach (Files file in (from f in filesList
                                                where f.getToMove() == true
                                                select f).ToList()) {
                            //get the files path, name and extension
                            string pathFrom = file.getParentFolder() + @"\" + file.getName() + file.getExtension();
                            //get the name and filetype of the current file and get where it was moved to
                            string logContent = "Filename: " + file.getName() + file.getExtension() 
                                                    + " " + " was moved to: " + pathTo;
                            ControlClass.controlClass.setContent(logContent);
                            //move the file
                            ControlClass.controlClass.moveFiles(pathFrom, pathTo);
                            ControlClass.controlClass.writeProgressToLog();
                            //add information about the move to the listbox
                            addItemInformation(logContent);
                            //remove the file from the list
                            filesList.Remove(file);
                        } //foreach
                        afterFileOperation(MOVE_FILES, "Moving completed: ");
                    } //if (dialog == DialogResult.OK)
                } else { //no files are selected for deletion
                    MessageBox.Show("No files are set to be moved.", "None selected");
                } //if (countMove() >= 1) 
            } catch (OperationCanceledException) {
                operationCancelled("move");
                addItemInformation("Cancelled moving of files.");
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error");
                addItemInformation(ex.Message);
            } //try/catch
        } //cmdMoveFiles_Click

        private void cmdMoveFilesRecycleBin_Click(object sender, EventArgs e) {
            //moves file(s) to the recycle bin
            try {
                int countFiles = ControlClass.controlClass.countFilesToDelete(filesList);
                //is there any files selected for deletion?
                if (countFiles >= 1) {
                    //loop through all the files set to be deleted
                    foreach (Files file in (from f in filesList
                                            where f.getToDelete() == true
                                            select f).ToList()) {
                        //get the files path, name and extension
                        string deletePath = file.getParentFolder() + @"\" + file.getName() + file.getExtension();
                        ControlClass.controlClass.setContent(deletePath);
                        //move the file to the recycle bin
                        ControlClass.controlClass.moveFilesToRecycleBin(deletePath);
                        ControlClass.controlClass.writeProgressToLog();
                        //add information about the deletion to the listbox
                        addItemInformation("File at: " + deletePath + " was moved to Recycle bin");
                        //remove the file from the list
                        filesList.Remove(file);
                    } //foreach
                    afterFileOperation(MOVE_FILES_RECYCLE_BIN, "Deletion completed: ");
                } else { //no files are selected for deletion
                    MessageBox.Show("No files are set to be deleted.", "None selected");
                } //if (countDelete() >= 1)
            } catch (OperationCanceledException) {
                operationCancelled("deletion");
                addItemInformation("The file deletion was cancelled.");
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error");
                addItemInformation(ex.Message);
            } //try/catch
        } //cmdMoveFilesRecycleBin_Click

        private void cmdDelFilePermanently_Click(object sender, EventArgs e) {
            //deletes permanently (no recycle bin)
            try {
                int countFiles = ControlClass.controlClass.countFilesToDelete(filesList);
                //is there any files selected for deletion?
                if (countFiles >= 1) {
                    //message to display to the user
                    string message = "This will delete the chosen file(s) permanently.\n"
                            + "You will not be able to restore the deleted file(s).\n"
                            + "Do you want to delete the file(s) permanently?";
                    //since this will delete the files permanently, 
                    //ask the user to confirm the action
                    if (MessageBox.Show(message, "Delete permanently?",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) {
                        //loop through all the files set to be deleted
                        foreach (Files file in (from f in filesList
                                                where f.getToDelete() == true
                                                select f).ToList()) {
                            //get the files path, name and extension
                            string deletePath = file.getParentFolder() + @"\" + file.getName() + file.getExtension();
                            ControlClass.controlClass.setContent(deletePath);
                            //delete the file permanently
                            ControlClass.controlClass.deleteFilesPermanently(deletePath);
                            ControlClass.controlClass.writeProgressToLog();
                            //add information about the deletion to the listbox
                            addItemInformation("Deleted file: " + deletePath);
                            //remove the file from the list
                            filesList.Remove(file);
                        } //foreach
                        afterFileOperation(DELETE_FILES_PERMANENTLY, "Deletion completed: ");
                    } //if (MessageBox.Show(...))
                } else { //no files are selected for deletion
                    MessageBox.Show("No files are set to be deleted.", "None selected");
                } //if (countDelete() >= 1)
            } catch (OperationCanceledException) {
                operationCancelled("deletion");
                addItemInformation("The file deletion was cancelled.");
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error");
                addItemInformation(ex.Message);
            } //try/catch
        } //cmdDelFilePermanently_Click

        private void afterFileOperation(int operation, string logText) {
            //update the drives size
            getDriveSize();
            //update the size earned
            updateSizeEarned();
            //write the status title to the log file
            ControlClass.controlClass.writeStatusToLog(operation, logText);
            //update/refresh the datagridview
            refreshDataGridView(false);
            //set focus back to the treeview
            treeviewFolders.Focus();
        } //afterFileOperation
        #endregion
    } //frmGui
} //namespace