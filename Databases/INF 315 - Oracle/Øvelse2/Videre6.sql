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
--variabeler for radene i tabellen
lag_rad lag_tbl%ROWTYPE;
medlem_rad medlem_tbl%ROWTYPE;
--stringer som skal returnere listen
lagstring varchar(255);
medlemstring varchar(255); 
--deklarer cursor's select setning direkte
cursor lag_peker is select * from lag_tbl; 
cursor medlem_peker is select * from medlem_tbl;  
begin 
--løkke for opplisting av lag
for lag_rad in lag_peker loop
lagstring := lag_rad;
end loop;
--løkke for opplisting av medlemmer
for medlem_rad in medlem_peker loop
medlemstring := medlem_rad;
end loop;
return lagstring || ' # ' || medlemstring;
exception when others then 
return 'Ingen registrert';
end lagliste;
End;