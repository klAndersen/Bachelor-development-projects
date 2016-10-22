  //Alle funksjonene som er i bruk for å sjekke om alt er ok, om felter er utfylt o.l
  
  /*******************************INNLOGGING**********************************/
  function innlogging() { //sjekking av innlogging    
    var email = document.getElementById("mail").value;
    var mail_resp = checkEmail(email);  
    var pwd = document.getElementById("pwd").value;  
    var feilmelding = ""; 
    var avbryt = "\nTrykk Avbryt for å korrigere.";                                              
      
    if (email == "") { //er e-post skrevet inn?
      feilmelding = "E-post må skrives inn"; 
      focusLoggInn(); //setter focus i logg inn feltet (e-post)
    } else if (pwd == "") { //sjekker om passord er blankt/tomt
      feilmelding = "Passordfeltet er tomt, skriv inn passord.";
      document.getElementById("pwd").focus(); //setter focus i passordfeltet
    } //if (pwd == "")    
    if(mail_resp != "") { //er innskrevet e-post gyldig?
      return confirm(mail_resp + avbryt);
    } else if(feilmelding != "") { //er e-post/passord skrevet inn?
      return confirm(feilmelding + avbryt);
    } //if(mail_resp != "")
  } //innlogging  
  
  /*****************************ENDRING AV PASSORD*****************************/
  function sjekkPwd() { //sjekking av passord ved endring  
    var feilmelding = "";       
    var gamPwd =  document.getElementById("gPwd").value;
    var nyttPwd = document.getElementById("nPwd").value;
    var bekreftPwd = document.getElementById("bnPwd").value;  
    var avbryt = "\nTrykk Avbryt for å korrigere."; 
    
    if (gamPwd == "" && nyttPwd == "" && bekreftPwd == "") { //alle feltene er tomme
      feilmelding = "Passordfeltene er tomme."; 
      focusPwd(); //setter focus i gammelt passord boksen   
    } else if (gamPwd == "") { //er gammelt passord fylt ut?
      feilmelding = "Feltet for gammelt passord er tomt."; 
      focusPwd(); //setter focus i gammelt passord boksen     
    } else if (nyttPwd == "") { //er nytt passord skrivd inn?
      feilmelding = "Feltet for nytt passord er tomt.";
      focusNyttPwd(); //setter fokus i feltet
    } else if (nyttPwd.length < 6) { //har nytt passord korrekt lengde?
      feilmelding = "Nytt passord er for kort,\n";
      feilmelding += "nytt passord må minst være 6 tegn.\n";
      feilmelding += "Felte(ne) blir nå blanket ut.";
      blankUtPwd(); //blanker ut nytt og gjenta passord
      focusNyttPwd(); //setter fokus i feltet
    } else if (nyttPwd.length > 20) { //er nytt passord for langt?
      feilmelding = "Nytt passord er for langt,\n";
      feilmelding += "nytt passord kan ikke overstige 20 tegn.\n";
      feilmelding += "Felte(ne) blir nå blanket ut.";
      blankUtPwd(); //blanker ut nytt og gjenta passord
      focusNyttPwd(); //setter fokus i feltet             
    } else if (bekreftPwd == "") { //er gjentatt passord skrevet inn?
      feilmelding = "Feltet for  å gjenta nytt passord er tomt.";
      document.getElementById("bnPwd").focus(); //setter fokus i feltet
    } else if (nyttPwd != bekreftPwd) { //er nytt og gjentatt passord like?
      feilmelding = "Nytt passord og bekreft nytt passord er ikke like.\n";
      feilmelding += "Felte(ne) blir nå blanket ut."; 
      blankUtPwd(); //blanker ut nytt og gjenta passord
      focusNyttPwd(); //setter fokus i feltet
    } //if (gamPwd == "")
                
    if(feilmelding != "") { //ligger det noe i feilmelding?                  
      return confirm(feilmelding + avbryt);
    } //if(feilmelding != "")
  } //sjekkPwd
  
  /************************************E-POST************************************/
  /* Funnet på http://www.webmasterworld.com/javascript/4130654.htm
  skrevet av TheMadScientist */   
  function checkEmail(email) { //sjekker om e-post er gyldig 
    var feilmelding = "";
    var emailFilter=/^[a-zA-Z0-9_.-]+@[a-z0-9][a-z0-9\-]{1,64}(\.[a-z]{2,4}|[a-z]{2,3}\.[a-z]{2})$/i; 
    var validEmail = emailFilter.test(email);
       
    if (email != "") { //er e-post skrevet inn?              
      if (validEmail) { //er e-post innskrevet gyldig?
        feilmelding = ""; 
      } else { //e-post er ikke gyldig
        feilmelding = "E-post oppgitt er ikke gyldig,\n"
        feilmelding += "vennligst sjekk innskrevet e-post."         
      } //if (validEmail)
    } //if (email != "")
    return feilmelding;
  } //checkEmail   
  
  /***************************NYE BRUKERE & REDIGERING*****************************/
  function sjekkRegistrering() { //sjekker feltene for registrering som ny bruker
    var email = document.getElementById("email").value;     
    var campus = document.getElementById("idCampus").value; 
    var fag = document.getElementById("idFag").value;  
    var feilmelding = sjekkNavn(); 
    var mail_resp = checkEmail(email);    
    var avbryt = "\nTrykk Avbryt for å korrigere."; 
    
    if (feilmelding == ""){ //ligger det noe i feilmelding?
      if (email == "") { //er e-post skrevet inn?
        feilmelding = "E-post må skrives inn"; 
        focusMail(); //setter fokus
      } else if (mail_resp != "") { //er oppgitt e-post gyldig?
        focusMail(); //setter focus i tekstfeltet  
      } else if (campus == "Intet valgt" ) { //har ikke valgt campus
        feilmelding = "Du må velge et campus.";
        focusCampus(); //setter fokus
      } else if (fag == "Intet valgt") { //har ikke valgt fagområde
        feilmelding = "Du må velge et fagområde.";
        focusFagomrade(); //setter fokus
      } //if (email == "")
    } //if (feilmelding == "")                            
    
    if (mail_resp != "" && feilmelding != "") { //feil/mangler i personlig info og e-post
      return confirm(mail_resp + "\n" + feilmelding + avbryt);     
    } else if(feilmelding != "") { //feil/mangler i personlig info
      return confirm(feilmelding + avbryt);
    } else if(mail_resp != "") { //ugyldig e-post         
      return confirm(mail_resp + avbryt);
    } //if (mail_resp != null && feilmelding != "")                
  } //sjekkRegistrering            
  
  /******************************SENDING AV MELDING****************************/
  function sjekkSending() { //sending til en person (sjekk av felter)
    var melding = document.getElementById("melding").value;    
    var feilmelding = sjekkNavn(); //er navn fylt ut?
    var avbryt = "\nTrykk Avbryt for å korrigere.";    
      
    if (feilmelding == "") { //ligger det noe i feilmelding?      
      if (melding == "" ) { //er meldingsfeltet tomt?
        feilmelding = "Du må skrive inn noe i meldingsfeltet"; 
        focusMelding(); //sett focus i tekstfeltet    
      } //if (melding == "" )
    } //if (feilmelding == "")    
    if(feilmelding != "") { //ligger det noe i feilmelding?
      return confirm(feilmelding + avbryt);
    } //if(feilmelding != "")
  } //sjekkSending
  
  function sjekkSendingMange() { //masseutsendelse (sjekk av drop-down og melding)
    var fag = document.getElementById("idFag").value;
    var melding = document.getElementById("melding").value;    
    var feilmelding = ""; 
    var avbryt = "\nTrykk Avbryt for å korrigere.";
    
    if (fag == "Intet valgt") { //er mottakergruppe valgt?
      feilmelding = "Du må velge en mottakergruppe.";
      focusFagomrade(); //setter fokus
    } else if (melding == "") { //er meldingsfeltet tomt?
      feilmelding = "Du må skrive inn noe i meldingsfeltet";
      focusMelding(); //setter fokus
    } //if (fag == "Intet valgt")                
    if(feilmelding != "") { //ligger det noe i feilmelding?
       return confirm(feilmelding + avbryt);
    } //if(feilmelding != "")     
  } //sjekkSendingMange
  
  /***************************SØKING ETTER BRUKERE*****************************/
  function sokPaaNavn() { //sjekker om brukeren har fylt ut minst et felt for søking
    var fnavn = document.getElementById("finnFnavn").value; 
    var enavn = document.getElementById("finnEnavn").value;        
    var feilmelding = ""; 
    var avbryt = "\nTrykk Avbryt for å korrigere.";    
      
    if (fnavn == "" && enavn == "") {
      feilmelding = "Du må skrive inn noe i enten fornavn\n"
      feilmelding += "eller etternavn for å søke.";
      focusFinn(); //sett focus i tekstfeltet for fornavn
    } //if (fnavn == "" && enavn == "")
    
    if (feilmelding != "") { //ligger det noe i feilmelding?
      return confirm(feilmelding + avbryt);
    } //if (feilmelding != "")
  } //sokPaaNavn
  
  function sjekkMail() { //sjekker om e-post er skrevet inn ved søk på denne
    var email = document.getElementById("email").value;
    var feilmelding = "";
    
    if (email == "") { //er e-post fylt ut?
      feilmelding = "Du må skrive inn e-post.";
      focusMail(); //setter focus i tekstfeltet
    } else { //e-post er utfylt, er den gyldig?  
      feilmelding = checkEmail(email); //sjekker om e-post er gyldig
      focusMail(); //setter focus i tekstfeltet
    } //if (email == "")     
    if (feilmelding != "") { //ligger det noe i feilmelding?   
      return confirm(feilmelding);  
    } //if (feilmelding != "")
  } //sjekkMail
  
  /****************************OPPRETTING AV NY BEGIVENHET*********************/
  function sjekkNyBegivenhet() {
    var tittel = document.getElementById("tittel").value;
    var fag = document.getElementById("idFag").value;
    var kortInfo = document.getElementById("kortInfo").value;
    var beskrivelse = document.getElementById("beskrivelse").value;  
    var feilmelding = ""; 
    var avbryt = "\nTrykk Avbryt for å korrigere."; 
            
    if (tittel.length < 3) { //tittel må minst være 3 tegn (kan være forkortelse)
      feilmelding = "Tittel må skrives inn, minst tre tegn."; 
      focusTittel(); //setter fokus i tekstfeltet 
    } else if (fag == "Intet valgt") { //fagområde det er knyttet til er ikke valgt
      feilmelding = "Du må velge et fagområde begivenheten gjelder.";      
      focusFagomrade(); //setter fokus
    } else if (kortInfo == "") { //kort beskrivelse er ikke skrevet
      feilmelding = "Du må skrive kort om begivenheten.";                  
      focusKortInfo();
    } else if (kortInfo.length > 140) { //er feltet for langt?
      var lengde = kortInfo.length - 140; //finner overskuddet
      feilmelding = "Kort om begivenheten kan ikke overstige 140 tegn."; 
      feilmelding += "Din tekst er " + lengde + " tegn for lang.";
    } else if (beskrivelse == "") { //beskrivelse av begivenheten er ikke skrevet    
      feilmelding = "Du må skrive inn hva begivenheten gjelder/hva som skjer.";
      focusBeskrivelse(); //setter focus i tekstfeltet
    } //if (tittel.length < 3)
  
    if(feilmelding != "") { //ligger det noe i feilmelding?
       return confirm(feilmelding + avbryt);
    } //if(feilmelding != "")        
  } //sjekkNyBegivenhet 
  
  /*******************ADMINISTRERING AV CAMPUS OG FAGOMRÅDE********************/
  function endreCampus() {    
    var campus = document.getElementById("nytt_campus").value;
    var feilmelding = ""; 
    var avbryt = "\nTrykk Avbryt for å korrigere."; 
    
    if (campus == "") { //er feltet er tomt
      feilmelding = "Du må skrive inn nytt navn på campus.";
      focusNyttCampus(); //setter focus
    } //if (campus == "")      
    if(feilmelding != "") { //ligger det noe i feilmelding?
      return confirm(feilmelding + avbryt);
    } //if(feilmelding != "") 
  } //endreCampus 
  
  function endreFagomraade() {
    var fag = document.getElementById("nytt_fagomraade").value;
    var feilmelding = ""; 
    var avbryt = "\nTrykk Avbryt for å korrigere.";
    
    if (fag == "") { //er feltet er tomt
      feilmelding = "Du må skrive inn nytt navn på fagområdet.";
      focusNyttFagomraade(); //setter focus
    } //if (fag == "") 
    if(feilmelding != "") { //ligger det noe i feilmelding?
      return confirm(feilmelding + avbryt);
    } //if(feilmelding != "")     
  } //endreFagomraade
  
  /**********************************KARANTENE***********************************/
  function sjekkKarantene() {
    var info = document.getElementById("info").value;
    var feilmelding = sjekkNavn();
    var avbryt = "\nTrykk Avbryt for å korrigere."; 
           
    if (feilmelding == "") { //ligger det noe i feilmelding?    
      if (info == "") { //intet innskrevet i feltet for karantene
        feilmelding = "Feltet for informasjon knyttet til karantenen er tom.";
        focusKaranteneInfo(); //setter fokus i tekstfeltet
      } //if (info == "")    
    } //if (feilmelding == "")  
    
    if(feilmelding != "") { //ligger det noe i feilmelding?
      return confirm(feilmelding + avbryt);
    } //if(feilmelding != "")          
  } //sjekkKarantene
  
  /**********************************NETTVERK***********************************/
  function nyttNettverk() { //oppretting/endring av navn på nettverk
    var nettNavn = document.getElementById("nytt_nettverk").value;
    var feilmelding = ""; 
    var avbryt = "\nTrykk Avbryt for å korrigere."; 

    if (nettNavn == "") { //er feltet er tomt
        feilmelding = "Du må skrive inn nytt navn på nettverket.";
        focusNyttNettverk(); //setter focus
      } //if (nettNavn == "")   
    if(feilmelding != "") { //ligger det noe i feilmelding?
      return confirm(feilmelding + avbryt);
    } //if(feilmelding != "") 
  } //nyttNettverk