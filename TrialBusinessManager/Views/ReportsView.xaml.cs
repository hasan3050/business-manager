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
using System.Windows.Browser;

namespace TrialBusinessManager
{
    public partial class ReportsView : Page
    {
        public ReportsView()
        {
            InitializeComponent();
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void reportButtonClicked(object sender, System.Windows.RoutedEventArgs e)
        {
                HtmlPopupWindowOptions options = new HtmlPopupWindowOptions();
                options.Resizeable = true;
                options.Left = 0;
                options.Top = 0;
                options.Width = 930;
                options.Height = 800;
                options.Menubar = false;
                options.Toolbar = false;
                options.Directories = false;
                options.Status = false;

                Button btn = sender as Button;


                string address = Application.Current.Host.Source.AbsoluteUri;
                int i = address.IndexOf("/ClientBin/", 1);
                string url = address.Substring(0, i);
                url = url + "/Report/ReportViewers/LedgerOnProductReportViewer.aspx";

                if (true == HtmlPage.IsPopupWindowAllowed)
                    HtmlPage.PopupWindow(new Uri(url), "new", options);

               
          
        }

        private void Inventory_Click(object sender, RoutedEventArgs e)
        {

            HtmlPopupWindowOptions options = new HtmlPopupWindowOptions();
            options.Resizeable = true;
            options.Left = 0;
            options.Top = 0;
            options.Width = 930;
            options.Height = 800;
            options.Menubar = false;
            options.Toolbar = false;
            options.Directories = false;
            options.Status = false;

            Button btn = sender as Button;


            string address = Application.Current.Host.Source.AbsoluteUri;
            int i = address.IndexOf("/ClientBin/", 1);
            string url = address.Substring(0, i);
            url = url + "/Report/ReportViewers/StockSummaryReportViewer.aspx";

            if (true == HtmlPage.IsPopupWindowAllowed)
                HtmlPage.PopupWindow(new Uri(url), "new", options);
        }

        private void InventoryLog_Click(object sender, RoutedEventArgs e)
        {

            HtmlPopupWindowOptions options = new HtmlPopupWindowOptions();
            options.Resizeable = true;
            options.Left = 0;
            options.Top = 0;
            options.Width = 930;
            options.Height = 800;
            options.Menubar = false;
            options.Toolbar = false;
            options.Directories = false;
            options.Status = false;

            Button btn = sender as Button;


            string address = Application.Current.Host.Source.AbsoluteUri;
            int i = address.IndexOf("/ClientBin/", 1);
            string url = address.Substring(0, i);
            url = url + "/Report/ReportViewers/DealerCollectionReportViewer.aspx";

            if (true == HtmlPage.IsPopupWindowAllowed)
                HtmlPage.PopupWindow(new Uri(url), "new", options);
             
        }

        private void Achievement_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Report/AchievementDashboard.xaml", UriKind.Relative));
        }
    }
}
