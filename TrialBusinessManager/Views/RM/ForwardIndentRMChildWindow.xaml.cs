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
using TrialBusinessManager.ViewModels.RM;
using GalaSoft.MvvmLight.Messaging;

namespace TrialBusinessManager.Views.RM
{
    public partial class ForwardIndentRMChildWindow : ChildWindow
    {
        public ForwardIndentRMChildWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<String>(this, OnMessageReceived);
            this.DataContext =new ForwardIndentViewModel();
        }

        void OnMessageReceived(String p)
        {
            this.DialogResult = true;
        }


        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        public void CloseChildWindow()
        {
            this.DialogResult = true;
        }
    }
}

