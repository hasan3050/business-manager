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
using System.Linq;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

namespace TrialBusinessManager.Models
{
    public class LedgerModule:ViewModelBase
    {
        private Ledger _ledger=new Ledger();
        public Ledger Ledger
        {
            get { return _ledger; }
            set { if (value != _ledger) _ledger = value; RaisePropertyChanged("Ledger"); }
        }

        private ObservableCollection<Product> _productCollection= new ObservableCollection<Product> ();
        public ObservableCollection<Product> ProductCollection
        {
            get { return _productCollection; }
            set { if (value != _productCollection) _productCollection = value; RaisePropertyChanged("ProductCollection"); }
        }

        private ObservableCollection<string> _method = new ObservableCollection<string> { "Bill"};
        public ObservableCollection<string> Method
        {
            get { return _method; }
        }

        private Product _selectedProduct= new Product();
        public  Product SelectedProduct
        {
            get { return _selectedProduct; }
            set { if (value != _selectedProduct) _selectedProduct = value; RaisePropertyChanged("SelectedProduct"); }
        }

        private string _selectedMethod;
        public  string SelectedMethod
        {
            get { return _selectedMethod; }
            set { if (value != _selectedMethod) _selectedMethod = value; RaisePropertyChanged("SelectedMethod"); }
        }

        private string _dealerName;
        public string DealerName
        {
            get { return _dealerName; }
            set { if (value != _dealerName) _dealerName = value; RaisePropertyChanged("DealerName"); }
        }
    }
}
