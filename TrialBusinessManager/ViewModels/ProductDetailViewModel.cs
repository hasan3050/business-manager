using System.Windows;
using GalaSoft.MvvmLight;
using TrialBusinessManager.Web;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using TrialBusinessManager.Models;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using System;
using TrialBusinessManager.Views;

namespace TrialBusinessManager.ViewModels
{
    public class ProductDetailViewModel:ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        BusyWindow MyWindow = new BusyWindow();
        
        public ProductDetailViewModel()
        {
            Initialize();
        }

        void Initialize()
        {
            ActivityStatus=new ObservableCollection<string>();
            ActivityStatus=ConstantCollections.ActivityStatus;
            _commissionCollection = new ObservableCollection<Commission>();
            Periods = new ObservableCollection<string>();
            Periods = ConstantCollections.TimePeriodName;
            PaymentTypes = new ObservableCollection<string>();
            PaymentTypes = ConstantCollections.CommissionName;
            Messenger.Default.Register<AgroDomainContext>(this, OnContextReceived);
            Messenger.Default.Register<Product>(this, OnProductReceived);
            
            AddCommissionCommand = new RelayCommand(AddCommission);
            RemoveCommissionCommand = new RelayCommand(RemoveCommission);
            SubmitChangesCommand = new RelayCommand(SubmitChanges);
        }

        void OnContextReceived(AgroDomainContext Context)
        {
            _context = Context;
        }

        void OnProductReceived(Product DetailProduct)
        {
            
            Product = new Product();
            Product = DetailProduct;
            SetCommissions();

        }
        void SetCommissions()
        {
            foreach (Commission x in _context.Commissions.Where(e => e.Product.GroupName == Product.GroupName && e.Product.ProductName == "DUMMY"))
            {
                _commissionCollection.Add(x);
            }
        }

        void SubmitChanges()
        {
           // if (Product.HasChanges)
            {
                MyWindow.Show();
                _context.SubmitChanges(OnSubmitCompleted,null);
            }
        }

        
        void OnSubmitCompleted(SubmitOperation so)
        {
            MyWindow.Close();
            if (so.HasError)
            {
                MessageBox.Show("update failed,please try again!!");
            }
            else
            {
                Messenger.Default.Send<String>("Updated");
            }
        }

        void RemoveCommission()
        {
            if (SelectedCommission == null)
            {
                MessageBox.Show("Please select a commission to remove!!");
                return;
            }
            else
            {
                Product.Commissions.Remove(SelectedCommission);
            }
        }

        void AddCommission()
        {
            if (SelectedPaymentType == null)
            {
                MessageBox.Show("Please select a payment type to add a commission");
                return;
            }
            else
            {
                if (Product.Commissions.Any(c => c.CommissionName.Equals(SelectedPaymentType)))
                {
                    MessageBox.Show("this Commission is already added!!");
                    return;
                }
                else
                {
                    Commission Commission = new Commission();
                    Commission.CommissionName = SelectedPaymentType;
                    Commission.CommissionStatus = "Active";
                    Commission.IntroducedDate = DateTime.Now;
                    Commission.NSMId = UserInfo.EmployeeID;
                    Commission.AdminId = UserInfo.EmployeeID;
                    Product.Commissions.Add(Commission);
                }
            }
        }


        public ICommand AddCommissionCommand { get; set; }
        public ICommand RemoveCommissionCommand { get; set; }
        public ICommand SubmitChangesCommand { get; set; }

        /// <summary>
        /// The <see cref="SelectedCommission" /> property's name.
        /// </summary>
        public const string SelectedCommissionPropertyName = "SelectedCommission";

        private Commission _selectedCommission  ;

        /// <summary>
        /// Gets the SelectedCommission property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public Commission SelectedCommission
        {
            get
            {
                return _selectedCommission;
            }

            set
            {
                if (_selectedCommission == value)
                {
                    return;
                }

                var oldValue = _selectedCommission;
                _selectedCommission = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(SelectedCommissionPropertyName);

            }
        }

        /// <summary>
        /// The <see cref="ActivityStatus" /> property's name.
        /// </summary>
        public const string ActivityStatusPropertyName = "ActivityStatus";

        private ObservableCollection<String> _activityStatus  ;

        /// <summary>
        /// Gets the ActivityStatus property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public ObservableCollection<String> ActivityStatus
        {
            get
            {
                return _activityStatus;
            }

            set
            {
                if (_activityStatus == value)
                {
                    return;
                }

                var oldValue = _activityStatus;
                _activityStatus = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(ActivityStatusPropertyName);

            }
        }

        private ObservableCollection<Commission> _commissionCollection;
        /// <summary>
        /// Gets the SelectedPaymentType property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public ObservableCollection<Commission> CommissionCollection
        {
            get
            {
                return _commissionCollection;
            }

            set
            {
                if (_commissionCollection == value)
                {
                    return;
                }

                var oldValue = _commissionCollection;
                _commissionCollection = value;

                // Update bindings, no broadcast
                RaisePropertyChanged("CommissionCollection");

            }
        }


        /// <summary>
        /// The <see cref="SelectedPaymentType" /> property's name.
        /// </summary>
        public const string SelectedPaymentTypePropertyName = "SelectedPaymentType";

        private string _selectedPaymentType  ;

        /// <summary>
        /// Gets the SelectedPaymentType property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public string SelectedPaymentType
        {
            get
            {
                return _selectedPaymentType;
            }

            set
            {
                if (_selectedPaymentType == value)
                {
                    return;
                }

                var oldValue = _selectedPaymentType;
                _selectedPaymentType = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(SelectedPaymentTypePropertyName);

            }
        }

        /// <summary>
        /// The <see cref="PaymentTypes" /> property's name.
        /// </summary>
        public const string PaymentTypesPropertyName = "PaymentTypes";

        private ObservableCollection<string> _paymentTypes  ;

        /// <summary>
        /// Gets the PaymentTypes property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public ObservableCollection<string> PaymentTypes
        {
            get
            {
                return _paymentTypes;
            }

            set
            {
                if (_paymentTypes == value)
                {
                    return;
                }

                var oldValue = _paymentTypes;
                _paymentTypes = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(PaymentTypesPropertyName);

            }
        }

        /// <summary>
        /// The <see cref="Periods" /> property's name.
        /// </summary>
        public const string PeriodsPropertyName = "Periods";

        private ObservableCollection<string> _periods ;

        /// <summary>
        /// Gets the Periods property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public ObservableCollection<string> Periods
        {
            get
            {
                return _periods;
            }

            set
            {
                if (_periods == value)
                {
                    return;
                }

                var oldValue = _periods;
                _periods = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(PeriodsPropertyName);

            }
        }

        public const string ProductPropertyName = "Product";


        private Product _product  ;
        public Product Product
        {
            get
            {
                return _product;
            }

            set
            {
                if (_product == value)
                {
                    return;
                }

                var oldValue = _product;
                _product = value;
                RaisePropertyChanged(ProductPropertyName);
            }
        }
    }
}
