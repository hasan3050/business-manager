using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TrialBusinessManager.Web;
using TrialBusinessManager.Models;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.ServiceModel.DomainServices.Client;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Messaging;
using TrialBusinessManager.Views;
using TrialBusinessManager.Views.RM;

namespace TrialBusinessManager.ViewModels.RM
{
    public class AssignSalesTargetViewModel:ViewModelBase
    {
        ObservableCollection<SalesOfficerTarget> NewTargets = new ObservableCollection<SalesOfficerTarget>();

        AgroDomainContext _context = new AgroDomainContext();
        BusyWindow busyWindow = new BusyWindow();
        Product targetProduct = new Product();
        RegionTarget regionTarget = new RegionTarget(); 
        int LoadFlag = 0;

        public AssignSalesTargetViewModel()
        {
            regionTarget = StaticMessaging.TargetMessage;

            selectedTarget = regionTarget.TargetQuantity.ToString();
            productname = regionTarget.Product.GroupName;

            busyWindow.Show();
            Initialize();
            LoadSalesOfficer();
            LoadRegionTargetData();

        
        }

        #region methods
        void Initialize()
        {

            _context.SalesOfficerTargets.Clear();
            isEnabled = true;
            SubmitCommand = new RelayCommand(Submit);
            

        }

       


        void LoadSalesOfficer()
        {
            _context.Load(_context.GetEmployeesQuery().Where(e => e.RegionId == UserInfo.Region.RegionId)).Completed += (r, t) =>
            {
                LoadFlag++;

                if (LoadFlag == 2)
                {
                    SetSalesOfficersNames();
                    InitializeNewRegionTargets();
                    busyWindow.Close();
                }

            };

        }

        void LoadRegionTargetData()
        {


            TargetCollection.Clear();
            NewTargets.Clear();

            busyWindow.Show();

            _context.Load(_context.GetSalesOfficerTargetsQuery().Where(e => e.SeasonId == regionTarget.SeasonId).Where(r => r.Employee.RegionId == regionTarget.RegionId).Where(t => t.ProductId == regionTarget.Product.ProductId)).Completed += (s, e) =>
            {

                LoadFlag++;
               
                foreach (SalesOfficerTarget _target in _context.SalesOfficerTargets.Where(t => t.SeasonId == regionTarget.SeasonId))
                {
                    TargetModel target = new TargetModel();
                    target.SalesOfficerTarget = new SalesOfficerTarget();
                    target.SalesOfficerTarget = _target;
                  
                   // target.Name = _context.Employees.Where(g => g.EmployeeId == _target.SalesOfficerId).First().Person.Name;
                    TargetCollection.Add(target);
                }

                if (LoadFlag == 2)
                {
                    SetSalesOfficersNames();
                    InitializeNewRegionTargets();
                    busyWindow.Close();
                }

                //TargetCollection.OrderBy(r => r.Product.GroupName);

            };
        }

        void SetSalesOfficersNames()
        {
            foreach (TargetModel my_target in TargetCollection)
            {
                my_target.Name = _context.Employees.Where(g => g.EmployeeId == my_target.SalesOfficerTarget.SalesOfficerId).First().Person.Name;
            }
        
        }


        //Initialize region targets for selected regions and seasons with each product groups
        void InitializeNewRegionTargets()
        {

            foreach (Employee salesOfficer in _context.Employees.Where(e => e.RegionId == UserInfo.Region.RegionId && e.Designation == "Sales Officer"))
            {
                if (!TargetCollection.Any(r => r.SalesOfficerTarget.Employee == salesOfficer))
                {
                    TargetModel newtargetgrid = new TargetModel();
                    newtargetgrid.SalesOfficerTarget = new SalesOfficerTarget();

                    SalesOfficerTarget newTarget = new SalesOfficerTarget();
                    newTarget.Product = regionTarget.Product;
                    // newTarget.ProductId = regionTarget.Product.ProductId;
                    newTarget.Season = regionTarget.Season;
                    newTarget.SalesOfficerId = salesOfficer.EmployeeId;

                    newTarget.AchievedAmount = 0;
                    newTarget.DistributedQuantity = 0;
                    newTarget.SalesReturnQuantity = 0;
                    newTarget.TargetQuantity = 0;
                    newTarget.TransportationLossQuantity = 0;

                    newtargetgrid.SalesOfficerTarget = newTarget;
                    newtargetgrid.Name = salesOfficer.Person.Name;

                    TargetCollection.Add(newtargetgrid);
                    NewTargets.Add(newTarget);
                }
            }
         
        }

