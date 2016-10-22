package dt.hin.android.kl_andersen;

import java.io.IOException;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import static dt.hin.android.kl_andersen.Konstanter.*;

/**
 * Servlet som mottar posisjonskoordinater fra klient, prosesserer de <br />
 * og videresender koordinater til andre enheter som er online.
 * @author Knut Lucas Andersen
 */
@SuppressWarnings("serial")
public class MottaMeldingFraEnhet extends ForelderServlet {

	/**
	 * @see HttpServlet#doPost(HttpServletRequest request, HttpServletResponse response)
	 */
	@Override
	protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		//hent ut parameter for om enhet gikk offline
		String enhetOffline = hentParametere(request, ENHET_GIKK_OFFLINE, "");
		String registrationID = hentParametere(request, PARAMETER_REGISTRATION_ID);
		/**
		 * Bruker kan velge å fortsette å motta posisjonsoppdateringer i bakgrunnen, selv om applikasjonen avsluttes.
		 * Men, for å unngå at andre enheter som er online skal slippe ressursbruk (bl.a. tegning på kart), så vil 
		 * enhet ved avslutning av applikasjon sende ut en offline melding for å fjerne gitt brukers rute fra andres kart.
		 * Det er dette det her sjekkes etter, om det er en offline melding, eller utsending av nye koordinater. 
		 */
		if (enhetOffline.equals("")) {
			//hent ut oversendte koordinater
			String enhetsNavn = hentParametere(request, PARAMETER_ENHETSNAVN);
			String latitude = hentParametere(request, PARAMETER_LATITUDE);
			String longitude = hentParametere(request, PARAMETER_LONGITUDE);
			//oppdater enhets lokasjon
			Databehandling.oppdaterKoordinater(registrationID, enhetsNavn, latitude, longitude);
			videresendKoordinater(request, response, latitude, longitude);
		} else {
			//legg ved ekstra attributter for videresending
			request.setAttribute(MELDINGSTYPE, ENHET_GIKK_OFFLINE);
			request.setAttribute(MELDINGS_EMNE, ENHET_GIKK_OFFLINE);
			Databehandling.setEnhetSomOffline(registrationID);
			request.getRequestDispatcher("SendMeldinger").forward(request, response);
		} //if
	} //doPost

	private void videresendKoordinater(HttpServletRequest request, HttpServletResponse response, String latitude, String longitude) throws ServletException {
		try {
			//Det forventes her at koordinater blir korrekt mottatt, men feil ved utsending kan skje,
			//så sjekk her at faktiske koordinater er mottatt
			//Et annet tilfelle er hvor bruker har startet applikasjonen, men ikke ønsker å dele sin posisjon
			if (!latitude.equals(IKKE_SEND_POSISJON) && !longitude.equals(IKKE_SEND_POSISJON)) {
				//legg ved ekstra attributter for utsending av koordinater
				request.setAttribute(MELDINGSTYPE, SEND_KOORDINATER_MELDING);
				request.setAttribute(MELDINGS_EMNE, SEND_KOORDINATER_MELDING);
				request.getRequestDispatcher("SendMeldinger").forward(request, response);
			} //if (!latitude.equals(IKKE_SEND_POSISJON) && !longitude.equals(IKKE_SEND_POSISJON))
		} catch (IOException ex) {
			ex.printStackTrace();
		} //try/catch
	} //videresendKoordinater
} //MottaMeldingFraEnhet