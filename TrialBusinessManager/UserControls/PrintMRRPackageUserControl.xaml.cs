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
    public partial class PrintMRRPackageUserControl : UserControl
    {
        AgroDomainContext _context = new AgroDomainContext();
        MRR PrintMRR = new MRR();
        ObservableCollection<MRRPackageInfo> MRRPackages = new ObservableCollection<MRRPackageInfo>();

        public PrintMRRPackageUserControl(AgroDomainContext SentContext, MRR SentMRR)
        {
            InitializeComponent();
            _context = SentContext;
            PrintMRR = SentMRR;
            UpdateMRRInfoUI();
            UpdateMRRPackages();
        }
        void UpdateMRRInfoUI()
        {
            PurchaseOrdertextBox.Text = PrintMRR.PurchaseOrderNo;
            MRRCodeTxtBox.Text = PrintMRR.MRRCode;
            ChalanNOtextBox.Text = PrintMRR.ChallanNo;
            DatetextBox.Text = PrintMRR.DateOfIssue.ToString();
            RetailerNametextBox.Text = PrintMRR.RetailerName;
        }

        void UpdateMRRPackages()
        {

            dataGrid2.ItemsSource = _context.MRRPackageInfos.Where(e => e.MRRId == PrintMRR.MRRId);
        }
    }
}
