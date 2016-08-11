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
using TrialBusinessManager.Web;
using System.ServiceModel.DomainServices.Client;
using System.Xml;
using TrialBusinessManager.ViewModels;
using TrialBusinessManager.Models;

namespace TrialBusinessManager.Views
{
    public partial class ViewDealerWarningView : Page
    {
        public ViewDealerWarningView()
        {
            InitializeComponent();
            this.DataContext = new ViewDealerWarningViewModel();
            
            if (UserInfo.Designation == "Regional Manager")
            {

                RegionCombo.Visibility = Visibility.Collapsed;
                RegionFilterBtn.Visibility = Visibility.Collapsed;
                RegionTxt.Visibility = Visibility.Collapsed;
            }
            if (UserInfo.Designation == "Admin" || UserInfo.Designation == "Co-ordinator")
            {
                EditBtn.Visibility = Visibility.Visible;
                //   AdvaneButton.Visibility = Visibility.Visible;
            }
            if (UserInfo.Designation == "Accountant")
            {
                //EditBtn.Visibility = Visibility.Visible;
                CommissionBtn.Visibility = Visibility.Visible;
            }
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void DealerAuto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Dealer MyDealer = new Dealer();
                DealerList.UpdateLayout();
                DealerList.ScrollIntoView(DealerAuto.SelectedItem);
                DealerList.SelectedItem = DealerAuto.SelectedItem;
            }
            catch (Exception ex)
            {
            }
        }

    }
}
