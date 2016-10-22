create or replace type selger_type as object (
s_nr integer,
s_fornavn varchar2(20),
s_enavn varchar2(30),
s_representerer varchar2(100), --I tilfelle det er representasjon; f.eks for sameie
s_tlf varchar2(20), --utenlandske nummer kan være lange
member function tostring return varchar2
)
not instantiable
not final;