package dt.hin.android.kl_andersen;

//java-import
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.Locale;
//android-import
import android.content.ContentResolver;
import android.content.ContentUris;
import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.net.Uri;
//google-import
import com.google.android.maps.GeoPoint;

/**
 * Klasse som inneholder funksjoner for lagring og uthenting av data fra 
 * databasen (SQLite). <br /><br />
 * <u>Klassen inneholder følgende funksjoner:</u><br /><br />
 * protected {@linkplain #registrerEnhetiDatabase(Context, String, String, boolean)} <br />
 * protected {@linkplain #registrerKoordinateriDatabase(Context, long, String, double, double)} <br />
 * protected {@linkplain #hentRuteKoordinater(Context, long, int)} <br />
 * protected {@linkplain #updateDatabaseEnhetsNavn(Context, long, String)} <br />
 * protected {@linkplain #updateDatabaseBrukersRegID(Context, long, String)} <br />
 * protected {@linkplain #slettRuteFraDatabase(long)} <br />
 * protected {@linkplain #slettAndresRuterFraDatabasen(long)} <br />
 * protected {@linkplain #opprettRuteID(Context, long)} <br />
 * protected {@linkplain #hentLagredeRuter(String)} <br />
 * private {@linkplain #erEnhetRegistrertiDatabase(Context, String)} <br />
 * private {@linkplain #erBrukerRegistrertiDatabase(Context)} <br />
 * private {@linkplain #insertEnhetIntoDatabase(Context, String, boolean)} <br />
 * @author Knut Lucas Andersen
 */
public final class DatabaseFunksjoner {
	/**
	 * Verdien = 30 min.  <br /> 
	 * Konstant for hvor gammel siste registrerte posisjon må være 
	 * før ny ruteID opprettes (i millisekund). 
	 * @see {@linkplain #opprettRuteID(Context, long)}
	 */
	private static final long INTERVALL_FOR_NY_RUTEID = 1800000;

	private DatabaseFunksjoner() {
		throw new UnsupportedOperationException();
	} //konstruktør
	
	/**
	 * Registrerer enhet i SQLite databasen. <br />
	 * Før insert kjøres, sjekkes det om det er brukers enhet eller andres som skal lagres. <br />
	 * Hvis brukers enhet, sjekkes det via {@linkplain #erBrukerRegistrertiDatabase(Context)} om bruker er registrert. <br />
	 * Hvis det er andre brukere, så sjekkes det via {@linkplain #erEnhetRegistrertiDatabase(Context, String)}. <br />
	 * Dersom enhet ikke er registrert i databasen, lagres den.
	 * @param context - Context
	 * @param registrationID - String: Enhetens registrationID
	 * @param enhetsNavn - String: Enhetens navn
	 * @param egenEnhet - boolean: Er dette applikasjons brukerens enhet
	 * @return long: Enhetens identifikator (primærnøkkel i databasen)
	 * @throws Exception
	 * @see {@linkplain #erBrukerRegistrertiDatabase(Context)} 
	 * @see {@linkplain #erEnhetRegistrertiDatabase(Context, String)}
	 */
	protected static long registrerEnhetiDatabase(Context context, String registrationID, String enhetsNavn, boolean egenEnhet) throws Exception {
		long identifikator = -1;
		try {
			//er det brukers enhet som skal registreres?
			if (!egenEnhet) {
				identifikator = erEnhetRegistrertiDatabase(context, registrationID);
				if (identifikator == -1) {
					//enhet ikke registrert, registrer den i databasen
					identifikator = insertEnhetIntoDatabase(context, enhetsNavn, egenEnhet);
				} //if (identifikator == -1)
			} else {
				//er brukers enhet registrert i databasen?
				identifikator = erBrukerRegistrertiDatabase(context);
				if (identifikator == -1) {
					//brukers enhet ikke registrert, registrer den i databasen
					identifikator = insertEnhetIntoDatabase(context, enhetsNavn, egenEnhet);
				} else {
					//oppdater og legg inn gjeldende registrationID
					updateDatabaseBrukersRegID(context, identifikator, registrationID);
				} //if (!enhetRegistrertDatabase(context))
			} //if (!enhetRegistrertDatabase(registrationID, context))	
		} catch (Exception ex) {
			throw ex;
		} //try/catch
		return identifikator;
	} //registrerEnhetiDatabase

