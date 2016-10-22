create or replace
TRIGGER BUD_TRIGGER
BEFORE UPDATE ON BUD_TBL 
FOR EACH ROW
BEGIN
/*
Opp til applikasjonsprogrammerer å legge inn sjekk for 'duplicate primary key' 
ved bruk av Insert - setninger.
Det er ikke lagt inn kontroll på om gittdato og fristdato er innenfor "vår tidsalder"
i tilfelle backup/rollback må gjennomføres, da vil tidligere data gi feilmelding.
*/
--er nytt beløp mindre enn gammelt beløp?
IF (:new.B_BELOP < :old.B_BELOP) THEN 
  raise_application_error(-20000, 'Ved oppdatering av bud må nytt beløp være større enn gammelt beløp.');
--er beløpene like, og ny frist etter gammel frist?
ELSIF (:new.B_BELOP = :old.B_BELOP) AND (to_char(:new.b_fristdato, 'YYYYMMDD') <= to_char(:old.b_fristdato, 'YYYYMMDD')) THEN 
  raise_application_error(-20001, 'Ved oppdatering av bud med samme beløp må en senere dato for frist registreres.');
--er beløpet økt, men fristdato satt til en tidligere utgang?
ELSIF (:new.B_BELOP > :old.B_BELOP) AND (to_char(:new.b_fristdato, 'YYYYMMDD') < to_char(:old.b_fristdato, 'YYYYMMDD')) THEN  
   raise_application_error(-20002, 'Du kan ikke sette fristdato til en tidligere dato selv om bud økes.');
END IF;
END;