using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace TrialBusinessManager.Web.Report.Model
{
    public class AchievementDashboardModel
    {
        [DataMember]
        public long RegionId { get; set; }
        [DataMember]
        public string RegionName { get; set; }
        [DataMember]
        public long SalesOfficerId { get; set; }
        [DataMember]
        public string SalesOfficerName { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public string ProductGroupName{get;set;}
        [DataMember]
        public string ProductSubType { get; set; }
        [DataMember]
        public string ProductType { get; set; }
        [DataMember]
        public double RegionSalesTargetVollume { get; set; }
        [DataMember]
        public double SalesOfficerSalesTargetVollume { get; set; }
        [DataMember]
        public double TotalSalesVollume { get; set; }
        [DataMember]
        public double TotalReturnVollume { get; set; }
    
        [DataMember]
        public double RemainingSalesVollume { get; set; }
    }
}