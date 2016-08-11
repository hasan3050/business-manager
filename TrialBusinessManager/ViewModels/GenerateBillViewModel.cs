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
using TrialBusinessManager.Web;
using GalaSoft.MvvmLight;
using TrialBusinessManager.Views;
using TrialBusinessManager.Models;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ServiceModel.DomainServices.Client;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Command;
using System.ServiceModel;

namespace TrialBusinessManager.ViewModels
{
    class QuantityPriceInfo
    {
        public double Quantity { get; set; }
        public double Price { get; set; }
      
        public QuantityPriceInfo( double quantity, double price )
        {
            Quantity = quantity;
            Price = price;
        }
    }

    public class GenerateBillViewModel:ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        bool RejectFlag = false;
        int flag = 0;
        Indent MyIndent = new Indent();
        List<Product> ProductList = new List<Product>();
        LotAllocation MyWindow;
        Product AddedProduct = new Product();
        Dictionary<Product, QuantityPriceInfo> ProductDictionary = new Dictionary<Product, QuantityPriceInfo>();
        ObservableCollection<LotAllocationModel> InventoryInfoes = new ObservableCollection<LotAllocationModel>();
        BusyWindow Busy = new BusyWindow();
        public GenerateBillViewModel()
        {
            Initialize();
        }

        void RegisterCommands()
        {
            LotAllocationCommand = new RelayCommand(AllocateLot);
            GenerateBillCommand = new RelayCommand(GenerateBill);
            PrintBillCommand = new RelayCommand(print);
            RejectDispatchCommand = new RelayCommand(RejectDispatch);
        }

        void RejectDispatch()
        {
            Busy.Show();
            RejectFlag = true;
          
            _context.Load(_context.GetIndentsQuery().Where(c => c.IndentId.Equals(MyIndent.IndentId))).Completed += (s, e) =>
                {
                    MyIndent = _context.Indents.Where(c => c.IndentId.Equals(MyIndent.IndentId)).Single();
                    MyIndent.IndentStatus = "Rejected";
                    _context.SubmitChanges(OnBillSubmitCompleted,null);
                };
        }

        void print()
        {
            PrintFactory.PrintBill(_context, Bill);
        }
        
        void GenerateBill()
        {
            ErrorMessage = "";
            ValidateInput();
            if (ErrorMessage != "") return;
            ConstructBillInitials();
        }


        void AllocateLot()
        {
            if (SelectedProduct== null)
            {
                MessageBox.Show("Please select a product!!");
                return;
            }
            MyWindow = new LotAllocation(SelectedProduct);
            MyWindow.Show();
            MyWindow.Closed += (s, e) =>
                {
                    if (MyWindow.success)
                        AddItems(StaticMessaging.LotAllocations);
                };
        }

        Double CalculateCommission(long ProductId)
        {
           
            return _context.IndentProductInfos.Where(e => e.ProductId == ProductId).SingleOrDefault().CommissionPercentage;
            
        }


        void AddItems(ObservableCollection<LotAllocationModel> Collection)
        {
             InventoryInfoes = new ObservableCollection<LotAllocationModel>();


            foreach (var item in Collection)
            {
                InventoryInfoes.Add(item);
            }

            for ( ; ; )
            {
                BillViewDataGridModel DeletedRow = new BillViewDataGridModel();
                
                try
                {
                    DeletedRow = DataGridInfos.Where(c => c.Product.ProductName.Equals(AddedProduct.ProductName)).First();
                    DataGridInfos.Remove(DeletedRow);
                }
                catch (Exception ex)
                {
                    break;
                }
            }

            for (int i = 0; i < Collection.Count; i++)
            {
                Double CommissionPercentage;
                BillViewDataGridModel BillData = new BillViewDataGridModel();
                BillData.Info = new InventoryProductInfo();
                BillData.Info = Collection[i].Info;
                BillData.LotNumber = Collection[i].Info.LotId;
                BillData.Product = new Product();
                BillData.Product = Collection[i].Info.Product;
                CommissionPercentage = CalculateCommission(BillData.Product.ProductId);
                BillData.Packets = Collection[i].AllotedPackets;
                BillData.TotalQuantity = BillData.Packets * BillData.Product.StockKeepingUnit;
                BillData.ActualUnitprice = MyIndent.IndentProductInfoes.Where(c => c.ProductId.Equals(BillData.Product.ProductId)).SingleOrDefault().ProductPrice;
                BillData.TotalPrice = (BillData.Packets * BillData.ActualUnitprice);
                if (BillData.TotalPrice > 0) DataGridInfos.Add(BillData);
            }
        }

