package oblig_244;
/**
 * Grunnklassene som baserer seg på dyrene som kan
 * fanges og deretter underklassene som arver egenskapene
 */
class Gjenfangst extends Dyr {
	private String farge;	 

	public Gjenfangst(String idDyr, double lengde, double vekt, 
			String farge, String sted, String dato) {		
		super(idDyr, lengde, vekt, sted, dato);
		this.farge = farge;	
	} //konstruktør for gjenfanget hare

	public Gjenfangst(String idDyr,double lengde, double vekt, 
			String sted, String dato) {
		//kaller på verdiene og funksjonene fra klassen Dyr
		super(idDyr, lengde, vekt, sted, dato);
	} //konstruktør for gjenfanget gaupe

	public String getFarge() {
		return farge;
	} //getFarge
} //klassen Gjenfangst

class Hare extends Dyr {
	private char hareType;
	private String farge;

	public Hare(String idDyr,char kjonn, double lengde, double vekt, String sted, String dato,
			char hareType, String farge) {
		//kaller på verdiene og funksjonene fra klassen Dyr
		super(idDyr, kjonn, lengde, vekt, sted, dato);
		this.hareType = hareType;
		this.farge = farge;
	} //konstruktør

	public char getHareType() {
		return hareType;
	}//getHareType

	public String getFarge() {
		return farge;
	} //getFarge
} //klassen Hare

class Gaupe extends Dyr {
	private double gaupeOre;

	public Gaupe(String idDyr,char kjonn, double lengde, double vekt, 
			String sted, String dato, double gaupeOre) {
		//kaller på verdiene og funksjonene fra klassen Dyr
		super(idDyr, kjonn, lengde, vekt, sted, dato);
		this.gaupeOre = gaupeOre;
	} //konstruktør

	public double getGaupeOre() {
		return gaupeOre;
	} //getGaupeOre
} //klassen Gaupe

class Dyr {
	private String idDyr;
	private char kjonn;
	private double lengde;
	private double vekt;
	private String sted;
	private String dato;	

	public Dyr (String idDyr,char kjonn, double lengde, 
			double vekt, String sted, String dato) {	
		this.idDyr = idDyr;
		this.kjonn = kjonn;
		this.lengde = lengde;
		this.vekt = vekt;
		this.sted = sted;
		this.dato = dato;		
	} //konstruktør som oppretter felles verdier for alle dyr


	public Dyr (String idDyr, double lengde, 
			double vekt, String sted, String dato) {	
		this.idDyr = idDyr;
		this.lengde = lengde;
		this.vekt = vekt;
		this.sted = sted;
		this.dato = dato;		
	}	//konstruktør som oppretter felles verdier klassen gjenfangst		

	public String getId(){
		return idDyr;
	}
	public char getKjonn(){
		return kjonn;
	}	
	public Double getLengde(){
		return lengde;
	}	
	public Double getVekt(){
		return vekt;
	}	
	public String getSted(){
		return sted;
	}	
	public String getDato(){
		return dato;
	}
} //klassen Dyr