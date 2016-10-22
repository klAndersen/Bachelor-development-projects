<?php
  /***************************************************************************** 
  *  Dette dokumentet inneholder kode som brukes på reg-avdeling.php           *
  *  Tar for seg oppretting, redigering, sletting og visning av avdelinger     *
  *  Laget av Knut Lucas Andersen                                              *    
  *****************************************************************************/ 
  function lagreAvdeling() {
    //henter innholdet i array's
    $avdArray = fyllAvdelingArray();  
    $personArray = fyllPersonArray();
    $salgArray = fyllSalgArray(); 
    $initArray = fyllInitArray();          
    $avdId = 0; //initiering
    $avdName = $_POST['avdNavn']; //henter innskrevet avdelingsnavn
    $vLeder = $_POST['vekselLeder']; //henter innskrevet vekseleder                
    if (empty($avdName)) { //er avdelingsnavn skrevet inn?    
      return false;  
    } else if (strlen($avdName) < 5) { //er avdelingsnavn minst 5 tegn?
      return false;
    } else if (empty($vLeder)) { //er vekselleder skrevet inn?    
      return false;    
    } else if (!(is_numeric($vLeder))) { //er vekselleder gyldig (dvs: er det et tall?)    
      return false;
    } else { //felter er fylt ut, start lagring            
      if (!(empty($avdArray))) { //ligger det noe i array?
        $avdId = count($avdArray[0]); //henter størrelsen og angir den som ny id i array
      } //if (!(empty($avdArray)))
      $id = $avdId + 1; //øker id (pga array starter på 0, mens id'n starter på 1)
      //oppretter en string med xml
      $xmlAvdeling = '<avdelinger>
      <avdeling avdId="'.$id.'">
      <avdelingNavn>'.$avdName.'</avdelingNavn>
      <vekselLeder>'.$vLeder.'</vekselLeder>
      </avdeling>
      </avdelinger>';                          
      $xml = simplexml_load_string($xmlAvdeling); //konverter string til xml      
      foreach($xml->children() as $child) { //henter ut xml og legger i assosiativ array
        //oppretter tabeller for alle elementenes verdier
        $id = $child['avdId']; //henter ut avdId fra xml-stringen
        $avdNavn = $child->avdelingNavn;
        $vLeder = $child->vekselLeder;
        //legger inn bakerst den nye avdelingen
        $avdArray[0][$avdId] = $id;  
        $avdArray[1][$avdId] = $avdNavn;  
        $avdArray[2][$avdId] = $vLeder;         
      } //foreach              
      //print_r($avdArray);      
      skrivAlle($avdArray, $personArray, $salgArray, $initArray); //skriver endring til fil
    } //if                            
    return true;
  } //lagreAvdeling
  
  function redigerAvdeling() {
    $avdId = $_POST['avdId'];
    $nyttNavn = $_POST['avdNavn'];
    $vLeder = $_POST['vekselLeder'];
    //henter innholdet i tabellene
    $avdArray = fyllAvdelingArray();
    $personArray = fyllPersonArray();
    $salgArray = fyllSalgArray(); 
    $initArray = fyllInitArray();    
    $funnet = false; //initiering
    $i = 0; //initiering           
    if (empty($nyttNavn)) { //er avdelingsnavn skrevet inn? 
      return false;
    } else if (strlen($nyttNavn) < 5) { //er avdelingsnavn minst 5 tegn?
      return false;
    } else {                                                           
      //while løkka kjøres så lenge avdeling ikke er funnet/loopet gjennom hele array'n
      while (!$funnet || $i < count($avdArray[0])) { 
        if ($avdArray[0][$i] == $avdId) { //er array's avdId den som skal endres navn på?
          $funnet = true; //sett funnet til true (fullfører løkka)
          $avdArray[1][$i] = $nyttNavn; //sett inn nytt navn på avdelingen
          $avdArray[2][$i] = $vLeder; //sett ny vekselleder (hvis endret) på avdelingen
        } //if ($avdArray[0][$i] == $avdId)
        $i++; //øk $i
      } //while
      //print_r($avdArray);
      skrivAlle($avdArray, $personArray, $salgArray, $initArray); //skriver endring til fil
    } // if (empty($avdName))
    return true;
  } //redigerAvdeling
  
  /*        
  //kommentert ut pga den ikke fungerer, har ikke fått til dette, da en av to 
  har skjedd, enten blir det ikke slettet, ellers så slettes alt               
  function slett() {
    $avdId = $_POST['avdId'];
    $avdArray = fyllAvdelingArray(); //henter array for avdelinger           
      unset($avdArray[0][$avdId]);
      unset($avdArray[1][$avdId]);
      unset($avdArray[2][$avdId]);
      print_r($avdArray);
     //skrivAlle($avdArray); //skriver endring til fil
  } //slett
  */
    
  function redigerSkjema($feilAvdNavn, $feilVleder) { //oppretter form for redigering av avdeling
    $skjema = '<form action="reg-avdeling.php?avd=redAvd" method="post" onsubmit="return sjekkAvd();">
    <tr>
    <td>Skriv inn nytt navn:</td>
    <td><input type="text" name="avdNavn" id="avdNavn" /></td>
    <td>'.$feilAvdNavn.'</td>
    </tr>
    <tr>
    <td>Skriv inn vekselleder:</td>
    <td><input type="text" name="vekselLeder" id="vekselLeder" value="'.$_POST['vekselLeder'].'" /></td>
    <td>'.$feilVleder.'</td>
    </tr>
    <tr>
    <td><input type="hidden" name="avdId" id="avdId" value="'.$_POST['avdId'].'" /></td>
    <td><input type="submit" value="Lagre endring" name="LAGRE_ENDRING" /></td>
    </tr>
    </form>';
    return $skjema;
  } //redigerSkjema
  
  function opprettNyAvdeling($feilAvdNavn, $feilVleder) {
    echo '<fieldset style="width: 700px;">
    <legend>Registrer ny avdeling - Alle felt må fylles ut</legend>
    <!--Luft-->
    <br /><br />
    Her kan nye avdelinger opprettes. Kravene er at avdelingsnavn må være minst fem tegn<br />    
    og vekselleder må være et positivt heltall.
    <!--Luft--> 
    <br /><br /><br />    
    <table>
    <form  method="post" action="" onsubmit="return sjekkAvd();">
    <tr>
    <td>Avdelingsnavn:</td><td><input type="text" name="avdNavn" id="avdNavn" /></td>
    <td>'.$feilAvdNavn.'</td>
    </tr>
    <tr>
    <td>Vekselleder:</td><td><input type="text" name="vekselLeder" id="vekselLeder" /></td>
    <td>'.$feilVleder.'</td>    
    </tr>
    <tr><td><input type="submit" value="Lagre ny avdeling" name="LAGRE_AVD" /></td></tr>
    </form> 
    </table>
    </fieldset>';
  } //opprettNyAvdeling
  
  function genererAvdelingside() {
    $feilAvdNavn = "";
    $feilVleder = "";
    if (isset($_POST['LAGRE_AVD'])) {
      if (!(lagreAvdeling())) { //ble avdeling lagret?      
        $feilAvdNavn = feilAvdelingnavn(); //hent feilmelding
        $feilVleder = feilVekselLeder();
        opprettNyAvdeling($feilAvdNavn, $feilVleder);
      } else { //avdeling ble lagret
        echo "Avdelingen ble opprettet. <br />
        <!--link til hovedsiden (lastes via buffer.php)-->
        <a href='buffer.php?buffer=avdeling'>Til avdelingsiden</a>";
      } //if (!(lagreAvdeling))
    } else { //vis skjema    
      opprettNyAvdeling($feilAvdNavn, $feilVleder);
    } //if (isset($_POST['LAGRE_AVD']))
  } //genererAvdelingside
?>