use BlogSystemData
create table Content(
ContentId INT NOT NULL,
ContentText varchar(max) NULL,
ContentImage varchar(max) NULL,
ContentDate datetime NULL,
PRIMARY KEY(ContentId))

create table Comment(
ContentId int not null,
AudienceName varchar(45) null,
AudienceMail varchar(max) null,
AudienceMsg varchar(300) null,
CommentDate datetime null
PRIMARY KEY(ContentId),
FOREIGN KEY(ContentId) REFERENCES Content(ContentId))

create table Category(
CategoryId int not null,
CategoryName varchar(45) null,
CategoryDesc varchar(45) null,
CategoryImage varchar(max) null,
PRIMARY KEY(CategoryId))

create table Author(
AuthorId int not null,
AuthorName varchar(45) not null,
AuthorLname varchar(50) not null,
AuthorMail varchar(max) not null,
Username varchar(max) not null,
Psw varchar(max) not null,
PRIMARY KEY(AuthorId))

create table CategoryContent(
CategoryId int not null,
ContentId int not null,
PRIMARY KEY(CategoryId,ContentId),
FOREIGN KEY(CategoryId) REFERENCES Category(CategoryId),
FOREIGN KEY(ContentId) REFERENCES Content(ContentId))

create table AuthorContent(
AuthorId int not null,
ContentId int not null,
PRIMARY KEY(AuthorId,ContentId),
FOREIGN KEY(AuthorId) REFERENCES Author(AuthorId),
FOREIGN KEY(ContentId) REFERENCES Content(ContentId))

