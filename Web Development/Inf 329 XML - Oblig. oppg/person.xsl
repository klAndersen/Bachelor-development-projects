<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
<!--
  Dette er et transformasjonskjema som gjelder for siden person.php
  Det som blir vist er alle personer som er registrert og mulighet for registrering av ny person
  Det er og lagt in php-kode for behandling av data o.l.
  Laget av Knut Lucas Andersen
-->
  <xsl:template match="/">
  <xsl:processing-instruction name="php">include "include/funksjoner.inc.php"; ?</xsl:processing-instruction>
  <!--legger inn linjeskift for bedre lesning/oversikt i fil og kildekode-->  
  <xsl:text>&#10;</xsl:text>
  <xsl:processing-instruction name="php">include "include/person.inc.php"; ?</xsl:processing-instruction>
  <xsl:text>&#10;</xsl:text>
  <xsl:processing-instruction name="php">include "include/feilmelding.inc.php"; ?</xsl:processing-instruction>    
  <xsl:text>&#10;</xsl:text>
  <html>
  <head>
  <xsl:comment> UTF-8 for visning av spesialtegn og særnorske bokstaver </xsl:comment>
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
  <xsl:comment> Gjør siden lesbar for Internet Explorer 8 </xsl:comment>
  <meta http-equiv="X-UA-Compatible" content="IE=8" />
  <xsl:comment> CSS </xsl:comment>
  <link type="text/css" rel="stylesheet" href="default.css" />
  <xsl:comment> Javascript </xsl:comment>
  <xsl:text>&#10;</xsl:text>
  <script language="JavaScript" src="default.js"></script>
  <title>Personer</title>
  </head>
  <xsl:comment> Setter fokus ved lasting av side </xsl:comment>
  <body onload="focusFornavn();">
  <xsl:processing-instruction name="php">include "include/link.inc.php"; ?</xsl:processing-instruction>
  <div id="resten">
    <br />
    <xsl:text>&#10;</xsl:text>
    <xsl:comment> felt for registrering av ny person (personId registreres "automatisk") </xsl:comment>      
    <xsl:processing-instruction name="php">registrerPersonSkjema(); ?</xsl:processing-instruction>
    <xsl:text>&#10;</xsl:text>
    <table> 
    <tr><td colspan='4'>
    <xsl:processing-instruction name="php">if (isset($_POST['LAGRE_PERSON'])) { ?</xsl:processing-instruction>
    <xsl:text>&#10;</xsl:text>
    <xsl:processing-instruction name="php">if (!(lagrePerson())) { //ble person lagret? ?</xsl:processing-instruction>
    <xsl:text>&#10;</xsl:text>
    <xsl:processing-instruction name="php">echo feilmeldingPerson(); //print ut feilmelding ?</xsl:processing-instruction>
    <xsl:text>&#10;</xsl:text>
    <xsl:processing-instruction name="php">} //if (!(lagrePerson())) ?</xsl:processing-instruction>
    <xsl:text>&#10;</xsl:text>
    <xsl:processing-instruction name="php">} //if (isset($_POST[LAGRE_PERSON''])) ?</xsl:processing-instruction>
    <xsl:text>&#10;</xsl:text>
    </td></tr>
    </table><xsl:text>&#10;</xsl:text>
    <br /><br /><br />
    <table border="1">
    <xsl:text>&#10;</xsl:text>
    <tr><th>Person</th><th>Avdeling</th><th>Veksel lørdag</th><th>Veksel søndag</th></tr>
    <xsl:text>&#10;</xsl:text>
    <xsl:processing-instruction name="php">echo listPersoner(); ?</xsl:processing-instruction>
    <xsl:text>&#10;</xsl:text>
    </table>
 </div>
 </body>
 </html>
</xsl:template>
</xsl:stylesheet>
