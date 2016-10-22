<?php
  /*******************************ENDRE TILGANG*******************************/    
  function endreTilgang($sbId,$bTypeId,$bType,$sId) {
    if ($sbId == 4) { //har innlogget admin rettigheter?
      if ($bTypeId == 2) { //valgt bruker er registrert   
        visBrukertype($bType); //viser valgt brukers brukertype                  
        settModerator($sId); //kan sette valgt bruker til moderator        
      } else if ($bTypeId == 3) { //valgt bruker er moderator
        visBrukertype($bType); //viser valgt brukers brukertype
        settAdministrator($sId); //kan sette valgt bruker til administrator
        settRegistrert($sId); //kan sette valgt bruker til registrert            
      } else { //valgt bruker er administrator, ingen rettigheter til endring
        visBrukertype($bType); //viser valgt brukers brukertype
      } //if ($bTypeId == 2)          
    } else { //innlogget er moderator, kan kun se valgt brukers profilinformasjon
      visBrukertype($bType); //viser valgt brukers brukertype
    } //if ($sbId == 4)
  } //endreTilgang                        
  
  /*******************************VIS BRUKERTYPE*******************************/
  function visBrukertype($bType) { //viser brukertypen
    echo '<tr><td>Brukertypen er: </td><td>'. $bType .'</td></tr>
    '; //linjeskift for bedre lesbarhet i kildekoden 
  } //visBrukertype  
   
  /****************************SETT BRUKERTYPE*********************************/
  function settAdministrator($sId) { //opprett ny administrator
    echo '<tr><td><form action="" method="post">
    <input type="submit" name="ADMINISTRATOR" value="Sett brukertype til Administrator"/>
    </form></td></tr>
    '; //linjeskift for bedre lesbarhet i kildekoden
    if (isset($_POST['ADMINISTRATOR']) && $_POST['ADMINISTRATOR'] == "Sett brukertype til Administrator") {
      $nySbId = 4;
      settTilgang($nySbId,$sId);
    } //if (isset($_POST['ADMINISTRATOR'])          
  } //settAdmin
  
  function settModerator($sId) { //opprett ny moderator
    echo '<tr><td><form action="" method="post">
    <input type="submit" name="MODERATOR" value="Sett brukertype til Moderator">
    </form></td></tr>
    '; //linjeskift for bedre lesbarhet i kildekoden
    if (isset($_POST['MODERATOR']) && $_POST['MODERATOR'] == "Sett brukertype til Moderator") {
      $nySbId = 3;
      settTilgang($nySbId,$sId);
    } //if (isset($_POST['MODERATOR'])         
  } //settModerator
   
  function settRegistrert($sId) { //sett en moderator til bruker
    echo '<tr><td><form action="" method="post">
    <input type="submit" name="REGISTRERT" value="Sett brukertype til Registrert"/>
    </form></td></tr>
    '; //linjeskift for bedre lesbarhet i kildekoden
    if (isset($_POST['REGISTRERT']) && $_POST['REGISTRERT'] == "Sett brukertype til Registrert") {
      $nySbId = 2; //sett brukertype til 2
      settTilgang($nySbId,$sId); //sett ny brukertype
    } //if (isset($_POST['BRUKER'])
  } //settRegistrert
  
  function settTilgang($nySbId, $sId) { //oppdaterer brukertypen
    $tilkobling = kobleTil(); //oppretter tilkobling til db
    $update_sql = "UPDATE tblStudent ";
    $update_sql .= "SET sbId = $nySbId ";
    $update_sql .= "WHERE sId = $sId;"; 
    //echo $sql;
    mysql_query($update_sql,$tilkobling); 
    mysql_close($tilkobling); //lukker tilkobling til db
  } //settTilgang  
?>