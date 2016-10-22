using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace CleanMyFolder {
    public partial class frmAbout : Form {

        public frmAbout() {
            InitializeComponent();
            this.Text = String.Format("About {0}", AssemblyTitle);
            this.lblProductName.Text = AssemblyProduct;
            this.lblVersion.Text = String.Format("Version {0}", AssemblyVersion);
            this.lblCopyright.Text = AssemblyCopyright;
            this.lblTrademark.Text = AssemblyTrademark;
            this.txtDescription.Text = AssemblyDescription;
        } //constructor

        #region Assembly Attribute Accessors

        public string AssemblyTitle {
            get {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0) {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "") {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion {
            get {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription {
            get {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0) {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct {
            get {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0) {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright {
            get {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0) {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyTrademark {
            get {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTrademarkAttribute), false);
                if (attributes.Length == 0) {
                    return "";
                }
                return ((AssemblyTrademarkAttribute)attributes[0]).Trademark;
            }
        }

        public string AssemblyCompany {
            get {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0) {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private void frmAbout_Load(object sender, EventArgs e) {
            createSeperator(lblSeperator1);
        } //frmAbout_Load

        private void createSeperator(Label lbl) {
            lbl.AutoSize = false;
            lbl.Height = 2;
            lbl.BorderStyle = BorderStyle.Fixed3D;
        } //createSeperator

        private void cmdOk_Click(object sender, EventArgs e) {
            this.Close();
        } //cmdOk_Click

        private void cmdShowTerms_Click(object sender, EventArgs e) {
            string file = Environment.CurrentDirectory;
            file += @"\License & Terms and conditions\Terms and Conditions.txt";
            readFile(file);
        } //cmdShowTerms_Click

        private void cmdShowLicense_Click(object sender, EventArgs e) {
            string file = Environment.CurrentDirectory;
            file += @"\License & Terms and conditions\License (GNU-GPL).txt";
            readFile(file);
        } //cmdShowLicense_Click

        private void readFile(string filePath) {
            try {
                //get the assembly description
                txtDescription.Text = AssemblyDescription;
                //add 2x newline
                txtDescription.AppendText("\r\n\r\n");
                //create a reader to read the files content
                System.IO.StreamReader readContent = new System.IO.StreamReader(filePath);
                //get the content in the file
                string fileContent = readContent.ReadToEnd();
                //close the reader
                readContent.Close();
                //add the content to the textbox
                txtDescription.AppendText(fileContent);
            } catch (Win32Exception ex) {
                MessageBox.Show(ex.Message, "Can't open file");
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Can't open file");
            } // try/catch
        } //readFile
    } //frmAbout
} //namespace