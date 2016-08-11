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
using GalaSoft.MvvmLight.Command;
using System.ServiceModel.DomainServices.Client;
using System.ServiceModel;
using System.Collections.ObjectModel;
using TrialBusinessManager.Models;
using System.Linq;
using System.Collections.Generic;
using TrialBusinessManager.Views;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrialBusinessManager.Views.RM
{
    public partial class EditDealerChildWindow : ChildWindow
    {
        AgroDomainContext _context = new AgroDomainContext();
        Dealer EditDealer = new Dealer();
        BusyWindow Busy = new BusyWindow();
        public EditDealerChildWindow(Dealer SentDealer,AgroDomainContext SentContext)
        {
            InitializeComponent();
           
            _context = SentContext;
            EditDealer = _context.Dealers.Where(e => e.DealerId == SentDealer.DealerId).Single();
            LayoutRoot.DataContext = EditDealer;
            LoadDealer();
            LoadSalesOfficers();
            PaymentTypeComboBox.ItemsSource = ConstantCollections.CommissionName;
            BusinessTypeComboBox.ItemsSource= ConstantCollections.BuisnessTypes;
            statuscomboBox.ItemsSource = ConstantCollections.ActivityStatus;
            
                 
        }

        public void LoadDealer()
        {
            _context.Load(_context.GetDealersQuery().Where(e => e.DealerId == EditDealer.DealerId)).Completed += (s, e) => {
                long dealerId;
                dealerId = EditDealer.DealerId;
                EditDealer = _context.Dealers.Where(r => r.DealerId == dealerId).SingleOrDefault(); 
              // 
                
         
            }; 
        }

        public void LoadSalesOfficers()
        {
            _context.Load(_context.GetEmployeesQuery().Where(c => c.Designation == "Sales Officer" && c.Region.RegionId == EditDealer.RegionId)).Completed += (s, args) => { SalesOfficersComboBox.ItemsSource = _context.Employees.Where(e => e.Designation == "Sales Officer" && e.Region.RegionId == EditDealer.RegionId); };
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Busy.Show();
            _context.SubmitChanges().Completed += (s, args) => {
                Busy.Close();
                this.DialogResult = true; };
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _context.RejectChanges();
            this.DialogResult = false;

        }

        private void statuscomboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          //  _context.Dealers.Where(r => r.DealerId == EditDealer.DealerId).SingleOrDefault().ActivityStatus = (String)statuscomboBox.SelectedItem;
            //EditDealer.ActivityStatus = (String)statuscomboBox.SelectedItem;        
        }

     /*   private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (EditDealer.ActivityStatus == "Active")
                _context.Dealers.Where(r => r.DealerId == EditDealer.DealerId).SingleOrDefault().ActivityStatus="Inactive";
            else
                _context.Dealers.Where(r => r.DealerId == EditDealer.DealerId).SingleOrDefault().ActivityStatus = "Active";

            Busy.Show();
            _context.SubmitChanges().Completed += (s, args) =>
            {
                Busy.Close();
                this.DialogResult = true;
            };
            
       
        }*/
    }
}

