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

namespace TrialBusinessManager.ViewModels
{

    public class UpdateFinishedGoodViewModel:ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        BusyWindow busy = new BusyWindow();
        int busyFlag;
        ObservableCollection<FinishedProductUpdateModel> inventoryProductCollection = new ObservableCollection<FinishedProductUpdateModel>();
        ObservableCollection<FinishedPackageUpdateModel> inventoryPackageCollection = new ObservableCollection<FinishedPackageUpdateModel>();
        ObservableCollection<InventoryProductInfo> _InventoryProductCollection = new ObservableCollection<InventoryProductInfo>();
        ObservableCollection<String> lotCollection = new ObservableCollection<string>();
        Inventory UserInventory = new Inventory();

        bool ResetFlag = false;
        int BusyFlag = 0;

        public UpdateFinishedGoodViewModel()
        {
            busy.Show();

            LoadProducts();
            LoadPackage();

            SaveCommand = new RelayCommand(Save);
            ResetCommand = new RelayCommand(Reset);
            AddProduct = new RelayCommand(AddProductToCollection);
            AddPackage = new RelayCommand(AddPackageToCollection);


            DeleteSelectedProduct = new RelayCommand(DeleteProduct);
            DeleteSelectedPackage = new RelayCommand(DeletePackage);

            CreateAllObjects();
          
        
        }


        private void CreateAllObjects()
        {
            selectedPackage = new InventoryPackageInfo();
            selectedPackage.PackageId = -1;
            selectedProduct = new InventoryProductInfo();
            selectedProduct.ProductId = 1;

        }
        void LoadProduct_Completed(object sender, EventArgs e)
        {
            if (BusyFlag == 0)
                BusyFlag = 1;
            else
                busy.Close();

            foreach (InventoryProductInfo x in _context.InventoryProductInfos)
            {
                if (x.Product.ProductName != "DUMMY")
                {
                    if (!_InventoryProductCollection.Any(r => r.Product == x.Product))
                    {
                        _InventoryProductCollection.Add(x);
                    }
                }
            }

        }

        private bool Validate()
        {
            int flag = 0;
            if (inventoryProductCollection.Count > 0)
                for (int i = 0; i < inventoryProductCollection.Count; i++)
                {
                    //  RequisitionProductInfo reqProduct = new RequisitionProductInfo();
                    FinishedProductUpdateModel finishedProduct = new FinishedProductUpdateModel();
                    finishedProduct = inventoryProductCollection.ElementAt(i);
                 
                    if (finishedProduct.UpdateQuantity <= 0)
                    {
                        MessageBox.Show("Quantity can not be zero or negative");///Proper Error Message Required!!
                        return false;
                    }
                    if (finishedProduct.UpdateQuantity * finishedProduct.UpdateProduct.StockKeepingUnit > finishedProduct.OnProcessingQuantity)
                    {
                        MessageBox.Show("Not enough product to update!");///Proper Error Message Required!!
                        return false;
                    }

                    flag++;

                 
                }

            if (inventoryPackageCollection.Count > 0)
                for (int i = 0; i < inventoryPackageCollection.Count; i++)
                {
                    InventoryPackageInfo up = new InventoryPackageInfo();
                    FinishedPackageUpdateModel finishedPackage = new FinishedPackageUpdateModel();

                    finishedPackage = InventoryPackageCollection.ElementAt(i);
                    if (finishedPackage.UpdateQuantity <= 0)
                    {
                        MessageBox.Show("Quantity can not be zero or negative");///Proper Error Message Required!!
                        return false;
                    }
                    if (finishedPackage.UpdateQuantity > finishedPackage.OnProcessingQuantity)
                    {
                        MessageBox.Show("Not enough package to update!");///Proper Error Message Required!!
                        return false;
                    }

                    flag++;

         }

            if (flag == 0)
                return false;

            return true;
    }

