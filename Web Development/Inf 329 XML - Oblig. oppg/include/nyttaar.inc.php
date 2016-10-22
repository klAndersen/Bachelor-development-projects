<?php
  /***************************************************************************** 
  *  Dette dokumentet inneholder kode som brukes på nyttaar.php                *
  *  Her opprettes skjema og init som blir registrert blir videresendt         *
  *  for registrering/lagring til fil                                          *     
  *  Laget av Knut Lucas Andersen                                              *      
  *****************************************************************************/ 
  function lagreAAr() {
    //henter innholdet i array's
    $avdArray = fyllAvdelingArray();  
    $personArray = fyllPersonArray();
    $salgArray = fyllSalgArray(); 
    $initArray = fyllInitArray();          
    $initId = 0; //initiering
    $stdVeksel = $_POST['veksel']; //henter innskrevet veksel
    $vTotalt = $_POST['vTotalt']; //henter innskrevet total veksel                
    if (empty($stdVeksel)) { //er vekslepenger skrevet inn?    
      return false; 
    } else if (empty($vTotalt)) { //er total veksel skrevet inn?    
      return false;    
    } else if (!(is_numeric($stdVeksel))) { //er vekslepenger gyldig (dvs: er det et tall?)    
      return false;
    } else if (!(is_numeric($vTotalt))) { //er total veksel gyldig (dvs: er det et tall?)    
      return false;
    } else { //felter er fylt ut og korrekte, start lagring            
      if (!(empty($initArray))) { //ligger det noe i array?
        $initId = count($initArray[0]); //henter størrelsen og angir den som ny id i array
      } //if (!(empty($initArray)))
      $id = $initId + 1; //øker id (pga array starter på 0, mens id'n starter på 1)
      //oppretter en string med xml
      $xmlInit = '<init>
      <init initId="'.$id.'">
      <vekselStandard>'.$stdVeksel.'</vekselStandard>
      <vekselTotalt>'.$vTotalt.'</vekselTotalt>
      </init>
      </init>';                          
      $xml = simplexml_load_string($xmlInit); //konverter string til xml      
      foreach($xml->children() as $child) { //henter ut xml og legger i assosiativ array
        //oppretter tabeller for alle elementenes verdier
        $id = $child['initId']; //henter ut initId fra xml-stringen
        $stdVeksel = $child->vekselStandard;
        $vTotalt = $child->vekselTotalt;
        //legger inn ny init bakerst i array
        $initArray[0][$initId] = $id;  
        $initArray[1][$initId] = $stdVeksel;  
        $initArray[2][$initId] = $vTotalt;         
      } //foreach              
      //print_r($initArray);      
      skrivAlle($avdArray, $personArray, $salgArray, $initArray); //skriver endring til fil
    } //if                            
    return true;    
  } //lagreAAr

  function aarSkjema($feilVeksel, $feilTotal) {
    echo '<form action="" method="post" onsubmit="return sjekkInit();">
    <table>
    <tr>
    <td>Vekslepenger pr. selger</td>
    <td><input type="text" name="veksel" id="veksel" value="" /></td>
    <td>'.$feilVeksel.'</td>
    </tr>
    <tr>
    <td>Vekslepenger tatt ut av bank (totalt)</td>
    <td><input type="text" name="vTotalt" id="vTotalt" value="" /></td>
    <td>'.$feilTotal.'</td>
    </tr>
    <tr><td><input type="submit" name="LAGRE_AAR" value="Klargjør nytt år" /></td></tr>
    </table>
    </form>';
  } //aarSkjema
?>