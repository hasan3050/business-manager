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
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using TrialBusinessManager.Models;
using TrialBusinessManager.Views;

namespace TrialBusinessManager.ViewModels
{
    public class CreateExpenditureViewModel:ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        BusyWindow busy = new BusyWindow();
        ObservableCollection<ExpenditureInfoModel> expenditureInfoCollection = new ObservableCollection<ExpenditureInfoModel>();
     
        public CreateExpenditureViewModel()
        {
            Expenditure x = new Expenditure();
            CreateAll();
            busy.Show();
            LoadEmployee();
        }

        void CreateAll()
        {
            selectedEmployee = new Employee();
            selectedEmployee.EmployeeId = -1;
            DeleteSelected = new RelayCommand(Delete);
            Add = new RelayCommand(ADDEmployeeToCollection);
            SaveCommand = new RelayCommand(Save);
            ResetCommand = new RelayCommand(Reset);
            selectedEx = -1;
        }

        void Reset()
        {
            CreateAll();
            expenditureInfoCollection.Clear();
            Code = null;
            SelectedEmployee = new Employee();
            
        }

        void Save()
        {
            _context.Expenditures.Clear();
            var NewExpenditure = new Expenditure();
            Double TotalPlacedAmount=0;
            NewExpenditure.DateOfIssue = DateTime.Now;
            NewExpenditure.PlacedById = UserInfo.EmployeeID;
            NewExpenditure.PlacedToId = UserInfo.EmployeeID;
            NewExpenditure.ExpenditureCode = DateTime.Now.ToString();
         //   NewExpenditure.Region = UserInfo.Region;
            NewExpenditure.RegionId = UserInfo.Region.RegionId;
            NewExpenditure.Status = "Pending";
            if (expenditureInfoCollection.Count == 0)
            {
                _context.RejectChanges();
                return;
            }

            for (int i = 0; i < expenditureInfoCollection.Count; i++)
            { 
                var selected=expenditureInfoCollection.ElementAt(i);
                if (selected.SelectedRemark == "Not")
                {
                    MessageBox.Show("Please select remark for each input!");
                    return;
                }
                if (selected.ExpenditureInfo.PlacedAmount <= 0)
                {
                    MessageBox.Show("Amount can not be zero or negative!!");
                    return;
                
                }
                ExpenditureInfo upExInfo = new ExpenditureInfo();
                upExInfo.NSMAcceptedAmount = 0;
                upExInfo.AccountAcceptedAmount = 0;
                upExInfo.PlacedAmount = selected.ExpenditureInfo.PlacedAmount;
                upExInfo.Remarks = selected.SelectedRemark;
              //  upExInfo.Employee = selected.ExpenditureInfo.Employee;
                upExInfo.SalesOfficerId = selected.ExpenditureInfo.SalesOfficerId;
                TotalPlacedAmount += upExInfo.PlacedAmount;
                NewExpenditure.ExpenditureInfoes.Add(upExInfo);

            }
            NewExpenditure.TotalAcceptedAmount = 0;
            NewExpenditure.TotalPlacedAmount = TotalPlacedAmount;
          
            if (NewExpenditure.HasValidationErrors == false)
            {
                _context.Expenditures.Add(NewExpenditure);
                _context.SubmitChanges(OnSubmitCompleted, null);
                busy.Show();
               
            }
            else
            {

                MessageBox.Show("Please provide required information");
            }

        }

        private void OnSubmitCompleted(SubmitOperation op)
        {
         

            if (op.HasError)
            {
                //  MessageBox.Show(op.Error.ToString());
                op.MarkErrorAsHandled();
            }
            else
            {
                MessageBox.Show("Expenditure placed");
                busy.Close();
                Reset();

            }

        }

        void LoadEmployee()
        {

            _context.Load(_context.GetEmployeesQuery().Where(e=>e.Designation=="Sales Officer"&&e.Region.RegionId==UserInfo.Region.RegionId)).Completed+=(s,args)=>{busy.Close();};
       
        }

        void ADDEmployeeToCollection()
        {
            try
            {
                if (selectedEmployee.EmployeeId == -1)
                    return;

                ExpenditureInfoModel Exinfo = new ExpenditureInfoModel();
                Exinfo.ExpenditureInfo = new ExpenditureInfo();
                Exinfo.Remarks = new ObservableCollection<string> { "Travelling", "Food", "Dealer Expense", "Others" };
                Exinfo.Name = selectedEmployee.Person.Name;
                //  Exinfo.ExpenditureInfo.Employee = selectedEmployee;
                Exinfo.ExpenditureInfo.SalesOfficerId = selectedEmployee.EmployeeId;
                Exinfo.SelectedRemark = "Not";
                Exinfo.ExpenditureInfo.PlacedAmount = 0;
                //    Exinfo.ExpenditureInfo.AcceptedAmount = 0;
                expenditureInfoCollection.Add(Exinfo);
            }
            catch(Exception ex)
            {
            }
           
        }


        private void Delete() //to delete product from datagrid
        {
            if (selectedEx > -1)
            {

                expenditureInfoCollection.RemoveAt(selectedEx);


            }


        }


        public ObservableCollection<ExpenditureInfoModel> ExpenditureInfoCollection { get { return expenditureInfoCollection; } }
        public EntitySet<Employee> Employees { get { return _context.Employees; } }


        /// <summary>
        /// The <see cref="SelectedEmployee" /> property's name.
        /// </summary>
        public const string SelectedEmployeePropertyName = "SelectedEmployee";

        private Employee selectedEmployee ;

        /// <summary>
        /// Sets and gets the SelectedEmployee property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Employee SelectedEmployee
        {
            get
            {
                return selectedEmployee;
            }

            set
            {
                if (selectedEmployee == value)
                {
                    return;
                }

                selectedEmployee = value;
                RaisePropertyChanged(SelectedEmployeePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SelectedEx" /> property's name.
        /// </summary>
        public const string SelectedExPropertyName = "SelectedEx";

        private int selectedEx ;

        /// <summary>
        /// Sets and gets the SelectedEx property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int SelectedEx
        {
            get
            {
                return selectedEx;
            }

            set
            {
                if (selectedEx == value)
                {
                    return;
                }

                selectedEx = value;
                RaisePropertyChanged(SelectedExPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Code" /> property's name.
        /// </summary>
        public const string CodePropertyName = "Code";

        private String code;

        /// <summary>
        /// Sets and gets the Code property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public String Code
        {
            get
            {
                return code;
            }

            set
            {
                if (code == value)
                {
                    return;
                }

                code = value;
                RaisePropertyChanged(CodePropertyName);
            }
        }

        public ICommand DeleteSelected { get; set; }
        public ICommand Add { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand ResetCommand { get; set; }
        
    }
}
