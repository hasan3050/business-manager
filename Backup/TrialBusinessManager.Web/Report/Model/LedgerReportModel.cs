using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrialBusinessManager.Web.Report.Model
{
    public class LedgerReportModel
    {
        public String SalesOfficerName { get; set; }
        public String DealerName { get; set; }
        public Double Sales { get; set; }
        public Double Collection { get; set; }
        public Double OpeningBalance { get; set; }
        public Double ClosingBalance { get; set; }
        public String RegionName { get; set; }
        public String ProductName { get; set; }
        

        public LedgerReportModel(string SO, string Dealer, double sales, double coll, string reg,string ProductName)
        {
            this.SalesOfficerName = SO;
            this.DealerName = Dealer;
            this.Sales = sales;
            this.Collection = coll;
            this.RegionName = reg;
            this.OpeningBalance = 0;
            this.ClosingBalance = 0;
            this.ProductName = ProductName;
        }

        public LedgerReportModel()
        {
        }
    }
}