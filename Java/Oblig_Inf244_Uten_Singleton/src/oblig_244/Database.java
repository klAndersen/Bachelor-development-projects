package oblig_244;
/**
 * Denne klassen tar ihovedsak for seg alt av SQL-sp�rringer, og har
 * pr�vd � unng� som best er at den gj�r noe annet enn dette
 * Koden for l�sing av JTable fant jeg her: 
 * http://www.roseindia.net/java/example/java/swing/EditDisable.shtml
 */
import java.sql.*;
import java.util.*;
import static javax.swing.JOptionPane.*;

class Database {
	private String bruker = "Lucas"; //db-bruker
	private String pwd = "hibu"; //db-passord	
	private String tilbakemelding; //tekststreng som gir bruker tilbakemelding om alt gikk greit
	private String dbnavn = "jdbc:mysql://localhost:3306/dyrebase"; //database-navn
	private String dbdriver = "com.mysql.jdbc.Driver"; //database-driver
	private Connection forbindelse; //tilkobling

	public Database() {			
		try {			
			Class.forName(dbdriver);
			forbindelse = DriverManager.getConnection(dbnavn, bruker, pwd); //pr�v � koble til databasen
		} catch (Exception e) { //klarte ikke koble til databasen
			showMessageDialog(null, "F�r ikke forbindelse med databasen.","Tilkobling", ERROR_MESSAGE);
		} // try/catch
	} //konstrukt�r	

	/***METODER FOR INNSETTING AV NY FANGST/GJENFANGST***/
	public String nyHare (String id,char kjonn, double lengde, double vekt, String sted, String dato, char hType, String farge) {				
		try {						
			String sqlsetning = "INSERT INTO tblDyr (idDyr, kjonn, lengde, vekt, sted, dato, hareType, farge) VALUES ('" +
			id + "', '"+kjonn + "'," + lengde + ","+  vekt + ", '"+ sted + "' ,'"+ dato +  "','"+ hType +"', '"+ farge +"'); ";
			//System.out.println(sqlsetning); //testform�l for � sammenligne/teste mot db
			Statement utsagn = forbindelse.createStatement(); //opprett tilkobling
			utsagn.execute(sqlsetning); //utf�r sp�rring
			tilbakemelding = "Haren ble lagret.";			
		} catch (Exception e) {
			tilbakemelding = "Haren ble ikke lagret i databasen. Feilen som oppsto var:\n" + e;
		} //try/catch
		return tilbakemelding;
	} //nyttDyr     

	public String nyGaupe (String id, char kjonn, double lengde, double vekt, String sted, String dato, double oreLengde) {          
		try {
			String sqlsetning   = "INSERT INTO tblDyr (idDyr,kjonn,lengde, vekt, sted, dato,oreLengde)  VALUES ('" +
			id + "', '"+kjonn + "', "+ lengde + ","+  vekt + ", '"+ sted + "' ,'"+ dato + "', "+ oreLengde + "); ";
			//System.out.println(sqlsetning); //testform�l for � sammenligne/teste mot db
			Statement utsagn = forbindelse.createStatement(); //opprett tilkobling
			utsagn.execute(sqlsetning); //utf�r sp�rring
			tilbakemelding = "Gaupen ble lagret.";	
		} catch (Exception e) {
			tilbakemelding = "Gaupen ble ikke lagret i databasen. Feilen som oppsto var:\n" + e;
		} //try/catch
		return tilbakemelding;
	} //nyttDyr   

	public String nyGjenfangstHare (String id, double lengde, double vekt, String sted, String dato, String farge) {
		String sqlsetning = null;
		try {
			sqlsetning = "INSERT INTO tblGjenfangst (tblDyr_idDyr, lengde, vekt, sted, dato, farge) VALUES ('" +
			id + "'," + lengde + ","+  vekt + ", '"+ sted + "' ,'"+ dato + "', '"+ farge + "' ); ";     
			//System.out.println(sqlsetning); //testform�l for � sammenligne/teste mot db
			Statement utsagn = forbindelse.createStatement(); //opprett tilkobling
			utsagn.execute(sqlsetning); //utf�r sp�rring
			tilbakemelding = "Gjenfangst av hare ble lagret.";
		} catch (Exception e) {
			tilbakemelding = "Kan ikke legge til hare i tabellen for gjenfangst.";
		} //try/catch
		return tilbakemelding;
	} //nyGjenfangstHare

