<?php
  /*****************************************************************************/
  /* Denne filen inneholder kun feilmeldinger for de forskjellige sidene.      */
  /* Fellestrekk for alle er at dersom alt er ok, dvs. felt er utfylt, valgt,  */
  /* etc, så returneres null, siden noen av disse legges inn ved oppstart og   */
  /* brukes for å sette focus på diverse sider hvor det er flere felter å      */
  /* fylle ut                                                                  */
  /*****************************************************************************/
  
  /*************************FEILMELDING FOR NAVN/E-POST*************************/
  function feilFnavn() { //sjekk at fornavn er korrekt
    $fnavn = $_POST['fnavn'];      
    if (empty($fnavn)) { //er fornavn innskrevet?
      return "<font color='red'>Fornavn må skrives inn.</font>";
    } else if (strlen($fnavn) < 2) {
      return "<font color='red'>Fornavn må minst være to bokstaver.</font>";
    } //if(empty($fnavn))
    return null; //fornavn er ok
  } //feilFnavn
  
  function feilEnavn() { //sjekk at etternavn er korrekt
    $enavn = $_POST['enavn'];       
    if (empty($enavn)) { //er etternavn innskrevet?
      return "<font color='red'>Etternavn må skrives inn.</font>";
    } else if (strlen($enavn) < 3) {
      return "<font color='red'>Etternavn må minst være tre bokstaver.</font>";      
    } //if (empty($enavn))
    return null; //etternavn er ok
  } //feilEnavn
  
  function feilEMail($endring, $epost, $ny_Epost) { //sjekk at e-post er korrekt         
    //for disse to linjene, se Kildereferanse: Sjekking av e-post i php (1) & (2)
    $regex = "/^[_\.0-9a-zA-Z-]+@([0-9a-zA-Z][0-9a-zA-Z-]+\.)+[a-zA-Z]{2,6}$/i";     
    $gyldig = preg_match($regex, $ny_Epost); //sjekk innhold
    //sjekking av e-postens innhold
    if (empty($ny_Epost)) { //er e-post innskrevet?
      return "<font color='red'>E-post må skrives inn.</font>";   
    }  else if (!$gyldig) { //er e-post gyldig?
      return "<font color='red'>E-posten du skrev inn er ugyldig.</font>";      
    } //if(empty($epost))    
    if ($endring) { //er det redigering/endring av e-post
      $sjekk = sjekkEpost($epost, $ny_Epost); //sjekker om den finnes
      if ($sjekk) { //finnes e-post fra før? 
        return "<font color='red'>E-posten du forsøkte å endre til finnes fra før.</font>";
      } //if (sjekkEpost($epost, $ny_Epost))
    } //if ($endring)
    return null; //e-post er ok 
  } //feilEMail 
  
  /***************************FEILMELDING FOR PASSORD***************************/
  function feilGamPwd() { //sjekk om gammelt passord er korrekt
    $gamPwd = $_POST['gpwd']; 
    if (empty($gamPwd)) { //er feltet for gammelt passord blankt?
    	return "<font color='red'>Du må skrive inn ditt nåværende passord.</font>";
    } else if (!empty($feil)) {
    return "<font color='red'>Ditt gamle passord er ikke korrekt. Vennligst prøv igjen.</font>";
    } //if (empty($gamPwd))
    $sjekkNytt = feilNyttPwd(); //hent resultat fra sjekk av nytt passord
    $sjekkBek = feilBekPwd(); //hent resultat fra sjekk av gjenta passord
    if (!empty($sjekkNytt) && !empty($sjekkBek)) { //er nytt og gjenta ok?
      return "<font color='red'>Husk å skrive inn nåværende passord.</font>";
    } else if (!endrePwd()) { //både nytt og gjenta er like, men var gammelt passord korrekt?             	          
      return "<font color='red'>Gammelt passord er ikke korrekt.</font>";
    } //if (!empty($sjekkNytt) && !empty($sjekkBek))
    return null; //alt ok
  } //feilGamPwd
  
  function feilNyttPwd() { //sjekk om nytt passord er korrekt     
    $nyttPwd = $_POST['nPwd'];
    $bekreftPwd = $_POST['bnPwd']; 
    if (empty($nyttPwd)) { //er felt nytt passord blankt?
    	return "<font color='red'>Du må skrive inn nytt passord.</font>";
    } else if (strlen($nyttPwd) < 6) { //er nytt passord mindre enn 6 tegn?
      return "<font color='red'>Nytt passord er for kort, nytt passord må minst være 6 tegn.</font>";
    } else if (strlen($nyttPwd) > 20) { //er nytt passord over 20 tegn?
      return "<font color='red'>Nytt passord er for langt, nytt passord kan ikke overstige 20 tegn.</font>";
    } else if ($nyttPwd != $bekreftPwd) { //er nytt og gjentatt passord like?
      return "<font color='red'>Nytt og bekreft passord er ikke like.</font>";
    } //if (empty($nyttPwd)) 
    return null; //alt ok       
  } //feilNyttPwd
  
  function feilBekPwd() { //sjekk om gjentakelse av nytt passord er korrekt
    $nyttPwd = $_POST['nPwd'];
    $bekreftPwd = $_POST['bnPwd']; 
    if (empty($bekreftPwd)) { //er felt bekreft passord blankt?
    	return "<font color='red'>Du må gjenta nytt passord.</font>";
    } else if ($nyttPwd != $bekreftPwd) { //er nytt og gjentatt passord like?	
      return "<font color='red'>Nytt og bekreft passord er ikke like.</font>";
    } //if (empty($bekreftPwd))    
    return null; //alt ok
  } //feilBekPwd
  
  /**************************FEILMELDING FOR MELDINGER**************************/
  function tomMelding() { //sjekker at melding har innhold
    $melding = $_POST['melding'];       
    if (empty($melding)) { //er melding tom?
      return "<font color='red'>Meldingsfeltet kan ikke være tomt.</font>";
    } //if (empty($melding))
    return null; //melding er ok  
  } //tomMelding
  
  function feilMottakergruppe() { //sjekker at mottakergruppe er satt    
    $fag = $_POST['idFag'];
    if ($fag == "Intet valgt") {
  	 return "<font color='red'>Du må velge en mottakergruppe.</font>"; 
    } //if ($fag == "Intet valgt")
    return null; //alt ok  
  } //feilMottakergruppe  
  
  /**********************FEILMELDING FOR CAMPUS/FAGOMRÅDE***********************/ 
  function feilCampus() { //sjekk at campus er korrekt
    $campus = $_POST['idCampus'];
    if ($campus == "Intet valgt") { //er campus valgt?
      return "<font color='red'>Du må velge et campus</font>"; 
    } //if ($campus == "Intet valgt")  
    return null; //campus er ok     
  } //feilCampus
  
  function feilFagomrade() { //sjekk at fagområde er korrekt
    $fag = $_POST['idFag'];
    if ($fag == "Intet valgt") { //er fagområde valgt? 
      return "<font color='red'>Du må velge et fagområde</font>"; 
    } //if ($fag == "Intet valgt")  
    return null; //fagområde er ok
  } //feilFagomrade 
       
  function feilNyttCampus() { //sjekk at nytt campus er korrekt
    $cNavn = $_POST['nytt_campus'];
    if (empty($cNavn)) { //er nytt campus navn skrevet inn?
    	return "<font color='red'>Du må skrive inn det nye navnet på campuset: </font>";
    } //if (empty($cNavn))
    return null; //alt ok  
  } //feilNyttCampus
  
  function feilNyttFagomraade() { //sjekk at nytt fagområde er korrekt
    $fNavn = $_POST['nytt_fagomraade'];
    if (empty($fNavn)) { //er nytt fagnavn skrivd inn?
      return "<font color='red'>Du må skrive inn det nye navnet på fagområdet: </font>";	
    } //if (empty($fNavn))
    return null; //alt ok
  } //feilNyttFagomraade   
  
  /***************************FEILMELDING FOR KARANTENE***************************/
  function feilKaranteneInfo() {
    $kInfo = $_POST['info'];
    if (empty($kInfo)) { //er info om karantene skrevet inn?
      return "<font color='red'>Du må skrive inn informasjon om hvorfor bruker utestenges.</font>";
    } //if (empty($kInfo)) 
    return null; //alt ok
  } //feilKaranteneInfo    
          
  /**************************FEILMELDING FOR BEGIVENHET**************************/
  function feilTittel() { //sjekk at tittel på begivenhet er korrekt
    $bNavn = $_POST['tittel'];       
    if (empty($bNavn)) { //er tittel skrevet inn?
      return "<font color='red'>Du må skrive inn tittel på begivenheten.</font>"; 
    } else if (strlen($bNavn) < 3) { //er tittel minst tre tegn
      return "<font color='red'>Tittel er for kort, tittel må minst være tre bokstaver.</font>";
    } //if (empty($bNavn))
    return null; //alt ok    
  } //feilTittel
  
  function feilKortInfo() { //sjekk at introduksjon er korrekt
    $bkortInfo = $_POST['kortInfo'];
    if (empty($bkortInfo)) {
      return "<font color='red'>Du må skrive en kort introduksjon.</font>";
    } //if (empty($bkortInfo))
    return null; //alt ok
  } //feilKortInfo
  
  function feilBeskrivelse() { //sjekk at beskrivelse er korrekt
    $bInfo = $_POST['beskrivelse'];
    if (empty($bInfo)) {
      return "<font color='red'>Du må skrive noe om hva som skjer på begivenheten.</font>";
    } //if (empty($bInfo))
    return null; //alt ok
  } //feilBeskrivelse
    
  /*****************************FEILMELDING FOR DATO***************************/
  function feilDato() { //sjekk at dato er korrekt 
    $fraAar = $_POST['fAar'];
    $fraMnd = $_POST['fMnd'];
    $fraDag = $_POST['fDag'];
    $tilAar = $_POST['tAar'];
    $tilMnd = $_POST['tMnd'];
    $tilDag = $_POST['tDag']; 
    
    if ($fraDag == $tilDag && $fraMnd == $tilMnd && $fraAar == $tilAar) { //er fra/til dato samme dag?
      return "<font color='red'>Fra og til dato kan ikke være like.</font>";
    } else if ($fraDag > $tilDag && $fraMnd == $tilMnd && $fraAar == $tilAar) { 
      //er startdato større enn sluttdato(hvis varighet er innenfor samme mnd og år)     
      return "<font color='red'>Fra dato kan ikke være etter til dato (når det er samme mnd og år).</font>";
    } else if ($fraMnd > $tilMnd && $fraAar == $tilAar) {
      return"<font color='red'>Fra måned kan ikke være etter til måned (innen for samme år).</font>";
    } else if ($fraAar > $tilAar) { //er fra-År større enn til-År?
      return "<font color='red'>Fra år kan ikke være etter til året.</font>";
    } //if ($fraDag == $tilDag)
    return null; //alt ok                
  } //feilDato  
  
  /*************************FEILMELDING FOR NETTVERK****************************/
  function feilNettverk() { //sjekk at valg av nettverk er korrekt   
    $nId = $_POST['nId'];
    if ($nId == "Intet valgt") { //er nettverk valgt?
    	return "<font color='red'>Du må velge et nettverk: </font>";
    } //if ($nId == "Intet valgt")
    return null; //alt ok 
  } //feilNettverk    
  
  function feilNyttNettverk() { //sjekk at nytt nettverk er korrekt
    $nyttNett = $_POST['nytt_nettverk'];
    if (empty($nyttNett)) { //er navn på det nye nettverket skrevet inn?
    	return "<font color='red'>Du må skrive inn navnet på nettverket: </font>";
    } //if (empty($nyttNett))
    return null; //alt ok  
  } //feilNyttNettverk  
  
  function feilEndreNettverk() { //sjekk at redigering er korrekt    
    $nyttNett = $_POST['nytt_nettverk'];
    if (empty($nyttNett)) { //er nytt nettverksnavn skrevet inn?
    	return "<font color='red'>Du må skrive inn navnet på det nye nettverket: </font>";
    } //if (empty($nyttNett))
    return null; //alt ok 
  } //feilEndreNettverk        
?>