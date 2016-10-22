<?php 
    include "/include_php/funksjoner.inc.php"; //inkluderer felles funksjoner
    include "/include_admin/admin_finnbruker.inc.php";
    echo sjekk_autentisert(); //sjekker om bruker er innlogget
    $sbId = finnBrukertype(); //finn brukertypen
    if ($sbId != 3 && $sbId != 4) { //er innlogget administrator/moderator?
      die("Denne siden er kun for administrator og moderator. " . FORSIDE);
    } //if ($sbId == 3)
    include "/include_sideoppsett/include_topp.inc.php"; //inkluderer toppen av siden
?>
  <title>Bruker - Administrering</title>    
  </head> 
  <!--Laster inn focusFinn når siden lastes, hvis admin/mod ønsker å søke opp brukere-->
  <body onload="focusFinn();"> 
  <!--------SIDETRAILER-------->  
  <div id="heading"><h2>Administrering av brukere</h2></div>      
  <?php 
      include "/include_sideoppsett/include_logo_venstre.inc.php";
  ?>
  <!--------INNHOLD-------->  
  <div id="innhold">
  <br />
  <!--Søkefelt-->
  <?php      
    echo visAlleBrukere(); //viser søkefelt og lister opp alle registrerte brukere 
  ?>
  </div>  
  <?php
    include "include_sideoppsett/include_hoyre.inc.php"; //inkluderer høyre marg/meny
    include "include_sideoppsett/include_bunn.inc.php"; //inkluderer bunnen av siden    
  ?>