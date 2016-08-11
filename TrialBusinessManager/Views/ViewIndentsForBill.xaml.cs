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
using System.Windows.Navigation;
using TrialBusinessManager.ViewModels;
using GalaSoft.MvvmLight.Messaging;

namespace TrialBusinessManager.Views
{
    public partial class ViewIndentsForBill : Page
    {
        public ViewIndentsForBill()
        {
            InitializeComponent();
            Messenger.Default.Register<String>(this, Navigate);
            this.DataContext = new IndentsForBillViewModel();
        }

        void Navigate(String message)
        {
            
            this.NavigationService.Navigate(new Uri("GenerateBill", UriKind.Relative));
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

    }
}
