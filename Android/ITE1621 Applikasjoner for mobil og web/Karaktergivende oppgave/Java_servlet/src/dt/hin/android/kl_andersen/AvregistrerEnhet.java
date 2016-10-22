package dt.hin.android.kl_andersen;

import java.io.IOException;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import static dt.hin.android.kl_andersen.Konstanter.*;

/**
 * Servlet som avregistrer enhet fra server.
 * @author Knut Lucas Andersen
 */
@SuppressWarnings("serial")
public class AvregistrerEnhet extends ForelderServlet {

	@Override
	protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException {
		String registrationID = hentParametere(request, PARAMETER_REGISTRATION_ID);
		Databehandling.avRegistrerEnhet(registrationID);
		setSuksess(response);
		try {
			//legg ved ekstra attributter for videresending
			request.setAttribute(MELDINGSTYPE, ENHET_GIKK_OFFLINE);
			request.setAttribute(MELDINGS_EMNE, ENHET_GIKK_OFFLINE);
			request.getRequestDispatcher("SendMeldinger").forward(request, response);
		} catch (IOException ex) {
			ex.printStackTrace();
		} //try/catch
	} //doPost
} //AvregistrerEnhet