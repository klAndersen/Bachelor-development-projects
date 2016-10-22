<?php 
  /*
  Det er noen plasser satt inn echo ' hvor teksten/innholdet starter på neste linje.
  Dette gjorde jeg siden dette er grunnstruktur, som er statisk, og det gjør da lesing
  av kildekoden enklere og mer oversiktlig     
  */
  //viser logoene
  echo '<!--Logo-->
  <div id="venstre_logo"><img src="carolines_logo.png" /></div>   
  <div id="hoyre_logo"><img src="carolines_logo.png" /></div> 
  <!--------VENSTRE-------->   
  <div id="venstre">';
  include "logginn.inc.php"; //inkluderer visning av innlogging 
  //viser linker som alle får se:    
  echo '
  <!--Meny/navigering på siden-->
  <ul type="circle">
  <li><a class="nonLink" href="default.php">Forsiden</a></li>          
  <!--Har manipulert denne linken ved å bruke en "a - element" uten href slik 
  at den åpner hibus hjemmeside i ny fane/vindu via javascript-->
  <li><a class="nonLink" onclick="visHibu();">Hibu\'s hjemmeside</a></li>
  <li><a class="nonLink" href="begivenhet.php?beg=visBegivenhet">Nyheter/Begivenheter</a></li>'; 
  //linker kun for innloggede
  if (isset($_SESSION["email"])) { //hvis bruker er innlogget, vis bruker-relaterte linker       
    hentUleste($_SESSION['email']); //har bruker uleste meldinger/mottatt nye meldinger?
    echo '
    <li><a class="nonLink" href="profil.php">Min profil</a></li>';
    //viser uleste meldinger (hvis det er noen)
    echo '
    <li><a class="nonLink" href="melding.php?melding=Innboks">Meldinger '. visAntallUleste() .'</a></li>';    
    echo '
    <li><a class="nonLink" href="nettverk.php">Nettverk</a></li>     
    <li><a class="nonLink" href="finnstudent.php?visning=visAlle">Finn medstudenter</a></li>            
    <li><a class="nonLink" href="endrepwd.php">Endre passord</a></li>'; 
    $sbId = finnBrukertype(); //henter ut brukertypen
    //linker kun for administrator og moderator
    if ($sbId == 3 || $sbId == 4) { //er bruker admin/mod?
      echo '
      <li><a class="nonLink" href="admin_begivenhet.php?beg=visBegivenhetNy">Administrer begivenhet</a></li>      
      <li><a class="nonLink" href="admin_brukere.php">Administrer brukere</a></li>                        
      <li><a class="nonLink" href="admin_campus.php?campus=visCampus">Administrer campus</a></li>
      <li><a class="nonLink" href="admin_fagomraade.php?fagomraade=visFag">Administrer fagområde</a></li>      
      <li><a class="nonLink" href="admin_nettverk.php?adm_nettverk=visNettverk">Administrer nettverk</a></li>';      
    } //if ($sbId == 3 || $sbId == 4)
    echo '
    <li><a class="nonLink" href="loggut.php">Logg ut</a></li>'; //utlogging 
  } //if(isset($_SESSION["email"])) 
  echo '
  </ul>
  </div>'; //avslutter meny-listen og div
?>