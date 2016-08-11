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
using TrialBusinessManager.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel.DataAnnotations;

namespace TrialBusinessManager.ViewModels.DO
{
    public class ProductTransferViewModel:ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        BusyWindow MyWindow = new BusyWindow();
        int ErrorFlag = 0;

        public ProductTransferViewModel()
        {
            Initialize();
            LoadData();
            RegisterCommands();
        }

        void Initialize()
        {
            InitializeTransfer();
            Transports = new ObservableCollection<string>();
            Transports = ConstantCollections.Transports;
            ProductTransferInfoes = new ObservableCollection<ProductTransferModel>();
        }

        void InitializeTransfer()
        {
            ProductTransfer = new ProductTransfer();
            ProductTransfer.DateOfIssue = DateTime.Now;
            ProductTransfer.DateOfRecieve = DateTime.Now;
            ProductTransfer.HasProductLoss = false;
            ProductTransfer.IssuedByDOId = UserInfo.EmployeeID;
            ProductTransfer.ProductLossCost = 0;
            ProductTransfer.ProductTransferStatus = "Issued By DO";
            ProductTransfer.SendFromInventoryId = UserInfo.Inventory.InventoryId;
        }

        void LoadData()
        {
            int flag = 0;
            MyWindow.Show();
           
            _context.Load(_context.GetInventoriesQuery()).Completed += (s, e) =>
                {
                    flag++;
                    if (flag == 2)
                    {
                        MyWindow.Close();
                    }
                };

            _context.Load(_context.GetInventoryProductInfoesByInventoryIdQuery(UserInfo.Inventory.InventoryId)).Completed += (s, e) =>
                {
                    flag++;
                    Products=new ObservableCollection<Product>();
                    
                    for (int i = 0; i < _context.InventoryProductInfos.Count; i++)
                    {
                        if (!Products.Any(c => c.ProductId.Equals(_context.InventoryProductInfos.ElementAt(i).ProductId)))
                        {
                            Products.Add(_context.InventoryProductInfos.ElementAt(i).Product);
                        }
                    }
                    
                    if (flag == 2)
                        MyWindow.Close();
                };
        }

        void RegisterCommands()
        {
            AddCommand = new RelayCommand(AddItem);
            DeleteCommand=new RelayCommand(DeleteItem);
            ResetCommand=new RelayCommand(Reset);
            SubmitCommand=new RelayCommand(Submit);
            ProductChangedCommand = new RelayCommand(ProductChanged);
            QuantityEditedCommand = new RelayCommand(QuantityEdited);
        }

        void ValidateInputs()
        {
           
            ErrorMessage="";
            
            if (ProductTransferInfoes.Count == 0)
            {
                ErrorMessage += "No Product Added For transfer\n";
                ErrorFlag = 1;
            }

            for (int i = 0; i < ProductTransferInfoes.Count; i++)      
            {
                if (ProductTransferInfoes.ElementAt(i).Quantity < 1)
                {
                    ErrorMessage += "You have products with quantity zero\n";
                    ErrorFlag = 1;
                    break;
                }
            }

            if (ProductTransfer.TransportType == "")
            {
                ErrorMessage += "Transport Type filed is required!\n";
                ErrorFlag = 1;
            }

           

            if(SelectedInventory==null)
            {
                ErrorMessage += "Inventory required!!\n";
                ErrorFlag = 1;
            }
            else
            {
                if (SelectedInventory.InventoryId.Equals(UserInfo.Inventory.InventoryId))
                {
                    ErrorMessage += "Wrong region input!!\n";
                    ErrorFlag = 1;
                }
            }

        }

        void Update()
        {
            MyWindow.Show();
            
            for (int i = 0; i < ProductTransferInfoes.Count; i++)
            {
                ProductTransferInfoes.ElementAt(i).Info.FinishedQuantity -= ProductTransferInfoes.ElementAt(i).Quantity * ProductTransferInfoes.ElementAt(i).Info.Product.StockKeepingUnit;
                
                ProductTransferDetail detail = new ProductTransferDetail();
                detail.LotId = ProductTransferInfoes.ElementAt(i).Info.LotId;
                detail.ProductId = ProductTransferInfoes.ElementAt(i).Info.ProductId;
                detail.PurchasePricePerUnit = (double)ProductTransferInfoes.ElementAt(i).Info.UnitLotCost;
                detail.RecievedQuantity = 0;
                detail.TransferQuantity = ProductTransferInfoes.ElementAt(i).Quantity;
                ProductTransfer.ProductTransferDetails.Add(detail);
            }

            ProductTransfer.RecievedToInventoryId = SelectedInventory.InventoryId;
            _context.ProductTransfers.Add(ProductTransfer);

            if ((ProductTransfer.ValidationErrors != null) && (ProductTransfer.ValidationErrors.Count > 0))
            {
                foreach (ValidationResult result in ProductTransfer.ValidationErrors)
                {
                    string error = string.Format("Property [{0}] has problem [{1}]",
                      result.MemberNames.First(), // ?
                      result.ErrorMessage);

                    MessageBox.Show(error, "Error", MessageBoxButton.OK);
                }
            }

            _context.SubmitChanges(OnSubmitCompleted,null);
        }



