create or replace type body person_type is overriding 
member function tostring return varchar2 
is
begin
return null;
end tostring;
end;