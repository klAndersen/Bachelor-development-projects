--alter table lagmedlem_tbl add constraint fk_lagkode foreign key (lagkode) references lag_tbl(lagkode);

--alter table lagmedlem_tbl add constraint chk_kaptein check (er_kaptein = 'J' or er_kaptein = 'N');

--insert into lagmedlem_tbl values (1000, 'G12', 'N');

--Må legge inn kombinert primærnøkkel for lagmedlem_tbl (fjerne "gammel" primærnøkkel)