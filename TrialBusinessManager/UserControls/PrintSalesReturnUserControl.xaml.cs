using System;
using System.Net;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight;
using TrialBusinessManager.Web;
using GalaSoft.MvvmLight.Command;
using System.ServiceModel.DomainServices.Client;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using TrialBusinessManager.Models;
using TrialBusinessManager.Views;


namespace TrialBusinessManager.UserControls
{
    public partial class PrintSalesReturnUserControl : UserControl
    {
        AgroDomainContext _context = new AgroDomainContext();
        SalesReturn PrintSR = new SalesReturn();
        ObservableCollection<SalesReturnInfo> SalesReturnInfoCollection = new ObservableCollection<SalesReturnInfo>();
       
        public PrintSalesReturnUserControl(AgroDomainContext SentContext,SalesReturn SentSR)
        {
            InitializeComponent();
            _context = SentContext;
            PrintSR = SentSR;
            ShowSalesReturnInfo();
            ShowSalesReturnProducts();
        }

        void ShowSalesReturnInfo()
        {
            SRCodetextBox.Text = PrintSR.SalesReturnCode;
            DealerNametextBox.Text = PrintSR.Dealer.Name;
            DealerAddtextBox.Text = PrintSR.Dealer.CompanyAddress;
            RegionNametextBox.Text = PrintSR.Employee.Region.RegionName;
        }

        void ShowSalesReturnProducts()
        {
          
            dataGrid1.ItemsSource = _context.SalesReturnInfos.Where(e => e.SalesReturnId == PrintSR.SalesReturnId);
        }
    }
}
