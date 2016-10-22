<?php                                         
  //Webprogrammering i php, s.280 s.296, s.304 og s.309  
  if (empty($_SESSION['email'])) {  //er bruker logget inn?
    echo "<!--Logg inn-->";   
    if (isset($_POST['LOGGINN']) && $_POST['LOGGINN'] == "Logg inn") { //skal bruker logges inn? 
      $tilkobling = kobleTil(); //opprett tilkobling til db
      //sørger for at forsøk på sql-injection blir nullifistert med mysql_real_escape_string
      $email = mysql_real_escape_string($_POST['mail']); //henter e-post            
      $pwd = mysql_real_escape_string($_POST['pwd']); //henter passord      
      $hash = hashPwd($pwd, $email); //hasher passord
      $sql = "SELECT * FROM tblstudent WHERE sEpost = '$email' ";     
      $sql .= "AND sPassord = '$hash';";         
      //echo "<br /> $sql + $hash";
      $resultat = mysql_query($sql, $tilkobling);
      $antall = mysql_num_rows($resultat);
      if ($antall == 1) { //ble e-post og passord funnet?
        $rad = mysql_fetch_assoc($resultat); //forventer kun et resultat, derfor ingen løkke 
        $sId = $rad['sId']; //henter id
        $fnavn = $rad['sFornavn']; //henter fornavn         
        /* man skulle tro at admin/mod sjekker om bruker er utestengt, men oppdaget
        * at dersom flere karantener gjelder (to innenfor samme tidsramme) så får 
        * bruker logget inn uten feilmelding og beskjed om at han/hun er utestengt 
        * derfor la jeg til en "beskyttelse"; LIMIT 1                            */                 
        $karantene_sql = "SELECT * FROM tblStudent, tblKarantene ";
        $karantene_sql .= "WHERE sId = $sId AND sId = ksId ";
        $karantene_sql .= "AND kKaranteneTil >= NOW() "; //NOW() er en SQL funksjon som returnerer dagens dato og tidspunkt
        $karantene_sql .= "LIMIT 1;"; 
        //echo $karantene_sql;        
        $resultat = mysql_query($karantene_sql, $tilkobling);
        $antall = mysql_num_rows($resultat);
        if ($antall == 1) { //bruker har karantene
          $adgang = false; //boolean - nekt adgang             
          while ($rad = mysql_fetch_assoc($resultat)) { 
            $fdato = explode("-", $rad['kKaranteneFra']); //fraDato 
            $fdato = fjernKlokkeslett($fdato); //fjerner klokkeslett fra dato visningen
            $tdato = explode("-", $rad['kKaranteneTil']); //tilDato 
            $tdato = fjernKlokkeslett($tdato); //fjerner klokkeslett fra dato visningen
            $info = $rad['kInfo']; //hent ut informasjon om karantenen                   
          } //while 
        } else { //bruker har ikke karantene
          $adgang = true; //boolean - tillat adgang          
        } //if ($antall == 1)                      
        if ($adgang) { //ingen karantene, la bruker logge inn
          $_SESSION['email'] = $email; //legger variabelen for e-post inn i $_SESSION                     
          $_SESSION['fnavn'] = $fnavn; //legger variabelen for fornanv inn i $_SESSION                   
        } else { //bruker har karantene, gi forklaring
          $tekst = "Du er utestengt fra Hibu Alumni. Grunnlag: \"$info\"<br /><br />";
          $tekst .= "Utestengelsen gjelder fra <strong>$fdato</strong> til <strong>$tdato</strong>.";
          echo logginnSkjema(); //vis skjema for innlogging
        } //if ($adgang)                 
        mysql_close($tilkobling); //lukker tilkoblingen til databasen                
      } else { //logg inn feilet, gi ny mulighet
        $tekst = '<center>
        <table>  
        <tr><td><h2 class="red">Feil brukernavn eller passord.</h2></td></tr>                        
        <tr><td>Du kan prøve igjen, eller:</td></tr>
        <tr><td>Gå til siden for glemt passord <a href="glemtpwd.php">her</a>,
        for å få et nytt</td><td>passord.</td></tr>
        <tr><td>Dersom du er ny bruker, kan du registrere deg her: </td>
        <td>'. NYBRUKER .'</td></tr>
        </table>
        </center>'; //tekst som vises i div id="innhold">
        echo logginnSkjema(); //vis skjema for innlogging      
      }	//if($antall == 1)     
    } else { //har ikke prøvd å logge inn 
      echo logginnSkjema(); //vis skjema for innlogging
    } //if (isset($_POST['LOGGINN'])                    
  } //if(empty($_SESSION['email']))
  
  function logginnSkjema() {
    $skjema = '<form name="innlogget" action="default.php" method="post" onsubmit="return innlogging();">
    <table>
    <tr><td><font size="2pt">E-post:</font></td></tr>
    <tr><td><input type="text" name="mail" id="mail" /></td></tr>
    <tr><td><font size="2pt">Passord:</font></td></tr> 
    <tr><td><input type="password" name="pwd" id="pwd" /></td></tr>
    <tr><td><input type="submit" name="LOGGINN" value="Logg inn"/></td></tr>        
    <tr><td><a href="nybruker.php"><font size="1pt">Ny bruker</font></a></td></tr>  
    <tr><td><a href="glemtpwd.php"><font size="1pt">Glemt passord?</font></a>
    </table>
    </form>
    <!--Luft-->
    <br /><br />
    '; //linjeskift for bedre lesbarhet i kildekoden
    return $skjema;
  } //logginnSkjema
?>