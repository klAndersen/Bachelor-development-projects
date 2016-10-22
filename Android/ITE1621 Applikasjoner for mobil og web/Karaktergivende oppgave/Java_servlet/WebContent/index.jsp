<%@page import="dt.hin.android.kl_andersen.Databehandling"%>

<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
	pageEncoding="ISO-8859-1"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
<!-- Javascript -->
<script language="JavaScript">
	
<%@ include file="Javascript.js"%>
	
</script>
<!--CSS-->
<link type="text/css" rel="stylesheet" href="default.css" />
<title>TracknHide</title>
</head>
<body onload="document.getElementById('username').focus();">
	<%
		//er et session-objekt opprettet, og har objektet verdi? 
		if (Databehandling.getSessionObjekt() == null
				|| Databehandling.getSessionBrukernavn() == null) {
	%>
	<%@include file="include_loggin.jsp"%>
	<%
		} else {
	%>
	<div id="heading">
		<h2>
			Velkommen
			<%=Databehandling.getSessionBrukernavn()%>!
		</h2>
	</div>
	<%@ include file="include_venstre_meny.jsp"%>
	<div id="innhold">
		Dette er hjemmesiden til Android programmet TracknHide, et
		program for Android. <br /> Programmet ble utviklet i 2013, i samsvar
		med faget ITE1621 - Applikasjoner for mobil og web, p� H�gskolen i
		Narvik. <br />
		<!-- Luft -->
		<p>
		<h3>Oppgaven inneholdt f�lgende:</h3>
		<ol>
			<li>Det skulle skrives en rapport som inneholdt forklaring om
				hva Google Cloud Messaging er for noe, i tillegg til dokumentasjon
				av programmet som ble utviklet.</li>
			<li>Den andre delen var � utvikle denne serveren med tilh�rende
				nettsider og database som tar vare p� administratorer og registrerte
				enheter som kommer online.</li>
			<li>Den tredje og st�rste delen var � utvikle et kartprogram for
				Android ved bruk av Google Maps, Google Cloud Messaging og
				tilkobling til nettopp denne serveren for � dele posisjonsdata.</li>
		</ol>
		<!-- luft -->
		<p>
		<h3>Ting du kan gj�re p� dette web-stedet er f�lgende:</h3>
		<ul>
			<li>Se hvilke enheter som er online, og deres siste registrerte
				posisjon</li>
			<li>Sende meldinger til alle enheter som er online</li>
			<li>Utestenge enheter fra server</li>
		</ul>
	</div>
	<%
		} //if (Databehandling.getSessionInnlogget() == null ...)
	%>
</body>
</html>