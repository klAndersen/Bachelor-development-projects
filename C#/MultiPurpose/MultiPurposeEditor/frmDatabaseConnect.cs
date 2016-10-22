using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//add MySQL
using MySql.Data.MySqlClient;
using MultiPurpose;

namespace MultiPurposeEditor {
    public partial class frmDatabaseConnect : Form {
        #region VARIABLER
        //constants for backgroundworker
        private const int LOG_IN = 0;
        private const int GET_DATABASER = 111;
        private const int GET_TABLES = 222;
        private const int QUERY = 333;
        private const int PORT = 3306;
        private int operation;
        private string cmbText = "";
        private bool cancelled;
        private string query;
        #endregion

        #region ARRAYS
        //array med servere
        private string[] serverArray = {
                                           "localhost"
                                       };
        //array med mysql-kommandoer
        private string[] parameterArray = {
                                           "CREATE DATABASE db_navn;",
                                           "CREATE TABLE tbl_navn (kolonne_navn data_type);",
                                           "DESCRIBE tbl_navn;",
                                           "SHOW DATABASES;",
                                           "SHOW TABLES;",
                                           "SHOW COMLUMNS FROM tbl_navn;",
                                           "USE db_navn;",
                                           "DROP DATABASE db_navn;",
                                           "DROP TABLE tbl_navn;",
                                           "SELECT * FROM tbl_navn WHERE felt_navn = 'verdi';",
                                           "ALTER TABLE tbl_navn DROP COLUMN kolonne_navn;",
                                           "ALTER TABLE tbl_navn ADD COLUMN ny_kolonne_navn data_type;",
                                           "DELETE FROM tbl_navn WHERE felt_navn = 'verdi';",
                                           "UPDATE tbl_navn SET verdi = ny_verdi WHERE id = ID;"
                                       };
        #endregion

        #region FORMLOAD
        public frmDatabaseConnect() {
            InitializeComponent();
            //set backgroundworker to support cancellation and reports
            bwLoading.WorkerSupportsCancellation = true;
            bwLoading.WorkerReportsProgress = true;
        } //constructor

        private void frmDatabaseConnect_Load(object sender, EventArgs e) {
            //set focus
            txtUser.Focus();
            //mask the passwordfield
            txtPwd.PasswordChar = '*';
            //hide controls
            hideAndShow();
            //fill comboboxes
            for (int i = 0; i < serverArray.Length; i++) {
                cmbServer.Items.Add(serverArray[i]);
            } //for
            for (int i = 0; i < parameterArray.Length; i++) {
                cmbParameter.Items.Add(parameterArray[i]);
            } //for
            //set that content of comboboxes can't be edited
            cmbDatabase.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTable.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbParameter.DropDownStyle = ComboBoxStyle.DropDownList;
            //set default selection
            cmbServer.SelectedIndex = 0;
            cmbParameter.SelectedIndex = 0;
            txtPort.Text = PORT.ToString();
        } //frmDatabaseConnect_Load

        private void hideAndShow() {
            //set visibility
            cmdUpdateDatabase.Visible = !cmdUpdateDatabase.Visible;
            cmdRunQuery.Visible = !cmdRunQuery.Visible;
            cmdLogOut.Visible = !cmdLogOut.Visible;
            cmdAddParameter.Visible = !cmdAddParameter.Visible;
            cmdCancel.Visible = !cmdCancel.Visible;
            dgvContent.Visible = !dgvContent.Visible;
            cmdAddDatabase.Visible = !cmdAddDatabase.Visible;
            cmdAddTable.Visible = !cmdAddTable.Visible;
            //hide labels
            lblSporring.Visible = !lblSporring.Visible;
            lblDB.Visible = !lblDB.Visible;
            lblTabell.Visible = !lblTabell.Visible;
            lblParameter.Visible = !lblParameter.Visible;
            //hide combobox
            cmbDatabase.Visible = !cmbDatabase.Visible;
            cmbTable.Visible = !cmbTable.Visible;
            cmbParameter.Visible = !cmbParameter.Visible;
            //hide textboxes
            txtQuery.Visible = !txtQuery.Visible;
            showLogIn();
        } //hideAndShow

