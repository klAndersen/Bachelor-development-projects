<?php 
    include "/include_php/funksjoner.inc.php"; //inkluderer felles funksjoner
    include "/include_php/feilmeldinger.inc.php"; //inkluderer feilmeldinger  
    include "/include_admin/admin_skole.inc.php";
    echo sjekk_autentisert(); //sjekker om bruker er innlogget
    $sbId = finnBrukertype(); //finn brukertypen
    if ($sbId != 3 && $sbId != 4) { //er innlogget administrator/moderator?
      die("Denne siden er kun for administrator og moderator. " . FORSIDE);
    } //if ($sbId == 3)
    if (!isset($_GET['campus'])) {
      $_GET['campus'] = "visCampus";
    } //if (!isset($_GET['campus']))      
    $feilCampus = ""; //initiering        
    include "/include_sideoppsett/include_topp.inc.php"; //inkluderer toppen av siden
?>
  <title>Campus - Administrering</title>      
  </head>       
  <!--Setter focus ved oppretting/redigering av campus når siden lastes-->
  <body onload="focusNyttCampus();"> 
  <!--------SIDETRAILER-------->  
  <div id="heading"><h2>Administrering av campus</h2></div>
  <?php 
      include "/include_sideoppsett/include_logo_venstre.inc.php";
  ?>   
  <!--------INNHOLD-------->  
  <div id="innhold"> 
    <a href="admin_campus.php?campus=visCampus">Vis alle campus</a><br />
    <a href="admin_campus.php?campus=leggTilCampus">Opprett nytt campus</a><br /><br />  
  <?php        
    if ($_GET['campus'] == "visCampus") { //lister opp registrerte campus    
      echo "<!--Viser alle registrerte campus-->";
      echo "<h3>Følgende campus er gitt:</h3>";  
      visCampus(); //viser alle campus med rediger og slette knapp      
    } else if ($_GET['campus'] == "leggTilCampus") { //oppretter et nytt campus
      echo "<!--Opprett et nytt campus-->";
      if (isset($_POST['OPPRETT_CAMPUS']) && $_POST['OPPRETT_CAMPUS'] == "Opprett campus") {        
        if (!opprettCampus()) { //ble nytt campus opprettet?
          $feilCampus = feilNyttCampus();
          skjemaOpprettCampus($feilCampus); //vis skjema med feilmeldinger  
        } //if (!opprettCampus())
      } else { //har ikke lagret nytt campus
        skjemaOpprettCampus($feilCampus); //vis skjema for oppretting av campus
      } //if (isset($_POST['OPPRETT_CAMPUS'])
    } else { //redigering av campus
      if (isset($_POST['LAGRE_ENDRING']) && $_POST['LAGRE_ENDRING'] == "Endre campus") {        
        if(!redigerCampus()) { //ble campus redigert og lagret?
          $feilCampus = feilNyttCampus();
          skjemaRedigerCampus($feilCampus); //vis skjema med feilmeldinger         
        } //if(!redigerCampus())
      } else if (isset($_POST['SLETT_CAMPUS']) && $_POST['SLETT_CAMPUS'] == "Slett") {
        slettCampus(); //forsøk å slette campus
      } else { //har ikke lagret endringer
        skjemaRedigerCampus($feilCampus); //vis skjema for redigering av campus
      } // if (isset($_POST['LAGRE_ENDRING'])
    } //if ($_GET['campus'] == "visCampus")
  ?>                                                
  </div>       
  <?php
    include "include_sideoppsett/include_hoyre.inc.php"; //inkluderer høyre marg/meny
    include "include_sideoppsett/include_bunn.inc.php"; //inkluderer bunnen av siden    
  ?>