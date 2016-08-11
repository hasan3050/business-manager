using TrialBusinessManager.Web;
using GalaSoft.MvvmLight;

namespace TrialBusinessManager.Models
{
    public class IndentInfoDetail:ViewModelBase
    {
        public IndentInfoDetail() { }

        public IndentInfoDetail(Product prdct, double comm, double qty,double prc)
        {
            Product = prdct;
            Commission = comm;
            Quantity = qty;
            Price = prc;
    //        _placedQuantity = qty;
        }

        private IndentProductInfo _productInfo;
        public IndentProductInfo ProductInfo
        {
            get { return _productInfo; }
            set { if (value != _productInfo) _productInfo = value; RaisePropertyChanged("ProductInfo"); }
        }
        
        private Product _product;
        public Product Product 
        {
            get { return _product; }
            set { if (value != _product) _product = value; RaisePropertyChanged("Product"); }
        }

        private double _commission;
        public double Commission
        {
            get { return _commission; }
            set { if (value != _commission) _commission = value; RaisePropertyChanged("Commission"); }
        }

        private double _price;
        public double Price
        {
            get { return _price; }
            set { if (value != _price) _price = value; RaisePropertyChanged("Price"); }
        }

        private double _quantity;
        public double Quantity
        {
            get { return _quantity; }
            set { if (value != _quantity) _quantity = value; RaisePropertyChanged("Quantity"); }
        }

        private double _totalQuantity;
        public double TotalQuantity
        {
            set { _totalQuantity = Quantity * Product.StockKeepingUnit; RaisePropertyChanged("TotalQuantity"); }
            get { return _totalQuantity; }
        }

        private double _netPrice;
        public double NetPrice
        {
            set { _netPrice = TotalPrice * (1 - Commission / 100.0); RaisePropertyChanged("NetPrice"); }
            get { return _netPrice; }
        }

        private double _totalPrice;
        public double TotalPrice
        {
            set { _totalPrice = Quantity * Price; RaisePropertyChanged("TotalPrice"); }
            get {return _totalPrice;}
        }

        private double _placedQuantity;
        public double PlacedQuantity
        {
            set { if(value!=_placedQuantity) _placedQuantity=value;}
            get { return _placedQuantity; }
        }
    }
}
