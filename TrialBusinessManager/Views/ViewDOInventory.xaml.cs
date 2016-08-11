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
using TrialBusinessManager.Views;

namespace TrialBusinessManager.Views
{
    public partial class ViewDOInventory : Page
    {
        AgroDomainContext _context = new AgroDomainContext();
        BusyWindow busy = new BusyWindow();
        public ViewDOInventory()
        {
            InitializeComponent();
            LoadInventoryProductInfo();
        }
        void LoadInventoryProductInfo()
        {
            busy.Show();
            _context.Load(_context.GetInventoryProductInfoesQuery().Where(e => e.InventoryId == UserInfo.Inventory.InventoryId&&e.Product.ProductName!="DUMMY")).Completed += new EventHandler(ViewDOInventory_Completed);
        }

        void ViewDOInventory_Completed(object sender, EventArgs e)
        {
            dataGrid1.ItemsSource=_context.InventoryProductInfos;
            autoCompleteBox1.ItemsSource =_context.InventoryProductInfos;
            busy.Close();
        }

        private void autoCompleteBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                InventoryProductInfo selectedProduct = new InventoryProductInfo();
                selectedProduct = (InventoryProductInfo)autoCompleteBox1.SelectedItem;
                dataGrid1.UpdateLayout();
                this.dataGrid1.Dispatcher.BeginInvoke(delegate
                {

                    this.dataGrid1.Focus();

                    dataGrid1.SelectedItem = selectedProduct;

                    this.dataGrid1.ScrollIntoView(selectedProduct, this.dataGrid1.Columns[0]);

                });
            }
            catch (Exception ex)
            {
                return;
            }
        }
      

    }
}
