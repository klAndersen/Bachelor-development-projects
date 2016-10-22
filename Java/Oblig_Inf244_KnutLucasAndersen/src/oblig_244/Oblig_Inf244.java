package oblig_244;
/**
 * Her ligger main-metoden og grensesnittet for hovedmenyen.
 * Hovedmenyen viser alternativene brukeren har.
 */
import java.awt.*;
import javax.swing.*;
import javax.swing.border.*;
import java.awt.event.*;
import static javax.swing.JOptionPane.*;

public class Oblig_Inf244 {	
	public static void main(String[] args) {
		Hovedmeny vindu = new Hovedmeny("Fangstprogram");
		vindu.setSize(400,300); //setter størrelsen
		vindu.setVisible(true); //viser vinduet
	} //main
} //Oblig_Inf244

/***HOVEDMENY***/
class Hovedmeny extends JFrame {	
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	private JButton valg1, valg2, valg3, valg4, valg5, valg6, valg7, lagre, avbryt; //knapper
	JLabel label;
	Kontroll kontroll;

	public Hovedmeny (String tittel) {		
		setTitle(tittel); //gir vinduet et navn/tittel
		setLayout(new GridLayout(9,1));
		MenyKnappeLytter lytt = new MenyKnappeLytter(); //lag en ny lytter
		label = new JLabel("Velkommen til Fangstprogrammet.", JLabel.CENTER);	
		add(label);
		label = new JLabel("(Snarvei for knapppene er Alt + nr. Ikke glem å koble til databasen.)",JLabel.CENTER);
		add(label);
		valg1 = new JButton("1: Koble til databasen"); //lag ny knapp 
		valg1.addActionListener(lytt); //knytt lytter til knappen
		valg1.setMnemonic('1'); //dersom man trykker Alt + 1 så vises dette vinduet (snarvei)
		add(valg1); //legg knappen på skjerm
		valg2 = new JButton("2: Ny hare");
		valg2.addActionListener(lytt);
		valg2.setMnemonic('2');
		add(valg2);
		valg3 = new JButton("3: Ny gaupe");
		valg3.addActionListener(lytt);
		valg3.setMnemonic('3');
		add(valg3);
		valg4 = new JButton("4: Harefangst");
		valg4.addActionListener(lytt);
		valg4.setMnemonic('4');
		add(valg4);
		valg5 = new JButton("5: Gaupefangst");
		valg5.addActionListener(lytt);
		valg5.setMnemonic('5');
		add(valg5);
		valg6 = new JButton("6: Utskrift");
		valg6.addActionListener(lytt);
		valg6.setMnemonic('6');
		add(valg6);				
		valg7 = new JButton("7: Avslutt programmet");
		valg7.addActionListener(lytt);
		valg7.setMnemonic('7');
		add(valg7);		
		pack(); //tilpasser innhold til vinduet
	} //konstruktør	

	private class MenyKnappeLytter implements ActionListener { //hører til hovedmenyen
		public void actionPerformed(ActionEvent hendelse) {
			JButton knapp = (JButton) hendelse.getSource();
			//hvilken knapp ble trykket?
			if (knapp == valg1) { //innstillinger for database
				DatabaseVindu dbVindu = visDatabaseVindu();
				dbVindu.setSize(250,125);
				dbVindu.setVisible(true);				
			} else if (knapp == valg2) { //ny hare
				NyHare hareVindu = visNyHare(); //setter hareVindu lik verdien fra funksjonen
				hareVindu.setSize(450, 400); //setter størrelsen på vinduet
				hareVindu.setVisible(true); //synliggjør vinduet
			} else if (knapp == valg3) { //ny gaupe
				NyGaupe gaupeVindu = visNyGaupe();
				gaupeVindu.setSize(450,300);
				gaupeVindu.setVisible(true);
			} else if (knapp == valg4) {  //ny hare-gjenfangst
				GjenfangstHare gHareVindu = visHGjenfangst();
				gHareVindu.setSize(450,300);
				gHareVindu.setVisible(true);
			} else if (knapp == valg5) { //ny gaupe-gjenfangst
				GjenfangstGaupe gGaupeVindu = visGGjenfangst();
				gGaupeVindu.setSize(450,230);
				gGaupeVindu.setVisible(true);
			} else if (knapp == valg6) { //utskrift			
				Utskrift rapport = visRapport();
				boolean vis = rapport.erUtskriftVisbar();
				if (vis) { //kan vinduet vises?
					rapport.setVisible(true);
				} //if (vis)
			} else { //avslutt
				try {
					kontroll.lukkTilkobling(); //lukk tilkobling til database
				} catch (Exception e) {
					//hvis det ikke blir tilkobling, kastes en NullPointErrorException
					//dette pga den ikke har en forbindelse å lukke, men behovet ser
					//ikke behovet for å gi bruker tilbakemelding om dette
				} // try/catch
				System.exit(0); //avslutter programmet				
			} //if (knapp == k1)
		} //actionPerformed
	} //KnappeLytter		

	/***OPPRETTING AV DIALOG VINDUER***/
	public DatabaseVindu visDatabaseVindu() {
		DatabaseVindu vindu = new DatabaseVindu(this); //oppretter nytt objekt av klassen med dette vinduet som forelder
		return vindu; //returner objektet som ble opprettet
	} //visDatabaseVindu

	public NyHare visNyHare() {
		NyHare vindu = new NyHare(this); 
		return vindu; 
	} //visNyHare

	public NyGaupe visNyGaupe() {
		NyGaupe vindu = new NyGaupe(this);
		return vindu;
	} //visNyGaupe

	public GjenfangstHare visHGjenfangst() {
		GjenfangstHare vindu = new GjenfangstHare(this);
		return vindu;
	} //visHGjenfangst

