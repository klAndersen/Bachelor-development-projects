package dt.hin.android.kl_andersen;

//android-import
import android.content.ContentProvider;
import android.content.ContentUris;
import android.content.ContentValues;
import android.content.Context;
import android.content.UriMatcher;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteDatabase.CursorFactory;
import android.database.sqlite.SQLiteOpenHelper;
import android.database.sqlite.SQLiteQueryBuilder;
import android.net.Uri;
import android.text.TextUtils;
import android.util.Log;

/**
 * En klasse som arver fra ContentProvider. <br />
 * Denne klassen har en intern anonym klasse, som oppretter 
 * og kjører spørringer direkte mot SQLite databasen. <br />
 * Det som lagres i SQLite databasen er tilkoblede enheter og deres ruter. <br />
 * For egendefinerte databasefunksjoner, se klassen DatabaseFunksjoner.
 * @author Knut Lucas Andersen
 * @see DatabaseFunksjoner
 */
public final class LoggingContentProvider extends ContentProvider {
	/** URI med adressen til databasen (SQLite) **/
	private static final String URI_STRING = "content://dt.android.rutelogging";
	/** Alle rader er valgt (for datamanipulasjon) **/
	private static final int ALLE_RADER = 1;
	/** Enkelt rad er valgt (for datamanipulasjon) **/
	private static final int ENKELT_RAD = 2;
	private static final UriMatcher uriMatcher;
	//verdier som tilhører UriMatcher objektet
	static {
		uriMatcher = new UriMatcher(UriMatcher.NO_MATCH);
		uriMatcher.addURI("dt.android.rutelogging", "rutelogger", ALLE_RADER);
		uriMatcher.addURI("dt.android.rutelogging", "rutelogger/#", ENKELT_RAD);
	}
	/** URI til denne databasen **/
	protected static final Uri DATABASE_URI = Uri.parse(URI_STRING);
	/** Tabellen som inneholder enheter **/
	protected static final String DB_TBLENHET = "tblEnhet";
	/** Primærnøkkel/identifikator for enheten **/
	protected static final String TBLENHET_PRIMARY_KEY = "enhet_id";
	/** Tar vare på verdi for enhetens navn **/
	protected static final String TBLENHET_NAVN = "enhetsNavn";
	/** Tar vare på verdi om dette er brukers enhet eller andres **/ 
	protected static final String TBLENHET_EGEN_ENHET = "egenEnhet";
	/** Tabellen som inneholder koordinater/posisjonsdata **/
	protected static final String DB_TBLKOORDINATER = "tblKoordinater";
	/** Primærnøkkel til disse koordinatene **/ 
	protected static final String TBLKOORDINATER_PRIMARY_KEY = "koordinat_id";
	/** Tar vare på verdien til mottatt latitude **/
	protected static final String TBLKOORDINATER_LATITUDE = "latitude";
	/** Tar vare på verdien til mottatt longitude **/
	protected static final String TBLKOORDINATER_LONGITUDE = "longitude";
	/** Tar vare på verdien til når koordinatene ble lagret **/ 
	protected static final String TBLKOORDINATER_RUTETIDSPUNKT = "ruteTidspunkt";
	/** Tar vare på verdien til rutens ID (for å opprette en sammenhengende rute **/
	protected static final String TBLKOORDINATER_RUTEID = "ruteID";
	/** Tar vare på verdien for registrationID til enhet hvis koordinater lagres **/
	protected static final String TBLKOORDINATER_REGISTRATIONID = "registrationID";
	/** Fremmednøkkel som kobler sammen koordinatene med enheten **/
	protected static final String TBLKOORDINATER_FOREIGN_KEY = "fk_enhet";
	/** Dato format som brukes for å lagre rutetidspunkt i databasen **/
	protected static final String DATO_TID_FORMAT = "yyyy-MM-dd HH:mm:ss";
	//variabler
	private static String _tblQuery;
	private static SQLiteHelperClass _sqlHelper;

	@Override
	public boolean onCreate() {
		//oppretter den underliggende databasen
		//databasen blir her kun opprettet, 
		//det blir ikke opprettet en connection
		_sqlHelper = new SQLiteHelperClass(getContext(), SQLiteHelperClass.DATABASE_NAVN, 
				null, SQLiteHelperClass.DATABASE_VERSJON);
		return true;
	} //onCreate

	@Override
	public Uri insert(Uri uri, ContentValues values) {
		//les fra databasen (read-only)
		SQLiteDatabase db = _sqlHelper.getWritableDatabase();
		//for å legge til tomme rader i databasen, ved å sende over tomme verdier i et objekt, 
		//må en bruke "null column hack" som parameter for å spesifisere at kolonnen skal settes 
		//til null
		String nullColumnHack = null;
		//legg verdiene inn i tabellen
		long id = db.insert(getTblQuery(), nullColumnHack, values);
		//ble verdier lagt inn i tabell?
		if (id > -1) {
			//konstruer og returner en URI som inneholder opprettet rads ID
			Uri innlagtID = ContentUris.withAppendedId(DATABASE_URI, id);
			//meld i fra om at nye rader er lagt til 
			getContext().getContentResolver().notifyChange(innlagtID, null);
			//return URI
			return innlagtID;
		} else {
			//rad ble ikke opprettet, returner null
			return null;
		} //if (id > -1) 
	} //insert