	/**
	 * Sjekker om andre påloggede brukers enhet er lagret i databasen. <br />
	 * Verdien som returneres er enten primærnøkkelen eller -1.
	 * @param context - Context
	 * @param registrationID - String:
	 * @return long: Enhetens identikator || -1
	 */
	private static long erEnhetRegistrertiDatabase(Context context, String registrationID) {
		long identifikator = -1;
		String tblEnhet = LoggingContentProvider.DB_TBLENHET;
		String tblKoordinater = LoggingContentProvider.DB_TBLKOORDINATER;
		String pkEnhet = LoggingContentProvider.TBLENHET_PRIMARY_KEY;
		String tblKoord_regID = LoggingContentProvider.TBLKOORDINATER_REGISTRATIONID;
		String fkEnhet = LoggingContentProvider.TBLKOORDINATER_FOREIGN_KEY;
		Cursor resultatCursor = null;
		//opprett og kjør spørring mot databasen
		String query = "SELECT DISTINCT " + pkEnhet 
				+ " FROM " + tblEnhet + ", "+ tblKoordinater 
				+ " WHERE " + tblEnhet + "." + LoggingContentProvider.TBLENHET_EGEN_ENHET + " = " + 0
				+ " AND " + tblEnhet + "." + pkEnhet + " = " + tblKoordinater + "." + fkEnhet
				+ " AND " + tblKoordinater + "." + tblKoord_regID + " = '" + registrationID + "'"
				+ " AND " + tblKoordinater + "." + LoggingContentProvider.TBLKOORDINATER_RUTETIDSPUNKT + ">= datetime('now','-1 day')";
		//hent resultatet og sjekk om pirmærnøkkel ble funnet
		resultatCursor = LoggingContentProvider.rawQuery(query, null);
		if (resultatCursor != null && resultatCursor.moveToFirst()) {
			//forventer kun at en nøkkel skal returneres
			int pk_tblEnhet = resultatCursor.getColumnIndexOrThrow(pkEnhet);
			identifikator = resultatCursor.getLong(pk_tblEnhet);
		} //if (resultatCursor != null && resultatCursor.getCount() > 0)
		resultatCursor.close();
		return identifikator;
	} //erEnhetRegistrertiDatabase

	/**
	 * Sjekker om brukers enhet allerede er registrert i databasen. <br />
	 * Verdien som returneres er enten primærnøkkelen eller -1.
	 * @return long: Brukers identifikator || -1
	 */
	private static long erBrukerRegistrertiDatabase(Context context) {
		long identifikator = -1;
		Cursor resultatCursor = null;
		ContentResolver cr = context.getContentResolver();
		//opprett array som inneholder verdiene som skal hentes ut
		String[] resultatArray = new String[] {
				LoggingContentProvider.TBLENHET_PRIMARY_KEY
		};
		String where = LoggingContentProvider.TBLENHET_EGEN_ENHET + " = " + 1;
		//"plassholdere"
		String[] whereArgs = null;
		String order = null;
		LoggingContentProvider.setTblQuery(LoggingContentProvider.DB_TBLENHET);
		//hent resultatet fra databasen
		resultatCursor = cr.query(LoggingContentProvider.DATABASE_URI, resultatArray, where, whereArgs, order);
		if (resultatCursor != null && resultatCursor.moveToFirst()) {
			int pk_tblEnhet = resultatCursor.getColumnIndexOrThrow(LoggingContentProvider.TBLENHET_PRIMARY_KEY);
			identifikator = resultatCursor.getLong(pk_tblEnhet);
		} //if (resultatCursor != null && resultatCursor.getCount() > 0)
		resultatCursor.close();
		return identifikator;
	} //erBrukerRegistrertiDatabase

