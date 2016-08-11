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
using System.Windows.Navigation;


namespace TrialBusinessManager.Views.SIC
{
    public partial class ProductTransferApprovalNotification : Page
    {
        AgroDomainContext _context = new AgroDomainContext();
        BusyWindow MyWindow = new BusyWindow();
        ProductTransferApprovalWindow Window = new ProductTransferApprovalWindow();
        int flag = 0;
        
        public ProductTransferApprovalNotification()
        {
            InitializeComponent();
            BindData();
            LoadData();
        }

        void LoadData()
        {
            MyWindow.Show();
            
            _context.Load(_context.GetProductTransfersByInvenotryIdQuery(UserInfo.Inventory.InventoryId, "Issued By DO")).Completed += (s, e) =>
                {
                    flag++;
                    if (flag == 2)
                    {
                        MyWindow.Close();
                    }
                };

            _context.Load(_context.GetProductTransferDetailsByInvenotryIDQuery(UserInfo.Inventory.InventoryId, "Issued By DO")).Completed += (s, e) =>
                {
                    flag++;
                    if (flag == 2)
                    {
                        MyWindow.Close();
                    }
                };
        }

        void BindData()
        {
            productTransferDataGrid.ItemsSource = _context.ProductTransfers;
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void productTransferDomainDataSource_LoadedData(object sender, LoadedDataEventArgs e)
        {

            if (e.HasError)
            {
                System.Windows.MessageBox.Show(e.Error.ToString(), "Load Error", System.Windows.MessageBoxButton.OK);
                e.MarkErrorAsHandled();
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (productTransferDataGrid.SelectedIndex == -1)
            {
                MessageBox.Show("Please Selecte one to approve");
                return;
            }

            Messenger.Default.Send<AgroDomainContext>(_context);
            Messenger.Default.Send<ProductTransfer>((ProductTransfer)productTransferDataGrid.SelectedItem);
            Window.Show();
        }

    }
}
