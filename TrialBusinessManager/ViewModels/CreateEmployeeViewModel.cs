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
using System.Collections.ObjectModel;
using System.Collections.Generic;
using TrialBusinessManager.Web;
using GalaSoft.MvvmLight.Command;
using System.ServiceModel.DomainServices.Client;
using TrialBusinessManager.Models;
using TrialBusinessManager.Views;

namespace TrialBusinessManager.ViewModels
{
    public class CreateEmployeeViewModel:ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        bool RegionOnceSelected = false;
        ObservableCollection<String> designations = new ObservableCollection<string>();
        BusyWindow MyWindow = new BusyWindow();
        int flag = 0;
       
        public CreateEmployeeViewModel()
        {
            designations = new ObservableCollection<String>();
            designations = ConstantCollections.Designations;
            newEmployee = CreateNewEmployee();
            newPerson = CreateNewPerson();
            SaveCommand = new RelayCommand(Save);
            ResetCommand = new RelayCommand(Reset);
            MyWindow.Show();
            LoadEmployeeList();
            LoadRegionList();

        }

        private Person CreateNewPerson()
        {
            Person _person = new Person();
            _person.DateOfBirth = DateTime.Now;
            _person.DateOfJoin = DateTime.Now;
            return _person;
        
        }

        private Employee CreateNewEmployee()
        {
            Employee _employee = new Employee();
            _employee.Password = "1000";
           
            return _employee;
        
        
        }

        private void LoadEmployeeList()
        {
            _context.Load(_context.GetEmployeesQuery()).Completed += (s, e) =>
                {
                    flag++;
                    if (flag == 2)
                        MyWindow.Close();
                };
        }

        private void LoadRegionList()
        {
            _context.Load(_context.GetRegionsQuery()).Completed += (s, e) =>
            {
                flag++;
                if (flag == 2)
                    MyWindow.Close();
            };
        }
       

        private void OnSubmitCompleted(SubmitOperation op)
        {
            MyWindow.Close();
            if (op.HasError)
            {
                _context.RejectChanges();
                MessageBox.Show("Update failed,please enter required fields!");
                op.MarkErrorAsHandled();
            }
            else {
                MessageBox.Show("New User Created");
                Reset();
            
            }
        
        }


        private void Save()
        {
            if (_context.Employees.Any(e => e.UserName == newEmployee.UserName))
            {
                MessageBox.Show("User Name has to be unique.Please try a new user name!");
                return;
            }

            if (RegionOnceSelected == false)
                return;

            newEmployee.Designation = selectedDesignation;
            newEmployee.ActivityStatus = "Active";
            
            newEmployee.Region = selectedRegion;
            newEmployee.RegionId = selectedRegion.RegionId;
            newEmployee.Password = MD5Core.GetString(MD5Core.GetHash("1000"));

            newEmployee.Person=newPerson;
            MyWindow.Show();
            _context.Employees.Add(newEmployee);
            _context.SubmitChanges(OnSubmitCompleted, null);
        
        
        }

        private void Reset()
        {
            
            NewEmployee = CreateNewEmployee();
            NewPerson = CreateNewPerson();
        
        }


        private bool isExistingEmployee;
        public bool ISExistingEmployee
        {
            get {
                return isExistingEmployee;
            }
            set {

                if (isExistingEmployee == value)
                {

                    return;
                }
                if (value == false)
                {
                    NewPerson = CreateNewPerson();
                    SelectedPerson=CreateNewEmployee();
                }
              
                isExistingEmployee = value;
                RaisePropertyChanged("ISExistingEmployee");
               

            }

        }

        public ICommand SaveCommand { get; set; }

        public ICommand ResetCommand { get; set; }

        public ObservableCollection<String> Designations { get { return designations; } }

        public EntitySet<Region> Regions { get { return _context.Regions; } }

        public EntitySet<Employee>  Employees{ get { return _context.Employees; } }
        /// <summary>
        /// The <see cref="NewPerson" /> property's name.
        /// </summary>
        public const string NewPersonPropertyName = "NewPerson";

        private Person newPerson;

        /// <summary>
        /// Sets and gets the NewPerson property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Person NewPerson
        {
            get
            {
                return newPerson;;
            }

            set
            {
                if (newPerson == value)
                {
                    return;
                }

                newPerson = value;
                RaisePropertyChanged(NewPersonPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SelectedPerson" /> property's name.
        /// </summary>
        public const string SelectedPersonPropertyName = "SelectedPerson";

        private Employee selectedPerson ;
        
        /// <summary>
        /// Sets and gets the SelectedPerson property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Employee SelectedPerson
        {
            get
            {
                return selectedPerson;
            }

            set
            {
                if (selectedPerson == value)
                {
                    return;
                }

                try
                {
                    NewPerson = value.Person;
                    selectedPerson = value;
                    RaisePropertyChanged(SelectedPersonPropertyName);
                }
                catch (Exception ex)
                {
                }
            }
        }

        /// <summary>
        /// The <see cref="NewEmployee" /> property's name.
        /// </summary>
        public const string NewEmployeePropertyName = "NewEmployee";

        private Employee newEmployee ;

        /// <summary>
        /// Sets and gets the NewEmployee property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Employee NewEmployee
        {
            get
            {
                return newEmployee;
            }

            set
            {
                if (newEmployee == value)
                {
                    return;
                }

                newEmployee = value;
                RaisePropertyChanged(NewEmployeePropertyName);
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
                RegionOnceSelected = true;
                selectedRegion = value;
                RaisePropertyChanged(SelectedRegionPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SelectedDesignation" /> property's name.
        /// </summary>
        public const string SelectedDesignationPropertyName = "SelectedDesignation";

        private String selectedDesignation;

        /// <summary>
        /// Sets and gets the SelectedDesignation property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public String SelectedDesignation
        {
            get
            {
                return selectedDesignation;
            }

            set
            {
                if (selectedDesignation == value)
                {
                    return;
                }

                selectedDesignation = value;
                RaisePropertyChanged(SelectedDesignationPropertyName);
            }
        }

    }
}
