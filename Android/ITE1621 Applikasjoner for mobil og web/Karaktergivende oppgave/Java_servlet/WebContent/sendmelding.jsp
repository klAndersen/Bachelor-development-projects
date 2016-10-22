<%@page import="dt.hin.android.kl_andersen.Databehandling"%>

<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
	pageEncoding="ISO-8859-1"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<%
	//sett opp default tekst som skal vises
	String emne = "GCM_Default_Tittel";
	String message = "Kringkastningsmelding som sendes til alle online enheter.";

	//i tilfelle en melding ble sendt, hent ut attributt og sjekk om den har verdi
	Object emneOjekt = request.getAttribute("meldingsEmne");
	if (emneOjekt != null) {
		emne = emneOjekt.toString();
	} //if (tittelsOjektVerdi != null)
		//gjør det samme for meldingsfeltet
	Object meldingsObjekt = request.getAttribute("melding");
	if (meldingsObjekt != null) {
		message = meldingsObjekt.toString();
	} //if (meldingsObjekt != null)
%>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
<!--CSS-->
<link type="text/css" rel="stylesheet" href="default.css" />
<title>Send melding</title>
</head>
<body>
	<%
	//er et session-objekt opprettet, og har objektet verdi? 
	if (Databehandling.getSessionObjekt() == null
			|| Databehandling.getSessionBrukernavn() == null) {
	%>
	<%@include file="forwardinnlogging.jsp"%>
	<%
		} else {
	%>

	<div id="heading">
		<h2>Send melding til enheter som er online</h2>
	</div>
	<%@ include file="include_venstre_meny.jsp"%>
	<div id="innhold">
		<%
			//hent ut antall enheter som er online, og hvis enheter er online, vis skjema for sending av meldinger
				int antOnline = Databehandling.getAntallOnline();
				if (antOnline > 0) {
		%>
		<form action="SendMeldinger?meldingsType=message" method="post">
			<label>Melding som skal kringkastes:</label> <br /> <input
				type="text" name="meldingsEmne" value="<%=emne%>" /> <br />
			<textarea name="melding" rows="10" cols="60"><%=message%> </textarea>
			<br /> <input type="submit" name="SEND_MELDING" value="Send Melding" />
		</form>
		<p>
		<p>
			<%
				if (Databehandling.getTilbakemelding() != null
								&& !Databehandling.getTilbakemelding().equals("")) {
			%>
			<%=Databehandling.getTilbakemelding()%>
			<%
				//tøm innhold i tilbakemelding
							Databehandling.setTilbakemelding("");
						} //if (!Databehandling.getTilbakemelding()...)
					} else {
			%>
			Ingen enheter er online.
			<%
				} //if (antOnline > 0)
			%>
		
	</div>
	<%
		} //if (Databehandling.getSessionObjekt() == null ...)
	%>
</body>
</html>