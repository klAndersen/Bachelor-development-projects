<?php
  /***************************************************************************** 
  *  Dette dokumentet inneholder kode som er felles flere av sidene            *
  *  Det er noen plasser lagt til utvidet kommentarfelt for å få en bedre      *
  *  utdypning av funksjonens poeng/oppbygging                                 *
  *  Laget av Knut Lucas Andersen                                              *    
  *****************************************************************************/                                                                                          
  function aapneFiler($mappeNavn, $lopper) {    
    $mappeRef = opendir($mappeNavn); //åpner mappen  
    if ($lopper) { //skal fil for loppemarked åpnes?      
      $gjeldendeAar = date('Y'); //henter ut året som er nå
      $xmlFil = $mappeNavn."loppemarked$gjeldendeAar.xml"; //henter ut fra nyeste
    } else {      
      $xmlFil = $mappeNavn."avdeling.xml"; //setter nytt filnavn    
    } //if ($lopper)     
    while ($neste = readdir($mappeRef)) { //henter ut filene
      $filNavn = $mappeNavn . $neste; //leser filer 
      //hvis filene er av filtype xml og navn er den som skal åpnes     
      if (strstr($filNavn, "xml") && $filNavn === $xmlFil) {          
        $xmlFil = $filNavn;        
        //echo "Fil: $xmlFil <br />"; //skriver ut filnavn for testformål        
      } //if (strstr($filnavn, "xml")
    } //while
    closedir($mappeRef); //lukker mappen 
    if (!file_exists($xmlFil)) { //finnes filen?
      return null; //fil finnes ikke, returner null      	
    } //if (!file_exists($xmlFil))       
    return $xmlFil; //returner filnavn
  } //aapneFiler              
    
  function lastInnXml($xmlFil, $xslFil, $fil) {
    //funksjon som laster inn xml og xsl og lager en html av denne
    $xml1 = simplexml_load_file($xmlFil);
    $xml2 = simplexml_load_file($xslFil);
    $xsltp = new XSLTProcessor;
    $xsltp -> importStyleSheet($xml2);
    $xsltp -> transformToURI($xml1, $fil);
    header("Location:" . $fil);
  } //lastInnXml
  
  function skrivAvdelingFil($avdeling) {
    //denne funksjonen skriver kun avdelinger til seperat fil 
    //dette fordi avdelinger er det eneste som ikke forandres fra år til år
    //(om det endres så vil gamle navn forbli i eldre filer)
    $mappe = "xml_kontroll/";
    $filNavn = $mappe."avdeling.xml";          
    $innhold = returnHeading(); //sett heading
    $xsd = "avdeling.xsd"; //navn på xsd-fil
    $xmlns = '"http://www.w3.org/2001/XMLSchema-instance"'; //deklarering av xmlns        
    //string med deklarasjon til 
    $innhold .= "\n<avdelinger xmlns:xsi= $xmlns ";
    $innhold .= 'xsi:noNamespaceSchemaLocation="'. $xsd .'">'."\n"; //rot tagg     
    $tagg1 = '<avdeling avdId="'; //ytterste tagg
    $sluttTagg1 = "</avdeling>\n"; //avslutning ytterste tagg
    $tagg2 = "<avdelingNavn>";
    $sluttTagg2 = "</avdelingNavn>\n";
    $tagg3 = "<vekselLeder>";
    $sluttTagg3 = "</vekselLeder>\n";       
    $innhold .= skrivToTagger($avdeling, $tagg1, $sluttTagg1, $tagg2, $sluttTagg2, $tagg3, $sluttTagg3); 
    $innhold .= "</avdelinger>"; //avslutter rot tagg           
    filBehandling($filNavn, $innhold); //filbehandling  
  }  //skrivAvdelingFil
    
  function skrivAlle($avdeling, $person, $salg, $init) {
    $mappe = "lopper/";      
    $gjeldendeAar = date('Y'); //henter ut året som er nå
    $filNavn = $mappe."loppemarked$gjeldendeAar.xml"; //henter ut fra nyeste  
    $innhold = returnHeading(); //sett heading
    $xsd = "kontroll.xsd"; //navn på xsd-fil
    $xmlns = '"http://www.w3.org/2001/XMLSchema-instance"'; //deklarering av xmlns        
    //string med deklarasjon til 
    $innhold .= "\n<skolekorps xmlns:xsi= $xmlns ";
    $innhold .= 'xsi:noNamespaceSchemaLocation="'. $xsd .'">'; //rot tagg 
    $innhold .= skrivAvdelinger($avdeling); //skriver avdelinger    
    $innhold .= skrivPerson($person); //skriver person
    $innhold .= skrivSalg($salg); //skriver salg
    $innhold .= skrivInit($init); //skriver init
    $innhold .= "\n</skolekorps>"; //avslutter rot tagg   
    filBehandling($filNavn, $innhold); //filbehandling 
    //ligger det noe i array for avdeling
    //denne skal kun skrives til hvis er registrering av avdeling)
    //dette for å unngå tomme elementer og blanke felt siden
    //avdeling ligger i seperat fil
    if (!(empty($avdeling))) { 
    	skrivAvdelingFil($avdeling); //skriv avdeling(er) til seperat fil
    } //if (!(empty($avdeling))) {         
  } //skrivAlle
  
  function returnHeading() {
    //stringen måtte splittes for at den ikke skulle leses som avslutning på php-scriptet
    $heading = '<?xml version="1.0" encoding="UTF-8"?'.'>'; //start på xml-fil  
    return $heading; 
  } //returnHeading
  
  function filBehandling($filNavn, $innhold) {    
    $filref = fopen($filNavn, "w"); //åpner fil for skriving (hvis fil ikke finnes opprettes den) 
    flock($filref, LOCK_EX); //låser filen så ingen andre får gjort endringer
    fwrite($filref, $innhold); //skriver innholdet til fil    
    flock($filref, LOCK_UN); //fjerner låsen
    fclose($filref); //lukker fil 
  } //filBehandling
  
  /*****************OPPRETTING AV TAGGER OG OVERSENDING AV DISSE****************/
  function skrivAvdelinger($avdeling) {    
    $innhold = "\n<avdelinger>\n"; //omsluttende tagg
    $tagg1 = '<avdeling avdId="'; //ytterste tagg
    $sluttTagg1 = "</avdeling>\n"; //avslutning ytterste tagg
    $tagg2 = "<avdelingNavn>";
    $sluttTagg2 = "</avdelingNavn>\n";
    $tagg3 = "<vekselLeder>";
    $sluttTagg3 = "</vekselLeder>\n";
    $innhold .= skrivToTagger($avdeling, $tagg1, $sluttTagg1, $tagg2, $sluttTagg2, $tagg3, $sluttTagg3);
    $innhold .= "</avdelinger>"; //avslutning omsluttende tagg
    return $innhold; 
  } //skrivAvdelinger
  
  function skrivPerson($person) {    
    $innhold = "\n<personer>\n"; //omsluttende tagg
    $tagg1 = '<person personId="'; //ytterste tagg
    $sluttTagg1 = "</person>\n"; //avslutning ytterste tagg
    $tagg2 = "<navn>";
    $sluttTagg2 = "</navn>\n";
    $tagg3 = "<vekselLordag>";
    $sluttTagg3 = "</vekselLordag>\n";
    $tagg4 = "<levertLordag>";
    $sluttTagg4 = "</levertLordag>\n";
    $tagg5 = "<vekselSondag>";
    $sluttTagg5 = "</vekselSondag>\n";
    $tagg6 = "<levertSondag>";
    $sluttTagg6 = "</levertSondag>\n";
    $tagg7 = '<avdeling avdId="';    
    $innhold .= skrivPersonTilFil($person, $tagg1, $sluttTagg1, $tagg2, $sluttTagg2, $tagg3, $sluttTagg3,
                               $tagg4, $sluttTagg4, $tagg5, $sluttTagg5, $tagg6, $sluttTagg6, $tagg7);
    $innhold .= "</personer>"; //avslutning omsluttende tagg 
    return $innhold;  
  } //skrivSalg
    
  function skrivSalg($salg) {             
    $innhold = "\n<salg>\n"; //omsluttende tagg
    $tagg1 = '<salg salgId="'; //ytterste tagg
    $sluttTagg1 = "</salg>\n"; //avslutning ytterste tagg
    $tagg2 = "<dag>";
    $sluttTagg2 = "</dag>\n";
    $tagg3 = "<penger>";
    $sluttTagg3 = "</penger>\n";
    $tagg4 = '<person personId="';    
    $innhold .= skrivSalgTilFil($salg, $tagg1, $sluttTagg1, $tagg2, $sluttTagg2, $tagg3, $sluttTagg3,
                               $tagg4);
    $innhold .= "</salg>"; //avslutning omsluttende tagg 
    return $innhold;  
  } //skrivSalg
  
  function skrivInit($init) {        
    $innhold = "\n<init>\n"; //omsluttende tagg
    $tagg1 = '<init initId="'; //ytterste tagg
    $sluttTagg1 = "</init>\n"; //avslutning ytterste tagg
    $tagg2 = "<vekselStandard>";
    $sluttTagg2 = "</vekselStandard>\n";
    $tagg3 = "<vekselTotalt>";
    $sluttTagg3 = "</vekselTotalt>\n";
    $innhold .= skrivToTagger($init, $tagg1, $sluttTagg1, $tagg2, $sluttTagg2, $tagg3, $sluttTagg3);
    $innhold .= "</init>"; //avslutning omsluttende tagg 
    return $innhold;
  } //skrivAvdelinger

  /***********************GENERERING AV INNHOLD OG TAGGER***********************/
  /***************************************************************************** 
  *  Underliggende funksjoner generer selve innholdet som legger tagger og     *
  *  innhold inn i en variabel $innhold som blir returnert til funksjonene     *
  *  over. Avdeling og Init benytter seg av samme funksjon, men grunnet        *
  *  mengden tagger på salg og person, måtte jeg lage egendefinerte versjoner  *
  *  for disse (grunnlaget er likt for disse to)                               *  
  *  Alle funksjonene har vedlagt en "utkommentert" linje som skriver ut på    *
  *  skjerm hvilken verdi variablene har og hva som ligger inni tabellen.      *
  *  Disse følger med i tilfelle endringer på senere tidspunkt så kan          *
  *  applikasjonsprogrammer bruke disse for enklere feilsøking                 *
  *****************************************************************************/  
  function skrivToTagger($array, $tagg1, $sluttTagg1, $tagg2, $sluttTagg2, $tagg3, $sluttTagg3) { 
    //denne funksjonen baserer på at det kun finnes to tagger under hoved tagg (eks: avdeling og init) 
    $x = 0; //initiering: variabel for om det skal vises avdId, avdNavn eller vekselLeder 
    $y = 0; //initiering: henter ut selve avdId, avdNavn eller vekselLeder basert på $x    
    $innhold = ""; //initering    
    for ($i = 0; $i < count($array); $i++) { //løkke som går gjennom første del av array       
      for ($j = 0; $j < count($array[$i]); $j++) { //løkke som går gjennom andre del av array
        if ($x == 0) { //er man på første element (identifikator)?
          $innhold .= $tagg1 . $array[$x][$y] . '">' . "\n"; //skriver inn attributt id
        } else if ($x == 1) { //er man på andre element?
          $innhold .= $tagg2 . $array[$x][$y] . $sluttTagg2;
        } else if ($x == 2) { //er man på tredje element?
          $innhold .= $tagg3 . $array[$x][$y] . $sluttTagg3;
        } // if ($x == 0)
        //utskrift for testformål:
        //echo '$x er: ' . $x . ' og $y er: '. $y . ' og tabellutskrift er: ' .$array[$x][$y]."<br />";        
        $x++; //øker x for å hente ut neste         
        if ($x == count($array)) { //er $x like stor som antall elementer?               
          $x = 0; //setter $x til å starte fra første element          
          $y++; //øker y (henter ut neste "element")
          $innhold .= $sluttTagg1;          
        } //if ($x == count($avdeling))  
      } //indre for     
    } //ytre for      
    return $innhold; //returner innhold      
  } //skrivTilFil
  
  function skrivPersonTilFil($array, $tagg1, $sluttTagg1, $tagg2, $sluttTagg2, $tagg3, $sluttTagg3,
                               $tagg4, $sluttTagg4, $tagg5, $sluttTagg5, $tagg6, $sluttTagg6, $tagg7) {
    $x = 0; //initiering: variabel for om det skal vises personId, navn, etc
    $y = 0; //initiering: henter ut selve  personId, navn, etc basert på $x    
    $innhold = ""; //initering       
    for ($i = 0; $i < count($array); $i++) { //løkke som går gjennom første del av array       
      for ($j = 0; $j < count($array[$i]); $j++) { //løkke som går gjennom andre del av array
      // echo '$x er: ' . $x . ' $i er: ' . $i . '$j er: ' . $j . ' $y er: ' . $y . "<br />";        
        switch($x) {
          case 0: 
            $innhold .= $tagg1 . $array[$x][$y] . '">' . "\n";          
          break;        
          case 1:
            $innhold .= $tagg2 . $array[$x][$y] . $sluttTagg2;
          break;
          case 2:
            $innhold .= $tagg3 . $array[$x][$y] . $sluttTagg3;
          break;
          case 3:
            $innhold .= $tagg4 . $array[$x][$y] . $sluttTagg4;
          break;
          case 4:
            $innhold .= $tagg5 . $array[$x][$y] . $sluttTagg5;
          break;
          case 5:
            $innhold .= $tagg6 . $array[$x][$y] . $sluttTagg6;
          break;
          case 6:
            $innhold .= $tagg7 . $array[$x][$y] . '" />' . "\n";
          break;
        } //switch
        //utskrift for testformål:
        //echo '$x er: ' . $x . ' og $y er: '. $y . ' og tabellutskrift er: ' .$array[$x][$y]."<br />";        
        $x++; //øker x for å hente ut neste         
        if ($x == count($array)) { //er $x like stor som antall elementer?               
          $x = 0; //setter $x til å starte fra første element         
          $y++; //øker y (henter ut neste "element")
          $innhold .= $sluttTagg1;          
        } //if ($x == count($avdeling))  
      } //indre for     
    } //ytre for      
    return $innhold; //returner innhold      
  } //skrivPersonTilFil
  
  function skrivSalgTilFil($array, $tagg1, $sluttTagg1, $tagg2, $sluttTagg2, $tagg3, $sluttTagg3,
                               $tagg4) {
    $x = 0; //initiering: variabel for om det skal vises salgId, penger, etc 
    $y = 0; //initiering: henter ut selve salgId, penger, etc basert på $x   
    $innhold = ""; //initering       
    for ($i = 0; $i < count($array); $i++) { //løkke som går gjennom første del av array       
      for ($j = 0; $j < count($array[$i]); $j++) { //løkke som går gjennom andre del av array
        switch($x) {
          case 0:      
            $innhold .= $tagg1 . $array[$x][$y] . '">' . "\n";          
          break;        
          case 1:
            $innhold .= $tagg2 . $array[$x][$y] . $sluttTagg2;
          break;
          case 2:
            $innhold .= $tagg3 . $array[$x][$y] . $sluttTagg3;          
          break;
          case 3:
            $innhold .= $tagg4 . $array[$x][$y] . '" />' . "\n";
          break;
        } //switch
        //utskrift for testformål:
        //echo '$x er: ' . $x . ' og $y er: '. $y . ' og tabellutskrift er: ' .$array[$x][$y]."<br />";        
        $x++; //øker x for å hente ut neste         
        if ($x == count($array)) { //er $x like stor som antall elementer?               
          $x = 0; //setter $x til å starte fra første element         
          $y++; //øker y (henter ut neste "element")
          $innhold .= $sluttTagg1;          
        } //if ($x == count($avdeling))  
      } //indre for     
    } //ytre for      
    return $innhold; //returner innhold   
  } //skrivSalgTilFil  
  
  /***************ARRAY FOR DE FORSKJELLIGE "DATABASE-TABELLENE"****************/  
  /***************************************************************************** 
  *  Det kunne her vært en løsning å laste alle fra loppemarked.xml inn i en   *
  *  array men jeg valgte å laste de i forskjellige array's i tilfelle det     *
  *  senere skulle bli behov for endring/omorganisering, så slipper man å      *
  *  splitte opp og måtte lage nye arrays                                      *
  *****************************************************************************/ 
   
  function fyllAvdelingArray() {
    $mappe = "xml_kontroll/";
    $fil = aapneFiler($mappe, false);  
    $avdArray = array();          
    if (empty($fil)) { //ble fil åpnet?   
      return null; //fil ikke åpnet, return null
    } //if (empty($fil))          
    $xml = simplexml_load_file($fil); 
    $i = 0; //initering
    foreach($xml->children() as $child) {
      //oppretter tabeller for alle elementenes verdier
      $id[$i] = $child['avdId']; //henter og legger inn avdId'n på plass $i
      $avdNavn[$i] = $child->avdelingNavn;
      $vLeder[$i] = $child->vekselLeder;
      //oppretter en assosiativ array som inneholder tabellene
      $avdArray = array($id, $avdNavn, $vLeder); 
      //Innhold: 0: avdelingsId 1: avdelingsnavn 2: vekselLeder  
      $i++; //øker $i   
    } //foreach            
    //print_r($avdArray); //printer ut innholdet av array'n                
    return $avdArray;
  } //fyllAvdelingArray
  
  function fyllPersonArray() {
    $mappe = "lopper/";
    $fil = aapneFiler($mappe, true);  
    $personArray = array();
    if (empty($fil)) { //ble fil åpnet?   
      return null; //fil ikke åpnet, return null
    } //if (empty($fil))          
    $xml = simplexml_load_file($fil); 
    $i = 0; //initering
    foreach($xml->personer->children() as $child) {
      //oppretter tabeller for alle elementenes verdier
      $id[$i] = $child['personId']; //henter og legger inn personId'n på plass $i
      $navn[$i] = $child->navn; 
      $vLordag[$i] = $child->vekselLordag; 
      $levLordag[$i] = $child->levertLordag; 
      $vSondag[$i] = $child->vekselSondag; 
      $levSondag[$i] = $child->levertSondag; 
      $avdeling[$i] =  $child->avdeling['avdId']; //henter og legger avdId'n på plass $i
      //oppretter en assosiativ array som inneholder tabellene
      $personArray = array($id, $navn, $vLordag, $levLordag, $vSondag, $levSondag, $avdeling); 
      //Innhold: 
      //0: personId 1: navn 2: vekselLørdag 3: levertLørdag 4: vekselSøndag 5: levertSøndag 6: avdelingsId     
      $i++; //øker $i
    } //foreach          
    //print_r($personArray); //printer ut innholdet av array'n
    return $personArray; //returner array 
  } //fyllPersonArray
  
  function fyllSalgArray() {        
    $mappe = "lopper/";
    $fil = aapneFiler($mappe, true); 
    $salgArray = array();
    if (empty($fil)) { //ble fil åpnet?   
      return null; //fil ikke åpnet, return null
    } //if (empty($fil))   
    $xml = simplexml_load_file($fil);          
    $i = 0; //initering                          
    foreach($xml->salg->children() as $child) {
      //oppretter tabeller for alle elementenes verdier
      $id[$i] = $child['salgId']; //henter og legger inn salgsId'n på plass $i
      $dag[$i] = $child->dag; //henter og legger inn dag på plass $i 
      $penger[$i] = $child->penger; //henter og legger inn penger på plass $i       
      $person[$i] =  $child->person['personId']; //henter og legger personId'n på plass $i
      //oppretter en assosiativ array som inneholder tabellene
      $salgArray = array($id, $dag, $penger, $person); 
      //Innhold: 0: salgID 1: dag 2: penger 3: aar 4: personID      
      $i++; //øker $i
    } //foreach
    //print_r($salgArray); //printer ut innholdet av array'n    
    return $salgArray; //returnerer array'n     
  } //fyllSalgArray
  
  function fyllInitArray() {
    $mappe = "lopper/";    
    $fil = aapneFiler($mappe, true); 
    $initArray = array();
    if (empty($fil)) { //ble fil åpnet?   
      return null; //fil ikke åpnet, return null
    } //if (empty($fil))   
    $xml = simplexml_load_file($fil); 
    $i = 0; //initering
    foreach($xml->init->children() as $child) {
      //oppretter tabeller for alle elementenes verdier
      $id[$i] = $child['initId']; //henter og legger inn initId'n på plass $i
      $vStandard[$i] = $child->vekselStandard;
      $vTotalt[$i] = $child->vekselTotalt;
      //oppretter en assosiativ array som inneholder tabellene
      $initArray = array($id, $vStandard, $vTotalt); 
      //Innhold: 0: initId 1: vekselStandard 2:vekselTotalt 
      $i++; //øker $i                    
    } //foreach
    //print_r($initArray); //printer ut innholdet av array'n
    return $initArray;    
  } //fyllInitArray          
?>