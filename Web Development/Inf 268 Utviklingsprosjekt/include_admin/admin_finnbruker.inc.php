<?php
  function visAlleBrukere() {
    $svar = '<center>
    <form action="" method="post" onsubmit="return sokPaaNavn();">
    <table>
    <tr><td>Fornavn:</td><td><input type="text" name="finnFnavn" id="finnFnavn" /></td>
    <td>Etternavn:</td><td><input type="text" name="finnEnavn" id="finnEnavn" /></td>
    <td><input type="submit" name="FINNNAVN" value="Søk etter bruker" /></td></tr>
    </table>
    </form>
    <br />
    '; //linjeskift for bedre lesbarhet i kildekode    
    if (isset($_POST['FINNNAVN']) && $_POST['FINNNAVN'] == "Søk etter bruker") { //søker på navn
      $svar .= sokPaaNavn(); //hent resultatet fra søket            
    } else { //ikke søkt på navn, skriv ut alle registrerte
        $svar .= "<!--Viser alle registrerte-->
        "; //linjeskift  
        $tilkobling = kobleTil(); //opprett tilkobling til db 
        $sql = "SELECT * FROM tblstudent, tblBrukertype,tblCampus, tblFagomrade ";
        $sql .= "WHERE sbId = bId AND scId = cId ";
        $sql .= "AND sfId = fId ORDER BY sEtternavn ASC;";
        $resultat = mysql_query($sql, $tilkobling); 
        $svar .= listOppBrukere($resultat); 
        mysql_close($tilkobling); //lukker tilkobling til db                         
    } //if (isset($_POST['FINNNAVN']))          
    return $svar; //returner resultatet 
  } //visAlleBrukere
  
  /*************************SØKEFUNKSJON OG SØKERESULTAT*************************/
  function sokPaaNavn() {
    $finnFnavn = $_POST['finnFnavn'];
    $finnEnavn = $_POST['finnEnavn'];
    if (empty($finnFnavn) && empty($finnEnavn)) { //sjekker at feltene ikke er tomme
      $tekst = "<table>
      <tr><td>
      <font color='red'>Dersom du skal søke på navn må minst et av feltene være utfylte.</font>
      </td></tr>
      <tr><td>Eller så kan du <a href='admin_brukere.php'>se alle registrerte</a></td></tr>            
      </table>
      "; //linjeskift      
    } else { //har skrivd inn noe i et av feltene
      $tilkobling = kobleTil(); //opprett tilkobling til db  
      if (empty($finnFnavn)) { //fornavn er blankt      
        $sql = "SELECT * FROM tblstudent, tblBrukertype, tblCampus, tblFagomrade ";
        $sql .= "WHERE sEtternavn LIKE '".$finnEnavn."%' AND sbId = bId AND scId = cId AND sfId = fId;";
      } else if (empty($finnEnavn)) { //etternavn er blankt
        $sql = "SELECT * FROM tblstudent, tblBrukertype, tblCampus, tblFagomrade ";
        $sql .= "WHERE sFornavn LIKE '".$finnFnavn."%' AND sbId = bId AND scId = cId AND sfId = fId;";             	
      } else { //begge felter er fylt ut
        $sql = "SELECT * FROM tblstudent, tblBrukertype, tblCampus, tblFagomrade ";
        $sql .= "WHERE sFornavn LIKE '".$finnFnavn ."%' AND sEtternavn LIKE '".$finnEnavn."%'";
        $sql .= "AND sbId = bId AND scId = cId AND sfId = fId;";
      } //if (empty($finnFnavn) && empty($finnEnavn))
      $resultat = mysql_query($sql, $tilkobling);
      $antall = mysql_num_rows($resultat);
      $tekst = sokeResultat($antall,$resultat);      
    } //if (empty($finnFnavn) && empty($finnEnavn)
    return $tekst;
  } //sokPaaNavn
    
  function sokeResultat($antall,$resultat) {
    if ($antall == 0) { //ingen treff
      $tekst = "<table>
      <tr><td>Beklager, men personen du søkte etter er ikke registrert.
      <tr><td>Prøv igjen, eller <a href='admin_brukere.php'>se alle registrerte</a></td></tr>
      </table>";      
    } else { //et eller flere treff      
      $tekst = listOppBrukere($resultat); 
      $tekst .= "<br /><a href='admin_brukere.php'>Se alle registrerte</a>"; //legg med en "tilbake" link     
    } //if ($antall == 0)
    return $tekst; //skriver ut treff
  } //sokeResultat  
  
  function listOppBrukere($resultat) { //skriver ut resultatet 
  /*
  Satt inn avslutning på ny linje for bedre oversikt/lesbarhet i kildekoden
  */ 
    $tekst = "<!--Fyller table med brukere-->
    ";
    $tekst .= "<table style='border-collapse:collapse'>
    ";
    $tekst .= "<tr><td class='ramme' style='width:180px;'>Navn:</td>";
    $tekst .= "<td class='ramme' style='width:110px;'>Brukertype: </td>";
    $tekst .= "<td class='ramme' style='width:180px;'>E-mail: </td>";
    $tekst .= "<td class='ramme' style='width:100px;'>Campus: </td>";
    $tekst .= "<td class='ramme' style='width:200px;'>Fagområde: </td>";
    $tekst .= "<td class='ramme' style='width:110px;'>Karantene: </td>";
    $tekst .= "<td class='ramme' style='width:110px;'>Rediger bruker: </td></tr>
    ";
    //hent ut resultatene
    while ($rad = mysql_fetch_assoc($resultat)) { //legger verdiene inn i en tabell
      $sId = $rad['sId'];
      $btype = $rad['bType'];
      $fnavn = $rad['sFornavn'];
      $enavn =  $rad['sEtternavn'];
      $email = $rad['sEpost'];
      $campus = $rad['cNavn'];
      $fagomraade = $rad['fNavn'];
      //legger resultatet inn i tabellen
      $tekst .= "<tr><td class='ramme'>$fnavn $enavn</td>";        
      $tekst .= "<td class='ramme'> $btype</td>";
      $tekst .= "<td class='ramme'>$email</td>";
      $tekst .= "<td class='ramme'>$campus</td>";
      $tekst .= "<td class='ramme'>$fagomraade</td>";
      $tekst .= "<td class='ramme'><a href='karantene.php?brukerId=$sId'>Vis karantene</a>";
      $tekst .= "<td class='ramme'><a href='redigerBruker.php?brukerId=$sId'>Rediger</a>";
      $tekst .= "</td></tr>
      ";     
    } //while
    $tekst .= "</table>"; 
    return $tekst; 
  } //listOppBrukere  
?>