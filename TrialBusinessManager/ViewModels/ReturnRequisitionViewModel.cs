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
    public class ReturnRequisitionViewModel:ViewModelBase
    {
        
        AgroDomainContext _context = new AgroDomainContext();
        int busyFlag;
        String checkRCode;
        BusyWindow busy = new BusyWindow();
        ObservableCollection<RequisitionProductInfo> requisitionProductCollection = new ObservableCollection<RequisitionProductInfo>();
        ObservableCollection<RequisitionPackageInfo> requisitionPackageCollection = new ObservableCollection<RequisitionPackageInfo>();
        ObservableCollection<string> RequisitionTypeName = new ObservableCollection<string>
        {
            "Return Onprocessing Goods",
            "Return Finished Goods"
        };
        ObservableCollection<InventoryProductInfo> inventoryProductCollection = new ObservableCollection<InventoryProductInfo>();
        ObservableCollection<InventoryPackageInfo> inventoryPackageCollection = new ObservableCollection<InventoryPackageInfo>();
        ObservableCollection<String> lotCollection = new ObservableCollection<string>();
        Inventory UserInventory = new Inventory();
       

        public ReturnRequisitionViewModel()
        {
            busyFlag = 0;
            busy.Show();
         
            LoadProducts();
            LoadPackage();
           // LoadRequisitions();

            SaveCommand = new RelayCommand(Save);
            ResetCommand = new RelayCommand(Reset);

            AddProduct = new RelayCommand(AddProductToCollection);
            AddPackage = new RelayCommand(AddPackageToCollection);

            DeleteSelectedProduct = new RelayCommand(DeleteProduct);
            DeleteSelectedPackage = new RelayCommand(DeletePackage);

            CreateAllObjects();
            selectedType = "NotSelected";
         //   _context.Load(_context.GetEmployeesByUsernameQuery(UserInfo.Username));
        
        }
        void LoadRequisitions()
        {
            _context.Load(_context.GetRequisitionsQuery().Where(e => e.Inventory.InventoryId == UserInfo.Inventory.InventoryId)).Completed += new EventHandler(CreateRequisitionViewModel_Completed);
               
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
            selectedPackage = new InventoryPackageInfo();
         
            selectedProduct = new InventoryProductInfo();
            
            newRequisition = new Requisition();
            selectedRequisitionPackageInfo = -1;
            selectedRequisitionProductInfo = -1;
           
        }

        private void Save()
        {
            _context.RequisitionProductInfos.Clear();
            _context.RequisitionPackageInfos.Clear();

            if (selectedType == "NotSelected")
            {
                MessageBox.Show("Error: Requisition type not selected!");
                return;
            }

            int flag = 0;

            if (requisitionProductCollection.Count > 0)
                for (int i = 0; i < requisitionProductCollection.Count; i++)
                {
                    RequisitionProductInfo up = new RequisitionProductInfo();



                    up = requisitionProductCollection.ElementAt(i);
                    if (selectedType=="Return Onprocessing Goods")
                    {
                   
                        
                        if(up.PlacedQuantity>_context.InventoryProductInfos.Where(r=>r.InventoryId==UserInfo.Inventory.InventoryId).Where(e=>e.ProductId==up.Product.ProductId&&e.LotId==up.LotId).First().OnProcessingQuantity)
                        {
                            MessageBox.Show("Not enough on processing product in stock!");
                            return;
                        }
                    }

                     if (selectedType=="Return Finished Goods")
                    {
                   
                        
                        if(up.PlacedQuantity>_context.InventoryProductInfos.Where(r=>r.InventoryId==UserInfo.Inventory.InventoryId).Where(e=>e.ProductId==up.Product.ProductId&&e.LotId==up.LotId).First().FinishedQuantity)
                        {
                             MessageBox.Show("Not enough finished product in stock!");
                            return;
                        }
                    }


            

                    if (up.PlacedQuantity <= 0)
                    {
                        MessageBox.Show("Quantity can not be zero or negative");///Proper Error Message Required!!
                        return;
                    }
                    newRequisition.RequisitionProductInfoes.Add(up);
                    flag++;

                 
                }

            if (requisitionPackageCollection.Count > 0)
                for (int i = 0; i < requisitionPackageCollection.Count; i++)
                {
                    RequisitionPackageInfo up = new RequisitionPackageInfo();
                   

                    up = RequisitionPackageCollection.ElementAt(i);
                   

                    if (up.PlacedQuantity <= 0)
                    {
                        MessageBox.Show("Quantity can not be zero or negative");///Proper Error Message Required!!
                        return;
                    }

                    if (selectedType == "Return Onprocessing Goods")
                    {


                        if (up.PlacedQuantity > _context.InventoryPackageInfos.Where(r => r.InventoryId == UserInfo.Inventory.InventoryId).Where(e => e.PackageId == up.PackageId).First().OnProcessingQuantity)
                        {
                            MessageBox.Show("Not enough on processing package in stock!");
                            return;
                        }
                    }
                    if (selectedType == "Return Finished Goods")
                    {


                        if (up.PlacedQuantity > _context.InventoryPackageInfos.Where(r => r.InventoryId == UserInfo.Inventory.InventoryId).Where(e => e.PackageId == up.PackageId).First().FinishedQuantity)
                        {
                            MessageBox.Show("Not enough finished package in stock!");
                            return;
                        }
                    }





                    if (up.PlacedQuantity <= 0)
                    {
                        MessageBox.Show("Quantity can not be zero or negative");///Proper Error Message Required!!
                        return;
                    }
                    newRequisition.RequisitionPackageInfoes.Add(up);
                    flag++;

                   
                }

            if (flag == 0)
                return;

          

            newRequisition.DateOfIssue = DateTime.Now;
            newRequisition.DateOfApproval = DateTime.Now;
            newRequisition.InventoryId = UserInfo.Inventory.InventoryId;       //Authentication required////
            newRequisition.IssuedById = UserInfo.EmployeeID;                   //Authentication required DO ID////
            newRequisition.IssuedToId = UserInfo.Inventory.StoreInChargeId;    //ID of SIC Authentication required////
            newRequisition.Status = "Pending";


            newRequisition.RequisitionCode = DateTime.Now.ToString();
            newRequisition.RequisitionType = selectedType;        // newRequisition.Employee = _context.Employees.Where(e=>e.UserName==UserInfo.Username).First();

            if (newRequisition.HasValidationErrors==false)
            {
                _context.Requisitions.Add(newRequisition);
                _context.SubmitChanges(OnSubmitCompleted, null);
                busy.Show();
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
           //     MessageBox.Show(op.Error.ToString());
             //   op.MarkErrorAsHandled();
            }
            else
            {
                MessageBox.Show("Requisition placed");
                Reset();

            }

        }

        private void Reset()
        {
            
          
            CreateAllObjects();
            RequisitionProductCollection.Clear();
            RequisitionPackageCollection.Clear();
          
        }

        private void DeleteProduct() //to delete product from datagrid
        {
            try
            {
                if (selectedRequisitionProductInfo > -1)
                {
                    requisitionProductCollection.RemoveAt(selectedRequisitionProductInfo);

                }
            }
            catch (Exception ex)
            { }


        }

        private void DeletePackage() //to delete package from datagrid
        {
            try
            {
                if (SelectedRequisitionPackageInfo > -1)
                {
                    requisitionPackageCollection.RemoveAt(SelectedRequisitionPackageInfo);

                }
            }
            catch (Exception ex)
            { }

        }


        private void AddProductToCollection()   //To Add AutoComplete box selected product to collection
        {
            if (selectedlot == null)
                return;
           
            try
            {
                
                if (requisitionProductCollection.Any(e => e.ProductId == selectedProduct.ProductId && e.LotId == selectedlot) ||String.IsNullOrEmpty(selectedProduct.Product.ProductCode))
                    return;
                RequisitionProductInfo R = new RequisitionProductInfo();
                R.Product = selectedProduct.Product;
                R.LotId = selectedlot;
                R.LotQuantity = 0;                ////Lot Quantity is  Accepted Quantity from a allocated lot//////
                R.PlacedQuantity = 0;
                requisitionProductCollection.Add(R);
                lotCollection.Clear();
                SelectedProduct = new InventoryProductInfo();
            }
            catch (Exception ex)
            {
                return;
            }
            selectedlot = null;

        }

        private void AddPackageToCollection()   //To Add AutoComplete box selected package to collection
        {

            try
            {
                if (requisitionPackageCollection.Any(e => e.PackageId == selectedPackage.PackageId||String.IsNullOrEmpty(selectedPackage.Package.PackageCode)))
                    return;

                RequisitionPackageInfo R = new RequisitionPackageInfo();
                R.Package = selectedPackage.Package;
                R.AcceptedQuantity = 0;

                R.PlacedQuantity = 0;
                requisitionPackageCollection.Add(R);
            }
            catch (Exception ex)
            {
                return;
            }

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

        private void LoadProducts()
        {
            _context.Load(_context.GetInventoryProductInfoesQuery().Where(e => e.InventoryId == UserInfo.Inventory.InventoryId&&e.Product.ProductName!="DUMMY")).Completed += new EventHandler(LoadProduct_Completed);   //Inventory ID from authentication////



        }

        private void LoadPackage()
        {
            _context.Load(_context.GetInventoryPackageInfoesQuery().Where(e => e.InventoryId == UserInfo.Inventory.InventoryId)).Completed += new EventHandler(LoadPackage_Completed);   //Inventory ID from authentication///

        }

        public ICommand SaveCommand { get; set; }

        public ICommand ResetCommand { get; set; }
        //Product DataGrid Button Command
        public ICommand DeleteSelectedProduct { get; set; }
        //Package DataGrid Button Command
        public ICommand DeleteSelectedPackage { get; set; }

        public ObservableCollection<RequisitionProductInfo> RequisitionProductCollection { get { return requisitionProductCollection; } }
        public ObservableCollection<RequisitionPackageInfo> RequisitionPackageCollection { get { return requisitionPackageCollection; } }
        public ObservableCollection<String> Type { get { return RequisitionTypeName; } }
        public ObservableCollection<String> Lots { get { return lotCollection; } }
        public ObservableCollection<InventoryProductInfo> Products { get { return inventoryProductCollection; } }

        public ObservableCollection<InventoryPackageInfo> Packages { get { return inventoryPackageCollection; } }
        /// <summary>
        /// The <see cref="NewRequisition" /> property's name.
        /// </summary>
        public const string NewRequisitionPropertyName = "NewRequisition";

        private Requisition newRequisition ;

        /// <summary>
        /// Sets and gets the NewRequisition property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Requisition NewRequisition
        {
            get
            {
                return newRequisition;
            }

            set
            {
                if (newRequisition == value)
                {
                    return;
                }

                newRequisition = value;
                RaisePropertyChanged(NewRequisitionPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SelectedRequisitionProduct" /> property's name.
        /// </summary>
        public const string SelectedRequisitionProductPropertyName = "SelectedRequisitionProduct";

        private int selectedRequisitionProductInfo ;

        /// <summary>
       ///To Delete a Selected product item from collection when delete button is pressed
        /// </summary>
        public int SelectedRequisitionProduct
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
        /// The <see cref="SelectedRequisitionPackageInfo" /> property's name.
        /// </summary>
        public const string SelectedRequisitionPackageInfoPropertyName = "SelectedRequisitionPackageInfo";

        private int selectedRequisitionPackageInfo;

        /// <summary>
        ///To Delete a Selected package item from collection when delete button is pressed
        /// </summary>
        public int SelectedRequisitionPackageInfo
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
        /// The <see cref="RCode" /> property's name.
        /// </summary>
        public const string RCodePropertyName = "RCode";

        private String rCode  ;

        /// <summary>
        /// Sets and gets the RCode property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public String RCode
        {
            get
            {
                return rCode;
            }

            set
            {
                if (rCode == value)
                {
                    return;
                }

                rCode = value;
                checkRCode = value;
                RaisePropertyChanged(RCodePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SelectedType" /> property's name.
        /// </summary>
        public const string SelectedTypePropertyName = "SelectedType";

        private String selectedType ;

        /// <summary>
        /// Sets and gets the SelectedType property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public String SelectedType
        {
            get
            {
                return selectedType;
            }

            set
            {
                if (selectedType == value)
                {
                    return;
                }

                selectedType = value;
                RaisePropertyChanged(SelectedTypePropertyName);
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
