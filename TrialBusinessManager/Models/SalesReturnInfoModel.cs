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
    public class SalesReturnInfoModel:ViewModelBase
    {
        /// <summary>
        /// The <see cref="SalesReturnInfo" /> property's name.
        /// </summary>
        public const string SalesReturnInfoPropertyName = "SalesReturnInfo";

        private SalesReturnInfo salesReturnInfo ;

        /// <summary>
        /// Sets and gets the SalesReturnInfo property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public SalesReturnInfo SalesReturnInfo
        {
            get
            {
                return salesReturnInfo;
            }

            set
            {
                if (salesReturnInfo == value)
                {
                    return;
                }

                salesReturnInfo = value;
                RaisePropertyChanged(SalesReturnInfoPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="BillAmount" /> property's name.
        /// </summary>
        public const string BillAmountPropertyName = "BillAmount";

        private Double billAmount ;

        /// <summary>
        /// Sets and gets the BillAmount property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Double BillAmount
        {
            get
            {
                return billAmount;
            }

            set
            {
                if (billAmount == value)
                {
                    return;
                }

                billAmount = value;
                RaisePropertyChanged(BillAmountPropertyName);
            }
        }

    }
}
