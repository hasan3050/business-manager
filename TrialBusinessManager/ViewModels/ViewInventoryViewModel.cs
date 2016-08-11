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
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace TrialBusinessManager.ViewModels
{
    public class ViewInventoryViewModel:ViewModelBase
    {

       
        AgroDomainContext _context = new AgroDomainContext();
        int BusyFlag = 0;
        BusyWindow busy = new BusyWindow();
        ObservableCollection<String> _Regions = new ObservableCollection<String>();
        public ViewInventoryViewModel()
        {
            RegisterCommands();
    

            InventoryProductsCollection = new ObservableCollection<InventoryProductModel>();
            InventoryPackageCollection = new ObservableCollection<InventoryPackageModel>();

            selectedProduct =CreateProductInfo();
            selectedPackage = CreatePackageInfo();

          
            
            busy.Show();

            if (UserInfo.Designation == "Store In Charge" || UserInfo.Designation == "Dispatch Officer")
            {
                LoadInventoryPackages();
                LoadInventoryProducts();
            }
            else if (UserInfo.Designation == "Regional Manager")
            {
                LoadRMInventoryProducts();
                LoadRMInventoryPackages();
            }
            else
            {
                LoadAllInventoryPackages();
                LoadAllInventoryProducts();

            }
        
        }


        private InventoryProductModel CreateProductInfo()
        {

            InventoryProductModel NewProductInfo = new InventoryProductModel();
            return NewProductInfo;
        
        }

        private InventoryPackageInfo CreatePackageInfo()
        {

            InventoryPackageInfo NewPackageInfo = new InventoryPackageInfo();
            return NewPackageInfo;
        
        
        }

        void Update()
        {
            try
            {
                InventoryProductsCollection.Clear();
                InventoryPackageCollection.Clear();

                ////////////////////////////////////////If All is selected///////////////////////////////////////////////////
                if (_SelectedRegion == "All")
                {
                    //////////////Updating Collection for Products///////////////////
                    foreach (InventoryProductInfo x in _context.InventoryProductInfos)
                    {


                        if (InventoryProductsCollection.Any(e => e.ProductCode == x.Product.ProductCode && e.LotId == x.LotId))
                        {

                            InventoryProductsCollection.Where(e => e.ProductCode == x.Product.ProductCode && e.LotId == x.LotId).First().UnfinishedQuantity += x.UnfinishedQuantity;
                            InventoryProductsCollection.Where(e => e.ProductCode == x.Product.ProductCode && e.LotId == x.LotId).First().FinishedQuantity += x.FinishedQuantity;
                            InventoryProductsCollection.Where(e => e.ProductCode == x.Product.ProductCode && e.LotId == x.LotId).First().OnProcessingQuantity += x.OnProcessingQuantity;


                        }
                        else
                        {
                            InventoryProductInfo selectedOne = new InventoryProductInfo();
                            InventoryProductModel inventoryProduct = new InventoryProductModel();
                            selectedOne = x;
                            inventoryProduct.GroupName = selectedOne.Product.GroupName;
                            inventoryProduct.ProductCode = selectedOne.Product.ProductCode;
                            inventoryProduct.BrandName = selectedOne.Product.BrandName;
                            inventoryProduct.UnfinishedQuantity = selectedOne.UnfinishedQuantity;
                            inventoryProduct.FinishedQuantity = selectedOne.FinishedQuantity;
                            inventoryProduct.OnProcessingQuantity = selectedOne.OnProcessingQuantity;
                            inventoryProduct.LotId = selectedOne.LotId;
                            inventoryProduct.PurchasePrice = (Double)selectedOne.UnitLotCost;
                            inventoryProduct.TotalProductQuantity = inventoryProduct.UnfinishedQuantity + inventoryProduct.FinishedQuantity + inventoryProduct.OnProcessingQuantity;
                            InventoryProductsCollection.Add(inventoryProduct);
                        }


                    }


                    //////////////Updating Collection for Packages/////////////
                    foreach (InventoryPackageInfo x in _context.InventoryPackageInfos)
                    {
                        if (InventoryPackageCollection.Any(e => e.PackageCode == x.Package.PackageCode))
                        {

                            InventoryPackageCollection.Where(e => e.PackageCode == x.Package.PackageCode).First().UnfinishedQuantity += x.UnfinishedQuantity;
                            InventoryPackageCollection.Where(e => e.PackageCode == x.Package.PackageCode).First().FinishedQuantity += x.FinishedQuantity;
                            InventoryPackageCollection.Where(e => e.PackageCode == x.Package.PackageCode).First().OnProcessingQuantity += x.OnProcessingQuantity;


                        }
                        else
                        {
                            InventoryPackageInfo selectedOne = new InventoryPackageInfo();
                            InventoryPackageModel inventoryPackage = new InventoryPackageModel();
                            selectedOne = x;
                            inventoryPackage.PackageName = selectedOne.Package.PackageName;
                            inventoryPackage.PackageCode = selectedOne.Package.PackageCode;
                            inventoryPackage.UnfinishedQuantity = selectedOne.UnfinishedQuantity;
                            inventoryPackage.FinishedQuantity = selectedOne.FinishedQuantity;
                            inventoryPackage.OnProcessingQuantity = selectedOne.OnProcessingQuantity;
                            inventoryPackage.PurchasePrice = (Double)selectedOne.UnitCost;
                            inventoryPackage.TotalPackageQuantity = inventoryPackage.UnfinishedQuantity + inventoryPackage.FinishedQuantity + inventoryPackage.OnProcessingQuantity;
                            InventoryPackageCollection.Add(inventoryPackage);
                        }

                     
                    }
                }
                //////////////////////////////////////////////////////////////////////////////////////////////////////
                //////////////////////If All is not selected,any region is selected///////////////////////////////////
                else
                {

                    //////////////Updating Collection for Products///////////////////
                    foreach (InventoryProductInfo x in _context.InventoryProductInfos)
                    {

                        if (x.Inventory.InventoryName == _SelectedRegion)
                        {
                            InventoryProductInfo selectedOne = new InventoryProductInfo();
                            InventoryProductModel inventoryProduct = new InventoryProductModel();
                            selectedOne = x;
                            inventoryProduct.GroupName = selectedOne.Product.GroupName;
                            inventoryProduct.ProductCode = selectedOne.Product.ProductCode;
                            inventoryProduct.BrandName = selectedOne.Product.BrandName;
                            inventoryProduct.UnfinishedQuantity = selectedOne.UnfinishedQuantity;
                            inventoryProduct.FinishedQuantity = selectedOne.FinishedQuantity;
                            inventoryProduct.OnProcessingQuantity = selectedOne.OnProcessingQuantity;
                            inventoryProduct.LotId = selectedOne.LotId;
                            inventoryProduct.PurchasePrice = (Double)selectedOne.UnitLotCost;
                            inventoryProduct.TotalProductQuantity = inventoryProduct.UnfinishedQuantity + inventoryProduct.FinishedQuantity + inventoryProduct.OnProcessingQuantity;
                            InventoryProductsCollection.Add(inventoryProduct);


                        }

                    }
                    //////////////Updating Collection for Packages//////////////////////
                    foreach (InventoryPackageInfo x in _context.InventoryPackageInfos)
                    {
                        if (x.Inventory.InventoryName == _SelectedRegion)
                        {
                            InventoryPackageInfo selectedOne = new InventoryPackageInfo();
                            InventoryPackageModel inventoryPackage = new InventoryPackageModel();
                            selectedOne = x;
                            inventoryPackage.PackageName = selectedOne.Package.PackageName;
                            inventoryPackage.PackageCode = selectedOne.Package.PackageCode;
                            inventoryPackage.UnfinishedQuantity = selectedOne.UnfinishedQuantity;
                            inventoryPackage.FinishedQuantity = selectedOne.FinishedQuantity;
                            inventoryPackage.OnProcessingQuantity = selectedOne.OnProcessingQuantity;
                            inventoryPackage.PurchasePrice = (Double)selectedOne.UnitCost;
                            inventoryPackage.TotalPackageQuantity = inventoryPackage.UnfinishedQuantity + inventoryPackage.FinishedQuantity + inventoryPackage.OnProcessingQuantity;
                            InventoryPackageCollection.Add(inventoryPackage);

                        }


                    }
                }

            }

            catch (Exception ex)
            {
               
                return;
            }
        
        
        }

        void LoadAllInventoryProducts()
        {
            _context.Load(_context.GetInventoriesQuery()).Completed += (s, args) =>
            {
                _Regions.Add("All");

                foreach (Inventory x in _context.Inventories)
                {
                    _Regions.Add(x.InventoryName);
                }


                BusyFlag++;
                if (BusyFlag==3)
                    busy.Close();
            };
            _context.Load(_context.GetInventoryProductInfoesQuery().Where(e=>e.Product.ProductName=="DUMMY")).Completed += (s, args) =>
            {
                BusyFlag++;
                if (BusyFlag == 3)
                    busy.Close();
            };
        
        }

        void LoadRMInventoryProducts()
        {
            _context.Load(_context.GetInventoriesQuery().Where(r=>r.RegionId==UserInfo.Region.RegionId)).Completed += (s, args) =>
            {
                _Regions.Add("All");

                foreach (Inventory x in _context.Inventories)
                {
                    _Regions.Add(x.InventoryName);
                }


                BusyFlag++;
                if (BusyFlag == 3)
                    busy.Close();
            };
            _context.Load(_context.GetInventoryProductInfoesQuery().Where(e => e.Product.ProductName == "DUMMY").Where(r => r.Inventory.RegionId == UserInfo.Region.RegionId)).Completed += (s, args) =>
            {
                BusyFlag++;
                if (BusyFlag == 3)
                    busy.Close();
            };

        }

        void LoadRMInventoryPackages()
        {
           _context.Load(_context.GetInventoryPackageInfoesQuery().Where(r=>r.Inventory.RegionId==UserInfo.Region.RegionId)).Completed += (s, args) =>
            {
                BusyFlag++;
                if (BusyFlag == 3)
                    busy.Close();
            };
        }

        void LoadAllInventoryPackages()
        {

            _context.Load(_context.GetInventoryPackageInfoesQuery()).Completed += (s, args) =>
            {
                BusyFlag++;
                if (BusyFlag == 3)
                    busy.Close();
            };
        }

        void LoadInventoryProducts()
        {
            LoadOperation<InventoryProductInfo> loadProductInfo = _context.Load(_context.GetInventoryProductInfoesQuery().Where(e =>e.Inventory.InventoryId == UserInfo.Inventory.InventoryId&&e.Product.ProductName=="DUMMY"));////****InventoryID will come from Authentication***

            loadProductInfo.Completed += (s, arg) =>
            {

                for (int i = 0; i < loadProductInfo.Entities.Count(); i++)
                {

                    InventoryProductInfo selectedOne = new InventoryProductInfo();
                    InventoryProductModel inventoryProduct = new InventoryProductModel();
                    selectedOne = loadProductInfo.Entities.ElementAt(i);
                    inventoryProduct.GroupName = selectedOne.Product.GroupName;
                    inventoryProduct.ProductCode = selectedOne.Product.ProductCode;
                    inventoryProduct.BrandName = selectedOne.Product.BrandName;
                    inventoryProduct.PurchasePrice = (Double)selectedOne.UnitLotCost;
                    inventoryProduct.UnfinishedQuantity = selectedOne.UnfinishedQuantity;
                    inventoryProduct.FinishedQuantity = selectedOne.FinishedQuantity;
                    inventoryProduct.OnProcessingQuantity = selectedOne.OnProcessingQuantity;
                    inventoryProduct.LotId = selectedOne.LotId;
                    inventoryProduct.TotalProductQuantity = inventoryProduct.UnfinishedQuantity + inventoryProduct.FinishedQuantity + inventoryProduct.OnProcessingQuantity;
                    InventoryProductsCollection.Add(inventoryProduct);

                }

                if (BusyFlag == 0)
                    BusyFlag = 1;
                else
                    busy.Close();
              

            };
        
        }

        private void LoadInventoryPackages()
        {

            LoadOperation<InventoryPackageInfo> loadPackageInfo = _context.Load(_context.GetInventoryPackageInfoesQuery().Where(e => e.Inventory.InventoryId == UserInfo.Inventory.InventoryId));////****InventoryID will come from Authentication***

            loadPackageInfo.Completed += (s, arg) =>
            {

                for (int i = 0; i < loadPackageInfo.Entities.Count(); i++)
                {

                    InventoryPackageInfo selectedOne = new InventoryPackageInfo();
                    InventoryPackageModel inventoryPackage = new InventoryPackageModel();
                    selectedOne = loadPackageInfo.Entities.ElementAt(i);
                    inventoryPackage.PackageName = selectedOne.Package.PackageName;
                    inventoryPackage.PackageCode = selectedOne.Package.PackageCode;

                    inventoryPackage.UnfinishedQuantity = selectedOne.UnfinishedQuantity;
                    inventoryPackage.FinishedQuantity = selectedOne.FinishedQuantity;
                    inventoryPackage.OnProcessingQuantity = selectedOne.OnProcessingQuantity;

                    inventoryPackage.TotalPackageQuantity = inventoryPackage.UnfinishedQuantity + inventoryPackage.FinishedQuantity + inventoryPackage.OnProcessingQuantity;
                    InventoryPackageCollection.Add(inventoryPackage);

                }

                if (BusyFlag == 0)
                    BusyFlag = 1;
                else
                    busy.Close();


            };
        
        }

        void RegisterCommands()
        {
            ProductChanged = new RelayCommand(ProductSelectionChanged);
        }

        void ProductSelectionChanged()
        {
            ChartVisibility = true;
            MyChart.Clear();
            
            MyChart.Add(new Chartview("finished", (Double)selectedProduct.FinishedQuantity));
            MyChart.Add(new Chartview("Unfinished", (Double)SelectedProduct.UnfinishedQuantity));
            MyChart.Add(new Chartview("On processing", (Double)SelectedProduct.OnProcessingQuantity));
        }

        public ObservableCollection<InventoryProductModel> InventoryProductsCollection { get; set; }

       
        public ObservableCollection<Chartview> MyChart { get; set; }

        public ObservableCollection<InventoryPackageModel> InventoryPackageCollection { get; set; }


        public ObservableCollection<String> Regions { get { return _Regions; } }
        //AutoCompleteBox For InventoryProduct will be binded with this
        public EntitySet<InventoryProductInfo> InventoryProducts { get { return _context.InventoryProductInfos; } }

        //AutoCompleteBox For InventoryPaclage will be binded with this
        public EntitySet<InventoryPackageInfo> InventoryPackages { get { return _context.InventoryPackageInfos; } }

        public ICommand ProductChanged { get; set; }

        /// <summary>
        /// The <see cref="SelectedProduct" /> property's name.
        /// </summary>
        /// 

        //**DataGrid's SelectedItem and BarChart Pie Chart will be binded with this property**
        public const string SeletedProductPropertyName = "SelectedProduct";

        private InventoryProductModel selectedProduct;

        /// <summary>
        /// Sets and gets the SeletedProduct property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public InventoryProductModel SelectedProduct
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
                RaisePropertyChanged(SeletedProductPropertyName);
            }
        }

        

        /// <summary>
        /// The <see cref="SelectedPackage" /> property's name.
        /// </summary>
        /// 

        //**DataGrid's SelectedItem and BarChart Pie Chart will be binded with this property**
        public const string SelectedPackagePropertyName = "SelectedPackage";

        private InventoryPackageInfo selectedPackage;

        /// <summary>
        /// Sets and gets the SelectedPackage property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public InventoryPackageInfo SelectedPackage
        {
            get
            {
                return selectedPackage;;
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

        /// <summary>
        /// The <see cref="TotalProductQuantity" /> property's name.
        /// </summary>
      
        /// <summary>
        /// The <see cref="TotalPackageQuabtity" /> property's name.
        /// </summary>

        /// <summary>
        /// The <see cref="ChartVisibility" /> property's name.
        /// </summary>
        public const string ChartVisibilityPropertyName = "ChartVisibility";

        private bool _chartVisibility = false;

        /// <summary>
        /// Sets and gets the ChartVisibility property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool ChartVisibility
        {
            get
            {
                return _chartVisibility;
            }

            set
            {
                if (_chartVisibility == value)
                {
                    return;
                }

                _chartVisibility = value;
                RaisePropertyChanged(ChartVisibilityPropertyName);
            }
        }

        public const string regionString = "SelectedRegion";

        private String _SelectedRegion;

        /// <summary>
        /// Sets and gets the ChartVisibility property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public String SelectedRegion
        {
            get
            {
                return _SelectedRegion;
            }

            set
            {
                if (_SelectedRegion == value)
                {
                    return;
                }

                _SelectedRegion = value;
                Update();
                RaisePropertyChanged(regionString);
            }
        }
        
        
        
    }
}
