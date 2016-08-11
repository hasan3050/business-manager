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
using TrialBusinessManager.Views.Accountant;

namespace TrialBusinessManager.ViewModels.RM
{
    public class BillPaymentViewModel:ViewModelBase
    {
        AgroDomainContext _context=new AgroDomainContext();
        BusyWindow Busy = new BusyWindow();
        public List<DueInfo> DueCollection { get {return _context.DueInfos.Where(e=>e.DealerId==SelectedDealer.DealerId).ToList();} }
        public List<Bill> BillCollection { get { return _context.Bills.Where(e => e.DealerId == SelectedDealer.DealerId).ToList(); } }
        public EntitySet<Product> ProductCollection { get { return _context.Products; } }
        private List<BillProductDetail> BillProductCollection;
        
        public ICommand SearchCommand { get; set; }
        public ICommand AddItemCommand { get; set; }
        public ICommand SelectionChangeCommand { get; set; }
        public ICommand DeleteItemCommand { get; set; }
        public ICommand ResetCommand { get; set; }
        public ICommand SubmitCommand { get; set; }
        public ICommand CellEditCommand { get; set; }
        public ICommand ChooseDealerCommand { get; set; }
        public ICommand Advanced { get; set; }

        public BillPaymentViewModel()
        {
            Messenger.Default.Register<Dealer>(this, SaveDealer);

            NewBillPayment.DateOfDeposit = DateTime.Now;
            SearchCommand = new RelayCommand(Search);
            AddItemCommand = new RelayCommand(AddItem);
            DeleteItemCommand = new RelayCommand(DeleteItem);
            ResetCommand = new RelayCommand(Reset);
            SubmitCommand = new RelayCommand(Submit);
            CellEditCommand = new RelayCommand(CellEdit);
            ChooseDealerCommand = new RelayCommand(ChooseDealer);
            Advanced = new RelayCommand(AdvanceDeposit);
            LoadAll();
        }

        #region Load
        private void LoadAll()
        {
            LoadBill();
        }

        private void LoadBill()
        {
            Busy.Show();
//-----------------------------------------------------------------------need to load unknown bill--------------------------------------------
            _context.Load(_context.GetDueInfoForBillPaymentsQuery().Where(e => e.DealerId == SelectedDealer.DealerId && e.DueStatus == "Unpaid"),LoadBehavior.RefreshCurrent,true).Completed += (s, e) =>
            {
                
                BindBill =new ObservableCollection<Bill>( BillCollection.OrderBy(o => o.PaymentDeadline));
                if (BindBill.Any(o => o.BillCode == "Unknown" && o.DealerId == SelectedDealer.DealerId))
                    UnknownBill = _context.Bills.Where(p => p.BillCode == "Unknown" && p.DealerId == SelectedDealer.DealerId).SingleOrDefault();
                
                else
                {
                    _context.Load(_context.GetBillListsQuery().Where(o => o.BillCode == "Unknown" && o.DealerId==SelectedDealer.DealerId), LoadBehavior.RefreshCurrent, true).Completed += (t, o) =>
                        {
                            UnknownBill = _context.Bills.Where(p => p.BillCode == "Unknown" && p.DealerId == SelectedDealer.DealerId).SingleOrDefault();
                        };
                }
                GenerateBillProductCollection();
                Busy.Close();
            };
        }
        #endregion

        #region Command
        private void Search()
        { 
            BindBill.Clear();
            if (SelectedDateBool)                               BindBill = new ObservableCollection<Bill>(BillCollection.Where(e => e.DateOfIssue.Date >= StartDate.Date && e.DateOfIssue.Date <= EndDate.Date));
            else if (SelectedCodeBool && SelectedCode != null)  BindBill = new ObservableCollection<Bill>(BillCollection.Where(e => e.BillCode == SelectedCode.BillCode));
            else if (SelectedAllBool)                           BindBill = new ObservableCollection<Bill>(BillCollection);
        }

