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
SET IDENTITY_INSERT [dbo].[Regions] ON 

GO
INSERT [dbo].[Regions] ([RegionId], [RegionName], [DistrictName]) VALUES (1, N'Barisal', N'Barisal')
GO
INSERT [dbo].[Regions] ([RegionId], [RegionName], [DistrictName]) VALUES (2, N'Dhaka', N'Dhaka')
GO
SET IDENTITY_INSERT [dbo].[Regions] OFF
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
SET IDENTITY_INSERT [dbo].[Inventories] ON 

GO
INSERT [dbo].[Inventories] ([InventoryId], [RegionId], [StoreInChargeId], [DispatchOfficerId]) VALUES (4, 1, 2, 4)
GO
SET IDENTITY_INSERT [dbo].[Inventories] OFF
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
SET IDENTITY_INSERT [dbo].[Expenditures] ON 

GO
INSERT [dbo].[Expenditures] ([ExpenditureId], [DateOfIssue], [RegionId], [PlacedById], [PlacedToId], [Status], [TotalPlacedAmount], [TotalAcceptedAmount], [ExpenditureCode]) VALUES (1, CAST(0x0000A09D000A5DF4 AS DateTime), 1, 1, 1, N'Pending', 2200, 0, N'7/29/2012 12:37:44 AM')
GO
SET IDENTITY_INSERT [dbo].[Expenditures] OFF
GO
SET IDENTITY_INSERT [dbo].[SalesReturns] ON 

GO
INSERT [dbo].[SalesReturns] ([SalesReturnId], [DateOfIssue], [DealerId], [RMId], [TotalPlacedAmount], [TotalAcceptedAmount], [SalesReturnCode], [Status]) VALUES (1, CAST(0x0000A09A00DAAE6F AS DateTime), 2, 1, 0, 0, N'7/26/2012 1:16:11 PM', N'Placed')
GO
INSERT [dbo].[SalesReturns] ([SalesReturnId], [DateOfIssue], [DealerId], [RMId], [TotalPlacedAmount], [TotalAcceptedAmount], [SalesReturnCode], [Status]) VALUES (2, CAST(0x0000A09C0180FBBC AS DateTime), 2, 1, 350, 0, N'7/28/2012 11:21:40 PM', N'Placed')
GO
SET IDENTITY_INSERT [dbo].[SalesReturns] OFF
GO
SET IDENTITY_INSERT [dbo].[Requisitions] ON 

GO
INSERT [dbo].[Requisitions] ([RequisitionId], [InventoryId], [DateOfIssue], [IssuedById], [IssuedToId], [Status], [DateOfApproval], [RequisitionType], [RequisitionCode]) VALUES (1, 4, CAST(0x0000A09A009D154C AS DateTime), 4, 2, N'Approved', CAST(0x0000A09A009D154D AS DateTime), N'Requisition For Packing', N'7/26/2012 9:31:55 AM')
GO
SET IDENTITY_INSERT [dbo].[Requisitions] OFF
GO
INSERT [dbo].[InventoryProductInfoes] ([InventoryId], [ProductId], [LotId], [UnfinishedQuantity], [OnProcessingQuantity], [FinishedQuantity], [UnitLotCost]) VALUES (4, 1, N'12', 900, 970, 4300, 30)
GO
INSERT [dbo].[InventoryProductInfoes] ([InventoryId], [ProductId], [LotId], [UnfinishedQuantity], [OnProcessingQuantity], [FinishedQuantity], [UnitLotCost]) VALUES (4, 2, N'10', 1400, 1000, 8000, 35)
GO
INSERT [dbo].[InventoryProductInfoes] ([InventoryId], [ProductId], [LotId], [UnfinishedQuantity], [OnProcessingQuantity], [FinishedQuantity], [UnitLotCost]) VALUES (4, 3, N'10', 2000, 1000, 10000, 40)
GO
INSERT [dbo].[InventoryPackageInfoes] ([InventoryId], [PackageId], [UnfinishedQuantity], [OnProcessingQuantity], [FinishedQuantity], [UnitCost]) VALUES (4, 1, 1060, 1000, 1000, 25)
GO
INSERT [dbo].[InventoryPackageInfoes] ([InventoryId], [PackageId], [UnfinishedQuantity], [OnProcessingQuantity], [FinishedQuantity], [UnitCost]) VALUES (4, 2, 1060, 1000, 1000, 20)
GO
INSERT [dbo].[InventoryPackageInfoes] ([InventoryId], [PackageId], [UnfinishedQuantity], [OnProcessingQuantity], [FinishedQuantity], [UnitCost]) VALUES (4, 3, 1000, 1000, 1000, 10)
GO
INSERT [dbo].[ExpenditureInfoes] ([ExpenditureId], [SalesOfficerId], [PlacedAmount], [Remarks], [NSMAcceptedAmount], [AccountAcceptedAmount]) VALUES (1, 8, 1000, N'Dealer Expense', 0, 0)
GO
INSERT [dbo].[ExpenditureInfoes] ([ExpenditureId], [SalesOfficerId], [PlacedAmount], [Remarks], [NSMAcceptedAmount], [AccountAcceptedAmount]) VALUES (1, 9, 1200, N'Food', 0, 0)
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
SET IDENTITY_INSERT [dbo].[PLRs] ON 

