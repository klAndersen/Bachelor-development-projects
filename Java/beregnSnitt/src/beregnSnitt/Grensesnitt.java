package beregnSnitt;

/**
 * BeregnSnitt regner ut karaktersnitt iforhold til høgskole karakterer. Her opprettes det grafiske grensesnittet.
 * Copyright (C) 2011  Knut Lucas Andersen
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

import static javax.swing.JOptionPane.*;
import java.awt.*;
import java.awt.event.*;
import javax.swing.*;

@SuppressWarnings("serial")
public class Grensesnitt extends JFrame {	
	private JLabel label;
	private JTextField skrivA, skrivB, skrivC, skrivD, skrivE, skrivSum, skrivStp;
	private JTextField [] karTabell, stpTabell; //tabell for oppretting av tekstfelt
	private JButton ok, avslutt, regnSnitt, about;
	private int antall = 0; //teller for antall "særegne" karakterer

	public Grensesnitt(String tittel) {
		setTitle(tittel); //oppretter tittel
		setLayout(new GridLayout(9,2)); //setter layout/utseende
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE); //programmet avsluttes hvis lukket
		KnappeLytter lytt = new KnappeLytter(); //lytter for knapper
		FocusText focus = new FocusText(); //lytter for focus

		label = new JLabel("Skriv inn antall karakter A:", JLabel.LEFT);
		add(label); //legger til label
		skrivA = new JTextField(10); //oppretter nytt tekstfelt
		skrivA.setText("0"); //innholdet i tekstfeltet er null
		skrivA.addFocusListener(focus); //legger til focus lytter
		skrivA.setToolTipText("Skriv inn antallet du har av karakter 'A'");
		add(skrivA); //legger til tekstfeltet

		label = new JLabel("Skriv inn antall karakter B:", JLabel.LEFT);
		add(label);
		skrivB = new JTextField(10);
		skrivB.setText("0");
		skrivB.addFocusListener(focus);
		add(skrivB);

		label = new JLabel("Skriv inn antall karakter C:", JLabel.LEFT);
		add(label);
		skrivC = new JTextField(10);
		skrivC.setText("0");
		skrivC.addFocusListener(focus);
		add(skrivC);

		label = new JLabel("Skriv inn antall karakter D:", JLabel.LEFT);
		add(label);
		skrivD = new JTextField(10);
		skrivD.setText("0");
		skrivD.addFocusListener(focus);
		add(skrivD);

		label = new JLabel("Skriv inn antall karakter E:", JLabel.LEFT);
		add(label);
		skrivE = new JTextField(10);
		skrivE.setText("0");
		skrivE.addFocusListener(focus);
		add(skrivE);		

		label = new JLabel("Skriv inn studiepoeng for emnene (kun en verdi):", JLabel.LEFT);
		label.setToolTipText("Skriv inn studiepoenget som gjelder for hvert emne. " +
		"Hvis noen har en annen \"sum\" (høyere/lavere), bruk 'Legg til seperate karakterer'");
		add(label);
		skrivStp = new JTextField(10);
		skrivStp.setText("0");
		skrivStp.addFocusListener(focus);
		add(skrivStp);

		label = new JLabel("Skriv inn summen av studiepoengene:", JLabel.LEFT);
		label.setToolTipText("Skriv inn studiepoengene du har totalt (eks: Bachelor = 180)");
		add(label);
		skrivSum = new JTextField(10);
		skrivSum.setText("0");		
		skrivSum.addFocusListener(focus);
		add(skrivSum);

		regnSnitt = new JButton("Legg til seperate karakterer");
		regnSnitt.addActionListener(lytt);
		regnSnitt.setToolTipText("Dersom du har karakterer med andre studiepoeng enn oppgitt, kan du legge til de her");
		regnSnitt.setMnemonic('L');
		add(regnSnitt);		

		ok = new JButton("Regn ut snitt");
		ok.addActionListener(lytt);
		ok.setToolTipText("Regner ut snittet basert på informasjonen i tekstfeltene");
		ok.setMnemonic('R');
		add(ok);

		avslutt = new JButton("Avslutt programmet");		
		avslutt.addActionListener(lytt);
		avslutt.setToolTipText("Avslutter programmet");
		avslutt.setMnemonic('P');		
		add(avslutt);

		about = new JButton("About");
		about.addActionListener(lytt);
		about.setMnemonic('A');		
		add(about);
		pack(); //tilpass vinduet
	} //konstruktør

	private class LagTekstfelt extends JDialog {
		private JButton regnUt;

		public LagTekstfelt() {
			setTitle("Beregning av seperate karakterer");
			antall = getAntall(); //henter antallet
			int strl = (antall * 2) + 1; //setter størrelsen			 
			setLayout(new GridLayout(strl, 2)); //setter layout basert på størrelse
			KnappeLytter lytt = new KnappeLytter();			
			//setter størrelsen på tabellene
			karTabell = new JTextField[antall];
			stpTabell = new JTextField[antall];
			//legger ut tekstfelt og labels
			for (int i = 0; i < antall; i++) {
				//gitt karakter
				label = new JLabel("Skriv inn karakter:", JLabel.LEFT);
				label.setToolTipText("Skriv inn karakteren (A - E)");
				add(label);
				karTabell[i] = new JTextField(10); //lager nytt tekstfelt på plass i				
				add(karTabell[i]); //legger ut tekstfeltet på plass i
				//studiepoeng for gitt karakter
				label = new JLabel("Skriv inn studiepoeng:", JLabel.LEFT);
				label.setToolTipText("Skriv inn studiepoengene til oppgitt karakter");
				add(label);
				stpTabell[i] = new JTextField(10);
				add(stpTabell[i]);
			} //for

			regnUt = new JButton("Regn ut snitt");
			regnUt.setMnemonic('R');
			regnUt.addActionListener(lytt);
			regnUt.setToolTipText("Regn ut snittet basert på informasjon skrevet inn her og hovedskjema");
			add(regnUt);
			pack();
		} //konstruktør

		private class KnappeLytter implements ActionListener {
			public void actionPerformed(ActionEvent e) {
				JButton knapp = (JButton) e.getSource();
				if (knapp == regnUt) {
					regnUtTekstfelt(false); //regn ut snitt med tilleggskarakterer
				} //if (knapp == regnUt)
			} //actionPerformed
		} //KnappeLytter
	} //LagTekstfelt

	private class KnappeLytter implements ActionListener {
		public void actionPerformed (ActionEvent e) {
			JButton knapp = (JButton) e.getSource();
			if (knapp == ok) { //skal regne ut kun tekstfeltenes verdi			
				regnUtTekstfelt(true); 
			} else if (knapp == regnSnitt) { //regner ut verdier fra tekstfelt og input
				setAntall(0); //nullstiller antall
				//ber om antall
				String skrivAntall = showInputDialog("Skriv inn antall som skal registreres:");
				if (skrivAntall != null) { //er Ok trykket?
					leggTilKarakterer(skrivAntall);
				} //if (settAntall != null)
			} else if (knapp == about) { //skal About vises?
				String aboutProg;
				aboutProg = "Programmet beregner snittet av høgskole-karakterene.";
				aboutProg += "\nFor hjelp hold musepeker over knappen eller beskrivelsen til tilhørende tekstfelt.";
				aboutProg += "\n\nBeregnSnitt  Copyright (C) 2011  Knut Lucas Andersen.";
				aboutProg += "\nThis program comes with ABSOLUTELY NO WARRANTY; \nSee Terms and Conditions §15-§17";
				aboutProg += "\nThis is free software, and you are welcome to redistribute it under certain conditions;"
							 + "\nSee Terms and Conditions. ";
				showMessageDialog(null, aboutProg,"About", 1);
			} else { //avslutt programmet
				System.exit(0); 
			} //if (knapp == ok)
		} //actionPerformed
	} //KnappeLytter

	private class FocusText implements FocusListener {
		@Override
		public void focusGained(FocusEvent e) { 
			JTextField focus = (JTextField) e.getSource();
			focus.selectAll();
		} //focusGained

		@Override
		public void focusLost(FocusEvent e) {
			// TODO Auto-generated method stub			
		} //focusLost
	} //FocusText

	public int getAntall() {
		return antall;
	} //getAntall

	public void setAntall(int antall) {
		this.antall = antall;
	} //setAntall

	public void leggTilKarakterer(String skrivAntall) {
		try {
			int antall = Integer.parseInt(skrivAntall); //forsøk å gjøre om til et heltall
			if (antall > 0 && antall < 11) { //er tallet innskrevet større enn 0 og mindre enn 11?
				setAntall(antall); //sett antallet som skal legges ut
				LagTekstfelt felt = new LagTekstfelt();					
				int y = antall * 90; //setter y basert på antall felt som skal vises
				felt.setSize(500, y); //setter størrelse
				felt.setLocationRelativeTo(null);
				felt.setVisible(true); //viser felt
			} else { //tall innskrevet er 0 eller lavere
				showMessageDialog(null, "Antall ekstra karakterer som skal beregnes må være større enn 0,\n" +
						"men antallet kan ikke overstige 10.", "Antall", 2);
			} //if (antall > 0)
		} catch (NumberFormatException e ) {
			showMessageDialog(null, "Du må skrive inn antall for å beregne ekstra karakterer.", "Antall",2);
		} catch (Exception e){
			showMessageDialog(null, "En feil oppsto. Feilen er: " + e, "Feil", 0);
		} //try/catch
	} //leggTilKarakterer

	/**
	 * Regner ut snitt basert på informasjonen lagt inn i tekstfeltene.
	 */
	private void regnUtTekstfelt(boolean kunTekst) {
		try {						
			boolean tilbakemelding = false;
			//er noe innskrevet i feltene?
			if (!(skrivA.getText().equals("") || skrivB.getText().equals("") || skrivC.getText().equals("") 
					|| skrivD.getText().equals("") || skrivE.getText().equals("") || 
					skrivStp.getText().equals("") || skrivSum.getText().equals(""))) {
				//hent ut verdien fra teksten og forsøk gjøre om til tall
				int antA = Integer.parseInt(skrivA.getText()); 
				int antB = Integer.parseInt(skrivB.getText());
				int antC = Integer.parseInt(skrivC.getText());
				int antD = Integer.parseInt(skrivD.getText());
				int antE = Integer.parseInt(skrivE.getText());
				double stpVerdi = Double.parseDouble(skrivStp.getText());
				double sumStp = Double.parseDouble(skrivSum.getText());			
				Karaktersnitt.setStp(0); //sett poeng til null
				//regn ut snittet
				if (kunTekst) { //regner ut kun fra tekstfelt
					Karaktersnitt.beregnSnitt(antA, antB, antC, antD, antE, stpVerdi, sumStp, 0, '0', 0);	
					tilbakemelding = true; //tilbakemelding skal vises
				} else { //regner ut tekstfelt og input
					tilbakemelding = regnUtTillegg(antA, antB, antC, antD, antE, stpVerdi, sumStp);					
				} //if (kunTekst)		
				if (tilbakemelding == true) {
					if (Karaktersnitt.getStp() > 0) { //er snitt utregnet?
						//gi tilbakemelding
						showMessageDialog(null, "Ditt snitt er: " + Karaktersnitt.getStp(), "Karaktersnitt", 1);
					} else { // snitt ble ikke beregnet
						showMessageDialog(null, "Kunne ikke beregne snitt.", "Karaktersnitt", 1);
					}//if (RegnUt.getStp() > 0)
				} //if (tilbakemelding == true)
			} else {
				showMessageDialog(null, "Et eller flere tekstfelt er blanke.", "Tomt felt", 2);
				skrivA.selectAll(); //markerer teksten i tekstfeltet
				skrivA.requestFocus(); //setter focus
			} //if (!(skrivA.getText().equals("")))
		} catch (NumberFormatException e) {
			showMessageDialog(null, "Karakterene som skrives inn må være et tall,\n" +
					"og studiepoeng må skilles ad med punktum\n" +
					"(hvis desimaltall).", "Ugyldig verdi", 0);
		} catch (Exception e) {
			showMessageDialog(null, "En feil oppsto. Feilen er: " + e, "Feil", 0);
		} //try/catch
	} //regnUtTekstfelt

	/**
	 * Regner ut snitt basert på informasjonen som skrives inn
	 * i tekstfelt og i input boksene.
	 */
	private boolean regnUtTillegg(int antA, int antB, int antC, int antD, int antE, double stp, double sumStp) {
		try {			
			int antall = getAntall(); //hent ut antallet
			for (int i = 0; i < karTabell.length; i++) {
				String karakter = karTabell[i].getText().toUpperCase(); //gjør om innhold til store bokstaver				
				char kar = karakter.charAt(0); //henter ut første tegn av det som ble skrevet inn
				if (kar == 'A' || kar == 'B' || kar == 'C' || kar == 'D' || kar == 'E' ) { //er korrekt karakter innskrevet?
					String skrivStp = stpTabell[i].getText(); //hent ut studiepoeng til gitt karakter
					double stp2 = Double.parseDouble(skrivStp); //forsøk å gjøre om til desimaltall
					antall--; //senker antall siden denne brukes i RegnUt.beregnSnitt
					Karaktersnitt.beregnSnitt(antA, antB, antC, antD, antE, stp, sumStp, antall, kar, stp2);					
				} else { //ikke en bokstav mellom A - E som er innskrevet
					showMessageDialog(null, "Karakter må være bokstav mellom A-E.", "Karakter", 2);
				} //if (kar == 'A')
			} //for
			return true; //snittet ble regnet ut
		} catch (StringIndexOutOfBoundsException e) { //feltene er blanke
			showMessageDialog(null, "Et eller flere felt er blanke.", "Tomt felt", 2);
			karTabell[0].selectAll(); //markerer teksten
			karTabell[0].requestFocus(); //setter focus
		} catch (NumberFormatException e) { //ikke innskrevet tall
			showMessageDialog(null, "Verdiene som skrives inn må være et tall,\n" +
					"og desimaltall må skilles ad med punktum.", "Ugyldig verdi", 0);
		} catch (Exception e) {
			showMessageDialog(null, "En feil oppsto. Feilen er: " + e, "Feil", 0);
		} //try/catch
		return false;
	} //regnUtTillegg
} //Grensesnitt