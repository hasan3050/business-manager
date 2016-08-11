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
using GalaSoft.MvvmLight.Messaging;
using TrialBusinessManager.Models;

namespace TrialBusinessManager.Views.SIC
{
    public partial class ProductTransferApprovalWindow : ChildWindow
    {
        public ProductTransferApprovalWindow()
        {
            InitializeComponent();
            InventoryTxt.Text = UserInfo.Inventory.InventoryName;
            Messenger.Default.Register<String>(this, OnKnocked);
        }

        void OnKnocked(String Message)
        {
            if (Message == "Close")
            {
                this.DialogResult = true;
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

