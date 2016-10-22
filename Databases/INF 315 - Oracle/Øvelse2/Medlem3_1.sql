create or replace
type body aktiv_type 
--denne delen kommer pga man skal override den abstrakte tostring
is
overriding member function tostring
return varchar2
--her kommer hva selve funksjonen tostring skal utføre
is 
begin
return 'Aktiv: ' || medlemsnr || ' - ' || medlemsnavn || ' (skap ' || skapnr || ')';
exception when others then
return 'Ingen registrert';
End tostring;
end;