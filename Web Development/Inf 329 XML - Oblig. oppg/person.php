<?php include "include/funksjoner.inc.php"; ?>
<?php include "include/person.inc.php"; ?>
<?php include "include/feilmelding.inc.php"; ?>
<html>
<head>
<!-- UTF-8 for visning av spesialtegn og særnorske bokstaver --><meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<!-- Gjør siden lesbar for Internet Explorer 8 --><meta http-equiv="X-UA-Compatible" content="IE=8">
<!-- CSS --><link type="text/css" rel="stylesheet" href="default.css">
<!-- Javascript -->
<script language="JavaScript" src="default.js"></script><title>Personer</title>
</head>
<!-- Setter fokus ved lasting av side --><body onload="focusFornavn();">
<?php include "include/link.inc.php"; ?><div id="resten">
<br>
<!-- felt for registrering av ny person (personId registreres "automatisk") --><?php registrerPersonSkjema(); ?>
<table><tr><td colspan="4">
<?php if (isset($_POST['LAGRE_PERSON'])) { ?>
<?php if (!(lagrePerson())) { //ble person lagret? ?>
<?php echo feilmeldingPerson(); //print ut feilmelding ?>
<?php } //if (!(lagrePerson())) ?>
<?php } //if (isset($_POST[LAGRE_PERSON''])) ?>
</td></tr></table>
<br><br><br><table border="1">
<tr>
<th>Person</th>
<th>Avdeling</th>
<th>Veksel lørdag</th>
<th>Veksel søndag</th>
</tr>
<?php echo listPersoner(); ?>
</table>
</div>
</body>
</html>
