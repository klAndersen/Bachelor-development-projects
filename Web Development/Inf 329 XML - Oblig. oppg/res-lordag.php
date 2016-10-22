<?php 
  include "include/funksjoner.inc.php";
  include "include/helgeresultat.inc.php";
  include "include/include_topp.inc.php"; 
?>
  <title>Resultat lørdag</title>
  </head>
  <body>
<?php
  include "include/link.inc.php"; //meny/intern navigering på siden
?>  
  <div id="resten">
  <h2>Omsetning lørdag</h2>
  <br><br>
  <?php
    $dag = "lørdag";
    printHelg($dag);
  ?>
  </body>
</html>