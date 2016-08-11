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
using GalaSoft.MvvmLight.Messaging;
using TrialBusinessManager.Views;

namespace TrialBusinessManager.ViewModels.Accountant
{
    public class VerifyBillPaymentDetailViewModel : ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        BusyWindow Busy = new BusyWindow();
        public ICommand AcceptCommand { get; set; }
        public ICommand RejectCommand { get; set; }

        public VerifyBillPaymentDetailViewModel()
        {
            SelectedBillPayment = StaticMessaging.BillPaymentMessage;
            AcceptCommand = new RelayCommand(Accept);
            RejectCommand = new RelayCommand(Reject);
            ErrorMessage = "";
            LoadBillPaymentInfo();
        }

        #region Method
        
        private void OnSubmitCompleted(SubmitOperation op)
        {
            Busy.Close();
            ButtonEnableBool = true;
            if (op.HasError == true)
            {
                _context.RejectChanges();
                return;
            }
            MessageBox.Show("Submitted!");
            StaticMessaging.AccountantBillPaymentSubmitted = true;
            ButtonEnableBool = false;
            ErrorMessage = "The Bill Payment is successfully verified. Please close this window to continue.";
        }

        private void Reject()
        {
            Busy.Show();
            ButtonEnableBool = false;

            BillPayment billPayment = _context.BillPayments.Where(e => e.BillPaymentId == SelectedBillPayment.BillPaymentId).SingleOrDefault();
            if (billPayment.Status == "Issued by RM for Advance")   billPayment.Status = "Advance Rejected by Accountant";
            else if (billPayment.Status == "Issued by RM")          billPayment.Status = "Rejected by Accountant";
            billPayment.AccountantId = UserInfo.EmployeeID;
            billPayment.AccountantFinalizeDate = DateTime.Now;
     
            foreach (BillPaymentInfo x in _context.BillPaymentInfos)
            {
                x.HasRejected = true;
            }
           
            _context.SubmitChanges(OnSubmitCompleted, null);
        }

        private void Accept()
        {
            Busy.Show();   
            ButtonEnableBool = false;

            BillPayment billPayment = _context.BillPayments.Where(e => e.BillPaymentId == SelectedBillPayment.BillPaymentId).SingleOrDefault();
            
            billPayment.AccountantId = UserInfo.EmployeeID;
            billPayment.AccountantFinalizeDate = DateTime.Now;
            billPayment.Status = "Approved by Accountant";
      
            _context.SubmitChanges(OnSubmitCompleted, null);
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

        public const string SelectedBillPaymentPropertyName = "SelectedBillPayment";
        private BillPayment _selectedBillPayment = StaticMessaging.BillPaymentMessage;
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
        private double _totalAmount = 0;
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

        public const string ErrorMessagePropertyName = "ErrorMessage";
        private string _ErrorMessage;
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set
            {
                if (_ErrorMessage == value) { return; }

                _ErrorMessage = value;
                RaisePropertyChanged(ErrorMessagePropertyName);
            }
        }

        public const string ButtonEnableBoolPropertyName = "ButtonEnableBool";
        private bool _ButtonEnableBool = true;
        public bool ButtonEnableBool
        {
            get { return _ButtonEnableBool; }
            set
            {
                if (_ButtonEnableBool == value) { return; }

                _ButtonEnableBool = value;
                RaisePropertyChanged(ButtonEnableBoolPropertyName);
            }
        }
        #endregion

    }
}
