<?php 
    include "/include_php/funksjoner.inc.php"; //inkluderer felles funksjoner
    include "/include_php/profil.inc.php";
    echo sjekk_autentisert(); //sjekker om bruker er innlogget  
    if (isset($_GET['visProfil'])) { //er det søkt på bruker?
      $email = $_GET['visProfil']; //hent ut søkt brukers e-post
      $tittel = "Vis profil"; //setter tittel på siden
      $tekst = '<a href="melding.php?melding=SendMelding&mottaker='.$email.'">Send en ny melding</a><br /><br />';                  
    } else { //vis innlogget brukers egen profil
      $email = $_SESSION['email']; //henter innlogget brukers e-post
      $tittel = "Min profil"; //setter tittel på siden       
      $tekst = "<br /><a href='redigerProfil.php'>Rediger profil</a>";              
    } //if(isset($_GET['visProfil']))
    include "/include_sideoppsett/include_topp.inc.php"; //inkluderer toppen av siden
?>
  <title>Profil</title>    
  </head>    
  <body>
  <!--SIDETRAILER-->
  <div id="heading"><h2> <?php echo $tittel; ?> </h2></div>
  <?php 
    include "/include_sideoppsett/include_logo_venstre.inc.php"; //inkluderer logo og venstre meny
  ?>
  <!--INNHOLD-->
  <div id="innhold">
  <br />
  <center>                                     
  <?php        
    echo visProfil($email); //viser profilen basert på variabelen $email
    echo $tekst; //skriver ut $tekst (innholdet baseres på om det er visning eller egen profil)      
  ?>
  </center>
  </div>
  <?php
    include "include_sideoppsett/include_hoyre.inc.php"; //inkluderer høyre marg/meny
    include "include_sideoppsett/include_bunn.inc.php"; //inkluderer bunnen av siden    
  ?>