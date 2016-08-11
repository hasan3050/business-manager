using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace TrialBusinessManager.Web.DataTransferModels
{
    public class RegionModel
    {
        [DataMember]
        public string RegionName { get; set; }
        [DataMember]
        public long RegionId { get; set; }

        public RegionModel()
        {}
    }
}