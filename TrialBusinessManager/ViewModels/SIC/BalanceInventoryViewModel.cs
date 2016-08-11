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
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
namespace TrialBusinessManager.ViewModels.SIC
{
    public class BalanceInventoryViewModel:ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        BusyWindow MyWindow = new BusyWindow();
        ObservableCollection<Double> Storage = new ObservableCollection<double>();
        Double Store;

        public BalanceInventoryViewModel()
        {
            Initialize();
        }

        void Initialize()
        {
            ModelCollection = new ObservableCollection<InventoryBalanceModel>();
            RegisterCommands();
            LoadData();
        }

        void RegisterCommands()
        {
            TransferCommand = new RelayCommand(Transfer);
            ProductChangedCommand = new RelayCommand(ProductChanged);
            SubmitCommand = new RelayCommand(Submit);
            SelectionChangedCommand = new RelayCommand(SelectionChanged);
            QuantityEditedCommand = new RelayCommand(QuantityEdited);
        }

        void Transfer()
        {
            if (FromLot == null || ToLot == null||SelectedProduct==null)
            {
                return;
            }
            
            if (FromLot.Equals(ToLot))
            {
                MessageBox.Show("You cannot transfer between same lots!!");
                return;
            }

            if (FromLot.UnfinishedQuantity < Quantity)
            {
                MessageBox.Show("You cannot transfer more than the existing quantity!!");
                return;
            }

            if (Quantity == 0)
            {
                return;
            }

            FromLot.UnfinishedQuantity -= Quantity;
            CreateLog(FromLot, Quantity,true);
            CreateLog(ToLot, Quantity, false);
            ToLot.UnfinishedQuantity += Quantity;
        }

        void SelectionChanged()
        {
            //Store = SelectedDatagridInfo.UnfinishedQuantity;
        }

        void PopulateModelCollection()
        {

            for (int i = 0; i < _context.InventoryProductInfos.Count; i++)
            {
                InventoryBalanceModel MyModel = new InventoryBalanceModel();
                MyModel.BalancedQuantity = 0;
                MyModel.Info = new InventoryProductInfo();
                MyModel.Info = _context.InventoryProductInfos.ElementAt(i);
                ModelCollection.Add(MyModel);
            }
        }
        
        void ProductChanged()
        {
            String GroupName = SelectedProduct.GroupName;
            LotInfoes = new ObservableCollection<InventoryProductInfo>();

            for (int i = 0; i < _context.InventoryProductInfos.Count; i++)
            {
                if (_context.InventoryProductInfos.ElementAt(i).Product.GroupName.Equals(GroupName))
                    LotInfoes.Add(_context.InventoryProductInfos.ElementAt(i));
            }    
        }

        void QuantityEdited()
        {
            if (SelectedDatagridInfo.BalancedQuantity < 0)
            {
                SelectedDatagridInfo.BalancedQuantity = 0;
            }
        }

        void CreateLog(InventoryProductInfo Info, Double Quantity, bool flag)
        {
            InventoryLog Log = new InventoryLog();
            if (flag) Log.Method = "Lot Transfer From";
            else Log.Method = "Lot Transfer To";
            Log.ProductId = Info.ProductId;
            Log.Date = DateTime.Now;
            Log.InventoryId = UserInfo.Inventory.InventoryId;
            Log.MemoNo = 0;
            Log.ProductQuantity = Quantity;
            Log.LotId = Info.LotId;
            Log.ProductCost = Quantity * (double)(Info.UnitLotCost);
            _context.InventoryLogs.Add(Log);
        }

        void AddBalance()
        {
            for (int i = 0; i < ModelCollection.Count; i++)
            {
                ModelCollection.ElementAt(i).Info.UnfinishedQuantity += ModelCollection.ElementAt(i).BalancedQuantity;
                if (ModelCollection.ElementAt(i).BalancedQuantity > 0)
                {
                    InventoryLog Log = new InventoryLog();
                    Log.ProductId = ModelCollection.ElementAt(i).Info.ProductId;
                    Log.Date = DateTime.Now;
                    Log.InventoryId = UserInfo.Inventory.InventoryId;
                    Log.LotId = ModelCollection.ElementAt(i).Info.LotId;
                    Log.MemoNo = 0;
                    Log.Method = "Inventory Balance";
                    Log.ProductQuantity = ModelCollection.ElementAt(i).BalancedQuantity;
                    _context.InventoryLogs.Add(Log);
                }
            }
        }

        void Submit()
        {
            AddBalance();
            MyWindow.Show();
            _context.SubmitChanges(OnSubmitCompleted, null);
        }

        void OnSubmitCompleted(SubmitOperation so)
        {
            MyWindow.Close();
            if (so.HasError)
            {
               // MessageBox.Show("Submit failed!! please try again!");
                return;
            }
            else
            {
                MessageBox.Show("Inventory Successfully updated.");
            }

            for (int i = 0; i < ModelCollection.Count; i++)
                ModelCollection.ElementAt(i).BalancedQuantity = 0;
        }

