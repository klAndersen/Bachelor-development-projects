alter table interessent_tbl add constraint comb_pk_interessent primary key (i_k_nr, i_e_nr);
alter table interessent_tbl add constraint fk_i_knr foreign key (i_k_nr) references kjoper_tbl(k_nr);
alter table interessent_tbl add constraint fk_i_enr foreign key (i_e_nr) references eiendom_tbl(e_nr);