package dt.hin.android.kl_andersen;

//java-import
import java.util.ArrayList;
//android-import
import android.content.Context;
import android.content.SharedPreferences;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.graphics.Path;
import android.graphics.Point;
import android.graphics.RectF;
import android.view.MotionEvent;
//google-import
import com.google.android.maps.GeoPoint;
import com.google.android.maps.MapView;
import com.google.android.maps.Overlay;
import com.google.android.maps.Projection;

/**
 * Klasse som håndterer tegning på kartet. <br />
 * Ting som tegnes på kartet er: <br />
 * - Online brukeres rute<br />
 * - Brukernes navn (på sist mottatte posisjon)<br />
 * - Det opprettes også en pop-up dialog via {@linkplain Overlay#onTouchEvent(MotionEvent, MapView)}<br />
 * @author Knut Lucas Andersen
 */
public class MapOverlay extends Overlay {
	/** Minimumstid bruker må trykke på skjermen før popup-dialog vises **/
	private final long minTid = 1000;
	/** Maksimumstid bruker må trykke på skjermen før popup-dialog vises **/
	private final long maksTid = 2500;
	/** Radius på rektangel som tegnes bak enhetens navn på kartet **/
	private final static int TEKST_RADIUS = 5;
	//variabler
	private Path _rute;
	private long _startTid;
	private long _stoppTid;
	private Paint _rutePaint;
	private Projection _projeksjon;
	private GeoPoint _forrige, _neste;
	//static objekter
	private static Context _context;
	private static boolean _egenEnhet;
	private static RectF _rektangel;
	private static String _enhetsNavn;
	private static String[] _fargeArray;
	private static GeoPoint _beroringsPunkt;
	private static ArrayList<Enhet> _onlineEnheter;
	private static Point _forrigePunkt, _nestePunkt;
	private static SharedPreferences _deltPreferanse;
	private static Paint _tekstPaint, _bakgrunnsPaint;
	private static ArrayList<GeoPoint> _koordinatListe;	

	public MapOverlay(Context context) {
		_context = context;
		_enhetsNavn = "";
		_egenEnhet = false;
		_nestePunkt = new Point();
		_forrigePunkt = new Point();
		_onlineEnheter = new ArrayList<Enhet>();
		_koordinatListe = new ArrayList<GeoPoint>();
		_deltPreferanse = Filbehandling.getSharedPreferances(_context);
		_fargeArray = _context.getResources().getStringArray(R.array.rute_farger_array);
		opprettRutePaint();
		opprettTekstPaint();
	} //konstruktør

	private void opprettRutePaint() {
		//opprett objekt for tegning av rute på kartet
		_rutePaint = new Paint();
		_rutePaint.setDither(true);
		_rutePaint.setColor(hentFargeSomSkalBrukes(0, false));
		_rutePaint.setStyle(Paint.Style.FILL_AND_STROKE);
		_rutePaint.setStrokeJoin(Paint.Join.ROUND);
		_rutePaint.setStrokeCap(Paint.Cap.ROUND);
		_rutePaint.setStrokeWidth(2);
	} //opprettRutePaint

	private void opprettTekstPaint() {
		_tekstPaint = new Paint();
		_tekstPaint.setARGB(250, 255, 255, 255);
		_tekstPaint.setAntiAlias(true);
		_tekstPaint.setFakeBoldText(true);
		_bakgrunnsPaint = new Paint();
		_bakgrunnsPaint.setARGB(175, 50, 50, 50);
		_bakgrunnsPaint.setAntiAlias(true);
	} //opprettTekstPaint

	@Override
	public void draw(Canvas canvas, MapView mapView, boolean shadow) {
		_onlineEnheter = FellesFunksjoner.getOnlineEnheter();
		//skal det tegnes på kartet, og finnes det enheter som kan tegnes?
		if (!shadow && _onlineEnheter.size() > 0) {
			_projeksjon = mapView.getProjection();
			_rute = new Path();
			for (int i = 0; i < _onlineEnheter.size(); i++) {
				_rute.reset();
				_enhetsNavn = _onlineEnheter.get(i).getEnhetsnavn();
				_egenEnhet = _onlineEnheter.get(i).getEgenEnhet();
				_rutePaint.setColor(hentFargeSomSkalBrukes(i, _egenEnhet));
				_koordinatListe = _onlineEnheter.get(i).getKoordinater();
				_forrige = _koordinatListe.get(0);
				//starter loop på 2.element i arraylist
				for (int j = 1; j < _koordinatListe.size(); j++) {
					//sett _neste punkt til gjeldende element i arraylist
					_neste = _koordinatListe.get(j);
					//konverter geopunkt til pixler
					_projeksjon.toPixels(_forrige, _forrigePunkt);
					_projeksjon.toPixels(_neste, _nestePunkt);
					//opprett ruta som skal tegnes
					_rute.moveTo(_nestePunkt.x, _nestePunkt.y);
					_rute.lineTo(_forrigePunkt.x,_forrigePunkt.y);
					//sett dette geopunktet til forrige punkt (for bruk i neste runde)
					_forrige = _neste;
				} //indre for				
				//tegn ruten og enhetsnavnet til denne brukeren
				canvas.drawPath(_rute, _rutePaint);
				tegnEnhetsnavn(canvas, _enhetsNavn);
			} //ytre for
		} //if (!shadow && brukersRute.size() > 2)
		super.draw(canvas, mapView, shadow);
	} //draw

