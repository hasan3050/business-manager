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
using TrialBusinessManager.ViewModels;
using GalaSoft.MvvmLight.Messaging;

namespace TrialBusinessManager.Views
{
    public partial class RequisitionVerification : ChildWindow
    {
        RequisitionVerificationViewModel obj = new RequisitionVerificationViewModel();
        public RequisitionVerification()
        {
            InitializeComponent();
            Messenger.Default.Register<String>(this, (s) => { this.DialogResult = true; });
            this.DataContext = obj;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            obj.Add();
            

        }

       
    }
}

