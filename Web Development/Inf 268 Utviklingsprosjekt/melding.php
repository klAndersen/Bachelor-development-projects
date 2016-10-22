<?php 
    include "/include_php/funksjoner.inc.php"; //inkluderer felles funksjoner
    include "/include_php/feilmeldinger.inc.php"; //inkluderer feilmeldinger  
    include "/include_php/melding.inc.php";
    echo sjekk_autentisert(); //er bruker innlogget?    
    $email = $_SESSION['email']; //finner pålogget bruker...
    $sbId = finnBrukertype(); //finner brukertypen...
    if ($sbId == 3 || $sbId == 4) { //gi mulighet for masseutsendelse hvis admin/mod
      $link = '<a href="melding.php?melding=SendMelding">Send en ny melding</a><br />';
      $link .= '<a href="melding.php?melding=Masseutsendelse">Send melding til flere</a><br /><br />';
    } else { //registrert, kan kun sende melding om gangen
      $link = '<a href="melding.php?melding=SendMelding">Send en ny melding</a><br /><br />';
    } //if ($sbId == 3)
    //variabler for feilmelding
    $feilFnavn = ""; //initiering 
    $feilEnavn = "";
    $feilMottakergruppe = ""; 
    $feilMelding = ""; 
    $tittel = setTittel($sbId); //setter tittel på siden                                 
    if (isset($_POST['SEND']) && $_POST['SEND'] == "Send melding") { //har det vært et forsøk på sending?
      if ($tittel == "Send melding") { //er det forsøkt å sende en melding?
        $feilFnavn = feilFnavn(); //er fornavn ok?
        $feilEnavn = feilEnavn(); //er etternavn ok?
        $feilMelding = tomMelding(); //er meldingen ok?
      } else { //eller er det forsøkt å sende flere meldinger?  
        $feilMottakergruppe = feilMottakergruppe(); //er mottakergruppe valgt? 
        $feilMelding = tomMelding(); //er melding ok?                   
      } //if ($tittel == "Send melding")        
    } //if (isset($_POST['SEND']) 
    if (isset($_GET['mottaker'])) { //er mottaker satt?
      if ((isset($_POST['SEND']) && $_POST['SEND'] == "Send melding")) { //er det forsøkt å sende til mottaker?
        $focus = "focusMelding();"; //sett focus i meldingsfeltet
      } else { //ikke forsøkt å sende til mottaker      
        $focus = "focusEmne();"; //sett focus i feltet for emne
      } //if ((isset($_POST['SEND'])
    } else { //mottaker er ikke satt, sjekk hvor focus skal settes
      $focus = settFocus($sbId, $feilFnavn,$feilEnavn, $feilMottakergruppe, $feilMelding);   
    } //if (isset($_GET['mottaker'])) 
    include "/include_sideoppsett/include_topp.inc.php"; //inkluderer toppen av nettsiden 
    echo "<title>$tittel</title>
          </head>"; //setter title og avslutter head
    echo '<!--Laster inn '.$focus.' når siden lastes, så bruker kan enklere starte sending av ny melding-->
          <body onload="'.$focus.'">
          <!--------SIDETRAILER-------->
          <div id="heading"><h2>Private meldinger - '.$tittel.'</h2></div>'; //laster inn focus og inkl. logo (neste linje)  
    include "/include_sideoppsett/include_logo_venstre.inc.php"; //inkluderer logo og venstre meny
  ?>   
  <!--------INNHOLD-------->         
  <div id="innhold">
  <!--Link meny-->
  <a href="melding.php?melding=Innboks">Innboks</a><br />
  <a href="melding.php?melding=Utboks">Utboks</a><br />   
  <?php
      echo $link;
      if ($_GET['melding'] == "Innboks") { //gå til innboks
        if (isset($_GET['mNr'])) { //skal en spesifikk melding vises    
          echo visEnMottatt($email); //viser en mottatt melding 
        } else { //viser alle mottatte meldinger
          echo visAlleMottate($email); //viser alle
        } //if (isset($_GET['mNr']))
      } else if ($_GET['melding'] == "Utboks") { //gå til utboks
        if (isset($_GET['mNr'])) { //skal en spesifikk melding vises    
          echo visEnSendt($email); //viser en sendt melding  
        } else { //viser alle sendte meldinger
          echo visAlleSendte($email); //viser alle
        } //if (isset($_GET['mNr']))        
      } else { //oppretter ny melding 
        if ((isset($_POST['SEND']) && $_POST['SEND'] == "Send melding")) { //skal melding sendes?
          hentSendingsType($email, $feilFnavn, $feilEnavn, $feilMottakergruppe, $feilMelding); //hvilken type melding skal sendes          
        } else { //har ikke skrivd melding enda, vis meldingsskjema
          //hent skjema(form) for sending av melding(er)        
          hentSendingsSkjema($sbId, $feilFnavn, $feilEnavn, $feilMottakergruppe, $feilMelding); 
      } //if (isset($_POST['SEND'])   
   }//if ($_GET['melding'] == "Innboks")
  ?>
  </div>  
  <?php
    include "include_sideoppsett/include_hoyre.inc.php"; //inkluderer høyre marg/meny
    include "include_sideoppsett/include_bunn.inc.php"; //inkluderer bunnen av siden    
  ?>