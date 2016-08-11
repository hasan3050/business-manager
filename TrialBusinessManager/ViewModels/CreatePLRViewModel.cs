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
using TrialBusinessManager.Views;
using TrialBusinessManager.Models;

namespace TrialBusinessManager.ViewModels
{
    public class CreatePLRViewModel:ViewModelBase
    {
       
        AgroDomainContext _context = new AgroDomainContext();
        int busyFlag;
        String checkRCode;
        BusyWindow busy = new BusyWindow();

        ObservableCollection<PLRProductInfoModel> pLRProductCollection = new ObservableCollection<PLRProductInfoModel>();
        ObservableCollection<PLRPackageInfoModel> pLRPackageCollection = new ObservableCollection<PLRPackageInfoModel>();
        ObservableCollection<InventoryProductInfo> inventoryProductCollection = new ObservableCollection<InventoryProductInfo>();
        ObservableCollection<InventoryPackageInfo> inventoryPackageCollection = new ObservableCollection<InventoryPackageInfo>();
        ObservableCollection<String> lotCollection = new ObservableCollection<string>();

        Inventory UserInventory = new Inventory();

        bool ResetFlag = false;

        public CreatePLRViewModel()
        {
            busyFlag = 0;

            busy.Show();
            LoadProducts();
            LoadPackage();
          //  LoadPLRs();
          //  _context = SICSharedData.SharedContext;

            SaveCommand = new RelayCommand(Save);
            ResetCommand = new RelayCommand(Reset);

            AddProduct = new RelayCommand(AddProductToCollection);
            AddPackage = new RelayCommand(AddPackageToCollection);

            DeleteSelectedProduct = new RelayCommand(DeleteProduct);
            DeleteSelectedPackage = new RelayCommand(DeletePackage);

            CreateAllObjects();
         //   _context.Load(_context.GetEmployeesByUsernameQuery(UserInfo.Username));
        
        }
        void LoadPLRs()
        {
            _context.Load(_context.GetPLRsQuery().Where(e => e.Inventory.InventoryId == UserInfo.Inventory.InventoryId)).Completed += new EventHandler(CreateRequisitionViewModel_Completed);
               
        }
        void CreateRequisitionViewModel_Completed(object sender, EventArgs e)
        {
            busyFlag++;
            if (busyFlag == 2)
                busy.Close();
           
        }

        void LoadProduct_Completed(object sender, EventArgs e)
        {
            busyFlag++;
            if (busyFlag == 2)
                busy.Close();
           
            foreach (InventoryProductInfo x in _context.InventoryProductInfos)
            {
                if (!inventoryProductCollection.Any(r => r.Product == x.Product))
                {
                    inventoryProductCollection.Add(x);
                }
            }
           
        }
        void LoadPackage_Completed(object sender, EventArgs e)
        {
            busyFlag++;
            if (busyFlag == 2)
                busy.Close();
            foreach (InventoryPackageInfo x in _context.InventoryPackageInfos)
            {
                inventoryPackageCollection.Add(x);
            }

        }

     

        private void CreateAllObjects()
        {
            SelectedPackage = new InventoryPackageInfo();
           
            SelectedProduct = new InventoryProductInfo();
          
            newPLR = new PLR();
          
        }

