<?php
  function visNyBrukerSkjema($feilFnavn,$feilEnavn,$feilEmail, $feilCampus, $feilFag) {
    $fnavn = ""; //initiering
    $enavn = "";
    $email = "";
    //ble en av knappene trykket? 
    if (isset($_POST['SJEKK_INFO']) || isset($_POST['REGISTRER']) || isset($_POST['ENDRE'])) {
      //isåfall hent informasjon, så de kan legges i feltene dersom den registrerende kommer tilbake hit...
      $fnavn = $_POST['fnavn'];
      $enavn = $_POST['enavn'];
      $email = $_POST['email'];  
    } //if (isset($_POST['SJEKK_INFO'])
    echo '<!--Registreringsskjema-->
    <p>
    <fieldset>
    <legend>Registreringsinfo</legend>
    <p>
    Her kan du registrere deg som ny bruker på Alumni Hibu. Alle felt utenom årskull må fylles ut.<br />
    Krav til fornavn er at det er minst to bokstaver, 
    og krav til etternavn er at det er minst tre bokstaver.<br />
    E-posten du oppgir må være gyldig og i bruk, da informasjon blir sendt til denne 
    e-posten.<br />
    Videre må du velge campuset og fagområdet du studerte. Med årskull menes året som du startet studiet.<br />           
    Det anbefales at du leser og setter deg inn i vår policy.       
    </p>
    <br />
    <form action="" method="post" onsubmit="return sjekkRegistrering();">
    <table>
    <!--Personlig info-->
    <tr><td>Fornavn:</td>
    <td><input type="text" id="fnavn" name="fnavn" value="'.$fnavn.'" maxlength="30" /> '
    .$feilFnavn.'</td></tr>
    <tr><td>Etternavn:</td>
    <td><input type="text" id="enavn" name="enavn" value="'.$enavn.'" maxlength="40" /> '
    .$feilEnavn.'</td></tr>
    <tr><td>E-post:</td>
    <td><input type="text" id="email" name="email" value="'.$email.'" maxlength="80" /> '
    .$feilEmail.'</td></tr>
    </table>
    <br />
    <table>
    '; //legger til feilmelding(er) (hvis det er noen) og tidligere innført info (hvis feil)  
    //og linjeskift for bedre oversikt i kildekoden     
    selectCampus(true, $feilCampus); //select for campus, true pga det skal vises for nye brukere 
    selectFagomrade(true, $feilFag); //select for fagområde, true pga det skal vises for nye brukere 
    selectAarskull(true); //select for årskull, true pga det skal vises for nye brukere                          
    echo '
    </table>
    <br />
    <!--Knapper og policy-->
    <p>
    Les vår <a onclick="visPolicy();" style="cursor: pointer; text-decoration: underline;">policy</a>
    <font size="1pt">(Åpnes i nytt vindu)</font>
    </p>
    <table>
    <tr>
    <td><input type=submit value="Kontroller informasjon" name="SJEKK_INFO" /></td>  
    <td><input type="reset" value="Tøm skjema" /></td>
    </tr>
    </table>
    </form>
    </fieldset>
    '; //linjeskift for bedre oversikt i kildekoden
  } //visNyBrukerSkjema
    
  function visOppsummeringsSkjema() {
    $campus = ""; //initierer variabelene pga feilmelding (Notice: Undefined variable...)
    $fag = "";                
    echo '<br />
    <fieldset>
    <legend>Oversikt - sjekk at informasjonen oppgitt stemmer</legend>      
    <form action="" method="post" onsubmit="return sjekkRegistrering();">
    <table>                                      
    <tr><td>Fornavn:</td><td>'. $_POST['fnavn'] .'</td></tr>
    <tr><td>Etternavn:</td><td>'.  $_POST['enavn'] .'</td></tr>
    <tr><td>E-mail:</td><td>'. $_POST['email'] .'</td></tr>
    '; //linjeskift for bedre oversikt i kildekoden    
    if (($_POST['idCampus'] != "Intet valgt") && ($_POST['idFag'] != "Intet valgt")) { //er disse satt?      
      $tilkobling = kobleTil(); //opprett tilkobling til db
      //for å vise bruker hvilken campus og fagområde som ble valgt
      $sql = "SELECT fNavn, cNavn FROM "; 
      $sql .= "tblFagomrade, tblCampus WHERE ";
      $sql .= " fId = ". $_POST['idFag'] ." AND cId = ". $_POST['idCampus'] .";";
      //echo $sql;
      $resultat = mysql_query($sql, $tilkobling);
      $rad = mysql_fetch_assoc($resultat); //forventer kun et resultat, derfor ingen løkke 
      $campus = $rad['cNavn'];
      $fag = $rad['fNavn'];      
      mysql_close($tilkobling); //lukker tilkobling til db
    } //if (($_POST['idcampus']))
    echo '<tr><td>Campus:</td><td>'.$campus.'</td></tr>
    <tr><td>Fagområde:</td><td>'.$fag.'</td></tr>
    <tr><td>Årskull:</td><td>'.$_POST['aarskull'].'</td></tr>';
    //lager skjulte elementer for å videreføre den registrerte informasjonen til en insert-sql
    echo '<tr><td><input type="hidden" value="' . $_POST['fnavn'] . '" id="fnavn" name="fnavn" /></td></tr>
    <tr><td><input type="hidden" value="' . $_POST['enavn'] . '" id="enavn" name="enavn" /></td></tr>
    <tr><td><input type="hidden" value="' . $_POST['email'] . '" id="email" name="email" /></td></tr>
    <tr><td><input type="hidden" value="' . $_POST['idCampus'] . '" id="idCampus" name="idCampus" /></td></tr>
    <tr><td><input type="hidden" value="' . $_POST['idFag'] . '" id="idFag" name="idFag" /></td></tr>
    <tr><td><input type="hidden" value="' . $_POST['aarskull'] . '" id="aarskull" name="aarskull" /></td></tr>
    '; //linjeskift for bedre oversikt i kildekoden
    echo '</table>
    <!--Knapper--> 
    <input type="submit" value="Registrer deg" name="REGISTRER" />
    <input type="submit" value="Endre?" name="ENDRE"  /> 
    </form> 
    </fieldset>
    '; //linjeskift for bedre oversikt i kildekoden     
  } //visOppsummeringsSkjema
  
  /*****************************SETT FOCUS**************************************/
  function settFocus($feilFnavn,$feilEnavn,$feilEmail, $feilCampus, $feilFag) {
    $focus = "focusFnavn();"; //default focus verdi
    if (!empty($feilFnavn)) { //er fornavn ok?
      $focus = "focusFnavn();"; //sett focus i feltet for fornavn
    } else if (!empty($feilEnavn)) { //er etternavn ok?
      $focus = "focusEnavn();"; //sett focus i feltet for etternavn
    } else if (!empty($feilEmail)) { //er e-post ok?	
      $focus = "focusMail();"; //sett focus i feltet for e-post
    } else if (!empty($feilCampus)) { //er campus ok?  
      $focus = "focusCampus();"; //sett focus i select for campus
    } else if (!empty($feilFag)) { //er fagområde ok?  
      $focus = "focusFagomrade();"; //sett focus i select for fagområde
    } //if (!empty($feilFnavn))
    return $focus; //returner focus
  } //settFocus           
?>                                                     