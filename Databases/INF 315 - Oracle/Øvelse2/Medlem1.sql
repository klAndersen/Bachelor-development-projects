create or replace type medlem_type as object (
medlemsnr number(8),
medlemsnavn varchar2(20),
member function tostring return varchar2
)
not instantiable -- abstrakt objekttype
not final; -- arv er tillatt