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
    public partial class DealerRegistrationChildWindow : ChildWindow
    {
        AgroDomainContext _context = new AgroDomainContext();
        Dealer MyDealer = new Dealer();
        BusyWindow Busy = new BusyWindow();
        public DealerRegistrationChildWindow(Dealer sentDealer,AgroDomainContext sentContext)
        {
            InitializeComponent();
            _context = sentContext;
            MyDealer = _context.Dealers.Where(e=>e.DealerId==sentDealer.DealerId).Single();
            LayoutRoot.DataContext = MyDealer;
        }

      

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Busy.Show();
            _context.Dealers.Remove(MyDealer);
            _context.SubmitChanges().Completed += (s, args) => { Busy.Close(); this.DialogResult = true; };
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Busy.Show();
            MyDealer.ActivityStatus = "Active";
            _context.SubmitChanges().Completed += (s, args) => { Busy.Close(); this.DialogResult = true; };
        }
    }
}

