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
        public Double Commission { get; set; }
        public Double ActualSale { get; set; }
        public String RegionName { get; set; }
        public String ProductName { get; set; }
        public Double SalesReturn { get; set; }
        public String ProductType { get; set; }

        public LedgerReportModel()
        {
        }
    }
}