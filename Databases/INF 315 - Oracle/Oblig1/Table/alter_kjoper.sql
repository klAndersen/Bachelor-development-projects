alter table kjoper_tbl add constraint pk_kjoper primary key (k_nr);
alter table kjoper_tbl modify (k_fornavn not null);
alter table kjoper_tbl modify (k_enavn not null);
alter table kjoper_Tbl modify (k_tlf not null);
alter table kjoper_tbl add constraint chk_knr check (k_nr > 0);
alter table kjoper_tbl add constraint CHK_KTLF check (
/*
denne sjekker et besifret telefonnr; 
f�rste siffer m� v�re mellom 1-9 og de 7 neste m� v�re mellom 0-9
*/
regexp_like(k_tlf, '^[1-9]{1}[0-9]{7}$') or 
/*
sjekker her at f�rste tegn er + og deretter at resterende 
tegn er mellom 0-9, frittst�ende tegn  fra alfabetet(a-z) 
eller bindestrek (tegn utenom m� legges inn senere hvis behov)
*/
regexp_like(k_tlf, '^[\+](([0-9]|[a-zA-Z\-])+)$')
);