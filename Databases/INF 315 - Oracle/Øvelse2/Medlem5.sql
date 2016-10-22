--insert into medlem_Tbl values (passiv_type(1000,'Thor Iversen', 10));
--insert into medlem_tbl values (aktiv_type(1001, 'Knut W. Hansson', 21));
--select * from medlem_tbl;
select med.tostring(), med.* from medlem_tbl med;