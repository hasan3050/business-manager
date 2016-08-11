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
    public class SetTargetViewModel : ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        int load_flag = 0;

        bool targetselected = false;
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

            DistributeCommand = new RelayCommand(Distrubute);
            ShowTargetCommand = new RelayCommand(LoadRegionTargetData);
            ResetCommand = new RelayCommand(Reset);

        }

        #region Methods
        //Load Seasons and regions to initialize 
        void LoadInitialData()
        {

            _context.Load(_context.GetSeasonsQuery()).Completed += (s, e) =>
            {
                load_flag++;
                if (load_flag == 2)
                    busyWindow.Close();
            };

          
            _context.Load(_context.GetProductsQuery().Where(e => e.ProductName == "DUMMY")).Completed += (r, e) =>
            {
                load_flag++;
                if (load_flag == 2)
                    busyWindow.Close();
            };

        }

        //Load Region Target data when view target command is executed
        void LoadRegionTargetData()
        {
            if (!seasonselected)
            {
                MessageBox.Show("Please select Season before loading target data!");
                return;
            }

            TargetCollection.Clear();
            NewTargets.Clear();

            busyWindow.Show();

            _context.Load(_context.GetRegionTargetsQuery().Where(e => e.SeasonId == SelectedSeason.SeasonId).Where(r => r.RegionId == UserInfo.Region.RegionId)).Completed += (s, e) =>
            {
                LoadedRegionId = UserInfo.Region.RegionId;
                loadedSeasonId = SelectedSeason.SeasonId;

                busyWindow.Close();

                foreach (RegionTarget _target in _context.RegionTargets.Where(t => t.SeasonId == SelectedSeason.SeasonId).Where(d => d.RegionId == UserInfo.Region.RegionId))
                {
                    TargetCollection.Add(_target);
                }


                TargetCollection.OrderBy(r => r.Product.GroupName);

            };
        }

        
        void Distrubute()
        {
            if (!targetselected)
            {
                MessageBox.Show("Please select a target to distribute!");
                return;
            }

            StaticMessaging.TargetMessage = SelectedTarget;
            AssignSalesTarget childWindow = new AssignSalesTarget();
            childWindow.Show();

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

        public ICommand DistributeCommand { get; set; }
        public ICommand ShowTargetCommand { get; set; }
        public ICommand EditTargetCommand { get; set; }
        public ICommand ResetCommand { get; set; }

        public EntitySet<Season> SeasonCollection { get { return _context.Seasons; } }

        public EntitySet<Region> RegionCollection { get { return _context.Regions; } }

        public const string SelectedIndentPropertyName = "SelectedSeason";
        private Season _selectedSeason = new Season();
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

        public const string SelectedRegionPropertyName = "SelectedTarget";
        private RegionTarget _selectedTarget = new RegionTarget();
        public RegionTarget SelectedTarget
        {
            get { return _selectedTarget; }
            set
            {
                if (_selectedTarget == value) { return; }

                _selectedTarget = value;
                targetselected = true;
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