        private void Save()
        {
            if (Validate())
            {
                int flag = 0;

                if (inventoryProductCollection.Count > 0)
                    for (int i = 0; i < inventoryProductCollection.Count; i++)
                    {
                        //  RequisitionProductInfo reqProduct = new RequisitionProductInfo();
                        FinishedProductUpdateModel finishedProduct = new FinishedProductUpdateModel();
                        InventoryProductInfo up = new InventoryProductInfo();
                        InventoryProductInfo dummyProduct = new InventoryProductInfo();

                        finishedProduct = inventoryProductCollection.ElementAt(i);
                        dummyProduct = _context.InventoryProductInfos.Where(e => e.InventoryId == UserInfo.Inventory.InventoryId && e.Product.GroupName == finishedProduct.UpdateProduct.GroupName && e.Product.ProductName == "DUMMY" && e.LotId == finishedProduct.LotId).Single();
                        up = _context.InventoryProductInfos.Where(e => e.InventoryId == UserInfo.Inventory.InventoryId && e.ProductId == finishedProduct.UpdateProduct.ProductId && e.LotId == finishedProduct.LotId).First();

                        flag++;

                        /* this will be done while accepting the requisition //*/
                        ////////Update is done is Pc,that's why '*finishedProduct.UpdateProduct.StockKeepingUnit' this is done//////////////// 
                        up.OnProcessingQuantity -= finishedProduct.UpdateQuantity * finishedProduct.UpdateProduct.StockKeepingUnit;
                        up.FinishedQuantity += finishedProduct.UpdateQuantity * finishedProduct.UpdateProduct.StockKeepingUnit;
                      
                        dummyProduct.FinishedQuantity += finishedProduct.UpdateQuantity * finishedProduct.UpdateProduct.StockKeepingUnit;
                        dummyProduct.OnProcessingQuantity -= finishedProduct.UpdateQuantity * finishedProduct.UpdateProduct.StockKeepingUnit;

                        CreateLog(up, finishedProduct.UpdateQuantity * finishedProduct.UpdateProduct.StockKeepingUnit, true);
                        CreateLog(up, finishedProduct.UpdateQuantity * finishedProduct.UpdateProduct.StockKeepingUnit, false);

                    }

                if (inventoryPackageCollection.Count > 0)
                    for (int i = 0; i < inventoryPackageCollection.Count; i++)
                    {
                        InventoryPackageInfo up = new InventoryPackageInfo();
                        FinishedPackageUpdateModel finishedPackage = new FinishedPackageUpdateModel();

                        finishedPackage = InventoryPackageCollection.ElementAt(i);
                        up = _context.InventoryPackageInfos.Where(e => e.Package.PackageId == finishedPackage.UpdatePackage.PackageId).First();

                        flag++;


                        up.OnProcessingQuantity -= finishedPackage.UpdateQuantity;
                        up.FinishedQuantity += finishedPackage.UpdateQuantity;

                    }

                if (flag == 0)
                    return;

                busy.Show();
                _context.SubmitChanges(OnSubmitCompleted, null);
            }

        }

        void CreateLog(InventoryProductInfo Info, Double Quantity, bool flag)
        {
            InventoryLog Log = new InventoryLog();
            if (flag == true) Log.Method = "Finished Good Update From";
            else Log.Method = "Finished Good Update To";
            Log.ProductId = Info.ProductId;
            Log.Date = DateTime.Now;
            Log.InventoryId = UserInfo.Inventory.InventoryId;
            Log.MemoNo = 0;
            Log.ProductQuantity = Quantity;
            Log.LotId = Info.LotId;
            Log.ProductCost = Quantity * (double)(Info.UnitLotCost);
            _context.InventoryLogs.Add(Log);
        }

        private void OnSubmitCompleted(SubmitOperation op)
        {

            if (op.HasError)
            {
              //  MessageBox.Show(op.Error.ToString());
                //op.MarkErrorAsHandled();
            }
            else
            {
                busy.Close();
               
                Reset();
                MessageBox.Show("Inventory Updated.");
            }

        }

        //Causing problem is Reset,can not reset SelectedPackage.which causes null reference exception in AddPackageToCollection()//
        private void Reset()
        {
            ResetFlag = true;
            inventoryProductCollection.Clear();
            inventoryPackageCollection.Clear();

            CreateAllObjects();
            ResetFlag = false;
       
           
        }

