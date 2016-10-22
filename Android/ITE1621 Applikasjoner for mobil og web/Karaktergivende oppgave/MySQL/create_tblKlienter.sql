create table tblKlienter (
	klientID int NOT NULL AUTO_INCREMENT,
	registrationID TEXT(4000) not null,
	enhetsNavn VARCHAR(255) null,
	registrertDato DATE null,
	primary key (klientID)
);