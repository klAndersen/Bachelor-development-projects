  <?php 
    include "/include_php/funksjoner.inc.php"; //inkluderer felles funksjoner
    include "/include_php/feilmeldinger.inc.php"; //inkluderer feilmeldinger  
    include "/include_php/nettverk.inc.php";      
    echo sjekk_autentisert(); //sjekk at bruker er innlogget
    $feilNett = ""; //initiering
    include "/include_sideoppsett/include_topp.inc.php"; //inkluderer toppen av siden
  ?>
  <title>Nettverk</title>    
  </head> 
  <!--Setter focus i select for nettverk-->
  <body onload="focusNettverk();">  
  <!--------SIDETRAILER-------->  
  <div id="heading"><h2>Nettverk</h2></div>
  <?php 
    include "/include_sideoppsett/include_logo_venstre.inc.php"; //inkluderer logo og venstre meny
  ?>   
  <!--------INNHOLD-------->  
  <div id="innhold">
  <table>    
  <?php                                 
    echo visMineNettverk($feilNett); //viser brukerens nettverk
  ?>  
  </table>
  </div>  
  <?php
    include "include_sideoppsett/include_hoyre.inc.php"; //inkluderer høyre marg/meny
    include "include_sideoppsett/include_bunn.inc.php"; //inkluderer bunnen av siden    
  ?>