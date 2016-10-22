/***Dette js-scriptet inneholder kun focus og vinduer som åpnes i nye vinduer ***/
/***men også to små script for sjekking av innskrevet navn og utblanking av passord ***/

  /*********************************NYTT VINDU**********************************/
  function visPolicy() { //åpner policy i nytt vindu          
    return window.open("policy.html"); 
  } //visPolicy  
  
  function visHibu() { //åpner Hibu's hjemmeside i nytt vindu  
    return window.open("http://www.hibu.no/");
  } //visHibu
  
  function visPersonligSide(variabel) { //åpner en satt hjemmeside i nytt vindu (fra profil)  
    return window.open(variabel);
  } //visPersonligSide  
  
  /****************************FOCUS FOR INNLOGGING*****************************/
  function focusLoggInn() { //setter pekeren i tekstboksen for innlogging: e-post    
    document.getElementById("mail").focus();
  } //focusLoggInn  
  
  /*******************************FOCUS FOR NAVN********************************/
  function focusFnavn() { //setter peker i tekstboksen for fornavn
    document.getElementById("fnavn").focus();
  } //focusFnavn
  
  function focusEnavn() { //setter pekeren i tekstboksen for etternavn
    document.getElementById("enavn").focus();
  } //focusEnavn
  
  function focusFinn() { //setter peker i tekstboksen for fornavn ved søking
    document.getElementById("finnFnavn").focus();
  } //focusFnavn   
        
  /******************************FOCUS FOR CAMPUS*******************************/
  function focusCampus() { //setter pekeren i select for campus
    document.getElementById("idCampus").focus();
  } //focusCampus
  
  function focusNyttCampus() {
    document.getElementById("nytt_campus").focus();
  } //focusNyttCampus 
  
  /******************************FOCUS FOR FAGOMRÅDE****************************/ 
  function focusNyttFagomraade() {
    document.getElementById("nytt_fagomraade").focus();
  } //focusNyttFagomraade 
   
  function focusFagomrade() { //setter pekeren i select for fagområde
    document.getElementById("idFag").focus();
  } //focusFagomrade    
     
  /******************************FOCUS FOR MELDINGER****************************/
  function focusEmne() { //setter pekeren i tekstboksen for emne ved sending av melding
    document.getElementById("emne").focus();
  } //focusEmne
  
  function focusMelding() { //setter pekeren i tekstboksen for innhold ved sending av melding
    document.getElementById("melding").focus();  
  } //focusMelding   
          
  /*****************************FOCUS FOR BEGIVENHET****************************/
  function focusTittel() { //setter peker i tekstboksen for navn på begivenhet    
    document.getElementById("tittel").focus();
  } //focusTittel
  
  function focusKortInfo() {
    document.getElementById("kortInfo").focus();
  } //focusKortInfo
    
  function focusBeskrivelse() {
    document.getElementById("beskrivelse").focus();  
  } //focusBeskrivelse 
    
  /*****************************FOCUS FOR PASSORD*******************************/
  function focusPwd() { //setter peker i tekstboksen for gammelt passord
    document.getElementById("gPwd").focus();
  } // focusPwd
  
  function focusNyttPwd() { //setter peker i tekstboksen for nytt passord
    document.getElementById("nPwd").focus();  
  } //focusNyttPwd   
    
  /******************************FOCUS FOR NETTVERK****************************/ 
  function focusNyttNettverk() {
    document.getElementById("nytt_nettverk").focus();
  } //focusNyttNettverk
  
  function focusNettverk() {
    document.getElementById("nId").focus(); //setter fokus
  } //focusNettverk       
                                     
  /******************************RESTERENDE FOCUS******************************/  
  function focusMail() { //setter peker i tekstboksen for e-post    
    document.getElementById("email").focus();
  } //focusMail
  
  function focusKaranteneInfo() {
    document.getElementById("info").focus();
  } //focusKaranteneInfo

  /*******************SJEKKING AV NAVN******************************************/
  function sjekkNavn() { //funksjon som sjekker om for - og etternavn er skrevet inn
    var fornavn = document.getElementById("fnavn").value; 
    var etternavn = document.getElementById("enavn").value; 
    var feilmelding = "";    
    
    if (fornavn.length < 2) { //for kort fornavn
      feilmelding = "Fornavn må skrives inn, minst to bokstaver.";
      focusFnavn(); //setter fokus i tekstfeltet
    } else if (etternavn.length < 3) { //for kort etternavn
      feilmelding = "Etternavn må skrives inn, minst tre bokstaver.";
      focusEnavn(); //setter fokus i tekstfeltet
    } //if (fnavn.length < 2)  
    return feilmelding;
  } //sjekkNavn
  
  /******************************TØM PASSORDFELT*******************************/
  function blankUtPwd() { //funksjon får å blanke ut passordfeltene    
    document.getElementById("nPwd").value = "";
    document.getElementById("bnPwd").value = "";
  } //blankUtPwd