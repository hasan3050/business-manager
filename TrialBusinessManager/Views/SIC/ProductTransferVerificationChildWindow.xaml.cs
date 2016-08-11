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
using GalaSoft.MvvmLight;
using TrialBusinessManager.Views;
using TrialBusinessManager.Models;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ServiceModel.DomainServices.Client;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Command;
using System.ServiceModel;

namespace TrialBusinessManager.Views.SIC
{
    public partial class ProductTransferVerificationChildWindow : ChildWindow
    {
        AgroDomainContext _context = new AgroDomainContext();
        ProductTransfer ReceivedProductTransfer = new ProductTransfer();
        BusyWindow Busy = new BusyWindow();

        public ProductTransferVerificationChildWindow(ProductTransfer SentTransfer,AgroDomainContext SentContext)
        {
            InitializeComponent();
         
            _context = SentContext;
            ReceivedProductTransfer = SentTransfer;
            LoadProdutDetails();
         
            VehicleNoTextBox.Text = SentTransfer.VehicleNo;
            EmergencyContactTextBox.Text = SentTransfer.EmergencyContactNo;
            TransportTypeTextBox.Text = SentTransfer.TransportType;
            TransportCostTextBox.Text = SentTransfer.TransportCost.ToString();

         
        }

        void LoadProdutDetails()
        {
            _context.Load(_context.GetProductTransferDetailsQuery().Where(e => e.ProductTransferId == ReceivedProductTransfer.ProductTransferID)).Completed += (a, b) =>
            {
                dataGrid1.ItemsSource = ReceivedProductTransfer.ProductTransferDetails;
            };
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            ReceivedProductTransfer.ProductTransferStatus = "Verified";
            ReceivedProductTransfer.ProductLossCost = 0;
            ReceivedProductTransfer.RecievedBySICId = UserInfo.EmployeeID;
            ReceivedProductTransfer.DateOfRecieve = DateTime.Now;
            bool flag = false;

            foreach (ProductTransferDetail x in ReceivedProductTransfer.ProductTransferDetails)
            {
                if (x.RecievedQuantity > 0)
                {
                    flag = true;
                    break;
                }
            }

            if (!flag)
            {
                MessageBoxResult result = MessageBox.Show("You have all products with received quantity 0 kg. Are you sure you want to receive the product transfer? If yes, press OK, else press Canel","Alert!",MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.Cancel) return;
            }

            foreach (ProductTransferDetail x in ReceivedProductTransfer.ProductTransferDetails)
            {
                if (x.RecievedQuantity < 0)
                {
                    MessageBox.Show("Quantity can not be negative!!");
                    return;
                }

                if(x.TransferQuantity < x.RecievedQuantity)
                {
                    MessageBox.Show("Invalid quantity input.received quantity can not be more than tranferred quantity!");
                    return;
                }

                if ((x.TransferQuantity - x.RecievedQuantity) > 0)
                {
                    ReceivedProductTransfer.HasProductLoss = true;
                    ReceivedProductTransfer.ProductLossCost += (x.TransferQuantity - x.RecievedQuantity)*x.Product.StockKeepingUnit; 
                }
              /*  if (_context.InventoryProductInfos.Any(r => r.ProductId == x.ProductId))
                {
                    InventoryProductInfo update = new InventoryProductInfo();
                    InventoryProductInfo dummy = new InventoryProductInfo();

                    update = _context.InventoryProductInfos.Where(w => w.InventoryId == UserInfo.Inventory.InventoryId && w.Product.ProductId == x.Product.ProductId && w.LotId == x.LotId).Single();
                    dummy = _context.InventoryProductInfos.Where(w => w.InventoryId == UserInfo.Inventory.InventoryId && w.Product.GroupName == x.Product.GroupName && w.LotId == x.LotId && w.Product.ProductName == "DUMMY").Single();

                    update.FinishedQuantity -= x.RecievedQuantity;
                    dummy.FinishedQuantity -= x.RecievedQuantity;
                }
                else
                {
                    InventoryProductInfo NewProduct = new InventoryProductInfo();
                    InventoryProductInfo NewDummy = new InventoryProductInfo();
                    update

                }*/
            
            }
            Busy.Show();
            _context.SubmitChanges().Completed += new EventHandler(ProductTransferVerificationChildWindow_Completed);
            
        }

        void ProductTransferVerificationChildWindow_Completed(object sender, EventArgs e)
        {
           
            Busy.Close();
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

