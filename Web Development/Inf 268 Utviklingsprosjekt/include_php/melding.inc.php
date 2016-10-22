<?php
   /***************************MOTTATTE MELDINGER********************************/ 
   function visAlleMottate($email) {
    $tilkobling = kobleTil(); //oppretter tilkobling til db   
    //finn brukererens meldinger    
    $til_sql = "SELECT * FROM tblStudent, tblMeldinger ";
    $til_sql .= "WHERE sEpost = '$email' AND sId = msTilId;";
    $resultat = mysql_query($til_sql, $tilkobling);    
    $antall = mysql_num_rows($resultat);
    if ($antall >= 1) { //det finnes minst en mottatt melding
      while ($rad = mysql_fetch_assoc($resultat)) { //henter mottakerId
        $tilId = $rad['msTilId'];                  
      } //while  
      //finner avsender(e) og meldingsdetaljer, viser nyeste først   
      $avs_sql = "SELECT * FROM tblStudent, tblMeldinger ";
      $avs_sql .= "WHERE sId = msFraId AND msTilId = $tilId ";
      $avs_sql .= "ORDER BY mDatoTid desc";
      $resultat = mysql_query($avs_sql, $tilkobling);
      //overskrift/typebetegnelse for feltene 
      echo "<table style='border-collapse:collapse'>
      <tr>
      <td class='ramme' style='width:180px;'><strong>Avsendere:</strong></td>
      <td class='ramme' style='width:180px;'><strong>Emne:</strong></td>
      <td class='ramme' style='width:180px;'><strong>Dato mottatt:</strong></td>
      <td class='ramme' style='width:180px;'><strong>Type melding:</strong></td>
      <td class='ramme' style='width:180px;'><strong>Vis melding:</strong></td>            
      </tr>
      "; //linjeskift for bedre oversikt i kildekoden                   
      while ($rad = mysql_fetch_assoc($resultat)) { //henter melding(er)
        $mId = $rad['mId'];        
        $fraFnavn = $rad['sFornavn'];  
        $fraEnavn = $rad['sEtternavn'];
        $emne = $rad['mTittel'];
        $mLest = $rad['mLest'];  
        $dato = explode("-", $rad['mDatoTid']);
        $dato = splittDatoOgKlokkeslett($dato);                     
        echo "<tr>
        <td class='ramme'>$fraFnavn $fraEnavn</td>";
        if(!empty($emne)) { //har meldingen et emne/tittel?
           echo "<td class='ramme'>$emne</td>";
        } else { //intet emne/tittel
           echo "<td class='ramme'>Intet emne</td>";
        } //if(!empty($emne))
        echo "<td class='ramme'>$dato</td>
              <td class='ramme'>";
        if ($mLest == 1) { //er meldingen ny?
        	echo "<strong>Ny</strong>";
        } else { //gammel melding
          echo "Lest";
        } //if ($mLest == 1)
        echo"</td>
        <td class='ramme'>
        <a href='melding.php?melding=Innboks&mNr=$mId'>Les melding</a></td>                 
        </tr>";
      } //while                          
      echo '</table>';      
    } else { //ingen sendte meldinger
      echo "Du har ikke mottatt noen meldinger enda...";
    } //if ($antall >= 1)
    mysql_close($tilkobling); //lukker tilkobling til db
   } //visAlleMottate
  
  function visEnMottatt($email) {
    $tilkobling = kobleTil(); //oppretter tilkobling til db      
    $mId = mysql_real_escape_string($_GET['mNr']); //henter meldingens id  
    $til_sql = "SELECT * FROM tblStudent, tblMeldinger ";
    $til_sql .= "WHERE sEpost = '$email' AND mId = $mId AND sId = msTilId;";
    $resultat = mysql_query($til_sql, $tilkobling);    
    $antall = mysql_num_rows($resultat);
    if ($antall == 1) { //meldingen finnes
      $rad = mysql_fetch_assoc($resultat); //forventer kun et resultat, derfor ingen løkke 
      //hent ut mottaker
      $sId = $rad['sId'];
      $tilId = $rad['msTilId'];
      $mLest = $rad['mLest'];                          
      //finner avsender og meldingsdetaljer   
      $avs_sql = "SELECT * FROM tblStudent, tblMeldinger ";
      $avs_sql .= "WHERE sId=msFraId AND mId = $mId;";      
      $resultat = mysql_query($avs_sql, $tilkobling);                          
      $rad = mysql_fetch_assoc($resultat); //forventer kun et resultat, derfor ingen løkke 
      //hent avsender og meldingens innhold
      $fraFnavn = $rad['sFornavn'];  
      $fraEnavn = $rad['sEtternavn'];
      $svarBruker = $rad['sEpost'];
      $emne = $rad['mTittel'];      
      $melding = $rad['mTekst'];
      $dato = explode("-", $rad['mDatoTid']);
      $dato = splittDatoOgKlokkeslett($dato);
      echo "<strong>Avsender: </strong>$fraFnavn $fraEnavn<br />";
      if(!empty($emne)) { //har meldingen et emne/tittel?
        echo "<strong>Emne: </strong>$emne<br />";
      } else { //intet emne/tittel
        echo "<strong>Emne: </strong>Intet emne<br />";
      } //if(!empty($emne))
      echo "<strong>Mottatt: </strong>$dato<br />
      <strong>Innhold: </strong>$melding<br /><br />";   
      echo '<a href="melding.php?melding=SendMelding&mottaker='.$svarBruker.'">Svar på melding</a>';         
      if ($mLest == 1) { //er meldingen ny?
        $_SESSION['nyMelding']--; //trekk ifra en ulest melding
        $update_sql = "UPDATE tblMeldinger SET mLest=0 WHERE mId=$mId;";
        mysql_query($update_sql,$tilkobling); 
      } //if ($mLest == 1)
    } else { //meldingen finnes ikke
      echo "Obs! Denne meldingen finnes ikke...";
    } //if ($antall >= 1)    
    mysql_close($tilkobling); //lukker tilkobling til db
  } //visEnMottatt 

  /***************************SENDTE MELDINGER**********************************/
  function visAlleSendte($email) {
    $tilkobling = kobleTil();        
    $avs_sql = "SELECT * FROM tblStudent, tblMeldinger ";
    $avs_sql .= "WHERE sEpost = '$email' AND sId = msFraId;";
    $resultat = mysql_query($avs_sql, $tilkobling);    
    $antall = mysql_num_rows($resultat);
    if ($antall >= 1) { //det finnes minst en mottatt melding
      while ($rad = mysql_fetch_assoc($resultat)) { //henter mottakerId
        $fraId = $rad['msFraId'];                  
      } //while  
      //finner mottakere(e) og meldingsdetaljer, viser nyeste først   
      $til_sql = "SELECT * FROM tblStudent, tblMeldinger ";
      $til_sql .= "WHERE sId = msTilId AND msFraId = $fraId ";
      $til_sql .= "ORDER BY mDatoTid desc";
      $resultat = mysql_query($til_sql, $tilkobling);
      //overskrift/typebetegnelse for feltene 
      echo "<table style='border-collapse:collapse'>
      <tr>
      <td class='ramme' style='width:180px;'><strong>Mottakere:</strong></td>
      <td class='ramme' style='width:180px;'><strong>Emne:</strong></td>
      <td class='ramme' style='width:180px;'><strong>Dato sendt:</strong></td>
      <td class='ramme'>Vis melding:</td>
      </tr>
      "; //linjeskift for bedre oversikt i kildekoden                   
      while ($rad = mysql_fetch_assoc($resultat)) { //henter melding(er)
        $mId = $rad['mId'];
        $tilFnavn = $rad['sFornavn'];  
        $tilEnavn = $rad['sEtternavn'];
        $emne = $rad['mTittel'];
        $dato = explode("-", $rad['mDatoTid']);
        $dato = splittDatoOgKlokkeslett($dato);
        echo "<tr>
        <td class='ramme'>$tilFnavn $tilEnavn</td>";
        if(!empty($emne)) { //har meldingen et emne/tittel?
           echo "<td class='ramme'>$emne</td>";
        } else { //intet emne/tittel
           echo "<td class='ramme'>Intet emne</td>";
        } //if(!empty($emne))
        echo "<td class='ramme'>$dato</td>
        <td class='ramme'><a href='melding.php?melding=Utboks&mNr=$mId'>Les melding</a></td>   
        </tr>";
      } //while                          
      echo '</table>';
    } else { //ingen sendte meldinger
      echo "Du har ikke sendt noen meldinger enda...";
    } //if ($antall >= 1)
    mysql_close($tilkobling); //lukker tilkobling til db
   } //visAlleSendte
   
  function visEnSendt($email) {
    $tilkobling = kobleTil();
    $mId = mysql_real_escape_string($_GET['mNr']); //henter meldingens id     
    //...finner avsenderId
    $avs_sql = "SELECT * FROM tblStudent, tblMeldinger ";
    $avs_sql .= "WHERE sEpost = '$email' AND mId = $mId AND sId = msFraId;";
    $resultat = mysql_query($avs_sql, $tilkobling);    
    $antall = mysql_num_rows($resultat);
    if ($antall == 1) { //meldingen finnes
      $rad = mysql_fetch_assoc($resultat); //forventer kun et resultat, derfor ingen løkke 
      $fraId = $rad['msFraId']; //henter avsenderId       
      //finner mottaker og meldingsdetaljer 
      $til_sql = "SELECT * FROM tblStudent, tblMeldinger ";
      $til_sql .= "WHERE sId = msTilId AND mId = $mId;";            
      $resultat = mysql_query($til_sql, $tilkobling); 
      echo "<h4>Sendt meldings innhold:</h4>";       
      $rad = mysql_fetch_assoc($resultat); //forventer kun et resultat, derfor ingen løkke 
      //henter mottaker og meldingens innhold
      $tilFnavn = $rad['sFornavn'];  
      $tilEnavn = $rad['sEtternavn'];           
      $melding = $rad['mTekst'];      
      $dato = explode("-", $rad['mDatoTid']);
      $dato = splittDatoOgKlokkeslett($dato);            
      echo "<strong>Mottaker:</strong> $tilFnavn $tilEnavn<br />
      <strong>Sendt:</strong> $dato<br />
      <strong>Melding:</strong> " . $melding . "<br /><br />";             
    } else { //ingen sendte meldinger
      echo "Obs! Denne meldingen finnes ikke...";
    } //if ($antall >= 1)
    mysql_close($tilkobling); //lukker tilkobling til db
  } //visEnSendt
      
  /*******************************HENT FUNKSJONER*******************************/
  function hentSendingsType($email, $feilFnavn, $feilEnavn, $feilMottakergruppe, $feilMelding){
    if ($_GET['melding'] == "Masseutsendelse") { //skal melding masseutsendes?      
      if (!sendTilMange($email)) { //meldingen(e) ble ikke sendt      
        echo visSkjema("MANGE", $feilFnavn, $feilEnavn, $feilMottakergruppe, $feilMelding); //vis skjema med feilmelding(er)
      } //if (!sendTilMange($email))
    } else { //melding skal kun sendes til en person            
      if (!sendMeld($email)) { //meldingen ble ikke sendt
        echo visSkjema("EN", $feilFnavn, $feilEnavn, $feilMottakergruppe, $feilMelding); //vis skjema med feilmelding(er)
      } //if if (!sendMeld($email))
    } //if ($_GET['melding'] == "Masseutsendelse")
  } //hentSendingsType
  
  function hentSendingsSkjema($sbId, $feilFnavn, $feilEnavn, $feilMottakergruppe, $feilMelding) {
    if ($_GET['melding'] == "Masseutsendelse") { //er masseutsendelse valgt?
      if ($sbId == 3 || $sbId == 4) { //er innlogget admin/mod?
        echo visSkjema("MANGE", $feilFnavn, $feilEnavn, $feilMottakergruppe, $feilMelding); //isåfall vis masseutsendelses-skjema
      } else { //ikke admin/mod (kan kommet hit via historie/logg)
        echo visSkjema("EN", $feilFnavn, $feilEnavn, $feilMottakergruppe, $feilMelding); //vis skjema for sending til en person
      } //if ($sbId == 3 || $sbId == 4)
    } else { //sending til en person er valgt
      echo visSkjema("EN", $feilFnavn, $feilEnavn, $feilMottakergruppe, $feilMelding); //vis skjema for sending av melding
    } //if ($_GET['melding'] == "Masseutsendelse")    
  } //hentSendingsSkjema  
    
  function finnMottakerGruppe($tilkobling, $til_gruppe) {
    $til_Sql = "SELECT DISTINCT sId FROM tblStudent ";
    $til_Sql .= "WHERE sfid = $til_gruppe;"; //finn alle brukere tilknyttet dette fagområdet
    //echo $til_Sql;
    $resultat = mysql_query($til_Sql, $tilkobling); 
    return $resultat;
  } //finnMottakerGruppe
  
  function setTittel($sbId) {      
    if (!isset($_GET['melding']) || $_GET['melding'] == "Innboks") { //skal innboks vises?
      $_GET['melding'] = "Innboks"; //intet valgt, gå rett til innboks
      $tittel = "Innboks";
    } else if ($_GET['melding'] == "Utboks") { //skal utboks vises
       $tittel = "Utboks";   	
    } else if (($_GET['melding'] == "Masseutsendelse") && ($sbId == 3 || $sbId == 4)) {  
       $tittel = "Masseutsendelse";
    } else { //kun en melding som skal sendes
       $tittel = "Send melding";
    } //if (!isset($_GET['melding']))
    return $tittel;
  } //setTittel    
    
  /*********************SKJEMA FOR SENDING AV MELDINGER**************************/   
  function visSkjema($type, $feilFnavn, $feilEnavn, $feilMottakergruppe, $feilMelding) { //skjema for sending av meldinger
    $tilkobling = kobleTil(); //kobler til db
    //variabler for sending
    $fnavn = ""; //initiering 
    $enavn = "";
    $emne = "";
    $melding = "";   
    if (isset($_POST['SEND']) && $_POST['SEND'] == "Send melding") {
      $emne = $_POST['emne'];
      $melding = $_POST['melding'];
      $feilMelding = tomMelding(); //henter ut feilmelding her, siden denne brukes på for begge typene
      if ($_GET['melding'] != "Masseutsendelse") { //er bare en melding om skal sendes
        $fnavn = $_POST['fnavn'];
        $enavn = $_POST['enavn'];
      } //if ($_GET['melding'] != "Masseutsendelse")        
    } //if (isset($_POST['SEND']))
    //i tilfellet man sender etter å ha besøkt profil    
    if (isset($_GET['mottaker'])) { 
      $mottaker = $_GET['mottaker']; 
      //echo $mottaker;
      $sql = "SELECT sFornavn, sEtternavn FROM tblStudent ";
      $sql .= "WHERE sEpost = '$mottaker';";
      $resultat = mysql_query($sql,$tilkobling);
      //echo $sql;
      $rad = mysql_fetch_assoc($resultat); //forventer kun et resultat, derfor ingen løkke 
      $fnavn = $rad['sFornavn'];
      $enavn = $rad['sEtternavn'];      
      mysql_close($tilkobling); //lukker tilkobling til db
    } //if if (isset($_GET['mottaker']))       
    echo '<!--Meldingsskjema-->
    <fieldset>
    <legend>Send melding</legend>
    <br />
    <form action="" method="post" onsubmit="return '; 
    //js som sjekker feltene (if test som henter tilknyttet funksjon)
    if ($type == "EN") { //skal kun sendes til en person
      echo 'sjekkSending()';
    } else { //skal sendes til flere (masseutsending)
      echo 'sjekkSendingMange()';
    } //if ($type == "EN").
    echo ';">
    <table>';
    if ($type == "EN") { //vis skjema for enkelt-sending av meldinger              
      echo '<!--Send til en mottaker-->
      <tr>
      <td>Fornavn:</td>
      <td>
      <input type="text" id="fnavn" name="fnavn" size="33" value="'.$fnavn.'" /> '
      . $feilFnavn. '</td></tr>
      <tr><td>Etternavn:</td>
      <td><input type="text" id="enavn" name="enavn" size="33" value="'.$enavn.'" /> '
      . $feilEnavn. '</td></tr>
      '; //linjeskift for bedre oversikt i kildekoden
    } else { //masseutsendelse av melding til flere mottakere
      if (isset($_POST['SEND']) && $_POST['SEND'] == "Send melding") { //har det vært et forsøk på sending?
        $feilMottakergruppe = feilMottakergruppe();         
      } //if (isset($_POST['SEND'])
      selectFagomrade(true, $feilMottakergruppe); //settes til true for å starte med "Velg fagområde"                                                         
    } //if ($type == "EN")
    echo '<!--Meldingens tittel/emne-->
    <tr><td>Emne:</td>
    <td><input type="text" id="emne" name="emne" size="33" value="'.$emne.'" /></td></tr>
    <!--Meldingen som skal sendes-->
    <tr><td>Melding:</td>
    <td><textarea cols="25" rows="3" id="melding" name="melding">'.$melding.'</textarea> '
    .$feilMelding.'</td></tr>      
    <tr><td><input type="submit" name="SEND" value="Send melding" /></td>
    <td><input type="reset" value="Tøm feltene" /></td></tr>
    </table>
    </form>
    </fieldset>
    '; //linjeskift for bedre oversikt i kildekoden         
  } //visSkjema
  
  /***************************SENDING AV MELDINGER******************************/       
  function sendMeld($email) { //funksjon som sender melding til en person
    $tilkobling = kobleTil(); //oppretter tilkobling til db
    $fnavn = mysql_real_escape_string($_POST['fnavn']);
    $enavn = mysql_real_escape_string($_POST['enavn']);
    $emne = mysql_real_escape_string($_POST['emne']);
    $melding = mysql_real_escape_string($_POST['melding']);
    //rensker tekste for html, for å forhindre Cross Site Scripting
    $melding = rensHtmlUTF8($melding, 3); //egenlagd funksjon for renskning av html    
    if (empty($fnavn) || empty($enavn) || empty ($melding)) { //er feltene blanke?
      return false; //tomme felter, send feilmelding
    } else if (strlen($fnavn) < 2 ||strlen($enavn) < 3) { //er fornavn/etternavn for kort?
      return false; //for korte felter, send feilmelding            
    } else { //navn og melding er utfylt
      $avs_sql = "SELECT DISTINCT sId FROM tblStudent ";
      $avs_sql .= "WHERE sEpost = '$email';"; 
      $resultat = mysql_query($avs_sql, $tilkobling);
      $rad = mysql_fetch_assoc($resultat); //forventer kun et resultat, derfor ingen løkke     
      $fraId = $rad['sId']; //henter avsender      
      //echo $fraId . "<br />";
      $til_Sql = "SELECT sId FROM tblStudent ";
      $til_Sql .= "WHERE sFornavn = '$fnavn' ";
      $til_Sql .= "AND sEtternavn = '$enavn';";
      $resultat = mysql_query($til_Sql, $tilkobling);
      $antall = mysql_num_rows($resultat);
      if ($antall == 1) { //sjekker om mottaker av melding finnes
        $rad = mysql_fetch_assoc($resultat); //forventer kun et resultat, derfor ingen løkke 
        $tilId = $rad['sId']; //henter mottaker       
        $nyMeld = true;
        $dato = date("Y-m-d H:i:s"); //dato-eks: 2011-12-31, hvis ikke blir d feil i db, med 24t format
        if (empty($emne)) { //er emne/tittel satt?
          $insert_sql = "INSERT INTO tblMeldinger (mTekst, mDatoTid, msFraId, msTilId, mLest) "; 
          $insert_sql .= "VALUES ('$melding', '$dato', $fraId, $tilId, $nyMeld);";    
        } else { //emne/tittel er satt
          $insert_sql = "INSERT INTO tblMeldinger (mTittel, mTekst, mDatoTid, msFraId, msTilId, mLest) ";
          $insert_sql .= "VALUES ('$emne', '$melding', '$dato', $fraId, $tilId, $nyMeld);";          
        } //if (empty($emne))
        mysql_query($insert_sql,$tilkobling);    
        //echo  $insert_sql;      
        echo "Meldingen ble sendt til $fnavn $enavn."; 
        return true; //melding ble sendt, returner true     
      } else { //personen finnes ikke på alumni
        echo "Brukeren du prøver å sende melding til, $fnavn $enavn finnes ikke.";
        //ingen feil innskrivning navn/melding, men bruker finnes ikke, så gi tilbakemelding
        //(siden det er ikke brukerens feil at mottaker ikke eksisterer, blir denne sendt som vanlig tekst)
        return true; 
      } //if ($antall == 1)  
    } //if (empty($fnavn) || empty($enavn) || empty ($melding))            
    mysql_close($tilkobling); //lukker tilkobling til db                 
  } //sendMeld
  
  function sendTilMange($email) { //funksjon for masseutsendelse av meldinger
    $tilkobling = kobleTil(); //kobler til db   
    $emne = mysql_real_escape_string($_POST['emne']); 
    $melding = mysql_real_escape_string($_POST['melding']); //rensker for mulig mysql-kode
    $melding = rensHtmlUTF8($melding,3); //egenlagd funksjon for renskning av html  
    if (empty ($melding)) { //er meldingsfeltet tomt?    
    	return false; //meldingsfelt tomt, send feilmelding
    } else { //meldingsfelt er utfylt
      $til_gruppe = $_POST['idFag']; //hent ut mottakergruppen
      $avs_sql = "SELECT sId FROM tblStudent ";
      $avs_sql .= "WHERE sEpost = '$email';"; 
      $resultat =   mysql_query($avs_sql, $tilkobling);
      $rad = mysql_fetch_assoc($resultat); //forventer kun et resultat, derfor ingen løkke      
      $fraId = $rad['sId']; //henter avsender
      if ($til_gruppe != "Intet valgt") { //er mottaker gruppe valgt?
        $resultat = finnMottakerGruppe($tilkobling,$til_gruppe); //hent mottaker(e)  
        $antall = mysql_num_rows($resultat);
        if ($antall >= 1) { //finnes det minst en person tilknyttet valgt fagområde?     
          $dato = date("Y-m-d H:i:s");  //dato-eks: 2011-12-31, hvis ikke blir d feil i db, med 24t format
          while ($rad = mysql_fetch_assoc($resultat)) {
            $tilId = $rad['sId']; //hent sId
            $nyMeld = true; //er satt som boolean i databasen, men leses av som 0/1
            if (empty($emne)) { //er emne/tittel satt?
              $insert_sql = "INSERT INTO tblMeldinger (mTekst, mDatoTid, msFraId, msTilId, mLest) "; 
              $insert_sql .= "VALUES ('$melding', '$dato', $fraId, $tilId, $nyMeld);";    
            } else { //emne/tittel er satt
              $insert_sql = "INSERT INTO tblMeldinger (mTittel, mTekst, mDatoTid, msFraId, msTilId, mLest) ";
              $insert_sql .= "VALUES ('$emne', '$melding', '$dato', $fraId, $tilId, $nyMeld);";          
            } //if (empty($emne))
            mysql_query($insert_sql,$tilkobling);  
            //echo "<br />" . $insert_sql;          
          } //while                       
          echo "Meldingen ble sendt.";
          return true; //meldingen ble sendt, gi tilbakemelding til bruker                
        } else { //ingen personer tilknyttet dette fagområdet
          echo "Det finnes ingen brukere registrert på dette fagområdet. " . TILBAKE;
          return true; //ikke brukers feil at det ikke finnes deltakere, så gi tilbakemelding
        } //if ($antall >= 1)           
      } else { //mottakergruppe er ikke valgt
        return false;
      } //if ($til_gruppe != "Intet valgt")
    } //if (empty($melding))
    mysql_close($tilkobling); //lukker tilkobling til db
  } //sendTilMange
  
  /***********************************SETT FOCUS*********************************/ 
  function settFocus($sbId, $feilFnavn,$feilEnavn, $feilMottakergruppe, $feilMelding) {
    $tittel = setTittel($sbId); //hvilken tittel har siden (skal det sendes en eller mange)?
    if ($tittel == "Send melding") { //kun sending til en
      $focus = "focusFnavn();"; //sett default focus verdi i fornavn
    } else { //sending til mange
      $focus = "focusFagomrade();"; //sett default focus verdi i fagområde 
    } //if ($tittel == "Send melding")
    if (!empty($feilFnavn)) { //er fornavn ok?
      $focus = "focusFnavn();"; //sett focus i feltet for fornavn
    } else if (!empty($feilEnavn)) { //er etternavn ok?
      $focus = "focusEnavn();"; //sett focus i feltet for etternavn
    } else if (!empty($feilMottakergruppe)) { //er fagområde ok?  
      $focus = "focusFagomrade();"; //sett focus i select for fagområde    
    } else if (!empty($feilMelding)) {
      $focus = "focusMelding();";         	
    } //if (!empty($feilFnavn))
    return $focus; //returner focus
  } //settFocus
?>