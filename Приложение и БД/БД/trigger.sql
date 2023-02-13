
go
CREATE TRIGGER Delete_space
ON  register   
after update, insert 
as
begin
declare @login nvarchar(50)
begin 
insert into register (login_user) values (trim(@login));
update register set login_user = trim(@login);
end;
end;