using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight;

namespace TrialBusinessManager.Models
{
    public class InventoryPackageModel:ViewModelBase
    {

        /// <summary>
        /// The <see cref="ProductName" /> property's name.
        /// </summary>
        public const string ProductNamePropertyName = "PackageName";

        private String packageName;

        /// <summary>
        /// Sets and gets the ProductName property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public String PackageName
        {
            get
            {
                return packageName;
            }

            set
            {
                if (packageName == value)
                {
                    return;
                }

                packageName = value;
                RaisePropertyChanged(ProductNamePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="ProductCode" /> property's name.
        /// </summary>
        public const string ProductCodePropertyName = "PackageCode";

        private String packageCode;

        /// <summary>
        /// Sets and gets the ProductCode property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public String PackageCode
        {
            get
            {
                return packageCode;
            }

            set
            {
                if (packageCode == value)
                {
                    return;
                }

                packageCode = value;
                RaisePropertyChanged(ProductCodePropertyName);
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

        private Double finishedQuabtity;

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

        private Double onProcessingQuantity;

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

        public const string TotalProductQuantityPropertyName = "TotalPackageQuantity";

        private Double totalPackageQuantity;

        /// <summary>
        /// Sets and gets the TotalProductQuantity property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Double TotalPackageQuantity
        {
            get
            {
                return totalPackageQuantity;
            }

            set
            {
                if (totalPackageQuantity == value)
                {
                    return;
                }

                totalPackageQuantity = value;
                RaisePropertyChanged(TotalProductQuantityPropertyName);
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
