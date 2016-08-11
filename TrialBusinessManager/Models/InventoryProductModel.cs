using System;
using System.Net;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight;
using TrialBusinessManager.Web;
using GalaSoft.MvvmLight.Command;
using System.ServiceModel.DomainServices.Client;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace TrialBusinessManager.Models
{
    public class InventoryProductModel:ViewModelBase
    {

        /// <summary>
        /// The <see cref="ProductName" /> property's name.
        /// </summary>
        public const string ProductNamePropertyName = "GroupName";

        private String productName  ;

        /// <summary>
        /// Sets and gets the ProductName property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public String GroupName
        {
            get
            {
                return productName;
            }

            set
            {
                if (productName == value)
                {
                    return;
                }

                productName = value;
                RaisePropertyChanged(ProductNamePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="ProductCode" /> property's name.
        /// </summary>
        public const string ProductCodePropertyName = "ProductCode";

        private String productCode  ;

        /// <summary>
        /// Sets and gets the ProductCode property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public String ProductCode
        {
            get
            {
                return productCode;
            }

            set
            {
                if (productCode == value)
                {
                    return;
                }

                productCode = value;
                RaisePropertyChanged(ProductCodePropertyName);
            }
        }

        private String brandName;

        /// <summary>
        /// Sets and gets the ProductCode property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public String BrandName
        {
            get
            {
                return brandName;
            }

            set
            {
                if (brandName == value)
                {
                    return;
                }

                brandName = value;
                RaisePropertyChanged("BrandName");
            }
        }



        /// <summary>
        /// The <see cref="UnfinishedQuantity" /> property's name.
        /// </summary>
        public const string UnfinishedQuantityPropertyName = "UnfinishedQuantity";

        private Double unfinishedQuantity;

        /// <summary>
        /// Sets and gets the UnfinishedQuantity property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Double UnfinishedQuantity
        {
            get
            {
                return unfinishedQuantity;
            }

            set
            {
                if (unfinishedQuantity == value)
                {
                    return;
                }

                unfinishedQuantity = value;
                RaisePropertyChanged(UnfinishedQuantityPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="FinishedQuantity" /> property's name.
        /// </summary>
        public const string FinishedQuantityPropertyName = "FinishedQuantity";

        private Double finishedQuabtity ;

        /// <summary>
        /// Sets and gets the FinishedQuantity property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Double FinishedQuantity
        {
            get
            {
                return finishedQuabtity;
            }

            set
            {
                if (finishedQuabtity == value)
                {
                    return;
                }

                finishedQuabtity = value;
                RaisePropertyChanged(FinishedQuantityPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="OnProcessingQuantity" /> property's name.
        /// </summary>
        public const string OnProcessingQuantityPropertyName = "OnProcessingQuantity";

        private Double onProcessingQuantity ;

        /// <summary>
        /// Sets and gets the OnProcessingQuantity property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Double OnProcessingQuantity
        {
            get
            {
                return onProcessingQuantity;
            }

            set
            {
                if (onProcessingQuantity == value)
                {
                    return;
                }

                onProcessingQuantity = value;
                RaisePropertyChanged(OnProcessingQuantityPropertyName);
            }
        }

        public const string TotalProductQuantityPropertyName = "TotalProductQuantity";

        private Double totalProductQuantity;

        /// <summary>
        /// Sets and gets the TotalProductQuantity property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Double TotalProductQuantity
        {
            get
            {
                return totalProductQuantity;
            }

            set
            {
                if (totalProductQuantity == value)
                {
                    return;
                }

                totalProductQuantity = value;
                RaisePropertyChanged(TotalProductQuantityPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="LotId" /> property's name.
        /// </summary>
        public const string LotIdPropertyName = "LotId";

        private String lotID ;

        /// <summary>
        /// Sets and gets the LotId property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public String LotId
        {
            get
            {
                return lotID;
            }

            set
            {
                if (lotID == value)
                {
                    return;
                }

                lotID = value;
                RaisePropertyChanged(LotIdPropertyName);
            }
        }

        private Double purchasePrice;

        /// <summary>
        /// Sets and gets the LotId property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Double PurchasePrice
        {
            get
            {
                return purchasePrice;
            }

            set
            {
                if (purchasePrice == value)
                {
                    return;
                }

                purchasePrice = value;
                RaisePropertyChanged("PurchasePrice");
            }
        }






    }
}
