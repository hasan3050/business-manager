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
    public class PrintBillModel : ViewModelBase
    {
        /// <summary>
        /// The <see cref="Product" /> property's name.
        /// </summary>
        public const string ProductPropertyName = "Product";

        private Product _product;

        /// <summary>
        /// Gets the Product property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public Product Product
        {
            get
            {
                return _product;
            }

            set
            {
                if (_product == value)
                {
                    return;
                }

                var oldValue = _product;
                _product = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(ProductPropertyName);

            }
        }

        /// <summary>
        /// The <see cref="Info" /> property's name.
        /// </summary>
        public const string InfoPropertyName = "Info";

        private BillProductInfo _info;

        /// <summary>
        /// Gets the Info property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public BillProductInfo Info
        {
            get
            {
                return _info;
            }

            set
            {
                if (_info == value)
                {
                    return;
                }

                var oldValue = _info;
                _info = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(InfoPropertyName);

            }
        }

        /// <summary>
        /// The <see cref="TotalPrice" /> property's name.
        /// </summary>
        public const string TotalPricePropertyName = "TotalPrice";

        private Double _totalPrice  ;

        /// <summary>
        /// Gets the TotalPrice property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public Double TotalPrice
        {
            get
            {
                return Info.LotQuantity*Info.ProductPrice;
            }

            set
            {
                if (_totalPrice == value)
                {
                    return;
                }

                var oldValue = _totalPrice;
                _totalPrice = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(TotalPricePropertyName);

            }
        }

        /// <summary>
        /// The <see cref="NetPrice" /> property's name.
        /// </summary>
        public const string NetPricePropertyName = "NetPrice";

        private Double _netPrice  ;

        /// <summary>
        /// Gets the NetPrice property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public Double NetPrice
        {
            get
            {
                return TotalPrice*((100-Info.ComissionPercentage)/100);
            }

            set
            {
                if (_netPrice == value)
                {
                    return;
                }

                var oldValue = _netPrice;
                _netPrice = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(NetPricePropertyName);

            }
        }

        private Double _PriceKg;

        /// <summary>
        /// Gets the NetPrice property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public Double PriceKg
        {
            get
            {
                return Info.Product.PricePerUnit/Info.Product.StockKeepingUnit;
            }

            set
            {
                if (_PriceKg == value)
                {
                    return;
                }

                var oldValue = _PriceKg;
                _PriceKg = value;

                // Update bindings, no broadcast
                RaisePropertyChanged("PriceKg");

            }
        }


        private Double _totalKg;

        /// <summary>
        /// Gets the NetPrice property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public Double TotalKg
        {
            get
            {
                return Info.LotQuantity* Info.Product.StockKeepingUnit;
            }

            set
            {
                if (_totalKg == value)
                {
                    return;
                }

                var oldValue = _totalKg;
                _totalKg = value;

                // Update bindings, no broadcast
                RaisePropertyChanged("TotalKg");

            }
        }
    
    }
}
