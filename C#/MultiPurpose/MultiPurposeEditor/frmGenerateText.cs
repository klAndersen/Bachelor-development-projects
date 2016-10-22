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
    public partial class frmGenerateText : Form {
        #region ARRAYS
        private string[] browserArray = {
                                            "Internet Explorer",
                                            "Google Chrome",
                                            "Mozilla Firefox",
                                            "Opera",
                                            "Safari"
                                         };

        private string[] browserProcess = {
                                              "IExplore.exe",
                                              "chrome.exe",
                                              "firefox.exe",
                                              "opera.exe",
                                              "safari.exe"
                                          };
        #endregion

        #region FORMLOAD

        public frmGenerateText() {
            InitializeComponent();
        } //constructor

        private void frmGenerateText_Load(object sender, EventArgs e) {
            for (int i = 0; i < browserArray.Length; i ++) {
                //add browsers combobox
                cmbBrowser.Items.Add(browserArray[i]);
            } //for
            //choose the first browser in the list
            cmbBrowser.SelectedIndex = 0;
            //colour the buttons
            cmdPrintBeginning.BackColor = Color.LightBlue;
            cmdPrintStartEnd.BackColor = Color.LightBlue;
            cmdPrintNoText.BackColor = Color.LightBlue;
            cmdPrintNoAndTxt.BackColor = Color.LightBlue;
            cmdEmpty.BackColor = Color.LightBlue;
            cmdHelp.BackColor = Color.LightBlue;
            cmdClose.BackColor = Color.LightBlue;
        } //frmGenerateText_Load

        #endregion

        #region BUTTONS
        private void cmdPrintBeginning_Click(object sender, EventArgs e) {
            //print only the beginning of the text
            printText(false, false);
        } //cmdPrintBeginning_Click

        private void cmdPrintStartEnd_Click(object sender, EventArgs e) {
            //print the beginning and the end of the text
            printText(false, true);
        } //cmdPrintStartEnd_Click

        private void cmdPrintNoText_Click(object sender, EventArgs e) {
            //print the starttext and the numbers
            printText(true, false);
        } //cmdPrintNoText_Click

        private void cmdPrintNoAndTxt_Click(object sender, EventArgs e) {
            //print the starttext, the numbers and the endtext
            printText(true, true);
        } //cmdPrintNoAndTxt_Click

        private void printText(bool numeric, bool multiple) {
            //get the value of the textfields
            string beginning = txtStartText.Text;
            string ending = txtEndText.Text;
            string start = txtStartNumber.Text;
            string end = txtEndNumber.Text;
            string jump = txtJump.Text;
            try {
                int startValue = 0;
                int endValue = 0;
                int jumpValue = 0;
                int.TryParse(start, out startValue);
                int.TryParse(end, out endValue);
                //is jump entered?
                if (!jump.Equals("")) {
                    int.TryParse(jump, out jumpValue);
                } //if (jump.equals(""))
                string print;
                //shall text be generated with numeric values?
                if (numeric) {
                    //shall text be generated with start/end text?
                    if (multiple) {
                        print = ControlClass.controlInstance.printNumericStartEnd(beginning, startValue, endValue, jumpValue, ending);
                    } else { //text to be generated with start text only
                        print = ControlClass.controlInstance.printNumericText(beginning, startValue, endValue, jumpValue);
                    } //if (multiple)
                } else { //just generate pure text
                    //shall text be generated with start/end?
                    if (multiple) {
                        print = ControlClass.controlInstance.printTextStartEnd(beginning, startValue, endValue, ending);
                    } else { //text generated has only a start value
                        print = ControlClass.controlInstance.printTextOnly(beginning, startValue, endValue);
                    } //if (multiple)
                } //if (numeric)
                //fills the textarea with the printed text
                txtResult.Text = "//Auto-generated text:\n" + print;
                //does the richtexbox contain links?
                if (txtResult.DetectUrls) {
                    //show the browser choice
                    lblBrowser.Visible = true;
                    cmbBrowser.Visible = true;
                } //if (txtResult.DetectUrls)
            } catch (FormatException) { //not a number typed into the numeric fields
                MessageBox.Show("You have to type in a number into the number fields.", "Not a number");
                txtStartNumber.Focus();
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error");
                txtStartNumber.Focus();
            } // try/catch
        } //checkFields

        private void cmdEmpty_Click(object sender, EventArgs e) {
            txtStartText.Text = "";
            txtEndText.Text = "";
            txtStartNumber.Text = "";
            txtEndNumber.Text = "";
            txtJump.Text = "";
            txtResult.Text = "";
            //hide the browser
            lblBrowser.Visible = false;
            cmbBrowser.Visible = false;
        } //cmdEmpty_Click

        private void cmdHelp_Click(object sender, EventArgs e) {
            string help;
            help = "This program generates text for use in programming.";
            help += "\nYou have choices between generating with and";
            help += "without numeric values.";
            help += "\nThe numeric value is added after the start text and";
            help += "before the end text.";
            MessageBox.Show(help, "Help");
            txtStartText.Focus();
        } //cmdHelp_Click

        private void cmdClose_Click(object sender, EventArgs e) {
            this.Close();
        } //cmdClose_Click

        #endregion

        private void txtResult_LinkClicked(object sender, LinkClickedEventArgs e) {
            //opens the link in a webbrowser
            try {
                //get the application to start
                int index = cmbBrowser.SelectedIndex;
                //get application process
                string browser = browserProcess[index];
                //open the link in the selected browser
                System.Diagnostics.Process.Start(browser, e.LinkText);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error");
            } //try/catch
        } //
    } //frmGenerateText
} //namespace