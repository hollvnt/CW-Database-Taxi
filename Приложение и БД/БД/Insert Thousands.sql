use teeest;

GO
create or ALTER procedure InsertThousands as
begin
declare @i int;
set @i = 1;
while @i <= 100000
begin
insert into register(login_user, password_user, driver_status )
values ('test', cast(FLOOR(1000000*RAND()) as nvarchar(50)), 0);
set @i = @i + 1;
end;
end;