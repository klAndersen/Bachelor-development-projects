--Lag et view med alle lagene og lagmedlemmer tilknyttet laget
create force view lag_antall_view as 
select distinct lag.antall() as Antall, lag.tostring() as Lag from lag_tbl lag, lagmedlem_tbl lm 
where lm.lagkode = lag.lagkode;