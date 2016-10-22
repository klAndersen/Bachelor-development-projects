<?php
  /************VISNING AV UTGÅTTE, EKSISTERENDE OG KOMMENDE BEGIVENHETER*******/
  function visBegivenhet($sorter) {
    $tilkobling = kobleTil();                        
    $sql = "SELECT * FROM tblBegivenhet, tblFagomrade ";
    $sql .= "WHERE bfId=fId ";     
    if ($sorter == "A-Z") { //skal visning sorteres på tittel?
      $sql .= "ORDER BY begNavn asc;";
    } else if ($sorter == "NY") { //skal visning sorteres fra nyest til eldst?
      $sql .= "ORDER BY begStart desc;";
    } else { //sorteres fra eldst til nyest
      $sql .= "ORDER BY begStart asc;";
    } //if ($sorter = "A-Z")
    //echo $sql;
    $resultat = mysql_query($sql, $tilkobling);
    $antall = mysql_num_rows($resultat);
    if ($antall >= 1) { //det finnes hvertfall en ny nyhet
    echo '<h3>Følgende begivenheter er opprettet:</h3>
          <table>
          <tr>
          <td class="ramme2" style="width:300px;"><strong>Tittel:</strong></td>
          <td class="ramme2" style="width:100px;"><strong>Vises:</strong></td>
          <td class="ramme2" style="width:100px;"><strong>Fjernes:</strong></td> 
          <td class="ramme2" style="width:300px;"><strong>Begivenheten gjelder:</strong></td> 
          <td class="ramme2" style="width:450px;"><strong>Informasjon:</strong></td>
          <td class="ramme2" style="width:150px;"><strong>Redigere?</strong></td>  
          </tr>';          
      while ($rad = mysql_fetch_assoc($resultat)) {     
        $begId = $rad['idBeg'];   
        $fId = $rad['fId'];    
        $bNavn = $rad['begNavn'];
        $bStart = explode("-", $rad['begStart']); 
        $bSlutt = explode("-", $rad['begSlutt']); 
        //splitter opp og re-organiserer visning av dato
        $fraDato = $bStart[2] ."/". $bStart[1] ."-". $bStart[0]; 
        $tilDato = $bSlutt[2] ."/". $bSlutt[1] ."-". $bSlutt[0];
        $bInfo = $rad['begKortInfo'];
        $fNavn = $rad['fNavn'];                        
        echo '<form action="" method="post">
              <tr>
              <td class="ramme2">'.$bNavn.'<input type="hidden" name="begId" value="'.$begId.'" /></td>                                         
              <td class="ramme2">'.$fraDato.'</td>
              <td class="ramme2">'.$tilDato.'</td>
              <td class="ramme2">'.$fNavn.'<input type="hidden" name="idFag" value="'.$fId.'" /></td>
              <td class="ramme2">'.$bInfo.'</td>
              <td class="ramme2"><input type="submit" name="ENDRE" value="Endre begivenhet" /></td>         
              </tr>
              </form>';                               
      } //while
      echo '</table>';
    } else { //ingen aktive/opprettede begivenheter
      echo "Ingen begivenheter er opprettet.";
    } //if ($antall >= 1)
    mysql_close($tilkobling); //lukker tilkobling til db
  } //visBegivenhet
  
  /******************SKJEMA FOR OPPRETTING AV NYE BEGIVENHETER*****************/
  function skjemaNyBegivenhet($feilTittel,$feilFagomrade,$feilKort,$feilBesk, $feilDato) {
    $bNavn = ""; //initiering         
    $bkortInfo = "";
    $bInfo = "";
    if (isset($_POST['LAGRE_BEGIVENHET']) && $_POST['LAGRE_BEGIVENHET'] == "Lagre begivenhet") {
      $bNavn = $_POST['tittel'];             
      $bkortInfo = $_POST['kortInfo'];   
      $bInfo = $_POST['beskrivelse'];
      } //if (isset($_POST['LAGRE_BEGIVENHET'])
    echo '<!--Opprett ny begivenhet-->
          <fieldset>
          <legend>Opprett ny begivenhet</legend>
          <!--Kort forklaring til feltene-->
          <p>
          Her opprettes nye begivenheter, og alle felt må fylles ut.<br />
          Tittel må minst være tre tegn.<br />          
          Kort introduksjon skal oppsummere kort hva begivenheten gjelder, og har grense på 140 tegn, 
          siden denne blir vist i høyre marg.<br />
          På beskriv begivenheten fortelles alle detaljer som omhandler begivenheten, 
          og denne har ingen begrensning.
          </p>
          <br />        
          <table>
          <form action="" method="post" onsubmit="return sjekkNyBegivenhet();">
          <tr><td>Tittel på begivenhet:</td>
          <td><input type="text" id="tittel" name="tittel" value="'.$bNavn.'" size="25" />
          '.$feilTittel.'</td></tr>
          <!--Dato for begivenhet--> 
          <tr><td>Når skal begivenhet vises:</td>
          <td>';
    echo selectDag("fDag",0); //starter på dagens dato
    echo selectMnd("fMnd",0);
    echo selectAar("fAar");
    echo '</td></tr>
          <tr><td>Når skal begivenhet fjernes:</td>
          <td>';
    echo selectDag("tDag",7); //slutter en uke fra dagens dato(kan endres)
    echo selectMnd("tMnd",7);
    echo selectAar("tAar");          
    echo ' '.$feilDato.'</td></tr>'; //lager luft mellom drop-down og feilmelding
    selectFagomrade(true, $feilFagomrade); //settes til true for å starte med "Velg fagområde"   
    echo '<!--Textarea for kort beskrivelse av begivenheten-->
          <tr><td>Kort introduksjon:</td>    
          <td><textarea cols="20" rows="2" id="kortInfo" name="kortInfo">'.$bkortInfo.'</textarea> '
          .$feilKort.'</td></tr>            
          <tr><td>          
          <!--Textarea for innføring av informasjon tilknyttet begivenheten-->
          <tr><td>Beskriv begivenheten:</td>
          <td><textarea cols="20" rows="4" id="beskrivelse" name="beskrivelse">'.$bInfo.'</textarea> '
          .$feilBesk.'</td></tr>  
          <tr><td>
          <input type="submit" id="LAGRE_BEGIVENHET" name="LAGRE_BEGIVENHET" value="Lagre begivenhet" />
          </td>
          <td><input type="reset" value="Tøm feltene" /></td>
          </tr>         
          </form></table>  
          </fieldset>';    
  } //skjemaNyBegivenhet
  
  /*****************************LAGRE BEGIVENHET(ER)*****************************/
  function lagreBegivenhet() { //lagre nye begivenheter   
    //tekst og fagområde  
    $bNavn =  $_POST['tittel'];       
    $bFag = $_POST['idFag']; 
    $bkortInfo = $_POST['kortInfo'];   
    $bInfo = $_POST['beskrivelse'];
    //dato-felt
    $fraAar = $_POST['fAar'];
    $fraMnd = $_POST['fMnd'];
    $fraDag = $_POST['fDag'];
    $tilAar = $_POST['tAar'];
    $tilMnd = $_POST['tMnd'];
    $tilDag = $_POST['tDag'];            
    if (empty($bNavn)) { //er navn på begivenheten skrivd inn?    
      return false; //navn/tittel ikke innskrevet, send feilmelding
    } else if ($fraDag == $tilDag && $fraMnd == $tilMnd && $fraAar == $tilAar) { //er fra/til dato samme dag? 
      return false; //fra/til dato, mnd og år er like, send feilmelding
    } else if ($fraDag > $tilDag && $fraMnd == $tilMnd && $fraAar == $tilAar) { 
      //er fradato etter sluttdato(hvis varighet er innenfor samme mnd)?
      return false; //fra-dag er etter sluttdag (innen for samme år og mnd), send feilmelding       
    } else if ($fraMnd > $tilMnd && $fraAar == $tilAar) {
      return false; //er fra mnd etter sluttmnd innenfor samme år, send feilmelding
    } else if ($fraAar > $tilAar) { //er fra-År etter til-År?
        return false; //start-År er etter slutt-År, send feilmelding         
    } else if ($bFag == "Intet valgt") { //er fagområde valgt?
      return false; //fagområde er ikke valgt, send feilmelding   
    } else if (empty($bInfo) || empty($bkortInfo)) { //er informasjon om begivenheten utfylt?
      return false; //informasjon er ikke skrevet inn, send feilmelding  
    } else { //alt ok, start registrering av begivenhet    
      //setter sammen og legger til skilletegn for at datoene skal lagres korrekt i db  
      $bStart = $fraAar ."-". $fraMnd ."-". $fraDag;
      $bSlutt = $tilAar ."-". $tilMnd ."-". $tilDag;          
      insertBegivenhet($bNavn, $bStart, $bSlutt, $bFag); //sender over parameterne for å legges inn i db
    } //if (empty($bNavn))    
    return true;    
  } //lagreBegivenhet  
  
  function insertBegivenhet($bNavn, $bStart, $bSlutt, $bFag) {
    $email = $_SESSION['email'];
    $tilkobling = kobleTil(); //oppretter tilkobling til databasen
    //rensker for mulig SQL-injection
    $bkortInfo = mysql_real_escape_string($_POST['kortInfo']);    
    $bInfo = mysql_real_escape_string($_POST['beskrivelse']);
    //egenlagd funksjon for renskning av html                 
    $bkortInfo = rensHtmlUTF8($bkortInfo, 3);    
    $bInfo = rensHtmlUTF8($bInfo, 3);
    //hent ut den som lager/oppretter begivenhet
    $sql = "SELECT sId FROM tblStudent WHERE sEpost = '$email';";      
    $resultat = mysql_query($sql, $tilkobling);
    while($rad = mysql_fetch_assoc($resultat)) {
      $sId = $rad['sId'];
    } //while      
    $ins_sql = "INSERT INTO tblbegivenhet ";
    $ins_sql .= "VALUES (null, '$bNavn', '$bStart', '$bSlutt', '$bkortInfo', '$bInfo', $sId, $bFag);";
    //echo $ins_sql . "<br />";
    mysql_query($ins_sql,$tilkobling);
    echo "$bNavn ble opprettet."; //gi tilbakemelding til bruker om at begivenhet har blitt opprettet
    mysql_close($tilkobling); //lukker tilkobling til db
  } //insertBegivenhet
  
  /*********************************SETT FOCUS**********************************/
  function settFocus($feilTittel, $feilFagomrade,$feilKort,$feilBesk) { 
    $focus = "focusTittel();"; //default verdi for focus   
    if (!empty($feilTittel)) { //er tittel ok?
      $focus = "focusTittel();";
    } else if (!empty($feilFagomrade)) { //er fagområdet ok?
      $focus = "focusFagomrade();";
    } else if (!empty($feilKort)) { //er kort info om begivenheten ok?
      $focus = "focusKortInfo();";
    } else if (!empty($feilBesk)) { //er beskrivelse av begivenhet ok?
      $focus = "focusBeskrivelse();";
    } //if (!empty($feilTittel))
    return $focus; //returner focus
  } //settFocus
                                        
  /******************REDIGERING AV BEGIVENHET(ER)***********************/ 
  function skjemaRedigerBegivenhet($feilTittel,$feilFagomrade,$feilKort,$feilBesk,$feilDato) {
    $bId = $_POST['begId'];
    $tilkobling = kobleTil(); //oppretter tilkobling til databasen
    $sql = "SELECT * FROM tblBegivenhet WHERE idBeg = $bId";    
    $resultat = mysql_query($sql, $tilkobling);    
    $rad = mysql_fetch_assoc($resultat);     
    $bNavn = $rad['begNavn'];
    $bkortInfo = $rad['begKortInfo'];
    $bInfo = $rad['begInfo'];    
    $bStart = explode("-", $rad['begStart']); 
    $bSlutt = explode("-", $rad['begSlutt']);      
    mysql_close($tilkobling); //lukker tilkobling til db 
    echo '<!--Rediger en begivenhet-->
          <fieldset>
          <legend>Rediger en begivenhet</legend>
          <!--Kort forklaring til feltene-->
          <p>
          Alle felt må være utfylt.<br /> 
          Tittel må minst være tre tegn.<br />         
          Kort introduksjon skal oppsummere kort hva begivenheten gjelder, og har grense på 140 tegn, 
          siden denne blir vist i høyre marg.<br />
          På beskriv begivenheten fortelles alle detaljer som omhandler begivenheten, 
          og denne har ingen begrensning.
          </p>
          <br />
          <table>
          <form action="" method="post" onsubmit="return sjekkNyBegivenhet();">
          <tr><td>Tittel på begivenhet:</td>
          <td><input type="text" id="tittel" name="tittel" value="'.$bNavn.'" size="25" />
          '.$feilTittel.'</td></tr>
          <!--Dato for begivenhet--> 
          <tr><td>Når skal begivenhet vises:</td>
          <td>';        
    echo redigerDag("fDag", $bStart[2], $bStart[1]);
    echo redigerMnd("fMnd", $bStart[1]);
    echo redigerAar("fAar", $bStart[0]);
    echo '</td></tr>
          <tr><td>Når skal begivenhet fjernes:</td>
          <td>';
    echo redigerDag("tDag", $bSlutt[2], $bSlutt[1]);
    echo redigerMnd("tMnd", $bSlutt[1]);
    echo redigerAar("tAar", $bSlutt[0]);
    echo ' '.$feilDato.'</td></tr>'; //lager luft mellom drop-down og feilmelding               
    selectFagomrade(true, $feilFagomrade); //setter fagområdet
    echo '<!--Textarea for kort beskrivelse av begivenheten-->
          <tr><td>Kort om begivenheten:</td>
          <td><textarea cols="20" rows="2" id="kortInfo" name="kortInfo">'.$bkortInfo.'</textarea> '
          .$feilKort.'</td></tr>  
          <tr><td>          
          <!--Textarea for innføring av informasjon tilknyttet begivenheten-->
          <tr><td>Hva skjer på begivenheten:</td>
          <td><textarea cols="20" rows="4" id="beskrivelse" name="beskrivelse">'.$bInfo.'</textarea> '
          .$feilBesk.'</td></tr>  
          <tr><td><input type="hidden" name="begId" value="'.$bId.'" /></td></tr>  
          <tr><td>
          <input type="submit" id="LAGRE_ENDRING" name="LAGRE_ENDRING" value="Lagre endringer" />
          </td>
          <td><input type="reset" value="Tøm feltene" /></td>
          </tr>         
          </form></table>
          </fieldset>';       
  } //skjemaRedigerBegivenhet
  
  function updateBegivenhet() {
    $tilkobling = kobleTil(); //oppretter tilkobling til databasen
    $idBeg = $_POST['begId'];
    $bfId = $_POST['idFag']; 
    $fraAar = $_POST['fAar'];
    $fraMnd = $_POST['fMnd'];
    $fraDag = $_POST['fDag'];
    $tilAar = $_POST['tAar'];
    $tilMnd = $_POST['tMnd'];
    $tilDag = $_POST['tDag'];   
    //rensker for mulig SQL-injection
    $bNavn =  mysql_real_escape_string($_POST['tittel']);     
    $bkortInfo = mysql_real_escape_string($_POST['kortInfo']);    
    $bInfo = mysql_real_escape_string($_POST['beskrivelse']);
    //egenlagd funksjon for renskning av html   
    $bNavn =  rensHtmlUTF8($bNavn, 3);                  
    $bkortInfo = rensHtmlUTF8($bkortInfo, 3);    
    $bInfo = rensHtmlUTF8($bInfo, 3);
    if (empty($bNavn)) { //er navn på begivenheten skrivd inn?    
      return false; //navn/tittel ikke innskrevet, send feilmelding
    } else if ($fraDag == $tilDag && $fraMnd == $tilMnd && $fraAar == $tilAar) { //er fra/til dato samme dag? 
      return false; //fra/til dato, mnd og år er like, send feilmelding
    } else if ($fraDag > $tilDag && $fraMnd == $tilMnd && $fraAar == $tilAar) { 
      //er fradato etter sluttdato(hvis varighet er innenfor samme mnd)?
      return false; //fra-dag er etter sluttdag (innen for samme år og mnd), send feilmelding       
    } else if ($fraMnd > $tilMnd && $fraAar == $tilAar) {
      return false; //er fra mnd etter sluttmnd innenfor samme år, send feilmelding
    } else if ($fraAar > $tilAar) { //er fra-År etter til-År?
        return false; //start-År er etter slutt-År, send feilmelding           
    } else if (empty($bInfo) || empty($bkortInfo)) { //er informasjon om begivenheten utfylt?
      return false; //informasjon er ikke skrevet inn, send feilmelding 
    } else { //felter er fylt ut, oppdater begivenhet
      $bStart = $fraAar ."-". $fraMnd ."-". $fraDag;
      $bSlutt = $tilAar ."-". $tilMnd ."-". $tilDag;  
      //update setning for oppdatering av begivenhet
      $update_sql = "UPDATE tblBegivenhet ";
      $update_sql .= "SET begNavn = '$bNavn', begKortInfo = '$bkortInfo', begInfo = '$bInfo', ";
      $update_sql .= "bfId = $bfId, begStart = '$bStart', begSlutt = '$bSlutt' ";
      $update_sql .= "WHERE idBeg = $idBeg;";
      //echo $update_sql;
      mysql_query($update_sql, $tilkobling);
      echo "Begivenheten ble endret.";
    } //if (empty($bNavn))
    mysql_close($tilkobling); 
    return true;
  } //updateBegivenhet
  
  /*
   *
   * Følgende funksjoner er basert på Select'ne som ligger i funksjoner.inc.php,
   * men har blitt tilpasset for redigering/endring av begivenheten
   *    
   */
  /**********************SELECT FOR REDIGERING AV DAG**************************/
  function redigerDag($name, $dag, $mnd) { 
    $maxDag = hentMaxAntallDager($mnd); //hvor mange dager skal vises totalt?                           
    //start oppretting av select for dag, starter på satt dag           
    $selectDag = '<select id="'.$name.'" name="'.$name.'">
                  <option value="'.$dag.'">'.$dag.'</option>';
    $i = $dag; //$i's startverdi settes lik dagens dato
    $i++; //øk $i så samme dato ikke vises to ganger                                           
    while ($i < $maxDag) { //fyller med resten av dagene      
      $selectDag .= "<option value='$i'>$i</option>";
      $i++; //øk $i
    } //while
    //hvilken dag startet select på?    
    if ($i > 2) { //er dagens dato den 03. eller høyere?
      $i = $dag; //sett $i sin verdi til dagens dato 
      $j = 1; //brukes for å fylle opp select fra dag 1 etter resterende dager av gjeldende måneder 
      while ($j < $i) { //fyll opp med dagene før dagens dato
        $selectDag .= "<option value='$j'>$j</option>";
        $j++; //øk $j
      } //while
    } //if ($i > 2)
    $selectDag .= '</select> / '; //avslutter Select og legger til skilletegn  
    return $selectDag; //returner select med datoer
  } //redigerDag             
  
  /*************************SELECT FOR REDIGERING AV MÅNED***********************/
  function redigerMnd($name, $mnd) {               
    $selectMnd = '<select id="'.$name.'" name="'.$name.'">
                  <option value="'.$mnd.'">'.$mnd.'</option>';  
    $i = $mnd; //$i's startverdi er 1
    $i++; //øk $i så vi ikke får to like måneder                      
    while ($i < 13) { //fyll opp med resterende måneder
      $selectMnd .= "<option value='$i'>$i</option>";
      $i++; //øk $i
    } //while 
    if ($i > 2) { //er måned etter februar ($i er 3 eller større)?
    	$i = $mnd; //sett $i sin verdi til gjeldende mnd 
    	$j = 1; //brukes for å fylle opp select fra første måned etter resterende måneder av gjeldene år
      while ($j < $i) { //fyll select med månedene før gjeldende måned
        $selectMnd .= "<option value='$j'>$j</option>";
        $j++; //øk $j
      } //while        
    } //if ($i > 2)    
    $selectMnd .= '</select> - '; //avslutter Select og legger til skilletegn
    return $selectMnd;    
  } //redigerMnd   
  
  /************************SELECT FOR REDIGERING AV ÅR*************************/
  function redigerAar($name, $aar) {    
    $aar_max = $aar + 100; //viser 100 år frem i tid 
    $selectAar = '<select id="'.$name.'" name="'.$name.'">
                  <option value="'.$aar.'">'.$aar.'</option>';
    $aar++; //øk år med 1      
    while ($aar < $aar_max) {
      $selectAar .= "<option value='$aar'>$aar</option>";
      $aar++; //øk $aar
    } //while  
    $selectAar .= '</select>';  
    return $selectAar;     
  } //redigerAar
?>