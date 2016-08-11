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
    public partial class PrintRequisitionUserControl : UserControl
    {
        AgroDomainContext _context = new AgroDomainContext();
        Requisition PrintRequisition = new Requisition();

        ObservableCollection<RequisitionProductInfo> ProductList = new ObservableCollection<RequisitionProductInfo>();
        ObservableCollection<RequisitionPackageInfo> PackageList = new ObservableCollection<RequisitionPackageInfo>();

        public PrintRequisitionUserControl(AgroDomainContext SentContext,Requisition SentRequisition)
        {
            InitializeComponent();
            _context = SentContext;
            PrintRequisition = SentRequisition;
            RequisitionGot();
            RequisitionProductGot();
            RequisitionPackageGot();
        }

        void RequisitionGot()
        {
           
            typeTextBox.Text = PrintRequisition.RequisitionType;
            RCodetextBox.Text = PrintRequisition.RequisitionCode;
            ADateTextBox.Text = PrintRequisition.DateOfApproval.ToString();

        }
        void RequisitionProductGot()
        {
           
            dataGrid1.ItemsSource = _context.RequisitionProductInfos.Where(e => e.RequisitionId == PrintRequisition.RequisitionId);

        }

        void RequisitionPackageGot()
        {
          
            dataGrid2.ItemsSource = _context.RequisitionPackageInfos.Where(e => e.RequisitionId == PrintRequisition.RequisitionId);

        }

    }
}
