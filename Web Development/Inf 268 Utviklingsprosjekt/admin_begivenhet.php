<?php
    include "/include_php/funksjoner.inc.php"; //inkluderer felles funksjoner
    include "/include_php/feilmeldinger.inc.php"; //inkluderer feilmeldinger                             
    include "/include_admin/admin_begivenhet.inc.php"; //inkluderer script for begivenhet
    echo sjekk_autentisert(); //sjekker om bruker er innlogget
    $sbId = finnBrukertype(); //finn den innloggedes brukertype
    if ($sbId != 3 && $sbId != 4) { //er innlogget administrator/moderator?
      die("Denne siden er kun for administrator og moderator. " . FORSIDE);
    } //if ($sbId == 3)
    if(!isset($_GET['beg'])) { //er visning valgt?
      $_GET['beg'] = "visBegivenhetNy"; //sett visning
    } //if(!isset($_GET['beg']))
    //disse brukes for å vise eventuelle feilmeldinger
    $feilTittel = ""; //initiering
    $feilFagomrade = "";
    $feilKort = "";
    $feilBesk = "";
    $feilDato = "";
    $focus = "focusTittel();"; //default verdi for focus
    if (isset($_POST['LAGRE_BEGIVENHET']) && $_POST['LAGRE_BEGIVENHET'] == "Lagre begivenhet" ||
        isset($_POST['LAGRE_ENDRING']) && $_POST['LAGRE_ENDRING'] == "Lagre endringer") { 
      $feilTittel = feilTittel(); //hent feilmelding for tittel (hvis det er noen)
      $feilFagomrade = feilFagomrade(); //hent feilmelding for fagområde
      $feilKort = feilKortInfo(); //hent feilmelding for kort info
      $feilBesk = feilBeskrivelse(); //hent feilmelding for beskrivelse
      $focus = settFocus($feilTittel, $feilFagomrade,$feilKort,$feilBesk); //setter focus
    } //if (isset($_POST['LAGRE_BEGIVENHET'])  
    include "/include_sideoppsett/include_topp.inc.php"; //inkluderer toppen av siden
    echo '<title>Begivenhet - Administrering</title>    
          </head>
          <!--Laster inn '.$focus.' når siden lastes, så bruker kan enklere starte oppretting av ny begivenhet-->       
          <body onload="'.$focus.'">  
          <!--------SIDETRAILER-------->  
          <div id="heading"><h2>Administrering av begivenheter</h2></div>';  
    include "/include_sideoppsett/include_logo_venstre.inc.php"; //inkluderer logo og venstre meny
  ?>
  <!--------INNHOLD-------->  
  <div id="innhold">
  <!--Link meny-->
  <a href="admin_begivenhet.php?beg=visBegivenhetA-Z">Vis begivenheter (A-Z)</a><br />
  <a href="admin_begivenhet.php?beg=visBegivenhetNy">Vis begivenheter (Nyeste til eldst)</a><br />
  <a href="admin_begivenhet.php?beg=visBegivenhetEldst">Vis begivenheter (Eldst til nyest)</a><br />
  <a href="admin_begivenhet.php?beg=opprettBegivenhet">Opprett ny begivenhet</a><br /><br />  
  <?php
      if ($_GET['beg'] == "opprettBegivenhet") { //skal begivenheter opprettes?  
        if (isset($_POST['LAGRE_BEGIVENHET']) && $_POST['LAGRE_BEGIVENHET'] == "Lagre begivenhet") {
          if (!lagreBegivenhet()) { //ble begivenhet opprettet?
            //begivenhet ble ikke opprettet, hent feilmelding(er)
            $feilTittel = feilTittel();
            $feilFagomrade = feilFagomrade();
            $feilKort = feilKortInfo();
            $feilBesk = feilBeskrivelse();
            $feilDato = feilDato();            
            //vis skjema med feilmelding(er)
            skjemaNyBegivenhet($feilTittel,$feilFagomrade,$feilKort,$feilBesk, $feilDato);
          } //if (!lagreBegivenhet())
        } else { 
          //vis skjema for oppretting av begivenhet
          skjemaNyBegivenhet($feilTittel,$feilFagomrade,$feilKort,$feilBesk, $feilDato);  
        } //if (isset($_POST['LAGRE_BEGIVENHET'])         
      } else { //vis/rediger begivenheter                
        if (isset($_POST['ENDRE']) && $_POST['ENDRE'] == "Endre begivenhet") { //skal begivenhet endres?
          skjemaRedigerBegivenhet($feilTittel, $feilFagomrade, $feilKort, $feilBesk, $feilDato);                     
        } else if(isset($_POST['LAGRE_ENDRING']) && $_POST['LAGRE_ENDRING'] == "Lagre endringer") {
            if(!updateBegivenhet()) { //ble endring lagret?
              $feilTittel = feilTittel();
              $feilFagomrade = feilFagomrade();
              $feilKort = feilKortInfo();
              $feilBesk = feilBeskrivelse();
              $feilDato = feilDato();  
              skjemaRedigerBegivenhet($feilTittel, $feilFagomrade, $feilKort, $feilBesk, $feilDato);  
            } //if(!updateBegivenhet())                             
        } else { //viser begivenhet(er)
          if ($_GET['beg'] == "visBegivenhetA-Z") { //skal begivenheter sorteres etter tittel?
            echo '<!--Vis begivenheter sortert på tittel-->';
            visBegivenhet("A-Z");
          } else if ($_GET['beg'] == "visBegivenhetNy") { //skal begivenheter sorteres på nyest?
            echo '<!--Vis begivenheter sortert nyest til eldst-->';
            visBegivenhet("NY");
          } else { //begivenheter sorteres på eldst            
            echo '<!--Vis begivenheter sortert eldst til nyest-->';
            visBegivenhet("ELDST");                       	
          } //if ($_GET['beg'] = "visBegivenhetA-Z")
        } //if(isset($_POST['ENDRE_BEGIVENHET']))                               
      } //if ($_GET['beg'] == "opprettBegivenhet")    
  ?>
  </div>
  <?php
    include "include_sideoppsett/include_hoyre.inc.php"; //inkluderer høyre marg/meny
    include "include_sideoppsett/include_bunn.inc.php"; //inkluderer bunnen av siden    
  ?>