        private void AddItem()
        {
            if (SelectedBillProductIndex < 0)
            {
                MessageBox.Show("Select an Item at first!");
                return;
            }

            if (!BindBillPayment.Contains(BindBillProduct[SelectedBillProductIndex]) && VerifyAdd())
            {
                BindBillPayment.Add(BindBillProduct[SelectedBillProductIndex]);
                SubmitButtonEnableBool = true;
            }
        }

        private void DeleteItem()
        {
            if (SelectedBillPaymentIndex > -1 && BindBillPayment.Count > 0)
            {
                if (BindBillPayment.All(e => e.Bill.PaymentDeadline.Date <= BindBillPayment[SelectedBillPaymentIndex].Bill.PaymentDeadline.Date))
                {
                    BindBillPayment[SelectedBillPaymentIndex].Amount = 0;
                    BindBillPayment.RemoveAt(SelectedBillPaymentIndex);                    
                }
                else MessageBox.Show("Can not delete a bill having least payment deadline!");
            }
            else
                MessageBox.Show("You have to select an item to Delete!");

            if (BindBillPayment.Count == 0) SubmitButtonEnableBool = false;
        }

        private void Reset()
        {
            SelectedBillPaymentIndex = -1;
            BindBillPayment.Clear();
            BindBill.Clear();
            BindBillProduct.Clear();
            DebitAmount = string.Empty;
            NewBillPayment.DateOfDeposit = DateTime.Now;
            SubmitButtonEnableBool = false;
            _context.BillPayments.Clear();
            _context.BillPaymentInfos.Clear();
            _context.DueInfos.Clear();
            _context.Bills.Clear();
            NewBillPayment = new BillPayment() { DateOfDeposit = DateTime.Now };

           // LoadBill();

           
        }

        private void Submit()
        {
            if (GenerateNewPayment() && HasValidationError()==false)
            {
                SubmitButtonEnableBool = false;
                _context.BillPayments.Add(NewBillPayment);
                Busy.Show();
                _context.SubmitChanges(OnSubmitCompleted, null);
            }
        }

        private void CellEdit()
        {
//            foreach (BillProductDetail detail in BindBillPayment)
            BillProductDetail detail = BindBillPayment[SelectedBillPaymentIndex];
            
            if (BindBillPayment.Any(e => e.Bill.PaymentDeadline.Date > detail.Bill.PaymentDeadline.Date) && detail.Amount != detail.Payable)
            {                  
                detail.Amount = detail.Payable;
                MessageBox.Show("Need to fully pay the bills having least payment deadline first!");
                return;                    
            }

            if (SelectedPaymentMethod == "Dealer Debit")
            {
                double goingToPay=BindBillPayment.Sum(e=>e.Amount);
//-----------------------------------------------------------------need to edit---------------------------------
           //     double deposit = SelectedDealer.TotalCollection - SelectedDealer.TotalDue;
                double deposit = UnknownBill.TotalPaid+ UnknownBill.SalesReturnCost+ UnknownBill.ProductLossCost - UnknownBill.TotalProductCost;
                if (goingToPay > deposit)
                {
                    detail.Amount = 0;
                    MessageBox.Show("Placed amount exceeds deposit amount by "+(goingToPay-deposit).ToString());
                    return;
                }
            }
        }
        #endregion

        #region Method

        void SaveDealer(Dealer dealer)
        {
            Reset();
            SelectedDealer = dealer;
            LoadBill();

        }

        void AdvanceDeposit()
        {

            if (SelectedDealer.CompanyName == null)
            {
                MessageBox.Show("Please select a dealer first to issue advanced deposit.");
                return;
            }
            StaticMessaging.DealerMessage = SelectedDealer;
            TrialBusinessManager.Views.RM.AdvancePaymentChildWindow obj = new TrialBusinessManager.Views.RM.AdvancePaymentChildWindow();
            obj.Show();
            
        }

