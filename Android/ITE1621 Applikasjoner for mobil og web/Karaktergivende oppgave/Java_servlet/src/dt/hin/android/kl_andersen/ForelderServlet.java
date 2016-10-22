package dt.hin.android.kl_andersen;

import java.io.IOException;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

/**
 * Servlet som fungerer som skjelett for de andre servletene.
 * @author Knut Lucas Andersen
 */
@SuppressWarnings("serial")
abstract class ForelderServlet extends HttpServlet {

	/**
	 * @see HttpServlet#doGet(HttpServletRequest request, HttpServletResponse response)
	 */
	@Override
	protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		super.doGet(request, response);
	} //doGet

	/**
	 * @see HttpServlet#doPost(HttpServletRequest request, HttpServletResponse response)
	 */
	@Override
	protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		super.doPost(request, response);
	} //doPost
	
	/**
	 * Henter ut parameterverdien fra oversendt HttpServletRequest.
	 * @param request - HttpServletRequest
	 * @param parameter - String: ID på parameter hvis verdi skal hentes ut
	 * @return String: verdien/innholdet til parameter
	 * @throws ServletException - Parameter ikke funnet
	 */
	protected String hentParametere(HttpServletRequest request, String parameter) throws ServletException {
		String verdi = request.getParameter(parameter);
		//inneholder parameteren verdi?
		if (erTomEllerNull(verdi)) {
			throw new ServletException("Mottatt parameter " + parameter + " ikke funnet!");
		} //if (erTomEllerNull(verdi))
		return verdi.trim();
	} //hentParametere
	
	/**
	 * Henter ut parameterverdien fra oversendt HttpServletRequest. <br />
	 * Dersom parameter som verdi skal hentes ut fra ikke har verdi (null/blank),  
	 * returneres oversendt defaultVerdi.
	 * @param request - HttpServletRequest
	 * @param parameter - String: ID på parameter hvis verdi skal hentes ut
	 * @param defaultVerdi - String: Verdi som skal returneres hvis parameter ikke har verdi
	 * @return String: oversendt parameters verdi || defaultverdi
	 */
	protected String hentParametere(HttpServletRequest request, String parameter, String defaultVerdi) {
		String verdi = request.getParameter(parameter);
		if (erTomEllerNull(verdi)) {
			verdi = defaultVerdi;
		} //if (erTomEllerNull(verdi))
		return verdi.trim();
	} //hentParametere
	
	protected void setSuksess(HttpServletResponse response) {
		setSuksess(response, 0);
	} //setSuksess

	protected void setSuksess(HttpServletResponse response, int size) {
		response.setStatus(HttpServletResponse.SC_OK);
		response.setContentType("text/plain");
		response.setContentLength(size);
	} //setSuksess

	/**
	 * Funksjon som sjekker om oversendt string har innhold. <br />
	 * Det sjekkes på om string er null og om den inneholder whitespace (""). <br />
	 * Dersom string har verdi returneres false, hvis string er tom returneres true.
	 * @param verdi - String: Verdi som skal sjekkes om har innhold
	 * @return Boolean: true - oversendt verdi er tom <br />
	 * false - oversendt verdi har innhold (ikke null/ blank(""))
	 */
	protected boolean erTomEllerNull(String verdi) {
		//er verdi null eller inneholder den kun space/intet?
		if (verdi == null || verdi.trim().length() == 0) {
			return true;
		} //if (verdi == null || verdi.trim().length() == 0)
		return false;
	} //erTomEllerNull
} //ForelderServlet