create force view lagmedlemmer_view
as 
select lag.*, med.* 
from lag_tbl lag, medlem_tbl med, lagmedlem_tbl lm
where lm.medlemnr = med.medlemsnr and lm.lagkode = lag.lagkode;