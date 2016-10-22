package oblig_244;
/**
 * Denne klassen tar ihovedsak for seg alt av SQL-spørringer, og har
 * prøvd å unngå som best er at den gjør noe annet enn dette
 * Koden for låsing av JTable fant jeg her: 
 * http://www.roseindia.net/java/example/java/swing/EditDisable.shtml
 */
import java.sql.*;
import java.util.*;

class Database {	
	private String dbnavn = "jdbc:mysql://localhost:3306/dyrebase"; //database-navn
	private String dbdriver = "com.mysql.jdbc.Driver"; //database-driver
	private Connection forbindelse; //tilkobling

	public Database(String bruker, String pwd) throws Exception {		
		try {			
			Class.forName(dbdriver);			
			forbindelse = DriverManager.getConnection(dbnavn, bruker, pwd); //prøv å koble til databasen
		} catch (Exception e) { //klarte ikke koble til databasen			
			throw e; //kast feil
		} // try/catch
	} //konstruktør	

	/***METODER FOR INNSETTING AV NY FANGST/GJENFANGST***/
	public boolean nyHare (String id,char kjonn, double lengde, double vekt, String sted, String dato, char hType, String farge) 
	throws  SQLException {	
		boolean sjekk = false;
		try {						
			String sqlsetning = "INSERT INTO tblDyr (idDyr, kjonn, lengde, vekt, sted, dato, hareType, farge) VALUES ('" +
			id + "', '"+kjonn + "'," + lengde + ","+  vekt + ", '"+ sted + "' ,'"+ dato +  "','"+ hType +"', '"+ farge +"'); ";
			//System.out.println(sqlsetning); //testformål for å sammenligne/teste mot db
			Statement utsagn = forbindelse.createStatement(); //opprett tilkobling
			utsagn.execute(sqlsetning); //utfør spørring
			sjekk = true;						
		} catch (SQLException e) {
			throw e; //kast feilmelding videre
		} //try/catch
		return sjekk;
	} //nyHare     

	public boolean nyGaupe (String id, char kjonn, double lengde, double vekt, String sted, String dato, double oreLengde)  
	throws  SQLException {
		boolean sjekk = false;
		try {
			String sqlsetning   = "INSERT INTO tblDyr (idDyr,kjonn,lengde, vekt, sted, dato,oreLengde)  VALUES ('" +
			id + "', '"+kjonn + "', "+ lengde + ","+  vekt + ", '"+ sted + "' ,'"+ dato + "', "+ oreLengde + "); ";
			//System.out.println(sqlsetning); //testformål for å sammenligne/teste mot db
			Statement utsagn = forbindelse.createStatement(); //opprett tilkobling
			utsagn.execute(sqlsetning); //utfør spørring			
			sjekk = true;
		} catch (SQLException e) {			
			throw e; //kast feilmelding videre
		} //try/catch
		return sjekk;
	} //nyGaupe   

	public boolean nyGjenfangstHare (String id, double lengde, double vekt, String sted, String dato, String farge)  throws  SQLException{
		boolean sjekk = false;
		try {
			String sqlsetning = "INSERT INTO tblGjenfangst (tblDyr_idDyr, lengde, vekt, sted, dato, farge) VALUES ('" +
			id + "'," + lengde + ","+  vekt + ", '"+ sted + "' ,'"+ dato + "', '"+ farge + "' ); ";     
			//System.out.println(sqlsetning); //testformål for å sammenligne/teste mot db
			Statement utsagn = forbindelse.createStatement(); //opprett tilkobling
			utsagn.execute(sqlsetning); //utfør spørring			
			sjekk = true;
		} catch (SQLException e) {
			throw e; //kast feilmelding videre
		} //try/catch
		return sjekk;
	} //nyGjenfangstHare

	public boolean nyGjenfangstGaupe (String id, double lengde, double vekt, String sted, String dato) throws  SQLException {
		boolean sjekk = false;
		try {
			String sqlsetning = "INSERT INTO tblGjenfangst (tblDyr_idDyr, lengde, vekt, sted, dato) VALUES ('" +
			id + "'," + lengde + ","+  vekt + ", '"+ sted + "' ,'"+ dato +  "' ); ";    
			//System.out.println(sqlsetning); //testformål for å sammenligne/teste mot db
			Statement utsagn = forbindelse.createStatement(); //opprett tilkobling
			utsagn.execute(sqlsetning); //utfør spørring			
			sjekk = true;
		} catch (SQLException e) {			
			throw e; //kast feilmelding videre
		} //try/catch
		return sjekk;
	} //nyGjenfangstGaupe

	/***METODER FOR ARRAYLIST***/
	public ArrayList<Hare> hentHare() {		
		ArrayList<Hare> Hare = new ArrayList<Hare>();
		String sqlsetning = null;
		ResultSet res = null;		
		try {			
			sqlsetning = "SELECT * FROM tbldyr WHERE idDyr LIKE 'H%'; ";
			//System.out.println(sqlsetning); //testformål for å sammenligne/teste mot db
			Statement utsagn = forbindelse.createStatement(); //opprett tilkobling
			res = utsagn.executeQuery(sqlsetning); //utfør spørring
			while (res.next()) { //kjør løkka så lengde det ligger noe i resultat
				String id = res.getString("idDyr"); //hent verdier
				String k = res.getString("kjonn");
				double lengde = res.getDouble("lengde");
				double vekt = res.getDouble("vekt");
				String sted = res.getString("sted"); 
				String dato = res.getString("dato");
				String ht = res.getString("haretype");
				String farge = res.getString("farge");
				char kjonn = Character.toUpperCase(k.charAt(0)); //omgjør til char
				char hType  = Character.toUpperCase(ht.charAt(0)); //omgjør til char
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
			//System.out.println(sqlsetning); //testformål for å sammenligne/teste mot db
			Statement utsagn = forbindelse.createStatement(); //opprett tilkobling
			res = utsagn.executeQuery(sqlsetning); //utfør spørring
			while (res.next()) {						
				String id = res.getString("idDyr");
				String k = res.getString("kjonn");
				double lengde = res.getDouble("lengde");
				double vekt = res.getDouble("vekt");
				String sted = res.getString("sted"); 
				String dato = res.getString("dato");
				double ore = res.getDouble("oreLengde");
				char kjonn = Character.toUpperCase(k.charAt(0)); //omgjør til char
				Gaupe regNyGaupe = new Gaupe(id, kjonn, lengde, vekt, sted, dato, ore);
				Gaupe.add(regNyGaupe); //legger gaupen inn i ArrayList				
			} //while
		} catch (SQLException e) {
			Gaupe = null;
		} // try/catch
		return Gaupe;
	} //hentGaupe

	/***SØKERESULTAT/UTHENTING AV DATA***/
	public ResultSet skrivUtFangst() throws SQLException {		
		ResultSet res = null; 
		try {									       
			Statement utsagn = forbindelse.createStatement(); //opprett tilkobling
			String select = "SELECT idDyr, kjonn, dato FROM tblDyr  ORDER BY idDyr asc;";
			res = utsagn.executeQuery(select); //kjør spørring mot databasen			
		} catch (SQLException e) {
			throw e; //kast feilmelding videre
		} // try/catch
		return res;
	} //skrivUtFangst

	/***AVSLUTT TILKOBLING TIL DATABASE***/
	public void kobleNed() throws SQLException {
		try {
			forbindelse.close(); //prøv å lukke forbindelsen
		} catch (SQLException e) {
			throw e; //klarte ikke  lukke forbindelse, kast feil
		} // try/catch
	} //kobleNed
} //Database