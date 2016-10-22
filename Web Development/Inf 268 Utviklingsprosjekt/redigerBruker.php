<?php 
    include "/include_php/funksjoner.inc.php"; //inkluderer felles funksjoner
    include "/include_php/feilmeldinger.inc.php"; //inkluderer feilmeldinger  
    include "/include_admin/sett_brukertype.inc.php";
    echo sjekk_autentisert(); //sjekker om bruker er innlogget
    $sbId = finnBrukertype(); //finn den innloggedes brukertype
    if ($sbId != 3 && $sbId != 4) { //er innlogget administrator/moderator?
      die("Denne siden er kun for administrator og moderator. " . FORSIDE);
    } //if ($sbId != 3 && $sbId != 4)
    /***HENT UT INFORMASJON OM VALGT BRUKER***/
    $tilkobling = kobleTil(); //kobler til db
    $sId = mysql_real_escape_string($_GET['brukerId']); //hent denne brukers id
    $sql = "SELECT * FROM tblStudent,tblBrukertype,tblCampus,tblFagomrade ";
    $sql .= "WHERE sId = $sId AND sbId=bId AND scId=cId AND sfId=fId;";
    $resultat = mysql_query($sql, $tilkobling);
    //echo $sql;
    $antall = mysql_num_rows($resultat);
    if ($antall != 1) { //finnes brukeren (i tilfelle endring av url)
      die("Denne brukeren finnes ikke. " . TILBAKE);
    } //if ($antall != 1)
    //siden det er kun en bruker, så trengs ikke løkke
    $rad = mysql_fetch_assoc($resultat); //legg resultatet i en assosiativ array
    //profilbile
    $pBilde = $rad['sProfilBilde'];
    //personlig info
    $fNavn = $rad['sFornavn'];
    $eNavn =  $rad['sEtternavn'];
    $epost = $rad['sEpost'];
    //sosiale nettverk og kort om karriere/utdanning
    $fb = $rad['sFacebook'];
    $ms = $rad['sMySpace'];
    $hs = $rad['sHjemmeside']; 
    $About =  $rad['sAbout'];
    //brukertype
    $bTypeId = $rad['sbId'];
    $bType = $rad['bType'];
    //skoleinfo
    $campus = $rad['cNavn'];
    $fag = $rad['fNavn'];    
    mysql_close($tilkobling); //lukker tilkobling til db
    $feilmelding = ""; //initiering
    if (isset($_POST['E_MAIL']) && $_POST['E_MAIL'] == "Endre e-post") {
        $feilmelding = endreMail($sId);
    } //if (isset($_POST['E_MAIL']))
    include "/include_sideoppsett/include_topp.inc.php"; //inkluderer toppen av siden
    //denne ligger etter siden det er en meta tagg
    if (isset($_POST['MODERATOR']) || isset($_POST['ADMINISTRATOR']) || isset($_POST['REGISTRERT'])) {
      //inntreffer kun om man har trykket på en av knappene for oppgradering/nedgradering 
      echo '<meta http-equiv="Refresh" content="1" />'; //oppdaterer siden etter 1 sekund
    } //if (isset($_POST['MODERATOR'])
?>
  <title>Rediger</title>    
  </head> 
  <body>  
  <!--------SIDETRAILER-------->  
  <div id="heading"><h2>Rediger/Utesteng bruker</h2></div>
  <?php 
    include "/include_sideoppsett/include_logo_venstre.inc.php"; //inkluderer logo og venstre meny
  ?> 
  <!--------INNHOLD-------->  
  <div id="innhold">
  <!--Link meny-->
  <?php
    echo '<a href="karantene.php?brukerId='.$sId.'">Vis karantener</a><br />
    <a href="melding.php?melding=Send Melding&mottaker='.$epost.'">Send melding til bruker</a><br />
    <a href="admin_brukere.php">Tilbake til administrering</a><br /><br />
    <center>
    <table>
    ';
    if (!empty($pBilde)) { //sjekker om bruker har profilbilde      
      echo '<!--Viser brukers profilbilde-->
      <tr><td>Brukers profilbilde:</td><td><img src="profilbilde/'.$pBilde.'" height="150" /></td></tr>'; 
    } //if (!empty($pBilde)) 
  ?>  
  <!--Brukers profil informasjon-->
  <tr><td>Brukers id:</td><td align="left"><?php echo $sId;  ?> </td></tr>
  <tr><td>Brukers navn:</td><td align="left"><?php echo  $fNavn . ' ' . $eNavn;  ?> </td></tr>
  <tr><td>Brukers campus:</td><td align="left"><?php echo  $campus;  ?> </td></tr>
  <tr><td>Brukers fagområde:</td><td align="left"> <?php echo  $fag;  ?> </td></tr>   
  <!--Mulighet for å endre brukers e-post-->
  <tr><form action="" method="post"><td align="left">Brukers e-post: </td>  
  <?php        
    echo '<td align="left"><input type="text" value="' .$epost. '" name="email" />
    <input type="submit" name="E_MAIL" value="Endre e-post" /> 
    </td></form></tr>
    '; //linjeskift for bedre lesbarhet i kildekoden            
    echo endreTilgang ($sbId, $bTypeId, $bType, $sId); //funksjon for å endre brukertype      
    //visning av linker (ren tekst) og kort om bruker
    if (!empty($fb) && $fb != "http://") { //har bruker skrevet inn link til Facebook?
      echo "<tr><td>Brukers Facebook:</td><td>$fb</td></tr>
      ";//linjeskift for bedre lesbarhet i kildekoden
    } //if (!empty($fb) && != "http://")
    if (!empty($ms) && $ms != "http://") {  //har bruker skrevet inn link til MySpace?
      echo "<tr><td>Brukers MySpace:</td><td>$ms</td></tr>
      ";//linjeskift for bedre lesbarhet i kildekoden      
    } //if (!empty() && != "http://")
    if (!empty($hs) && $hs != "http://") {  //har bruker skrevet inn link til egen hjemmeside?
      echo "<tr><td>Brukers Hjemmeside:</td><td>$hs</td></tr>
      ";//linjeskift for bedre lesbarhet i kildekoden      
    } //if (!empty() && != "http://")
    if (!empty($About)) { //har bruker skrivd noe om sin karriere/kompetanse?
      echo "<tr><td>Brukers Karriere:</td><td>$About</td></tr>
      ";//linjeskift for bedre lesbarhet i kildekoden      
    } //if (!empty($About))           
    if (!empty($feilmelding)) { //har en feil oppstått under endring av e-post?   
      echo "<!--Viser feilmelding-->
      <tr><td>$feilmelding</td></tr>
      "; //linjeskift for bedre lesbarhet i kildekoden
    } // if (!empty($feilmelding))   
     echo '</table>';//linjeskift for bedre lesbarhet i kildekoden       
  ?>   
  </center>
  </div>  
  <?php
    include "include_sideoppsett/include_hoyre.inc.php"; //inkluderer høyre marg/meny
    include "include_sideoppsett/include_bunn.inc.php"; //inkluderer bunnen av siden    
  ?>