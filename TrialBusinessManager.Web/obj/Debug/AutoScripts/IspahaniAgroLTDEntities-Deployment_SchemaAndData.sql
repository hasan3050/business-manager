SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductId] [bigint] IDENTITY(1,1) NOT NULL,
	[ProductCode] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ProductName] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ProductType] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ProductWing] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[StockKeepingUnit] [float] NOT NULL,
	[IsOpOrHibrid] [bit] NOT NULL,
	[IsImported] [bit] NOT NULL,
	[PurchasePeriodStart] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PurchasePeriodEnd] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SalesPeriodStart] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SalesPeriodEnd] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[IntroducedDate] [datetime] NOT NULL,
	[ProductStatus] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PricePerUnit] [float] NOT NULL,
	[BrandName] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO
SET IDENTITY_INSERT [dbo].[Products] ON 

GO
INSERT [dbo].[Products] ([ProductId], [ProductCode], [ProductName], [ProductType], [ProductWing], [StockKeepingUnit], [IsOpOrHibrid], [IsImported], [PurchasePeriodStart], [PurchasePeriodEnd], [SalesPeriodStart], [SalesPeriodEnd], [IntroducedDate], [ProductStatus], [PricePerUnit], [BrandName]) VALUES (1, N'AmonR', N'AmonRice', N'Rice', N'Seed', 30, 1, 1, N'March', N'April', N'March', N'April', CAST(0x0000808800000000 AS DateTime), N'Active', 35, N'AmanBrand')
GO
INSERT [dbo].[Products] ([ProductId], [ProductCode], [ProductName], [ProductType], [ProductWing], [StockKeepingUnit], [IsOpOrHibrid], [IsImported], [PurchasePeriodStart], [PurchasePeriodEnd], [SalesPeriodStart], [SalesPeriodEnd], [IntroducedDate], [ProductStatus], [PricePerUnit], [BrandName]) VALUES (2, N'Br12', N'BiriRice', N'Rice', N'Seed', 25, 1, 1, N'March', N'March', N'March', N'March', CAST(0x0000808800000000 AS DateTime), N'Active', 28, N'BiriBrand')
GO
INSERT [dbo].[Products] ([ProductId], [ProductCode], [ProductName], [ProductType], [ProductWing], [StockKeepingUnit], [IsOpOrHibrid], [IsImported], [PurchasePeriodStart], [PurchasePeriodEnd], [SalesPeriodStart], [SalesPeriodEnd], [IntroducedDate], [ProductStatus], [PricePerUnit], [BrandName]) VALUES (3, N'Kali', N'KalijiraRice', N'Rice', N'Agro', 30, 1, 1, N'March', N'March', N'March', N'March', CAST(0x0000808800000000 AS DateTime), N'Active', 40, N'KaliBB')
GO
INSERT [dbo].[Products] ([ProductId], [ProductCode], [ProductName], [ProductType], [ProductWing], [StockKeepingUnit], [IsOpOrHibrid], [IsImported], [PurchasePeriodStart], [PurchasePeriodEnd], [SalesPeriodStart], [SalesPeriodEnd], [IntroducedDate], [ProductStatus], [PricePerUnit], [BrandName]) VALUES (5, N'm12', N'fazli', N'Fruit', N'Seed', 0, 1, 1, N'January', N'Early January', N'Early January', N'Late January', CAST(0x0000A09A00C8F0AF AS DateTime), N'Pending', 0, NULL)
GO
INSERT [dbo].[Products] ([ProductId], [ProductCode], [ProductName], [ProductType], [ProductWing], [StockKeepingUnit], [IsOpOrHibrid], [IsImported], [PurchasePeriodStart], [PurchasePeriodEnd], [SalesPeriodStart], [SalesPeriodEnd], [IntroducedDate], [ProductStatus], [PricePerUnit], [BrandName]) VALUES (6, N'f12', N'fazli', N'Rice', N'Seed', 0, 1, 1, N'Early January', N'Mid January', N'Early January', N'Early January', CAST(0x0000A09A00CBF6F5 AS DateTime), N'Pending', 0, NULL)
GO
INSERT [dbo].[Products] ([ProductId], [ProductCode], [ProductName], [ProductType], [ProductWing], [StockKeepingUnit], [IsOpOrHibrid], [IsImported], [PurchasePeriodStart], [PurchasePeriodEnd], [SalesPeriodStart], [SalesPeriodEnd], [IntroducedDate], [ProductStatus], [PricePerUnit], [BrandName]) VALUES (7, N'a', N'aa', N'Rice', N'Agro', 0, 0, 1, N'January', N'Early January', N'Mid January', N'January', CAST(0x0000A09A00CD52E6 AS DateTime), N'Pending', 0, NULL)
GO
INSERT [dbo].[Products] ([ProductId], [ProductCode], [ProductName], [ProductType], [ProductWing], [StockKeepingUnit], [IsOpOrHibrid], [IsImported], [PurchasePeriodStart], [PurchasePeriodEnd], [SalesPeriodStart], [SalesPeriodEnd], [IntroducedDate], [ProductStatus], [PricePerUnit], [BrandName]) VALUES (8, N'vg', N'hb', N'Rice', N'Agro', 0, 1, 1, N'January', N'January', N'Early January', N'Late January', CAST(0x0000A09A00D2395B AS DateTime), N'Pending', 0, NULL)
GO
INSERT [dbo].[Products] ([ProductId], [ProductCode], [ProductName], [ProductType], [ProductWing], [StockKeepingUnit], [IsOpOrHibrid], [IsImported], [PurchasePeriodStart], [PurchasePeriodEnd], [SalesPeriodStart], [SalesPeriodEnd], [IntroducedDate], [ProductStatus], [PricePerUnit], [BrandName]) VALUES (9, N'br3', N'biri', N'Rice', N'Agro', 30, 1, 1, N'January', N'Early January', N'Mid January', N'January', CAST(0x0000A09A00D4721D AS DateTime), N'Pending', 30, N'hari')
GO
INSERT [dbo].[Products] ([ProductId], [ProductCode], [ProductName], [ProductType], [ProductWing], [StockKeepingUnit], [IsOpOrHibrid], [IsImported], [PurchasePeriodStart], [PurchasePeriodEnd], [SalesPeriodStart], [SalesPeriodEnd], [IntroducedDate], [ProductStatus], [PricePerUnit], [BrandName]) VALUES (10, N'KB14', N'Kalijira black', N'Tea', N'Bio-Tech', 20, 1, 1, N'Mid January', N'Early January', N'Early January', N'Early February', CAST(0x0000A09C00E8C3B6 AS DateTime), N'Pending', 20, N'black')
GO
INSERT [dbo].[Products] ([ProductId], [ProductCode], [ProductName], [ProductType], [ProductWing], [StockKeepingUnit], [IsOpOrHibrid], [IsImported], [PurchasePeriodStart], [PurchasePeriodEnd], [SalesPeriodStart], [SalesPeriodEnd], [IntroducedDate], [ProductStatus], [PricePerUnit], [BrandName]) VALUES (11, N'aas', N'Capsicum', N'Rice', N'Bio-Tech', 30, 1, 1, N'Late January', N'February', N'Late February', N'Mid March', CAST(0x0000A09C00E8E91E AS DateTime), N'Pending', 30, N'cs13')
GO
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[People](
	[PersonId] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DateOfBirth] [datetime] NOT NULL,
	[DateOfJoin] [datetime] NOT NULL,
	[Address] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ContactNo] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[EmailAddress] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_People] PRIMARY KEY CLUSTERED 
(
	[PersonId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO
SET IDENTITY_INSERT [dbo].[People] ON 

GO
INSERT [dbo].[People] ([PersonId], [Name], [DateOfBirth], [DateOfJoin], [Address], [ContactNo], [EmailAddress]) VALUES (1, N'Galib', CAST(0x000081C100000000 AS DateTime), CAST(0x000081C100000000 AS DateTime), N'Dhaka', N'12/12/1990', N'12/12/1990')
GO
INSERT [dbo].[People] ([PersonId], [Name], [DateOfBirth], [DateOfJoin], [Address], [ContactNo], [EmailAddress]) VALUES (2, N'Dibosh', CAST(0x000081C100000000 AS DateTime), CAST(0x000081C100000000 AS DateTime), N'12/12/1990', N'12/12/1990', N'12/12/1990')
GO
INSERT [dbo].[People] ([PersonId], [Name], [DateOfBirth], [DateOfJoin], [Address], [ContactNo], [EmailAddress]) VALUES (3, N'Fahad', CAST(0x000081C100000000 AS DateTime), CAST(0x000081C100000000 AS DateTime), N'12/12/1990', N'12/12/1990', N'12/12/1990')
GO
INSERT [dbo].[People] ([PersonId], [Name], [DateOfBirth], [DateOfJoin], [Address], [ContactNo], [EmailAddress]) VALUES (4, N'Ifti', CAST(0x000081C100000000 AS DateTime), CAST(0x000081C100000000 AS DateTime), N'12/12/1990', N'12/12/1990', N'12/12/1990')
GO
INSERT [dbo].[People] ([PersonId], [Name], [DateOfBirth], [DateOfJoin], [Address], [ContactNo], [EmailAddress]) VALUES (5, N'rahi', CAST(0x000081C100000000 AS DateTime), CAST(0x000081C100000000 AS DateTime), N'12/12/1990', N'12/12/1990', N'12/12/1990')
GO
INSERT [dbo].[People] ([PersonId], [Name], [DateOfBirth], [DateOfJoin], [Address], [ContactNo], [EmailAddress]) VALUES (6, N'hasan', CAST(0x000081C100000000 AS DateTime), CAST(0x000081C100000000 AS DateTime), N'12/12/1990', N'12/12/1990', N'12/12/1990')
GO
INSERT [dbo].[People] ([PersonId], [Name], [DateOfBirth], [DateOfJoin], [Address], [ContactNo], [EmailAddress]) VALUES (7, N'Raman', CAST(0x000063FF00000000 AS DateTime), CAST(0x0000A09C00E43A81 AS DateTime), N'Admilla', N'01919991', N'yoyo')
GO
INSERT [dbo].[People] ([PersonId], [Name], [DateOfBirth], [DateOfJoin], [Address], [ContactNo], [EmailAddress]) VALUES (8, N'Aman', CAST(0x0000806800000000 AS DateTime), CAST(0x0000A09C00E46DEB AS DateTime), N'akol', N'019919991', N'emeil')
GO
INSERT [dbo].[People] ([PersonId], [Name], [DateOfBirth], [DateOfJoin], [Address], [ContactNo], [EmailAddress]) VALUES (9, N'Joshim', CAST(0x0000806800000000 AS DateTime), CAST(0x0000A09C00E48D60 AS DateTime), N'parbnnn', N'019918881', N'email')
GO
INSERT [dbo].[People] ([PersonId], [Name], [DateOfBirth], [DateOfJoin], [Address], [ContactNo], [EmailAddress]) VALUES (10, N'Raz', CAST(0x0000806800000000 AS DateTime), CAST(0x0000A09C00E4B037 AS DateTime), N'raz', N'0199991', N'email')
GO
SET IDENTITY_INSERT [dbo].[People] OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages](
	[MessageId] [bigint] IDENTITY(1,1) NOT NULL,
	[MessageType] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Subject] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Body] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED 
