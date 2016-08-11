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

namespace TrialBusinessManager.Views.Co_ordinator
{
    public partial class CODealerEditChildWindow : ChildWindow
    {
        
        AgroDomainContext _context = new AgroDomainContext();
        Dealer EditDealer = new Dealer();

        public CODealerEditChildWindow(Dealer SentDealer,AgroDomainContext SentContext)
        {
            InitializeComponent();
           
            _context = SentContext;
            EditDealer = _context.Dealers.Where(e=>e.DealerId==SentDealer.DealerId).Single();
            LayoutRoot.DataContext = EditDealer;
           
            LoadSalesOfficers();
            
                 
        }

        public void LoadSalesOfficers()
        {
            _context.Load(_context.GetEmployeesQuery().Where(c => c.Designation == "Sales Officer" && c.Region.RegionName == UserInfo.RegionName)).Completed += (s, args) => { EditDealer.Employee = _context.Dealers.Where(e => e.DealerId == EditDealer.DealerId).Single().Employee; };
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _context.SubmitChanges().Completed += (s, args) => { this.DialogResult = true; };
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _context.RejectChanges();
            this.DialogResult = false;

        }
    }
}

