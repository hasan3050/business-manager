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
    public class BillRowModel:ViewModelBase
    {
        private Product _product;
        public Product Product
        {
            get{ return _product;}
            set{ if (_product != value) _product = value;   RaisePropertyChanged("Product");}
        }

        private double _loss;
        public double Loss
        {
            get { return _loss; }
            set { if (_loss != value) _loss = value; RaisePropertyChanged("Loss"); }
        }

        private BillProductInfo _info;
        public BillProductInfo Info
        {
            get { return _info; }
            set { if (_info != value) _info = value; RaisePropertyChanged("Info"); }
        }

        private double _totalQuantity;
        public double TotalQuantity
        {
            get { return _totalQuantity; }
            set { _totalQuantity = Info.LotQuantity; RaisePropertyChanged("Total Quantity"); }
        }

        private double _totalPrice;
        public double TotalPrice
        {
            get { return _totalPrice; }
            set { _totalPrice = Info.Product.PricePerUnit * TotalQuantity; RaisePropertyChanged("Total Price"); }
        }

        private double _netPrice;
        public double NetPrice
        {
            get { return _netPrice; }
            set { _netPrice = TotalPrice * (1 - (Info.ComissionPercentage / 100.0))- Loss; RaisePropertyChanged("Net Price"); }
        }
    }
}
