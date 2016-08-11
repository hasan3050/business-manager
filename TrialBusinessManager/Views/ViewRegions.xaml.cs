using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using TrialBusinessManager.Web;
using System.ServiceModel.DomainServices.Client;
using System.Collections.ObjectModel;
using TrialBusinessManager.Models;

namespace TrialBusinessManager.Views
{
    
    
    public partial class ViewRegions : Page
    {        
        public ViewRegions()
        {
            InitializeComponent();

            if (UserInfo.Designation == "Admin")
            {
                createinvbtn.Visibility = Visibility.Visible;
                createRegionbtn.Visibility = Visibility.Visible;
                editbtn.Visibility = Visibility.Visible;
                //editRegionbtn.Visibility = Visibility.Collapsed;
            }
            else
            {
                createinvbtn.Visibility = Visibility.Collapsed;
                createRegionbtn.Visibility = Visibility.Collapsed;
                editbtn.Visibility = Visibility.Collapsed;
                //editRegionbtn.Visibility = Visibility.Visible;
            }

            
           
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        
               
    }
}
