using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MultiPurpose;

namespace MultiPurposeEditor {
    public partial class frmCreateReadMe : Form {
        private string LABEL_DEFAULT = "Select a folder to save the ReadMe.txt...";

        public frmCreateReadMe() {
            InitializeComponent();
        } //constructor

        private void frmCreateReadMe_Load(object sender, EventArgs e) {
            //set backgroundcolour on the buttons
            cmdBrowse.BackColor = Color.PaleGreen;
            cmdClose.BackColor = Color.PaleGreen;
            cmdShowExample.BackColor = Color.PaleGreen;
            cmdInsertAsteric.BackColor = Color.PaleGreen;
            cmdEmptyFields.BackColor = Color.PaleGreen;
            cmdSave.BackColor = Color.PaleGreen;
        } //frmCreateReadMe_Load

        #region BUTTONS
        private void cmdBrowse_Click(object sender, EventArgs e) {
            DialogResult dr = fdbFilePath.ShowDialog();
            //was a path selected?
            if (dr == DialogResult.OK) {
                //put selected path in label
                lblFilePath.Text = fdbFilePath.SelectedPath;
                //if the filepath is to long, display it in a tooltip
                tipHelp.SetToolTip(lblFilePath, lblFilePath.Text);
            } //if (dr == DialogResult.OK)
        } //cmdBrowse_Click

        private void cmdClose_Click(object sender, EventArgs e) {
            //close this window
            this.Close();
        } //cmdClose_Click

        private void cmdShowExample_Click(object sender, EventArgs e) {
            //add an example to the textfield
            txtContent.Text = ControlClass.controlInstance.returnReadMeExample();
        } //cmdShowExample_Click

        private void cmdInsertAsteric_Click(object sender, EventArgs e) {
            txtContent.Text += ControlClass.controlInstance.createStars();
        } //cmdInsertAsteric_Click

        private void cmdEmptyFields_Click(object sender, EventArgs e) {
            //empty all the fields
            lblFilePath.Text = LABEL_DEFAULT;
            txtFileName.Text = "";
            txtTitle.Text = "";
            txtVersion.Text = "";
            txtContent.Text = "";
            //uncheck the checkboxes
            chkOverWrite.Checked = false;
            chkAsteric.Checked = false;
            chkIncludeVersion.Checked = false;
        } //cmdEmptyFields_Click

        private void cmdSave_Click(object sender, EventArgs e) {
            saveToFile();
        } //cmdSave_Click

        private void saveToFile() {
            //get the textvalues
            string filePath = lblFilePath.Text;
            string title = txtTitle.Text;
            string vNo = txtVersion.Text;
            string content = txtContent.Text;
            //get the checkbox values
            bool overwrite = chkOverWrite.Checked;
            bool incAsteric = chkAsteric.Checked;
            bool incVersion = chkIncludeVersion.Checked;
            try {
                //is a filepath selected?
                if (filePath.Equals(LABEL_DEFAULT)) {
                    MessageBox.Show("No path is selected. Please select a location to save the file.", "No filepath");
                } else if (txtFileName.Text.Equals("")) { //is filename entered?
                    MessageBox.Show("Filename is empty. Please enter a filename.", "No filename");
                    txtFileName.Focus();
                } else if (title.Equals("")) { //is a title entered?
                    MessageBox.Show("Title is empty. Please enter a title.", "No title");
                    txtTitle.Focus();
                } else if (incVersion && vNo.Equals("")) { //is vno to be included, and is it empty?
                    MessageBox.Show("Version number is empty. If versionnumber is to be included, you must enter a version number.", "No versionumber");
                    txtVersion.Focus();
                } else {
                    //get the filename and add a backslash and filetype
                    string fileName = @"\" + txtFileName.Text + ".txt";
                    //add the filename to the filepath
                    filePath += fileName;
                    //try to save the file
                    ControlClass.controlInstance.saveReadMeToFile(filePath, title, vNo, content, incAsteric, overwrite, incVersion);
                    //tell user everything went okay
                    MessageBox.Show("The content was saved to file sucessfully!", "Saved");
                } //if (filePath.Equals(LABEL_DEFAULT))
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error");
            } //try/catch
        }
        #endregion
    } //frmCreateReadMe
} //namespace