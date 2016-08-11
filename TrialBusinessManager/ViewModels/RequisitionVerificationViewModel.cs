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
using TrialBusinessManager.Models;
using TrialBusinessManager.Views;
using GalaSoft.MvvmLight.Messaging;

namespace TrialBusinessManager.ViewModels
{
    public class RequisitionVerificationViewModel:ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        Requisition SelectedRequisition = new Requisition();
        BusyWindow busy = new BusyWindow();
        int BusyFlag = 0;
        long RequisitionID ;////////////Requisition ID Will Come From Previous Page//////////////
        int status;
        ObservableCollection<RequisitionProductInfo> requisitionProductCollection = new ObservableCollection<RequisitionProductInfo>();
        ObservableCollection<RequisitionProductInfo> requisitionProducts = new ObservableCollection<RequisitionProductInfo>();
      
        //this collection will be used for lot allocation of a requisition product//
        ObservableCollection<LotInfo> LotInfoCollection = new ObservableCollection<LotInfo>();

        public RequisitionVerificationViewModel()
        {

          
            RequisitionPackages = new ObservableCollection<RequisitionPackageInfo>();
            RequisitionID=InventoryMesseging.RequisitionID;

            busy.Show();
            isEnabled = false; //This will be false until an item is selected from comboBox
            IsEnabled = false;
            LoadRequisition();
            LoadInventoryInfo();

            SaveCommand = new RelayCommand(Save);
            AddCommand = new RelayCommand(Add);
            Reject = new RelayCommand(RejectRequisition);

        
        }

        //When button of Lot Allocation will be pressed tihs will fire..and a childwindow will open//
        public void Add()
        {
            if (SelectionEnabled == false)
                 return; //If No combo box items are selected button function wont work//
                
              LotInfoCollection.Clear();
              LotSelectionChildWindow obj = new LotSelectionChildWindow(_context, selectedProduct, LotInfoCollection,RequisitionProductsCollection);
              obj.Show();
              obj.Closed += new EventHandler(obj_Closed);
          
        }

        //When child window is closed//
        void obj_Closed(object sender, EventArgs e)
        {
            //To remove products where Lotquantity is zero//
            while (LotInfoCollection.Any(f => f.Quantity <= 0))
            {

                LotInfoCollection.Remove(LotInfoCollection.Where(f => f.Quantity <= 0).First());
            
            
            }
            //To delete previous occurances of the product,to overrite the lot allocation//
            while (RequisitionProductsCollection.Any(r => r.Product.ProductId == selectedProduct.Product.ProductId))
            {

                RequisitionProductInfo del = new RequisitionProductInfo();
                del = (RequisitionProductInfo)RequisitionProductsCollection.Where(t => t.Product.ProductId == selectedProduct.Product.ProductId).FirstOrDefault();
                RequisitionProductsCollection.Remove(del);
                 
                  
               
            }
            // To remove product occurances in _context for safety,this is required//
            while(_context.RequisitionProductInfos.Any(r => r.Product.ProductId == selectedProduct.Product.ProductId))
            {

                if (_context.RequisitionProductInfos.Any(r => r.Product.ProductId == selectedProduct.Product.ProductId))
                {

                    _context.RequisitionProductInfos.Remove(_context.RequisitionProductInfos.Where(r => r.Product.ProductId == selectedProduct.Product.ProductId).FirstOrDefault());
                    
                 }
                
                
             }
            //To populate RequisitionProductsCollection collection for datagrid and database update
             if (LotInfoCollection.Count() > 0)
             {

                    for (int i = 0; i < LotInfoCollection.Count(); i++)
                    {

                        RequisitionProductInfo reqProduct = new RequisitionProductInfo();
                        reqProduct.Product = selectedProduct.Product;
                        reqProduct.RequisitionId = SelectedRequisition.RequisitionId;
                        reqProduct.PlacedQuantity = selectedProduct.PlacedQuantity;
                        reqProduct.LotId = LotInfoCollection.ElementAt(i).LotId;
                        reqProduct.LotQuantity = LotInfoCollection.ElementAt(i).Quantity;

                        if (reqProduct.LotQuantity > 0)
                        {
                            RequisitionProductsCollection.Add(reqProduct);
                        }

                    }
                  
             }

            LotInfoCollection.Clear();
        }