(
	[MessageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sysdiagrams](
	[name] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[principal_id] [int] NOT NULL,
	[diagram_id] [int] IDENTITY(1,1) NOT NULL,
	[version] [int] NULL,
	[definition] [varbinary](max) NULL,
 CONSTRAINT [PK_sysdiagrams] PRIMARY KEY CLUSTERED 
(
	[diagram_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Regions](
	[RegionId] [bigint] IDENTITY(1,1) NOT NULL,
	[RegionName] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DistrictName] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_Regions] PRIMARY KEY CLUSTERED 
(
	[RegionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO
SET IDENTITY_INSERT [dbo].[Regions] ON 

GO
INSERT [dbo].[Regions] ([RegionId], [RegionName], [DistrictName]) VALUES (1, N'Barisal', N'Barisal')
GO
INSERT [dbo].[Regions] ([RegionId], [RegionName], [DistrictName]) VALUES (2, N'Dhaka', N'Dhaka')
GO
SET IDENTITY_INSERT [dbo].[Regions] OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Promotions](
	[PromotionId] [bigint] IDENTITY(1,1) NOT NULL,
	[ProductId] [bigint] NOT NULL,
	[StartAt] [datetime] NOT NULL,
	[EndAt] [datetime] NOT NULL,
	[PromotionTitle] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PromotionProductName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PromotionProductQuantity] [float] NOT NULL,
	[ProductPrice] [float] NOT NULL,
	[ProductQuantity] [float] NOT NULL,
 CONSTRAINT [PK_Promotions] PRIMARY KEY CLUSTERED 
(
	[PromotionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

CREATE NONCLUSTERED INDEX [IX_FK_ProductPromotion] ON [dbo].[Promotions] 
(
	[ProductId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegionTargets](
	[RegionId] [bigint] NOT NULL,
	[ProductName] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[TargetQuantity] [float] NOT NULL,
	[AchievedQuantity] [float] NOT NULL,
	[AchievedAmount] [float] NOT NULL,
	[Product_ProductId] [bigint] NOT NULL,
 CONSTRAINT [PK_RegionTargets] PRIMARY KEY CLUSTERED 
(
	[RegionId] ASC,
	[ProductName] ASC,
	[StartDate] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

CREATE NONCLUSTERED INDEX [IX_FK_ProductRegionTarget] ON [dbo].[RegionTargets] 
(
	[Product_ProductId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ledgers](
	[Date] [datetime] NOT NULL,
	[Method] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[IsDealerOrEmployee] [bit] NOT NULL,
	[IsDebitOrCredit] [bit] NOT NULL,
	[PartyId] [bigint] NOT NULL,
	[MemoNo] [bigint] NOT NULL,
	[ProductId] [bigint] NOT NULL,
	[ProductQuantity] [float] NOT NULL,
	[CreditAmount] [float] NOT NULL,
 CONSTRAINT [PK_Ledgers] PRIMARY KEY CLUSTERED 
(
	[Date] ASC,
	[Method] ASC,
	[PartyId] ASC,
	[MemoNo] ASC,
	[ProductId] ASC,
	[IsDealerOrEmployee] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

CREATE NONCLUSTERED INDEX [IX_FK_ProductLedger] ON [dbo].[Ledgers] 
(
	[ProductId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
INSERT [dbo].[Ledgers] ([Date], [Method], [IsDealerOrEmployee], [IsDebitOrCredit], [PartyId], [MemoNo], [ProductId], [ProductQuantity], [CreditAmount]) VALUES (CAST(0x0000A09A00AD805D AS DateTime), N'Bill Generation', 1, 0, 2, 1, 1, 100, 105000)
GO
INSERT [dbo].[Ledgers] ([Date], [Method], [IsDealerOrEmployee], [IsDebitOrCredit], [PartyId], [MemoNo], [ProductId], [ProductQuantity], [CreditAmount]) VALUES (CAST(0x0000A09A00AD805E AS DateTime), N'Bill Generation', 1, 0, 2, 1, 2, 80, 56000)
GO
INSERT [dbo].[Ledgers] ([Date], [Method], [IsDealerOrEmployee], [IsDebitOrCredit], [PartyId], [MemoNo], [ProductId], [ProductQuantity], [CreditAmount]) VALUES (CAST(0x0000A09A00AF121D AS DateTime), N'Bill Generation', 1, 0, 2, 0, 1, 100, 105000)
GO
INSERT [dbo].[Ledgers] ([Date], [Method], [IsDealerOrEmployee], [IsDebitOrCredit], [PartyId], [MemoNo], [ProductId], [ProductQuantity], [CreditAmount]) VALUES (CAST(0x0000A09C00000000 AS DateTime), N'Bill Generation', 1, 0, 3, 3, 5, 2000, 120000)
GO
INSERT [dbo].[Ledgers] ([Date], [Method], [IsDealerOrEmployee], [IsDebitOrCredit], [PartyId], [MemoNo], [ProductId], [ProductQuantity], [CreditAmount]) VALUES (CAST(0x0000A09C00000000 AS DateTime), N'Bill Generation', 1, 0, 4, 5, 6, 2000, 125000)
GO
INSERT [dbo].[Ledgers] ([Date], [Method], [IsDealerOrEmployee], [IsDebitOrCredit], [PartyId], [MemoNo], [ProductId], [ProductQuantity], [CreditAmount]) VALUES (CAST(0x0000A09C00000000 AS DateTime), N'Bill Generation', 1, 0, 5, 6, 7, 1700, 109000)
GO
INSERT [dbo].[Ledgers] ([Date], [Method], [IsDealerOrEmployee], [IsDebitOrCredit], [PartyId], [MemoNo], [ProductId], [ProductQuantity], [CreditAmount]) VALUES (CAST(0x0000A09C00000000 AS DateTime), N'Bill Generation', 1, 0, 5, 12, 6, 900, 100000)
GO
INSERT [dbo].[Ledgers] ([Date], [Method], [IsDealerOrEmployee], [IsDebitOrCredit], [PartyId], [MemoNo], [ProductId], [ProductQuantity], [CreditAmount]) VALUES (CAST(0x0000A09C00000000 AS DateTime), N'Bill Generation', 1, 0, 6, 9, 9, 1000, 120000)
GO
INSERT [dbo].[Ledgers] ([Date], [Method], [IsDealerOrEmployee], [IsDebitOrCredit], [PartyId], [MemoNo], [ProductId], [ProductQuantity], [CreditAmount]) VALUES (CAST(0x0000A09C00000000 AS DateTime), N'Bill Generation', 1, 0, 7, 7, 8, 1200, 108900)
GO
INSERT [dbo].[Ledgers] ([Date], [Method], [IsDealerOrEmployee], [IsDebitOrCredit], [PartyId], [MemoNo], [ProductId], [ProductQuantity], [CreditAmount]) VALUES (CAST(0x0000A09C00000000 AS DateTime), N'Bill Generation', 1, 0, 7, 13, 7, 800, 90000)
GO
INSERT [dbo].[Ledgers] ([Date], [Method], [IsDealerOrEmployee], [IsDebitOrCredit], [PartyId], [MemoNo], [ProductId], [ProductQuantity], [CreditAmount]) VALUES (CAST(0x0000A09C00000000 AS DateTime), N'Bill Generation', 1, 0, 8, 10, 8, 1000, 130000)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[EmployeeId] [bigint] IDENTITY(1,1) NOT NULL,
	[PersonId] [bigint] NOT NULL,
	[RegionId] [bigint] NOT NULL,
	[Designation] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ActivityStatus] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[UserName] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Password] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

CREATE NONCLUSTERED INDEX [IX_FK_EmployeeRegion] ON [dbo].[Employees] 
(
	[RegionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO

CREATE NONCLUSTERED INDEX [IX_FK_PersonEmployee] ON [dbo].[Employees] 
(
	[PersonId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
SET IDENTITY_INSERT [dbo].[Employees] ON 

GO
INSERT [dbo].[Employees] ([EmployeeId], [PersonId], [RegionId], [Designation], [ActivityStatus], [UserName], [Password]) VALUES (1, 1, 1, N'Regional Manager', N'Active', N'rm', N'rm')
GO
INSERT [dbo].[Employees] ([EmployeeId], [PersonId], [RegionId], [Designation], [ActivityStatus], [UserName], [Password]) VALUES (2, 2, 1, N'Store In Charge', N'Active', N'sic', N'sic')
GO
INSERT [dbo].[Employees] ([EmployeeId], [PersonId], [RegionId], [Designation], [ActivityStatus], [UserName], [Password]) VALUES (4, 3, 1, N'Dispatch Officer', N'Active', N'do', N'do')
GO
INSERT [dbo].[Employees] ([EmployeeId], [PersonId], [RegionId], [Designation], [ActivityStatus], [UserName], [Password]) VALUES (5, 4, 1, N'National Sales Manager', N'Active', N'nsm', N'nsm')
GO
INSERT [dbo].[Employees] ([EmployeeId], [PersonId], [RegionId], [Designation], [ActivityStatus], [UserName], [Password]) VALUES (6, 5, 1, N'Admin', N'Active', N'admin', N'admin')
GO
INSERT [dbo].[Employees] ([EmployeeId], [PersonId], [RegionId], [Designation], [ActivityStatus], [UserName], [Password]) VALUES (7, 6, 1, N'Accountant', N'Active', N'ac', N'ac')
GO
INSERT [dbo].[Employees] ([EmployeeId], [PersonId], [RegionId], [Designation], [ActivityStatus], [UserName], [Password]) VALUES (8, 7, 1, N'Sales Officer', N'Pending', N'ramnamm', N'1000')
GO
INSERT [dbo].[Employees] ([EmployeeId], [PersonId], [RegionId], [Designation], [ActivityStatus], [UserName], [Password]) VALUES (9, 8, 1, N'Sales Officer', N'Pending', N'amanman', N'1000')
GO
INSERT [dbo].[Employees] ([EmployeeId], [PersonId], [RegionId], [Designation], [ActivityStatus], [UserName], [Password]) VALUES (10, 9, 2, N'Sales Officer', N'Pending', N'jamnsaa', N'1000')
GO
INSERT [dbo].[Employees] ([EmployeeId], [PersonId], [RegionId], [Designation], [ActivityStatus], [UserName], [Password]) VALUES (11, 10, 2, N'Sales Officer', N'Pending', N'razazaaa', N'1000')
GO
SET IDENTITY_INSERT [dbo].[Employees] OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dealers](
	[DealerId] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[FatherName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[MotherName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PresentAddress] [nvarchar](150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PermamentAddress] [nvarchar](150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Nationality] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DateOfBirth] [datetime] NOT NULL,
	[ContactNo] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[EmailAddress] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PictureLink] [nvarchar](150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CompanyName] [nvarchar](150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CompanyAddress] [nvarchar](150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[LicenseNo] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[LicenseIssueDate] [datetime] NOT NULL,
	[BusinessType] [nvarchar](60) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[OwnerName] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[OwnerAddress] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PreferredTypeOfPayment] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[HasAnotherDealership] [bit] NOT NULL,
	[DealershipCompany] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[HasOwnOffice] [bit] NOT NULL,
	[DateOfRegistration] [datetime] NOT NULL,
	[CreditLimit] [float] NOT NULL,
	[ExpectedDefaulterDate] [datetime] NOT NULL,
	[ActivityStatus] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TotalDue] [float] NOT NULL,
	[UserName] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Password] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DateOfAdminApproval] [datetime] NOT NULL,
	[RegionId] [bigint] NOT NULL,
	[SalesOfficerId] [bigint] NOT NULL,
 CONSTRAINT [PK_Dealers] PRIMARY KEY CLUSTERED 
(
	[DealerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

CREATE NONCLUSTERED INDEX [IX_FK_DealerRegion] ON [dbo].[Dealers] 
(
	[RegionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO

CREATE NONCLUSTERED INDEX [IX_FK_EmployeeDealer] ON [dbo].[Dealers] 
(
	[SalesOfficerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
SET IDENTITY_INSERT [dbo].[Dealers] ON 

GO
INSERT [dbo].[Dealers] ([DealerId], [Name], [FatherName], [MotherName], [PresentAddress], [PermamentAddress], [Nationality], [DateOfBirth], [ContactNo], [EmailAddress], [PictureLink], [CompanyName], [CompanyAddress], [LicenseNo], [LicenseIssueDate], [BusinessType], [OwnerName], [OwnerAddress], [PreferredTypeOfPayment], [HasAnotherDealership], [DealershipCompany], [HasOwnOffice], [DateOfRegistration], [CreditLimit], [ExpectedDefaulterDate], [ActivityStatus], [TotalDue], [UserName], [Password], [DateOfAdminApproval], [RegionId], [SalesOfficerId]) VALUES (2, N'Abdul', N'adbil', N'12/12/1990', N'12/12/1990', N'12/12/1990', N'12/12/1990', CAST(0x000081C100000000 AS DateTime), N'12/12/1990', N'12/12/1990', N'12/12/1990', N'Anvil', N'Dhaka', N'12/12/1990', CAST(0x000081C100000000 AS DateTime), N'Agro', N'Adil', N'12/12/1990', N'Advanced', 1, N'Fork', 1, CAST(0x000081C100000000 AS DateTime), 90000, CAST(0x000100F000000000 AS DateTime), N'Active', 266000, N'oo', N'oo', CAST(0x000081C100000000 AS DateTime), 1, 1)
GO
INSERT [dbo].[Dealers] ([DealerId], [Name], [FatherName], [MotherName], [PresentAddress], [PermamentAddress], [Nationality], [DateOfBirth], [ContactNo], [EmailAddress], [PictureLink], [CompanyName], [CompanyAddress], [LicenseNo], [LicenseIssueDate], [BusinessType], [OwnerName], [OwnerAddress], [PreferredTypeOfPayment], [HasAnotherDealership], [DealershipCompany], [HasOwnOffice], [DateOfRegistration], [CreditLimit], [ExpectedDefaulterDate], [ActivityStatus], [TotalDue], [UserName], [Password], [DateOfAdminApproval], [RegionId], [SalesOfficerId]) VALUES (3, N'Rahman', N'ammam', N'amamam', N'Dhaka', N'Dhaka', N'bangla', CAST(0x0000A09C00E4EC75 AS DateTime), N'9888888', N'N/A', N'milu.jpg', N'Werr', N'sssss', N'sss', CAST(0x0000A09C00E4EC75 AS DateTime), N'Agro', N'sss', N'sss', N'Cash', 1, N'ssssss', 1, CAST(0x0000A09C00E4EC75 AS DateTime), 90000, CAST(0x0000E7F200E4EC75 AS DateTime), N'Pending', 0, N'N/A', N'PPPPPPPPPP', CAST(0x0000A09C00E4EC75 AS DateTime), 1, 8)
GO
INSERT [dbo].[Dealers] ([DealerId], [Name], [FatherName], [MotherName], [PresentAddress], [PermamentAddress], [Nationality], [DateOfBirth], [ContactNo], [EmailAddress], [PictureLink], [CompanyName], [CompanyAddress], [LicenseNo], [LicenseIssueDate], [BusinessType], [OwnerName], [OwnerAddress], [PreferredTypeOfPayment], [HasAnotherDealership], [DealershipCompany], [HasOwnOffice], [DateOfRegistration], [CreditLimit], [ExpectedDefaulterDate], [ActivityStatus], [TotalDue], [UserName], [Password], [DateOfAdminApproval], [RegionId], [SalesOfficerId]) VALUES (4, N'Jazmin', N'ahh', N'hhhhhh', N'Dhaka', N'Dhaka', N'banlga', CAST(0x0000A09C00E53038 AS DateTime), N'0199199191', N'N/A', N'milu.jpg', N'pepsi', N'ssss', N'ssss', CAST(0x0000A09C00E53038 AS DateTime), N'Seed', N'sssss', N'ssss', N'Advanced', 0, N'ssssss', 0, CAST(0x0000A09C00E53038 AS DateTime), 90000, CAST(0x0000E7F200E53038 AS DateTime), N'Pending', 0, N'N/A', N'PPPPPPPPPP', CAST(0x0000A09C00E53038 AS DateTime), 1, 9)
GO
INSERT [dbo].[Dealers] ([DealerId], [Name], [FatherName], [MotherName], [PresentAddress], [PermamentAddress], [Nationality], [DateOfBirth], [ContactNo], [EmailAddress], [PictureLink], [CompanyName], [CompanyAddress], [LicenseNo], [LicenseIssueDate], [BusinessType], [OwnerName], [OwnerAddress], [PreferredTypeOfPayment], [HasAnotherDealership], [DealershipCompany], [HasOwnOffice], [DateOfRegistration], [CreditLimit], [ExpectedDefaulterDate], [ActivityStatus], [TotalDue], [UserName], [Password], [DateOfAdminApproval], [RegionId], [SalesOfficerId]) VALUES (5, N'Faruk', N'hhh', N'hhhhh', N'dddd', N'fgggg', N'dddd', CAST(0x0000A09C00E6069C AS DateTime), N'ddd', N'N/A', N'milu.jpg', N'ggg', N'dddddsdwewdw', N'ddddddddd', CAST(0x0000A09C00E6069C AS DateTime), N'Agro', N'dddd', N'dddd', N'Cash', 0, N'sdsdsdsd', 0, CAST(0x0000A09C00E6069C AS DateTime), 90000, CAST(0x0000E7F200E6069C AS DateTime), N'Pending', 0, N'N/A', N'PPPPPPPPPP', CAST(0x0000A09C00E6069C AS DateTime), 2, 10)
GO
INSERT [dbo].[Dealers] ([DealerId], [Name], [FatherName], [MotherName], [PresentAddress], [PermamentAddress], [Nationality], [DateOfBirth], [ContactNo], [EmailAddress], [PictureLink], [CompanyName], [CompanyAddress], [LicenseNo], [LicenseIssueDate], [BusinessType], [OwnerName], [OwnerAddress], [PreferredTypeOfPayment], [HasAnotherDealership], [DealershipCompany], [HasOwnOffice], [DateOfRegistration], [CreditLimit], [ExpectedDefaulterDate], [ActivityStatus], [TotalDue], [UserName], [Password], [DateOfAdminApproval], [RegionId], [SalesOfficerId]) VALUES (6, N'Rahim al', N'sdsdsdsd', N'sdsd', N'asasas', N'asasa', N'asasasasasasasasasasasa', CAST(0x0000A09C00E62883 AS DateTime), N'sasasasasas', N'N/A', N'milu.jpg', N'asa', N'sasas', N'asasa', CAST(0x0000A09C00E62883 AS DateTime), N'Agro', N'asasa', N'asas', N'Advanced', 0, N'asasasas', 1, CAST(0x0000A09C00E62883 AS DateTime), 90000, CAST(0x0000E7F200E62883 AS DateTime), N'Pending', 0, N'N/A', N'PPPPPPPPPP', CAST(0x0000A09C00E62883 AS DateTime), 2, 11)
GO
INSERT [dbo].[Dealers] ([DealerId], [Name], [FatherName], [MotherName], [PresentAddress], [PermamentAddress], [Nationality], [DateOfBirth], [ContactNo], [EmailAddress], [PictureLink], [CompanyName], [CompanyAddress], [LicenseNo], [LicenseIssueDate], [BusinessType], [OwnerName], [OwnerAddress], [PreferredTypeOfPayment], [HasAnotherDealership], [DealershipCompany], [HasOwnOffice], [DateOfRegistration], [CreditLimit], [ExpectedDefaulterDate], [ActivityStatus], [TotalDue], [UserName], [Password], [DateOfAdminApproval], [RegionId], [SalesOfficerId]) VALUES (7, N'farik', N'Saleh', N'molla', N'nnnnn', N'sdsdsdsds', N'hhh', CAST(0x0000A09C00E813BF AS DateTime), N'hhhh', N'N/A', N'milu.jpg', N'hhhhh', N'sdsdsds', N'sdsd', CAST(0x0000A09C00E813C0 AS DateTime), N'Agro', N'dsd', N'sdsd', N'Advanced', 0, N'sdsdsds', 1, CAST(0x0000A09C00E813BF AS DateTime), 90000, CAST(0x0000E7F200E813C0 AS DateTime), N'Pending', 0, N'N/A', N'PPPPPPPPPP', CAST(0x0000A09C00E813BF AS DateTime), 2, 10)
GO
INSERT [dbo].[Dealers] ([DealerId], [Name], [FatherName], [MotherName], [PresentAddress], [PermamentAddress], [Nationality], [DateOfBirth], [ContactNo], [EmailAddress], [PictureLink], [CompanyName], [CompanyAddress], [LicenseNo], [LicenseIssueDate], [BusinessType], [OwnerName], [OwnerAddress], [PreferredTypeOfPayment], [HasAnotherDealership], [DealershipCompany], [HasOwnOffice], [DateOfRegistration], [CreditLimit], [ExpectedDefaulterDate], [ActivityStatus], [TotalDue], [UserName], [Password], [DateOfAdminApproval], [RegionId], [SalesOfficerId]) VALUES (8, N'saleh', N'Saleh', N'molla', N'nnnnn', N'sdsdsdsds', N'hhh', CAST(0x0000A09C00E813BF AS DateTime), N'hhhh', N'N/A', N'milu.jpg', N'hhhhh', N'sdsdsds', N'sdsdasasa', CAST(0x0000A09C00E813C0 AS DateTime), N'Agro', N'dsd', N'sdsd', N'Advanced', 0, N'sdsdsds', 1, CAST(0x0000A09C00E813BF AS DateTime), 90000, CAST(0x0000E7F200E813C0 AS DateTime), N'Pending', 0, N'N/A', N'PPPPPPPPPP', CAST(0x0000A09C00E813BF AS DateTime), 2, 10)
GO
INSERT [dbo].[Dealers] ([DealerId], [Name], [FatherName], [MotherName], [PresentAddress], [PermamentAddress], [Nationality], [DateOfBirth], [ContactNo], [EmailAddress], [PictureLink], [CompanyName], [CompanyAddress], [LicenseNo], [LicenseIssueDate], [BusinessType], [OwnerName], [OwnerAddress], [PreferredTypeOfPayment], [HasAnotherDealership], [DealershipCompany], [HasOwnOffice], [DateOfRegistration], [CreditLimit], [ExpectedDefaulterDate], [ActivityStatus], [TotalDue], [UserName], [Password], [DateOfAdminApproval], [RegionId], [SalesOfficerId]) VALUES (9, N'arif', N'Saleh', N'molla', N'nnnnn', N'sdsdsdsds', N'hhh', CAST(0x0000A09C00E813BF AS DateTime), N'hhhh', N'N/A', N'milu.jpg', N'hhhhh', N'sdsdsds', N'sdsdasasdsdsa', CAST(0x0000A09C00E813C0 AS DateTime), N'Agro', N'dsd', N'sdsd', N'Advanced', 0, N'sdsdsds', 1, CAST(0x0000A09C00E813BF AS DateTime), 90000, CAST(0x0000E7F200E813C0 AS DateTime), N'Pending', 0, N'N/A', N'PPPPPPPPPP', CAST(0x0000A09C00E813BF AS DateTime), 2, 11)
GO
SET IDENTITY_INSERT [dbo].[Dealers] OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Commissions](
	[CommissionId] [bigint] IDENTITY(1,1) NOT NULL,
	[ProductId] [bigint] NOT NULL,
	[Percentage] [float] NOT NULL,
	[Duration] [smallint] NOT NULL,
	[CommissionName] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CommissionStatus] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IntroducedDate] [datetime] NULL,
	[NSMId] [bigint] NOT NULL,
	[AdminId] [bigint] NOT NULL,
 CONSTRAINT [PK_Commissions] PRIMARY KEY CLUSTERED 
(
	[CommissionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

CREATE NONCLUSTERED INDEX [IX_FK_EmployeeApproveCommission] ON [dbo].[Commissions] 
(
	[AdminId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO

CREATE NONCLUSTERED INDEX [IX_FK_EmployeeCreateCommission] ON [dbo].[Commissions] 
(
	[NSMId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO

CREATE NONCLUSTERED INDEX [IX_FK_ProductCommission] ON [dbo].[Commissions] 
(
	[ProductId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
SET IDENTITY_INSERT [dbo].[Commissions] ON 

GO
INSERT [dbo].[Commissions] ([CommissionId], [ProductId], [Percentage], [Duration], [CommissionName], [CommissionStatus], [IntroducedDate], [NSMId], [AdminId]) VALUES (1, 8, 5, 7, N'On Cash', N'Pending', CAST(0x0000A09A00D22856 AS DateTime), 5, 5)
GO
INSERT [dbo].[Commissions] ([CommissionId], [ProductId], [Percentage], [Duration], [CommissionName], [CommissionStatus], [IntroducedDate], [NSMId], [AdminId]) VALUES (2, 8, 5, 7, N'Advance', N'Pending', CAST(0x0000A09A00D22FB2 AS DateTime), 5, 5)
GO
INSERT [dbo].[Commissions] ([CommissionId], [ProductId], [Percentage], [Duration], [CommissionName], [CommissionStatus], [IntroducedDate], [NSMId], [AdminId]) VALUES (3, 9, 7, 5, N'Advance', N'Pending', CAST(0x0000A09A00D457F8 AS DateTime), 5, 5)
GO
INSERT [dbo].[Commissions] ([CommissionId], [ProductId], [Percentage], [Duration], [CommissionName], [CommissionStatus], [IntroducedDate], [NSMId], [AdminId]) VALUES (4, 10, 12, 10, N'Advance', N'Pending', CAST(0x0000A09C00E8B0B3 AS DateTime), 5, 5)
GO
SET IDENTITY_INSERT [dbo].[Commissions] OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inventories](
	[InventoryId] [bigint] IDENTITY(1,1) NOT NULL,
	[RegionId] [bigint] NOT NULL,
	[StoreInChargeId] [bigint] NOT NULL,
	[DispatchOfficerId] [bigint] NOT NULL,
 CONSTRAINT [PK_Inventories] PRIMARY KEY CLUSTERED 
(
	[InventoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

CREATE NONCLUSTERED INDEX [IX_FK_EmployeeInventory] ON [dbo].[Inventories] 
(
	[StoreInChargeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO

CREATE NONCLUSTERED INDEX [IX_FK_EmployeeInventory1] ON [dbo].[Inventories] 
(
	[DispatchOfficerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO

CREATE NONCLUSTERED INDEX [IX_FK_RegionInventory] ON [dbo].[Inventories] 
(
	[RegionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
SET IDENTITY_INSERT [dbo].[Inventories] ON 

GO
INSERT [dbo].[Inventories] ([InventoryId], [RegionId], [StoreInChargeId], [DispatchOfficerId]) VALUES (4, 1, 2, 4)
GO
SET IDENTITY_INSERT [dbo].[Inventories] OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductEdits](
	[ProductEditId] [bigint] IDENTITY(1,1) NOT NULL,
	[ProductId] [bigint] NOT NULL,
	[EditType] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[NSMId] [bigint] NOT NULL,
	[AdminId] [bigint] NOT NULL,
	[PreviousValue] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[EditedValue] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[EditStatus] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ChangeApplicableFrom] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductEdits] PRIMARY KEY CLUSTERED 
(
	[ProductEditId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

CREATE NONCLUSTERED INDEX [IX_FK_EmployeeApprovesProductEdits] ON [dbo].[ProductEdits] 
(
	[AdminId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO

CREATE NONCLUSTERED INDEX [IX_FK_EmployeeProductEdit] ON [dbo].[ProductEdits] 
(
	[NSMId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO

CREATE NONCLUSTERED INDEX [IX_FK_ProductProductEdit] ON [dbo].[ProductEdits] 
(
	[ProductId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
SET IDENTITY_INSERT [dbo].[ProductEdits] ON 

GO
INSERT [dbo].[ProductEdits] ([ProductEditId], [ProductId], [EditType], [NSMId], [AdminId], [PreviousValue], [EditedValue], [EditStatus], [ChangeApplicableFrom]) VALUES (2, 5, N'Create New Product with Commission', 5, 5, N'NULL', N'New Product fazli', N'Pending', CAST(0x00011D4500000000 AS DateTime))
GO
INSERT [dbo].[ProductEdits] ([ProductEditId], [ProductId], [EditType], [NSMId], [AdminId], [PreviousValue], [EditedValue], [EditStatus], [ChangeApplicableFrom]) VALUES (3, 6, N'Create New Product with Commission', 5, 5, N'NULL', N'New Product fazli', N'Pending', CAST(0x00011D4500000000 AS DateTime))
GO
INSERT [dbo].[ProductEdits] ([ProductEditId], [ProductId], [EditType], [NSMId], [AdminId], [PreviousValue], [EditedValue], [EditStatus], [ChangeApplicableFrom]) VALUES (4, 7, N'Create New Product with Commission', 5, 5, N'NULL', N'New Product aa', N'Pending', CAST(0x00011D4500000000 AS DateTime))
GO
INSERT [dbo].[ProductEdits] ([ProductEditId], [ProductId], [EditType], [NSMId], [AdminId], [PreviousValue], [EditedValue], [EditStatus], [ChangeApplicableFrom]) VALUES (5, 8, N'Create New Product with Commission', 5, 5, N'NULL', N'New Product hb', N'Pending', CAST(0x00011D4500000000 AS DateTime))
GO
INSERT [dbo].[ProductEdits] ([ProductEditId], [ProductId], [EditType], [NSMId], [AdminId], [PreviousValue], [EditedValue], [EditStatus], [ChangeApplicableFrom]) VALUES (6, 9, N'Create New Product with Commission', 5, 5, N'NULL', N'New Product biri', N'Pending', CAST(0x00011D4500000000 AS DateTime))
GO
INSERT [dbo].[ProductEdits] ([ProductEditId], [ProductId], [EditType], [NSMId], [AdminId], [PreviousValue], [EditedValue], [EditStatus], [ChangeApplicableFrom]) VALUES (7, 10, N'Create New Product with Commission', 5, 5, N'NULL', N'New Product Kalijira black', N'Pending', CAST(0x00011D4500000000 AS DateTime))
GO
INSERT [dbo].[ProductEdits] ([ProductEditId], [ProductId], [EditType], [NSMId], [AdminId], [PreviousValue], [EditedValue], [EditStatus], [ChangeApplicableFrom]) VALUES (8, 11, N'Create New Product with Commission', 5, 5, N'NULL', N'New Product Capsicum', N'Pending', CAST(0x00011D4500000000 AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[ProductEdits] OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Packages](
	[PackageId] [bigint] IDENTITY(1,1) NOT NULL,
	[PackageName] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PackageCode] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PackageStatus] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[NSMId] [bigint] NOT NULL,
	[AdminId] [bigint] NOT NULL,
	[IntroducedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Packages] PRIMARY KEY CLUSTERED 
(
	[PackageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

CREATE NONCLUSTERED INDEX [IX_FK_EmployeeApprovedPackage] ON [dbo].[Packages] 
(
	[AdminId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO

CREATE NONCLUSTERED INDEX [IX_FK_EmployeeCreatedPackage] ON [dbo].[Packages] 
(
	[NSMId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
SET IDENTITY_INSERT [dbo].[Packages] ON 

GO
INSERT [dbo].[Packages] ([PackageId], [PackageName], [PackageCode], [PackageStatus], [NSMId], [AdminId], [IntroducedDate]) VALUES (1, N'Big', N'b1', N'Active', 5, 6, CAST(0x0000808800000000 AS DateTime))
GO
INSERT [dbo].[Packages] ([PackageId], [PackageName], [PackageCode], [PackageStatus], [NSMId], [AdminId], [IntroducedDate]) VALUES (2, N'Small', N's1', N'Active', 5, 6, CAST(0x0000808800000000 AS DateTime))
GO
INSERT [dbo].[Packages] ([PackageId], [PackageName], [PackageCode], [PackageStatus], [NSMId], [AdminId], [IntroducedDate]) VALUES (3, N'Huge', N'h1', N'Active', 5, 6, CAST(0x0000808800000000 AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Packages] OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoticeBoards](
	[NoticeId] [bigint] IDENTITY(1,1) NOT NULL,
	[NoticeSubject] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[NoticeBody] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IssuedById] [bigint] NOT NULL,
 CONSTRAINT [PK_NoticeBoards] PRIMARY KEY CLUSTERED 
(
	[NoticeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

CREATE NONCLUSTERED INDEX [IX_FK_EmployeeNoticeBoard] ON [dbo].[NoticeBoards] 
(
	[IssuedById] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Expenditures](
	[ExpenditureId] [bigint] IDENTITY(1,1) NOT NULL,
	[DateOfIssue] [datetime] NOT NULL,
	[RegionId] [bigint] NOT NULL,
	[PlacedById] [bigint] NOT NULL,
	[PlacedToId] [bigint] NOT NULL,
	[Status] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TotalPlacedAmount] [float] NOT NULL,
	[TotalAcceptedAmount] [float] NOT NULL,
	[ExpenditureCode] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_Expenditures] PRIMARY KEY CLUSTERED 
(
	[ExpenditureId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

CREATE NONCLUSTERED INDEX [IX_FK_EmployeeExpenditure] ON [dbo].[Expenditures] 
(
	[PlacedById] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO

CREATE NONCLUSTERED INDEX [IX_FK_EmployeeExpenditure1] ON [dbo].[Expenditures] 
(
	[PlacedToId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO

CREATE NONCLUSTERED INDEX [IX_FK_RegionExpenditure] ON [dbo].[Expenditures] 
(
	[RegionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
SET IDENTITY_INSERT [dbo].[Expenditures] ON 

GO
INSERT [dbo].[Expenditures] ([ExpenditureId], [DateOfIssue], [RegionId], [PlacedById], [PlacedToId], [Status], [TotalPlacedAmount], [TotalAcceptedAmount], [ExpenditureCode]) VALUES (1, CAST(0x0000A09D000A5DF4 AS DateTime), 1, 1, 1, N'Pending', 2200, 0, N'7/29/2012 12:37:44 AM')
GO
SET IDENTITY_INSERT [dbo].[Expenditures] OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MessageDeliveries](
	[OfficeCode] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SendFromId] [bigint] NOT NULL,
	[SendToId] [bigint] NOT NULL,
	[DateOfIssue] [datetime] NOT NULL,
	[MessageId] [bigint] NOT NULL,
 CONSTRAINT [PK_MessageDeliveries] PRIMARY KEY CLUSTERED 
(
	[OfficeCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

CREATE NONCLUSTERED INDEX [IX_FK_EmployeeMessageDelivery] ON [dbo].[MessageDeliveries] 
(
	[SendFromId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO

CREATE NONCLUSTERED INDEX [IX_FK_MessageDeliveryEmployee] ON [dbo].[MessageDeliveries] 
(
	[SendToId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO

CREATE NONCLUSTERED INDEX [IX_FK_MessageDeliveryMessage] ON [dbo].[MessageDeliveries] 
(
	[MessageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesOfficerTargets](
	[SalesOfficerId] [bigint] NOT NULL,
	[ProductName] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[TargetQuantity] [float] NOT NULL,
	[AchievedQuantity] [float] NOT NULL,
	[AchievedAmount] [float] NOT NULL,
	[Product_ProductId] [bigint] NOT NULL,
 CONSTRAINT [PK_SalesOfficerTargets] PRIMARY KEY CLUSTERED 
(
	[SalesOfficerId] ASC,
	[ProductName] ASC,
	[StartDate] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

CREATE NONCLUSTERED INDEX [IX_FK_SalesOfficerTargetProduct] ON [dbo].[SalesOfficerTargets] 
(
	[Product_ProductId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[YearSummarySOExpenditures](
	[SalesOfficerId] [bigint] NOT NULL,
	[SeasonStart] [datetime] NOT NULL,
	[SeasonEnd] [datetime] NOT NULL,
	[TotalExpenditure] [float] NOT NULL,
 CONSTRAINT [PK_YearSummarySOExpenditures] PRIMARY KEY CLUSTERED 
(
	[SalesOfficerId] ASC,
	[SeasonStart] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesReturns](
	[SalesReturnId] [bigint] IDENTITY(1,1) NOT NULL,
	[DateOfIssue] [datetime] NOT NULL,
	[DealerId] [bigint] NOT NULL,
	[RMId] [bigint] NOT NULL,
	[TotalPlacedAmount] [float] NOT NULL,
	[TotalAcceptedAmount] [float] NOT NULL,
	[SalesReturnCode] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Status] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_SalesReturns] PRIMARY KEY CLUSTERED 
(
	[SalesReturnId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

CREATE NONCLUSTERED INDEX [IX_FK_DealerSalesReturn] ON [dbo].[SalesReturns] 
(
	[DealerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO

CREATE NONCLUSTERED INDEX [IX_FK_EmployeeSalesReturn] ON [dbo].[SalesReturns] 
(
	[RMId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
SET IDENTITY_INSERT [dbo].[SalesReturns] ON 

GO
INSERT [dbo].[SalesReturns] ([SalesReturnId], [DateOfIssue], [DealerId], [RMId], [TotalPlacedAmount], [TotalAcceptedAmount], [SalesReturnCode], [Status]) VALUES (1, CAST(0x0000A09A00DAAE6F AS DateTime), 2, 1, 0, 0, N'7/26/2012 1:16:11 PM', N'Placed')
GO
INSERT [dbo].[SalesReturns] ([SalesReturnId], [DateOfIssue], [DealerId], [RMId], [TotalPlacedAmount], [TotalAcceptedAmount], [SalesReturnCode], [Status]) VALUES (2, CAST(0x0000A09C0180FBBC AS DateTime), 2, 1, 350, 0, N'7/28/2012 11:21:40 PM', N'Placed')
GO
SET IDENTITY_INSERT [dbo].[SalesReturns] OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[YearSummaryInventoryProducts](
	[InventoryId] [bigint] NOT NULL,
	[ProductId] [bigint] NOT NULL,
	[LotId] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SeasonStart] [datetime] NOT NULL,
	[SeasonEnd] [datetime] NOT NULL,
	[OpeningProduct] [float] NOT NULL,
	[MRRInQuantity] [float] NOT NULL,
	[PLRLostQuantity] [float] NOT NULL,
	[ProductSellQuantity] [float] NOT NULL,
	[SellAmount] [float] NOT NULL,
	[ClosingProduct] [float] NOT NULL,
	[PurchaseAmount] [float] NOT NULL,
 CONSTRAINT [PK_YearSummaryInventoryProducts] PRIMARY KEY CLUSTERED 
(
	[InventoryId] ASC,
	[ProductId] ASC,
	[LotId] ASC,
	[SeasonStart] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

CREATE NONCLUSTERED INDEX [IX_FK_ProductYearSummaryInventoryProduct] ON [dbo].[YearSummaryInventoryProducts] 
(
	[ProductId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[YearSummaryInventoryPackages](
	[InventoryId] [bigint] NOT NULL,
	[PackageId] [bigint] NOT NULL,
	[SessionStart] [datetime] NOT NULL,
	[SessionEnd] [datetime] NOT NULL,
	[OpeningPackage] [float] NOT NULL,
	[MRRInQuantity] [float] NOT NULL,
	[PLRLostQuantity] [float] NOT NULL,
	[PackageUsedQuantity] [float] NOT NULL,
	[ClosingPackage] [float] NOT NULL,
	[PurchaseAmount] [float] NOT NULL,
	[LostAmount] [float] NOT NULL,
 CONSTRAINT [PK_YearSummaryInventoryPackages] PRIMARY KEY CLUSTERED 
(
	[InventoryId] ASC,
	[PackageId] ASC,
	[SessionStart] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

CREATE NONCLUSTERED INDEX [IX_FK_PackageYearSummaryInventoryPackage] ON [dbo].[YearSummaryInventoryPackages] 
(
	[PackageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[YearSummaryDealers](
	[DealerId] [bigint] NOT NULL,
	[SeasonStart] [datetime] NOT NULL,
	[SeasonEnd] [datetime] NOT NULL,
	[ProductId] [bigint] NOT NULL,
	[OpeningBalance] [float] NOT NULL,
	[ClosingBalance] [float] NOT NULL,
	[ProductQuantity] [float] NOT NULL,
	[DebitAmount] [float] NOT NULL,
	[CreditAmount] [float] NOT NULL,
 CONSTRAINT [PK_YearSummaryDealers] PRIMARY KEY CLUSTERED 
(
	[DealerId] ASC,
	[SeasonStart] ASC,
	[ProductId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

CREATE NONCLUSTERED INDEX [IX_FK_ProductYearSummaryDealer] ON [dbo].[YearSummaryDealers] 
(
	[ProductId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Requisitions](
	[RequisitionId] [bigint] IDENTITY(1,1) NOT NULL,
	[InventoryId] [bigint] NOT NULL,
	[DateOfIssue] [datetime] NOT NULL,
	[IssuedById] [bigint] NOT NULL,
	[IssuedToId] [bigint] NOT NULL,
	[Status] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DateOfApproval] [datetime] NOT NULL,
	[RequisitionType] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[RequisitionCode] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_Requisitions] PRIMARY KEY CLUSTERED 
(
	[RequisitionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

CREATE NONCLUSTERED INDEX [IX_FK_EmployeeRequisition] ON [dbo].[Requisitions] 
(
	[IssuedById] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO

CREATE NONCLUSTERED INDEX [IX_FK_EmployeeRequisition1] ON [dbo].[Requisitions] 
(
	[IssuedToId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO

CREATE NONCLUSTERED INDEX [IX_FK_InventoryRequisition] ON [dbo].[Requisitions] 
(
	[InventoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
SET IDENTITY_INSERT [dbo].[Requisitions] ON 

GO
INSERT [dbo].[Requisitions] ([RequisitionId], [InventoryId], [DateOfIssue], [IssuedById], [IssuedToId], [Status], [DateOfApproval], [RequisitionType], [RequisitionCode]) VALUES (1, 4, CAST(0x0000A09A009D154C AS DateTime), 4, 2, N'Approved', CAST(0x0000A09A009D154D AS DateTime), N'Requisition For Packing', N'7/26/2012 9:31:55 AM')
GO
SET IDENTITY_INSERT [dbo].[Requisitions] OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InventoryProductInfoes](
	[InventoryId] [bigint] NOT NULL,
	[ProductId] [bigint] NOT NULL,
	[LotId] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[UnfinishedQuantity] [float] NOT NULL,
	[OnProcessingQuantity] [float] NOT NULL,
	[FinishedQuantity] [float] NOT NULL,
	[UnitLotCost] [float] NULL,
 CONSTRAINT [PK_InventoryProductInfoes] PRIMARY KEY CLUSTERED 
(
	[InventoryId] ASC,
	[ProductId] ASC,
	[LotId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

CREATE NONCLUSTERED INDEX [IX_FK_ProductInventoryProductInfo] ON [dbo].[InventoryProductInfoes] 
(
	[ProductId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
INSERT [dbo].[InventoryProductInfoes] ([InventoryId], [ProductId], [LotId], [UnfinishedQuantity], [OnProcessingQuantity], [FinishedQuantity], [UnitLotCost]) VALUES (4, 1, N'12', 900, 970, 4300, 30)
GO
INSERT [dbo].[InventoryProductInfoes] ([InventoryId], [ProductId], [LotId], [UnfinishedQuantity], [OnProcessingQuantity], [FinishedQuantity], [UnitLotCost]) VALUES (4, 2, N'10', 1400, 1000, 8000, 35)
GO
INSERT [dbo].[InventoryProductInfoes] ([InventoryId], [ProductId], [LotId], [UnfinishedQuantity], [OnProcessingQuantity], [FinishedQuantity], [UnitLotCost]) VALUES (4, 3, N'10', 2000, 1000, 10000, 40)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InventoryPackageInfoes](
	[InventoryId] [bigint] NOT NULL,
	[PackageId] [bigint] NOT NULL,
	[UnfinishedQuantity] [float] NOT NULL,
	[OnProcessingQuantity] [float] NOT NULL,
	[FinishedQuantity] [float] NOT NULL,
	[UnitCost] [float] NULL,
 CONSTRAINT [PK_InventoryPackageInfoes] PRIMARY KEY CLUSTERED 
(
	[InventoryId] ASC,
	[PackageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

CREATE NONCLUSTERED INDEX [IX_FK_PackageInventoryPackageInfo] ON [dbo].[InventoryPackageInfoes] 
(
	[PackageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
INSERT [dbo].[InventoryPackageInfoes] ([InventoryId], [PackageId], [UnfinishedQuantity], [OnProcessingQuantity], [FinishedQuantity], [UnitCost]) VALUES (4, 1, 1060, 1000, 1000, 25)
GO
INSERT [dbo].[InventoryPackageInfoes] ([InventoryId], [PackageId], [UnfinishedQuantity], [OnProcessingQuantity], [FinishedQuantity], [UnitCost]) VALUES (4, 2, 1060, 1000, 1000, 20)
GO
INSERT [dbo].[InventoryPackageInfoes] ([InventoryId], [PackageId], [UnfinishedQuantity], [OnProcessingQuantity], [FinishedQuantity], [UnitCost]) VALUES (4, 3, 1000, 1000, 1000, 10)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InventoryLogs](
	[Date] [datetime] NOT NULL,
	[ProductId] [bigint] NOT NULL,
	[InventoryId] [bigint] NOT NULL,
	[LotId] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Method] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[MemoNo] [bigint] NOT NULL,
	[ProductQuantity] [float] NOT NULL,
	[OpeningQuantity] [float] NOT NULL,
	[ClosingQuantity] [float] NOT NULL,
	[ProductCost] [float] NOT NULL,
 CONSTRAINT [PK_InventoryLogs] PRIMARY KEY CLUSTERED 
(
	[Date] ASC,
	[ProductId] ASC,
	[InventoryId] ASC,
	[LotId] ASC,
	[Method] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

CREATE NONCLUSTERED INDEX [IX_FK_InventoryInventoryLog] ON [dbo].[InventoryLogs] 
(
	[InventoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO

CREATE NONCLUSTERED INDEX [IX_FK_ProductInventoryLog] ON [dbo].[InventoryLogs] 
(
	[ProductId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExpenditureInfoes](
	[ExpenditureId] [bigint] NOT NULL,
	[SalesOfficerId] [bigint] NOT NULL,
	[PlacedAmount] [float] NOT NULL,
	[Remarks] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[NSMAcceptedAmount] [float] NOT NULL,
	[AccountAcceptedAmount] [float] NOT NULL,
 CONSTRAINT [PK_ExpenditureInfoes] PRIMARY KEY CLUSTERED 
(
	[ExpenditureId] ASC,
	[SalesOfficerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

CREATE NONCLUSTERED INDEX [IX_FK_EmployeeExpenditureInfo] ON [dbo].[ExpenditureInfoes] 
(
	[SalesOfficerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
INSERT [dbo].[ExpenditureInfoes] ([ExpenditureId], [SalesOfficerId], [PlacedAmount], [Remarks], [NSMAcceptedAmount], [AccountAcceptedAmount]) VALUES (1, 8, 1000, N'Dealer Expense', 0, 0)
GO
INSERT [dbo].[ExpenditureInfoes] ([ExpenditureId], [SalesOfficerId], [PlacedAmount], [Remarks], [NSMAcceptedAmount], [AccountAcceptedAmount]) VALUES (1, 9, 1200, N'Food', 0, 0)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillPayments](
	[BillPaymentId] [bigint] IDENTITY(1,1) NOT NULL,
	[DealerId] [bigint] NOT NULL,
	[RMId] [bigint] NOT NULL,
	[DateOfIssue] [datetime] NOT NULL,
	[TotalAmount] [float] NOT NULL,
	[PaymentMethod] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[BankName] [nvarchar](40) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[BranchName] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DDNumber] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Status] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[AccountantId] [bigint] NOT NULL,
	[AccountantFinalizeDate] [datetime] NOT NULL,
	[BillPaymentCode] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_BillPayments] PRIMARY KEY CLUSTERED 
(
	[BillPaymentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

CREATE NONCLUSTERED INDEX [IX_FK_DealerBillPayment] ON [dbo].[BillPayments] 
(
	[DealerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO

CREATE NONCLUSTERED INDEX [IX_FK_EmployeeBillPayment] ON [dbo].[BillPayments] 
(
	[RMId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
SET IDENTITY_INSERT [dbo].[BillPayments] ON 

GO
INSERT [dbo].[BillPayments] ([BillPaymentId], [DealerId], [RMId], [DateOfIssue], [TotalAmount], [PaymentMethod], [BankName], [BranchName], [DDNumber], [Status], [AccountantId], [AccountantFinalizeDate], [BillPaymentCode]) VALUES (5, 2, 1, CAST(0x00009FCB00000000 AS DateTime), 10000, N'Cash', NULL, NULL, NULL, N'Approved by Accountant', 7, CAST(0x0000A09B0031F5E3 AS DateTime), N'AA')
GO
INSERT [dbo].[BillPayments] ([BillPaymentId], [DealerId], [RMId], [DateOfIssue], [TotalAmount], [PaymentMethod], [BankName], [BranchName], [DDNumber], [Status], [AccountantId], [AccountantFinalizeDate], [BillPaymentCode]) VALUES (7, 2, 1, CAST(0x00009FCB00000000 AS DateTime), 12000, N'Cash', NULL, NULL, NULL, N'Rejected by Accountant', 7, CAST(0x0000A09B00320828 AS DateTime), N'SA')
GO
INSERT [dbo].[BillPayments] ([BillPaymentId], [DealerId], [RMId], [DateOfIssue], [TotalAmount], [PaymentMethod], [BankName], [BranchName], [DDNumber], [Status], [AccountantId], [AccountantFinalizeDate], [BillPaymentCode]) VALUES (8, 2, 1, CAST(0x0000A09D001E72C6 AS DateTime), 3400, N'Cash', N'', N'', N'', N'Issued by RM', 0, CAST(0x00011D5700000000 AS DateTime), N'7/29/2012 1:50:51 AM')
GO
SET IDENTITY_INSERT [dbo].[BillPayments] OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MRRs](
	[MRRId] [bigint] IDENTITY(1,1) NOT NULL,
	[DateOfIssue] [datetime] NOT NULL,
	[InventoryId] [bigint] NOT NULL,
	[IssuedById] [bigint] NOT NULL,
	[IssuedToId] [bigint] NOT NULL,
	[Status] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DateOfApproval] [datetime] NOT NULL,
	[MRRCode] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PurchaseOrderNo] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ChallanNo] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Wing] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[RetailerName] [nvarchar](80) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[MRRType] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_MRRs] PRIMARY KEY CLUSTERED 
(
	[MRRId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

CREATE NONCLUSTERED INDEX [IX_FK_EmployeeMRR] ON [dbo].[MRRs] 
(
	[IssuedById] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO

CREATE NONCLUSTERED INDEX [IX_FK_InventoryMRR] ON [dbo].[MRRs] 
(
	[InventoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
SET IDENTITY_INSERT [dbo].[MRRs] ON 

GO
INSERT [dbo].[MRRs] ([MRRId], [DateOfIssue], [InventoryId], [IssuedById], [IssuedToId], [Status], [DateOfApproval], [MRRCode], [PurchaseOrderNo], [ChallanNo], [Wing], [RetailerName], [MRRType]) VALUES (1, CAST(0x0000A09A009C1D99 AS DateTime), 4, 2, 2, N'Issued', CAST(0x0000A09A009C1D99 AS DateTime), N'7/26/2012 9:28:23 AM', N'123ERT', N'chalan1', N'Seed', N'Jasim', N'Product')
GO
INSERT [dbo].[MRRs] ([MRRId], [DateOfIssue], [InventoryId], [IssuedById], [IssuedToId], [Status], [DateOfApproval], [MRRCode], [PurchaseOrderNo], [ChallanNo], [Wing], [RetailerName], [MRRType]) VALUES (2, CAST(0x0000A09A009C5755 AS DateTime), 4, 2, 2, N'Issued', CAST(0x0000A09A009C5755 AS DateTime), N'7/26/2012 9:29:13 AM', N'12345RTY', N'chalan2', N'Seed', N'Zara', N'Package')
GO
INSERT [dbo].[MRRs] ([MRRId], [DateOfIssue], [InventoryId], [IssuedById], [IssuedToId], [Status], [DateOfApproval], [MRRCode], [PurchaseOrderNo], [ChallanNo], [Wing], [RetailerName], [MRRType]) VALUES (3, CAST(0x0000A09C0187433E AS DateTime), 4, 2, 2, N'Issued', CAST(0x0000A09C0187433E AS DateTime), N'7/28/2012 11:44:32 PM', N'123SERW', N'CH12123', N'Agro', N'Fahim', N'Package')
GO
SET IDENTITY_INSERT [dbo].[MRRs] OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PLRs](
	[PLRId] [bigint] IDENTITY(1,1) NOT NULL,
	[InventoryId] [bigint] NOT NULL,
	[DateOfIssue] [datetime] NOT NULL,
	[IssuedById] [bigint] NOT NULL,
	[IssuedToId] [bigint] NOT NULL,
	[Status] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DateOfApproval] [datetime] NOT NULL,
	[PLRCode] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_PLRs] PRIMARY KEY CLUSTERED 
(
	[PLRId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

CREATE NONCLUSTERED INDEX [IX_FK_EmployeePLR] ON [dbo].[PLRs] 
(
	[IssuedById] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO

CREATE NONCLUSTERED INDEX [IX_FK_EmployeePLR1] ON [dbo].[PLRs] 
(
	[IssuedToId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO

CREATE NONCLUSTERED INDEX [IX_FK_InventoryPLR] ON [dbo].[PLRs] 
(
	[InventoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
SET IDENTITY_INSERT [dbo].[PLRs] ON 

GO
INSERT [dbo].[PLRs] ([PLRId], [InventoryId], [DateOfIssue], [IssuedById], [IssuedToId], [Status], [DateOfApproval], [PLRCode]) VALUES (1, 4, CAST(0x0000A09A00ECDF30 AS DateTime), 2, 2, N'Verified', CAST(0x0000A09A00ECDF31 AS DateTime), N'7/26/2012 2:22:25 PM')
GO
SET IDENTITY_INSERT [dbo].[PLRs] OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Indents](
	[IndentId] [bigint] IDENTITY(1,1) NOT NULL,
	[DateOfPlace] [datetime] NOT NULL,
	[IndentStatus] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[IssuedById] [bigint] NOT NULL,
	[IssuedToId] [bigint] NOT NULL,
	[ForwardedToId] [bigint] NOT NULL,
	[PaymentMethod] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[IsConsideredByNSM] [bit] NOT NULL,
	[IndentCode] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_Indents] PRIMARY KEY CLUSTERED 
(
	[IndentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

CREATE NONCLUSTERED INDEX [IX_FK_DealerPlacesIndent] ON [dbo].[Indents] 
(
	[IssuedById] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO

CREATE NONCLUSTERED INDEX [IX_FK_RMVerifiesIndent] ON [dbo].[Indents] 
(
	[IssuedToId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
SET IDENTITY_INSERT [dbo].[Indents] ON 

GO
INSERT [dbo].[Indents] ([IndentId], [DateOfPlace], [IndentStatus], [IssuedById], [IssuedToId], [ForwardedToId], [PaymentMethod], [IsConsideredByNSM], [IndentCode]) VALUES (1, CAST(0x0000A09A009DD7F7 AS DateTime), N'Dispatched', 2, 1, 2, N'Cash', 0, N'indent123')
GO
INSERT [dbo].[Indents] ([IndentId], [DateOfPlace], [IndentStatus], [IssuedById], [IssuedToId], [ForwardedToId], [PaymentMethod], [IsConsideredByNSM], [IndentCode]) VALUES (2, CAST(0x0000A09A009DF2AA AS DateTime), N'Dispatched', 2, 1, 2, N'Advanced', 0, N'indent2')
GO
SET IDENTITY_INSERT [dbo].[Indents] OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IndentProductInfoes](
	[IndentId] [bigint] NOT NULL,
	[ProductId] [bigint] NOT NULL,
	[ProductPrice] [float] NOT NULL,
	[ProductQuantity] [float] NOT NULL,
	[EditTime] [datetime] NOT NULL,
	[CommissionPercentage] [float] NOT NULL,
	[ProductQuantityByRM] [float] NULL,
	[ProductQuantityBySIC] [float] NULL,
	[EditTimeRM] [datetime] NULL,
	[EditTimeSIC] [datetime] NULL,
 CONSTRAINT [PK_IndentProductInfoes] PRIMARY KEY CLUSTERED 
(
	[IndentId] ASC,
	[ProductId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

CREATE NONCLUSTERED INDEX [IX_FK_IndentProductInfoProduct] ON [dbo].[IndentProductInfoes] 
(
	[ProductId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
INSERT [dbo].[IndentProductInfoes] ([IndentId], [ProductId], [ProductPrice], [ProductQuantity], [EditTime], [CommissionPercentage], [ProductQuantityByRM], [ProductQuantityBySIC], [EditTimeRM], [EditTimeSIC]) VALUES (1, 1, 35, 100, CAST(0x0000A09A009DD80A AS DateTime), 0, 100, 100, CAST(0x0000A09A00AC60AF AS DateTime), CAST(0x0000A09A00AC9358 AS DateTime))
GO
INSERT [dbo].[IndentProductInfoes] ([IndentId], [ProductId], [ProductPrice], [ProductQuantity], [EditTime], [CommissionPercentage], [ProductQuantityByRM], [ProductQuantityBySIC], [EditTimeRM], [EditTimeSIC]) VALUES (1, 2, 28, 80, CAST(0x0000A09A009DD80D AS DateTime), 0, 80, 80, CAST(0x0000A09A00AC60AF AS DateTime), CAST(0x0000A09A00AC9358 AS DateTime))
GO
INSERT [dbo].[IndentProductInfoes] ([IndentId], [ProductId], [ProductPrice], [ProductQuantity], [EditTime], [CommissionPercentage], [ProductQuantityByRM], [ProductQuantityBySIC], [EditTimeRM], [EditTimeSIC]) VALUES (2, 1, 35, 100, CAST(0x0000A09A009DF2AA AS DateTime), 0, 100, 100, CAST(0x0000A09A00AC6B85 AS DateTime), CAST(0x0000A09A00AC9B08 AS DateTime))
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bills](
	[BillId] [bigint] IDENTITY(1,1) NOT NULL,
	[DateOfIssue] [datetime] NOT NULL,
	[IndentId] [bigint] NOT NULL,
	[TransportType] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TransportCost] [float] NOT NULL,
	[VehicleNo] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[EmergencyContactNo] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TotalProductCost] [float] NOT NULL,
	[TotalPaid] [float] NOT NULL,
	[PaymentDeadline] [datetime] NOT NULL,
	[BillStatus] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[HasProductLoss] [bit] NOT NULL,
	[ProductLossCost] [float] NOT NULL,
	[DispatchedById] [bigint] NOT NULL,
	[BillCode] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[HasSalesReturn] [bit] NULL,
	[SalesReturnCost] [float] NULL,
	[DealerId] [bigint] NOT NULL,
 CONSTRAINT [PK_Bills] PRIMARY KEY CLUSTERED 
(
	[BillId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

CREATE NONCLUSTERED INDEX [IX_FK_DealerBill] ON [dbo].[Bills] 
(
	[DealerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO

CREATE NONCLUSTERED INDEX [IX_FK_EmployeeBill] ON [dbo].[Bills] 
(
	[DispatchedById] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO

CREATE NONCLUSTERED INDEX [IX_FK_IndentBill] ON [dbo].[Bills] 
(
	[IndentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
SET IDENTITY_INSERT [dbo].[Bills] ON 

GO
INSERT [dbo].[Bills] ([BillId], [DateOfIssue], [IndentId], [TransportType], [TransportCost], [VehicleNo], [EmergencyContactNo], [TotalProductCost], [TotalPaid], [PaymentDeadline], [BillStatus], [HasProductLoss], [ProductLossCost], [DispatchedById], [BillCode], [HasSalesReturn], [SalesReturnCost], [DealerId]) VALUES (1, CAST(0x0000A09A00AD7FF3 AS DateTime), 1, N'Truck', 1000, N'er123', N'01818881', 161000, 3400, CAST(0x0000A0B800AD8003 AS DateTime), N'Verified by RM Partially Paid', 0, 0, 4, N'bill01', NULL, NULL, 2)
GO
INSERT [dbo].[Bills] ([BillId], [DateOfIssue], [IndentId], [TransportType], [TransportCost], [VehicleNo], [EmergencyContactNo], [TotalProductCost], [TotalPaid], [PaymentDeadline], [BillStatus], [HasProductLoss], [ProductLossCost], [DispatchedById], [BillCode], [HasSalesReturn], [SalesReturnCost], [DealerId]) VALUES (2, CAST(0x0000A09A00AF11FB AS DateTime), 2, N'Truck', 100, N'1010110', N'101010010', 105000, 0, CAST(0x0000A0B800AF1211 AS DateTime), N'Verified By RM', 1, 0, 4, N'bill1', NULL, NULL, 2)
GO
SET IDENTITY_INSERT [dbo].[Bills] OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PLRProductInfoes](
	[PLRId] [bigint] NOT NULL,
	[ProductId] [bigint] NOT NULL,
	[LotId] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Quantity] [float] NOT NULL,
	[Remarks] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[LostAmount] [float] NULL,
	[State] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_PLRProductInfoes] PRIMARY KEY CLUSTERED 
(
	[PLRId] ASC,
	[ProductId] ASC,
	[LotId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

CREATE NONCLUSTERED INDEX [IX_FK_ProductPLRProductInfo] ON [dbo].[PLRProductInfoes] 
(
	[ProductId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
INSERT [dbo].[PLRProductInfoes] ([PLRId], [ProductId], [LotId], [Quantity], [Remarks], [LostAmount], [State]) VALUES (1, 1, N'12', 30, N'lost', 900, N'OnProcessing')
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PLRPackageInfoes](
	[PLRId] [bigint] NOT NULL,
	[PackageId] [bigint] NOT NULL,
	[Quantity] [float] NOT NULL,
	[Remarks] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[LostAmount] [float] NULL,
	[State] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_PLRPackageInfoes] PRIMARY KEY CLUSTERED 
(
	[PLRId] ASC,
	[PackageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

CREATE NONCLUSTERED INDEX [IX_FK_PackagePLRPackageInfo] ON [dbo].[PLRPackageInfoes] 
(
	[PackageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MRRProductInfoes](
	[MRRId] [bigint] NOT NULL,
	[ProductId] [bigint] NOT NULL,
	[LotId] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PlacedQuantity] [float] NOT NULL,
	[AcceptedQuantity] [float] NOT NULL,
	[PurchasePrice] [float] NULL,
 CONSTRAINT [PK_MRRProductInfoes] PRIMARY KEY CLUSTERED 
(
	[MRRId] ASC,
	[ProductId] ASC,
	[LotId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

CREATE NONCLUSTERED INDEX [IX_FK_ProductMRRProductInfo] ON [dbo].[MRRProductInfoes] 
(
	[ProductId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
INSERT [dbo].[MRRProductInfoes] ([MRRId], [ProductId], [LotId], [PlacedQuantity], [AcceptedQuantity], [PurchasePrice]) VALUES (1, 1, N'12', 1000, 1000, 30)
GO
INSERT [dbo].[MRRProductInfoes] ([MRRId], [ProductId], [LotId], [PlacedQuantity], [AcceptedQuantity], [PurchasePrice]) VALUES (1, 2, N'10', 1500, 1500, 35)
GO
INSERT [dbo].[MRRProductInfoes] ([MRRId], [ProductId], [LotId], [PlacedQuantity], [AcceptedQuantity], [PurchasePrice]) VALUES (1, 3, N'10', 2000, 2000, 40)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MRRPackageInfoes](
	[MRRId] [bigint] NOT NULL,
	[PackageId] [bigint] NOT NULL,
	[PlacedQuantity] [float] NOT NULL,
	[AcceptedQuantity] [float] NOT NULL,
	[PurchasePrice] [float] NULL,
 CONSTRAINT [PK_MRRPackageInfoes] PRIMARY KEY CLUSTERED 
(
	[MRRId] ASC,
	[PackageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

CREATE NONCLUSTERED INDEX [IX_FK_PackageMRRPackageInfo] ON [dbo].[MRRPackageInfoes] 
(
	[PackageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
INSERT [dbo].[MRRPackageInfoes] ([MRRId], [PackageId], [PlacedQuantity], [AcceptedQuantity], [PurchasePrice]) VALUES (2, 1, 1000, 1000, 25)
GO
INSERT [dbo].[MRRPackageInfoes] ([MRRId], [PackageId], [PlacedQuantity], [AcceptedQuantity], [PurchasePrice]) VALUES (2, 2, 1000, 1000, 20)
GO
INSERT [dbo].[MRRPackageInfoes] ([MRRId], [PackageId], [PlacedQuantity], [AcceptedQuantity], [PurchasePrice]) VALUES (2, 3, 1000, 1000, 10)
GO
INSERT [dbo].[MRRPackageInfoes] ([MRRId], [PackageId], [PlacedQuantity], [AcceptedQuantity], [PurchasePrice]) VALUES (3, 1, 120, 120, 12)
GO
INSERT [dbo].[MRRPackageInfoes] ([MRRId], [PackageId], [PlacedQuantity], [AcceptedQuantity], [PurchasePrice]) VALUES (3, 2, 150, 150, 10)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RequisitionProductInfoes](
	[RequisitionId] [bigint] NOT NULL,
	[ProductId] [bigint] NOT NULL,
	[LotId] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PlacedQuantity] [float] NOT NULL,
	[LotQuantity] [float] NOT NULL,
 CONSTRAINT [PK_RequisitionProductInfoes] PRIMARY KEY CLUSTERED 
(
	[RequisitionId] ASC,
	[ProductId] ASC,
	[LotId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

CREATE NONCLUSTERED INDEX [IX_FK_ProductRequisitionProductInfo] ON [dbo].[RequisitionProductInfoes] 
(
	[ProductId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
INSERT [dbo].[RequisitionProductInfoes] ([RequisitionId], [ProductId], [LotId], [PlacedQuantity], [LotQuantity]) VALUES (1, 1, N'12', 100, 100)
GO
INSERT [dbo].[RequisitionProductInfoes] ([RequisitionId], [ProductId], [LotId], [PlacedQuantity], [LotQuantity]) VALUES (1, 2, N'10', 100, 100)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RequisitionPackageInfoes](
	[RequisitionId] [bigint] NOT NULL,
	[PackageId] [bigint] NOT NULL,
	[PlacedQuantity] [float] NOT NULL,
	[AcceptedQuantity] [float] NOT NULL,
 CONSTRAINT [PK_RequisitionPackageInfoes] PRIMARY KEY CLUSTERED 
(
	[RequisitionId] ASC,
	[PackageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

CREATE NONCLUSTERED INDEX [IX_FK_PackageRequisitionPackageInfo] ON [dbo].[RequisitionPackageInfoes] 
(
	[PackageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
INSERT [dbo].[RequisitionPackageInfoes] ([RequisitionId], [PackageId], [PlacedQuantity], [AcceptedQuantity]) VALUES (1, 1, 80, 60)
GO
INSERT [dbo].[RequisitionPackageInfoes] ([RequisitionId], [PackageId], [PlacedQuantity], [AcceptedQuantity]) VALUES (1, 2, 90, 90)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransportationLosses](
	[BillId] [bigint] NOT NULL,
	[ProductId] [bigint] NOT NULL,
	[Remarks] [nvarchar](80) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[HasBalanced] [bit] NOT NULL,
	[LossQuantity] [float] NOT NULL,
	[UnitPrice] [float] NOT NULL,
	[BalancedQuantity] [float] NOT NULL,
 CONSTRAINT [PK_TransportationLosses] PRIMARY KEY CLUSTERED 
(
	[BillId] ASC,
	[ProductId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

CREATE NONCLUSTERED INDEX [IX_FK_ProductTransportationLoss] ON [dbo].[TransportationLosses] 
(
	[ProductId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
INSERT [dbo].[TransportationLosses] ([BillId], [ProductId], [Remarks], [HasBalanced], [LossQuantity], [UnitPrice], [BalancedQuantity]) VALUES (2, 1, N'RM Verified', 0, 10, 35, 0)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesReturnInfoes](
	[ProductId] [bigint] NOT NULL,
	[SalesReturnId] [bigint] NOT NULL,
	[LotId] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ProductPrice] [float] NOT NULL,
	[PlacedQuantity] [bigint] NOT NULL,
	[AcceptedQuantity] [bigint] NOT NULL,
	[BillId] [bigint] NOT NULL,
 CONSTRAINT [PK_SalesReturnInfoes] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC,
	[SalesReturnId] ASC,
	[LotId] ASC,
	[BillId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

CREATE NONCLUSTERED INDEX [IX_FK_BillSalesReturnInfoes] ON [dbo].[SalesReturnInfoes] 
(
	[BillId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO

CREATE NONCLUSTERED INDEX [IX_FK_SalesReturnSalesReturnInfo] ON [dbo].[SalesReturnInfoes] 
(
	[SalesReturnId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
INSERT [dbo].[SalesReturnInfoes] ([ProductId], [SalesReturnId], [LotId], [ProductPrice], [PlacedQuantity], [AcceptedQuantity], [BillId]) VALUES (1, 2, N'Return', 35, 10, 0, 1)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillPaymentInfoes](
	[BillPaymentId] [bigint] NOT NULL,
	[ProductId] [bigint] NOT NULL,
	[BillId] [bigint] NOT NULL,
	[Amount] [float] NOT NULL,
	[HasRejected] [bit] NOT NULL,
 CONSTRAINT [PK_BillPaymentInfoes] PRIMARY KEY CLUSTERED 
(
	[BillPaymentId] ASC,
	[ProductId] ASC,
	[BillId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

CREATE NONCLUSTERED INDEX [IX_FK_BillBillPaymentInfo] ON [dbo].[BillPaymentInfoes] 
(
	[BillId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO

CREATE NONCLUSTERED INDEX [IX_FK_ProductBillPaymentInfo] ON [dbo].[BillPaymentInfoes] 
(
	[ProductId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
INSERT [dbo].[BillPaymentInfoes] ([BillPaymentId], [ProductId], [BillId], [Amount], [HasRejected]) VALUES (5, 1, 1, 100, 0)
GO
INSERT [dbo].[BillPaymentInfoes] ([BillPaymentId], [ProductId], [BillId], [Amount], [HasRejected]) VALUES (5, 2, 1, 200, 0)
GO
INSERT [dbo].[BillPaymentInfoes] ([BillPaymentId], [ProductId], [BillId], [Amount], [HasRejected]) VALUES (5, 3, 1, 300, 0)
GO
INSERT [dbo].[BillPaymentInfoes] ([BillPaymentId], [ProductId], [BillId], [Amount], [HasRejected]) VALUES (7, 1, 2, 100, 1)
GO
INSERT [dbo].[BillPaymentInfoes] ([BillPaymentId], [ProductId], [BillId], [Amount], [HasRejected]) VALUES (7, 2, 2, 300, 1)
GO
INSERT [dbo].[BillPaymentInfoes] ([BillPaymentId], [ProductId], [BillId], [Amount], [HasRejected]) VALUES (8, 1, 1, 3400, 0)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillProductInfoes](
	[BillId] [bigint] NOT NULL,
	[ProductId] [bigint] NOT NULL,
	[ProductPrice] [float] NOT NULL,
	[LotId] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[LotQuantity] [float] NOT NULL,
	[ComissionPercentage] [float] NOT NULL,
 CONSTRAINT [PK_BillProductInfoes] PRIMARY KEY CLUSTERED 
(
	[BillId] ASC,
	[ProductId] ASC,
	[LotId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

CREATE NONCLUSTERED INDEX [IX_FK_ProductBillProductInfo] ON [dbo].[BillProductInfoes] 
(
	[ProductId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
INSERT [dbo].[BillProductInfoes] ([BillId], [ProductId], [ProductPrice], [LotId], [LotQuantity], [ComissionPercentage]) VALUES (1, 1, 35, N'12', 100, 0)
GO
INSERT [dbo].[BillProductInfoes] ([BillId], [ProductId], [ProductPrice], [LotId], [LotQuantity], [ComissionPercentage]) VALUES (1, 2, 28, N'10', 80, 0)
GO
INSERT [dbo].[BillProductInfoes] ([BillId], [ProductId], [ProductPrice], [LotId], [LotQuantity], [ComissionPercentage]) VALUES (2, 1, 35, N'12', 100, 0)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DueInfoes](
	[DealerId] [bigint] NOT NULL,
	[BillId] [bigint] NOT NULL,
	[ProductId] [bigint] NOT NULL,
	[Due] [float] NOT NULL,
	[DueStatus] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_DueInfoes] PRIMARY KEY CLUSTERED 
(
	[BillId] ASC,
	[ProductId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO

CREATE NONCLUSTERED INDEX [IX_FK_DealerDueInfo] ON [dbo].[DueInfoes] 
(
	[DealerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO

CREATE NONCLUSTERED INDEX [IX_FK_ProductDueInfo] ON [dbo].[DueInfoes] 
(
	[ProductId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
INSERT [dbo].[DueInfoes] ([DealerId], [BillId], [ProductId], [Due], [DueStatus]) VALUES (2, 1, 1, 105000, N'Unpaid')
GO
INSERT [dbo].[DueInfoes] ([DealerId], [BillId], [ProductId], [Due], [DueStatus]) VALUES (2, 1, 2, 56000, N'Unpaid')
GO
INSERT [dbo].[DueInfoes] ([DealerId], [BillId], [ProductId], [Due], [DueStatus]) VALUES (2, 2, 1, 105000, N'Unpaid')
GO
ALTER TABLE [dbo].[Promotions]  WITH CHECK ADD  CONSTRAINT [FK_ProductPromotion] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[Promotions] CHECK CONSTRAINT [FK_ProductPromotion]
GO
ALTER TABLE [dbo].[RegionTargets]  WITH CHECK ADD  CONSTRAINT [FK_ProductRegionTarget] FOREIGN KEY([Product_ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[RegionTargets] CHECK CONSTRAINT [FK_ProductRegionTarget]
GO
ALTER TABLE [dbo].[RegionTargets]  WITH CHECK ADD  CONSTRAINT [FK_RegionRegionTarget] FOREIGN KEY([RegionId])
REFERENCES [dbo].[Regions] ([RegionId])
GO
ALTER TABLE [dbo].[RegionTargets] CHECK CONSTRAINT [FK_RegionRegionTarget]
GO
ALTER TABLE [dbo].[Ledgers]  WITH CHECK ADD  CONSTRAINT [FK_ProductLedger] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[Ledgers] CHECK CONSTRAINT [FK_ProductLedger]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeRegion] FOREIGN KEY([RegionId])
REFERENCES [dbo].[Regions] ([RegionId])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_EmployeeRegion]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_PersonEmployee] FOREIGN KEY([PersonId])
REFERENCES [dbo].[People] ([PersonId])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_PersonEmployee]
GO
ALTER TABLE [dbo].[Dealers]  WITH CHECK ADD  CONSTRAINT [FK_DealerRegion] FOREIGN KEY([RegionId])
REFERENCES [dbo].[Regions] ([RegionId])
GO
ALTER TABLE [dbo].[Dealers] CHECK CONSTRAINT [FK_DealerRegion]
GO
ALTER TABLE [dbo].[Dealers]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeDealer] FOREIGN KEY([SalesOfficerId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[Dealers] CHECK CONSTRAINT [FK_EmployeeDealer]
GO
ALTER TABLE [dbo].[Commissions]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeApproveCommission] FOREIGN KEY([AdminId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[Commissions] CHECK CONSTRAINT [FK_EmployeeApproveCommission]
GO
ALTER TABLE [dbo].[Commissions]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeCreateCommission] FOREIGN KEY([NSMId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[Commissions] CHECK CONSTRAINT [FK_EmployeeCreateCommission]
GO
ALTER TABLE [dbo].[Commissions]  WITH CHECK ADD  CONSTRAINT [FK_ProductCommission] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[Commissions] CHECK CONSTRAINT [FK_ProductCommission]
GO
ALTER TABLE [dbo].[Inventories]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeInventory] FOREIGN KEY([StoreInChargeId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[Inventories] CHECK CONSTRAINT [FK_EmployeeInventory]
GO
ALTER TABLE [dbo].[Inventories]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeInventory1] FOREIGN KEY([DispatchOfficerId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[Inventories] CHECK CONSTRAINT [FK_EmployeeInventory1]
GO
ALTER TABLE [dbo].[Inventories]  WITH CHECK ADD  CONSTRAINT [FK_RegionInventory] FOREIGN KEY([RegionId])
REFERENCES [dbo].[Regions] ([RegionId])
GO
ALTER TABLE [dbo].[Inventories] CHECK CONSTRAINT [FK_RegionInventory]
GO
ALTER TABLE [dbo].[ProductEdits]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeApprovesProductEdits] FOREIGN KEY([AdminId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[ProductEdits] CHECK CONSTRAINT [FK_EmployeeApprovesProductEdits]
GO
ALTER TABLE [dbo].[ProductEdits]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeProductEdit] FOREIGN KEY([NSMId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[ProductEdits] CHECK CONSTRAINT [FK_EmployeeProductEdit]
GO
ALTER TABLE [dbo].[ProductEdits]  WITH CHECK ADD  CONSTRAINT [FK_ProductProductEdit] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[ProductEdits] CHECK CONSTRAINT [FK_ProductProductEdit]
GO
ALTER TABLE [dbo].[Packages]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeApprovedPackage] FOREIGN KEY([AdminId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[Packages] CHECK CONSTRAINT [FK_EmployeeApprovedPackage]
GO
ALTER TABLE [dbo].[Packages]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeCreatedPackage] FOREIGN KEY([NSMId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[Packages] CHECK CONSTRAINT [FK_EmployeeCreatedPackage]
GO
ALTER TABLE [dbo].[NoticeBoards]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeNoticeBoard] FOREIGN KEY([IssuedById])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[NoticeBoards] CHECK CONSTRAINT [FK_EmployeeNoticeBoard]
GO
ALTER TABLE [dbo].[Expenditures]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeExpenditure] FOREIGN KEY([PlacedById])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[Expenditures] CHECK CONSTRAINT [FK_EmployeeExpenditure]
GO
ALTER TABLE [dbo].[Expenditures]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeExpenditure1] FOREIGN KEY([PlacedToId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[Expenditures] CHECK CONSTRAINT [FK_EmployeeExpenditure1]
GO
ALTER TABLE [dbo].[Expenditures]  WITH CHECK ADD  CONSTRAINT [FK_RegionExpenditure] FOREIGN KEY([RegionId])
REFERENCES [dbo].[Regions] ([RegionId])
GO
ALTER TABLE [dbo].[Expenditures] CHECK CONSTRAINT [FK_RegionExpenditure]
GO
ALTER TABLE [dbo].[MessageDeliveries]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeMessageDelivery] FOREIGN KEY([SendFromId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[MessageDeliveries] CHECK CONSTRAINT [FK_EmployeeMessageDelivery]
GO
ALTER TABLE [dbo].[MessageDeliveries]  WITH CHECK ADD  CONSTRAINT [FK_MessageDeliveryEmployee] FOREIGN KEY([SendToId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[MessageDeliveries] CHECK CONSTRAINT [FK_MessageDeliveryEmployee]
GO
ALTER TABLE [dbo].[MessageDeliveries]  WITH CHECK ADD  CONSTRAINT [FK_MessageDeliveryMessage] FOREIGN KEY([MessageId])
REFERENCES [dbo].[Messages] ([MessageId])
GO
ALTER TABLE [dbo].[MessageDeliveries] CHECK CONSTRAINT [FK_MessageDeliveryMessage]
GO
ALTER TABLE [dbo].[SalesOfficerTargets]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeSalesOfficerTarget] FOREIGN KEY([SalesOfficerId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[SalesOfficerTargets] CHECK CONSTRAINT [FK_EmployeeSalesOfficerTarget]
GO
ALTER TABLE [dbo].[SalesOfficerTargets]  WITH CHECK ADD  CONSTRAINT [FK_SalesOfficerTargetProduct] FOREIGN KEY([Product_ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[SalesOfficerTargets] CHECK CONSTRAINT [FK_SalesOfficerTargetProduct]
GO
ALTER TABLE [dbo].[YearSummarySOExpenditures]  WITH CHECK ADD  CONSTRAINT [FK_YearSummarySOExpenditureEmployee] FOREIGN KEY([SalesOfficerId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[YearSummarySOExpenditures] CHECK CONSTRAINT [FK_YearSummarySOExpenditureEmployee]
GO
ALTER TABLE [dbo].[SalesReturns]  WITH CHECK ADD  CONSTRAINT [FK_DealerSalesReturn] FOREIGN KEY([DealerId])
REFERENCES [dbo].[Dealers] ([DealerId])
GO
ALTER TABLE [dbo].[SalesReturns] CHECK CONSTRAINT [FK_DealerSalesReturn]
GO
ALTER TABLE [dbo].[SalesReturns]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeSalesReturn] FOREIGN KEY([RMId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[SalesReturns] CHECK CONSTRAINT [FK_EmployeeSalesReturn]
GO
ALTER TABLE [dbo].[YearSummaryInventoryProducts]  WITH CHECK ADD  CONSTRAINT [FK_InventoryYearSummaryInventoryProduct] FOREIGN KEY([InventoryId])
REFERENCES [dbo].[Inventories] ([InventoryId])
GO
ALTER TABLE [dbo].[YearSummaryInventoryProducts] CHECK CONSTRAINT [FK_InventoryYearSummaryInventoryProduct]
GO
ALTER TABLE [dbo].[YearSummaryInventoryProducts]  WITH CHECK ADD  CONSTRAINT [FK_ProductYearSummaryInventoryProduct] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[YearSummaryInventoryProducts] CHECK CONSTRAINT [FK_ProductYearSummaryInventoryProduct]
GO
ALTER TABLE [dbo].[YearSummaryInventoryPackages]  WITH CHECK ADD  CONSTRAINT [FK_InventoryYearSummaryInventoryPackage] FOREIGN KEY([InventoryId])
REFERENCES [dbo].[Inventories] ([InventoryId])
GO
ALTER TABLE [dbo].[YearSummaryInventoryPackages] CHECK CONSTRAINT [FK_InventoryYearSummaryInventoryPackage]
GO
ALTER TABLE [dbo].[YearSummaryInventoryPackages]  WITH CHECK ADD  CONSTRAINT [FK_PackageYearSummaryInventoryPackage] FOREIGN KEY([PackageId])
REFERENCES [dbo].[Packages] ([PackageId])
GO
ALTER TABLE [dbo].[YearSummaryInventoryPackages] CHECK CONSTRAINT [FK_PackageYearSummaryInventoryPackage]
GO
ALTER TABLE [dbo].[YearSummaryDealers]  WITH CHECK ADD  CONSTRAINT [FK_ProductYearSummaryDealer] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[YearSummaryDealers] CHECK CONSTRAINT [FK_ProductYearSummaryDealer]
GO
ALTER TABLE [dbo].[YearSummaryDealers]  WITH CHECK ADD  CONSTRAINT [FK_YearSummaryDealerDealer] FOREIGN KEY([DealerId])
REFERENCES [dbo].[Dealers] ([DealerId])
GO
ALTER TABLE [dbo].[YearSummaryDealers] CHECK CONSTRAINT [FK_YearSummaryDealerDealer]
GO
ALTER TABLE [dbo].[Requisitions]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeRequisition] FOREIGN KEY([IssuedById])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[Requisitions] CHECK CONSTRAINT [FK_EmployeeRequisition]
GO
ALTER TABLE [dbo].[Requisitions]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeRequisition1] FOREIGN KEY([IssuedToId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[Requisitions] CHECK CONSTRAINT [FK_EmployeeRequisition1]
GO
ALTER TABLE [dbo].[Requisitions]  WITH CHECK ADD  CONSTRAINT [FK_InventoryRequisition] FOREIGN KEY([InventoryId])
REFERENCES [dbo].[Inventories] ([InventoryId])
GO
ALTER TABLE [dbo].[Requisitions] CHECK CONSTRAINT [FK_InventoryRequisition]
GO
ALTER TABLE [dbo].[InventoryProductInfoes]  WITH CHECK ADD  CONSTRAINT [FK_InventoryInventoryProductInfo] FOREIGN KEY([InventoryId])
REFERENCES [dbo].[Inventories] ([InventoryId])
GO
ALTER TABLE [dbo].[InventoryProductInfoes] CHECK CONSTRAINT [FK_InventoryInventoryProductInfo]
GO
ALTER TABLE [dbo].[InventoryProductInfoes]  WITH CHECK ADD  CONSTRAINT [FK_ProductInventoryProductInfo] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[InventoryProductInfoes] CHECK CONSTRAINT [FK_ProductInventoryProductInfo]
GO
ALTER TABLE [dbo].[InventoryPackageInfoes]  WITH CHECK ADD  CONSTRAINT [FK_InventoryInventoryPackageInfo] FOREIGN KEY([InventoryId])
REFERENCES [dbo].[Inventories] ([InventoryId])
GO
ALTER TABLE [dbo].[InventoryPackageInfoes] CHECK CONSTRAINT [FK_InventoryInventoryPackageInfo]
GO
ALTER TABLE [dbo].[InventoryPackageInfoes]  WITH CHECK ADD  CONSTRAINT [FK_PackageInventoryPackageInfo] FOREIGN KEY([PackageId])
REFERENCES [dbo].[Packages] ([PackageId])
GO
ALTER TABLE [dbo].[InventoryPackageInfoes] CHECK CONSTRAINT [FK_PackageInventoryPackageInfo]
GO
ALTER TABLE [dbo].[InventoryLogs]  WITH CHECK ADD  CONSTRAINT [FK_InventoryInventoryLog] FOREIGN KEY([InventoryId])
REFERENCES [dbo].[Inventories] ([InventoryId])
GO
ALTER TABLE [dbo].[InventoryLogs] CHECK CONSTRAINT [FK_InventoryInventoryLog]
GO
ALTER TABLE [dbo].[InventoryLogs]  WITH CHECK ADD  CONSTRAINT [FK_ProductInventoryLog] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[InventoryLogs] CHECK CONSTRAINT [FK_ProductInventoryLog]
GO
ALTER TABLE [dbo].[ExpenditureInfoes]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeExpenditureInfo] FOREIGN KEY([SalesOfficerId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[ExpenditureInfoes] CHECK CONSTRAINT [FK_EmployeeExpenditureInfo]
GO
ALTER TABLE [dbo].[ExpenditureInfoes]  WITH CHECK ADD  CONSTRAINT [FK_ExpenditureExpenditureInfo] FOREIGN KEY([ExpenditureId])
REFERENCES [dbo].[Expenditures] ([ExpenditureId])
GO
ALTER TABLE [dbo].[ExpenditureInfoes] CHECK CONSTRAINT [FK_ExpenditureExpenditureInfo]
GO
ALTER TABLE [dbo].[BillPayments]  WITH CHECK ADD  CONSTRAINT [FK_DealerBillPayment] FOREIGN KEY([DealerId])
REFERENCES [dbo].[Dealers] ([DealerId])
GO
ALTER TABLE [dbo].[BillPayments] CHECK CONSTRAINT [FK_DealerBillPayment]
GO
ALTER TABLE [dbo].[BillPayments]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeBillPayment] FOREIGN KEY([RMId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[BillPayments] CHECK CONSTRAINT [FK_EmployeeBillPayment]
GO
ALTER TABLE [dbo].[MRRs]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeMRR] FOREIGN KEY([IssuedById])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[MRRs] CHECK CONSTRAINT [FK_EmployeeMRR]
GO
ALTER TABLE [dbo].[MRRs]  WITH CHECK ADD  CONSTRAINT [FK_InventoryMRR] FOREIGN KEY([InventoryId])
REFERENCES [dbo].[Inventories] ([InventoryId])
GO
ALTER TABLE [dbo].[MRRs] CHECK CONSTRAINT [FK_InventoryMRR]
GO
ALTER TABLE [dbo].[PLRs]  WITH CHECK ADD  CONSTRAINT [FK_EmployeePLR] FOREIGN KEY([IssuedById])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[PLRs] CHECK CONSTRAINT [FK_EmployeePLR]
GO
ALTER TABLE [dbo].[PLRs]  WITH CHECK ADD  CONSTRAINT [FK_EmployeePLR1] FOREIGN KEY([IssuedToId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[PLRs] CHECK CONSTRAINT [FK_EmployeePLR1]
GO
ALTER TABLE [dbo].[PLRs]  WITH CHECK ADD  CONSTRAINT [FK_InventoryPLR] FOREIGN KEY([InventoryId])
REFERENCES [dbo].[Inventories] ([InventoryId])
GO
ALTER TABLE [dbo].[PLRs] CHECK CONSTRAINT [FK_InventoryPLR]
GO
ALTER TABLE [dbo].[Indents]  WITH CHECK ADD  CONSTRAINT [FK_DealerPlacesIndent] FOREIGN KEY([IssuedById])
REFERENCES [dbo].[Dealers] ([DealerId])
GO
ALTER TABLE [dbo].[Indents] CHECK CONSTRAINT [FK_DealerPlacesIndent]
GO
ALTER TABLE [dbo].[Indents]  WITH CHECK ADD  CONSTRAINT [FK_RMVerifiesIndent] FOREIGN KEY([IssuedToId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[Indents] CHECK CONSTRAINT [FK_RMVerifiesIndent]
GO
ALTER TABLE [dbo].[IndentProductInfoes]  WITH CHECK ADD  CONSTRAINT [FK_IndentProductInfoIndent] FOREIGN KEY([IndentId])
REFERENCES [dbo].[Indents] ([IndentId])
GO
ALTER TABLE [dbo].[IndentProductInfoes] CHECK CONSTRAINT [FK_IndentProductInfoIndent]
GO
ALTER TABLE [dbo].[IndentProductInfoes]  WITH CHECK ADD  CONSTRAINT [FK_IndentProductInfoProduct] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[IndentProductInfoes] CHECK CONSTRAINT [FK_IndentProductInfoProduct]
GO
ALTER TABLE [dbo].[Bills]  WITH CHECK ADD  CONSTRAINT [FK_DealerBill] FOREIGN KEY([DealerId])
REFERENCES [dbo].[Dealers] ([DealerId])
GO
ALTER TABLE [dbo].[Bills] CHECK CONSTRAINT [FK_DealerBill]
GO
ALTER TABLE [dbo].[Bills]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeBill] FOREIGN KEY([DispatchedById])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[Bills] CHECK CONSTRAINT [FK_EmployeeBill]
GO
ALTER TABLE [dbo].[Bills]  WITH CHECK ADD  CONSTRAINT [FK_IndentBill] FOREIGN KEY([IndentId])
REFERENCES [dbo].[Indents] ([IndentId])
GO
ALTER TABLE [dbo].[Bills] CHECK CONSTRAINT [FK_IndentBill]
GO
ALTER TABLE [dbo].[PLRProductInfoes]  WITH CHECK ADD  CONSTRAINT [FK_PLRPLRProductInfo] FOREIGN KEY([PLRId])
REFERENCES [dbo].[PLRs] ([PLRId])
GO
ALTER TABLE [dbo].[PLRProductInfoes] CHECK CONSTRAINT [FK_PLRPLRProductInfo]
GO
ALTER TABLE [dbo].[PLRProductInfoes]  WITH CHECK ADD  CONSTRAINT [FK_ProductPLRProductInfo] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[PLRProductInfoes] CHECK CONSTRAINT [FK_ProductPLRProductInfo]
GO
ALTER TABLE [dbo].[PLRPackageInfoes]  WITH CHECK ADD  CONSTRAINT [FK_PackagePLRPackageInfo] FOREIGN KEY([PackageId])
REFERENCES [dbo].[Packages] ([PackageId])
GO
ALTER TABLE [dbo].[PLRPackageInfoes] CHECK CONSTRAINT [FK_PackagePLRPackageInfo]
GO
ALTER TABLE [dbo].[PLRPackageInfoes]  WITH CHECK ADD  CONSTRAINT [FK_PLRPLRPackageInfo] FOREIGN KEY([PLRId])
REFERENCES [dbo].[PLRs] ([PLRId])
GO
ALTER TABLE [dbo].[PLRPackageInfoes] CHECK CONSTRAINT [FK_PLRPLRPackageInfo]
GO
ALTER TABLE [dbo].[MRRProductInfoes]  WITH CHECK ADD  CONSTRAINT [FK_MRRMRRProductInfo] FOREIGN KEY([MRRId])
REFERENCES [dbo].[MRRs] ([MRRId])
GO
ALTER TABLE [dbo].[MRRProductInfoes] CHECK CONSTRAINT [FK_MRRMRRProductInfo]
GO
ALTER TABLE [dbo].[MRRProductInfoes]  WITH CHECK ADD  CONSTRAINT [FK_ProductMRRProductInfo] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[MRRProductInfoes] CHECK CONSTRAINT [FK_ProductMRRProductInfo]
GO
ALTER TABLE [dbo].[MRRPackageInfoes]  WITH CHECK ADD  CONSTRAINT [FK_MRRMRRPackageInfo] FOREIGN KEY([MRRId])
REFERENCES [dbo].[MRRs] ([MRRId])
GO
ALTER TABLE [dbo].[MRRPackageInfoes] CHECK CONSTRAINT [FK_MRRMRRPackageInfo]
GO
ALTER TABLE [dbo].[MRRPackageInfoes]  WITH CHECK ADD  CONSTRAINT [FK_PackageMRRPackageInfo] FOREIGN KEY([PackageId])
REFERENCES [dbo].[Packages] ([PackageId])
GO
ALTER TABLE [dbo].[MRRPackageInfoes] CHECK CONSTRAINT [FK_PackageMRRPackageInfo]
GO
ALTER TABLE [dbo].[RequisitionProductInfoes]  WITH CHECK ADD  CONSTRAINT [FK_ProductRequisitionProductInfo] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[RequisitionProductInfoes] CHECK CONSTRAINT [FK_ProductRequisitionProductInfo]
GO
ALTER TABLE [dbo].[RequisitionProductInfoes]  WITH CHECK ADD  CONSTRAINT [FK_RequisitionRequisitionProductInfo] FOREIGN KEY([RequisitionId])
REFERENCES [dbo].[Requisitions] ([RequisitionId])
GO
ALTER TABLE [dbo].[RequisitionProductInfoes] CHECK CONSTRAINT [FK_RequisitionRequisitionProductInfo]
GO
ALTER TABLE [dbo].[RequisitionPackageInfoes]  WITH CHECK ADD  CONSTRAINT [FK_PackageRequisitionPackageInfo] FOREIGN KEY([PackageId])
REFERENCES [dbo].[Packages] ([PackageId])
GO
ALTER TABLE [dbo].[RequisitionPackageInfoes] CHECK CONSTRAINT [FK_PackageRequisitionPackageInfo]
GO
ALTER TABLE [dbo].[RequisitionPackageInfoes]  WITH CHECK ADD  CONSTRAINT [FK_RequisitionRequisitionPackageInfo] FOREIGN KEY([RequisitionId])
REFERENCES [dbo].[Requisitions] ([RequisitionId])
GO
ALTER TABLE [dbo].[RequisitionPackageInfoes] CHECK CONSTRAINT [FK_RequisitionRequisitionPackageInfo]
GO
ALTER TABLE [dbo].[TransportationLosses]  WITH CHECK ADD  CONSTRAINT [FK_BillTransportationLoss] FOREIGN KEY([BillId])
REFERENCES [dbo].[Bills] ([BillId])
GO
ALTER TABLE [dbo].[TransportationLosses] CHECK CONSTRAINT [FK_BillTransportationLoss]
GO
ALTER TABLE [dbo].[TransportationLosses]  WITH CHECK ADD  CONSTRAINT [FK_ProductTransportationLoss] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[TransportationLosses] CHECK CONSTRAINT [FK_ProductTransportationLoss]
GO
ALTER TABLE [dbo].[SalesReturnInfoes]  WITH CHECK ADD  CONSTRAINT [FK_BillSalesReturnInfoes] FOREIGN KEY([BillId])
REFERENCES [dbo].[Bills] ([BillId])
GO
ALTER TABLE [dbo].[SalesReturnInfoes] CHECK CONSTRAINT [FK_BillSalesReturnInfoes]
GO
ALTER TABLE [dbo].[SalesReturnInfoes]  WITH CHECK ADD  CONSTRAINT [FK_ProductSalesReturnInfo] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[SalesReturnInfoes] CHECK CONSTRAINT [FK_ProductSalesReturnInfo]
GO
ALTER TABLE [dbo].[SalesReturnInfoes]  WITH CHECK ADD  CONSTRAINT [FK_SalesReturnSalesReturnInfo] FOREIGN KEY([SalesReturnId])
REFERENCES [dbo].[SalesReturns] ([SalesReturnId])
GO
ALTER TABLE [dbo].[SalesReturnInfoes] CHECK CONSTRAINT [FK_SalesReturnSalesReturnInfo]
GO
ALTER TABLE [dbo].[BillPaymentInfoes]  WITH CHECK ADD  CONSTRAINT [FK_BillBillPaymentInfo] FOREIGN KEY([BillId])
REFERENCES [dbo].[Bills] ([BillId])
GO
ALTER TABLE [dbo].[BillPaymentInfoes] CHECK CONSTRAINT [FK_BillBillPaymentInfo]
GO
ALTER TABLE [dbo].[BillPaymentInfoes]  WITH CHECK ADD  CONSTRAINT [FK_BillPaymentBillPaymentInfo] FOREIGN KEY([BillPaymentId])
REFERENCES [dbo].[BillPayments] ([BillPaymentId])
GO
ALTER TABLE [dbo].[BillPaymentInfoes] CHECK CONSTRAINT [FK_BillPaymentBillPaymentInfo]
GO
ALTER TABLE [dbo].[BillPaymentInfoes]  WITH CHECK ADD  CONSTRAINT [FK_ProductBillPaymentInfo] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[BillPaymentInfoes] CHECK CONSTRAINT [FK_ProductBillPaymentInfo]
GO
ALTER TABLE [dbo].[BillProductInfoes]  WITH CHECK ADD  CONSTRAINT [FK_BillBillProductInfo] FOREIGN KEY([BillId])
REFERENCES [dbo].[Bills] ([BillId])
GO
ALTER TABLE [dbo].[BillProductInfoes] CHECK CONSTRAINT [FK_BillBillProductInfo]
GO
ALTER TABLE [dbo].[BillProductInfoes]  WITH CHECK ADD  CONSTRAINT [FK_ProductBillProductInfo] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[BillProductInfoes] CHECK CONSTRAINT [FK_ProductBillProductInfo]
GO
ALTER TABLE [dbo].[DueInfoes]  WITH CHECK ADD  CONSTRAINT [FK_BillDueInfo] FOREIGN KEY([BillId])
REFERENCES [dbo].[Bills] ([BillId])
GO
ALTER TABLE [dbo].[DueInfoes] CHECK CONSTRAINT [FK_BillDueInfo]
GO
ALTER TABLE [dbo].[DueInfoes]  WITH CHECK ADD  CONSTRAINT [FK_DealerDueInfo] FOREIGN KEY([DealerId])
REFERENCES [dbo].[Dealers] ([DealerId])
GO
ALTER TABLE [dbo].[DueInfoes] CHECK CONSTRAINT [FK_DealerDueInfo]
GO
ALTER TABLE [dbo].[DueInfoes]  WITH CHECK ADD  CONSTRAINT [FK_ProductDueInfo] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[DueInfoes] CHECK CONSTRAINT [FK_ProductDueInfo]
GO
