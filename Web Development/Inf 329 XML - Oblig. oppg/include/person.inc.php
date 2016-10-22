<?php
  /***************************************************************************** 
  *  Dette dokumentet inneholder kode som brukes på person.php                 *
  *  Tar for seg oppretting og visning av personer                             *
  *  Laget av Knut Lucas Andersen                                              *    
  *****************************************************************************/  
  function listPersoner() {
    $avdArray = fyllAvdelingArray();
    $personArray = fyllPersonArray(); 
    $personer = ""; //initiering
    $funnet = false; //initiering    
    if (!empty($personArray)) { //finnes det noen personer som er registrert?    
      for ($i = 0; $i < count($personArray[0]); $i++) {
        $navn = $personArray[1][$i]; //hent ut navn
        $vLordag = $personArray[2][$i]; //hent veksel for lørdag 
        $vSondag = $personArray[4][$i]; //hent veksel for søndag      
        $p_avdId = (int)$personArray[6][$i]; //hent avdelingsid'n 
        $j = 0; //sett $j til 0
        //while løkka kjøres så lenge avdeling ikke funnet/loopet gjennom hele array'n         
        while (!$funnet || $j < count($avdArray[0])) { 
          if ($avdArray[0][$j] == $p_avdId) { //er avdelingen funnet?
            $funnet = true; //sett funnet til true (fullfører løkka)
            $avdNavn = $avdArray[1][$j]; //sett inn nytt navn på avdelingen
          } //if ($avdArray[0][$i] == $avdId)
          $j++; //øk $i
        } //while
        //legger til en rad med navn, avdeling og veksel
        $personer .= "<tr>
        <td style='width:250px;'>$navn</td>
        <td style='width:250px;'>$avdNavn</td>
        <td style='width:250px;' align='right'>$vLordag</td>
        <td style='width:250px;' align='right'>$vSondag</td>
        </tr>";
        $funnet = false; //setter denne til false slik at søk starter på nytt for neste person   
      } //for
      return $personer; //returner tekst med registrert(e) person(er)
    } //if (!empty($personArray)) 
    return "<tr><td colspan='4'>Ingen personer er registrert så langt.</td></tr>";
  } //listPersoner   
    
  function lagrePerson() {
    //henter innholdet i array's
    $avdArray = fyllAvdelingArray();  
    $personArray = fyllPersonArray();
    $salgArray = fyllSalgArray(); 
    $initArray = fyllInitArray(); 
    $pId = 0; //initiering          
    $fnavn = $_POST['fNavn']; //henter innskrevet fornavn
    $enavn = $_POST['eNavn'];
    $levert = "nei"; //settes til nei pga antas personer registreres ved utlevering
    $veksel = $_POST['veksel'];
    $avdId = $_POST['avdeling'];
    $dag = $_POST['dag'];     
    $vekselLørdag = 0;
    $vekselSøndag = 0;
    if (empty($fnavn)) { //er fornavn skrevet inn?
      return false;  
    } else if (strlen($fnavn) < 2) { //er fornavn minst 2 tegn?
      return false;
    } else if (empty($enavn)) { //er etternavn skrevet inn?
      return false;  
    } else if (strlen($enavn) < 3) { //er etternavn minst 3 tegn?
      return false;
    } else if (empty($veksel)) { //er veksel skrevet inn?
      return false;    
    } else if ($avdId == "velg_avd") { //er avdeling valgt?
      return false;
    } else if ($dag == "velg_dag") { //er dag valgt?
      return false;
    } else { //felter er fylt ut, start lagring         
      if (!(empty($personArray))) { //ligger det noe i array?
        $pId = count($personArray[0]); //henter størrelsen og angir den som ny id i array
      } //if (!(empty($personArray)))      
      $id = $pId + 1; //øker id (pga array starter på 0, mens id'n starter på 1)
      $fulltNavn = "$fnavn $enavn"; //legger navnet i en string
      //oppretter en string med xml
      $xmlPerson = '<personer>
      <person personId="'.$id.'">
      <navn>'.$fulltNavn.'</navn>';
      if ($dag == "lørdag") { //er personen registrert på lørdag?
        $vekselLørdag = $veksel; //legg veksel i felt for lørdag
        $vekselSøndag = 0;
      } else { //registrert på en søndga
        $vekselLørdag = 0;
        $vekselSøndag = $veksel; //legg veksel i søndag              
      } //if ($dag == "lørdag")
      $xmlPerson .= '<vekselLordag>'.$vekselLørdag.'</vekselLordag>
      <levertLordag>'.$levert.'</levertLordag>
      <vekselSondag>'.$vekselSøndag.'</vekselSondag>
      <levertSondag>'.$levert.'</levertSondag>
      <avdeling avdId="'.$avdId.'" />
      </person>
      </personer>';                          
      $xml = simplexml_load_string($xmlPerson); //konverter string til xml      
      foreach($xml->children() as $child) { //henter ut xml og legger i assosiativ array
        //oppretter tabeller for alle elementenes verdier
        $id = $child['personId']; //henter ut personId fra xml-stringen
        $navn = $child->navn; 
        $vLordag = $child->vekselLordag; 
        $levLordag = $child->levertLordag; 
        $vSondag = $child->vekselSondag; 
        $levSondag = $child->levertSondag; 
        $avdeling =  $child->avdeling['avdId'];
        //legger inn den nye personen bakerst i array
        $personArray[0][$pId] = $id;  
        $personArray[1][$pId] = $navn;
        $personArray[2][$pId] = $vLordag;  
        $personArray[3][$pId] = $levLordag; 
        $personArray[4][$pId] = $vSondag;  
        $personArray[5][$pId] = $levSondag;  
        $personArray[6][$pId] = $avdeling;                  
      } //foreach              
      //print_r($personArray);      
      skrivAlle($avdArray, $personArray, $salgArray, $initArray); //skriver endring til fil
    } //if (empty($fnavn))                            
    return true;
  } //lagrePerson
    
  function registrerPersonSkjema() {
    $selectAvd = selectAvd();
    if (empty($selectAvd)) { //er select fylt opp?
      //ikke fylt = ingen avdelinger: ingen registrering
      echo "<tr><td>Ingen avdelinger er registrert. Det må finnes minst en avdeling før personer kan registreres.</td></tr>";    
    } else { //finnes minst en avdeling, vis skjema for registrering av person(er)
      echo '<fieldset>
      <legend>Registrer ny person</legend>
      <!--luft-->
      <br />
      Krav til registrering er at fornavn må minst være to bokstaver og etternavn minst tre.<br />
      Vekslepenger utlevert må skrives inn, og både avdeling og dag må velges.
      <!--luft-->
      <br /><br />      
      <form method="post" action="" onsubmit="return sjekkPerson();">
      <table>    
      <tr><td style="width:250px;">Fornavn: <input type="text" name="fNavn" id="fNavn" /></td>    
      <td style="width:250px;">Etternavn: <input type="text" name="eNavn" id="eNavn" /></td>
      <td style="width:275px;">Vekslepenger: <input type="text" name="veksel" id="veksel" /></td>    
      <td style="width:220px;">Avdeling: ';
      echo $selectAvd;
      echo '</td>    
      <td style="width:200px;">Velg dag: ';
      echo selectDag(); 
      echo '</td>
      <td><input type="submit" value="Legg til ny person" name="LAGRE_PERSON" /></td>    
      </tr>
      </table>
      </form>
      </fieldset>';
    } //if (empty($select)) 
  } //registrerPersonSkjema
  
  function selectAvd() {
    $avdArray = fyllAvdelingArray(); //henter array
    if (empty($avdArray)) { //finnes det avdelinger?
      return null; //ingen avdelinger, returner null
    } //if (empty($avdArray))
    $select = '<select name="avdeling" id="avdeling">
    <option value="velg_avd">--Velg avdeling--</option>'; //oppretter start på select    
    for ($i = 0; $i < count($avdArray[0]); $i++) {
      $avdId = $avdArray[0][$i]; //hent ut id
      $avdNavn = $avdArray[1][$i]; //hent ut navn     
      $select .= '<option value="'.$avdId.'">'.$avdNavn.'</option>';
    } //for    
    $select .= '</select>';
    return $select;
  } //selectAvd
  
  function selectDag() { 
    $select = '<select name="dag" id="dag">
    <option value="velg_dag">--Velg dag--</option>';
    $select .= '<option value="lørdag">Lørdag</option>';
    $select .= '<option value="velg">Søndag</option>';
    $select .= "</select>";
    return $select;
  } //selectDag
?>