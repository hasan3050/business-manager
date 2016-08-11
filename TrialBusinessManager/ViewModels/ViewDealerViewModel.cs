using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using TrialBusinessManager.Web;
using GalaSoft.MvvmLight;
using System.ServiceModel.DomainServices.Client;
using TrialBusinessManager.Views;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Linq;
using TrialBusinessManager.Models;
using TrialBusinessManager.Views.RM;
using System.Windows.Data;
using TrialBusinessManager.Views.Co_ordinator;
using TrialBusinessManager.Views.Accountant;

namespace TrialBusinessManager.ViewModels
{
    public class ViewDealerViewModel : ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        ObservableCollection<ViewDealerModel> DealerModels = new ObservableCollection<ViewDealerModel>();
        public ViewDealerViewModel()
        {
            int flag = 0;
            BusyWindow Loading = new BusyWindow();
            IsLoading = true;
            Loading.Show();

            if (UserInfo.Designation =="Regional Manager")
            {
                _context.Load(_context.GetDealersQuery().Where(c => c.Region.RegionName.Equals(UserInfo.Region.RegionName)&&c.ActivityStatus!="Pending")).Completed += (s, e) =>
                {
                    foreach (Dealer x in _context.Dealers)
                    {
                        ViewDealerModel NewDealerModel = new ViewDealerModel();
                        NewDealerModel.Dealer = x;
                        NewDealerModel.Due = x.TotalDue - x.TotalCollection;
                        DealerModels.Add(NewDealerModel);
                    
                    }
                    Dealers = new PagedCollectionView(DealerModels);
                    flag++;
                    if (flag == 2)
                    {
                        Loading.Close();
                        IsLoading = false;
                    }
                    
                };

                _context.Load(_context.GetEmployeesQuery().Where(c => c.Designation == "Sales Officer" && c.Region.RegionName == UserInfo.RegionName)).Completed+=(s,e)=>
                {
                    flag++;
                    if (flag == 2)
                    {
                        Loading.Close();
                        IsLoading = false;
                    }
                };
            }
            else
            {
                _context.Load(_context.GetDealersQuery()).Completed += (s, e) =>
                {
                    foreach (Dealer x in _context.Dealers)
                    {
                        ViewDealerModel NewDealerModel = new ViewDealerModel();
                        NewDealerModel.Dealer = x;
                        NewDealerModel.Due = x.TotalDue - x.TotalCollection;
                        DealerModels.Add(NewDealerModel);

                    }
                    Dealers = new PagedCollectionView(DealerModels);
                    flag++;
                    if (flag == 3)
                    {
                        Loading.Close();
                        IsLoading = false;
                    }
                };

                 _context.Load(_context.GetRegionsQuery()).Completed += (s, e) =>
                {
                    flag++;
                    if (flag == 3)
                    {
                        Loading.Close();
                        IsLoading = false;
                    }
                };

                 _context.Load(_context.GetEmployeesQuery().Where(c => c.Designation.Equals("Sales Officer"))).Completed += (s, e) =>
                     {
                         flag++;
                         if (flag == 3)
                         {
                             Loading.Close();
                             IsLoading = false;
                         }
                     };
            }
            
            SelectCommand = new RelayCommand(() =>
            {
                if (SavedDealer != null)
                {
                /*    if (SavedDealer.ActivityStatus != "Active"&&Messages.SelectionType!="BP")
                    {
                        MessageBox.Show("You must select an active dealer!");
                        return;
                    }*/
                    Messages.SelectionType = "N";
                    Messenger.Default.Send<Dealer>(SavedDealer);
                  
                }
                else
                {
                    MessageBox.Show("Null!!");
                }
            });

            SelectionChangedCommand = new RelayCommand(
                () =>
                {
                    try
                    {
                        ChooseEnabled = true;
                        SavedDealer = new Dealer();
                        SavedDealer = SelectedDealer.Dealer;
                    }
                    catch (Exception ex)
                    { }
                });
            
            IssueSalesReturn = new RelayCommand(issueSalesReturn);
            DetailCommand = new RelayCommand(viewDealerDetail);
            RegionFilterCommand = new RelayCommand(FilterDealersByRegion);
            NoFilterCommmnad = new RelayCommand(NoFilter);
            EditCommand = new RelayCommand(EditDealer);
            AdvanceCommand = new RelayCommand(AdvanceDeposit);
        }
        void EditDealer()
        {
            if (UserInfo.Designation == "Admin")
            {
                try
                {
                    EditDealerChildWindow obj = new EditDealerChildWindow(SelectedDealer.Dealer, _context);
                    obj.Show();
                }
                catch (Exception ex)
                {
                    return;
                }
            }
            else if (UserInfo.Designation == "Co-ordinator")
            {
                try
                {
                    CODealerEditChildWindow obj = new CODealerEditChildWindow(SelectedDealer.Dealer, _context);
                    obj.Show();
                }
                catch (Exception ex)
                {
                    return;
                }
            }
        }

