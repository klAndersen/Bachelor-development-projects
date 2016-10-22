<?php
  /********************************ENDRE PASSORD********************************/ 
  function endrePwd() {     
    $gamPwd = $_POST['gpwd']; 
    $nyttPwd = $_POST['nPwd']; 
    $bekreftPwd = $_POST['bnPwd'];         
    if (empty($gamPwd) || empty($nyttPwd) || empty($bekreftPwd)) { //er passord-felt(ene) tomme?
      return false; //et eller flere felt er tomme, send feilmelding
    } else if (strlen($nyttPwd) < 6) { //er nytt passord for kort?
      return false; //nytt passord er for kort, send feilmelding
    } else if (strlen($nyttPwd) > 20) { //er nytt passord for langt?
      return false; //nytt passord er for langt, send feilmelding
    } else if ($nyttPwd != $bekreftPwd) { //er nytt og gjentatt passord like?
      return false; //nye passord er ikke like, send feilmelding
    } else { //alt ok så langt        
      return oppdaterPwd(); //returnerer resultatet av oppdateringen                    
    } //if (empty($gamPwd) || empty($nyttPwd) || empty($bekreftPwd))
  } //endrePwd
  
  function endrePwdSkjema($feilGamPwd, $feilNyPwd, $feilBekPwd) {  
    $pwd = "";
    if (isset($_GET['pwd'])) { //er det en ny bruker?
      $pwd = $_GET['pwd']; //legg passordet i gammelt passord    
    } //if (isset($_GET['pwd']))  
    echo '<!--Skjema for endring av passord-->       
          <p>          
          <fieldset>
          <legend>Skjema for endring av passord</legend>
          <p>
          Alle felt må fylles ut. <br />
          Krav til nytt passord er at det må minimum være 6 tegn og maksimum 20 tegn.<br />
          Det anbefales å bruke både tegn og tall for å få et mest mulig sikkert passord.
          <p/>
          <br />
          <p>
          <form action="" method="post" onsubmit="return sjekkPwd();">
          <table>                                      
          <tr><td>Skriv inn gammelt passord:</td>
          <td><input type="password" id="gPwd" name="gpwd" value="'.$pwd.'" /> '.$feilGamPwd.'</td></tr>
          <tr><td>Skriv inn nytt passord:</td>
          <td><input type="password" id="nPwd" name="nPwd" /> '.$feilNyPwd.'</td></tr>
          <tr><td>Gjenta nytt passord:</td>
          <td><input type="password" id="bnPwd" name="bnPwd" /> '.$feilBekPwd.'</td></tr>
          <tr></tr>     
          <tr><td><input type="submit" value="Lagre nytt passord" name="LAGRE" /></td></tr>
          </table>
          </form>
          </p>
          </fieldset>            
          </p>';
  } //endrePwdSkjema
  
  function oppdaterPwd() {
    $tilkobling = kobleTil(); //opprett tilkobling til db
    $gamPwd = mysql_real_escape_string($_POST['gpwd']); 
    $nyttPwd = mysql_real_escape_string($_POST['nPwd']);
    $email = $_SESSION['email'];  //hent brukers -epost     
    $hash = hashPwd($gamPwd, $email); //omgjør passordet som skrives inn til en hash
    $sql = "SELECT sEpost, sPassord FROM tblstudent WHERE sEpost = '$email' ";
    $sql .= "AND sPassord = '$hash'; ";
    //$tekst =  "<br /> $sql";
    $resultat = mysql_query($sql, $tilkobling);
    $antall = mysql_num_rows($resultat);
    if ($antall == 1) { //overenstemmelse, oppdater passord i databasen      
      $nyHash = hashPwd($nyttPwd, $email); 
      $sql = "UPDATE tblstudent ";
      $sql .= "SET sPassord = '$nyHash' ";
      $sql .= "WHERE sEpost = '$email';"; 
      mysql_query($sql,$tilkobling);       
      //echo $sql;
      echo "Ditt passord er nå endret."; //tilbakemelding
      mysql_close($tilkobling); //lukker tilkobling til db
      return true; //alt gikk ok, send tilbakemelding
    } //if ($antall == 1)
    return false; //nytt passord ble ikke lagret
  } //oppdaterPwd
  
  /********************************GLEMT PASSORD********************************/
  function glemtPwd() {
    //grunnlaget er fra Webprogrammering i php s.380
    $tilkobling = kobleTil(); //opprett tilkobling til db 
    $epost = mysql_real_escape_string($_POST['email']);  
    //for disse to linjene, se Kildereferanse: Sjekking av e-post i php (1) & (2)
    $regex = "/^[_\.0-9a-zA-Z-]+@([0-9a-zA-Z][0-9a-zA-Z-]+\.)+[a-zA-Z]{2,6}$/i";     
    $valid = preg_match($regex, $epost); //sjekk likhet
    if (empty($epost)) { //er e-post feltet er blankt...          
      return false; //e-post er ikke innskrevet, send feilmelding
    }  else if (!$valid) { //er e-post gyldig?
      return false; //e-post er ikke gyldig, send feilmelding
    } else { //e-post er ok
      $pwd = lagPwd(); //kaller på lag passord (genererer et tilfeldig passord)  
      $sql = "SELECT sEpost FROM tblstudent WHERE sEpost = '$epost';"; 
      $resultat = mysql_query($sql, $tilkobling);
      $antall = mysql_num_rows($resultat);
      if ($antall == 0) { //Ikke registrert
        echo "E-posten <strong>$epost</strong> er ikke registrert hos oss. " . GLEMT_PWD;
        return true; //e-posten finnes ikke, gi tilbakemelding
      } else { //er registrert    
        $hash = hashPwd($pwd, $epost); //omgjør passordet til en hash 
        $sql = "UPDATE tblStudent ";
        $sql .= "SET sPassord = '$hash' ";
        $sql .= "WHERE sEpost='$epost';"; 
        mysql_query($sql,$tilkobling);
        //echo "sql: $sql <br /> pwd: $pwd <br />"; 
        mysql_close($tilkobling); //lukker tilkobling til db    
        //$testMail = "AdminHibu@localhost"; //for testformål
        $link = "<a href='http://localhost/Nettsider/utvoppg/endrepwd.php?bruker=$epost&pwd=$pwd'>Logg inn og endre passord</a>"; 
        $emne = 'Glemt passord';
        $innhold = "Hei. <br />";
        $innhold .= "Det har blitt sendt ut en etterspørsel om nytt passord.<br />";
        $innhold .= "Ditt nye passord er: <font color='red'>$pwd</font><br />"; //passord merkes i rødt
        $innhold .= "Vi anbefaler deg å skifte passord etter neste innlogging.<br />";
        $innhold .= "Eller du kan logge inn og endre ved å benytte denne linken:<br />";
        $innhold .= "$link"; //legger ved link i e-post 
        $innhold .= "<br /><br />"; //blanke linjer for å skille ad
        $innhold .= "Dersom du ikke sendte denne forespørselen, anbefaler vi at du <br />";
        $innhold .= "logger inn snarest og endrer passord for å unngå misbruk.";                
        $fra = 'From: postmaster@localhost'; //avsender        
        email_utf8($epost, $emne, $innhold, $fra); //konverterer e-post til utf-8
        echo "Nytt passord er sendt til $epost. " . FORSIDE; //gi tilbakemelding             
        return true; //alt ok - nytt passord sendt 
      } //if ($antall == 0)
    } //if (empty($epost))
  } //glemtPwd
  
  function glemtPwdSkjema($feilEmail) {
    echo 'Glemt passord? <br />              
    <form name="glemt_passord" action="" method="post" onsubmit="return sjekkMail();">
    '; //linjeskift for bedre oversikt i kildekoden
    if(empty($feilEmail)) { //er feilmelding tom?
      echo "Skriv inn e-epost og klikk Send, så kommer nytt passord til din e-post: "; 
    } else { //noe er feil
      echo "$feilEmail <font color=red>Skriv inn e-post: </font>";
    } //if(empty($feilEmail))                       
    echo '<input type="text" name="email" id="email" />
    <input type="submit" name="GLEMT_PWD" value="Send passord" /><br />
    </form>'; 
  } //glemtPwdSkjema    
?>