	public GjenfangstGaupe visGGjenfangst() {
		GjenfangstGaupe vindu = new GjenfangstGaupe(this);
		return vindu;
	} //visGGjenfangst

	public Utskrift visRapport() {
		Utskrift vindu = new Utskrift(this);
		return vindu;
	} //visRapport

	/***DATABASE TILKOBLING***/
	/**
	 * Denne klassen tar for seg oppretting av tilkobling til
	 * databasen, her logger man seg inn. 
	 * (Må gjøre dette hver gang programmet starter)
	 */
	private class DatabaseVindu extends JDialog {
		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		JTextField brukernavn, passord; //tekstfelt		
		JLabel label;

		public DatabaseVindu(JFrame forelder) {		
			super(forelder, "Sett database innstillinger", true);
			setDefaultCloseOperation(JFrame.HIDE_ON_CLOSE);				
			setLayout(new BorderLayout(5, 5)); //mellomrom mellom panelene
			add(new DbekstFelt(), BorderLayout.NORTH);			
			add(new DbKnappePanel(), BorderLayout.SOUTH);
			pack(); //tilpasser innhold til vinduet
		} //konstruktør

		private class DbekstFelt extends JPanel {
			/**
			 * 
			 */
			private static final long serialVersionUID = 1L;

			public DbekstFelt() {
				setLayout(new GridLayout(2,1));
				label = new JLabel("Brukernavn:");
				add(label);
				brukernavn = new JTextField(10);
				add(brukernavn);
				label = new JLabel("Passord:");
				add(label);
				passord = new JTextField(10);			
				add(passord);		
			} //konstruktør
		} //DbekstFelt

		private class DbKnappePanel extends JPanel{
			/**
			 * 
			 */
			private static final long serialVersionUID = 1L;

			public DbKnappePanel() {			
				DbKnappeLytter lytt  =new DbKnappeLytter(); //opprettt lytter
				lagre = new JButton("Sjekk tilkobling"); //lag ny knapp
				lagre.addActionListener(lytt); //knytt lytter til knappen
				lagre.setMnemonic('S'); //sett snarvei på knappen
				add(lagre); //legg knapp i panelet
				avbryt = new JButton("Avbryt");
				avbryt.addActionListener(lytt);
				avbryt.setMnemonic('A');
				add(avbryt);
			} //konstruktør
		} //DbKnappePanel

		private class DbKnappeLytter implements ActionListener {
			public void actionPerformed(ActionEvent hendelse) {
				JButton knapp = (JButton) hendelse.getSource();
				if (knapp == lagre) { //sett igang innsetting i tabell/database				
					String bruker = brukernavn.getText();
					String pwd = passord.getText();					
					sjekkData(bruker, pwd);
				} else { //lukk dette vinduet?
					int svar = showConfirmDialog(null, "Hvis du avbryter, blir ikke brukernavn/passord registrert.\nØnsker du å avbryte? (Yes for ja, No for nei)",
							"Avbryt?", YES_NO_OPTION);
					if (svar == YES_OPTION) { //ønsker å lukke vinduet
						setVisible(false); //skjuler vinduet
					} //if (svar == YES_OPTION)
				} //if (knapp == lagre)
			} //actionPerformed
		} //DbKnappeLytter	

		public void sjekkData(String bruker, String pwd) { //kontroll av data for db-tilkobling
			if (bruker.equals("")) { //er brukernavn feltet tomt?
				Kontroll.lagMessageDialog("Brukernavn er tomt.", "Intet brukernavn", 0);
				brukernavn.requestFocus(); //sett fokus i feltet
			} else if (pwd.equals("")) { //er passordfeltet tomt?
				int svar = showConfirmDialog(null, "Passordfelt er tomt, hvis dette er korrekt, trykk Yes.\nHvis feil trykk No.",
						"Tomt passordfelt", YES_NO_OPTION);
				if (svar == YES_OPTION) { //passord skal være blankt
					getKontroll(bruker, pwd);
					setVisible(false);
				} else { //passord skal ikke være blankt
					passord.requestFocus(); //sett fokus i feltet
				} //if (svar == YES_OPTION)
			} else { //lagt inn etter innlevering
				getKontroll(bruker, pwd); //lagt inn etter innlevering
				setVisible(false);
			} //if (bruker.equals(""))
		} //sjekkData			
	} //DatabaseVindu

	/**
	 * Denne lille metoden oppretter tilkobling til klassen
	 * Kontroll databasen, og returner deretter seg selv.
	 */
	public Kontroll getKontroll(String bruker, String pwd) {			
		//Kontroll.lagMessageDialog(b,"getKontroll",2);
		kontroll = Kontroll.getKontrollIinstans(bruker, pwd);		
		return kontroll;
	} //getKontroll	

	/***NY FANGST (HARE/GAUPE)***/
	/**
	 * Denne klassen tar for seg grensesnittet og mottak av data fra
	 * bruker for registrering av førstegangs-fangst av en hare
	 */
	private class NyHare extends JDialog {	
		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		private String dyret = "haren"; //variabel som sendes til TekstFeltVindu
		private TekstFeltVindu tekst = new TekstFeltVindu(dyret);
		private RadiopanelKjonn kjonn = new RadiopanelKjonn(dyret);
		private RadiopanelType type = new RadiopanelType();
		private RadiopanelFarge farge = new RadiopanelFarge();			

		public NyHare (JFrame forelder) {
			super(forelder, "Registrer fangst av hare", true);		
			setDefaultCloseOperation(JFrame.HIDE_ON_CLOSE);				
			setLayout(new BorderLayout(5, 5)); //mellomrom mellom panelene
			add(new NyHareTekstPanel(), BorderLayout.NORTH);
			add(new NyHareRadioOmraade(), BorderLayout.CENTER);
			add(new NyHareKnappePanel(), BorderLayout.SOUTH);
			pack(); //tilpasser innhold til vinduet
		} //konstruktør