        void ChooseDealer()
        {
            ViewDealerChildWindow Dealers = new ViewDealerChildWindow();
            Dealers.Show();
        }

        private void GenerateBillProductCollection()
        {
            BillProductCollection = new List<BillProductDetail>();

            foreach(DueInfo nowDue in DueCollection)
            {                
                BillProductDetail temp = new BillProductDetail();
                    
                temp.GenerateBillDetail(nowDue);
                    
                if (nowDue.PaymentDeadLine.Date < DateTime.Now.Date && temp.Payable>=0)
                {
                    temp.Commission = 0;
                    temp.NetPrice = temp.TotalPrice-temp.Loss-temp.SalesReturn;
                    temp.Payable = temp.NetPrice - temp.Paid;
                }
                    
                BillProductCollection.Add(temp);
            }
        }

        private void GenerateBillProductDetail()
        {
            BindBillProduct =new  ObservableCollection<BillProductDetail>(BillProductCollection.Where(e => e.Bill.BillId == BindBill[SelectedBillIndex].BillId && e.Payable!=0));
        }

        private bool VerifyAdd()
        {
            if (BindBillProduct[SelectedBillProductIndex].Payable == 0)
            {
                MessageBox.Show("Selected Item has been Paid earlier");
                return false;
            }

            string str= string.Empty;
            Bill nowBill = BindBill[SelectedBillIndex];

            if (nowBill != BindBill.OrderBy(e => e.PaymentDeadline).First())    //not the first unpaid bill having least deadline
            {
                for (int i = 0; i < BindBill.Count && (nowBill.PaymentDeadline.Date > BindBill[i].PaymentDeadline.Date); i++) //for every bill in the list having least deadline than the selected bill
                {
                    // finding the bill products which have not fully paid
                    List<BillProductDetail> checking= BillProductCollection.Where(e=>e.Bill.BillId==BindBill[i].BillId && e.Payable!=0).ToList();                                 
                    
                    foreach (BillProductDetail detail in checking)
                    {
                        //whether the unpaid bill wasn't added or added one wasn't paid upto required amount
                        if (!BindBillPayment.Contains(detail) || BindBillPayment.Any(e=>e.Bill==detail.Bill && e.Product==detail.Product && e.Payable>e.Amount))
                        {
                            if (string.IsNullOrEmpty(str))
                                str += BindBill[i].BillCode;
                            else
                                str += string.Format(" , " + BindBill[i].BillCode);

                            break;
                        }
                    }
                }

                if (str!=string.Empty)//empty str means no unpaid bill product found
                {
                  /*  if (str.Contains(","))  MessageBox.Show(string.Format("Please pay the Bills : " + str + " at first"));
                    else                    MessageBox.Show(string.Format("Please pay the Bill : " + str + " at first"));
                    return false;*/
                }
            }
            return true;
        }

        private bool GenerateNewPayment()
        {
            NewBillPayment.BillPaymentCode = DateTime.Now.ToString();//need to be changed by a trigger in server side
            NewBillPayment.Status = "Issued by RM";
            NewBillPayment.PaymentMethod = SelectedPaymentMethod;
            NewBillPayment.DealerId =  SelectedDealer.DealerId;
            NewBillPayment.DateOfIssue = DateTime.Now;
            NewBillPayment.RMId = UserInfo.EmployeeID;
            NewBillPayment.TotalAmount = BindBillPayment.Sum(e => e.Amount);
            NewBillPayment.AccountantId = 0;
            NewBillPayment.AccountantFinalizeDate = new DateTime(2099, 12, 30);
       
            foreach (BillProductDetail detail in BindBillPayment)
            {
                BillPaymentInfo temp = new BillPaymentInfo();
                temp.BillId = detail.Bill.BillId;
                temp.ProductId = detail.Product.ProductId;
                temp.Amount = detail.Amount;
                temp.HasRejected = false;

                if(temp.Amount>0)
                    NewBillPayment.BillPaymentInfoes.Add(temp);
            }

            if (NewBillPayment.BillPaymentInfoes.Count == 0)
            {
                MessageBox.Show("Insert Bill Payment Amount!");
                NewBillPayment = new BillPayment();
                return false;
            }
            
            BillUpdate();

            return true;
        }

