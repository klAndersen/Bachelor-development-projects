using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

//add reference to MySql.Data.dll 
//exists in .\programfiles\mysql\connector\assemblies
using MySql.Data.MySqlClient;

namespace MultiPurpose {
    internal sealed class Database {
        #region VARIABLES
        //constants for connection string
        private const string SERVER = "SERVER=";
        private const string PORT = "PORT=";
        private const string DATABASE = "DATABASE=";
        private const string USER = "UID=";
        private const string PWD = "PASSWORD=";
        //connection to database
        private MySqlConnection connection;
        private string status;
        private bool loggedIn = false;
        #endregion

        #region CONNECTING & CLOSING CONNECTION
        /// <summary>
        /// Attempts to connect to a MySQL server with the given values
        /// </summary>
        /// <param name="server">The server to connect to</param>
        /// <param name="port">The port to connect to</param>
        /// <param name="user">The users username</param>
        /// <param name="pwd">The users password</param>
        /// <returns>bool</returns>
        internal bool connectToDB(string server, string port, string user, string pwd) {
            bool connected = false;
            try {
                connection = null;
                string serverString = SERVER + server + ";";
                string portString = PORT + port + ";";
                string dbString = DATABASE + ";";
                string userString = USER + user + ";";
                string pwdString = PWD + pwd + ";";
                //string for connection
                string connString = serverString + portString + dbString + userString + pwdString;
                connString += "Persist Security Info=false;";
                //create new connection
                connection = new MySqlConnection(connString);
                //open connection
                connection.Open();
                //set that the connection was successfull
                connected = true;
            } catch (Exception ex) {
                throw ex;
            } //try / catch
            return connected;
        } //connectToDB

        /// <summary>
        /// Closes the connection to the database and logs out the user
        /// </summary>
        internal void closeConnection() {
            //close the connection to the database
            connection.Close();
        } //closeConnection

        #endregion

        #region RUN COMMAND AND GET DATA

        private MySqlDataReader runCommand(string query) {
            try {
                //create a command to the database
                MySqlCommand command = connection.CreateCommand();
                //set the command
                command.CommandText = query; 
                //run query
                MySqlDataReader reader = command.ExecuteReader();
                return reader;
            } catch (Exception ex) {
                throw ex;
            } // try/catch
        } //runCommand

        /// <summary>
        /// Connect to another database
        /// </summary>
        /// <param name="query">Query containing the change of database</param>
        internal void changeDatabase(string query) {
            try {
                MySqlDataReader reader = runCommand(query);
                reader.Close();
            } catch (Exception ex) {
                throw ex;
            } //try/catch
        } //changeDatabase

        /// <summary>
        /// Returns a List&lt;Objects&gt; containg the values from the query
        /// </summary>
        /// <param name="query">The query to be run</param>
        /// <returns>List&lt;Objects&gt;</returns>
        internal List<Object> returnListOfObjects(string query) {
            List<Object> objList = new List<object>();
            MySqlDataReader reader = null;
            int i = 0;
            try {
                setStatus("Status: Henter data...");
                //run the query
                reader = runCommand(query); 
                //as long as there is results
                while (reader.Read()) {
                    for (int j = 0; j < reader.FieldCount; j++) {
                        //get the value
                        objList.Add(reader.GetValue(j));
                    } //for
                    i++;
                } //while
            } catch (Exception ex) {
                throw ex;
            } finally {
                if (reader != null) {
                    //close the reader
                    reader.Close();
                } //if (reader != null)
            } // try/catch
            return objList;
        } //returnListOfObjects

        /// <summary>
        /// Returns a DataTable filled with the result from the 
        /// query
        /// </summary>
        /// <param name="query">The query to be excecuted</param>
        /// <returns>DataTable</returns>
        internal DataTable returnDataTable(string query) {
            try {
                //create an adapter
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                //run the query
                adapter.SelectCommand = new MySqlCommand(query, connection);
                //create a datatable
                DataTable dataTable = new DataTable();
                //fill the datatable
                adapter.Fill(dataTable);
                //return the filled datatable
                return dataTable;
            } catch (Exception ex) {
                throw ex;
            } // try/catch
        } //returnDataTable

        #endregion

        #region GET og SET
        internal string getStatus() {
            return status;
        } //getStatus

        internal void setStatus(string status) {
            this.status = status;
        } //setStatus

        internal bool getLoggedIn() {
            return loggedIn;
        } //getLoggedIn

        internal void setLoggedIn(bool loggedIn) {
            this.loggedIn = loggedIn;
        } //setLoggedIn
        #endregion
    } //Database
} //namespace