	/**
	 * Lagrer enhet i databasen via Insert.
	 * @param context - Context
	 * @param enhetsNavn - String: Enhetens navn
	 * @param egenEnhet - boolean: Er dette brukers enhet?
	 * @return long: Enhetens identifikator (primærnøkkel i databasen) || -1
	 */
	private static long insertEnhetIntoDatabase(Context context, String enhetsNavn, boolean egenEnhet) {
		//opprett objekt som skal inneholde verdiene
		ContentValues verdier = new ContentValues();
		ContentResolver cr = context.getContentResolver();
		//legg inn verdier som skal logges
		LoggingContentProvider.setTblQuery(LoggingContentProvider.DB_TBLENHET);	
		verdier.put(LoggingContentProvider.TBLENHET_NAVN, enhetsNavn);		
		verdier.put(LoggingContentProvider.TBLENHET_EGEN_ENHET, egenEnhet);
		//lagre enhet i databasen
		Uri uri = cr.insert(LoggingContentProvider.DATABASE_URI, verdier);
		return ContentUris.parseId(uri);
	} //insertEnhetIntoDatabase

	/**
	 * Registrerer mottatte (eller egne) koordinater i databasen.
	 * @param context - Context
	 * @param identifikator - long: Enhetens primærnøkkel i databasen
	 * @param registrationID - String: Enhetens registrationID
	 * @param latitude - double: Enhetens latitude
	 * @param longitude - double: Enhetens longitude
	 * @throws Exception
	 */
	protected static void registrerKoordinateriDatabase(Context context, long identifikator, String registrationID, double latitude, double longitude) throws Exception {
		try {
			double ikkeSendPosisjon = context.getResources().getInteger(R.integer.ikke_send_posisjon);
			//sjekk at faktiske koordinater er mottatt/registrert
			if (latitude != ikkeSendPosisjon && longitude != ikkeSendPosisjon) {
				//datoformat for visning av dato
				Locale locale = Locale.getDefault();
				SimpleDateFormat datoFormatering = new SimpleDateFormat(LoggingContentProvider.DATO_TID_FORMAT, locale);
				//formater dagens dato til string
				String tidspunkt = datoFormatering.format(new Date());
				//opprett objekt som skal inneholde verdiene
				ContentValues verdier = new ContentValues();
				ContentResolver cr = context.getContentResolver();
				int ruteID = FellesFunksjoner.getEksisterendeRuteID(registrationID);
				//legg inn verdier som skal logges
				LoggingContentProvider.setTblQuery(LoggingContentProvider.DB_TBLKOORDINATER);
				verdier.put(LoggingContentProvider.TBLKOORDINATER_REGISTRATIONID, registrationID);
				verdier.put(LoggingContentProvider.TBLKOORDINATER_LATITUDE, latitude);
				verdier.put(LoggingContentProvider.TBLKOORDINATER_LONGITUDE, longitude);
				verdier.put(LoggingContentProvider.TBLKOORDINATER_RUTETIDSPUNKT, tidspunkt);
				verdier.put(LoggingContentProvider.TBLKOORDINATER_RUTEID, ruteID);
				verdier.put(LoggingContentProvider.TBLKOORDINATER_FOREIGN_KEY, identifikator);
				//lagre enhetens koordinater i databasen
				cr.insert(LoggingContentProvider.DATABASE_URI, verdier);
			} //if (latitude != ikkeSendPosisjon && longitude !=ikkeSendPosisjon)
		} catch (Exception ex) {
			throw ex;
		} //try/catch
	} //registrerKoordinateriDatabase