        void ProductReceived(Product MyProduct)
        {
            AddedProduct = MyProduct;
        }

        void LoadData()
        {            
            _context.Load(_context.GetDealersQuery().Where(c => c.DealerId.Equals(MyIndent.Dealer.DealerId))).Completed += (s, arg) =>
            {
                MyDealer = new Dealer();
                MyDealer = _context.Dealers.Where(sp => sp.DealerId.Equals(MyIndent.Dealer.DealerId)).SingleOrDefault();
                flag++;
                if (flag == 3)
                {
                    ButtonEnabledBool = true;
                }
            };



            _context.Load(_context.GetIndentProductInfoesQuery().Where(c => c.IndentId.Equals(MyIndent.IndentId))).Completed += (s, e) =>
                {
                    ComboBoxInfos = new ObservableCollection<ProductMessage>();
                    for (int i = 0; i < _context.IndentProductInfos.Count; i++)
                    {
                        ProductMessage info = new ProductMessage();
                        info.Product = new Product();

                        try
                        {
                            info.Product = _context.IndentProductInfos.ElementAt(i).Product;
                        }
                        catch (Exception ex)
                        {
                        }

                        info.Quanity = ((double)_context.IndentProductInfos.ElementAt(i).ProductQuantityBySIC);
                        if(info.Quanity>0)
                        ComboBoxInfos.Add(info);
                    }
                    
                    flag++;
                    if (flag == 3)
                    {
                        ButtonEnabledBool = true;
                    }
                };

            _context.Load(_context.GetInventoryProductInfoesQuery().Where(c => c.InventoryId.Equals(UserInfo.Inventory.InventoryId))).Completed += (sss, args) =>
            {
                flag++;
                if (flag == 3)
                {
                    ButtonEnabledBool = true;
                }
            };

            
        }

        void RegisterForMessages()
        {
            Messenger.Default.Register<Product>(this, ProductReceived);
        }

        void InitializeData()
        {
            ButtonEnabledBool = false;
            StaticMessaging.BillGenerated = false;
            Bill = new Bill();
            MyIndent = Messages.indent;
            TransportTypes = ConstantCollections.Transports;
            DataGridInfos = new ObservableCollection<BillViewDataGridModel>();
            IsLoading = true;
        }

        void Initialize()
        {
            InitializeData();   
            RegisterForMessages();
            RegisterCommands();
            LoadData();
        }

        void ConstructBillInitials() 
        {
            int Duration=0;
            int LatestDuration;
            Bill.BillStatus = "Dispatched";
            Bill.DateOfIssue = DateTime.Now;
            Bill.DispatchedById = UserInfo.EmployeeID;
            Bill.HasProductLoss = false;
            Bill.IndentId = MyIndent.IndentId;
            Bill.ProductLossCost = 0;
            Bill.TotalPaid = 0;
            Bill.Dealer = _myDealer;
            Bill.HasSalesReturn = false;
            Bill.SalesReturnCost = 0;
            
            for (int i = 0; i < _context.IndentProductInfos.Count; i++)
            {
                try
                {
                    LatestDuration = _context.IndentProductInfos.ElementAt(i).Product.Commissions.Where(c => c.CommissionName.Equals(MyIndent.PaymentMethod)).SingleOrDefault().Duration;
               
                }
                catch (Exception ex)
                {
                    LatestDuration=30;
                
                }
                
                if(LatestDuration>Duration) Duration=LatestDuration;
               // MessageBox.Show(_context.IndentProductInfos.ElementAt(i).Product.Commissions.Where(c => c.CommissionName.Equals(MyIndent.PaymentMethod)).SingleOrDefault().Duration);
            }

           
            Bill.PaymentDeadline = (DateTime)(DateTime.Now.AddDays(Duration));
            
           
            UpdateBillProductInfoes();
            Busy.Show();
            _context.Bills.Add(Bill);
            _context.SubmitChanges(OnBillSubmitCompleted,null);
        }

