<?php 
    include "/include_php/funksjoner.inc.php"; //inkluderer felles funksjoner
    include "/include_php/feilmeldinger.inc.php"; //inkluderer feilmeldinger  
    include "/include_php/profil.inc.php";
    include "/include_php/nettverk.inc.php"; 
    echo sjekk_autentisert(); //sjekker om bruker er innlogget
    if (!isset($_GET['Profil'])) {
      $_GET['Profil'] = "profilInfo";
    } //if (!isset($_GET['Profil']))  
    $feilFnavn = ""; //initieres
    $feilEnavn = ""; 
    $feilEmail = "";  
    $feilNett = "";     
    $feilCampus = ""; //initieres, tas med her kun pga feilmelding(Notice: Undefined variable...) 
    $feilFag = ""; //men disse gjelder kun på registrering av nye brukere
    include "/include_sideoppsett/include_topp.inc.php"; //inkluderer toppen av siden
    if (isset($_POST['LAGRE_ENDRING']) && $_POST['LAGRE_ENDRING'] == "Lagre endringer") { 
      //skal endringer lagres? 
      $email = $_SESSION['email'];
      $ny_Epost = $_POST['email'];
      $feilFnavn = feilFnavn(); //er fornavn ok?
      $feilEnavn = feilEnavn(); //er etternavn ok?
      $feilEmail = feilEmail(true, $email, $ny_Epost); //er e-post ok?
      $lagre = lagreEndring(); //forsøk å lagre endringer
      if ($lagre) { //ble endringer lagret?        
        echo '<meta http-equiv="Refresh" content="0" />'; //oppdaterer siden "umiddelbart"             
      } //if ($lagret)
    } //if (isset($_POST['LAGRE_ENDRING']))       
?>
  <title>Rediger profil</title>    
  </head>    
  <body onload="focusNettverk();"> 
  <!--SIDETRAILER-->
  <div id="heading"><h2>Redigering av profil</h2></div>
  <?php 
    include "/include_sideoppsett/include_logo_venstre.inc.php"; //inkluderer logo og venstre meny
  ?>
  <!--INNHOLD-->
  <div id="innhold">
  <!--Link meny-->
  <a href="redigerProfil.php?Profil=profilInfo">Rediger profil</a><br />   
  <a href="redigerProfil.php?Profil=leggTilNett">Koble til nettverk</a><br /> 
  <a href="redigerProfil.php?Profil=fjernNettverk">Fjern nettverk</a><br />
  <a href="profil.php">Tilbake til Min profil</a><br />    
  <br />                                            
  <?php    
      if ($_GET['Profil'] == "profilInfo") {        
        echo redigerPersonligInfo($feilFnavn,$feilEnavn,$feilEmail); //viser felt med navn og e-post                    
        selectCampus(false, $feilCampus); //select for campus, false pga det er redigering 
        selectFagomrade(false, $feilFag); //select for fagområde, false pga det er redigering 
        selectAarskull(false); //select for årskull, false pga det er redigering
        echo redigerSosialeNettverk(); //viser felt for sosiale nettverk og karrierre/utdanning                   
  ?>
  <br />
  <table>
  <tr><td>
  <input type="submit" name="LAGRE_ENDRING" value="Lagre endringer" />
  <input type="reset" value="Tilbakestill feltene" />
  </td></tr>
  </table>
  </form>
  </fieldset>
  <?php   
      } else if($_GET['Profil'] == "leggTilNett") { //koble til nytt nettverk  
        echo "<!--Legg deg til i et nettverk-->";
        if (isset($_POST['LEGG_NETTVERK']) && $_POST['LEGG_NETTVERK'] == "Koble til nettverket") {                   
          if (!nyttNettverk()) { //ble bruker tilknyttet nettverket?
            $feilNett = feilNettverk();
            echo nyttNettverkSkjema($feilNett);
          } //if (!nyttNettverk())
        } else { //gi mulighet for å koble til et nettverk     
          echo nyttNettverkSkjema($feilNett); //viser skjema
        } //(isset($_POST['LEGG_NETTVERK'])  
    } else { //ønsker å fjerne seg fra nettverk 
      if (isset($_POST['FJERN_NETTVERK']) && $_POST['FJERN_NETTVERK'] == "Fjern deg fra nettverket") {                   
        if (!fjernNettverk()) { //ble bruker fjernet fra nettverket?
          $feilNett = feilNettverk();
          echo visMineNettverk($feilNett); //vis nettverk med feilmelding 
        } //if (!nyttNettverk())
      } else { //vis nettverkene som bruker har
        echo visMineNettverk($feilNett); 
      } // if (isset($_POST['FJERN_NETTVERK'])    
    } //if ($_GET['Profil'] == "profilInfo") 
  ?>
  </div>
  <?php
    include "include_sideoppsett/include_hoyre.inc.php"; //inkluderer høyre marg/meny
    include "include_sideoppsett/include_bunn.inc.php"; //inkluderer bunnen av siden    
  ?>