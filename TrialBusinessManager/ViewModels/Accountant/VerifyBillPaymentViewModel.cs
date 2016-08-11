using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using TrialBusinessManager.Models;
using TrialBusinessManager.Views;
using TrialBusinessManager.Web;
using TrialBusinessManager.Views.Accountant;

namespace TrialBusinessManager.ViewModels.Accountant
{
    public class VerifyBillPaymentViewModel : ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        private VerifyBillPaymentDetailChildWindow detail;
        BusyWindow Busy = new BusyWindow();
        int flag = 0;

        public ICommand SearchCommand { get; set; }
        public ICommand ViewBillPaymentCommand { get; set; }

        public ObservableCollection<BillPayment> BillPaymentCollection { get { return new ObservableCollection<BillPayment>(_context.BillPayments.Where(e => e.Status == "Issued by RM" || e.Status == "Issued by RM for Advance")); } }
        public EntitySet<Region> RegionCollection { get { return _context.Regions; } }
        public EntitySet<Dealer> DealerCollection { get { return _context.Dealers; } }
        public EntitySet<Employee> EmployeeCollection { get { return _context.Employees; } }

        public VerifyBillPaymentViewModel()
        {
            //Messenger.Default.Register<string>(this, OnMessageReceive);
            StaticMessaging.AccountantBillPaymentSubmitted = false;
            
            SearchCommand = new RelayCommand(Search);
            ViewBillPaymentCommand = new RelayCommand(ViewBillPayment);
            Busy.Show();
            LoadAll();
        }

        void detail_Closed(object sender, EventArgs e)
        {
            if (StaticMessaging.AccountantBillPaymentSubmitted)
            {
                BillPayment billPayment = BindBillPayment.Where(p => p.BillPaymentId == StaticMessaging.BillPaymentMessage.BillPaymentId).SingleOrDefault();
                BindBillPayment.Remove(billPayment);
                StaticMessaging.AccountantBillPaymentSubmitted = false;
            }
        }

        #region Methods
        
        private void Search()
        {
            BindBillPayment.Clear();

            if (SelectedDealerBool && SelectedDealer != null) 
                BindBillPayment = new ObservableCollection<BillPayment>(BillPaymentCollection.Where(e => e.DealerId == SelectedDealer.DealerId));
            
            else if (SelectedRegionBool && SelectedRegion != null) 
                BindBillPayment = new ObservableCollection<BillPayment>(BillPaymentCollection.Where(e => e.Dealer.RegionId == SelectedRegion.RegionId));
            
            else if (SelectedRMBool && SelectedEmployee != null) 
                BindBillPayment = new ObservableCollection<BillPayment>(BillPaymentCollection.Where(e => e.RMId == SelectedEmployee.EmployeeId));
            
            else if (SelectedDateBool) 
                BindBillPayment = new ObservableCollection<BillPayment>(BillPaymentCollection.Where(e => e.DateOfIssue.Date >= StartDate.Date && e.DateOfIssue.Date <= EndDate.Date));
            
            else if (SelectedCodeBool && SelectedCode != null) 
                BindBillPayment = new ObservableCollection<BillPayment>(BillPaymentCollection.Where(e => e.BillPaymentCode == SelectedCode.BillPaymentCode));
           
            else if (SelectedMethodBool && SelectedMethod != null) 
                BindBillPayment = new ObservableCollection<BillPayment>(BillPaymentCollection.Where(e => e.PaymentMethod == SelectedMethod));
            
            else if (SelectedAllBool) 
                BindBillPayment = BillPaymentCollection;
        }

        private void LoadAll()
        {
            LoadBillPayments();
            LoadRegions();
            LoadDealers();
            LoadEmployees();
        }

        private void LoadBillPayments()
        {
            _context.Load(_context.GetBillPaymentDetailsQuery().Where(e => e.Status == "Issued by RM" || e.Status == "Issued by RM for Advance"), k => { }, null).Completed += (s, e) =>
            {
                BindBillPayment = BillPaymentCollection;
                flag++;
                if (flag == 4)
                    Busy.Close();
            };
        }

        private void LoadRegions()
        {
            _context.Load(_context.GetRegionsQuery(), k => { }, null).Completed += (s, e) =>
                {
                    flag++;
                    if (flag == 4)
                        Busy.Close();
                };
        }

        private void LoadDealers()
        {
            _context.Load(_context.GetDealersQuery(), k => { }, null).Completed += (s, e) =>
                {
                    flag++;
                    if (flag == 4)
                        Busy.Close();
                };
        }