        private void Save()
        {
            _context.PLRProductInfos.Clear();
            _context.PLRPackageInfos.Clear();

          

            int flag = 0;

            if (pLRProductCollection.Count > 0)
                for (int i = 0; i < pLRProductCollection.Count; i++)
                {
                   PLRProductInfo up = new PLRProductInfo();
                   PLRProductInfoModel now = new PLRProductInfoModel();
                  
                    

                    now = pLRProductCollection.ElementAt(i);
                    up = now.PLRProductInfo;
                    up.LostAmount = now.PurchasePrice * now.PLRProductInfo.Quantity;
                    up.State = now.SelectedType;
                    if (now.PLRProductInfo.Quantity <= 0)
                    {
                        MessageBox.Show("Quantity can not be zero or negative");///Proper Error Message Required!!
                        return;
                    }
                   
                    if (now.PLRProductInfo.State == "Not selected yet")
                    {
                        MessageBox.Show("Please select product status!!");
                        return;
                    }

                    if (now.PLRProductInfo.State == "OnProcessing")
                    {
                        if (now.PLRProductInfo.Quantity > _context.InventoryProductInfos.Where(e => e.InventoryId == UserInfo.Inventory.InventoryId && e.Product.ProductId == up.Product.ProductId && e.LotId == up.LotId).Single().OnProcessingQuantity)
                        {
                            MessageBox.Show("Not enough product available is stock for PLR!");
                            return;
                        }
                    }
                    else
                    {
                        if (now.PLRProductInfo.Quantity > _context.InventoryProductInfos.Where(e => e.InventoryId == UserInfo.Inventory.InventoryId && e.Product.ProductId == up.Product.ProductId && e.LotId == up.LotId).Single().FinishedQuantity)
                        {
                            MessageBox.Show("Not enough product available is stock for PLR!");
                            return;
                        }
                    
                    }

                    if (now.PLRProductInfo.Remarks == "write remarks")
                    {
                        MessageBox.Show("Please write remarks !!");
                        return;
                    }

                  
                  
                    newPLR.PLRProductInfoes.Add(up);
                    flag++;

                 
                }

            if (pLRPackageCollection.Count > 0)
                for (int i = 0; i < pLRPackageCollection.Count; i++)
                {
                   PLRPackageInfo up = new PLRPackageInfo();
                   PLRPackageInfoModel now = new PLRPackageInfoModel();
                   now = pLRPackageCollection.ElementAt(i);
                   up = now.PLRPackageInfo;
                   up.LostAmount = now.PLRPackageInfo.Quantity * now.PurchasePrice;
                   up.State = now.SelectedType;
                  //  up = PLRPackageCollection.ElementAt(i);

                   if (now.PLRPackageInfo.State == "Not selected yet")
                   {
                       MessageBox.Show("Please select package status!!");
                       return;
                   }
                   if (now.PLRPackageInfo.Remarks == "write remarks")
                   {
                       MessageBox.Show("Please write remarks !!");
                       return;
                   }
                    if (up.Quantity <= 0)
                    {
                        MessageBox.Show("Quantity can not be zero or negative");///Proper Error Message Required!!
                        return;
                    }

                    if (now.PLRPackageInfo.State == "OnProcessing")
                    {
                        if (now.PLRPackageInfo.Quantity > _context.InventoryPackageInfos.Where(e => e.InventoryId == UserInfo.Inventory.InventoryId && e.Package.PackageId == up.Package.PackageId).Single().OnProcessingQuantity)
                        {
                            MessageBox.Show("Not enough product available is stock for PLR!");
                            return;
                        }
                    }
                    else
                    {
                        if (now.PLRPackageInfo.Quantity > _context.InventoryPackageInfos.Where(e => e.InventoryId == UserInfo.Inventory.InventoryId && e.Package.PackageId == up.Package.PackageId).Single().FinishedQuantity)
                        {
                            MessageBox.Show("Not enough product available is stock for PLR!");
                            return;
                        }

                    }
                    newPLR.PLRPackageInfoes.Add(up);
                    flag++;

                   
                }

            if (flag == 0)
                return;

          
            
            newPLR.DateOfIssue = DateTime.Now;
            newPLR.DateOfApproval = DateTime.Now;
            newPLR.InventoryId = UserInfo.Inventory.InventoryId;       //Authentication required////
            newPLR.IssuedById = UserInfo.EmployeeID;                   //Authentication required DO ID////
            newPLR.IssuedToId = UserInfo.Inventory.StoreInChargeId;    //ID of SIC Authentication required////
            newPLR.Status = "Pending";


            newPLR.PLRCode = DateTime.Now.ToString();
           // newPLR.RequisitionType = ConstantCollections.RequisitionTypeName.ElementAt(0);
           // newPLR.Employee = _context.Employees.Where(e=>e.UserName==UserInfo.Username).First();
          
            if (newPLR.HasValidationErrors==false)
            {
                busy.Show();
                _context.PLRs.Add(newPLR);
                _context.SubmitChanges(OnSubmitCompleted, null);
                
            }
            else 
            {

                MessageBox.Show("Please provide required information");
            }

        }

