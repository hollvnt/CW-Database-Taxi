use teeest;
-----ШИФРОВАНИЕ----

create master key encryption by password = '123123';
CREATE CERTIFICATE Certificate_test WITH SUBJECT = 'test';

GO

SELECT name CertName, 
    certificate_id CertID, 
    pvt_key_encryption_type_desc EncryptType, 
    issuer_name Issuer
FROM sys.certificates;

CREATE SYMMETRIC KEY SymKey_test WITH ALGORITHM = AES_256 ENCRYPTION BY CERTIFICATE Certificate_test;

SELECT name KeyName, 
    symmetric_key_id KeyID, 
    key_length KeyLength, 
    algorithm_desc KeyAlgorithm
FROM sys.symmetric_keys;

ALTER TABLE register
ADD Password_encrypt varbinary(MAX)
OPEN SYMMETRIC KEY SymKey_test
        DECRYPTION BY CERTIFICATE Certificate_test;		
UPDATE register
        SET Password_encrypt = EncryptByKey (Key_GUID('SymKey_test'), password_user)
        FROM register;
        GO
		CLOSE SYMMETRIC KEY SymKey_test;
            GO
select * from register;
exec Refresh;


OPEN SYMMETRIC KEY SymKey_test
        DECRYPTION BY CERTIFICATE Certificate_test;
		SELECT id_user, login_user, password_user, driver_status, Password_encrypt AS 'Encrypted data',
            CONVERT(nvarchar(50), DecryptByKey(Password_encrypt)) AS 'Decrypted password'
            FROM register;



-----МАСКИРОВАНИЕ-----
GRANT SELECT ON Drivers TO [MaskingTestUser];  

select * from Drivers
Alter table Drivers
Alter column password_driver nvarchar(50) MASKED WITH (FUNCTION='default()')
execute as user = 'MaskingTestUser';
select * from Drivers;
revert;

GRANT UNMASK TO MaskingTestUser;
GO
EXECUTE AS USER = 'MaskingTestUser';  
SELECT * FROM Drivers;  
REVERT; 
GO
REVOKE UNMASK TO MaskingTestUser;
EXECUTE AS USER = 'MaskingTestUser';  
SELECT * FROM Drivers;  
REVERT; 


--Демонстрация

EXECUTE AS USER = 'MaskingTestUser';  
SELECT * FROM Drivers;  
REVERT; 
GO

--без маски
REVERT;
SELECT * FROM Drivers; 