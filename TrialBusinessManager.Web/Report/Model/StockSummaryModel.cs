using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrialBusinessManager.Web.Report.Model
{
    public class StockSummaryModel
    {
        public StockSummaryModel()
        {
        }
        
        public Double OpeningQuantity { get; set; }
        public Double ClosingQuantity { get; set; }
        public Double Inwards { get; set; }
        public Double Outwards { get; set; }
        public String RegionName { get; set; }
        public String ProductName { get; set; }
    }
}