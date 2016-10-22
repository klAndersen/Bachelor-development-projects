Create or replace type lag_type as object (
lagkode char(3),
lagnavn varchar2(30),
member function tostring return varchar2,
member function antall return integer,
member function lagliste return varchar2
);