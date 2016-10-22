<?php
  //splittet opp for enklere og raskere endring
  $eier = "Høgskolen i Buskerud"; //eier/firma som har nettsiden
  $kontaktEier = "<tr><td>All rights reserved &copy; $eier</td></tr>";  
  $mail = "AdminHibu@localhost"; //e-post adresse til administrator/eier av nettsiden 
  $mailTo = '<tr><td>Kontakt oss: <a href="mailto:'.$mail.'">'.$mail.'</a></td></tr>'; //send e-post til administrator/eier 
  //linjeskift for bedre oversikt i kildekoden
  echo "
  <!--BUNNEN-->
  <div id='bunn'>
  <center>
  <table>
  $kontaktEier
  $mailTo
  </table>
  </center>
  <!--Viser link til policy-->
  <div id='policy' onclick='visPolicy();'>Policy</div>  
  </div>
  </body>
  </html>";
?>