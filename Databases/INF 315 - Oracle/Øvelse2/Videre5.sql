create or replace 
function kaptein (kode in char)
return number
is
mednr number;
begin
--Hent ut medlemsnr basert på lagkoden som sendes inn (kode)
select med.medlemsnr into mednr from medlem_tbl med, lagmedlem_Tbl lm 
where lm.medlemnr = med.medlemsnr and lm.lagkode = kode and lm.er_kaptein = 'J';
return mednr;
exception when others then 
return null;
end kaptein;