        void NoFilter()
        {
            Dealers.Filter = null;
        }

        void FilterDealersByRegion()
        {
            Dealers.Filter = null;
            Dealers.Filter = new Predicate<object>((obj) =>
            {
                var Dealer = obj as ViewDealerModel;
                    return (Dealer.Dealer != null && Dealer.Dealer.Region.Equals(SelectedRegion));
            });
        }

        void viewDealerDetail()
        {
            if (SelectedDealer != null)
            {
                DealerDetailWindow Window = new DealerDetailWindow(SelectedDealer.Dealer, _context);
                Window.Show();
            }
            else
            {
                MessageBox.Show("please select a dealer!!!");
                return;
            }
        }
        
        void issueSalesReturn()
        {
            //MessageBox.Show(SelectedDealer.Name);
            IssueSalesReturnChildWindow obj = new IssueSalesReturnChildWindow(_context,SelectedDealer.Dealer);
            obj.Show();
        }

        void AdvanceDeposit()
        {

            if (SelectedDealer.Dealer.CompanyName == null)
            {
                MessageBox.Show("Please select a dealer first to issue advanced deposit.");
                return;
            }
            IssueCommissionChildWindow obj = new IssueCommissionChildWindow(SelectedDealer.Dealer);
            obj.Show();
        }