	/**
	 * Denne funksjonen går inn i databasen, og sjekker om det finnes tidligere registrerte ruter for 
	 * gitt enhet basert på oversendt identifikator og ruteID. Dersom koordinater finnes, blir disse 
	 * lagt inn i ArrayList som returneres. 
	 * @param context - Context
	 * @param identifikator - long: Enhetens identifikator (primærnøkkel i databasen)
	 * @param ruteID - int: Enhetens ruteID
	 * @return ArrayList&lt;GeoPoint&gt;: ArrayList med koordinater || new ArrayList&lt;GeoPoint&gt;(); 
	 * @throws Exception
	 */
	protected static ArrayList<GeoPoint> hentRuteKoordinater(Context context, long identifikator, int ruteID) throws Exception {
		Cursor resultatCursor = null;
		ContentResolver cr = context.getContentResolver();
		ArrayList<GeoPoint> ruteListe = new ArrayList<GeoPoint>();
		try {
			LoggingContentProvider.setTblQuery(LoggingContentProvider.DB_TBLKOORDINATER);
			//opprett array som inneholder verdiene som skal hentes ut
			String[] resultatArray = new String[] {
					LoggingContentProvider.TBLKOORDINATER_LATITUDE,
					LoggingContentProvider.TBLKOORDINATER_LONGITUDE
			};
			String where = LoggingContentProvider.TBLKOORDINATER_FOREIGN_KEY + " = " + identifikator 
					+ " AND " + LoggingContentProvider.TBLKOORDINATER_RUTEID + " = " + ruteID;
			//"plassholdere"
			String[] whereArgs = null;
			String order = LoggingContentProvider.TBLKOORDINATER_RUTETIDSPUNKT + " ASC";
			//hent resultatet fra databasen
			resultatCursor = cr.query(LoggingContentProvider.DATABASE_URI, resultatArray, where, whereArgs, order);
			//ble et resultat hentet ut (har denne enheten rute(r) fra før)?
			if (resultatCursor != null && resultatCursor.getCount() > 0) {
				int KOLONNE_LATITUDE = resultatCursor.getColumnIndexOrThrow(LoggingContentProvider.TBLKOORDINATER_LATITUDE);
				int KOLONNE_LONGITUDE = resultatCursor.getColumnIndexOrThrow(LoggingContentProvider.TBLKOORDINATER_LONGITUDE);
				while (resultatCursor.moveToNext()) {
					Double lat = resultatCursor.getDouble(KOLONNE_LATITUDE);
					Double lon = resultatCursor.getDouble(KOLONNE_LONGITUDE);
					ruteListe.add(new GeoPoint(lat.intValue(), lon.intValue()));
				} //while
			} //if (resultatCursor != null && resultatCursor.getCount() > 0)
		} catch (Exception ex) {
			throw ex;
		} finally {
			//lukk cursor hvis den ble åpnet
			if (resultatCursor != null) {
				resultatCursor.close();
			} //if (resultatCursor != null)
		} //try/catch
		return ruteListe;
	} //hentRuteKoordinater

	/**
	 * Oppdaterer enhetens navn i databasen.
	 * @param context - Context
	 * @param identifikator - long: Enhetens identifikator 
	 * @param enhetsNavn - String: Enhetens navn
	 */
	protected static void updateDatabaseEnhetsNavn(Context context, long identifikator, String enhetsNavn) {
		String pkEnhet = LoggingContentProvider.TBLENHET_PRIMARY_KEY;
		ContentValues verdier = new ContentValues();
		ContentResolver cr = context.getContentResolver();
		verdier.put(LoggingContentProvider.TBLENHET_NAVN, enhetsNavn);
		LoggingContentProvider.setTblQuery(LoggingContentProvider.DB_TBLENHET);
		String where = pkEnhet + " = " + identifikator;
		String[] selectionArgs = null;
		cr.update(LoggingContentProvider.DATABASE_URI, verdier, where, selectionArgs);
	} //updateDatabaseEnhetsNavn
	
	/**
	 * Oppdaterer brukerens registrationID i databasen.
	 * @param context - Context
	 * @param identifikator - long: Enhetens identifikator 
	 * @param registrationID - String: Bruker sin enhets registrationID
	 */
	protected static void updateDatabaseBrukersRegID(Context context, long identifikator, String registrationID) {
		String fkEnhet = LoggingContentProvider.TBLKOORDINATER_FOREIGN_KEY;
		ContentValues verdier = new ContentValues();
		ContentResolver cr = context.getContentResolver();
		verdier.put(LoggingContentProvider.TBLKOORDINATER_REGISTRATIONID, registrationID);
		LoggingContentProvider.setTblQuery(LoggingContentProvider.DB_TBLKOORDINATER);
		String where = fkEnhet + " = " + identifikator;
		String[] selectionArgs = null;
		cr.update(LoggingContentProvider.DATABASE_URI, verdier, where, selectionArgs);
	} //updateDatabaseBrukersRegID

