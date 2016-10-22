create or replace type aktiv_type under medlem_type (
skapnr integer,
overriding member function tostring return varchar2 --overstyrer tostring som den arver
)
instantiable -- denne skal brukes/instansieres
final; -- denne kan ikke arves