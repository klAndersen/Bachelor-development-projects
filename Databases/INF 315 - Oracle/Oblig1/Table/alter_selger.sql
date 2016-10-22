alter table selger_tbl add constraint pk_selgernr primary key (s_nr);
--modify
alter table selger_tbl modify (s_fornavn not null);
alter table selger_tbl modify (s_enavn not null);
alter table selger_tbl modify (s_tlf not null);
alter table selger_tbl add constraint chk_stlf check (
--samme prinsipp her som på kjoper_tbl
regexp_like(s_tlf, '^[1-9]{1}[0-9]{7}$') or 
regexp_like(s_tlf,  '^[\+](([0-9]|[a-zA-Z\-])+)$')
);