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

namespace TrialBusinessManager.ViewModels.NSM
{
    public class SetTargetViewModel:ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        int load_flag = 0;
        
        bool regionselected = false;
        bool seasonselected = false;
        
        long loadedSeasonId = 0;
        long LoadedRegionId = 0;

        ObservableCollection<RegionTarget> NewTargets = new ObservableCollection<RegionTarget>();
        BusyWindow busyWindow = new BusyWindow();
        
        public SetTargetViewModel()
        {
            Initialize();
            busyWindow.Show();
            LoadInitialData();    
        }

        void Initialize()
        {
            
            SubmitCommand = new RelayCommand(Submit);
            ShowTargetCommand = new RelayCommand(LoadRegionTargetData);
            EditTargetCommand = new RelayCommand(EditTarget);
            ResetCommand = new RelayCommand(Reset);
           
        }

        #region Mehods
        //Load Seasons and regions to initialize 
        void LoadInitialData()
        {
          
            _context.Load(_context.GetSeasonsQuery()).Completed += (s, e) =>
             {
                 load_flag++;
                 if (load_flag == 3)
                     busyWindow.Close();
             };
            
            _context.Load(_context.GetRegionsQuery()).Completed += (w, e) =>
            {
                load_flag++;
                if (load_flag == 3)
                    busyWindow.Close();
            };

            _context.Load(_context.GetProductsQuery().Where(e => e.ProductName == "DUMMY")).Completed += (r, e) =>
            {
                load_flag++;
                if (load_flag == 3)
                    busyWindow.Close();
            };

        }

        //Load Region Target data when view target command is executed
        void LoadRegionTargetData()
        {
            if (!regionselected || !seasonselected)
            {
                MessageBox.Show("Please select Season and Region before loading target data!");
                return;
            }

            TargetCollection.Clear();
            NewTargets.Clear();

            busyWindow.Show();

            _context.Load(_context.GetRegionTargetsQuery().Where(e => e.SeasonId == SelectedSeason.SeasonId).Where(r => r.RegionId == SelectedRegion.RegionId)).Completed += (s, e) =>
             {
                 LoadedRegionId = SelectedRegion.RegionId;
                 loadedSeasonId = SelectedSeason.SeasonId;

                 busyWindow.Close();

                 foreach (RegionTarget _target in _context.RegionTargets.Where(t => t.SeasonId == SelectedSeason.SeasonId).Where(d => d.RegionId == SelectedRegion.RegionId))
                 {
                     TargetCollection.Add(_target);
                 }


                 InitializeNewRegionTargets();
                 TargetCollection.OrderBy(r => r.Product.GroupName);
                 
             };
        }

        //Not implemented yet
        void EditTarget()
        {
            
        }

        //Initialize region targets for selected regions and seasons with each product groups
        void InitializeNewRegionTargets()
        {
           
            foreach (Product _product in _context.Products)
            {
                if (!TargetCollection.Any(r => r.Product == _product))
                {
                    RegionTarget newTarget = new RegionTarget();
                    newTarget.Product= _product;
                    newTarget.RegionId = SelectedRegion.RegionId;
                    newTarget.SeasonId = SelectedSeason.SeasonId;

                    newTarget.AchievedAmount = 0;
                    newTarget.DistributedAmount = 0;
                    newTarget.DistributedQuantity = 0;
                    newTarget.SalesReturnQuantity = 0;
                    newTarget.TransportationLossQuantity = 0;
                    newTarget.SalesPeriod = "N/A";
                    newTarget.TargetQuantity = 0;

                    TargetCollection.Add(newTarget);
                    NewTargets.Add(newTarget);
                }
            }
        }

        //negative or null target quantity entires are replaced with 0s
        void ValidateInput()
        {
            foreach (RegionTarget _target in TargetCollection)
            {
                if (!(_target.TargetQuantity > 0))
                {
                    _target.TargetQuantity = 0;
                }

                if (_target.SalesPeriod == null)
                    _target.SalesPeriod = "N/A";
            }
        }


        void Submit()
        {

            ValidateInput();

            //exsisting entries are updated 
            foreach (RegionTarget _target in _context.RegionTargets.Where(t => t.SeasonId == loadedSeasonId).Where(d => d.RegionId == LoadedRegionId))
            {
               _target.TargetQuantity = TargetCollection.Where(e => e.Product == _target.Product).First().TargetQuantity;
            }
            
            //new entries are inserted
            foreach (RegionTarget _target in NewTargets)
            {
                _context.RegionTargets.Add(_target);
            }

            busyWindow.Show();
            
            _context.SubmitChanges().Completed += new EventHandler(SetTargetViewModel_Completed);
        
        
        }

        void SetTargetViewModel_Completed(object sender, EventArgs e)
        {
            busyWindow.Close();
        }


        void Reset()
        {
            TargetCollection.Clear();
            NewTargets.Clear();

        }
        #endregion 


        #region Properties

        public ICommand SubmitCommand { get; set; }
        public ICommand ShowTargetCommand { get; set; }
        public ICommand EditTargetCommand { get; set; }
        public ICommand ResetCommand { get; set; }

        public EntitySet<Season> SeasonCollection { get { return _context.Seasons; } }

        public EntitySet<Region> RegionCollection { get { return _context.Regions; } }

        public const string SelectedIndentPropertyName = "SelectedSeason";
        private Season _selectedSeason= new Season();
        public Season SelectedSeason
        {
            get { return _selectedSeason; }
            set
            {
                if (_selectedSeason == value) { return; }
                seasonselected = true;
                _selectedSeason = value;
                RaisePropertyChanged(SelectedIndentPropertyName);
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
                regionselected = true;
                RaisePropertyChanged(SelectedRegionPropertyName);
            }
        }

        private bool _editableFlag = true;
        public bool EditableFlag
        {
            get { return _editableFlag; }
            set
            {
                if (_editableFlag == value) { return; }

                _editableFlag = value;

                RaisePropertyChanged("EditableFlag");
            }
        }
        

        public const string TargetCollectionPropertyName = "TargetCollection";
        private ObservableCollection<RegionTarget> _targetCollection = new ObservableCollection<RegionTarget>();
        public ObservableCollection<RegionTarget> TargetCollection
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

    }
}
