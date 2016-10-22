--droppper "gammel" indeks
drop index b_år_indeks;
--Skaper en indeks
create index b_år_indeks on bil(b_år desc);
--Henter ut info basert på indeks
Select * from bil
order by b_år desc;
--I forhold til hva jeg har lest, lages indeksen ut ifra innholdet i parantesen.
--For å benytte seg av indeksen, må man sortere resultatet etter samme parameter
--gitt under skaping/oppretting av selve indeksen.