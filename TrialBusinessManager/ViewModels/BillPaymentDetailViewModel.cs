using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TrialBusinessManager.Web;
using TrialBusinessManager.Models;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.ServiceModel.DomainServices.Client;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;

namespace TrialBusinessManager.ViewModels
{
    public class BillPaymentDetailViewModel:ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();

        public BillPaymentDetailViewModel()
        {
            SelectedBillPayment = StaticMessaging.BillPaymentMessage;
            Message = string.Empty;

            if (SelectedBillPayment.AccountantId != 0)    LoadEmployee();
            LoadBillPaymentInfo();
        }

        #region Method
        
        private void LoadEmployee()
        {
            _context.Load(_context.GetEmployeesQuery().Where(e => e.EmployeeId == SelectedBillPayment.AccountantId)).Completed += (s, e) =>
                {
                    Message = "Bill Payment has been " + (SelectedBillPayment.Status == "Approved" ? "Approved " : "Rejected ") + "by " + _context.Employees.Where(o => o.EmployeeId == SelectedBillPayment.AccountantId).SingleOrDefault().Person.Name + " at " + SelectedBillPayment.AccountantFinalizeDate.ToString();
                };
        }

        private void LoadBillPaymentInfo()
        {
            _context.Load(_context.GetBillPaymentInfoDetailsQuery().Where(e => e.BillPaymentId == SelectedBillPayment.BillPaymentId)).Completed += (s, e) =>
                {
                    BindBillPaymentInfo = new ObservableCollection<BillPaymentInfo>(_context.BillPaymentInfos.Where(o => o.BillPaymentId == SelectedBillPayment.BillPaymentId));
                    TotalAmount = BindBillPaymentInfo.Sum(o => o.Amount);
                };
        }

        #endregion

        #region Properties
        private string _Message;
        public string Message
        {
            get { return _Message; }
            set { if (value != _Message)_Message = value; RaisePropertyChanged("Message"); }
        }

        public const string SelectedBillPaymentPropertyName = "SelectedBillPayment";
        private BillPayment _selectedBillPayment=StaticMessaging.BillPaymentMessage;
        public BillPayment SelectedBillPayment
        {
            get { return _selectedBillPayment; }
            set
            {
                if (_selectedBillPayment == value) { return; }

                _selectedBillPayment = value;
                RaisePropertyChanged(SelectedBillPaymentPropertyName);
            }
        }

        public const string BindBillPaymentInfoPropertyName = "BindBillPaymentInfo";
        private ObservableCollection<BillPaymentInfo> _bindBillPaymentInfo = new ObservableCollection<BillPaymentInfo>();
        public ObservableCollection<BillPaymentInfo> BindBillPaymentInfo
        {
            get { return _bindBillPaymentInfo; }
            set
            {
                if (_bindBillPaymentInfo == value) { return; }

                _bindBillPaymentInfo = value;
                RaisePropertyChanged(BindBillPaymentInfoPropertyName);
            }
        }
  
        public const string TotalAmountPropertyName = "TotalAmount";
        private double _totalAmount =0;
        public double TotalAmount
        {
            get { return _totalAmount; }
            set
            {
                if (_totalAmount == value) { return; }

                _totalAmount = value;
                RaisePropertyChanged(TotalAmountPropertyName);
            }
        }
        #endregion

    }
}