        private void LoadEmployees()
        {
            _context.Load(_context.GetEmployeesQuery().Where(e => e.Designation == "Regional Manager"), k => { }, null).Completed += (s, e) =>
                {
                    flag++;
                    if (flag == 4)
                        Busy.Close();
                };
        }

        private void ViewBillPayment()
        {
            if (SelectedIndex > -1)
            {
                StaticMessaging.BillPaymentMessage = BindBillPayment[SelectedIndex];
                detail = new VerifyBillPaymentDetailChildWindow();
                detail.Closed += new EventHandler(detail_Closed);
                detail.Show();
                return;
            }
            MessageBox.Show("Select a bill payment to view", "Invalid Selection", MessageBoxButton.OK);
        }
        #endregion

        #region Properties

        private ObservableCollection<string> _billPaymentMethod = new ObservableCollection<string> { "Cash", "DD" };
        public ObservableCollection<string> BillPaymentMethod
        {
            get { return _billPaymentMethod; }
        }

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

        public const string SelectedEmployeePropertyName = "SelectedEmployee";
        private Employee _selectedEmployee = new Employee();
        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                if (_selectedEmployee == value) { return; }

                _selectedEmployee = value;
                RaisePropertyChanged(SelectedEmployeePropertyName);
            }
        }

        public const string SelectedRegionPropertyName = "SelectedRegion";
        private Region _selectedRegion = new Region();
        public Region SelectedRegion
        {
            get { return _selectedRegion; }
            set
            {
                if (_selectedRegion == value) { return; }

                _selectedRegion = value;
                RaisePropertyChanged(SelectedRegionPropertyName);
            }
        }

        public const string SelectedMethodPropertyName = "SelectedMethod";
        private string _selectedMethod;
        public string SelectedMethod
        {
            get { return _selectedMethod; }
            set
            {
                if (_selectedMethod == value) { return; }

                _selectedMethod = value;
                RaisePropertyChanged(SelectedMethodPropertyName);
            }
        }

        public const string SelectedCodePropertyName = "SelectedCode";
        private BillPayment _selectedCode;
        public BillPayment SelectedCode
        {
            get { return _selectedCode; }
            set
            {
                if (_selectedCode == value) { return; }

                _selectedCode = value;
                RaisePropertyChanged(SelectedCodePropertyName);
            }
        }

        public const string SelectedIndexPropertyName = "SelectedIndex";
        private int _selectedIndex;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                if (_selectedIndex == value) { return; }

                _selectedIndex = value;
                RaisePropertyChanged(SelectedIndexPropertyName);
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

        public const string BindBillPaymentPropertyName = "BindBillPayment";
        private ObservableCollection<BillPayment> _bindBillPayment = new ObservableCollection<BillPayment>();
        public ObservableCollection<BillPayment> BindBillPayment
        {
            get { return _bindBillPayment; }
            set
            {
                if (_bindBillPayment == value) { return; }

                _bindBillPayment = value;
                RaisePropertyChanged(BindBillPaymentPropertyName);
            }
        }

        public const string SelectedRegionBoolPropertyName = "SelectedRegionBool";
        private bool _selectedRegionBool = false;
        public bool SelectedRegionBool
        {
            get { return _selectedRegionBool; }
            set
            {
                if (_selectedRegionBool == value) { return; }
                _selectedRegionBool = value;
                RaisePropertyChanged(SelectedRegionBoolPropertyName);
            }
        }

        public const string SelectedDealerBoolPropertyName = "SelectedDealerBool";
        private bool _selectedDealerBool = false;
        public bool SelectedDealerBool
        {
            get { return _selectedDealerBool; }
            set
            {
                if (_selectedDealerBool == value) { return; }
                _selectedDealerBool = value;
                RaisePropertyChanged(SelectedDealerBoolPropertyName);
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

        public const string SelectedRMBoolPropertyName = "SelectedRMBool";
        private bool _selectedRMBool = false;
        public bool SelectedRMBool
        {
            get { return _selectedRMBool; }
            set
            {
                if (_selectedRMBool == value) { return; }
                _selectedRMBool = value;
                RaisePropertyChanged(SelectedRMBoolPropertyName);
            }
        }

        public const string SelectedMethodBoolPropertyName = "SelectedMethodBool";
        private bool _selectedMethodBool = false;
        public bool SelectedMethodBool
        {
            get { return _selectedMethodBool; }
            set
            {
                if (_selectedMethodBool == value) { return; }
                _selectedMethodBool = value;
                RaisePropertyChanged(SelectedMethodBoolPropertyName);
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
        #endregion
    }
}
