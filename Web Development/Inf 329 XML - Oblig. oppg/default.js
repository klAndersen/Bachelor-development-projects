/******************************************************************************* 
*  Dette dokumentet inneholder javascript som hovedsaklig brukes til å         *
*  gi bruker tilbakemelding/feilmelding og sette fokus i tekstfelt             *
*******************************************************************************/  

function focusAvdeling() { //setter focus i tekstfelt for avdelingsnavn
  document.getElementById("avdNavn").focus();
} //focusAvd

function focusFornavn() { //setter fokus i tekstfelt for fornavn
  document.getElementById("fNavn").focus(); //setter fokus i felt for fornavn
} //focusFornavn

function focusEtternavn() { //setter focus i tekstfelt for etternavn
  document.getElementById("eNavn").focus();
} //focusEtternavn

function focusVekselleder() { //setter fokus i tekstfelt for vekselleder
  document.getElementById("vekselLeder").focus(); //setter fokus i felt for fornavn
} //focusVekselleder

function focusVekslepenger() { //setter focus i tekstfelt for vekslepenger
  document.getElementById("veksel").focus();
} //focusVekslepenger

function focusVekseltotal() { //setter focus i tekstfelt for vekslepenger
  document.getElementById("vTotalt").focus();
} //focusVekslepenger

function sjekkAvd() { //sjekker at felt er gyldig ved registrering av ny avdeling
  var avdNavn = document.getElementById("avdNavn").value;
  var vekselLeder = document.getElementById("vekselLeder").value;
  var feilmelding = "";
  var avbryt = "\nTrykk Avbryt for å korrigere.";   
  
  if (avdNavn == "") {
    feilmelding = "Avdelingsnavn må skrives inn.";
    focusAvdeling();
  } else if (avdNavn.length < 5) {
    feilmelding = "Avdelingsnavn må minst være 5 tegn.";
    focusAvdeling();
  } else if (vekselLeder == "") { 
    feilmelding = "Vekselleder må skrives inn.";
    focusVekselleder();
  } else if (vekselLeder < 0) {
    feilmelding = "Vekselleder må være et positivt siffer."; 
    focusVekselleder();
  } //if (avdNavn == "")
  
  if (feilmelding != "") {
    return confirm(feilmelding + avbryt);
  } //if (feilmelding != "")  
} //sjekkAvd

function sjekkPerson() { //sjekker at felt er gyldige ved registrering av person
  var fNavn = document.getElementById("fNavn").value;
  var eNavn = document.getElementById("eNavn").value;
  var veksel = document.getElementById("veksel").value;
  var avd = document.getElementById("avdeling").value; 
  var dag = document.getElementById("dag").value; 
  var feilmelding = "";
  var avbryt = "\nTrykk Avbryt for å korrigere.";
  
  if (fNavn == "") {
    feilmelding = "Fornavn må skrives inn.";
    focusFornavn();
  } else if (fNavn.length < 2) {
    feilmelding = "Fornavn må minst være 2 bokstaver.";
    focusFornavn();
  } else if (eNavn == "") {
    feilmelding = "Etternavn må skrives inn.";
    focusEtternavn();
  } else if (eNavn.length < 3) {
    feilmelding = "Etternavn må minst være 3 bokstaver.";
    focusEtternavn();
  } else if (veksel == "") { 
    feilmelding = "Veksel må skrives inn.";
    focusVekslepenger();
  } else if (avd == "velg_avd") {
    feilmelding = "Person må tilknyttes en avdeling.";
  } else if (dag == "velg_dag") {
    feilmelding = "Dagen som personen deltar må settes.";
  } //if (fNavn == "")
  
  if (feilmelding != "") {
    return confirm(feilmelding + avbryt);
  } //if (feilmelding != "") 
} //sjekkPerson

function sjekkInit() {
  var stdVeksel = document.getElementById("veksel").value;
  var totVeksel = document.getElementById("vTotalt").value;
  var feilmelding = "";
  var avbryt = "\nTrykk Avbryt for å korrigere.";   
  
  if (stdVeksel == "") {
    feilmelding = "Vekslepenger for selger må skrives inn.";
    focusVekslepenger();
  } else if (totVeksel == "") { 
    feilmelding = "Total veksel må skrives inn.";
    focusVekseltotal();    
  } //if (avdNavn == "")
  
  if (feilmelding != "") {
    return confirm(feilmelding + avbryt);
  } //if (feilmelding != "") 
} //sjekkInit