        private void setToolTip() {
            //buttons tooltip
            tipHelp.SetToolTip(cmdUpdateDatabase, "Update the list over databases");
            tipHelp.SetToolTip(cmdAddDatabase, "Add chosen database to textfield for query");
            tipHelp.SetToolTip(cmdAddDatabase, "Add chosen table to textfield for query");
            tipHelp.SetToolTip(cmdAddParameter, "Add chosen value to textfield for query");
            tipHelp.SetToolTip(cmdRunQuery, "Run the query added to the textfield");
            tipHelp.SetToolTip(cmdLogOut, "Disconnect from server and log out");
            tipHelp.SetToolTip(cmdCancel, "Cancel running operation");
            tipHelp.SetToolTip(cmdClose, "Close this window");
            //combobox tooltip
            tipHelp.SetToolTip(cmbDatabase, "All the databases in your MySQL");
            tipHelp.SetToolTip(cmbServer, "Velg en server fra listen, eller skriv inn din egen");
            tipHelp.SetToolTip(cmbTable, "Viser alle tabellene til valgt database");
            tipHelp.SetToolTip(cmbParameter, "Parametere som kan settes inn i tekstfelt for spørring/query");
            //textboxes tooltip
            tipHelp.SetToolTip(txtUser, "Enter your username to connect to server (MySQL)");
            tipHelp.SetToolTip(txtPwd, "Enter your password to connect to server (if you have one)");
            tipHelp.SetToolTip(txtPort, "Enter the port to connect to (3306 is default)");
            tipHelp.SetToolTip(txtQuery, "Enter your query here - (no checks are done on entered query)");
        } //setToolTip

        private void showLogIn() {
            //show login area
            lblUser.Visible = true;
            lblPwd.Visible = true;
            lblServer.Visible = true;
            lblPort.Visible = true;
            txtUser.Visible = true;
            txtPwd.Visible = true;
            txtPort.Visible = true;
            cmbServer.Visible = true;
            cmdLogIn.Visible = true;
            cmdClose.Visible = true;
        } //showLogIn

        private void hideLogIn() {
            //hide login area
            lblUser.Visible = false;
            lblPwd.Visible = false;
            lblServer.Visible = false;
            lblPort.Visible = false;
            txtUser.Visible = false;
            txtPwd.Visible = false;
            txtPort.Visible = false;
            cmbServer.Visible = false;
            cmdLogIn.Visible = false;
            cmdClose.Visible = false;
        } //hideLogIn
        #endregion

        #region BUTTONS
        private void cmdAddDatabase_Click(object sender, EventArgs e) {
            //add chosen database to textfield for query
            txtQuery.Text += cmbDatabase.Text;
            //put focus back in textfield
            txtQuery.Focus();
        } //cmdAddDatabase_Click

        private void cmdUpdateDatabase_Click(object sender, EventArgs e) {
            //is the backgroundworker available
            if (!bwLoading.IsBusy) {
                //empty the combobox
                cmbDatabase.Items.Clear();
                //set the operation to be excecuted
                setOperation(GET_DATABASER);
                //start backgroundworker
                bwLoading.RunWorkerAsync();
            } //if (!bwLoading.IsBusy)
        } //cmdUpdateDatabase_Click

        private void cmdAddTable_Click(object sender, EventArgs e) {
            //add table to textfield for query
            txtQuery.Text += cmbTable.Text;
            //put focus back in textfield
            txtQuery.Focus();
        } //cmdAddTable_Click

        private void cmdAddParameter_Click(object sender, EventArgs e) {
            //add chosen parameter to query
            txtQuery.Text += cmbParameter.Text + "\n";
            //put focus back in textfield
            txtQuery.Focus();
        } //cmdAddParameter_Click

        private void cmdRunQuery_Click(object sender, EventArgs e) {
            try {
                if (!bwLoading.IsBusy) {
                    //get query
                    setQuery(txtQuery.Text);
                    //set operation
                    setOperation(QUERY);
                    //start backgroundworker
                    bwLoading.RunWorkerAsync();
                } //if (!bwLoading.IsBusy)
            } catch (MySqlException ex) {
                MessageBox.Show(ex.Message, "MySQL");
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error");
            } //try/catch
        } //cmdRunQuery_Click

        private void cmdCancel_Click(object sender, EventArgs e) {
            cancelBackgroundWorker();
        } //cmdCancel_Click

