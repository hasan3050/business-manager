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
using TrialBusinessManager.Views;
using System.Windows.Data;

namespace TrialBusinessManager.ViewModels.Histories
{
    public class ViewIndentViewModel:ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        BusyWindow MyWindow = new BusyWindow();
        int count=3, DataCount = 0;
        int flag = 0;
        
        private IndentDetailChildWindow detail;
        public ICommand SearchCommand { get; set; }
        public ICommand ViewIndentCommand { get; set; }

        public EntitySet<Indent> IndentCollection {get { return _context.Indents; } }
        public EntitySet<Region> RegionCollection {get {return _context.Regions;}}
        public EntitySet<Dealer> DealerCollection {get {return _context.Dealers;}}
        public EntitySet<Employee> EmployeeCollection {get { return _context.Employees;}}
        
        public ViewIndentViewModel()
        {
            IsEnabled = checkDesignation();
            if (IsEnabled) count = 3;
            else count=2;
            SearchCommand = new RelayCommand(Search);
            ViewIndentCommand = new RelayCommand(ViewIndent);
            MyWindow.Show();
            LoadAll();
        }

        bool checkDesignation()
        {
            if (UserInfo.Designation == "Admin" || UserInfo.Designation == "Accountant" || UserInfo.Designation == "Viewer" || UserInfo.Designation == "National Sales Manager"||UserInfo.Designation == "Store In Charge"||UserInfo.Designation=="Dispatch Officer")
                return true;
            return false;
        }

        #region Methods

        

        private void Search()
        {
            _context.Indents.Clear();
            MyWindow.Show();

            LoadOperation<Indent> LoadIndents;
            if (IsEnabled) LoadIndents = _context.Load(_context.GetIndentHistoryQuery(SelectedDealerBool, SelectedRegionBool, SelectedCodeBool, SelectedStatusBool, SelectedDealer.DealerId, StartDate, EndDate.AddDays(1), SelectedRegion.RegionId, SelectedStatus, SelectedCode, SelectedDateBool));
            else LoadIndents = _context.Load(_context.GetIndentHistoryQuery(SelectedDealerBool, true, SelectedCodeBool, SelectedStatusBool, SelectedDealer.DealerId, StartDate, EndDate.AddDays(1), UserInfo.Region.RegionId, SelectedStatus, SelectedCode, SelectedDateBool));
            LoadIndents.Completed += (s, e) =>
            {
                BindIndent = new PagedCollectionView(LoadIndents.Entities);
                MyWindow.Close();
            };
        }
        
        

        

        private void LoadAll()
        {
            LoadRegions();
            LoadDealers();
            LoadEmployees();
        }

        

        private void LoadRegions()
        {
            if (IsEnabled)
            {
                _context.Load(_context.GetRegionsQuery(), k => { }, null).Completed += (s, e) =>
                    {
                        flag++;
                        if (flag == count)
                            MyWindow.Close();
                    };
            }
        }

        private void LoadDealers()
        {
            if (IsEnabled)
            {
                _context.Load(_context.GetDealersQuery(), k => { }, null).Completed += (s, e) =>
                    {
                        flag++;
                        if (flag == count)
                            MyWindow.Close();
                    };
            }
            else
            {
                _context.Load(_context.GetDealersQuery().Where(c=>c.RegionId.Equals(UserInfo.Region.RegionId)), k => { }, null).Completed += (s, e) =>
                {
                    flag++;
                    if (flag == count)
                        MyWindow.Close();
                };
            }
        }

        private void LoadEmployees()
        {
            if (IsEnabled)
            {
                _context.Load(_context.GetEmployeesQuery().Where(e => e.Designation == "Regional Manager"), k => { }, null).Completed += (s, e) =>
                    {
                        flag++;
                        if (flag == count)
                            MyWindow.Close();
                    };
            }
            else
            {
                _context.Load(_context.GetEmployeesQuery().Where(e => e.EmployeeId.Equals(UserInfo.EmployeeID)), k => { }, null).Completed += (s, e) =>
                {
                    flag++;
                    if (flag == count)
                        MyWindow.Close();
                };
            }
        }

        private void ViewIndent()
        {
            if (SelectedIndex > -1)
            {
                StaticMessaging.IndentMessage =(Indent) BindIndent.GetItemAt(SelectedIndex);
                detail = new IndentDetailChildWindow();
                detail.Show();
                return;
            }
            MessageBox.Show("Select an indent to view", "Invalid Selection", MessageBoxButton.OK);
        }
        #endregion

        #region Properties

        private bool _isEnabled = true;
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (_isEnabled == value)
                {
                    return;
                }

                _isEnabled = value;
                RaisePropertyChanged("IsEnabled");
            }
        }

        public const string IndentStatusPropertyName = "IndentStatus";
        private ObservableCollection<string> _indentStatus = ConstantCollections.IndentStatusType;
        public ObservableCollection<string> IndentStatus
        {
            get { return _indentStatus; }
        }

        public const string SelectedDealerPropertyName = "SelectedDealer";
        private Dealer _selectedDealer = new Dealer();
        public Dealer SelectedDealer
        {
            get { return _selectedDealer; }
            set
            {
                if (value == null) value = new Dealer();
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
                if (value == null) value = new Region();
                if (_selectedRegion == value) { return; }
                else _selectedRegion = value;
                RaisePropertyChanged(SelectedRegionPropertyName);
            }
        }

        public const string SelectedStatusPropertyName = "SelectedStatus";
        private string _selectedStatus = "";
        public string SelectedStatus
        {
            get { return _selectedStatus; }
            set
            {
                if (value == null) value = "";
                if (_selectedStatus == value) { return; }
                else _selectedStatus = value;
                RaisePropertyChanged(SelectedStatusPropertyName);
            }
        }

        public const string SelectedCodePropertyName = "SelectedCode";
        private String _selectedCode = "";
        public String SelectedCode
        {
            get { return _selectedCode; }
            set
            {
                if (value == null) value = "";
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
        private DateTime _startDate=DateTime.Now;
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
        private DateTime _endDate=DateTime.Now;
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

        public const string BindIndentPropertyName = "BindIndent";
        private PagedCollectionView _bindIndent;
        public PagedCollectionView BindIndent
        {
            get { return _bindIndent; }
            set
            {
                if (_bindIndent == value) { return; }

                _bindIndent = value;
                RaisePropertyChanged(BindIndentPropertyName);
            }
        }

        public const string SelectedRegionBoolPropertyName = "SelectedRegionBool";
        private bool _selectedRegionBool = false;
        public bool SelectedRegionBool
        {
            get{ return _selectedRegionBool; }
            set
            {
                if (_selectedRegionBool == value)   {return;}
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

        public const string SelectedStatusBoolPropertyName = "SelectedStatusBool";
        private bool _selectedStatusBool = false;
        public bool SelectedStatusBool
        {
            get { return _selectedStatusBool; }
            set
            {
                if (_selectedStatusBool == value) { return; }
                _selectedStatusBool = value;
                RaisePropertyChanged(SelectedStatusBoolPropertyName);
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
