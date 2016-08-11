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
using System.ServiceModel.DomainServices.Client;

namespace TrialBusinessManager.Models
{
    public class IndentInfo:ViewModelBase
    {
        /// <summary>
        /// The <see cref="ProductName" /> property's name.
        /// </summary>
        public const string ProductNamePropertyName = "ProductName";

        private String _productName ;

        /// <summary>
        /// Sets and gets the ProductName property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public String ProductName
        {
            get
            {
                return _productName;
            }

            set
            {
                if (_productName == value)
                {
                    return;
                }

                _productName = value;
                RaisePropertyChanged(ProductNamePropertyName);
            }
        }
        private String _productCode;

        /// <summary>
        /// Sets and gets the ProductName property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public String ProductCode
        {
            get
            {
                return _productCode;
            }

            set
            {
                if (_productCode == value)
                {
                    return;
                }

                _productCode = value;
                RaisePropertyChanged("ProductCode");
            }
        }

        /// <summary>
        /// The <see cref="BrandName" /> property's name.
        /// </summary>
        public const string ProductCodePropertyName = "ProductCode";

        private String _brandName;

        /// <summary>
        /// Sets and gets the ProductCode property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public String BrandName
        {
            get
            {
                return _brandName;
            }

            set
            {
                if (_brandName == value)
                {
                    return;
                }

                _brandName = value;
                RaisePropertyChanged(ProductCodePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="PacketSize" /> property's name.
        /// </summary>
        public const string PacketSizePropertyName = "PacketSize";

        private Double _packetSize  ;

        /// <summary>
        /// Sets and gets the PacketSize property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Double PacketSize
        {
            get
            {
                return _packetSize;
            }

            set
            {
                if (_packetSize == value)
                {
                    return;
                }

                _packetSize = value;
                RaisePropertyChanged(PacketSizePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Quantity" /> property's name.
        /// </summary>
        public const string QuantityPropertyName = "Quantity";

        private Double _quantity;

        /// <summary>
        /// Sets and gets the Quantity property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Double Quantity
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
                RaisePropertyChanged(QuantityPropertyName);
            }
        }

        
        /// <summary>
        /// The <see cref="Commission" /> property's name.
        /// </summary>
        public const string CommissionPropertyName = "Commission";

        private Double _comission  ;

        /// <summary>
        /// Sets and gets the Commission property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Double Commission
        {
            get
            {
                return _comission;
            }

            set
            {
                if (_comission == value)
                {
                    return;
                }

                _comission = value;
                RaisePropertyChanged(CommissionPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="NetPrice" /> property's name.
        /// </summary>
        public const string NetPricePropertyName = "NetPrice";

        private Double _netPrice;

        /// <summary>
        /// Sets and gets the NetPrice property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Double NetPrice
        {
            get
            {
                return _netPrice;
            }

            set
            {
                if (_netPrice == value)
                {
                    return;
                }

                _netPrice = value;
                RaisePropertyChanged(NetPricePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="UnitPrice" /> property's name.
        /// </summary>
        public const string UnitPricePropertyName = "UnitPrice";

        private Double _unitPrice  ;

        /// <summary>
        /// Sets and gets the UnitPrice property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Double UnitPrice
        {
            get
            {
                return _unitPrice;
            }

            set
            {
                if (_unitPrice == value)
                {
                    return;
                }

                _unitPrice = value;
                RaisePropertyChanged(UnitPricePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="TotalPrice" /> property's name.
        /// </summary>
        public const string TotalPricePropertyName = "TotalPrice";

        private Double _totalprice;

        /// <summary>
        /// Sets and gets the TotalPrice property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Double TotalPrice
        {
            get
            {
                return _totalprice;
            }

            set
            {
                if (_totalprice == value)
                {
                    return;
                }

                _totalprice = value;
                RaisePropertyChanged(TotalPricePropertyName);
            }
        }

        

        /// <summary>
        /// The <see cref="ProductID" /> property's name.
        /// </summary>
        public const string ProductIDPropertyName = "ProductID";

        private Int64 _productID  ;

        /// <summary>
        /// Sets and gets the ProductID property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Int64 ProductID
        {
            get
            {
                return _productID;
            }

            set
            {
                if (_productID == value)
                {
                    return;
                }

                _productID = value;
                RaisePropertyChanged(ProductIDPropertyName);
            }
        }

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
                if (_product == value)
                {
                    return;
                }

                _product = value;
                RaisePropertyChanged(ProductPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="TotalQuantity" /> property's name.
        /// </summary>
        public const string MyPropertyTotalQuantityPropertyName = "TotalQuantity";

        private Double _totalQuantity ;

        /// <summary>
        /// Sets and gets the MyPropertyTotalQuantity property.
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
                RaisePropertyChanged(MyPropertyTotalQuantityPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="LotID" /> property's name.
        /// </summary>
        public const string LotIDPropertyName = "LotID";

        private long _lotID;

        /// <summary>
        /// Sets and gets the LotID property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public long LotID
        {
            get
            {
                return _lotID;
            }

            set
            {
                if (_lotID == value)
                {
                    return;
                }

                _lotID = value;
                RaisePropertyChanged(LotIDPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="TotalReturnAmount" /> property's name.
        /// </summary>
        public const string TotalReturnAmountPropertyName = "TotalReturnAmount";

        private Double _totalReturnAmount  ;

        /// <summary>
        /// Sets and gets the TotalReturnAmount property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Double TotalReturnAmount
        {
            get
            {
                return _totalReturnAmount;
            }

            set
            {
                if (_totalReturnAmount == value)
                {
                    return;
                }

                _totalReturnAmount = value;
                RaisePropertyChanged(TotalReturnAmountPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="AcceptedQuantity" /> property's name.
        /// </summary>
        public const string AcceptedQuantityPropertyName = "AcceptedQuantity";

        private long _acceptedQuantity;

        /// <summary>
        /// Sets and gets the AcceptedQuantity property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public long AcceptedQuantity
        {
            get
            {
                return _acceptedQuantity;
            }

            set
            {
                if (_acceptedQuantity == value)
                {
                    return;
                }

                _acceptedQuantity = value;
                RaisePropertyChanged(AcceptedQuantityPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="PlacedQuantity" /> property's name.
        /// </summary>
        public const string PlacedQuantityPropertyName = "PlacedQuantity";

        private long _placedQuantity;

        /// <summary>
        /// Sets and gets the PlacedQuantity property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public long PlacedQuantity
        {
            get
            {
                return _placedQuantity;
            }

            set
            {
                if (_placedQuantity == value)
                {
                    return;
                }

                _placedQuantity = value;
                RaisePropertyChanged(PlacedQuantityPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="TotalAcceptedAmount" /> property's name.
        /// </summary>
        public const string TotalAcceptedAmountPropertyName = "TotalAcceptedAmount";

        private Double _totalAcceptedAmount  ;

        /// <summary>
        /// Sets and gets the TotalAcceptedAmount property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Double TotalAcceptedAmount
        {
            get
            {
                return _totalAcceptedAmount;
            }

            set
            {
                if (_totalAcceptedAmount == value)
                {
                    return;
                }

                _totalAcceptedAmount = value;
                RaisePropertyChanged(TotalAcceptedAmountPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="TotalPlacedAmount" /> property's name.
        /// </summary>
        public const string TotalPlacedAmountPropertyName = "TotalPlacedAmount";

        private Double _totalPlacedAmount  ;

        /// <summary>
        /// Sets and gets the TotalPlacedAmount property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Double TotalPlacedAmount
        {
            get
            {
                return _totalPlacedAmount;
            }

            set
            {
                if (_totalPlacedAmount == value)
                {
                    return;
                }

                _totalPlacedAmount = value;
                RaisePropertyChanged(TotalPlacedAmountPropertyName);
            }
        }
    }
}
