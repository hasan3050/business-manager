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

namespace TrialBusinessManager.Views.NSM
{
    public partial class CreateProduct : Page
    {
        public CreateProduct()
        {
            InitializeComponent();
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void ToggleTypeBtn_Click(object sender, RoutedEventArgs e)
        {
            
            if (ToggleTypeBtn.Content.Equals("Create"))
            {
                ToggleTypeBtn.Content = "Select";
                TypeCombo.Visibility = Visibility.Collapsed;
                TypeCombo.SelectedItem = null;
                TypeCreateTxt.Visibility = Visibility.Visible;
                
            }
            else
            {
                ToggleTypeBtn.Content = "Create";
                TypeCombo.Visibility = Visibility.Visible;
                TypeCreateTxt.Visibility = Visibility.Collapsed;
                TypeCreateTxt.Text = "";
            }
        }

        private void ToggleWingBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ToggleWingBtn.Content.Equals("Create"))
            {
                ToggleWingBtn.Content = "Select";
                WingCombo.Visibility = Visibility.Collapsed;
                WingCombo.SelectedItem = null;
                WingCreateTxt.Visibility = Visibility.Visible;
            }
            else
            {
                ToggleWingBtn.Content = "Create";
                WingCombo.Visibility = Visibility.Visible;
                WingCreateTxt.Visibility = Visibility.Collapsed;
                WingCreateTxt.Text = "";
            }
        }

    }
}