		private class NyHareTekstPanel extends JPanel {
			/**
			 * 
			 */
			private static final long serialVersionUID = 1L;

			public NyHareTekstPanel() {
				add(tekst);
			} //konstruktør
		} //NyHarePanel

		private class NyHareRadioOmraade extends JPanel {
			/**
			 * 
			 */
			private static final long serialVersionUID = 1L;

			public NyHareRadioOmraade() {
				setLayout(new GridLayout(3,1,5,5));
				add(kjonn); //legg til på panelet
				add(type);
				add(farge);
			} //konstruktør
		} //NyHareRadioOmraade

		private class NyHareKnappePanel extends JPanel{
			/**
			 * 
			 */
			private static final long serialVersionUID = 1L;

			public NyHareKnappePanel() {			
				NyHareKnappeLytter lytt  =new NyHareKnappeLytter(); //opprettt lytter
				lagre = new JButton("Lagre"); //lag ny knapp
				lagre.addActionListener(lytt); //knytt lytter til knappen
				lagre.setMnemonic('L');
				add(lagre); //legg knapp i panelet
				avbryt = new JButton("Avbryt");
				avbryt.addActionListener(lytt);
				avbryt.setMnemonic('A');
				add(avbryt);
			} //konstruktør
		} //NyHareKnappePanel

		private class NyHareKnappeLytter implements ActionListener {
			public void actionPerformed(ActionEvent hendelse) {
				JButton knapp = (JButton) hendelse.getSource();
				if (knapp == lagre) { //sett igang innsetting i tabell/database				
					sjekkHare();
				} else { //lukk dette vinduet
					setVisible(false); //skjuler vinduet
				} //if (knapp == lagre)
			} //actionPerformed
		} //NyHareKnappeLytter	

		public void sjekkHare() {		
			JTextField lengde = new JTextField(); //lager et nytt tekstfeltobjekt...
			JTextField vekt = new JTextField();
			JTextField sted = new JTextField();
			JTextField dato = new JTextField();
			lengde.setText( tekst.getLengde());  //...hent ut orginalobjektets innhold og legg innholdet inn i det nye tekstfeltet...
			vekt.setText( tekst.getVekt());		
			sted.setText(tekst.getSted());
			dato.setText(tekst.getDato());
			boolean okLengde = Kontroll.sjekkLengde(lengde.getText()); //...som deretter sjekkes om innhold er ok...
			if (okLengde) { //er lengde ok?
				boolean okVekt = Kontroll.sjekkVekt(vekt.getText());
				if (okVekt) { //er vekt ok?
					boolean okSted = Kontroll.sjekkSted(sted.getText().toUpperCase());
					if (okSted) { //er sted oppgitt ok?
						boolean okDato = Kontroll.sjekkDato(dato.getText());
						if (okDato) { //er dato ok?
							//alt ok, hent verdier
							double l = Double.parseDouble(lengde.getText());
							double v = Double.parseDouble(vekt.getText());
							String s = sted.getText().toUpperCase();		
							String d = dato.getText();						
							leggTilHare(l, v, s, d); //videresend verdier til funksjonen
						} else { //feil dato
							tekst.setFocusDato(); //setter fokus i tekstfeltet
						} //if(okDato)
					} else { //feil sted
						tekst.setFocusSted();
					} //if(okSted)
				} else { //feil vekt
					tekst.setFocusVekt();
				} //if (okVekt)			
			} else { // feil lengde
				tekst.setFocusLengde();
			} //if(okLengde)
		} //sjekkHare

		public void leggTilHare(double l, double v, String s, String d) {
			char valgtKjonn = kjonn.getKjonn(); //hent kjønnet som ble valgt
			char valgtType = type.getType(); //hent typen som ble valgt
			String valgtFarge = farge.getFarge(); //hent fargen som ble valgt
			String tittel = "Lagre Hare";
			try {
				boolean sjekk = kontroll.lagreHare(valgtKjonn, l, v, s, d, valgtType, valgtFarge);		
				if (sjekk) { //hvis haren ble lagret...
					Kontroll.lagMessageDialog( "Haren ble lagret", tittel, 1); //1 er INFORMATION_MESSAGE			
				} //if (sjekk)
			} catch (NullPointerException e) { //ingen aktiv databasetilkobling
				Kontroll.lagMessageDialog("Databasetilkobling er ikke aktiv.\nDu trenger en aktiv tilkobling for å lagre fangst.\nBruk \"Koble til databasen.\"",
						tittel, 0);
			} catch (Exception e){
				Kontroll.lagMessageDialog("Haren ble ikke lagret. Feilen som oppsto var:\n" + e, tittel, 0); //0 er ERROR_MESSAGE
			} // try/catch
			setVisible(false); //skjul vinduet
		}	//leggTilHare
	} //NyHare

	/**
	 * Denne klassen tar for seg grensesnittet og mottak av data fra
	 * bruker for registrering av førstegangs-fangst av en gaupe
	 */
	private class NyGaupe extends JDialog {	
		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		private String dyret = "gaupen";  //variabel som sendes til TekstFeltVindu
		private TekstFeltVindu tekst = new TekstFeltVindu(dyret);
		private RadiopanelKjonn kjonn = new RadiopanelKjonn(dyret);				

		public NyGaupe (JFrame forelder) {
			super(forelder, "Registrer fangst av gaupe", true);
			setDefaultCloseOperation(JFrame.HIDE_ON_CLOSE);		
			setLayout(new BorderLayout(5, 5)); //mellomrom mellom panelene
			add(new NyGaupeTekstPanel(), BorderLayout.NORTH); //legg tekstfelt ut på skjerm, øverst
			add(new NyGaupeRadioOmraade(), BorderLayout.CENTER); //legg radiobutton på skjerm, midten
			add(new NyGaupeKnappePanel(), BorderLayout.SOUTH); //legg knapper på skjerm, nederst
			pack(); //tilpasser innhold til vinduet
		} //konstruktør