        private void DeleteProduct() //to delete product from datagrid
        {
            try
            {

                if (selectedRequisitionProductInfo > -1)
                {
                    inventoryProductCollection.RemoveAt(selectedRequisitionProductInfo);

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
                    inventoryPackageCollection.RemoveAt(SelectedRequisitionPackageInfo);

                }
            }
            catch (Exception ex)
            { }

        }


         private void AddProductToCollection() //To Add AutoComplete box selected product to collection
        {
            try
            {
                if (ResetFlag == true)
                    return;
                if (String.IsNullOrEmpty(selectedProduct.Product.ProductCode))
                    return;
              /*  if (inventoryProductCollection.Any(e => e.UpdateProduct.ProductId == selectedProduct.ProductId && e.LotId == selectedProduct.LotId))
                    return;*/

                FinishedProductUpdateModel finishedProduct = new FinishedProductUpdateModel();
                finishedProduct.UpdateProduct = selectedProduct.Product;
                finishedProduct.OnProcessingQuantity = _context.InventoryProductInfos.Where(e => e.InventoryId == UserInfo.Inventory.InventoryId && e.ProductId == selectedProduct.ProductId&&e.LotId==selectedlot).First().OnProcessingQuantity;
                finishedProduct.UpdateQuantity = 0;
                finishedProduct.LotId = selectedlot;
                if (selectedlot == null)
                    return;
                inventoryProductCollection.Add(finishedProduct);
                selectedlot = null;
            }
            catch (Exception ex)
            { }

        }

        private void AddPackageToCollection()//To Add AutoComplete box selected package to collection
        {
            try
            {
                
                if (inventoryPackageCollection.Any(e => e.UpdatePackage.PackageId == selectedPackage.PackageId) || String.IsNullOrEmpty(selectedPackage.Package.PackageCode))
                    return;

                FinishedPackageUpdateModel finishedPackage = new FinishedPackageUpdateModel();
                finishedPackage.UpdatePackage = selectedPackage.Package;
                finishedPackage.OnProcessingQuantity = selectedPackage.OnProcessingQuantity;
                finishedPackage.UpdateQuantity = 0;
                inventoryPackageCollection.Add(finishedPackage);
            }
            catch (Exception ex)
            { }
        }

        private void LoadProducts()
        {
            _context.Load(_context.GetInventoryProductInfoesQuery().Where(e => e.InventoryId == UserInfo.Inventory.InventoryId)).Completed += new EventHandler(LoadProduct_Completed);   //Inventory ID from authentication////



        }

        void UpdateFinishedGoodViewModelCompleted(object sender, EventArgs e)
        {
            if (BusyFlag == 0)
                BusyFlag = 1;
            else
                busy.Close();
        }

        private void LoadPackage()//inventory id will come from authentication//
        {
            _context.Load(_context.GetInventoryPackageInfoesQuery().Where(e => e.InventoryId == UserInfo.Inventory.InventoryId && e.OnProcessingQuantity > 0)).Completed += new EventHandler(UpdateFinishedGoodViewModel_Completed);    //Inventory ID from authentication///

        }

        void UpdateFinishedGoodViewModel_Completed(object sender, EventArgs e)
        {

            if (BusyFlag == 0)
                BusyFlag = 1;
            else
                busy.Close();
                
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

        public ObservableCollection<FinishedProductUpdateModel> InventoryProductCollection { get { return inventoryProductCollection; } }
        public ObservableCollection<FinishedPackageUpdateModel> InventoryPackageCollection { get { return inventoryPackageCollection; } }
        public ObservableCollection<String> Lots { get { return lotCollection; } }
        public ObservableCollection<InventoryProductInfo> Products { get { return _InventoryProductCollection; } }

        public EntitySet<InventoryPackageInfo> Packages { get { return _context.InventoryPackageInfos; } }
     
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


                RaisePropertyChanged(SelectedPackagePropertyName);
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
