
namespace TrialBusinessManager.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;


    // The MetadataTypeAttribute identifies BillMetadata as the class
    // that carries additional metadata for the Bill class.
    [MetadataTypeAttribute(typeof(Bill.BillMetadata))]
    public partial class Bill
    {

        // This class allows you to attach custom attributes to properties
        // of the Bill class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class BillMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private BillMetadata()
            {
            }

            public long BillId { get; set; }

            [Include]
            public EntityCollection<BillPaymentInfo> BillPaymentInfoes { get; set; }
            [Include]
            public EntityCollection<BillProductInfo> BillProductInfoes { get; set; }

            public string BillCode { get; set; }

            public DateTime DateOfIssue { get; set; }

            public long DispatchedById { get; set; }

            public EntityCollection<DueInfo> DueInfoes { get; set; }

            public string EmergencyContactNo { get; set; }

            public Employee Employee { get; set; }

            public bool HasProductLoss { get; set; }

            public bool HasSalesReturn { get; set; }

            [Include]
            public Indent Indent { get; set; }

            public long IndentId { get; set; }

            public DateTime PaymentDeadline { get; set; }

            public double ProductLossCost { get; set; }

            public double SalesReturnCost { get; set; }

            public EntityCollection<SalesReturnInfo> SalesReturnInfoes { get; set; }

            public double TotalPaid { get; set; }

            public double TotalProductCost { get; set; }

            [Include]
            public EntityCollection<TransportationLoss> TransportationLosses { get; set; }

            public double TransportCost { get; set; }

            public string TransportType { get; set; }

            public string VehicleNo { get; set; }
            [Include]
            public Dealer Dealer { get; set; }
            public long DealerId { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies BillPaymentMetadata as the class
    // that carries additional metadata for the BillPayment class.
    [MetadataTypeAttribute(typeof(BillPayment.BillPaymentMetadata))]
    public partial class BillPayment
    {

        // This class allows you to attach custom attributes to properties
        // of the BillPayment class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class BillPaymentMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private BillPaymentMetadata()
            {
            }

            public DateTime AccountantFinalizeDate { get; set; }

            public long AccountantId { get; set; }

            public string BankName { get; set; }

            public long BillPaymentId { get; set; }

            public EntityCollection<BillPaymentInfo> BillPaymentInfoes { get; set; }

            public string BranchName { get; set; }

            public DateTime DateOfIssue { get; set; }

            public string DDNumber { get; set; }

            public Dealer Dealer { get; set; }

            public long DealerId { get; set; }

            public Employee Employee { get; set; }

//            public Employee Employee_1 { get; set; }

            public string PaymentMethod { get; set; }

            public long RMId { get; set; }

            public string Status { get; set; }

            public double TotalAmount { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies BillPaymentInfoMetadata as the class
    // that carries additional metadata for the BillPaymentInfo class.
    [MetadataTypeAttribute(typeof(BillPaymentInfo.BillPaymentInfoMetadata))]
    public partial class BillPaymentInfo
    {

        // This class allows you to attach custom attributes to properties
        // of the BillPaymentInfo class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class BillPaymentInfoMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private BillPaymentInfoMetadata()
            {
            }

            public double Amount { get; set; }

            public Bill Bill { get; set; }

            public long BillId { get; set; }

            public BillPayment BillPayment { get; set; }

            public long BillPaymentId { get; set; }

            public Product Product { get; set; }

            public long ProductId { get; set; }

            public bool HasRejected { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies BillProductInfoMetadata as the class
    // that carries additional metadata for the BillProductInfo class.
    [MetadataTypeAttribute(typeof(BillProductInfo.BillProductInfoMetadata))]
    public partial class BillProductInfo
    {

        // This class allows you to attach custom attributes to properties
        // of the BillProductInfo class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class BillProductInfoMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private BillProductInfoMetadata()
            {
            }

            public Bill Bill { get; set; }

            public long BillId { get; set; }

            public double ComissionPercentage { get; set; }

            public string LotId { get; set; }

            public double LotQuantity { get; set; }
               [Include]
            public Product Product { get; set; }

            public long ProductId { get; set; }

            public double ProductPrice { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies CommissionMetadata as the class
    // that carries additional metadata for the Commission class.
    [MetadataTypeAttribute(typeof(Commission.CommissionMetadata))]
    public partial class Commission
    {

        // This class allows you to attach custom attributes to properties
        // of the Commission class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class CommissionMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private CommissionMetadata()
            {
            }

            public long AdminId { get; set; }
            
            public long NSMId { get; set; }
            
            public Employee Employee { get; set; }
            
            public long CommissionId { get; set; }

            public short Duration { get; set; }

            public double Percentage { get; set; }

            public Product Product { get; set; }

            public long ProductId { get; set; }

            public DateTime IntroducedDate { get; set; }

            public string CommissionName { get; set; }

            public string CommissionStatus { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies DealerMetadata as the class
    // that carries additional metadata for the Dealer class.
    [MetadataTypeAttribute(typeof(Dealer.DealerMetadata))]
    public partial class Dealer
    {

        // This class allows you to attach custom attributes to properties
        // of the Dealer class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class DealerMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private DealerMetadata()
            {
            }

            [Required]
            public string ActivityStatus { get; set; }

            public EntityCollection<Bill> Bills { get; set; }
//            [Include]
            public EntityCollection<BillPayment> BillPayments { get; set; }

            public string BusinessType { get; set; }

            [Required]
            public string CompanyAddress { get; set; }

            [Required]
            public string CompanyName { get; set; }

            [Required]
            public string ContactNo { get; set; }

            [Required]
            public double CreditLimit { get; set; }


            public DateTime DateOfAdminApproval { get; set; }

            [Required]
            public DateTime DateOfBirth { get; set; }

            [Required]
            public DateTime DateOfRegistration { get; set; }

            public long DealerId { get; set; }

            [Required]
            public string DealershipCompany { get; set; }

            
            public EntityCollection<DueInfo> DueInfoes { get; set; }

            [Required] 
            public string EmailAddress { get; set; }

            public Employee Employee { get; set; }
            
            public DateTime ExpectedDefaulterDate { get; set; }

            public string FatherName { get; set; }

            [Required]
            public bool HasAnotherDealership { get; set; }

            [Required]
            public bool HasOwnOffice { get; set; }

            [Required]
            public DateTime LicenseIssueDate { get; set; }

            [Required]
            public string LicenseNo { get; set; }
            
            [Required]
            public string MotherName { get; set; }

            [Required]
            [StringLength(10)]
            public string Name { get; set; }

            [Required]
            public string Nationality { get; set; }

            [Required]
            public string OwnerAddress { get; set; }

            [Required]
            public string OwnerName { get; set; }

            [Required]
            public string Password { get; set; }

            [Required]
            public string PermamentAddress { get; set; }

            [Required]
            public string PictureLink { get; set; }

            [Required]
            public string PreferredTypeOfPayment { get; set; }

            [Required]
            public string PresentAddress { get; set; }
            
            [Include]
            public Region Region { get; set; }

            [Required]
            public long RegionId { get; set; }

            public long SalesOfficerId { get; set; }

            public EntityCollection<SalesReturn> SalesReturns { get; set; }

            public double TotalDue { get; set; }

            [Required]
            public string UserName { get; set; }

            public EntityCollection<YearSummaryDealer> YearSummaryDealers { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies DueInfoMetadata as the class
    // that carries additional metadata for the DueInfo class.
    [MetadataTypeAttribute(typeof(DueInfo.DueInfoMetadata))]
    public partial class DueInfo
    {

        // This class allows you to attach custom attributes to properties
        // of the DueInfo class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class DueInfoMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private DueInfoMetadata()
            {
            }

            public Bill Bill { get; set; }

            public long BillId { get; set; }

            public Dealer Dealer { get; set; }

            public long DealerId { get; set; }

            public double Due { get; set; }

            public string DueStatus { get; set; }

            public Product Product { get; set; }

            public long ProductId { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies EmployeeMetadata as the class
    // that carries additional metadata for the Employee class.
    [MetadataTypeAttribute(typeof(Employee.EmployeeMetadata))]
    public partial class Employee
    {

        // This class allows you to attach custom attributes to properties
        // of the Employee class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class EmployeeMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private EmployeeMetadata()
            {
            }

            public string ActivityStatus { get; set; }

            public EntityCollection<BillPayment> BillPayments { get; set; }



            public EntityCollection<Bill> Bills { get; set; }

            public EntityCollection<Dealer> Dealers { get; set; }

            public EntityCollection<Package> Packages { get; set; }

            public EntityCollection<Package> Packages1 { get; set; }

            public EntityCollection<Indent> Indents { get; set; }

            public string Designation { get; set; }

            public long EmployeeId { get; set; }

            public EntityCollection<ExpenditureInfo> ExpenditureInfoes { get; set; }

            public EntityCollection<Expenditure> Expenditures { get; set; }

            public EntityCollection<Expenditure> Expenditures1 { get; set; }

            public EntityCollection<Inventory> Inventories { get; set; }

            public EntityCollection<Inventory> Inventories1 { get; set; }

            public EntityCollection<MessageDelivery> MessageDeliveries { get; set; }

            public EntityCollection<MessageDelivery> MessageDeliveries1 { get; set; }

            public EntityCollection<MRR> MRRs { get; set; }

            public EntityCollection<NoticeBoard> NoticeBoards { get; set; }

            public string Password { get; set; }

            [Include]
            public Person Person { get; set; }

            public long PersonId { get; set; }

            public EntityCollection<PLR> PLRs { get; set; }

            public EntityCollection<PLR> PLRs1 { get; set; }

            public EntityCollection<ProductEdit> ProductEdits { get; set; }

            [Include]
            public Region Region { get; set; }

            public long RegionId { get; set; }

            public EntityCollection<Requisition> Requisitions { get; set; }

            public EntityCollection<SalesOfficerTarget> SalesOfficerTargets { get; set; }

            public EntityCollection<SalesReturn> SalesReturns { get; set; }

            public string UserName { get; set; }

            public EntityCollection<YearSummarySOExpenditure> YearSummarySOExpenditures { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies ExpenditureMetadata as the class
    // that carries additional metadata for the Expenditure class.
    [MetadataTypeAttribute(typeof(Expenditure.ExpenditureMetadata))]
    public partial class Expenditure
    {

        // This class allows you to attach custom attributes to properties
        // of the Expenditure class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ExpenditureMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private ExpenditureMetadata()
            {
            }

            public DateTime DateOfIssue { get; set; }

            public Employee Employee { get; set; }

            public Employee Employee1 { get; set; }

            public long ExpenditureId { get; set; }

            public string ExpenditureCode { get; set; }

            public EntityCollection<ExpenditureInfo> ExpenditureInfoes { get; set; }

            public long PlacedById { get; set; }

            public long PlacedToId { get; set; }
            [Include]
            public Region Region { get; set; }

            public long RegionId { get; set; }

            public string Status { get; set; }

            public double TotalAcceptedAmount { get; set; }

            public double TotalPlacedAmount { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies ExpenditureInfoMetadata as the class
    // that carries additional metadata for the ExpenditureInfo class.
    [MetadataTypeAttribute(typeof(ExpenditureInfo.ExpenditureInfoMetadata))]
    public partial class ExpenditureInfo
    {

        // This class allows you to attach custom attributes to properties
        // of the ExpenditureInfo class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ExpenditureInfoMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private ExpenditureInfoMetadata()
            {
            }

            public double NSMAcceptedAmount { get; set; }

            public double AccountAcceptedAmount { get; set; }

            public Employee Employee { get; set; }

            public Expenditure Expenditure { get; set; }

            public long ExpenditureId { get; set; }

            public double PlacedAmount { get; set; }

            public string Remarks { get; set; }

            public long SalesOfficerId { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies IndentMetadata as the class
    // that carries additional metadata for the Indent class.
    [MetadataTypeAttribute(typeof(Indent.IndentMetadata))]
    public partial class Indent
    {

        // This class allows you to attach custom attributes to properties
        // of the Indent class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class IndentMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private IndentMetadata()
            {
            }

            public EntityCollection<Bill> Bills { get; set; }

            public DateTime DateOfPlace { get; set; }
            
            public long ForwardedToId { get; set; }

            public long IndentId { get; set; }

            [Include]
            public EntityCollection<IndentProductInfo> IndentProductInfoes { get; set; }

            public string IndentCode { get; set; }

            public string IndentStatus { get; set; }

            public bool IsConsideredByNSM { get; set; }

            [Include]
            public Dealer Dealer { get; set; }
            public long IssuedById { get; set; }

            [Include]
            public Employee Employee1 { get; set; }
            public long IssuedToId { get; set; }

            public string PaymentMethod { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies IndentProductInfoMetadata as the class
    // that carries additional metadata for the IndentProductInfo class.
    [MetadataTypeAttribute(typeof(IndentProductInfo.IndentProductInfoMetadata))]
    public partial class IndentProductInfo
    {

        // This class allows you to attach custom attributes to properties
        // of the IndentProductInfo class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class IndentProductInfoMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private IndentProductInfoMetadata()
            {
            }

            public double CommissionPercentage { get; set; }
            
            public DateTime EditTime { get; set; }
            public DateTime EditTimeRM { get; set; }
            public DateTime EditTimeSIC { get; set; }
            [Include]
            public Indent Indent { get; set; }

            public long IndentId { get; set; }
            [Include]
            public Product Product { get; set; }

            public long ProductId { get; set; }

            public double ProductPrice { get; set; }

            public double ProductQuantity { get; set; }
            public double ProductQuantityByRM { get; set; }
            public double ProductQuantityBySIC { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies InventoryMetadata as the class
    // that carries additional metadata for the Inventory class.
    [MetadataTypeAttribute(typeof(Inventory.InventoryMetadata))]
    public partial class Inventory
    {

        // This class allows you to attach custom attributes to properties
        // of the Inventory class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class InventoryMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private InventoryMetadata()
            {
            }

            public EntityCollection<InventoryLog> InventoryLogs { get; set; }

            public EntityCollection<InventoryPackageInfo> InventoryPackageInfoes { get; set; }

            [Include]
            public EntityCollection<InventoryProductInfo> InventoryProductInfoes { get; set; }

            public EntityCollection<MRR> MRRs { get; set; }

            public EntityCollection<PLR> PLRs { get; set; }

            public EntityCollection<Requisition> Requisitions { get; set; }

            public EntityCollection<YearSummaryInventoryPackage> YearSummaryInventoryPackages { get; set; }

            public EntityCollection<YearSummaryInventoryProduct> YearSummaryInventoryProducts { get; set; }

            public Region Region { get; set; }

            public long RegionId { get; set; }

            [Include]
            public Employee Employee { get; set; }

            public long StoreInChargeId { get; set; }

            [Include]
            public Employee Employee1 { get; set; }

            public long DispatchOfficerId { get; set; }

            public long InventoryId { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies InventoryLogMetadata as the class
    // that carries additional metadata for the InventoryLog class.
    [MetadataTypeAttribute(typeof(InventoryLog.InventoryLogMetadata))]
    public partial class InventoryLog
    {

        // This class allows you to attach custom attributes to properties
        // of the InventoryLog class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class InventoryLogMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private InventoryLogMetadata()
            {
            }

            public double ClosingQuantity { get; set; }

            public DateTime Date { get; set; }

            public Inventory Inventory { get; set; }

            public long InventoryId { get; set; }

            public string LotId { get; set; }

            public long MemoNo { get; set; }

            public string Method { get; set; }

            public double OpeningQuantity { get; set; }

            public Product Product { get; set; }

            public long ProductId { get; set; }

            public double ProductQuantity { get; set; }

            public double ProductCost { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies InventoryPackageInfoMetadata as the class
    // that carries additional metadata for the InventoryPackageInfo class.
    [MetadataTypeAttribute(typeof(InventoryPackageInfo.InventoryPackageInfoMetadata))]
    public partial class InventoryPackageInfo
    {

        // This class allows you to attach custom attributes to properties
        // of the InventoryPackageInfo class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class InventoryPackageInfoMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private InventoryPackageInfoMetadata()
            {
            }

            public double FinishedQuantity { get; set; }

            public Inventory Inventory { get; set; }

            public long InventoryId { get; set; }

            public double OnProcessingQuantity { get; set; }
            
            [Include]
            public Package Package { get; set; }

            public long PackageId { get; set; }

            public double UnfinishedQuantity { get; set; }

            public double UnitCost { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies InventoryProductInfoMetadata as the class
    // that carries additional metadata for the InventoryProductInfo class.
    [MetadataTypeAttribute(typeof(InventoryProductInfo.InventoryProductInfoMetadata))]
    public partial class InventoryProductInfo
    {

        // This class allows you to attach custom attributes to properties
        // of the InventoryProductInfo class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class InventoryProductInfoMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private InventoryProductInfoMetadata()
            {
            }

            public double FinishedQuantity { get; set; }

            public Inventory Inventory { get; set; }

            public long InventoryId { get; set; }

            public string LotId { get; set; }

            public double OnProcessingQuantity { get; set; }
            [Include]
            public Product Product { get; set; }

            public long ProductId { get; set; }

            public double UnfinishedQuantity { get; set; }

            public double UnitLotCost { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies LedgerMetadata as the class
    // that carries additional metadata for the Ledger class.
    [MetadataTypeAttribute(typeof(Ledger.LedgerMetadata))]
    public partial class Ledger
    {

        // This class allows you to attach custom attributes to properties
        // of the Ledger class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class LedgerMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private LedgerMetadata()
            {
            }

            public double CreditAmount { get; set; }

            public DateTime Date { get; set; }

            public bool IsDealerOrEmployee { get; set; }

            public bool IsDebitOrCredit { get; set; }

            public long MemoNo { get; set; }

            public string Method { get; set; }

            public long PartyId { get; set; }

            public Product Product { get; set; }

            public long ProductId { get; set; }

            public double ProductQuantity { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies MessageMetadata as the class
    // that carries additional metadata for the Message class.
    [MetadataTypeAttribute(typeof(Message.MessageMetadata))]
    public partial class Message
    {

        // This class allows you to attach custom attributes to properties
        // of the Message class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class MessageMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private MessageMetadata()
            {
            }

            public string Body { get; set; }

            public EntityCollection<MessageDelivery> MessageDeliveries { get; set; }

            public long MessageId { get; set; }

            public string MessageType { get; set; }

            public string Subject { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies MessageDeliveryMetadata as the class
    // that carries additional metadata for the MessageDelivery class.
    [MetadataTypeAttribute(typeof(MessageDelivery.MessageDeliveryMetadata))]
    public partial class MessageDelivery
    {

        // This class allows you to attach custom attributes to properties
        // of the MessageDelivery class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class MessageDeliveryMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private MessageDeliveryMetadata()
            {
            }

            public DateTime DateOfIssue { get; set; }

            public Employee Employee { get; set; }

            public Employee Employee1 { get; set; }

            public Message Message { get; set; }

            public long MessageId { get; set; }

            public string OfficeCode { get; set; }

            public long SendFromId { get; set; }

            public long SendToId { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies MRRMetadata as the class
    // that carries additional metadata for the MRR class.
    [MetadataTypeAttribute(typeof(MRR.MRRMetadata))]
    public partial class MRR
    {

        // This class allows you to attach custom attributes to properties
        // of the MRR class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class MRRMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private MRRMetadata()
            {
            }

            public DateTime DateOfApproval { get; set; }

            public DateTime DateOfIssue { get; set; }
            [Include]
            public Employee Employee { get; set; }

            public Inventory Inventory { get; set; }

            public long InventoryId { get; set; }

            public long IssuedById { get; set; }

            public long IssuedToId { get; set; }

            public long MRRId { get; set; }

            public EntityCollection<MRRPackageInfo> MRRPackageInfoes { get; set; }

            public EntityCollection<MRRProductInfo> MRRProductInfoes { get; set; }

            public string Status { get; set; }
            
            public string MRRCode { get; set; }

            public string PurchaseOrderNo { get; set; }

            public string ChallanNo { get; set; }

            public string Wing { get; set; }

            public string RetailerName { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies MRRPackageInfoMetadata as the class
    // that carries additional metadata for the MRRPackageInfo class.
    [MetadataTypeAttribute(typeof(MRRPackageInfo.MRRPackageInfoMetadata))]
    public partial class MRRPackageInfo
    {

        // This class allows you to attach custom attributes to properties
        // of the MRRPackageInfo class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class MRRPackageInfoMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private MRRPackageInfoMetadata()
            {
            }

            public double AcceptedQuantity { get; set; }

            [Include]
            public MRR MRR { get; set; }

            public long MRRId { get; set; }
            [Include]
            public Package Package { get; set; }

            public long PackageId { get; set; }

            public double PlacedQuantity { get; set; }

            public double PurchasePrice { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies MRRProductInfoMetadata as the class
    // that carries additional metadata for the MRRProductInfo class.
    [MetadataTypeAttribute(typeof(MRRProductInfo.MRRProductInfoMetadata))]
    public partial class MRRProductInfo
    {

        // This class allows you to attach custom attributes to properties
        // of the MRRProductInfo class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class MRRProductInfoMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private MRRProductInfoMetadata()
            {
            }

            public double AcceptedQuantity { get; set; }

            public string LotId { get; set; }

            [Include]
            public MRR MRR { get; set; }

            public long MRRId { get; set; }

            public double PlacedQuantity { get; set; }
            
            [Include]
            public Product Product { get; set; }

            public long ProductId { get; set; }

            public double PurchasePrice { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies NoticeBoardMetadata as the class
    // that carries additional metadata for the NoticeBoard class.
    [MetadataTypeAttribute(typeof(NoticeBoard.NoticeBoardMetadata))]
    public partial class NoticeBoard
    {

        // This class allows you to attach custom attributes to properties
        // of the NoticeBoard class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class NoticeBoardMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private NoticeBoardMetadata()
            {
            }

            public Employee Employee { get; set; }

            public DateTime EndDate { get; set; }

            public bool IsActive { get; set; }

            public long IssuedById { get; set; }

            public string NoticeBody { get; set; }

            public long NoticeId { get; set; }

            public string NoticeSubject { get; set; }

            public DateTime StartDate { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies PackageMetadata as the class
    // that carries additional metadata for the Package class.
    [MetadataTypeAttribute(typeof(Package.PackageMetadata))]
    public partial class Package
    {

        // This class allows you to attach custom attributes to properties
        // of the Package class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class PackageMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private PackageMetadata()
            {
            }

            public EntityCollection<InventoryPackageInfo> InventoryPackageInfoes { get; set; }

            public EntityCollection<MRRPackageInfo> MRRPackageInfoes { get; set; }

            [Required]
            public string PackageCode { get; set; }

            public long PackageId { get; set; }

          //  [CustomValidation(typeof(TrialBusinessManager.Web.CreatePackageValidation), "IsValidPackageName")]
            [Required]
            public string PackageName { get; set; }

            [Required]
            public string PackageStatus { get; set; }

            [Include]
            public Employee Employee { get; set; }
            
            [Required]
            public long NSMId { get; set; }

            [Include]
            public Employee Employee1 { get; set; }
            [Required]
            public long AdminId { get; set; }

            [Required]
            public DateTime IntroducedDate { get; set; }

            public EntityCollection<PLRPackageInfo> PLRPackageInfoes { get; set; }

            public EntityCollection<RequisitionPackageInfo> RequisitionPackageInfoes { get; set; }

            public EntityCollection<YearSummaryInventoryPackage> YearSummaryInventoryPackages { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies PersonMetadata as the class
    // that carries additional metadata for the Person class.
    [MetadataTypeAttribute(typeof(Person.PersonMetadata))]
    public partial class Person
    {

        // This class allows you to attach custom attributes to properties
        // of the Person class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class PersonMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private PersonMetadata()
            {
            }

            public string Address { get; set; }

            public string ContactNo { get; set; }

            public DateTime DateOfBirth { get; set; }

            public DateTime DateOfJoin { get; set; }

            public string EmailAddress { get; set; }

            public EntityCollection<Employee> Employees { get; set; }

            public string Name { get; set; }

            public long PersonId { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies PLRMetadata as the class
    // that carries additional metadata for the PLR class.
    [MetadataTypeAttribute(typeof(PLR.PLRMetadata))]
    public partial class PLR
    {

        // This class allows you to attach custom attributes to properties
        // of the PLR class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class PLRMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private PLRMetadata()
            {
            }

            public DateTime DateOfApproval { get; set; }

            public DateTime DateOfIssue { get; set; }

            public string PLRCode { get; set; }

            public Employee Employee { get; set; }

            public Employee Employee1 { get; set; }

            public Inventory Inventory { get; set; }

            public long InventoryId { get; set; }

            public long IssuedById { get; set; }

            public long IssuedToId { get; set; }

            public long PLRId { get; set; }

            public EntityCollection<PLRPackageInfo> PLRPackageInfoes { get; set; }

            public EntityCollection<PLRProductInfo> PLRProductInfoes { get; set; }

            public string Status { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies PLRPackageInfoMetadata as the class
    // that carries additional metadata for the PLRPackageInfo class.
    [MetadataTypeAttribute(typeof(PLRPackageInfo.PLRPackageInfoMetadata))]
    public partial class PLRPackageInfo
    {

        // This class allows you to attach custom attributes to properties
        // of the PLRPackageInfo class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class PLRPackageInfoMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private PLRPackageInfoMetadata()
            {
            }
            
            [Include]
            public Package Package { get; set; }

            public long PackageId { get; set; }

            [Include]
            public PLR PLR { get; set; }

            public long PLRId { get; set; }

            public double Quantity { get; set; }

            public string Remarks { get; set; }

            public double LostAmount { get; set; }

            public string State { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies PLRProductInfoMetadata as the class
    // that carries additional metadata for the PLRProductInfo class.
    [MetadataTypeAttribute(typeof(PLRProductInfo.PLRProductInfoMetadata))]
    public partial class PLRProductInfo
    {

        // This class allows you to attach custom attributes to properties
        // of the PLRProductInfo class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class PLRProductInfoMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private PLRProductInfoMetadata()
            {
            }

            public string LotId { get; set; }

            [Include]
            public PLR PLR { get; set; }

            public long PLRId { get; set; }

            [Include]
            public Product Product { get; set; }

            public long ProductId { get; set; }

            public double Quantity { get; set; }

            public string Remarks { get; set; }

            public string State { get; set; }

            public double LostAmount { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies ProductMetadata as the class
    // that carries additional metadata for the Product class.
    [MetadataTypeAttribute(typeof(Product.ProductMetadata))]
    public partial class Product
    {

        // This class allows you to attach custom attributes to properties
        // of the Product class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ProductMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private ProductMetadata()
            {
            }

            public EntityCollection<BillPaymentInfo> BillPaymentInfoes { get; set; }

            public EntityCollection<BillProductInfo> BillProductInfoes { get; set; }

            [Include]
            public EntityCollection<Commission> Commissions { get; set; }

            public EntityCollection<DueInfo> DueInfoes { get; set; }

            public EntityCollection<IndentProductInfo> IndentProductInfoes { get; set; }

            public DateTime IntroducedDate { get; set; }

            public EntityCollection<InventoryLog> InventoryLogs { get; set; }

            public EntityCollection<InventoryProductInfo> InventoryProductInfoes { get; set; }

            public string BrandName { get; set; }
            [Required]
            public bool IsImported { get; set; }

            [Required]
            public bool IsOpOrHibrid { get; set; }

            public EntityCollection<Ledger> Ledgers { get; set; }

            public EntityCollection<MRRProductInfo> MRRProductInfoes { get; set; }

            public EntityCollection<PLRProductInfo> PLRProductInfoes { get; set; }

            [Required]
            public double PricePerUnit { get; set; }

            [Required]
            public string ProductCode { get; set; }

            public EntityCollection<ProductEdit> ProductEdits { get; set; }

            public long ProductId { get; set; }

            [Required]
            public string ProductName { get; set; }

            public string ProductStatus { get; set; }

            [Required]
            public string ProductType { get; set; }

            [Required]
            public string ProductWing { get; set; }

            public EntityCollection<Promotion> Promotions { get; set; }

            [Required]
            public string PurchasePeriodEnd { get; set; }

            [Required]
            public string PurchasePeriodStart { get; set; }

            public EntityCollection<RegionTarget> RegionTargets { get; set; }

            public EntityCollection<RequisitionProductInfo> RequisitionProductInfoes { get; set; }

            public EntityCollection<SalesOfficerTarget> SalesOfficerTargets { get; set; }

            [Required]
            public string SalesPeriodEnd { get; set; }

            [Required]
            public string SalesPeriodStart { get; set; }

            public EntityCollection<SalesReturnInfo> SalesReturnInfoes { get; set; }

            [Required]
            public double StockKeepingUnit { get; set; }

            public EntityCollection<TransportationLoss> TransportationLosses { get; set; }

            public EntityCollection<YearSummaryDealer> YearSummaryDealers { get; set; }

            public EntityCollection<YearSummaryInventoryProduct> YearSummaryInventoryProducts { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies ProductEditMetadata as the class
    // that carries additional metadata for the ProductEdit class.
    [MetadataTypeAttribute(typeof(ProductEdit.ProductEditMetadata))]
    public partial class ProductEdit
    {

        // This class allows you to attach custom attributes to properties
        // of the ProductEdit class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ProductEditMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private ProductEditMetadata()
            {
            }

            public long AdminId { get; set; }

            public DateTime ChangeApplicableFrom { get; set; }

            public string EditedValue { get; set; }

            public string EditStatus { get; set; }

            public string EditType { get; set; }

            public Employee Employee { get; set; }

            public long NSMId { get; set; }

            public string PreviousValue { get; set; }

            public Product Product { get; set; }

            public long ProductEditId { get; set; }

            public long ProductId { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies PromotionMetadata as the class
    // that carries additional metadata for the Promotion class.
    [MetadataTypeAttribute(typeof(Promotion.PromotionMetadata))]
    public partial class Promotion
    {

        // This class allows you to attach custom attributes to properties
        // of the Promotion class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class PromotionMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private PromotionMetadata()
            {
            }

            public DateTime EndAt { get; set; }

            public Product Product { get; set; }

            public long ProductId { get; set; }

            public double ProductPrice { get; set; }

            public double ProductQuantity { get; set; }

            public long PromotionId { get; set; }

            public string PromotionProductName { get; set; }

            public double PromotionProductQuantity { get; set; }

            public string PromotionTitle { get; set; }

            public DateTime StartAt { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies RegionMetadata as the class
    // that carries additional metadata for the Region class.
    [MetadataTypeAttribute(typeof(Region.RegionMetadata))]
    public partial class Region
    {

        // This class allows you to attach custom attributes to properties
        // of the Region class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class RegionMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private RegionMetadata()
            {
            }

            public EntityCollection<Dealer> Dealers { get; set; }

            public string DistrictName { get; set; }

            public EntityCollection<Employee> Employees { get; set; }

            public EntityCollection<Expenditure> Expenditures { get; set; }

            public EntityCollection<Inventory> Inventories { get; set; }

            public long RegionId { get; set; }

            public string RegionName { get; set; }

            public EntityCollection<RegionTarget> RegionTargets { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies RegionTargetMetadata as the class
    // that carries additional metadata for the RegionTarget class.
    [MetadataTypeAttribute(typeof(RegionTarget.RegionTargetMetadata))]
    public partial class RegionTarget
    {

        // This class allows you to attach custom attributes to properties
        // of the RegionTarget class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class RegionTargetMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private RegionTargetMetadata()
            {
            }

            public double AchievedAmount { get; set; }

            public double AchievedQuantity { get; set; }

            public DateTime EndDate { get; set; }

            public Product Product { get; set; }

            public long Product_ProductId { get; set; }

            public string ProductName { get; set; }

            public Region Region { get; set; }

            public long RegionId { get; set; }

            public DateTime StartDate { get; set; }

            public double TargetQuantity { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies RequisitionMetadata as the class
    // that carries additional metadata for the Requisition class.
    [MetadataTypeAttribute(typeof(Requisition.RequisitionMetadata))]
    public partial class Requisition
    {

        // This class allows you to attach custom attributes to properties
        // of the Requisition class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class RequisitionMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private RequisitionMetadata()
            {
            }

            public DateTime DateOfApproval { get; set; }

            public DateTime DateOfIssue { get; set; }

            public Employee Employee { get; set; }

            public Inventory Inventory { get; set; }

            public string RequisitionCode { get; set; }

            public long InventoryId { get; set; }

            public long IssuedById { get; set; }

            public long IssuedToId { get; set; }

            public long RequisitionId { get; set; }

            public EntityCollection<RequisitionPackageInfo> RequisitionPackageInfoes { get; set; }

            public EntityCollection<RequisitionProductInfo> RequisitionProductInfoes { get; set; }

            public string Status { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies RequisitionPackageInfoMetadata as the class
    // that carries additional metadata for the RequisitionPackageInfo class.
    [MetadataTypeAttribute(typeof(RequisitionPackageInfo.RequisitionPackageInfoMetadata))]
    public partial class RequisitionPackageInfo
    {

        // This class allows you to attach custom attributes to properties
        // of the RequisitionPackageInfo class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class RequisitionPackageInfoMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private RequisitionPackageInfoMetadata()
            {
            }

            public double AcceptedQuantity { get; set; }
               [Include]
            public Package Package { get; set; }

            public long PackageId { get; set; }

            public double PlacedQuantity { get; set; }

            public Requisition Requisition { get; set; }

            public long RequisitionId { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies RequisitionProductInfoMetadata as the class
    // that carries additional metadata for the RequisitionProductInfo class.
    [MetadataTypeAttribute(typeof(RequisitionProductInfo.RequisitionProductInfoMetadata))]
    public partial class RequisitionProductInfo
    {

        // This class allows you to attach custom attributes to properties
        // of the RequisitionProductInfo class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class RequisitionProductInfoMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private RequisitionProductInfoMetadata()
            {
            }

            public string LotId { get; set; }

            public double LotQuantity { get; set; }

            public double PlacedQuantity { get; set; }
            [Include]
            public Product Product { get; set; }

            public long ProductId { get; set; }

            public Requisition Requisition { get; set; }

            public long RequisitionId { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies SalesOfficerTargetMetadata as the class
    // that carries additional metadata for the SalesOfficerTarget class.
    [MetadataTypeAttribute(typeof(SalesOfficerTarget.SalesOfficerTargetMetadata))]
    public partial class SalesOfficerTarget
    {

        // This class allows you to attach custom attributes to properties
        // of the SalesOfficerTarget class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SalesOfficerTargetMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private SalesOfficerTargetMetadata()
            {
            }

            public double AchievedAmount { get; set; }

            public double AchievedQuantity { get; set; }

            public Employee Employee { get; set; }

            public DateTime EndDate { get; set; }

            public Product Product { get; set; }

            public long Product_ProductId { get; set; }

            public string ProductName { get; set; }

            public long SalesOfficerId { get; set; }

            public DateTime StartDate { get; set; }

            public double TargetQuantity { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies SalesReturnMetadata as the class
    // that carries additional metadata for the SalesReturn class.
    [MetadataTypeAttribute(typeof(SalesReturn.SalesReturnMetadata))]
    public partial class SalesReturn
    {

        // This class allows you to attach custom attributes to properties
        // of the SalesReturn class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SalesReturnMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private SalesReturnMetadata()
            {
            }

            public DateTime DateOfIssue { get; set; }

            [Include]
            public Dealer Dealer { get; set; }

            public long DealerId { get; set; }

            [Include]
            public Employee Employee { get; set; }

            public long RMId { get; set; }

            public long SalesReturnId { get; set; }

            public string SalesReturnCode { get; set; }

            public string Status { get; set; }

            public EntityCollection<SalesReturnInfo> SalesReturnInfoes { get; set; }

            public double TotalAcceptedAmount { get; set; }

            public double TotalPlacedAmount { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies SalesReturnInfoMetadata as the class
    // that carries additional metadata for the SalesReturnInfo class.
    [MetadataTypeAttribute(typeof(SalesReturnInfo.SalesReturnInfoMetadata))]
    public partial class SalesReturnInfo
    {

        // This class allows you to attach custom attributes to properties
        // of the SalesReturnInfo class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SalesReturnInfoMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private SalesReturnInfoMetadata()
            {
            }

            [Include]
            public Bill Bill { get; set; }

            public long BillId { get; set; }

            public long AcceptedQuantity { get; set; }

            public string LotId { get; set; }

            public long PlacedQuantity { get; set; }
            
            [Include]
            public Product Product { get; set; }

            public long ProductId { get; set; }

            public double ProductPrice { get; set; }

            [Include]
            public SalesReturn SalesReturn { get; set; }

            public long SalesReturnId { get; set; }
        }
        
    }

    // The MetadataTypeAttribute identifies TransportationLossMetadata as the class
    // that carries additional metadata for the TransportationLoss class.
    [MetadataTypeAttribute(typeof(TransportationLoss.TransportationLossMetadata))]
    public partial class TransportationLoss
    {

        // This class allows you to attach custom attributes to properties
        // of the TransportationLoss class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class TransportationLossMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private TransportationLossMetadata()
            {
            }

            [Include]
            public Bill Bill { get; set; }

            public long BillId { get; set; }

            [Include]
            public Product Product { get; set; }

            public long ProductId { get; set; }

            public double LossQuantity { get; set; }

            public string Remarks { get; set; }

            public bool HasBalanced { get; set; }

            public double BalancedQuantity { get; set; }

            public double UnitPrice { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies YearSummaryDealerMetadata as the class
    // that carries additional metadata for the YearSummaryDealer class.
    [MetadataTypeAttribute(typeof(YearSummaryDealer.YearSummaryDealerMetadata))]
    public partial class YearSummaryDealer
    {

        // This class allows you to attach custom attributes to properties
        // of the YearSummaryDealer class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class YearSummaryDealerMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private YearSummaryDealerMetadata()
            {
            }

            public double ClosingBalance { get; set; }

            public double CreditAmount { get; set; }

            public Dealer Dealer { get; set; }

            public long DealerId { get; set; }

            public double DebitAmount { get; set; }

            public double OpeningBalance { get; set; }

            public Product Product { get; set; }

            public long ProductId { get; set; }

            public double ProductQuantity { get; set; }

            public DateTime SeasonEnd { get; set; }

            public DateTime SeasonStart { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies YearSummaryInventoryPackageMetadata as the class
    // that carries additional metadata for the YearSummaryInventoryPackage class.
    [MetadataTypeAttribute(typeof(YearSummaryInventoryPackage.YearSummaryInventoryPackageMetadata))]
    public partial class YearSummaryInventoryPackage
    {

        // This class allows you to attach custom attributes to properties
        // of the YearSummaryInventoryPackage class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class YearSummaryInventoryPackageMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private YearSummaryInventoryPackageMetadata()
            {
            }

            public double ClosingPackage { get; set; }

            public Inventory Inventory { get; set; }

            public long InventoryId { get; set; }

            public double MRRInQuantity { get; set; }

            public double OpeningPackage { get; set; }

            public Package Package { get; set; }

            public long PackageId { get; set; }

            public double PackageUsedQuantity { get; set; }

            public double PLRLostQuantity { get; set; }

            public DateTime SessionEnd { get; set; }

            public DateTime SessionStart { get; set; }

            public double PurchaseAmount { get; set; }

            public double LostAmount { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies YearSummaryInventoryProductMetadata as the class
    // that carries additional metadata for the YearSummaryInventoryProduct class.
    [MetadataTypeAttribute(typeof(YearSummaryInventoryProduct.YearSummaryInventoryProductMetadata))]
    public partial class YearSummaryInventoryProduct
    {

        // This class allows you to attach custom attributes to properties
        // of the YearSummaryInventoryProduct class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class YearSummaryInventoryProductMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private YearSummaryInventoryProductMetadata()
            {
            }

            public double ClosingProduct { get; set; }

            public Inventory Inventory { get; set; }

            public long InventoryId { get; set; }

            public string LotId { get; set; }

            public double MRRInQuantity { get; set; }

            public double OpeningProduct { get; set; }

            public double PLRLostQuantity { get; set; }

            public Product Product { get; set; }

            public long ProductId { get; set; }

            public double ProductSellQuantity { get; set; }

            public DateTime SeasonEnd { get; set; }

            public DateTime SeasonStart { get; set; }

            public double SellAmount { get; set; }

            public double PurchaseAmount { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies YearSummarySOExpenditureMetadata as the class
    // that carries additional metadata for the YearSummarySOExpenditure class.
    [MetadataTypeAttribute(typeof(YearSummarySOExpenditure.YearSummarySOExpenditureMetadata))]
    public partial class YearSummarySOExpenditure
    {

        // This class allows you to attach custom attributes to properties
        // of the YearSummarySOExpenditure class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class YearSummarySOExpenditureMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private YearSummarySOExpenditureMetadata()
            {
            }

            public Employee Employee { get; set; }

            public long SalesOfficerId { get; set; }

            public DateTime SeasonEnd { get; set; }

            public DateTime SeasonStart { get; set; }

            public double TotalExpenditure { get; set; }
        }
    }
}
