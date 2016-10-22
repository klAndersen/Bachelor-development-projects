<?php 
  include "include/funksjoner.inc.php";
  include "include/include_topp.inc.php";     
?>
  <title>Vang musikkkorps</title>
  </head>
  <body>
<?php
  include "include/link.inc.php"; //meny/intern navigering på siden
?>  
  <div id="resten">
  <center><h1 style="color:red;">Loppemarked <?php echo date('Y') ?></h1></center>

  Denne applikasjonen er laget for å støtte økonomiansvarlig for et loppemarkedet.<br /><br />
  Det er tatt ut 50700.00 i vekslepenger til årets loppmarked.<br /><br />
  
  Menysystemet skal være ganske intuitivt, for den som kjenner opplegget for loppmarkedet. 
  Under registreringsfunksjoner må avdelinger loppmarkedet skal være delt inn i være registrert riktig. 
  Så lenge det er en eller flere personer registrert på en avdeling, kan denne avdelingen ikke slettes. 
  Regnskapsoversikt settes opp avdeling for avdeling.<br /><br />

  Når avdelinger er riktig registrert, kan man registrere personer. 
  Dette kan gjøres på forhånd, eller i forbindelse med utdeling av vekslepenger hver morgen. 
  Det kommer opp et forslag på beløp vekslepenger som blir utdelt, men dette kan endres for den enkelte. 
  Det skal utpekes en person som leder for hver avdeling.<br /><br />

  Så lenge det er vekslepenger ute, vil regnskapsoversikt ikke være riktig. 
  Først etter at alle salgsvesker er levert inn for dagen, vil oversikten være riktig.<br /><br />

  Det er meningen at systemet skal gi god nok oversikt til at penger telt opp ved dags slutt skal kunne kontrolleres mot regnskapstall. 
  Penger satt inn i bank skal også registreres.<br /><br />
  </div>
  </body>
</html>