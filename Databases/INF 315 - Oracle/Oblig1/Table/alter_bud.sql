--kombinert primærnøkkel
alter table bud_Tbl add constraint comb_pk_bud primary key (b_k_nr, b_e_nr);
--fremmednøkler
alter table bud_Tbl add constraint fk_b_enr foreign key (b_e_nr) references eiendom_tbl(e_nr);
alter table bud_Tbl add constraint fk_b_knr foreign key (b_k_nr) references kjoper_tbl (k_nr); 
--check
alter table bud_Tbl add constraint chk_belop check (b_belop > 0);
alter table bud_tbl add constraint chk_dato check (b_gittdato <= b_fristdato);