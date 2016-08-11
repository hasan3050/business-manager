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
    public class FinishedProductUpdateModel:ViewModelBase
    {
        /// <summary>
        /// The <see cref="UpdateProduct" /> property's name.
        /// </summary>
        public const string UpdateProductPropertyName = "UpdateProduct";

        private Product updateProduct ;

        /// <summary>
        /// Sets and gets the UpdateProduct property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Product UpdateProduct
        {
            get
            {
                return updateProduct;
            }

            set
            {
                if (updateProduct == value)
                {
                    return;
                }

                updateProduct = value;
                RaisePropertyChanged(UpdateProductPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="LotId" /> property's name.
        /// </summary>
        public const string LotIdPropertyName = "LotId";

        private String lotId ;

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

        /// <summary>
        /// The <see cref="UpdateQuantity" /> property's name.
        /// </summary>
        public const string UpdateQuantityPropertyName = "UpdateQuantity";

        private Double updateQuantity ;

        /// <summary>
        /// Sets and gets the UpdateQuantity property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Double UpdateQuantity
        {
            get
            {
                return updateQuantity;
            }

            set
            {
                if (updateQuantity == value)
                {
                    return;
                }

                updateQuantity = value;
                RaisePropertyChanged(UpdateQuantityPropertyName);
            }
        }



    }
}