	/**
	 * Sletter ruter basert på oversendt identifikator. <br />
	 * Det slettes både fra tblEnhet og tblKoordinater.
	 * @param identifikator - long: Identifikator (pk) for ruter som skal slettes
	 */
	protected static void slettRuteFraDatabase(long identifikator) {
		String[] whereArgs = null;
		String tblEnhet = LoggingContentProvider.DB_TBLENHET;
		String tblKoordinater = LoggingContentProvider.DB_TBLKOORDINATER;
		String pkEnhet = LoggingContentProvider.TBLENHET_PRIMARY_KEY;
		String fkEnhet = LoggingContentProvider.TBLKOORDINATER_FOREIGN_KEY;
		//slett ruten(e) fra tblkoordinater
		String where = fkEnhet + " = " + identifikator;
		LoggingContentProvider.delete(tblKoordinater, where, whereArgs);
		//slett enheten fra tblenhet
		where = pkEnhet + " = " + identifikator;
		LoggingContentProvider.delete(tblEnhet, where, null);
	} //slettRuteFraDatabase

	/**
	 * Sletter alle ruter (og enheter) utenom enhets identifikator 
	 * som blir oversendt.
	 * @param identifikator - long: Identifikator for enhet som ikke skal slettes.
	 */
	protected static void slettAndresRuterFraDatabasen(long identifikator) {
		String tblEnhet = LoggingContentProvider.DB_TBLENHET;
		String pkEnhet = LoggingContentProvider.TBLENHET_PRIMARY_KEY;
		String tblKoordinater = LoggingContentProvider.DB_TBLKOORDINATER;
		String fkEnhet = LoggingContentProvider.TBLKOORDINATER_FOREIGN_KEY;
		String[] whereArgs = null;
		String where = fkEnhet + " != " + identifikator;
		LoggingContentProvider.delete(tblKoordinater, where, whereArgs);
		where = pkEnhet + " != " + identifikator;
		LoggingContentProvider.delete(tblEnhet, where, whereArgs);
	} //slettAndresRuterFraDatabasen
	
