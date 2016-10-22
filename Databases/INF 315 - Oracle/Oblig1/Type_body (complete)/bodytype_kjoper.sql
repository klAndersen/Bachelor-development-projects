create or replace
type body kjoper_type as 
member function tostring return varchar2 
is
kliste varchar2(2000);
begin
kliste := 'Kjøpernr: ' || self.k_nr || ' Navn: ' || self.k_fornavn || ' '  
          || self.k_enavn || ' Telefon: ' || self.k_tlf;
return kliste;
exception when others then --noe gikk galt/en feil oppsto
  return 'En feil opppsto i kjoper_type.tostring()';
end tostring;

member function bud return varchar2 
is
antall number; --antallet bud kjøper har
har_bud varchar(2000); --bud som returneres
--peker som henter aktive bud:
cursor peker is 
--valgte her å kalle på eiendom.tostring() for å ikke ha en for lang sql-setning
select b.b_belop belop, b.b_gittdato gitt, b.b_fristdato frist, e.tostring() e_string
from bud_tbl b, eiendom_tbl e
where b.b_k_nr = self.k_nr and sammenlign_dato(b.b_fristdato) >= 1 
--forutsetter at det er kun usolgte eiendommer som skal listes opp
and b.b_e_nr = e.e_nr and e.e_salgspris = 0;
begin
--henter antall gjeldende/"aktive" bud (hvor eiendom er usolgt)
select count(*) into antall 
from bud_tbl b, eiendom_tbl e
where b.b_k_nr = self.k_nr and sammenlign_dato(b.b_fristdato) >= 1 
and b.b_e_nr = e.e_nr and e.e_salgspris = 0;
if antall = 0 then --har ingen "aktive" bud
  har_bud := self.k_fornavn || ' ' || self.k_enavn ||' har ingen bud.';
else --har "aktive" bud
  har_bud := self.k_fornavn || ' ' || self.k_enavn ||' -';
end if;
for rad in peker loop
  har_bud := har_bud || ' Budets beløp: ' || rad.belop || ' på ' || rad.e_string;
end loop;
return har_bud;
exception when others then --noe gikk galt/en feil oppsto
  return 'En feil opppsto i kjoper_type.bud()';
end bud;
end;