        public ICommand IssueSalesReturn { get; set; }
        public ICommand DetailCommand { get; set; }
        public ICommand SelectCommand { get; set; }
        public ICommand SelectionChangedCommand { get; set; }
        public ICommand RegionFilterCommand { get; set; }
        public ICommand NoFilterCommmnad { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand AdvanceCommand { get; set; }
        public EntitySet<Region> Regions { get { return _context.Regions; } }

        /// <summary>
        /// The <see cref="SelectedRegion" /> property's name.
        /// </summary>
        public const string SelectedRegionPropertyName = "SelectedRegion";

        private Region _selectedRegion  ;

        /// <summary>
        /// Gets the SelectedRegion property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public Region SelectedRegion
        {
            get
            {
                return _selectedRegion;
            }

            set
            {
                if (_selectedRegion == value)
                {
                    return;
                }

                var oldValue = _selectedRegion;
                _selectedRegion = value;

               // Update bindings, no broadcast
                RaisePropertyChanged(SelectedRegionPropertyName);

               
            }
        }

        /// <summary>
        /// The <see cref="MyVisibility" /> property's name.
        /// </summary>
        public const string MyVisibilityPropertyName = "MyVisibility";

        private bool _myVisibility  ;

        /// <summary>
        /// Gets the MyVisibility property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public bool MyVisibility
        {
            get
            {
                return _myVisibility;
            }

            set
            {
                if (_myVisibility == value)
                {
                    return;
                }

                var oldValue = _myVisibility;
                _myVisibility = value;

                
                // Update bindings, no broadcast
                RaisePropertyChanged(MyVisibilityPropertyName);
            }
        }
       
        
        /// <summary>
        /// The <see cref="SavedDealer" /> property's name.
        /// </summary>
        public const string SavedDealerPropertyName = "SavedDealer";

        private Dealer _savedDealer  ;

        /// <summary>
        /// Sets and gets the SavedDealer property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Dealer SavedDealer
        {
            get
            {
                return _savedDealer;
            }

            set
            {
                if (_savedDealer == value)
                {
                    return;
                }

                _savedDealer = value;
                RaisePropertyChanged(SavedDealerPropertyName);
            }
        }


        //telecast message
        void SelectDealer()
        {
           
           // MessageBox.Show(SavedDealer.DealerId.ToString());
            Messenger.Default.Send<Dealer>(SavedDealer);
            Messenger.Default.Send<String>("ViewDealerProfile");
            
        }

        /// <summary>
        /// The <see cref="IsLoading" /> property's name.
        /// </summary>
        public const string IsLoadingPropertyName = "IsLoading";

        /// <summary>
        /// The <see cref="Dealers" /> property's name.
        /// </summary>
        public const string DealersPropertyName = "Dealers";

        private PagedCollectionView _dealer  ;

        /// <summary>
        /// Gets the Dealers property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public PagedCollectionView Dealers
        {
            get
            {
                return _dealer;
            }

            set
            {
                if (_dealer == value)
                {
                    return;
                }

                var oldValue = _dealer;
                _dealer = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(DealersPropertyName);
            }
        }

        private bool _isLoading;

        /// <summary>
        /// Sets and gets the IsLoading property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }

            set
            {
                if (_isLoading == value)
                {
                    return;
                }

                _isLoading = value;
                RaisePropertyChanged(IsLoadingPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SelectedDealer" /> property's name.
        /// </summary>
        public const string SelectedDealerPropertyName = "SelectedDealer";

        private ViewDealerModel _selectedDealer;

        /// <summary>
        /// Sets and gets the SelectedDealer property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ViewDealerModel SelectedDealer
        {
            get
            {
                return _selectedDealer;
            }

            set
            {
                if (_selectedDealer == value)
                {
                    return;
                }

                try
                {
                    ChooseEnabled = true;
                    SavedDealer = new Dealer();
                    SavedDealer = _selectedDealer.Dealer;
                }
                catch (Exception ex)
                { 
                    
                }

                _selectedDealer = value;
                RaisePropertyChanged(SelectedDealerPropertyName);
                
            }
        }

        /// <summary>
        /// The <see cref="ChooseEnabled" /> property's name.
        /// </summary>
        public const string ChooseEnabledPropertyName = "ChooseEnabled";

        private bool _chooseEnabled = false;

        /// <summary>
        /// Sets and gets the ChooseEnabled property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool ChooseEnabled
        {
            get
            {
                return _chooseEnabled;
            }

            set
            {
                if (_chooseEnabled == value)
                {
                    return;
                }

                _chooseEnabled = value;
                RaisePropertyChanged(ChooseEnabledPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="FinalDealer" /> property's name.
        /// </summary>
        public const string FinalDealerPropertyName = "FinalDealer";

        private Dealer _finalDealer;

        /// <summary>
        /// Sets and gets the FinalDealer property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Dealer FinalDealer
        {
            get
            {
                return _finalDealer;
            }

            set
            {
                if (_finalDealer == value)
                {
                    return;
                }

                _finalDealer = value;
                RaisePropertyChanged(FinalDealerPropertyName);
            }
        }

        public class ViewDealerModel:ViewModelBase
        {
            private Dealer _Dealer;

            /// <summary>
            /// Sets and gets the PLRPackageInfo property.
            /// Changes to that property's value raise the PropertyChanged event. 
            /// </summary>
            public Dealer Dealer
            {
                get
                {
                    return _Dealer;
                }

                set
                {
                    if (_Dealer == value)
                    {
                        return;
                    }

                    _Dealer = value;
                    RaisePropertyChanged("Dealer");
                }
            }

            private double _Due;

            /// <summary>
            /// Sets and gets the PLRPackageInfo property.
            /// Changes to that property's value raise the PropertyChanged event. 
            /// </summary>
            public Double Due
            {
                get
                {
                    return _Due;
                }

                set
                {
                    if (_Due == value)
                    {
                        return;
                    }

                    _Due = value;
                    RaisePropertyChanged("Due");
                }
            }
              
        }
    }
}
