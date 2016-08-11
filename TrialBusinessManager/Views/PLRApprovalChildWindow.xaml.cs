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

namespace TrialBusinessManager.ViewModels
{
    public partial class PLRApprovalChildWindow : ChildWindow
    {
        AgroDomainContext _context = new AgroDomainContext();
        long plrID;
        PLR selectedPLR = new PLR();
        public PLRApprovalChildWindow(PLR SentPLR)
        {
            InitializeComponent();
            selectedPLR = SentPLR;

            plrID = SentPLR.PLRId;
            ShowPLRData();
            LoadData();
        }

        void LoadData()
        {
            
            _context.Load(_context.GetPLRProductInfoesQuery().Where(e => e.PLRId == selectedPLR.PLRId)).Completed += (s, arg) =>
            {
                dataGrid1.ItemsSource = _context.PLRProductInfos.Where(e => e.PLRId == plrID);
            };

            _context.Load(_context.GetPLRPackageInfoesQuery().Where(e => e.PLRId == plrID)).Completed += (s, arg) =>
            {
                dataGrid2.ItemsSource = _context.PLRPackageInfos.Where(e => e.PLRId == plrID);
            };

            _context.Load(_context.GetPLRsQuery().Where(e => e.PLRId == plrID)).Completed += (s, arg) =>
            {
                selectedPLR = _context.PLRs.Where(e => e.PLRId == plrID).First();

            };

        }
        void ShowPLRData()
        {

           IssueDatetextBox.Text = selectedPLR.DateOfIssue.ToString();
           VerifyDateTextBox.Text = selectedPLR.DateOfApproval.ToString();
           StatustextBox.Text = selectedPLR.Status;
            textBox2.Text = selectedPLR.Employee.Person.Name;
          
            textBox4.Text = selectedPLR.Employee.Designation;
        }


        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

