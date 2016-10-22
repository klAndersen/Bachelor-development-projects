<?php 
  include "include/funksjoner.inc.php";
  include "include/nyttaar.inc.php";
  include "include/feilmelding.inc.php";
  include "include/include_topp.inc.php";   
  $feilVeksel = ""; //initiering
  $feilTotal = ""; 
?>
  <title>Nytt år</title>
  </head>
  <!--Setter fokus ved lastning av siden-->
  <body onload="focusVekslepenger();">
<?php
  include "include/link.inc.php"; //meny/intern navigering på siden
?>  
  <div id="resten">
  <h2>Nytt år - klargjøring</h2>
  <?php
    if (isset($_POST['LAGRE_AAR'])) { //skal det lagres?
      if(!(lagreAAr())) { //ble initalverdi regisrert?
        $feilVeksel = feilVeksel();
        $feilTotal = feilVekselTotal();
        aarSkjema($feilVeksel, $feilTotal); 
      } else { //initial verdi ble registrert
        echo "Initial vekselverdi ble opprettet og registrert.";      
      } //if(!(lagreAAr()))
    } else { //vis skjema
      aarSkjema($feilVeksel, $feilTotal);    
    } //if (isset($_POST['LAGRE_AAR']))
  ?>
  </div>
  </body>
  </html>    