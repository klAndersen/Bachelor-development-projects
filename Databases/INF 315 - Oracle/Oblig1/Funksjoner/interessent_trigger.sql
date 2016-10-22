create or replace
TRIGGER INTERESSENT_TRIGGER
AFTER INSERT ON BUD_TBL 
for each row
DECLARE
antall integer;
BEGIN
--hent ut antallet som er registrert som interessenter med gitt kjoper - og eiendomsnr
Select count(*) into antall 
from interessent_tbl
where i_k_nr = :new.b_k_nr and i_e_nr = :new.b_e_nr;
--er budgiver registrert som interessent?
if antall = 0 then
  Insert into interessent_tbl(i_k_nr, i_e_nr) values (:new.b_k_nr, :new.b_e_nr);
end if;
END;