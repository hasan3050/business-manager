using System;
using System.Net;
using System.Linq;
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
using GalaSoft.MvvmLight.Command;
using System.ServiceModel.DomainServices.Client;
using TrialBusinessManager.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TrialBusinessManager.Views;
using GalaSoft.MvvmLight.Messaging;
using System.ComponentModel.DataAnnotations;


namespace TrialBusinessManager.ViewModels
{
    public class CreateInventoryViewModel:ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        ObservableCollection<Employee> SICEmployees = new ObservableCollection<Employee>();
        BusyWindow Busy = new BusyWindow();
        ObservableCollection<Employee> DOEmployees = new ObservableCollection<Employee>();
        public CreateInventoryViewModel()
        {

            CreateAllObjects();

            LoadRegionList();
            LoadEmployeeList();

            SaveCommand = new RelayCommand(Save);
            ResetCommant = new RelayCommand(Reset);
            ResetDOCommand = new RelayCommand(ResetDO);
            ResetSICCommand = new RelayCommand(ResetSIC);

        }

        private void CreateAllObjects()
        {

            newInventory = new Inventory();
            selectedRegion = new Region();
            selectedSIC = new Employee();
            selectedDO = new Employee();
        
        
        }

        void ResetSIC()
        {
            SelectedSIC = _context.Employees.Where(r => r.Designation == "Invalid" || r.UserName == "Invalid").First();
            MessageBox.Show("SIC has been reset!");
        }

        void ResetDO()
        {
            SelectedDO = _context.Employees.Where(r => r.Designation == "Invalid" || r.UserName == "Invalid").First();
            MessageBox.Show("DO has been reset!");
        }

        private void Save()
        {
          
            if (_context.Employees.Any(r => r.EmployeeId == selectedDO.EmployeeId) && _context.Employees.Any(t => t.EmployeeId == selectedSIC.EmployeeId))
            {
                if (String.IsNullOrEmpty(newInventory.InventoryName) || String.IsNullOrEmpty(newInventory.InventoryAddress))
                {
                    MessageBox.Show("Please provide necessary information to create an inventory!");
                    return;
                }
                if (_context.Inventories.Any(r => r.InventoryName==newInventory.InventoryName))
                {
                    MessageBox.Show("There is already an inventory with this name, Inventory name has to be unique!");
                    return;
                }

                if (_context.Inventories.Any(r => r.Employee.EmployeeId == selectedSIC.EmployeeId && r.Employee.Designation != "Invalid"))
                {
                    MessageBox.Show("Store In Charge already appointed to another inventory!");
                    return;
                }

                if (_context.Inventories.Any(r => r.Employee1.EmployeeId == selectedDO.EmployeeId  && r.Employee1.Designation != "Invalid"))
                {
                    MessageBox.Show("Dispatch Officer already appointed to another inventory!");
                    return;
                }
               
                newInventory.Employee = selectedSIC;
                newInventory.Employee1 = SelectedDO;
                newInventory.RegionId = InventoryMesseging.SentRegion.RegionId;
                newInventory.StoreInChargeId = selectedSIC.EmployeeId;
                newInventory.DispatchOfficerId = selectedDO.EmployeeId;

                newInventory.DateOfEstablished = DateTime.Now;

                Busy.Show();
                _context.Inventories.Add(newInventory);
                _context.SubmitChanges(OnSubmitCompleted, null);

            }
            else {

                MessageBox.Show("Please provide required information for creating an inventory");
            
            }
            
        
        }


        private void OnSubmitCompleted(SubmitOperation op)
        {
            Busy.Close();
            if (op.HasError)
            {
                MessageBox.Show("Failed,Please Try again!!");
                op.MarkErrorAsHandled();
                
                if ((newInventory.ValidationErrors != null) && (newInventory.ValidationErrors.Count > 0))
                {
                    foreach (ValidationResult result in newInventory.ValidationErrors)
                    {
                        string error = string.Format("Property [{0}] has problem [{1}]",
                          result.MemberNames.First(), // ?
                          result.ErrorMessage);

                        MessageBox.Show(error, "Error", MessageBoxButton.OK);
                    }
                }
                
                _context.RejectChanges();
            }
            else
            {
                MessageBox.Show("New Inventory Created");
                Messenger.Default.Send<string,CreateInventoryView>("message");
                Reset();

            }

        }

        private void Reset()
        {
            NewInventory = new Inventory();
            SelectedDO = new Employee();
            SelectedRegion = new Region();
            SelectedSIC = new Employee();
        
        }

        private void LoadEmployeeList()
        {
            _context.Load(_context.GetEmployeesQuery().Where(e => e.Designation == "Store In Charge" || e.Designation == "Dispatch Officer"||e.Designation == "Invalid")).Completed += (s, args) => {

                foreach (Employee x in _context.Employees)
                {
                    if (x.Designation == "Store In Charge")
                    {
                        SICEmployees.Add(x);

                    }
                    else if (x.Designation == "Dispatch Officer")
                    {
                        DOEmployees.Add(x);
                    }
                    
                }
            
            
            };
        }


        private void LoadRegionList()
        {
            _context.Load(_context.GetRegionsQuery(), k => { }, null);
        }



        public EntitySet<Region> Regions { get { return _context.Regions; } }

        public ObservableCollection<Employee> EmployeesSIC { get { return SICEmployees; } }
        public ObservableCollection<Employee> EmployeesDO { get { return DOEmployees; } }

        /// <summary>
        /// The <see cref="NewInventory" /> property's name.
        /// </summary>
        public const string NewInventoryPropertyName = "NewInventory";

        private Inventory newInventory;

        /// <summary>
        /// Sets and gets the NewInventory property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Inventory NewInventory
        {
            get
            {
                return newInventory;
            }

            set
            {
                if (newInventory == value)
                {
                    return;
                }

                newInventory = value;
                RaisePropertyChanged(NewInventoryPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SelectedRegion" /> property's name.
        /// </summary>
        public const string SelectedRegionPropertyName = "SelectedRegion";

        private Region selectedRegion;

        /// <summary>
        /// Sets and gets the SelectedRegion property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Region SelectedRegion
        {
            get
            {
                return selectedRegion;
            }

            set
            {
                if (selectedRegion == value)
                {
                    return;
                }

                selectedRegion = value;
         
         
                RaisePropertyChanged(SelectedRegionPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SelectedSIC" /> property's name.
        /// </summary>
        public const string SelectedSICPropertyName = "SelectedSIC";

        private Employee selectedSIC;

        /// <summary>
        /// Sets and gets the SelectedSIC property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Employee SelectedSIC
        {
            get
            {
                return selectedSIC;
            }

            set
            {
                if (selectedSIC == value)
                {
                    return;
                }

                selectedSIC = value;

   
                RaisePropertyChanged(SelectedSICPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SelectedDO" /> property's name.
        /// </summary>
        public const string SelectedDOPropertyName = "SelectedDO";

        private Employee selectedDO;

        /// <summary>
        /// Sets and gets the SelectedDO property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Employee SelectedDO
        {
            get
            {
                return selectedDO;
            }

            set
            {
                if (selectedDO == value)
                {
                    return;
                }

                selectedDO = value;
              

                RaisePropertyChanged(SelectedDOPropertyName);
            }
        }


        public ICommand SaveCommand { get; set; }
        public ICommand ResetCommant { get; set; }
        public ICommand ResetSICCommand { get; set; }
        public ICommand ResetDOCommand { get; set; }

    }
}
