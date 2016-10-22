<?php 
  include "include/funksjoner.inc.php";
  include "include/helgeresultat.inc.php";
  include "include/include_topp.inc.php"; 
?>
  <title>Resultat søndag</title>
  </head>
  <body>
<?php
  include "include/link.inc.php"; //meny/intern navigering på siden
?>  
  <div id="resten">
  <h2>Omsetning søndag</h2>
  <br><br>
  <?php
    $dag = "søndag";
    printHelg($dag);
  ?>
  </body>
</html>