<?php        
  /******************************VIS CAMPUS************************************/
  function visCampus() {
    $tilkobling = kobleTil(); //koble til db
    $sql = "SELECT * FROM tblCampus ORDER BY cNavn asc;";
    $resultat = mysql_query($sql, $tilkobling);
    echo '<table>';
    while ($rad = mysql_fetch_assoc($resultat)) {      
      $campusId = $rad['cId'];
      $campusNavn = $rad['cNavn'];
      //oppretter liste over campus med rediger og slett knapp
      echo '<form action="admin_campus.php?campus=redigerCampus" method="post">
      <tr><td class="ramme2" style="width:200px;">' . $campusNavn . 
      '<input type="hidden" name="campusId" value="'.$campusId.'"/>
      <input type="hidden" name="campusNavn" value="'.$campusNavn.'"/></td>
      <td class="ramme2"><input type="submit" name="REDIGER_CAMPUS" value="Rediger" /></td>
      <td class="ramme2"><input type="submit" name="SLETT_CAMPUS" value="Slett" />';
      echo '</td></tr></form>';     
    } //while
    echo '</table>'; 
    mysql_close($tilkobling); //lukker tilkobling til db
  } //visCampus
  
  /*************************REDIGER/SLETT CAMPUS*******************************/
  function redigerCampus() {
    $cId = $_POST['campusId'];
    $cNavn = $_POST['nytt_campus']; 
    if (empty($cNavn)) { //er nytt campus navn innskrevet?
      return false; //nytt campus navn er ikke skrevet, send feilmelding
    } else { //campus navn er innskrevet, endre navn
      $tilkobling = kobleTil(); //koble til db
      $update_sql = "UPDATE tblCampus SET cNavn = '$cNavn' WHERE cId = $cId";
      mysql_query($update_sql, $tilkobling);
      mysql_close($tilkobling); //lukker tilkobling til db
      echo "Campus heter nå $cNavn.";
      //echo $sql;
      return true;
    } //if (empty($cNavn))
  } //redigerCampus
  
  function slettCampus() {
    $cId = $_POST['campusId'];
    $tilkobling = kobleTil(); //koble til db
    $sql = "SELECT cNavn FROM tblStudent, tblCampus ";
    $sql .= "WHERE scId=cId AND cId = $cId;";
    $resultat = mysql_query($sql, $tilkobling);  
    $antall = mysql_num_rows($resultat);
    if ($antall >= 1) { //er campus i bruk?
      $rad = mysql_fetch_assoc($resultat); //forventer kun et resultat, derfor ingen løkke 
      $navn = $rad['cNavn'];      
      echo "Du kan ikke slette $navn på grunn av det fins brukere tilknyttet campuset.";
    } else { //ingen er tilknyttet campus
      $slett_sql = "DELETE FROM tblCampus WHERE cId = $cId";
      mysql_query($slett_sql, $tilkobling);
      echo "Campus er fjernet.";
    } //if ($antall >= 1)
    mysql_close($tilkobling); //lukker tilkobling til db
  } //slettCampus
  
  /****************************OPPRETT CAMPUS**********************************/
  function opprettCampus() {
    $cNavn = $_POST['nytt_campus']; 
    if (empty($cNavn)) { //er nytt campus navn skrevet inn?
      return false; //nytt campus navn er ikke skrevet inn, send feilmelding	
    } else { //nytt campus navn er skrevet inn 
      $tilkobling = kobleTil(); //kobler til db               
      $insert_sql = "INSERT INTO tblCampus VALUES (null,'$cNavn');";
      mysql_query($insert_sql, $tilkobling);
      //echo $insert_sql;
      mysql_close($tilkobling); //lukker tilkobling til db
      echo "Campus $cNavn er opprettet.";
      return true;
    }//if (empty($cNavn))
  } //opprettCampus
  
  /******************************VIS FAGOMRÅDE**********************************/
  function visFagomraade() {
    $tilkobling = kobleTil(); //koble til db
    $sql = "SELECT * FROM tblFagomrade ORDER BY fNavn asc;";
    $resultat = mysql_query($sql, $tilkobling);
    echo '<table>';
    while ($rad = mysql_fetch_assoc($resultat)) {
      $fagId = $rad['fId'];
      $fagNavn = $rad['fNavn'];
      echo '<form action="admin_fagomraade.php?fagomraade=redigerFag" method="post">
      <tr><td class="ramme2" style="width:200px;">' . $fagNavn . 
      '<input type="hidden" name="fagId" value="'.$fagId.'"/></td>
      <input type="hidden" name="fagNavn" value="'.$fagNavn.'"/></td>
      <td class="ramme2"><input type="submit" name="REDIGER_FAG" value="Rediger" /></td>
      <td class="ramme2"><input type="submit" name="SLETT_FAG" value="Slett" />';
      echo '</td></tr></form>';     
    } //while
    echo '</table>'; 
    mysql_close($tilkobling); //lukker tilkobling til db
  } // visFagomraade
  
  /*************************REDIGER/SLETT FAGOMRÅDE*****************************/
  function redigerFagomraade() {
    $fId = $_POST['fagId'];
    $fNavn = $_POST['nytt_fagomraade']; 
    if (empty($fNavn)) { //er nytt fagnavn skrivd inn?
      return false; //feltet er tomt, returner feilmelding
    } else { //nytt fagnavn er skrivd inn
      $tilkobling = kobleTil(); //koble til db
      $sql = "UPDATE tblFagomrade SET fNavn = '$fNavn' WHERE fId = $fId";
      mysql_query($sql, $tilkobling);
      mysql_close($tilkobling); //lukker tilkobling til db
      echo "Fagområdet heter nå $fNavn.";
      //echo "<br /> $sql";
      return true;
    } //if (empty($fNavn))
  } //redigerFagomraade
  
  function slettFagomraade() {
    $fId = $_POST['fagId'];
    $tilkobling = kobleTil(); //koble til db
    $sql = "SELECT DISTINCT fNavn FROM tblStudent, tblFagomrade, tblBegivenhet ";
    $sql .= "WHERE (sfId=fId OR bfId=fId) AND fId = $fId;";
    $resultat = mysql_query($sql, $tilkobling);
    $antall = mysql_num_rows($resultat);
    if ($antall >= 1) { //er fagområdet i bruk?
      $rad = mysql_fetch_assoc($resultat); //forventer kun et resultat, derfor ingen løkke 
      $navn = $rad['fNavn'];      
      echo "Du kan ikke slette $navn på grunn av det fins brukere eller begivenheter tilknyttet fagområdet.";
    } else { //intet er tilknyttet fagområdet
      $slett_sql = "DELETE FROM tblFagomrade WHERE fId = $fId";
      mysql_query($slett_sql, $tilkobling);
      echo "Fagområdet er fjernet.";
    } //if ($antall >= 1)
    mysql_close($tilkobling); //lukker tilkobling til db
  } //slettFagomraade
  
  /****************************OPPRETT FAGOMRÅDE********************************/
  function opprettFagomraade() {
    $fagNavn = $_POST['nytt_fagomraade']; 
    if (empty($fagNavn)) { //er nytt fagnavn skrevet inn?
      return false;	//nytt fagområde er ikke skrevet inn, send feilmelding
    } else { //nytt fagnavn er skrevet inn
      $tilkobling = kobleTil(); //kobler til db               
      $insert_sql = "INSERT INTO tblFagomrade VALUES (null,'$fagNavn');";
      mysql_query($insert_sql, $tilkobling);
      //echo $insert_sql;
      mysql_close($tilkobling); //lukker tilkobling til db
      echo "Fagområdet $fagNavn er opprettet.";
      return true;
    }//if (empty($fagNavn)) 
  } //opprettFagomraade
  
  /*********************************SKJEMAER************************************/  
  function skjemaOpprettCampus($feilOpprett) { //lager skjema for oppretting av nytt campus
    echo '<h3>Opprett nytt campus:</h3>  
          <table>
          <form action="" method="post" onsubmit="return endreCampus();">';
    if (empty($feilOpprett)) { //ligger det noe i feilmelding?
      echo '<tr><td class="ramme2">Navngi campus:</td>'; 
    } else { //ligger noe i feilmelding
      echo "<td class='ramme2' style='width:320px;'>$feilOpprett</td>"; //vis feilmelding   
    } //if (empty($feilOpprett))
    echo '<td class="ramme2"><input type="text" id="nytt_campus" name="nytt_campus" /></td>
          <td class="ramme2"><input type="submit" name="OPPRETT_CAMPUS" value="Opprett campus" /></td>
          </tr>
          </form></table>';  
  } //skjemaOpprettCampus
  
  function skjemaRedigerCampus($feilRediger) { //lager skjema for redigering av campus
    echo '<h3>Rediger campus:</h3>
          <table>
          <form action="" method="post" onsubmit="return endreCampus();">
          <tr>';
    if (empty($feilRediger)) { //ligger det noe i feilmelding?
      echo "<td class='ramme2' style='width:300px;'>Endre navn på campus fra ". $_POST['campusNavn'] ." til:</td>";          
    } else { //ligger noe i feilmelding, vis feil
      echo "<td class='ramme2' style='width:320px;'>$feilRediger</td>";
    } //if (empty($feilRediger))          
    echo '<td class="ramme2"><input type="text" id="nytt_campus" name="nytt_campus" /></td>          
          <td class="ramme2"><input type="submit" name="LAGRE_ENDRING" value="Endre campus" /></td>
          </tr>
          <tr>
          <!--Skjulte elementer-->
          <td><input type="hidden" name="campusId" value="'.$_POST['campusId'].'"/></td>
          <td><input type="hidden" name="campusNavn" value="'.$_POST['campusNavn'].'"/></td>         
          </tr>
          </form></table>';  
  } //skjemaRedigerCampus
  
  function skjemaOpprettFag($feilOpprett) {  //lager skjema for oppretting av nytt fagområde
    echo '<h3>Opprett nytt fagområde:</h3>
          <table>
          <form action="" method="post" onsubmit="return endreFagomraade();">
          <tr>';
    if (empty($feilOpprett)) { //ligger det noe i feilmelding?
      echo "<td class='ramme2' style='width:135px;'>Navngi fagområdet:</td>"; 
    } else { //ligger noe i feilmelding
      echo "<td class='ramme2' style='width:330px;'>$feilOpprett</td>"; //vis feilmelding   
    } //if (empty($feilOpprett))
    echo '<td class="ramme2"><input type="text" id="nytt_fagomraade" name="nytt_fagomraade" /></td>
          <td class="ramme2"><input type="submit" name="OPPRETT_FAG" value="Opprett fagområde" /></td>
          </tr>
          </form></table>';  
  } //skjemaOpprettFag
  
  function skjemaRedigerFag($feilRediger) { //lager skjema for redigering av fagområde
    echo '<h3>Rediger fagområde:</h3> 
          <table> 
          <form action="" method="post" onsubmit="return endreFagomraade();">
          <tr>';
    if (empty($feilRediger)) { //ligger det noe i feilmelding?
      echo "<td class='ramme2' style='width:350px;'>Endre navn på fagområde fra ".$_POST['fagNavn']." til:</td>";
    } else { //ligger noe i feilmelding, vis feil
      echo "<td class='ramme2' style='width:340px;'>$feilRediger</td>";
    } //if (empty($feilRediger))          
    echo '<td class="ramme2"><input type="text" id="nytt_fagomraade" name="nytt_fagomraade" /></td>          
          <td class="ramme2"><input type="submit" name="LAGRE_ENDRING" value="Endre fagområde" /></td>
          </tr>
          <tr>          
          <!--Skjulte elementer-->          
          <td><input type="hidden" name="fagId" value="'.$_POST['fagId'].'"/></td>
          <td><input type="hidden" name="fagNavn" value="'.$_POST['fagNavn'].'"/></td>
          </tr>
          </form></table>';
  } //skjemaRedigerFag
?>