<?php 
  include "include/funksjoner.inc.php";
  include "include/penger.inc.php";
  include "include/include_topp.inc.php"; 
?>
  <title>Registrer penger</title>
  </head>
  <body>
<?php
  include "include/link.inc.php"; //meny/intern navigering på siden
?>  
  <div id="resten">
  <h2>Registrer penger levert</h2>
  <br><br>
  <?php
    echo skjemaInnlevering();    
  ?>
  </body>
</html>