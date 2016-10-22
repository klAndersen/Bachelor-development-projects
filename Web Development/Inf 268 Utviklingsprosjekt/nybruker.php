<?php 
    include "/include_php/funksjoner.inc.php"; //inkluderer felles funksjoner
    include "/include_php/feilmeldinger.inc.php"; //inkluderer feilmeldinger  
    include "/include_php/nybruker.inc.php"; 
    //variabler for feilmeldinger 
    $feilFnavn = ""; //initieres
    $feilEnavn = "";
    $feilEmail = "";
    $feilCampus = ""; 
    $feilFag = "";
    $oppsummering = false; //variabel for om oppsummering skal vises
    $registrer = false; //variabel for om bruker skal registreres
    $tittel = "Ny bruker"; //setter tittel    
    if (isset($_POST['SJEKK_INFO']) && $_POST['SJEKK_INFO'] == "Kontroller informasjon") {
      $epost = $_POST['email']; //hent e-post 
      $feilFnavn = feilFnavn(); //er fornavn korrekt?
      $feilEnavn = feilEnavn(); //er etternavn korrekt?
      //false pga ikke redigering, null pga ikke innlogget, $epost pga denne skal sjekkes
      $feilEmail = feilEMail(false, null, $epost); //er e-post korrekt 
      $feilCampus = feilCampus(); //er campus valgt?
      $feilFag = feilFagomrade(); //er fagområde valgt?
      if (empty($feilFnavn) && empty($feilEnavn) && empty($feilEmail)&& empty($feilCampus) && empty($feilFag)) {
        $tittel = "Sjekk av informasjon"; //endre tittel
        $oppsummering = true; //skal vise skjema for oppsummering
      } //if (empty($feilFnavn))      
    } else if (isset($_POST['REGISTRER']) && $_POST['REGISTRER']=='Registrer deg') {
      $registrer = true; //vis registrering
      $epost = $_POST['email']; //hent e-post  
      //tilbakemelding hvis registrering var suksessfull
      $tekst = "Din profil har blitt opprettet. Passord er ";
      $tekst .= "sendt til din e-post: $epost."; 
      $tittel = "Registrering av ny bruker"; //endre tittel           
    } //if (isset($_POST['SJEKK_INFO'])
    $focus = settFocus($feilFnavn,$feilEnavn,$feilEmail, $feilCampus, $feilFag); //hvor skal focus settes?
    include "/include_sideoppsett/include_topp.inc.php"; //inkluderer toppen av siden
    echo "<title>$tittel</title>"; //setter tittel basert på hva som skal vises        
    echo '</head>
    <!--Laster inn '.$focus.' når siden lastes for enklere registrering-->    
    <body onload="'.$focus.'">
    <!--SIDETRAILER-->
    <div id="heading"><h2>'.$tittel.'</h2></div>
    ';  
    include "/include_sideoppsett/include_logo_venstre.inc.php"; //inkluderer logo og venstre meny
  ?>
  <!--INNHOLD-->
  <div id="innhold">
  <?php                   
    if ($oppsummering) { //skal oppsummering vises?        
      visOppsummeringsSkjema(); //viser oppsummering
    } else if ($registrer) { //skal tilbakemelding på registrering vises?
      include "/include_php/insert_person.inc.php"; //inkluder oppsett for registrering av brukere
      echo $tekst; //vis teksten      
    } else { //skal skjema for registrering av ny bruker vises?
      visNyBrukerSkjema($feilFnavn,$feilEnavn,$feilEmail, $feilCampus, $feilFag);      
    } //if (isset($_POST['SJEKK_INFO']) 
  ?>
  </div>
  <?php
    include "include_sideoppsett/include_hoyre.inc.php"; //inkluderer høyre marg/meny
    include "include_sideoppsett/include_bunn.inc.php"; //inkluderer bunnen av siden    
  ?>