        private void BillUpdate()
        {
            var group = from bill in BindBillPayment
                        group bill by bill.Bill.BillId into grouping
                        select grouping;

            foreach (BillProductDetail value in BindBillPayment)
            {
                if (BindBillPayment.Any(e => e.Bill.PaymentDeadline.Date > value.Bill.PaymentDeadline.Date)==true)
                {
                    if (value.Bill.BillStatus != "Verified by RM Fully Paid")
                    {
                        Bill myBill = _context.Bills.Where(e => e.BillId == value.Bill.BillId).First();
                        myBill.BillStatus = "Verified by RM Fully Paid"; 
                    }
                }
                else if (value.Bill.BillStatus != "Verified by RM Partially Paid")
                {
                    Bill myBill = _context.Bills.Where(e => e.BillId == value.Bill.BillId).First();
                    myBill.BillStatus = "Verified by RM Partially Paid";
                }
            }
        }

        private bool HasValidationError()
        {
            if ((NewBillPayment.ValidationErrors != null) && (NewBillPayment.ValidationErrors.Count > 0))
            {
                string error = string.Empty;
                foreach (ValidationResult result in NewBillPayment.ValidationErrors)
                {
                    error += string.Format("{0} has problem {1}\n", result.MemberNames.First(), result.ErrorMessage);
                }

                MessageBox.Show(error, "Error", MessageBoxButton.OK);
                return true;
            }

            if (string.IsNullOrEmpty(SelectedPaymentMethod))
            {
                MessageBox.Show("Select a payment Method first!");
                return true;
            }
//------------------------------------------------------------------------------------------need to edit-------------------------------------------- 
            if (SelectedPaymentMethod == "Dealer Debit" && (BindBillPayment.Sum(e => e.Amount) > (UnknownBill.TotalPaid+ UnknownBill.SalesReturnCost+ UnknownBill.ProductLossCost - UnknownBill.TotalProductCost)))
            {
                MessageBox.Show("Placed Amount Exceeds Dealer's Deposited Amount!");
                return true;
            }

            if (SelectedPaymentMethod == "DD" && (string.IsNullOrEmpty(NewBillPayment.BankName) || string.IsNullOrEmpty(NewBillPayment.BranchName) || string.IsNullOrEmpty(NewBillPayment.DDNumber)))
            {
                MessageBox.Show("Insert DD Info Properly!");
                return true;
            }

            else if (SelectedPaymentMethod == "Cash" && string.IsNullOrEmpty(NewBillPayment.MoneyReciept))
            {
                MessageBox.Show("Insert Money Reciept Properly!");
                return true;
            }

            return false;
        }

        private void OnSubmitCompleted(SubmitOperation op)
        {
            SubmitButtonEnableBool = true;
            if (op.HasError == true)
            {
               // MessageBox.Show(string.Format("Invalid Entry!!!\nError: {0}", op.Error.Message));
               // op.MarkErrorAsHandled();
                return;
            }

            MessageBox.Show("Entry Successful!");
            Busy.Close();
            Reset();
        }

        #endregion

        #region Properties

        #region Show Bill
        public const string SelectedDealerPropertyName = "SelectedDealer";
        private Dealer _selectedDealer = new Dealer();
        public Dealer SelectedDealer
        {
            get { return _selectedDealer; }
            set
            {
                if (_selectedDealer == value) { return; }

                _selectedDealer = value;                
                RaisePropertyChanged(SelectedDealerPropertyName);
            }
        }

