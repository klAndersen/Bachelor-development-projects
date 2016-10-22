<?php  
  /*****************************************************************************
   * Dette dokumentet brukes på både res-lørdag.php og på res-søndag.php       *
   * Grunnlaget for oppbyggingen er den samme som brukes på samlet.inc.php     *
   * med noen utvidelser her iforhold til utregning og uthenting               *
   *  Laget av Knut Lucas Andersen                                             *                                                                        
   ****************************************************************************/     
  function printHelg($dag) {
    $resultat = hentResultat($dag);
    $i = 0; //initering
    $salg = 0;
    $veksel = 0;
    $antPers = 0;
    $sumAvd = 0;   
    $antall = 0;     
    //print_r($resultat);
    echo "<table border='1'>
    <th>Avdeling</th><th>Vekslepenger</th><th>Salg</th><th>Sum</th>";
    while($i < count($resultat[0])) { 
      $id = (int)$resultat[0][$i]; //hent id 
      $avdNavn = $resultat[1][$i]; //hent navn                
      for($j = 0; $j < count($resultat[0]); $j++) {       
        if($id == $resultat[0][$j]) { //er id funnet?           
          $funnet[$i] = $id; //legg id'n som ble funnet i en array   
          $antPers++;
          $veksel = $veksel + $resultat[2][$i];  
          $salg++;
          $sumAvd = $sumAvd + $resultat[3][$i];
        } //if         
      } //for        
      for($j = 0; $j < count($funnet); $j++) { //loop gjennom de id'r som har blitt vist      
        if ($funnet[$j] == $id) { //er id funnet?
          $antall++; //øk antallet ganger id er funnet
        } //if ($funnet[$j] == $id)
      } //for          
      if ($antall == 1) { //finnes det bare en av det som skal skrives ut?             
        echo "<tr>
        <td style='width: 200px;' align='left'>Antall personer på $avdNavn: $antPers</td>
        <td style='width: 200px;' align='right'>$veksel</td>
        <td style='width: 200px;' align='right'>Antall salg: $salg</td>
        <td style='width: 200px;' align='right'>$sumAvd</td>
        </tr>"; //skriver ut en tabellrad som inneholder verdiene
      } //if ($funnet == 1)
      $salg = 0; //nullstiller variablene
      $veksel = 0;
      $antPers = 0;
      $sumAvd = 0;
      $antall = 0;          
      $i++;
    } //while
    echo "</table>";  
  } //
  
  function hentResultat($hentDag) {
    //henter innholdet i arrays
    $avdArray = fyllAvdelingArray();
    $personArray = fyllPersonArray();
    $salgArray = fyllSalgArray();
    $x = 0; 
    if (empty($salgArray)) { //finnes det salg registrert?
      return null;
    } //if (empty($salgArray))
    //henter ut alle salgene
    for($i = 0; $i < count($salgArray[0]); $i++) { //loop gjennom  array for salg
      if ($salgArray[1][$i] == $hentDag) { //er dagen som skal brukes funnet?
        $dag[$x] = $salgArray[1][$i]; //hent dagen  
        $penger[$x] = $salgArray[2][$i]; //hent pengene    
        $s_pId[$x] = (int)$salgArray[3][$i]; //hent id'n for person 
        $x++; //øker $x     
      } //if ($salgArray[1][$i] == $hentDag)          
    } //for
    //henter ut personer og avdeling tilknyttet salg 
    $j = 0; //initiering; teller for while løkke
    while ($j < count($s_pId)) { //så lenge det finnes personer tilknyttet salg...
      for($a = 0; $a < count($personArray[0]); $a++) { //loop gjennom alle personene
        if ($personArray[0][$a] == $s_pId[$j]) { //er det match?          
          $p_avdId[$j] = (int)$personArray[6][$a]; //hent avdelingen personen er tilknyttet          
          if ($hentDag == "lørdag") { //er dagen som skal hentes ut lørdag?
            $veksel[$j] = $personArray[3][$a]; //hent veksel for lørdag
          } else { //dagen er søndag
            $veksel[$j] = $personArray[4][$a]; //hent veksel for søndag
          } //if ($hentDag == "lørdag")
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
    $tabell = array($avdId, $avdNavn, $veksel, $penger);   
    return $tabell; //returner array'n
  } //hentResultat
?>