		private class NyGaupeTekstPanel extends JPanel {
			/**
			 * 
			 */
			private static final long serialVersionUID = 1L;

			public NyGaupeTekstPanel() {
				add(tekst);			
			} //konstruktør
		} //NyGaupeTekstPanel

		private class NyGaupeRadioOmraade extends JPanel {
			/**
			 * 
			 */
			private static final long serialVersionUID = 1L;

			public NyGaupeRadioOmraade() {
				setLayout(new GridLayout(1,1,5,5));
				add(kjonn); //legg til på panelet
			} //konstruktør
		} //NyGaupeRadioOmraade

		private class NyGaupeKnappePanel extends JPanel{
			/**
			 * 
			 */
			private static final long serialVersionUID = 1L;

			public NyGaupeKnappePanel() {			
				NyGaupeKnappeLytter lytt  =new NyGaupeKnappeLytter(); //opprettt lytter
				lagre = new JButton("Lagre"); //lag ny knapp
				lagre.addActionListener(lytt); //knytt lytter til knappen
				lagre.setMnemonic('L');
				add(lagre); //legg knapp i panelet
				avbryt = new JButton("Avbryt");
				avbryt.addActionListener(lytt);
				avbryt.setMnemonic('A');
				add(avbryt);
			} //konstruktør
		} //NyGaupeKnappePanel

		private class NyGaupeKnappeLytter implements ActionListener {
			public void actionPerformed(ActionEvent hendelse) {
				JButton knapp = (JButton) hendelse.getSource();
				if (knapp == lagre) { //sett igang innsetting i tabell/database				
					sjekkGaupe();
				} else { //lukk dette vinduet
					setVisible(false); //skjuler vinduet
				} //if (knapp == lagre)
			} //actionPerformed
		} //NyGaupeKnappeLytter	

		public void sjekkGaupe() {
			JTextField lengde = new JTextField(); //lager et nytt tekstfeltobjekt...
			JTextField vekt = new JTextField();
			JTextField oreLengde = new JTextField();
			JTextField sted = new JTextField();
			JTextField dato = new JTextField();		
			lengde.setText( tekst.getLengde());  //...hent ut orginalobjektets innhold og legg innholdet inn i det nye tekstfeltet...
			vekt.setText( tekst.getVekt());		
			oreLengde.setText( tekst.getOreLengde());
			sted.setText(tekst.getSted());
			dato.setText(tekst.getDato());
			boolean okLengde = Kontroll.sjekkLengde(lengde.getText()); //...som deretter sjekkes om innhold er ok...
			if (okLengde) { //er lengde ok?
				boolean okVekt = Kontroll.sjekkVekt(vekt.getText());
				if (okVekt) { //er vekt ok?
					boolean okOre = Kontroll.sjekkOre(oreLengde.getText());
					if (okOre) { //er ørelengden ok?
						boolean okSted = Kontroll.sjekkSted(sted.getText().toUpperCase());
						if (okSted) { //er sted oppgitt ok?
							boolean okDato = Kontroll.sjekkDato(dato.getText());
							if (okDato) { //er dato ok?
								//alt ok, hent verdier
								double l = Double.parseDouble(lengde.getText());
								double v = Double.parseDouble(vekt.getText());
								String s = sted.getText().toUpperCase();		
								String d = dato.getText();
								double ore = Double.parseDouble(oreLengde.getText());
								leggTilGaupe(l, v, s, d, ore); //videresend verdier til funksjonen
							} else { //feil dato
								tekst.setFocusDato(); //setter fokus i tekstfeltet
							} //if(okDato)
						} else { //feil sted
							tekst.setFocusSted();
						} //if(okSted)
					} else {
						tekst.setFocusOre();
					} //if (okOre)
				} else { //feil vekt
					tekst.setFocusVekt();
				} //if (okVekt)			
			} else { // feil lengde
				tekst.setFocusLengde();
			} //if(okLengde)
		} //sjekkGaupe

		public void leggTilGaupe(double l, double v, String s, String d, double ore) {
			char valgtKjonn = kjonn.getKjonn(); //hent kjønnet som ble valgt
			String tittel = "Lagre Gaupe";
			try {
				boolean sjekk  = kontroll.lagreGaupe(valgtKjonn, l, v, s, d, ore);		
				if (sjekk) { //hvis gaupen ble lagret...
					Kontroll.lagMessageDialog( "Gaupen ble lagret.",tittel, 1); //1 er INFORMATION_MESSAGE		
				} //if (sjekk)
			} catch (NullPointerException e) { //ingen aktiv databasetilkobling
				Kontroll.lagMessageDialog("Databasetilkobling er ikke aktiv.\nDu trenger en aktiv tilkobling for å lagre fangst.\nBruk \"Koble til databasen.\"",
						tittel, 0);
			} catch (Exception e){
				Kontroll.lagMessageDialog("Gaupen ble ikke lagret. Feilen som oppsto var:\n" + e, tittel, 0); //0 er ERROR_MESSAGE
			} // try/catch
			setVisible(false); //skjul vinduet)
		}	//leggTilGaupe
	} //NyGaupe

	/***GJENFANGST (HARE/GAUPE)***/
	/**
	 * Denne klassen tar for seg gjenfangst av harer. Data blir 
	 * sendt til kontroll i Kontroll klassen og deretter videresendt
	 * til databasen dersom alt er ok. Denne henter tekstfelt, labels o.l
	 * fra de samme klassene som de andre GUI - klassene
	 */
	private class GjenfangstHare extends JDialog {
		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		private String dyret = "gjenfangst";  //variabel som sendes til TekstFeltVindu
		private TekstFeltVindu tekst = new TekstFeltVindu(dyret);
		private RadiopanelFarge farge = new RadiopanelFarge();			

