using System;
using System.Net;
using System.Linq;
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
using GalaSoft.MvvmLight.Command;
using System.ServiceModel.DomainServices.Client;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using TrialBusinessManager.Models;
using GalaSoft.MvvmLight.Messaging;

namespace TrialBusinessManager.Views.DO
{
    public partial class TransferLotAllocationChildWindow : ChildWindow
    {

        AgroDomainContext _context = new AgroDomainContext();
        ObservableCollection<LotInfo> LotInfoCollection = new ObservableCollection<LotInfo>();
        ObservableCollection<ProductTransferDetail> ProductTransferDetailList = new ObservableCollection<ProductTransferDetail>();
        TransferIndentDetail TDetails = new TransferIndentDetail();

        public TransferLotAllocationChildWindow(AgroDomainContext myContext,TransferIndentDetail myDetails, ObservableCollection<LotInfo> LotCollection, ObservableCollection<ProductTransferDetail> SentProductTransferDetail)
        {
            InitializeComponent();
            _context = myContext;
            TDetails = myDetails;
            LotInfoCollection = LotCollection;
            dataGrid1.ItemsSource = LotInfoCollection;
            ProductTransferDetailList = SentProductTransferDetail;
            reqAmountTxtBox.Text = TDetails.ApprovedProductQuantity.ToString();
            LoadCollection();
        }

        private void LoadCollection()
        {
            foreach (InventoryProductInfo x in _context.InventoryProductInfos.Where(e => e.Product.ProductId==TDetails.Product.ProductId))
            {
                LotInfo lotInfo = new LotInfo();
                Double SelectedQuantity = 0;
                lotInfo.LotId = x.LotId;
                lotInfo.Quantity = 0;
                if (ProductTransferDetailList.Any(r => r.Product.GroupName == x.Product.GroupName && r.LotId == x.LotId))
                {
                    foreach (ProductTransferDetail TransferProduct in ProductTransferDetailList.Where(r => r.Product.ProductId == x.Product.ProductId && r.LotId == x.LotId))
                    {
                      
                        SelectedQuantity += TransferProduct.TransferQuantity;
                    }
                }
                //Inventory productInfo is in Kg and producttransfer quantity is in pc//
                lotInfo.AvailableQuantity =(x.FinishedQuantity/x.Product.StockKeepingUnit) - SelectedQuantity;
                lotInfo.LotProduct = x.Product;
                LotInfoCollection.Add(lotInfo);

            }

        }


        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void dataGrid1_CellEditEnded(object sender, DataGridCellEditEndedEventArgs e)
        {
            if (LotInfoCollection.Count > 0)
            {
                double total = 0;
                for (int i = 0; i < LotInfoCollection.Count(); i++)
                {

                    if (LotInfoCollection.ElementAt(i).Quantity < 0)
                    {
                        MessageBox.Show("Quantity can not be negative!");
                        LotInfoCollection.ElementAt(i).Quantity = 0;
                        return;
                    }

                    if (LotInfoCollection.ElementAt(i).AvailableQuantity < LotInfoCollection.ElementAt(i).Quantity)
                    {
                        MessageBox.Show("Quantity can not exceed available quantity!");
                        LotInfoCollection.ElementAt(i).Quantity = 0;
                        return;
                    }

                    total += LotInfoCollection.ElementAt(i).Quantity;

                }
                if (TDetails.ApprovedProductQuantity < total)
                {
                    var selected = (LotInfo)dataGrid1.SelectedItem;
                    LotInfoCollection.Where(r => r.LotId == selected.LotId).First().Quantity = 0;
                    MessageBox.Show("Quantity can not exceed requested quantity!");
                    return;

                }
                totaltextBox.Text = total.ToString();

            }
        }
    }
}

