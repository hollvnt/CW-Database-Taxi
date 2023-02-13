use teeest;


go 
create procedure ExProdToXml
as
begin
	select login_user, password_user, driver_status from register
		for xml path('USER'), root('USERS');

	EXEC master.dbo.sp_configure 'show advanced options', 1
		reconfigure with override
	EXEC master .dbo.sp_configure 'xp_cmdshell', 1
		reconfigure with override;

	declare @fileName nvarchar(100)
	declare @sqlStr varchar(1000)
	declare @sqlCmd varchar(1000)
	
	set @fileName = 'C:\Users\Lenovo\AppData\Local\Export.xml';
	set @sqlStr = 'USE teeest; select login_user, password_user, driver_status from register FOR XML PATH(''USER''), ROOT(''USERS'') '
	set @sqlCmd = 'bcp.exe "' + @sqlStr + '" queryout ' + @fileName + ' -w -T'
	EXEC xp_cmdshell @sqlCmd;
end;



exec ExProdToXml;

go
create or alter procedure XmlToProduct
as begin
DECLARE @xml XML;

SELECT @xml = CONVERT(xml, BulkColumn, 2) FROM OPENROWSET(BULK 'C:\Users\Lenovo\AppData\Local\Import.xml', SINGLE_BLOB) AS x

INSERT INTO  register(id_user, login_user, password_user, driver_status)
SELECT 
	t.x.query('id_user').value('.', 'INT'),
	t.x.query('login_user').value('.', 'varchar(50)'),
	t.x.query('password_user').value('.', 'varchar(50)') ,
	t.x.query('driver_status').value('.', 'INT')
FROM @xml.nodes('//USERS/USER') t(x)
end
SET IDENTITY_INSERT register ON;
select * from register;
exec XmlToProduct;