        void Save()
        {
            Requisition UpdateRequisition = new Requisition();
            int flag = 0;
            UpdateRequisition = SelectedRequisition;
            UpdateRequisition.Status = "Approved";
            //To Update requisition Product Info//
            for (int i = 0; i < RequisitionProductsCollection.Count(); i++)
            { 

                RequisitionProductInfo selectedPro=new RequisitionProductInfo();
                InventoryProductInfo dummyProduct = new InventoryProductInfo();
                selectedPro = RequisitionProductsCollection.ElementAt(i);

                //This is redundant,but done for safety..to remove previous entires//
                if (_context.RequisitionProductInfos.Any(e => e.Product.ProductId == selectedPro.Product.ProductId && e.LotId == "-1"))
                {
                    _context.RequisitionProductInfos.Remove(_context.RequisitionProductInfos.Where(e => e.Product.ProductId == selectedPro.Product.ProductId && e.LotId == "-1").FirstOrDefault());
                }

                //If the product exists in OnProcessing or Finished then//
                if (_context.InventoryProductInfos.Any(e => e.Product.ProductId == selectedPro.Product.ProductId && e.LotId == selectedPro.LotId && e.Product.ProductName != "DUMMY"))
                {
                    InventoryProductInfo inventoryProduct = new InventoryProductInfo();
              
                    inventoryProduct = _context.InventoryProductInfos.Where(e => e.Product.ProductId == selectedPro.Product.ProductId && e.LotId == selectedPro.LotId&&e.InventoryId==UserInfo.Inventory.InventoryId).SingleOrDefault();
                    dummyProduct = _context.InventoryProductInfos.Where(e => e.Product.GroupName == selectedPro.Product.GroupName && e.LotId == selectedPro.LotId &&e.Product.ProductName=="DUMMY").SingleOrDefault();
                  
                    if (RequisitionProductsCollection.ElementAt(i).LotQuantity > 0)
                    {
                        UpdateRequisition.RequisitionProductInfoes.Add(RequisitionProductsCollection.ElementAt(i));
                    }
                    if (selectedPro.LotQuantity < 0)
                    {
                        MessageBox.Show("Quantity can not be negative!");
                        return;
                    }
                    if (dummyProduct.UnfinishedQuantity < selectedPro.LotQuantity)
                    {
                        MessageBox.Show("Not enough "+selectedPro.Product.GroupName+" available in stock!");
                        return;
                    }
                    dummyProduct.UnfinishedQuantity -= selectedPro.LotQuantity;
                    dummyProduct.OnProcessingQuantity += selectedPro.LotQuantity;
                    inventoryProduct.OnProcessingQuantity += selectedPro.LotQuantity;
                    flag++;
                }
                //If the product doesnt exist in OnProcessing or Finished then//
                
                else
                {

                    InventoryProductInfo inventoryProduct = new InventoryProductInfo();
              
                    dummyProduct = _context.InventoryProductInfos.Where(e => e.Product.GroupName == selectedPro.Product.GroupName && e.LotId == selectedPro.LotId && e.Product.ProductName == "DUMMY").SingleOrDefault();
                    if (RequisitionProductsCollection.ElementAt(i).LotQuantity > 0)
                    {
                        UpdateRequisition.RequisitionProductInfoes.Add(RequisitionProductsCollection.ElementAt(i));
                    }
                    if (selectedPro.LotQuantity < 0)
                    {
                        MessageBox.Show("Quantity can not be negative!");
                        return;
                    }

                    dummyProduct.UnfinishedQuantity -= selectedPro.LotQuantity;
                    dummyProduct.OnProcessingQuantity += selectedPro.LotQuantity;
                    ///Update inventory:From Unfinished to OnProcessing,This will vary according to Requisition Type///
                   
                    inventoryProduct.Product = selectedPro.Product;
                    inventoryProduct.UnfinishedQuantity -= 0;
                    inventoryProduct.OnProcessingQuantity = selectedPro.LotQuantity;
                    inventoryProduct.FinishedQuantity = 0;
                    inventoryProduct.LotId = selectedPro.LotId;
                    inventoryProduct.InventoryId = UserInfo.Inventory.InventoryId;
                    inventoryProduct.UnitLotCost = dummyProduct.UnitLotCost;
                    _context.InventoryProductInfos.Add(inventoryProduct);

                    flag++;
                
                }
            }

            //To Update requisition Package Info//
            for (int i = 0; i < RequisitionPackages.Count(); i++)
            {

                RequisitionPackageInfo reqPackage = new RequisitionPackageInfo();
                RequisitionPackageInfo selectedPackage = new RequisitionPackageInfo();
                InventoryPackageInfo inventoryPackage = new InventoryPackageInfo();

                selectedPackage = RequisitionPackages.ElementAt(i);

                inventoryPackage = _context.InventoryPackageInfos.Where(e => e.Package.PackageId == selectedPackage.PackageId).SingleOrDefault();
                reqPackage = _context.RequisitionPackageInfos.Where(e=>e.Package.PackageId==selectedPackage.PackageId).SingleOrDefault();

                reqPackage.AcceptedQuantity = selectedPackage.AcceptedQuantity;
                ///These Two lines will vary according to requisition type///
                if (selectedPackage.PlacedQuantity< selectedPackage.AcceptedQuantity)
                {
                    MessageBox.Show("Accepted package quantity can not be more than placed quantity!");
                    return;
                }
                if (inventoryPackage.UnfinishedQuantity < selectedPackage.AcceptedQuantity)
                {
                     MessageBox.Show("Not enough " + selectedPackage.Package.PackageName+" available in stock!");
                     return;
                }
                if (selectedPackage.AcceptedQuantity < 0)
                {
                    MessageBox.Show("Quantity can not be negative!");
                    return;
                }
                inventoryPackage.UnfinishedQuantity -= selectedPackage.AcceptedQuantity;
                inventoryPackage.OnProcessingQuantity += selectedPackage.AcceptedQuantity;
                flag++;
            }

            status = 1;
            if (flag != 0)
            busy.Show();
            _context.SubmitChanges(OnSubmitCompleted, null);

        }