	/**
	 * Funksjon som returnerer en parset verdi for fargen som skal brukes for å tegne rute på kartet. <br />
	 * Det er lagt vekt på å prøve å gjøre brukers farge unik, m.a.o. at bruker er den eneste med "sin" farge på kartet. <br />
	 * Det sjekkes derfor om det er brukers enhet som skal tegnes på kartet, og at gjeldende indeks ikke er brukers farge.
	 * @param fargeIndeks - int: Tallverdi for uthenting av farge fra array 
	 * @param egenEnhet - boolean: Er dette brukers enhet hvis rute skal tegnes?
	 * @return int: Resultatet av {@link Color#parseColor(String)}
	 */
	private static int hentFargeSomSkalBrukes(int fargeIndeks, boolean egenEnhet) {
		int antFarger = _fargeArray.length - 1;
		//hent ut  indeks for hvilken farge brukers rute skal vises i
		int brukersValg = _deltPreferanse.getInt(Filbehandling.RUTEFARGE_NOKKEL, 0);
		//er dette brukers egen enhet?
		if (egenEnhet) {
			fargeIndeks = brukersValg; 
		} else {
			//øk farge og kontroller om fargen må nullstilles
			fargeIndeks++;
			fargeIndeks = nullStillFargeIndeks(antFarger, fargeIndeks);
			//hvis farge ble nullstilt, eller hvis fargeIndeks ble økt, 
			//sjekk at nåværende farge ikke er den samme som brukerens farge
			if (brukersValg == fargeIndeks) {
				fargeIndeks++;
				fargeIndeks = nullStillFargeIndeks(antFarger, fargeIndeks);
			} //if (brukersValg == _fargeIndeks)
		} //if (egenEnhet)
		return Color.parseColor(_fargeArray[fargeIndeks]);
	} //hentFargeSomSkalBrukes

	/**
	 * Sjekker at gjeldende indeks for farge som skal hentes fra array ikke 
	 * overstiger maks antall elementer i array. <br /> 
	 * Hvis indeks er større en antall elementer i array, blir indeks satt til null.
	 * @param antFarger
	 * @param indeksFarge
	 * @return int: indeksFarge || 0
	 */
	private static int nullStillFargeIndeks(int antFarger, int indeksFarge) {
		if (indeksFarge > antFarger) {
			indeksFarge = 0;
		} //if (indeksFarge > antFarger)
		return indeksFarge;
	} //nullStillFargeIndeks

	/**
	 * Tegner enhetsnavnet på siden av ruten til gjeldende enhet. <br />
	 * Enhetsnavnet tegnes på enhets siste posisjon.
	 * @param canvas - Canvas: Canvaset det skal tegnes på
	 * @param enhetsNavn - String: Gjeldende enhets navn som skal tegnes
	 * @see RectF#RectF(float, float, float, float)
	 * @see Canvas#drawRect(android.graphics.Rect, Paint)
	 * @see Canvas#drawText(String, float, float, Paint)
	 */
	private static void tegnEnhetsnavn(Canvas canvas, String enhetsNavn) {
		//beregning av lengde og høyde for rektangel i bakgrunnen av navnet
		//formelen for lengeX er basert på eksemplet fra Kap. 13 - Where Am I
		//hvor jeg testet med lengre navn for å få ut en tallverdi som 
		//"passet" med lengden på navnet. formelen for y er den samme som i boka
		int lengdeX = _nestePunkt.x + (9 * _enhetsNavn.length()), 
				hoydeY = _nestePunkt.y - (3 * TEKST_RADIUS);
		//opprett rektangelet
		_rektangel = new RectF(_nestePunkt.x + 2 + TEKST_RADIUS,
				hoydeY, lengdeX, _nestePunkt.y + TEKST_RADIUS);
		//plassering av tekstens X (gjenbruk av variabel) for å forbedre lesning
		lengdeX = _nestePunkt.x + (2 * TEKST_RADIUS);
		//tegn rektangelet og teksten
		canvas.drawRoundRect(_rektangel, TEKST_RADIUS, TEKST_RADIUS, _bakgrunnsPaint);
		canvas.drawText(_enhetsNavn, lengdeX, _nestePunkt.y, _tekstPaint);
	} //tegnEnhetsnavn

	@Override
	public boolean onTouchEvent(MotionEvent e, MapView mapView) {
		long tidPresset = 0;
		//hent ut skjermkoordinatene
		MainMapActivity.skjermX = (int)e.getX();
		MainMapActivity.skjermY = (int)e.getY();
		//konverter berøringspunktet til et punkt på kartet
		_beroringsPunkt = mapView.getProjection().fromPixels(MainMapActivity.skjermX, MainMapActivity.skjermY);
		//er skjermen berørt?
		if (e.getAction() == MotionEvent.ACTION_DOWN) {
			_startTid = e.getEventTime();
		} //if (e.getAction() == MotionEvent.ACTION_DOWN)
		//er skjermen ikke lenger berørt?
		if (e.getAction() == MotionEvent.ACTION_UP) {
			_stoppTid = e.getEventTime();
		} //if (e.getAction() == MotionEvent.ACTION_UP)
		tidPresset = _stoppTid - _startTid;
		if (tidPresset < maksTid && tidPresset > minTid) {
			MainMapActivity.opprettPopupDialog();			
		} //if (tidPresset < maksTid && tidPresset > minTid)
		return false;
	} //onTouchEvent

	public static GeoPoint getBeroringsPunkt() {
		return _beroringsPunkt;
	} //getBeroringsPunkt

	@Override
	public boolean onTap(GeoPoint point, MapView mapView) {
		return false;
	} //onTap
} //MapOverlay