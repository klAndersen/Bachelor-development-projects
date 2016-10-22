<?php
  include "/include_php/funksjoner.inc.php"; //inkluderer felles funksjoner
  include "/include_php/feilmeldinger.inc.php"; //inkluderer feilmeldinger  
  include "/include_admin/karantene.inc.php";
  echo sjekk_autentisert(); //sjekk om bruker er innlogget
  $sbId = finnBrukertype(); //finn brukertypen
  if ($sbId != 3 && $sbId != 4) { //er innlogget administrator/moderator?
      die("Denne siden er kun for administrator og moderator. " . FORSIDE);
  } //if ($sbId == 3)
  if (!isset($_GET['brukerId'])) { //har admin/mod kommet hit fra logg/url?
  	die("Du må velge en bruker fra Administrer brukere. ". TILBAKE); //gi feilmelding
  } else { //kommer hit fra admin_brukere
    $sId = $_GET['brukerId']; //hent valgt brukers id
  } //if (!isset($_GET['brukerId']))
  if (!isset($_GET['karantene'])) { //er visning valgt?
    $_GET['karantene'] = "brukerId=$sId"; //intet valgt, vis karantener
    $tittel = "Vis Karantener"; //setter tittel      
  } else { //skal utestenge en bruker
    $tittel = "Sett bruker i karantene"; //sett tittel
  } //if (!isset($_GET['karantene']))
  $feilFnavn = ""; //initiering
  $feilEnavn = "";
  $feilDato = "";
  $feilInfo = "";
  $focus = "focusKaranteneInfo();"; //default verdi for focus 
  if (isset($_POST['UTESTENG']) && $_POST['UTESTENG'] == "Utesteng bruker") {
    $feilFnavn = feilFnavn(); //hent feilmelding (hvis det er noen)
    $feilEnavn = feilEnavn(); //hent feilmelding    
    $feilInfo = feilKaranteneInfo(); //hent feilmelding
    $focus = settFocus($feilFnavn, $feilEnavn, $feilInfo); //setter focus
  } //if (isset($_POST['UTESTENG']) 
  include "/include_sideoppsett/include_topp.inc.php"; //inkluderer toppen av siden
?>     
  <title>Karantene</title>    
  </head>
  <?php  
    echo '<!--Laster inn '.$focus.' når siden lastes, for enklere oppretting av utestengelse-->       
          <body onload="'.$focus.'">  
          <!--------SIDETRAILER-------->  
          <div id="heading"><h2>'.$tittel.'</h2></div>'; //setter focus og viser tittel
    include "/include_sideoppsett/include_logo_venstre.inc.php"; //inkluderer logo og venstre meny
  ?>   
  <!--------INNHOLD-------->  
  <div id="innhold">
  <!--Link meny-->
  <?php
    echo '<a href="karantene.php?brukerId='. $sId .'">Vis karantener</a><br />
          <a href="karantene.php?karantene=settKarantene&brukerId='. $sId .'">Sett bruker i karantene</a><br />
          <a href="admin_brukere.php">Tilbake til administrering</a><br /><br />';         
    if ($_GET['karantene'] == "brukerId=$sId") { //skal karantene(r) vises?        
      echo hentKarantene($sId);
    } else { //eller skal karantene settes?
      if (isset($_POST['UTESTENG']) && $_POST['UTESTENG'] == "Utesteng bruker") {
        if (!settKarantene($sId)) { //var utestengning sukessfull?
          $feilFnavn = feilFnavn(); //hent feilmelding (hvis det er noen)
          $feilEnavn = feilEnavn();
          $feilDato = feilDato();
          $feilInfo = feilKaranteneInfo();
          //vis skjema med feilmelding(er)
          echo skjemaKarantene($sId, $feilFnavn, $feilEnavn, $feilDato, $feilInfo);
        } //if (!settKarantene($sId))
      } else { //ikke forsøkt å utestenge, vis skjema
        echo skjemaKarantene($sId, $feilFnavn, $feilEnavn, $feilDato, $feilInfo);
      } //if (isset($_POST['UTESTENG'])   
    } //if ($_GET['karantene'] == "brukerId=$sId")   
  ?>
  </div>  
  <?php
    include "include_sideoppsett/include_hoyre.inc.php"; //inkluderer høyre marg/meny
    include "include_sideoppsett/include_bunn.inc.php"; //inkluderer bunnen av siden    
  ?>