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
    public partial class ProductConvertChildWindow : ChildWindow
    {
        AgroDomainContext _context = new AgroDomainContext();
        ObservableCollection<Product> InventoryProductCollection = new ObservableCollection<Product>();
        ObservableCollection<String> Lots = new ObservableCollection<string>();
        BusyWindow Busy = new BusyWindow();
        public ProductConvertChildWindow()
        {
            InitializeComponent();
            LoadInventoryProducts();
        }

        void LoadInventoryProducts()
        {
            _context.Load(_context.GetInventoryProductInfoesQuery().Where(e => e.InventoryId == UserInfo.Inventory.InventoryId)).Completed += (s, e) =>
                {
                    foreach (InventoryProductInfo x in _context.InventoryProductInfos)
                    {
                        if(!InventoryProductCollection.Any(r=>r.ProductId==x.ProductId)&&x.Product.ProductName!="DUMMY")
                                 InventoryProductCollection.Add(x.Product);
                    }
                    FromProductComboBox.ItemsSource = InventoryProductCollection;
                };

            _context.Load(_context.GetProductsQuery()).Completed += (s, e) => 
            {
                ToProductComboBox.ItemsSource = _context.Products.Where(r => r.ProductName != "DUMMY");
            
            };
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                InventoryProductInfo FromProduct = new InventoryProductInfo();
                var selectedFromProduct = (Product)FromProductComboBox.SelectedItem;
                var FromDummy = (InventoryProductInfo)_context.InventoryProductInfos.Where(r => r.Product.GroupName == selectedFromProduct.GroupName && r.Product.ProductName == "DUMMY" && r.InventoryId == UserInfo.Inventory.InventoryId && r.LotId == (String)FromLotcomboBox.SelectedItem).First();
                InventoryProductInfo ToProduct = new InventoryProductInfo();
                var selectedToProduct = (Product)ToProductComboBox.SelectedItem;
                //Minus from product
                FromProduct = _context.InventoryProductInfos.Where(r => r.ProductId == selectedFromProduct.ProductId && r.LotId == (String)FromLotcomboBox.SelectedItem && r.InventoryId == UserInfo.Inventory.InventoryId).First();
                if (double.Parse(QuantityTextBox.Text) > FromProduct.FinishedQuantity)
                {
                    MessageBox.Show("Invalid quantity input. Can not convert more than " + FromProduct.FinishedQuantity+" kg!!");
                    return;
                }
                FromProduct.FinishedQuantity -= double.Parse(QuantityTextBox.Text);
                //Minus from dummy
                FromDummy.FinishedQuantity -= double.Parse(QuantityTextBox.Text);
                CreateLog(FromProduct, double.Parse(QuantityTextBox.Text), true);
                //if 'to product' exists in inventory//
              //  MessageBox.Show(selectedToProduct.ProductId.ToString()+","+selectedToProduct.ProductName+","+ToLotTextBox.Text);
              //  MessageBox.Show(_context.InventoryProductInfos.Where(r=>r.InventoryId==UserInfo.Inventory.InventoryId&&r.ProductId==67&&r.LotId=="01").Single().Product.ProductName);
              
                if (_context.InventoryProductInfos.Any(r => r.ProductId == selectedToProduct.ProductId && r.LotId == ToLotTextBox.Text && r.InventoryId == UserInfo.Inventory.InventoryId))
                {
                    //add to 'to product'
                 
                    ToProduct = _context.InventoryProductInfos.Where(r => r.ProductId == selectedToProduct.ProductId && r.LotId == ToLotTextBox.Text && r.InventoryId == UserInfo.Inventory.InventoryId).First();
                    ToProduct.FinishedQuantity += double.Parse(QuantityTextBox.Text);
                    //add to dummy
                    var ToDummy = (InventoryProductInfo)_context.InventoryProductInfos.Where(r => r.Product.GroupName == selectedToProduct.GroupName && r.Product.ProductName == "DUMMY" && r.InventoryId == UserInfo.Inventory.InventoryId && r.LotId == ToLotTextBox.Text).First();
                    ToDummy.FinishedQuantity += double.Parse(QuantityTextBox.Text);
                    CreateLog(ToProduct, double.Parse(QuantityTextBox.Text), false);
                
                }

                else
                {
                  
                    //new 'to product'
                    ToProduct.Product = selectedToProduct;
                    ToProduct.InventoryId = UserInfo.Inventory.InventoryId;
                    ToProduct.LotId = ToLotTextBox.Text;
                    ToProduct.UnitLotCost = _context.InventoryProductInfos.Where(r => r.ProductId == selectedFromProduct.ProductId && r.LotId == (String)FromLotcomboBox.SelectedItem && r.InventoryId == UserInfo.Inventory.InventoryId).First().UnitLotCost;
                    ToProduct.UnfinishedQuantity = 0;
                    ToProduct.OnProcessingQuantity = 0;
                    ToProduct.FinishedQuantity = double.Parse(QuantityTextBox.Text);

                    _context.InventoryProductInfos.Add(ToProduct);
                    //new dummy
                    if (_context.InventoryProductInfos.Any(r => r.Product.GroupName == selectedToProduct.GroupName && r.Product.ProductName == "DUMMY" && r.LotId == ToLotTextBox.Text && r.InventoryId == UserInfo.Inventory.InventoryId))
                    {
                        var ToDummy = (InventoryProductInfo)_context.InventoryProductInfos.Where(r => r.Product.GroupName == selectedToProduct.GroupName && r.Product.ProductName == "DUMMY" && r.InventoryId == UserInfo.Inventory.InventoryId && r.LotId == ToLotTextBox.Text).First();
                        ToDummy.FinishedQuantity += double.Parse(QuantityTextBox.Text);
                        CreateLog(ToProduct, double.Parse(QuantityTextBox.Text),false);
                    }
                    else
                    {
                        var ToDummy = new InventoryProductInfo();
                        ToDummy.Product = _context.Products.Where(r => r.ProductName == "DUMMY" && r.GroupName == selectedToProduct.GroupName).First();
                        ToDummy.InventoryId = UserInfo.Inventory.InventoryId;
                        ToDummy.LotId = ToLotTextBox.Text;
                        ToDummy.UnitLotCost = _context.InventoryProductInfos.Where(r => r.ProductId == selectedFromProduct.ProductId && r.LotId == (String)FromLotcomboBox.SelectedItem && r.InventoryId == UserInfo.Inventory.InventoryId).First().UnitLotCost;
                        ToDummy.UnfinishedQuantity = 0;
                        ToDummy.OnProcessingQuantity = 0;
                        ToDummy.FinishedQuantity = double.Parse(QuantityTextBox.Text);

                        _context.InventoryProductInfos.Add(ToDummy);
                        CreateLog(ToProduct, double.Parse(QuantityTextBox.Text),false);
                   
                    }
                }

            }
            catch (Exception ex)
            {
            //    MessageBox.Show(ex.Message);
                 MessageBox.Show("Some error occured.please input again!");
                this.DialogResult = false;
            }
            Busy.Show();
            _context.SubmitChanges().Completed += new EventHandler(ProductConvertChildWindow_Completed);
           // this.DialogResult = true;
        }

        void CreateLog(InventoryProductInfo Info, Double Quantity, bool flag)
        {
            InventoryLog Log = new InventoryLog();
            if (flag == true) Log.Method = "Product Convert From";
            else Log.Method = "Product Convert To";
            Log.ProductId = Info.ProductId;
            Log.Date = DateTime.Now;
            Log.InventoryId = UserInfo.Inventory.InventoryId;
            Log.MemoNo = 0;
            Log.ProductQuantity = Quantity;
            Log.LotId = Info.LotId;
            Log.ProductCost = Quantity * (double)(Info.UnitLotCost);
            _context.InventoryLogs.Add(Log);
        }

        void ProductConvertChildWindow_Completed(object sender, EventArgs e)
        {
            Busy.Close();
            this.DialogResult = true;
          //  throw new NotImplementedException();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void FromProductComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedProduct = (Product)FromProductComboBox.SelectedItem;
            Lots.Clear();
            foreach (InventoryProductInfo x in _context.InventoryProductInfos)
            {
                if (x.ProductId == selectedProduct.ProductId)
                    Lots.Add(x.LotId);
            }

            FromLotcomboBox.ItemsSource = Lots;

        }

        

    }
}

