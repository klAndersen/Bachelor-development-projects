create or replace type eiendom_type as object (
e_nr integer(6),
e_typeid integer, 
e_adresse varchar2(20),
e_postnr integer(4),
e_verditakst integer,
e_salgspris integer,
e_s_nr integer,
member function tostring return varchar2,
member function interessentliste return varchar2,
member function budliste return varchar2,
member function budprosent return integer
);