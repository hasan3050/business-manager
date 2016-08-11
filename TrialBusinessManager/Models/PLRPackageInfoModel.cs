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
    public class PLRPackageInfoModel:ViewModelBase
    {
        /// <summary>
        /// The <see cref="PLRPackageInfo" /> property's name.
        /// </summary>
        public const string PLRPackageInfoPropertyName = "PLRPackageInfo";

        private PLRPackageInfo plrPackageInfo ;

        /// <summary>
        /// Sets and gets the PLRPackageInfo property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public PLRPackageInfo PLRPackageInfo
        {
            get
            {
                return plrPackageInfo;
            }

            set
            {
                if (plrPackageInfo == value)
                {
                    return;
                }

                plrPackageInfo = value;
                RaisePropertyChanged(PLRPackageInfoPropertyName);
            }
        }


        ObservableCollection<String> packageStatus = new ObservableCollection<string>
        {
           "OnProcessing","Finished"  
        };
        public ObservableCollection<string> PackageStatus { get { return packageStatus; } set { } }

        /// <summary>
        /// The <see cref="SelectedType" /> property's name.
        /// </summary>
        public const string SelectedTypePropertyName = "SelectedType";

        private String selectedType;

        /// <summary>
        /// Sets and gets the SelectedType property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public String SelectedType
        {
            get
            {
                return selectedType;
            }

            set
            {
                if (selectedType == value)
                {
                    return;
                }

                selectedType = value;
                RaisePropertyChanged(SelectedTypePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="PurchasePrice" /> property's name.
        /// </summary>
        public const string PurchasePricePropertyName = "PurchasePrice";

        private Double purchasePrice;

        /// <summary>
        /// Sets and gets the PurchasePrice property.
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
                RaisePropertyChanged(PurchasePricePropertyName);
            }
        }

    }
}
