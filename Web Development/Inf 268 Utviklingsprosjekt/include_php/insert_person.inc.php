<?php
  //$bType skal alltid være 2! 
  //(1 = Gjest, 2 = Registrert, 3 = Moderator, 4 = Administrator)
  $bType = 2;  
  $tilkobling = kobleTil(); //opprett tilkobling til db  
  $fnavn = mysql_real_escape_string($_POST['fnavn']);
  $enavn = mysql_real_escape_string($_POST['enavn']);
  $epost = mysql_real_escape_string($_POST['email']); 
  $pbilde = "IMG_01.jpg"; //default profilbilde alle brukere får
  $campId = $_POST['idCampus'];
  $fagId = $_POST['idFag'];
  $aarskull = $_POST['aarskull'];
  $mailFinnes = finnesMail($epost); //sjekk om e-post finnes
  $pwd = lagPwd(); //kaller på lag passord (genererer et tilfeldig passord)
  //sjekker at obligatoriske felter er satt    
  if (!empty($epost) && !empty($fnavn) && !empty($enavn) && ($campId != "Intet valgt") && ($fagId != "Intet valgt")) { 
    if ($mailFinnes) { //e-post finnes allerede
      /**************************************************************************
      * man skulle tro at admin/mod sjekker om bruker er utestengt, men         * 
      * oppdaget at dersom flere karantener gjelder                             * 
      * (to innenfor samme tidsramme) så får bruker kun beskjed om at e-post    * 
      * finnes(ikke at han/hun er utestengt) derfor la jeg til en               *
      * "beskyttelse"; LIMIT 1                                                  * 
      **************************************************************************/
      $karantene_sql = "SELECT * FROM tblstudent, tblKarantene ";
      $karantene_sql .= "WHERE sEpost='$epost' AND sId=ksId ";
      $karantene_sql .= "AND kKaranteneTil >= NOW() ";
      $karantene_sql .= "LIMIT 1;";
      $resultat = mysql_query($karantene_sql, $tilkobling);
      $antall = mysql_num_rows($resultat);
      if ($antall == 1) { //e-post er registrert og bruker har karantene
        $karantene = true;  
      } else { //e-post er registrert, men bruker har ikke karantene
        $karantene = false; 
      } //if ($antall == 1)
      while ($rad = mysql_fetch_assoc($resultat)) { //hvis karantene, hent ut info
        $fdato = explode("-", $rad['kKaranteneFra']); //fraDato 
        $fdato = fjernKlokkeslett($fdato);
        $tdato = explode("-", $rad['kKaranteneTil']); //tilDato 
        $tdato = fjernKlokkeslett($tdato);
        $info = $rad['kInfo'];
      } //while 
      if ($karantene) { //har bruker karantene?
        $tekst = "Du er utestengt fra Hibu Alumni. Grunnlag: \"$info\"<br /><br />";
        $tekst .= "Utestengelsen gjelder fra <strong>$fdato</strong> til <strong>$tdato</strong>.";
      } else { //bruker har ikke karantene, men e-posten finnes   
        $tekst = "E-posten <strong>$epost</strong> er allerede registrert, sjekk om rett e-post er oppført. " . TOMME_FELTER;
      } //if ($karantene)
    } else { //e-post finnes ikke
      $hash = hashPwd($pwd, $epost);       
      if ($aarskull == "Intet valgt") { //er årskull valgt?     
        $insert_sql = "INSERT INTO tblStudent "; 
        $insert_sql .= "(sEpost, sFornavn, sEtternavn, sPassord, sProfilbilde, sbId, sfid, ";
        $insert_sql .= "scid) VALUES ('$epost', '$fnavn', '$enavn', '$hash', '$pbilde', ";
        $insert_sql .= "$bType,  $fagId, $campId);";
      } else { //årskull er valgt
        $insert_sql = "INSERT INTO tblStudent "; 
        $insert_sql .= "(sEpost, sFornavn, sEtternavn, sPassord, sProfilbilde, ";         
        $insert_sql .= "sAarskull, sbId, sfid, scid) VALUES ('$epost', '$fnavn', '$enavn', ";
        $insert_sql .= "'$hash', '$pbilde', $aarskull, $bType,  $fagId, $campId);";
      } // if($aarskull == "Intet valgt") 
    mysql_query($insert_sql,$tilkobling); 
    mysql_close($tilkobling); //lukker tilkobling til db
    //link for direkte kobling til endring av passord
    $link = "<a href='http://localhost/Nettsider/utvoppg/endrepwd.php?bruker=$epost&pwd=$pwd'>Logg inn og endre passord</a>"; 
    //Send mail til bruker med velkomstmelding
    //$testMail = "AdminHibu@localhost"; //for testformål               
    $emne ="Velkommen som ny bruker";    
    $innhold = "Velkommen som ny bruker på Alumni Hibu, $fnavn $enavn.<br />";
    $innhold .= "Ditt brukernavn er din e-post: $epost <br />"; 
    $innhold .= "Ditt passord er: <font color='red'>$pwd</font><br /><br />";//markerer passord i rødt
    $innhold .= "Vi anbefaler deg å endre passord etter du har logget inn.<br />";
    $innhold .= "Eller du kan logge inn ved å benytte denne linken:<br />";
    $innhold .= "$link"; //legger ved link i e-post 
    $fra = 'From: postmaster@localhost'; //avsender
    email_utf8($epost, $emne, $innhold, $fra); //konverterer e-post til utf-8      
    } //if($antall >= 1)    	
  } else {
    $tekst = "Et eller flere av de obligatoriske feltene var tomme. " . TOMME_FELTER;
  } //if(!empty($epost))
  /****UTSKRIFTS-TEST******   
  echo "Dette ble registrert: $bType, $fnavn, $enavn, $epost, $aarskull, $pwd <br /><br /> $insert_sql"; */                    
?>                              