        void OnSubmitCompleted(SubmitOperation so)
        {
            MyWindow.Close();
            Reset();

            if (so.HasError)
            {
                MessageBox.Show("Failed");
                
            }
            else
            {
                MessageBox.Show("Suceeded");
            }
        }

        void QuantityEdited()
        {
            
            if (SelectedDataGridInfo.AvailableQuantity < SelectedDataGridInfo.Quantity)
            {
                MessageBox.Show("Transfer quantity cannot be greater than available quantity");
                SelectedDataGridInfo.Quantity = 0;
            }

            if (SelectedDataGridInfo.Quantity < 0)
            {
                SelectedDataGridInfo.Quantity = 0;
            }

            SelectedDataGridInfo.TotalQuantity = SelectedDataGridInfo.AvailableQuantity * SelectedDataGridInfo.Info.Product.StockKeepingUnit;
        }

        void AddItem()
        {
            if (SelectedProduct == null)
            {
                MessageBox.Show("Please select a product");
                return;
            }

            if (SelectedLot == null)
            {
                MessageBox.Show("Please select a Lot");
                return;
            }

            if (ProductTransferInfoes.Any(c => c.Info.ProductId.Equals(SelectedProduct.ProductId) && c.Info.LotId.Equals(SelectedLot)))
            {
                MessageBox.Show("This product With this Lot is already added!");
                return;
            }

            ProductTransferModel MyModel = new ProductTransferModel();
            MyModel.Info = new InventoryProductInfo();
            MyModel.Info = _context.InventoryProductInfos.Where(c => c.LotId.Equals(SelectedLot) && c.ProductId.Equals(SelectedProduct.ProductId)).Single();
            MyModel.Quantity = 0;
            MyModel.TotalQuantity = 0;
            _productTransferInfoes.Add(MyModel);
        }

        void DeleteItem()
        {
            if (SelectedDataGridInfo == null)
            {
                MessageBox.Show("Please select an item to delete");
                return;
            }

            ProductTransferInfoes.Remove(SelectedDataGridInfo);
        }

        void Reset()
        {
            ProductTransfer = new ProductTransfer();
            ProductTransferInfoes = new ObservableCollection<ProductTransferModel>();
            ErrorFlag = 0;
        }

        void Submit()
        {
            ValidateInputs();

            if (ErrorFlag == 0)
            {
                Update();
            }
        }

