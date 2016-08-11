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
    public class CreateRequisitionViewModel:ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        int busyFlag;
        String checkRCode;
        BusyWindow busy = new BusyWindow();
        ObservableCollection<RequisitionProductInfo> requisitionProductCollection = new ObservableCollection<RequisitionProductInfo>();
        ObservableCollection<RequisitionPackageInfo> requisitionPackageCollection = new ObservableCollection<RequisitionPackageInfo>();
        ObservableCollection<InventoryProductInfo> inventoryProductCollection = new ObservableCollection<InventoryProductInfo>();
        
        Inventory UserInventory = new Inventory();
        bool ResetFlag = false;

        public CreateRequisitionViewModel()
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
         //   _context.Load(_context.GetEmployeesByUsernameQuery(UserInfo.Username));
        
        }
        void LoadRequisitions()
        {
            _context.Load(_context.GetRequisitionsQuery().Where(e => e.Inventory.InventoryId == UserInfo.Inventory.InventoryId)).Completed += new EventHandler(CreateRequisitionViewModel_Completed);
               
        }

        void ProductLoad_Completed(object sender, EventArgs e)
        {
            busyFlag++;
            if (busyFlag == 2)
                busy.Close();
         
           
        }

        void CreateRequisitionViewModel_Completed(object sender, EventArgs e)
        {
            busyFlag++;
            if (busyFlag == 2)
                busy.Close();
           
        }

     

        private void CreateAllObjects()
        {
            SelectedPackage = new InventoryPackageInfo();
            SelectedPackage.PackageId = -1;
            SelectedProduct = new Product();
            SelectedProduct.ProductId = -1;
            newRequisition = new Requisition();
            selectedRequisitionProductInfo = -1;
            selectedRequisitionPackageInfo = -1;
        }

        private void Save()
        { 
           
            _context.RequisitionProductInfos.Clear();
            _context.RequisitionPackageInfos.Clear();

           

            int flag = 0;

            if (requisitionProductCollection.Count > 0)
                for (int i = 0; i < requisitionProductCollection.Count; i++)
                {
                    RequisitionProductInfo up = new RequisitionProductInfo();



                    up = requisitionProductCollection.ElementAt(i);
                   

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
            newRequisition.RequisitionType = ConstantCollections.RequisitionTypeName.ElementAt(0);
           // newRequisition.Employee = _context.Employees.Where(e=>e.UserName==UserInfo.Username).First();

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
              //  MessageBox.Show(op.Error.ToString());
              //  op.MarkErrorAsHandled();
            }
            else
            {
                MessageBox.Show("Requisition placed");
                Reset();

            }

        }

        private void Reset()
        {
            
            ResetFlag = true;
            CreateAllObjects();
            RequisitionProductCollection.Clear();
            RequisitionPackageCollection.Clear();
            ResetFlag = false;
        }

        private void DeleteProduct() //to delete product from datagrid
        {
            if (selectedRequisitionProductInfo > -1)
            {
               
                  requisitionProductCollection.RemoveAt(selectedRequisitionProductInfo);

              
            }


        }

        private void DeletePackage() //to delete package from datagrid
        {
          
            if (SelectedRequisitionPackageInfo>-1)
            {                                             
                requisitionPackageCollection.RemoveAt(SelectedRequisitionPackageInfo);
               
            }

        }


         private void AddProductToCollection()   //To Add AutoComplete box selected product to collection
        {
            try
            {
                if (requisitionProductCollection.Any(e => e.ProductId == selectedProduct.ProductId)||String.IsNullOrEmpty(selectedProduct.ProductCode))
                    return;
                RequisitionProductInfo R = new RequisitionProductInfo();
                R.Product = selectedProduct;
                R.LotId = "-1";
                R.LotQuantity = 0;                ////Lot Quantity is  Accepted Quantity from a allocated lot//////
                R.PlacedQuantity = 0;
                requisitionProductCollection.Add(R);
            }
            catch (Exception ex)
            {
                return;
            }
            SelectedProduct = new Product();
        }

        private void AddPackageToCollection()   //To Add AutoComplete box selected package to collection
        {
            try
            {
                

                if (requisitionPackageCollection.Any(e => e.PackageId == selectedPackage.PackageId)||String.IsNullOrEmpty(selectedPackage.Package.PackageCode))
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
            SelectedPackage = new InventoryPackageInfo();
        }

        private void LoadProducts()
        {
            _context.Load(_context.GetProductsQuery().Where(e=>e.ProductName!="DUMMY")).Completed+=new EventHandler(ProductLoad_Completed);   //Inventory ID from authentication////



        }

        private void LoadPackage()
        {
            _context.Load(_context.GetInventoryPackageInfoesQuery().Where(e => e.InventoryId == UserInfo.Inventory.InventoryId)).Completed += new EventHandler(CreateRequisitionViewModel_Completed);   //Inventory ID from authentication///

        }


        public ICommand SaveCommand { get; set; }

        public ICommand ResetCommand { get; set; }
        //Product DataGrid Button Command
        public ICommand DeleteSelectedProduct { get; set; }
        //Package DataGrid Button Command
        public ICommand DeleteSelectedPackage { get; set; }

        public ObservableCollection<RequisitionProductInfo> RequisitionProductCollection { get { return requisitionProductCollection; } }
        public ObservableCollection<RequisitionPackageInfo> RequisitionPackageCollection { get { return requisitionPackageCollection; } }

        public EntitySet<Product> Products { get { return _context.Products;} }

        public EntitySet<InventoryPackageInfo> Packages { get { return _context.InventoryPackageInfos; } }
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
               // AddProductToCollection();
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


        public ICommand AddProduct { get; set; }

        public ICommand AddPackage { get; set; }

    }
}
