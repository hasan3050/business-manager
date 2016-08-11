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


namespace TrialBusinessManager
{
    public partial class About : Page
    {
        AgroDomainContext _context = new AgroDomainContext();
        public About()
        {
            InitializeComponent();
          //  dataGrid1.ItemsSource = _context.Dealers;
            _context.Load(_context.GetDealersQuery());
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
    }
}