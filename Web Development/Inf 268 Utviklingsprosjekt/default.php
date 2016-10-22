<?php 
    include "/include_php/funksjoner.inc.php"; //inkluderer felles funksjoner
    include "/include_sideoppsett/include_topp.inc.php"; //inkluderer toppen av siden  
?>
  <title>Alumni Hibu</title>    
  </head>       
  <!--Laster inn focusLoggInn når siden lastes, så bruker lettere kan logge inn-->
  <body onload="focusLoggInn();">  
  <!--------SIDETRAILER-------->  
  <div id="heading"><h2>Alumni Hibu</h2></div>
  <?php 
    include "/include_sideoppsett/include_logo_venstre.inc.php"; //inkluderer logo og venstre meny
  ?>   
  <!--------INNHOLD-------->                     
  <div id="innhold">
  <?php                   
    if (isset($_SESSION['email'])) { //er bruker innlogget?                  
      echo '<h2>Velkommen ' .$_SESSION['fnavn']. '</h2>
      <br /><br />
      Denne siden er utviklet for studenter som enten har deltatt, eller fortsatt
      deltar i studier på Høgskolen i Buskerud. Hensikten med denne nettsiden er at det
      skal fungere som et slags forum slik at tidligere og nåværende studenter
      kan finne tidligere deltakere fra samme kurs. <br />Mer informasjon kommer!';                             
    } else if(!empty($tekst)) { //sjekk om $tekst er tom...
      echo $tekst;  
    } else { //ingen innloggingsforsøk...
  ?>
  <h2>Velkommen til Høgskolen i Buskeruds Alumni.</h2>
  <br /><br />
  Denne siden er utviklet for studenter som enten har deltatt, eller fortsatt deltar 
  i studier på Høgskolen i Buskerud. Hensikten med denne nettsiden er at det skal 
  fungere som et slags forum slik at tidligere og nåværende studenter kan finne 
  tidligere deltakere fra samme kurs. <br />Mer informasjon kommer!
  <?php
      }//if (isset($_SESSION['email']))
   ?>
  </div>       
  <?php
    include "include_sideoppsett/include_hoyre.inc.php"; //inkluderer høyre marg/meny
    include "include_sideoppsett/include_bunn.inc.php"; //inkluderer bunnen av siden    
  ?>