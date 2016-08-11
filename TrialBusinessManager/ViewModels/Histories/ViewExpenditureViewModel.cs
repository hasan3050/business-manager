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
    public class ViewExpenditureViewModel : ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        BusyWindow MyWindow = new BusyWindow();
        int flag = 0;
        int count, DataCount = 0;
        
        public ICommand SearchCommand { get; set; }
        public ICommand ViewExpenditureCommand { get; set; }
        public ICommand LoadMoreCommand { get; set; }
        
        private ExpenditureDetailChildWindow detail;

        public EntitySet<Expenditure> ExpenditureCollection { get { return _context.Expenditures; } }
        public EntitySet<Region> RegionCollection { get { return _context.Regions; } }
        public EntitySet<Employee> EmployeeCollection { get { return _context.Employees; } }

        public ViewExpenditureViewModel()
        {
            SearchCommand = new RelayCommand(Search);
            ViewExpenditureCommand = new RelayCommand(ViewExpenditure);
            LoadMoreCommand = new RelayCommand(LoadMoreData);
            MyWindow.Show();
            LoadAll();
        }

        #region Methods

        private void LoadMoreData()
        {
            MyWindow.Show();
            LoadExpenditures();
        }

        private void ViewExpenditure()
        {
            if (SelectedIndex > -1)
            {
                StaticMessaging.ExpenditureMessage = (Expenditure)BindExpenditure.GetItemAt(SelectedIndex);
                detail = new ExpenditureDetailChildWindow();
                detail.Show();

                return;
            }
            MessageBox.Show("Select a Expenditure to view", "Invalid Selection", MessageBoxButton.OK);
        }

        private void Search()
        {
            
            if (SelectedRegionBool && SelectedRegion != null) 
                BindExpenditure = new PagedCollectionView(ExpenditureCollection.Where(e => e.Employee.RegionId == SelectedRegion.RegionId).ToList());
           
            else if (SelectedEmployeeBool && SelectedEmployee != null) 
                BindExpenditure = new PagedCollectionView(ExpenditureCollection.Where(e => e.PlacedById == SelectedEmployee.EmployeeId).ToList());
            
            else if (SelectedStatusBool && !string.IsNullOrEmpty(SelectedStatus)) 
                BindExpenditure = new PagedCollectionView(ExpenditureCollection.Where(e => e.Status == SelectedStatus).ToList());
            
            else if (SelectedDateBool) 
                BindExpenditure = new PagedCollectionView(ExpenditureCollection.Where(e => e.DateOfIssue.Date >= StartDate.Date && e.DateOfIssue.Date <= EndDate.Date).ToList());
            
            else if (SelectedCodeBool && SelectedCode != null) 
                BindExpenditure = new PagedCollectionView(ExpenditureCollection.Where(e => e.ExpenditureCode == SelectedCode.ExpenditureCode).ToList());
            
            else if (SelectedAllBool) 
                BindExpenditure = new PagedCollectionView(ExpenditureCollection.ToList());
        }

        private void LoadAll()
        {
            LoadExpenditures();
            LoadRegions();
            LoadEmployees();
        }

        private void LoadExpenditures()
        {
            if (UserInfo.Designation == "Regional Manager")
            {
                _context.Load(_context.GetExpendituresQuery().Where(e => e.PlacedById == UserInfo.EmployeeID).OrderByDescending(e => e.DateOfIssue).Skip(DataCount * 10).Take(10), k => { }, null).Completed += (s, e) =>
                {
                    
                    flag++;
                    if (flag == 3||DataCount>0)
                        MyWindow.Close();
                    BindExpenditure = new PagedCollectionView(ExpenditureCollection.ToList());
                };

            }
            else
            {
                _context.Load(_context.GetExpendituresQuery().OrderByDescending(e => e.DateOfIssue).OrderByDescending(e => e.DateOfIssue).Skip(DataCount * 10).Take(10), k => { }, null).Completed += (s, e) =>
                {
                    IsEnabled= true;
                    flag++;
                    if (flag == 3||DataCount>0)
                        MyWindow.Close();
                    BindExpenditure = new PagedCollectionView(ExpenditureCollection.ToList());
                };
            }
            
            DataCount++;
        }

        private void LoadRegions()
        {
            _context.Load(_context.GetRegionsQuery(), k => { }, null).Completed += (s, e) =>
                {
                    flag++;
                    if (flag == 3)
                        MyWindow.Close();
                };
        }

        private void LoadEmployees()
        {
            _context.Load(_context.GetEmployeesQuery(), k => { }, null).Completed += (s, e) =>
                {
                    flag++;
                    if (flag == 3)
                        MyWindow.Close();
                };
        }
        #endregion
        #region Properties

        public const string ExpenditureStatusPropertyName = "ExpenditureStatus";
        private ObservableCollection<string> _ExpenditureStatus = ConstantCollections.ExpenditureStatusType;
        public ObservableCollection<string> ExpenditureStatus
        {
            get { return _ExpenditureStatus; }
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

        public const string SelectedStatusPropertyName = "SelectedStatus";
        private string _selectedStatus;
        public string SelectedStatus
        {
            get { return _selectedStatus; }
            set
            {
                if (_selectedStatus == value) { return; }

                _selectedStatus = value;
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
        private Expenditure _selectedCode;
        public Expenditure SelectedCode
        {
            get { return _selectedCode; }
            set
            {
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

        public const string BindExpenditurePropertyName = "BindExpenditure";
        private PagedCollectionView _bindExpenditure;
        public PagedCollectionView BindExpenditure
        {
            get { return _bindExpenditure; }
            set
            {
                if (_bindExpenditure == value) { return; }

                _bindExpenditure = value;
                RaisePropertyChanged(BindExpenditurePropertyName);
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
