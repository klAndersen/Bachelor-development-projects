<?php
    include "/include_php/funksjoner.inc.php"; //inkluderer felles funksjoner
    include "/include_php/begivenhet.inc.php";    
    if (!isset($_GET['beg'])) {
      $_GET['beg'] == "visBegivenhet";   	
    } //if (!isset($_GET['beg']))    
    include "/include_sideoppsett/include_topp.inc.php"; //inkluderer toppen av siden
?>
  <title>Begivenheter</title>    
  </head>       
  <body>  
  <!--------SIDETRAILER-------->  
  <div id="heading"><h2>Begivenheter</h2></div>
  <?php
    include "/include_sideoppsett/include_logo_venstre.inc.php"; //inkluderer logo og venstre meny
  ?>
  <!--------INNHOLD-------->  
  <div id="innhold">
  <!--Gi mulighet for å vise alle begivenheter-->
  <a href="begivenhet.php?beg=visBegivenhet">Se alle begivenheter</a><br /><br /> 
  <?php
      if ($_GET['beg'] == "visBegivenhet") { //skal alle begivenheter vises?
        echo '<!--Viser alle aktive begivenheter-->';
        visBegivenhet(); //viser alle begivenheter
      } else if ($_GET['beg'] == "visEn") { //eller en spesifikk begivenhet? 
        echo '<!--Viser en spesifikk begivenhet-->';
        echo visEnBegivenhet(); //viser en begivenhet  
      } //if ($_GET['beg'] == "visBegivenhet")      
  ?>                  
  </div>
  <?php
    include "include_sideoppsett/include_hoyre.inc.php"; //inkluderer høyre marg/meny
    include "include_sideoppsett/include_bunn.inc.php"; //inkluderer bunnen av siden    
  ?>