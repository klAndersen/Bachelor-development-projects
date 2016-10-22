alter table medlem_Tbl add constraint pk_medlemsnr primary key (medlemsnr);
ALTER TABLE medlem_Tbl MODIFY (medlemsnavn NOT NULL);
alter table medlem_Tbl add constraint chk_medlemsnr check (medlemsnr >= 1000);