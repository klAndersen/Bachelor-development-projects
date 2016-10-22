<%@page import="dt.hin.android.kl_andersen.Databehandling"%>

<div id="heading">
	<h2>Kun for administrator - Logg inn!</h2>
</div>

<div id="ikke_innlogget_innhold">
	Denne siden er kun for administratorer av TracknHide! <br /> Logg
	inn ved å oppgi brukernavn og passord.
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
		%>
		<!--Logg inn form-->
	<center>
		<form name="loggInn" action="LoggInn" method="post"
			onsubmit="return kontrollerInnloggingsdata();">
			<table>
				<tr>
					<td><font size="2pt">Username:</font></td>
					<td><input type="text" name="username" id="username" /></td>
				</tr>
				<tr>
					<td><font size="2pt">Password:</font></td>
					<td><input type="password" name="pwd" id="pwd" /></td>
				</tr>
				<tr>
					<td><input type="submit" name="LOGG_INN" value="Logg inn" /></td>
				</tr>
			</table>
		</form>
	</center>
</div>