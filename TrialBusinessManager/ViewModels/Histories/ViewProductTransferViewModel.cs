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

namespace TrialBusinessManager.ViewModels.Histories
{
    public class ViewProductTransferViewModel : ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        private ProductTransferDetailChildWindow detail;

        private int flag = 0,count;
        public ICommand SearchCommand { get; set; }
        public ICommand ViewProductTransferCommand { get; set; }
        private BusyWindow Busy = new BusyWindow();
        public EntitySet<ProductTransfer> ProductTransferCollection { get { return _context.ProductTransfers; } }
        public EntitySet<Inventory> InventoryCollection { get { return _context.Inventories; } }
        public EntitySet<Employee> EmployeeCollection { get { return _context.Employees; } }

        public ViewProductTransferViewModel()
        {
            IsEnabled = checkDesignation();
            count = 2;
            SearchCommand = new RelayCommand(Search);
            ViewProductTransferCommand = new RelayCommand(ViewProductTransfer);
            LoadAll();
        }


        #region Methods

        public List<StaticMessaging.ProductTransferForBind> CreateList(List<ProductTransfer> Collection)
        {
            List<StaticMessaging.ProductTransferForBind> BindTransfer = new List<StaticMessaging.ProductTransferForBind>();
            foreach (ProductTransfer temp in Collection)
            {
                StaticMessaging.ProductTransferForBind newTransfer = new StaticMessaging.ProductTransferForBind();
                newTransfer.Transfer = temp;
                newTransfer.InventoryToName = _context.Inventories.Where(o => o.InventoryId == temp.RecievedToInventoryId).SingleOrDefault().InventoryName;
                if (temp.RecievedBySICId <= 0)  newTransfer.SICName = "Unknown";
                else                            newTransfer.SICName = _context.Employees.Where(o => o.EmployeeId == temp.RecievedBySICId).SingleOrDefault().Person.Name;
                BindTransfer.Add(newTransfer);
            }
            return BindTransfer;
        }

        bool checkDesignation()
        {
            if (UserInfo.Designation == "Admin" || UserInfo.Designation == "Accountant" || UserInfo.Designation == "Viewer" || UserInfo.Designation == "National Sales Manager" || UserInfo.Designation == "Store In Charge" || UserInfo.Designation == "Dispatch Officer")
                return true;
            return false;
        }

        void Search()
        {
            try
            {
                _context.ProductTransfers.Clear();
                Busy.Show();

                LoadOperation<ProductTransfer> LoadProductTransfers;
                if (IsEnabled) LoadProductTransfers = _context.Load(_context.GetProductTransferHistoryQuery(SelectedInventoryFromBool, SelectedInventoryToBool, SelectedCodeBool, SelectedStatusBool, SelectedDateBool, SelectedInventoryFrom.InventoryId, SelectedInventoryTo.InventoryId, SelectedCode, SelectedStatus, StartDate, EndDate.AddDays(1)));
                else LoadProductTransfers = _context.Load(_context.GetProductTransferHistoryQuery(true, true, SelectedCodeBool, SelectedStatusBool, SelectedDateBool, UserInfo.Inventory.InventoryId, UserInfo.Inventory.InventoryId, SelectedCode, SelectedStatus, StartDate, EndDate.AddDays(1)));
                LoadProductTransfers.Completed += (s, e) =>
                {
                    BindProductTransfer = CreateList(LoadProductTransfers.Entities.ToList());
                    Busy.Close();
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadAll()
        {
            Busy.Show();
            LoadInventories();
            LoadEmployees();
        }

        

        private void LoadInventories()
        {
             _context.Load(_context.GetInventoriesQuery(), k => { }, null).Completed += (s, e) =>
                {
                    flag++;
                    if (flag == count)
                    {
                        BindProductTransfer = CreateList(ProductTransferCollection.ToList());
                        Busy.Close();
                    }
                };
            
        }

        private void LoadEmployees()
        {
            _context.Load(_context.GetEmployeesQuery().Where(e => e.Designation == "Dispatch Officer" || e.Designation == "Store In Charge"), k => { }, null).Completed += (s, e) =>
            {
                flag++;
                if (flag == count)
                {
                    BindProductTransfer = CreateList(ProductTransferCollection.ToList());
                    Busy.Close();
                }
            };
        }

        private void ViewProductTransfer()
        {
            if (SelectedIndex > -1)
            {
                StaticMessaging.ProductTransferMessage = BindProductTransfer[SelectedIndex];
                detail = new ProductTransferDetailChildWindow();
                detail.Show();
                return;
            }
            MessageBox.Show("Select a bill to view", "Invalid Selection", MessageBoxButton.OK);
        }
        #endregion

        #region Properties

        private ObservableCollection<string> _producttransferStatus = new ObservableCollection<string> {"Dispatched", "Verified"};
        public ObservableCollection<string> PTStatus
        {
            get { return _producttransferStatus; }
        }

        public const string SelectedStatusPropertyName = "SelectedStatus";
        private string _selectedStatus = "";
        public string SelectedStatus
        {
            get { return _selectedStatus; }
            set
            {
                if (_selectedStatus == value) { return; }
                if (value == null) value = "";
                _selectedStatus = value;
                RaisePropertyChanged(SelectedStatusPropertyName);
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

        public const string SelectedInventoryFromPropertyName = "SelectedInventoryFrom";
        private Inventory _selectedInventoryFrom = new Inventory();
        public Inventory SelectedInventoryFrom
        {
            get { return _selectedInventoryFrom; }
            set
            {
                if (_selectedInventoryFrom == value) { return; }
                if(value==null) value = new Inventory();
                _selectedInventoryFrom = value;
                RaisePropertyChanged(SelectedInventoryFromPropertyName);
            }
        }

        public const string SelectedInventoryToPropertyName = "SelectedInventoryTo";
        private Inventory _selectedInventoryTo = new Inventory();
        public Inventory SelectedInventoryTo
        {
            get { return _selectedInventoryTo; }
            set
            {
                if (_selectedInventoryTo == value) { return; }
                if (value == null) value = new Inventory();
                _selectedInventoryTo = value;
                RaisePropertyChanged(SelectedInventoryToPropertyName);
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

        public const string BindProductTransferPropertyName = "BindProductTransfer";
        private List<StaticMessaging.ProductTransferForBind> _bindProductTransfer = new List<StaticMessaging.ProductTransferForBind>();
        public List<StaticMessaging.ProductTransferForBind> BindProductTransfer
        {
            get { return _bindProductTransfer; }
            set
            {
                if (_bindProductTransfer == value) { return; }

                _bindProductTransfer = value;
                RaisePropertyChanged(BindProductTransferPropertyName);
            }
        }

        public const string SelectedInventoryFromBoolPropertyName = "SelectedInventoryFromBool";
        private bool _selectedInventoryFromBool = false;
        public bool SelectedInventoryFromBool
        {
            get { return _selectedInventoryFromBool; }
            set
            {
                if (_selectedInventoryFromBool == value) { return; }
                _selectedInventoryFromBool = value;
                RaisePropertyChanged(SelectedInventoryFromBoolPropertyName);
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

        public const string SelectedInventoryToBoolPropertyName = "SelectedInventoryToBool";
        private bool _selectedInventoryToBool = false;
        public bool SelectedInventoryToBool
        {
            get { return _selectedInventoryToBool; }
            set
            {
                if (_selectedInventoryToBool == value) { return; }
                _selectedInventoryToBool = value;
                RaisePropertyChanged(SelectedInventoryToBoolPropertyName);
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
