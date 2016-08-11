using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace TrialBusinessManager.Web.DataTransferModels
{
    public class EmployeeModel
    {
        [DataMember]
        public string EmployeeName { get; set; }
        [DataMember]
        public long EmployeeId { get; set; }
        [DataMember]
        public string Designation { get; set; }
        [DataMember]
        public string RegionName { get; set; }
        [DataMember]
        public long RegionId { get; set; }

        public EmployeeModel()
        { }
    }
}