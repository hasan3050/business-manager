using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrialBusinessManager.Web.Report.Model
{
    public class InventoryLogModel
    {
        public String RegionName  { get; set; }
        public String ProductName { get; set; }
        public DateTime Date { get; set; }
        public String MemoType { get; set; }
        public Double OpeningQuantity {get;set;}
        public Double ClosingQuantity{get;set;}
        public Double ProductCost { get; set; }
        public String LotNumber { get; set; }
        public Double ProductQuantity { get; set; }
    }
}