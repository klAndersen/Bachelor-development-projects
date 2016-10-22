<?php  
  /***************************************************************************** 
  *  Dette er et kort script som i hovedsak bare laster inn og videresender    *
  *  bruker til siden for avdeling eller person. Den eneste gangen dette       *
  *  scriptet vil vises for bruker er ved første gangs registrering av         *
  *  avdelinger og person (siden disse vil være tomme)                         *
  *  Etter dette brukes denne kun som en mellomlastning (derav sidenavnet)     *
  *  som laster inn og benytter seg av XSLT for å transformere xml og php      *
  *  til HTML-dokumenter                                                       *
  *****************************************************************************/ 
  include "include/funksjoner.inc.php";
  include "include/feilmelding.inc.php";
  include "include/avdeling.inc.php";
  include "include/person.inc.php";
  include "include/include_topp.inc.php";
  echo '</head>
  <body onload="focusAvdeling();">';
  include "include/link.inc.php"; //meny/intern navigering på siden
  echo '<div id="resten">
  <!--I tilfelle det ikke finnes noen avdelinger, så lastes denne mellomsiden for registrering-->
  <br /><br />';                
  if ($_GET['buffer'] == "avdeling") { //skal avdelinger vises? 
    $mappeNavn = "xml_kontroll/";  
    $xmlFil = $mappeNavn."avdeling.xml";    
    $xslFil = $mappeNavn."avdeling.xsl";     
    $fil = "reg-avdeling.php";      
  } else if ($_GET['buffer'] == "person") { 
    $gjeldendeAar = date('Y'); //henter ut året som er nå
    $mappeNavn = "lopper/";
    $xslFil = "person.xsl"; 
    $fil = "person.php"; 
    $xmlFil = $mappeNavn."loppemarked$gjeldendeAar.xml"; //henter ut fra nyeste                         
  } //if($_GET['buffer'] == "avdeling")
  //print($xmlFil."<br />". $xslFil ."<br />". $fil); //utskrift for testformål
  if (!file_exists($xmlFil)) { //finnes filen?
    if ($_GET['buffer'] == "avdeling") { //skulle siden for avdelinger bli vist?
      echo genererAvdelingside();
    } else {
      registrerPersonSkjema(); //vis skjema for registrering av personer
      echo '<br /><br />'; //luft
      echo listPersoner(); //(hvis ingen er registrert vises tabell med melding om ingen registrert
    } //if ($_GET['buffer'] == "avdeling")
  } else { //fil finnes, last siden
    lastInnXml($xmlFil, $xslFil, $fil); //åpne og last inn xml   
  } //if (!file_exists($xmlFil))             
?>
</div>
</body>
</html>                   