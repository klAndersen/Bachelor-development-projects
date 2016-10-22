<?php 
  /****************************HENT/VIS KARANTENE(R)****************************/
  function hentKarantene($sId) {
    $tilkobling = kobleTil(); //kobler til db
    $sql = "SELECT * FROM tblStudent ";
    $sql .= "WHERE sId = $sId";
    $resultat = mysql_query($sql,$tilkobling);
    $antall = mysql_num_rows($resultat);
    //echo "$sql <br />";
    if ($antall >= 1) { //finnes bruker? (nødvendig sjekk siden id hentes fra url)
      $sql = "SELECT * FROM tblStudent, tblKarantene ";
      $sql .= "WHERE sId = $sId AND ksId = $sId";  
      $resultat = mysql_query($sql,$tilkobling);
      $antall = mysql_num_rows($resultat);
      //echo "$sql <br />";
      if ($antall >= 1) { //det finnes minst 1 karantene registrert på valgt bruker
        $utskrift = "Følgende karantener er registrert: <br /><br />
        "; //linjeskift for bedre lesbarhet i kildekoden
        while ($rad = mysql_fetch_assoc($resultat)) {          
          $kKaranteneFra = explode("-", $rad['kKaranteneFra']); //fraDato
          $kKaranteneFra = splittDatoOgKlokkeslett($kKaranteneFra);
          $kInfo = $rad['kInfo'];//info om karanten
          $kKaranteneTil = explode("-", $rad['kKaranteneTil']); //tilDato
          $kKaranteneTil = splittDatoOgKlokkeslett($kKaranteneTil);
          $fnavn = $rad['sFornavn'];
          $enavn = $rad['sEtternavn']; 
          $utskrift .= "<strong>Navn:</strong> $fnavn $enavn<br />
          <strong>Karantene fra:</strong> $kKaranteneFra<br />
          <strong>Karantene til:</strong> $kKaranteneTil<br />
          <strong>Informasjon:</strong> $kInfo<br /><br />";    
        } //while
      } else { //bruker har ingen karantener
        $utskrift = "Denne brukeren har ingen utestengelser så langt.";
      } //if ($antall >= 1) - har bruker karantener?
    } else { //bruker valgt finnes ikke
      $utskrift = "Obs! Denne brukeren finnes ikke...<br />
                  Vennligst velg brukere <a href='admin_brukere.php'>herfra</a>";
    } //if ($antall >= 1) - finnes bruker?
    mysql_close($tilkobling); //lukker tilkobling til db
    return $utskrift;
  } //hentKarantene
  
  /****************************SKJEMA FOR KARANTENER****************************/
  function skjemaKarantene($sId, $feilFnavn, $feilEnavn, $feilDato, $feilInfo) {
    $tilkobling = kobleTil();
    $sql = "SELECT sFornavn, sEtternavn FROM tblStudent ";
    $sql .= "WHERE sId = $sId;";
    $resultat = mysql_query($sql,$tilkobling);
    $rad = mysql_fetch_assoc($resultat); //forventer kun et resultat, derfor ingen løkke 
    $fnavn = $rad['sFornavn'];
    $enavn = $rad['sEtternavn'];    
    echo '<!--Skjema for å sette bruker i karantene-->
          <fieldset><legend>Sett bruker i karantene</legend>
          <form method="post" action="" onsubmit="return sjekkKarantene();">
          <table><tr>
          <!--Navn-->
          <td>Fornavn:</td> <td><input type="text" id="fnavn" name="fnavn" value="'.$fnavn.'" /> '.$feilFnavn.'</td>
          </tr>
          <tr> 
          <td>Etternavn:</td> <td><input type="text" id="enavn" name="enavn" value="'.$enavn.'" /> '.$feilEnavn.'</td>
          </tr>
          <tr>
          <!--Dato for karantene-->
          <td>Karantene fra:</td>
          <td>';   
    echo selectDag("fDag", 0); //dagens dato
    echo selectMnd("fMnd", 0);
    echo selectAar("fAar");
    echo '</td></tr>
          <tr>
          <td>Karantene til:</td>
          <td>';
    echo selectDag("tDag", 7); //setter tildato for karantene gjelder minst en uke frem i tid
    echo selectMnd("tMnd", 7);
    echo selectAar("tAar");          
    echo ' '.$feilDato.'</td></tr>
          <!--Informasjon om karanten-->
          <tr><td>Informasjon:</td> 
          <td><textarea cols="25" rows="3" id="info" name="info"></textarea> '.$feilInfo.'</td>
          </tr></table><table><tr>           
          <td><input type="submit" name="UTESTENG" value="Utesteng bruker" /></td>
          </tr></table></form></fieldset>';
    mysql_close($tilkobling); //lukker tilkobling til db
  } //skjemaKarantene
  
  /***************************SETT BRUKER I KARANTENE***************************/
  function settKarantene($sId) { 
    $tilkobling = kobleTil(); //kobler til db   
    $fnavn = mysql_real_escape_string($_POST['fnavn']);
    $enavn = mysql_real_escape_string($_POST['enavn']);
    $fraAar = $_POST['fAar'];
    $fraMnd = $_POST['fMnd'];
    $fraDag = $_POST['fDag'];
    $tilAar = $_POST['tAar'];
    $tilMnd = $_POST['tMnd'];
    $tilDag = $_POST['tDag'];
    $kInfo = mysql_real_escape_string($_POST['info']); 
    $kInfo = rensHtmlUTF8($kInfo,3);   
    if (empty($fnavn) || empty($enavn)) { //er feltene for navn utfylt?
      return false; //felt for navn er tomt, send feilmelding  
    } else if ($fraDag == $tilDag && $fraMnd == $tilMnd && $fraAar == $tilAar) { //er fra/til dato samme dag? 
      return false; //fra/til dato, mnd og år er like, send feilmelding
    } else if ($fraDag > $tilDag && $fraMnd == $tilMnd && $fraAar == $tilAar) { 
      //er fradato etter sluttdato(hvis varighet er innenfor samme mnd)?
      return false; //fra-dag er etter sluttdag (innen for samme år og mnd), send feilmelding       
    } else if ($fraMnd > $tilMnd && $fraAar == $tilAar) {
      return false; //er fra mnd etter sluttmnd innenfor samme år, send feilmelding
    } else if ($fraAar > $tilAar) { //er fra-År etter til-År?
      return false; //start-År er etter slutt-År, send feilmelding         
    } else if (empty($kInfo)) { //er informasjon om utestengelsen utfylt?
      return false; //info om karantene er ikke utfylt, send feilmelding  
    } else { //alt ok, start registrering av karantene
      $tid = date("H:i:s"); //henter ut tidspunktet
      //setter sammen og legger til skilletegn for at datoene skal lagres korrekt i db       
      $kKaranteneFra = $fraAar ."-". $fraMnd ."-". $fraDag ." ". $tid; 
      $kKaranteneTil = $tilAar ."-". $tilMnd ."-". $tilDag;
      $insert_sql = "INSERT INTO tblKarantene VALUES ";
      $insert_sql .= "(null, '$kKaranteneFra', '$kInfo', '$kKaranteneTil', $sId);";
      //echo $insert_sql . "<br />";
      mysql_query($insert_sql,$tilkobling);
      echo "$fnavn $enavn ble satt i karantene."; //gi tilbakemelding
      mysql_close($tilkobling); //lukker tilkobling til db  
      return true;          
    } //if (empty($fnavn) || empty($enavn))    
  } //settKarantene  
  
  /*********************************SETT FOCUS**********************************/
  function settFocus($feilFnavn, $feilEnavn, $feilInfo) {    
    $focus = "focusKaranteneInfo();"; //default verdi for focus   
    if (!empty($feilFnavn)) { //er fornavn ok?
      $focus = "focusFnavn();"; //setter fokus på fornavn
    } else if (!empty($feilEnavn)) { //er etternavn ok?
      $focus = "focusEnavn();";
    } else if (!empty($feilInfo)) { //er info om karantene ok?
      $focus = "focusKaranteneInfo();";
    } //if (!empty($feilFnavn))
    return $focus;
  } //settFocus  
?>    