        private void cmdLogOut_Click(object sender, EventArgs e) {
            cancelBackgroundWorker();
            ControlClass.controlInstance.disconnectMySQL();
            hideAndShow();
            cancelled = false;
            lblStatus.Text = "Status: Log out successfull!";
            this.Close();
        } //cmdLogOut_Click

        private void cmdLogIn_Click(object sender, EventArgs e) {
            try {
                //is backgroundworker available
                if (!bwLoading.IsBusy) {
                    bool logIn = controlLogIn();
                    //is all set for login?
                    if (logIn) {
                        //get status for login
                        logIn = ControlClass.controlInstance.connectToMySQL(txtUser.Text, txtPwd.Text, txtPort.Text, cmbServer.Text);
                        if (logIn) {
                            //set operation to be excecuted
                            setOperation(LOG_IN);
                            //start backgroundworker
                            bwLoading.RunWorkerAsync();
                        } //if (logIn)
                    } //if (logIn)
                } //if (!bwLoading.IsBusy)
            } catch (MySqlException ex) {
                MessageBox.Show(ex.Message, "MySQL");
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error");
            } //try/catch
        } //cmdLogIn_Click

        private bool controlLogIn() {
            bool logIn = false;
            string errorMessage = "";
            //is username entered?
            if (txtUser.Text.Equals("")) {
                errorMessage = "Username must be entered.";
                MessageBox.Show(errorMessage, "No username");
                lblStatus.Text = "Status: " + errorMessage;
                //put focus in textfield
                txtUser.Focus();
            } else if (txtPwd.Text.Equals("")) {  //is password entered?
                if (MessageBox.Show("Password is not entered.\nIs password to be left empty?",
                            "No password", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                    logIn = true;
                    lblStatus.Text = "Status: Logging in...";
                } else { //password is to be entered
                    txtPwd.Focus();
                } //if (MessageBox)
            } else { //all fields are ok
                logIn = true;
            } //if (txtUser.Text.Equals(""))
            return logIn;
        } //controlLogIn

        private void cmdClose_Click(object sender, EventArgs e) {
            this.Close();
        } //cmdClose_Click
        #endregion

        #region BACKGROUNDWORKER
        private void bwLoading_DoWork(object sender, DoWorkEventArgs e) {
            switch (operation) {
                case LOG_IN:
                    //log in the user with the given port, server and password
                    bwLoading.ReportProgress(0);
                    getDatabases();
                    getTables();
                    break;
                case GET_DATABASER:
                    //get all the databases
                    bwLoading.ReportProgress(0);
                    getDatabases();
                    break;
                case GET_TABLES:
                    //get the chosen databases tables
                    bwLoading.ReportProgress(0);
                    getTables();
                    break;
                case QUERY:
                    //fill the datagridview with data
                    fillDatagridView(query);
                    break;
            } //switch (operasjon)
        } //bwLoading_DoWork

        private void bwLoading_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            lblStatus.Text = getStatus();
        } //bwLoading_ProgressChanged

        private void bwLoading_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            //was the operation cancelled?
            if (cancelled) {
                lblStatus.Text = "Status: The operation was cancelled!";
            } else if (e.Error != null) { //did an error occur?
                lblStatus.Text = e.Error.Message;
            } else { //operation completed, show result
                lblStatus.Text = "Status: Operation completed!";
                //was the operation a log in?
                if (getOperation() == LOG_IN) {
                    //show the logged in controls
                    hideAndShow();
                    //hide the login area
                    hideLogIn();
                    lblStatus.Text = "Status: Log in successfull!";
                } //if (getOperasjon() == LOG_IN)
                enableKnapper();
            } //if (e.Cancelled)
        } //bwLoading_RunWorkerCompleted

        private void enableKnapper() {
            //is there at least one table?
            if (cmbTable.Items.Count >= 1) {
                cmbTable.SelectedIndex = 0;
            } //if (cmbTabell.Items.Count >= 1)
        } //enableKnapper

        private void cancelBackgroundWorker() {
            if (bwLoading.WorkerSupportsCancellation) {
                bwLoading.CancelAsync();
                cancelled = true;
            } //if (bwLoading.WorkerSupportsCancellation)
        } //cancelBackgroundWorker
        #endregion

        #region COMBOBOX

