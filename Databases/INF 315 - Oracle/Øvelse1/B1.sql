create table eier (
e_nr integer,
e_navn varchar2(20),
e_telefon integer(8)
);

create table bil (
b_nr char(8),
b_merke varchar2(10),
b_år integer,
b_bilde blob,
b_e_nr integer
);