<?php
  /********************************VIS PROFIL***********************************/
  function visProfil($email) {
    $tilkobling = kobleTil(); //kobler til db 
    //spørring som henter ut profil-relatert info
    $sql = "SELECT * FROM tblstudent, tblcampus, tblfagomrade ";
    $sql .= "WHERE sEpost = '$email' AND sfid=fId and scid=cId";
    //echo $sql;
    $resultat = mysql_query($sql, $tilkobling);
    echo '<table style="border-collapse:collapse">
    '; //linjeskift for bedre oversikt i kildekoden
    $rad = mysql_fetch_assoc($resultat); //forventer kun et resultat, derfor ingen løkke 
    $fnavn = $rad['sFornavn'];
    $enavn = $rad['sEtternavn'];
    $pbilde = $rad['sProfilBilde'];
    $campus = $rad['cNavn'];
    $fag = $rad['fNavn'];
    $aarskull = $rad['sAarskull'];
    $fb = $rad['sFacebook'];
    $ms = $rad['sMySpace'];
    $hs = $rad['sHjemmeside']; 
    $About =  $rad['sAbout'];
    if (!empty($pbilde)) { //sjekker om bruker har profilbilde
      //er ikke innsatt i table pga skalering av feltene
      echo '<img src="profilbilde/'.$pbilde.'" height="150" />
      '; //linjeskift for bedre oversikt i kildekoden 
    } //if (!empty($pbilde))    
    echo '<tr><td class="ramme">Navn:</td>
          <td class="ramme" style="width:180px;">'. $fnavn .' '. $enavn .'</td></tr>
          <tr><td class="ramme">E-mail:</td><td class="ramme">'. $email .'</td></tr>
          <tr><td class="ramme">Campus:</td><td class="ramme">'. $campus .'</td></tr>
          <tr><td class="ramme">Fagområde:</td><td class="ramme">'. $fag .'</td></tr>
          '; //linjeskift for bedre oversikt i kildekoden
    if (!empty($aarskull)) { //har bruker satt årskull?
      echo '<tr><td class="ramme">Årskull:</td><td class="ramme">'. $aarskull .'</td></tr>';
    } //if (!empty($aarskull)) 
    /*disse feltene vises kun dersom det er skrivd inn en link her
        videre så vil de åpnes i et nytt vindu når linken klikkes, og 
        musa vil se ut som en liten hånd*/ 
    if (!empty($fb) && $fb != "http://") { //ligger det noe her, og er tekst noe annet enn http://
      echo '<tr><td class="ramme">Gå til min:</td>
            <td class="ramme">
            <a style="cursor:pointer;" onclick="visPersonligSide(\''.$fb.'\');">
            Facebook</a></td></tr>';         
    } //if (!empty($fb) && $fb != "http://")
    if (!empty($ms) && $ms != "http://") { //ligger det noe her, og er tekst noe annet enn http://
          echo '<tr><td class="ramme">Gå til min:</td>
                <td class="ramme">          
                <a style="cursor:pointer;" onclick="visPersonligSide(\''.$ms.'\');">
                MySpace</a></td></tr>';           
    } //if (!empty($ms) && $ms != "http://")
    if (!empty($hs) && $hs != "http://") { //ligger det noe her, og er tekst noe annet enn http://
      echo '<tr><td class="ramme">Gå til min:</td>
              <td class="ramme">     
              <a style="cursor:pointer;" onclick="visPersonligSide(\''.$hs.'\');">
              Hjemmeside</a></td></tr>';        
    } //if (!empty($hs) && $hs != "http://")
    if(!empty($About)) { //har bruker skrevet noe om seg selv?
      echo '<tr><td class="ramme">Kompetanse/Karriere:</td>
            <td class="ramme">' . $About . '</td></tr>'; 
    } // if(!empty($About))                                   
    echo '
    </table>
    '; //linjeskift for bedre oversikt i kildekoden
    mysql_close($tilkobling); //lukker tilkobling til db
  } //visProfil
  
  /******************************REDIGER PROFIL*********************************/
  function redigerPersonligInfo($feilFnavn, $feilEnavn, $feilEmail) {
    $email = $_SESSION['email'];
    $tilkobling = kobleTil(); //oppretter tilkobling til db      
    //spørring som henter ut profil-relatert info
    $sql = "SELECT * FROM tblStudent ";
    $sql .= "WHERE sEpost = '$email';";
    //echo $sql;
    $resultat = mysql_query($sql, $tilkobling);
    $rediger = '<fieldset>
    <legend>Rediger/Legg til informasjon - valgfrie felt er frivillig</legend>
    <br />
    <form method="post" action="" onsubmit="return sjekkRegistrering();">
    <table>
    '; //linjeskift for bedre oversikt i kildekoden     
    $rad = mysql_fetch_assoc($resultat); //forventer kun et resultat, derfor ingen løkke 
      $fnavn = $rad['sFornavn'];
      $enavn = $rad['sEtternavn'];
      $pBilde = $rad['sProfilBilde'];    
    if (isset($_GET['bValg'])) { //har bruker vært på albumsiden?
      $verdi = 'value="'.$_GET['bValg'].'" '; //sett profilbilde til valgt bilde       
    } else { //ikke valgt nytt bilde    
      $verdi = 'value="'.$pBilde.'" '; //sett profilbilde til eksisterende bilde
    } //if (isset($_GET['bValg']))
    $rediger .= '<!--Personalia-->
    <tr><td>Skriv inn fornavn:</td>
    <td><input type="text" id="fnavn" name="fnavn" value="'.$fnavn.'" maxlength="30" /> ' 
    .$feilFnavn.'</td></tr>
    <tr><td>Skriv inn etternavn:</td>
    <td><input type="text" id="enavn" name="enavn" value="'.$enavn.'" maxlength="40" /> ' 
    .$feilEnavn.'</td></tr>
    <tr><td>Skriv inn e-post:</td>
    <td><input type="text" id="email" name="email" value="'.$email.'" maxlength="80" /> '
    .$feilEmail.'</td></tr>
    <tr><td>Sett profilbilde:</td>
    <td><input type="text" name="bValg" '.$verdi.' />
    <a href="bildeopplastning.php?bilde=lastOpp">Endre profilbilde</a></td></tr>';                 
    mysql_close($tilkobling); //lukker tilkobling til db
    return $rediger; //returnerer skjemaet
  } //redigerPersonligInfo
  
  function redigerSosialeNettverk() {
    $email = $_SESSION['email'];
    $tilkobling = kobleTil(); //oppretter tilkobling til db      
    //spørring som henter ut profil-relatert info
    $sql = "SELECT * FROM tblStudent ";
    $sql .= "WHERE sEpost = '$email';";
    //echo $sql;
    $resultat = mysql_query($sql, $tilkobling);  
    $rad = mysql_fetch_assoc($resultat); //forventer kun et resultat, derfor ingen løkke 
      $sAbout = $rad['sAbout'];
      $fb = $rad['sFacebook'];
      $ms = $rad['sMySpace'];
      $hs = $rad['sHjemmeside'];    
    $rediger = '<!--Personlige/sosiale nettverkssider-->    
    <tr><td>Link til Facebook:</td>
    <td><input type="text" id="fb" name="fb" value="';
    if(!empty($fb)) { //ligger det noe i db?
      $rediger .= $fb; //vis innhold
    } else { //db er tom
      $rediger .= "http://"; // vis http://
    } //if(!empty($fb))    
    $rediger .= '" maxlength="255" size="60"/> (valgfri)</td></tr>    
    <tr><td>Link til MySpace:</td>
    <td><input type="text" id="ms" name="ms" value="';
    if(!empty($ms)) { //ligger det noe i db?
      $rediger .= $ms; //vis innhold
    } else { //db er tom
      $rediger .= "http://"; // vis http://
    } //if(!empty($ms)) 
    $rediger .=  '" maxlength="255" size="60"/> (valgfri)</td></tr>        
    <tr><td>Link til Hjemmeside:</td>
    <td><input type="text" id="hs" name="hs" value="';
    if(!empty($hs)) { //ligger det noe i db?
      $rediger .= $hs; //vis innhold
    } else { //db er tom
      $rediger .= "http://"; // vis http://
    } //if(!empty($hs))
    $rediger .= '" maxlength="255" size="60"/> (valgfri)</td></tr>       
    <tr><td>Kompetanse/Karriere:</td>
    <td><textarea cols="45" rows="5" name="omMeg">'.$sAbout.'</textarea> (valgfri)</td>  
    </tr>
    </table>'; 
    mysql_close($tilkobling); //lukker tilkobling til db
    return $rediger; //returnerer skjemaet
  } //redigerSosialeNettverk
  
  /**************************LAGRE ENDRING(ER) I DB*****************************/
  function lagreEndring() {
    $email = $_SESSION['email'];
    $tilkobling = kobleTil(); //opprett tilkobling til db
    $fnavn = mysql_real_escape_string($_POST['fnavn']);
    $enavn = mysql_real_escape_string($_POST['enavn']);
    $ny_Epost = mysql_real_escape_string($_POST['email']); 
    $bildefil = $_POST['bValg'];
    $cId = $_POST['idCampus'];
    $fId = $_POST['idFag'];
    $aarskull = $_POST['aarskull'];
    //er e-post endret og hvis den er, finnes ny e-post fra før?
    $sjekkEmail = feilEMail(true, $email, $ny_Epost); //henter ut feilmeldinger (hvis det er noen)    
    
    if (empty($fnavn) || empty($enavn) || empty($ny_Epost)) { //er navn/e-post blanket ut/tomt ut?
      return false; //tomme felt(er), send feilmelding
    } else if (strlen($fnavn) < 2 ||strlen($enavn) < 3) { //er fornavn/etternavn for kort?
      return false; //for kort fornavn/etternavn, send feilmelding   
    } else if (!empty($sjekkEmail)) { //er e-post ok?
      return false; //feil i e-post, send feilmelding         	                	           
    } else { //alt ok, oppdater profil
      //først renskes feltene for mulig mysql-kode (siden feltene ikke har max-length)             
      $fb = mysql_real_escape_string($_POST['fb']);
      $ms = mysql_real_escape_string($_POST['ms']);
      $hs = mysql_real_escape_string($_POST['hs']);
      $sAbout = mysql_real_escape_string($_POST['omMeg']);              
      //deretter renskes feltene for mulig html-kode (siden feltene ikke har max-length)
      $fb = rensHtmlUTF8($fb, 3); 
      $ms = rensHtmlUTF8($ms, 3); 
      $hs = rensHtmlUTF8($hs, 3);
      $sAbout = rensHtmlUTF8($sAbout, 3);       
      //så sjekkes det om feltene har verdi 
      $fb = isNull($fb); 
      $ms = isNull($ms); 
      $hs = isNull($hs); 
      $sAbout = isNull($sAbout);
      //til slutt om linkene starter på http://
      $fb = harHTTP($fb);
      $ms = harHTTP($ms);
      $hs = harHTTP($hs);
      if ($aarskull == "Intet valgt") { //sjekker årskull er satt            
        $update_sql = "UPDATE tblStudent "; 
        $update_sql .= "SET sFornavn = '$fnavn', sEtternavn = '$enavn', ";
        $update_sql .= "sEpost = '$ny_Epost', scid = $cId, sfid = $fId, ";
        $update_sql .= "sProfilBilde='$bildefil' ";
        $update_sql .= "WHERE sEpost = '$email';";      
      } else { // alle feltene er fylt ut
        $update_sql = "UPDATE tblStudent "; 
        $update_sql .= "SET sFornavn = '$fnavn', sEtternavn = '$enavn', ";
        $update_sql .= "sEpost = '$ny_Epost', sFaceBook = '$fb', ";
        $update_sql .= "sMySpace = '$ms', sHjemmeside = '$hs', scid = $cId,  ";
        $update_sql .= "sfid = $fId, sProfilBilde='$bildefil', ";
        $update_sql .= "sAarskull = $aarskull, sAbout = '$sAbout' ";
        $update_sql .= "WHERE sEpost = '$email';";  
      } // if($aarskull == "Intet valgt")
      //echo $update_sql;
      mysql_query($update_sql,$tilkobling); 
      mysql_close($tilkobling); //lukker tilkobling til db
      $_SESSION['email'] = $ny_Epost; //setter brukers innlogging på nytt i tilfelle e-post ble endret  
      return true; //profil ble oppdatert
    } //if (empty($fnavn) || empty($enavn))    
  } //lagreEndring
  
  function isNull($verdi) {
    if (empty($verdi)) { //har $verdi innhold?
      return null; //tom, returner null
    } else { //har verdi, returner verdien
      return $verdi;
    } //if (empty($verdi))
  } //isNull
  
  function harHTTP($link) {
    $http = "http://"; //sett http
    $subString = substr($link, 0, 7); //hent ut starten av linken
    //echo "sub: $subString";
    if ($subString != $http) { //starter linken på http://?
      $link = $http . $link; //legg til http:// på starten av linken
      //echo "link ".$link;
    } //if ($subString != $http)
    return $link; //returner linken
  } //harHTTP
  
  /********************************PROFIL BILDE*********************************/
  function nyttProfilbilde() { 
    //grunnlaget er basert på Webprogrammering i php, s. 225           
    if (empty($_FILES['filbane']['name'])) { //ble en fil valgt?
      echo "<br />Ingen fil ble valgt."; //la inn <br /> for å få mer luft 
    } else { //en fil ble valgt    
      $filtype = $_FILES['filbane']['type']; //MIME typen til filen
      //$temp_fil er et midlertidig filnavn bestemt i php.ini
      $temp_fil = $_FILES['filbane']['tmp_name'];        
      $mappeNavn = "profilbilde/"; //adressen til mappen
      $mappeRef = opendir($mappeNavn); //åpner mappen      
      $i = -2; //initerer teller, -2 pga den teller to bilder for mye
      while ($neste  = readdir($mappeRef)) { //henter ut bildene
        if (strstr($filtype,"jpeg") || strstr($filtype,"jpg") || strstr($filtype,"gif") || strstr($filtype,"png")) {  
          $i++; //tell opp bilder
        } //if (strstr($filtype,"jpeg")                
      }//while  
       $i += 1; //øk teller med en for det nye bilde som skal opplastes
       //les og sett filtypen i variabelen $slutt
      if (strstr($filtype,"jpeg")) { //er filen av typen .jpeg?
        $slutt = ".jpeg";
      } else if (strstr($filtype,"jpg")) { //er filen av typen .jpg?
        $slutt = ".jpg";
      } else if (strstr($filtype,"gif")) { //er filen av typen .gif?
        $slutt = ".gif";        
      } else if (strstr($filtype,"png"))  { //er filen av typen .png?
        $slutt = ".png"; 	
      } else { //ikke godkjent filtype 
        /*fil blir lastet opp til server, selv om den ikke blir vist, derfor
          "ødelegg" filnavnet, slik at om noen prøver laste opp f.eks et
          php-script, så blir det ikke kjørbart */
        $slutt = ".xxx"; 
      } //if (strstr($filtype,"jpeg")) 
      $bilde = "IMG_0" . $i . $slutt; //navngir filen, hvis $i = 2 og filtypen er jpg: IMG02.jpg 
      $filnavn = "profilbilde/" . $bilde; //legg filen inn i mappen profilbilde    
      //echo $filnavn;                  
      //kopierer og flytter fil
      move_uploaded_file($temp_fil,$filnavn) or die("En feil oppstod: Kunne ikke kopiere fil");      
      //$storrelse = $_FILES['filbane']['size']; //filens størrelse, foreløpig ikke i bruk
      if (strstr($filtype,"jpeg") || strstr($filtype,"jpg") || strstr($filtype,"gif") || strstr($filtype,"png")) { //vis bilder
        echo "<h3>Bildet ble lastet opp</h3>";
        echo "<img src='$filnavn' height='200'/><br />"; 
        echo "<a href='redigerProfil.php?Profil=profilInfo&bValg=$bilde'>";
        echo "Velg</a>"; 
      } else { //forsøkt å laste opp ugyldig filtype
        echo "<br />Feil under opplastning, tillatte filtyper er: .jpeg, .jpg, .gif og .png";    
      } //if (strstr($filtype,"jpeg")        
    } //if (empty($_FILES['filbane']['name'])) - ferdig med å kopiere og vise info om fil
  } //nyttProfilbilde      
?>