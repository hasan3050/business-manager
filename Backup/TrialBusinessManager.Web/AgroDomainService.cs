
namespace TrialBusinessManager.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Linq;
    using System.ServiceModel.DomainServices.EntityFramework;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;
    using TrialBusinessManager.Web.Report.Model;


    // Implements application logic using the IspahaniAgroLTDEntities context.
    // TODO: Add your application logic to these methods or in additional methods.
    // TODO: Wire up authentication (Windows/ASP.NET Forms) and uncomment the following to disable anonymous access
    // Also consider adding roles to restrict access as appropriate.
    // [RequiresAuthentication]
    [EnableClientAccess()]
    [RequiresAuthentication]
    public class AgroDomainService : LinqToEntitiesDomainService<IspahaniAgroLTDEntities>
    {


        public IEnumerable<Season> GetSeasonsForReport()
        {
            IspahaniAgroLTDEntities _context = new IspahaniAgroLTDEntities();
            return _context.Seasons;
        }


        public IQueryable<Employee> GetInventoryPersonels()
        {
            return this.ObjectContext.Employees.Include("Person").Where(c => c.Designation.Equals("Store In Charge") || c.Designation.Equals("Dispatch Officer") || (c.Designation.Equals("Invalid") && c.UserName.Equals("Invalid")));
        }

        public IEnumerable<Product> GetProductsForReport()
        {
            IspahaniAgroLTDEntities Entities = new IspahaniAgroLTDEntities();
            return Entities.Products.Where(c => c.ProductName.Equals("DUMMY"));
        }

        public IEnumerable<Region> GetRegionsForReport()
        {
            IspahaniAgroLTDEntities Entities = new IspahaniAgroLTDEntities();
            return Entities.Regions;
        }


        double CalculateOpeningQuantity(long RegionId, String group, DateTime StartDate, String LotId)
        {
            IspahaniAgroLTDEntities _context = new IspahaniAgroLTDEntities();
            Inventory MyInventory = new Inventory();
            Product MyProduct = new Product();

            MyProduct = _context.Products.Where(c => c.GroupName.Equals(group)).First();
            MyInventory = _context.Inventories.Where(c => c.RegionId.Equals(RegionId)).Single();

            Season prevSeason = _context.Seasons.Where(e => e.SeasonEndDate <= StartDate).OrderBy(e => e.SeasonId).ToList().Last();
            List<InventoryLog> PrevLogs = _context.InventoryLogs.Where(e => e.InventoryId.Equals(MyInventory.InventoryId) && e.Product.GroupName.Equals(MyProduct.GroupName) && e.LotId.Equals(LotId) && e.Date >= prevSeason.SeasonEndDate && e.Date <= StartDate).ToList();
            double opening;

            try
            {
                opening = _context.YearSummaryInventoryProducts.Where(e => e.InventoryId.Equals(MyInventory.InventoryId) && e.SeasonId <= prevSeason.SeasonId && e.Product.ProductId.Equals(MyProduct.GroupName) && e.LotId.Equals(LotId)).OrderBy(e => e.SeasonId).Last().ClosingProduct;
            }
            catch (Exception ex)
            {
                opening = 0;
            }

            foreach (var item in PrevLogs)
            {
                if (item.Method.Equals("Bill")) opening -= item.ProductQuantity;
                if (item.Method.Equals("MRR")) opening += item.ProductQuantity;
                if (item.Method.Equals("Sales Return")) opening += item.ProductQuantity;
                if (item.Method.Equals("PLR")) opening -= item.ProductQuantity;
            }

            return opening;
        }

        public IEnumerable<InventoryLogModel> GetInventoryLogsForReport(int? RegionId, int? ProductId, string StartDate, string EndDate)
        {
            var context = new IspahaniAgroLTDEntities();
            List<InventoryLogModel> Models = new List<InventoryLogModel>();
            List<Product> Products = new List<Product>();
            List<Region> Regions = new List<Region>();
            var logs = context.InventoryLogs.OrderBy(c => c.Date).ToList();

            if (StartDate != null && EndDate != null)
            {
                DateTime s = DateTime.ParseExact(StartDate, "dd-MM-yyyy", null);
                DateTime p = DateTime.ParseExact(EndDate, "dd-MM-yyyy", null);

                if (!ProductId.HasValue) Products = GetProductsForReport().ToList();
                else
                {
                    Products.Add(context.Products.Where(c => c.ProductId.Equals(ProductId)).Single());
                    logs = logs.Where(c => c.Product.GroupName.Equals(Products.First().GroupName)).ToList();
                }

                if (!RegionId.HasValue) Regions = GetRegionsForReport().ToList();
                else
                {
                    Regions.Add(context.Regions.Where(c => c.RegionId.Equals(RegionId)).Single());
                    logs = logs.Where(c => c.InventoryId.Equals(Regions.First().Inventories.First().InventoryId)).ToList();
                }

                var LogGroupByRegion = logs.GroupBy(e => e.InventoryId);

                foreach (var item in LogGroupByRegion)
                {
                    var LogGroupByProduct = item.GroupBy(e => e.Product.GroupName);

                    foreach (var x in LogGroupByProduct)
                    {
                        var LogGroupByLot = item.GroupBy(e => e.LotId);

                        foreach (var MyItem in LogGroupByLot)
                        {
                            var y = MyItem.OrderBy(c => c.Date).ToList();
                            Double OpeningQuantity = CalculateOpeningQuantity(y.First().Inventory.RegionId, y.First().Product.GroupName, s, y.First().LotId);

                            foreach (var log in y)
                            {
                                InventoryLogModel LogModel = new InventoryLogModel();
                                LogModel.Date = log.Date;
                                LogModel.OpeningQuantity = OpeningQuantity;
                                LogModel.LotNumber = log.LotId;
                                LogModel.MemoType = log.Method;
                                LogModel.ProductCost = log.ProductCost;
                                LogModel.ProductName = log.Product.GroupName;
                                LogModel.ProductQuantity = log.ProductQuantity;
                                LogModel.RegionName = log.Inventory.Region.RegionName;

                                if (LogModel.MemoType.Equals("Bill")) LogModel.ClosingQuantity = LogModel.OpeningQuantity - LogModel.ProductQuantity;
                                if (LogModel.MemoType.Equals("MRR")) LogModel.ClosingQuantity = LogModel.OpeningQuantity + LogModel.ProductQuantity;
                                if (LogModel.MemoType.Equals("Sales Return")) LogModel.ClosingQuantity = LogModel.OpeningQuantity + LogModel.ProductQuantity;
                                if (LogModel.MemoType.Equals("PLR")) LogModel.ClosingQuantity = LogModel.OpeningQuantity - LogModel.ProductQuantity;

                                Models.Add(LogModel);

                                OpeningQuantity = LogModel.ClosingQuantity;
                            }
                        }
                    }
                }

            }

            else { };

            return Models;
        }

        public IEnumerable<LedgerReportModel> GetReportLedgersOnProducts(int? RegionId, int? ProductId, string StartDate, string EndDate)
        {
            List<LedgerReportModel> ReturnLedger = new List<LedgerReportModel>();
            IspahaniAgroLTDEntities _context = new IspahaniAgroLTDEntities();

            if (StartDate != null && EndDate != null)
            {
                DateTime Start = DateTime.ParseExact(StartDate, "MM-dd-yyyy", null);
                DateTime End = DateTime.ParseExact(EndDate, "MM-dd-yyyy", null);
                Season prevSeason = _context.Seasons.Where(e => e.SeasonEndDate <= Start).OrderBy(e => e.SeasonId).ToList().Last();
                List<Ledger> ledgers = _context.Ledgers.Where(e => e.IsDealerOrEmployee == true && e.Date >= Start && e.Date <= End).ToList();
                List<Ledger> prevLedger = _context.Ledgers.Where(e => e.IsDealerOrEmployee == true && e.Date >= prevSeason.SeasonEndDate && e.Date <= Start).ToList();

                if (ProductId.HasValue)
                {
                    ledgers = ledgers.Where(c => c.Product.GroupName.Equals(_context.Products.Where(e => e.ProductId.Equals(ProductId)).Single().GroupName)).ToList();
                    prevLedger = prevLedger.Where(c => c.Product.GroupName.Equals(_context.Products.Where(e => e.ProductId.Equals(ProductId)).Single().GroupName)).ToList();
                }

                if (RegionId.HasValue)
                {
                    List<Dealer> dealers = _context.Dealers.Where(e => e.RegionId == RegionId).ToList();
                    ledgers = (from d in dealers
                               join l in ledgers on d.DealerId equals l.PartyId
                               select l).ToList();

                    prevLedger = (from d in dealers
                                  join pl in prevLedger on d.DealerId equals pl.PartyId
                                  select pl).ToList();
                }

                var ledgerGroup = ledgers.GroupBy(e => e.PartyId);

                foreach (var nowLedger in ledgerGroup)
                {
                    Dealer dealer = _context.Dealers.Where(e => e.DealerId == nowLedger.Key).First();
                    var productGroup = nowLedger.GroupBy(e => e.Product.GroupName);

                    foreach (var product in productGroup)
                    {
                        LedgerReportModel temp = new LedgerReportModel();
                        temp.DealerName = dealer.Name;
                        temp.RegionName = dealer.Region.RegionName;
                        temp.SalesOfficerName = dealer.Employee.Person.Name;
                        temp.ProductName = product.Key;
                        temp.Sales = product.Where(e => e.IsDebitOrCredit == true).Sum(e => e.CreditAmount);
                        temp.Collection = product.Where(e => e.IsDebitOrCredit = false).Sum(e => e.CreditAmount);

                        double opening;
                        try
                        {
                            opening = _context.YearSummaryDealers.Where(e => e.DealerId == dealer.DealerId && e.SeasonId <= prevSeason.SeasonId && e.Product.GroupName == product.Key).OrderBy(e => e.SeasonId).Last().ClosingBalance;
                        }
                        catch (Exception ex)
                        {
                            opening = 0;
                        }

                        opening += prevLedger.Where(e => e.PartyId == dealer.DealerId && e.Product.GroupName == product.Key && e.IsDebitOrCredit == true).Sum(e => e.CreditAmount);
                        opening -= prevLedger.Where(e => e.PartyId == dealer.DealerId && e.Product.GroupName == product.Key && e.IsDebitOrCredit == false).Sum(e => e.CreditAmount);

                        temp.OpeningBalance = opening;
                        temp.ClosingBalance = opening + temp.Sales - temp.Collection;

                        ReturnLedger.Add(temp);
                    }
                }
            }

            return ReturnLedger;

        }

        public List<ProductPerformanceModel> CalculateProductPerformance(Int64? RegionId, String StartDate, String EndDate)
        {

            IspahaniAgroLTDEntities _context = new IspahaniAgroLTDEntities();
            List<Ledger> DealerLedger = new List<Ledger>();
            List<Dealer> Dealers = new List<Dealer>();
            List<ProductPerformanceModel> PerformanceList = new List<ProductPerformanceModel>();

            if (StartDate != null && EndDate != null)
            {
                DateTime S = DateTime.ParseExact(StartDate, "dd-MM-yyyy", null);
                DateTime E = DateTime.ParseExact(EndDate, "dd-MM-yyyy", null);
                DealerLedger = _context.Ledgers.Where(e => e.Date >= S && e.Date <= E).ToList();
                if (RegionId.HasValue) Dealers = _context.Dealers.Where(e => e.RegionId == RegionId).ToList();
                else Dealers = _context.Dealers.ToList();

                var DealerGroup = from ledger in DealerLedger
                                  join dealer in Dealers on ledger.PartyId equals dealer.DealerId
                                  select new { ActualLedger = ledger, SOInfo = dealer.Employee };

                var employee = from target in DealerGroup
                               group target by target.SOInfo.EmployeeId into grouping
                               select grouping;

                foreach (var SO in employee)
                {
                    var Product = from product in SO
                                  group product by product.ActualLedger.Product.ProductType into grouping
                                  select grouping;

                    foreach (var product in Product)
                    {
                        ProductPerformanceModel temp = new ProductPerformanceModel();
                        temp.SOName = product.FirstOrDefault().SOInfo.Person.Name;
                        temp.RegionName = product.FirstOrDefault().SOInfo.Region.RegionName;
                        temp.Distribution = product.Where(o => o.ActualLedger.Method == "Bill").Sum(e => e.ActualLedger.CreditAmount);
                        temp.Distribution -= product.Where(o => o.ActualLedger.Method == "Commission On Sale").Sum(e => e.ActualLedger.CreditAmount);
                        temp.Return = product.Where(o => o.ActualLedger.Method == "Sales Return").Sum(e => e.ActualLedger.CreditAmount);
                        temp.Loss = product.Where(o => o.ActualLedger.Method == "Transportation Loss").Sum(e => e.ActualLedger.CreditAmount);
                        //temp.Target = product.FirstOrDefault().SOInfo.SalesOfficerTargets.Where(e => e.Season.SeasonStartDate >= S && e.Season.SeasonEndDate >= E && e.ProductId == product.FirstOrDefault().ActualLedger.ProductId).Single().;
                        temp.ActualSale = temp.Distribution - temp.Return - temp.Loss;
                        temp.AchievedPercentage = (temp.ActualSale / temp.Target) * 100.00;
                        temp.ProductType = product.FirstOrDefault().ActualLedger.Product.ProductType;
                        PerformanceList.Add(temp);
                    }
                }
            }

            return PerformanceList;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Bills' query.
        public IQueryable<Bill> GetBills()
        {
            return this.ObjectContext.Bills.Include("Indent").Include("BillProductInfoes");
        }
        public IQueryable<Bill> GetBillLists()
        {
            return this.ObjectContext.Bills.Include("Dealer").Include("Employee");
        }


        public IQueryable<Bill> GetAllBillsForBillPayment()
        {
            return this.ObjectContext.Bills.Include("BillProductInfoes").Include("Dealer").Include("Employee").Include("TransportationLosses").Include("BillPaymentInfoes").Include("SalesReturnInfoes");
        }

        public IQueryable<Bill> GetBillsForVerification(long RegionID)
        {
            var query = from x in this.ObjectContext.Bills.Include("BillProductInfoes").Include("Dealer")
                        where x.BillStatus.Equals("Dispatched")
                        && x.Dealer.RegionId.Equals(RegionID)
                        select x;

            return query;
        }
        public IQueryable<Bill> GetBillForHistories()
        {
            return this.ObjectContext.Bills.Include("Dealer").Include("Employee").Include("TransportationLosses");
        }

        public void InsertBill(Bill bill)
        {
            if ((bill.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(bill, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Bills.AddObject(bill);
            }
        }

        public void UpdateBill(Bill currentBill)
        {
            this.ObjectContext.Bills.AttachAsModified(currentBill, this.ChangeSet.GetOriginal(currentBill));
        }

        public void DeleteBill(Bill bill)
        {
            if ((bill.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(bill, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.Bills.Attach(bill);
                this.ObjectContext.Bills.DeleteObject(bill);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'BillPayments' query.
        public IQueryable<BillPayment> GetBillPayments()
        {
            return this.ObjectContext.BillPayments;
        }

        public IQueryable<BillPayment> GetBillPaymentDetails()
        {
            return this.ObjectContext.BillPayments.Include("Dealer").Include("Employee");
        }

        public void InsertBillPayment(BillPayment billPayment)
        {
            if ((billPayment.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(billPayment, EntityState.Added);
            }
            else
            {
                this.ObjectContext.BillPayments.AddObject(billPayment);
            }
        }

        public void UpdateBillPayment(BillPayment currentBillPayment)
        {
            this.ObjectContext.BillPayments.AttachAsModified(currentBillPayment, this.ChangeSet.GetOriginal(currentBillPayment));
        }

        public void DeleteBillPayment(BillPayment billPayment)
        {
            if ((billPayment.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(billPayment, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.BillPayments.Attach(billPayment);
                this.ObjectContext.BillPayments.DeleteObject(billPayment);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'BillPaymentInfoes' query.
        public IQueryable<BillPaymentInfo> GetBillPaymentInfoes()
        {
            return this.ObjectContext.BillPaymentInfoes;
        }

        public IQueryable<BillPaymentInfo> GetBillPaymentInfoDetails()
        {
            return this.ObjectContext.BillPaymentInfoes.Include("Bill").Include("Product").Include("BillPayment");
        }

        public void InsertBillPaymentInfo(BillPaymentInfo billPaymentInfo)
        {
            if ((billPaymentInfo.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(billPaymentInfo, EntityState.Added);
            }
            else
            {
                this.ObjectContext.BillPaymentInfoes.AddObject(billPaymentInfo);
            }
        }

        public void UpdateBillPaymentInfo(BillPaymentInfo currentBillPaymentInfo)
        {
            this.ObjectContext.BillPaymentInfoes.AttachAsModified(currentBillPaymentInfo, this.ChangeSet.GetOriginal(currentBillPaymentInfo));
        }

        public void DeleteBillPaymentInfo(BillPaymentInfo billPaymentInfo)
        {
            if ((billPaymentInfo.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(billPaymentInfo, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.BillPaymentInfoes.Attach(billPaymentInfo);
                this.ObjectContext.BillPaymentInfoes.DeleteObject(billPaymentInfo);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'BillProductInfoes' query.
        public IQueryable<BillProductInfo> GetBillProductInfoes()
        {
            return this.ObjectContext.BillProductInfoes.Include("Product").Include("Bill");
        }

        public void InsertBillProductInfo(BillProductInfo billProductInfo)
        {
            if ((billProductInfo.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(billProductInfo, EntityState.Added);
            }
            else
            {
                this.ObjectContext.BillProductInfoes.AddObject(billProductInfo);
            }
        }

        public void UpdateBillProductInfo(BillProductInfo currentBillProductInfo)
        {
            this.ObjectContext.BillProductInfoes.AttachAsModified(currentBillProductInfo, this.ChangeSet.GetOriginal(currentBillProductInfo));
        }

        public void DeleteBillProductInfo(BillProductInfo billProductInfo)
        {
            if ((billProductInfo.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(billProductInfo, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.BillProductInfoes.Attach(billProductInfo);
                this.ObjectContext.BillProductInfoes.DeleteObject(billProductInfo);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Commissions' query.
        public IQueryable<Commission> GetCommissions()
        {
            return this.ObjectContext.Commissions;
        }

        public void InsertCommission(Commission commission)
        {
            if ((commission.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(commission, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Commissions.AddObject(commission);
            }
        }

        public void UpdateCommission(Commission currentCommission)
        {
            this.ObjectContext.Commissions.AttachAsModified(currentCommission, this.ChangeSet.GetOriginal(currentCommission));
        }

        public void DeleteCommission(Commission commission)
        {
            if ((commission.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(commission, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.Commissions.Attach(commission);
                this.ObjectContext.Commissions.DeleteObject(commission);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Dealers' query.
        public IQueryable<Dealer> GetDealers()
        {
            return this.ObjectContext.Dealers.Include("Region");
        }

        public Dealer GetDealerByID(Int64 id)
        {
            return this.ObjectContext.Dealers.Where(c => c.DealerId.Equals(id)).SingleOrDefault();
        }


        public void InsertDealer(Dealer dealer)
        {
            if ((dealer.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(dealer, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Dealers.AddObject(dealer);
            }
        }

        public void UpdateDealer(Dealer currentDealer)
        {
            this.ObjectContext.Dealers.AttachAsModified(currentDealer, this.ChangeSet.GetOriginal(currentDealer));
        }

        public void DeleteDealer(Dealer dealer)
        {
            if ((dealer.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(dealer, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.Dealers.Attach(dealer);
                this.ObjectContext.Dealers.DeleteObject(dealer);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'DueInfoes' query.
        public IQueryable<DueInfo> GetDueInfoes()
        {
            return this.ObjectContext.DueInfoes;
        }

        public void InsertDueInfo(DueInfo dueInfo)
        {
            if ((dueInfo.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(dueInfo, EntityState.Added);
            }
            else
            {
                this.ObjectContext.DueInfoes.AddObject(dueInfo);
            }
        }

        public void UpdateDueInfo(DueInfo currentDueInfo)
        {
            this.ObjectContext.DueInfoes.AttachAsModified(currentDueInfo, this.ChangeSet.GetOriginal(currentDueInfo));
        }

        public void DeleteDueInfo(DueInfo dueInfo)
        {
            if ((dueInfo.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(dueInfo, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.DueInfoes.Attach(dueInfo);
                this.ObjectContext.DueInfoes.DeleteObject(dueInfo);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Employees' query.
        public IQueryable<Employee> GetEmployees()
        {
            return this.ObjectContext.Employees.Include("Person").Include("Region");
        }

        public Employee GetEmployeesByUsername(String Username)
        {
            return this.ObjectContext.Employees.Include("Person").Include("Region").Where(c => c.UserName == Username).SingleOrDefault();
        }

        public Employee GetRMByRegion(String RegionName)
        {
            return this.ObjectContext.Employees.Include("Person").Include("Region").Where(c => c.Region.RegionName.Equals(RegionName) && c.Designation == "RM").SingleOrDefault();
        }

        public void InsertEmployee(Employee employee)
        {
            if ((employee.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(employee, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Employees.AddObject(employee);
            }
        }

        public void UpdateEmployee(Employee currentEmployee)
        {
            this.ObjectContext.Employees.AttachAsModified(currentEmployee, this.ChangeSet.GetOriginal(currentEmployee));
        }

        public void DeleteEmployee(Employee employee)
        {
            if ((employee.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(employee, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.Employees.Attach(employee);
                this.ObjectContext.Employees.DeleteObject(employee);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Expenditures' query.
        public IQueryable<Expenditure> GetExpenditures()
        {
            return this.ObjectContext.Expenditures.Include("Region");
        }

        public void InsertExpenditure(Expenditure expenditure)
        {
            if ((expenditure.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(expenditure, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Expenditures.AddObject(expenditure);
            }
        }

        public void UpdateExpenditure(Expenditure currentExpenditure)
        {
            this.ObjectContext.Expenditures.AttachAsModified(currentExpenditure, this.ChangeSet.GetOriginal(currentExpenditure));
        }

        public void DeleteExpenditure(Expenditure expenditure)
        {
            if ((expenditure.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(expenditure, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.Expenditures.Attach(expenditure);
                this.ObjectContext.Expenditures.DeleteObject(expenditure);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'ExpenditureInfoes' query.
        public IQueryable<ExpenditureInfo> GetExpenditureInfoes()
        {
            return this.ObjectContext.ExpenditureInfoes;
        }

        public void InsertExpenditureInfo(ExpenditureInfo expenditureInfo)
        {
            if ((expenditureInfo.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(expenditureInfo, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ExpenditureInfoes.AddObject(expenditureInfo);
            }
        }

        public void UpdateExpenditureInfo(ExpenditureInfo currentExpenditureInfo)
        {
            this.ObjectContext.ExpenditureInfoes.AttachAsModified(currentExpenditureInfo, this.ChangeSet.GetOriginal(currentExpenditureInfo));
        }

        public void DeleteExpenditureInfo(ExpenditureInfo expenditureInfo)
        {
            if ((expenditureInfo.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(expenditureInfo, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ExpenditureInfoes.Attach(expenditureInfo);
                this.ObjectContext.ExpenditureInfoes.DeleteObject(expenditureInfo);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Indents' query.
        public IQueryable<Indent> GetIndents()
        {
            return this.ObjectContext.Indents.Include("Dealer").Include("Employee1").Include("IndentProductInfoes");
        }

        public IQueryable<Indent> GetIndentDetails()
        {
            return this.ObjectContext.Indents.Include("Dealer").Include("Employee1").Include("IndentProductInfoes");
        }

        public void InsertIndent(Indent indent)
        {
            if ((indent.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(indent, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Indents.AddObject(indent);
            }
        }

        public void UpdateIndent(Indent currentIndent)
        {
            this.ObjectContext.Indents.AttachAsModified(currentIndent, this.ChangeSet.GetOriginal(currentIndent));
        }

        public void DeleteIndent(Indent indent)
        {
            if ((indent.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(indent, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.Indents.Attach(indent);
                this.ObjectContext.Indents.DeleteObject(indent);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'IndentProductInfoes' query.
        public IQueryable<IndentProductInfo> GetIndentProductInfoes()
        {
            return this.ObjectContext.IndentProductInfoes.Include("Indent").Include("Product");
        }

        public void InsertIndentProductInfo(IndentProductInfo indentProductInfo)
        {
            if ((indentProductInfo.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(indentProductInfo, EntityState.Added);
            }
            else
            {
                this.ObjectContext.IndentProductInfoes.AddObject(indentProductInfo);
            }
        }

        public void UpdateIndentProductInfo(IndentProductInfo currentIndentProductInfo)
        {
            this.ObjectContext.IndentProductInfoes.AttachAsModified(currentIndentProductInfo, this.ChangeSet.GetOriginal(currentIndentProductInfo));
        }

        public void DeleteIndentProductInfo(IndentProductInfo indentProductInfo)
        {
            if ((indentProductInfo.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(indentProductInfo, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.IndentProductInfoes.Attach(indentProductInfo);
                this.ObjectContext.IndentProductInfoes.DeleteObject(indentProductInfo);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Inventories' query.
        public IQueryable<Inventory> GetInventories()
        {
            return this.ObjectContext.Inventories;
        }

        public IQueryable<Inventory> GetInventorieWithInventoryProductInfoes()
        {
            return this.ObjectContext.Inventories.Include("InventoryProductInfoes");
        }

        public Inventory GetInventoryByRegionId(long regionID)
        {
            return this.ObjectContext.Inventories.Where(e=>e.RegionId==regionID).SingleOrDefault();
        
        }

        public void InsertInventory(Inventory inventory)
        {
            if ((inventory.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(inventory, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Inventories.AddObject(inventory);
            }
        }

        public void UpdateInventory(Inventory currentInventory)
        {
            this.ObjectContext.Inventories.AttachAsModified(currentInventory, this.ChangeSet.GetOriginal(currentInventory));
        }

        public void DeleteInventory(Inventory inventory)
        {
            if ((inventory.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(inventory, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.Inventories.Attach(inventory);
                this.ObjectContext.Inventories.DeleteObject(inventory);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'InventoryLogs' query.
        public IQueryable<InventoryLog> GetInventoryLogs()
        {
            return this.ObjectContext.InventoryLogs;
        }

        public void InsertInventoryLog(InventoryLog inventoryLog)
        {
            if ((inventoryLog.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(inventoryLog, EntityState.Added);
            }
            else
            {
                this.ObjectContext.InventoryLogs.AddObject(inventoryLog);
            }
        }

        public void UpdateInventoryLog(InventoryLog currentInventoryLog)
        {
            this.ObjectContext.InventoryLogs.AttachAsModified(currentInventoryLog, this.ChangeSet.GetOriginal(currentInventoryLog));
        }

        public void DeleteInventoryLog(InventoryLog inventoryLog)
        {
            if ((inventoryLog.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(inventoryLog, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.InventoryLogs.Attach(inventoryLog);
                this.ObjectContext.InventoryLogs.DeleteObject(inventoryLog);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'InventoryPackageInfoes' query.
        public IQueryable<InventoryPackageInfo> GetInventoryPackageInfoes()
        {
            return this.ObjectContext.InventoryPackageInfoes.Include("Package");
        }

        public void InsertInventoryPackageInfo(InventoryPackageInfo inventoryPackageInfo)
        {
            if ((inventoryPackageInfo.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(inventoryPackageInfo, EntityState.Added);
            }
            else
            {
                this.ObjectContext.InventoryPackageInfoes.AddObject(inventoryPackageInfo);
            }
        }

        public void UpdateInventoryPackageInfo(InventoryPackageInfo currentInventoryPackageInfo)
        {
            this.ObjectContext.InventoryPackageInfoes.AttachAsModified(currentInventoryPackageInfo, this.ChangeSet.GetOriginal(currentInventoryPackageInfo));
        }

        public void DeleteInventoryPackageInfo(InventoryPackageInfo inventoryPackageInfo)
        {
            if ((inventoryPackageInfo.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(inventoryPackageInfo, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.InventoryPackageInfoes.Attach(inventoryPackageInfo);
                this.ObjectContext.InventoryPackageInfoes.DeleteObject(inventoryPackageInfo);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'InventoryProductInfoes' query.
        public IQueryable<InventoryProductInfo> GetInventoryProductInfoes()
        {
            return this.ObjectContext.InventoryProductInfoes.Include("Product");
        }
        public IQueryable<InventoryProductInfo> GetInventoryProductInfoesByInventoryId(long InventoryId)
        {
            return this.ObjectContext.InventoryProductInfoes.Include("Product").Where(c => c.InventoryId.Equals(InventoryId));
        }
        public IQueryable<ProductTransfer> GetProductTransfersByInvenotryId(long InventoryID, String Status)
        {
            return this.ObjectContext.ProductTransfers.Include("Inventory").Where(c => c.RecievedToInventoryId.Equals(InventoryID) && c.ProductTransferStatus.Equals(Status));
        }
        public IQueryable<ProductTransferDetail> GetProductTransferDetailsByInvenotryID(long InventoryID, String Status)
        {
            return this.ObjectContext.ProductTransferDetails.Include("Product").Where(c => c.ProductTransfer.RecievedToInventoryId.Equals(InventoryID) && c.ProductTransfer.ProductTransferStatus.Equals(Status));
        }

        public void InsertInventoryProductInfo(InventoryProductInfo inventoryProductInfo)
        {
            if ((inventoryProductInfo.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(inventoryProductInfo, EntityState.Added);
            }
            else
            {
                this.ObjectContext.InventoryProductInfoes.AddObject(inventoryProductInfo);
            }
        }

        public void UpdateInventoryProductInfo(InventoryProductInfo currentInventoryProductInfo)
        {
            this.ObjectContext.InventoryProductInfoes.AttachAsModified(currentInventoryProductInfo, this.ChangeSet.GetOriginal(currentInventoryProductInfo));
        }

        public void DeleteInventoryProductInfo(InventoryProductInfo inventoryProductInfo)
        {
            if ((inventoryProductInfo.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(inventoryProductInfo, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.InventoryProductInfoes.Attach(inventoryProductInfo);
                this.ObjectContext.InventoryProductInfoes.DeleteObject(inventoryProductInfo);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Ledgers' query.
        public IEnumerable<Season> GetSeasons()
        {
            return this.ObjectContext.Seasons;
        }

        public void InsertSeason(Season season)
        {
            if ((season.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(season, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Seasons.AddObject(season);
            }
        }

        public void UpdateSeason(Season currentSeason)
        {
            this.ObjectContext.Seasons.AttachAsModified(currentSeason, this.ChangeSet.GetOriginal(currentSeason));
        }

        public void DeleteSeason(Season season)
        {
            if ((season.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(season, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.Seasons.Attach(season);
                this.ObjectContext.Seasons.DeleteObject(season);
            }
        }

        public IQueryable<Ledger> GetLedgers()
        {
            return this.ObjectContext.Ledgers;
        }

        public void InsertLedger(Ledger ledger)
        {
            if ((ledger.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(ledger, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Ledgers.AddObject(ledger);
            }
        }

        public void UpdateLedger(Ledger currentLedger)
        {
            this.ObjectContext.Ledgers.AttachAsModified(currentLedger, this.ChangeSet.GetOriginal(currentLedger));
        }

        public void DeleteLedger(Ledger ledger)
        {
            if ((ledger.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(ledger, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.Ledgers.Attach(ledger);
                this.ObjectContext.Ledgers.DeleteObject(ledger);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Messages' query.
        public IQueryable<Message> GetMessages()
        {
            return this.ObjectContext.Messages;
        }

        public void InsertMessage(Message message)
        {
            if ((message.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(message, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Messages.AddObject(message);
            }
        }

        public void UpdateMessage(Message currentMessage)
        {
            this.ObjectContext.Messages.AttachAsModified(currentMessage, this.ChangeSet.GetOriginal(currentMessage));
        }

        public void DeleteMessage(Message message)
        {
            if ((message.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(message, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.Messages.Attach(message);
                this.ObjectContext.Messages.DeleteObject(message);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'MessageDeliveries' query.
        public IQueryable<MessageDelivery> GetMessageDeliveries()
        {
            return this.ObjectContext.MessageDeliveries;
        }

        public void InsertMessageDelivery(MessageDelivery messageDelivery)
        {
            if ((messageDelivery.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(messageDelivery, EntityState.Added);
            }
            else
            {
                this.ObjectContext.MessageDeliveries.AddObject(messageDelivery);
            }
        }

        public void UpdateMessageDelivery(MessageDelivery currentMessageDelivery)
        {
            this.ObjectContext.MessageDeliveries.AttachAsModified(currentMessageDelivery, this.ChangeSet.GetOriginal(currentMessageDelivery));
        }

        public void DeleteMessageDelivery(MessageDelivery messageDelivery)
        {
            if ((messageDelivery.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(messageDelivery, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.MessageDeliveries.Attach(messageDelivery);
                this.ObjectContext.MessageDeliveries.DeleteObject(messageDelivery);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'MRRs' query.
        public IQueryable<MRR> GetMRRs()
        {
            return this.ObjectContext.MRRs;
        }

        public void InsertMRR(MRR mRR)
        {
            if ((mRR.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(mRR, EntityState.Added);
            }
            else
            {
                this.ObjectContext.MRRs.AddObject(mRR);
            }
        }

        public void UpdateMRR(MRR currentMRR)
        {
            this.ObjectContext.MRRs.AttachAsModified(currentMRR, this.ChangeSet.GetOriginal(currentMRR));
        }

        public void DeleteMRR(MRR mRR)
        {
            if ((mRR.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(mRR, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.MRRs.Attach(mRR);
                this.ObjectContext.MRRs.DeleteObject(mRR);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'MRRPackageInfoes' query.
        public IQueryable<MRRPackageInfo> GetMRRPackageInfoes()
        {
            return this.ObjectContext.MRRPackageInfoes.Include("Package");
        }

        public void InsertMRRPackageInfo(MRRPackageInfo mRRPackageInfo)
        {
            if ((mRRPackageInfo.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(mRRPackageInfo, EntityState.Added);
            }
            else
            {
                this.ObjectContext.MRRPackageInfoes.AddObject(mRRPackageInfo);
            }
        }

        public void UpdateMRRPackageInfo(MRRPackageInfo currentMRRPackageInfo)
        {
            this.ObjectContext.MRRPackageInfoes.AttachAsModified(currentMRRPackageInfo, this.ChangeSet.GetOriginal(currentMRRPackageInfo));
        }

        public void DeleteMRRPackageInfo(MRRPackageInfo mRRPackageInfo)
        {
            if ((mRRPackageInfo.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(mRRPackageInfo, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.MRRPackageInfoes.Attach(mRRPackageInfo);
                this.ObjectContext.MRRPackageInfoes.DeleteObject(mRRPackageInfo);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'MRRProductInfoes' query.
        public IQueryable<MRRProductInfo> GetMRRProductInfoes()
        {
            return this.ObjectContext.MRRProductInfoes.Include("Product");
        }

        public void InsertMRRProductInfo(MRRProductInfo mRRProductInfo)
        {
            if ((mRRProductInfo.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(mRRProductInfo, EntityState.Added);
            }
            else
            {
                this.ObjectContext.MRRProductInfoes.AddObject(mRRProductInfo);
            }
        }

        public void UpdateMRRProductInfo(MRRProductInfo currentMRRProductInfo)
        {
            this.ObjectContext.MRRProductInfoes.AttachAsModified(currentMRRProductInfo, this.ChangeSet.GetOriginal(currentMRRProductInfo));
        }

        public void DeleteMRRProductInfo(MRRProductInfo mRRProductInfo)
        {
            if ((mRRProductInfo.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(mRRProductInfo, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.MRRProductInfoes.Attach(mRRProductInfo);
                this.ObjectContext.MRRProductInfoes.DeleteObject(mRRProductInfo);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'NoticeBoards' query.
        public IQueryable<NoticeBoard> GetNoticeBoards()
        {
            return this.ObjectContext.NoticeBoards;
        }

        public void InsertNoticeBoard(NoticeBoard noticeBoard)
        {
            if ((noticeBoard.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(noticeBoard, EntityState.Added);
            }
            else
            {
                this.ObjectContext.NoticeBoards.AddObject(noticeBoard);
            }
        }

        public void UpdateNoticeBoard(NoticeBoard currentNoticeBoard)
        {
            this.ObjectContext.NoticeBoards.AttachAsModified(currentNoticeBoard, this.ChangeSet.GetOriginal(currentNoticeBoard));
        }

        public void DeleteNoticeBoard(NoticeBoard noticeBoard)
        {
            if ((noticeBoard.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(noticeBoard, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.NoticeBoards.Attach(noticeBoard);
                this.ObjectContext.NoticeBoards.DeleteObject(noticeBoard);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Packages' query.
        public IQueryable<Package> GetPackages()
        {
            return this.ObjectContext.Packages.Include("Employee");
        }

        public void InsertPackage(Package package)
        {
            if ((package.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(package, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Packages.AddObject(package);
            }
        }

        public void UpdatePackage(Package currentPackage)
        {
            this.ObjectContext.Packages.AttachAsModified(currentPackage, this.ChangeSet.GetOriginal(currentPackage));
        }

        public void DeletePackage(Package package)
        {
            if ((package.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(package, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.Packages.Attach(package);
                this.ObjectContext.Packages.DeleteObject(package);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'People' query.
        public IQueryable<Person> GetPeople()
        {
            return this.ObjectContext.People;
        }

        public void InsertPerson(Person person)
        {
            if ((person.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(person, EntityState.Added);
            }
            else
            {
                this.ObjectContext.People.AddObject(person);
            }
        }

        public void UpdatePerson(Person currentPerson)
        {
            this.ObjectContext.People.AttachAsModified(currentPerson, this.ChangeSet.GetOriginal(currentPerson));
        }

        public void DeletePerson(Person person)
        {
            if ((person.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(person, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.People.Attach(person);
                this.ObjectContext.People.DeleteObject(person);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'PLRs' query.
        public IQueryable<PLR> GetPLRs()
        {
            return this.ObjectContext.PLRs.Include("PLRProductInfoes").Include("PLRPackageInfoes").Include("Inventory");
        }

        public void InsertPLR(PLR pLR)
        {
            if ((pLR.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(pLR, EntityState.Added);
            }
            else
            {
                this.ObjectContext.PLRs.AddObject(pLR);
            }
        }

        public void UpdatePLR(PLR currentPLR)
        {
            this.ObjectContext.PLRs.AttachAsModified(currentPLR, this.ChangeSet.GetOriginal(currentPLR));
        }

        public void DeletePLR(PLR pLR)
        {
            if ((pLR.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(pLR, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.PLRs.Attach(pLR);
                this.ObjectContext.PLRs.DeleteObject(pLR);
            }
        }

        // TODO:
        // Consider constraining the results of your query method. If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'ProductTransfer' query.

        public IQueryable<ProductTransfer> GetProductTransfers()
        {
            return this.ObjectContext.ProductTransfers.Include("Inventory");
        }

        public void InsertProductTransfer(ProductTransfer productTransfer)
        {
            if ((productTransfer.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(productTransfer, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ProductTransfers.AddObject(productTransfer);
            }
        }

        public IQueryable<ProductTransferDetail> GetProductTransferDetails()
        {
            return this.ObjectContext.ProductTransferDetails.Include("Product");
        }

        public IQueryable<ProductTransferDetail> GetProductTransferDetailsByTransferId(long Id)
        {
            return this.ObjectContext.ProductTransferDetails.Include("Product").Where(c => c.ProductTransferId.Equals(Id));
        }

        public void UpdateProductTransferDetails(ProductTransferDetail currentProductTransferDetail)
        {
            this.ObjectContext.ProductTransferDetails.AttachAsModified(currentProductTransferDetail, this.ChangeSet.GetOriginal(currentProductTransferDetail));
        }
        public void UpdateProductTransfers(ProductTransfer currentProductTransfer)
        {
            this.ObjectContext.ProductTransfers.AttachAsModified(currentProductTransfer, this.ChangeSet.GetOriginal(currentProductTransfer));
        }

        public void InsertProductTransferDetail(ProductTransferDetail productTransferDetail)
        {
            if ((productTransferDetail.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(productTransferDetail, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ProductTransferDetails.AddObject(productTransferDetail);
            }
        }
        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'PLRPackageInfoes' query.
        public IQueryable<PLRPackageInfo> GetPLRPackageInfoes()
        {
            return this.ObjectContext.PLRPackageInfoes.Include("Package");
        }

        public void InsertPLRPackageInfo(PLRPackageInfo pLRPackageInfo)
        {
            if ((pLRPackageInfo.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(pLRPackageInfo, EntityState.Added);
            }
            else
            {
                this.ObjectContext.PLRPackageInfoes.AddObject(pLRPackageInfo);
            }
        }

        public void UpdatePLRPackageInfo(PLRPackageInfo currentPLRPackageInfo)
        {
            this.ObjectContext.PLRPackageInfoes.AttachAsModified(currentPLRPackageInfo, this.ChangeSet.GetOriginal(currentPLRPackageInfo));
        }

        public void DeletePLRPackageInfo(PLRPackageInfo pLRPackageInfo)
        {
            if ((pLRPackageInfo.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(pLRPackageInfo, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.PLRPackageInfoes.Attach(pLRPackageInfo);
                this.ObjectContext.PLRPackageInfoes.DeleteObject(pLRPackageInfo);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'PLRProductInfoes' query.
        public IQueryable<PLRProductInfo> GetPLRProductInfoes()
        {
            return this.ObjectContext.PLRProductInfoes.Include("Product");
        }

        public void InsertPLRProductInfo(PLRProductInfo pLRProductInfo)
        {
            if ((pLRProductInfo.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(pLRProductInfo, EntityState.Added);
            }
            else
            {
                this.ObjectContext.PLRProductInfoes.AddObject(pLRProductInfo);
            }
        }

        public void UpdatePLRProductInfo(PLRProductInfo currentPLRProductInfo)
        {
            this.ObjectContext.PLRProductInfoes.AttachAsModified(currentPLRProductInfo, this.ChangeSet.GetOriginal(currentPLRProductInfo));
        }

        public void DeletePLRProductInfo(PLRProductInfo pLRProductInfo)
        {
            if ((pLRProductInfo.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(pLRProductInfo, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.PLRProductInfoes.Attach(pLRProductInfo);
                this.ObjectContext.PLRProductInfoes.DeleteObject(pLRProductInfo);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Products' query.
        public IQueryable<Product> GetProducts()
        {
            return this.ObjectContext.Products.Include("Commissions");
        }

        public void InsertProduct(Product product)
        {
            if ((product.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(product, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Products.AddObject(product);
            }
        }

        public void UpdateProduct(Product currentProduct)
        {
            this.ObjectContext.Products.AttachAsModified(currentProduct, this.ChangeSet.GetOriginal(currentProduct));
        }

        public void DeleteProduct(Product product)
        {
            if ((product.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(product, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.Products.Attach(product);
                this.ObjectContext.Products.DeleteObject(product);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'ProductEdits' query.
        public IQueryable<ProductEdit> GetProductEdits()
        {
            return this.ObjectContext.ProductEdits;
        }

        public void InsertProductEdit(ProductEdit productEdit)
        {
            if ((productEdit.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(productEdit, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ProductEdits.AddObject(productEdit);
            }
        }

        public void UpdateProductEdit(ProductEdit currentProductEdit)
        {
            this.ObjectContext.ProductEdits.AttachAsModified(currentProductEdit, this.ChangeSet.GetOriginal(currentProductEdit));
        }

        public void DeleteProductEdit(ProductEdit productEdit)
        {
            if ((productEdit.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(productEdit, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ProductEdits.Attach(productEdit);
                this.ObjectContext.ProductEdits.DeleteObject(productEdit);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Promotions' query.
        public IQueryable<Promotion> GetPromotions()
        {
            return this.ObjectContext.Promotions;
        }

        public void InsertPromotion(Promotion promotion)
        {
            if ((promotion.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(promotion, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Promotions.AddObject(promotion);
            }
        }

        public void UpdatePromotion(Promotion currentPromotion)
        {
            this.ObjectContext.Promotions.AttachAsModified(currentPromotion, this.ChangeSet.GetOriginal(currentPromotion));
        }

        public void DeletePromotion(Promotion promotion)
        {
            if ((promotion.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(promotion, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.Promotions.Attach(promotion);
                this.ObjectContext.Promotions.DeleteObject(promotion);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Regions' query.
        public IQueryable<Region> GetRegions()
        {
            return this.ObjectContext.Regions.Include("Inventories");
        }

        public Region GetRegionsById(Int64 id)
        {
            return this.ObjectContext.Regions.Where(c => c.RegionId.Equals(id)).SingleOrDefault();
        }

        public void InsertRegion(Region region)
        {
            if ((region.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(region, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Regions.AddObject(region);
            }
        }

        public void UpdateRegion(Region currentRegion)
        {
            this.ObjectContext.Regions.AttachAsModified(currentRegion, this.ChangeSet.GetOriginal(currentRegion));
        }

        public void DeleteRegion(Region region)
        {
            if ((region.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(region, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.Regions.Attach(region);
                this.ObjectContext.Regions.DeleteObject(region);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'RegionTargets' query.
        public IQueryable<RegionTarget> GetRegionTargets()
        {
            return this.ObjectContext.RegionTargets;
        }

        public void InsertRegionTarget(RegionTarget regionTarget)
        {
            if ((regionTarget.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(regionTarget, EntityState.Added);
            }
            else
            {
                this.ObjectContext.RegionTargets.AddObject(regionTarget);
            }
        }

        public void UpdateRegionTarget(RegionTarget currentRegionTarget)
        {
            this.ObjectContext.RegionTargets.AttachAsModified(currentRegionTarget, this.ChangeSet.GetOriginal(currentRegionTarget));
        }

        public void DeleteRegionTarget(RegionTarget regionTarget)
        {
            if ((regionTarget.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(regionTarget, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.RegionTargets.Attach(regionTarget);
                this.ObjectContext.RegionTargets.DeleteObject(regionTarget);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Requisitions' query.
        public IQueryable<Requisition> GetRequisitions()
        {
            return this.ObjectContext.Requisitions.Include("Employee");
        }

        public void InsertRequisition(Requisition requisition)
        {
            if ((requisition.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(requisition, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Requisitions.AddObject(requisition);
            }
        }

        public void UpdateRequisition(Requisition currentRequisition)
        {
            this.ObjectContext.Requisitions.AttachAsModified(currentRequisition, this.ChangeSet.GetOriginal(currentRequisition));
        }

        public void DeleteRequisition(Requisition requisition)
        {
            if ((requisition.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(requisition, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.Requisitions.Attach(requisition);
                this.ObjectContext.Requisitions.DeleteObject(requisition);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'RequisitionPackageInfoes' query.
        public IQueryable<RequisitionPackageInfo> GetRequisitionPackageInfoes()
        {
            return this.ObjectContext.RequisitionPackageInfoes.Include("Package");
        }

        public void InsertRequisitionPackageInfo(RequisitionPackageInfo requisitionPackageInfo)
        {
            if ((requisitionPackageInfo.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(requisitionPackageInfo, EntityState.Added);
            }
            else
            {
                this.ObjectContext.RequisitionPackageInfoes.AddObject(requisitionPackageInfo);
            }
        }

        public void UpdateRequisitionPackageInfo(RequisitionPackageInfo currentRequisitionPackageInfo)
        {
            this.ObjectContext.RequisitionPackageInfoes.AttachAsModified(currentRequisitionPackageInfo, this.ChangeSet.GetOriginal(currentRequisitionPackageInfo));
        }

        public void DeleteRequisitionPackageInfo(RequisitionPackageInfo requisitionPackageInfo)
        {
            if ((requisitionPackageInfo.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(requisitionPackageInfo, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.RequisitionPackageInfoes.Attach(requisitionPackageInfo);
                this.ObjectContext.RequisitionPackageInfoes.DeleteObject(requisitionPackageInfo);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'RequisitionProductInfoes' query.
        public IQueryable<RequisitionProductInfo> GetRequisitionProductInfoes()
        {
            return this.ObjectContext.RequisitionProductInfoes.Include("Product");
        }

        public void InsertRequisitionProductInfo(RequisitionProductInfo requisitionProductInfo)
        {
            if ((requisitionProductInfo.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(requisitionProductInfo, EntityState.Added);
            }
            else
            {
                this.ObjectContext.RequisitionProductInfoes.AddObject(requisitionProductInfo);
            }
        }

        public void UpdateRequisitionProductInfo(RequisitionProductInfo currentRequisitionProductInfo)
        {
            this.ObjectContext.RequisitionProductInfoes.AttachAsModified(currentRequisitionProductInfo, this.ChangeSet.GetOriginal(currentRequisitionProductInfo));
        }

        public void DeleteRequisitionProductInfo(RequisitionProductInfo requisitionProductInfo)
        {
            if ((requisitionProductInfo.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(requisitionProductInfo, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.RequisitionProductInfoes.Attach(requisitionProductInfo);
                this.ObjectContext.RequisitionProductInfoes.DeleteObject(requisitionProductInfo);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SalesOfficerTargets' query.
        public IQueryable<SalesOfficerTarget> GetSalesOfficerTargets()
        {
            return this.ObjectContext.SalesOfficerTargets;
        }

        public void InsertSalesOfficerTarget(SalesOfficerTarget salesOfficerTarget)
        {
            if ((salesOfficerTarget.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(salesOfficerTarget, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SalesOfficerTargets.AddObject(salesOfficerTarget);
            }
        }

        public void UpdateSalesOfficerTarget(SalesOfficerTarget currentSalesOfficerTarget)
        {
            this.ObjectContext.SalesOfficerTargets.AttachAsModified(currentSalesOfficerTarget, this.ChangeSet.GetOriginal(currentSalesOfficerTarget));
        }

        public void DeleteSalesOfficerTarget(SalesOfficerTarget salesOfficerTarget)
        {
            if ((salesOfficerTarget.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(salesOfficerTarget, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SalesOfficerTargets.Attach(salesOfficerTarget);
                this.ObjectContext.SalesOfficerTargets.DeleteObject(salesOfficerTarget);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SalesReturns' query.
        public IQueryable<SalesReturn> GetSalesReturns()
        {
            return this.ObjectContext.SalesReturns.Include("Dealer").Include("SalesReturnInfoes");
        }

        public void InsertSalesReturn(SalesReturn salesReturn)
        {
            if ((salesReturn.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(salesReturn, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SalesReturns.AddObject(salesReturn);
            }
        }

        public IQueryable<DueInfo> GetDueInfoForBillPayments()
        {
            return this.ObjectContext.DueInfoes.Include("Bill").Include("Product");
        }


        public void UpdateSalesReturn(SalesReturn currentSalesReturn)
        {
            this.ObjectContext.SalesReturns.AttachAsModified(currentSalesReturn, this.ChangeSet.GetOriginal(currentSalesReturn));
        }

        public void DeleteSalesReturn(SalesReturn salesReturn)
        {
            if ((salesReturn.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(salesReturn, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SalesReturns.Attach(salesReturn);
                this.ObjectContext.SalesReturns.DeleteObject(salesReturn);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SalesReturnInfoes' query.
        public IQueryable<SalesReturnInfo> GetSalesReturnInfoes()
        {
            return this.ObjectContext.SalesReturnInfoes.Include("Product").Include("Bill").Include("SalesReturn");
        }

        public void InsertSalesReturnInfo(SalesReturnInfo salesReturnInfo)
        {
            if ((salesReturnInfo.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(salesReturnInfo, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SalesReturnInfoes.AddObject(salesReturnInfo);
            }
        }

        public void UpdateSalesReturnInfo(SalesReturnInfo currentSalesReturnInfo)
        {
            this.ObjectContext.SalesReturnInfoes.AttachAsModified(currentSalesReturnInfo, this.ChangeSet.GetOriginal(currentSalesReturnInfo));
        }

        public void DeleteSalesReturnInfo(SalesReturnInfo salesReturnInfo)
        {
            if ((salesReturnInfo.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(salesReturnInfo, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SalesReturnInfoes.Attach(salesReturnInfo);
                this.ObjectContext.SalesReturnInfoes.DeleteObject(salesReturnInfo);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'TransportationLosses' query.
        public IQueryable<TransportationLoss> GetTransportationLosses()
        {
            return this.ObjectContext.TransportationLosses;
        }
        public IQueryable<TransportationLoss> GetTransportationLossesByDOID(long DOID)
        {
            var query = from x in this.ObjectContext.TransportationLosses.Include("Product")
                        where x.HasBalanced.Equals(false)
                        && x.Bill.DispatchedById.Equals(DOID)
                        select x;
            return query;
        }

        public void InsertTransportationLoss(TransportationLoss transportationLoss)
        {
            if ((transportationLoss.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(transportationLoss, EntityState.Added);
            }
            else
            {
                this.ObjectContext.TransportationLosses.AddObject(transportationLoss);
            }
        }

        public void UpdateTransportationLoss(TransportationLoss currentTransportationLoss)
        {
            this.ObjectContext.TransportationLosses.AttachAsModified(currentTransportationLoss, this.ChangeSet.GetOriginal(currentTransportationLoss));
        }

        public void DeleteTransportationLoss(TransportationLoss transportationLoss)
        {
            if ((transportationLoss.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(transportationLoss, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.TransportationLosses.Attach(transportationLoss);
                this.ObjectContext.TransportationLosses.DeleteObject(transportationLoss);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'YearSummaryDealers' query.
        public IQueryable<YearSummaryDealer> GetYearSummaryDealers()
        {
            return this.ObjectContext.YearSummaryDealers;
        }

        public void InsertYearSummaryDealer(YearSummaryDealer yearSummaryDealer)
        {
            if ((yearSummaryDealer.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(yearSummaryDealer, EntityState.Added);
            }
            else
            {
                this.ObjectContext.YearSummaryDealers.AddObject(yearSummaryDealer);
            }
        }

        public void UpdateYearSummaryDealer(YearSummaryDealer currentYearSummaryDealer)
        {
            this.ObjectContext.YearSummaryDealers.AttachAsModified(currentYearSummaryDealer, this.ChangeSet.GetOriginal(currentYearSummaryDealer));
        }

        public void DeleteYearSummaryDealer(YearSummaryDealer yearSummaryDealer)
        {
            if ((yearSummaryDealer.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(yearSummaryDealer, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.YearSummaryDealers.Attach(yearSummaryDealer);
                this.ObjectContext.YearSummaryDealers.DeleteObject(yearSummaryDealer);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'YearSummaryInventoryPackages' query.
        public IQueryable<YearSummaryInventoryPackage> GetYearSummaryInventoryPackages()
        {
            return this.ObjectContext.YearSummaryInventoryPackages;
        }

        public void InsertYearSummaryInventoryPackage(YearSummaryInventoryPackage yearSummaryInventoryPackage)
        {
            if ((yearSummaryInventoryPackage.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(yearSummaryInventoryPackage, EntityState.Added);
            }
            else
            {
                this.ObjectContext.YearSummaryInventoryPackages.AddObject(yearSummaryInventoryPackage);
            }
        }

        public void UpdateYearSummaryInventoryPackage(YearSummaryInventoryPackage currentYearSummaryInventoryPackage)
        {
            this.ObjectContext.YearSummaryInventoryPackages.AttachAsModified(currentYearSummaryInventoryPackage, this.ChangeSet.GetOriginal(currentYearSummaryInventoryPackage));
        }

        public void DeleteYearSummaryInventoryPackage(YearSummaryInventoryPackage yearSummaryInventoryPackage)
        {
            if ((yearSummaryInventoryPackage.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(yearSummaryInventoryPackage, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.YearSummaryInventoryPackages.Attach(yearSummaryInventoryPackage);
                this.ObjectContext.YearSummaryInventoryPackages.DeleteObject(yearSummaryInventoryPackage);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'YearSummaryInventoryProducts' query.
        public IQueryable<YearSummaryInventoryProduct> GetYearSummaryInventoryProducts()
        {
            return this.ObjectContext.YearSummaryInventoryProducts;
        }

        public void InsertYearSummaryInventoryProduct(YearSummaryInventoryProduct yearSummaryInventoryProduct)
        {
            if ((yearSummaryInventoryProduct.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(yearSummaryInventoryProduct, EntityState.Added);
            }
            else
            {
                this.ObjectContext.YearSummaryInventoryProducts.AddObject(yearSummaryInventoryProduct);
            }
        }

        public void UpdateYearSummaryInventoryProduct(YearSummaryInventoryProduct currentYearSummaryInventoryProduct)
        {
            this.ObjectContext.YearSummaryInventoryProducts.AttachAsModified(currentYearSummaryInventoryProduct, this.ChangeSet.GetOriginal(currentYearSummaryInventoryProduct));
        }

        public void DeleteYearSummaryInventoryProduct(YearSummaryInventoryProduct yearSummaryInventoryProduct)
        {
            if ((yearSummaryInventoryProduct.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(yearSummaryInventoryProduct, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.YearSummaryInventoryProducts.Attach(yearSummaryInventoryProduct);
                this.ObjectContext.YearSummaryInventoryProducts.DeleteObject(yearSummaryInventoryProduct);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'YearSummarySOExpenditures' query.
        public IQueryable<YearSummarySOExpenditure> GetYearSummarySOExpenditures()
        {
            return this.ObjectContext.YearSummarySOExpenditures;
        }

        public void InsertYearSummarySOExpenditure(YearSummarySOExpenditure yearSummarySOExpenditure)
        {
            if ((yearSummarySOExpenditure.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(yearSummarySOExpenditure, EntityState.Added);
            }
            else
            {
                this.ObjectContext.YearSummarySOExpenditures.AddObject(yearSummarySOExpenditure);
            }
        }

        public void UpdateYearSummarySOExpenditure(YearSummarySOExpenditure currentYearSummarySOExpenditure)
        {
            this.ObjectContext.YearSummarySOExpenditures.AttachAsModified(currentYearSummarySOExpenditure, this.ChangeSet.GetOriginal(currentYearSummarySOExpenditure));
        }

        public void DeleteYearSummarySOExpenditure(YearSummarySOExpenditure yearSummarySOExpenditure)
        {
            if ((yearSummarySOExpenditure.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(yearSummarySOExpenditure, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.YearSummarySOExpenditures.Attach(yearSummarySOExpenditure);
                this.ObjectContext.YearSummarySOExpenditures.DeleteObject(yearSummarySOExpenditure);
            }
        }
    }
}


