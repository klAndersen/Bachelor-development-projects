alter table lag_tbl add constraint chk_ForsteTegn 
check (regexp_like (lagkode, '^([A-Z]|�|�|�{1})[0-9]{2}$'));