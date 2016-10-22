using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace guiModellTog {
    public partial class frmAbout : Form {

        public frmAbout() {
            InitializeComponent();
            this.Text = String.Format("About {0}", AssemblyTitle);
            this.lblProductName.Text = AssemblyProduct;
            this.lblVersion.Text = String.Format("Version {0}", AssemblyVersion);
            this.lblCopyright.Text = AssemblyCopyright;
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
            lagSeperator(lblSeperator1);
        } //frmAbout_Load

        private void lagSeperator(Label lbl) {
            lbl.AutoSize = false;
            lbl.Height = 2;
            lbl.BorderStyle = BorderStyle.Fixed3D;
        } //lagSeperator

        private void cmdOk_Click(object sender, EventArgs e) {
            this.Close();
        } //cmdOk_Click

        private void cmdShowTerms_Click(object sender, EventArgs e) {
            string fil = Environment.CurrentDirectory;
            fil += @"\License & Terms and conditions\Terms and Conditions.txt";
            lesFil(fil);
        } //cmdShowTerms_Click

        private void cmdShowLicense_Click(object sender, EventArgs e) {
            string fil = Environment.CurrentDirectory;
            fil += @"\License & Terms and conditions\License (GNU-GPL).txt";
            lesFil(fil);
        } //cmdShowLicense_Click

        private void lesFil(string filSti) {
            try {
                //hent beskrivelsen for kompilasjonen
                txtDescription.Text = AssemblyDescription;
                //legg til dobbelt linjeskift
                txtDescription.AppendText("\r\n\r\n");
                //opprett leser for filens innhold
                System.IO.StreamReader lesInnhold = new System.IO.StreamReader(filSti);
                //hent filens innhold
                string filInnhold = lesInnhold.ReadToEnd();
                //lukk leseren
                lesInnhold.Close();
                //legg til innholdet i tekstboksen
                txtDescription.AppendText(filInnhold);
            } catch (Win32Exception ex) {
                MessageBox.Show(ex.Message, "Kan ikke åpne filen");
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Kan ikke åpne filen");
            } // try/catch
        } //lesFil
    } //frmAbout
} //namespace