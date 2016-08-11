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
using GalaSoft.MvvmLight.Command;
using System.ServiceModel.DomainServices.Client;
using System.ServiceModel;
using System.Collections.ObjectModel;
using TrialBusinessManager.Models;
using System.Linq;
using System.Collections.Generic;
using TrialBusinessManager.Views;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrialBusinessManager.ViewModels.RM
{
    public class CreateDealerViewModel : ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        Employee InChargeSO = new Employee();
        BusyWindow MyWindow = new BusyWindow();
        
        
        
        //Constructor
        public CreateDealerViewModel()
        {
            PaymentTypes = new ObservableCollection<string>();
            BusinessTypes = new ObservableCollection<string>();
            Initialize();
            LoadSalesOfficers();
            BlankDealer = CreateBlankDealer();
        }

        //Create a new blank dealer to bind to UI
        public Dealer CreateBlankDealer()
        {
            //Preset properties
            var dealer = new Dealer(){
                                       
                                        CreditLimit = 0,
                                        TotalDue = 0,
                                     
                                        DateOfAdminApproval = DateTime.Now,
                                        DateOfBirth=DateTime.Now,
                                   
                                        ActivityStatus = "Pending",
                                       
                                        TotalCollection=0,
                                        ExpectedDefaulterDate=DateTime.Now.AddYears(50)
                                     };

            return dealer;
        }

        private void OnSubmitCompleted(SubmitOperation so)
        {
            MyWindow.Close();
            if (so.HasError)
            {
                MessageBox.Show("Dealer creation failed");
                _context.RejectChanges();
                so.MarkErrorAsHandled();
                return;
            }
            else
            {
               Reset();
            }
        }

        public void Reset()
        {
            BlankDealer= CreateBlankDealer();
        }

        void Initialize()
        {
            PaymentTypes = ConstantCollections.CommissionName;
            BusinessTypes = ConstantCollections.BuisnessTypes;
            SaveCommand = new RelayCommand(Save);
            ResetCommand = new RelayCommand(Reset);
        }

        public void Save()
        {
            _context.Dealers.Clear();

            try
            {
                blankDealer.SalesOfficerId = SalesOfficer.EmployeeId;
                blankDealer.RegionId = UserInfo.Region.RegionId;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please input required fields!!!");
                return;
            }
            
          //  MessageBox.Show("Name:" + blankDealer.Name + "\n" + "Fname:" + blankDealer.FatherName + "\n" + "Mname:"  + "PresentAddr:" + blankDealer.PresentAddress + "\n" + "PermanentAddr:" + blankDealer.PermamentAddress + "\n" + "Nationality:" + blankDealer.Nationality + "\n" + "Date of birth:" + blankDealer.DateOfBirth.ToString() + "\n" + "Contactno:" + blankDealer.ContactNo + "\n" + "EmailAddress:" + blankDealer.EmailAddress + "\n" + "PictureLink:" + blankDealer.PictureLink + "\n" + "Companyname:" + blankDealer.CompanyName + "\n" + "CompanyAddr:" + blankDealer.CompanyAddress + "\n" + "Licsenseno:" + blankDealer.LicenseNo + "\n" + "LIssueDate:" + blankDealer.LicenseIssueDate.ToString() + "\n" + "BusinessType:" + blankDealer.BusinessType + "\n" + "Ownername:" + blankDealer.OwnerName + "\n" + "OwnerAddess:" + blankDealer.OwnerAddress + "\n" + "Payment:" + blankDealer.PreferredTypeOfPayment + "\n" + "has another dealership:" + blankDealer.HasAnotherDealership.ToString() + "\n" + "dealcompany:" + blankDealer.DealershipCompany + "\n" + "Hasownoffice:" + blankDealer.HasOwnOffice.ToString() + "\n" + "regdate:" + blankDealer.DateOfRegistration.ToString() + "\n" + "Creditlitmit:" + blankDealer.CreditLimit.ToString() + "\n" + "DefaultDate:" + blankDealer.ExpectedDefaulterDate.ToString() + "\n" + "Activitystat:" + blankDealer.ActivityStatus + "\n" + "totaldue:" + blankDealer.TotalDue.ToString() + "\n" + "username:" + blankDealer.UserName + "\n" + "Pass:" + blankDealer.Password + "\n" + "AdminDate:" + blankDealer.DateOfAdminApproval.ToString() + "\n" + "regionId:" + blankDealer.RegionId.ToString() + "\n" + "SOID:" + blankDealer.SalesOfficerId.ToString() + "\n");

            if (BlankDealer.HasValidationErrors == false)
            {
                try
                {
                    _context.Dealers.Add(blankDealer);
                    MyWindow.Show();
                    _context.SubmitChanges(OnSubmitCompleted, null);
                }
                catch (Exception ex)
                { return; }
            }
            else
            {
                MessageBox.Show("Please input required fields!!!");
             }
                
            
        }

        public void LoadSalesOfficers()
        {
            MyWindow.Show();
            _context.Load(_context.GetEmployeesQuery().Where(c => c.Designation == "Sales Officer" && c.Region.RegionName == UserInfo.RegionName)).Completed += (s, e) =>
                {
                    MyWindow.Close();
                };
        }
        
        public ObservableCollection<String> BusinessTypes { get; set; }
        public ObservableCollection<String> PaymentTypes { get; set; }


        public EntitySet<Employee> SalesOfficers { get{return _context.Employees;}}
        public EntitySet<Region> Regions { get { return _context.Regions; } }

        /// <summary>
        /// The <see cref="SalesOfficer" /> property's name.
        /// </summary>
        public const string SalesOfficerPropertyName = "SalesOfficer";

        private Employee _salesofficer ;

        /// <summary>
        /// Sets and gets the SalesOfficer property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Employee SalesOfficer
        {
            get
            {
                return _salesofficer;
            }

            set
            {
                if (_salesofficer == value)
                {
                    return;
                }
               
                _salesofficer = value;
                RaisePropertyChanged(SalesOfficerPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="BlankDealer" /> property's name.
        /// </summary>
        public const string BlankDealerPropertyName = "BlankDealer";

        private Dealer blankDealer;

        /// <summary>
        /// Sets and gets the BlankDealer property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Dealer BlankDealer
        {
            get
            {
                return blankDealer;
            }

            set
            {
                if (blankDealer == value)
                {
                    return;
                }

                blankDealer = value;
                RaisePropertyChanged(BlankDealerPropertyName);
            }
        }


        //commands
        public ICommand SaveCommand{get;set;}
        public ICommand ResetCommand{get;set;}


       
    }
}
