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
using TrialBusinessManager.Web;

namespace TrialBusinessManager.Views
{
    public partial class DealerDetailWindow : ChildWindow
    {
        Dealer Dealer = new Dealer();
        AgroDomainContext _context = new AgroDomainContext();
       
        public DealerDetailWindow(Dealer MyDealer,AgroDomainContext Context)
        {
            InitializeComponent();
            Dealer = MyDealer;
            _context = Context;
            LayoutRoot.DataContext = Dealer;
            SalesOfficerTxt.Text = _context.Employees.Where(c => c.EmployeeId.Equals(Dealer.SalesOfficerId)).Single().Person.Name;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}

