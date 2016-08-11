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
using TrialBusinessManager.Views.Admin;

namespace TrialBusinessManager.ViewModels
{
    public class ViewPackageViewModel : ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        public ICommand SearchCommand { get; set; }
        public ICommand Edit { get; set; }
        int flag = 0;
        BusyWindow Busy = new BusyWindow();

        public EntitySet<Package> PackageCollection { get { return _context.Packages; } }
       
        public ViewPackageViewModel()
        {
            SearchCommand = new RelayCommand(Search);
            Edit = new RelayCommand(EditPackage);
            Busy.Show();
            LoadPackages();
            LoadEmployees();
        }

        #region Methods
        private void Search()
        {
            BindPackage.Clear();
            if (SelectedStatusBool && !string.IsNullOrEmpty(SelectedStatus)) BindPackage = PackageCollection.Where(e => e.PackageStatus == SelectedStatus).ToList();
            else if (SelectedDateBool) BindPackage = PackageCollection.Where(e => e.IntroducedDate.Date >= StartDate.Date && e.IntroducedDate.Date <= EndDate.Date).ToList();
            else if (SelectedNameBool && SelectedName != null) BindPackage = PackageCollection.Where(e => e.PackageName == SelectedName.PackageName).ToList();
            else if (SelectedCodeBool && SelectedCode != null) BindPackage = PackageCollection.Where(e => e.PackageCode == SelectedCode.PackageCode).ToList();
            else if (SelectedAllBool)                          BindPackage = PackageCollection.ToList();
        }

        private void LoadPackages()
        {
            _context.Load(_context.GetPackagesQuery(), k => { }, null).Completed += (s, e) => 
            {
                BindPackage = PackageCollection.ToList();
                flag++;
                if (flag == 2)
                    Busy.Close();
            };
        }
        void EditPackage()
        {
            if (String.IsNullOrEmpty(SelectedPackage.PackageName))
            {
                MessageBox.Show("Please select a package!");
                return;
            }
            PackageEditWindow obj = new PackageEditWindow(_context,SelectedPackage);
            obj.Show();
        }

        private void LoadEmployees()
        {
            LoadOperation<Employee> LoadEmployee = _context.Load(_context.GetEmployeesQuery().Where(e => e.Designation == "National Sales Manager"), k =>
            {
                flag++;
                if (flag == 2)
                    Busy.Close();
            }, null);
        }

        #endregion

        #region Properties

        public const string PackageStatusPropertyName = "PackageStatus";
        private ObservableCollection<string> _PackageStatus = ConstantCollections.PackageStatusType;
        public ObservableCollection<string> PackageStatus
        {
            get { return _PackageStatus; }
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

        public const string SelectedNamePropertyName = "SelectedName";
        private Package _selectedName;
        public Package SelectedName
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
        private Package _selectedCode;
        public Package SelectedCode
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

        public const string BindPackagePropertyName = "BindPackage";
        private List<Package> _bindPackage = new List<Package>();
        public List<Package> BindPackage
        {
            get { return _bindPackage; }
            set
            {
                if (_bindPackage == value) { return; }

                _bindPackage = value;
                RaisePropertyChanged(BindPackagePropertyName);
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

        private Package _selectedPackage = new Package();
        public Package SelectedPackage
        {
            get { return _selectedPackage; }
            set
            {
                if (_selectedPackage == value) { return; }
                _selectedPackage = value;
                RaisePropertyChanged("SelectedPackage");
            }
        }

        

        #endregion
    }
}
