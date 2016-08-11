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
using GalaSoft.MvvmLight.Messaging;
using TrialBusinessManager.Views;

namespace TrialBusinessManager.Views.NSM
{
    public partial class ViewIndentForForwardingByNSM : Page
    {
        public ViewIndentForForwardingByNSM()
        {
            InitializeComponent();
            Messenger.Default.Register<string>(this, OnMessageReceived);
        }

        void OnMessageReceived(string message)
        {
            this.NavigationService.Navigate(new Uri("/NSM/ForwardIndentNSM", UriKind.Relative));
        }
        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

    }
}
