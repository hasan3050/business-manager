using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using TrialBusinessManager.Web;
using System.ServiceModel.DomainServices.Client;
using TrialBusinessManager.Models;
using TrialBusinessManager.Views.NSM;


namespace TrialBusinessManager.Views
{
    public partial class VerifySalesReturns : Page
    {
        AgroDomainContext _context = new AgroDomainContext();
        BusyWindow Busy = new BusyWindow();
        int Flag = 0;
        public VerifySalesReturns()
        {

            InitializeComponent();
            Busy.Show();
            LoadSalesReturn();

        }

        void LoadSalesReturn()
        {

            if (UserInfo.Designation == "National Sales Manager")
            {
                _context.Load(_context.GetSalesReturnsQuery().Where(e => e.Status == "Placed")).Completed += new EventHandler(VerifySalesReturns_Completed);
            }

            else if (UserInfo.Designation == "Store In Charge")
            {
                _context.Load(_context.GetSalesReturnsQuery().Where(e => e.Status == "Verified by NSM"&&e.SendToInventoryId==UserInfo.Inventory.InventoryId)).Completed += new EventHandler(VerifySalesReturns_Completed);
         
            }
        
        }

        void VerifySalesReturns_Completed(object sender, EventArgs e)
        {
          
            Busy.Close();
            dataGrid1.ItemsSource = _context.SalesReturns;
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
         


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SalesReturn selectedReturn = new SalesReturn();

                selectedReturn = (SalesReturn)dataGrid1.SelectedItem;

                if (selectedReturn.Status == "Placed")
                {
                    SalesReturnNSMVerificationChildWindow obj = new SalesReturnNSMVerificationChildWindow(selectedReturn, selectedReturn.Dealer,_context);
                    obj.Show();
                }

                else if (selectedReturn.Status == "Verified by NSM")
                {
                    SalesReturnVerificationChildWindow obj = new SalesReturnVerificationChildWindow(selectedReturn, selectedReturn.Dealer, _context);
                    obj.Show();
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }

    }
}
