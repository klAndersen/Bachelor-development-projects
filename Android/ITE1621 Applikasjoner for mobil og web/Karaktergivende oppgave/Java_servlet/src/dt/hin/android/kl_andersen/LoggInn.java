package dt.hin.android.kl_andersen;

import java.io.IOException;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

/**
 * Servlet som tar for seg innlogging av administratorer. <br />
 * Kun de som er innlogget får se innholdet på sidene.
 * @author Knut Lucas Andersen
 */
@SuppressWarnings("serial")
public class LoggInn extends ForelderServlet {
	private static final String PARAMETER_USERNAME = "username";
	private static final String PARAMETER_PASSWORD = "pwd";

	protected void processRequest(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		String defaultVerdi = "";
		String brukernavn = hentParametere(request, PARAMETER_USERNAME, defaultVerdi);
		String passord = hentParametere(request, PARAMETER_PASSWORD, defaultVerdi);
		//er brukernavn og passord skrivet inn?
		if (brukernavn.equals(defaultVerdi)) {
			//brukernavn ikke oppgitt
			String tilbakemelding = "<font color='red'>Brukernavn må oppgis.</font>";
			Databehandling.setTilbakemelding(tilbakemelding);
		} else if (passord.equals(defaultVerdi)) {
			//passord ikke oppgitt
			String tilbakemelding = "<font color='red'>Passord må oppgis.</font>";
			Databehandling.setTilbakemelding(tilbakemelding);
		} else {
			loggInnBruker(request, brukernavn, passord);
		} //if (erTomEllerNull(brukernavn))
		request.getRequestDispatcher("index.jsp").forward(request, response);
	} //processRequest

	private void loggInnBruker(HttpServletRequest request, String brukernavn, String passord) {
		boolean innlogget = false;
		//oppretter nytt session objekt
		String userInfo = Databehandling.getSessionBrukernavn();
		Databehandling.setSessionObjekt(request);
		if (userInfo == null) {
			innlogget = Databehandling.loggInnBruker(brukernavn, passord);
			if (innlogget) {
				//setter at bruker er innlogget og legg ved brukernavn
				Databehandling.setSessionBrukernavn(brukernavn);
			} else {
				//feil under innlogging
				String tilbakemelding = "<font color='red'>Kunne ikke logge deg inn.<br /> Vennligst oppgi brukernavn og passord på nytt.</font>";
				Databehandling.setTilbakemelding(tilbakemelding);
			} //if (innlogget)
		} //if (userInfo == null)
	} //loggInnBruker

	/**
	 * @see HttpServlet#doPost(HttpServletRequest request, HttpServletResponse response)
	 */
	@Override
	protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		processRequest(request, response);
	} //doPost
} //LoggInn