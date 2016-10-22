create or replace
type body bud_type as 
member function tostring return varchar2 
is
budliste varchar2(2000);
begin
--oppslittet string for bedre oversikt/lesbarhet
--listen er ikke sortert, det blir opp til applikasjonsprogrammerer å legge inn
budliste := 'Kjøpernr: ' || self.b_k_nr || ' Eiendomnr: ' || self.b_e_nr
        || ' Budets beløp: ' || self.b_belop || ' Opprettet dato: ' || self.b_gittdato
        || ' Gyldig til: ' || self.b_fristdato;
return budliste;
exception when others then --noe gikk galt/en feil oppsto
return 'En feil opppsto i bud_type.tostring()';
end tostring;

member function hastesak return varchar2 
is
hast varchar2(200);
cursor peker is select sammenlign_dato(self.b_fristdato) sjekk from dual;
begin
for rad in peker loop
  if rad.sjekk = 1 then --er fristen dagens dato?
    hast := hast || 'Haster';
  else --fristen er utgått/"i fremtiden"
    hast := hast || 'Haster ikke';
  end if;
end loop;
if hast is null then --finnes ingen bud
  hast := 'Det finnes ingen bud.';
end if;
return hast;
exception when others then --noe gikk galt/en feil oppsto
return 'En feil opppsto i bud_type.hastesak()';
end hastesak;
end;