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
    public partial class SalesReturnNSMVerificationChildWindow : ChildWindow
    {
        AgroDomainContext _context = new AgroDomainContext();
        SalesReturn SelectedSalesReturn = new SalesReturn();
        Dealer SelectedDealer = new Dealer();
        int Flag = 0;
        BusyWindow Busy = new BusyWindow();
        public SalesReturnNSMVerificationChildWindow(SalesReturn mySalesReturn, Dealer myDealer,AgroDomainContext MyContext)
        {
            InitializeComponent();
            _context = MyContext;
            this.SelectedSalesReturn = mySalesReturn;
            this.SelectedDealer = myDealer;
            SetTextBoxes();

            OKButton.IsEnabled = false;
            button1.IsEnabled = false;
       
            _context.Load(_context.GetSalesReturnInfoesQuery().Where(e => e.SalesReturnId == mySalesReturn.SalesReturnId)).Completed+=new EventHandler(SalesReturnNSMVerificationChildWindow_Completed);
            _context.Load(_context.GetDealersQuery().Where(e => e.DealerId == SelectedDealer.DealerId)).Completed += new EventHandler(SalesReturnNSMVerificationChildWindow_Completed);
            _context.Load(_context.GetSalesReturnsQuery().Where(e => e.SalesReturnId == SelectedSalesReturn.SalesReturnId)).Completed += new EventHandler(SalesReturnNSMVerificationChildWindow_Completed);
            
            dataGrid1.ItemsSource = _context.SalesReturnInfos.Where(r=>r.SalesReturnId==mySalesReturn.SalesReturnId);
        }

        void SetTextBoxes()
        {
            DealerNametextBox.Text = SelectedDealer.Name;
            DealerAddressTextBlock.Text = SelectedDealer.CompanyAddress;
            IssueDatetextBox.Text = SelectedSalesReturn.DateOfIssue.ToString();
        }

        ///////////////////Event for Approve button////////////////
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            long SalesReturnId = SelectedSalesReturn.SalesReturnId;
            SelectedSalesReturn = _context.SalesReturns.Where(r => r.SalesReturnId == SalesReturnId).Single();
            SelectedSalesReturn.Status = "Verified by NSM";
            Busy.Show();
            _context.SubmitChanges().Completed += new EventHandler(Submit_Completed);
            
        }
        void Submit_Completed(object sender, EventArgs e)
        {
            Busy.Close();
            this.DialogResult = true;
        }


        void SalesReturnNSMVerificationChildWindow_Completed(object sender, EventArgs e)
        {
            Flag++;
            if (Flag == 3)
            {
                dataGrid1.ItemsSource = _context.SalesReturnInfos.Where(r => r.SalesReturnId == SelectedSalesReturn.SalesReturnId);
     
                OKButton.IsEnabled = true;
                button1.IsEnabled = true;
            }
        }


        ///////////////////Event for Close button/////////////////
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }


        ///////////////////Event for Reject button////////////////
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            long SalesReturnId = SelectedSalesReturn.SalesReturnId;
            SelectedSalesReturn = _context.SalesReturns.Where(r => r.SalesReturnId == SalesReturnId).Single();
            SelectedSalesReturn.Status = "Rejected";
            Busy.Show();
            _context.SubmitChanges().Completed += new EventHandler(Submit_Completed);
           
        }
    }
}