        void RejectRequisition()
        {
            Requisition UpdateRequisition = new Requisition();
            UpdateRequisition = SelectedRequisition;
            UpdateRequisition.Status = "Rejected";
            status = 2;
            busy.Show();
            _context.SubmitChanges(OnSubmitCompleted, null);
        }

        private void OnSubmitCompleted(SubmitOperation op)
        {
            busy.Close();
            if (op.HasError == true)
            {
             //   MessageBox.Show("Submition failed.Please try again!");
                _context.RejectChanges();
            }
            else
            {

                MessageBox.Show("Completed");
                if (status == 1)
                    InventoryMesseging.RequisitionStatus = "Approved";
                else
                    InventoryMesseging.RequisitionStatus = "Rejected";
                Messenger.Default.Send<String, RequisitionVerification>("Close");
            }
        }

        //Load Inventory Products and Packages of SiC's inventory//
        private void LoadInventoryInfo()
        {

            _context.Load(_context.GetInventoryProductInfoesQuery().Where(e => e.InventoryId == UserInfo.Inventory.InventoryId)).Completed += new EventHandler(RequisitionVerificationViewModel_Completed);  ////Inventory ID from Authentication////
            _context.Load(_context.GetInventoryPackageInfoesQuery().Where(e => e.InventoryId == UserInfo.Inventory.InventoryId)).Completed += new EventHandler(RequisitionVerificationViewModelPackage_Completed);   ////Inventory ID from Authentication////

         }

        void RequisitionVerificationViewModel_Completed(object sender, EventArgs e)
        {
            BusyFlag++;
            if (BusyFlag == 5)
            {
                busy.Close();
                IsEnabled = true;
            }
        }

        void RequisitionVerificationViewModelPackage_Completed(object sender, EventArgs e)
        {
            BusyFlag++;
            if (BusyFlag == 5)
                busy.Close();
            IsEnabled = true;
        }

