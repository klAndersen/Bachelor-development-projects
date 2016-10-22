<?php
  include "/include_php/funksjoner.inc.php"; //inkluderer felles funksjoner
  include "/include_php/feilmeldinger.inc.php"; //inkluderer feilmeldinger  
  include "/include_admin/admin_nettverk.inc.php";
  echo sjekk_autentisert(); //sjekk at bruker er innlogget
  $sbId = finnBrukertype(); //finn brukertypen
  if ($sbId != 3 && $sbId != 4) { //er innlogget administrator/moderator?
    die("Denne siden er kun for administrator og moderator. " . FORSIDE);
  } //if ($sbId == 3)
  if(!isset($_GET['adm_nettverk'])) {
    $_GET['adm_nettverk'] = "visNettverk";
  } //if(!isset($_GET['adm_nettverk']))
  $verdi = $_GET['adm_nettverk'];  
  $feilNettverk = ""; //initiering
  include "/include_sideoppsett/include_topp.inc.php"; //inkluderer toppen av siden
?>
  <title>Nettverk - Administrering</title>    
  </head> 
  <!--Laster inn focusNyttNettverk() når siden lastes-->  
  <body onload="focusNyttNettverk();">
  <!--------SIDETRAILER-------->  
  <div id="heading"><h2>Administrering av nettverk</h2></div>
  <?php 
      include "/include_sideoppsett/include_logo_venstre.inc.php";
  ?>   
  <!--------INNHOLD-------->  
  <div id="innhold">
  <a href="admin_nettverk.php?adm_nettverk=visNettverk">Vis alle nettverk</a><br />
  <a href="admin_nettverk.php?adm_nettverk=nyttNettverk">Opprett nytt nettverk</a><br /><br />  
  <table>
  <?php 
      if ($_GET['adm_nettverk'] == "visNettverk") { //viser alle nettverk
        echo "<!--Viser alle registrerte nettverk-->
              <h3>Følgende nettverk er gitt:</h3>
              "; //linjeskift for bedre oversikt i kildekoden
        visNettverk(); //viser alle nettverkene som finnes
      } else if ($_GET['adm_nettverk'] == "nyttNettverk") { //lag nytt nettverk  
        echo '<!--Opprett et nytt nettverk-->';
        if (isset($_POST['OPPRETT_NETTVERK']) && $_POST['OPPRETT_NETTVERK'] == "Opprett nettverk") {        
          if (!opprettNettverk()) { //ble nytt nettverk opprettet?
            $feilNettverk = feilNyttNettverk(); //hent feilmelding
            echo nyttNettverkSkjema($feilNettverk); //vis feilmelding
          } //if (!opprettNettverk())
        } else { //vis skjema for å opprette et nytt nettverk               
          echo nyttNettverkSkjema($feilNettverk); //skjema for å opprette et nytt nettverk
        } //if (isset($_POST['OPPRETT_NETTVERK'])                             
      } else { //endring av et nettverk      
        echo "<!--Endre/slette nettverk-->";
        if (isset($_POST['ENDRE_NETTVERK']) && $_POST['ENDRE_NETTVERK'] == "Endre nettverk") {           
          if (!endreNettverk()) { //ble nettverket endret?
            $feilNettverk = feilEndreNettverk(); //hent feilmelding
            echo endreNettverkSkjema($feilNettverk); //vis feilmelding
          } //if (!endreNettverk())
        } else if (isset($_POST['SLETT_NETTVERK']) && $_POST['SLETT_NETTVERK'] == "Slett") {
          slettNettverk(); //forsøk å slette nettverk
        } else { //har ikke lagret noen endringer enda
          echo endreNettverkSkjema($feilNettverk); //vis skjema for å endre navn på nettverk
        } //if (isset($_POST['ENDRE_NETTVERK'])
      } //if ($_GET['adm_nettverk'] == "nyttNettverk") 
  ?>   
 </table>
 </div>  
  <?php
    include "include_sideoppsett/include_hoyre.inc.php"; //inkluderer høyre marg/meny
    include "include_sideoppsett/include_bunn.inc.php"; //inkluderer bunnen av siden    
  ?>