        private void OnSubmitCompleted(SubmitOperation op)
        {
            busy.Close();

            if (op.HasError)
            {
              //  MessageBox.Show(op.Error.ToString());
               // op.MarkErrorAsHandled();
            }
            else
            {
                MessageBox.Show("PLR placed");
                Reset();

            }

        }

        private void Reset()
        {
            
            ResetFlag = true;
            CreateAllObjects();
            PLRProductCollection.Clear();
            PLRPackageCollection.Clear();
            ResetFlag = false;
        }

        private void DeleteProduct() //to delete product from datagrid
        {
            try
            {
                if (SelectedPLRProduct > -1)
                {
                    pLRProductCollection.RemoveAt(SelectedPLRProduct);

                }
            }
            catch (Exception ex)
            { }


        }

        private void DeletePackage() //to delete package from datagrid
        {
            try
            {
                if (SelectedPLRPackageInfo > -1)
                {
                    pLRPackageCollection.RemoveAt(SelectedPLRPackageInfo);

                }
            }
            catch (Exception ex)
            { }

        }


         private void AddProductToCollection()   //To Add AutoComplete box selected product to collection
        {
            try
            {
            if (selectedlot == null)
                return;
           
            if (pLRProductCollection.Any(e => e.PLRProductInfo.Product.ProductId == selectedProduct.ProductId &&e.PLRProductInfo.LotId==selectedlot) )
                return;
           
                PLRProductInfoModel R = new PLRProductInfoModel();
                R.ProductStatus = new ObservableCollection<string> {"OnProcessing", "Finished" };
                R.PLRProductInfo = new PLRProductInfo();
                R.PLRProductInfo.Product = selectedProduct.Product;
                R.PLRProductInfo.LotId = selectedlot;
                R.PLRProductInfo.Quantity = 0;
                R.PLRProductInfo.LostAmount = 0;
                R.PurchasePrice = (Double)selectedProduct.UnitLotCost;
                R.PLRProductInfo.State = "Not";

                R.PLRProductInfo.Remarks = "write remarks";
                R.SelectedType = "Not selected yet";

                //R.PlacedQuantity = 0;
                pLRProductCollection.Add(R);
                SelectedProduct = new InventoryProductInfo();
                SelectedLot = null;
            }
            catch (Exception ex)
            {
                return;
            
            }
        }

