<?php        
  /******************************VIS NETTVERK**********************************/
  function visNettverk() { //viser alle nettverkene som er registrert
    $tilkobling = kobleTil(); //koble til db
    $sql = "SELECT * FROM tblNettverk ORDER BY nNavn asc;";
    $resultat = mysql_query($sql, $tilkobling);
    while ($rad = mysql_fetch_assoc($resultat)) {      
      $nettId = $rad['nId'];
      $nettNavn = $rad['nNavn'];
      //oppretter liste over nettverk med rediger og slett knapp
      echo '<form action="admin_nettverk.php?adm_nettverk=endreNettverk" method="post">
      <tr><td class="ramme2" style="width:200px;">' .$nettNavn. 
      '<input type="hidden" name="nettId" value="'.$nettId.'"/>
      <input type="hidden" name="nettNavn" value="'.$nettNavn.'"/></td>
      <td class="ramme2"><input type="submit" name="REDIGER_NETTVERK" value="Rediger" /></td>
      <td class="ramme2"><input type="submit" name="SLETT_NETTVERK" value="Slett" />
      </td></tr>
      </form>';     
    } //while 
    mysql_close($tilkobling); //lukker tilkobling til db
  } //visNettverk
  
  /****************************ENDRE NETTVERK****************************/
  function endreNettverk() { //funksjon som endrer navnet på et nettverk
    $nId = $_POST['nettId']; 
    $nNavn = $_POST['nytt_nettverk'];
    if ($nId == "Intet valgt") { //er nettverk valgt?
      return false; //nettverk er ikke valgt, send feilmelding
    } else if (empty($nNavn)) { //er nytt nettverksnavn fylt ut?
      return false; //nytt navn er ikke satt, send feilmelding
    } else { //nettverk er valgt og navn er fylt ut 
      $tilkobling = kobleTil(); //kobler til db         
      $update_sql = "UPDATE tblNettverk SET nNavn = '$nNavn' WHERE nId=$nId;";
      mysql_query($update_sql, $tilkobling);
      //echo $update_sql . "<br />";  
      mysql_close($tilkobling); //lukker tilkobling til db
      echo "<tr><td>Nettverket er nå endret.</td></tr>"; //gi tilbakemelding
      return true;
    }//if ($dnId == "Intet valgt")
  } //endreNettverk
  
  /********************************SLETT NETTVERK*****************************/
  function slettNettverk() { //funksjon som sletter nettverk
    $nId = $_POST['nettId'];
    $tilkobling = kobleTil(); //koble til db
    $sql = "SELECT DISTINCT nNavn FROM tblStudent, tblNettverk, tblStudent_has_tblNettverk ";
    $sql .= "WHERE sId = dpId AND dnId = nId AND nId = $nId;";
    $resultat = mysql_query($sql, $tilkobling);
    $antall = mysql_num_rows($resultat);
    if ($antall >= 1) { //er brukere tilknyttet nettverket?
      $rad = mysql_fetch_assoc($resultat); //forventer kun et resultat, derfor ingen løkke 
      $navn = $rad['nNavn'];      
      echo "<tr><td>Du kan ikke slette $navn på grunn av det fins brukere tilknyttet nettverket.</td></tr>";
    } else { //ingen er tilknyttet nettverket
      $slett_sql = "DELETE FROM tblNettverk WHERE nId = $nId";
      mysql_query($slett_sql, $tilkobling);
      echo "<tr><td>Nettverket er fjernet.</td></tr>";
    } //if ($antall >= 1)
    mysql_close($tilkobling); //lukker tilkobling til db
  } //slettNettverk
  
  /****************************OPPRETT NETTVERK****************************/
  function opprettNettverk() { //funksjon som oppretter nytt nettverk
    $nNavn = $_POST['nytt_nettverk'];
    $email = $_SESSION['email']; 
    if (empty($nNavn)) { //er nytt nettverksnavn skrevet inn?
      return false;	//nytt nettverksnavn ikke innskrevet, send feilmelding
    } else { //nytt nettverksnavn er skrevet inn
      //henter "skaper" av nettverket 
      $tilkobling = kobleTil(); //kobler til db   
      $sql = "SELECT sId FROM tblstudent WHERE sEpost = '$email';"; //hent brukers id  
      $resultat = mysql_query($sql, $tilkobling);
      $rad = mysql_fetch_assoc($resultat); //forventer kun et resultat, derfor ingen løkke 
      $sId = $rad['sId'];    
      //echo $sql ."<br />";             
      $insert_sql = "INSERT INTO tblNettverk VALUES (null,'$nNavn', $sId);";
      mysql_query($insert_sql, $tilkobling);
      //echo $insert_sql . "<br />";
      mysql_close($tilkobling); //lukker tilkobling til db
      echo "<tr><td>Nettverket $nNavn er opprettet.</td></tr>";
      return true;
    }//if (empty($nNavn))
  } //opprettNettverk                              
  
  /*********************************SKJEMAER************************************/  
  function nyttNettverkSkjema($feilmelding) { //oppretter skjema for nye nettverk
    $skjema = '
    <h3>Opprett nytt nettverk:</h3>
    <form action="" method="post" onsubmit="return nyttNettverk();">
    <tr>
    '; //linjeskift for bedre oversikt i kildekoden
    if (empty($feilmelding)) { //ligger det noe i feilmelding?
      $skjema .= '<td class="ramme2">Navngi nettverk:</td>'; 
    } else { //ligger noe i feilmelding
      $skjema .= "<td class='ramme2' style='width:265px;'>$feilmelding</td>"; //vis feilmelding   
    } //if (empty($feilmelding))
    $skjema .= '<td class="ramme2"><input type="text" id="nytt_nettverk" name="nytt_nettverk" /></td>
    <td class="ramme2"><input type="submit" name="OPPRETT_NETTVERK" value="Opprett nettverk" /></td>
    </tr>
    </form>';
    return $skjema;  
  } //nyttNettverkSkjema                        
  
  function endreNettverkSkjema($feilmelding) { //oppretter skjema for endring av nettverk
    $skjema = '<h3>Rediger nettverk:</h3>  
    <form action="" method="post" onsubmit="return nyttNettverk();">
    <tr>';
    if (empty($feilmelding)) { //ligger det noe i feilmelding?
      $skjema .= "<td class='ramme2' style='width:350px;'>Endre navn på nettverk fra ". $_POST['nettNavn'] ." til: </td>";          
    } else { //ligger noe i feilmelding, vis feil
      $skjema .= "<td class='ramme2' style='width:320px;'>$feilmelding</td>"; //vis feilmelding   
    } //if (empty($feilRediger))          
    $skjema .= '<td class="ramme2"><input type="text" id="nytt_nettverk" name="nytt_nettverk" /></td>
    <td class="ramme2"><input type="submit" name="ENDRE_NETTVERK" value="Endre nettverk" /></td>
    </tr>
    <!--Skjulte elementer-->
    <tr>                                
    <td><input type="hidden" name="nettId" value="'.$_POST['nettId'].'"/></td>
    <td><input type="hidden" name="nettNavn" value="'.$_POST['nettNavn'].'"/></td>                
    </tr>
    </form>';  
    return $skjema;
  } //endreNettverkSkjema
?>