<?php
  /*****************************************************************************
  * Denne filen inneholder kun feilmeldinger for de forskjellige sidene.       *
  * Fellestrekk for alle er at dersom alt er ok, dvs. felt er                  *
  * utfylt/valgt/etc så returneres null, siden disse legges inn ved lasting    *
  * av skjema                                                                  *
  * Laget av Knut Lucas Andersen                                               *  
  *****************************************************************************/
  
  /*************************FEILMELDING FOR PERSON******************************/
  function feilFnavn() { //sjekk at fornavn er korrekt
    $fnavn = $_POST['fNavn'];      
    if (empty($fnavn)) { //er fornavn innskrevet?
      return "<font color='red'>Fornavn må skrives inn.</font>";
    } else if (strlen($fnavn) < 2) {
      return "<font color='red'>Fornavn må minst være to bokstaver.</font>";
    } //if(empty($fnavn))
    return null; //fornavn er ok
  } //feilFnavn
  
  function feilEnavn() { //sjekk at etternavn er korrekt
    $enavn = $_POST['eNavn'];       
    if (empty($enavn)) { //er etternavn innskrevet?
      return "<font color='red'>Etternavn må skrives inn.</font>";
    } else if (strlen($enavn) < 3) {
      return "<font color='red'>Etternavn må minst være tre bokstaver.</font>";      
    } //if (empty($enavn))
    return null; //etternavn er ok
  } //feilEnavn
  
  function feilVeksel() {
    $veksel = $_POST['veksel'];
    if ($veksel == "") {
       return "<font color='red'>Vekslepenger må skrives inn.</font>";
    } else if(!(is_numeric($veksel))) {
    	return "<font color='red'>Vekslepenger må bestå av tall.</font>";
    } //if ($veksel == "") 
    return null; //veksel er ok
  } //feilVeksel  
    
  function feilSelectAvdeling() { //sjekk at avdeling er korrekt
    $avdeling = $_POST['avdeling'];
    if ($avdeling == "velg") { //er avdeling valgt?
      return "<font color='red'>Du må velge en avdeling.</font>"; 
    } //if ($avdeling == "Intet valgt")  
    return null; //avdeling er valgt    
  } //feilSelectAvdeling  
  
  function feilSelectDag() {
    $dag = $_POST['dag'];
    if ($dag == "velg") { //er avdeling valgt?
      return "<font color='red'>Du må velge en dag.</font>"; 
    } //if ($avdeling == "Intet valgt")  
    return null; //avdeling er valgt          
  } //feilDag
  
  function feilmeldingPerson() {
    $feilFnavn = feilFnavn();
    $feilEnavn = feilEnavn();
    $feilVeksel = feilVeksel();
    $feilAvd = feilSelectAvdeling();  
    $feilDag = feilSelectDag();       
    
    if (!(empty($feilFnavn))) { //er fornavn ok?
      return $feilFnavn;
    } else if (!(empty($feilEnavn))) { //er etternavn ok?
      return $feilEnavn;
    } else if(!(empty($feilVeksel))) { //er veksel ok?
      return $feilVeksel;
    } else if(!(empty($feilAvd))) { //er avdeling ok?
      return $feilAvd;      
    } else if(!(empty($feilDag))) { //er dag ok?     
      return $feilDag;	
    } //if (!(empty($feilFnavn)))
    return null;
  } //feilmeldingPerson
  
  /**********************FEILMELDING FOR AVDELING*******************************/
  function feilAvdelingnavn() { //sjekk at avdelingsnavn er korrekt
    $avdNavn = $_POST['avdNavn'];      
    if (empty($avdNavn)) { //er fornavn innskrevet?
      return "<font color='red'>Navn på avdeling må skrives inn.</font>";
    } else if (strlen($avdNavn) < 5) {
      return "<font color='red'>Navn på avdeling må minst være fem bokstaver.</font>";
    } //if(empty($fnavn))
    return null; //avdelingsnavn er ok
  } //feilAvdelingnavn
  
  function feilVekselLeder() {
    $vLeder = $_POST['vekselLeder'];
    if ($vLeder == "") {
      return "<font color='red'>Vekselleder må skrives inn.</font>";
    } else if(!(is_numeric($vLeder))) {
    	return "<font color='red'>Vekselleder må bestå av tall.</font>";
    } //if ($veksel == "") 
    return null; //veksel er ok  
  } //feilVekselLeder
  
  /*************************FEILMELDING FOR INIT********************************/
  function feilVekselTotal() {
    $veksel = $_POST['vTotalt'];
    if ($veksel == "") {
       return "<font color='red'>Totalveksel fra bank må skrives inn.</font>";
    } else if(!(is_numeric($veksel))) {
    	return "<font color='red'>Totalveksel må bestå av tall.</font>";
    } //if ($veksel == "") 
    return null; //veksel er ok     
  } //feilVekselTotal 
?>