		public GjenfangstHare(JFrame forelder) {
			super(forelder, "Registrer gjenfangst av hare", true);
			setDefaultCloseOperation(JFrame.HIDE_ON_CLOSE);		
			setLayout(new BorderLayout(5, 5)); //mellomrom mellom panelene		
			add(new GjenfHareTekstPanel(), BorderLayout.NORTH); //legg tekstfelt ut på skjerm, øverst
			add(new GjenfHareRadioOmrPanel(), BorderLayout.CENTER);
			add(new GjenfHareKnappePanel(), BorderLayout.SOUTH); //legg knapper på skjerm, nederst
			pack();
		} //konstruktør

		private class GjenfHareTekstPanel extends JPanel {
			/**
			 * 
			 */
			private static final long serialVersionUID = 1L;

			public GjenfHareTekstPanel() {
				add(tekst);
			} //konstruktør
		} //GjenfHareTekstPanel	

		private class GjenfHareKnappePanel extends JPanel{
			/**
			 * 
			 */
			private static final long serialVersionUID = 1L;

			public GjenfHareKnappePanel() {			
				GjenfHareKnappeLytter lytt = new GjenfHareKnappeLytter(); //opprettt lytter
				lagre = new JButton("Lagre"); //lag ny knapp
				lagre.addActionListener(lytt); //knytt lytter til knappen
				lagre.setMnemonic('L');
				add(lagre); //legg knapp i panelet
				avbryt = new JButton("Avbryt");
				avbryt.addActionListener(lytt);
				avbryt.setMnemonic('A');
				add(avbryt);
			} //konstruktør
		} //GjenfHareKnappePanel

		private class GjenfHareRadioOmrPanel extends JPanel {
			/**
			 * 
			 */
			private static final long serialVersionUID = 1L;

			public GjenfHareRadioOmrPanel() {   
				setLayout(new GridLayout(1,1,5,5));
				add(farge);
			} //konstruktør
		} //GjenfHareRadioOmrPanel

		private class GjenfHareKnappeLytter implements ActionListener {
			public void actionPerformed(ActionEvent hendelse) {
				JButton knapp = (JButton) hendelse.getSource(); //hent kilden til knappen som ble trykket
				if (knapp == lagre) { //sett igang innsetting i tabell/database				
					sjekkHareGjenfangst();
				} else { //lukk dette vinduet								
					setVisible(false); //skjuler vinduet
				} //if (knapp == lagre)
			} //actionPerformed
		} //GjenfHareKnappeLytter		

		public void sjekkHareGjenfangst() {
			char type = 'H';
			JTextField idDyr = new JTextField(); //lager et nytt tekstfeltobjekt...
			JTextField lengde = new JTextField();
			JTextField vekt = new JTextField();
			JTextField sted = new JTextField();
			JTextField dato = new JTextField();
			idDyr.setText(tekst.getIdDyr()); //...hent ut orginalobjektets innhold og legg innholdet inn i det nye tekstfeltet...
			lengde.setText( tekst.getLengde());
			vekt.setText( tekst.getVekt());
			sted.setText(tekst.getSted());
			dato.setText(tekst.getDato());
			boolean okId = Kontroll.sjekkId(idDyr.getText().toUpperCase(), type); //...som deretter sjekkes om innhold er ok...
			if (okId) {
				boolean okLengde = Kontroll.sjekkLengde(lengde.getText());		
				if (okLengde) { //er lengde ok?
					boolean okVekt = Kontroll.sjekkVekt(vekt.getText());
					if (okVekt) { //er vekt ok?
						boolean okSted = Kontroll.sjekkSted(sted.getText());
						if (okSted) { //er sted oppgitt ok?
							boolean okDato = Kontroll.sjekkDato(dato.getText());
							if (okDato) { //er dato ok?
								//alt ok, hent verdier
								String id = idDyr.getText().toUpperCase();
								double l = Double.parseDouble(lengde.getText());
								double v = Double.parseDouble(vekt.getText());
								String s = sted.getText().toUpperCase();						
								String d = dato.getText();							
								leggTilHareGjenfangst(id, l, v, s, d); //videresend verdiene til funksjon
							} else { //feil dato
								tekst.setFocusDato(); //setter fokus i tekstfeltet
							} //if(okDato)
						} else { //feil sted
							tekst.setFocusSted();
						} //if(okSted)
					} else { //feil vekt
						tekst.setFocusVekt();
					} //if (okVekt)			
				} else { // feil lengde
					tekst.setFocusLengde();
				} //if(okLengde)
			} else { //feil id oppgitt
				tekst.setFocusIdDyr();
			} //if (okId)
		} //sjekkHareGjenfangst	

		public void leggTilHareGjenfangst(String id, double l, double v, String s, String d) {
			String valgtFarge = farge.getFarge();
			String tittel = "Lagre gjenfangst";
			try {
				boolean sjekk = kontroll.lagreGjenfangstHare(id, l, v, s, d, valgtFarge);
				if (sjekk) { //hvis gjenfangst ble lagret...					
					Kontroll.lagMessageDialog( "Gjenfangst av hare ble lagret.", tittel, 1); //1 er INFORMATION_MESSAGE	
				} //if (sjekk)
			} catch (NullPointerException e) { //ingen aktiv databasetilkobling
				Kontroll.lagMessageDialog("Databasetilkobling er ikke aktiv.\nDu trenger en aktiv tilkobling for å lagre fangst.\nBruk \"Koble til databasen.\"",
						tittel, 0);
			} catch(Exception e) { //...feil under lagring...
				Kontroll.lagMessageDialog("Gjenfangst av hare ble ikke lagret. Feilen som oppsto var:\n" + e,
						tittel, 0); //0 er ERROR_MESSAGE
			} // try/catch
			setVisible(false); //skjul vinduet
		} //leggTilHareGjenfangst
	} //GjenfangstHare

