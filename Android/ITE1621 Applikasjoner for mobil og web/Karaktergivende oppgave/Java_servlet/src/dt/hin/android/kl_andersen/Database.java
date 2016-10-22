package dt.hin.android.kl_andersen;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;

/**
 * Database-klasse som tar for seg alt tilknyttet MySQL. <br />
 * Tilgangsmodifier er enten protected eller private på alle funksjoner. <br />
 * Databasen benytter seg av PreparedStatement for å unngå SQL-injeksjon. <br />
 * Funksjonene er laget slik at etter data-uthenting/manipulasjon er over, så 
 * lukkes tilkoblingen til databasen.
 * @author Knut Lucas Andersen
 */
public final class Database {
	//database
	private static final String DATABASE_NAME = "KnutLucasDB";
	private static final String DATABASE_DRIVER = "com.mysql.jdbc.Driver";
	private static final String DATABASE_URL = "jdbc:mysql://kark.hin.no:3306/";
	//database bruker ("root")
	private static final String DATABASE_USER = "knutlucas";
	private static final String DATABASE_PWD = "knutlucas";
	//database tabell med Administrator info
	private static final String DATABASE_TBLADMIN = "tblAdmin";
	private static final String COLUMN_ADMIN_USERNAME = "brukerNavn";
	private static final String COLUMN_ADMIN_PWD = "pwd";
	@SuppressWarnings("unused")
	//tenkt modifikator for adgangsnivå; f.eks; Administrator og moderator
	//----ikke implementert---
	private static final String COLUMN_ADMIN_ADGANGSNIVAA = "adgangsNivaa";
	//database tabell med Klient info
	private static final String DATABASE_TBLKLIENT = "tblKlienter";
	private static final String COLUMN_REGISTRATION_ID = "registrationID";
	private static final String COLUMN_ENHETSNAVN = "enhetsNavn";
	//dato enhet ble registrert på server (ikke i bruk i spørringer)
	private static final String COLUMN_REGISTRERT_DATO = "registrertDato";

	private Database() {
		throw new UnsupportedOperationException();
	} //konstruktør

	/**
	 * Forsøker å koble til database basert på oppgitte verdier. <br />
	 * Verdiene er basert på satte konstanter.
	 * @return Connection: Tilkobling til databasen eller null.
	 * @throws ClassNotFoundException
	 * @throws SQLException
	 */
	private static Connection kobleTilDatabase() throws ClassNotFoundException, SQLException {
		Connection tilkobling = null;
		try {
			//hent driver og forsøk å koble til databasen
			Class.forName(DATABASE_DRIVER);
			tilkobling = DriverManager.getConnection(DATABASE_URL + DATABASE_NAME, DATABASE_USER, DATABASE_PWD);
		} catch (ClassNotFoundException ex) {
			throw new ClassNotFoundException("Database driver ikke funnet!", ex);		
		} catch (SQLException ex) {
			throw new SQLException("Kunne ikke koble til database!", ex);
		} //try/catch
		return tilkobling;
	} //kobleTilDatabase

	/**
	 * Denne funksjonen lagrer en ny enhet i databasen. <br />
	 * @param registrationID - String: Enhetens registrationID
	 * @param enhetsnavn - String: Enhetens navn
	 * @param dagensDato - String: Dagens dato
	 * @throws SQLException
	 * @throws ClassNotFoundException
	 */
	protected static void insertIntoTblKlienter(String registrationID, String enhetsnavn, String dagensDato) throws SQLException, ClassNotFoundException {
		Connection tilkobling = null;
		PreparedStatement sporring = null;
		ResultSet resultat = null;
		boolean finnes = false;
		try {
			//forsøk å koble til databasen
			tilkobling = kobleTilDatabase();
			String query = "INSERT INTO " + DATABASE_TBLKLIENT + " VALUES (" 
					+ "null," 
					+ "'" + registrationID + "',"
					+ "'" + enhetsnavn +  "',"
					+ "'" + dagensDato + "');";
			//opprett spørring og lagre enhet i database
			sporring = tilkobling.prepareStatement(query);
			sporring.executeUpdate();
		} catch (ClassNotFoundException ex) {
			throw new ClassNotFoundException("Database driver ikke funnet!", ex);		
		} catch (SQLException ex) {
			throw new SQLException("SQLException: Kunne ikke lagre ny enhet!", ex);
		} finally {
			avsluttDatabaseTilkobling(resultat, sporring, tilkobling);		
		} //try/catch
		if (finnes) {
			updateEnhetsnavn(registrationID, enhetsnavn);
		} //if (finnes)
	} //lagreNyEnhet