GO
INSERT [dbo].[PLRs] ([PLRId], [InventoryId], [DateOfIssue], [IssuedById], [IssuedToId], [Status], [DateOfApproval], [PLRCode]) VALUES (1, 4, CAST(0x0000A09A00ECDF30 AS DateTime), 2, 2, N'Verified', CAST(0x0000A09A00ECDF31 AS DateTime), N'7/26/2012 2:22:25 PM')
GO
SET IDENTITY_INSERT [dbo].[PLRs] OFF
GO
SET IDENTITY_INSERT [dbo].[Indents] ON 

GO
INSERT [dbo].[Indents] ([IndentId], [DateOfPlace], [IndentStatus], [IssuedById], [IssuedToId], [ForwardedToId], [PaymentMethod], [IsConsideredByNSM], [IndentCode]) VALUES (1, CAST(0x0000A09A009DD7F7 AS DateTime), N'Dispatched', 2, 1, 2, N'Cash', 0, N'indent123')
GO
INSERT [dbo].[Indents] ([IndentId], [DateOfPlace], [IndentStatus], [IssuedById], [IssuedToId], [ForwardedToId], [PaymentMethod], [IsConsideredByNSM], [IndentCode]) VALUES (2, CAST(0x0000A09A009DF2AA AS DateTime), N'Dispatched', 2, 1, 2, N'Advanced', 0, N'indent2')
GO
SET IDENTITY_INSERT [dbo].[Indents] OFF
GO
INSERT [dbo].[IndentProductInfoes] ([IndentId], [ProductId], [ProductPrice], [ProductQuantity], [EditTime], [CommissionPercentage], [ProductQuantityByRM], [ProductQuantityBySIC], [EditTimeRM], [EditTimeSIC]) VALUES (1, 1, 35, 100, CAST(0x0000A09A009DD80A AS DateTime), 0, 100, 100, CAST(0x0000A09A00AC60AF AS DateTime), CAST(0x0000A09A00AC9358 AS DateTime))
GO
INSERT [dbo].[IndentProductInfoes] ([IndentId], [ProductId], [ProductPrice], [ProductQuantity], [EditTime], [CommissionPercentage], [ProductQuantityByRM], [ProductQuantityBySIC], [EditTimeRM], [EditTimeSIC]) VALUES (1, 2, 28, 80, CAST(0x0000A09A009DD80D AS DateTime), 0, 80, 80, CAST(0x0000A09A00AC60AF AS DateTime), CAST(0x0000A09A00AC9358 AS DateTime))
GO
INSERT [dbo].[IndentProductInfoes] ([IndentId], [ProductId], [ProductPrice], [ProductQuantity], [EditTime], [CommissionPercentage], [ProductQuantityByRM], [ProductQuantityBySIC], [EditTimeRM], [EditTimeSIC]) VALUES (2, 1, 35, 100, CAST(0x0000A09A009DF2AA AS DateTime), 0, 100, 100, CAST(0x0000A09A00AC6B85 AS DateTime), CAST(0x0000A09A00AC9B08 AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Bills] ON 

GO
INSERT [dbo].[Bills] ([BillId], [DateOfIssue], [IndentId], [TransportType], [TransportCost], [VehicleNo], [EmergencyContactNo], [TotalProductCost], [TotalPaid], [PaymentDeadline], [BillStatus], [HasProductLoss], [ProductLossCost], [DispatchedById], [BillCode], [HasSalesReturn], [SalesReturnCost], [DealerId]) VALUES (1, CAST(0x0000A09A00AD7FF3 AS DateTime), 1, N'Truck', 1000, N'er123', N'01818881', 161000, 3400, CAST(0x0000A0B800AD8003 AS DateTime), N'Verified by RM Partially Paid', 0, 0, 4, N'bill01', NULL, NULL, 2)
GO
INSERT [dbo].[Bills] ([BillId], [DateOfIssue], [IndentId], [TransportType], [TransportCost], [VehicleNo], [EmergencyContactNo], [TotalProductCost], [TotalPaid], [PaymentDeadline], [BillStatus], [HasProductLoss], [ProductLossCost], [DispatchedById], [BillCode], [HasSalesReturn], [SalesReturnCost], [DealerId]) VALUES (2, CAST(0x0000A09A00AF11FB AS DateTime), 2, N'Truck', 100, N'1010110', N'101010010', 105000, 0, CAST(0x0000A0B800AF1211 AS DateTime), N'Verified By RM', 1, 0, 4, N'bill1', NULL, NULL, 2)
GO
SET IDENTITY_INSERT [dbo].[Bills] OFF
GO
INSERT [dbo].[PLRProductInfoes] ([PLRId], [ProductId], [LotId], [Quantity], [Remarks], [LostAmount], [State]) VALUES (1, 1, N'12', 30, N'lost', 900, N'OnProcessing')
GO
INSERT [dbo].[MRRProductInfoes] ([MRRId], [ProductId], [LotId], [PlacedQuantity], [AcceptedQuantity], [PurchasePrice]) VALUES (1, 1, N'12', 1000, 1000, 30)
GO
INSERT [dbo].[MRRProductInfoes] ([MRRId], [ProductId], [LotId], [PlacedQuantity], [AcceptedQuantity], [PurchasePrice]) VALUES (1, 2, N'10', 1500, 1500, 35)
GO
INSERT [dbo].[MRRProductInfoes] ([MRRId], [ProductId], [LotId], [PlacedQuantity], [AcceptedQuantity], [PurchasePrice]) VALUES (1, 3, N'10', 2000, 2000, 40)
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
INSERT [dbo].[RequisitionProductInfoes] ([RequisitionId], [ProductId], [LotId], [PlacedQuantity], [LotQuantity]) VALUES (1, 1, N'12', 100, 100)
GO
INSERT [dbo].[RequisitionProductInfoes] ([RequisitionId], [ProductId], [LotId], [PlacedQuantity], [LotQuantity]) VALUES (1, 2, N'10', 100, 100)
GO
INSERT [dbo].[RequisitionPackageInfoes] ([RequisitionId], [PackageId], [PlacedQuantity], [AcceptedQuantity]) VALUES (1, 1, 80, 60)
GO
INSERT [dbo].[RequisitionPackageInfoes] ([RequisitionId], [PackageId], [PlacedQuantity], [AcceptedQuantity]) VALUES (1, 2, 90, 90)
GO
INSERT [dbo].[TransportationLosses] ([BillId], [ProductId], [Remarks], [HasBalanced], [LossQuantity], [UnitPrice], [BalancedQuantity]) VALUES (2, 1, N'RM Verified', 0, 10, 35, 0)
GO
INSERT [dbo].[SalesReturnInfoes] ([ProductId], [SalesReturnId], [LotId], [ProductPrice], [PlacedQuantity], [AcceptedQuantity], [BillId]) VALUES (1, 2, N'Return', 35, 10, 0, 1)
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
INSERT [dbo].[BillProductInfoes] ([BillId], [ProductId], [ProductPrice], [LotId], [LotQuantity], [ComissionPercentage]) VALUES (1, 1, 35, N'12', 100, 0)
GO
INSERT [dbo].[BillProductInfoes] ([BillId], [ProductId], [ProductPrice], [LotId], [LotQuantity], [ComissionPercentage]) VALUES (1, 2, 28, N'10', 80, 0)
GO
INSERT [dbo].[BillProductInfoes] ([BillId], [ProductId], [ProductPrice], [LotId], [LotQuantity], [ComissionPercentage]) VALUES (2, 1, 35, N'12', 100, 0)
GO
INSERT [dbo].[DueInfoes] ([DealerId], [BillId], [ProductId], [Due], [DueStatus]) VALUES (2, 1, 1, 105000, N'Unpaid')
GO
INSERT [dbo].[DueInfoes] ([DealerId], [BillId], [ProductId], [Due], [DueStatus]) VALUES (2, 1, 2, 56000, N'Unpaid')
GO
INSERT [dbo].[DueInfoes] ([DealerId], [BillId], [ProductId], [Due], [DueStatus]) VALUES (2, 2, 1, 105000, N'Unpaid')
GO
