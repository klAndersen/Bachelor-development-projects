create or replace type firma_type under selger_type (
f_orgnr integer(9),
overriding member function tostring return varchar2
)
instantiable 
final;