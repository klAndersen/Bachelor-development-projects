package oblig_244;
/**
 * Her ligger main-metoden og grensesnittet for hovedmenyen.
 * Hovedmenyen viser alternativene brukeren har.
 */
import java.awt.*;

import javax.swing.*;
import java.awt.event.*;

public class Oblig_Inf244 {	
	public static void main(String[] args) {
		Hovedmeny vindu = new Hovedmeny("Fangstprogram");
		vindu.setSize(400,300); //setter størrelsen
		vindu.setVisible(true); //viser vinduet
	} //main
} //Oblig_Inf244

class Hovedmeny extends JFrame {	
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	private JButton k1,k2,k3,k4,k5,k6;		
	JLabel label;	

	public Hovedmeny (String tittel) {
		setTitle(tittel); //gir vinduet et navn/tittel
		setLayout(new GridLayout(8,1));
		KnappeLytter lytt = new KnappeLytter(); //lag en ny lytter
		label = new JLabel("Velkommen til Fangstprogrammet.", JLabel.CENTER);	
		add(label);
		label = new JLabel("(Snarvei for knapppene er Alt + nr)",JLabel.CENTER);
		add(label);
		k1 = new JButton("1: Ny hare"); //lag ny knapp
		k1.addActionListener(lytt); //knytt lytter til knappen
		k1.setMnemonic('1'); //dersom man trykker Alt + 1 så vises dette vinduet (snarvei)
		add(k1); //legg knappen på skjerm
		k2 = new JButton("2: Ny gaupe");
		k2.addActionListener(lytt);
		k2.setMnemonic('2');
		add(k2);
		k3 = new JButton("3: Harefangst");
		k3.addActionListener(lytt);
		k3.setMnemonic('3');
		add(k3);
		k4 = new JButton("4: Gaupefangst");
		k4.addActionListener(lytt);
		k4.setMnemonic('4');
		add(k4);
		k5 = new JButton("5: Utskrift");
		k5.addActionListener(lytt);
		k5.setMnemonic('5');
		add(k5);
		k6 = new JButton("6: Avslutt programmet");
		k6.addActionListener(lytt);
		k6.setMnemonic('6');
		add(k6);
		pack(); //tilpasser innhold til vinduet
	} //konstruktør

	public NyHare visNyHare() {
		NyHare vindu = new NyHare(this); //oppretter nytt objekt av klassen med dette vinduet som forelder
		return vindu; //returner objektet som ble opprettet
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

	private class KnappeLytter implements ActionListener {
		public void actionPerformed(ActionEvent hendelse) {
			JButton knapp = (JButton) hendelse.getSource();
			//hvilken knapp ble trykket?
			if (knapp == k1) { //ny hare
				NyHare hareVindu = visNyHare(); //setter hareVindu lik verdien fra funksjonen
				hareVindu.setSize(450, 400); //setter størrelsen på vinduet
				hareVindu.setVisible(true); //synliggjør vinduet
			} else if (knapp == k2) { //ny gaupe
				NyGaupe gaupeVindu = visNyGaupe();
				gaupeVindu.setSize(450,300);
				gaupeVindu.setVisible(true);
			} else if (knapp == k3) { //ny hare-gjenfangst
				GjenfangstHare gHareVindu = visHGjenfangst();
				gHareVindu.setSize(450,300);
				gHareVindu.setVisible(true);
			} else if (knapp == k4) { //ny gaupe-gjenfangst
				GjenfangstGaupe gGaupeVindu = visGGjenfangst();
				gGaupeVindu.setSize(450,230);
				gGaupeVindu.setVisible(true);
			} else if (knapp == k5) { //utskrift
				Utskrift rapport = visRapport();
				boolean vis = rapport.erUtskriftVisbar();
				if (vis) { //kan vinduet vises?
					rapport.setVisible(true);
				} //if (vis)
			} else { //avslutt
				System.exit(0); //avslutter programmet
			} //if (knapp == k1)
		} //actionPerformed
	} //KnappeLytter
} //Grensesnitt

/**
 * Denne klassen fyller en JTable dersom det ligger noe i den.
 * Har laget funksjon som henter resultat i klassen Database, 
 * resultatet sendes til klasssen Kontroll, hvor resultatet videresendes
 * til funksjonen erUtskriftVisbar. Hvis erUtskriftVisbar = true, så blir vinduet vist.
 */
class Utskrift extends JDialog {	
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	Kontroll kontroll = new Kontroll();	

	public Utskrift(JFrame forelder) {
		super(forelder, "Oversikt over registrerte dyr", true);		
		setDefaultCloseOperation(JFrame.HIDE_ON_CLOSE); //skjul vinduet når det lukkes
		try {
			JTable dyretabell = kontroll.fyllJTable(); //fyll opp tabellen		
			JScrollPane scroll = new JScrollPane(dyretabell); //Legger tabellen inn i et scrollefelt
			add(scroll,BorderLayout.CENTER); //legger scrollefeltet på skjerm					
			dyretabell.setPreferredScrollableViewportSize(new Dimension(300,200)); //Sett strl på vinduet tabellen vises i
			pack(); //tilpasser innhold til vinduet
		} catch (Exception e) {
			kontroll.lagMessageDialog("Klarte ikke å fylle opp tabellen. Feilmelding:\n" + e,"Feil under fylling", 0);
		} // try/catch
	} //konstruktør

	public boolean erUtskriftVisbar() {
		boolean sjekk = false;
		String feilmelding = "Klarte ikke fylle tabell";
		try {
			JTable tabell = kontroll.fyllJTable(); //fyller tabellen
			Object tabellVerdi = tabell.getValueAt(0, 0); //finner verdi på 1.plassering i JTable
			if (tabellVerdi == null) { //ligger det noe i JTable?
				kontroll.lagMessageDialog("Det er ingen fangst registrert, derfor kan ikke utskrift vises.", "Ingen data", 2);			
			} else if (tabellVerdi != feilmelding) { //det ligger noe i JTable, hva ligger der?
				sjekk = true; //ligger noe annet enn feilmelding, så sett til true så vindu kan vises
			} //if (tabellVerdi == null)
		} catch (Exception e) {
			sjekk = false;
		} // try/catch
		return sjekk;
	} //erUtskriftVisbar
} //Utskrift