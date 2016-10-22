<?php 
    include "/include_php/funksjoner.inc.php"; //inkluderer felles funksjoner
    include "/include_php/feilmeldinger.inc.php"; //inkluderer feilmeldinger  
    include "/include_php/passord.inc.php";
    $feilEmail = ""; //initiering
    include "/include_sideoppsett/include_topp.inc.php"; //inkluderer toppen av siden
?>
  <title>Glemt passord</title>    
  </head>
  <!--Laster inn focusMail når siden lastes, så bruker raskere kan skrive inn e-post-->
  <body onload="focusMail();">  
  <!--------SIDETRAILER-------->  
  <div id="heading"><h2>Glemt passord?</h2></div>
  <?php 
      include "/include_sideoppsett/include_logo_venstre.inc.php"; //inkluderer logo og venstre meny
  ?>  
  <!--------INNHOLD-------->  
  <div id="innhold">  
  <br />
  <?php
      if (isset($_POST['GLEMT_PWD']) && $_POST['GLEMT_PWD'] == "Send passord") {              
        if (!glemtPwd()) { //ble nytt passord sendt?          
          $epost = $_POST['email']; //hent e-post som ble skrevet inn
          //false pga ikke redigering, null pga den ikke skal sammenlignes
          $feilEmail = feilEMail(false, null, $epost); //hent feilmelding  
          glemtPwdSkjema($feilEmail); //vis feilmelding
        } //if (!glemtPwd())
      } else { //vis skjema for glemt passord
        glemtPwdSkjema($feilEmail); //viser skjema       
      } //if (isset($_POST['PWD']) 
  ?> 
  </div>  
  <?php
    include "include_sideoppsett/include_hoyre.inc.php"; //inkluderer høyre marg/meny
    include "include_sideoppsett/include_bunn.inc.php"; //inkluderer bunnen av siden    
  ?>