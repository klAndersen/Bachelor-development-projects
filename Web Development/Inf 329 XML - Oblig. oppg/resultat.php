<?php
  //oversikt over resultat (totalt?)
  include "include/funksjoner.inc.php";
  include "include/samlet.inc.php";
  include "include/include_topp.inc.php"; 
?>
  <title>Samlet oversikt</title>
  </head>
  <body>
<?php
  include "include/link.inc.php"; //meny/intern navigering på siden
?>
  <div id="resten">
  <h2>Resultat årets loppemarked</h2>
  <br />
  <br /> 
  <?php
    echo printResultat(); //skriver ut resultatet 
  ?> 
  </div>
  </body>
</html>