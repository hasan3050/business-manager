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
    public class LossInfo:ViewModelBase
    {
        public Double Commission { get; set; }

        public Double UnitPrice { get; set; }

        /// <summary>
        /// The <see cref="Product" /> property's name.
        /// </summary>
        public const string ProductPropertyName = "Product";

        private Product _product  ;

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
        /// The <see cref="PurchasedQuantity" /> property's name.
        /// </summary>
        public const string PurchasedQuantityPropertyName = "PurchasedQuantity";

        private Double _purchasedQuantity  ;

        /// <summary>
        /// Gets the PurchasedQuantity property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public Double PurchasedQuantity
        {
            get
            {
                return _purchasedQuantity;
            }

            set
            {
                if (_purchasedQuantity == value)
                {
                    return;
                }

                var oldValue = _purchasedQuantity;
                _purchasedQuantity = value;

                
                // Update bindings, no broadcast
                RaisePropertyChanged(PurchasedQuantityPropertyName);

               
            }
        }

        /// <summary>
        /// The <see cref="LostQuantity" /> property's name.
        /// </summary>
        public const string LostQuantityPropertyName = "LostQuantity";

        private Double _lostQuantity  ;

        /// <summary>
        /// Gets the LostQuantity property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public Double LostQuantity
        {
            get
            {
                return _lostQuantity;
            }

            set
            {
                if (_lostQuantity == value)
                {
                    return;
                }

                var oldValue = _lostQuantity;
                _lostQuantity = value;

               

                // Update bindings, no broadcast
                RaisePropertyChanged(LostQuantityPropertyName);

                
            }
        }

        /// <summary>
        /// The <see cref="LostAmount" /> property's name.
        /// </summary>
        public const string LostAmountPropertyName = "LostAmount";

        private Double _lostAmount  ;

        /// <summary>
        /// Gets the LostAmount property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public Double LostAmount
        {
            get
            {
                return _lostAmount;
            }

            set
            {
                if (_lostAmount == value)
                {
                    return;
                }

                var oldValue = _lostAmount;
                _lostAmount = value;

               

                // Update bindings, no broadcast
                RaisePropertyChanged(LostAmountPropertyName);

                
            }
        }

        /// <summary>
        /// The <see cref="DispatchError" /> property's name.
        /// </summary>
        public const string DispatchErrorPropertyName = "DispatchError";

        private bool _dispatchError = false;

        /// <summary>
        /// Gets the DispatchError property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public bool DispatchError
        {
            get
            {
                return _dispatchError;
            }

            set
            {
                if (_dispatchError == value)
                {
                    return;
                }

                var oldValue = _dispatchError;
                _dispatchError = value;

               

                // Update bindings, no broadcast
                RaisePropertyChanged(DispatchErrorPropertyName);

                
            }
        }
    }
}
