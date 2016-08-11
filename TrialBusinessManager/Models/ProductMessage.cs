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
using TrialBusinessManager.Web;
using GalaSoft.MvvmLight;

namespace TrialBusinessManager.Models
{
    public class ProductMessage:ViewModelBase
    {
        /// <summary>
        /// The <see cref="Product" /> property's name.
        /// </summary>
        public const string ProductPropertyName = "Product";

        private Product _product;

        /// <summary>
        /// Sets and gets the Product property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Product Product
        {
            get
            {
                return _product;
            }

            set
            {
                if (_product== value)
                {
                    return;
                }

                _product = value;
                RaisePropertyChanged(ProductPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Quanity" /> property's name.
        /// </summary>
        public const string QuanityPropertyName = "Quanity";

        private Double _quantity  ;

        /// <summary>
        /// Sets and gets the Quanity property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Double Quanity
        {
            get
            {
                return _quantity;
            }

            set
            {
                if (_quantity == value)
                {
                    return;
                }

                _quantity = value;
                RaisePropertyChanged(QuanityPropertyName);
            }
        }
    }
}
