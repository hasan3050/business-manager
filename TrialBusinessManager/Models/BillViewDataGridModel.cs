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
    public class BillViewDataGridModel:ViewModelBase
    {
        /// <summary>
        /// The <see cref="Info" /> property's name.
        /// </summary>
        public const string InfoPropertyName = "Info";

        private InventoryProductInfo _info  ;

        /// <summary>
        /// Sets and gets the Info property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public InventoryProductInfo Info
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

                _info = value;
                RaisePropertyChanged(InfoPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="Product" /> property's name.
        /// </summary>
        public const string ProductPropertyName = "Product";

        private Product _product  ;

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
                if (_product == value)
                {
                    return;
                }

                _product = value;
                RaisePropertyChanged(ProductPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Packets" /> property's name.
        /// </summary>
        public const string PacketsPropertyName = "Packets";

        private Double _packets  ;

        /// <summary>
        /// Sets and gets the Packets property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Double Packets
        {
            get
            {
                return _packets;
            }

            set
            {
                if (_packets == value)
                {
                    return;
                }

                _packets = value;
                RaisePropertyChanged(PacketsPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="TotalQuantity" /> property's name.
        /// </summary>
        public const string TotalQuantityPropertyName = "TotalQuantity";

        private Double _totalQuantity  ;

        /// <summary>
        /// Sets and gets the TotalQuantity property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Double TotalQuantity
        {
            get
            {
                return _totalQuantity;
            }

            set
            {
                if (_totalQuantity == value)
                {
                    return;
                }

                _totalQuantity = value;
                RaisePropertyChanged(TotalQuantityPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="TotalPrice" /> property's name.
        /// </summary>
        public const string TotalPricePropertyName = "TotalPrice";

        private Double _totalPrice  ;

        /// <summary>
        /// Sets and gets the TotalPrice property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Double TotalPrice
        {
            get
            {
                return _totalPrice;
            }

            set
            {
                if (_totalPrice == value)
                {
                    return;
                }

                _totalPrice = value;
                RaisePropertyChanged(TotalPricePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="LotNumber" /> property's name.
        /// </summary>
        public const string LotNumberPropertyName = "LotNumber";

        private String _lotNumber  ;

        /// <summary>
        /// Sets and gets the LotNumber property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public String LotNumber
        {
            get
            {
                return _lotNumber;
            }

            set
            {
                if (_lotNumber == value)
                {
                    return;
                }

                _lotNumber = value;
                RaisePropertyChanged(LotNumberPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="ActualUnitprice" /> property's name.
        /// </summary>
        public const string ActualUnitpricePropertyName = "ActualUnitprice";

        private Double _actualUnitprice  ;

        /// <summary>
        /// Sets and gets the ActualUnitprice property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Double ActualUnitprice
        {
            get
            {
                return _actualUnitprice;
            }

            set
            {
                if (_actualUnitprice == value)
                {
                    return;
                }

                _actualUnitprice = value;
                RaisePropertyChanged(ActualUnitpricePropertyName);
            }
        }
    }
}
