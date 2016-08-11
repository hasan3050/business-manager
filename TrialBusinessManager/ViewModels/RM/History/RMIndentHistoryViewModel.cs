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
using GalaSoft.MvvmLight.Messaging;
using TrialBusinessManager.Views;

namespace TrialBusinessManager.ViewModels.RM.History
{
    public class RMIndentHistoryViewModel:ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        public ICommand SearchCommand { get; set; }
        public ICommand ViewIndentCommand { get; set; }
        BusyWindow Busy = new BusyWindow();
        int flag = 0;
        
        public EntitySet<Indent> IndentCollection {get {return _context.Indents;}}
        public EntitySet<Dealer> DealerCollection {get {return _context.Dealers;}}
        
        IndentDetailChildWindow detail;
        
        public RMIndentHistoryViewModel()
        {
            SearchCommand = new RelayCommand(Search);
            ViewIndentCommand = new RelayCommand(ViewIndent);
            Busy.Show();
            LoadAll();      
        }
        
        #region Methods
        private void Search()
        {
            BindIndent.Clear();
            if (SelectedDealerBool && SelectedDealer != null) 
                BindIndent = IndentCollection.Where(e => e.IssuedById == SelectedDealer.DealerId).ToList();

            else if (SelectedStatusBool && !string.IsNullOrEmpty(SelectedStatus)) 
                BindIndent = IndentCollection.Where(e => e.IndentStatus == SelectedStatus).ToList();

            else if (SelectedDateBool) 
                BindIndent = IndentCollection.Where(e => e.DateOfPlace.Date >= StartDate.Date && e.DateOfPlace.Date <= EndDate.Date).ToList();

            else if (SelectedCodeBool && SelectedCode != null) 
                BindIndent = IndentCollection.Where(e => e.IndentCode == SelectedCode.IndentCode).ToList();

            else if (SelectedAllBool == true) 
                BindIndent = IndentCollection.Where(e => e.Dealer.RegionId == UserInfo.Region.RegionId).ToList();
        }

        private void ViewIndent()
        {
            if (SelectedIndex > -1)
            {
                StaticMessaging.IndentMessage = BindIndent[SelectedIndex];
                detail = new IndentDetailChildWindow();
                detail.Show();
                return;
            }
            MessageBox.Show("Select an indent to view", "Invalid Selection", MessageBoxButton.OK);
        }

        private void LoadAll()
        {
            LoadDealers();
            
        }

        private void LoadIndents()
        {
            _context.Load(_context.GetIndentsQuery().Where(e => e.Dealer.RegionId == UserInfo.Region.RegionId && e.IndentCode != "Unknown")).Completed += (s, e) => { BindIndent = IndentCollection.ToList(); flag++; if (flag == 2) Busy.Close(); };
        }

        private void LoadDealers()
        {
            _context.Load(_context.GetDealersQuery().Where(e => e.RegionId == UserInfo.Region.RegionId)).Completed += (s, e) => { LoadIndents(); flag++; if (flag == 2) Busy.Close(); };
        }
        #endregion

        #region Properties

        public const string IndentStatusPropertyName = "IndentStatus";
        private ObservableCollection<string> _indentStatus = ConstantCollections.IndentStatusType;
        public ObservableCollection<string> IndentStatus
        {
            get { return _indentStatus; }
        }

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
        
        public const string SelectedStatusPropertyName = "SelectedStatus";
        private string _selectedStatus ;
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
        private DateTime _startDate=DateTime.Now;
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
        private DateTime _endDate=DateTime.Now;
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
        private List<Indent> _bindIndent = new List<Indent>();
        public List<Indent> BindIndent
        {
            get { return _bindIndent; }
            set
            {
                if (_bindIndent == value) { return; }

                _bindIndent = value;
                RaisePropertyChanged(BindIndentPropertyName);
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
        #endregion
    }
}
