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
    public class ApprovedRequisitionViewModel:ViewModelBase
    {
        BusyWindow busy = new BusyWindow();
        AgroDomainContext _context = new AgroDomainContext();
        bool selectionCheck;

        public ApprovedRequisitionViewModel()
        {
            busy.Show();
            LoadRequisitions();
            selectionCheck = false;
            ViewDetails = new RelayCommand(View);
        }

        void LoadRequisitions()
        {

            _context.Load(_context.GetRequisitionsQuery().Where(e => e.Status != "Pending" && e.Inventory.InventoryId == UserInfo.Inventory.InventoryId)).Completed += new EventHandler(ApprovedRequisitionViewModel_Completed);
        
        }

        void ApprovedRequisitionViewModel_Completed(object sender, EventArgs e)
        {
            busy.Close();
        }

        void View()
        {
            if (selectionCheck == true&&selectedRequisition.Status=="Approved")
            {
                RequisitionApprovalChildWindow obj = new RequisitionApprovalChildWindow(selectedRequisition.RequisitionId);
                obj.Show();
            }
           
        }

        void obj_Closed(object sender, EventArgs e)
        {
           // selectionCheck = false;
           
        }

        public ICommand ViewDetails { get; set; }

        public EntitySet<Requisition> Requisitions { get { return _context.Requisitions; } }
        /// <summary>
        /// The <see cref="SelectedRequisition" /> property's name.
        /// </summary>
        public const string SelectedRequisitionPropertyName = "SelectedRequisition";

        private Requisition selectedRequisition ;

        /// <summary>
        /// Sets and gets the SelectedRequisition property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Requisition SelectedRequisition
        {
            get
            {
                return selectedRequisition;
            }

            set
            {
                if (selectedRequisition == value)
                {
                    return;
                }

                selectedRequisition = value;
                selectionCheck = true;
                RaisePropertyChanged(SelectedRequisitionPropertyName);
            }
        }
    }
}
