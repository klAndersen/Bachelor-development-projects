create or replace
TRIGGER BUD_TRIGGER
BEFORE UPDATE ON BUD_TBL 
FOR EACH ROW
BEGIN
/*
Opp til applikasjonsprogrammerer � legge inn sjekk for 'duplicate primary key' 
ved bruk av Insert - setninger.
Det er ikke lagt inn kontroll p� om gittdato og fristdato er innenfor "v�r tidsalder"
i tilfelle backup/rollback m� gjennomf�res, da vil tidligere data gi feilmelding.
*/
--er nytt bel�p mindre enn gammelt bel�p?
IF (:new.B_BELOP < :old.B_BELOP) THEN 
  raise_application_error(-20000, 'Ved oppdatering av bud m� nytt bel�p v�re st�rre enn gammelt bel�p.');
--er bel�pene like, og ny frist etter gammel frist?
ELSIF (:new.B_BELOP = :old.B_BELOP) AND (to_char(:new.b_fristdato, 'YYYYMMDD') <= to_char(:old.b_fristdato, 'YYYYMMDD')) THEN 
  raise_application_error(-20001, 'Ved oppdatering av bud med samme bel�p m� en senere dato for frist registreres.');
--er bel�pet �kt, men fristdato satt til en tidligere utgang?
ELSIF (:new.B_BELOP > :old.B_BELOP) AND (to_char(:new.b_fristdato, 'YYYYMMDD') < to_char(:old.b_fristdato, 'YYYYMMDD')) THEN  
   raise_application_error(-20002, 'Du kan ikke sette fristdato til en tidligere dato selv om bud �kes.');
END IF;
END;