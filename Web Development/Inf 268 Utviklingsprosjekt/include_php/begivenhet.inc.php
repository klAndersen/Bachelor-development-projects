<?php
  /****************************VIS BEGIVENHETER*********************************/
  function visBegivenhet() { //viser alle "aktive" begivenheter
    $tilkobling = kobleTil(); //kobler til db         
    $sql = "SELECT * FROM tblBegivenhet, tblFagomrade "; 
    $sql .= "WHERE bfId=fId "; 
    $sql .= "AND begStart <= NOW() "; 
    $sql .= "AND begSlutt >= NOW() "; 
    $sql .= "ORDER BY begStart desc;";
    $resultat = mysql_query($sql, $tilkobling);
    $antall = mysql_num_rows($resultat);
    if ($antall >= 1) { //det finnes hvertfall en "aktiv" begivenhet/nyhet
      while ($rad = mysql_fetch_assoc($resultat)) {        
        $bNavn = $rad['begNavn']; 
        $bInfo = $rad['begInfo'];
        $fNavn = $rad['fNavn'];  
        $bStart = explode("-", $rad['begStart']); 
        $bSlutt = explode("-", $rad['begSlutt']);      
        //splitter opp og re-organiserer visning av dato
        $fraDato = $bStart[2] ."/". $bStart[1] ."-". $bStart[0]; 
        $tilDato = $bSlutt[2] ."/". $bSlutt[1] ."-". $bSlutt[0];
        echo '<h2>'.$bNavn.'</h2> 
              <center>              
              <table>
              <tr><td style="width:400px;"><strong>Startet:</strong> '.$fraDato.'</td></tr>
              <tr><td style="width:400px;"><strong>Avsluttes:</strong> '.$tilDato.'</td></tr>
              <tr><td style="width:400px;"><strong>Begivenheten gjelder for:</strong> '.$fNavn.'</td></tr>
              <tr><td style="width:400px;"><strong>Informasjon:</strong> '.$bInfo.'</td></tr>
              </table>
              </center>
              <br />';      
      } //while
    } else { //ingen aktive/opprettede begivenheter
      echo "Ingen begivenheter.";
    } //if ($antall >= 1)
    mysql_close($tilkobling); //lukker tilkobling til db
  } //visBegivenhet
  
  function visEnBegivenhet() { //viser en spesifikk "aktiv" begivenhet
    $tilkobling = kobleTil(); //koble til db
    $idBeg = mysql_real_escape_string($_GET['bId']); //hent begivenheten fra link
    //siden denne hentes fra link, omslutter jeg denne med en mysql_real_escape_string
    //for å sikre at noen ikke prøver å kjøre spørring fra URL-feltet
    if (isset($idBeg)) { //kom man til siden fra en link?      
      $sql = "SELECT * FROM tblBegivenhet,tblFagomrade ";
      $sql .= "WHERE idBeg = $idBeg AND bfId=fId ";
      $sql .= "AND begStart <= NOW() "; 
      $sql .= "AND begSlutt >= NOW();"; 
      $resultat = mysql_query($sql, $tilkobling);
      $antall = mysql_num_rows($resultat);
      //echo $sql;
      if ($antall == 1) { //finnes begivenheten?      	      
        $rad = mysql_fetch_assoc($resultat); //forventer kun et resultat, derfor ingen løkke 
        $bNavn = $rad['begNavn'];       
        $fNavn = $rad['fNavn']; 
        $bInfo = $rad['begInfo']; 
        $bStart = explode("-", $rad['begStart']); 
        $bSlutt = explode("-", $rad['begSlutt']);      
        //splitter opp og re-organiserer visning av dato
        $fraDato = $bStart[2] ."/". $bStart[1] ."-". $bStart[0]; 
        $tilDato = $bSlutt[2] ."/". $bSlutt[1] ."-". $bSlutt[0];                                         
        echo '<!--Viser valgt begivenhet-->
              <h2>'.$bNavn.'</h2><br /> 
              <center>
              <table>
              <tr><td><strong>Startet:</strong> '.$fraDato.'</td></tr>
              <tr><td><tr><td><strong>Avsluttes:</strong> '.$tilDato.'</td></tr>
              <tr><td><strong>Begivenheten gjelder for:</strong> '.$fNavn.'</td></tr>
              <tr><td><strong>Informasjon:</strong> '.$bInfo.'</td></tr>
              </table>
              </center>';     
      } else { //begivenhet finnes ikke/forsøkt endre link
        echo 'Obs! Denne begivenheten finnes ikke...';
      } //if ($antall == 1)      
    } //if (isset($idBeg))  
    mysql_close($tilkobling); //lukk tilkobling til db                
  } //visEnBegivenhet
?>