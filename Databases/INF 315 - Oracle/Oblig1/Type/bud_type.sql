create or replace type bud_type as object (
b_k_nr integer,
b_e_nr integer(6),
b_belop integer,
b_gittdato date,
b_fristdato date,
member function tostring return varchar2,
member function hastesak return varchar2
);