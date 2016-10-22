alter table lag_tbl add constraint pk_lagkode primary key (lagkode);
alter table lag_tbl add constraint ak_lagnavn unique (lagnavn);
ALTER TABLE lag_tbl MODIFY (lagnavn NOT NULL);
alter table lag_tbl add constraint chk_lagkode check (lagkode like 'P%' or lagkode like 'S%' or lagkode like 'G%');