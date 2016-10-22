package com.example.oblig3_knutlucasandersen_del1;

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
 * og kjører spørringer direkte mot SQLite databasen.
 * @author Knut Lucas Andersen
 */
public class LoggingContentProvider extends ContentProvider {
	//private konstanter
	private static final String URI_STRING = "content://com.example.systemlogging";
	private static final int ALLE_RADER = 1;
	private static final int ENKELT_RAD = 2;
	private static final UriMatcher uriMatcher;
	//verdier som tilhører UriMatcher objektet
	static {
		uriMatcher = new UriMatcher(UriMatcher.NO_MATCH);
		uriMatcher.addURI("com.example.systemlogging", "systemlogger", ALLE_RADER);
		uriMatcher.addURI("com.example.systemlogging", "systemlogger/#", ENKELT_RAD);
	}
	//public konstanter
	public static final Uri LOGG_URI = Uri.parse(URI_STRING);
	public static final String LOGG_ID = "logg_id";
	public static final String LOGG_DATO = "logg_dato";
	public static final String LOGG_TIDSPUNKT = "logg_tidspunkt";
	public static final String LOGG_KATEGORI = "logg_kategori";
	public static final String LOGG_TEKST = "logg_tekst";
	public static final String LOGG_DETALJER = "logg_detaljer";
	//variabler
	private SQLiteHelperClass sqlHelper;

	@Override
	public int delete(Uri uri, String selection, String[] selectionArgs) {
		//les fra databasen (read-only)
		SQLiteDatabase db = sqlHelper.getWritableDatabase();
		//hvis det er en rad URI, begrens sletting til valgt rad
		switch (uriMatcher.match(uri)) {
		case ENKELT_RAD:
			//hent radens ID
			String radID = uri.getPathSegments().get(1);
			selection = LOGG_ID + "=" + radID
					//hvis selektering ikke er tom, legg den med via 'AND'
					+ (!TextUtils.isEmpty(selection) ? 
							" AND (" + selection + ')' : "");
		default: 
			break;
		} //switch
		//For å returnere antallet som ble slettet, trengs en where klasul.
		//Dersom ingen enkelt rad ble spesifisert, send over 1 (sletter alle rader).
		if (selection == null) {
			selection = "1";
		} //if (selection == null)
		//utfør sletting
		int deleteCount = db.delete(SQLiteHelperClass.DATABASE_TABELL, selection, selectionArgs);
		//meld i fra om at sletting ble utført
		getContext().getContentResolver().notifyChange(uri, null);
		//returner antall slettede elementer
		return deleteCount; 
	} //delete

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

	@Override
	public Uri insert(Uri uri, ContentValues values) {
		//les fra databasen (read-only)
		SQLiteDatabase db = sqlHelper.getWritableDatabase();
		//for å legge til tomme rader i databasen, ved å sende over tomme verdier i et objekt, 
		//må en bruke "null column hack" som parameter for å spesifisere at kolonnen skal settes 
		//til null
		String nullColumnHack = null;
		//legg verdiene inn i tabellen
		long id = db.insert(SQLiteHelperClass.DATABASE_TABELL, nullColumnHack, values);
		//ble verdier lagt inn i tabell?
		if (id > -1) {
			//konstruer og returner en URI som inneholder opprettet rads ID
			Uri innlagtID = ContentUris.withAppendedId(LOGG_URI, id);
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
	public boolean onCreate() {
		//oppretter den underliggende databasen
		//databasen blir her kun opprettet, 
		//det blir ikke opprettet en connection
		sqlHelper = new SQLiteHelperClass(getContext(), SQLiteHelperClass.DATABASE_NAVN, 
				null, SQLiteHelperClass.DATABASE_VERSJON);
		return true;
	} //onCreate

	@Override
	public Cursor query(Uri uri, String[] projection, String selection,
			String[] selectionArgs, String sortOrder) {
		//les fra databasen (read-only)
		SQLiteDatabase db = sqlHelper.getWritableDatabase();
		//plassholdere - hvis behov...
		String groupBy = null;
		String having = null;
		//bygg spørring og sett tabell som skal brukes
		SQLiteQueryBuilder queryBuilder = new SQLiteQueryBuilder();
		queryBuilder.setTables(SQLiteHelperClass.DATABASE_TABELL);
		//hvis det er uthenting av en rad, hent ut den enkelte raden...
		switch (uriMatcher.match(uri)) {
		case ENKELT_RAD:
			//hent radens ID
			String radID = uri.getPathSegments().get(1);
			//legg til where i spørring som spesifiserer radens ID
			queryBuilder.appendWhere(LOGG_ID + "=" + radID);
		default:
			break;
		} //switch
		//opprett peker som inneholder resultatet av spørringen
		Cursor peker = queryBuilder.query(db, projection, selection, selectionArgs, groupBy, having, sortOrder);
		return peker;
	} //query

	@Override
	public int update(Uri uri, ContentValues values, String selection, String[] selectionArgs) {
		//les fra databasen (read-only)
		SQLiteDatabase db = sqlHelper.getWritableDatabase();
		//hvis det kun er en rad, begrens oppdatering til valgt rad
		switch (uriMatcher.match(uri)) {
		case ENKELT_RAD:
			//hent ut radens ID
			String radID = uri.getPathSegments().get(1);
			selection = radID + "=" + radID
					//hvis selektering ikke er tom, legg den med via 'AND'
					+ (!TextUtils.isEmpty(selection) ? 
							" AND (" + selection + ')' : "");
		default: 
			break;
		} //switch
		//oppdater databasen
		int updateCount = db.update(SQLiteHelperClass.DATABASE_TABELL, values, selection, selectionArgs);
		//meld ifra om at databasen er oppdatert
		getContext().getContentResolver().notifyChange(uri, null);
		//returner antall rader som ble oppdatert
		return updateCount;
	} //update

	private static class SQLiteHelperClass extends SQLiteOpenHelper {
		private static final String DATABASE_NAVN = "systemLoggingDatabase.db";
		private static final int DATABASE_VERSJON = 1;
		private static final String DATABASE_TABELL = "system_logg_table";
		private static final String DROP_TABELL = "(DROP TABLE IF EXISTS " + DATABASE_TABELL + ");";
		//setning som oppretter database
		private static final String CREATE_DATABASE = "create table "
				+ DATABASE_TABELL + " (" 
				+ LOGG_ID + " integer primary key autoincrement, " 
				+ LOGG_DATO + " date not null, " 
				+ LOGG_TIDSPUNKT + " text not null, "
				+ LOGG_KATEGORI + " text not null, "
				+ LOGG_TEKST + " text not null, "
				+ LOGG_DETALJER + " text not null"
				+ ");";

		public SQLiteHelperClass(Context context, String name, CursorFactory factory, int version) {
			super(context, name, factory, version);
			// TODO Auto-generated constructor stub
		} //konstruktør

		@Override
		public void onCreate(SQLiteDatabase db) {
			// oppretter database dersom database ikke finnes
			db.execSQL(CREATE_DATABASE);
		} //onCreate

		@Override
		public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
			//oppgraderer databasen dersom det finnes flere versjoner
			Log.w("Systemloggingsdatabase", "Oppgraderer fra versjon " + oldVersion 
					+ " til versjon " + newVersion + ". Dette sletter alle gamle data!");
			//dropper den gamle tabellen
			db.execSQL(DROP_TABELL);
			//oppretter ny database
			onCreate(db);
		} //onUpgrade
	} //SQLiteHelperClass
} //LoggingContentProvider
