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
using GalaSoft.MvvmLight;
using TrialBusinessManager.Web;
using TrialBusinessManager.Views;
using System.Collections.ObjectModel;
using TrialBusinessManager.Models;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace TrialBusinessManager.ViewModels.NSM
{
    public class ViewRegionsViewModel:ViewModelBase
    {
        AgroDomainContext _context=new AgroDomainContext();
        BusyWindow Busy = new BusyWindow();
        int flag = 0;

        public ViewRegionsViewModel()
        {
            Messenger.Default.Register<string>(this,(s) => 
            {
                Regions.Clear();
                Busy.Show();
                LoadData();
            });
            Busy.Show();
            RegisterCommands();
            LoadData();
        }

        void RegisterCommands()
        {
            CreateInventoryCommand = new RelayCommand(CreateInventory);
            CreateRegionCommand = new RelayCommand(CreateRegion);
            EditRegionCommand = new RelayCommand(EditRegion);
            EditInventoryCommand = new RelayCommand(EditInventory);
        }

        void CreateRegion()
        {
            CreateRegionChildWindow obj = new CreateRegionChildWindow();
            obj.Show();
        }

        void EditRegion()
        {
        }

        void CreateInventory()
        {
            try
            {
                var SendRegion = SelectedRegion;

                InventoryMesseging.SentRegion = _context.Regions.Where(r => r.RegionId == SendRegion.Region.RegionId).Single();
                CreateInventoryView obj = new CreateInventoryView();
                obj.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Please select a region to allocated inventory !!");
            }
        }

        void EditInventory()
        {
            try
            {
                if (SelectedRegion.InventoryAddress=="n/a")
                {
                    MessageBox.Show("Currently there is no inventory allocated for selected region !!");
                    return;
                }
                EditInventoryInfoChildWindow obj = new EditInventoryInfoChildWindow(_context, _context.Inventories.Where(r => r.InventoryId.Equals(SelectedRegion.Inventory.InventoryId)).Single());
                obj.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please select a Region to edit inventory info !!");
            }
        }

        void LoadData()
        {
            Regions = new ObservableCollection<RegionsModel>();
            Regions.Clear();
            _context.Employees.Clear();
            
            _context.Load(_context.GetRegionsQuery()).Completed += (s, e) =>
                {
                    flag++;
                    if (flag == 3)
                    {
                        flag = 0;
                        Regions.Clear();
                        PopulateModel();
                        Busy.Close();
                    }
                };

            _context.Load(_context.GetInventoriesQuery()).Completed += (s, e) =>
                {
                    flag++;
                    if (flag == 3)
                    {
                        flag = 0;
                        Regions.Clear();
                        PopulateModel();
                        Busy.Close();
                    }
                };

            _context.Load(_context.GetInventoryPersonelsQuery()).Completed += (s, e) =>
                {
                    flag++;
                    if (flag == 3)
                    {
                        flag = 0;
                        Regions.Clear();
                        PopulateModel();
                        Busy.Close();
                    }
                };
        }

        void PopulateModel()
        {
            foreach (var Region in _context.Regions)
            {
                int flag = 0;
                
                foreach (var Inventory in _context.Inventories)
                {
                    if (Inventory.RegionId.Equals(Region.RegionId))
                    {
                        flag = 1;
                        RegionsModel Model = new RegionsModel();
                        Model.Inventory=new Inventory();
                        Model.Region=new Region();
                        Model.Inventory = Inventory;
                        Model.Region = Region;
                        long doid = Inventory.Employee1.EmployeeId;
                        long sicid = Inventory.Employee.EmployeeId;
                        try
                        {
                            Model.SIC = _context.Employees.Where(c => c.Designation.Equals("Store In Charge") && c.EmployeeId.Equals(sicid)).Single().Person.Name;
                        }
                        catch (Exception ex)
                        {
                            Model.SIC = "Unselected";
                        }
                        try
                        {
                            Model.DO = _context.Employees.Where(c => c.Designation.Equals("Dispatch Officer") && c.EmployeeId.Equals(doid)).Single().Person.Name;
                        }
                        catch (Exception ex)
                        {
                            Model.DO = "Unselected";
                        }
                        
                        Model.InventoryAddress = Inventory.InventoryAddress;
                        Regions.Add(Model);
                    }
                }

                if (flag == 0)
                {
                    RegionsModel Model = new RegionsModel();
                    Model.Inventory = new Inventory();
                    Model.Region = new Region();
                    Model.Region = Region;
                    Model.SIC ="N/A";
                    Model.DO = "N/A";
                    Model.InventoryAddress = "N/A";
                    Regions.Add(Model);
                }
            }
        }

        public ICommand CreateRegionCommand { get; set; }
        public ICommand CreateInventoryCommand { get; set; }
        public ICommand EditInventoryCommand { get; set; }
        public ICommand EditRegionCommand { get; set; }

        /// <summary>
        /// The <see cref="SelectedInfo" /> property's name.
        /// </summary>
        public const string SelectedInfoPropertyName = "SelectedRegion";

        private RegionsModel _selectedRegion  ;

        /// <summary>
        /// Gets the SelectedInfo property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public RegionsModel SelectedRegion
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
                RaisePropertyChanged(SelectedInfoPropertyName);

            }
        }
        /// <summary>
        /// The <see cref="Regions" /> property's name.
        /// </summary>
        public const string RegionsPropertyName = "Regions";

        private ObservableCollection<RegionsModel> _regions  ;

        /// <summary>
        /// Gets the Regions property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public ObservableCollection<RegionsModel> Regions
        {
            get
            {
                return _regions;
            }

            set
            {
                if (_regions == value)
                {
                    return;
                }

                var oldValue = _regions;
                _regions = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(RegionsPropertyName);

            }
        }
    }
}
