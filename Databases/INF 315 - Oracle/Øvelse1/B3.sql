alter table BIL add constraint fkEier foreign key (B_E_NR) references EIER (E_NR) on delete cascade; 
--cascade gj�r at om eier slettes, s� slettes eiers biler ogs�
--valgte denne l�sningen for � unng� at det blir biler uten eiere