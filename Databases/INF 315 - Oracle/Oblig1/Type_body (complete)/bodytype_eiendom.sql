create or replace
type body eiendom_type as 
member function tostring return varchar2 
is
etype varchar2(100); --variabel for eiendomstypen
eliste varchar2(2000); --listen som skal returneres
begin
--henter ut eiendomstypene
select distinct et.type_navn tnavn into etype
from eiendomtype_tbl et
where self.e_typeid = et.type_id;
eliste := 'Eiendomsnr: ' || self.e_nr || ' Eiendomstype: ' || etype || '. Adresse: ' 
          || self.e_adresse || ' Postnr: ' || self.e_postnr || ' Verditakst: ' 
          || self.e_verditakst;
if self.e_salgspris = 0 then --er eiendommen solgt?
  eliste := eliste || '. Ikke solgt.';
else --eiendommen er solgt
  eliste := eliste || '. Solgt for: ' || self.e_salgspris;
end if;
return eliste;
exception when others then --noe gikk galt/en feil oppsto
return 'En feil opppsto i eiendom_type.tostring()';
end tostring;

member function interessentliste return varchar2 
is
antall integer; --variabel for antallet interesserte
etype varchar(100); --variabel for eiendomstypen
int_liste varchar2(2000); --variabel for listen som skal returneres
cursor peker is 
select k.k_nr knr, k.k_fornavn fnavn, k.k_enavn enavn, k.k_tlf tlf
from interessent_tbl it, kjoper_tbl k
where it.i_k_nr = k.k_nr and it.i_e_nr = self.e_nr;
begin
--henter ut eiendomstypene
select distinct et.type_navn into etype 
from eiendomtype_tbl et 
where self.e_typeid = et.type_id;
--henter ut antall interessenter
select count(*) into antall 
from interessent_tbl it
where self.e_nr = it.i_e_nr;
int_liste := 'Eiendomsnr: ' || self.e_nr || ' Eiendomstype: ' || etype || '. '
              || antall || ' interessenter:';
--løkke som lister opp alle interessentene
for rad in peker loop
  int_liste := int_liste || ' Interessentnr ' || rad.knr || ' ' 
              || rad.fnavn || ' ' || rad.enavn || ' Tlf: ' || rad.tlf || '.';
end loop;
return int_liste;
exception when others then --noe gikk galt/en feil oppsto
return 'En feil opppsto i eiendom_type.interessentliste()';
end interessentliste;

member function budliste return varchar2 
is
antall number; --variabel for antallet budgivere på gitt eiendom
aktiv number; --variabel for om budet fortsatt er gjeldende (frist ikke utgått)
budliste varchar2(2000); --variabel for listen som skal returneres
cursor peker is 
select k.k_nr knr, k.k_fornavn fnavn, k.k_enavn enavn, k.k_tlf tlf, b.b_belop belop, b.b_fristdato frist
from bud_tbl b, kjoper_tbl k 
where b.b_e_nr = self.e_nr and b.b_k_nr = k.k_nr
order by b.b_k_nr asc;
begin
--henter ut antall bud til gjeldende eiendom
select count(*) into antall 
from bud_tbl b, kjoper_tbl k 
where b.b_k_nr = k.k_nr and b.b_e_nr = self.e_nr;
budliste := 'På eiendom ' || self.e_nr || ' er det ' || antall || ' budgivere:';
for rad in peker loop
  select sammenlign_dato(rad.frist) into aktiv from dual; --henter ut budets "gyldighet"
  if aktiv >= 1 then --er budet aktivt
    --forutsetter at (*) skal komme før budgivers informasjon
    budliste := budliste || ' *';
  end if;
  budliste := budliste || ' Budgiverid: ' || rad.knr || ' ' || rad.fnavn || ' ' 
              || rad.enavn || '. Bud gyldig til ' || rad.frist || '.';
end loop;
return budliste;
exception when others then --noe gikk galt/en feil oppsto
return 'En feil opppsto i eiendom_type.budliste()';
end budliste ;

member function budprosent return integer 
is
bud integer;
pros_verdi integer;
begin
--henter ut høyeste beløp for gitt eiendom
select max(b.b_belop) into bud 
from bud_tbl b
where b.b_e_nr = self.e_nr;
--regner ut prosentverdien
pros_verdi := bud/self.e_verditakst * 100;
if pros_verdi is null then --har eiendommen bud?
  pros_verdi := 0; --ingen bud; sett prosent til null
end if;
return pros_verdi;
exception when others then --noe gikk galt/en feil oppsto
return 'En feil opppsto i eiendom_type.budprosent()';
end budprosent;
end;