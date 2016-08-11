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


namespace TrialBusinessManager.Views
{
    public partial class RequisitionApprovalChildWindow : ChildWindow
    {
        long RequisitionId;
        AgroDomainContext _context = new AgroDomainContext();
        Requisition approvedRequisition = new Requisition();
        ObservableCollection<RequisitionProductInfo> ProductList = new ObservableCollection<RequisitionProductInfo>();
        ObservableCollection<RequisitionPackageInfo> PackageList = new ObservableCollection<RequisitionPackageInfo>();

        public RequisitionApprovalChildWindow(long id)
        {
            InitializeComponent();
            InitializeComponent();
            RequisitionId = id;

            // busyIndicator1.IsBusy = true;

            _context.Load(_context.GetRequisitionsQuery()).Completed += new EventHandler(RequisitionGot);
            _context.Load(_context.GetRequisitionProductInfoesQuery().Where(e => e.RequisitionId == id)).Completed += new EventHandler(RequisitionProductGot);
            _context.Load(_context.GetRequisitionPackageInfoesQuery().Where(e => e.RequisitionId == id)).Completed += new EventHandler(RequisitionPackageGot);
          

    
        }
        void RequisitionGot(object sender,EventArgs e)
        {
            approvedRequisition = _context.Requisitions.Where(r=>r.RequisitionId==RequisitionId).First();
            typeTextBox.Text= approvedRequisition.RequisitionType;
            RCodetextBox.Text= approvedRequisition.RequisitionCode;
            ADateTextBox.Text = approvedRequisition.DateOfApproval.ToString();
    
        }
        void RequisitionProductGot(object sender, EventArgs e)
        {
            foreach (RequisitionProductInfo x in _context.RequisitionProductInfos)
            {
                ProductList.Add(x);

            }
            dataGrid1.ItemsSource = ProductList;

        }

        void RequisitionPackageGot(object sender, EventArgs e)
        {
            foreach (RequisitionPackageInfo x in _context.RequisitionPackageInfos)
            {
                PackageList.Add(x);
            }
            dataGrid2.ItemsSource = PackageList;

        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            PrintFactory.PrintRequisition(_context, approvedRequisition);
        }
    }
}

