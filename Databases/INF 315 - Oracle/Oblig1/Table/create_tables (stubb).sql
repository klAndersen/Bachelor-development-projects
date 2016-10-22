create table selger_tbl of selger_type;
create table kjoper_tbl of kjoper_type;
create table eiendom_tbl of eiendom_type;
create table bud_tbl of bud_type;
--mellom tabell for interessent
create table interessent_tbl(
i_k_nr integer,
i_e_nr integer
);
--tabell for forskjellige eiendomstyper
create table eiendomtype_Tbl(
type_id integer,
type_navn varchar(15)
);