	/**
	 * Denne klassen tar for seg gjenfangst av gauper. Data blir 
	 * sendt til kontroll i Kontroll klassen og deretter videresendt
	 * til databasen dersom alt er ok. Denne henter tekstfelt, labels o.l
	 * fra de samme klassene som de andre GUI - klassene
	 */
	private class GjenfangstGaupe extends JDialog {
		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		private String dyret = "gjenfangst";  //variabel som sendes til TekstFeltVindu
		private TekstFeltVindu tekst = new TekstFeltVindu(dyret);		

		public GjenfangstGaupe(JFrame forelder) {
			super(forelder, "Registrer gjenfangst av gaupe", true);
			setDefaultCloseOperation(JFrame.HIDE_ON_CLOSE);		
			setLayout(new BorderLayout(5, 5)); //mellomrom mellom panelene		
			add(new GjenfGaupeTekstPanel(), BorderLayout.NORTH); //legg tekstfelt ut på skjerm, øverst
			add(new GjenfGaupeKnappePanel(), BorderLayout.SOUTH); //legg knapper på skjerm, nederst
			pack();
		} //konstruktør

		private class GjenfGaupeTekstPanel extends JPanel {
			/**
			 * 
			 */
			private static final long serialVersionUID = 1L;

			public GjenfGaupeTekstPanel() {			
				add(tekst);
			} //konstruktør
		} //GjenfGaupeTekstPanel	

		private class GjenfGaupeKnappePanel extends JPanel{
			/**
			 * 
			 */
			private static final long serialVersionUID = 1L;

			public GjenfGaupeKnappePanel() {			
				GjenfGaupeKnappeLytter lytt = new GjenfGaupeKnappeLytter(); //opprettt lytter
				lagre = new JButton("Lagre"); //lag ny knapp
				lagre.addActionListener(lytt); //knytt lytter til knappen
				lagre.setMnemonic('L');
				add(lagre); //legg knapp i panelet
				avbryt = new JButton("Avbryt");
				avbryt.addActionListener(lytt);
				avbryt.setMnemonic('A');
				add(avbryt);
			} //konstruktør
		} //GjenfGaupeKnappePanel

		private class GjenfGaupeKnappeLytter implements ActionListener {
			public void actionPerformed(ActionEvent hendelse) {
				JButton knapp = (JButton) hendelse.getSource();
				if (knapp == lagre) { //sett igang innsetting i tabell/database				
					sjekkGaupeGjenfangst();
				} else { //lukk dette vinduet				
					setVisible(false); //skjuler vinduet
				} //if (knapp == lagre)
			} //actionPerformed
		} //GjenfGaupeKnappeLytter	

		public void sjekkGaupeGjenfangst() {
			char type ='G';
			JTextField idDyr = new JTextField(); //lager et nytt tekstfeltobjekt...
			JTextField lengde = new JTextField();
			JTextField vekt = new JTextField();
			JTextField sted = new JTextField();
			JTextField dato = new JTextField();
			idDyr.setText(tekst.getIdDyr()); //...hent ut orginalobjektets innhold og legg innholdet inn i det nye tekstfeltet...
			lengde.setText( tekst.getLengde());
			vekt.setText( tekst.getVekt());
			sted.setText(tekst.getSted());
			dato.setText(tekst.getDato());
			boolean okId = Kontroll.sjekkId(idDyr.getText().toUpperCase(), type); //...som deretter sjekkes om innhold er ok...
			if (okId) { //er id ok?
				boolean okLengde = Kontroll.sjekkLengde(lengde.getText());		
				if (okLengde) { //er lengde ok?
					boolean okVekt = Kontroll.sjekkVekt(vekt.getText());
					if (okVekt) { //er vekt ok?
						boolean okSted = Kontroll.sjekkSted(sted.getText().toUpperCase());
						if (okSted) { //er sted oppgitt ok?
							boolean okDato = Kontroll.sjekkDato(dato.getText());
							if (okDato) { //er dato ok?
								//alt ok, hent verdier
								String id = idDyr.getText().toUpperCase();
								double l = Double.parseDouble(lengde.getText());
								double v = Double.parseDouble(vekt.getText());
								String s = sted.getText().toUpperCase();						
								String d = dato.getText();
								leggTilGaupeGjenfangst(id, l, v, s, d); //videresend verdiene til funksjon
							} else { //feil dato
								tekst.setFocusDato(); //setter fokus i tekstfeltet
							} //if(okDato)
						} else { //feil sted
							tekst.setFocusSted();
						} //if(okSted)
					} else { //feil vekt
						tekst.setFocusVekt();
					} //if (okVekt)			
				} else { // feil lengde
					tekst.setFocusLengde();
				} //if(okLengde)
			} else { //feil id oppgitt
				tekst.setFocusIdDyr();
			} //if (okId)
		} //sjekkGaupeGjenfangst

