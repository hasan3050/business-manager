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
using GalaSoft.MvvmLight.Messaging;

namespace TrialBusinessManager.ViewModels
{
    public class ViewProductViewModel : ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        ProductDetailWindow detail;
        ProductEditWindow Edit;
        BusyWindow MyWindow = new BusyWindow();
        public ICommand SearchCommand { get; set; }
        public ICommand ViewDetailCommand { get; set; }
        public ICommand ViewEditCommand { get; set; }

        public EntitySet<Product> ProductCollection { get { return _context.Products; } }
        public List<string> WingCollection { get { return ConstantCollections.ProductWingName.ToList(); } }
        public List<string> TypeCollection { get { return ConstantCollections.ProductTypeName.ToList(); } }
       
        public ViewProductViewModel()
        {
            Messenger.Default.Register<String>(this, OnReject);
            SearchCommand = new RelayCommand(Search);
            ViewDetailCommand = new RelayCommand(ViewDetail);
            ViewEditCommand = new RelayCommand(ViewEdit);
            MyWindow.Show();
            LoadProducts();
        }

        #region Methods

        private void OnReject(String s)
        {
            if (s == "Rejected")
            {
                _context.RejectChanges();
            }
        }
        
        private void ViewDetail()
        {
            if (SelectedIndex > -1)
            {
                detail = new ProductDetailWindow();
                Messenger.Default.Send<AgroDomainContext>(_context);
                Messenger.Default.Send<Product>(BindProduct[SelectedIndex]);
                detail.Show();
                return;
            }
            MessageBox.Show("Select a product to view", "Invalid Selection", MessageBoxButton.OK);
        }

        private void ViewEdit()
        {
            if (SelectedIndex > -1)
            {
                Edit = new ProductEditWindow();
                Messenger.Default.Send<AgroDomainContext>(_context);
                Messenger.Default.Send<Product>(BindProduct[SelectedIndex]);
                
                Edit.Show();
                return;
            }
            MessageBox.Show("Select a product to view", "Invalid Selection", MessageBoxButton.OK);
        }

        private void Search()
        {
            BindProduct.Clear();
            if (SelectedStatusBool && !string.IsNullOrEmpty(SelectedStatus))    BindProduct = ProductCollection.Where(e => e.ProductStatus == SelectedStatus).ToList();
            else if (SelectedDateBool)                                          BindProduct = ProductCollection.Where(e => e.IntroducedDate.Date >= StartDate.Date && e.IntroducedDate.Date <= EndDate.Date).ToList();
            else if (SelectedWingBool && SelectedWing != null)                  BindProduct = ProductCollection.Where(e => e.ProductWing == SelectedWing).ToList();
            else if (SelectedTypeBool && SelectedType != null)                  BindProduct = ProductCollection.Where(e => e.ProductType == SelectedType).ToList();
            else if (SelectedNameBool && SelectedName != null)                  BindProduct = ProductCollection.Where(e => e.ProductName == SelectedName.ProductName).ToList();
            else if (SelectedCodeBool && SelectedCode != null)                  BindProduct = ProductCollection.Where(e => e.ProductCode == SelectedCode.ProductCode).ToList();
            else if (SelectedAllBool)                                           BindProduct = ProductCollection.ToList();
        }

        private void LoadProducts()
        {
            _context.Load(_context.GetProductsQuery().Where(c=> c.ProductStatus.Equals("Active")), k => { }, null).Completed += (s, e) => 
            {
                BindProduct = ProductCollection.Where(c=>c.ProductName!="DUMMY").ToList();
                MyWindow.Close();
            };
        }
        #endregion

        #region Properties

        public const string ProductStatusPropertyName = "ProductStatus";
        private ObservableCollection<string> _ProductStatus = ConstantCollections.ProductStatusType;
        public ObservableCollection<string> ProductStatus
        {
            get { return _ProductStatus; }
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

        public const string SelectedWingPropertyName = "SelectedWing";
        private string _selectedWing;
        public string SelectedWing
        {
            get { return _selectedWing; }
            set
            {
                if (_selectedWing == value) { return; }

                _selectedWing = value;
                RaisePropertyChanged(SelectedWingPropertyName);
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

        public const string SelectedNamePropertyName = "SelectedName";
        private Product _selectedName;
        public Product SelectedName
        {
            get { return _selectedName; }
            set
            {
                if (_selectedName == value) { return; }

                _selectedName = value;
                RaisePropertyChanged(SelectedNamePropertyName);
            }
        }

        public const string SelectedCodePropertyName = "SelectedCode";
        private Product _selectedCode;
        public Product SelectedCode
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

        public const string BindProductPropertyName = "BindProduct";
        private List<Product> _bindProduct = new List<Product>();
        public List<Product> BindProduct
        {
            get { return _bindProduct; }
            set
            {
                if (_bindProduct == value) { return; }

                _bindProduct = value;
                RaisePropertyChanged(BindProductPropertyName);
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

        public const string SelectedWingBoolPropertyName = "SelectedWingBool";
        private bool _selectedWingBool = false;
        public bool SelectedWingBool
        {
            get { return _selectedWingBool; }
            set
            {
                if (_selectedWingBool == value) { return; }
                _selectedWingBool = value;
                RaisePropertyChanged(SelectedWingBoolPropertyName);
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

        public const string SelectedNameBoolPropertyName = "SelectedNameBool";
        private bool _selectedNameBool = false;
        public bool SelectedNameBool
        {
            get { return _selectedNameBool; }
            set
            {
                if (_selectedNameBool == value) { return; }
                _selectedNameBool = value;
                RaisePropertyChanged(SelectedNameBoolPropertyName);
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
