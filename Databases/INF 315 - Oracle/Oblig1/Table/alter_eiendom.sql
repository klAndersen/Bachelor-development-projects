alter table eiendom_tbl add constraint pk_enr primary key (e_nr);
alter table eiendom_tbl add constraint fk_typeid foreign key (e_typeid) references eiendomtype_tbl(type_id);
alter table eiendom_tbl add constraint fk_snr foreign key (e_s_nr) references selger_tbl(s_nr);
alter table eiendom_tbl modify (e_adresse not null);
--check
alter table eiendom_tbl add constraint chk_verdi check (e_verditakst > 0);
alter table eiendom_tbl add constraint chk_salgspris check (e_salgspris >= 0);
alter table eiendom_tbl add constraint chk_enr check (e_nr >= 100000);
alter table eiendom_tbl add constraint chk_postnr check (
regexp_like(e_postnr, '^[0-9]{4}$')
);