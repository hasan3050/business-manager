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
using TrialBusinessManager.Models;

namespace TrialBusinessManager.Views
{
    public partial class ViewPackage : Page
    {
        public ViewPackage()
        {
            InitializeComponent();

            if (UserInfo.Designation == "Admin")
            {
                CreatePackageButton.Visibility = Visibility.Visible;
                button2.Visibility = Visibility.Visible;
            }
            else
            {
                CreatePackageButton.Visibility = Visibility.Collapsed;
                button2.Visibility = Visibility.Collapsed;
            }
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            CreatePackageChildWindow obj = new CreatePackageChildWindow();
            obj.Show();
        }

    }
}