        public const string SelectedCodePropertyName = "SelectedCode";
        private Bill _selectedCode;
        public Bill SelectedCode
        {
            get { return _selectedCode; }
            set
            {
                if (_selectedCode == value) { return; }

                _selectedCode = value;
                RaisePropertyChanged(SelectedCodePropertyName);
            }
        }

        public const string UnknownBillPropertyName = "UnknownBill";
        private Bill _unknownBill;
        public Bill UnknownBill
        {
            get { return _unknownBill; }
            set
            {
                if (_unknownBill == value) { return; }

                _unknownBill = value;
//--------------------------------------------------------------need to edit---------------------------------------------
                if ((_unknownBill.TotalPaid + _unknownBill.SalesReturnCost + _unknownBill.ProductLossCost - _unknownBill.TotalProductCost) > 0)
                {
                    PaymentMethod = new ObservableCollection<string> { "Cash", "DD", "Dealer Debit", "Bill Adjustment" };
                    DebitAmount = string.Format(SelectedDealer.Name + " has Debit Amount of : " + (_unknownBill.TotalPaid + _unknownBill.SalesReturnCost + _unknownBill.ProductLossCost - _unknownBill.TotalProductCost).ToString());
                }

                else
                {
                    PaymentMethod = new ObservableCollection<string> { "Cash", "DD","Bill Adjustment" };
                    DebitAmount = string.Empty;
                }

                RaisePropertyChanged(UnknownBillPropertyName);
            }
        }

        public const string SelectedBillIndexPropertyName = "SelectedBillIndex";
        private int _selectedBillIndex;
        public int SelectedBillIndex
        {
            get { return _selectedBillIndex; }
            set
            {
                if (_selectedBillIndex == value) { return; }

                _selectedBillIndex = value;

                if(_selectedBillIndex>-1)
                    GenerateBillProductDetail();
                
                RaisePropertyChanged(SelectedBillIndexPropertyName);
            }
        }

        public const string SelectedDateOfIssuePropertyName = "SelectedDateOfIssue";
        private DateTime _selectedDateOfIssue = DateTime.Now;
        public DateTime SelectedDateOfIssue
        {
            get { return _selectedDateOfIssue; }
            set
            {
                if (_selectedDateOfIssue == value) { return; }

                _selectedDateOfIssue = value;
                RaisePropertyChanged(SelectedDateOfIssuePropertyName);
            }
        }

        public const string StartDatePropertyName = "StartDate";
        private DateTime _startDate = DateTime.Now;
        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                if (_startDate == value) { return; }

