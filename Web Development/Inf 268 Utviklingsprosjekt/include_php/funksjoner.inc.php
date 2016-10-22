<?php
  session_start(); //starter sessions  
  /***********************************DEFINE***********************************/
  define ("TILBAKE","<a href='javascript:history.go(-1)'>Tilbake</a>");
  define ("TOMME_FELTER", "<a href='javascript:history.go(-2)'>Tilbake til registrering</a>");
  define ("NYBRUKER", "<a href='nybruker.php'>Registrering av nye brukere</a>"); 
  define ("FORSIDE", "<a href='default.php'>Til Alumni Hibus forside</a>"); 
  define ("GLEMT_PWD","<a href='glemtpwd.php'>Prøv igjen</a>");
  
  /********************************DATABASE************************************/
  function kobleTil() { //hentet fra s.268 i PHP-boka  
    $vert = "localhost"; //vert/server til db
    $bruker = "Lucas"; //brukernavn til db
    $pwd = "hibu"; //passord til db
    $dbNavn = "HibuAlumni"; //navn på db
    $tilkobling = mysql_connect($vert, $bruker, $pwd); //prøv å koble til
    if (!$tilkobling) { //ble tilkobling opprettet?
      die("Kunne ikke koble til: " . mysql_error());
    } //if(!$tilkobling)
    $valgtDb = mysql_select_db($dbNavn,$tilkobling); //opprett tilkobling til db
    if (!$valgtDb) { //ble databasen funnet?
      die("Kunne ikke bruke databasen: " . mysql_error());
    } //if(!$valgtDb)       
    return $tilkobling; //skjer bare hvis alt gikk bra
  } //kobleTil
  
  /***********************************PASSORD***********************************/ 
  function hashPwd($pwd, $mail) { //funksjon for hashing av passord  
    //grunnlaget er hentet fra: http://stackoverflow.com/questions/401656/secure-hash-and-salt-for-php-passwords
    //http://no.php.net/manual/en/function.hash-hmac.php
    $skilletegn = substr($mail, 0 , 3); //henter ut de tre første tegnene i brukerens e-psot
    $nokkel = 'HibuAlumni'; //oppretter en sikkerhetsnøkkel     
    //oppretter en hashing som er tregere og som man trenger nøkkel for å vise/lese
    $hash = hash_hmac('md5', $pwd . $skilletegn, $nokkel); 
    //kanskje sikrere å bruke SHA512, men da trengs det plass til ca 150 tegn eller mer i databasen
    //derfor valgte jeg å beholde md5 her 
    //echo $hash;
    return $hash; //returnerer resultatet
  } //hashPwd
  /*****************************************************************************/
  /* lagPwd() generer et tilfeldig passord basert på forskjellige tegn fra     */
  /* ASCII-tabellen (http://www.asciitable.com/). Grunnen til jeg har          */
  /* if-testen er pga tegnene oppgitt (60,62,92,96) tolkes (og omgjøres)       */
  /* enten av mysql_real_escape_string eller leses som html-tegn               */                                                                            
  /* 60 er < 62 er > 92 er \ og 96 er `                                        */
  /* Derfor øker jeg $rand med 1 i løkken for å unngå at den legger til tegnet */
  /*****************************************************************************/
  function lagPwd() {
    $pwd = ""; //initieres
    for ($i=0; $i<15; $i++) { //løkke for å lage et nytt, tilfeldig passord
      $rand = mt_rand(48, 122); //henter ut et pseudo-tilfeldig tall
      //echo "rand er: $rand <br />"; //utskrift av $random for å se hva tallet er
      if ($rand == 60 || $rand == 62 || $rand == 92 || $rand == 96) { 
        //echo "inni if og rand er $rand <br />"; //utskrift for å se at den gikk inni if
      	$rand += 1; //øk $rand med 1
      } //if ($rand == 92 || $rand == 96)
      $pwd .= chr($rand); //gjør om tallet til et tegn 
      //echo "pwd er : $pwd<br />"; //utskrift for å se hva passord ble
    } //for ($i=0; $i<15; $i++) 
    return $pwd; //returner det genererte passordet 
  } //lagPwd                                       
  
  /********************************AUTENTISERING********************************/
  function sjekk_autentisert() {    
    if (empty($_SESSION['email'])) { //er bruker logget inn?    
    //siden sidene er lagd i UTF-8, men denne lastes inn før meta-taggen, så
    //la jeg inn her et kort html-script som gjør at teksten lesbar (særnorske tegn)  
      die('<html>
          <head>
          <meta http-equiv=Content-Type content="text/html; charset=utf-8">
          </head>
          <body>
          Du må logge inn for å kunne vise denne siden. ' .FORSIDE .
          '</body>
          </html>');
    } //if(empty($_SESSION['email']))
  } //sjekk_autentisert
  
  /*********************OPPSPLITTING AV DATO OG KLOKKESLETT********************/  
  function splittDatoOgKlokkeslett($dato) {
    //henter måned og år
    $mndAar = "/". $dato[1] ."-". $dato[0];
    //splitter opp dag og klokkeslett
    $dagTid = explode(" ", $dato[2]);
    //oppretter ny tekst-streng med dato og klokkeslett
    $dato = $dagTid[0] . $mndAar ." ". $dagTid[1]; 
    return $dato;    
  } //splittDatoOgKlokkeslett
  
  function fjernKlokkeslett($dato) {
    //henter måned og år
    $mndAar = "/". $dato[1] ."-". $dato[0];
    //splitter opp dag og klokkeslett
    $dagTid = explode(" ", $dato[2]);
    //oppretter ny tekst-streng med dato uten klokkeslett
    $dato = $dagTid[0] . $mndAar; 
    return $dato;   
  } //fjernKlokkeslett
  
  /*************************SJEKKING AV HTML-ENTITIES *************************/
  function rensHtmlUTF8($input, $type) {
    //lagde denne for å få lagret øæ,ø,å i databasen - Tegnsett: UTF-8
    switch ($type) { //hvilken "Quote" konvertering skal brukes
      case 1: 
      $input = htmlentities($input, ENT_COMPAT, 'UTF-8'); //konverter kun gåseøyne
      break;
      case 2: 
      $input = htmlentities($input, ENT_QUOTES, 'UTF-8'); //konverterer begge
      break;
      case 3: 
      $input = htmlentities($input, ENT_NOQUOTES, 'UTF-8'); //konverterer ikke gåseøyne/fnutt
      break;    
    } //switch
    return $input;
  } //rensHtmlUTF8
  
  /***************************KONVERTER E-POST TIL UTF-8************************/
  function email_utf8($mottaker, $emne, $innhold, $fra) {
    //Kilde: http://php.net/manual/en/function.mail.php
    $header = 'MIME-Version: 1.0' . "\n" . 'Content-type: text/html; charset=UTF-8'; 
    $header .= "\n" . 'From: AlumniHibu <' . $fra . ">\n"; //sender e-post som tekst og HTML
    mail($mottaker, '=?UTF-8?B?'.base64_encode($emne).'?=', $innhold, $header);
  } //email_utf8 
  
  /***************************SJEKKING AV E-POST********************************/  
  function sjekkEpost($epost, $ny_Epost) {    
    if ($epost == $ny_Epost) { //er e-post endret?
      return false; //ikke endret 
    } else { //e-post er endret
      $mailFinnes = finnesMail($ny_Epost); //sjekk om ny e-post finnes
      if ($mailFinnes) { //finnes e-post?
        return true; //e-post finnes      
      } else { //e-posten finnes ikke
        return false; //e-post finnes ikke
      } //if ($mailFinnes)
    } //if ($epost == $ny_Epost)
  } //sjekkEpost
  
  function finnesMail($epost) {
    $tilkobling = kobleTil(); //opprett tilkobling til db  
    $sql = "Select * from tblstudent where sEpost='$epost'";
    $resultat = mysql_query($sql, $tilkobling);
    $antall = mysql_num_rows($resultat);
    if ($antall >= 1) { //finnes denne e-posten?  
      return true; //e-post finnes   
    } else { //e-posten finnes ikke
      return false; //e-post finnes ikke
    } //if ($antall >= 1)
  } //finnesMail   
  
  /*********************************MOD & ADMIN*********************************/
  function finnBrukertype() {
    $email = $_SESSION['email'];     
    $tilkobling = kobleTil(); //opprett tilkobling til db         
    $sql = "SELECT sbId FROM tblstudent ";
    $sql .= "WHERE sEpost = '$email';";
    $resultat = mysql_query($sql, $tilkobling);
    //echo $sql;      
    $rad = mysql_fetch_assoc($resultat); //forventer kun et resultat, derfor ingen løkke  
    $sbId = $rad['sbId'];       
    mysql_close($tilkobling); //lukker tilkobling til db  
    return $sbId; 
  } //finnBrukertype      
  
  function endreMail($sId) { //endring av e-post
    $tilkobling = kobleTil(); //opprett tilkobling til db  
    $sql = "SELECT * FROM tblStudent WHERE sId = $sId"; //finn valgt brukers e-post
    $resultat = mysql_query($sql, $tilkobling); //hent resultatet
    $rad = mysql_fetch_assoc($resultat); //legg resultatet i en assosiativ array
    $epost = $rad['sEpost']; //hent ut orginal e-post    
    $ny_Epost = mysql_real_escape_string($_POST['email']); //hent ny e-post    
    $feil = feilEMail(true, $epost, $ny_Epost); //hent ut mulige feilmeldinger
        
    if (!empty($feil)) { //finnes det noen feilmeldinger?            
      return $feil; //noe er feil, returner feilmelding
    } else { //alt ok, endre e-post      
      $sql = "UPDATE tblStudent ";
      $sql .= "SET sEpost='$ny_Epost' ";
      $sql .= "WHERE sId = $sId";
      //echo $sql;    
      mysql_query($sql,$tilkobling); 
    } //if ($antall >= 1)
    mysql_close($tilkobling); //lukker tilkobling til db     
  } //endreMail               
  
  /*********************TELLER FOR ANTALL ULESTE MELDINGER*********************/
  function hentUleste($email) {
    $tilkobling = kobleTil(); //kobler til db
    $msg_sql = "SELECT * FROM tblStudent, tblMeldinger ";
    $msg_sql .= "WHERE sEpost='$email' AND ";
    $msg_sql .= "msTilId=sId AND mLest=1;"; //henter uleste meldinger
    $resultat = mysql_query($msg_sql, $tilkobling);
    $ulest = mysql_num_rows($resultat); //antall uleste meldinger baseres på resultatet
    $_SESSION['nyMelding'] = $ulest; //legg antallet inn i $_SESSION
    mysql_close($tilkobling); //lukker tilkobling til db
  } //hentUleste
  
  function visAntallUleste() {
    //funksjon som skal si ifra at bruker har uleste meldinger
    $ulest = $_SESSION['nyMelding']; //hent ut verdien fra $_SESSION
    if ($ulest > 0) { //finnes det uleste meldinger?
      return "($ulest)"; //vis antall uleste meldinger    
    } //if ($ulest > 0)
  } //visAntallUleste
  
  /********************VISER BEGIVENHETER I HØYRE MARG**************************/
  function visHoyreMarg() { //tanken er at denne skal vise begivenhet i høyre marg
    $tilkobling = kobleTil(); //opprett tilkobling til db      
    $sql = "SELECT idBeg,  begNavn, begKortInfo, MAX(begstart) AS antall "; 
    $sql .= "FROM tblbegivenhet ";
    $sql .= "WHERE begStart <= NOW() "; 
    $sql .= "AND begSlutt >= NOW() "; 
    $sql .= "GROUP BY idBeg "; 
    $sql .= "ORDER BY antall desc ";
    $sql .= "LIMIT 4;"; //spørring som henter ut de 4 nyeste begivenhetene (se kildereferanse)   
    $resultat = mysql_query($sql, $tilkobling);
    $antall = mysql_num_rows($resultat);
    //echo $sql . "<br />";
    if ($antall >= 1) {    
      while ($rad = mysql_fetch_assoc($resultat)) {
        $begId = $rad['idBeg'];
        $begNavn = $rad['begNavn'];        
        $kort = $rad['begKortInfo'];
        echo "<!--Tittel-->
        <strong>$begNavn</strong><br />
        <!--Kort beskrivelse-->
        $kort<br />
        <!--Link til begivenhet-->
        <a href='begivenhet.php?beg=visEn&bId=$begId'>Les mer</a>
        <br /><br />
        "; //linjeskift for bedre visning i kildekoden
      } //while
    } //if ($antall >= 1)
    mysql_close($tilkobling); //lukker tilkobling til db
  } //visHoyreMarg  
  
  /*********************************SELECT FOR DAG******************************/
  function selectDag($name, $ant_dager) {
    $dag = date('d'); //henter nåværende dag
    $mnd = date('m'); //henter nåværende måned        
    $maxDag = hentMaxAntallDager($mnd); //hvor mange dager skal vises totalt?    
    //sjekk om det er tildato som skal settes og hvor mange dager som skal vises
    if ($ant_dager > 0) { //er det tildato som skal opprettes?          
      $dag = finnTilDato($ant_dager, $maxDag); //hent dag for tildato      
      $maxDag = hentMaxAntallNyMnd($dag); //hvis måned ble endret, så endre antall maxdager
    } //if ($ant_dager > 0)  
    $i = $dag; //$i's startverdi settes lik dagens dato
    $i++; //øk $i så samme dato ikke vises to ganger           
    //start oppretting av select for dag, starter på satt dag           
    $selectDag = '<select id="'.$name.'" name="'.$name.'">
                  <option value="'.$dag.'">'.$dag.'</option>
                  '; //linjeskift for bedre oversikt i kildekoden                           
    while ($i < $maxDag) { //fyller med resten av dagene      
      $selectDag .= "<option value='$i'>$i</option>
      "; //linjeskift for bedre oversikt i kildekoden
      $i++; //øk $i
    } //while
    //hvilken dag startet select på?    
    if ($i > 2) { //er dagens dato den 03. eller høyere?
      $i = $dag; //sett $i sin verdi til dagens dato 
      $j = 1; //brukes for å fylle opp select fra dag 1 etter resterende dager av gjeldende måneder 
      while ($j < $i) { //fyll opp med dagene før dagens dato
        $selectDag .= "<option value='$j'>$j</option>
        "; //linjeskift for bedre oversikt i kildekoden
        $j++; //øk $j
      } //while
    } //if ($i > 2)
    $selectDag .= '</select> / '; //avslutter Select og legger til skilletegn  
    return $selectDag; //returner select med datoer
  } //selectDag
  
  function finnTilDato($ant_dager, $maxDager) {
    $dag = date('d'); //henter nåværende dag 
    $tildato = $dag + $ant_dager; //default for antall dager er 7                                  
    if ($tildato > 30 && $maxDager == 31) { //er dato 31 eller høyere?
      $tildato = $tildato - 30; // trekk ifra 30, burde bli 01 om dato er f.eks 31       
    } else if ($tildato > 31 && $maxDager == 32) {
      $tildato = $tildato - 31; // trekk ifra 31, burde bli 01 om dato er f.eks 32
    } // if ($tildato > 30 && $maxDager == 31)
    return $tildato; //returnerer tildato  
  } //finnTilDato
  
  /*****************************************************************************/
  /* Disse to funksjonene ser kanskje like ut, og de er per definisjon         */
  /* identiske, men forskjellen er at den ene kalles før, og den andre etter   */       
  /* at maks dato er satt. Grunnen er at om dato overstiger gjeldende måned,   */
  /* så vil måned økes til å skifte til neste måned. Denne måneden kan         */
  /* muligens (såfremt det ikke er skille mellom juli og august) så vil        */
  /* antall dager endres                                                       */
  /*****************************************************************************/    
  function hentMaxAntallDager($mnd) {           
    if (($mnd == 2) || ($mnd == 4) || ($mnd == 6) || ($mnd == 9) || ($mnd == 11)) {
      //er det en måned som kun har 30 dager?
      $maxDag = 31; //skal ikke vise mer enn 30 dager (brukes i while løkken)
    } else { //måned som har 31 dager       
      $maxDag = 32; //skal ikke vise mer enn 31 dager (brukes i while løkken) 
    } //if (($mnd == 2) 
    return $maxDag; //returner antall dager gjeldende måned inneholder 
  } //hentMaxDag
  
  function hentMaxAntallNyMnd($tilDato) {      
    $mnd = finnTilMnd($tilDato, date('m')); //hent måneden som ble satt      
    $maxDag = hentMaxAntallDager($mnd);
    return $maxDag; //returner antall dager gjeldende måned inneholder 
  } //hentMaxAntallNyMnd
  
  /*****************************SELECT FOR MÅNED********************************/
  function selectMnd($name, $ant_dager) {  
    $mnd = date('m'); //henter nåværende måned         
    if ($ant_dager > 0) { //skal til måned settes?
      $maxDag = hentMaxAntallDager($mnd); //hent maxdager      
      $tilDato = finnTilDato($ant_dager, $maxDag); //hent dagen for tildato
      $mnd = finnTilMnd($tilDato, $mnd); //finn (og sett) måneden for tildato  
    } //if ($ant_dager > 0)    
    $selectMnd = '<select id="'.$name.'" name="'.$name.'">
                  <option value="'.$mnd.'">'.$mnd.'</option>
                  '; //linjeskift for bedre oversikt i kildekoden    
    $i = $mnd; //$i's startverdi er 1
    $i++; //øk $i så vi ikke får to like måneder      
    while ($i < 13) { //fyll opp med resterende måneder
      $selectMnd .= "<option value='$i'>$i</option>
      "; //linjeskift for bedre oversikt i kildekoden
      $i++; //øk $i
    } //while 
    if ($i > 2) { //er måned etter februar ($i er 3 eller større)?
    	$i = $mnd; //sett $i sin verdi til gjeldende mnd 
    	$j = 1; //brukes for å fylle opp select fra første måned etter resterende måneder av gjeldene år
      while ($j < $i) { //fyll select med månedene før gjeldende måned
        $selectMnd .= "<option value='$j'>$j</option>
        "; //linjeskift for bedre oversikt i kildekoden
        $j++; //øk $j
      } //while        
    } //if ($i > 2)    
    $selectMnd .= '</select> - '; //avslutter Select og legger til skilletegn
    return $selectMnd;    
  } //selectMnd  
  
  function finnTilMnd($tilDato, $mnd) {      
    if ($tilDato < 8) { //er det innen for de 7 første dagene i den nye måneden?    
      $mnd = $mnd + 1; //øk måned med 1  
      if ($mnd == 13) { //var forrige måned desember?
        $mnd = 1; //sett måned til januar      
      } //if ($mnd == 13)
    } //if ($tilDato < 8)
    return $mnd; //returner måneden
  } //finnTilMnd   
  
  /*********************************SELECT FOR ÅR*****************************/
  function selectAar($name) {
    $aar = date('Y'); //henter nåværende år
    $aar_max = $aar + 100; //viser 100 år frem i tid 
    $selectAar = '<select id="'.$name.'" name="'.$name.'">
                  <option value="'.$aar.'">'.$aar.'</option>
                  '; //linjeskift for bedre oversikt i kildekoden
    $aar++; //øk år med 1      
    while ($aar < $aar_max) {
      $selectAar .= "<option value='$aar'>$aar</option>
      "; //linjeskift for bedre oversikt i kildekoden
      $aar++; //øk $aar
    } //while  
    $selectAar .= '</select>';  
    return $selectAar;     
  } //selectAar    
  
  /*****************************SELECT FOR CAMPUS*****************************/
  function selectCampus($ny_bruker, $feilCampus) {
    $tilkobling = kobleTil(); //oppretter tilkobling til db 
    if (isset($_SESSION['email'])) { //er bruker innlogget?
      $email = $_SESSION['email']; //hent e-post
    } //if (isset($_SESSION['email']))  
    if (isset($_POST['SJEKK_INFO']) || isset($_POST['REGISTRER']) || isset($_POST['ENDRE'])) {      
      $valgtCampus = $_POST['idCampus']; //hent ut verdien som ble satt
    } else { //ikke registrering/ikke forsøkt å registrere seg
      $valgtCampus = "Intet valgt";
    } //if (isset($_POST['SJEKK_INFO']) 
    echo "<!--Select for campus-->
    "; //linjeskift for bedre oversikt i kildekoden   
    //LAG SELECT FOR CAMPUS
    echo "<tr><td>Campus:</td>
          <td><select id='idCampus' name='idCampus' style='width: 180px;'>
          "; //linjeskift for bedre oversikt i kildekoden  
    if ($ny_bruker) { //er det en ny bruker som skal registres?
      if ($valgtCampus != "Intet valgt") { //er campus satt?
        $valgt_sql = "SELECT * FROM tblcampus ";
        $valgt_sql .= "WHERE cId = $valgtCampus"; //hent ut den spesifikke
        $resultat = mysql_query($valgt_sql, $tilkobling); //hent resultat
        $rad = mysql_fetch_assoc($resultat); //forventer kun et resultat, derfor ingen løkke 
        $cId = $rad['cId'] ;         
        $cNavn = $rad['cNavn'];
        //sett campus som valgt campus
        echo "<option value='$cId' >$cNavn</option>
        "; //linjeskift for bedre oversikt i kildekoden        
        //hent resterende fra tblCampus
        $sql = "SELECT * FROM tblcampus WHERE cId NOT LIKE $cId ORDER BY cNavn asc"; 
      } else { //ikke valgt campus
        //Select settes til --Velg campus--        
        echo "<option value='Intet valgt'>--Velg campus--</option>
        "; //linjeskift for bedre oversikt i kildekoden 
        $sql = "SELECT * FROM tblcampus ORDER BY cNavn asc"; //hent alle campus
      } //if ($valgtCampus != "Intet valgt") 
    } else { //eller redigerer bruker sin profil?
      //finn brukers registrerte campus    
      $finn_sql = "SELECT cId, cNavn FROM tblStudent, tblCampus ";
      $finn_sql .= "WHERE sEpost = '$email' AND scId=cId";
      $resultat = mysql_query($finn_sql, $tilkobling); //hent resultat
      $rad = mysql_fetch_assoc($resultat); //forventer kun et resultat, derfor ingen løkke 
      $cId = $rad['cId'] ;         
      $cNavn = $rad['cNavn'];
      //sett campus som valgt campus
      echo "<option value='$cId' >$cNavn</option>
      "; //linjeskift for bedre oversikt i kildekoden      
      //velg resterende fra tblCampus   
      $sql = "SELECT * FROM tblcampus WHERE cId NOT LIKE $cId ORDER BY cNavn asc";      
    } //if ($ny_bruker)     
    $resultat = mysql_query($sql, $tilkobling); //hent resultat  
    while ($rad = mysql_fetch_assoc($resultat)) { //legger verdiene inn i en tabell
      $id = $rad['cId'] ;         
      $navn = $rad['cNavn']; 
      //fyller select med campus 
      echo "<option value='$id' >$navn</option>
      "; //linjeskift for bedre oversikt i kildekoden 
    } //while 
    echo "</select> $feilCampus</td></tr>
    "; //avslutter select og legger inn linjeskift for bedre lesbarhet i kildekoden      
    mysql_close($tilkobling); //lukker tilkobling til db 
  } //selectCampus
  
  /*****************************SELECT FOR FAGOMRÅDE*****************************/
  function selectFagomrade($ny_bruker, $feilFag) {      
    $tilkobling = kobleTil(); //oppretter tilkobling til db 
    if (isset($_SESSION['email'])) { //er bruker innlogget?
      $email = $_SESSION['email']; //hent e-post
    } //if (isset($_SESSION['email'])) 
    //knapper som brukes ved registrering/masseutsending 
    if (isset($_POST['SJEKK_INFO']) || isset($_POST['REGISTRER']) || isset($_POST['ENDRE']) 
    || isset($_POST['SEND']) || isset($_POST['LAGRE_BEGIVENHET'])
    || isset($_POST['LAGRE_ENDRING'])) {        
      $valgtFag = $_POST['idFag']; //hent ut verdien som ble satt      
    } else { //ikke registrering/ikke forsøkt å registrere seg
      $valgtFag = "Intet valgt";
    } //if (isset($_POST['SJEKK_INFO']) 
    echo "<!--Select for fagområde-->
    "; //linjeskift for bedre oversikt i kildekoden 
    //LAG SELECT FOR FAGOMRÅDE
    echo "<tr><td>Fagområde:</td>
          <td><select id='idFag' name='idFag' style='width: 180px;'>
          "; //linjeskift for bedre oversikt i kildekoden  
    if ($ny_bruker) { //er det en ny bruker som skal registres?
      if ($valgtFag != "Intet valgt") { //er et fagområde valgt?
        $valgt_sql = "SELECT * FROM tblFagomrade ";
        $valgt_sql .= "WHERE fId = $valgtFag"; //hent ut den spesifikke
        $resultat = mysql_query($valgt_sql, $tilkobling); //hent resultat
        $rad = mysql_fetch_assoc($resultat); //forventer kun et resultat, derfor ingen løkke 
        $fId = $rad['fId'] ;         
        $fNavn = $rad['fNavn'];
        //sett fagområde som valgt fagområde
        echo "<option value='$fId' >$fNavn</option>
        "; //linjeskift for bedre oversikt i kildekoden          
        //hent resterende fra tblFagomrade
        $sql = "SELECT * FROM tblFagomrade WHERE fId NOT LIKE $fId ORDER BY fNavn asc"; 
      } else { //ikke valgt fagområde
        //Select settes til --Velg fagområde--
        echo "<option value='Intet valgt'>--Velg fagområde--</option>
        "; //linjeskift for bedre oversikt i kildekoden
        $sql = "SELECT * FROM tblFagomrade ORDER BY fNavn asc"; //hent alle fagområder
      } //if ($valgtFag != "Intet valgt") 
    } else { //redigerer bruker sin profil?
      //finn brukers registrerte fagområder    
      $finn_sql = "SELECT fId, fNavn FROM tblStudent, tblFagomrade ";
      $finn_sql .= "WHERE sEpost = '$email' AND sfId=fId";
      $resultat = mysql_query($finn_sql, $tilkobling); //hent resultat
      $rad = mysql_fetch_assoc($resultat); //forventer kun et resultat, derfor ingen løkke 
      $fId = $rad['fId'] ;         
      $fNavn = $rad['fNavn'];
      //sett fagområde som valgt fagområde
      echo "<option value='$fId' >$fNavn</option>
      "; //linjeskift for bedre oversikt i kildekoden      
      //hent resterende fra tblFagomrade  
      $sql = "SELECT * FROM tblFagomrade WHERE fId NOT LIKE $fId ORDER BY fNavn asc";      
    } //if ($ny_bruker)     
    $resultat = mysql_query($sql, $tilkobling); //hent resultat  
    while ($rad = mysql_fetch_assoc($resultat)) { //legger verdiene inn i en tabell
      $id = $rad['fId'] ;         
      $navn = $rad['fNavn'];
      echo "<option value='$id' >$navn</option>
      "; //linjeskift for bedre oversikt i kildekoden  
    } //while 
    echo "</select> $feilFag</td></tr>
    "; //avslutter select og legger inn linjeskift for bedre lesbarhet i kildekoden     
    mysql_close($tilkobling); //lukker tilkobling til db 
  } //selectFagomrade
  
  /*****************************SELECT FOR ÅRSKULL******************************/
  function selectAarskull($ny_bruker) {    
    $studaar = 1980; //antatt oppstart av Hibu
    $kjor = true; //variabel for  while - løkka
    if (isset($_SESSION['email'])) { //er bruker innlogget?
      $email = $_SESSION['email']; //hent e-post
    } //if (isset($_SESSION['email'])
    if (isset($_POST['SJEKK_INFO']) || isset($_POST['REGISTRER']) || isset($_POST['ENDRE'])) {      
      $valgtAar = $_POST['aarskull']; //hent ut verdien som ble satt
    } else { //ikke registrering/ikke forsøkt å registrere seg
      $valgtAar = "Intet valgt";
    } //if (isset($_POST['SJEKK_INFO'])
    echo "<!--Select for årskull-->
    "; //linjeskift for bedre oversikt i kildekoden      
    //LAG SELECT FOR ÅRSKULL             
    echo "<tr><td>Årskull:</td><td>
          <select id='aarskull' name='aarskull'>
          "; //linjeskift for bedre oversikt i kildekoden 
    if ($ny_bruker) { //er det en ny bruker som skal registres?
      if ($valgtAar != "Intet valgt") { //har den registrerende valgt et år?
        echo "<option value='$valgtAar' >$valgtAar</option>"; //sett året som ble valgt
        //i tilfelle den registrerende ombestemmer seg, så har han/hun
        //muligheten til å sette feltet til intet valgt
        echo "<option value='Intet valgt' >--Velg studieår--</option>
        "; //linjeskift for bedre oversikt i kildekoden 
      } else { //ikke valgt årskull
        echo "<option value='Intet valgt'>--Velg studieår--</option>
        "; //linjeskift for bedre oversikt i kildekoden 
      } //if ($valgtAar != "Intet valgt")
    } else { //bruker redigerer sin profil
      $tilkobling = kobleTil(); //koble til db
      $finn_sql = "SELECT sAarskull FROM tblStudent ";
      $finn_sql .= "WHERE sEpost = '$email'";
      $resultat = mysql_query($finn_sql, $tilkobling); //hent resultat
      $rad = mysql_fetch_assoc($resultat); //forventer kun et resultat, derfor ingen løkke   
      $aar = $rad['sAarskull'] ; //hent verdien
      if ($aar != null) { //satte bruker årskull ved registrering?
          echo "<option value='$aar' >$aar</option>
          "; //linjeskift for bedre oversikt i kildekoden 
      } else { //årskull var ikke satt ved registrering
          echo "<option value='Intet valgt'>--Velg studieår--</option>
          "; //linjeskift for bedre oversikt i kildekoden 
      } //if ($aar != null)            
      mysql_close($tilkobling); //lukk tilkobling til db
    } //if ($ny_bruker)
    //løkke som fyller select med studieår, den kjører så lenge $kjor = true
    while ($kjor) {       
      if ($studaar < 2030) { //sjekker om årskullet er mindre enn 2030 
        //fyll select
        echo "<option value='$studaar'>$studaar</option>
        "; //linjeskift for bedre oversikt i kildekoden 
        $studaar++; //øk året    
      } else { //$studaar = 2029      
        $kjor = false; //stopper løkken
      } //if ($studaar < 2030) 	
    } //while
    echo "</select> (valgfri)</td></tr>
    "; //avslutter select og legger inn linjeskift for bedre lesbarhet i kildekoden
  } //selectAarskull
?>