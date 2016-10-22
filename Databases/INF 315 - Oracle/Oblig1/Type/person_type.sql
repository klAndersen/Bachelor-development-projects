create or replace type person_type under selger_type (
p_fnr integer(11),
overriding member function tostring return varchar2
)
instantiable 
final;