                _startDate = value;
                RaisePropertyChanged(StartDatePropertyName);
            }
        }

        public const string EndDatePropertyName = "EndDate";
        private DateTime _endDate = DateTime.Now;
        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                if (_endDate == value) { return; }

                _endDate = value;
                RaisePropertyChanged(EndDatePropertyName);
            }
        }

        public const string SelectedCodeBoolPropertyName = "SelectedCodeBool";
        private bool _selectedCodeBool = false;
        public bool SelectedCodeBool
        {
            get { return _selectedCodeBool; }
            set
            {
                if (_selectedCodeBool == value) { return; }
                _selectedCodeBool = value;
                RaisePropertyChanged(SelectedCodeBoolPropertyName);
            }
        }

        public const string SelectedAllBoolPropertyName = "SelectedAllBool";
        private bool _selectedAllBool = true;
        public bool SelectedAllBool
        {
            get { return _selectedAllBool; }
            set
            {
                if (_selectedAllBool == value) { return; }
                _selectedAllBool = value;
                RaisePropertyChanged(SelectedAllBoolPropertyName);
            }
        }

        public const string BindBillPropertyName = "BindBill";
        private ObservableCollection<Bill> _bindBill = new ObservableCollection<Bill>();
        public ObservableCollection<Bill> BindBill
        {
            get { return _bindBill; }
            set
            {
                if (_bindBill == value) { return; }

                _bindBill = value;
                RaisePropertyChanged(BindBillPropertyName);
            }
        }

        public const string BindBillProductPropertyName = "BindBillProduct";
        private ObservableCollection<BillProductDetail> _bindBillProduct = new ObservableCollection<BillProductDetail>();
        public ObservableCollection<BillProductDetail> BindBillProduct
        {
            get { return _bindBillProduct; }
            set
            {
                if (_bindBillProduct == value) { return; }

                _bindBillProduct = value;
                RaisePropertyChanged(BindBillProductPropertyName);
            }
        }
        #endregion
        
        #region Bill Payment

        public const string SelectedBillProductIndexPropertyName = "SelectedBillProductIndex";
        private int _selectedBillProductIndex;
        public int SelectedBillProductIndex
        {
            get { return _selectedBillProductIndex; }
            set
            {
                if (_selectedBillProductIndex == value) { return; }

                _selectedBillProductIndex = value;
                RaisePropertyChanged(SelectedBillProductIndexPropertyName);
            }
        }

        public const string SelectedBillPaymentIndexPropertyName = "SelectedBillPaymentIndex";
        private int _selectedBillPaymentIndex;
        public int SelectedBillPaymentIndex
        {
            get { return _selectedBillPaymentIndex; }
            set
            {
                if (_selectedBillPaymentIndex == value) { return; }

                _selectedBillPaymentIndex = value;
                RaisePropertyChanged(SelectedBillPaymentIndexPropertyName);
            }
        }
        
        public const string BindBillPaymentPropertyName = "BindBillPayment";
        private ObservableCollection<BillProductDetail> _bindBillPayment = new ObservableCollection<BillProductDetail>();
        public ObservableCollection<BillProductDetail> BindBillPayment
        {
            get { return _bindBillPayment; }
            set
            {
                if (_bindBillPayment == value) { return; }

                _bindBillPayment = value;
                RaisePropertyChanged(BindBillPaymentPropertyName);
            }
        }

        public const string NewBillPaymentPropertyName = "NewBillPayment";
        private BillPayment _newBillPayment = new BillPayment() { DateOfDeposit = DateTime.Now };
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
        private bool _paymentMethodBool=true;
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
                if (SelectedPaymentMethod != null && (SelectedPaymentMethod == "Cash" || SelectedPaymentMethod == "Dealer Debit" || SelectedPaymentMethod == "Bill Adjustment"))
                {
                    NewBillPayment.BankName = "NA";
                    NewBillPayment.BranchName = "NA";
                    NewBillPayment.DDNumber = "NA";
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

        public const string DebitAmountPropertyName = "DebitAmount";
        private string _debitAmount=string.Empty;
        public string DebitAmount
        {
            get { return _debitAmount; }
            set
            {
                if (_debitAmount == value) { return; }

                _debitAmount = value;
                RaisePropertyChanged(DebitAmountPropertyName);
            }
        }

        public const string PaymentMethodPropertyName = "PaymentMethod";
        private ObservableCollection<string> _paymentMethod = new ObservableCollection<string> ();
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

        public const string SubmitButtonEnableBoolPropertyName = "SubmitButtonEnableBool";
        private bool _submitButtonEnableBool = false;
        public bool SubmitButtonEnableBool
        {
            get { return _submitButtonEnableBool; }
            set
            {
                if (_submitButtonEnableBool == value) { return; }
                _submitButtonEnableBool = value;
                RaisePropertyChanged(SubmitButtonEnableBoolPropertyName);
            }
        }

        public const string SelectedDateBoolPropertyName = "SelectedDateBool";
        private bool _selectedDateBool = false;
        public bool SelectedDateBool
        {
            get { return _selectedDateBool; }
            set
            {
                if (_selectedDateBool == value) { return; }
                _selectedDateBool = value;
                RaisePropertyChanged(SelectedDateBoolPropertyName);
            }
        }
        #endregion

        #endregion
    }
}
