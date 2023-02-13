use teeest;

create table register(
id_user int identity(1, 1) not null primary key,
login_user varchar(50) not null,
password_user varchar(50) not null, 
driver_status int 
);

create table Orders(
OrderID int identity(1,1) not null primary key,
Adress1 nvarchar(100),
Adress2 nvarchar(100),
Category nvarchar(20),
Price int,
Date date,
Status nvarchar(50),
id_user int not null foreign key references register(id_user),
id_driver int not null  foreign key references Drivers(id_driver) )

create table Administrators(
admin_id int identity(1,1) not null,
admin_login nvarchar(50),
admin_password nvarchar(50)
);

create table Drivers(
id_driver int identity(1, 1) primary key not null,
login_driver varchar(50) not null,
password_driver varchar(50) not null
);

Create table History (
HistoryID int identity (1,1) not null primary key,
Adress1 nvarchar(100),
Adress2 nvarchar(100),
Status nvarchar(50),
Date date,
id_driver int,
id_user int
);