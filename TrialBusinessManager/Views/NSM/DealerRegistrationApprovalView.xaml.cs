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


namespace TrialBusinessManager.Views.NSM
{
    public partial class DealerRegistrationApprovalView : Page
    {
        AgroDomainContext _context = new AgroDomainContext();
        BusyWindow Busy = new BusyWindow();
        public DealerRegistrationApprovalView()
        {
            InitializeComponent();
            Busy.Show();
            LoadDealer();
           
        }
        void LoadDealer()
        {
            _context.Load(_context.GetDealersQuery().Where(e => e.ActivityStatus == "Pending")).Completed += (s, args) => 
            {
                Busy.Close();
                dataGrid1.ItemsSource = _context.Dealers.Where(e => e.ActivityStatus == "Pending"); 

            };
        
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            Dealer MyDealer = new Dealer();
            MyDealer = (Dealer)dataGrid1.SelectedItem;
           
                DealerRegistrationChildWindow obj = new DealerRegistrationChildWindow(MyDealer, _context);
                obj.Show();
            }
            catch (Exception ex)
            {
                return;
            }
        }

        // Executes when the user navigates to this page.
       

    }
}