        void ValidateInput()
        {
            ErrorMessage = "";
            if (Bill.TransportType == null) ErrorMessage += "Please enter transport type\n";
            if (Bill.EmergencyContactNo == null) ErrorMessage += "Please enter Emergency Contact No.\n";
            if (Bill.VehicleNo == null) ErrorMessage += "Please enter Vehicle no.\n";

            for (int i = 0; i < ComboBoxInfos.Count; i++)
            {
                String CheckProduct;
                CheckProduct = ComboBoxInfos[i].Product.ProductName;
                int j;

                for (j = 0; j < DataGridInfos.Count; j++)
                {
                    if (DataGridInfos[j].Product.ProductName.Equals(ComboBoxInfos[i].Product.ProductName))
                    {
                        break;
                    }
                }

                if (j >= DataGridInfos.Count)
                {
                    ErrorMessage += "You havent added all the requested products\n";
                }
            }
        }

        private void OnBillSubmitCompleted(SubmitOperation so)
        {
            Busy.Close();
            if (so.HasError == true)
            {
                return;
            }
            else
            {
                ButtonEnabledBool = false;
                StaticMessaging.BillGenerated = true;
                if (RejectFlag) ErrorMessage = "The Bill has been succesfully rejected, Please close this window to continue";
                else
                {
                    PrintEnabledBool = true;
                    ErrorMessage = "The Bill has been succesfully dispatched, Please close this window to continue";
                }
            }
        }

        

        void UpdateBillProductInfoes() 
        {
            TotalPrice = 0;
            for (int i = 0; i < DataGridInfos.Count; i++)
            {
                BillProductInfo AddInfo = new BillProductInfo();           
                AddInfo.LotId = DataGridInfos[i].LotNumber;
                AddInfo.LotQuantity = DataGridInfos[i].Packets;
                AddInfo.ProductId = DataGridInfos[i].Product.ProductId;
                AddInfo.ComissionPercentage = CalculateCommission(AddInfo.ProductId);
                AddInfo.ProductPrice=DataGridInfos[i].ActualUnitprice;
                TotalPrice += AddInfo.LotQuantity * AddInfo.ProductPrice;
                Bill.BillProductInfoes.Add(AddInfo);

                //***************for updating inventory******************

                //for constructing due info
                if (ProductDictionary.ContainsKey(DataGridInfos[i].Product))
                {
                    ProductDictionary[DataGridInfos[i].Product].Price += DataGridInfos[i].TotalPrice;
                    ProductDictionary[DataGridInfos[i].Product].Quantity += DataGridInfos[i].Packets;
                }
                else
                {
                    ProductDictionary.Add(DataGridInfos[i].Product, new QuantityPriceInfo ( DataGridInfos[i].Packets , DataGridInfos[i].TotalPrice ));
                }
            }

            Bill.TotalProductCost = TotalPrice;
        }

        void UpdateIndentStatus()
        {
            Indent newIndent = _context.Indents.Where( s => s.IndentId.Equals( MyIndent.IndentId )).SingleOrDefault();
            newIndent.IndentStatus = "Dispatched";
        }

        public const string PrintEnabledBoolPropertyName = "PrintEnabledBool";
        private bool _printEnabledBool;
        public bool PrintEnabledBool
        {
            get { return _printEnabledBool; }
            set
            {
                if (_printEnabledBool == value) { return; }
                _printEnabledBool = value;
                RaisePropertyChanged(PrintEnabledBoolPropertyName);
            }
        }

        public const string ButtonEnabledBoolPropertyName = "ButtonEnabledBool";
        private bool _buttonEnabledBool;
        public bool ButtonEnabledBool
        {
            get { return _buttonEnabledBool; }
            set
            {
                if (_buttonEnabledBool == value) { return; }
                _buttonEnabledBool = value;
                RaisePropertyChanged(ButtonEnabledBoolPropertyName);
            }
        }
        