	@Override
	public Cursor query(Uri uri, String[] projection, String selection, String[] selectionArgs, String sortOrder) {
		//les fra databasen (read-only)
		SQLiteDatabase db = _sqlHelper.getWritableDatabase();
		//plassholdere - hvis behov...
		String groupBy = null;
		String having = null;
		//bygg spørring og sett tabell som skal brukes
		SQLiteQueryBuilder queryBuilder = new SQLiteQueryBuilder();
		queryBuilder.setTables(getTblQuery());
		//hvis det er uthenting av en rad, hent ut den enkelte raden...
		switch (uriMatcher.match(uri)) {
		case ENKELT_RAD:
			//hent radens ID
			String radID = uri.getPathSegments().get(1);
			if (getTblQuery().equals(DB_TBLENHET)) {
				//legg til where i spørring som spesifiserer radens ID
				queryBuilder.appendWhere(TBLENHET_PRIMARY_KEY + "=" + radID);
			} else {
				queryBuilder.appendWhere(TBLKOORDINATER_PRIMARY_KEY + "=" + radID);	
			} //if (getTblQuery().equals(DB_TBLENHET))
		default:
			break;
		} //switch
		//opprett peker som inneholder resultatet av spørringen
		Cursor peker = queryBuilder.query(db, projection, selection, selectionArgs, groupBy, having, sortOrder);
		return peker;
	} //query

	/**
	 * Funksjon som lar en skrive en full query og forsøker å kjøre det 
	 * mot databasen.
	 * @param query - String: Spørringen som skal kjøres
	 * @param selectionArgs - String[]
	 * @return Cursor: Resultatet av spørringen
	 */
	protected static Cursor rawQuery(String query, String selectionArgs[]) {
		SQLiteDatabase db = _sqlHelper.getWritableDatabase();
		Cursor cursor =  db.rawQuery(query, selectionArgs);
		return cursor;
	} //rawQuery

	@Override
	public int update(Uri uri, ContentValues values, String selection, String[] selectionArgs) {
		//les fra databasen (read-only)
		SQLiteDatabase db = _sqlHelper.getWritableDatabase();
		//hvis det kun er en rad, begrens oppdatering til valgt rad
		switch (uriMatcher.match(uri)) {
		case ENKELT_RAD:
			//hent ut radens ID
			String radID = uri.getPathSegments().get(1);
			if (getTblQuery().equals(DB_TBLENHET)) {
				//legg til where i spørring som spesifiserer radens ID
				selection = TBLENHET_PRIMARY_KEY + "=" + radID
						//hvis selektering ikke er tom, legg den med via 'AND'
						+ (!TextUtils.isEmpty(selection) ? 
								" AND (" + selection + ')' : "");
			} else {
				selection = TBLKOORDINATER_PRIMARY_KEY + "=" + radID
						//hvis selektering ikke er tom, legg den med via 'AND'
						+ (!TextUtils.isEmpty(selection) ? 
								" AND (" + selection + ')' : "");
			} //if (getTblQuery().equals(DB_TBLENHET))
		default: 
			break;
		} //switch
		//oppdater databasen
		int updateCount = db.update(getTblQuery(), values, selection, selectionArgs);
		//meld ifra om at databasen er oppdatert
		getContext().getContentResolver().notifyChange(uri, null);
		//returner antall rader som ble oppdatert
		return updateCount;
	} //update

	@Override
	public int delete(Uri uri, String selection, String[] selectionArgs) {
		//les fra databasen (read-only)
		SQLiteDatabase db = _sqlHelper.getWritableDatabase();
		//hvis det er en rad URI, begrens sletting til valgt rad
		switch (uriMatcher.match(uri)) {
		case ENKELT_RAD:
			//hent radens ID
			String radID = uri.getPathSegments().get(1);
			if (getTblQuery().equals(DB_TBLENHET)) {
				//legg til where i spørring som spesifiserer radens ID
				selection = TBLENHET_PRIMARY_KEY + "=" + radID
						//hvis selektering ikke er tom, legg den med via 'AND'
						+ (!TextUtils.isEmpty(selection) ? 
								" AND (" + selection + ')' : "");
			} else {
				selection = TBLKOORDINATER_PRIMARY_KEY + "=" + radID
						//hvis selektering ikke er tom, legg den med via 'AND'
						+ (!TextUtils.isEmpty(selection) ? 
								" AND (" + selection + ')' : "");
			} //if (getTblQuery().equals(DB_TBLENHET))
		default: 
			break;
		} //switch
		//For å returnere antallet som ble slettet, trengs en where klasul.
		//Dersom ingen enkelt rad ble spesifisert, send over 1 (sletter alle rader).
		if (selection == null) {
			selection = "1";
		} //if (selection == null)
		//utfør sletting
		int deleteCount = db.delete(getTblQuery(), selection, selectionArgs);
		//meld i fra om at sletting ble utført
		getContext().getContentResolver().notifyChange(uri, null);
		//returner antall slettede elementer
		return deleteCount; 
	} //delete

