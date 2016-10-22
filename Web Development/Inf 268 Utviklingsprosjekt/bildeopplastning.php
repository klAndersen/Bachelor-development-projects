 <?php 
   include "/include_php/funksjoner.inc.php"; //inkluderer felles funksjoner
   include "/include_php/profil.inc.php"; //inkluderer profil.inc.php
   echo sjekk_autentisert(); //sjekker om bruker er innlogget
   if (!isset($_GET['bilde'])) {
      $_GET['bilde'] = "lastOpp";
    } // if (!isset($_GET['bilde']))    
    include "/include_sideoppsett/include_topp.inc.php"; //inkluderer toppen av siden    
 ?>
  <title>Album</title>    
  </head> 
  <body>  
  <center>
  <!--Link meny-->
  <table>
  <tr><td><a href="bildeopplastning.php?bilde=lastOpp">Last opp bilde</a></td></tr>
  <tr><td><a href="bildeopplastning.php?bilde=album">Velg bilde fra album</a></td></tr>
  <!--Denne linken tar deg tilbake til redigerProfil-->
  <tr><td><a href="redigerProfil.php?Profil=profilInfo">Tilbake til redigering av profil</a></td></tr>
  </table>
  <br />    
  <?php    
    if ($_GET['bilde'] == "lastOpp") { //skal det lastes opp et nytt bilde?    
      echo '<!--Skjema for opplastning av bilde(r)-->
            <form action="" method="post" enctype="multipart/form-data">
            Nytt profilbilde? 
            <input type="file" name="filbane" size="30" />
            <input type="submit" name="LASTOPP" value="Last opp bilde" /><br />
            <!--Merknad til bruker om at bildet kan bli sett og brukt av andre-->
            <font color="red" size="1pt">NB! Bildet du laster opp blir tilgjengelig 
            for alle å se og eventuelt bruke på sin profil.</font>   
            </form>'; //skjema for opplastning av bilder
      if (isset($_POST['LASTOPP'])) { //skal et bilde lastes opp?
        nyttProfilbilde(); //forsøk å laste opp bildet           
      } //if (isset($_POST['LASTOPP']))   
    } else { //skal velge et profilbilde fra albumet
      echo '<!--Viser bildene fra album-->
            Klikk "Velg" for å velge et profilbilde<br />'; //hjelp/hint til bruker
      $mappeNavn = "profilbilde/"; //adressen til/navnet på mappen
      $mappeRef = opendir($mappeNavn); //åpner mappen
      while ($neste  = readdir($mappeRef)) { //henter ut bildene
        $filnavn = "profilbilde/" . $neste; //leser filer
        //hvis filene er av gyldig filtype
        if (strstr($filnavn,"jpeg") || strstr($filnavn,"jpg") || strstr($filnavn,"gif") 
            || strstr($filnavn,"png")) {                            
          echo "<img src='$filnavn' height='180' /><br />"; //viser bildene..
          //legger filnavnet inn i en link som deretter skal legges inn på redigerProfil.php 
          echo "<a href='redigerProfil.php?Profil=profilInfo&bValg=$neste'>";
          echo "Velg</a><br />"; //avslutter link og legger inn luft             
          } //if (strstr($filnavn,".jpg"))
        } //while
      closedir($mappeRef); //lukker mappen
    } //if ($_GET['bilde'] == "lastOpp")                   
  ?>
  </center>
  </body>
  </html>