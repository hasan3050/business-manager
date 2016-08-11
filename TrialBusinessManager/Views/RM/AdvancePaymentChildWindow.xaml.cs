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
using TrialBusinessManager.Models;

namespace TrialBusinessManager.Views.RM
{
    public partial class AdvancePaymentChildWindow : ChildWindow
    {
        public AdvancePaymentChildWindow()
        {
            InitializeComponent();
            ShopNameTxtBox.Text = StaticMessaging.DealerMessage.CompanyName;
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

