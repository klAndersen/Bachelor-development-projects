<?php
  /*****************************VIS NETTVERK**********************************/
  function visMineNettverk($feilNett) {         
    //henter nettverk(ene) den innloggede tilhører (hvis bruker har nettverk)
    $email = $_SESSION['email'];
    $tilkobling = kobleTil(); //kobler til db
    $sql = "SELECT nId, nNavn FROM tblStudent, tblNettverk, tblStudent_has_tblNettverk ";
    $sql .= "WHERE sEpost = '$email' AND sId=dpId AND nId=dnId;";
    $resultat = mysql_query($sql, $tilkobling);
    $antall = mysql_num_rows($resultat);
    if ($antall >= 1) { //minst tilknyttet et nettverk 
      //oppretter en select fylt med brukers nettverk
      echo '<!--Vis nettverk bruker er tilknyttet-->
      <form method="post" action="">
      <tr><td>'.$feilNett.'<select id="nId" name="nId">
      <option value="Intet valgt">--Velg nettverk--</option>
      '; //linjeskift for bedre oversikt i kildekoden
      while ($rad = mysql_fetch_assoc($resultat)) {
        $nId = $rad['nId'];
        $nNavn = $rad['nNavn'];
        echo "<option value='$nId'>$nNavn</option>
        "; //linjeskift for bedre oversikt i kildekoden
      } //while
      echo '</select></td>
      '; //avslutter select og legger til linjeskift for bedre oversikt i kildekoden      
      if (isset($_GET['Profil']) && $_GET['Profil'] == "fjernNettverk") {
        //hvis bruker skal fjerne seg fra et nettverk,- vis knappen for fjerning 
        echo '<td><input type="submit" name="FJERN_NETTVERK" value="Fjern deg fra nettverket" /></td>
        '; //linjeskift for bedre oversikt i kildekoden
      } else { //visning av nettverk/brukere av nettverk
        echo '<td><input type="submit" name="visNettverk" value="Vis medlemmer tilknyttet nettverk" /></td>
        </tr>
        ';//linjeskift for bedre oversikt i kildekoden                
      } //if (isset($_GET['Profil'])  
      if (isset($_POST['visNettverk']) && $_POST['visNettverk'] == "Vis medlemmer tilknyttet nettverk") {                         
        visNettverksMedlemmer($tilkobling, $email); //viser medlemmer tilknyttet valgt nettverk
      } //if (isset($_POST['visNettverk']))
      echo '</form>
      ';//linjeskift for bedre oversikt i kildekoden 
    } else { //ikke tilknyttet noen nettverk enda
      echo "<tr><td>
      Du er ikke medlem av noen nettverk enda. Du kan koble deg til et nettverk 
      <a href='redigerProfil.php?Profil=leggTilNett'>her</a>
      </td></tr>
      "; //linjeskift for bedre oversikt i kildekoden
    } //if ($antall >= 1)  
    mysql_close($tilkobling); //lukker tilkobling til db
  } //visMineNettverk 
  
  function visNettverksMedlemmer($tilkobling, $email) { //funksjon som viser nettverkets medlemmer
    $dnId = $_POST['nId'];
    echo "</table>
    <table>
    "; /*avslutter tabellen her for å unngå stort gap mellom
        select og knapp, siden tilbakemeldingene er lange */
    if ($dnId != "Intet valgt") { //er nettverk valgt? 
      //hent alle medlemmer fra valgt nettverk utenom denne brukeren                 
      $sql = "SELECT * FROM tblStudent, tblstudent_has_tblnettverk, tblNettverk ";
      $sql .= "WHERE nId=$dnId AND sId=dpId AND dnid=nid AND sEpost NOT LIKE '$email'"; 
      //echo $sql;
      $resultat = mysql_query($sql, $tilkobling);
      $antall = mysql_num_rows($resultat);
      if ($antall >= 1) { //er flere enn bruker tilknyttet dette nettverket?
        while ($rad = mysql_fetch_assoc($resultat)) {
          $fnavn = $rad['sFornavn'];
          $enavn = $rad['sEtternavn'];        
          echo "<tr><td>$fnavn $enavn er medlem av nettverket.</td></tr>
          ";//linjeskift for bedre oversikt i kildekoden
        } //while        
      } else { //kun bruker er tilknyttet nettverket            
        echo "<tr><td>Du er det eneste medlemmet av dette nettverket.</td></tr>";
      } //if ($antall >= 1)
    } else { //har ikke valgt et nettverk
      echo "<tr><td><font color='red'>Du må velge et nettverk.</font></td></tr>";
    } //if ($dnId != "Intet valgt")     
  } //visNettverksMedlemmer
   
  /*****************************NYE NETTVERK**********************************/
  function nyttNettverkSkjema($feilNett) { //oppretter skjema for nytt nettverk
    $vis = '<table>
            <form action="" method="post">
            <!--Vis nettverk--> 
            <tr><td>';
    if(empty($feilNett)) { //ligger det noe i $feilNett (feilmelding)?
     $vis .= 'Nettverk: ';
    } else { //noe er feil, vis feilmelding
      $vis .= $feilNett;
    } //if(empty($feilNett))
    $vis .= '</td>        
            <td>';
    $vis .= selectNettverk();
    $vis .= '</td>
            <td><input type="submit" name="LEGG_NETTVERK" value="Koble til nettverket" /></td></tr>            
            </form>
            </table>
            '; //linjeskift for bedre oversikt i kildekoden
    return $vis;
  } //nyttNettverkSkjema
  
  function nyttNettverk() {
    $email = $_SESSION['email'];   
    $nId = $_POST['nId']; 
    if ($nId == "Intet valgt") { //er nettverk valgt?
    	return false; //har ikke valgt nettverk, send feilmelding
    } else { //har valgt nettverk
      $tilkobling = kobleTil(); //opprett tilkobling til db     
      $sql = "SELECT sId FROM tblStudent ";
      $sql .= "WHERE sEpost = '$email'";
      $resultat = mysql_query($sql, $tilkobling);
      $rad = mysql_fetch_assoc($resultat); //forventer kun et resultat, derfor ingen løkke 
      $sId = $rad['sId'];        
      //echo $sql ."<br />";  
      //legger bruker inn i nettverk    
      $insert_sql = "INSERT INTO tblstudent_has_tblnettverk VALUES ($sId, $nId);";
      mysql_query($insert_sql, $tilkobling);
      mysql_close($tilkobling); //lukker tilkobling til db
      echo "Du er nå tilknyttet nettverket."; //gi tilbakemelding
      return true;
    } //if (empty($nId))
  } //nyttNettverk
                  
  /*****************************FJERN NETTVERK**********************************/
  function fjernNettverk() {
    $email = $_SESSION['email'];
    $nId = $_POST['nId'];
    if ($nId == "Intet valgt") { //er nettverk valgt?
    	return false; //ikke valgt nettverk, send feilmelding
    } else { //nettverk valgt, fjern tilknytting
      $tilkobling = kobleTil(); //kobler til db
      $sql = "SELECT sId FROM tblStudent ";
      $sql .= "WHERE sEpost = '$email'";
      $resultat = mysql_query($sql, $tilkobling);
      $rad = mysql_fetch_assoc($resultat); //forventer kun et resultat, derfor ingen løkke 
      $sId = $rad['sId'];      
      $slett_sql = "DELETE FROM tblstudent_has_tblnettverk "; 
      $slett_sql .= "WHERE dpId = $sId AND dnId = $nId;";
      //echo $slett_sql;
      mysql_query($slett_sql,$tilkobling);
      mysql_close($tilkobling); //lukker tilkobling til db
      echo "Du er nå fjernet fra nettverket.";
      return true;
    } //if (empty($nId))
  } //fjernNettverk
  
  /*********************************SELECT NETTVERK***********************************/
  function selectNettverk() { //oppretter en select som fylles med eksisterende nettverk
    $nettverk = '<select id="nId" name="nId">
    '; //linjeskift for bedre oversikt i kildekoden
    $tilkobling = kobleTil(); //oppretter tilkobling til db 
    $nettverk .= '<option value="Intet valgt">--Velg nettverk--</option>
    '; //linjeskift for bedre oversikt i kildekoden
    $sql = "SELECT * FROM tblNettverk ORDER BY nNavn asc"; //hent alle nettverk
    $resultat = mysql_query($sql, $tilkobling);       
    while ($rad = mysql_fetch_assoc($resultat)) { //fyller opp select med nettverkene som finnes    
      $nId = $rad['nId'] ;         
      $nNavn = $rad['nNavn'] ; 
      $nettverk .= "<option value='$nId' >$nNavn</option>
      "; //linjeskift for bedre oversikt i kildekoden
    } //while
    $nettverk .= '</select>
    '; //avslutter select og legger inn linjeskift for bedre oversikt i kildekoden
    mysql_close($tilkobling); //lukker tilkobling til db
    return $nettverk;
  } //selectNettverk   
?>