	/**
	 * Sammenligner oversendt registrationID med de som er registrert i database. <br />
	 * Dersom ID finnes returneres enheten, hvis ikke returneres null.
	 * @param registrationID - String: ID som skal søkes opp
	 * @return Enhet: Objekt av enhet || null
	 * @throws SQLException
	 * @throws ClassNotFoundException
	 */
	protected static Enhet erEnhetRegistrert(String registrationID, double latitude, double longitude) throws SQLException, ClassNotFoundException {
		Connection tilkobling = null;
		PreparedStatement sporring = null;
		ResultSet resultat = null;
		Enhet enhet = null;
		try {
			String enhetsNavn = "",
					registrertDato = "";
			//forsøk å koble til databasen
			tilkobling = kobleTilDatabase();
			String query = "SELECT " + COLUMN_ENHETSNAVN + ", " + COLUMN_REGISTRERT_DATO
					+ " FROM " + DATABASE_TBLKLIENT 
					+ " WHERE " + COLUMN_REGISTRATION_ID + " = '" + registrationID + "';";
			//opprett spørring og hent ut resultat fra databasen
			sporring = tilkobling.prepareStatement(query);
			resultat = sporring.executeQuery();
			while(resultat.next()) {
				//hent ut verdier og opprett enhet
				enhetsNavn = resultat.getString(COLUMN_ENHETSNAVN);
				registrertDato = resultat.getString(COLUMN_REGISTRERT_DATO);
				enhet = new Enhet(registrationID, enhetsNavn, latitude, longitude, registrertDato);
			} //while(resultat.next())
		} catch (ClassNotFoundException ex) {
			throw ex;	
		} catch (SQLException ex) {
			throw ex;
		} finally {
			avsluttDatabaseTilkobling(resultat, sporring, tilkobling);
		} //try/catch
		return enhet;
	} //erEnhetRegistrert

	/**
	 * Oppdaterer enhet i databasen ved å bytte ut gammel registrationID med den nye.
	 * @param gammelRegID - String: Original registrationID
	 * @param nyRegID - String: Den nye registrationID
	 * @throws ClassNotFoundException
	 * @throws SQLException
	 */
	protected static void updateRegistrationID(String gammelRegID, String nyRegID) throws ClassNotFoundException, SQLException {
		Connection tilkobling = null;
		PreparedStatement sporring = null;
		ResultSet resultat = null;
		try {
			//forsøk å koble til databasen
			tilkobling = kobleTilDatabase();
			String query = "UPDATE " + DATABASE_TBLKLIENT 
					+ " SET " + COLUMN_REGISTRATION_ID + " = '" + nyRegID + "'"
					+ " WHERE " + COLUMN_REGISTRATION_ID + " = '" + gammelRegID + "';";
			//opprett spørring og oppdater registrationID i database
			sporring = tilkobling.prepareStatement(query);
			sporring.executeUpdate();
			sporring = tilkobling.prepareStatement(query);
		} catch (ClassNotFoundException ex) {
			throw new ClassNotFoundException("Database driver ikke funnet!", ex);		
		} catch (SQLException ex) {
			throw new SQLException("SQLException: Kunne ikke hente resultater!", ex);
		} finally {
			avsluttDatabaseTilkobling(resultat, sporring, tilkobling);		
		} //try/catch
	} //updateRegistrationID

	protected static void updateEnhetsnavn(String registrationID, String enhetsNavn) throws ClassNotFoundException, SQLException {
		Connection tilkobling = null;
		PreparedStatement sporring = null;
		ResultSet resultat = null;
		try {
			//forsøk å koble til databasen
			tilkobling = kobleTilDatabase();
			String query = "UPDATE " + DATABASE_TBLKLIENT 
					+ " SET " + COLUMN_ENHETSNAVN + " = '" + enhetsNavn + "'"
					+ " WHERE " + COLUMN_REGISTRATION_ID + " = '" + registrationID + "';";
			//opprett spørring og oppdater registrationID i database
			sporring = tilkobling.prepareStatement(query);
			sporring.executeUpdate();
			sporring = tilkobling.prepareStatement(query);
		} catch (ClassNotFoundException ex) {
			throw new ClassNotFoundException("Database driver ikke funnet!", ex);		
		} catch (SQLException ex) {
			throw new SQLException("SQLException: Kunne ikke hente resultater!", ex);
		} finally {
			avsluttDatabaseTilkobling(resultat, sporring, tilkobling);		
		} //try/catch
	} //updateEnhetsnavn

