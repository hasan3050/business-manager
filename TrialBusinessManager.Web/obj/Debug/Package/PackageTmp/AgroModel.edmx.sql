
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 10/07/2012 19:58:59
-- Generated from EDMX file: D:\Works\BusinessManagerVersions\Somee Version\Version5\businessmanager_original\TrialBusinessManager.Web\AgroModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [IspahaniDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_BillBillPaymentInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BillPaymentInfoes] DROP CONSTRAINT [FK_BillBillPaymentInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_BillBillProductInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BillProductInfoes] DROP CONSTRAINT [FK_BillBillProductInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_BillDueInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DueInfoes] DROP CONSTRAINT [FK_BillDueInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_BillPaymentBillPaymentInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BillPaymentInfoes] DROP CONSTRAINT [FK_BillPaymentBillPaymentInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_BillSalesReturnInfoes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SalesReturnInfoes] DROP CONSTRAINT [FK_BillSalesReturnInfoes];
GO
IF OBJECT_ID(N'[dbo].[FK_BillTransportationLoss]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TransportationLosses] DROP CONSTRAINT [FK_BillTransportationLoss];
GO
IF OBJECT_ID(N'[dbo].[FK_DealerBill]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Bills] DROP CONSTRAINT [FK_DealerBill];
GO
IF OBJECT_ID(N'[dbo].[FK_DealerBillPayment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BillPayments] DROP CONSTRAINT [FK_DealerBillPayment];
GO
IF OBJECT_ID(N'[dbo].[FK_DealerDueInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DueInfoes] DROP CONSTRAINT [FK_DealerDueInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_DealerPlacesIndent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Indents] DROP CONSTRAINT [FK_DealerPlacesIndent];
GO
IF OBJECT_ID(N'[dbo].[FK_DealerRegion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Dealers] DROP CONSTRAINT [FK_DealerRegion];
GO
IF OBJECT_ID(N'[dbo].[FK_DealerSalesReturn]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SalesReturns] DROP CONSTRAINT [FK_DealerSalesReturn];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeApproveCommission]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Commissions] DROP CONSTRAINT [FK_EmployeeApproveCommission];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeApprovedPackage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Packages] DROP CONSTRAINT [FK_EmployeeApprovedPackage];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeApprovesProductEdits]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductEdits] DROP CONSTRAINT [FK_EmployeeApprovesProductEdits];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeBill]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Bills] DROP CONSTRAINT [FK_EmployeeBill];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeBillPayment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BillPayments] DROP CONSTRAINT [FK_EmployeeBillPayment];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeCreateCommission]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Commissions] DROP CONSTRAINT [FK_EmployeeCreateCommission];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeCreatedPackage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Packages] DROP CONSTRAINT [FK_EmployeeCreatedPackage];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeDealer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Dealers] DROP CONSTRAINT [FK_EmployeeDealer];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeExpenditure]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Expenditures] DROP CONSTRAINT [FK_EmployeeExpenditure];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeExpenditure1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Expenditures] DROP CONSTRAINT [FK_EmployeeExpenditure1];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeExpenditureInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ExpenditureInfoes] DROP CONSTRAINT [FK_EmployeeExpenditureInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeInventory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Inventories] DROP CONSTRAINT [FK_EmployeeInventory];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeInventory1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Inventories] DROP CONSTRAINT [FK_EmployeeInventory1];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeMessageDelivery]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MessageDeliveries] DROP CONSTRAINT [FK_EmployeeMessageDelivery];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeMRR]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MRRs] DROP CONSTRAINT [FK_EmployeeMRR];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeNoticeBoard]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NoticeBoards] DROP CONSTRAINT [FK_EmployeeNoticeBoard];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeePLR]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PLRs] DROP CONSTRAINT [FK_EmployeePLR];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeePLR1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PLRs] DROP CONSTRAINT [FK_EmployeePLR1];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeProductEdit]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductEdits] DROP CONSTRAINT [FK_EmployeeProductEdit];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeRegion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Employees] DROP CONSTRAINT [FK_EmployeeRegion];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeRequisition]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Requisitions] DROP CONSTRAINT [FK_EmployeeRequisition];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeRequisition1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Requisitions] DROP CONSTRAINT [FK_EmployeeRequisition1];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeSalesOfficerTarget]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SalesOfficerTargets] DROP CONSTRAINT [FK_EmployeeSalesOfficerTarget];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeSalesReturn]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SalesReturns] DROP CONSTRAINT [FK_EmployeeSalesReturn];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeTransferIndents]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TransferIndents] DROP CONSTRAINT [FK_EmployeeTransferIndents];
GO
IF OBJECT_ID(N'[dbo].[FK_ExpenditureExpenditureInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ExpenditureInfoes] DROP CONSTRAINT [FK_ExpenditureExpenditureInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_IndentBill]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Bills] DROP CONSTRAINT [FK_IndentBill];
GO
IF OBJECT_ID(N'[dbo].[FK_IndentProductInfoIndent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IndentProductInfoes] DROP CONSTRAINT [FK_IndentProductInfoIndent];
GO
IF OBJECT_ID(N'[dbo].[FK_IndentProductInfoProduct]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IndentProductInfoes] DROP CONSTRAINT [FK_IndentProductInfoProduct];
GO
IF OBJECT_ID(N'[dbo].[FK_InventoryInventoryLog]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InventoryLogs] DROP CONSTRAINT [FK_InventoryInventoryLog];
GO
IF OBJECT_ID(N'[dbo].[FK_InventoryInventoryPackageInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InventoryPackageInfoes] DROP CONSTRAINT [FK_InventoryInventoryPackageInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_InventoryInventoryProductInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InventoryProductInfoes] DROP CONSTRAINT [FK_InventoryInventoryProductInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_InventoryMRR]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MRRs] DROP CONSTRAINT [FK_InventoryMRR];
GO
IF OBJECT_ID(N'[dbo].[FK_InventoryPLR]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PLRs] DROP CONSTRAINT [FK_InventoryPLR];
GO
IF OBJECT_ID(N'[dbo].[FK_InventoryRequisition]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Requisitions] DROP CONSTRAINT [FK_InventoryRequisition];
GO
IF OBJECT_ID(N'[dbo].[FK_InventorySalesReturns]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SalesReturns] DROP CONSTRAINT [FK_InventorySalesReturns];
GO
IF OBJECT_ID(N'[dbo].[FK_InventoryTransferIndents]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TransferIndents] DROP CONSTRAINT [FK_InventoryTransferIndents];
GO
IF OBJECT_ID(N'[dbo].[FK_InventoryYearSummaryInventoryPackage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[YearSummaryInventoryPackages] DROP CONSTRAINT [FK_InventoryYearSummaryInventoryPackage];
GO
IF OBJECT_ID(N'[dbo].[FK_InventoryYearSummaryInventoryProduct]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[YearSummaryInventoryProducts] DROP CONSTRAINT [FK_InventoryYearSummaryInventoryProduct];
GO
IF OBJECT_ID(N'[dbo].[FK_MessageDeliveryEmployee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MessageDeliveries] DROP CONSTRAINT [FK_MessageDeliveryEmployee];
GO
IF OBJECT_ID(N'[dbo].[FK_MessageDeliveryMessage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MessageDeliveries] DROP CONSTRAINT [FK_MessageDeliveryMessage];
GO
IF OBJECT_ID(N'[dbo].[FK_MRRMRRPackageInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MRRPackageInfoes] DROP CONSTRAINT [FK_MRRMRRPackageInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_MRRMRRProductInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MRRProductInfoes] DROP CONSTRAINT [FK_MRRMRRProductInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_PackageInventoryPackageInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InventoryPackageInfoes] DROP CONSTRAINT [FK_PackageInventoryPackageInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_PackageMRRPackageInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MRRPackageInfoes] DROP CONSTRAINT [FK_PackageMRRPackageInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_PackagePLRPackageInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PLRPackageInfoes] DROP CONSTRAINT [FK_PackagePLRPackageInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_PackageRequisitionPackageInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RequisitionPackageInfoes] DROP CONSTRAINT [FK_PackageRequisitionPackageInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_PackageYearSummaryInventoryPackage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[YearSummaryInventoryPackages] DROP CONSTRAINT [FK_PackageYearSummaryInventoryPackage];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonEmployee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Employees] DROP CONSTRAINT [FK_PersonEmployee];
GO
IF OBJECT_ID(N'[dbo].[FK_PLRPLRPackageInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PLRPackageInfoes] DROP CONSTRAINT [FK_PLRPLRPackageInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_PLRPLRProductInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PLRProductInfoes] DROP CONSTRAINT [FK_PLRPLRProductInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductBillPaymentInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BillPaymentInfoes] DROP CONSTRAINT [FK_ProductBillPaymentInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductBillProductInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BillProductInfoes] DROP CONSTRAINT [FK_ProductBillProductInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductCommission]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Commissions] DROP CONSTRAINT [FK_ProductCommission];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductDueInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DueInfoes] DROP CONSTRAINT [FK_ProductDueInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductInventoryLog]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InventoryLogs] DROP CONSTRAINT [FK_ProductInventoryLog];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductInventoryProductInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InventoryProductInfoes] DROP CONSTRAINT [FK_ProductInventoryProductInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductLedger]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Ledgers] DROP CONSTRAINT [FK_ProductLedger];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductMRRProductInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MRRProductInfoes] DROP CONSTRAINT [FK_ProductMRRProductInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductPLRProductInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PLRProductInfoes] DROP CONSTRAINT [FK_ProductPLRProductInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductProductEdit]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductEdits] DROP CONSTRAINT [FK_ProductProductEdit];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductPromotion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Promotions] DROP CONSTRAINT [FK_ProductPromotion];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductRegionTarget]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RegionTargets] DROP CONSTRAINT [FK_ProductRegionTarget];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductRequisitionProductInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RequisitionProductInfoes] DROP CONSTRAINT [FK_ProductRequisitionProductInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductSalesReturnInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SalesReturnInfoes] DROP CONSTRAINT [FK_ProductSalesReturnInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductTransferDetail_Product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductTransferDetails] DROP CONSTRAINT [FK_ProductTransferDetail_Product];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductTransferDetail_ProductTransfer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductTransferDetails] DROP CONSTRAINT [FK_ProductTransferDetail_ProductTransfer];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductTransferEmployee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductTransfers] DROP CONSTRAINT [FK_ProductTransferEmployee];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductTransferIndentDetails]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TransferIndentDetails] DROP CONSTRAINT [FK_ProductTransferIndentDetails];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductTransferSendInventory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductTransfers] DROP CONSTRAINT [FK_ProductTransferSendInventory];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductTransportationLoss]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TransportationLosses] DROP CONSTRAINT [FK_ProductTransportationLoss];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductYearSummaryDealer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[YearSummaryDealers] DROP CONSTRAINT [FK_ProductYearSummaryDealer];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductYearSummaryInventoryProduct]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[YearSummaryInventoryProducts] DROP CONSTRAINT [FK_ProductYearSummaryInventoryProduct];
GO
IF OBJECT_ID(N'[dbo].[FK_RegionExpenditure]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Expenditures] DROP CONSTRAINT [FK_RegionExpenditure];
GO
IF OBJECT_ID(N'[dbo].[FK_RegionInventory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Inventories] DROP CONSTRAINT [FK_RegionInventory];
GO
IF OBJECT_ID(N'[dbo].[FK_RegionRegionTarget]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RegionTargets] DROP CONSTRAINT [FK_RegionRegionTarget];
GO
IF OBJECT_ID(N'[dbo].[FK_RequisitionRequisitionPackageInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RequisitionPackageInfoes] DROP CONSTRAINT [FK_RequisitionRequisitionPackageInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_RequisitionRequisitionProductInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RequisitionProductInfoes] DROP CONSTRAINT [FK_RequisitionRequisitionProductInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_RMVerifiesIndent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Indents] DROP CONSTRAINT [FK_RMVerifiesIndent];
GO
IF OBJECT_ID(N'[dbo].[FK_SalesOfficerTargetProduct]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SalesOfficerTargets] DROP CONSTRAINT [FK_SalesOfficerTargetProduct];
GO
IF OBJECT_ID(N'[dbo].[FK_SalesReturnSalesReturnInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SalesReturnInfoes] DROP CONSTRAINT [FK_SalesReturnSalesReturnInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_SeasonRegionTargets]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RegionTargets] DROP CONSTRAINT [FK_SeasonRegionTargets];
GO
IF OBJECT_ID(N'[dbo].[FK_SeasonSalesOfficerTargets]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SalesOfficerTargets] DROP CONSTRAINT [FK_SeasonSalesOfficerTargets];
GO
IF OBJECT_ID(N'[dbo].[FK_SeasonYearSummaryDealers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[YearSummaryDealers] DROP CONSTRAINT [FK_SeasonYearSummaryDealers];
GO
IF OBJECT_ID(N'[dbo].[FK_SeasonYearSummaryInventoryPackages]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[YearSummaryInventoryPackages] DROP CONSTRAINT [FK_SeasonYearSummaryInventoryPackages];
GO
IF OBJECT_ID(N'[dbo].[FK_SeasonYearSummaryInventoryProducts]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[YearSummaryInventoryProducts] DROP CONSTRAINT [FK_SeasonYearSummaryInventoryProducts];
GO
IF OBJECT_ID(N'[dbo].[FK_SeasonYearSummarySOExpenditures]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[YearSummarySOExpenditures] DROP CONSTRAINT [FK_SeasonYearSummarySOExpenditures];
GO
IF OBJECT_ID(N'[dbo].[FK_TransferIndent_ProductTransfer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductTransfers] DROP CONSTRAINT [FK_TransferIndent_ProductTransfer];
GO
IF OBJECT_ID(N'[dbo].[FK_TransferIndentTransferIndentDetails]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TransferIndentDetails] DROP CONSTRAINT [FK_TransferIndentTransferIndentDetails];
GO
IF OBJECT_ID(N'[dbo].[FK_YearSummaryDealerDealer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[YearSummaryDealers] DROP CONSTRAINT [FK_YearSummaryDealerDealer];
GO
IF OBJECT_ID(N'[dbo].[FK_YearSummarySOExpenditureEmployee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[YearSummarySOExpenditures] DROP CONSTRAINT [FK_YearSummarySOExpenditureEmployee];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[BillPaymentInfoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BillPaymentInfoes];
GO
IF OBJECT_ID(N'[dbo].[BillPayments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BillPayments];
GO
IF OBJECT_ID(N'[dbo].[BillProductInfoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BillProductInfoes];
GO
IF OBJECT_ID(N'[dbo].[Bills]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Bills];
GO
IF OBJECT_ID(N'[dbo].[Commissions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Commissions];
GO
IF OBJECT_ID(N'[dbo].[Dealers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Dealers];
GO
IF OBJECT_ID(N'[dbo].[DueInfoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DueInfoes];
GO
IF OBJECT_ID(N'[dbo].[Employees]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employees];
GO
IF OBJECT_ID(N'[dbo].[ExpenditureInfoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ExpenditureInfoes];
GO
IF OBJECT_ID(N'[dbo].[Expenditures]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Expenditures];
GO
IF OBJECT_ID(N'[dbo].[IndentProductInfoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IndentProductInfoes];
GO
IF OBJECT_ID(N'[dbo].[Indents]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Indents];
GO
IF OBJECT_ID(N'[dbo].[Inventories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Inventories];
GO
IF OBJECT_ID(N'[dbo].[InventoryLogs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InventoryLogs];
GO
IF OBJECT_ID(N'[dbo].[InventoryPackageInfoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InventoryPackageInfoes];
GO
IF OBJECT_ID(N'[dbo].[InventoryProductInfoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InventoryProductInfoes];
GO
IF OBJECT_ID(N'[dbo].[Ledgers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Ledgers];
GO
IF OBJECT_ID(N'[dbo].[MessageDeliveries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MessageDeliveries];
GO
IF OBJECT_ID(N'[dbo].[Messages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Messages];
GO
IF OBJECT_ID(N'[dbo].[MRRPackageInfoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MRRPackageInfoes];
GO
IF OBJECT_ID(N'[dbo].[MRRProductInfoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MRRProductInfoes];
GO
IF OBJECT_ID(N'[dbo].[MRRs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MRRs];
GO
IF OBJECT_ID(N'[dbo].[NoticeBoards]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NoticeBoards];
GO
IF OBJECT_ID(N'[dbo].[Packages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Packages];
GO
IF OBJECT_ID(N'[dbo].[People]', 'U') IS NOT NULL
    DROP TABLE [dbo].[People];
GO
IF OBJECT_ID(N'[dbo].[PLRPackageInfoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PLRPackageInfoes];
GO
IF OBJECT_ID(N'[dbo].[PLRProductInfoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PLRProductInfoes];
GO
IF OBJECT_ID(N'[dbo].[PLRs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PLRs];
GO
IF OBJECT_ID(N'[dbo].[ProductEdits]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductEdits];
GO
IF OBJECT_ID(N'[dbo].[Products]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Products];
GO
IF OBJECT_ID(N'[dbo].[ProductTransferDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductTransferDetails];
GO
IF OBJECT_ID(N'[dbo].[ProductTransfers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductTransfers];
GO
IF OBJECT_ID(N'[dbo].[Promotions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Promotions];
GO
IF OBJECT_ID(N'[dbo].[Regions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Regions];
GO
IF OBJECT_ID(N'[dbo].[RegionTargets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RegionTargets];
GO
IF OBJECT_ID(N'[dbo].[RequisitionPackageInfoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RequisitionPackageInfoes];
GO
IF OBJECT_ID(N'[dbo].[RequisitionProductInfoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RequisitionProductInfoes];
GO
IF OBJECT_ID(N'[dbo].[Requisitions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Requisitions];
GO
IF OBJECT_ID(N'[dbo].[SalesOfficerTargets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SalesOfficerTargets];
GO
IF OBJECT_ID(N'[dbo].[SalesReturnInfoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SalesReturnInfoes];
GO
IF OBJECT_ID(N'[dbo].[SalesReturns]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SalesReturns];
GO
IF OBJECT_ID(N'[dbo].[Seasons]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Seasons];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[TransferIndentDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TransferIndentDetails];
GO
IF OBJECT_ID(N'[dbo].[TransferIndents]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TransferIndents];
GO
IF OBJECT_ID(N'[dbo].[TransportationLosses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TransportationLosses];
GO
IF OBJECT_ID(N'[dbo].[YearSummaryDealers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[YearSummaryDealers];
GO
IF OBJECT_ID(N'[dbo].[YearSummaryInventoryPackages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[YearSummaryInventoryPackages];
GO
IF OBJECT_ID(N'[dbo].[YearSummaryInventoryProducts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[YearSummaryInventoryProducts];
GO
IF OBJECT_ID(N'[dbo].[YearSummarySOExpenditures]', 'U') IS NOT NULL
    DROP TABLE [dbo].[YearSummarySOExpenditures];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'BillPaymentInfoes'
CREATE TABLE [dbo].[BillPaymentInfoes] (
    [BillPaymentId] bigint  NOT NULL,
    [ProductId] bigint  NOT NULL,
    [BillId] bigint  NOT NULL,
    [Amount] float  NOT NULL,
    [HasRejected] bit  NOT NULL
);
GO

-- Creating table 'BillPayments'
CREATE TABLE [dbo].[BillPayments] (
    [BillPaymentId] bigint IDENTITY(1,1) NOT NULL,
    [DealerId] bigint  NOT NULL,
    [RMId] bigint  NOT NULL,
    [DateOfIssue] datetime  NOT NULL,
    [TotalAmount] float  NOT NULL,
    [PaymentMethod] nvarchar(30)  NOT NULL,
    [BankName] nvarchar(40)  NULL,
    [BranchName] nvarchar(30)  NULL,
    [DDNumber] nvarchar(30)  NULL,
    [Status] nvarchar(30)  NOT NULL,
    [AccountantId] bigint  NOT NULL,
    [AccountantFinalizeDate] datetime  NOT NULL,
    [BillPaymentCode] nvarchar(30)  NULL,
    [MoneyReciept] nvarchar(60)  NULL,
    [DateOfDeposit] datetime  NULL
);
GO

-- Creating table 'BillProductInfoes'
CREATE TABLE [dbo].[BillProductInfoes] (
    [BillId] bigint  NOT NULL,
    [ProductId] bigint  NOT NULL,
    [ProductPrice] float  NOT NULL,
    [LotId] nvarchar(30)  NOT NULL,
    [LotQuantity] float  NOT NULL,
    [ComissionPercentage] float  NOT NULL
);
GO

-- Creating table 'Bills'
CREATE TABLE [dbo].[Bills] (
    [BillId] bigint IDENTITY(1,1) NOT NULL,
    [DateOfIssue] datetime  NOT NULL,
    [IndentId] bigint  NOT NULL,
    [TransportType] nvarchar(30)  NOT NULL,
    [TransportCost] float  NOT NULL,
    [VehicleNo] nvarchar(30)  NOT NULL,
    [EmergencyContactNo] nvarchar(30)  NOT NULL,
    [TotalProductCost] float  NOT NULL,
    [TotalPaid] float  NOT NULL,
    [PaymentDeadline] datetime  NOT NULL,
    [BillStatus] nvarchar(30)  NOT NULL,
    [HasProductLoss] bit  NOT NULL,
    [ProductLossCost] float  NOT NULL,
    [DispatchedById] bigint  NOT NULL,
    [BillCode] nvarchar(30)  NULL,
    [HasSalesReturn] bit  NOT NULL,
    [SalesReturnCost] float  NOT NULL,
    [DealerId] bigint  NOT NULL
);
GO

-- Creating table 'Commissions'
CREATE TABLE [dbo].[Commissions] (
    [CommissionId] bigint IDENTITY(1,1) NOT NULL,
    [ProductId] bigint  NOT NULL,
    [Percentage] float  NOT NULL,
    [Duration] smallint  NOT NULL,
    [CommissionName] nvarchar(30)  NOT NULL,
    [CommissionStatus] nvarchar(30)  NULL,
    [IntroducedDate] datetime  NULL,
    [NSMId] bigint  NOT NULL,
    [AdminId] bigint  NOT NULL
);
GO

-- Creating table 'Dealers'
CREATE TABLE [dbo].[Dealers] (
    [DealerId] bigint IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [FatherName] nvarchar(50)  NOT NULL,
    [DateOfBirth] datetime  NOT NULL,
    [ContactNo] nvarchar(50)  NOT NULL,
    [CompanyName] nvarchar(150)  NOT NULL,
    [CompanyAddress] nvarchar(150)  NOT NULL,
    [LicenseNo] nvarchar(30)  NOT NULL,
    [CreditLimit] float  NOT NULL,
    [ExpectedDefaulterDate] datetime  NOT NULL,
    [ActivityStatus] nvarchar(30)  NOT NULL,
    [TotalDue] float  NOT NULL,
    [DateOfAdminApproval] datetime  NOT NULL,
    [RegionId] bigint  NOT NULL,
    [SalesOfficerId] bigint  NOT NULL,
    [TotalCollection] float  NOT NULL,
    [MotherName] nvarchar(50)  NOT NULL,
    [PresentAddress] nvarchar(150)  NOT NULL,
    [PermamentAddress] nvarchar(150)  NOT NULL,
    [Nationality] nvarchar(50)  NOT NULL,
    [EmailAddress] nvarchar(50)  NULL,
    [PictureLink] nvarchar(150)  NULL,
    [LicenseIssueDate] datetime  NOT NULL,
    [BusinessType] nvarchar(60)  NOT NULL,
    [OwnerName] nvarchar(30)  NOT NULL,
    [OwnerAddress] nvarchar(100)  NOT NULL,
    [PreferredTypeOfPayment] nvarchar(100)  NOT NULL,
    [HasAnotherDealership] bit  NOT NULL,
    [DealershipCompany] nvarchar(100)  NULL,
    [HasOwnOffice] bit  NOT NULL,
    [DateOfRegistration] datetime  NOT NULL,
    [UserName] nvarchar(30)  NOT NULL,
    [Password] nvarchar(30)  NOT NULL
);
GO

-- Creating table 'DueInfoes'
CREATE TABLE [dbo].[DueInfoes] (
    [DealerId] bigint  NOT NULL,
    [BillId] bigint  NOT NULL,
    [ProductId] bigint  NOT NULL,
    [DueStatus] nvarchar(30)  NOT NULL,
    [ProductCost] float  NOT NULL,
    [TransportationLoss] float  NOT NULL,
    [SalesReturn] float  NOT NULL,
    [Paid] float  NOT NULL,
    [CommissionOnSell] float  NOT NULL,
    [CommissionOnBenefit] float  NOT NULL,
    [PaymentDeadLine] datetime  NOT NULL
);
GO

-- Creating table 'Employees'
CREATE TABLE [dbo].[Employees] (
    [EmployeeId] bigint IDENTITY(1,1) NOT NULL,
    [PersonId] bigint  NOT NULL,
    [RegionId] bigint  NOT NULL,
    [Designation] nvarchar(30)  NOT NULL,
    [ActivityStatus] nvarchar(30)  NOT NULL,
    [UserName] nvarchar(30)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ExpenditureInfoes'
CREATE TABLE [dbo].[ExpenditureInfoes] (
    [ExpenditureId] bigint  NOT NULL,
    [SalesOfficerId] bigint  NOT NULL,
    [PlacedAmount] float  NOT NULL,
    [Remarks] nvarchar(200)  NOT NULL,
    [NSMAcceptedAmount] float  NOT NULL,
    [AccountAcceptedAmount] float  NOT NULL
);
GO

-- Creating table 'Expenditures'
CREATE TABLE [dbo].[Expenditures] (
    [ExpenditureId] bigint IDENTITY(1,1) NOT NULL,
    [DateOfIssue] datetime  NOT NULL,
    [RegionId] bigint  NOT NULL,
    [PlacedById] bigint  NOT NULL,
    [PlacedToId] bigint  NOT NULL,
    [Status] nvarchar(50)  NOT NULL,
    [TotalPlacedAmount] float  NOT NULL,
    [TotalAcceptedAmount] float  NOT NULL,
    [ExpenditureCode] nvarchar(30)  NULL
);
GO

-- Creating table 'IndentProductInfoes'
CREATE TABLE [dbo].[IndentProductInfoes] (
    [IndentId] bigint  NOT NULL,
    [ProductId] bigint  NOT NULL,
    [ProductPrice] float  NOT NULL,
    [ProductQuantity] float  NOT NULL,
    [EditTime] datetime  NOT NULL,
    [CommissionPercentage] float  NOT NULL,
    [ProductQuantityByRM] float  NULL,
    [ProductQuantityBySIC] float  NULL,
    [EditTimeRM] datetime  NULL,
    [EditTimeSIC] datetime  NULL
);
GO

-- Creating table 'Indents'
CREATE TABLE [dbo].[Indents] (
    [IndentId] bigint IDENTITY(1,1) NOT NULL,
    [DateOfPlace] datetime  NOT NULL,
    [IndentStatus] nvarchar(30)  NOT NULL,
    [IssuedById] bigint  NOT NULL,
    [IssuedToId] bigint  NOT NULL,
    [ForwardedToId] bigint  NOT NULL,
    [PaymentMethod] nvarchar(30)  NOT NULL,
    [IsConsideredByNSM] bit  NOT NULL,
    [IndentCode] nvarchar(30)  NULL,
    [ForwardedToInventoryId] bigint  NOT NULL
);
GO

-- Creating table 'Inventories'
CREATE TABLE [dbo].[Inventories] (
    [InventoryId] bigint IDENTITY(1,1) NOT NULL,
    [RegionId] bigint  NOT NULL,
    [StoreInChargeId] bigint  NOT NULL,
    [DispatchOfficerId] bigint  NOT NULL,
    [InventoryName] nvarchar(30)  NOT NULL,
    [InventoryAddress] nvarchar(100)  NOT NULL,
    [DateOfEstablished] datetime  NOT NULL
);
GO

-- Creating table 'InventoryLogs'
CREATE TABLE [dbo].[InventoryLogs] (
    [Date] datetime  NOT NULL,
    [ProductId] bigint  NOT NULL,
    [InventoryId] bigint  NOT NULL,
    [LotId] nvarchar(30)  NOT NULL,
    [Method] nvarchar(50)  NOT NULL,
    [MemoNo] bigint  NOT NULL,
    [ProductQuantity] float  NOT NULL,
    [ProductCost] float  NOT NULL,
    [InventoryLogId] bigint IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'InventoryPackageInfoes'
CREATE TABLE [dbo].[InventoryPackageInfoes] (
    [InventoryId] bigint  NOT NULL,
    [PackageId] bigint  NOT NULL,
    [UnfinishedQuantity] float  NOT NULL,
    [OnProcessingQuantity] float  NOT NULL,
    [FinishedQuantity] float  NOT NULL,
    [UnitCost] float  NULL
);
GO

-- Creating table 'InventoryProductInfoes'
CREATE TABLE [dbo].[InventoryProductInfoes] (
    [InventoryId] bigint  NOT NULL,
    [ProductId] bigint  NOT NULL,
    [LotId] nvarchar(30)  NOT NULL,
    [UnfinishedQuantity] float  NOT NULL,
    [OnProcessingQuantity] float  NOT NULL,
    [FinishedQuantity] float  NOT NULL,
    [UnitLotCost] float  NULL
);
GO

-- Creating table 'Ledgers'
CREATE TABLE [dbo].[Ledgers] (
    [Date] datetime  NOT NULL,
    [Method] nvarchar(30)  NOT NULL,
    [IsDealerOrEmployee] bit  NOT NULL,
    [IsDebitOrCredit] bit  NOT NULL,
    [PartyId] bigint  NOT NULL,
    [MemoNo] nvarchar(30)  NOT NULL,
    [ProductId] bigint  NOT NULL,
    [ProductQuantity] float  NOT NULL,
    [CreditAmount] float  NOT NULL,
    [LedgerId] bigint IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'MessageDeliveries'
CREATE TABLE [dbo].[MessageDeliveries] (
    [OfficeCode] nvarchar(50)  NOT NULL,
    [SendFromId] bigint  NOT NULL,
    [SendToId] bigint  NOT NULL,
    [DateOfIssue] datetime  NOT NULL,
    [MessageId] bigint  NOT NULL
);
GO

-- Creating table 'Messages'
CREATE TABLE [dbo].[Messages] (
    [MessageId] bigint IDENTITY(1,1) NOT NULL,
    [MessageType] nvarchar(50)  NOT NULL,
    [Subject] nvarchar(50)  NOT NULL,
    [Body] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'MRRPackageInfoes'
CREATE TABLE [dbo].[MRRPackageInfoes] (
    [MRRId] bigint  NOT NULL,
    [PackageId] bigint  NOT NULL,
    [PlacedQuantity] float  NOT NULL,
    [AcceptedQuantity] float  NOT NULL,
    [PurchasePrice] float  NULL
);
GO

-- Creating table 'MRRProductInfoes'
CREATE TABLE [dbo].[MRRProductInfoes] (
    [MRRId] bigint  NOT NULL,
    [ProductId] bigint  NOT NULL,
    [LotId] nvarchar(30)  NOT NULL,
    [PlacedQuantity] float  NOT NULL,
    [AcceptedQuantity] float  NOT NULL,
    [PurchasePrice] float  NULL
);
GO

-- Creating table 'MRRs'
CREATE TABLE [dbo].[MRRs] (
    [MRRId] bigint IDENTITY(1,1) NOT NULL,
    [DateOfIssue] datetime  NOT NULL,
    [InventoryId] bigint  NOT NULL,
    [IssuedById] bigint  NOT NULL,
    [IssuedToId] bigint  NOT NULL,
    [Status] nvarchar(30)  NOT NULL,
    [DateOfApproval] datetime  NOT NULL,
    [MRRCode] nvarchar(30)  NULL,
    [PurchaseOrderNo] nvarchar(30)  NOT NULL,
    [ChallanNo] nvarchar(30)  NOT NULL,
    [Wing] nvarchar(30)  NOT NULL,
    [RetailerName] nvarchar(80)  NOT NULL,
    [MRRType] nvarchar(30)  NOT NULL
);
GO

-- Creating table 'NoticeBoards'
CREATE TABLE [dbo].[NoticeBoards] (
    [NoticeId] bigint IDENTITY(1,1) NOT NULL,
    [NoticeSubject] nvarchar(100)  NOT NULL,
    [NoticeBody] nvarchar(max)  NOT NULL,
    [StartDate] datetime  NOT NULL,
    [EndDate] datetime  NOT NULL,
    [IsActive] bit  NOT NULL,
    [IssuedById] bigint  NOT NULL
);
GO

-- Creating table 'Packages'
CREATE TABLE [dbo].[Packages] (
    [PackageId] bigint IDENTITY(1,1) NOT NULL,
    [PackageName] nvarchar(30)  NOT NULL,
    [PackageCode] nvarchar(30)  NOT NULL,
    [PackageStatus] nvarchar(30)  NOT NULL,
    [NSMId] bigint  NOT NULL,
    [AdminId] bigint  NOT NULL,
    [IntroducedDate] datetime  NOT NULL
);
GO

-- Creating table 'People'
CREATE TABLE [dbo].[People] (
    [PersonId] bigint IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [DateOfBirth] datetime  NOT NULL,
    [DateOfJoin] datetime  NOT NULL,
    [Address] nvarchar(100)  NOT NULL,
    [ContactNo] nvarchar(100)  NOT NULL,
    [EmailAddress] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'PLRPackageInfoes'
CREATE TABLE [dbo].[PLRPackageInfoes] (
    [PLRId] bigint  NOT NULL,
    [PackageId] bigint  NOT NULL,
    [Quantity] float  NOT NULL,
    [Remarks] nvarchar(50)  NOT NULL,
    [LostAmount] float  NULL,
    [State] nvarchar(30)  NULL
);
GO

-- Creating table 'PLRProductInfoes'
CREATE TABLE [dbo].[PLRProductInfoes] (
    [PLRId] bigint  NOT NULL,
    [ProductId] bigint  NOT NULL,
    [LotId] nvarchar(30)  NOT NULL,
    [Quantity] float  NOT NULL,
    [Remarks] nvarchar(100)  NOT NULL,
    [LostAmount] float  NULL,
    [State] nvarchar(30)  NULL
);
GO

-- Creating table 'PLRs'
CREATE TABLE [dbo].[PLRs] (
    [PLRId] bigint IDENTITY(1,1) NOT NULL,
    [InventoryId] bigint  NOT NULL,
    [DateOfIssue] datetime  NOT NULL,
    [IssuedById] bigint  NOT NULL,
    [IssuedToId] bigint  NOT NULL,
    [Status] nvarchar(30)  NOT NULL,
    [DateOfApproval] datetime  NOT NULL,
    [PLRCode] nvarchar(30)  NULL
);
GO

-- Creating table 'ProductEdits'
CREATE TABLE [dbo].[ProductEdits] (
    [ProductEditId] bigint IDENTITY(1,1) NOT NULL,
    [ProductId] bigint  NOT NULL,
    [EditType] nvarchar(50)  NOT NULL,
    [NSMId] bigint  NOT NULL,
    [AdminId] bigint  NOT NULL,
    [PreviousValue] nvarchar(50)  NOT NULL,
    [EditedValue] nvarchar(50)  NOT NULL,
    [EditStatus] nvarchar(30)  NOT NULL,
    [ChangeApplicableFrom] datetime  NOT NULL
);
GO

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [ProductId] bigint IDENTITY(1,1) NOT NULL,
    [ProductCode] nvarchar(30)  NOT NULL,
    [ProductName] nvarchar(30)  NOT NULL,
    [ProductType] nvarchar(30)  NOT NULL,
    [ProductWing] nvarchar(30)  NOT NULL,
    [StockKeepingUnit] float  NOT NULL,
    [IsOpOrHibrid] bit  NOT NULL,
    [IsImported] bit  NOT NULL,
    [PurchasePeriodStart] nvarchar(50)  NOT NULL,
    [PurchasePeriodEnd] nvarchar(50)  NOT NULL,
    [SalesPeriodStart] nvarchar(50)  NOT NULL,
    [SalesPeriodEnd] nvarchar(50)  NOT NULL,
    [IntroducedDate] datetime  NOT NULL,
    [ProductStatus] nvarchar(30)  NOT NULL,
    [PricePerUnit] float  NOT NULL,
    [BrandName] nvarchar(30)  NOT NULL,
    [GroupName] nvarchar(30)  NOT NULL,
    [SubType] nvarchar(30)  NOT NULL
);
GO

-- Creating table 'Promotions'
CREATE TABLE [dbo].[Promotions] (
    [PromotionId] bigint IDENTITY(1,1) NOT NULL,
    [ProductId] bigint  NOT NULL,
    [StartAt] datetime  NOT NULL,
    [EndAt] datetime  NOT NULL,
    [PromotionTitle] nvarchar(50)  NOT NULL,
    [PromotionProductName] nvarchar(50)  NOT NULL,
    [PromotionProductQuantity] float  NOT NULL,
    [ProductPrice] float  NOT NULL,
    [ProductQuantity] float  NOT NULL
);
GO

-- Creating table 'Regions'
CREATE TABLE [dbo].[Regions] (
    [RegionId] bigint IDENTITY(1,1) NOT NULL,
    [RegionName] nvarchar(30)  NOT NULL,
    [DistrictName] nvarchar(30)  NOT NULL
);
GO

-- Creating table 'RegionTargets'
CREATE TABLE [dbo].[RegionTargets] (
    [RegionId] bigint  NOT NULL,
    [TargetQuantity] float  NOT NULL,
    [AchievedAmount] float  NOT NULL,
    [Product_ProductId] bigint  NOT NULL,
    [DistributedQuantity] float  NOT NULL,
    [SalesReturnQuantity] float  NOT NULL,
    [TransportationLossQuantity] float  NOT NULL,
    [SeasonId] bigint  NOT NULL
);
GO

-- Creating table 'RequisitionPackageInfoes'
CREATE TABLE [dbo].[RequisitionPackageInfoes] (
    [RequisitionId] bigint  NOT NULL,
    [PackageId] bigint  NOT NULL,
    [PlacedQuantity] float  NOT NULL,
    [AcceptedQuantity] float  NOT NULL
);
GO

-- Creating table 'RequisitionProductInfoes'
CREATE TABLE [dbo].[RequisitionProductInfoes] (
    [RequisitionId] bigint  NOT NULL,
    [ProductId] bigint  NOT NULL,
    [LotId] nvarchar(30)  NOT NULL,
    [PlacedQuantity] float  NOT NULL,
    [LotQuantity] float  NOT NULL
);
GO

-- Creating table 'Requisitions'
CREATE TABLE [dbo].[Requisitions] (
    [RequisitionId] bigint IDENTITY(1,1) NOT NULL,
    [InventoryId] bigint  NOT NULL,
    [DateOfIssue] datetime  NOT NULL,
    [IssuedById] bigint  NOT NULL,
    [IssuedToId] bigint  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [DateOfApproval] datetime  NOT NULL,
    [RequisitionType] nvarchar(30)  NOT NULL,
    [RequisitionCode] nvarchar(30)  NULL
);
GO

-- Creating table 'SalesOfficerTargets'
CREATE TABLE [dbo].[SalesOfficerTargets] (
    [SalesOfficerId] bigint  NOT NULL,
    [TargetQuantity] float  NOT NULL,
    [AchievedAmount] float  NOT NULL,
    [ProductId] bigint  NOT NULL,
    [DistributedQuantity] float  NOT NULL,
    [TransportationLossQuantity] float  NOT NULL,
    [SalesReturnQuantity] float  NOT NULL,
    [SeasonId] bigint  NOT NULL
);
GO

-- Creating table 'SalesReturnInfoes'
CREATE TABLE [dbo].[SalesReturnInfoes] (
    [ProductId] bigint  NOT NULL,
    [SalesReturnId] bigint  NOT NULL,
    [LotId] nvarchar(30)  NOT NULL,
    [ProductPrice] float  NOT NULL,
    [PlacedQuantity] float  NOT NULL,
    [AcceptedQuantity] float  NOT NULL,
    [BillId] bigint  NOT NULL
);
GO

-- Creating table 'SalesReturns'
CREATE TABLE [dbo].[SalesReturns] (
    [SalesReturnId] bigint IDENTITY(1,1) NOT NULL,
    [DateOfIssue] datetime  NOT NULL,
    [DealerId] bigint  NOT NULL,
    [RMId] bigint  NOT NULL,
    [TotalPlacedAmount] float  NOT NULL,
    [TotalAcceptedAmount] float  NOT NULL,
    [SalesReturnCode] nvarchar(30)  NULL,
    [Status] nvarchar(30)  NOT NULL,
    [SendToInventoryId] bigint  NOT NULL
);
GO

-- Creating table 'TransportationLosses'
CREATE TABLE [dbo].[TransportationLosses] (
    [BillId] bigint  NOT NULL,
    [ProductId] bigint  NOT NULL,
    [Remarks] nvarchar(80)  NOT NULL,
    [HasBalanced] bit  NOT NULL,
    [LossQuantity] float  NOT NULL,
    [UnitPrice] float  NOT NULL,
    [BalancedQuantity] float  NOT NULL
);
GO

-- Creating table 'YearSummaryDealers'
CREATE TABLE [dbo].[YearSummaryDealers] (
    [DealerId] bigint  NOT NULL,
    [ProductId] bigint  NOT NULL,
    [OpeningBalance] float  NOT NULL,
    [ClosingBalance] float  NOT NULL,
    [ProductQuantity] float  NOT NULL,
    [DebitAmount] float  NOT NULL,
    [CreditAmount] float  NOT NULL,
    [SeasonId] bigint  NOT NULL
);
GO

-- Creating table 'YearSummaryInventoryPackages'
CREATE TABLE [dbo].[YearSummaryInventoryPackages] (
    [InventoryId] bigint  NOT NULL,
    [PackageId] bigint  NOT NULL,
    [OpeningPackage] float  NOT NULL,
    [MRRInQuantity] float  NOT NULL,
    [PLRLostQuantity] float  NOT NULL,
    [PackageUsedQuantity] float  NOT NULL,
    [ClosingPackage] float  NOT NULL,
    [PurchaseAmount] float  NOT NULL,
    [LostAmount] float  NOT NULL,
    [SeasonId] bigint  NOT NULL
);
GO

-- Creating table 'YearSummaryInventoryProducts'
CREATE TABLE [dbo].[YearSummaryInventoryProducts] (
    [InventoryId] bigint  NOT NULL,
    [ProductId] bigint  NOT NULL,
    [LotId] nvarchar(30)  NOT NULL,
    [OpeningProduct] float  NOT NULL,
    [MRRInQuantity] float  NOT NULL,
    [PLRLostQuantity] float  NOT NULL,
    [ProductSellQuantity] float  NOT NULL,
    [SellAmount] float  NOT NULL,
    [ClosingProduct] float  NOT NULL,
    [PurchaseAmount] float  NOT NULL,
    [ProductReturnQuantity] float  NOT NULL,
    [LostAmount] float  NOT NULL,
    [ReturnAmount] float  NOT NULL,
    [SeasonId] bigint  NOT NULL,
    [ProductTransferFromQuantity] float  NOT NULL,
    [ProductTransferToQuantity] float  NOT NULL,
    [TransferFromAmount] float  NOT NULL,
    [TransferToAmount] float  NOT NULL
);
GO

-- Creating table 'YearSummarySOExpenditures'
CREATE TABLE [dbo].[YearSummarySOExpenditures] (
    [SalesOfficerId] bigint  NOT NULL,
    [TotalExpenditure] float  NOT NULL,
    [SeasonId] bigint  NOT NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'Seasons'
CREATE TABLE [dbo].[Seasons] (
    [SeasonId] bigint IDENTITY(1,1) NOT NULL,
    [SeasonStartDate] datetime  NOT NULL,
    [SeasonEndDate] datetime  NOT NULL,
    [SeasonName] nvarchar(80)  NOT NULL,
    [IsCurrentSeason] bit  NOT NULL,
    [HasClosed] bit  NOT NULL
);
GO

-- Creating table 'TransferIndentDetails'
CREATE TABLE [dbo].[TransferIndentDetails] (
    [TransferIndentId] bigint  NOT NULL,
    [ProductId] bigint  NOT NULL,
    [PlacedProductQuantity] float  NOT NULL,
    [ApprovedProductQuantity] float  NOT NULL
);
GO

-- Creating table 'TransferIndents'
CREATE TABLE [dbo].[TransferIndents] (
    [TransferIndentId] bigint IDENTITY(1,1) NOT NULL,
    [TransferIndentCode] nvarchar(30)  NULL,
    [IssuedFromInventoryId] bigint  NOT NULL,
    [IssuedById] bigint  NOT NULL,
    [DateOfIssue] datetime  NOT NULL,
    [Status] nvarchar(30)  NOT NULL,
    [IssuedToInventoryId] bigint  NOT NULL,
    [IssuedToSICId] bigint  NOT NULL,
    [ApprovedByNSMId] bigint  NOT NULL
);
GO

-- Creating table 'ProductTransferDetails'
CREATE TABLE [dbo].[ProductTransferDetails] (
    [ProductTransferId] bigint  NOT NULL,
    [ProductId] bigint  NOT NULL,
    [LotId] nvarchar(30)  NOT NULL,
    [PurchasePricePerUnit] float  NOT NULL,
    [TransferQuantity] float  NOT NULL,
    [RecievedQuantity] float  NOT NULL
);
GO

-- Creating table 'ProductTransfers'
CREATE TABLE [dbo].[ProductTransfers] (
    [ProductTransferID] bigint IDENTITY(1,1) NOT NULL,
    [ProductTransferCode] nvarchar(30)  NULL,
    [TransferIndentId] bigint  NOT NULL,
    [SendFromInventoryId] bigint  NOT NULL,
    [IssuedByDOId] bigint  NOT NULL,
    [DateOfIssue] datetime  NOT NULL,
    [RecievedToInventoryId] bigint  NOT NULL,
    [RecievedBySICId] bigint  NOT NULL,
    [DateOfRecieve] datetime  NULL,
    [TransportType] nvarchar(30)  NOT NULL,
    [VehicleNo] nvarchar(30)  NULL,
    [TransportCost] float  NOT NULL,
    [EmergencyContactNo] nvarchar(30)  NOT NULL,
    [HasProductLoss] bit  NOT NULL,
    [ProductLossCost] float  NOT NULL,
    [ProductTransferStatus] nvarchar(30)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [BillPaymentId], [ProductId], [BillId] in table 'BillPaymentInfoes'
ALTER TABLE [dbo].[BillPaymentInfoes]
ADD CONSTRAINT [PK_BillPaymentInfoes]
    PRIMARY KEY CLUSTERED ([BillPaymentId], [ProductId], [BillId] ASC);
GO

-- Creating primary key on [BillPaymentId] in table 'BillPayments'
ALTER TABLE [dbo].[BillPayments]
ADD CONSTRAINT [PK_BillPayments]
    PRIMARY KEY CLUSTERED ([BillPaymentId] ASC);
GO

-- Creating primary key on [BillId], [ProductId], [LotId] in table 'BillProductInfoes'
ALTER TABLE [dbo].[BillProductInfoes]
ADD CONSTRAINT [PK_BillProductInfoes]
    PRIMARY KEY CLUSTERED ([BillId], [ProductId], [LotId] ASC);
GO

-- Creating primary key on [BillId] in table 'Bills'
ALTER TABLE [dbo].[Bills]
ADD CONSTRAINT [PK_Bills]
    PRIMARY KEY CLUSTERED ([BillId] ASC);
GO

-- Creating primary key on [CommissionId] in table 'Commissions'
ALTER TABLE [dbo].[Commissions]
ADD CONSTRAINT [PK_Commissions]
    PRIMARY KEY CLUSTERED ([CommissionId] ASC);
GO

-- Creating primary key on [DealerId] in table 'Dealers'
ALTER TABLE [dbo].[Dealers]
ADD CONSTRAINT [PK_Dealers]
    PRIMARY KEY CLUSTERED ([DealerId] ASC);
GO

-- Creating primary key on [BillId], [ProductId] in table 'DueInfoes'
ALTER TABLE [dbo].[DueInfoes]
ADD CONSTRAINT [PK_DueInfoes]
    PRIMARY KEY CLUSTERED ([BillId], [ProductId] ASC);
GO

-- Creating primary key on [EmployeeId] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [PK_Employees]
    PRIMARY KEY CLUSTERED ([EmployeeId] ASC);
GO

-- Creating primary key on [ExpenditureId], [SalesOfficerId] in table 'ExpenditureInfoes'
ALTER TABLE [dbo].[ExpenditureInfoes]
ADD CONSTRAINT [PK_ExpenditureInfoes]
    PRIMARY KEY CLUSTERED ([ExpenditureId], [SalesOfficerId] ASC);
GO

-- Creating primary key on [ExpenditureId] in table 'Expenditures'
ALTER TABLE [dbo].[Expenditures]
ADD CONSTRAINT [PK_Expenditures]
    PRIMARY KEY CLUSTERED ([ExpenditureId] ASC);
GO

-- Creating primary key on [IndentId], [ProductId] in table 'IndentProductInfoes'
ALTER TABLE [dbo].[IndentProductInfoes]
ADD CONSTRAINT [PK_IndentProductInfoes]
    PRIMARY KEY CLUSTERED ([IndentId], [ProductId] ASC);
GO

-- Creating primary key on [IndentId] in table 'Indents'
ALTER TABLE [dbo].[Indents]
ADD CONSTRAINT [PK_Indents]
    PRIMARY KEY CLUSTERED ([IndentId] ASC);
GO

-- Creating primary key on [InventoryId] in table 'Inventories'
ALTER TABLE [dbo].[Inventories]
ADD CONSTRAINT [PK_Inventories]
    PRIMARY KEY CLUSTERED ([InventoryId] ASC);
GO

-- Creating primary key on [InventoryLogId] in table 'InventoryLogs'
ALTER TABLE [dbo].[InventoryLogs]
ADD CONSTRAINT [PK_InventoryLogs]
    PRIMARY KEY CLUSTERED ([InventoryLogId] ASC);
GO

-- Creating primary key on [InventoryId], [PackageId] in table 'InventoryPackageInfoes'
ALTER TABLE [dbo].[InventoryPackageInfoes]
ADD CONSTRAINT [PK_InventoryPackageInfoes]
    PRIMARY KEY CLUSTERED ([InventoryId], [PackageId] ASC);
GO

-- Creating primary key on [InventoryId], [ProductId], [LotId] in table 'InventoryProductInfoes'
ALTER TABLE [dbo].[InventoryProductInfoes]
ADD CONSTRAINT [PK_InventoryProductInfoes]
    PRIMARY KEY CLUSTERED ([InventoryId], [ProductId], [LotId] ASC);
GO

-- Creating primary key on [LedgerId] in table 'Ledgers'
ALTER TABLE [dbo].[Ledgers]
ADD CONSTRAINT [PK_Ledgers]
    PRIMARY KEY CLUSTERED ([LedgerId] ASC);
GO

-- Creating primary key on [OfficeCode] in table 'MessageDeliveries'
ALTER TABLE [dbo].[MessageDeliveries]
ADD CONSTRAINT [PK_MessageDeliveries]
    PRIMARY KEY CLUSTERED ([OfficeCode] ASC);
GO

-- Creating primary key on [MessageId] in table 'Messages'
ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [PK_Messages]
    PRIMARY KEY CLUSTERED ([MessageId] ASC);
GO

-- Creating primary key on [MRRId], [PackageId] in table 'MRRPackageInfoes'
ALTER TABLE [dbo].[MRRPackageInfoes]
ADD CONSTRAINT [PK_MRRPackageInfoes]
    PRIMARY KEY CLUSTERED ([MRRId], [PackageId] ASC);
GO

-- Creating primary key on [MRRId], [ProductId], [LotId] in table 'MRRProductInfoes'
ALTER TABLE [dbo].[MRRProductInfoes]
ADD CONSTRAINT [PK_MRRProductInfoes]
    PRIMARY KEY CLUSTERED ([MRRId], [ProductId], [LotId] ASC);
GO

-- Creating primary key on [MRRId] in table 'MRRs'
ALTER TABLE [dbo].[MRRs]
ADD CONSTRAINT [PK_MRRs]
    PRIMARY KEY CLUSTERED ([MRRId] ASC);
GO

-- Creating primary key on [NoticeId] in table 'NoticeBoards'
ALTER TABLE [dbo].[NoticeBoards]
ADD CONSTRAINT [PK_NoticeBoards]
    PRIMARY KEY CLUSTERED ([NoticeId] ASC);
GO

-- Creating primary key on [PackageId] in table 'Packages'
ALTER TABLE [dbo].[Packages]
ADD CONSTRAINT [PK_Packages]
    PRIMARY KEY CLUSTERED ([PackageId] ASC);
GO

-- Creating primary key on [PersonId] in table 'People'
ALTER TABLE [dbo].[People]
ADD CONSTRAINT [PK_People]
    PRIMARY KEY CLUSTERED ([PersonId] ASC);
GO

-- Creating primary key on [PLRId], [PackageId] in table 'PLRPackageInfoes'
ALTER TABLE [dbo].[PLRPackageInfoes]
ADD CONSTRAINT [PK_PLRPackageInfoes]
    PRIMARY KEY CLUSTERED ([PLRId], [PackageId] ASC);
GO

-- Creating primary key on [PLRId], [ProductId], [LotId] in table 'PLRProductInfoes'
ALTER TABLE [dbo].[PLRProductInfoes]
ADD CONSTRAINT [PK_PLRProductInfoes]
    PRIMARY KEY CLUSTERED ([PLRId], [ProductId], [LotId] ASC);
GO

-- Creating primary key on [PLRId] in table 'PLRs'
ALTER TABLE [dbo].[PLRs]
ADD CONSTRAINT [PK_PLRs]
    PRIMARY KEY CLUSTERED ([PLRId] ASC);
GO

-- Creating primary key on [ProductEditId] in table 'ProductEdits'
ALTER TABLE [dbo].[ProductEdits]
ADD CONSTRAINT [PK_ProductEdits]
    PRIMARY KEY CLUSTERED ([ProductEditId] ASC);
GO

-- Creating primary key on [ProductId] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([ProductId] ASC);
GO

-- Creating primary key on [PromotionId] in table 'Promotions'
ALTER TABLE [dbo].[Promotions]
ADD CONSTRAINT [PK_Promotions]
    PRIMARY KEY CLUSTERED ([PromotionId] ASC);
GO

-- Creating primary key on [RegionId] in table 'Regions'
ALTER TABLE [dbo].[Regions]
ADD CONSTRAINT [PK_Regions]
    PRIMARY KEY CLUSTERED ([RegionId] ASC);
GO

-- Creating primary key on [RegionId], [SeasonId], [Product_ProductId] in table 'RegionTargets'
ALTER TABLE [dbo].[RegionTargets]
ADD CONSTRAINT [PK_RegionTargets]
    PRIMARY KEY CLUSTERED ([RegionId], [SeasonId], [Product_ProductId] ASC);
GO

-- Creating primary key on [RequisitionId], [PackageId] in table 'RequisitionPackageInfoes'
ALTER TABLE [dbo].[RequisitionPackageInfoes]
ADD CONSTRAINT [PK_RequisitionPackageInfoes]
    PRIMARY KEY CLUSTERED ([RequisitionId], [PackageId] ASC);
GO

-- Creating primary key on [RequisitionId], [ProductId], [LotId] in table 'RequisitionProductInfoes'
ALTER TABLE [dbo].[RequisitionProductInfoes]
ADD CONSTRAINT [PK_RequisitionProductInfoes]
    PRIMARY KEY CLUSTERED ([RequisitionId], [ProductId], [LotId] ASC);
GO

-- Creating primary key on [RequisitionId] in table 'Requisitions'
ALTER TABLE [dbo].[Requisitions]
ADD CONSTRAINT [PK_Requisitions]
    PRIMARY KEY CLUSTERED ([RequisitionId] ASC);
GO

-- Creating primary key on [SalesOfficerId], [SeasonId], [ProductId] in table 'SalesOfficerTargets'
ALTER TABLE [dbo].[SalesOfficerTargets]
ADD CONSTRAINT [PK_SalesOfficerTargets]
    PRIMARY KEY CLUSTERED ([SalesOfficerId], [SeasonId], [ProductId] ASC);
GO

-- Creating primary key on [ProductId], [SalesReturnId], [LotId], [BillId] in table 'SalesReturnInfoes'
ALTER TABLE [dbo].[SalesReturnInfoes]
ADD CONSTRAINT [PK_SalesReturnInfoes]
    PRIMARY KEY CLUSTERED ([ProductId], [SalesReturnId], [LotId], [BillId] ASC);
GO

-- Creating primary key on [SalesReturnId] in table 'SalesReturns'
ALTER TABLE [dbo].[SalesReturns]
ADD CONSTRAINT [PK_SalesReturns]
    PRIMARY KEY CLUSTERED ([SalesReturnId] ASC);
GO

-- Creating primary key on [BillId], [ProductId] in table 'TransportationLosses'
ALTER TABLE [dbo].[TransportationLosses]
ADD CONSTRAINT [PK_TransportationLosses]
    PRIMARY KEY CLUSTERED ([BillId], [ProductId] ASC);
GO

-- Creating primary key on [DealerId], [ProductId], [SeasonId] in table 'YearSummaryDealers'
ALTER TABLE [dbo].[YearSummaryDealers]
ADD CONSTRAINT [PK_YearSummaryDealers]
    PRIMARY KEY CLUSTERED ([DealerId], [ProductId], [SeasonId] ASC);
GO

-- Creating primary key on [InventoryId], [PackageId], [SeasonId] in table 'YearSummaryInventoryPackages'
ALTER TABLE [dbo].[YearSummaryInventoryPackages]
ADD CONSTRAINT [PK_YearSummaryInventoryPackages]
    PRIMARY KEY CLUSTERED ([InventoryId], [PackageId], [SeasonId] ASC);
GO

-- Creating primary key on [InventoryId], [ProductId], [LotId], [SeasonId] in table 'YearSummaryInventoryProducts'
ALTER TABLE [dbo].[YearSummaryInventoryProducts]
ADD CONSTRAINT [PK_YearSummaryInventoryProducts]
    PRIMARY KEY CLUSTERED ([InventoryId], [ProductId], [LotId], [SeasonId] ASC);
GO

-- Creating primary key on [SalesOfficerId], [SeasonId] in table 'YearSummarySOExpenditures'
ALTER TABLE [dbo].[YearSummarySOExpenditures]
ADD CONSTRAINT [PK_YearSummarySOExpenditures]
    PRIMARY KEY CLUSTERED ([SalesOfficerId], [SeasonId] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [SeasonId] in table 'Seasons'
ALTER TABLE [dbo].[Seasons]
ADD CONSTRAINT [PK_Seasons]
    PRIMARY KEY CLUSTERED ([SeasonId] ASC);
GO

-- Creating primary key on [TransferIndentId], [ProductId] in table 'TransferIndentDetails'
ALTER TABLE [dbo].[TransferIndentDetails]
ADD CONSTRAINT [PK_TransferIndentDetails]
    PRIMARY KEY CLUSTERED ([TransferIndentId], [ProductId] ASC);
GO

-- Creating primary key on [TransferIndentId] in table 'TransferIndents'
ALTER TABLE [dbo].[TransferIndents]
ADD CONSTRAINT [PK_TransferIndents]
    PRIMARY KEY CLUSTERED ([TransferIndentId] ASC);
GO

-- Creating primary key on [ProductTransferId], [ProductId], [LotId] in table 'ProductTransferDetails'
ALTER TABLE [dbo].[ProductTransferDetails]
ADD CONSTRAINT [PK_ProductTransferDetails]
    PRIMARY KEY CLUSTERED ([ProductTransferId], [ProductId], [LotId] ASC);
GO

-- Creating primary key on [ProductTransferID] in table 'ProductTransfers'
ALTER TABLE [dbo].[ProductTransfers]
ADD CONSTRAINT [PK_ProductTransfers]
    PRIMARY KEY CLUSTERED ([ProductTransferID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [BillId] in table 'BillPaymentInfoes'
ALTER TABLE [dbo].[BillPaymentInfoes]
ADD CONSTRAINT [FK_BillBillPaymentInfo]
    FOREIGN KEY ([BillId])
    REFERENCES [dbo].[Bills]
        ([BillId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BillBillPaymentInfo'
CREATE INDEX [IX_FK_BillBillPaymentInfo]
ON [dbo].[BillPaymentInfoes]
    ([BillId]);
GO

-- Creating foreign key on [BillPaymentId] in table 'BillPaymentInfoes'
ALTER TABLE [dbo].[BillPaymentInfoes]
ADD CONSTRAINT [FK_BillPaymentBillPaymentInfo]
    FOREIGN KEY ([BillPaymentId])
    REFERENCES [dbo].[BillPayments]
        ([BillPaymentId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ProductId] in table 'BillPaymentInfoes'
ALTER TABLE [dbo].[BillPaymentInfoes]
ADD CONSTRAINT [FK_ProductBillPaymentInfo]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([ProductId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductBillPaymentInfo'
CREATE INDEX [IX_FK_ProductBillPaymentInfo]
ON [dbo].[BillPaymentInfoes]
    ([ProductId]);
GO

-- Creating foreign key on [DealerId] in table 'BillPayments'
ALTER TABLE [dbo].[BillPayments]
ADD CONSTRAINT [FK_DealerBillPayment]
    FOREIGN KEY ([DealerId])
    REFERENCES [dbo].[Dealers]
        ([DealerId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DealerBillPayment'
CREATE INDEX [IX_FK_DealerBillPayment]
ON [dbo].[BillPayments]
    ([DealerId]);
GO

-- Creating foreign key on [RMId] in table 'BillPayments'
ALTER TABLE [dbo].[BillPayments]
ADD CONSTRAINT [FK_EmployeeBillPayment]
    FOREIGN KEY ([RMId])
    REFERENCES [dbo].[Employees]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeBillPayment'
CREATE INDEX [IX_FK_EmployeeBillPayment]
ON [dbo].[BillPayments]
    ([RMId]);
GO

-- Creating foreign key on [BillId] in table 'BillProductInfoes'
ALTER TABLE [dbo].[BillProductInfoes]
ADD CONSTRAINT [FK_BillBillProductInfo]
    FOREIGN KEY ([BillId])
    REFERENCES [dbo].[Bills]
        ([BillId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ProductId] in table 'BillProductInfoes'
ALTER TABLE [dbo].[BillProductInfoes]
ADD CONSTRAINT [FK_ProductBillProductInfo]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([ProductId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductBillProductInfo'
CREATE INDEX [IX_FK_ProductBillProductInfo]
ON [dbo].[BillProductInfoes]
    ([ProductId]);
GO

-- Creating foreign key on [BillId] in table 'DueInfoes'
ALTER TABLE [dbo].[DueInfoes]
ADD CONSTRAINT [FK_BillDueInfo]
    FOREIGN KEY ([BillId])
    REFERENCES [dbo].[Bills]
        ([BillId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [BillId] in table 'TransportationLosses'
ALTER TABLE [dbo].[TransportationLosses]
ADD CONSTRAINT [FK_BillTransportationLoss]
    FOREIGN KEY ([BillId])
    REFERENCES [dbo].[Bills]
        ([BillId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [DispatchedById] in table 'Bills'
ALTER TABLE [dbo].[Bills]
ADD CONSTRAINT [FK_EmployeeBill]
    FOREIGN KEY ([DispatchedById])
    REFERENCES [dbo].[Employees]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeBill'
CREATE INDEX [IX_FK_EmployeeBill]
ON [dbo].[Bills]
    ([DispatchedById]);
GO

-- Creating foreign key on [IndentId] in table 'Bills'
ALTER TABLE [dbo].[Bills]
ADD CONSTRAINT [FK_IndentBill]
    FOREIGN KEY ([IndentId])
    REFERENCES [dbo].[Indents]
        ([IndentId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_IndentBill'
CREATE INDEX [IX_FK_IndentBill]
ON [dbo].[Bills]
    ([IndentId]);
GO

-- Creating foreign key on [ProductId] in table 'Commissions'
ALTER TABLE [dbo].[Commissions]
ADD CONSTRAINT [FK_ProductCommission]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([ProductId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductCommission'
CREATE INDEX [IX_FK_ProductCommission]
ON [dbo].[Commissions]
    ([ProductId]);
GO

-- Creating foreign key on [DealerId] in table 'DueInfoes'
ALTER TABLE [dbo].[DueInfoes]
ADD CONSTRAINT [FK_DealerDueInfo]
    FOREIGN KEY ([DealerId])
    REFERENCES [dbo].[Dealers]
        ([DealerId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DealerDueInfo'
CREATE INDEX [IX_FK_DealerDueInfo]
ON [dbo].[DueInfoes]
    ([DealerId]);
GO

-- Creating foreign key on [RegionId] in table 'Dealers'
ALTER TABLE [dbo].[Dealers]
ADD CONSTRAINT [FK_DealerRegion]
    FOREIGN KEY ([RegionId])
    REFERENCES [dbo].[Regions]
        ([RegionId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DealerRegion'
CREATE INDEX [IX_FK_DealerRegion]
ON [dbo].[Dealers]
    ([RegionId]);
GO

-- Creating foreign key on [DealerId] in table 'SalesReturns'
ALTER TABLE [dbo].[SalesReturns]
ADD CONSTRAINT [FK_DealerSalesReturn]
    FOREIGN KEY ([DealerId])
    REFERENCES [dbo].[Dealers]
        ([DealerId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DealerSalesReturn'
CREATE INDEX [IX_FK_DealerSalesReturn]
ON [dbo].[SalesReturns]
    ([DealerId]);
GO

-- Creating foreign key on [SalesOfficerId] in table 'Dealers'
ALTER TABLE [dbo].[Dealers]
ADD CONSTRAINT [FK_EmployeeDealer]
    FOREIGN KEY ([SalesOfficerId])
    REFERENCES [dbo].[Employees]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeDealer'
CREATE INDEX [IX_FK_EmployeeDealer]
ON [dbo].[Dealers]
    ([SalesOfficerId]);
GO

-- Creating foreign key on [DealerId] in table 'YearSummaryDealers'
ALTER TABLE [dbo].[YearSummaryDealers]
ADD CONSTRAINT [FK_YearSummaryDealerDealer]
    FOREIGN KEY ([DealerId])
    REFERENCES [dbo].[Dealers]
        ([DealerId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ProductId] in table 'DueInfoes'
ALTER TABLE [dbo].[DueInfoes]
ADD CONSTRAINT [FK_ProductDueInfo]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([ProductId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductDueInfo'
CREATE INDEX [IX_FK_ProductDueInfo]
ON [dbo].[DueInfoes]
    ([ProductId]);
GO

-- Creating foreign key on [PlacedById] in table 'Expenditures'
ALTER TABLE [dbo].[Expenditures]
ADD CONSTRAINT [FK_EmployeeExpenditure]
    FOREIGN KEY ([PlacedById])
    REFERENCES [dbo].[Employees]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeExpenditure'
CREATE INDEX [IX_FK_EmployeeExpenditure]
ON [dbo].[Expenditures]
    ([PlacedById]);
GO

-- Creating foreign key on [PlacedToId] in table 'Expenditures'
ALTER TABLE [dbo].[Expenditures]
ADD CONSTRAINT [FK_EmployeeExpenditure1]
    FOREIGN KEY ([PlacedToId])
    REFERENCES [dbo].[Employees]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeExpenditure1'
CREATE INDEX [IX_FK_EmployeeExpenditure1]
ON [dbo].[Expenditures]
    ([PlacedToId]);
GO

-- Creating foreign key on [SalesOfficerId] in table 'ExpenditureInfoes'
ALTER TABLE [dbo].[ExpenditureInfoes]
ADD CONSTRAINT [FK_EmployeeExpenditureInfo]
    FOREIGN KEY ([SalesOfficerId])
    REFERENCES [dbo].[Employees]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeExpenditureInfo'
CREATE INDEX [IX_FK_EmployeeExpenditureInfo]
ON [dbo].[ExpenditureInfoes]
    ([SalesOfficerId]);
GO

-- Creating foreign key on [StoreInChargeId] in table 'Inventories'
ALTER TABLE [dbo].[Inventories]
ADD CONSTRAINT [FK_EmployeeInventory]
    FOREIGN KEY ([StoreInChargeId])
    REFERENCES [dbo].[Employees]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeInventory'
CREATE INDEX [IX_FK_EmployeeInventory]
ON [dbo].[Inventories]
    ([StoreInChargeId]);
GO

-- Creating foreign key on [DispatchOfficerId] in table 'Inventories'
ALTER TABLE [dbo].[Inventories]
ADD CONSTRAINT [FK_EmployeeInventory1]
    FOREIGN KEY ([DispatchOfficerId])
    REFERENCES [dbo].[Employees]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeInventory1'
CREATE INDEX [IX_FK_EmployeeInventory1]
ON [dbo].[Inventories]
    ([DispatchOfficerId]);
GO

-- Creating foreign key on [SendFromId] in table 'MessageDeliveries'
ALTER TABLE [dbo].[MessageDeliveries]
ADD CONSTRAINT [FK_EmployeeMessageDelivery]
    FOREIGN KEY ([SendFromId])
    REFERENCES [dbo].[Employees]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeMessageDelivery'
CREATE INDEX [IX_FK_EmployeeMessageDelivery]
ON [dbo].[MessageDeliveries]
    ([SendFromId]);
GO

-- Creating foreign key on [IssuedById] in table 'MRRs'
ALTER TABLE [dbo].[MRRs]
ADD CONSTRAINT [FK_EmployeeMRR]
    FOREIGN KEY ([IssuedById])
    REFERENCES [dbo].[Employees]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeMRR'
CREATE INDEX [IX_FK_EmployeeMRR]
ON [dbo].[MRRs]
    ([IssuedById]);
GO

-- Creating foreign key on [IssuedById] in table 'NoticeBoards'
ALTER TABLE [dbo].[NoticeBoards]
ADD CONSTRAINT [FK_EmployeeNoticeBoard]
    FOREIGN KEY ([IssuedById])
    REFERENCES [dbo].[Employees]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeNoticeBoard'
CREATE INDEX [IX_FK_EmployeeNoticeBoard]
ON [dbo].[NoticeBoards]
    ([IssuedById]);
GO

-- Creating foreign key on [IssuedById] in table 'PLRs'
ALTER TABLE [dbo].[PLRs]
ADD CONSTRAINT [FK_EmployeePLR]
    FOREIGN KEY ([IssuedById])
    REFERENCES [dbo].[Employees]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeePLR'
CREATE INDEX [IX_FK_EmployeePLR]
ON [dbo].[PLRs]
    ([IssuedById]);
GO

-- Creating foreign key on [IssuedToId] in table 'PLRs'
ALTER TABLE [dbo].[PLRs]
ADD CONSTRAINT [FK_EmployeePLR1]
    FOREIGN KEY ([IssuedToId])
    REFERENCES [dbo].[Employees]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeePLR1'
CREATE INDEX [IX_FK_EmployeePLR1]
ON [dbo].[PLRs]
    ([IssuedToId]);
GO

-- Creating foreign key on [NSMId] in table 'ProductEdits'
ALTER TABLE [dbo].[ProductEdits]
ADD CONSTRAINT [FK_EmployeeProductEdit]
    FOREIGN KEY ([NSMId])
    REFERENCES [dbo].[Employees]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeProductEdit'
CREATE INDEX [IX_FK_EmployeeProductEdit]
ON [dbo].[ProductEdits]
    ([NSMId]);
GO

-- Creating foreign key on [AdminId] in table 'ProductEdits'
ALTER TABLE [dbo].[ProductEdits]
ADD CONSTRAINT [FK_EmployeeApprovesProductEdits]
    FOREIGN KEY ([AdminId])
    REFERENCES [dbo].[Employees]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeApprovesProductEdits'
CREATE INDEX [IX_FK_EmployeeApprovesProductEdits]
ON [dbo].[ProductEdits]
    ([AdminId]);
GO

-- Creating foreign key on [RegionId] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [FK_EmployeeRegion]
    FOREIGN KEY ([RegionId])
    REFERENCES [dbo].[Regions]
        ([RegionId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeRegion'
CREATE INDEX [IX_FK_EmployeeRegion]
ON [dbo].[Employees]
    ([RegionId]);
GO

-- Creating foreign key on [IssuedById] in table 'Requisitions'
ALTER TABLE [dbo].[Requisitions]
ADD CONSTRAINT [FK_EmployeeRequisition]
    FOREIGN KEY ([IssuedById])
    REFERENCES [dbo].[Employees]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeRequisition'
CREATE INDEX [IX_FK_EmployeeRequisition]
ON [dbo].[Requisitions]
    ([IssuedById]);
GO

-- Creating foreign key on [IssuedToId] in table 'Requisitions'
ALTER TABLE [dbo].[Requisitions]
ADD CONSTRAINT [FK_EmployeeRequisition1]
    FOREIGN KEY ([IssuedToId])
    REFERENCES [dbo].[Employees]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeRequisition1'
CREATE INDEX [IX_FK_EmployeeRequisition1]
ON [dbo].[Requisitions]
    ([IssuedToId]);
GO

-- Creating foreign key on [SalesOfficerId] in table 'SalesOfficerTargets'
ALTER TABLE [dbo].[SalesOfficerTargets]
ADD CONSTRAINT [FK_EmployeeSalesOfficerTarget]
    FOREIGN KEY ([SalesOfficerId])
    REFERENCES [dbo].[Employees]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [RMId] in table 'SalesReturns'
ALTER TABLE [dbo].[SalesReturns]
ADD CONSTRAINT [FK_EmployeeSalesReturn]
    FOREIGN KEY ([RMId])
    REFERENCES [dbo].[Employees]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeSalesReturn'
CREATE INDEX [IX_FK_EmployeeSalesReturn]
ON [dbo].[SalesReturns]
    ([RMId]);
GO

-- Creating foreign key on [SendToId] in table 'MessageDeliveries'
ALTER TABLE [dbo].[MessageDeliveries]
ADD CONSTRAINT [FK_MessageDeliveryEmployee]
    FOREIGN KEY ([SendToId])
    REFERENCES [dbo].[Employees]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MessageDeliveryEmployee'
CREATE INDEX [IX_FK_MessageDeliveryEmployee]
ON [dbo].[MessageDeliveries]
    ([SendToId]);
GO

-- Creating foreign key on [PersonId] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [FK_PersonEmployee]
    FOREIGN KEY ([PersonId])
    REFERENCES [dbo].[People]
        ([PersonId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonEmployee'
CREATE INDEX [IX_FK_PersonEmployee]
ON [dbo].[Employees]
    ([PersonId]);
GO

-- Creating foreign key on [SalesOfficerId] in table 'YearSummarySOExpenditures'
ALTER TABLE [dbo].[YearSummarySOExpenditures]
ADD CONSTRAINT [FK_YearSummarySOExpenditureEmployee]
    FOREIGN KEY ([SalesOfficerId])
    REFERENCES [dbo].[Employees]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ExpenditureId] in table 'ExpenditureInfoes'
ALTER TABLE [dbo].[ExpenditureInfoes]
ADD CONSTRAINT [FK_ExpenditureExpenditureInfo]
    FOREIGN KEY ([ExpenditureId])
    REFERENCES [dbo].[Expenditures]
        ([ExpenditureId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [RegionId] in table 'Expenditures'
ALTER TABLE [dbo].[Expenditures]
ADD CONSTRAINT [FK_RegionExpenditure]
    FOREIGN KEY ([RegionId])
    REFERENCES [dbo].[Regions]
        ([RegionId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RegionExpenditure'
CREATE INDEX [IX_FK_RegionExpenditure]
ON [dbo].[Expenditures]
    ([RegionId]);
GO

-- Creating foreign key on [IndentId] in table 'IndentProductInfoes'
ALTER TABLE [dbo].[IndentProductInfoes]
ADD CONSTRAINT [FK_IndentProductInfoIndent]
    FOREIGN KEY ([IndentId])
    REFERENCES [dbo].[Indents]
        ([IndentId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ProductId] in table 'IndentProductInfoes'
ALTER TABLE [dbo].[IndentProductInfoes]
ADD CONSTRAINT [FK_IndentProductInfoProduct]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([ProductId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_IndentProductInfoProduct'
CREATE INDEX [IX_FK_IndentProductInfoProduct]
ON [dbo].[IndentProductInfoes]
    ([ProductId]);
GO

-- Creating foreign key on [InventoryId] in table 'InventoryLogs'
ALTER TABLE [dbo].[InventoryLogs]
ADD CONSTRAINT [FK_InventoryInventoryLog]
    FOREIGN KEY ([InventoryId])
    REFERENCES [dbo].[Inventories]
        ([InventoryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_InventoryInventoryLog'
CREATE INDEX [IX_FK_InventoryInventoryLog]
ON [dbo].[InventoryLogs]
    ([InventoryId]);
GO

-- Creating foreign key on [InventoryId] in table 'InventoryPackageInfoes'
ALTER TABLE [dbo].[InventoryPackageInfoes]
ADD CONSTRAINT [FK_InventoryInventoryPackageInfo]
    FOREIGN KEY ([InventoryId])
    REFERENCES [dbo].[Inventories]
        ([InventoryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [InventoryId] in table 'InventoryProductInfoes'
ALTER TABLE [dbo].[InventoryProductInfoes]
ADD CONSTRAINT [FK_InventoryInventoryProductInfo]
    FOREIGN KEY ([InventoryId])
    REFERENCES [dbo].[Inventories]
        ([InventoryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [InventoryId] in table 'MRRs'
ALTER TABLE [dbo].[MRRs]
ADD CONSTRAINT [FK_InventoryMRR]
    FOREIGN KEY ([InventoryId])
    REFERENCES [dbo].[Inventories]
        ([InventoryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_InventoryMRR'
CREATE INDEX [IX_FK_InventoryMRR]
ON [dbo].[MRRs]
    ([InventoryId]);
GO

-- Creating foreign key on [InventoryId] in table 'PLRs'
ALTER TABLE [dbo].[PLRs]
ADD CONSTRAINT [FK_InventoryPLR]
    FOREIGN KEY ([InventoryId])
    REFERENCES [dbo].[Inventories]
        ([InventoryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_InventoryPLR'
CREATE INDEX [IX_FK_InventoryPLR]
ON [dbo].[PLRs]
    ([InventoryId]);
GO

-- Creating foreign key on [InventoryId] in table 'Requisitions'
ALTER TABLE [dbo].[Requisitions]
ADD CONSTRAINT [FK_InventoryRequisition]
    FOREIGN KEY ([InventoryId])
    REFERENCES [dbo].[Inventories]
        ([InventoryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_InventoryRequisition'
CREATE INDEX [IX_FK_InventoryRequisition]
ON [dbo].[Requisitions]
    ([InventoryId]);
GO

-- Creating foreign key on [InventoryId] in table 'YearSummaryInventoryPackages'
ALTER TABLE [dbo].[YearSummaryInventoryPackages]
ADD CONSTRAINT [FK_InventoryYearSummaryInventoryPackage]
    FOREIGN KEY ([InventoryId])
    REFERENCES [dbo].[Inventories]
        ([InventoryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [InventoryId] in table 'YearSummaryInventoryProducts'
ALTER TABLE [dbo].[YearSummaryInventoryProducts]
ADD CONSTRAINT [FK_InventoryYearSummaryInventoryProduct]
    FOREIGN KEY ([InventoryId])
    REFERENCES [dbo].[Inventories]
        ([InventoryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [RegionId] in table 'Inventories'
ALTER TABLE [dbo].[Inventories]
ADD CONSTRAINT [FK_RegionInventory]
    FOREIGN KEY ([RegionId])
    REFERENCES [dbo].[Regions]
        ([RegionId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RegionInventory'
CREATE INDEX [IX_FK_RegionInventory]
ON [dbo].[Inventories]
    ([RegionId]);
GO

-- Creating foreign key on [ProductId] in table 'InventoryLogs'
ALTER TABLE [dbo].[InventoryLogs]
ADD CONSTRAINT [FK_ProductInventoryLog]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([ProductId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductInventoryLog'
CREATE INDEX [IX_FK_ProductInventoryLog]
ON [dbo].[InventoryLogs]
    ([ProductId]);
GO

-- Creating foreign key on [PackageId] in table 'InventoryPackageInfoes'
ALTER TABLE [dbo].[InventoryPackageInfoes]
ADD CONSTRAINT [FK_PackageInventoryPackageInfo]
    FOREIGN KEY ([PackageId])
    REFERENCES [dbo].[Packages]
        ([PackageId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PackageInventoryPackageInfo'
CREATE INDEX [IX_FK_PackageInventoryPackageInfo]
ON [dbo].[InventoryPackageInfoes]
    ([PackageId]);
GO

-- Creating foreign key on [ProductId] in table 'InventoryProductInfoes'
ALTER TABLE [dbo].[InventoryProductInfoes]
ADD CONSTRAINT [FK_ProductInventoryProductInfo]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([ProductId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductInventoryProductInfo'
CREATE INDEX [IX_FK_ProductInventoryProductInfo]
ON [dbo].[InventoryProductInfoes]
    ([ProductId]);
GO

-- Creating foreign key on [ProductId] in table 'Ledgers'
ALTER TABLE [dbo].[Ledgers]
ADD CONSTRAINT [FK_ProductLedger]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([ProductId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductLedger'
CREATE INDEX [IX_FK_ProductLedger]
ON [dbo].[Ledgers]
    ([ProductId]);
GO

-- Creating foreign key on [MessageId] in table 'MessageDeliveries'
ALTER TABLE [dbo].[MessageDeliveries]
ADD CONSTRAINT [FK_MessageDeliveryMessage]
    FOREIGN KEY ([MessageId])
    REFERENCES [dbo].[Messages]
        ([MessageId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MessageDeliveryMessage'
CREATE INDEX [IX_FK_MessageDeliveryMessage]
ON [dbo].[MessageDeliveries]
    ([MessageId]);
GO

-- Creating foreign key on [MRRId] in table 'MRRPackageInfoes'
ALTER TABLE [dbo].[MRRPackageInfoes]
ADD CONSTRAINT [FK_MRRMRRPackageInfo]
    FOREIGN KEY ([MRRId])
    REFERENCES [dbo].[MRRs]
        ([MRRId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [PackageId] in table 'MRRPackageInfoes'
ALTER TABLE [dbo].[MRRPackageInfoes]
ADD CONSTRAINT [FK_PackageMRRPackageInfo]
    FOREIGN KEY ([PackageId])
    REFERENCES [dbo].[Packages]
        ([PackageId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PackageMRRPackageInfo'
CREATE INDEX [IX_FK_PackageMRRPackageInfo]
ON [dbo].[MRRPackageInfoes]
    ([PackageId]);
GO

-- Creating foreign key on [MRRId] in table 'MRRProductInfoes'
ALTER TABLE [dbo].[MRRProductInfoes]
ADD CONSTRAINT [FK_MRRMRRProductInfo]
    FOREIGN KEY ([MRRId])
    REFERENCES [dbo].[MRRs]
        ([MRRId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ProductId] in table 'MRRProductInfoes'
ALTER TABLE [dbo].[MRRProductInfoes]
ADD CONSTRAINT [FK_ProductMRRProductInfo]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([ProductId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductMRRProductInfo'
CREATE INDEX [IX_FK_ProductMRRProductInfo]
ON [dbo].[MRRProductInfoes]
    ([ProductId]);
GO

-- Creating foreign key on [PackageId] in table 'PLRPackageInfoes'
ALTER TABLE [dbo].[PLRPackageInfoes]
ADD CONSTRAINT [FK_PackagePLRPackageInfo]
    FOREIGN KEY ([PackageId])
    REFERENCES [dbo].[Packages]
        ([PackageId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PackagePLRPackageInfo'
CREATE INDEX [IX_FK_PackagePLRPackageInfo]
ON [dbo].[PLRPackageInfoes]
    ([PackageId]);
GO

-- Creating foreign key on [PackageId] in table 'RequisitionPackageInfoes'
ALTER TABLE [dbo].[RequisitionPackageInfoes]
ADD CONSTRAINT [FK_PackageRequisitionPackageInfo]
    FOREIGN KEY ([PackageId])
    REFERENCES [dbo].[Packages]
        ([PackageId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PackageRequisitionPackageInfo'
CREATE INDEX [IX_FK_PackageRequisitionPackageInfo]
ON [dbo].[RequisitionPackageInfoes]
    ([PackageId]);
GO

-- Creating foreign key on [PackageId] in table 'YearSummaryInventoryPackages'
ALTER TABLE [dbo].[YearSummaryInventoryPackages]
ADD CONSTRAINT [FK_PackageYearSummaryInventoryPackage]
    FOREIGN KEY ([PackageId])
    REFERENCES [dbo].[Packages]
        ([PackageId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PackageYearSummaryInventoryPackage'
CREATE INDEX [IX_FK_PackageYearSummaryInventoryPackage]
ON [dbo].[YearSummaryInventoryPackages]
    ([PackageId]);
GO

-- Creating foreign key on [PLRId] in table 'PLRPackageInfoes'
ALTER TABLE [dbo].[PLRPackageInfoes]
ADD CONSTRAINT [FK_PLRPLRPackageInfo]
    FOREIGN KEY ([PLRId])
    REFERENCES [dbo].[PLRs]
        ([PLRId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [PLRId] in table 'PLRProductInfoes'
ALTER TABLE [dbo].[PLRProductInfoes]
ADD CONSTRAINT [FK_PLRPLRProductInfo]
    FOREIGN KEY ([PLRId])
    REFERENCES [dbo].[PLRs]
        ([PLRId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ProductId] in table 'PLRProductInfoes'
ALTER TABLE [dbo].[PLRProductInfoes]
ADD CONSTRAINT [FK_ProductPLRProductInfo]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([ProductId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductPLRProductInfo'
CREATE INDEX [IX_FK_ProductPLRProductInfo]
ON [dbo].[PLRProductInfoes]
    ([ProductId]);
GO

-- Creating foreign key on [ProductId] in table 'ProductEdits'
ALTER TABLE [dbo].[ProductEdits]
ADD CONSTRAINT [FK_ProductProductEdit]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([ProductId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductProductEdit'
CREATE INDEX [IX_FK_ProductProductEdit]
ON [dbo].[ProductEdits]
    ([ProductId]);
GO

-- Creating foreign key on [ProductId] in table 'Promotions'
ALTER TABLE [dbo].[Promotions]
ADD CONSTRAINT [FK_ProductPromotion]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([ProductId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductPromotion'
CREATE INDEX [IX_FK_ProductPromotion]
ON [dbo].[Promotions]
    ([ProductId]);
GO

-- Creating foreign key on [Product_ProductId] in table 'RegionTargets'
ALTER TABLE [dbo].[RegionTargets]
ADD CONSTRAINT [FK_ProductRegionTarget]
    FOREIGN KEY ([Product_ProductId])
    REFERENCES [dbo].[Products]
        ([ProductId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductRegionTarget'
CREATE INDEX [IX_FK_ProductRegionTarget]
ON [dbo].[RegionTargets]
    ([Product_ProductId]);
GO

-- Creating foreign key on [ProductId] in table 'RequisitionProductInfoes'
ALTER TABLE [dbo].[RequisitionProductInfoes]
ADD CONSTRAINT [FK_ProductRequisitionProductInfo]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([ProductId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductRequisitionProductInfo'
CREATE INDEX [IX_FK_ProductRequisitionProductInfo]
ON [dbo].[RequisitionProductInfoes]
    ([ProductId]);
GO

-- Creating foreign key on [ProductId] in table 'SalesReturnInfoes'
ALTER TABLE [dbo].[SalesReturnInfoes]
ADD CONSTRAINT [FK_ProductSalesReturnInfo]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([ProductId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ProductId] in table 'TransportationLosses'
ALTER TABLE [dbo].[TransportationLosses]
ADD CONSTRAINT [FK_ProductTransportationLoss]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([ProductId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductTransportationLoss'
CREATE INDEX [IX_FK_ProductTransportationLoss]
ON [dbo].[TransportationLosses]
    ([ProductId]);
GO

-- Creating foreign key on [ProductId] in table 'YearSummaryDealers'
ALTER TABLE [dbo].[YearSummaryDealers]
ADD CONSTRAINT [FK_ProductYearSummaryDealer]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([ProductId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductYearSummaryDealer'
CREATE INDEX [IX_FK_ProductYearSummaryDealer]
ON [dbo].[YearSummaryDealers]
    ([ProductId]);
GO

-- Creating foreign key on [ProductId] in table 'YearSummaryInventoryProducts'
ALTER TABLE [dbo].[YearSummaryInventoryProducts]
ADD CONSTRAINT [FK_ProductYearSummaryInventoryProduct]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([ProductId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductYearSummaryInventoryProduct'
CREATE INDEX [IX_FK_ProductYearSummaryInventoryProduct]
ON [dbo].[YearSummaryInventoryProducts]
    ([ProductId]);
GO

-- Creating foreign key on [ProductId] in table 'SalesOfficerTargets'
ALTER TABLE [dbo].[SalesOfficerTargets]
ADD CONSTRAINT [FK_SalesOfficerTargetProduct]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([ProductId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SalesOfficerTargetProduct'
CREATE INDEX [IX_FK_SalesOfficerTargetProduct]
ON [dbo].[SalesOfficerTargets]
    ([ProductId]);
GO

-- Creating foreign key on [RegionId] in table 'RegionTargets'
ALTER TABLE [dbo].[RegionTargets]
ADD CONSTRAINT [FK_RegionRegionTarget]
    FOREIGN KEY ([RegionId])
    REFERENCES [dbo].[Regions]
        ([RegionId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [RequisitionId] in table 'RequisitionPackageInfoes'
ALTER TABLE [dbo].[RequisitionPackageInfoes]
ADD CONSTRAINT [FK_RequisitionRequisitionPackageInfo]
    FOREIGN KEY ([RequisitionId])
    REFERENCES [dbo].[Requisitions]
        ([RequisitionId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [RequisitionId] in table 'RequisitionProductInfoes'
ALTER TABLE [dbo].[RequisitionProductInfoes]
ADD CONSTRAINT [FK_RequisitionRequisitionProductInfo]
    FOREIGN KEY ([RequisitionId])
    REFERENCES [dbo].[Requisitions]
        ([RequisitionId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [SalesReturnId] in table 'SalesReturnInfoes'
ALTER TABLE [dbo].[SalesReturnInfoes]
ADD CONSTRAINT [FK_SalesReturnSalesReturnInfo]
    FOREIGN KEY ([SalesReturnId])
    REFERENCES [dbo].[SalesReturns]
        ([SalesReturnId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SalesReturnSalesReturnInfo'
CREATE INDEX [IX_FK_SalesReturnSalesReturnInfo]
ON [dbo].[SalesReturnInfoes]
    ([SalesReturnId]);
GO

-- Creating foreign key on [NSMId] in table 'Commissions'
ALTER TABLE [dbo].[Commissions]
ADD CONSTRAINT [FK_EmployeeCreateCommission]
    FOREIGN KEY ([NSMId])
    REFERENCES [dbo].[Employees]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeCreateCommission'
CREATE INDEX [IX_FK_EmployeeCreateCommission]
ON [dbo].[Commissions]
    ([NSMId]);
GO

-- Creating foreign key on [AdminId] in table 'Commissions'
ALTER TABLE [dbo].[Commissions]
ADD CONSTRAINT [FK_EmployeeApproveCommission]
    FOREIGN KEY ([AdminId])
    REFERENCES [dbo].[Employees]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeApproveCommission'
CREATE INDEX [IX_FK_EmployeeApproveCommission]
ON [dbo].[Commissions]
    ([AdminId]);
GO

-- Creating foreign key on [AdminId] in table 'Packages'
ALTER TABLE [dbo].[Packages]
ADD CONSTRAINT [FK_EmployeeApprovedPackage]
    FOREIGN KEY ([AdminId])
    REFERENCES [dbo].[Employees]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeApprovedPackage'
CREATE INDEX [IX_FK_EmployeeApprovedPackage]
ON [dbo].[Packages]
    ([AdminId]);
GO

-- Creating foreign key on [NSMId] in table 'Packages'
ALTER TABLE [dbo].[Packages]
ADD CONSTRAINT [FK_EmployeeCreatedPackage]
    FOREIGN KEY ([NSMId])
    REFERENCES [dbo].[Employees]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeCreatedPackage'
CREATE INDEX [IX_FK_EmployeeCreatedPackage]
ON [dbo].[Packages]
    ([NSMId]);
GO

-- Creating foreign key on [IssuedById] in table 'Indents'
ALTER TABLE [dbo].[Indents]
ADD CONSTRAINT [FK_DealerPlacesIndent]
    FOREIGN KEY ([IssuedById])
    REFERENCES [dbo].[Dealers]
        ([DealerId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DealerPlacesIndent'
CREATE INDEX [IX_FK_DealerPlacesIndent]
ON [dbo].[Indents]
    ([IssuedById]);
GO

-- Creating foreign key on [IssuedToId] in table 'Indents'
ALTER TABLE [dbo].[Indents]
ADD CONSTRAINT [FK_RMVerifiesIndent]
    FOREIGN KEY ([IssuedToId])
    REFERENCES [dbo].[Employees]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RMVerifiesIndent'
CREATE INDEX [IX_FK_RMVerifiesIndent]
ON [dbo].[Indents]
    ([IssuedToId]);
GO

-- Creating foreign key on [BillId] in table 'SalesReturnInfoes'
ALTER TABLE [dbo].[SalesReturnInfoes]
ADD CONSTRAINT [FK_BillSalesReturnInfoes]
    FOREIGN KEY ([BillId])
    REFERENCES [dbo].[Bills]
        ([BillId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BillSalesReturnInfoes'
CREATE INDEX [IX_FK_BillSalesReturnInfoes]
ON [dbo].[SalesReturnInfoes]
    ([BillId]);
GO

-- Creating foreign key on [DealerId] in table 'Bills'
ALTER TABLE [dbo].[Bills]
ADD CONSTRAINT [FK_DealerBill]
    FOREIGN KEY ([DealerId])
    REFERENCES [dbo].[Dealers]
        ([DealerId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DealerBill'
CREATE INDEX [IX_FK_DealerBill]
ON [dbo].[Bills]
    ([DealerId]);
GO

-- Creating foreign key on [SeasonId] in table 'RegionTargets'
ALTER TABLE [dbo].[RegionTargets]
ADD CONSTRAINT [FK_SeasonRegionTargets]
    FOREIGN KEY ([SeasonId])
    REFERENCES [dbo].[Seasons]
        ([SeasonId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SeasonRegionTargets'
CREATE INDEX [IX_FK_SeasonRegionTargets]
ON [dbo].[RegionTargets]
    ([SeasonId]);
GO

-- Creating foreign key on [SeasonId] in table 'SalesOfficerTargets'
ALTER TABLE [dbo].[SalesOfficerTargets]
ADD CONSTRAINT [FK_SeasonSalesOfficerTargets]
    FOREIGN KEY ([SeasonId])
    REFERENCES [dbo].[Seasons]
        ([SeasonId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SeasonSalesOfficerTargets'
CREATE INDEX [IX_FK_SeasonSalesOfficerTargets]
ON [dbo].[SalesOfficerTargets]
    ([SeasonId]);
GO

-- Creating foreign key on [SeasonId] in table 'YearSummaryDealers'
ALTER TABLE [dbo].[YearSummaryDealers]
ADD CONSTRAINT [FK_SeasonYearSummaryDealers]
    FOREIGN KEY ([SeasonId])
    REFERENCES [dbo].[Seasons]
        ([SeasonId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SeasonYearSummaryDealers'
CREATE INDEX [IX_FK_SeasonYearSummaryDealers]
ON [dbo].[YearSummaryDealers]
    ([SeasonId]);
GO

-- Creating foreign key on [SeasonId] in table 'YearSummaryInventoryPackages'
ALTER TABLE [dbo].[YearSummaryInventoryPackages]
ADD CONSTRAINT [FK_SeasonYearSummaryInventoryPackages]
    FOREIGN KEY ([SeasonId])
    REFERENCES [dbo].[Seasons]
        ([SeasonId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SeasonYearSummaryInventoryPackages'
CREATE INDEX [IX_FK_SeasonYearSummaryInventoryPackages]
ON [dbo].[YearSummaryInventoryPackages]
    ([SeasonId]);
GO

-- Creating foreign key on [SeasonId] in table 'YearSummaryInventoryProducts'
ALTER TABLE [dbo].[YearSummaryInventoryProducts]
ADD CONSTRAINT [FK_SeasonYearSummaryInventoryProducts]
    FOREIGN KEY ([SeasonId])
    REFERENCES [dbo].[Seasons]
        ([SeasonId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SeasonYearSummaryInventoryProducts'
CREATE INDEX [IX_FK_SeasonYearSummaryInventoryProducts]
ON [dbo].[YearSummaryInventoryProducts]
    ([SeasonId]);
GO

-- Creating foreign key on [SeasonId] in table 'YearSummarySOExpenditures'
ALTER TABLE [dbo].[YearSummarySOExpenditures]
ADD CONSTRAINT [FK_SeasonYearSummarySOExpenditures]
    FOREIGN KEY ([SeasonId])
    REFERENCES [dbo].[Seasons]
        ([SeasonId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SeasonYearSummarySOExpenditures'
CREATE INDEX [IX_FK_SeasonYearSummarySOExpenditures]
ON [dbo].[YearSummarySOExpenditures]
    ([SeasonId]);
GO

-- Creating foreign key on [SendToInventoryId] in table 'SalesReturns'
ALTER TABLE [dbo].[SalesReturns]
ADD CONSTRAINT [FK_InventorySalesReturns]
    FOREIGN KEY ([SendToInventoryId])
    REFERENCES [dbo].[Inventories]
        ([InventoryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_InventorySalesReturns'
CREATE INDEX [IX_FK_InventorySalesReturns]
ON [dbo].[SalesReturns]
    ([SendToInventoryId]);
GO

-- Creating foreign key on [IssuedById] in table 'TransferIndents'
ALTER TABLE [dbo].[TransferIndents]
ADD CONSTRAINT [FK_EmployeeTransferIndents]
    FOREIGN KEY ([IssuedById])
    REFERENCES [dbo].[Employees]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeTransferIndents'
CREATE INDEX [IX_FK_EmployeeTransferIndents]
ON [dbo].[TransferIndents]
    ([IssuedById]);
GO

-- Creating foreign key on [IssuedFromInventoryId] in table 'TransferIndents'
ALTER TABLE [dbo].[TransferIndents]
ADD CONSTRAINT [FK_InventoryTransferIndents]
    FOREIGN KEY ([IssuedFromInventoryId])
    REFERENCES [dbo].[Inventories]
        ([InventoryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_InventoryTransferIndents'
CREATE INDEX [IX_FK_InventoryTransferIndents]
ON [dbo].[TransferIndents]
    ([IssuedFromInventoryId]);
GO

-- Creating foreign key on [ProductId] in table 'TransferIndentDetails'
ALTER TABLE [dbo].[TransferIndentDetails]
ADD CONSTRAINT [FK_ProductTransferIndentDetails]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([ProductId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductTransferIndentDetails'
CREATE INDEX [IX_FK_ProductTransferIndentDetails]
ON [dbo].[TransferIndentDetails]
    ([ProductId]);
GO

-- Creating foreign key on [TransferIndentId] in table 'TransferIndentDetails'
ALTER TABLE [dbo].[TransferIndentDetails]
ADD CONSTRAINT [FK_TransferIndentTransferIndentDetails]
    FOREIGN KEY ([TransferIndentId])
    REFERENCES [dbo].[TransferIndents]
        ([TransferIndentId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [IssuedByDOId] in table 'ProductTransfers'
ALTER TABLE [dbo].[ProductTransfers]
ADD CONSTRAINT [FK_ProductTransferEmployee]
    FOREIGN KEY ([IssuedByDOId])
    REFERENCES [dbo].[Employees]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductTransferEmployee'
CREATE INDEX [IX_FK_ProductTransferEmployee]
ON [dbo].[ProductTransfers]
    ([IssuedByDOId]);
GO

-- Creating foreign key on [SendFromInventoryId] in table 'ProductTransfers'
ALTER TABLE [dbo].[ProductTransfers]
ADD CONSTRAINT [FK_ProductTransferSendInventory]
    FOREIGN KEY ([SendFromInventoryId])
    REFERENCES [dbo].[Inventories]
        ([InventoryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductTransferSendInventory'
CREATE INDEX [IX_FK_ProductTransferSendInventory]
ON [dbo].[ProductTransfers]
    ([SendFromInventoryId]);
GO

-- Creating foreign key on [ProductId] in table 'ProductTransferDetails'
ALTER TABLE [dbo].[ProductTransferDetails]
ADD CONSTRAINT [FK_ProductTransferDetail_Product]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([ProductId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductTransferDetail_Product'
CREATE INDEX [IX_FK_ProductTransferDetail_Product]
ON [dbo].[ProductTransferDetails]
    ([ProductId]);
GO

-- Creating foreign key on [ProductTransferId] in table 'ProductTransferDetails'
ALTER TABLE [dbo].[ProductTransferDetails]
ADD CONSTRAINT [FK_ProductTransferDetail_ProductTransfer]
    FOREIGN KEY ([ProductTransferId])
    REFERENCES [dbo].[ProductTransfers]
        ([ProductTransferID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [TransferIndentId] in table 'ProductTransfers'
ALTER TABLE [dbo].[ProductTransfers]
ADD CONSTRAINT [FK_TransferIndent_ProductTransfer]
    FOREIGN KEY ([TransferIndentId])
    REFERENCES [dbo].[TransferIndents]
        ([TransferIndentId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TransferIndent_ProductTransfer'
CREATE INDEX [IX_FK_TransferIndent_ProductTransfer]
ON [dbo].[ProductTransfers]
    ([TransferIndentId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------