        void ProductChanged()
        {
            Lots = new ObservableCollection<string>();
            
            for (int i = 0; i < _context.InventoryProductInfos.Count; i++)
            {
                if (_context.InventoryProductInfos.ElementAt(i).ProductId.Equals(SelectedProduct.ProductId) && !_context.InventoryProductInfos.ElementAt(i).Product.ProductName.Equals("DUMMY"))
                {
                    Lots.Add(_context.InventoryProductInfos.ElementAt(i).LotId);
                }
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand ResetCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SubmitCommand { get; set; }
        public ICommand ProductChangedCommand { get; set; }
        public ICommand QuantityEditedCommand { get; set; }

        public EntitySet<Inventory> Inventories { get { return _context.Inventories; } }

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
        /// The <see cref="ErrorMessage" /> property's name.
        /// </summary>
        public const string ErrorMessagePropertyName = "ErrorMessage";

        private String _errorMessage  ;

        /// <summary>
        /// Gets the ErrorMessage property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public String ErrorMessage
        {
            get
            {
                return _errorMessage;
            }

            set
            {
                if (_errorMessage == value)
                {
                    return;
                }

                var oldValue = _errorMessage;
                _errorMessage = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(ErrorMessagePropertyName);

            }
        }

        /// <summary>
        /// The <see cref="ProductTransfer" /> property's name.
        /// </summary>
        public const string ProductTransferPropertyName = "ProductTransfer";

        private ProductTransfer _productTransfer  ;

        /// <summary>
        /// Gets the ProductTransfer property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public ProductTransfer ProductTransfer
        {
            get
            {
                return _productTransfer;
            }

            set
            {
                if (_productTransfer == value)
                {
                    return;
                }

                var oldValue = _productTransfer;
                _productTransfer = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(ProductTransferPropertyName);

            }
        }

        /// <summary>
        /// The <see cref="SelectedTransport" /> property's name.
        /// </summary>
        public const string SelectedTransportPropertyName = "SelectedTransport";

        private String _selectedTransport  ;

        /// <summary>
        /// Gets the SelectedTransport property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public String SelectedTransport
        {
            get
            {
                return _selectedTransport;
            }

            set
            {
                if (_selectedTransport == value)
                {
                    return;
                }

                var oldValue = _selectedTransport;
                _selectedTransport = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(SelectedTransportPropertyName);

            }
        }

        /// <summary>
        /// The <see cref="Transports" /> property's name.
        /// </summary>
        public const string TransportsPropertyName = "Transports";

        private ObservableCollection<String> _Transports  ;


        /// <summary>
        /// Gets the Transports property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public ObservableCollection<String> Transports
        {
            get
            {
                return _Transports;
            }

            set
            {
                if (_Transports == value)
                {
                    return;
                }

                var oldValue = _Transports;
                _Transports = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(TransportsPropertyName);

            }
        }

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

        /// <summary>
        /// The <see cref="Lots" /> property's name.
        /// </summary>
        public const string LotsPropertyName = "Lots";

        private ObservableCollection<String> _lots  ;

        /// <summary>
        /// Gets the Lots property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public ObservableCollection<String> Lots
        {
            get
            {
                return _lots;
            }

            set
            {
                if (_lots == value)
                {
                    return;
                }

                var oldValue = _lots;
                _lots = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(LotsPropertyName);

            }
        }

        /// <summary>
        /// The <see cref="SelectedDataGridInfo" /> property's name.
        /// </summary>
        public const string SelectedDataGridInfoPropertyName = "SelectedDataGridInfo";

        private ProductTransferModel _selectedDataGridInfo  ;

        /// <summary>
        /// Gets the SelectedDataGridInfo property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public ProductTransferModel SelectedDataGridInfo
        {
            get
            {
                return _selectedDataGridInfo;
            }

            set
            {
                if (_selectedDataGridInfo == value)
                {
                    return;
                }

                var oldValue = _selectedDataGridInfo;
                _selectedDataGridInfo = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(SelectedDataGridInfoPropertyName);

            }
        }

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
        public Product SelectedProduct
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
        /// The <see cref="ProductTransferInfoes" /> property's name.
        /// </summary>
        public const string ProductTransferInfoesPropertyName = "ProductTransferInfoes";

        private ObservableCollection<ProductTransferModel> _productTransferInfoes  ;

        /// <summary>
        /// Gets the ProductTransferInfoes property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public ObservableCollection<ProductTransferModel> ProductTransferInfoes
        {
            get
            {
                return _productTransferInfoes;
            }

            set
            {
                if (_productTransferInfoes == value)
                {
                    return;
                }

                var oldValue = _productTransferInfoes;
                _productTransferInfoes = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(ProductTransferInfoesPropertyName);

            }
        }
    }

    public class ProductTransferModel:ViewModelBase
    {
        /// <summary>
        /// The <see cref="Info" /> property's name.
        /// </summary>
        public const string InfoPropertyName = "Info";

        private InventoryProductInfo _info  ;

        /// <summary>
        /// Gets the Info property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public InventoryProductInfo Info
        {
            get
            {
                return _info;
            }

            set
            {
                if (_info == value)
                {
                    return;
                }

                var oldValue = _info;
                _info = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(InfoPropertyName);

            }
        }

        /// <summary>
        /// The <see cref="AvailableQuantity" /> property's name.
        /// </summary>
        public const string AvailableQuantityPropertyName = "AvailableQuantity";

        private Double _availableQuantity  ;

        /// <summary>
        /// Gets the AvailableQuantity property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public Double AvailableQuantity
        {
            get
            {
                return (Info.FinishedQuantity/Info.Product.StockKeepingUnit);
            }

            set
            {
                if (_availableQuantity == value)
                {
                    return;
                }

                var oldValue = _availableQuantity;
                _availableQuantity = Info.FinishedQuantity / Info.Product.StockKeepingUnit;

                // Update bindings, no broadcast
                RaisePropertyChanged(AvailableQuantityPropertyName);

            }
        }

        /// <summary>
        /// The <see cref="Quantity" /> property's name.
        /// </summary>
        public const string QuantityPropertyName = "Quantity";

        private Double _quantity  ;

        /// <summary>
        /// Gets the Quantity property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public Double Quantity
        {
            get
            {
                return _quantity;
            }

            set
            {
                if (_quantity == value)
                {
                    return;
                }

                var oldValue = _quantity;
                _quantity = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(QuantityPropertyName);

            }
        }

        /// <summary>
        /// The <see cref="TotalQuantity" /> property's name.
        /// </summary>
        public const string TotalQuantityPropertyName = "TotalQuantity";

        private Double _totalQuantity  ;

        /// <summary>
        /// Gets the TotalQuantity property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public Double TotalQuantity
        {
            get
            {
                return _totalQuantity;
            }

            set
            {
                if (_totalQuantity == value)
                {
                    return;
                }

                var oldValue = _totalQuantity;
                _totalQuantity = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(TotalQuantityPropertyName);

            }
        }
    }
}
