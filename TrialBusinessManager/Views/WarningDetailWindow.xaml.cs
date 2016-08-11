using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using TrialBusinessManager.Web;
using GalaSoft.MvvmLight;
using System.ServiceModel.DomainServices.Client;
using TrialBusinessManager.Views;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq;
using TrialBusinessManager.Models;

namespace TrialBusinessManager.Views
{
    public partial class WarningDetailWindow : ChildWindow
    {
        Dealer myDealer = new Dealer();
        AgroDomainContext _context = new AgroDomainContext();
        ObservableCollection<WarningInfo> InfoCollection = new ObservableCollection<WarningInfo>();

        public WarningDetailWindow(Dealer selectedDealer)
        {
            InitializeComponent();
            myDealer = selectedDealer;
            textBlock.Text = "Dealer: " + selectedDealer.CompanyName;
            dataGrid1.ItemsSource = InfoCollection;

            _context.Load(_context.GetDueInfoesQuery().Where(e => e.DealerId == selectedDealer.DealerId)).Completed += (s, e) =>
             {
                 foreach(DueInfo x in _context.DueInfos)
                 {
                     if (InfoCollection.Any(r => r.Due.BillId == x.BillId))
                     {
                         if ((x.ProductCost - x.SalesReturn - x.TransportationLoss - x.Paid)>0)
                                InfoCollection.Where(r => r.Due.BillId == x.BillId).First().Due.ProductCost += (x.ProductCost - x.SalesReturn - x.TransportationLoss - x.Paid-x.CommissionOnBenefit);


                     }

                     else
                     {
                         WarningInfo info = new WarningInfo();

                         info.Billcode = "BI" + x.BillId.ToString();

                         info.Due = x;
                         info.Due.ProductCost = x.ProductCost - x.SalesReturn - x.TransportationLoss - x.Paid-x.CommissionOnBenefit;

                         if (info.Due.ProductCost > 0)
                             InfoCollection.Add(info);
                     }
                 }
             };
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