	/**
	 * Dette er en funksjon som oppretter ruteID ved mottak av koordinater/posisjon. <br />
	 * RuteID er tenkt å være en identifikator for hver enkelt rute som bruker mottar på sin enhet. <br />
	 * RuteID kan være den samme for flere koordinater, men det vil bli opprettet ny ruteID for nye ruter.  <br /><br />
	 * <u>Et eksempel på ruteID: </u> <br />
	 * En bruker beveger seg fra punkt A til B. Tiden det tar er irrelevant. <br /> 
	 * Etter bruker når punkt B, så avslutter denne brukeren applikasjonen. <br />
	 * Tiden det tar før applikasjon starter å sende nye koordinater kan være alt fra et minutt til flere dager. <br />
	 * Over lengre tid så vil bruker ha forflyttet seg en gitt distanse fra punkt B til punkt C. <br />
	 * Men ruten som opprettes mellom punkt B og C vil ikke være mulig å opprette, <br />
	 * siden distansen og veien til punkt C er ukjent. <br />
	 * RuteID med verdi 1 vil da kun gjelde for alle registrerte punkter fra A til B,  <br />
	 * mens RuteID med verdi 2 vil gjelde for alle punkter fra C til D. 
	 * @param context - Context
	 * @param identifikator - long: Enhetens identifkator
	 * @return int: RuteID for koordinater som skal lagres i database
	 * @throws Exception
	 */
	protected static int opprettRuteID(Context context, long identifikator) throws Exception {
		int ruteID = 0;
		Cursor resultatCursor = null;
		ContentResolver cr = context.getContentResolver();
		try {
			//opprett array som inneholder verdiene som skal hentes ut
			String[] resultatArray = new String[] {
					LoggingContentProvider.TBLKOORDINATER_RUTETIDSPUNKT,
					LoggingContentProvider.TBLKOORDINATER_RUTEID
			};
			String where = LoggingContentProvider.TBLKOORDINATER_FOREIGN_KEY + " = " + identifikator;
			//"plassholdere"
			String[] whereArgs = null;
			String order = null;
			LoggingContentProvider.setTblQuery(LoggingContentProvider.DB_TBLKOORDINATER);
			//hent resultatet fra databasen
			resultatCursor = cr.query(LoggingContentProvider.DATABASE_URI, resultatArray, where, whereArgs, order);
			//ble et resultat hentet ut (har denne enheten rute(r) fra før)?
			if (resultatCursor != null && resultatCursor.getCount() > 0) {
				if (resultatCursor.moveToLast()) {
					int datoKolonne = resultatCursor.getColumnIndexOrThrow(LoggingContentProvider.TBLKOORDINATER_RUTETIDSPUNKT);
					int ruteIDKolonne = resultatCursor.getColumnIndexOrThrow(LoggingContentProvider.TBLKOORDINATER_RUTEID);
					String dbTid = resultatCursor.getString(datoKolonne);
					Locale locale = Locale.getDefault();
					SimpleDateFormat datoFormatering = new SimpleDateFormat(LoggingContentProvider.DATO_TID_FORMAT, locale);
					//formater dagens dato til string
					String tidspunktNaa = datoFormatering.format(new Date());
					Date dateNaa = datoFormatering.parse(tidspunktNaa);
					Date dateDB = datoFormatering.parse(dbTid);
					//beregn hvor lenge siden forrige registrering var
					long differanse = dateNaa.getTime() - dateDB.getTime();
					//har det gått lengre enn satt intervall siden siste mottak av posisjon?
					if (INTERVALL_FOR_NY_RUTEID < differanse) {
						//hent ut den forrige rutens verdi og øk ruteID
						ruteID = resultatCursor.getInt(ruteIDKolonne);
						ruteID++;
					} else {
						//hent ut eksisterende ruteID
						ruteID = resultatCursor.getInt(ruteIDKolonne);
					} //if (INTERVALL_FOR_NY_RUTEID < differanse)
				} //if (resultatCursor.moveToLast())
			} else {
				//ingen ruter registrert på denne enheten, 
				//opprett ny ruteID
				ruteID++;
			} //if (resultatCursor != null && resultatCursor.getCount() > 0)
		} catch (Exception ex) {
			throw ex;
		} finally {
			//øk ruteID hvis den ikke fikk verdi
			if (ruteID == 0) {
				ruteID++;
			} //if (ruteID == 0)
			//lukk cursor hvis den ble åpnet
			if (resultatCursor != null) {
				resultatCursor.close();
			} //if (resultatCursor != null)
		} //try/catch
		return ruteID;
	} //opprettRuteID
	
