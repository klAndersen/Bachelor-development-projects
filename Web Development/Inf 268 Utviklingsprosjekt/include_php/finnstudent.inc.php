<?php
  /**********************************SØKING ETTER BRUKERE***********************/   
  function finnVedNavn() { //let opp bruker via navn
    $tilkobling = kobleTil(); //opprett tilkobling til db 
    $finnFnavn = mysql_real_escape_string($_POST['finnFnavn']);
    $finnEnavn = mysql_real_escape_string($_POST['finnEnavn']);
    if (empty($finnFnavn) && empty($finnEnavn)) { //sjekker at feltene ikke er tomme      
      echo "<font color='red'>Dersom du skal søke på navn må minst et av feltene være utfylte.</font>";    
    } else { //noe er skrevet inn i et av søkefeltene 
      if (empty($finnFnavn)) { //fornavn er blankt      
        $sql = "SELECT * FROM tblstudent, tblCampus, tblFagomrade ";
        $sql .= "WHERE sEtternavn LIKE '".$finnEnavn."%' AND scId = cId AND sfId = fId;";
      } else if (empty($finnEnavn)) { //etternavn er blankt
        $sql = "SELECT * FROM tblstudent, tblCampus, tblFagomrade ";
        $sql .= "WHERE sFornavn LIKE '".$finnFnavn."%' AND scId = cId AND sfId = fId;";             	
      } else { //begge felter er fylt ut
        $sql = "SELECT * FROM tblstudent, tblCampus, tblFagomrade "; 
        $sql .= "WHERE sFornavn LIKE '".$finnFnavn ."%' AND sEtternavn LIKE '".$finnEnavn."%'";
        $sql .= "AND scId = cId AND sfId = fId;";
      } //if (empty($finnFnavn) && empty($finnEnavn))
      //echo $sql . "<br />";                    
      $resultat = mysql_query($sql, $tilkobling);
      $antall = mysql_num_rows($resultat);
      $sokeResultat = finnBrukere($antall,$resultat); 
      mysql_close($tilkobling); //lukker tilkobling
      return $sokeResultat;
    } //if (empty($finnFnavn) && empty($finnEnavn))    
  } //finnVedNavn
  
  function finnVedMail() { //let opp bruker via mail
    $tilkobling = kobleTil(); //opprett tilkobling til db 
    $finnMail = mysql_real_escape_string($_POST['finnEmail']);
    if (empty($finnMail)) { //mail feltet er blankt      
      return "<font color='red'>Du må skrive inn en e-post.</font>";
    } else { //e-post er skrevet inn 
      $sql = "SELECT * FROM tblstudent, tblCampus, tblFagomrade ";
      $sql .= "WHERE sEpost ='$finnMail' AND scId = cId AND sfId = fId;";
      $resultat = mysql_query($sql, $tilkobling);
      $antall = mysql_num_rows($resultat);
      $sokeResultat = finnBrukere($antall,$resultat); 
      mysql_close($tilkobling); //lukker tilkobling
      return $sokeResultat;
    } //if (empty($finnMail))    
  } //finnVedMail
  
  function finnBrukere($antall,$resultat) {  
    if ($antall == 0) { //ingen treff    
      $tekst = "Beklager, personen du søkte etter er ikke registrert.
      "; //linjeskift for bedre oversikt i kildekoden
    } else { //et eller flere treff    
      $tekst = "<table style='border-collapse:collapse'>
      <tr><td class='ramme' style='width:180px;'>Navn:</td>
      <td class='ramme' style='width:100px;'>Campus:</td>
      <td class='ramme' style='width:200px;'>Fagområde:</td>
      <td class='ramme' style='width:100px;'>Årskull:</td>
      <td class='ramme' style='width:100px;'>Profil:</td>
      <td class='ramme' style='width:120px;'>Send melding:</td></tr>
      "; //linjeskift for bedre oversikt i kildekoden     
      while ($rad = mysql_fetch_assoc($resultat)) { //legger verdiene inn i en tabell
        $sId = $rad['sId'];
        $fnavn = $rad['sFornavn'];
        $enavn =  $rad['sEtternavn'];
        $email = $rad['sEpost'];
        $campus = $rad['cNavn'];
        $fagomraade = $rad['fNavn'];
        $aarskull = $rad['sAarskull'];
        //legger resultatet inn i tabellen
        $tekst .= "<tr><td class='ramme'>$fnavn $enavn</td>
        <td class='ramme'>$campus</td>
        <td class='ramme'>$fagomraade</td>
        "; //linjeskift for bedre oversikt i kildekoden
        if (!empty($aarskull)) { //har bruker(ene) satt årskull?
          $tekst .= "<td class='ramme'>$aarskull</td>
          "; //linjeskift for bedre oversikt i kildekoden
        } else { //ikke valgt årskull
          $tekst .= "<td class='ramme'>Ikke satt</td>
          "; //linjeskift for bedre oversikt i kildekoden
        } //if (!empty($aarskull))
        $tekst .= "<td class='ramme'><a href='profil.php?visProfil=$email'>Vis profil</a></td>
        <td class='ramme'><a href='melding.php?melding=Send Melding&mottaker=$email'>Send melding</a></td></tr>
        "; //linjeskift for bedre oversikt i kildekoden    
      } //while 
      $tekst .= "</table>
      "; //linjeskift for bedre oversikt i kildekoden
    } //if ($antall == 0)   
    return $tekst; //returner resultatet av søket
  } //finnBrukere  
  
  /***********************************VISNING AV BRUKERE************************/   
  function visFinnStudenter() {     
    $visning = $_GET['visning']; //sjekker hva som er satt til visning         
    if ($visning == "visAlle") { //vis alle registrerte
      $svar = 'Dette er en liste over alle registrerte brukere på Hibu Alumni:
      <!--Luft-->
      <br /><br />
      <!--Viser alle registrerte-->
      '; //linjeskift for bedre oversikt i kildekoden  
      $tilkobling = kobleTil(); //opprett tilkobling til db 
      //henter ut alle registrerte sortert på etternavn
      $sql = "SELECT * FROM tblstudent, tblBrukertype,tblCampus, tblFagomrade ";
      $sql .= "WHERE sbId = bId AND scId = cId ";
      $sql .= "AND sfId = fId ORDER BY sEtternavn ASC;"; 
      $resultat = mysql_query($sql, $tilkobling); 
      $antall = mysql_num_rows($resultat);
      $svar .= finnBrukere($antall, $resultat); 
      mysql_close($tilkobling); //lukker tilkobling til db         
    } else if ($visning == "sokNavn") { //søk på navn
      $svar = visSkjemaNavn();    
      if (isset($_POST['FINNNAVN']) && $_POST['FINNNAVN'] == "Søk på navn") { //søker på navn
        $svar = "<br />" .  finnVedNavn();
      } //if (isset($_POST['FINNNAVN']))           
    } else { //søk på e-post
      $svar = visSkjemaEpost();  
      if (isset($_POST['FINNMAIL']) && $_POST['FINNMAIL'] == "Søk på e-post") { //søker på e-post
        $svar = "<br />" . finnVedMail();
      } //if (isset($_POST['FINNMAIL']))
    } //if($visning == "visAlle")                  
    return $svar;    
  } //visFinnStudenter
  
  function visSkjemaNavn() {
    echo '<!--Søk på navn-->
    <table>  
    <tr><td>Her kan du søke opp registrerte medlemmer via navn.</td></tr>
    <tr><td>Du kan velge å søke bare på fornavn/etternavn eller begge.</td></tr>
    </table>
    <!--Luft-->
    <br />
    <!--Skjema for søking på navn-->
    <table>
    <form action="" method="post" onsubmit="return sokPaaNavn();">                      
    <tr><td>Fornavn:</td>
    <td><input type="text" id="finnFnavn" name="finnFnavn" /></td>
    <td>Etternavn:</td>
    <td><input type="text" id="finnEnavn" name="finnEnavn" /></td>
    <td><input type="submit" name="FINNNAVN" value="Søk på navn" />
    </td></tr>
    </form>
    </table>
    '; //linjeskift for bedre oversikt i kildekoden
  } //visSkjemaNavn
  
  function visSkjemaEpost() {
    echo '<!--Søk på e-post-->
    Her kan du søke opp registrerte medlemmer via e-post:
    <!--Luft-->
    <br /><br />
    <!--Skjema for søking på e-post-->
    <form action="" method="post" onsubmit="return sjekkMail();">
    <table>
    <tr><td>E-post:</td>
    <td><input type="text" id="email" name="finnEmail" /></td>
    <td><input type="submit" name="FINNMAIL" value="Søk på e-post" /></td>
    </tr>
    </table>
    </form>
    '; //linjeskift for bedre oversikt i kildekoden
  } //visSkjemaEpost
?>