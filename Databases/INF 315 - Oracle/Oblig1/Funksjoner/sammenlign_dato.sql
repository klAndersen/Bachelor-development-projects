create or replace
function sammenlign_dato(ny_dato in date) return number 
is 
dagens_dato varchar2(10); --variabel for dagens dato
mottatt_dato varchar2(10); --variabel for konvertering av oversendt dato (ny_dato)
resultat number;
begin
--omgjør datoen som ble oversendt til streng (uten klokkeslett)
mottatt_dato := to_char(ny_dato, 'YYYYMMDD');
--henter ut dagens dato (uten klokkeslett)
select to_char(sysdate, 'YYYYMMDD') into dagens_dato from dual;
--er dagens dato før oversendt dato?
if dagens_dato < mottatt_dato then
  resultat := 2;
--er dagens dato lik oversendt dato? 
elsif dagens_dato = mottatt_dato then
  resultat := 1;
else --oversendt dato er før dagens dato
  resultat := 0;
end if;
return resultat;
exception when others then 
return 'Feil oppsto ved sammenligning av dato';
end sammenlign_dato;