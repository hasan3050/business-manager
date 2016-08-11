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
    public class ProductTransferDetailViewModel : ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        private EntitySet<ProductTransferDetail> ProductTransferProductCollection { get { return _context.ProductTransferDetails ; } }
        
/*        public class ProductTransferRowModel
        {
            public ProductTransferDetail Info { get; set; }
            public double TotalTransferQuantity { get; set; }
            public double TotalAcceptedQuantity { get; set; }
        }*/

        public ProductTransferDetailViewModel()
        {
            SelectedProductTransfer = StaticMessaging.ProductTransferMessage;
            Message = string.Empty;

            LoadProductTransferProduct();

            Print = new RelayCommand(PrintPage);
        }

        #region Method
        void PrintPage()
        {
       //     PrintFactory.PrintProductTransfer(_context, SelectedProductTransfer);
        }

        private void LoadProductTransferProduct()
        {
            _context.Load(_context.GetProductTransferDetailsQuery().Where(e => e.ProductTransferId == SelectedProductTransfer.Transfer.ProductTransferID)).Completed += (s, e) =>
            {
                BindProductTransferInfo = new ObservableCollection<ProductTransferDetail>(ProductTransferProductCollection);
           /*     BindProductTransferInfo.Clear();
                foreach (ProductTransferDetail Info in ProductTransferProductCollection)
                {
                    ProductTransferRowModel temp = new ProductTransferRowModel();
                    temp.Info = Info;
                    temp.TotalTransferQuantity = Info.Product.StockKeepingUnit * Info.TransferQuantity;
                    temp.TotalAcceptedQuantity = Info.Product.StockKeepingUnit*Info.RecievedQuantity;

                    BindProductTransferInfo.Add(temp);
                }*/
            };
        }

        #endregion

        #region Properties
        private string _Message;
        public string Message
        {
            get { return _Message; }
            set { if (value != _Message)_Message = value; RaisePropertyChanged("Message"); }
        }

        public const string SelectedProductTransferPropertyName = "SelectedProductTransfer";
        private StaticMessaging.ProductTransferForBind _selectedProductTransfer = StaticMessaging.ProductTransferMessage;
        public StaticMessaging.ProductTransferForBind SelectedProductTransfer
        {
            get { return _selectedProductTransfer; }
            set
            {
                if (_selectedProductTransfer == value) { return; }

                _selectedProductTransfer = value;
                RaisePropertyChanged(SelectedProductTransferPropertyName);
            }
        }

        public const string BindProductTransferInfoPropertyName = "BindProductTransferInfo";
 //       private ObservableCollection<ProductTransferRowModel> _bindProductTransferInfo = new ObservableCollection<ProductTransferRowModel>();
 //       public ObservableCollection<ProductTransferRowModel> BindProductTransferInfo
        private ObservableCollection<ProductTransferDetail> _bindProductTransferInfo = new ObservableCollection<ProductTransferDetail>();
        public ObservableCollection<ProductTransferDetail> BindProductTransferInfo
        {
            get { return _bindProductTransferInfo; }
            set
            {
                if (_bindProductTransferInfo == value) { return; }

                _bindProductTransferInfo = value;
                RaisePropertyChanged(BindProductTransferInfoPropertyName);
            }
        }

        public const string TotalAmountPropertyName = "TotalAmount";
        private double _totalAmount = 0;
        public double TotalAmount
        {
            get { return _totalAmount; }
            set
            {
                if (_totalAmount == value) { return; }

                _totalAmount = value;
                RaisePropertyChanged(TotalAmountPropertyName);
            }
        }
        public ICommand Print { get; set; }
        #endregion

    }
}
