<?php
  echo '<!--HØYRE-->
  <div id="hoyre">
  <!--Dagens dato-->
  Dagens dato: ' . date('d/m-Y'); //viser dagens dato 
  echo '
  <!--Luft-->
  <br /><br />
  <!--Vis begivenheter-->
  '; //linjeskift for bedre oversikt i kildekoden
  echo visHoyreMarg(); //hent ut/vis aktive begivenheter
  echo '</div>';
?>