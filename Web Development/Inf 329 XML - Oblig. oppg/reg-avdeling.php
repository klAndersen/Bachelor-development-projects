<?php include "include/funksjoner.inc.php"; ?>
<?php include "include/avdeling.inc.php"; ?>
<?php include "include/feilmelding.inc.php"; ?>
<?php if (!isset($_GET['avd'])) { ?>
<?php $_GET['avd'] = "visAlle"; ?>
<?php } //if (!isset($_GET['avd'])) ?>
<?php $feilAvdNavn=""; $feilVleder=""; ?><html>
<head>
<!-- UTF-8 for visning av spesialtegn og særnorske bokstaver -->
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<!-- Gjør siden lesbar for Internet Explorer 8 -->
<meta http-equiv="X-UA-Compatible" content="IE=8">
<!-- CSS -->
<link type="text/css" rel="stylesheet" href="default.css">
<!-- Javascript -->
<script language="JavaScript" src="default.js"></script><title>Avdelinger</title>
</head>
<!-- Setter fokus ved lasting av side --><body onload="focusAvdeling();">
<?php include "include/link.inc.php"; ?><div id="resten">
<!-- Linkmeny -->
<!-- Vis alle peker til buffer pga da lastes transformasjonen inn(i tilfelle det har blitt lagt til/endret så dette blir vist) -->
<a href="buffer.php?buffer=avdeling">Vis alle avdelinger</a><br>
<a href="reg-avdeling.php?avd=regAvd">Registrer avdeling</a><br><br>
<?php if ($_GET['avd'] == "regAvd") { //skal det registreres en ny avdeling? ?>
<?php if (isset($_POST['LAGRE_AVD']) && $_POST['LAGRE_AVD'] == "Lagre ny avdeling") { ?>
<?php if (!(lagreAvdeling())) { //ble ny avdeling opprettet? ?>
<?php  $feilAvdNavn = feilAvdelingnavn(); $feilVleder = feilVekselLeder(); ?>
<?php echo opprettNyAvdeling($feilAvdNavn, $feilVleder); //vis skjema med feilmeldinger ?>
<?php  }else { echo "Ny avdeling ble opprettet."; } //if (!lagreAvdeling())) ?>
<?php } else { //har ikke lagret ny avdeling ?>
<!-- felt for registrering av ny avdeling (avdId registreres "automatisk") --><?php echo opprettNyAvdeling($feilAvdNavn, $feilVleder); ?>
<?php   } //if (isset($_POST['LAGRE_AVD']) ?>
<?php } elseif ($_GET['avd'] == "visAlle") { //viser alle avdelingene ?>
<h3>Følgende avdelinger finnes:</h3>
<table border="1">
<tr><form method="post" action="reg-avdeling.php?avd=redAvd">
<td style="width: 200px;" align="right">Auksjon</td>
<td style="width: 100px;" align="right">5</td>
<input type="hidden" name="avdId" value="1">
<input type="hidden" name="vekselLeder" value="5">
<td style="width: 100px;" align="right"><input type="submit" value="Rediger" name="REDIGER_AVD"></td>
<td style="width: 100px;" align="right"><input type="submit" value="Slett" name="SLETT_AVD"></td>
</form></tr>
<tr><form method="post" action="reg-avdeling.php?avd=redAvd">
<td style="width: 200px;" align="right">Bøker</td>
<td style="width: 100px;" align="right">6</td>
<input type="hidden" name="avdId" value="2">
<input type="hidden" name="vekselLeder" value="6">
<td style="width: 100px;" align="right"><input type="submit" value="Rediger" name="REDIGER_AVD"></td>
<td style="width: 100px;" align="right"><input type="submit" value="Slett" name="SLETT_AVD"></td>
</form></tr>
</table>
<?php } else { //skal redigere en avdeling ?>
<?php if(isset($_POST['SLETT_AVD'])) { ?>
<?php //slett(); //funker ikke, se avdeling.inc.php ?>
<?php } //if ?>
<?php if (isset($_POST['LAGRE_ENDRING'])) {  ?>
<?php if(!(redigerAvdeling())) { ?>
<?php $feilAvdNavn = feilAvdelingnavn(); $feilVleder = feilVekselLeder(); ?>
<?php echo redigerSkjema($feilAvdNavn, $feilVleder);//vis skjema med feilmelding ?>
<?php }else { echo "Endringen ble lagret.";} // if(!(redigerAvdeling())) ?>
<?php } else { //har ikke lagret endringer ?>
<?php echo redigerSkjema($feilAvdNavn, $feilVleder); ?>
<?php } // if (isset($_POST['LAGRE_ENDRING']) ?>
<?php  }//if ($_GET['avd'] == "regAvd") ?>
</div>
</body>
</html>