        void LoadData()
        {
            MyWindow.Show();
            _context.Load(_context.GetInventoryProductInfoesQuery().Where(c => c.InventoryId.Equals(UserInfo.Inventory.InventoryId) && c.Product.ProductName.Equals("DUMMY"))).Completed += (s, e) =>
                {
                    MyWindow.Close();
                    PopulateProducts();
                    PopulateModelCollection();
                };
        }

        void PopulateProducts()
        {
            Products = new ObservableCollection<Product>();
            
            for (int i = 0; i < _context.InventoryProductInfos.Count; i++)
            {
                Storage.Add(_context.InventoryProductInfos.ElementAt(i).UnfinishedQuantity);
                Product NewProduct = new Product();
                NewProduct = _context.InventoryProductInfos.ElementAt(i).Product;
                if(!Products.Contains(NewProduct)) Products.Add(NewProduct);
            }
        }

        public ICommand TransferCommand { get; set; }
        public ICommand SubmitCommand { get; set; }
        public ICommand ProductChangedCommand { get; set; }
        public ICommand SelectionChangedCommand { get; set; }
        public ICommand QuantityEditedCommand { get; set; }
        
        public EntitySet<InventoryProductInfo> Infoes { get { return _context.InventoryProductInfos; } }

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
        /// The <see cref="FromLot" /> property's name.
        /// </summary>
        public const string FromLotPropertyName = "FromLot";

        private InventoryProductInfo _fromLot  ;

        /// <summary>
        /// Gets the FromLot property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public InventoryProductInfo FromLot
        {
            get
            {
                return _fromLot;
            }

            set
            {
                if (_fromLot == value)
                {
                    return;
                }

                var oldValue = _fromLot;
                _fromLot = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(FromLotPropertyName);

            }
        }

        /// <summary>
        /// The <see cref="Index" /> property's name.
        /// </summary>
        public const string IndexPropertyName = "Index";

        private int _index  ;

        /// <summary>
        /// Gets the Index property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public int Index
        {
            get
            {
                return _index;
            }

            set
            {
                if (_index == value)
                {
                    return;
                }

                var oldValue = _index;
                _index = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(IndexPropertyName);

            }
        }

        /// <summary>
        /// The <see cref="ToLot" /> property's name.
        /// </summary>
        public const string ToLotPropertyName = "ToLot";

        private InventoryProductInfo _toLot  ;

        /// <summary>
        /// Gets the ToLot property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public InventoryProductInfo ToLot
        {
            get
            {
                return _toLot;
            }

            set
            {
                if (_toLot == value)
                {
                    return;
                }

                var oldValue = _toLot;
                _toLot = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(ToLotPropertyName);

            }
        }

        /// <summary>
        /// The <see cref="SelectedDatagridInfo" /> property's name.
        /// </summary>
        public const string SelectedDatagridInfoPropertyName = "SelectedDatagridInfo";

        private InventoryBalanceModel _selectedDataGridInfo  ;

        /// <summary>
        /// Gets the SelectedDatagridInfo property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public InventoryBalanceModel SelectedDatagridInfo
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
                RaisePropertyChanged(SelectedDatagridInfoPropertyName);

            }
        }

        /// <summary>
        /// The <see cref="LotInfoes" /> property's name.
        /// </summary>
        public const string LotInfoesPropertyName = "LotInfoes";

        private ObservableCollection<InventoryProductInfo> _Lots  ;

        /// <summary>
        /// Gets the LotInfoes property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public ObservableCollection<InventoryProductInfo> LotInfoes
        {
            get
            {
                return _Lots;
            }

            set
            {
                if (_Lots == value)
                {
                    return;
                }

                var oldValue = _Lots;
                _Lots = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(LotInfoesPropertyName);
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
        /// The <see cref="ModelCollection" /> property's name.
        /// </summary>
        public const string ModelCollectionPropertyName = "ModelCollection";

        private ObservableCollection<InventoryBalanceModel> _modelCollection  ;

        /// <summary>
        /// Gets the ModelCollection property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public ObservableCollection<InventoryBalanceModel> ModelCollection
        {
            get
            {
                return _modelCollection;
            }

            set
            {
                if (_modelCollection == value)
                {
                    return;
                }

                var oldValue = _modelCollection;
                _modelCollection = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(ModelCollectionPropertyName);
    
            }
        }

        /// <summary>
        /// The <see cref="Products" /> property's name.
        /// </summary>
        public const string ProductsPropertyName = "Products";

        private ObservableCollection<Product> _products  ;

        /// <summary>
        /// Gets the ProductS property.
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
    }

    public class InventoryBalanceModel:ViewModelBase
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
        /// The <see cref="BalancedQuantity" /> property's name.
        /// </summary>
        public const string BalancedQuantityPropertyName = "BalancedQuantity";

        private Double _balancedQuantity  ;

        /// <summary>
        /// Gets the BalancedQuantity property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public Double BalancedQuantity
        {
            get
            {
                return _balancedQuantity;
            }

            set
            {
                if (_balancedQuantity == value)
                {
                    return;
                }

                var oldValue = _balancedQuantity;
                _balancedQuantity = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(BalancedQuantityPropertyName);

            }
        }
    }
}
