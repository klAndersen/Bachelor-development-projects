/*Javascript*/

/**
 * Kontrollerer innloggingsdata, og hvis feil gir bruker 
 * mulighet til å korrigere via confirm dialog
 * @returns confirm()
 */
function kontrollerInnloggingsdata() {
	var feilmelding = "";
	var avbryt = "\nTrykk Avbryt for å korrigere.";  
	var brukernavn = window.document.getElementById("username");
	var passord = window.document.getElementById("pwd");
	//ble brukernavn/passord skrivd inn?
	if (brukernavn.value == "") {
		feilmelding = "Felt for brukernavn er tomt.\nVennligst skriv inn ditt brukernavn.";
		brukernavn.focus();		
	} else if (passord.value == "") {
		feilmelding = "Felt for passord er tomt. \nVennligst skriv inn ditt passord";
		passord.focus();
	} //if (brukernavn == "")
	//inneholdt noen av feltene feil?
	if(feilmelding != "") {
		return confirm(feilmelding + avbryt);
	} //if(feilmelding != "")
} //kontrollerInnloggingsdata

function velgAlleCheckboxer() {
	//hent ut checkboxene og huk de av
	var checkbox = window.document.getElementById("chkVelgAlle");
	var checkboxArray = window.document.getElementsByName("chkKobleFra");
	var antall = checkboxArray.length;
	for(var i = 0; i < antall; i++) {
		checkboxArray[i].checked = checkbox.checked;
	} //for
} //velgAlleCheckboxer