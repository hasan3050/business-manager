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
using TrialBusinessManager.ViewModels.NSM;

namespace TrialBusinessManager.Views
{
    public partial class CreateRegionChildWindow : ChildWindow
    {
        public CreateRegionChildWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<String>(this, (s) =>
            {
                Messenger.Default.Send<string, ViewRegionsViewModel>("Message");
                this.DialogResult = true;
            });
            this.DataContext = new CreateRegionViewModel();
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

