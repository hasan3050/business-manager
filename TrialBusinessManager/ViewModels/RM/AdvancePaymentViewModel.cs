using System;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.ServiceModel.DomainServices.Client;
using TrialBusinessManager.Web;
using TrialBusinessManager.Models;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Messaging;
using System.ComponentModel.DataAnnotations;
using TrialBusinessManager.Views;

namespace TrialBusinessManager.ViewModels.RM
{
    public class AdvancePaymentViewModel : ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        BusyWindow Busy = new BusyWindow();
        Bill DummyBill = new Bill();
        Product DummyProduct = new Product();
        Dealer SelectedDealer = new Dealer();
        public ICommand ResetCommand { get; set; }
        public ICommand SubmitCommand { get; set; }

        public AdvancePaymentViewModel()
        {
            SelectedDealer = StaticMessaging.DealerMessage;
            LoadBillPayment();
            LoadDefaultProduct();
            ResetCommand = new RelayCommand(Reset);
            SubmitCommand = new RelayCommand(Submit);
        }
        
        private void LoadBillPayment()
        {
            _context.Load(_context.GetBillsQuery().Where(e => e.BillCode == "Unknown" && e.DealerId == SelectedDealer.DealerId)).Completed += (s, e) =>
            {
                DummyBill = _context.Bills.Where(r => r.BillCode == "Unknown" && r.DealerId == SelectedDealer.DealerId).SingleOrDefault();
            };

        }

        private void LoadDefaultProduct()
        {
            _context.Load(_context.GetProductsQuery().Where(e => e.GroupName == "Default")).Completed += (s, e) =>
            {
                DummyProduct = _context.Products.Where(r => r.GroupName == "Default").SingleOrDefault();
            };
                
        }
        private void Reset()
        {
            NewBillPayment = new BillPayment();
            PaymentMethodBool = true;
            SelectedPaymentMethod = "";
            MoneyReciept = "";
            Amount = 0;
        }

        private void Submit()
        {
            
            if (HasValidationError())
                return;
            GenerateNewBillPayment();
           _context.BillPayments.Add(NewBillPayment);
                Busy.Show();
                _context.SubmitChanges(OnSubmitCompleted, null);
           
           
        }

        private bool HasValidationError()
        {
          /*  if ((NewBillPayment.ValidationErrors != null) && (NewBillPayment.ValidationErrors.Count > 0))
            {
                string error = string.Empty;
                foreach (ValidationResult result in NewBillPayment.ValidationErrors)
                {
                    error += string.Format("{0} has problem {1}\n", result.MemberNames.First(), result.ErrorMessage);
                }

                MessageBox.Show(error, "Error", MessageBoxButton.OK);
                return true;
            }
            */
            if (string.IsNullOrEmpty(SelectedPaymentMethod))
            {
                MessageBox.Show("Select a payment Method first!");
                return true;
            }
          
            if (SelectedPaymentMethod == "DD" && (string.IsNullOrEmpty(NewBillPayment.BankName) || string.IsNullOrEmpty(NewBillPayment.BranchName) || string.IsNullOrEmpty(NewBillPayment.DDNumber)))
            {
                MessageBox.Show("Insert DD Info Properly!");
                return true;
            }

            else if (SelectedPaymentMethod == "Cash" && string.IsNullOrEmpty(MoneyReciept))
            {
                MessageBox.Show("Insert Money Reciept Properly!");
                return true;
            }

            if (NewBillPayment.DateOfDeposit == null)
            {
                MessageBox.Show("Insert Date of Deposit!");
                return true;
            }

            return false;
        }

        private void OnSubmitCompleted(SubmitOperation op)
        {
            if (op.HasError == true)
            {
                
                return;
            }

            MessageBox.Show("The advanced deposit is successfully issued. Please close this window to complete.");
            Busy.Close();
            Reset();
        }

        private void GenerateNewBillPayment()
        {
            NewBillPayment.BillPaymentCode = DateTime.Now.ToString();     //need to be changed by a trigger in server side
            NewBillPayment.Status = "Issued by RM for Advance";
            NewBillPayment.PaymentMethod = SelectedPaymentMethod;
            NewBillPayment.DealerId = SelectedDealer.DealerId;
            NewBillPayment.DateOfIssue = DateTime.Now;
            NewBillPayment.RMId = UserInfo.EmployeeID;
            NewBillPayment.TotalAmount = _amount;
            NewBillPayment.AccountantId = 0;
            NewBillPayment.AccountantFinalizeDate = new DateTime(2099, 12, 30);
            NewBillPayment.MoneyReciept = MoneyReciept;

            BillPaymentInfo temp = new BillPaymentInfo();
            temp.BillId = DummyBill.BillId;
            temp.ProductId = DummyProduct.ProductId;//DummyProduct.ProductId;
            temp.Amount = _amount;
            temp.HasRejected = false;

            NewBillPayment.BillPaymentInfoes.Add(temp);
        
        
        }

        public const string NewBillPaymentPropertyName = "NewBillPayment";
        private BillPayment _newBillPayment = new BillPayment();
        public BillPayment NewBillPayment
        {
            get { return _newBillPayment; }
            set
            {
                if (_newBillPayment == value) { return; }

                _newBillPayment = value;
                RaisePropertyChanged(NewBillPaymentPropertyName);
            }
        }
        public const string PaymentMethodBoolPropertyName = "PaymentMethodBool";
        private bool _paymentMethodBool = true;
        public bool PaymentMethodBool
        {
            get { return _paymentMethodBool; }
            set
            {
                if (_paymentMethodBool == value) { return; }

                _paymentMethodBool = value;
                RaisePropertyChanged(PaymentMethodBoolPropertyName);
            }
        }

        public const string SelectedPaymentMethodPropertyName = "SelectedPaymentMethod";
        private string _selectedPaymentMethod;
        public string SelectedPaymentMethod
        {
            get { return _selectedPaymentMethod; }
            set
            {
                if (_selectedPaymentMethod == value) { return; }

                _selectedPaymentMethod = value;
                if (SelectedPaymentMethod != null && SelectedPaymentMethod == "Cash")
                {
                    NewBillPayment.BankName = "N/A";
                    NewBillPayment.BranchName = "N/A";
                    NewBillPayment.DDNumber = "N/A";
                    PaymentMethodBool = false;
                }
                else
                    PaymentMethodBool = true;

                RaisePropertyChanged(SelectedPaymentMethodPropertyName);
            }
        }

        public const string MoneyRecieptPropertyName = "MoneyReciept";
        private string _moneyReciept;
        public string MoneyReciept
        {
            get { return _moneyReciept; }
            set
            {
                if (_moneyReciept == value) { return; }

                _moneyReciept = value;
                RaisePropertyChanged(MoneyRecieptPropertyName);
            }
        }

        public const string AmountPropertyName = "Amount";
        private double _amount;
        public double Amount
        {
            get { return _amount; }
            set
            {
                if (_amount == value) { return; }

                if (_amount < 0)
                {
                    MessageBox.Show("Amount can not be negative");
                    _amount = 0;
                    return;
                
                }
                _amount = value;
                RaisePropertyChanged(AmountPropertyName);
            }
        }


        public const string PaymentMethodPropertyName = "PaymentMethod";
        private ObservableCollection<string> _paymentMethod = new ObservableCollection<string>() { "Cash","DD"};
        public ObservableCollection<string> PaymentMethod
        {
            get { return _paymentMethod; }

            set
            {
                if (_paymentMethod == value) { return; }

                _paymentMethod = value;
                RaisePropertyChanged(PaymentMethodPropertyName);
            }
        }
    }
}
