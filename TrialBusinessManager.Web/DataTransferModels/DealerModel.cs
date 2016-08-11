using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TrialBusinessManager.Web.DataTransferModels
{
    public class DealerModel
    {        
        [DataMember]
        public String Name { get; set; }
        [DataMember]
        public long DealerId { get; set; }
        [DataMember]
        public String CompanyName { get; set; }
        [DataMember]
        public long RegionId { get; set; }

        public DealerModel()
        { }
        
    }
}