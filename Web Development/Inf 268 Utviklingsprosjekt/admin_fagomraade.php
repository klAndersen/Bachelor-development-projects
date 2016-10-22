<?php 
    include "/include_php/funksjoner.inc.php"; //inkluderer felles funksjoner
    include "/include_php/feilmeldinger.inc.php"; //inkluderer feilmeldinger  
    include "/include_admin/admin_skole.inc.php";
    echo sjekk_autentisert(); //sjekker om bruker er innlogget
    $sbId = finnBrukertype(); //finn brukertypen
    if ($sbId != 3 && $sbId != 4) { //er innlogget administrator/moderator?
      die("Denne siden er kun for administrator og moderator. " . FORSIDE);
    } //if ($sbId == 3)
    if (!isset($_GET['fagomraade'])) {
      $_GET['fagomraade'] = "visFag";
    } //if (!isset($_GET['fagomraade']))    
    $feilFag = ""; //initiering
    include "/include_sideoppsett/include_topp.inc.php"; //inkluderer toppen av siden 
?>
  <title>Fag - Administrering</title>      
  </head>       
  <!--Setter focus ved oppretting/redigering av fagområde når siden lastes-->
  <body onload="focusNyttFagomraade();">  
  <!--------SIDETRAILER-------->  
  <div id="heading"><h2>Administrering av fagområde</h2></div>
  <?php 
      include "/include_sideoppsett/include_logo_venstre.inc.php";
  ?>   
  <!--------INNHOLD-------->  
  <div id="innhold">
    <a href="admin_fagomraade.php?fagomraade=visFag">Vis alle fagområder</a><br />
    <a href="admin_fagomraade.php?fagomraade=leggTilFag">Opprett nytt fagområde</a><br /><br>    
  <?php
    if ($_GET['fagomraade'] == "visFag") { //lister opp registrerte fagområder
      echo "<!--Viser alle registrerte fagområde-->";
      echo "<h3>Følgende fagområder er gitt:</h3>";  
      echo visFagomraade(); //vis fagområder med redigering og slette knapp
    } else if ($_GET['fagomraade'] == "leggTilFag") { //oppretter et nytt fagområde
      echo "<!--Opprett et nytt fagområde-->";
      if (isset($_POST['OPPRETT_FAG']) && $_POST['OPPRETT_FAG'] == "Opprett fagområde") {        
          if (!opprettFagomraade()) { //ble nytt fagområde opprettet?
            $feilFag = feilNyttFagomraade(); //vis skjema med feilmeldinger
            skjemaOpprettFag($feilFag);
          } //if (!opprettFagomraade())
      } else { //har ikke lagret nytt fagområde
        skjemaOpprettFag($feilFag); //vis skjema for oppretting av nytt fagområde             
      } //if (isset($_POST['OPPRETT_NETTVERK'])
    } else { //redigering av fagområder
      if (isset($_POST['LAGRE_ENDRING']) && $_POST['LAGRE_ENDRING'] == "Endre fagområde") {
        if(!redigerFagomraade()) { //ble campus redigert?
          $feilFag = feilNyttFagomraade();
          skjemaRedigerFag($feilFag); //vis skjema med feilmeldinger
        } //if(!redigerFagomraade())
      } else if (isset($_POST['SLETT_FAG']) && $_POST['SLETT_FAG'] == "Slett") {
        echo slettFagomraade(); //forsøk å slette fagområde
      } else { //har ikke lagret endringer  
       skjemaRedigerFag($feilFag); //vis skjema for redigering av fagområde
      } // if (isset($_POST['LAGRE_ENDRING'])
    } //if ($_GET['fagomraade'] == "visFag")
  ?>
  </div>       
  <?php
    include "include_sideoppsett/include_hoyre.inc.php"; //inkluderer høyre marg/meny
    include "include_sideoppsett/include_bunn.inc.php"; //inkluderer bunnen av siden    
  ?>