package oblig_244;
/*
 * Denne klassen tar for seg grensesnittet og mottak av data fra
 * bruker for registrering av f�rstegangs-fangst av en gaupe
 */
import java.awt.*;
import javax.swing.*;
import java.awt.event.*;

public class NyGaupe extends JDialog {	
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	private String dyret = "gaupen";  //variabel som sendes til TekstFeltVindu
	private Kontroll kontroll = new Kontroll(); //objekt av Kontroll klassen
	private TekstFeltVindu tekst = new TekstFeltVindu(dyret);
	private RadiopanelKjonn kjonn = new RadiopanelKjonn(dyret);		
	private JButton lagre, avbryt; //knapper

	public NyGaupe (JFrame forelder) {
		super(forelder, "Registrer fangst av gaupe", true);
		setDefaultCloseOperation(JFrame.HIDE_ON_CLOSE);		
		setLayout(new BorderLayout(5, 5)); //mellomrom mellom panelene
		add(new TekstfeltPanel(), BorderLayout.NORTH); //legg tekstfelt ut p� skjerm, �verst
		add(new RadioOmraadePanel(), BorderLayout.CENTER); //legg radiobutton p� skjerm, midten
		add(new KnappePanel(), BorderLayout.SOUTH); //legg knapper p� skjerm, nederst
		pack(); //tilpasser innhold til vinduet
	} //konstrukt�r

	private class TekstfeltPanel extends JPanel {
		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;

		public TekstfeltPanel() {
			add(tekst);			
		} //konstrukt�r
	} //TekstfeltPanel

	private class RadioOmraadePanel extends JPanel {
		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;

		public RadioOmraadePanel() {
			setLayout(new GridLayout(1,1,5,5));
			add(kjonn); //legg til p� panelet
		} //konstrukt�r
	} //RadioOmraadePanel

	private class KnappePanel extends JPanel{
		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;

		public KnappePanel() {			
			KnappeLytter lytt  =new KnappeLytter(); //opprettt lytter
			lagre = new JButton("Lagre"); //lag ny knapp
			lagre.addActionListener(lytt); //knytt lytter til knappen
			lagre.setMnemonic('L');
			add(lagre); //legg knapp i panelet
			avbryt = new JButton("Avbryt");
			avbryt.addActionListener(lytt);
			avbryt.setMnemonic('A');
			add(avbryt);
		} //konstrukt�r
	} //KnappePanel

	private class KnappeLytter implements ActionListener {
		public void actionPerformed(ActionEvent hendelse) {
			JButton knapp = (JButton) hendelse.getSource();
			if (knapp == lagre) { //sett igang innsetting i tabell/database				
				sjekkGaupe();
			} else { //lukk dette vinduet
				setVisible(false); //skjuler vinduet
			} //if (knapp == lagre)
		} //actionPerformed
	} //KnappeLytter	

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
		boolean okLengde = kontroll.sjekkLengde(lengde.getText()); //...som deretter sjekkes om innhold er ok...
		if (okLengde) { //er lengde ok?
			boolean okVekt = kontroll.sjekkVekt(vekt.getText());
			if (okVekt) { //er vekt ok?
				boolean okOre = kontroll.sjekkOre(oreLengde.getText());
				if (okOre) { //er �relengden ok?
					boolean okSted = kontroll.sjekkSted(sted.getText().toUpperCase());
					if (okSted) { //er sted oppgitt ok?
						boolean okDato = kontroll.sjekkDato(dato.getText());
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
		char valgtKjonn = kjonn.getKjonn(); //hent kj�nnet som ble valgt
		String tilbakemelding = kontroll.lagreGaupe(valgtKjonn, l, v, s, d, ore);		
		if (tilbakemelding.equals("Gaupen ble lagret.")) { //hvis haren ble lagret...
			kontroll.lagMessageDialog( tilbakemelding,"Lagring av ny gaupe", 1); //1 er INFORMATION_MESSAGE			
		} else { //...feil under lagring...
			kontroll.lagMessageDialog( tilbakemelding,"Lagring av ny gaupe", 0); //0 er ERROR_MESSAGE
		} //if (tilbakemelding.equals("Gaupen ble lagret."))
		setVisible(false); //skjul vinduet)
	}	//leggTilGaupe
} //NyGaupe