	/**
	 * Sletter enhet fra databasen med oppgitt registrationID.
	 * @param registrationID - String: RegistrationID på enhet som skal slettes
	 * @throws ClassNotFoundException
	 * @throws SQLException
	 */
	protected static void slettEnhet(String registrationID) throws ClassNotFoundException, SQLException {
		Connection tilkobling = null;
		PreparedStatement sporring = null;
		ResultSet resultat = null;
		try {
			//forsøk å koble til databasen
			tilkobling = kobleTilDatabase();
			String query = "DELETE FROM " + DATABASE_TBLKLIENT 
					+ " WHERE " + COLUMN_REGISTRATION_ID + "= '" + registrationID + "';";
			//opprett spørring og slett enhet fra databasen
			sporring = tilkobling.prepareStatement(query);
			sporring.executeUpdate();
		} catch (ClassNotFoundException ex) {
			throw new ClassNotFoundException("Database driver ikke funnet!", ex);		
		} catch (SQLException ex) {
			throw new SQLException("SQLException: Kunne ikke hente resultater!", ex);
		} finally {
			avsluttDatabaseTilkobling(resultat, sporring, tilkobling);		
		} //try/catch
	} //slettEnhet

	/**
	 * Funksjon som henter ut innloggingsdata fra MySQL og sammenligner 
	 * innloggingsdata med data registrert i databasen.
	 * @param username - String: brukernavn
	 * @param pwd - String: passord
	 * @return boolean: True - bruker finnes, <br />
	 * false - feil innloggingsinformasjon/bruker finnes ikke 
	 * @throws SQLException
	 * @throws ClassNotFoundException
	 */
	protected static boolean loggInnBruker(String username, String pwd) throws SQLException, ClassNotFoundException {
		Connection tilkobling = null;
		PreparedStatement sporring = null;
		ResultSet resultat = null;
		boolean innlogget = false;
		try {
			//forsøk å koble til databasen
			tilkobling = kobleTilDatabase();
			String query = "SELECT * FROM " + DATABASE_TBLADMIN + ";";
			//opprett spørring og hent ut resultat fra databasen
			sporring = tilkobling.prepareStatement(query);
			resultat = sporring.executeQuery();
			//loop gjennom resultatet og se om admin ble funnet
			while(resultat.next() && !innlogget) {
				//finnes oppgitt brukernavn og passord i databasen?
				if (username.equals(resultat.getString(COLUMN_ADMIN_USERNAME)) 
						&& pwd.equals(resultat.getString(COLUMN_ADMIN_PWD))) {
					innlogget = true;
				} //if (username.equals(...)  && pwd.equals(...))
			} //while(resultat.next() && !funnet)
		} catch (SQLException ex) {
			throw new SQLException("SQLException: Kunne ikke hente resultater!", ex);
		} finally {
			avsluttDatabaseTilkobling(resultat, sporring, tilkobling);
		} //finally
		return innlogget;
	} //loggInnBruker

	/**
	 * Lukker alle åpne tilkoblinger til databasen i følgende rekkefølge: <br />
	 * - ResultSet <br />
	 * - PreparedStatement <br />
	 * - Connection 
	 * @param resultat - ResultSet
	 * @param sporring - PreparedStatement
	 * @param tilkobling - Connection
	 * @throws SQLException
	 */
	private static void avsluttDatabaseTilkobling(ResultSet resultat, PreparedStatement sporring, Connection tilkobling) throws SQLException {
		try {
			lukkResultatSet(resultat);
			lukkSporring(sporring);
			lukkDatabaseTilkobling(tilkobling);
		} catch (SQLException ex) {
			throw new SQLException("SQLException oppsto under lukking av tilkoblinger.", ex);
		} //try/catch
	} //avsluttDatabaseTilkobling

	private static void lukkResultatSet(ResultSet resultat) throws SQLException {
		if (resultat != null) {
			resultat.close();
			resultat = null;
		} //if (resultat != null) 
	} //lukkResultatSet

	private static void lukkSporring(PreparedStatement sporring) throws SQLException {
		if (sporring != null) {
			sporring.close();
			sporring = null;
		} //if (sporring != null) 
	} //lukkSporring

	private static void lukkDatabaseTilkobling(Connection tilkobling) throws SQLException {
		if (tilkobling != null) {
			tilkobling.close();
			tilkobling = null;
		} //if (tilkobling != null)
	} //lukkDatabaseTilkobling
} //Database