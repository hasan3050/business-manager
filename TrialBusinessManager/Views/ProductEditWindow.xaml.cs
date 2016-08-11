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

namespace TrialBusinessManager.Views
{
    public partial class ProductEditWindow : ChildWindow
    {
        public ProductEditWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<String>(this, OnMessageReceived);
        }

        void OnMessageReceived(String X)
        {
            if (X == "Updated")
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

        private void OKBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Send<String>("Rejected");
            this.DialogResult = true;
        }
    }
}

