create or replace type body passiv_type 
--denne delen kommer pga man skal override den abstrakte tostring
is
overriding member function tostring
return varchar2
--her kommer hva selve funksjonen tostring skal utføre
is 
begin
return 'Passiv: ' || medlemsnr || ' - ' || medlemsnavn || ' (rabatt ' || rabatt_prosent || ')';
exception when others then
return 'Ingen registrert';
End tostring;
end;