        /// <summary>
        /// Load operations of the requisition ,requisition Product info & requisition package info
        /// </summary>
        /// 
        //To Load the requisition info as well as requisition product and package info//
        private void LoadRequisition()
        {
           
            LoadOperation<Requisition> LoadRequisition;
            LoadRequisition = _context.Load(_context.GetRequisitionsQuery().Where(e => e.RequisitionId == RequisitionID));

            LoadOperation<RequisitionProductInfo> LoadReqProducts;
            LoadOperation<RequisitionPackageInfo> LoadReqPackage;

            //Only products of this requisition are loaded.It is a must to load only products of this requisition 
            LoadReqProducts=_context.Load(_context.GetRequisitionProductInfoesQuery().Where(e => e.RequisitionId == RequisitionID));
          
            //Only packages of this requisition are loaded.It is a must to load only packages of this requisition 
            LoadReqPackage=_context.Load(_context.GetRequisitionPackageInfoesQuery().Where(e => e.RequisitionId == RequisitionID));

            LoadRequisition.Completed += (s, arg) =>
            {

                    SelectedRequisition = LoadRequisition.Entities.SingleOrDefault();
                    BusyFlag++;
                    if (BusyFlag == 5)
                        busy.Close();
                
            };

            LoadReqProducts.Completed += (s, arg) =>
            {
                if (LoadReqProducts.Entities.Count() > 0)
                {

                    for (int i = 0; i < LoadReqProducts.Entities.Count(); i++)
                    {
                        requisitionProductCollection.Add(LoadReqProducts.Entities.ElementAt(i));

                    }
                }

                BusyFlag++;
                if (BusyFlag == 5)
                    busy.Close();

            };

            LoadReqPackage.Completed += (s, arg) => {

                if (LoadReqPackage.Entities.Count()>0)
                {

                    for (int i = 0; i < LoadReqPackage.Entities.Count(); i++)
                    {
                        RequisitionPackages.Add(LoadReqPackage.Entities.ElementAt(i));
                    }
                }
                
            };
            requisitionProductCollection.OrderBy(e => e.Product.ProductName);
            BusyFlag++;
            if (BusyFlag == 5)
                busy.Close();
        }

        /// <summary>
        /// The <see cref="SelectedProduct" /> property's name.
        /// </summary>
        public const string SelectedProductPropertyName = "SelectedProduct";

        private RequisitionProductInfo selectedProduct ;

        /// <summary>
        /// Sets and gets the SelectedProduct property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public RequisitionProductInfo SelectedProduct
        {
            get
            {
                return selectedProduct;
            }

            set
            {
                if (selectedProduct == value)
                {
                    return;
                }
                SelectionEnabled = true;
                selectedProduct = value;
                RaisePropertyChanged(SelectedProductPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="IsEnabled" /> property's name.
        /// </summary>
        public const string IsEnabledPropertyName = "IsEnabled";

        private bool isEnabled ;

        /// <summary>
        /// Sets and gets the IsEnabled property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsEnabled
        {
            get
            {
                return isEnabled;
            }

            set
            {
                if (isEnabled == value)
                {
                    return;
                }

                isEnabled = value;
                RaisePropertyChanged(IsEnabledPropertyName);
            }
        }



        private bool _SelectionEnabled;

        /// <summary>
        /// Sets and gets the IsEnabled property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool SelectionEnabled
        {
            get
            {
                return _SelectionEnabled;
            }

            set
            {
                if (_SelectionEnabled == value)
                {
                    return;
                }

                _SelectionEnabled = value;
                RaisePropertyChanged("SelectionEnabled");
            }
        }

        //This will be binded with the comboBox
        public ObservableCollection<RequisitionProductInfo> RequisitionProducts { get { return requisitionProductCollection; } }
        //This will be binded with the datagrid..
        public ObservableCollection<RequisitionProductInfo> RequisitionProductsCollection { get { return requisitionProducts; } }

     

        public ObservableCollection<RequisitionPackageInfo> RequisitionPackages { get ;set;  }

        //To Open child window when lot allocation button is pressed.command for lot Allocation button// 
        public ICommand AddCommand { get; set; }
        //Command of Submit button//
        public ICommand SaveCommand { get; set; }

        public ICommand Reject { get; set; }

      
    }
}
