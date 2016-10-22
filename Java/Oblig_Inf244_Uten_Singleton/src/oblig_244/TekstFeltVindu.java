package oblig_244;
/**
 * Her ligger mesteparten av de GUI - relaterte feltene som de 
 * forskjellige klassene benytter seg av; tekstfelt, labels, radiobuttons, osv
 */
import java.awt.*;
import java.awt.event.*;
import javax.swing.*;
import javax.swing.border.*;

class TekstFeltVindu extends JPanel {
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	private JLabel label; //Label (brukes for å beskrive feltene på skjerm
	private JTextField idDyr, vekt, lengde, sted, dato, oreLengde; //tekstfelt

	public TekstFeltVindu(String dyret) {		
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

class RadiopanelKjonn extends JPanel {	
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

class RadiopanelType extends JPanel {      
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

class RadiopanelFarge extends JPanel {
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