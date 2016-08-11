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
using TrialBusinessManager.Views.NSM;
using TrialBusinessManager.Views.RM;

namespace TrialBusinessManager.ViewModels
{
    public class ConfirmExpenditureViewModel:ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        bool selectionCheck;
        BusyWindow Busy = new BusyWindow();
        int Flag = 0;
        public ConfirmExpenditureViewModel()
        {
  
            selectionCheck = false;
            ViewDetails = new RelayCommand(View);
            Busy.Show();
            LoadExpenditures();
        }

        void LoadExpenditures()
        {

            if (UserInfo.Designation == "National Sales Manager")
            {
                _context.Load(_context.GetExpendituresQuery().Where(e => e.Status == "Pending")).Completed += new EventHandler(ConfirmExpenditureViewModel_Completed);
            }
            else if (UserInfo.Designation == "Accountant")
            {
                _context.Load(_context.GetExpendituresQuery().Where(e => e.Status == "Approved by NSM")).Completed+=new EventHandler(ConfirmExpenditureViewModel_Completed);
            
            }
            else if (UserInfo.Designation == "Regional Manager")
            {
                _context.Load(_context.GetExpendituresQuery().Where(e => e.Status == "Approved by Accounts")).Completed+=new EventHandler(ConfirmExpenditureViewModel_Completed);
            
            }

        }

        void ConfirmExpenditureViewModel_Completed(object sender, EventArgs e)
        {
            
             Busy.Close();
            
        }

        void View()
        {
            
            if (selectionCheck == false)
                return;

            if (selected.Status == "Pending")
            {
                NSMExpenditureApprovalChildWindow obj = new NSMExpenditureApprovalChildWindow(selected);
                obj.Show();
                
            }

            else if (selected.Status == "Approved by NSM")
            {
                ExpenditureApprovalChildWindow obj = new ExpenditureApprovalChildWindow(selected);
                obj.Show();
                
            }

            else if (selected.Status == "Approved by Accounts")
            {
                AcceptedExpenditureChildWindow obj = new AcceptedExpenditureChildWindow(selected);
                obj.Show();
            }
          
        }
        void obj_Closed(object sender, EventArgs e)
        {

            if (InventoryMesseging.RequisitionStatus == "Approved")
            {
                selected.Status = "Approved";
                InventoryMesseging.RequisitionStatus = "";
            }
            if (InventoryMesseging.RequisitionStatus == "Rejected")
            {
                selected.Status = "Rejected";
                InventoryMesseging.RequisitionStatus = "";
            }

        }


        public ICommand ViewDetails { get; set; }

        public EntitySet<Expenditure> Expenditures { get { return _context.Expenditures; } }
        /// <summary>
        /// The <see cref="Selected" /> property's name.
        /// </summary>
        public const string SelectedRequisitionPropertyName = "Selected";

        private Expenditure selected;

        /// <summary>
        /// Sets and gets the Selected property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Expenditure Selected
        {
            get
            {
                return selected;
            }

            set
            {
                if (selected == value)
                {
                    return;
                }

                selected = value;
                selectionCheck = true;
                RaisePropertyChanged(SelectedRequisitionPropertyName);
            }
        }


    }
}
