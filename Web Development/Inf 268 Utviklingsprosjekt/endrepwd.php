<?php 
    include "/include_php/funksjoner.inc.php"; //inkluderer felles funksjoner
    include "/include_php/feilmeldinger.inc.php"; //inkluderer feilmeldinger  
    include "/include_php/passord.inc.php";
    if (isset($_GET['bruker']) && isset($_GET['pwd'])) { //er dette en ny bruker?
      $tilkobling = kobleTil(); //koble til db
      $bruker = mysql_real_escape_string($_GET['bruker']); //hent bruker
      $pwd = mysql_real_escape_string($_GET['pwd']); //hent passord
      $hash = hashPwd($pwd, $bruker); //hash passordet
      $sql = "SELECT * FROM tblStudent ";
      $sql .= "WHERE sEpost = '$bruker' AND sPassord = '$hash' ";
      $resultat = mysql_query($sql,$tilkobling); //finn bruker
      $antall = mysql_num_rows($resultat); //tell opp treff      
      if ($antall == 1) { //finnes bruker med dette passord?
        $rad = mysql_fetch_assoc($resultat); //fyller array med resultat
        $_SESSION['fnavn'] = $rad['sFornavn']; //henter ut fornavn til bruker
        mysql_close($tilkobling); //lukker tilkobling til db
        $_SESSION['email'] = $bruker; //setter bruker til innlogget        
        header("location:endrepwd.php?pwd=$pwd"); //endrer url så kun generert passord vises
      } //if ($antall == 1)
    } //if (isset($_GET['bruker'])
    echo sjekk_autentisert(); //sjekker at bruker ble innlogget    
    $feilGamPwd = ""; //initieres
    $feilNyttPwd = "";
    $feilBekPwd = "";
    include "/include_sideoppsett/include_topp.inc.php"; //inkluderer toppen av siden
?>           
  <title>Endre passord</title>    
  </head>    
  <!--Laster inn focusPwd når siden lastes, så bruker lettere kan endre passord-->
  <?php
    echo '<body onload="';
    if (!isset($_GET['pwd'])) { //er det endring av passord?
      echo 'focusPwd();'; //setter focus i gammelt passord
    } else { //eller dette en ny bruker?
      echo 'focusNyttPwd();'; //setter focus i nytt passord
    } //if (!isset($_GET['pwd']))
    echo '">';
  ?>
 <!--SIDETRAILER-->
  <div id="heading"><h2>Endring av passord</h2></div>
  <?php 
    include "/include_sideoppsett/include_logo_venstre.inc.php"; //inkluderer logo og venstre meny
  ?> 
    <!--INNHOLD-->
  <div id="innhold" >
  <?php     
      if (isset($_POST['LAGRE']) && $_POST['LAGRE'] = "Lagre nytt passord") {
        if (!endrePwd()) {
          $feilGamPwd = feilGamPwd();
          $feilNyttPwd = feilNyttPwd();
          $feilBekPwd = feilBekPwd();
          endrePwdSkjema($feilGamPwd,$feilNyttPwd,$feilBekPwd);
        } //if (!endrePwd())
      } else { //vis skjema for å endre passord
        endrePwdSkjema($feilGamPwd,$feilNyttPwd,$feilBekPwd);      
      } //if (isset($_POST['LAGRE'])) {
  ?>               
  </div> 
  <?php
    include "include_sideoppsett/include_hoyre.inc.php"; //inkluderer høyre marg/meny
    include "include_sideoppsett/include_bunn.inc.php"; //inkluderer bunnen av siden    
  ?>