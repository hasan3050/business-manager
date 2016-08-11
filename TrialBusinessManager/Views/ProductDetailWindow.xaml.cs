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
using TrialBusinessManager.Web;
using TrialBusinessManager.ViewModels;

namespace TrialBusinessManager.Views
{
    public partial class ProductDetailWindow : ChildWindow
    {
        public ProductDetailWindow()
        {
            InitializeComponent(); 
            //this.DataContext = new ProductDetailViewModel();

            
        }

        private void OKBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        
    }
}

