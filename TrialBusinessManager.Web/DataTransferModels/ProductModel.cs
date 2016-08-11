using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace TrialBusinessManager.Web.DataTransferModels
{
    public class ProductModel
    {
        [DataMember]
        public string ProductGroupName { get; set; }
        [DataMember]
        public string ProductSubType { get; set; }
        [DataMember]
        public string ProductType { get; set; }
        [DataMember]
        public long ProductId { get; set; }

        public ProductModel()
        { }
    }
}