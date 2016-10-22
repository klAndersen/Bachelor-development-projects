alter table BIL add constraint fkEier foreign key (B_E_NR) references EIER (E_NR) on delete cascade; 
--cascade gjør at om eier slettes, så slettes eiers biler også
--valgte denne løsningen for å unngå at det blir biler uten eiere