	public String nyGjenfangstGaupe (String id, double lengde, double vekt, String sted, String dato) {
		String sqlsetning = null;
		try {
			sqlsetning = "INSERT INTO tblGjenfangst (tblDyr_idDyr, lengde, vekt, sted, dato) VALUES ('" +
			id + "'," + lengde + ","+  vekt + ", '"+ sted + "' ,'"+ dato +  "' ); ";    
			//System.out.println(sqlsetning); //testform�l for � sammenligne/teste mot db
			Statement utsagn = forbindelse.createStatement(); //opprett tilkobling
			utsagn.execute(sqlsetning); //utf�r sp�rring
			tilbakemelding = "Gjenfangst av gaupe ble lagret.";
		} catch (Exception e) {
			tilbakemelding = "Kan ikke legge til gaupe i tabellen for gjenfangst.";
		} //try/catch
		return tilbakemelding;
	} //nyGjenfangstGaupe

	/***METODER FOR ARRAYLIST***/
	public ArrayList<Hare> hentHare() {		
		ArrayList<Hare> Hare = new ArrayList<Hare>();
		String sqlsetning = null;
		ResultSet res = null;		
		try {			
			sqlsetning = "SELECT * FROM tbldyr WHERE idDyr LIKE 'H%'; ";
			//System.out.print(sqlsetning);
			Statement utsagn = forbindelse.createStatement(); //opprett tilkobling
			res = utsagn.executeQuery(sqlsetning); //utf�r sp�rring
			while (res.next()) { //kj�r l�kka s� lengde det ligger noe i resultat
				String id = res.getString("idDyr"); //hent verdier
				String k = res.getString("kjonn");
				double lengde = res.getDouble("lengde");
				double vekt = res.getDouble("vekt");
				String sted = res.getString("sted"); 
				String dato = res.getString("dato");
				String ht = res.getString("haretype");
				String farge = res.getString("farge");
				char kjonn = Character.toUpperCase(k.charAt(0)); //omgj�r til char
				char hType  = Character.toUpperCase(ht.charAt(0)); //omgj�r til char
				Hare regNyHare = new Hare(id, kjonn, lengde, vekt, sted, dato, hType, farge);
				Hare.add(regNyHare); //legger haren inn i ArrayList				
			} //while
		} catch (SQLException e) {
			Hare = null;
		} // try/catch
		return Hare;
	} //hentHare

	public ArrayList<Gaupe> hentGaupe() {		
		ArrayList<Gaupe> Gaupe = new ArrayList<Gaupe>();
		String sqlsetning = null;
		ResultSet res = null;		
		try {			
			sqlsetning = "SELECT * FROM tbldyr WHERE idDyr LIKE 'G%'; ";
			//System.out.print(sqlsetning);
			Statement utsagn = forbindelse.createStatement(); //opprett tilkobling
			res = utsagn.executeQuery(sqlsetning); //utf�r sp�rring
			while (res.next()) {						
				String id = res.getString("idDyr");
				String k = res.getString("kjonn");
				double lengde = res.getDouble("lengde");
				double vekt = res.getDouble("vekt");
				String sted = res.getString("sted"); 
				String dato = res.getString("dato");
				double ore = res.getDouble("oreLengde");
				char kjonn = Character.toUpperCase(k.charAt(0)); //omgj�r til char
				Gaupe regNyGaupe = new Gaupe(id, kjonn, lengde, vekt, sted, dato, ore);
				Gaupe.add(regNyGaupe); //legger gaupen inn i ArrayList				
			} //while
		} catch (SQLException e) {
			Gaupe = null;
		} // try/catch
		return Gaupe;
	} //hentGaupe
	
	/***S�KERESULTAT/UTHENTING AV DATA***/
	public ResultSet skrivUtFangst() throws SQLException {		
		ResultSet res = null; 
		try {									       
			Statement utsagn = forbindelse.createStatement(); //opprett tilkobling
			String select = "Select idDyr, kjonn, dato from tblDyr";
			res = utsagn.executeQuery(select);			
		} catch (SQLException e) {
			throw e; //kast feilmelding videre
		} // try/catch
		return res;
	} //skrivUtFangst

	/***AVSLUTT TILKOBLING TIL DATABASE***/
	public void kobleNed() throws SQLException {
		try {
			forbindelse.close(); //pr�v � lukke forbindelsen
		} catch (SQLException e) {
			throw e; //klarte ikke  lukke forbindelse, kast feil
		} // try/catch
	} //kobleNed
} //Database