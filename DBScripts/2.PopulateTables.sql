SET IDENTITY_INSERT StockItems ON
INSERT INTO StockItems (Id, RegNo, Make, Model, ModelYear, KMS, Colour, VIN, RetailPrice, CostPrice, DTCreated, DTUpdated)
VALUES
    (1, 'AB12CDE', 'Toyota', 'Corolla', 2020, 45000, 'Red', '1HGCM82633A123456', 250000, 220000, GETDATE(), GETDATE()),
    (2, 'XY34FGH', 'Honda', 'Civic', 2019, 60000, 'Blue', '2HGFA16578H123457', 230000, 200000, GETDATE(), GETDATE()),
    (3, 'LM56JKL', 'Ford', 'Focus', 2021, 30000, 'White', '3FAHP0HA8CR123458', 270000, 240000, GETDATE(), GETDATE()),
    (4, 'QR78MNO', 'BMW', '320i', 2018, 75000, 'Black', 'WBA8E1G50JNU12345', 350000, 310000, GETDATE(), GETDATE()),
    (5, 'ST90PQR', 'Mercedes', 'C-Class', 2022, 15000, 'Silver', 'WDDGF8AB4EA123459', 500000, 450000, GETDATE(), GETDATE()),
    (6, 'UV12STU', 'Volkswagen', 'Golf', 2020, 40000, 'Grey', 'WVWRF9AU7LW123450', 280000, 250000, GETDATE(), GETDATE()),
    (7, 'WX34VWX', 'Audi', 'A4', 2017, 85000, 'White', 'WAUZFCF52KA123451', 370000, 330000, GETDATE(), GETDATE()),
    (8, 'YZ56YZA', 'Nissan', 'X-Trail', 2021, 25000, 'Green', 'JN8AZ2KR9MT123452', 320000, 290000, GETDATE(), GETDATE()),
    (9, 'BC78BCD', 'Hyundai', 'Tucson', 2019, 50000, 'Blue', 'KM8J3CA46HU123453', 290000, 260000, GETDATE(), GETDATE()),
    (10, 'DE90DEF', 'Mazda', 'CX-5', 2023, 10000, 'Red', 'JM3KFBDM4P0123454', 450000, 400000, GETDATE(), GETDATE());
SET IDENTITY_INSERT StockItems OFF

SET IDENTITY_INSERT StockAccessories ON
	INSERT INTO StockAccessories (Id, Description, StockItemId)
VALUES
    (1, 'Alloy Wheels', 1),
    (2, 'Leather Seats', 2),
    (3, 'Sunroof', 3),
    (4, 'Navigation System', 4),
    (5, 'Rearview Camera', 5),
    (6, 'Bluetooth Audio', 6),
    (7, 'Heated Seats', 7),
    (8, 'Parking Sensors', 8),
    (9, 'Fog Lights', 9),
    (10, 'Roof Rack', 10),
    (11, 'LED Headlights', 1),
    (12, 'Sport Package', 2),
    (13, 'Adaptive Cruise Control', 3),
    (14, 'Lane Departure Warning', 4),
    (15, 'Wireless Charging', 5),
    (16, 'Keyless Entry', 6),
    (17, 'Premium Sound System', 7),
    (18, 'Blind Spot Monitoring', 8),
    (19, 'Apple CarPlay', 9),
    (20, 'Android Auto', 10);
SET IDENTITY_INSERT StockAccessories OFF

DECLARE @BaseDirectory NVARCHAR(255) = 'G:\Development\CMSDemo\DemoImages\';--PLEASE UPDATE THE FOLDER HERE TO WHERE YOU HAVE COPIED IT , E.G. 'C:\Users\Robbie\Desktop\DemoImages\';

DECLARE @SQL NVARCHAR(MAX);

SET IDENTITY_INSERT Images ON
SET @SQL = '
INSERT INTO Images (Id, Name, ImageBinary, StockItemId)
VALUES
    (1, ''ToyotaCorolla.jpg'', (SELECT * FROM OPENROWSET(BULK ''' + @BaseDirectory + 'ToyotaCorolla.jpg'', SINGLE_BLOB) AS ImageFile), 1),
    (2, ''HondaCivic.jpg'', (SELECT * FROM OPENROWSET(BULK ''' + @BaseDirectory + 'HondaCivic.jpg'', SINGLE_BLOB) AS ImageFile), 2),
    (3, ''FordFocus.jpg'', (SELECT * FROM OPENROWSET(BULK ''' + @BaseDirectory + 'FordFocus.jpg'', SINGLE_BLOB) AS ImageFile), 3),
    (4, ''BMW320i.jpg'', (SELECT * FROM OPENROWSET(BULK ''' + @BaseDirectory + 'BMW320i.jpg'', SINGLE_BLOB) AS ImageFile), 4),
    (5, ''MercedesCclass.jpg'', (SELECT * FROM OPENROWSET(BULK ''' + @BaseDirectory + 'MercedesCclass.jpg'', SINGLE_BLOB) AS ImageFile), 5),
    (6, ''VWGolf.jpg'', (SELECT * FROM OPENROWSET(BULK ''' + @BaseDirectory + 'VWGolf.jpg'', SINGLE_BLOB) AS ImageFile), 6),
    (7, ''AudiA4.jpg'', (SELECT * FROM OPENROWSET(BULK ''' + @BaseDirectory + 'AudiA4.jpg'', SINGLE_BLOB) AS ImageFile), 7),
    (8, ''NissanXtrail.jpg'', (SELECT * FROM OPENROWSET(BULK ''' + @BaseDirectory + 'NissanXtrail.jpg'', SINGLE_BLOB) AS ImageFile), 8),
    (9, ''HyundaiTucson.jpg'', (SELECT * FROM OPENROWSET(BULK ''' + @BaseDirectory + 'HyundaiTucson.jpg'', SINGLE_BLOB) AS ImageFile), 9),
    (10, ''MazdaCX-5.jpg'', (SELECT * FROM OPENROWSET(BULK ''' + @BaseDirectory + 'MazdaCX-5.jpg'', SINGLE_BLOB) AS ImageFile), 10);
';

EXEC sp_executesql @SQL;
SET IDENTITY_INSERT Images OFF