        private void AddPackageToCollection()   //To Add AutoComplete box selected package to collection
        {

            try
            {
              
            if (pLRPackageCollection.Any(e => e.PLRPackageInfo.PackageId == selectedPackage.PackageId) )
                return;
                PLRPackageInfoModel R = new PLRPackageInfoModel();
                R.PLRPackageInfo = new PLRPackageInfo();
                R.PackageStatus = new ObservableCollection<string> { "OnProcessing", "Finished" };
                R.PLRPackageInfo.Package = selectedPackage.Package;
                R.PLRPackageInfo.Quantity = 0;
                R.PLRPackageInfo.Remarks = "write remarks";
                R.SelectedType = "Not selected yet";
                R.PLRPackageInfo.State = "Not";
                R.PLRPackageInfo.LostAmount = 0;
                R.PurchasePrice = (Double)selectedPackage.UnitCost;
                pLRPackageCollection.Add(R);
                SelectedPackage = new InventoryPackageInfo();
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void LoadProducts()
        {
            _context.Load(_context.GetInventoryProductInfoesQuery().Where(e=>e.InventoryId==UserInfo.Inventory.InventoryId&&e.Product.ProductName!="DUMMY")).Completed+=new EventHandler(LoadProduct_Completed);   //Inventory ID from authentication////



        }

        private void LoadPackage()
        {
            _context.Load(_context.GetInventoryPackageInfoesQuery().Where(e => e.InventoryId == UserInfo.Inventory.InventoryId)).Completed += new EventHandler(LoadPackage_Completed);   //Inventory ID from authentication///

        }
        void UpdateLotCollection()
        {
            try
            {
                Lots.Clear();
                lotCollection.Clear();
                foreach (InventoryProductInfo x in _context.InventoryProductInfos)
                {
                    if (x.Product == selectedProduct.Product)
                    {
                        Lots.Add(x.LotId);
                    }
                }
            }
            catch (Exception ex)
            {
                return;
            }
        
        }


        public ICommand SaveCommand { get; set; }

        public ICommand ResetCommand { get; set; }
        //Product DataGrid Button Command
        public ICommand DeleteSelectedProduct { get; set; }
        //Package DataGrid Button Command
        public ICommand DeleteSelectedPackage { get; set; }

        public ObservableCollection<PLRProductInfoModel> PLRProductCollection { get { return pLRProductCollection; } }
        public ObservableCollection<PLRPackageInfoModel> PLRPackageCollection { get { return pLRPackageCollection; } }

        public ObservableCollection<String> Lots { get { return lotCollection; } }
        public ObservableCollection<InventoryProductInfo> Products { get { return inventoryProductCollection; } }

        public ObservableCollection<InventoryPackageInfo> Packages { get { return inventoryPackageCollection; } }
        /// <summary>
        /// The <see cref="NewPLR" /> property's name.
        /// </summary>
        public const string NewRequisitionPropertyName = "NewPLR";

        private PLR newPLR ;

        /// <summary>
        /// Sets and gets the NewPLR property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public PLR NewPLR
        {
            get
            {
                return newPLR;
            }

            set
            {
                if (newPLR == value)
                {
                    return;
                }

                newPLR = value;
                RaisePropertyChanged(NewRequisitionPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SelectedPLRProduct" /> property's name.
        /// </summary>
        public const string SelectedRequisitionProductPropertyName = "SelectedPLRProduct";

        private int selectedRequisitionProductInfo ;

        /// <summary>
       ///To Delete a Selected product item from collection when delete button is pressed
        /// </summary>
        public int SelectedPLRProduct
        {
            get
            {
                return selectedRequisitionProductInfo;
            }

            set
            {
                if (selectedRequisitionProductInfo == value)
                {
                    return;
                }

            
                selectedRequisitionProductInfo = value;
                RaisePropertyChanged(SelectedRequisitionProductPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SelectedPLRPackageInfo" /> property's name.
        /// </summary>
        public const string SelectedRequisitionPackageInfoPropertyName = "SelectedPLRPackageInfo";

        private int selectedRequisitionPackageInfo;

        /// <summary>
        ///To Delete a Selected package item from collection when delete button is pressed
        /// </summary>
        public int SelectedPLRPackageInfo
        {
            get
            {
                return selectedRequisitionPackageInfo;
            }

            set
            {
                if (selectedRequisitionPackageInfo == value)
                {
                    return;
                }

                selectedRequisitionPackageInfo = value;
                RaisePropertyChanged(SelectedRequisitionPackageInfoPropertyName);
            }
        }

        public const string SelectedProductPropertyName = "SelectedProduct";

        private InventoryProductInfo selectedProduct;

        //This will be binded with product Auto Complete box's Selected Item

        public InventoryProductInfo SelectedProduct
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


                selectedProduct = value;
                UpdateLotCollection();
                RaisePropertyChanged(SelectedProductPropertyName);

            }
        }


        public const string SelectedPackagePropertyName = "SelectedPackage";

        private InventoryPackageInfo selectedPackage;

        //This will be binded with package Auto Complete box's Selected Item    
        public InventoryPackageInfo SelectedPackage
        {
            get
            {
                return selectedPackage;
            }

            set
            {
                if (selectedPackage == value)
                {
                    return;
                }

                selectedPackage = value;
              //  AddPackageToCollection();
                RaisePropertyChanged(SelectedPackagePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="PLRRCode" /> property's name.
        /// </summary>
        public const string RCodePropertyName = "PLRRCode";

        private String plrCode  ;

        /// <summary>
        /// Sets and gets the PLRRCode property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public String PLRRCode
        {
            get
            {
                return plrCode;
            }

            set
            {
                if (plrCode == value)
                {
                    return;
                }

                plrCode = value;
                checkRCode = value;
                RaisePropertyChanged(RCodePropertyName);
            }
        }
        private String selectedlot;

        /// <summary>
        /// Sets and gets the PLRRCode property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public String SelectedLot
        {
            get
            {
                return selectedlot;
            }

            set
            {
                if (selectedlot == value)
                {
                    return;
                }

                selectedlot = value;

                RaisePropertyChanged("SelectedLot");
            }
        }

        


        public ICommand AddProduct { get; set; }

        public ICommand AddPackage { get; set; }

    }
}

