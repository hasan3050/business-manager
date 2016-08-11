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
using TrialBusinessManager.Web;

namespace TrialBusinessManager.Views.RM
{
    public partial class IssueSalesReturn : Page
    {
        public IssueSalesReturn()
        {

            InitializeComponent();
            this.DataContext = new ViewDealerViewModel();

        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        private void NameAuto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Dealer MyDealer = new Dealer();
                DealerList.UpdateLayout();
                DealerList.ScrollIntoView(NameAuto.SelectedItem);
                DealerList.SelectedItem = NameAuto.SelectedItem;
            }
            catch (Exception ex)
            {
            }
        }

    }
}
