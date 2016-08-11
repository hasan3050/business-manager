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
using TrialBusinessManager.Views.DO;
using TrialBusinessManager.Views.SIC;

namespace TrialBusinessManager.ViewModels
{
    public class ProductTransferVerificationViewModel:ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        BusyWindow Busy = new BusyWindow();
        bool selectionCheck=false;
        int Flag = 0;
        public ProductTransferVerificationViewModel()
        {
            Busy.Show();
            LoadProductTransfers();
            LoadInventories();
            selectionCheck = false;
            ViewDetails = new RelayCommand(View);
        }

        void LoadInventories()
        {
            _context.Load(_context.GetInventoriesQuery()).Completed+=new EventHandler(ProductTransferVerificationViewModel_Completed);
            
        }


        void View()
        {
            if (selectionCheck == true && selectedTransfer.ProductTransferStatus=="Dispatched")
            {
                ProductTransferVerificationChildWindow obj= new ProductTransferVerificationChildWindow(selectedTransfer, _context);
                obj.Show();

            }
           
        }

        void LoadProductTransfers()
        {
            _context.Load(_context.GetProductTransfersQuery().Where(e => e.ProductTransferStatus == "Dispatched" && e.RecievedToInventoryId == UserInfo.Inventory.InventoryId)).Completed += new EventHandler(ProductTransferVerificationViewModel_Completed);
        
        }

        void ProductTransferVerificationViewModel_Completed(object sender, EventArgs e)
        {
            Flag++;
            if (Flag == 2)
                Busy.Close();
        }

        public ICommand ViewDetails { get; set; }
        public EntitySet<ProductTransfer> ProductTransfers { get {return _context.ProductTransfers; } }


        private ProductTransfer selectedTransfer;

        /// <summary>
        /// Sets and gets the SelectedRequisition property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ProductTransfer SelectedTransfer
        {
            get
            {
                return selectedTransfer;
            }

            set
            {
                if (selectedTransfer == value)
                {
                    return;
                }

                selectedTransfer = value;
                selectionCheck = true;
                RaisePropertyChanged("SelectedTransfer");
            }
        }
    }
}
