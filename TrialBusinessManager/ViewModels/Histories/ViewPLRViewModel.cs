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
using System.ComponentModel;
using System.Collections.ObjectModel;
using TrialBusinessManager.Views;
using System.Windows.Data;

namespace TrialBusinessManager.ViewModels.Histories
{
    public class ViewPLRViewModel : ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        BusyWindow MyWindow = new BusyWindow();
        int flag = 0;
        int count=2, DataCount = 0;
        public ICommand SearchCommand { get; set; }
        public ICommand ViewPLRCommand { get; set; }
        public ICommand LoadMoreCommand { get; set; }
        private PLRDetailChildWindow detail;

        public EntitySet<PLR> PLRCollection { get { return _context.PLRs; } }
        public EntitySet<Region> RegionCollection { get { return _context.Regions; } }
        public EntitySet<Employee> EmployeeCollection { get { return _context.Employees; } }

        public ViewPLRViewModel()
        {
            IsEnabled = checkDesignation();
            SearchCommand = new RelayCommand(Search);
            ViewPLRCommand = new RelayCommand(ViewPLR);
            MyWindow.Show();
            LoadAll();
        }

        bool checkDesignation()
        {
            if (UserInfo.Designation == "Admin" || UserInfo.Designation == "Accountant" || UserInfo.Designation == "Viewer" || UserInfo.Designation == "National Sales Manager")
                return true;
            return false;
        }

        #region Methods

        private void ViewPLR()
        {
            if (SelectedIndex > -1)
            {
                StaticMessaging.PLRMessage = (PLR)_bindPLR.GetItemAt(SelectedIndex); 
                detail = new PLRDetailChildWindow();
                detail.Show();
                
                return;
            }
            MessageBox.Show("Select a PLR to view", "Invalid Selection", MessageBoxButton.OK);
        }

 

        private void Search()
        {
            _context.PLRs.Clear();
            MyWindow.Show();

            LoadOperation<PLR> LoadPLRs ;
            if (IsEnabled) LoadPLRs = _context.Load(_context.GetPLRHistoryQuery(SelectedRegionBool, SelectedCodeBool, SelectedDateBool, SelectedStatusBool, StartDate, EndDate.AddDays(1), SelectedRegion.RegionId, SelectedCode, SelectedStatus));
            else LoadPLRs = _context.Load(_context.GetPLRHistoryQuery(true, SelectedCodeBool, SelectedDateBool, SelectedStatusBool, StartDate, EndDate.AddDays(1), UserInfo.Region.RegionId, SelectedCode, SelectedStatus));
            LoadPLRs.Completed += (s, e) =>
            {
                BindPLR = new PagedCollectionView(LoadPLRs.Entities);
                MyWindow.Close();
            };
        }

        private void LoadAll()
        {
            LoadRegions();
            LoadEmployees();
        }

        

        private void LoadRegions()
        {
            _context.Load(_context.GetRegionsQuery(), k => { }, null).Completed += (s, e) =>
                {
                    flag++;
                    if (flag == count)
                        MyWindow.Close();
                };
        }

        private void LoadEmployees()
        {
            _context.Load(_context.GetEmployeesQuery(), k => { }, null).Completed += (s, e) =>
                {
                    flag++;
                    if (flag == count)
                        MyWindow.Close();
                };
        }
        #endregion
        #region Properties

        public const string PLRStatusPropertyName = "PLRStatus";
        private ObservableCollection<string> _PLRStatus = ConstantCollections.PLRStatusType;
        public ObservableCollection<string> PLRStatus
        {
            get { return _PLRStatus; }
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

        public const string BindPLRPropertyName = "BindPLR";
        private PagedCollectionView _bindPLR;
        public PagedCollectionView BindPLR
        {
            get { return _bindPLR; }
            set
            {
                if (_bindPLR == value) { return; }

                _bindPLR = value;
                RaisePropertyChanged(BindPLRPropertyName);
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


        public const string SelectedEmployeeBoolPropertyName = "SelectedEmployeeBool";
        private bool _selectedEmployeeBool = false;
        public bool SelectedEmployeeBool
        {
            get { return _selectedEmployeeBool; }
            set
            {
                if (_selectedEmployeeBool == value) { return; }
                _selectedEmployeeBool = value;
                RaisePropertyChanged(SelectedEmployeeBoolPropertyName);
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

        private bool _IsEnabled;
        public bool IsEnabled
        {
            get { return _IsEnabled; }
            set
            {
                if (_IsEnabled == value) { return; }

                _IsEnabled = value;
                RaisePropertyChanged("IsEnabled");
            }
        }
        #endregion
    }
}
