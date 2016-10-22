create or replace view budprosent_view 
as 
--lister opp budprosent fra høyest til lavest
select distinct e.budprosent() as Budprosent, e.e_nr
from eiendom_tbl e 
order by e.budprosent() desc;

create or replace view int_liste_view
as
--lister opp interessenter på eiendom(mer); stigende eiendomsnr
select e.interessentliste() as Interessentliste
from eiendom_tbl e 
order by e.e_nr asc;


create or replace view hastesak_view 
as
--lister opp hastesakene, viser de som haster først
Select b.hastesak() as Status, b.* 
from bud_tbl b 
order by b.hastesak() asc;

create or replace view budliste_view
as
--lister opp budgivere, sortert på eiendomsnr
select e.budliste() as Budliste
from eiendom_tbl e
order by e_nr;