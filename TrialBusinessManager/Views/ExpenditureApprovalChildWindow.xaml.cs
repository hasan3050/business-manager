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
    public partial class ExpenditureApprovalChildWindow : ChildWindow
    {
        AgroDomainContext _context = new AgroDomainContext();
        long RegionId;
        Expenditure myExpenditure = new Expenditure();
        Expenditure updateExpenditure = new Expenditure();
        BusyWindow Busy = new BusyWindow();
        public ExpenditureApprovalChildWindow(Expenditure expenditure)
        {
            InitializeComponent();
          //  LoadExpenditureInfo()
            myExpenditure = expenditure;
            
            RegionId = expenditure.Region.RegionId;
            LoadExpenditureInfo();
            LoadEmployee();

            issueDatetextbox.Text = myExpenditure.DateOfIssue.ToString();
            regiontextBox.Text = myExpenditure.Region.RegionName;
            OKButton.IsEnabled = true;
            button1.IsEnabled = true;
        }
     
        void LoadExpenditureInfo()
        {
            
            _context.Load(_context.GetExpenditureInfoesQuery().Where(e => e.ExpenditureId ==myExpenditure.ExpenditureId)).Completed += new EventHandler(ExpenditureApprovalChildWindow_Completed);
            _context.Load(_context.GetExpendituresQuery().Where(e=>e.ExpenditureId==myExpenditure.ExpenditureId)).Completed+=new EventHandler(ExpenditureApprovalChildWindowCompleted);
        
        }
        void ExpenditureApprovalChildWindowCompleted(object sender, EventArgs e)
        {
            updateExpenditure = _context.Expenditures.Where(c => c.ExpenditureId == myExpenditure.ExpenditureId).First();

        }

        void ExpenditureApprovalChildWindow_Completed(object sender, EventArgs e)
        {
            dataGrid1.ItemsSource = _context.ExpenditureInfos;

        }

        void LoadEmployee()
        {

            _context.Load(_context.GetEmployeesQuery().Where(e=>e.Region.RegionId==RegionId&&e.Designation=="Sales Officer"));
       
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            Double totalAcceptedAmount = 0;

            for (int i = 0; i < _context.ExpenditureInfos.Count; i++)
            {
                totalAcceptedAmount += _context.ExpenditureInfos.ElementAt(i).AccountAcceptedAmount;
               
            }
           
            updateExpenditure.Status = "Approved by Accountant";
            myExpenditure.Status = "Approved by Accountant";
            updateExpenditure.TotalAcceptedAmount = totalAcceptedAmount;
            Busy.Show();
           _context.SubmitChanges().Completed+=new EventHandler(SubmitCompleted);
        }

      

        void SubmitCompleted(object sender, EventArgs e)
        {
            Busy.Close();
            this.DialogResult = true;

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

            this.DialogResult = false;

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            updateExpenditure.Status = "Rejected by Accountant";
            myExpenditure.Status = "Rejected by Accountant";
            _context.SubmitChanges().Completed += new EventHandler(SubmitCompleted);

        }
    }
}

