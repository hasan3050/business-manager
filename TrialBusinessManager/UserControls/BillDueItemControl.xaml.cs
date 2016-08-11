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

namespace TrialBusinessManager.UserControls
{
    public partial class BillDueItemControl : UserControl
    {
        public BillDueItemControl(UnpaidBillModel unpaidBill)
        {
            InitializeComponent();
            if (unpaidBill.Bill.BillCode == "Unknown")
                BillCodeText.Text = "Opening";
            else
                BillCodeText.Text = unpaidBill.Bill.BillCode;

            IssueDateText.Text = unpaidBill.Bill.DateOfIssue.ToShortDateString();
            AmountText.Text = unpaidBill.DueAmount.ToString();
            
        }

        public BillDueItemControl(Double totalDue)
        {
            InitializeComponent();
            BillCodeText.Text = "Total Due";
            IssueDateText.Text = "................";
            AmountText.Text = totalDue.ToString();
        }
    }
}
