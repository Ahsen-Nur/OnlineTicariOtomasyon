USE MVCTicariOtomasyon;
--------------------------------------------------------------

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

SELECT * FROM KategoriOzellikler;

SELECT * FROM UrunOzellikler;

SELECT * FROM Carilers;

DELETE FROM Carilers
WHERE CariMail = 'aysekaya@hotmail.com'

DELETE FROM SatisHarekets
WHERE CariId NOT IN (SELECT CariId FROM Carilers)

DELETE FROM UrunYorumlar
WHERE CariId NOT IN (SELECT CariId FROM Carilers)


--------------------------------------------------------------

--Ürün Özellikleri Ekleme
INSERT INTO UrunOzellikler (OzellikAd)
VALUES ('Renk');

INSERT INTO UrunOzellikler (OzellikAd)
VALUES ('Numara');

INSERT INTO UrunOzellikler (OzellikAd)
VALUES ('Beden');

INSERT INTO UrunOzellikler (OzellikAd)
VALUES ('Yaş Aralığı');

INSERT INTO UrunOzellikler (OzellikAd)
VALUES ('Enerji Sınıfı');

--------------------------------------------------------------

--Kategori-Ozellik Eşleştirme
INSERT INTO KategoriOzellikler (KategoriId, OzellikId)
VALUES
(1, 1),  -- Renk
(1, 5);  -- Enerji Sinifi

INSERT INTO KategoriOzellikler (KategoriId, OzellikId)
VALUES
(2, 1),  -- Renk
(2, 3);  -- Beden

DELETE FROM KategoriOzellikler
WHERE KategoriId = 2 AND OzellikId = 3;


INSERT INTO KategoriOzellikler (KategoriId, OzellikId)
VALUES
(3, 1);  -- Renk

INSERT INTO KategoriOzellikler (KategoriId, OzellikId)
VALUES
(4, 1);  -- Renk

INSERT INTO KategoriOzellikler (KategoriId, OzellikId)
VALUES
(2002, 4);  -- Yas Araligi

SELECT 
    k.KategoriAd,
    o.OzellikAd
FROM KategoriOzellikler ko
JOIN Kategoris k ON ko.KategoriId = k.KategoriId
JOIN UrunOzellikler o ON ko.OzellikId = o.OzellikId
ORDER BY k.KategoriAd;

----------------------------------------------------------------

SELECT * FROM Admins;

INSERT INTO Admins (KullaniciAd, Sifre, Yetki)
VALUES ('admin', '1234', 'A');


SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Carilers';