		public void leggTilGaupeGjenfangst(String id, double l, double v, String s, String d) {		
			String tittel = "Lagre gjenfangst";
			try {
				boolean sjekk = kontroll.lagreGjenfangstGaupe(id, l, v, s, d);		
				if (sjekk) { //hvis gjenfangst ble lagret...					
					Kontroll.lagMessageDialog( "Gjenfangst av gaupen ble lagret.", tittel, 1); //1 er INFORMATION_MESSAGE
				} //if (sjekk)
			} catch (NullPointerException e) { //ingen aktiv databasetilkobling
				Kontroll.lagMessageDialog("Databasetilkobling er ikke aktiv.\nDu trenger en aktiv tilkobling for å lagre fangst.\nBruk \"Koble til databasen.\"",
						tittel, 0);
			} catch (Exception e) { //...feil under lagring...
				Kontroll.lagMessageDialog( "Gjenfangst av hare ble ikke lagret. Feilen som oppsto var:\n" + e,
						tittel, 0); //0 er ERROR_MESSAGE
			} // try/catch
			setVisible(false); //skjul vinduet
		} //leggTilGaupeGjenfangst
	} //GjenfangstGaupe

	/***UTSKRIFT AV RAPPORT FOR FØRSTEGANGSFANGST***/
	/**
	 * Denne klassen fyller en JTable dersom det ligger noe i den.
	 * Har laget funksjon som henter resultat i klassen Database, 
	 * resultatet sendes til klasssen Kontroll, hvor resultatet videresendes
	 * til funksjonen erUtskriftVisbar. Hvis erUtskriftVisbar = true, så blir vinduet vist.
	 */
	private class Utskrift extends JDialog {			

		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;

		public Utskrift(JFrame forelder) {
			super(forelder, "Oversikt over registrerte dyr", true);		
			String tittel = "Vis utskrift";
			setDefaultCloseOperation(JFrame.HIDE_ON_CLOSE); //skjul vinduet når det lukkes
			try {
				JTable dyretabell = kontroll.fyllJTable(); //fyll opp tabellen		
				JScrollPane scroll = new JScrollPane(dyretabell); //Legger tabellen inn i et scrollefelt
				add(scroll,BorderLayout.CENTER); //legger scrollefeltet på skjerm					
				dyretabell.setPreferredScrollableViewportSize(new Dimension(300,200)); //Sett strl på vinduet tabellen vises i
				pack(); //tilpasser innhold til vinduet
			} catch (NullPointerException e) { //ingen aktiv databasetilkobling
				Kontroll.lagMessageDialog("For å vise utskrift, trengs tilkobling til databasen.\nBruk \"Koble til databasen.\"", 
						tittel, 0);
			} catch (Exception e) { //uventet feil
				Kontroll.lagMessageDialog("Kunne ikke å fylle opp tabellen.\n(Feilmelding: " + e + ")",
						tittel, 0);
			} // try/catch
		} //konstruktør

		public boolean erUtskriftVisbar() {
			boolean sjekk = false;			
			try {
				JTable tabell = kontroll.fyllJTable(); //fyller tabellen
				Object tabellVerdi = tabell.getValueAt(0, 0); //finner verdi på 1.plassering i JTable
				if (tabellVerdi == null) { //ligger det noe i JTable?
					Kontroll.lagMessageDialog("Det er ingen fangst registrert, derfor kan ikke utskrift vises.", "Vis utskrift", 2);			
				} else { //det ligger noe i JTable
					sjekk = true;
				} //if (tabellVerdi == null)
			} catch (Exception e) {
				sjekk = false;
			} // try/catch
			return sjekk;
		} //erUtskriftVisbar
	} //Utskrift

	/***FELLES BRUKERGRENSESNITT DELT MELLOM KLASSENE**/
	/***OPPRETTING AV TEKSTFELT***/
	private class TekstFeltVindu extends JPanel {
		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		private JLabel label; //Label (brukes for å beskrive feltene på skjerm
		private JTextField idDyr, vekt, lengde, sted, dato, oreLengde; //tekst felt

		public TekstFeltVindu(String dyret) { //tekstfeltvindu for registrering av dyr		
			if (dyret.equals("gaupen") || dyret.equals("gjenfangst")) { //hvilken klasse kaller på tekstfelt? 
				setLayout(new GridLayout(6,2,5,5)); //gaupe og gjenfangst har flere tekstfelt
			} else {
				setLayout(new GridLayout(5,2,5,5));
			} //if (dyret.equals("gaupen"))

			if (dyret.equals("gjenfangst")) { //legger til gjenfangst på skjerm
				label = new JLabel("Skriv inn dyrets id:", JLabel.LEFT);
				idDyr = new JTextField(10);
				add(label);
				add(idDyr);
			} // if (dyret.equals("gjenfangst"))
			//lag en ny label med tekst, denne legges til venstre når den vises på skjerm
			label  = new JLabel("Skriv inn lengde: ", JLabel.LEFT);
			lengde = new JTextField(10);
			add(label); //legg til label i panelet
			add(lengde); //legg til tekstfeltet i panelet
			label  = new JLabel("Skriv inn vekt: ", JLabel.LEFT);
			vekt = new JTextField(10);		
			add(label);     
			add(vekt);

			if (dyret.equals("gaupen")) { //hvis dyret er en gaupe
				label  = new JLabel("Skriv inn lengden på øret:", JLabel.LEFT); //legg til label
				oreLengde = new JTextField(10); //og tekstfelt for registrering av øre-lengde
				add(label);		
				add(oreLengde);
			} //if (dyret.equals("gaupen"))

			label  = new JLabel("Skriv inn sted: ", JLabel.LEFT);
			sted = new JTextField(10);
			add(label);     
			add(sted);
			label  = new JLabel("Skriv inn dato (DD.MM.YYYY): ", JLabel.LEFT); 
			dato = new JTextField(10);
			add(label);     
			add(dato);        
			label  = new JLabel("NB! Bruk punktum som desimaltegn.", JLabel.LEFT);
			add(label); //legger til en label helt nederst i panelet, ment som veiledning
		} //konstruktør

		/***GET METODER***/
		public String getIdDyr() {
			return idDyr.getText();
		}

		public String getLengde() {
			return lengde.getText();
		}

		public String getVekt() {
			return vekt.getText();
		}

