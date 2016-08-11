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
    public class PLRProductInfoModel:ViewModelBase
    {
        /// <summary>
        /// The <see cref="PLRProductInfo" /> property's name.
        /// </summary>
        public const string PLRProductInfoPropertyName = "PLRProductInfo";

        private PLRProductInfo plrProductInfo ;

        /// <summary>
        /// Sets and gets the PLRProductInfo property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public PLRProductInfo PLRProductInfo
        {
            get
            {
                return plrProductInfo;
            }

            set
            {
                if (plrProductInfo == value)
                {
                    return;
                }

                plrProductInfo = value;
                RaisePropertyChanged(PLRProductInfoPropertyName);
            }
        }



        ObservableCollection<String> productStatus = new ObservableCollection<string>
        {
           "OnProcessing","Finished"  
        };
       public ObservableCollection<string> ProductStatus { get { return productStatus; } set{} }

       /// <summary>
       /// The <see cref="SelectedType" /> property's name.
       /// </summary>
       public const string SelectedTypePropertyName = "SelectedType";

       private String selectedType ;

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

       private Double purchasePrice  ;

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
