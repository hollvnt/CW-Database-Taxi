use teeest;

GO
ALTER procedure [dbo].[ActiveOrder]
@id int
as
select Date, Status, Adress1, Adress2 from Orders where Id_user = @id ;

GO
ALTER procedure [dbo].[AddDriver]
as
insert into Drivers(login_driver, password_driver)
select  login_user, password_user from register t2
where driver_status = 1 and NOT EXISTS(select * from	Drivers t1 where t1.login_driver = t2.login_user);

GO
ALTER procedure [dbo].[AddDriverId]
@id int,
@id_user int
as
update Orders set id_driver = @id;

GO
ALTER procedure [dbo].[AddHistory]
as
insert into History(Adress1, Adress2, Status, Date, id_driver, id_user)
select Adress1, Adress2, Status, Date, id_driver, id_user from Orders t2
where Status = 'Doned' or Status = 'Cancelled';

GO
ALTER PROC [dbo].[AddOrder]
@Adress1 nvarchar(100),
@Adress2 nvarchar(100),
@Category nvarchar(20),
@Price int,
@Status nvarchar(50),
@Date date,
@Id int out
as
INSERT INTO Orders( Adress1, Adress2, Category, Price,Date, id_user, Status, id_driver)
values( @Adress1,@Adress2,@Category,@Price,@Date, @Id, @Status, 0)

GO
ALTER   PROC [dbo].[AddOrders]
@Adress1 nvarchar(100),
@Adress2 nvarchar(100),
@Category nvarchar(20),
@Price int,
@Date date,
@Id int out,
@Id_driver int
as
INSERT INTO Orders( Adress1, Adress2, Category, Price,Date, Id_user, id_driver)
values( @Adress1,@Adress2,@Category,@Price,@Date, @Id, @Id_driver)

GO
ALTER PROC [dbo].[AddUser]
@login_user varchar(50),
@password_user varchar(50)
as
INSERT INTO register(login_user, password_user, driver_status)
values(@login_user, @password_user, 0)

GO
ALTER procedure [dbo].[CancelledOrder]
@Adress1 nvarchar(max)
as
update Orders set Status = 'Cancelled' where Adress1 = @Adress1;

GO
ALTER PROCEDURE [dbo].[CheckUser] 
@login_user varchar(50),
@password_user varchar(50)
as
set nocount on;	
select id_user, login_user from register

GO
ALTER PROC [dbo].[CreateDriver]
@login_driver varchar(50),
@password_driver varchar(50)
as
INSERT INTO Drivers(login_driver, password_driver)
values(@login_driver, @password_driver)

GO
ALTER proc [dbo].[CreateOrder]
@Adress1 nvarchar(100),
@Adress2 nvarchar(100),
@Category nvarchar(20),
@Price int,
@Status nvarchar(50),
@Date date,
@IdUser int,
@IdDriver int
as
INSERT INTO Orders( Adress1, Adress2, Category, Price,Date, id_user, id_driver, Status)
values( @Adress1,@Adress2,@Category,@Price,@Date, @IdUser, @IdDriver, @Status)

GO
ALTER proc [dbo].[DeleteDriver]
@id int
as
delete from Drivers where id_driver = @id ;

GO
ALTER proc [dbo].[DeleteOrder]

@id int
as
delete from Orders where OrderID = @id ;

GO
ALTER procedure [dbo].[DeleteUser]
@id int
as 
delete from register where id_user = @id;

GO
ALTER procedure [dbo].[DonedOrder]
@Adress1 nvarchar(max)
as
update Orders set Status = 'Doned' where Adress1 = @Adress1;

GO
ALTER procedure [dbo].[HaveAdmins]
@admin_login nvarchar(50),
@admin_password  nvarchar(50)
as 
select * from Administrators where admin_login = @admin_login  and admin_password = @admin_password ;

GO
ALTER procedure [dbo].[HaveDrivers]
@login_driver nvarchar(50),
@password_driver nvarchar(50)
as
select id_driver, login_driver, password_driver from Drivers where login_driver= @login_driver and password_driver= @password_driver;

