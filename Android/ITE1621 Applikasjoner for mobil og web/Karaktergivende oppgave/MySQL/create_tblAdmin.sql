create table tblAdmin (
	adminID int NOT NULL AUTO_INCREMENT,
	brukerNavn varchar(50) not null,
	pwd varchar(255) not null,
	adgangsNivaa int NOT NULL,
	PRIMARY KEY (adminID)
);