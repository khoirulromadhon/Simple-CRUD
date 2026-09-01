create table Product(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Name Varchar(51) not null unique,
	Description Varchar(256),
	Price Decimal(18,2) not null,
	CreatedAt Datetime not null
);

create table Users(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	username varchar(100) not null,
	password varchar(256) not null
);