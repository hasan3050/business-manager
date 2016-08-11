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
using TrialBusinessManager.Views.Histories;
using System.Windows.Data;

namespace TrialBusinessManager.ViewModels.SIC
{
    public class ViewRequisitionViewModel : ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        private RequisitionDetailChildWindow detail;
        BusyWindow Busy = new BusyWindow();
        int flag = 0;
        int count, DataCount = 0;
        public ICommand LoadMoreCommand { get; set; }

        public ICommand SearchCommand { get; set; }
        public ICommand ViewRequisitionCommand { get; set; }

        public EntitySet<Requisition> RequisitionCollection { get { return _context.Requisitions; } }
        public EntitySet<Region> RegionCollection { get { return _context.Regions; } }
        public EntitySet<Dealer> DealerCollection { get { return _context.Dealers; } }
        public EntitySet<Employee> EmployeeCollection { get { return _context.Employees; } }

        public ViewRequisitionViewModel()
        {
            SearchCommand = new RelayCommand(Search);
            ViewRequisitionCommand = new RelayCommand(ViewRequisition);
            LoadMoreCommand = new RelayCommand(LoadMoreData);
            Busy.Show();
            LoadAll();
        }

        #region Methods

        private void LoadMoreData()
        {
            Busy.Show();
            LoadRequisitions();
        }

        private void Search()
        {

            if (SelectedDOBool && SelectedEmployee != null)
                BindRequisition = new PagedCollectionView(RequisitionCollection.Where(e => e.IssuedById == SelectedEmployee.EmployeeId).ToList());

            else if (SelectedTypeBool && SelectedType != null)
                BindRequisition = new PagedCollectionView(RequisitionCollection.Where(e => e.RequisitionType == SelectedType).ToList());

            else if (SelectedDateBool)
                BindRequisition = new PagedCollectionView(RequisitionCollection.Where(e => e.DateOfIssue.Date >= StartDate.Date && e.DateOfIssue.Date <= EndDate.Date).ToList());

            else if (SelectedCodeBool && SelectedCode != null)
                BindRequisition = new PagedCollectionView(RequisitionCollection.Where(e => e.RequisitionCode == SelectedCode.RequisitionCode).ToList());

            else if (SelectedAllBool)
                BindRequisition = new PagedCollectionView(RequisitionCollection.ToList());
        }

        private void LoadAll()
        {
            LoadRequisitions();
            LoadEmployees();
        }

        private void LoadRequisitions()
        {
            _context.Load(_context.GetRequisitionsQuery().Where(e => e.Employee.Region.RegionId == UserInfo.Region.RegionId).OrderByDescending(e => e.DateOfIssue).Skip(DataCount * 10).Take(10), k => { }, null).Completed += (s, e) =>
            {
                flag++;
                BindRequisition = new PagedCollectionView(RequisitionCollection.ToList());
                if (flag == 2||DataCount>0)
                    Busy.Close();
            };

            DataCount++;
        }

        private void LoadEmployees()
        {
            _context.Load(_context.GetEmployeesQuery().Where(e => e.Designation == "Store in Charge" || e.Designation == "Dispatch Officer"), k => { }, null).Completed += (s, e) =>
                {
                    flag++;
                    if (flag == 2)
                        Busy.Close();
                };
        }

        private void ViewRequisition()
        {
            if (SelectedIndex > -1)
            {
                               StaticMessaging.RequisitionMessage = (Requisition)BindRequisition.GetItemAt(SelectedIndex);
                                detail = new RequisitionDetailChildWindow();
                                detail.Show();
                return;
            }
            MessageBox.Show("Select a Requisition to view", "Invalid Selection", MessageBoxButton.OK);
        }
        #endregion

        #region Properties

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

        public const string RequisitionTypeCollectionPropertyName = "RequisitionTypeCollection";
        private ObservableCollection<string> _requisitionTypeCollection = ConstantCollections.RequisitionTypeName;
        public ObservableCollection<string> RequisitionTypeCollection
        {
            get { return _requisitionTypeCollection; }
        }

        public const string SelectedCodePropertyName = "SelectedCode";
        private Requisition _selectedCode;
        public Requisition SelectedCode
        {
            get { return _selectedCode; }
            set
            {
                if (_selectedCode == value) { return; }

                _selectedCode = value;
                RaisePropertyChanged(SelectedCodePropertyName);
            }
        }

        public const string SelectedTypePropertyName = "SelectedType";
        private string _selectedType;
        public string SelectedType
        {
            get { return _selectedType; }
            set
            {
                if (_selectedType == value) { return; }

                _selectedType = value;
                RaisePropertyChanged(SelectedTypePropertyName);
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

        public const string BindRequisitionPropertyName = "BindRequisition";
        private PagedCollectionView _bindRequisition;
        public PagedCollectionView BindRequisition
        {
            get { return _bindRequisition; }
            set
            {
                if (_bindRequisition == value) { return; }

                _bindRequisition = value;
                RaisePropertyChanged(BindRequisitionPropertyName);
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

        public const string SelectedDOBoolPropertyName = "SelectedDOBool";
        private bool _selectedDOBool = false;
        public bool SelectedDOBool
        {
            get { return _selectedDOBool; }
            set
            {
                if (_selectedDOBool == value) { return; }
                _selectedDOBool = value;
                RaisePropertyChanged(SelectedDOBoolPropertyName);
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

        public const string SelectedTypeBoolPropertyName = "SelectedTypeBool";
        private bool _selectedTypeBool = false;
        public bool SelectedTypeBool
        {
            get { return _selectedTypeBool; }
            set
            {
                if (_selectedTypeBool == value) { return; }
                _selectedTypeBool = value;
                RaisePropertyChanged(SelectedTypeBoolPropertyName);
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
