<?php 
  include "/include_php/funksjoner.inc.php"; //inkluderer felles funksjoner
  if (isset($_SESSION['email'])) { //hvis bruker er innlogget   
    session_destroy(); //fjern alt tilknyttet brukers innloggging
    header ("Location: default.php"); //gå til forsiden
  } else { //ikke innlogget, men kommer til siden...
    echo "Du er ikke innlogget, så du kan ikke logge ut. " . FORSIDE;
  } //if (isset($_SESSION['email']))
?>