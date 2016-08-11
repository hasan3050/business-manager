using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrialBusinessManager.Web.Report.Model
{
    public class ProductPerformanceModel
    {
        public String RegionName { get; set; }
        public String ProductType { get; set; }
        public Double Target { get; set; }
        public Double Distribution { get; set; }
        public Double Return { get; set; }
        public Double Loss { get; set; }
        public Double ActualSale { get; set; }
        public Double AchievedPercentage { get; set; }
        public String SOName { get; set; }
    }
}