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
    public class ConfirmPLRViewModel : ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        bool selectionCheck;
        BusyWindow Busy = new BusyWindow();
        int Flag = 0;
        public ConfirmPLRViewModel()
        {
           
            selectionCheck = false;
            ViewDetails = new RelayCommand(View);
            Busy.Show();
            LoadPLRs();
        }

        void LoadPLRs()
        {

            if (UserInfo.Designation == "Store In Charge" || UserInfo.Designation == "Dispatch Officer")
            {
                _context.Load(_context.GetPLRsQuery().Where(e => e.Status == "Verified" || e.Status == "Rejected").Where(e => e.Inventory.InventoryId == UserInfo.Inventory.InventoryId)).Completed += new EventHandler(ConfirmPLRViewModel_Completed);
            }
            else 
            {
                _context.Load(_context.GetPLRsQuery().Where(e => e.Status == "Pending")).Completed+=new EventHandler(ConfirmPLRViewModel_Completed);
            }
            _context.Load(_context.GetEmployeesQuery().Where(e=>e.Designation=="Store In Charge"||e.Designation=="Dispatch Officer")).Completed+=new EventHandler(ConfirmPLRViewModel_Completed);
         
        }

        void ConfirmPLRViewModel_Completed(object sender, EventArgs e)
        {
            Flag++;
            if (Flag == 2)
                Busy.Close();
        }

        void View()
        {

            try
            {
                if (selectedPLR.Status == "Pending")
                {

                    PLRConfirmationChildWindow obj = new PLRConfirmationChildWindow(selectedPLR,_context);
                    obj.Show();
                }

                else if (selectedPLR.Status == "Verified" || selectedPLR.Status == "Rejected")
                {
                    PLRApprovalChildWindow obj = new PLRApprovalChildWindow(selectedPLR);
                    obj.Show();
                }
            }
            catch (Exception ex)
            {
                return;
            }
            
        }

        void obj_Closed(object sender, EventArgs e)
        {


        }

        public ICommand ViewDetails { get; set; }

        public EntitySet<PLR> PLRs { get { return _context.PLRs; } }

        /// <summary>
        /// The <see cref="SelectedPLR" /> property's name.
        /// </summary>
        public const string SelectedPLRPropertyName = "SelectedPLR";

        private PLR selectedPLR  ;

        /// <summary>
        /// Sets and gets the SelectedPLR property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public PLR SelectedPLR
        {
            get
            {
                return selectedPLR;
            }

            set
            {
                if (selectedPLR == value)
                {
                    return;
                }

                selectedPLR = value;
                selectionCheck = true;

                RaisePropertyChanged(SelectedPLRPropertyName);
            }
        }
   
    }
}
