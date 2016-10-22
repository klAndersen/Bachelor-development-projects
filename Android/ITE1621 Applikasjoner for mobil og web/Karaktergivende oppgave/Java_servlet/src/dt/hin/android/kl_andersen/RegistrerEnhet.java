package dt.hin.android.kl_andersen;

import java.io.IOException;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import static dt.hin.android.kl_andersen.Konstanter.*;

/**
 * Servlet som registrerer en enhet på server.
 * @author Knut Lucas Andersen
 */
@SuppressWarnings("serial")
public class RegistrerEnhet extends ForelderServlet {

	/**
	 * @see HttpServlet#doPost(HttpServletRequest request, HttpServletResponse response)
	 */
	@Override
	protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException {
		String registrationID = hentParametere(request, PARAMETER_REGISTRATION_ID);
		String enhetsNavn = hentParametere(request, PARAMETER_ENHETSNAVN);
		String latitude = hentParametere(request, PARAMETER_LATITUDE);
		String longitude = hentParametere(request, PARAMETER_LONGITUDE);
		//registrer enhet med oversendte parametere
		Databehandling.registrerEnhet(registrationID, enhetsNavn, latitude, longitude);
		setSuksess(response);
		try {
			//skal registrert enhets posisjon sendes?
			if (!latitude.equals(IKKE_SEND_POSISJON) && !longitude.equals(IKKE_SEND_POSISJON)) {
				//legg ved ekstra attributter for utsending av koordinater
				request.setAttribute(MELDINGSTYPE, SEND_KOORDINATER_MELDING);
				request.setAttribute(MELDINGS_EMNE, SEND_KOORDINATER_MELDING);
				request.getRequestDispatcher("SendMeldinger").forward(request, response);
			} //if (!latitude.equals(IKKE_SEND_POSISJON) && !longitude.equals(IKKE_SEND_POSISJON))
		} catch (IOException ex) {
			ex.printStackTrace();
		} //try/catch
	} //doPost	
} //RegistrerEnhet