<?php
  /***************************************************************************** 
  *  Dette dokumentet inneholder linkene som blir vist på nettsiden            *
  *  Sider som ikke fører noe sted/ikke lagd leder til kommer.php              *
  *  for å fortelle bruker/besøkende at denne delen er under utvikling         *
  *  men også for å gjøre det enklere for applikasjonprogrammerer å legge      *
  *  inn disse                                                                 *  
  *  Laget av Knut Lucas Andersen                                              *    
  *****************************************************************************/
  echo '<!--topp-delen-->
  <div id="topp">
  <!--Her kan logo legges inn-->
  <div id="logo"><a href="default.php"><img src="logo.png"></a></div>
  </div>  
  <div id="linker">
  <!--la inn center for midtstilling for bedre visuell plassering av linkene-->
  <center>
  <a href="kommer.php">Registrer</a> 
  <a href="buffer.php?buffer=avdeling">Avdeling</a> 
  <!--tomme linker for enklere oppbygging for applikasjonsprogrammer på et senere tidspunkt-->
  <!--fører til kommer.php som forteller at siden er under utvikling-->
  <a href="buffer.php?buffer=person">Person</a>  
  <a href="penger.php">Penger</a>  
  <a href="kommer.php">Resultat</a> 
  <a href="res-lordag.php">Lørdag</a>  
  <!--tomme linker for enklere oppbygging for applikasjonsprogrammer på et senere tidspunkt-->
  <!--fører til kommer.php som forteller at siden er under utvikling-->
  <a href="res-sondag.php">Søndag</a>  
  <a href="resultat.php">Samlet</a>  
  <a href="kommer.php">Kontroll</a>   
  <a href="nyttAar.php">Bytt år</a> 
  </center>
  </div>';
?>