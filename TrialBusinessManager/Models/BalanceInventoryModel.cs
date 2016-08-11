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
    public class BalanceInventoryModel:ViewModelBase
    {

        /// <summary>
        /// The <see cref="UpdateProduct" /> property's name.
        /// </summary>
        public const string UpdateProductPropertyName = "Product";

        private Product _Product;

        /// <summary>
        /// Sets and gets the UpdateProduct property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Product Product
        {
            get
            {
                return _Product;
            }

            set
            {
                if (_Product == value)
                {
                    return;
                }

                _Product = value;
                RaisePropertyChanged(UpdateProductPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="LotId" /> property's name.
        /// </summary>
        public const string LotIdPropertyName = "LotId";

        private String lotId;

        /// <summary>
        /// Sets and gets the LotId property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public String LotId
        {
            get
            {
                return lotId;
            }

            set
            {
                if (lotId == value)
                {
                    return;
                }

                lotId = value;
                RaisePropertyChanged(LotIdPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="LotId" /> property's name.
        /// </summary>
        public const string LPropertyName = "DispatchedQuantity";

        private Double dispatchedQuantity;

        /// <summary>
        /// Sets and gets the LotId property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Double DispatchedQuantity
        {
            get
            {
                return dispatchedQuantity;
            }

            set
            {
                if (dispatchedQuantity == value)
                {
                    return;
                }

                dispatchedQuantity = value;
                RaisePropertyChanged(LPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="LotId" /> property's name.
        /// </summary>
        public const string PropertyName = "BalanceQuantity";

        private Double balanceQuantity;

        /// <summary>
        /// Sets and gets the LotId property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Double BalanceQuantity
        {
            get
            {
                return balanceQuantity;
            }

            set
            {
                if (balanceQuantity == value)
                {
                    return;
                }

                balanceQuantity = value;
                RaisePropertyChanged(PropertyName);
            }
        }

    }
}
