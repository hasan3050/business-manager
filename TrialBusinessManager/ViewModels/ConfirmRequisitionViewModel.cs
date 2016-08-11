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
    public class ConfirmRequisitionViewModel:ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        BusyWindow Busy = new BusyWindow();
        bool selectionCheck;
        public ConfirmRequisitionViewModel()
        {
            Busy.Show();
            LoadRequisitions();
            selectionCheck = false;
            ViewDetails = new RelayCommand(View);
        }

        void LoadRequisitions()
        {

            _context.Load(_context.GetRequisitionsQuery().Where(e => e.Status == "Pending" && e.Inventory.InventoryId == UserInfo.Inventory.InventoryId)).Completed += (s, e) =>
                {
                    Busy.Close();
                };
        
        }

        void View()
        {
            
            if (selectionCheck==false)
                return;
            if (selectedRequisition.RequisitionType == ConstantCollections.RequisitionTypeName.ElementAt(0) && selectedRequisition.Status == "Pending")
            {
                InventoryMesseging.RequisitionID = selectedRequisition.RequisitionId;
                RequisitionVerification obj = new RequisitionVerification();
                obj.Show();
                obj.Closed += new EventHandler(obj_Closed);

            }
            else if (selectedRequisition.Status == "Pending")
            {
                ReturnRequisitionVerificationChildWindow obj = new ReturnRequisitionVerificationChildWindow(selectedRequisition.RequisitionId);
                obj.Show();
                obj.Closed += new EventHandler(obj_Closed);
            }
        }

        void obj_Closed(object sender, EventArgs e)
        {

            if (InventoryMesseging.RequisitionStatus == "Approved")
            {
                selectedRequisition.Status = "Approved";
                InventoryMesseging.RequisitionStatus = "";
            }
            if (InventoryMesseging.RequisitionStatus == "Rejected")
            {
                selectedRequisition.Status = "Rejected";
                InventoryMesseging.RequisitionStatus = "";
            }

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
