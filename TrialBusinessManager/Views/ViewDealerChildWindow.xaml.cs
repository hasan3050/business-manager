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
using TrialBusinessManager.Web;

namespace TrialBusinessManager.Views
{
    public partial class ViewDealerChildWindow : ChildWindow
    {
        public ViewDealerChildWindow()
        {
            InitializeComponent();
            this.DataContext = new ViewDealerViewModel();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void autoCompleteBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
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

