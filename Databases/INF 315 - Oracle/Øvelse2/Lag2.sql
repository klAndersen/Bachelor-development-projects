create or replace type body lag_type as  
member function tostring return varchar2
is 
begin
return lagkode || ' - ' || lagnavn;
exception when others then
return 'Ingen registrert';
End tostring;
member function antall return integer
is
begin
return null; --ikke definert, derfor returneres null
end antall;
member function lagliste return varchar2
is
begin
return null; --ikke definert, derfor returneres null
end lagliste;
End;