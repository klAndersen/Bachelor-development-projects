package beregnSnitt;

/**
 * BeregnSnitt regner ut karaktersnitt iforhold til høgskole karakterer. Oppstart skjer her.
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

public class StartProgram {

	public static void main(String [] args) {
		Grensesnitt gui = new Grensesnitt("Beregn karaktersnitt");
		gui.setSize(600,300);
		gui.setLocationRelativeTo(null); //setter vindu i senter av skjermen 
		gui.setVisible(true);		
	} //main
} //BeregnKaraktersnitt