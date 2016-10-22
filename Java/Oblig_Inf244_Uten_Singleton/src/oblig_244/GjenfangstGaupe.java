package oblig_244;
/**
 * Denne klassen tar for seg gjenfangst av gauper. Data blir 
 * sendt til kontroll i Kontroll klassen og deretter videresendt
 * til databasen dersom alt er ok. Denne henter tekstfelt, labels o.l
 * fra de samme klassene som de andre GUI - klassene
 */
import java.awt.*;
import java.awt.event.*;
import javax.swing.*;

class GjenfangstGaupe extends JDialog {
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	private String dyret = "gjenfangst";  //variabel som sendes til TekstFeltVindu
	private TekstFeltVindu tekst = new TekstFeltVindu(dyret);
	private Kontroll kontroll = new Kontroll(); //objekt av Kontroll klassen
	private JButton lagre, avbryt; //knapper

	public GjenfangstGaupe(JFrame forelder) {
		super(forelder, "Registrer gjenfangst av gaupe", true);
		setDefaultCloseOperation(JFrame.HIDE_ON_CLOSE);		
		setLayout(new BorderLayout(5, 5)); //mellomrom mellom panelene		
		add(new TekstfeltPanel(), BorderLayout.NORTH); //legg tekstfelt ut på skjerm, øverst
		add(new KnappePanel(), BorderLayout.SOUTH); //legg knapper på skjerm, nederst
		pack();
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
				sjekkGaupeGjenfangst();
			} else { //lukk dette vinduet				
				setVisible(false); //skjuler vinduet
			} //if (knapp == lagre)
		} //actionPerformed
	} //KnappeLytter	

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
		boolean okId = kontroll.sjekkId(idDyr.getText().toUpperCase(), type); //...som deretter sjekkes om innhold er ok...
		if (okId) { //er id ok?
			boolean okLengde = kontroll.sjekkLengde(lengde.getText());		
			if (okLengde) { //er lengde ok?
				boolean okVekt = kontroll.sjekkVekt(vekt.getText());
				if (okVekt) { //er vekt ok?
					boolean okSted = kontroll.sjekkSted(sted.getText().toUpperCase());
					if (okSted) { //er sted oppgitt ok?
						boolean okDato = kontroll.sjekkDato(dato.getText());
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
		String tilbakemelding = kontroll.lagreGjenfangstGaupe(id, l, v, s, d);		
		if (tilbakemelding.equals("Gjenfangst av gaupe ble lagret.")) { //hvis gjenfangst ble lagret...					
			kontroll.lagMessageDialog( tilbakemelding,"Lagring av gjenfangst", 1); //1 er INFORMATION_MESSAGE			
		} else { //...feil under lagring...
			kontroll.lagMessageDialog( tilbakemelding,"Lagring av gjenfangst", 0); //0 er ERROR_MESSAGE
		} //if (tilbakemelding.equals("Gjenfangst av gaupe ble lagret."))
		setVisible(false); //skjul vinduet
	} //leggTilGaupeGjenfangst
} //GjenfangstGaupe