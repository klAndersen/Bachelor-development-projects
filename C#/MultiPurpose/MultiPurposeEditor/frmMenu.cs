using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MultiPurposeEditor {
    public partial class frmMenu : Form {

        public frmMenu() {
            InitializeComponent();
        } //constructor

        private void cmdGenerateText_Click(object sender, EventArgs e) {
            frmGenerateText generate = new frmGenerateText();
            generate.Show();
        } //cmdGenerateText_Click

        private void cmdCreateReadMe_Click(object sender, EventArgs e) {
            frmCreateReadMe readMe = new frmCreateReadMe();
            readMe.Show();
        } //cmdCreateReadMe_Click

        private void cmdMySQLConnect_Click(object sender, EventArgs e) {
            frmDatabaseConnect dbConnect = new frmDatabaseConnect();
            dbConnect.Show();
        } //cmdMySQLConnect_Click

        private void cmdAbout_Click(object sender, EventArgs e) {
            string aboutProg = "MultiPurpose Editor  Copyright (C) 2012  Knut Lucas Andersen.";
            aboutProg += "\nThis program comes with ABSOLUTELY NO WARRANTY; \nSee Terms and Conditions §15-§17";
            aboutProg += "\nThis is free software, and you are welcome to redistribute it under certain conditions;"
                         + "\nSee Terms and Conditions. ";
            MessageBox.Show(aboutProg, "About");
        } //cmdAbout_Click

        private void cmdExit_Click(object sender, EventArgs e) {
            this.Close();
        } //cmdExit_Click

        private void frmMenu_Load(object sender, EventArgs e) {
            cmdGenerateText.BackColor = Color.LightBlue;
            cmdCreateReadMe.BackColor = Color.LightBlue;
            cmdMySQLConnect.BackColor = Color.LightBlue;
            cmdAbout.BackColor = Color.LightBlue;
            cmdExit.BackColor = Color.LightBlue;
        } //frmMenu_Load
    } //frmMenu
} //namespace