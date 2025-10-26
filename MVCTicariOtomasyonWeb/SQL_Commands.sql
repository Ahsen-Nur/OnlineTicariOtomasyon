USE MVCTicariOtomasyon;
SELECT * FROM Kategoris;

DELETE FROM __EFMigrationsHistory;

USE MVCTicariOtomasyon;
DELETE FROM Kategoris;


SELECT name FROM sys.databases;


SELECT * FROM INFORMATION_SCHEMA.TABLES;


IF OBJECT_ID(N'__EFMigrationsHistory', N'U') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] NVARCHAR(150) NOT NULL PRIMARY KEY,
        [ProductVersion] NVARCHAR(32) NOT NULL
    );
END;


DROP TABLE IF EXISTS Admins;


SELECT * FROM __EFMigrationsHistory;


USE master;
DROP DATABASE IF EXISTS MVCTicariOtomasyon;


USE master;
ALTER DATABASE MVCTicariOtomasyon SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
DROP DATABASE MVCTicariOtomasyon;



SELECT name FROM sys.databases;

USE MVCTicariOtomasyon;
SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES;



SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES;


USE MVCTicariOtomasyon;
SELECT * FROM Uruns;



USE MVCTicariOtomasyon;
GO

ALTER TABLE Uruns
ADD CONSTRAINT FK_Uruns_Kategoris_KategoriId
FOREIGN KEY (KategoriId)
REFERENCES Kategoris(KategoriId)
ON DELETE NO ACTION
ON UPDATE NO ACTION;
GO


EXEC sp_helpconstraint 'Uruns';
GO