GO
ALTER procedure [dbo].[HaveUsers]
@login_user nvarchar(50),
@password_user nvarchar(50)
as 
begin
select id_user, login_user, password_user from register where login_user= @login_user and password_user= @password_user;
End;

GO
ALTER procedure [dbo].[OutAllCurrentOrders]
@id int
as
select Status, Adress1, Adress2, Date from Orders WHERE id_driver = @id and Status = 'InProcess'; 

GO
ALTER procedure [dbo].[OutAllUsers]
as
select Status, Adress1, Adress2, Date from Orders WHERE Status = 'Active' 

GO
ALTER procedure [dbo].[OutHistoryDrivers]
@id int
as
select Status, Adress1, Adress2, Date from History WHERE id_driver = @id; 

GO
ALTER procedure [dbo].[OutHistoryUsers]
@id int
as
select Status, Adress1, Adress2, Date from History WHERE id_user = @id; 

GO
ALTER procedure [dbo].[OutOrders]
@login_user int
as
select Status, Adress1, Adress2, Date from Orders WHERE Status = 'Active' and id_user = @login_user;

GO
ALTER proc [dbo].[Refresh]
as
select id_user, login_user, password_user, driver_status from register order by id_user;

GO
ALTER proc [dbo].[RefreshDriver]
as
select * from Drivers order by id_driver;

GO
ALTER proc [dbo].[RefreshOrders]
as
select * from Orders order by OrderID;

GO
ALTER procedure [dbo].[ReturnDriverId]
@driver_login nvarchar(50),
@Id int out
as
select @Id = id_driver from Drivers where login_driver = @driver_login;

GO
ALTER procedure [dbo].[ReturnID]
@user_login nvarchar(50),
@Id int out
as
select @Id = id_user  from register where login_user = @user_login;

GO
ALTER procedure [dbo].[Search]
@search nvarchar(50)
as 
select * from register where concat(id_user, login_user) like '%' + @search + '%';

GO
ALTER proc [dbo].[SearchDriver]
@search nvarchar(50)
as
select * from Drivers where concat(id_driver, login_driver) like '%' + @search + '%';

GO
ALTER proc [dbo].[SearchOrder]
@search nvarchar(50)
as
select Status, Adress1, Adress2, Date from Orders where concat(Status, Adress1, Adress2, Date) like '%' + @search + '%';

GO
ALTER proc [dbo].[SearchOrderForAdmin]
@search nvarchar(50)
as
select * from Orders where concat(Adress1, Adress2, Date, Status, Category, Price, id_driver, id_user) like '%' + @search + '%';

GO
ALTER procedure [dbo].[SearchUser]
@search nvarchar(50)
as 
select * from register where concat(id_user, login_user) like '%' + @search + '%';

GO
ALTER proc [dbo].[UpdateDriver]
@id int,
@login_driver nvarchar(50),
@password_driver nvarchar(50)
as
update Drivers set login_driver = @login_driver, password_driver = @password_driver where id_driver = @id;


GO
ALTER procedure [dbo].[UpdateOrderInProcess]
@Adress1 nvarchar(max)
as
Update Orders set Status = 'InProcess' where Adress1 = @Adress1;

GO
ALTER proc [dbo].[UpdateOrders]
@id int,
@Adress1 nvarchar(100),
@Adress2 nvarchar(100),
@Category nvarchar(20),
@Price int,
@Status nvarchar(50),
@Date date,
@IdUser int,
@IdDriver int
as
update Orders set Adress1 = @Adress1, Adress2 = @Adress2, Category = @Category, Price =@Price, Status = @Status,
Date = @Date, id_user = @IdUser, id_driver = @IdDriver where OrderID = @id;

GO
ALTER procedure [dbo].[UpdateUser]
@id int,
@login_user nvarchar(50),
@password_user nvarchar(50),
@driver_status int
as 
update register set login_user = @login_user, password_user = @password_user, driver_status = @driver_status where id_user = @id;

