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
using TrialBusinessManager.ViewModels;
using GalaSoft.MvvmLight.Messaging;
using TrialBusinessManager.ViewModels.NSM;

namespace TrialBusinessManager.Views
{
    public partial class CreateInventoryView : ChildWindow
    {
        public CreateInventoryView()
        {
            InitializeComponent();
            Messenger.Default.Register<String>(this, (s) =>
            {
                Messenger.Default.Send<string, ViewRegionsViewModel>("Message");
                this.DialogResult = true;
            });
            RegiontextBox.Text = InventoryMesseging.SentRegion.RegionName;
        }

       /* private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }*/

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