		public String getOreLengde() {
			return oreLengde.getText();
		}

		public String getSted() {
			return sted.getText();
		}

		public String getDato() {
			return dato.getText();
		}
		/***SET FOCUS METODER***/
		public void setFocusIdDyr() {
			idDyr.selectAll(); //marker hele teksten, så bruker kan starte ny innskrivning
			idDyr.requestFocus(); //setter markøren i dette tekstfeltet
		}
		public void setFocusLengde() {
			lengde.selectAll();
			lengde.requestFocus();
		}

		public void setFocusVekt() {
			vekt.selectAll();
			vekt.requestFocus();
		}

		public void setFocusOre() {
			oreLengde.selectAll();
			oreLengde.requestFocus();
		}

		public void setFocusSted() {
			sted.setText("S"); //setter inn 'S' for å forenkle, siden sted må starte på 'S'
			sted.requestFocus();
		}

		public void setFocusDato() {		
			dato.selectAll();
			dato.requestFocus();
		}
	} //TekstFeltVindu

	/***OPPRETTING AV RADIOBUTTONS***/
	private class RadiopanelKjonn extends JPanel {	
		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		private char valgtKjonn = 'M'; //default verdi for radioknapp
		//radiobuttons for dyrets kjønn
		private JRadioButton kjonn1 = new JRadioButton("Male",true);
		private JRadioButton kjonn2 = new JRadioButton("Female",false);

		public RadiopanelKjonn(String dyret) {          			
			RadioknappLytter rLytt = new RadioknappLytter(); //opprett lytter
			ButtonGroup kjonnGruppe = new ButtonGroup(); //opprett gruppe
			kjonnGruppe.add(kjonn1); //legg radiobuttons i gruppe
			kjonnGruppe.add(kjonn2);
			kjonn1.addActionListener(rLytt); //knytt lytter til radiobutton
			add(kjonn1); //legg til radiobutton i panelet
			kjonn2.addActionListener(rLytt);
			add(kjonn2);                        
			//Lager en ramme rundt radioboksen
			SoftBevelBorder ramme = new SoftBevelBorder(BevelBorder.RAISED);
			Border gruppeboks = BorderFactory.createTitledBorder(ramme,"Hvilket kjønn har " + dyret + "?");
			setBorder(gruppeboks);
		} //konstruktør

		private class RadioknappLytter implements ActionListener {
			public void actionPerformed(ActionEvent hendelse) {
				String valgt  = hendelse.getActionCommand();
				if (valgt.equals("Male")) { //er dyrets kjønn mann...
					valgtKjonn = 'M';
				} else { //...eller kvinne
					valgtKjonn = 'F';
				} //if (valgt.equals("Male"))
			} //actionPerformed
		} //RadioknappLytter

		public char getKjonn() {
			return valgtKjonn;
		} //getKjonn
	} //RadiopanelKjonn

	private class RadiopanelType extends JPanel {      
		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		private char valgtType = 'S'; //default verdi for radioknapp
		//radiobuttons for harens type 
		private JRadioButton type1 = new JRadioButton("Sørlig",true);
		private JRadioButton type2 = new JRadioButton("Vanlig",false);

		public RadiopanelType() {          
			RadioknappLytter rLytt = new RadioknappLytter();
			ButtonGroup typeGruppe = new ButtonGroup();
			typeGruppe.add(type1);
			typeGruppe.add(type2);
			add(type1);
			type1.addActionListener(rLytt);
			add(type2);                     
			type2.addActionListener(rLytt);
			SoftBevelBorder ramme = new SoftBevelBorder(BevelBorder.RAISED);
			Border gruppeboks = BorderFactory.createTitledBorder(ramme,"Hvilken haretype er fanget?");
			setBorder(gruppeboks);
		} //konstruktør

		private class RadioknappLytter implements ActionListener {
			public void actionPerformed(ActionEvent hendelse) {
				String valgt  = hendelse.getActionCommand();
				if (valgt.equals("Sørlig")) { //er haren Sørlig eller...
					valgtType = 'S';
				} else { //...vanlig?
					valgtType= 'V';
				} //if (valgt.equals("Sørlig")) {
			} //actionPerformed
		} // RadioknappLytter

		public char getType() {
			return valgtType;
		} //getType
	} //RadiopanelType

	private class RadiopanelFarge extends JPanel {
		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;
		private String valgtFarge = "Hvit";  //default verdi for radioknapp
		//radiobuttons for harens farge
		private JRadioButton farge1 = new JRadioButton("Hvit",true);
		private JRadioButton farge2= new JRadioButton("Brun",false);

		public RadiopanelFarge() {          
			RadioknappLytter rLytt = new RadioknappLytter();
			ButtonGroup fargeGruppe = new ButtonGroup();
			fargeGruppe.add(farge1);
			fargeGruppe.add(farge2);
			add(farge1);
			farge1.addActionListener(rLytt);
			add(farge2);                        
			farge2.addActionListener(rLytt);
			SoftBevelBorder ramme = new SoftBevelBorder(BevelBorder.RAISED);
			Border gruppeboks = BorderFactory.createTitledBorder(ramme,"Hvilken farge har haren?");
			setBorder(gruppeboks);
		} //konstruktør

		private class RadioknappLytter implements ActionListener {
			public void actionPerformed(ActionEvent hendelse) {
				String valgt  = hendelse.getActionCommand();
				if (valgt.equals("Hvit")) { //er haren Hvit eller...
					valgtFarge = "Hvit";
				} else { //...brun?
					valgtFarge = "Brun";
				} //if (valgt.equals("Hvit"))
			} //actionPerformed
		} //RadioknappLytter

		public String getFarge() {
			return valgtFarge;
		} //getFarge
	} //RadiopanelFarge
} //Grensesnitt