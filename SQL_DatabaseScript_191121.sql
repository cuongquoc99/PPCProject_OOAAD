
--Create database

USE [master]
GO
IF NOT EXISTS (SELECT [name] FROM sys.databases WHERE name = N'PPCDB')
BEGIN
CREATE DATABASE [PPCDB] COLLATE SQL_Latin1_General_CP1_CI_AS
END
GO
EXEC dbo.sp_dbcmptlevel @dbname=N'PPCDB', @new_cmptlevel=100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
BEGIN
	EXEC [PPCDB].[dbo].[sp_fulltext_database] @action = 'enable'
END
GO
ALTER DATABASE [PPCDB] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [PPCDB] SET ANSI_NULLS OFF
GO
ALTER DATABASE [PPCDB] SET ANSI_PADDING OFF
GO
ALTER DATABASE [PPCDB] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [PPCDB] SET ARITHABORT OFF
GO
ALTER DATABASE [PPCDB] SET AUTO_CLOSE ON
GO
ALTER DATABASE [PPCDB] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [PPCDB] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [PPCDB] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [PPCDB] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [PPCDB] SET CURSOR_DEFAULT GLOBAL
GO
ALTER DATABASE [PPCDB] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [PPCDB] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [PPCDB] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [PPCDB] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [PPCDB] SET ENABLE_BROKER
GO
ALTER DATABASE [PPCDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [PPCDB] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [PPCDB] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [PPCDB] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [PPCDB] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [PPCDB] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [PPCDB] SET READ_WRITE
GO
ALTER DATABASE [PPCDB] SET RECOVERY SIMPLE
GO
ALTER DATABASE [PPCDB] SET MULTI_USER
GO
ALTER DATABASE [PPCDB] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [PPCDB] SET DB_CHAINING OFF
GO

USE [PPCDB]
GO

--Table dbo.City

USE [PPCDB]
GO

--Create table and its columns
CREATE TABLE [dbo].[City] (
	[ID] [int] NOT NULL IDENTITY (1, 1),
	[City_Name] [nvarchar](50) NULL);
GO

SET IDENTITY_INSERT [dbo].[City] ON
GO
INSERT INTO [dbo].[City] ([ID], [City_Name])
	VALUES (1, N'Hồ Chí Minh')

GO
INSERT INTO [dbo].[City] ([ID], [City_Name])
	VALUES (2, N'Hà Nội')

GO
INSERT INTO [dbo].[City] ([ID], [City_Name])
	VALUES (3, N'Đà Nẵng')

GO
INSERT INTO [dbo].[City] ([ID], [City_Name])
	VALUES (4, N'Bình Dương')

GO
INSERT INTO [dbo].[City] ([ID], [City_Name])
	VALUES (5, N'Vũng Tàu')

GO
INSERT INTO [dbo].[City] ([ID], [City_Name])
	VALUES (6, N'Bắc Giang')

GO
INSERT INTO [dbo].[City] ([ID], [City_Name])
	VALUES (7, N'Đồng Nai')

GO
INSERT INTO [dbo].[City] ([ID], [City_Name])
	VALUES (8, N'Cà Mau')

GO
INSERT INTO [dbo].[City] ([ID], [City_Name])
	VALUES (9, N'Long An')

GO
INSERT INTO [dbo].[City] ([ID], [City_Name])
	VALUES (10, N'Cần Thơ')

GO
SET IDENTITY_INSERT [dbo].[City] OFF
GO

--Table dbo.District

USE [PPCDB]
GO

--Create table and its columns
CREATE TABLE [dbo].[District] (
	[ID] [int] NOT NULL IDENTITY (1, 1),
	[City_ID] [int] NOT NULL,
	[District_Name] [nvarchar](50) NULL);
GO

SET IDENTITY_INSERT [dbo].[District] ON
GO
INSERT INTO [dbo].[District] ([ID], [City_ID], [District_Name])
	VALUES (1, 1, N'Quận Bình Tân')

GO
INSERT INTO [dbo].[District] ([ID], [City_ID], [District_Name])
	VALUES (2, 1, N'Quận Bình Thạnh')

GO
INSERT INTO [dbo].[District] ([ID], [City_ID], [District_Name])
	VALUES (3, 1, N'Quận 1')

GO
INSERT INTO [dbo].[District] ([ID], [City_ID], [District_Name])
	VALUES (4, 1, N'Quận 2')

GO
INSERT INTO [dbo].[District] ([ID], [City_ID], [District_Name])
	VALUES (5, 1, N'Quận 3')

GO
INSERT INTO [dbo].[District] ([ID], [City_ID], [District_Name])
	VALUES (6, 1, N'Quận 4')

GO
INSERT INTO [dbo].[District] ([ID], [City_ID], [District_Name])
	VALUES (7, 1, N'Quận 5')

GO
INSERT INTO [dbo].[District] ([ID], [City_ID], [District_Name])
	VALUES (8, 1, N'Quận 6')

GO
INSERT INTO [dbo].[District] ([ID], [City_ID], [District_Name])
	VALUES (9, 1, N'Quận 7')

GO
INSERT INTO [dbo].[District] ([ID], [City_ID], [District_Name])
	VALUES (10, 1, N'Quận 8')

GO
INSERT INTO [dbo].[District] ([ID], [City_ID], [District_Name])
	VALUES (11, 9, N'Huyện Bến Lức')

GO
INSERT INTO [dbo].[District] ([ID], [City_ID], [District_Name])
	VALUES (12, 9, N'Huyện Đức Hòa')

GO
INSERT INTO [dbo].[District] ([ID], [City_ID], [District_Name])
	VALUES (14, 9, N'Huyện Đức Huệ')

GO
INSERT INTO [dbo].[District] ([ID], [City_ID], [District_Name])
	VALUES (15, 4, N'Huyện Bến Cát')

GO
INSERT INTO [dbo].[District] ([ID], [City_ID], [District_Name])
	VALUES (16, 4, N'Huyện Dầu Tiếng')

GO
INSERT INTO [dbo].[District] ([ID], [City_ID], [District_Name])
	VALUES (17, 4, N'Huyện Thuận An')

GO
INSERT INTO [dbo].[District] ([ID], [City_ID], [District_Name])
	VALUES (18, 4, NULL)

GO
SET IDENTITY_INSERT [dbo].[District] OFF
GO

--Table dbo.Full_Contract

USE [PPCDB]
GO

--Create table and its columns
CREATE TABLE [dbo].[Full_Contract] (
	[ID] [int] NOT NULL IDENTITY (1, 1),
	[Full_Contract_Code] [varchar](10) NULL,
	[Customer_Name] [nvarchar](50) NOT NULL,
	[Year_Of_Birth] [int] NULL,
	[SSN] [varchar](15) NOT NULL,
	[Customer_Address] [nvarchar](100) NULL,
	[Mobile] [varchar](15) NULL,
	[Property_ID] [int] NOT NULL,
	[Date_Of_Contract] [date] NULL,
	[Price] [numeric](18, 0) NULL,
	[Deposit] [numeric](18, 0) NULL,
	[Remain] [numeric](18, 0) NULL,
	[Status] [bit] NOT NULL);
GO

SET IDENTITY_INSERT [dbo].[Full_Contract] ON
GO
INSERT INTO [dbo].[Full_Contract] ([ID], [Full_Contract_Code], [Customer_Name], [Year_Of_Birth], [SSN], [Customer_Address], [Mobile], [Property_ID], [Date_Of_Contract], [Price], [Deposit], [Remain], [Status])
	VALUES (1, N'FC19110001', N'Lý Thị Huyền Châu', 1990, N'301198908', N'45 Trần Hưng Đạo, Quận 5, Thành phố Hồ Chí Minh', N'0919686576', 1, CAST(0x5d400b AS date), 1000000000, 100000000, 900000000, CAST ('True' AS bit))

GO
INSERT INTO [dbo].[Full_Contract] ([ID], [Full_Contract_Code], [Customer_Name], [Year_Of_Birth], [SSN], [Customer_Address], [Mobile], [Property_ID], [Date_Of_Contract], [Price], [Deposit], [Remain], [Status])
	VALUES (2, N'FC19110002', N'Trần Công Anh', 1989, N'404948494', N'36 Lê Văn Sỹ, Quận 3, TP.HCM', N'0967686878', 2, CAST(0x5e400b AS date), 2000000000, 200000000, 1800000000, CAST ('True' AS bit))

GO
SET IDENTITY_INSERT [dbo].[Full_Contract] OFF
GO

--Table dbo.Installment_Contract

USE [PPCDB]
GO

--Create table and its columns
CREATE TABLE [dbo].[Installment_Contract] (
	[ID] [int] NOT NULL IDENTITY (1, 1),
	[Installment_Contract_Code] [varchar](10) NULL,
	[Customer_Name] [nvarchar](50) NOT NULL,
	[Year_Of_Birth] [int] NULL,
	[SSN] [varchar](15) NOT NULL,
	[Customer_Address] [nvarchar](100) NULL,
	[Mobile] [varchar](15) NULL,
	[Property_ID] [int] NOT NULL,
	[Date_Of_Contract] [date] NULL,
	[Installment_Payment_Method] [nvarchar](10) NOT NULL CONSTRAINT [DF_Installment_Contract_Installment_Payment_Method] DEFAULT (N'Tháng'),
	[Payment_Period] [int] NULL,
	[Price] [numeric](18, 0) NULL,
	[Deposit] [numeric](18, 0) NULL,
	[Loan_Amount] [numeric](18, 0) NULL,
	[Taken] [numeric](18, 0) NULL CONSTRAINT [DF_Installment_Contract_Taken] DEFAULT ((0)),
	[Remain] [numeric](18, 0) NULL,
	[Status] [bit] NOT NULL);
GO

SET IDENTITY_INSERT [dbo].[Installment_Contract] ON
GO
INSERT INTO [dbo].[Installment_Contract] ([ID], [Installment_Contract_Code], [Customer_Name], [Year_Of_Birth], [SSN], [Customer_Address], [Mobile], [Property_ID], [Date_Of_Contract], [Installment_Payment_Method], [Payment_Period], [Price], [Deposit], [Loan_Amount], [Taken], [Remain], [Status])
	VALUES (1, N'IC19110001', N'Lâm Bá Thắng', 1980, N'123467647', N'1 Lê Lợi, Quận 1, TP.HCM', N'0918273378', 3, CAST(0x4a400b AS date), N'Tháng', 12, 5000000000, 500000000, 4500000000, 0, 4500000000, CAST ('True' AS bit))

GO
SET IDENTITY_INSERT [dbo].[Installment_Contract] OFF
GO

--Table dbo.Property

USE [PPCDB]
GO

--Create table and its columns
CREATE TABLE [dbo].[Property] (
	[ID] [int] NOT NULL IDENTITY (1, 1),
	[Property_Code] [varchar](7) NULL,
	[Property_Name] [nvarchar](50) NULL,
	[Property_Type_ID] [int] NOT NULL,
	[Description] [nvarchar](MAX) NULL,
	[District_ID] [int] NOT NULL,
	[Address] [nvarchar](100) NULL,
	[Area] [int] NULL,
	[Bed_Room] [int] NULL,
	[Bath_Room] [int] NULL,
	[Price] [numeric](18, 0) NULL,
	[Installment_Rate] [float] NULL,
	[Avatar] [nvarchar](MAX) NULL,
	[Album] [nvarchar](MAX) NULL,
	[Property_Status_ID] [int] NOT NULL);
GO

SET IDENTITY_INSERT [dbo].[Property] ON
GO
INSERT INTO [dbo].[Property] ([ID], [Property_Code], [Property_Name], [Property_Type_ID], [Description], [District_ID], [Address], [Area], [Bed_Room], [Bath_Room], [Price], [Installment_Rate], [Avatar], [Album], [Property_Status_ID])
	VALUES (1, N'P190001', N'NHÀ PHỐ GARDEN KHANG ĐIỀN', 3, N'Nhà xây 1 trệt, 2 lầu, hoàn thiện bên ngoài kính cường lực, sơn nước chống rêu mốc chất lượng, có cửa kính cường lực, gara ô tô để xe thoải mái.', 1, N'Dự án Melosa Garden, Quận 9, Hồ Chí Minh', 80, 2, 2, 1000000000, CAST ('7.99' AS float), N'ppc0001.jpg', N'ppc0002.jpg;ppc0003.jpg;', 6)

GO
INSERT INTO [dbo].[Property] ([ID], [Property_Code], [Property_Name], [Property_Type_ID], [Description], [District_ID], [Address], [Area], [Bed_Room], [Bath_Room], [Price], [Installment_Rate], [Avatar], [Album], [Property_Status_ID])
	VALUES (2, N'P190002', N'NHÀ 4 TẦNG 3 MẶT THOÁNG TRẦN HƯNG ĐẠO Q1', 3, N'Bán nhà trung tâm Quận 1 đoạn đẹp nhất đường Trần Hưng Đạo.', 1, N'Đường Trần Hưng Đạo, Quận 1, Hồ Chí Minh', 78, 2, 2, 2000000000, CAST ('7.99' AS float), N'ppc0004.jpg', N'ppc0005.jpg;ppc0006.jpg', 6)

GO
INSERT INTO [dbo].[Property] ([ID], [Property_Code], [Property_Name], [Property_Type_ID], [Description], [District_ID], [Address], [Area], [Bed_Room], [Bath_Room], [Price], [Installment_Rate], [Avatar], [Album], [Property_Status_ID])
	VALUES (3, N'P190003', N'LAVITA CHARM', 2, N'Trong làn gió mát rượi, hương thơm cỏ cây tại Lavita Charm hòa theo từng bước chân sẽ đưa bạn trở về với không gian sống bình yên, tách biệt khỏi sự huyên náo của chốn phồn hoa. Lavita Charm như một nốt trầm yên ả của điệu nhạc du dương cho cảm xúc thăng hoa và nuôi dưỡng đam mê bất tận, đem đến nguồn vui, nguồn cảm hứng mới cho cuộc sống mỗi ngày.', 2, N'Dự án Lavita Charm, Đường 1, Phường Trường Thọ, Thủ Đức, Hồ Chí Minh', 120, 4, 4, 5000000000, CAST ('7.99' AS float), N'ppc0007.jpg', N'ppc0008.jpg;', 7)

GO
SET IDENTITY_INSERT [dbo].[Property] OFF
GO

--Table dbo.Property_Service

USE [PPCDB]
GO

--Create table and its columns
CREATE TABLE [dbo].[Property_Service] (
	[ID] [int] NOT NULL IDENTITY (1, 1),
	[Service_ID] [int] NOT NULL,
	[Property_ID] [int] NOT NULL);
GO

SET IDENTITY_INSERT [dbo].[Property_Service] ON
GO
INSERT INTO [dbo].[Property_Service] ([ID], [Service_ID], [Property_ID])
	VALUES (1, 1, 1)

GO
INSERT INTO [dbo].[Property_Service] ([ID], [Service_ID], [Property_ID])
	VALUES (2, 2, 1)

GO
INSERT INTO [dbo].[Property_Service] ([ID], [Service_ID], [Property_ID])
	VALUES (3, 3, 1)

GO
INSERT INTO [dbo].[Property_Service] ([ID], [Service_ID], [Property_ID])
	VALUES (4, 4, 1)

GO
INSERT INTO [dbo].[Property_Service] ([ID], [Service_ID], [Property_ID])
	VALUES (5, 1, 2)

GO
INSERT INTO [dbo].[Property_Service] ([ID], [Service_ID], [Property_ID])
	VALUES (6, 2, 2)

GO
INSERT INTO [dbo].[Property_Service] ([ID], [Service_ID], [Property_ID])
	VALUES (7, 1, 3)

GO
INSERT INTO [dbo].[Property_Service] ([ID], [Service_ID], [Property_ID])
	VALUES (8, 3, 3)

GO
INSERT INTO [dbo].[Property_Service] ([ID], [Service_ID], [Property_ID])
	VALUES (9, 4, 3)

GO
INSERT INTO [dbo].[Property_Service] ([ID], [Service_ID], [Property_ID])
	VALUES (10, 1, 4)

GO
INSERT INTO [dbo].[Property_Service] ([ID], [Service_ID], [Property_ID])
	VALUES (11, 2, 4)

GO
INSERT INTO [dbo].[Property_Service] ([ID], [Service_ID], [Property_ID])
	VALUES (12, 3, 4)

GO
SET IDENTITY_INSERT [dbo].[Property_Service] OFF
GO

--Table dbo.Property_Status

USE [PPCDB]
GO

--Create table and its columns
CREATE TABLE [dbo].[Property_Status] (
	[ID] [int] NOT NULL IDENTITY (1, 1),
	[Property_Status_Name] [nvarchar](50) NULL);
GO

SET IDENTITY_INSERT [dbo].[Property_Status] ON
GO
INSERT INTO [dbo].[Property_Status] ([ID], [Property_Status_Name])
	VALUES (1, N'Đang bán')

GO
INSERT INTO [dbo].[Property_Status] ([ID], [Property_Status_Name])
	VALUES (2, N'Đã bán thanh toán một lần')

GO
INSERT INTO [dbo].[Property_Status] ([ID], [Property_Status_Name])
	VALUES (3, N'Đã bán trả góp')

GO
INSERT INTO [dbo].[Property_Status] ([ID], [Property_Status_Name])
	VALUES (4, N'Không hiển thị')

GO
INSERT INTO [dbo].[Property_Status] ([ID], [Property_Status_Name])
	VALUES (5, N'Hết hạn để bán')

GO
INSERT INTO [dbo].[Property_Status] ([ID], [Property_Status_Name])
	VALUES (6, N'Đang cọc đầy đủ')

GO
INSERT INTO [dbo].[Property_Status] ([ID], [Property_Status_Name])
	VALUES (7, N'Đang cọc trả góp')

GO
SET IDENTITY_INSERT [dbo].[Property_Status] OFF
GO

--Table dbo.Property_Type

USE [PPCDB]
GO

--Create table and its columns
CREATE TABLE [dbo].[Property_Type] (
	[ID] [int] NOT NULL IDENTITY (1, 1),
	[Property_Type_Name] [nvarchar](50) NULL,
	[Property_Amount] [int] NULL CONSTRAINT [DF_Property_Type_Property_Amount] DEFAULT ((0)));
GO

SET IDENTITY_INSERT [dbo].[Property_Type] ON
GO
INSERT INTO [dbo].[Property_Type] ([ID], [Property_Type_Name], [Property_Amount])
	VALUES (1, N'Chung cư', 1)

GO
INSERT INTO [dbo].[Property_Type] ([ID], [Property_Type_Name], [Property_Amount])
	VALUES (2, N'Căn hộ dịch vụ', 1)

GO
INSERT INTO [dbo].[Property_Type] ([ID], [Property_Type_Name], [Property_Amount])
	VALUES (3, N'Nhà riêng', 1)

GO
INSERT INTO [dbo].[Property_Type] ([ID], [Property_Type_Name], [Property_Amount])
	VALUES (4, N'Villa', 0)

GO
INSERT INTO [dbo].[Property_Type] ([ID], [Property_Type_Name], [Property_Amount])
	VALUES (5, N'Studio', 0)

GO
INSERT INTO [dbo].[Property_Type] ([ID], [Property_Type_Name], [Property_Amount])
	VALUES (6, N'Office', 0)

GO
SET IDENTITY_INSERT [dbo].[Property_Type] OFF
GO

--Table dbo.Service

USE [PPCDB]
GO

--Create table and its columns
CREATE TABLE [dbo].[Service] (
	[ID] [int] NOT NULL IDENTITY (1, 1),
	[Service_Name] [nvarchar](50) NULL);
GO

SET IDENTITY_INSERT [dbo].[Service] ON
GO
INSERT INTO [dbo].[Service] ([ID], [Service_Name])
	VALUES (1, N'Ban công')

GO
INSERT INTO [dbo].[Service] ([ID], [Service_Name])
	VALUES (2, N'Thang máy')

GO
INSERT INTO [dbo].[Service] ([ID], [Service_Name])
	VALUES (3, N'Nhà bếp')

GO
INSERT INTO [dbo].[Service] ([ID], [Service_Name])
	VALUES (4, N'Hồ bơi')

GO
INSERT INTO [dbo].[Service] ([ID], [Service_Name])
	VALUES (5, N'Wifi')

GO
INSERT INTO [dbo].[Service] ([ID], [Service_Name])
	VALUES (6, N'Chỗ đậu xe')

GO
SET IDENTITY_INSERT [dbo].[Service] OFF
GO

--Executing Entities
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE TRIGGER [dbo].[TG_PROPERTY_INSERT]
ON PROPERTY
FOR INSERT
AS
BEGIN
	DECLARE @ID INT, @NAMHT VARCHAR(2)
	DECLARE @PROPERTY_CODE VARCHAR(10), @MAX INT
	SET @ID = (SELECT ID FROM INSERTED)
	SET @NAMHT = CONVERT(VARCHAR(2),RIGHT(YEAR(GETDATE()),2))
	IF EXISTS (SELECT 1 FROM Property WHERE SUBSTRING(Property_Code,2,2)=@NAMHT)
		SET @MAX = (SELECT MAX(SUBSTRING(Property_Code,4,4)) FROM Property
			WHERE  SUBSTRING(Property_Code,2,2)=@NAMHT)+1
	ELSE
		SET @MAX =1
	SET @PROPERTY_CODE = 'P'+ @NAMHT+RIGHT('000' + CONVERT(VARCHAR(4),@MAX),4)
	UPDATE Property SET Property_Code = @PROPERTY_CODE WHERE ID =@ID
END
GO

GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE TRIGGER [dbo].[TG_INSTALLMENTCONTRACT_INSERT]
ON INSTALLMENT_CONTRACT
FOR INSERT
AS
BEGIN
	DECLARE @ID INT, @NAMHT VARCHAR(2), @THANGHT VARCHAR(2)
	DECLARE @INSTALLMENT_CONTRACT_CODE VARCHAR(10), @MAX INT
	SET @ID = (SELECT ID FROM INSERTED)
	SET @NAMHT = CONVERT(VARCHAR(2),RIGHT(YEAR(GETDATE()),2))
	SET @THANGHT = CONVERT(VARCHAR(2) ,MONTH(GETDATE()))
	IF EXISTS (SELECT 1 FROM Installment_Contract WHERE SUBSTRING(Installment_Contract_Code,3,2)=@NAMHT)
		SET @MAX = (SELECT MAX(SUBSTRING(Installment_Contract_Code,7,4)) FROM Installment_Contract
			WHERE  SUBSTRING(Installment_Contract_Code,3,2)=@NAMHT)+1
	ELSE
		SET @MAX =1
	SET @INSTALLMENT_CONTRACT_CODE = 'IC'+ @NAMHT+ RIGHT(('0'+@THANGHT),2)+RIGHT('000' + CONVERT(VARCHAR(4),@MAX),4)
	UPDATE Installment_Contract SET Installment_Contract_Code = @INSTALLMENT_CONTRACT_CODE WHERE ID =@ID
END
GO

GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE TRIGGER [dbo].[TG_FULLCONTRACT_INSERT]
ON FULL_CONTRACT
FOR INSERT
AS
BEGIN
	DECLARE @ID INT, @NAMHT VARCHAR(2), @THANGHT VARCHAR(2)
	DECLARE @FULL_CONTRACT_CODE VARCHAR(10), @MAX INT
	SET @ID = (SELECT ID FROM INSERTED)
	SET @NAMHT = CONVERT(VARCHAR(2),RIGHT(YEAR(GETDATE()),2))
	SET @THANGHT = CONVERT(VARCHAR(2) ,MONTH(GETDATE()))
	IF EXISTS (SELECT 1 FROM Full_Contract WHERE SUBSTRING(Full_Contract_Code,3,2)=@NAMHT)
		SET @MAX = (SELECT MAX(SUBSTRING(Full_Contract_Code,7,4)) FROM Full_Contract
			WHERE  SUBSTRING(Full_Contract_Code,3,2)=@NAMHT)+1
	ELSE
		SET @MAX =1
	SET @FULL_CONTRACT_CODE = 'FC'+ @NAMHT+ RIGHT(('0'+@THANGHT),2)+RIGHT('000' + CONVERT(VARCHAR(4),@MAX),4)
	UPDATE Full_Contract SET Full_Contract_Code = @FULL_CONTRACT_CODE WHERE ID =@ID
END
GO

GO

--Indexes of table dbo.City
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER TABLE [dbo].[City] ADD CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED ([ID]) 
GO

--Indexes of table dbo.District
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER TABLE [dbo].[District] ADD CONSTRAINT [PK_District] PRIMARY KEY CLUSTERED ([ID]) 
GO

--Indexes of table dbo.Full_Contract
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER TABLE [dbo].[Full_Contract] ADD CONSTRAINT [PK_Full_Contract_1] PRIMARY KEY CLUSTERED ([ID]) 
GO

--Indexes of table dbo.Installment_Contract
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER TABLE [dbo].[Installment_Contract] ADD CONSTRAINT [PK_Installment_Contract_1] PRIMARY KEY CLUSTERED ([ID]) 
GO

--Indexes of table dbo.Property
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER TABLE [dbo].[Property] ADD CONSTRAINT [PK_Property] PRIMARY KEY CLUSTERED ([ID]) 
GO

--Indexes of table dbo.Property_Service
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER TABLE [dbo].[Property_Service] ADD CONSTRAINT [PK_Property_Service] PRIMARY KEY CLUSTERED ([ID]) 
GO

--Indexes of table dbo.Property_Status
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER TABLE [dbo].[Property_Status] ADD CONSTRAINT [PK_Property_Status] PRIMARY KEY CLUSTERED ([ID]) 
GO

--Indexes of table dbo.Property_Type
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER TABLE [dbo].[Property_Type] ADD CONSTRAINT [PK_Property_Type] PRIMARY KEY CLUSTERED ([ID]) 
GO

--Indexes of table dbo.Service
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER TABLE [dbo].[Service] ADD CONSTRAINT [PK_Service] PRIMARY KEY CLUSTERED ([ID]) 
GO

--Foreign Keys

USE [PPCDB]
GO
ALTER TABLE [dbo].[District] WITH CHECK ADD CONSTRAINT [FK_District_City] 
	FOREIGN KEY ([City_ID]) REFERENCES [dbo].[City] ([ID])
	ON UPDATE NO ACTION
	ON DELETE NO ACTION
GO
ALTER TABLE [dbo].[District] CHECK CONSTRAINT [FK_District_City]
GO
ALTER TABLE [dbo].[Full_Contract] WITH CHECK ADD CONSTRAINT [FK_Full_Contract_Property] 
	FOREIGN KEY ([Property_ID]) REFERENCES [dbo].[Property] ([ID])
	ON UPDATE NO ACTION
	ON DELETE NO ACTION
GO
ALTER TABLE [dbo].[Full_Contract] CHECK CONSTRAINT [FK_Full_Contract_Property]
GO
ALTER TABLE [dbo].[Installment_Contract] WITH CHECK ADD CONSTRAINT [FK_Installment_Contract_Property] 
	FOREIGN KEY ([Property_ID]) REFERENCES [dbo].[Property] ([ID])
	ON UPDATE NO ACTION
	ON DELETE NO ACTION
GO
ALTER TABLE [dbo].[Installment_Contract] CHECK CONSTRAINT [FK_Installment_Contract_Property]
GO
ALTER TABLE [dbo].[Property] WITH CHECK ADD CONSTRAINT [FK_Property_District] 
	FOREIGN KEY ([District_ID]) REFERENCES [dbo].[District] ([ID])
	ON UPDATE NO ACTION
	ON DELETE NO ACTION
GO
ALTER TABLE [dbo].[Property] CHECK CONSTRAINT [FK_Property_District]
GO
ALTER TABLE [dbo].[Property] WITH CHECK ADD CONSTRAINT [FK_Property_Property_Status] 
	FOREIGN KEY ([Property_Status_ID]) REFERENCES [dbo].[Property_Status] ([ID])
	ON UPDATE NO ACTION
	ON DELETE NO ACTION
GO
ALTER TABLE [dbo].[Property] CHECK CONSTRAINT [FK_Property_Property_Status]
GO
ALTER TABLE [dbo].[Property] WITH CHECK ADD CONSTRAINT [FK_Property_Property_Type] 
	FOREIGN KEY ([Property_Type_ID]) REFERENCES [dbo].[Property_Type] ([ID])
	ON UPDATE NO ACTION
	ON DELETE NO ACTION
GO
ALTER TABLE [dbo].[Property] CHECK CONSTRAINT [FK_Property_Property_Type]
GO
ALTER TABLE [dbo].[Property_Service] WITH CHECK ADD CONSTRAINT [FK_Property_Service_Service] 
	FOREIGN KEY ([Service_ID]) REFERENCES [dbo].[Service] ([ID])
	ON UPDATE NO ACTION
	ON DELETE NO ACTION
GO
ALTER TABLE [dbo].[Property_Service] CHECK CONSTRAINT [FK_Property_Service_Service]
GO
