<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
<!--
  Dette er et transformasjonskjema som gjelder for siden reg-avdeling.php
  Det som blir vist er alle avdelinger som finnes og mulighet for registrering av ny avdeling
  Det er og lagt in php-kode for behandling av data o.l.
  Laget av Knut Lucas Andersen
-->
<xsl:template match="/">
  <xsl:processing-instruction name="php">include "include/funksjoner.inc.php"; ?</xsl:processing-instruction>
  <!--legger inn linjeskift for bedre lesning/oversikt i fil og kildekode-->
  <xsl:text>&#10;</xsl:text>
  <xsl:processing-instruction name="php">include "include/avdeling.inc.php"; ?</xsl:processing-instruction>
  <xsl:text>&#10;</xsl:text>
  <xsl:processing-instruction name="php">include "include/feilmelding.inc.php"; ?</xsl:processing-instruction>
  <xsl:text>&#10;</xsl:text>
  <xsl:processing-instruction name="php">if (!isset($_GET['avd'])) { ?</xsl:processing-instruction>
  <xsl:text>&#10;</xsl:text>
  <xsl:processing-instruction name="php">$_GET['avd'] = "visAlle"; ?</xsl:processing-instruction>
  <xsl:text>&#10;</xsl:text>
  <xsl:processing-instruction name="php">} //if (!isset($_GET['avd'])) ?</xsl:processing-instruction>
  <xsl:text>&#10;</xsl:text>
  <xsl:processing-instruction name="php">$feilAvdNavn=""; $feilVleder=""; ?</xsl:processing-instruction>
  <!--
    Dette er koden som blir produsert over:
    include "funksjoner.inc.php";
    if (!isset($_GET['avd'])) {
      $_GET['avd'] = "visAlle";
    } //if (!isset($_GET['avd']))  
  -->
  <html>
  <head>
  <xsl:comment> UTF-8 for visning av spesialtegn og særnorske bokstaver </xsl:comment><xsl:text>&#10;</xsl:text>
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
  <xsl:comment> Gjør siden lesbar for Internet Explorer 8 </xsl:comment><xsl:text>&#10;</xsl:text>
  <meta http-equiv="X-UA-Compatible" content="IE=8" />
  <xsl:comment> CSS </xsl:comment><xsl:text>&#10;</xsl:text>
  <link type="text/css" rel="stylesheet" href="default.css" />
  <xsl:comment> Javascript </xsl:comment>
  <xsl:text>&#10;</xsl:text>
  <script language="JavaScript" src="default.js"></script>
  <title>Avdelinger</title>
  </head>
  <xsl:comment> Setter fokus ved lasting av side </xsl:comment>
  <body onload="focusAvdeling();">
  <xsl:processing-instruction name="php">include "include/link.inc.php"; ?</xsl:processing-instruction>
  <div id="resten">
  <!--linker for navigering på siden (linkmeny)-->
  <xsl:comment> Linkmeny </xsl:comment>  <xsl:text>&#10;</xsl:text>
  <xsl:comment> Vis alle peker til buffer pga da lastes transformasjonen inn(i tilfelle det har blitt lagt til/endret så dette blir vist) </xsl:comment>
  <xsl:text>&#10;</xsl:text>
  <a href="buffer.php?buffer=avdeling">Vis alle avdelinger</a><br /> <xsl:text>&#10;</xsl:text>
  <a href="reg-avdeling.php?avd=regAvd">Registrer avdeling</a><br /><br /><xsl:text>&#10;</xsl:text>
  <!--her kommer en if test som sjekker om alle avdelinger skal vises eller om det skal registreres ny avdeling-->
  <xsl:processing-instruction name="php">if ($_GET['avd'] == "regAvd") { //skal det registreres en ny avdeling? ?</xsl:processing-instruction>
  <xsl:text>&#10;</xsl:text>
  <xsl:processing-instruction name="php">if (isset($_POST['LAGRE_AVD']) &amp;&amp; $_POST['LAGRE_AVD'] == "Lagre ny avdeling") { ?</xsl:processing-instruction>
  <xsl:text>&#10;</xsl:text>
  <xsl:processing-instruction name="php">if (!(lagreAvdeling())) { //ble ny avdeling opprettet? ?</xsl:processing-instruction>
  <xsl:text>&#10;</xsl:text>
  <xsl:processing-instruction name="php"> $feilAvdNavn = feilAvdelingnavn(); $feilVleder = feilVekselLeder(); ?</xsl:processing-instruction>
  <xsl:text>&#10;</xsl:text>
  <xsl:processing-instruction name="php">echo opprettNyAvdeling($feilAvdNavn, $feilVleder); //vis skjema med feilmeldinger ?</xsl:processing-instruction> 
  <xsl:text>&#10;</xsl:text>
  <xsl:processing-instruction name="php"> }else { echo "Ny avdeling ble opprettet."; } //if (!lagreAvdeling())) ?</xsl:processing-instruction>
  <xsl:text>&#10;</xsl:text>
  <xsl:processing-instruction name="php">} else { //har ikke lagret ny avdeling ?</xsl:processing-instruction>
<xsl:text>&#10;</xsl:text>
<xsl:comment> felt for registrering av ny avdeling (avdId registreres "automatisk") </xsl:comment>
<xsl:processing-instruction name="php">echo opprettNyAvdeling($feilAvdNavn, $feilVleder); ?</xsl:processing-instruction> 
<xsl:text>&#10;</xsl:text>
<xsl:processing-instruction name="php">  } //if (isset($_POST['LAGRE_AVD']) ?</xsl:processing-instruction> 
<xsl:text>&#10;</xsl:text>
    <xsl:processing-instruction name="php">} elseif ($_GET['avd'] == "visAlle") { //viser alle avdelingene ?</xsl:processing-instruction>
    <xsl:text>&#10;</xsl:text>
    <h3>Følgende avdelinger finnes:</h3>
    <table border="1">
    <xsl:apply-templates select="avdelinger/avdeling" />
    </table>
    <xsl:text>&#10;</xsl:text>
    <xsl:processing-instruction name="php">} else { //skal redigere en avdeling ?</xsl:processing-instruction>
    <xsl:text>&#10;</xsl:text>
    <xsl:processing-instruction name="php">if(isset($_POST['SLETT_AVD'])) { ?</xsl:processing-instruction>
    <xsl:text>&#10;</xsl:text>
    <xsl:processing-instruction name="php">//slett(); //funker ikke, se avdeling.inc.php ?</xsl:processing-instruction>
    <xsl:text>&#10;</xsl:text>
    <xsl:processing-instruction name="php">} //if ?</xsl:processing-instruction>
    <xsl:text>&#10;</xsl:text>
    <xsl:processing-instruction name="php">if (isset($_POST['LAGRE_ENDRING'])) {  ?</xsl:processing-instruction>
    <xsl:text>&#10;</xsl:text>
    <xsl:processing-instruction name="php">if(!(redigerAvdeling())) { ?</xsl:processing-instruction>
    <xsl:text>&#10;</xsl:text>
    <xsl:processing-instruction name="php">$feilAvdNavn = feilAvdelingnavn(); $feilVleder = feilVekselLeder(); ?</xsl:processing-instruction>
    <xsl:text>&#10;</xsl:text>
    <xsl:processing-instruction name="php">echo redigerSkjema($feilAvdNavn, $feilVleder);//vis skjema med feilmelding ?</xsl:processing-instruction>
    <xsl:text>&#10;</xsl:text>
    <xsl:processing-instruction name="php">}else { echo "Endringen ble lagret.";} // if(!(redigerAvdeling())) ?</xsl:processing-instruction>    
    <xsl:text>&#10;</xsl:text>
    <xsl:processing-instruction name="php">} else { //har ikke lagret endringer ?</xsl:processing-instruction>
    <xsl:text>&#10;</xsl:text>
    <xsl:processing-instruction name="php">echo redigerSkjema($feilAvdNavn, $feilVleder); ?</xsl:processing-instruction>    
    <xsl:text>&#10;</xsl:text>
    <xsl:processing-instruction name="php">} // if (isset($_POST['LAGRE_ENDRING']) ?</xsl:processing-instruction>
    <xsl:text>&#10;</xsl:text>
    <xsl:processing-instruction name="php"> }//if ($_GET['avd'] == "regAvd") ?</xsl:processing-instruction>
    <xsl:text>&#10;</xsl:text>
 </div>
 </body>
 </html>
 </xsl:template>
  <!--siden det her er kun avdelinger som skal hentes ut, velges kun avdelingers children-->
  <xsl:template match="avdelinger/avdeling">
  <tr>
    <form method="post" action="reg-avdeling.php?avd=redAvd">
    <td style="width: 200px;" align="right"><xsl:apply-templates select="avdelingNavn"/></td><xsl:text>&#10;</xsl:text>
    <td style="width: 100px;" align="right"><xsl:apply-templates select="vekselLeder"/></td><xsl:text>&#10;</xsl:text>
    <!--legger inn skjult element som inneholder avdelingsId'n-->
    <input type="hidden" name="avdId">
    <xsl:attribute name="value">
      <xsl:apply-templates select="@avdId"/>
    </xsl:attribute>
    </input><xsl:text>&#10;</xsl:text>
    <input type="hidden" name="vekselLeder">
    <xsl:attribute name="value">
      <xsl:apply-templates select="vekselLeder"/>
    </xsl:attribute>
    </input><xsl:text>&#10;</xsl:text>
    <td style="width: 100px;"  align="right"><input type="submit" value="Rediger" name="REDIGER_AVD" /></td><xsl:text>&#10;</xsl:text>
    <td style="width: 100px;"  align="right"><input type="submit" value="Slett" name="SLETT_AVD" /></td><xsl:text>&#10;</xsl:text>
    </form>
  </tr>
  </xsl:template>
</xsl:stylesheet>