        private void fillComboBox(string query, ComboBox cmb) {
            List<Object> objListe = ControlClass.controlInstance.returnListOfObjects(query);
            foreach (Object obj in objListe) {
                //fill combobox with data
                invokeFillingCmb(cmb, obj);
            } //foreach
        } //fyllComboBox

        private delegate void invokeFillingDelegateCmb(ComboBox cmb, Object obj);

        private void invokeFillingCmb(ComboBox cmb, Object obj) {
            if (cmb.InvokeRequired) {
                cmb.Invoke(new invokeFillingDelegateCmb(invokeFillingCmb), cmb, obj);
            } else {
                cmb.Items.Add(obj);
            } //if (cmb.InvokeRequired)
        } //invokeFyllingCmb

        private void cmbDatabase_TextChanged(object sender, EventArgs e) {
            if (!bwLoading.IsBusy) {
                cmbTable.Items.Clear();
                setOperation(GET_TABLES);
                bwLoading.RunWorkerAsync();
            } //if (!bwLoading.IsBusy) 
        }  //cmbDatabase_TextChanged

        #endregion

        #region GET OBJECTS
        private void getDatabases() {
            try {
                setStatus("Status: Getting databases...");
                string query = "show databases;";
                //fill combobox with databases
                fillComboBox(query, cmbDatabase);
                invokeSetCmbIndex(cmbDatabase, 0);
            } catch (MySqlException ex) {
                MessageBox.Show(ex.Message, "MySQL");
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error");
            } // try/catch
        } //getDatabases

        private delegate void invokeGettingDelegateCmbText(ComboBox cmb);

        private void invokeGettingCmbText(ComboBox cmb) {
            if (cmb.InvokeRequired) {
                cmb.Invoke(new invokeGettingDelegateCmbText(invokeGettingCmbText), cmb);
            } else {
                cmbText = cmb.Text;
            } //if (cmb.InvokeRequired)
        } //invokeGettingCmbText

        private delegate void invokeSetIndexDelegate(ComboBox cmb, int index);

        private void invokeSetCmbIndex(ComboBox cmb, int index) {
            if (cmb.InvokeRequired) {
                cmb.Invoke(new invokeSetIndexDelegate(invokeSetCmbIndex), cmb, index);
            } else {
                cmb.SelectedIndex = index;
            } //if (cmb.InvokeRequired)
        } //invokeCmbIndeks

        private void getTables() {
            try {
                setStatus("Status: Getting tables...");
                invokeGettingCmbText(cmbDatabase);
                string query = "use " + cmbText + ";";
                ControlClass.controlInstance.changeDatabase(query);
                query = "show tables;";
                //fill combobox
                fillComboBox(query, cmbTable);
            } catch (MySqlException ex) {
                MessageBox.Show(ex.Message, "MySQL");
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error");
            } //try/catch
        } //getTables
        #endregion

        #region DATAGRIDVIEW

        private void fillDatagridView(string query) {
            try {
                //get datatable from the ControlClass
                DataTable dataTable = ControlClass.controlInstance.returnDataTable(query);
                //create a new bindingsource
                BindingSource bindSource = new BindingSource();
                //set the datasource
                bindSource.DataSource = dataTable;
                //fill the datagridview
                invokeFyllingDatagridview(bindSource);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error");
            } //try/catch
        } //fillDatagridView

        private delegate void invokeFyllingDelegateDatagridView(BindingSource bindSource);

        private void invokeFyllingDatagridview(BindingSource bindSource) {
            if (dgvContent.InvokeRequired) {
                dgvContent.Invoke(new invokeFyllingDelegateDatagridView(invokeFyllingDatagridview), bindSource);
            } else {
                dgvContent.DataSource = bindSource;
            } //if (dgvContent.InvokeRequired)
        } //invokeFyllingDatagridview

        #endregion

        #region GET og SET
        private string getStatus() {
            return ControlClass.controlInstance.getDatabaseStatus();
        } //getStatus

        private int getOperation() {
            return operation;
        } //getOperation

        private string getQuery() {
            return query;
        } //getQuery

        private void setOperation(int operation) {
            this.operation = operation;
        } //setOperation

        private void setStatus(string status) {
            ControlClass.controlInstance.setDataBaseStatus(status);
        } //setStatus

        private void setQuery(string query) {
            this.query = query;
        } //setQuery
        #endregion
    } //frmDatabaseConnect
} //namespace