	/**
	 * Funksjon som henter ut tidligere registrerte ruter og legger det i en ArrayList. <br />
	 * Dersom ingen ruter er registrert/lagret, returneres en tom ArrayList.
	 * @param query - String: Spørringen som skal kjøres mot databasen
	 * @return ArrayList&lt;Enhet&gt;: <br /> 
	 * ArrayList som inneholder lagrede ruter || new ArrayList&lt;Enhet&gt;()
	 * @throws Exception
	 */
	protected static ArrayList<Enhet> hentLagredeRuter(String query) throws Exception {
		GeoPoint koordinat = null;
		Enhet enhet = null;
		long pkEnhet = 0,
				forrigePK = -1;
		int indeks = -1,
				ruteID = 0,
				forrigeRuteID = 0,
				egenEnhetInt = 0;
		Boolean egenEnhetBool = false;
		String enhetsNavn = "",
				ruteTidspunkt = "",
				//settes til null for å unngå blanding med online brukere
				registrationID = null;
		Double lat = 0.0, 
				lon = 0.0;
		Cursor resultatCursor = null;
		ArrayList<Enhet> ruteListe = new ArrayList<Enhet>();
		ArrayList<GeoPoint> koordinatListe = new ArrayList<GeoPoint>();
		try {
		//kjør spørring mot databasen
		resultatCursor = LoggingContentProvider.rawQuery(query, null);
		//returnerte spørringen et resultat?
		if (resultatCursor != null && resultatCursor.getCount() > 0) {
			//kolonneid for verdier i databasen
			int pkEnhet_Kolonne = resultatCursor.getColumnIndexOrThrow(LoggingContentProvider.TBLENHET_PRIMARY_KEY);
			int enhetsNavn_Kolonne = resultatCursor.getColumnIndexOrThrow(LoggingContentProvider.TBLENHET_NAVN);
			int egenEnhet_Kolonne = resultatCursor.getColumnIndexOrThrow(LoggingContentProvider.TBLENHET_EGEN_ENHET);
			int latitude_Kolonne = resultatCursor.getColumnIndexOrThrow(LoggingContentProvider.TBLKOORDINATER_LATITUDE);
			int longitude_Kolonne = resultatCursor.getColumnIndexOrThrow(LoggingContentProvider.TBLKOORDINATER_LONGITUDE);
			int rutetidspunkt_Kolonne = resultatCursor.getColumnIndexOrThrow(LoggingContentProvider.TBLKOORDINATER_RUTETIDSPUNKT);
			int ruteID_Kolonne = resultatCursor.getColumnIndexOrThrow(LoggingContentProvider.TBLKOORDINATER_RUTEID);
			//loop gjennom resultatet
			while(resultatCursor.moveToNext()) {
				//hent ut verdiene fra cursor
				pkEnhet = resultatCursor.getLong(pkEnhet_Kolonne);
				ruteID = resultatCursor.getInt(ruteID_Kolonne);
				lat = resultatCursor.getDouble(latitude_Kolonne);
				lon = resultatCursor.getDouble(longitude_Kolonne);
				//konverter latitude og longitude til geopoint
				koordinat = new GeoPoint(lat.intValue(), lon.intValue());
				//er dette en ny enhet (eller en ny rute)?
				if (pkEnhet == forrigePK && ruteID == forrigeRuteID) {
					//legg til koordinatene i arraylist
					ruteListe.get(indeks).addGeoPoint(koordinat);
				} else {
					enhetsNavn = resultatCursor.getString(enhetsNavn_Kolonne);
					egenEnhetInt = resultatCursor.getInt(egenEnhet_Kolonne);
					//hent boolsk verdi og konverter den (er dette brukers enhet?)
					if (egenEnhetInt == 0) {
						egenEnhetBool = false;
					} else {
						egenEnhetBool = true;
					}// if (egenEnhetInt == 0)						
					ruteTidspunkt = resultatCursor.getString(rutetidspunkt_Kolonne);
					koordinatListe = new ArrayList<GeoPoint>();
					koordinatListe.add(koordinat);
					//opprett enhet og legg den i arraylist 
					//(setter minus foran pkEnhet for å unngå blanding med eksisterende ruter)
					enhet = new Enhet(-pkEnhet, registrationID, enhetsNavn, egenEnhetBool, ruteID, ruteTidspunkt, koordinatListe);
					ruteListe.add(enhet);
					//øker indeks
					indeks++;
				} //if
				forrigePK = pkEnhet;
				forrigeRuteID = ruteID;
			} //while
		} //if (resultatCursor != null && resultatCursor.moveToFirst())
		} catch (Exception ex) {
			throw ex;
		} finally {
			//lukk cursor hvis den ble åpnet
			if (resultatCursor != null) {
				resultatCursor.close();
			} //if (resultatCursor != null)
		} //try/catch
		return ruteListe;
	} //hentLagredeRuter
} //DatabaseFunksjoner