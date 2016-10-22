<?php
  /***************************************************************************** 
  *  Dette dokumentet inneholder kode som brukes på penger.php                 *
  *  Det skulle vært flere funksjoner som skulle vært lagt her, men de får     *
  *  heller opprettes og lages på et senere tidspunkt                          *   
  *  Laget av Knut Lucas Andersen                                              *  
  *****************************************************************************/
  function skjemaInnlevering() {
    $select = selectPerson();
    $skjema = '<table border="1">
    <th style="width: 200px;">Person</th><th>Avdeling</th><th>Vekslepenger</th>
    <tr><td>'.$select.'</td><td></td><td></td></tr>
    </table>';
    return $skjema;
  } //skjemaInnlevering
  
  function selectPerson() {    
    $personArray = fyllPersonArray(); //henter array
    if (empty($personArray)) { //finnes det avdelinger?
      return null; //ingen avdelinger, returner null
    } //if (empty($avdArray))
    $select = '<select name="person" id="person" style="width: 175px;">
    <option value="velg_avd">--Velg person--</option>'; //oppretter start på select    
    for ($i = 0; $i < count($personArray[0]); $i++) {
      $pId = $personArray[0][$i]; //hent ut id
      $persNavn = $personArray[1][$i]; //hent ut navn     
      $select .= '<option value="'.$pId.'">'.$persNavn.'</option>';
    } //for    
    $select .= '</select>';
    return $select;
  } //selectPerson
  
  function innlevertePenger() {
  
  } //innlevertePenger
?>