create or replace type passiv_type under medlem_type (
rabatt_prosent integer,
overriding member function tostring return varchar2 --overstyrer tostring som den arver
)
instantiable -- denne skal brukes/instansieres
final; -- denne kan ikke arves