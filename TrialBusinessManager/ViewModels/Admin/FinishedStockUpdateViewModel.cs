using System;
using System.Net;
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
using TrialBusinessManager.Views;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace TrialBusinessManager.ViewModels.Admin
{
    public class FinishedStockUpdateViewModel:ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        BusyWindow MyWindow = new BusyWindow();
        int flag = 0;
        
        public FinishedStockUpdateViewModel()
        {
            Initialize();
        }

        void RegisterCommands()
        {
            AddCommand = new RelayCommand(Additem);
            DeleteCommand = new RelayCommand(DeleteItem);
            SubmitCommand = new RelayCommand(Submit);
        }

        void Initialize()
        {
            MyWindow.Show();
            RegisterCommands();
            LoadData();
        }

        bool Validate()
        {
            foreach (var item in Infos)
            {
                if (item.FinishedQuantity <= 0)
                {
                    MessageBox.Show("You have inputs with invalid Unfinished Quantities");
                    return false;
                }

                if (item.UnitLotCost <= 0)
                {
                    MessageBox.Show("You have inputs with invalid Unit Lot Cost");
                    return false;
                }
            }
            
            return true;
        }

        void CreateLog(InventoryProductInfo Info, Double Quantity,Inventory Inventory)
        {
            InventoryLog Log = new InventoryLog();
            Log.Method = "Finished Stock Update";
            Log.ProductId = Info.ProductId;
            Log.Date = DateTime.Now;
            Log.InventoryId = Inventory.InventoryId;
            Log.MemoNo = 0;
            Log.ProductQuantity = Quantity;
            Log.LotId = Info.LotId;
            Log.ProductCost = Quantity * (double)(Info.UnitLotCost);
            _context.InventoryLogs.Add(Log);
        }

        void Reset()
        {
            Infos.Clear();
        }

        void Submit()
        {
            if (!Validate()) return;

            MyWindow.Show();

            

            foreach (var item in Infos)
            {
               
                
                if (_context.InventoryProductInfos.Any(c => c.InventoryId.Equals(item.Inventory.InventoryId) && c.ProductId.Equals(item.Product.ProductId) && c.LotId.Equals(item.LotId)))
                {
                    InventoryProductInfo ExistingInfo = new InventoryProductInfo();
                    ExistingInfo = _context.InventoryProductInfos.Where(c => (c.InventoryId.Equals(item.Inventory.InventoryId) && c.ProductId.Equals(item.Product.ProductId) && c.LotId.Equals(item.LotId))).Single();
                    ExistingInfo.FinishedQuantity += item.FinishedQuantity;
                    
                }
                else
                {
                    _context.InventoryProductInfos.Add(item);
                }

                if (_context.InventoryProductInfos.Any(c => c.InventoryId.Equals(item.Inventory.InventoryId) && c.Product.GroupName.Equals(item.Product.GroupName) && c.Product.ProductName.Equals("DUMMY") && c.LotId.Equals(item.LotId)))
                {
                    InventoryProductInfo ExistingInfo = new InventoryProductInfo();
                    ExistingInfo = _context.InventoryProductInfos.Where(c => c.InventoryId.Equals(item.Inventory.InventoryId) && c.Product.GroupName.Equals(item.Product.GroupName) && c.Product.ProductName.Equals("DUMMY") && c.LotId.Equals(item.LotId)).Single();
                    ExistingInfo.FinishedQuantity += item.FinishedQuantity;
                }
                else
                {
                    InventoryProductInfo DummyInfo = new InventoryProductInfo();
                    
                    DummyInfo.Product = new Product();
                    DummyInfo.Product = _context.Products.Where(c => c.GroupName.Equals(item.Product.GroupName) && c.ProductName.Equals("DUMMY")).Single();
                    DummyInfo.LotId = item.LotId;
                    DummyInfo.FinishedQuantity = item.FinishedQuantity;
                    DummyInfo.Inventory = item.Inventory;
                    DummyInfo.OnProcessingQuantity = 0;
                    DummyInfo.UnitLotCost = 0;
                   
                    _context.InventoryProductInfos.Add(DummyInfo);
                }

                CreateLog(item, item.FinishedQuantity, item.Inventory);
            }

            _context.SubmitChanges(OnSubmitCompleted,null);
        }

        void OnSubmitCompleted(SubmitOperation so)
        {
            MyWindow.Close();
            if (so.HasError)
            {
                MessageBox.Show("Failed,Please reload the page and try again");
                so.MarkErrorAsHandled();
            }
            else
            {
                MessageBox.Show("Stock Updated");
                Reset();
            }
        }

        void Additem()
        {
            if (Infos.Any(c => c.Inventory.Equals(SelectedInventory) && c.Product.Equals(SelectedProduct) && c.LotId.Equals(SelectedLot)))
            {
                MessageBox.Show("Your current selection is already added in the list,\nyou can edit the amount there.");
                return;
            }
            
            if (SelectedInventory == null)
            {
                MessageBox.Show("Please select the inventory");
                return;
            }
            
            if (SelectedProduct ==null)
            {
                MessageBox.Show("Please select the Product");
                return;
            }
            if (SelectedLot == null || SelectedLot == "")
            {
                MessageBox.Show("Please enter the lot Number");
                return;
            }

            AddInfo(SelectedProduct, SelectedLot);
        }

        void AddInfo(Product Product, String LotNumber)
        {
            InventoryProductInfo MyInfo = new InventoryProductInfo();
            MyInfo.Inventory=SelectedInventory;
            MyInfo.Product=SelectedProduct;
            MyInfo. LotId=LotNumber;
            MyInfo. OnProcessingQuantity=0;
            MyInfo.UnfinishedQuantity=0;
            MyInfo.FinishedQuantity=0;
            MyInfo.UnitLotCost = 0;
            Infos.Add(MyInfo);
            
            _context.InventoryProductInfos.Remove(MyInfo);
        }

        void DeleteItem()
        {
            if (SelectedInfo == null)
            {
                MessageBox.Show("Please select an item to delete!!");
                return;
            }

            Infos.Remove(SelectedInfo);
        }

        void LoadData()
        {
            Infos = new ObservableCollection<InventoryProductInfo>();
            _context.Load(_context.GetProductsQuery()).Completed += (s, e) =>
                {
                    flag++;
                    Products = new ObservableCollection<Product>();
                    for (int i = 0; i < _context.Products.Count; i++)
                    {
                        if (_context.Products.ElementAt(i).ProductName != "DUMMY")
                            Products.Add(_context.Products.ElementAt(i));
                    }
                    
                    if (flag == 3)
                        MyWindow.Close();
                };
            
            _context.Load(_context.GetInventoriesQuery()).Completed += (s, e) =>
            {
                flag++;
                if (flag == 3)
                    MyWindow.Close();
            };

            _context.Load(_context.GetInventoryProductInfoesQuery()).Completed += (s, e) =>
            {
                flag++;
                if (flag == 3)
                    MyWindow.Close();
            };
        }

        public EntitySet<Inventory> Inventories { get{return _context.Inventories;} }


        /// <summary>
        /// The <see cref="Products" /> property's name.
        /// </summary>
        public const string ProductsPropertyName = "Products";

        private ObservableCollection<Product> _products  ;

        /// <summary>
        /// Gets the Products property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public ObservableCollection<Product> Products
        {
            get
            {
                return _products;
            }

            set
            {
                if (_products == value)
                {
                    return;
                }

                var oldValue = _products;
                _products = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(ProductsPropertyName);

            }
        }
        
        public ICommand DeleteCommand { get; set; }
        public ICommand SubmitCommand { get; set; }
        public ICommand AddCommand { get; set; }

        /// <summary>
        /// The <see cref="SelectedProduct" /> property's name.
        /// </summary>
        public const string SelectedProductPropertyName = "SelectedProduct";

        private Product _selectedProduct  ;

        /// <summary>
        /// Gets the SelectedProduct property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public Product  SelectedProduct
        {
            get
            {
                return _selectedProduct;
            }

            set
            {
                if (_selectedProduct == value)
                {
                    return;
                }

                var oldValue = _selectedProduct;
                _selectedProduct = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(SelectedProductPropertyName);

            }
        }

        /// <summary>
        /// The <see cref="Infos" /> property's name.
        /// </summary>
        public const string InfosPropertyName = "Infos";

        private ObservableCollection<InventoryProductInfo> _infos ;

        /// <summary>
        /// Gets the Infos property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public ObservableCollection<InventoryProductInfo> Infos
        {
            get
            {
                return _infos;
            }

            set
            {
                if (_infos == value)
                {
                    return;
                }

                var oldValue = _infos;
                _infos = value;


                // Update bindings and broadcast change using GalaSoft.MvvmLight.Messenging
                RaisePropertyChanged(InfosPropertyName, oldValue, value, true);
            }
        }

        /// <summary>
        /// The <see cref="SelectedInfo" /> property's name.
        /// </summary>
        public const string SelectedInfoPropertyName = "SelectedInfo";

        private InventoryProductInfo _selectedInfo  ;

        /// <summary>
        /// Gets the SelectedInfo property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public InventoryProductInfo SelectedInfo
        {
            get
            {
                return _selectedInfo;
            }

            set
            {
                if (_selectedInfo == value)
                {
                    return;
                }

                var oldValue = _selectedInfo;
                _selectedInfo = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(SelectedInfoPropertyName);

            }
        }

        /// <summary>
        /// The <see cref="SelectedInventory" /> property's name.
        /// </summary>
        public const string SelectedInventoryPropertyName = "SelectedInventory";

        private Inventory _selectedInventory  ;

        /// <summary>
        /// Gets the SelectedInventory property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public Inventory SelectedInventory
        {
            get
            {
                return _selectedInventory;
            }

            set
            {
                if (_selectedInventory == value)
                {
                    return;
                }

                var oldValue = _selectedInventory;
                _selectedInventory = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(SelectedInventoryPropertyName);

            }
        }

        /// <summary>
        /// The <see cref="SelectedLot" /> property's name.
        /// </summary>
        public const string SelectedLotPropertyName = "SelectedLot";

        private String _selectedLot  ;

        /// <summary>
        /// Gets the SelectedLot property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public String SelectedLot
        {
            get
            {
                return _selectedLot;
            }

            set
            {
                if (_selectedLot == value)
                {
                    return;
                }

                var oldValue = _selectedLot;
                _selectedLot = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(SelectedLotPropertyName);

            }
        }
    }
}
