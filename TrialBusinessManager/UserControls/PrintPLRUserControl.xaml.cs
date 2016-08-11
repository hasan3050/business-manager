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
    public partial class PrintPLRUserControl : UserControl
    {
        AgroDomainContext _context = new AgroDomainContext();
        PLR PrintPLR = new PLR();

        ObservableCollection<PLRProductInfo> PLRProducts = new ObservableCollection<PLRProductInfo>();
        ObservableCollection<PLRPackageInfo> PLRPackages = new ObservableCollection<PLRPackageInfo>();

        public PrintPLRUserControl(AgroDomainContext SentContext,PLR SentPLR)
        {
            InitializeComponent();

            _context = SentContext;
            PrintPLR = SentPLR;

            ShowPLRInfo();
            ShowPLRProducts();
            ShowPLRPackages();
        }

        void ShowPLRInfo()
        {
            IssueDatetextBox.Text = PrintPLR.DateOfIssue.ToString();
            VerifyDateTextBox.Text = PrintPLR.DateOfApproval.ToString();
            PLRCodetextBox.Text = PrintPLR.PLRCode;
            IssuedByTxtBox.Text = PrintPLR.Employee.Person.Name;
            DesignationtextBox.Text = PrintPLR.Employee.Designation;
        }

        void ShowPLRProducts()
        {
           
            dataGrid1.ItemsSource = _context.PLRProductInfos.Where(e => e.PLRId == PrintPLR.PLRId);
        }

        void ShowPLRPackages()
        {
           
            dataGrid2.ItemsSource = _context.PLRPackageInfos.Where(e => e.PLRId == PrintPLR.PLRId);
        }

       
    }
}
