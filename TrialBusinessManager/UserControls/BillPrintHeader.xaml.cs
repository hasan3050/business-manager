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

namespace TrialBusinessManager.UserControls
{
    public partial class BillPrintHeader : UserControl
    {
        public BillPrintHeader( Bill PrintBill)
        {
            InitializeComponent();
            BillControl.DataContext = PrintBill;
            DealerControl.DataContext = PrintBill.Dealer;

           
            if ((PrintBill.PaymentDeadline.Date-PrintBill.DateOfIssue.Date).TotalDays == 0)
                PaymentMethodTxt.Text = "Advance Payment";
            if ((PrintBill.PaymentDeadline.Date - PrintBill.DateOfIssue.Date).TotalDays == 7)
                PaymentMethodTxt.Text = "Cash Payment";
            if ((PrintBill.PaymentDeadline.Date - PrintBill.DateOfIssue.Date).TotalDays == 30)
                PaymentMethodTxt.Text = "One Month Credit";
            if ((PrintBill.PaymentDeadline.Date - PrintBill.DateOfIssue.Date).TotalDays == 60)
                PaymentMethodTxt.Text = "Two Month Credit";
            if ((PrintBill.PaymentDeadline.Date - PrintBill.DateOfIssue.Date).TotalDays == 90)
                PaymentMethodTxt.Text = "Three Month Credit";
        
        
        
            
        }


    }
}
