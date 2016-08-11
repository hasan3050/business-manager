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
   
    public class CreateMRRViewModel:ViewModelBase
    {
         AgroDomainContext _context = new AgroDomainContext();
         int busyFlag;
            
         ObservableCollection<MRRProductInfo> mrrProductCollection = new ObservableCollection<MRRProductInfo>() ;
         ObservableCollection<MRRPackageInfo> mrrPackageCollection = new ObservableCollection<MRRPackageInfo>();
         Inventory UserInventory = new Inventory();
         BusyWindow busy = new BusyWindow();
        public CreateMRRViewModel()
        {
            busyFlag = 0;

            busy.Show();

            LoadProducts();
            LoadPackage();
           
            LoadMRRs();

            LoadInventoryProductInfo();
            LoadInventoryPackageInfo();
          //  _context = SICSharedData.SharedContext;

            SaveProductCommand = new RelayCommand(SaveProduct);
            SavePackageCommand = new RelayCommand(SavePackage);
            ResetCommand = new RelayCommand(Reset);
            AddProduct = new RelayCommand(AddProductToCollection);
            AddPackage = new RelayCommand(AddPackageToCollection);

            DeleteSelectedProduct = new RelayCommand(DeleteProduct);
            DeleteSelectedPackage = new RelayCommand(DeletePackage);

            UserInventory = UserInfo.Inventory;

            CreateAll();
           
        }

        void LoadMRRs()
        {
            _context.Load(_context.GetMRRsQuery().Where(e=>e.Inventory.InventoryId==UserInfo.Inventory.InventoryId)).Completed+=new EventHandler(CreateMRRViewMode_Completed);
        
        }

        void CreateMRRViewMode_Completed(object sender, EventArgs e)
        {
            busyFlag++;
            if (busyFlag == 4)
                busy.Close();
        
        }
     
        //To Create All the Required Objects
        void CreateAll()
        {
            newMRR = CreateNewMRR();
            SelectedProduct = new Product();
            SelectedProduct.ProductId = -1;
            SelectedPackage = new Package();
            SelectedPackage.PackageId = -1;

            ProductPurchase = null;
            PackagePurchase = null;
            ProductRetailer = null;
            PackageRetailer = null;
            ProductChalan = null;
            PackageChalan = null;
           // MrrCode = "";

            selectedMRRProduct = -1;
            selectedMRRPackage = -1;
          
        }

        private MRR CreateNewMRR()
        { 
            MRR x=new MRR();
            
           
            return x;
        
        }
        private void SavePackage()
        {
            _context.MRRPackageInfos.Clear();

            
            if (MRRPackageCollection.Count > 0)
                for (int i = 0; i < MRRPackageCollection.Count; i++)
                {
                    MRRPackageInfo up = new MRRPackageInfo();
                    InventoryPackageInfo updatePackage = new InventoryPackageInfo();

                    up = MRRPackageCollection.ElementAt(i);
                    up.PlacedQuantity = up.AcceptedQuantity;
                    if (up.PurchasePrice <= 0||up.PurchasePrice==null)
                    {
                        MessageBox.Show("Purchase price can not be zero or negative");///Proper Error Message Required!!
                        return;

                    }
                    if (up.AcceptedQuantity <= 0||up.AcceptedQuantity==null)
                    {
                        MessageBox.Show("Quantity can not be zero or negative");///Proper Error Message Required!!
                        return;
                    }

                    newMRR.MRRPackageInfoes.Add(up);


                    if (_context.InventoryPackageInfos.Any(e => e.Package == up.Package))
                    {
                        updatePackage = (InventoryPackageInfo)_context.InventoryPackageInfos.Where(c => c.Package.PackageId == up.Package.PackageId).SingleOrDefault();
                        updatePackage.UnfinishedQuantity += up.AcceptedQuantity;
                    }

                    else
                    {

                        updatePackage.Package = up.Package;

                        updatePackage.InventoryId = UserInventory.InventoryId;/////////////////////Authentication is Required Here/////////////
                        updatePackage.OnProcessingQuantity = 0;
                        updatePackage.UnfinishedQuantity = up.AcceptedQuantity;
                        updatePackage.FinishedQuantity = 0;
                        updatePackage.UnitCost = up.PurchasePrice;
                        _context.InventoryPackageInfos.Add(updatePackage);

                    }
                }
            else
            {
                MessageBox.Show("Please provide required information!");
                return;
            }

          

            newMRR.Status = "Issued";
            newMRR.IssuedById = UserInfo.EmployeeID;            /////////////////////Authentication is Required Here/////////////
            newMRR.IssuedToId = UserInfo.EmployeeID;            /////////////////////Authentication is Required Here/////////////
            newMRR.InventoryId = UserInfo.Inventory.InventoryId;////////////////////Authentication is Required Here/////////////
            newMRR.ChallanNo = packagechalan;
            newMRR.RetailerName = packageretailer;
            newMRR.Wing = selectedPackageWing;
            newMRR.PurchaseOrderNo = packagePurchase;
            newMRR.MRRType = "Package";
            newMRR.MRRCode = DateTime.Now.ToString();
            newMRR.DateOfIssue = DateTime.Now;
            newMRR.DateOfApproval = DateTime.Now;
            //   newMRR.Employee = _context.Employees.Where(e => e.EmployeeId == UserInfo.EmployeeID).First();
            if (newMRR.HasValidationErrors == false && newMRR.ChallanNo != null && newMRR.RetailerName != null && newMRR.Wing != null && newMRR.PurchaseOrderNo != null)
            {
                _context.MRRs.Add(newMRR);
                busy.Show();
                _context.SubmitChanges(OnSubmitCompleted, null);
                
            }
            else
            {

                MessageBox.Show("Please provide required information!");
            }
           
        
        
        }

        private void SaveProduct()
        {
            

            _context.MRRProductInfos.Clear();
          

           
            //Adding products from collection to _context.MRRProductInfoes//
            if (MRRProductCollection.Count > 0)
                for (int i = 0; i < MRRProductCollection.Count; i++)
                {
                    MRRProductInfo up = new MRRProductInfo();
                    InventoryProductInfo updateProduct = new InventoryProductInfo();
               

                    up = mrrProductCollection.ElementAt(i);
                    up.PlacedQuantity = up.AcceptedQuantity;
                 
                    if (up.AcceptedQuantity <= 0||up.AcceptedQuantity==null)
                    {
                        MessageBox.Show("Quantity can not be zero or negative");///Proper Error Message Required!!
                        return;
                    }
                    if (up.PurchasePrice <= 0||up.PurchasePrice==null)
                    {
                        MessageBox.Show("Purchase price can not be zero or negative");///Proper Error Message Required!!
                        return;
                    }
                    if (up.LotId == "0"||up.LotId==null)
                    {
                        MessageBox.Show("Please enter lot number!");
                        return;
                    }
                    newMRR.MRRProductInfoes.Add(up);
                  
                    if (_context.InventoryProductInfos.Any(e => e.Product == up.Product&&e.LotId==up.LotId))
                    {
                        updateProduct = (InventoryProductInfo)_context.InventoryProductInfos.Where(e => e.Product.ProductId == up.Product.ProductId && e.LotId == up.LotId).SingleOrDefault();
                        updateProduct.UnfinishedQuantity += up.AcceptedQuantity;
                    
                    }

                    else
                    {

                        updateProduct.Product = up.Product;
                        updateProduct.LotId = up.LotId;
                        updateProduct.InventoryId = UserInventory.InventoryId;/////////////////////Authentication is Required Here/////////////
                        updateProduct.OnProcessingQuantity = 0;
                        updateProduct.UnfinishedQuantity = up.AcceptedQuantity;
                        updateProduct.FinishedQuantity = 0;
                        updateProduct.UnitLotCost = up.PurchasePrice;
                        _context.InventoryProductInfos.Add(updateProduct);

                    }
                }

           else
                {
                    MessageBox.Show("Please provide required information");
                    return;
                }
        

            newMRR.Status = "Issued";
            newMRR.IssuedById = UserInfo.EmployeeID;            /////////////////////Authentication is Required Here/////////////
            newMRR.IssuedToId = UserInfo.EmployeeID;            /////////////////////Authentication is Required Here/////////////
            newMRR.InventoryId = UserInfo.Inventory.InventoryId;////////////////////Authentication is Required Here/////////////
            newMRR.ChallanNo = productchalan;
            newMRR.RetailerName = productretailer;
            newMRR.Wing = selectedProductWing;
            newMRR.MRRCode = DateTime.Now.ToString();
            newMRR.DateOfIssue = DateTime.Now;
            newMRR.DateOfApproval = DateTime.Now;
            newMRR.PurchaseOrderNo = productPurchase;
            newMRR.MRRType = "Product";
         //   newMRR.Employee = _context.Employees.Where(e => e.EmployeeId == UserInfo.EmployeeID).First();
            if (newMRR.HasValidationErrors == false && newMRR.ChallanNo != null && newMRR.RetailerName != null && newMRR.Wing != null && newMRR.PurchaseOrderNo!=null)
            {
                _context.MRRs.Add(newMRR);
                busy.Show();
                _context.SubmitChanges(OnSubmitCompleted, null);
                
            }
            else 
            {

                MessageBox.Show("Please provide required information! ");
            }
        
        }

        void  OnSubmitCompleted(SubmitOperation op)
        {
            busy.Close();
            if (op.HasError == false)
            {
               // MessageBox.Show("The MRR is Placed Successfully");///Proper Message Required!!
                //Show Completion Message
                Reset();
            }
            else {

              //  MessageBox.Show("Errors");///Proper Error Message Required!!
                ////Show Error
            
            }
        
        
        }

        private void Reset()
        {

            CreateAll();
            MRRProductCollection.Clear();
            MRRPackageCollection.Clear();

        
        }
        private void DeleteProduct() //to delete product from datagrid
        {

            try
            {
                if (selectedMRRProduct > -1)
                {
                    MRRProductCollection.RemoveAt(selectedMRRProduct);


                }
            }
            catch (Exception ex)
            { }
        
        
        }

        private void DeletePackage() //to delete package from datagrid
        {
            
            try
            {

                MRRPackageCollection.RemoveAt(selectedMRRPackage);


            }
            catch (Exception ex)
            { }

           
        
        }

      

      

        private void LoadProducts()
        {
            _context.Load(_context.GetProductsQuery().Where(e=>e.ProductName=="DUMMY")).Completed += new EventHandler(CreateMRRViewModelCompleted);
           
        
        
        }

        void CreateMRRViewModelCompleted(object sender, EventArgs e)
        {
            busyFlag++;
            if (busyFlag == 4)
                busy.Close();
        
        }

        private void LoadPackage()
        {
            _context.Load(_context.GetPackagesQuery()).Completed += new EventHandler(CreateMRRViewModelCompleted1);
        
        }

        void CreateMRRViewModelCompleted1(object sender, EventArgs e)
        {
            busyFlag++;
            if (busyFlag == 4)
                busy.Close();
        
        }

        private void LoadInventoryProductInfo()
        {

            _context.Load(_context.GetInventoryProductInfoesQuery().Where(e => e.InventoryId == UserInfo.Inventory.InventoryId&&e.Product.ProductName=="DUMMY")).Completed += new EventHandler(CreateMRRViewModel_Completed);    ///////////InventoryID From Authentication////////////////
        
        }

        void CreateMRRViewModel_Completed(object sender, EventArgs e)
        {
            busyFlag++;
            if (busyFlag == 4)
                busy.Close();
        
        }

        private void LoadInventoryPackageInfo()
        {
            _context.Load(_context.GetInventoryPackageInfoesQuery().Where(e => e.InventoryId == UserInfo.Inventory.InventoryId)).Completed+=new EventHandler(CreateMRRViewModel_Completed);            ///////////InventoryID From Authentication////////////////
        
        
        }


        private void AddProductToCollection() //To Add AutoComplete box selected product to collection
        {
            try
            {
                if (selectedProduct.ProductId == -1)
                    return;

                if (String.IsNullOrEmpty(SelectedProduct.ProductCode))
                    return;
              /*  if (mrrProductCollection.Any(e => e.Product == selectedProduct))
                    return; */
                MRRProductInfo AddedProduct = new MRRProductInfo();
                AddedProduct.Product = selectedProduct;
                AddedProduct.PlacedQuantity = 0;
                AddedProduct.AcceptedQuantity = 0;
                AddedProduct.PurchasePrice = 0;
                AddedProduct.LotId = "0";        
                MRRProductCollection.Add(AddedProduct);

            }
            catch (Exception ex)
            {
                return;
            }

            SelectedProduct = new Product();
        }

        private void AddPackageToCollection()//To Add AutoComplete box selected package to collection
        {
            try
            {
                if (selectedPackage.PackageId == -1)
                    return;
                if (mrrPackageCollection.Any(e => e.Package == selectedPackage) == true || String.IsNullOrEmpty(selectedPackage.PackageCode))
                    return;
                MRRPackageInfo AddedPackage = new MRRPackageInfo();
                AddedPackage.Package = selectedPackage;
                AddedPackage.PlacedQuantity = 0;
                AddedPackage.AcceptedQuantity = 0;
                AddedPackage.PurchasePrice = 0;
                MRRPackageCollection.Add(AddedPackage);
            }
            catch (Exception ex)
            {
                return;
            }
            SelectedPackage = new Package();
        }



        public ObservableCollection<MRRProductInfo> MRRProductCollection { get{return mrrProductCollection;} }

        public ObservableCollection<MRRPackageInfo> MRRPackageCollection { get { return mrrPackageCollection; } }
        public ObservableCollection<String> Wings { get { return ConstantCollections.Wings; } }
        public EntitySet<Product> Products { get { return _context.Products; } }

        public EntitySet<Package> Packages { get { return _context.Packages; } }

        /// <summary>
        /// The <see cref="MrrCode" /> property's name.
        /// </summary>
        public const string MrrCodePropertyName = "ProductRetailer";

        private String productretailer ;

        /// <summary>
        /// Sets and gets the MrrCode property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public String ProductRetailer
        {
            get
            {
                return productretailer;
            }

            set
            {
                if (productretailer == value)
                {
                    return;
                }

                productretailer = value;
            //    MessageBox.Show("p");
                RaisePropertyChanged(MrrCodePropertyName);
            }
        }
        /// <summary>
        /// The <see cref="MrrCode" /> property's name.
        /// </summary>
        public const string MrCodePropertyName = "PackageRetailer";

        private String packageretailer;

        /// <summary>
        /// Sets and gets the MrrCode property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public String PackageRetailer
        {
            get
            {
                return packageretailer;
            }

            set
            {
                if (packageretailer == value)
                {
                    return;
                }

                packageretailer = value;
                //    MessageBox.Show("p");
                RaisePropertyChanged(MrCodePropertyName);
            }
        }

        private String productchalan;

        /// <summary>
        /// Sets and gets the MrrCode property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public String ProductChalan
        {
            get
            {
                return productchalan;
            }

            set
            {
                if (productchalan == value)
                {
                    return;
                }

                productchalan = value;

                RaisePropertyChanged("ProductChalan");
            }
        }

        private String packagechalan;

        /// <summary>
        /// Sets and gets the MrrCode property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public String PackageChalan
        {
            get
            {
                return packagechalan;
            }

            set
            {
                if (packagechalan == value)
                {
                    return;
                }

                packagechalan = value;

                RaisePropertyChanged("PackageChalan");
            }
        }

        private String productPurchase;
        public String ProductPurchase
        {
            get
            {
                return productPurchase;
            }

            set
            {
                if (productPurchase == value)
                {
                    return;
                }

                productPurchase = value;

                RaisePropertyChanged("ProductPurchase");
            }
        }

        private String packagePurchase;
        public String PackagePurchase
        {
            get
            {
                return packagePurchase;
            }

            set
            {
                if (packagePurchase == value)
                {
                    return;
                }

                packagePurchase = value;

                RaisePropertyChanged("PackagePurchase");
            }
        }

        private String selectedProductWing;

        /// <summary>
        /// Sets and gets the MrrCode property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public String SelectedProductWing
        {
            get
            {
                return selectedProductWing;
            }

            set
            {
                if (selectedProductWing == value)
                {
                    return;
                }

                selectedProductWing = value;

                RaisePropertyChanged("SelectedProductWing");
            }
        }

        private String selectedPackageWing;

        /// <summary>
        /// Sets and gets the MrrCode property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public String SelectedPackageWing
        {
            get
            {
                return selectedPackageWing;
            }

            set
            {
                if (selectedPackageWing == value)
                {
                    return;
                }

                selectedPackageWing = value;

                RaisePropertyChanged("SelectedPackageWing");
            }
        }





        public const string NewMRRPropertyName = "NewMRR";

        private MRR newMRR;

        //This MRR will be saved 
        public MRR NewMRR 
        {
            get
            {
                return newMRR;;
            }

            set
            {
                if (newMRR == value)
                {
                    return;
                }

                newMRR = value;
                RaisePropertyChanged(NewMRRPropertyName);
            }
        }

      
        public const string SelectedProductPropertyName = "SelectedProduct";

        private Product selectedProduct;

        //This will be binded with product Auto Complete box's Selected Item

        public Product SelectedProduct
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
                
                RaisePropertyChanged(SelectedProductPropertyName);
              
            }
        }

      
        public const string SelectedPackagePropertyName = "SelectedPackage";

        private Package selectedPackage;

        //This will be binded with package Auto Complete box's Selected Item    
        public Package SelectedPackage
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
               // AddPackageToCollection();
                RaisePropertyChanged(SelectedPackagePropertyName);
            }
        }

     
        public const string SelectedMRRProductPropertyName = "SelectedMRRProduct";

        private int selectedMRRProduct;

        //This will be binded with Product DataGrid's Selected Item.This Will Be Removed from Collection When A Product is removed from DataGrid
        public int SelectedMRRProduct
        {
            get
            {
                return selectedMRRProduct;
            }

            set
            {
                if (selectedMRRProduct == value)
                {
                    return;
                }
             
                selectedMRRProduct = value;
                RaisePropertyChanged(SelectedMRRProductPropertyName);
            }
        }

        public const string SelectedMRRPackagePropertyName = "SelectedMRRPackage";

        private int selectedMRRPackage;

        //This will be binded with Package DataGrid's Selected Item.This Will Be Removed from Collection When A Package is removed from DataGrid
     
        public int SelectedMRRPackage
        {
            get
            {
                return selectedMRRPackage;
            }

            set
            {
                if (selectedMRRPackage == value)
                {
                    return;
                }
              
                selectedMRRPackage = value;
                RaisePropertyChanged(SelectedMRRPackagePropertyName);
            }
        }
        

        //Save Button Command
        public ICommand SaveProductCommand { get; set; }
        public ICommand SavePackageCommand { get; set; }
        //Reset Button Command
        public ICommand ResetCommand { get; set; }
        //Product DataGrid Button Command
        public ICommand DeleteSelectedProduct { get; set; }
        //Package DataGrid Button Command
        public ICommand DeleteSelectedPackage { get; set; }

        public ICommand AddProduct { get; set; }
        public ICommand AddPackage { get; set; }

        
        
        
        
    }
}
