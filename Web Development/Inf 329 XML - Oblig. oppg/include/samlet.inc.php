<?php
  /***************************************************************************** 
  *  Dette dokumentet inneholder kode som brukes på resultat.php               *
  *  printResultat() skriver ut et resultat basert på en assosiativ array      *
  *  som opprettes og fylles i funksjonen hentResultat().                      *
  *  Laget av Knut Lucas Andersen                                              *    
  *****************************************************************************/ 
  function printResultat() {
    $i = 0; //initiering    
    $lordag = 0;
    $sondag = 0;
    $sumLordag = 0;
    $sumSondag = 0;
    $sumAvd = 0; 
    $sumTot = 0; 
    $antall = 0;    
    $resultat = hentResultat();    
    if (empty($resultat)) { //finnes det noen salg? 
      return "Intet er registrert ennå.";    
    } //if (empty($resultat))
    echo '<table border="1">
    <th>Avdeling</th><th>Lørdag</th><th>Søndag</th><th>Sum avdeling</th>';       
    // print_r($resultat);          
    while($i < count($resultat[0])) { 
      $id = (int)$resultat[0][$i]; //hent id 
      $avdNavn = $resultat[1][$i]; //hent navn                
      for($j = 0; $j < count($resultat[0]); $j++) {       
        if($id == $resultat[0][$j]) { //er id funnet?           
          $funnet[$i] = $id; //legg id'n som ble funnet i en array        
          if ($resultat[2][$j] == "lørdag") { //er dagen lørdag?
            $lordag = $lordag + $resultat[3][$j]; //summer dagen          
          } else { //dagen er søndag
            $sondag = $sondag + $resultat[3][$j]; 
          } //if ($resultat[2][$j] == "lørdag")       
        } //if         
      } //for         
      for($j = 0; $j < count($funnet); $j++) { //loop gjennom de id'r som har blitt vist      
        if ($funnet[$j] == $id) { //er id funnet?
          $antall++; //øk antallet ganger id er funnet
        } //if ($funnet[$j] == $id)
      } //for         
      if ($antall == 1) { //finnes det bare en av avdelingen som skal skrives ut?
        $sumLordag = $sumLordag + $lordag;
        $sumSondag = $sumSondag + $sondag;
        $sumAvd = $lordag + $sondag; //regner ut summen for avdelingen              
        echo "<tr>
        <td style='width: 200px;' align='left'>$avdNavn</td>
        <td style='width: 200px;' align='right'>$lordag</td>
        <td style='width: 200px;' align='right'>$sondag</td>
        <td style='width: 200px;' align='right'>$sumAvd</td>
        </tr>"; //skriver ut en tabellrad som inneholder verdiene
      } //if ($funnet == 1)
      $lordag = 0; //nullstiller verdiene
      $sondag = 0;     
      $antall=0;   
      $i++; //øker $i
    } //while    
    $sumTot = $sumLordag + $sumSondag; //regner ut sum total
    echo "<tr><td>Sum</td><td align='right'>$sumLordag</td><td align='right'>$sumSondag</td>
    <td align='right'>$sumTot</td></tr>";
    echo "<tr><td colspan='3' ><strong>Loppemarked ". date('Y') ." samlet</strong></td>
    <td align='right'>$sumTot</td></tr>
    </table>"; //legger totalsum inntjent i nederste del av tabellen
  } // printResultat
  
  function hentResultat() {
    //henter innholdet i arrays
    $avdArray = fyllAvdelingArray();
    $personArray = fyllPersonArray();
    $salgArray = fyllSalgArray();      
    if (empty($salgArray)) { //finnes det salg registrert?
      return null;
    } //if (empty($salgArray))
    //henter ut alle salgene
    for($i = 0; $i < count($salgArray[0]); $i++) { //loop gjennom  array for salg
      $dag[$i] = $salgArray[1][$i]; //hent dagen
      $penger[$i] = $salgArray[2][$i]; //hent pengene
      $s_pId[$i] = (int)$salgArray[3][$i]; //hent id'n for person      
    } //for
    //henter ut personer og avdeling tilknyttet salg 
    $j = 0; //initiering; teller for while løkke
    while ($j < count($s_pId)) { //så lenge det finnes personer tilknyttet salg...
      for($a = 0; $a < count($personArray[0]); $a++) { //loop gjennom alle personene     
        if ($personArray[0][$a] == $s_pId[$j]) { //er det match?    
          $p_avdId[$j] = (int)$personArray[6][$a]; //hent avdelingen personen er tilknyttet         
        } //if ($personArray[0][$a] == $s_pId[$j])                
      } //for    
      $j++; //øker $j
    } //while                    
    //henter ut avdeling tilknyttet person
    $k = 0; //initiering; teller for while løkke   
    while ($k < count($p_avdId)) { //så lenge det finnes avdelinger tilknyttet salg...             
      for ($b = 0; $b < count($avdArray[0]); $b++) { //loop gjennom avdelingene  
        if ($avdArray[0][$b] == $p_avdId[$k]) { //er det match?
            $avdId[$k] = $avdArray[0][$b]; //hent ut avdelingsId'n
            $avdNavn[$k] = $avdArray[1][$b]; //hent ut avdelingsnavnet           
        } //if ($avdArray[0][$b] == $p_avdId[$k])         
      } //for   
      $k++; //øk $k            
    } //while            
    //opprett assosiativ tabell med verdiene som er hentet ut
    $tabell = array($avdId, $avdNavn, $dag, $penger);   
    return $tabell; //returner array'n
  } //hentResultat
?>