--alter table lagmedlem_tbl add constraint fk_lagkode foreign key (lagkode) references lag_tbl(lagkode);

--alter table lagmedlem_tbl add constraint chk_kaptein check (er_kaptein = 'J' or er_kaptein = 'N');

--insert into lagmedlem_tbl values (1000, 'G12', 'N');

--M� legge inn kombinert prim�rn�kkel for lagmedlem_tbl (fjerne "gammel" prim�rn�kkel)