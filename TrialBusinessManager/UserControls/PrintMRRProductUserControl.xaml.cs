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
    public partial class PrintMRRProductUserControl : UserControl
    {
        AgroDomainContext _context = new AgroDomainContext();
        MRR PrintMRR = new MRR();
        ObservableCollection<MRRProductInfo> MRRProducts = new ObservableCollection<MRRProductInfo>();

        public PrintMRRProductUserControl(AgroDomainContext SentContext,MRR SentMRR)
        {
            InitializeComponent();
            _context = SentContext;
            PrintMRR = SentMRR;
            UpdateMRRInfoUI();
            UpdateMRRProducts();
        }

        void UpdateMRRInfoUI()
        {
            PurchaseOrdertextBox.Text = PrintMRR.PurchaseOrderNo;
            MRRCodeTxtBox.Text = PrintMRR.MRRCode;
            ChalanNOtextBox.Text = PrintMRR.ChallanNo;
            DatetextBox.Text = PrintMRR.DateOfIssue.ToString();
            RetailerNametextBox.Text = PrintMRR.RetailerName;
        }

        void UpdateMRRProducts()
        {

            dataGrid1.ItemsSource = _context.MRRProductInfos.Where(e => e.MRRId == PrintMRR.MRRId);
        }
    }
}
