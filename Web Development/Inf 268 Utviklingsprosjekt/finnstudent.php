<?php 
    include "/include_php/funksjoner.inc.php"; //inkluderer felles funksjoner
    include "/include_php/finnstudent.inc.php";
    echo sjekk_autentisert(); //sjekker om bruker er innlogget
    if (!isset($_GET['visning'])) {
      $_GET['visning'] = "visAlle"; //intet valgt, gå rett til innboks
    } //if (!isset($_GET['melding']))
    if ($_GET['visning'] == "sokNavn") { //skal det søkes på navn?
      $load = "focusFinn();"; //sett fokus i navn
    } else { //eller søkes på e-post?
      $load = "focusMail();"; //sett fokus i e-post feltet
    } //if ($_GET['visning'] == "sokNavn")
    include "/include_sideoppsett/include_topp.inc.php"; //inkluderer toppen av siden
?>
  <title>Finn tidligere medstudenter</title>    
  </head> 
  <!--Laster inn focusFinn når siden lastes, så bruker kan søke etter andre brukere-->
  <body onload= <?php echo $load; //laster inn satt js-funksjon ?> >
  <!--------SIDETRAILER-------->  
  <div id="heading"><h2>Finn tidligere medstudenter</h2></div>
  <?php 
      include "/include_sideoppsett/include_logo_venstre.inc.php"; //inkluderer logo og venstre meny
  ?>
  <!--------INNHOLD-------->  
  <div id="innhold">
  <!--Link meny-->
  <a href="finnstudent.php?visning=sokNavn">Søk på navn</a><br>
  <a href="finnstudent.php?visning=sokMail">Søk på e-post</a><br>
  <a href="finnstudent.php?visning=visAlle">Vis alle registrerte</a><br /><br>
  <center>
  <?php      
    echo visFinnStudenter(); //lister opp alle brukere som er registrert                
  ?>
  </center>
  </div>  
  <?php
    include "include_sideoppsett/include_hoyre.inc.php"; //inkluderer høyre marg/meny
    include "include_sideoppsett/include_bunn.inc.php"; //inkluderer bunnen av siden    
  ?>