        /// <summary>
        /// The <see cref="MyDealer" /> property's name.
        /// </summary>
        public const string MyDealerPropertyName = "MyDealer";

        private Dealer _myDealer  ;

        /// <summary>
        /// Sets and gets the MyDealer property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Dealer MyDealer
        {
            get
            {
                return _myDealer;
            }

            set
            {
                if (_myDealer == value)
                {
                    return;
                }

                _myDealer = value;
                RaisePropertyChanged(MyDealerPropertyName);
            }
        }

        public ICommand LotAllocationCommand{ get; set; }
        public ICommand GenerateBillCommand { get; set; }
        public ICommand PrintBillCommand { get; set; }
        public ICommand RejectDispatchCommand { get; set; }

        /// <summary>
        /// The <see cref="TransportTypes" /> property's name.
        /// </summary>
        public const string TransportTypesPropertyName = "TransportTypes";

        private ObservableCollection<String> _transportTypes  ;

        /// <summary>
        /// Sets and gets the TransportTypes property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<String> TransportTypes
        {
            get
            {
                return _transportTypes;
            }

            set
            {
                if (_transportTypes == value)
                {
                    return;
                }

                _transportTypes = value;
                RaisePropertyChanged(TransportTypesPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SelectedProduct" /> property's name.
        /// </summary>
        public const string SelectedProductPropertyName = "SelectedProduct";

        private ProductMessage _selectedProduct  ;

        /// <summary>
        /// Sets and gets the SelectedProduct property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ProductMessage SelectedProduct
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

                _selectedProduct = value;
                RaisePropertyChanged(SelectedProductPropertyName);
   
            }
        }

        /// <summary>
        /// The <see cref="TotalPrice" /> property's name.
        /// </summary>
        public const string TotalPricePropertyName = "TotalPrice";

        private Double _totalPrice  ;

        /// <summary>
        /// Sets and gets the TotalPrice property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Double TotalPrice
        {
            get
            {
                return _totalPrice;
            }

            set
            {
                if (_totalPrice == value)
                {
                    return;
                }

                _totalPrice = value;
                RaisePropertyChanged(TotalPricePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="DataGridInfos" /> property's name.
        /// </summary>
        public const string DataGridInfosPropertyName = "DataGridInfos";

        private ObservableCollection<BillViewDataGridModel> _dataGridInfos  ;

        /// <summary>
        /// Sets and gets the DataGridInfos property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<BillViewDataGridModel> DataGridInfos
        {
            get
            {
                return _dataGridInfos;
            }

            set
            {
                if (_dataGridInfos == value)
                {
                    return;
                }

                _dataGridInfos = value;
                RaisePropertyChanged(DataGridInfosPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="ComboBoxInfos" /> property's name.
        /// </summary>
        public const string ComboBoxInfosPropertyName = "ComboBoxInfos";

        private ObservableCollection<ProductMessage> _comboBoxInfos  ;

        /// <summary>
        /// Sets and gets the ComboBoxInfos property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<ProductMessage> ComboBoxInfos
        {
            get
            {
                return _comboBoxInfos;
            }

            set
            {
                if (_comboBoxInfos == value)
                {
                    return;
                }

                _comboBoxInfos = value;
                RaisePropertyChanged(ComboBoxInfosPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Bill" /> property's name.
        /// </summary>
        public const string BillPropertyName = "Bill";

        private Bill _bill ;

        /// <summary>
        /// Sets and gets the Bill property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Bill Bill
        {
            get
            {
                return _bill;
            }

            set
            {
                if (_bill == value)
                {
                    return;
                }

                _bill = value;
                RaisePropertyChanged(BillPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="IsLoading" /> property's name.
        /// </summary>
        public const string IsLoadingPropertyName = "IsLoading";

        private bool _isLoading = false;

        /// <summary>
        /// Sets and gets the IsLoading property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }

            set
            {
                if (_isLoading == value)
                {
                    return;
                }

                _isLoading = value;
                RaisePropertyChanged(IsLoadingPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="ErrorMessage" /> property's name.
        /// </summary>
        public const string ErrorMessagePropertyName = "ErrorMessage";

        private String _errorMessage;

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
    }
}
