<%@ page import="java.util.ArrayList"%>
<%@page import="dt.hin.android.kl_andersen.Konstanter"%>
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
<title>Vis online enheter</title>
</head>
<body>
	<%
		//er et session-objekt opprettet, og har objektet verdi? 
		if (Databehandling.getSessionObjekt() == null
				|| Databehandling.getSessionBrukernavn() == null) {
	%>
	<%@ include file="forwardinnlogging.jsp"%>
	<%
		} else {
	%>
	<div id="heading">
		<h2>Følgende enheter er online</h2>
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
			NB! Enheter med verdien
			<%=Konstanter.IKKE_SEND_POSISJON%>
			har ikke slått på sending av egne posisjonsdata.
		<p>
			<%
				if (onlineEnheter.size() > 0) {
			%>
		
		<table border="1">
			<tr>
				<th>RegistrationID:</th>
				<th>Enhetsnavn:</th>
				<th>Latitude:</th>
				<th>Longitude:</th>
			</tr>
			<%
				String startTekst = "<tr><td>", sluttTekst = "</td></tr>", utskrift;
						for (int i = 0; i < onlineEnheter.size(); i++) {
							//hvis koordinater har verdi, beregn den 
							double lat = (onlineEnheter.get(i).getLatitude() == Double
									.parseDouble(Konstanter.IKKE_SEND_POSISJON)) ? Double
									.parseDouble(Konstanter.IKKE_SEND_POSISJON)
									: (onlineEnheter.get(i).getLatitude() / 1E6);
							double lon = (onlineEnheter.get(i).getLongitude() == Double
									.parseDouble(Konstanter.IKKE_SEND_POSISJON)) ? 999.0
									: (onlineEnheter.get(i).getLongitude() / 1E6);
							utskrift = startTekst
									+ onlineEnheter.get(i).getRegistrationID()
									+ "</td><td>"
									+ onlineEnheter.get(i).getEnhetsnavn()
									+ "</td><td>" + lat + "</td><td>" + lon
									+ sluttTekst;
			%>
			<%=utskrift%>
			<%
				} //for
					} // if (onlineEnheter.size() > 0)
			%>
		</table>
	</div>
	<%
		} //if (Databehandling.getSessionObjekt() == null ...)
	%>
</body>
</html>