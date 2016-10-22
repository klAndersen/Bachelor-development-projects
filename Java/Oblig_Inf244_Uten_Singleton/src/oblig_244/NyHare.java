package oblig_244;
/**
 * Denne klassen tar for seg grensesnittet og mottak av data fra
 * bruker for registrering av førstegangs-fangst av en hare
 */
import java.awt.*;
import javax.swing.*;
import java.awt.event.*;

public class NyHare extends JDialog {	
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	private String dyret = "haren"; //variabel som sendes til TekstFeltVindu
	private TekstFeltVindu tekst = new TekstFeltVindu(dyret);
	private Kontroll kontroll = new Kontroll(); //objekt av Kontroll klassen
	private RadiopanelKjonn kjonn = new RadiopanelKjonn(dyret);
	private RadiopanelType type = new RadiopanelType();
	private RadiopanelFarge farge = new RadiopanelFarge();	
	private JButton lagre, avbryt; //knapper	

	public NyHare (JFrame forelder) {
		super(forelder, "Registrer fangst av hare", true);		
		setDefaultCloseOperation(JFrame.HIDE_ON_CLOSE);				
		setLayout(new BorderLayout(5, 5)); //mellomrom mellom panelene
		add(new TekstfeltPanel(), BorderLayout.NORTH);
		add(new RadioOmraadePanel(), BorderLayout.CENTER);
		add(new KnappePanel(), BorderLayout.SOUTH);
		pack(); //tilpasser innhold til vinduet
	} //konstruktør

	private class TekstfeltPanel extends JPanel {
		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;

		public TekstfeltPanel() {
			add(tekst);
		} //konstruktør
	} //TekstfeltPanel

	private class RadioOmraadePanel extends JPanel {
		/**
		 * 
		 */
		private static final long serialVersionUID = 1L;

		public RadioOmraadePanel() {
			setLayout(new GridLayout(3,1,5,5));
			add(kjonn); //legg til på panelet
			add(type);
			add(farge);
		} //konstruktør
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
		} //konstruktør
	} //KnappePanel

	private class KnappeLytter implements ActionListener {
		public void actionPerformed(ActionEvent hendelse) {
			JButton knapp = (JButton) hendelse.getSource();
			if (knapp == lagre) { //sett igang innsetting i tabell/database				
				sjekkHare();
			} else { //lukk dette vinduet
				setVisible(false); //skjuler vinduet
			} //if (knapp == lagre)
		} //actionPerformed
	} //KnappeLytter	

	public void sjekkHare() {		
		JTextField lengde = new JTextField(); //lager et nytt tekstfeltobjekt...
		JTextField vekt = new JTextField();
		JTextField sted = new JTextField();
		JTextField dato = new JTextField();
		lengde.setText( tekst.getLengde());  //...hent ut orginalobjektets innhold og legg innholdet inn i det nye tekstfeltet...
		vekt.setText( tekst.getVekt());		
		sted.setText(tekst.getSted());
		dato.setText(tekst.getDato());
		boolean okLengde = kontroll.sjekkLengde(lengde.getText()); //...som deretter sjekkes om innhold er ok...
		if (okLengde) { //er lengde ok?
			boolean okVekt = kontroll.sjekkVekt(vekt.getText());
			if (okVekt) { //er vekt ok?
				boolean okSted = kontroll.sjekkSted(sted.getText().toUpperCase());
				if (okSted) { //er sted oppgitt ok?
					boolean okDato = kontroll.sjekkDato(dato.getText());
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
		String tilbakemelding = kontroll.lagreHare(valgtKjonn, l, v, s, d, valgtType, valgtFarge);		
		if (tilbakemelding.equals("Haren ble lagret.")) { //hvis haren ble lagret...
			kontroll.lagMessageDialog( tilbakemelding,"Lagring av ny hare", 1); //1 er INFORMATION_MESSAGE			
		} else { //...feil under lagring...
			kontroll.lagMessageDialog( tilbakemelding,"Lagring av ny hare", 0); //0 er ERROR_MESSAGE
		} //if (tilbakemelding.equals("Haren ble lagret."))
		setVisible(false); //skjul vinduet
	}	//leggTilHare
} //NyHare