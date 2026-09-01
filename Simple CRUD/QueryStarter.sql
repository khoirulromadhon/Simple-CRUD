CREATE DATABASE simple_crud;
GO

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

INSERT INTO Product (Name, Description, Price, CreatedAt)
VALUES
('Laptop ASUS VivoBook 15', 'Laptop untuk kebutuhan kerja dan belajar sehari-hari', 8500000.00, GETDATE()),
('Mouse Logitech M331', 'Mouse wireless silent click dengan baterai tahan lama', 245000.00, GETDATE()),
('Keyboard Mechanical Red Switch', 'Keyboard mechanical dengan RGB backlight', 675000.00, GETDATE()),
('Monitor LG 24 Inch', 'Monitor Full HD IPS 24 inci untuk produktivitas', 2250000.00, GETDATE()),
('Printer Epson L3250', 'Printer multifungsi dengan fitur WiFi', 3150000.00, GETDATE()),
('Flashdisk Sandisk 64GB', 'Media penyimpanan USB 3.0 berkapasitas 64GB', 125000.00, GETDATE()),
('Headset HyperX Cloud Stinger', 'Headset gaming dengan kualitas suara jernih', 890000.00, GETDATE()),
('Webcam Logitech C920', 'Webcam Full HD untuk meeting dan streaming', 1350000.00, GETDATE()),
('SSD Samsung 970 EVO 500GB', 'SSD NVMe berkecepatan tinggi untuk PC dan laptop', 1125000.00, GETDATE()),
('Router TP-Link Archer C6', 'Router dual-band untuk jaringan rumah dan kantor', 725000.00, GETDATE());

CREATE INDEX product_id_idx on Product (Id);
CREATE INDEX product_name_idx on Product (Name);

CREATE OR ALTER PROCEDURE GetProducts
    @Keyword VARCHAR(100) = NULL,
    @MinPrice DECIMAL(18,2) = NULL,
    @MaxPrice DECIMAL(18,2) = NULL,
    @CursorId INT = 0
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 5
        Id,
        Name,
        Description,
        Price,
        CreatedAt
    FROM Product
    WHERE Id > @CursorId
      AND (
            @Keyword IS NULL
            OR @Keyword = ''
            OR LOWER(Name) LIKE LOWER('%' + @Keyword + '%')
          )
      AND (
            @MinPrice IS NULL
            OR Price >= @MinPrice
          )
      AND (
            @MaxPrice IS NULL
            OR Price <= @MaxPrice
          )
    ORDER BY Id ASC;
END;
