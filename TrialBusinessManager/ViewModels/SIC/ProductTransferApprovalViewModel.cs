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
using TrialBusinessManager.Views;
using TrialBusinessManager.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel.DataAnnotations;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;

namespace TrialBusinessManager.ViewModels.SIC
{
    public class ProductTransferApprovalViewModel:ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        BusyWindow MyWindow = new BusyWindow();

        public ProductTransferApprovalViewModel()
        {
            RegisterCommands();
            RegisterForMessages();
            
        }

        void RegisterCommands()
        {
            QuantityEditedCommand = new RelayCommand(QuantityEdited);
            AcceptCommand = new RelayCommand(Accept);
        }

        void RegisterForMessages()
        {
            Messenger.Default.Register<AgroDomainContext>(this, OnContextReceived);
            Messenger.Default.Register<ProductTransfer>(this, OnTransferReceived);
        }

        void OnContextReceived(AgroDomainContext Context)
        {
            _context = Context;

        }

        void OnTransferReceived(ProductTransfer Transfer)
        {
            
            ProductTransfer = new ProductTransfer();
            ProductTransfer = Transfer;
            Details = new ObservableCollection<ProductTransferDetail>(_context.ProductTransferDetails.Where(c => c.ProductTransferId.Equals(Transfer.ProductTransferID)).ToList());
            MessageBox.Show("D : "+Details.Count.ToString());
        }

        void QuantityEdited()
        {
            if (SelectedDetail.RecievedQuantity < 0)
            {
                SelectedDetail.RecievedQuantity = 0;
                return;
            }
            if (SelectedDetail.RecievedQuantity > SelectedDetail.TransferQuantity)
            {
                SelectedDetail.RecievedQuantity = 0;
                MessageBox.Show("Received Quantity Cannot be grater than accepted quantity!!");
                return;
            }
        }

        void UpdateInventory()
        {
            for (int i = 0; i < Details.Count; i++)
            {
                ProductTransferDetail x = new ProductTransferDetail();
                x = Details.ElementAt(i);

                if (x.RecievedQuantity < x.TransferQuantity)
                {
                    ProductTransfer.HasProductLoss = true;
                    ProductTransfer.ProductLossCost += (x.TransferQuantity - x.RecievedQuantity) * x.Product.StockKeepingUnit * x.Product.PricePerUnit;
                }

                if (_context.InventoryProductInfos.Any(c => c.ProductId.Equals(x.ProductId) && c.LotId.Equals(x.LotId)))
                {
                    InventoryProductInfo MyInventoryInfo = new InventoryProductInfo();
                    MyInventoryInfo = _context.InventoryProductInfos.Where(c => c.ProductId.Equals(x.ProductId) && c.LotId.Equals(x.LotId)).Single();
                    MyInventoryInfo.FinishedQuantity += x.RecievedQuantity * x.Product.StockKeepingUnit;
                }
                else
                {
                    InventoryProductInfo MyInventoryInfo = new InventoryProductInfo();
                    MyInventoryInfo.FinishedQuantity += x.RecievedQuantity * x.Product.StockKeepingUnit;
                    MyInventoryInfo.InventoryId = UserInfo.Inventory.InventoryId;
                    MyInventoryInfo.LotId = x.LotId;
                    MyInventoryInfo.OnProcessingQuantity = 0;
                    MyInventoryInfo.ProductId = x.ProductId;
                    MyInventoryInfo.UnfinishedQuantity = 0;
                    MyInventoryInfo.UnitLotCost = x.PurchasePricePerUnit;
                    _context.InventoryProductInfos.Add(MyInventoryInfo);
                }
            }
        }

        void UpdateProductTransfer()
        {
            ProductTransfer.DateOfRecieve = DateTime.Now;
            ProductTransfer.RecievedBySICId = UserInfo.EmployeeID;
        }

        void Accept()
        {
            MyWindow.Show();
            ProductTransfer.ProductTransferStatus = "Approved";
            UpdateInventory();
            UpdateProductTransfer();
            _context.SubmitChanges(OnSubmitCompleted,null);
        }

        void OnSubmitCompleted(SubmitOperation so)
        {
            MyWindow.Close();
            
            if (so.HasError)
            {
               
                _context.RejectChanges();
                Messenger.Default.Send<String>("Close");
            }
            else
            {
                MessageBox.Show("Submitted!!");
                Messenger.Default.Send<String>("Close");
            }
        }

        public ICommand QuantityEditedCommand { get; set; }
        public ICommand AcceptCommand { get;set;}


        /// <summary>
        /// The <see cref="Details" /> property's name.
        /// </summary>
        public const string DetailsPropertyName = "Details";

        private ObservableCollection<ProductTransferDetail> _details  ;

        /// <summary>
        /// Gets the Details property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public ObservableCollection<ProductTransferDetail> Details
        {
            get
            {
                return _details;
            }

            set
            {
                if (_details == value)
                {
                    return;
                }

                var oldValue = _details;
                _details = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(DetailsPropertyName);

            }
        }

        /// <summary>
        /// The <see cref="SelectedDetail" /> property's name.
        /// </summary>
        public const string SelectedDetailPropertyName = "SelectedDetail";

        private ProductTransferDetail _selectedDetail  ;

        /// <summary>
        /// Gets the SelectedDeatil property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public ProductTransferDetail SelectedDetail
        {
            get
            {
                return _selectedDetail;
            }

            set
            {
                if (_selectedDetail == value)
                {
                    return;
                }

                var oldValue = _selectedDetail;
                _selectedDetail = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(SelectedDetailPropertyName);

            }
        }

        /// <summary>
        /// The <see cref="ProductTransfer" /> property's name.
        /// </summary>
        public const string ProductTransferPropertyName = "ProductTransfer";

        private ProductTransfer _productTransfer;

        /// <summary>
        /// Gets the ProductTransfer property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public ProductTransfer ProductTransfer
        {
            get
            {
                return _productTransfer;
            }

            set
            {
                if (_productTransfer == value)
                {
                    return;
                }

                var oldValue = _productTransfer;
                _productTransfer = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(ProductTransferPropertyName);

            }
        }
    }
}
