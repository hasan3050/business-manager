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
using GalaSoft.MvvmLight.Messaging;
using TrialBusinessManager.Views.SIC;
using TrialBusinessManager.Views;

namespace TrialBusinessManager.ViewModels.SIC
{
    public class ViewIndentForForwardingBySICViewModel : ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        ForwardIndentSICChildWindow detail;
        BusyWindow MyWindow = new BusyWindow();
        int flag = 0;

        public ICommand SearchCommand { get; set; }
        public ICommand ViewIndentCommand { get; set; }

        public ObservableCollection<Indent> IndentCollection { get { return new ObservableCollection<Indent>(_context.Indents.Where(e => e.IndentStatus == "Forwarded to SIC")); } }
        public EntitySet<Dealer> DealerCollection { get { return _context.Dealers; } }
        public EntitySet<Region> RegionCollection { get { return _context.Regions; } }
        
        public ViewIndentForForwardingBySICViewModel()
        {
            MyWindow.Show();
            LoadAll();
            SearchCommand = new RelayCommand(Search);
            ViewIndentCommand = new RelayCommand(ViewIndent);
        }

        #region Methods
        private void LoadAll()
        {
            LoadIndents();
            LoadDealers();
            LoadRegions();
        }

        private void Search()
        {            
            BindIndent.Clear();
            if (SelectedRegionBool && SelectedRegion != null)           BindIndent = new ObservableCollection<Indent>(IndentCollection.Where(e => e.Dealer.Region.RegionId == SelectedRegion.RegionId));
            else if (SelectedDealerBool && SelectedDealer != null)      BindIndent = new ObservableCollection<Indent>(IndentCollection.Where(e => e.IssuedById == SelectedDealer.DealerId));
            else if (SelectedDateBool)                                  BindIndent = new ObservableCollection<Indent>(IndentCollection.Where(e => e.DateOfPlace.Date >= StartDate.Date && e.DateOfPlace.Date <= EndDate.Date));
            else if (SelectedCodeBool && SelectedCode != null)          BindIndent = new ObservableCollection<Indent>(IndentCollection.Where(e => e.IndentCode == SelectedCode.IndentCode));
            else if (SelectedAllBool)                                   BindIndent = new ObservableCollection<Indent>(IndentCollection);
        }

        private void ViewIndent()
        {
            if (SelectedIndex > -1)
            {
                StaticMessaging.IndentMessage = BindIndent[SelectedIndex];
                detail = new ForwardIndentSICChildWindow();
                detail.Closed += new EventHandler(detail_Closed);
                detail.Show();
                return;
            }
            MessageBox.Show("Select an indent to forward","Invalid Selection",MessageBoxButton.OK);
        }

        void detail_Closed(object sender, EventArgs p)
        {
            if (StaticMessaging.SICIndentVerificationSubmitted == true)
            {
                Indent indent = BindIndent.Where(e => e.IndentId == StaticMessaging.IndentMessage.IndentId).SingleOrDefault();
                BindIndent.Remove(indent);
                StaticMessaging.IndentMessage = new Indent();
                StaticMessaging.SICIndentVerificationSubmitted = false;
            }
        }

        private void LoadIndents()
        {
            LoadOperation<Indent> LoadIndent = _context.Load(_context.GetIndentsQuery().Where(e => e.IndentStatus == "Forwarded to SIC" && e.ForwardedToInventoryId == UserInfo.Inventory.InventoryId), k => { }, null);
            LoadIndent.Completed += (s, e) =>
            {
                flag++;
                if (flag == 3)
                    MyWindow.Close(); 
                BindIndent = new ObservableCollection<Indent>(IndentCollection);
            };
        }

        private void LoadDealers()
        {
            _context.Load(_context.GetDealersQuery().Where(e => e.RegionId == UserInfo.Region.RegionId), k => { }, null).Completed += (s, e) =>
                {
                    flag++;
                    if (flag == 3)
                        MyWindow.Close();
                };
        }

        private void LoadRegions()
        {
            _context.Load(_context.GetRegionsQuery()).Completed += (s, e) =>
                {
                    flag++;
                    if (flag == 3)
                        MyWindow.Close();
                };
        }
        #endregion

        #region Properties

        public const string SelectedDealerPropertyName = "SelectedDealer";
        private Dealer _selectedDealer = new Dealer();
        public Dealer SelectedDealer
        {
            get { return _selectedDealer; }
            set
            {
                if (_selectedDealer == value) { return; }

                _selectedDealer = value;
                RaisePropertyChanged(SelectedDealerPropertyName);
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

        public const string SelectedCodePropertyName = "SelectedCode";
        private Indent _selectedCode;
        public Indent SelectedCode
        {
            get { return _selectedCode; }
            set
            {
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

        public const string BindIndentPropertyName = "BindIndent";
        private ObservableCollection<Indent> _bindIndent = new ObservableCollection<Indent>();
        public ObservableCollection<Indent> BindIndent
        {
            get { return _bindIndent; }
            set
            {
                if (_bindIndent == value) { return; }

                _bindIndent = value;
                RaisePropertyChanged(BindIndentPropertyName);
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
        #endregion
    }
}
