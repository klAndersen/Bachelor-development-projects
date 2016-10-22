create or replace
type body lag_type as  
member function tostring return varchar2
is 
begin
return lagkode || ' - ' || lagnavn;
exception when others then
return 'Ingen registrert';
End tostring;

member function antall return integer
is
antDeltakere integer;
begin
select count(*) into antDeltakere from lagmedlem_Tbl lm, lag_tbl lag 
where lag.lagkode = lm.lagkode;
return antDeltakere;
exception when others then
return 'Ingen deltakere registrert';
end antall;

member function lagliste return varchar2
is
begin
return null; --ikke definert, derfor returneres null
end lagliste;
End;