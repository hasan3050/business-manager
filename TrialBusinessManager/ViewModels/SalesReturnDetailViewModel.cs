using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TrialBusinessManager.Web;
using TrialBusinessManager.Models;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.ServiceModel.DomainServices.Client;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;

namespace TrialBusinessManager.ViewModels
{
    public class SalesReturnClass:ViewModelBase
    {
        private SalesReturnInfo _SRInfo=new SalesReturnInfo();
        public SalesReturnInfo SRInfo
        {
            get { return _SRInfo; }
            set
            {
                if (_SRInfo != value)
                {
                    _SRInfo = value;
                    RaisePropertyChanged("SRInfo");
                }
            }
        }

        public double TotalAcceptedPrice { get; set; }
        public double TotalPlacedPrice { get; set; }
    }
    public class SalesReturnDetailViewModel : ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        private EntitySet<SalesReturnInfo> SalesReturnCollection { get { return _context.SalesReturnInfos; } }

        public SalesReturnDetailViewModel()
        {
            SelectedSalesReturn = StaticMessaging.SalesReturnMessage;
            LoadSalesReturn();

            Print = new RelayCommand(PrintPage);
        }

        #region Method
        void PrintPage()
        {
            PrintFactory.PrintSalesReturn(_context,SelectedSalesReturn);
        }
        private void LoadSalesReturn()
        {
            _context.Load(_context.GetSalesReturnInfoesQuery().Where(e => e.SalesReturnId == SelectedSalesReturn.SalesReturnId)).Completed += (s, e) =>
            {
                BindSalesReturn.Clear();

                foreach(SalesReturnInfo x in SalesReturnCollection)
                {
                    SalesReturnClass temp=new SalesReturnClass();
                    temp.SRInfo = x;
                    temp.TotalPlacedPrice = x.ProductPrice * x.PlacedQuantity;
                    temp.TotalAcceptedPrice = x.ProductPrice * x.AcceptedQuantity;
                    BindSalesReturn.Add(temp);
                }

                TotalPrice = BindSalesReturn.Sum(o => o.TotalAcceptedPrice);
            };
        }

        #endregion

        #region Properties

        public const string SelectedSalesReturnPropertyName = "SelectedSalesReturn";
        private SalesReturn _selectedSalesReturn = StaticMessaging.SalesReturnMessage;
        public SalesReturn SelectedSalesReturn
        {
            get { return _selectedSalesReturn; }
            set
            {
                if (_selectedSalesReturn == value) { return; }
                _selectedSalesReturn = value;
                RaisePropertyChanged(SelectedSalesReturnPropertyName);
            }
        }

        public const string TotalPricePropertyName = "TotalPrice";
        private double _totalPrice = 0;
        public double TotalPrice
        {
            get { return _totalPrice; }
            set
            {
                if (_totalPrice == value) { return; }

                _totalPrice = value;
                RaisePropertyChanged(TotalPricePropertyName);
            }
        }

        public const string BindSalesReturnPropertyName = "BindSalesReturn";
        private ObservableCollection<SalesReturnClass> _bindSalesReturn = new ObservableCollection<SalesReturnClass>();
        public ObservableCollection<SalesReturnClass> BindSalesReturn
        {
            get { return _bindSalesReturn; }
            set
            {
                if (_bindSalesReturn == value) { return; }

                _bindSalesReturn = value;
                RaisePropertyChanged(BindSalesReturnPropertyName);
            }
        }
        public ICommand Print { get; set; }
        #endregion

    }
}
