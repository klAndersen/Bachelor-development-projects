--droppper "gammel" indeks
drop index b_�r_indeks;
--Skaper en indeks
create index b_�r_indeks on bil(b_�r desc);
--Henter ut info basert p� indeks
Select * from bil
order by b_�r desc;
--I forhold til hva jeg har lest, lages indeksen ut ifra innholdet i parantesen.
--For � benytte seg av indeksen, m� man sortere resultatet etter samme parameter
--gitt under skaping/oppretting av selve indeksen.