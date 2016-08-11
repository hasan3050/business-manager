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
using TrialBusinessManager.Web;
namespace TrialBusinessManager.Models
{
    public class LotInfo : ViewModelBase
    {
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
        /// The <see cref="Quantity" /> property's name.
        /// </summary>
        public const string QuantityPropertyName = "Quantity";

        private Double quantity;

        /// <summary>
        /// Sets and gets the Quantity property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Double Quantity
        {
            get
            {
                return quantity;
            }

            set
            {
                if (quantity == value)
                {
                    return;
                }

                quantity = value;
                RaisePropertyChanged(QuantityPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="AvailableQuantity" /> property's name.
        /// </summary>
        public const string AvailableQuantityPropertyName = "AvailableQuantity";

        private Double availableQuantity;

        /// <summary>
        /// Sets and gets the AvailableQuantity property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Double AvailableQuantity
        {
            get
            {
                return availableQuantity;
            }

            set
            {
                if (availableQuantity == value)
                {
                    return;
                }

                availableQuantity = value;
                RaisePropertyChanged(AvailableQuantityPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="LotProduct" /> property's name.
        /// </summary>
        public const string LotProductPropertyName = "LotProduct";

        private Product lotProduct;

        /// <summary>
        /// Sets and gets the LotProduct property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Product LotProduct
        {
            get
            {
                return lotProduct;
            }

            set
            {
                if (lotProduct == value)
                {
                    return;
                }

                lotProduct = value;
                RaisePropertyChanged(LotProductPropertyName);
            }
        }

    }
}
