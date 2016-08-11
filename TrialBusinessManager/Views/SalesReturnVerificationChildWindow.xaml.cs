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

namespace TrialBusinessManager.Views
{
    public partial class SalesReturnVerificationChildWindow : ChildWindow
    {
        AgroDomainContext _context = new AgroDomainContext();
        SalesReturn SelectedSalesReturn = new SalesReturn();
        Dealer SelectedDealer = new Dealer();
      
        BusyWindow Busy = new BusyWindow();
        int Flag = 0;
        public SalesReturnVerificationChildWindow(SalesReturn mySalesReturn,Dealer myDealer,AgroDomainContext myContext)
        {
            InitializeComponent();
            OKButton.IsEnabled = false;
            CancelButton.IsEnabled = false;
            _context = myContext;
            this.SelectedSalesReturn = mySalesReturn;
            this.SelectedDealer = myDealer;
            SetTextBoxes();
          //  _context.Load(_context.GetBillsQuery());
            _context.Load(_context.GetSalesReturnInfoesQuery().Where(e=>e.SalesReturnId==mySalesReturn.SalesReturnId)).Completed+=new EventHandler(SalesReturnVerificationChildWindow_Completed);
            _context.Load(_context.GetDealersQuery().Where(e => e.DealerId == SelectedDealer.DealerId)).Completed += new EventHandler(SalesReturnVerificationChildWindow_Completed);
            _context.Load(_context.GetSalesReturnsQuery().Where(e => e.SalesReturnId == SelectedSalesReturn.SalesReturnId)).Completed += new EventHandler(SalesReturnVerificationChildWindow_Completed);
             }
        void SetTextBoxes()
        {
            DealerNametextBox.Text = SelectedDealer.Name;
            DealerAddressTextBlock.Text = SelectedDealer.CompanyAddress;
            IssueDatetextBox.Text = SelectedSalesReturn.DateOfIssue.ToString();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (SalesReturnInfo x in _context.SalesReturnInfos.Where(r => r.SalesReturnId == SelectedSalesReturn.SalesReturnId))
            {
                if (x.AcceptedQuantity > x.PlacedQuantity)
                {
                    MessageBox.Show("Accepted quantity can not exceed placed quantity!");
                    x.AcceptedQuantity = x.PlacedQuantity;

                    return;
                }

            }
           
            Double TotalAcceptedAmount = 0;

            foreach (SalesReturnInfo x in _context.SalesReturnInfos.Where(r => r.SalesReturnId == SelectedSalesReturn.SalesReturnId))
            {
                TotalAcceptedAmount += x.AcceptedQuantity*x.ProductPrice;
                
            }
            long SalesReturnId=SelectedSalesReturn.SalesReturnId;
           
            SelectedSalesReturn = _context.SalesReturns.Where(r => r.SalesReturnId == SalesReturnId).Single();
          

           
            

            SelectedSalesReturn.TotalAcceptedAmount = TotalAcceptedAmount;
            SelectedSalesReturn.Status = "Approved";
            Busy.Show();
            _context.SubmitChanges().Completed += new EventHandler(Submit_Completed);
       
        }
        //Need to update inventory product info////
        //////Proedure needs to be introduced//////
        //////////////////////////////Unfinished need to update dueinfo//////////////////////////////
      
        //////Proedure needs to be introduced//////
        /////////////////////////////update ledger///////////////////////////////
        void UpdateLeadger()
        {
            Ledger SalesReturnLedger = new Ledger();
        
        }
        void SalesReturnVerificationChildWindow_Completed(object sender, EventArgs e)
        {
            Flag++;
            if (Flag == 3)
            {
                dataGrid1.ItemsSource = _context.SalesReturnInfos.Where(r => r.SalesReturnId == SelectedSalesReturn.SalesReturnId);
      
                OKButton.IsEnabled = true;
                CancelButton.IsEnabled = true;
            }
        }

        void Submit_Completed(object sender, EventArgs e)
        {
            Busy.Close();
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private class BillUpdateTracker
        {
           public long Billid;
           public Double SalesReturnAmount=0;
        }
    }
}