	/**
	 * Sletter fra tabell i database basert på oversendte parameter.
	 * @param table - String: Tabellen det skal slettes fra
	 * @param whereClause - String: Where-klasel for spesifisering
	 * @param whereArgs - String[]
	 * @return int: Antall slettede rader
	 */
	protected static int delete(String table, String whereClause, String whereArgs[]) {
		SQLiteDatabase db = _sqlHelper.getWritableDatabase();
		return db.delete(table, whereClause, whereArgs);
	} //delete

	/**
	 * Sletter eksisterende database og oppretter den på nytt. <br />
	 * Dersom den skal brukes til å slette alt, kan newVersion settes til 1.
	 * @param newVersion - int: Den nye versjonen av databasen
	 */
	protected static void dropDatabase(int newVersion) {
		SQLiteDatabase db = _sqlHelper.getWritableDatabase(); 
		_sqlHelper.onUpgrade(db, SQLiteHelperClass.DATABASE_VERSJON, newVersion);
	} //dropDatabase

	@Override
	public String getType(Uri uri) {
		String type = null;
		switch (uriMatcher.match(uri)) {
		case ALLE_RADER:
			type = "vnd.android.cursor.dir/vnd.paad.logging";
			break;
		case ENKELT_RAD:
			type = "vnd.android.cursor.item/vnd.paad.logging";
			break;
		default:
			throw new IllegalArgumentException("Unsupported URI: " + uri);
		} //switch
		return type;
	} //getType

	protected static String getTblQuery() {
		return _tblQuery;
	} //getTblQuery

	protected static void setTblQuery(String tblQuery) {
		_tblQuery = tblQuery;
	} //setTblQuery

	private static class SQLiteHelperClass extends SQLiteOpenHelper {
		private static final String DATABASE_NAVN = "tracknHideDatabase.db";
		private static final int DATABASE_VERSJON = 1;		
		private static final String DROP_TABELL_ENHET = "DROP TABLE IF EXISTS " + DB_TBLENHET;
		private static final String DROP_TABELL_KOORDINATER = "DROP TABLE IF EXISTS " + DB_TBLKOORDINATER;
		//oppretter tabell for enheter
		private static final String CREATE_DATABASE_ENHET = "create table "
				+ DB_TBLENHET + " (" 
				+ TBLENHET_PRIMARY_KEY + " integer primary key autoincrement, "
				+ TBLENHET_NAVN + " text not null, "
				+ TBLENHET_EGEN_ENHET + " integer not null"
				+ ");";
		//oppretter tabell for koordinater/posisjonering
		private static final String CREATE_DATABASE_KOORDINATER ="create table "
				+ DB_TBLKOORDINATER + " (" 
				+ TBLKOORDINATER_PRIMARY_KEY + " integer primary key autoincrement, " 
				+ TBLKOORDINATER_REGISTRATIONID + " text not null, "
				+ TBLKOORDINATER_LATITUDE + " real not null, "
				+ TBLKOORDINATER_LONGITUDE + " real not null, "
				+ TBLKOORDINATER_RUTETIDSPUNKT + " text not null," 
				+ TBLKOORDINATER_RUTEID + " integer not null,"
				+ TBLKOORDINATER_FOREIGN_KEY + " integer not null, "
				+ "FOREIGN KEY(" + TBLKOORDINATER_FOREIGN_KEY + ") REFERENCES " + DB_TBLENHET + "(" + TBLENHET_PRIMARY_KEY + "));";

		public SQLiteHelperClass(Context context, String name, CursorFactory factory, int version) {
			super(context, name, factory, version);
		} //konstruktør

		@Override
		public void onCreate(SQLiteDatabase db) {
			// oppretter database dersom database ikke finnes
			db.execSQL(CREATE_DATABASE_ENHET);
			db.execSQL(CREATE_DATABASE_KOORDINATER);
		} //onCreate

		@Override
		public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
			//oppgraderer databasen dersom det finnes flere versjoner
			Log.w("Posisjonsdatabase", "Oppgraderer fra versjon " + oldVersion 
					+ " til versjon " + newVersion + ". Dette sletter alle gamle data!");
			//dropper de gamle tabellene
			db.execSQL(DROP_TABELL_KOORDINATER);
			db.execSQL(DROP_TABELL_ENHET);
			//oppretter ny database
			onCreate(db);
		} //onUpgrade
	} //SQLiteHelperClass
} //LoggingContentProvider