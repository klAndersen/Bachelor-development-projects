create or replace
type kjoper_type as object (
k_nr integer,
k_fornavn varchar2(20),
k_enavn varchar2(30),
k_tlf varchar2(20), --utenlandske nummer kan være lange
member function tostring return varchar2,
member function bud return varchar2
);