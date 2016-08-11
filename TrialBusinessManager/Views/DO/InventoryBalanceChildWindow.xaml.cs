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
using System.ComponentModel;
using TrialBusinessManager.Models;
using TrialBusinessManager.Views;


namespace TrialBusinessManager.Views.DO
{
    public partial class InventoryBalanceChildWindow : ChildWindow
    {
        TransportationLoss MyTLOSS = new TransportationLoss();
        ObservableCollection<BalanceInventoryModel> dataGridCollection = new ObservableCollection<BalanceInventoryModel>();
        AgroDomainContext _context = new AgroDomainContext();
        Double total = 0;
        int Flag = 0;
        public InventoryBalanceChildWindow(TransportationLoss SentTLoss,AgroDomainContext SentContext)
        {
            InitializeComponent();
            OKButton.IsEnabled = false;
            MyTLOSS = SentTLoss;
            _context = SentContext;
            LoadBillProducts();
            LoadInventoryProducts();
            LoadProducts();
            TotalLosttextBox.Text = MyTLOSS.LossQuantity.ToString();
            TotaltextBox.Text = "0";
           
            
            
        }

        void LoadProducts()
        {
            _context.Load(_context.GetProductsQuery().Where(t=>t.ProductName=="DUMMY")).Completed+=new EventHandler(InventoryBalanceChildWindow_Completed);
        }


        void LoadBillProducts()
        {
            
            _context.Load(_context.GetBillProductInfoesQuery().Where(e => e.BillId == MyTLOSS.BillId)).Completed += (s, args) => {
                foreach (BillProductInfo x in _context.BillProductInfos)
                {
                    if (x.BillId == MyTLOSS.BillId)
                    {

                        BalanceInventoryModel balance = new BalanceInventoryModel();
                        balance.Product = MyTLOSS.Product;
                        balance.LotId = x.LotId;
                        balance.DispatchedQuantity = x.LotQuantity;
                        balance.BalanceQuantity = 0;
                        dataGridCollection.Add(balance);
                    }
                        
                }
                dataGrid1.ItemsSource = dataGridCollection;

                Flag++;
                if (Flag == 3)
                    OKButton.IsEnabled = true;
              
            };
        
        }

        void LoadInventoryProducts()
        {
            _context.Load(_context.GetInventoryProductInfoesQuery().Where(e => e.Inventory.InventoryId == UserInfo.Inventory.InventoryId && e.Product.GroupName == MyTLOSS.Product.GroupName)).Completed += new EventHandler(InventoryBalanceChildWindow_Completed);
        }

        void InventoryBalanceChildWindow_Completed(object sender, EventArgs e)
        {
            Flag++;
            if(Flag==3)
                OKButton.IsEnabled = true;
        }


        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            MyTLOSS.HasBalanced = true;
            MyTLOSS.BalancedQuantity = total;

            if (dataGridCollection.Count <= 0)
                return;

            foreach (BalanceInventoryModel x in dataGridCollection)
            {
                if (x.BalanceQuantity > 0)
                {
                    if (_context.InventoryProductInfos.Any(r => r.Product == x.Product && r.LotId == x.LotId))
                    {
                        InventoryProductInfo inventoryProduct = new InventoryProductInfo();
                        InventoryProductInfo dummy = new InventoryProductInfo();
                        try
                        {
                            dummy = _context.InventoryProductInfos.Where(r => r.Product.GroupName == x.Product.GroupName && r.LotId == x.LotId && r.Product.ProductName == "DUMMY").First();
                            inventoryProduct = _context.InventoryProductInfos.Where(r => r.Product.ProductId == x.Product.ProductId && r.LotId == x.LotId).First();
                            inventoryProduct.FinishedQuantity += x.BalanceQuantity * x.Product.StockKeepingUnit;
                            dummy.FinishedQuantity += x.BalanceQuantity * x.Product.StockKeepingUnit;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Some error occured!!");
                            return;
                        }
                    }
                    else 
                    {
                        MessageBox.Show("Some error occured!The product "+x.Product.ProductName+" can not be updated!");
                        return;
                      
                    }

                }
            }
            _context.SubmitChanges().Completed += (s, args) => { this.DialogResult = true; };
           
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void dataGrid1_CellEditEnded(object sender, DataGridCellEditEndedEventArgs e)
        {
            total = 0;
            
            foreach (BalanceInventoryModel x in dataGridCollection)
            {
                if (x.BalanceQuantity > x.DispatchedQuantity)
                {
                    MessageBox.Show("Balanced quantity can not be more than dispatched quantity");
                    x.BalanceQuantity = 0;
                    return;
                }
                total += x.BalanceQuantity;
                
            }
            TotaltextBox.Text = total.ToString();
            if (total > MyTLOSS.LossQuantity)
            {
                MessageBox.Show("Total Balanced quantity can not be more than Total Lost quantity!!");
                return;
            }
            if (total>0)
                OKButton.IsEnabled = true;


            else OKButton.IsEnabled = false;
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ChildWindow_Closed(object sender, EventArgs e)
        {

        }
    }
}