        //negative or null target quantity entires are replaced with 0s
        void ValidateInput()
        {
            foreach (TargetModel _target in TargetCollection)
            {
                if (!(_target.SalesOfficerTarget.TargetQuantity > 0))
                {
                    _target.SalesOfficerTarget.TargetQuantity = 0;
                }
            }
        }

        void Submit()
        {
            ValidateInput();

            //exsisting entries are updated 
            foreach (SalesOfficerTarget _target in _context.SalesOfficerTargets.Where(e => e.Season == regionTarget.Season && e.Product == regionTarget.Product).Where(r => r.Employee.Region == regionTarget.Region))
            {

                _target.TargetQuantity = TargetCollection.Where(e => e.SalesOfficerTarget.Product == _target.Product).Where(r => r.SalesOfficerTarget.Employee.Region == regionTarget.Region).First().SalesOfficerTarget.TargetQuantity;
            }

            //new entries are inserted
            foreach (SalesOfficerTarget _target in NewTargets)
            {
                SalesOfficerTarget newTarget = new SalesOfficerTarget();

                newTarget.ProductId = _target.Product.ProductId;
                newTarget.SeasonId = _target.Season.SeasonId;
                newTarget.SalesOfficerId = _target.SalesOfficerId;
                newTarget.AchievedAmount = _target.AchievedAmount;
                newTarget.DistributedQuantity = _target.DistributedQuantity;
                newTarget.TargetQuantity = _target.TargetQuantity;
                newTarget.TransportationLossQuantity = _target.TransportationLossQuantity;
                newTarget.SalesReturnQuantity = _target.SalesReturnQuantity;
                _context.SalesOfficerTargets.Add(newTarget);

                //_context.SalesOfficerTargets.Add(_target);
            }

            busyWindow.Show();

            _context.SubmitChanges(OnSubmitCompleted, null);


        }

        void OnSubmitCompleted(SubmitOperation op)
        {
            if (op.HasError)
            {
                MessageBox.Show("Some error occured! Please close this window and refresh the page before trying again!");
                return;
            }

            isEnabled = false;
            MessageBox.Show("Successfully updated. Please close this window now to continue.");
            busyWindow.Close();
            
        }
        #endregion


        #region Properties

        public ICommand DistributeCommand { get; set; }
        public ICommand SubmitCommand { get; set; }
       

        private String productname=" ";

        public String ProductName
        {
            get
            {
                return productname;
            }

            set
            {
                if (productname == value)
                {
                    return;
                }

                productname = value;
                RaisePropertyChanged("ProductName");
            }
        }

        private String selectedTarget=" ";

        public String SelectedTarget
        {
            get
            {
                return selectedTarget;
            }

            set
            {
                if (SelectedTarget == value)
                {
                    return;
                }

                selectedTarget = value;
                RaisePropertyChanged("SelectedTarget");
            }
        }

        private bool isEnabled;

        public bool IsEnabled
        {
            get
            {
                return isEnabled;
            }

            set
            {
                if (isEnabled == value)
                {
                    return;
                }

                isEnabled = value;
                RaisePropertyChanged("IsEnabled");
            }
        }


        public const string TargetCollectionPropertyName = "TargetCollection";
        private ObservableCollection<TargetModel> _targetCollection = new ObservableCollection<TargetModel>();
        public ObservableCollection<TargetModel> TargetCollection
        {
            get { return _targetCollection; }
            set
            {
                if (_targetCollection == value) { return; }

                _targetCollection = value;
                RaisePropertyChanged(TargetCollectionPropertyName);
            }
        }


        #endregion

        #region Classes

        //Model to bind collection with ui datagrid
       
        #endregion
    }
}
