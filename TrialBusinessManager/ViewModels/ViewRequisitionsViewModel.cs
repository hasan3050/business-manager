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
    public class ViewRequisitionsViewModel:ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        ObservableCollection<Requisition> requisitionCollection = new ObservableCollection<Requisition>();

        public ViewRequisitionsViewModel()
        {
            ViewDetailsCommand = new RelayCommand(ViewDetails);
            LoadRequisitions();
            selectedRequisition = new Requisition();
        }

        private void ViewDetails()
        {
            if (selectedRequisition.RequisitionType == "Request for unfinished goods")
            {
                InventoryMesseging.RequisitionID = selectedRequisition.RequisitionId;
              
                RequisitionVerification obj = new RequisitionVerification();
                obj.Show();
            }
        
        }

        void LoadRequisitions()
        {
            LoadOperation<Requisition> loadRequisitions = _context.Load(_context.GetRequisitionsQuery().Where(e => e.Inventory == UserInfo.Inventory && e.Status == "Pending"));
           
            loadRequisitions.Completed += (s, arg) =>
                {
                    for (int i = 0; i < loadRequisitions.Entities.Count(); i++)
                    {
                        Requisition down = new Requisition();
                        down = loadRequisitions.Entities.ElementAt(i);
                        requisitionCollection.Add(down);
                    }

                };

        }

        public ObservableCollection<Requisition> RequisitionCollection { get {return requisitionCollection; } }
        public ICommand ViewDetailsCommand { get; set; }

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
                RaisePropertyChanged(SelectedRequisitionPropertyName);
            }
        }
    }
}
