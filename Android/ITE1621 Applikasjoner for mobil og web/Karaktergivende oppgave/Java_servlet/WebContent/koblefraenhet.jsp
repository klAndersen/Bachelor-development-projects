<%@ page import="java.util.ArrayList"%>
<%@page import="dt.hin.android.kl_andersen.Enhet"%>
<%@page import="dt.hin.android.kl_andersen.Databehandling"%>

<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
	pageEncoding="ISO-8859-1"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
<!--CSS-->
<link type="text/css" rel="stylesheet" href="default.css" />
<!-- Javascript -->
<script language="JavaScript">
	
<%@ include file="Javascript.js"%>
	
</script>
<title>Frakobling</title>
</head>
<body>
	<%
		//er enheter valgt for frakobling?
		String valgteEnheter[] = request.getParameterValues("chkKobleFra");
		if (valgteEnheter != null && valgteEnheter.length != 0) {
			Databehandling.kobleFraEnheter(valgteEnheter);
		} //if (valgteEnheter != null && valgteEnheter.length != 0)

		//er et session-objekt opprettet, og har objektet verdi? 
		if (Databehandling.getSessionObjekt() == null
				|| Databehandling.getSessionBrukernavn() == null) {
	%>
	<%@ include file="forwardinnlogging.jsp"%>
	<%
		} else {
	%>
	<div id="heading">
		<h2>Frakobling av enheter</h2>
	</div>
	<%@ include file="include_venstre_meny.jsp"%>
	<div id="innhold">
		<%
			ArrayList<Enhet> onlineEnheter = Databehandling
						.hentAlleEnheter();
		%>
		Antall enheter som er online:
		<%=onlineEnheter.size()%>
		<p>
		<p>
			<%
				if (onlineEnheter.size() > 0) {
			%>
			<!-- Form for å koble enheter fra server -->
		<form name="kobleFraEnheter" action="" method="post">
			<table border="1">
				<tr>
					<th>RegistrationID:</th>
					<th>Enhetsnavn:</th>
					<th>Dato registrert</th>
					<!-- Checkbox som huker av alle/ingen -->
					<th><input type='checkbox' id='chkVelgAlle'
						onClick='velgAlleCheckboxer()' /> Koble fra enhet</th>
				</tr>
				<%
					String startTekst = "<tr><td>", sluttTekst = "</td></tr></form>", utskrift;
							for (int i = 0; i < onlineEnheter.size(); i++) {
								utskrift = startTekst
										+ onlineEnheter.get(i).getRegistrationID()
										+ "</td><td>"
										+ onlineEnheter.get(i).getEnhetsnavn()
										+ "</td><td>"
										+ onlineEnheter.get(i).getRegistrertDato()
										+ "</td><td>"
										+ "<input type='checkbox' name='chkKobleFra' value='"
										+ onlineEnheter.get(i).getRegistrationID()
										+ "'>" + sluttTekst;
				%>
				<%=utskrift%>
				<%
					} //for
				%>
			</table>
			<input type="submit" name=KOBLE_FRA value="Koble fra enhet(er)" />
		</form>
		<%
			} //if (onlineEnheter.size() > 0)
		%>
	</div>
	<%
		} //if (Databehandling.getSessionObjekt() == null ...)
	%>
</body>
</html>