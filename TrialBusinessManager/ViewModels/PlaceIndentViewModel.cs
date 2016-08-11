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
using TrialBusinessManager.Models;
using System.Collections.ObjectModel;
using System.ServiceModel.DomainServices.Client;
using System.Linq;
using GalaSoft.MvvmLight.Command;
using TrialBusinessManager.Views;
using GalaSoft.MvvmLight.Messaging;

namespace TrialBusinessManager.ViewModels
{
    public class PlaceIndentViewModel : ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        public event EventHandler SelectedProductChanged;
        BusyWindow MyWindow = new BusyWindow();
        //Int64 DealerID;

        Indent IndentToBePlaced;
        Int64 RMId;

        public PlaceIndentViewModel()
        {
            initialize();
            RegisterCommands();
        }

        //registering commands
        void RegisterCommands()
        {
            DeleteCommand = new RelayCommand(DeleteIndentInfo);
            ResetCommand = new RelayCommand(Reset);
            SaveCommand = new RelayCommand(SaveIndent);
            AddProductCommand = new RelayCommand(AddProductToIndent);
            EditQuantityCommand = new RelayCommand(QuantityEdited);
            ChooseDealerCommand = new RelayCommand(ChooseDealer);
            PaymentMethodChangedCommand = new RelayCommand(UpdateCommissionsAndPrices);
        }

        void ChooseDealer()
        {
            ViewDealerChildWindow Dealers = new ViewDealerChildWindow();
            Dealers.Show();
        }

        //Method to be called in Constructor for Initialization
        void initialize()
        {
            Messenger.Default.Register<Dealer>(this, SaveDealer);
            SelectedProductChanged += new EventHandler(PlaceIndentViewModel_SelectedProdcutChanged);
            IndentInfos = new ObservableCollection<IndentInfo>();
            _context.Load(_context.GetEmployeesByUsernameQuery(UserInfo.Username)).Completed += new EventHandler(PlaceIndentViewModel_Completed);
            PaymentTypes = ConstantCollections.CommissionName;

            _isProductsLoading = true;
            BusyWindow Window = new BusyWindow();
            Window.Show();
            _context.Load(_context.GetProductsQuery().Where(c=>c.ProductStatus.Equals("Active"))).Completed += (s, e) =>
            {
                Window.Close();
                _isProductsLoading = true; 
            };
        }

        void SaveDealer(Dealer dealer)
        {
            RequestingDealer = new Dealer();
            RequestingDealer = dealer;
        }



        void PlaceIndentViewModel_SelectedProdcutChanged(object sender, EventArgs e)
        {
            //AddProductToIndent();
        }

        void PlaceIndentViewModel_Completed(object sender, EventArgs e)
        {
            Employee myEmployee = (sender as LoadOperation<Employee>).Entities.FirstOrDefault();
            RMId = myEmployee.EmployeeId;
        }

        void ConstructIndentInitials()
        {

            IndentToBePlaced.IssuedToId = RMId;
            IndentToBePlaced.IssuedById = RequestingDealer.DealerId;
            IndentToBePlaced.ForwardedToId = RMId;
            IndentToBePlaced.ForwardedToInventoryId = 0;
            IndentToBePlaced.DateOfPlace = DateTime.Now;
            IndentToBePlaced.PaymentMethod = SelectedPaymentType;
            IndentToBePlaced.IsConsideredByNSM = false;
            IndentToBePlaced.IndentCode = SerialCode;
            IndentToBePlaced.IndentStatus = "Placed by Dealer";
        }


        void ConstructIndentProductInfoes()
        {
            for (int i = 0; i < IndentInfos.Count; i++)
            {
                IndentProductInfo ProductInfo = new IndentProductInfo();
                ProductInfo.Product = IndentInfos[i].Product;
                ProductInfo.ProductId = IndentInfos[i].ProductID;
                ProductInfo.ProductPrice = IndentInfos[i].UnitPrice;
                ProductInfo.ProductQuantity = IndentInfos[i].Quantity;    //dealer knows how many kg he needs

                ProductInfo.EditTime = DateTime.Now;
                ProductInfo.IndentId = IndentToBePlaced.IndentId;
                ProductInfo.CommissionPercentage = IndentInfos[i].Commission;
                IndentToBePlaced.IndentProductInfoes.Add(ProductInfo);
            }
        }

        String ValidateIndent()
        {
            String ErrorMessage="";

            if (SerialCode == "")
            {
                ErrorMessage += "You havent provided an indent code.\n";
            }

            if (IndentInfos.Count.ToString().Equals("0"))
            {
                ErrorMessage += "You have added no products.\n";
            }
            
            for (int i = 0; i < IndentInfos.Count; i++)
            {
                if (IndentInfos[i].Quantity <= 0)
                {
                    ErrorMessage += "You have products With invalid quantity.\n";
                    break;
                }
            }

            if(RequestingDealer==null)
            {
                ErrorMessage +="You havent chosen a dealer.\n";
            }

            if(SelectedPaymentType==null)
            {
                ErrorMessage +="You havent chosen a Payment method.\n";
            }
            
            return ErrorMessage;
        }

        //save indent
        void SaveIndent()
        {
            String s = ValidateIndent();
            
            if (s != "")
            {
                ErrorMessage = s;
                ErrorTextVisible = true;
                return;
            }
            
            IndentToBePlaced = new Indent();
            ConstructIndentInitials();
            ConstructIndentProductInfoes();
            _context.Indents.Add(IndentToBePlaced);
            MyWindow.Show();
            _context.SubmitChanges(OnSubmitCompleted, null);
            return;
        }

        private void OnSubmitCompleted(SubmitOperation so)
        {
            MyWindow.Close();
            if (so.HasError==true)
            {
               // MessageBox.Show("indent failure!!!");
                Reset();
                return;
            }
            else
            {
                MessageBox.Show("Placed!!");
                ErrorMessage = "";
                Reset();
            }
        }

        

        IndentInfo ConvertProductToIndentInfo(Product Product)
        {
            IndentInfo AddedInfo = new IndentInfo();

            AddedInfo.ProductName = Product.ProductName;
            AddedInfo.BrandName = Product.BrandName;
            AddedInfo.ProductCode = Product.ProductCode;
            AddedInfo.PacketSize = Product.StockKeepingUnit;
            AddedInfo.UnitPrice = Product.PricePerUnit;
            AddedInfo.Product = Product;
            AddedInfo.ProductID = Product.ProductId;
     
            //update by payment method
            if (SelectedPaymentType == null)
            {
                AddedInfo.Commission = 0;
            }
            else
            {
                try
                {
                    AddedInfo.Commission = _context.Commissions.Where(c => c.CommissionName.Equals(SelectedPaymentType)&&c.Product.ProductCode==Product.ProductCode&&c.Product.ProductName=="DUMMY"&&c.CommissionStatus=="Active").SingleOrDefault().Percentage;
                }
                catch (Exception ex)
                {
                    AddedInfo.Commission = 0;
                }
            }

            return AddedInfo;
        }

        
        //*************************//
        //update commission for all products for change of payment type
        //and update all the prices
        //Fired on the selectionChanged of payment method combobox
        void UpdateCommissionsAndPrices()
        {
            IndentPriceTotal = 0;
            
            for (int i = 0; i < IndentInfos.Count(); i++)
            {
                try
                {
                      IndentInfos[i].Commission = _context.Commissions.Where(c => c.CommissionName.Equals(SelectedPaymentType) && c.Product.ProductCode == IndentInfos[i].Product.ProductCode && c.Product.ProductName == "DUMMY" && c.CommissionStatus == "Active").SingleOrDefault().Percentage;
                  
                }
                catch (Exception ex)
                {
                    IndentInfos[i].Commission = 0;
                  //  MessageBox.Show("No commission availabe for this product!");
                }
                
                IndentInfos[i].TotalPrice = (IndentInfos[i].Quantity * IndentInfos[i].UnitPrice) * ((100 - IndentInfos[i].Commission) / 100);
                IndentPriceTotal += IndentInfos[i].TotalPrice;
            }
        }

        
        //Fired when the quanity of a product added is changed(Obviously Unit Price only)
        //All other field are ready only.
        void QuantityEdited()
        {
            //*************************//
            //calculate on basis of commission all the prices
            if (SelectedIndentInfo.Quantity < 0) SelectedIndentInfo.Quantity = 0;
            Double Subtract = SelectedIndentInfo.TotalPrice;
            SelectedIndentInfo.TotalQuantity = SelectedIndentInfo.Quantity * SelectedIndentInfo.PacketSize;
            SelectedIndentInfo.NetPrice = SelectedIndentInfo.Quantity * SelectedIndentInfo.UnitPrice;
            SelectedIndentInfo.TotalPrice = (SelectedIndentInfo.Quantity * SelectedIndentInfo.UnitPrice)*((100-SelectedIndentInfo.Commission)/100);
            IndentPriceTotal -= Subtract;
            IndentPriceTotal += SelectedIndentInfo.TotalPrice;
            if (IndentPriceTotal < 0)
            {
                IndentPriceTotal = 0;
            }
        }

        
        //method to be fired when selection of select product AutoComplete Changes
        void AddProductToIndent()
        {
            try
            {
                bool flag = false;

                for (int i = 0; i < IndentInfos.Count; i++)
                {
                    if (IndentInfos[i].ProductID.Equals(SelectedProduct.ProductId))
                    {
                        flag = true;
                        break;
                    }
                }

                if (!flag) IndentInfos.Add(ConvertProductToIndentInfo(SelectedProduct));
                else MessageBox.Show("Product already added!!");
            }
            catch (Exception ex)
            { }
        }

        //method to be fired deleting a ListBox Item
        void DeleteIndentInfo()
        {
            IndentPriceTotal -= SelectedIndentInfo.TotalPrice;
            IndentInfos.Remove(SelectedIndentInfo);
            DeleteEnabled = false;
            SelectedIndentInfo = null;
        }

        //reset method
        void Reset()
        {
            SelectedProduct = new Product();
            RequestingDealer = null;
            SerialCode = null;
            SelectedPaymentType = null;
            IndentInfos.Clear();
            IndentPriceTotal = 0;
        }

        public ICommand DeleteCommand { get; set; }
        public ICommand ResetCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand AddProductCommand { get; set; }
        public ICommand EditQuantityCommand { get; set; }
        public ICommand ChooseDealerCommand { get; set; }
        public ICommand PaymentMethodChangedCommand { get; set; }

        public ObservableCollection<IndentInfo> IndentInfos { get; set; }
        public ObservableCollection<String> PaymentTypes { get; set; }
        public EntitySet<Product> Products { get { return _context.Products; } }

        /// <summary>
        /// The <see cref="ErrorTextVisible" /> property's name.
        /// </summary>
        public const string ErrorTextVisiblePropertyName = "ErrorTextVisible";

        private bool _errorTextVisible  ;

        /// <summary>
        /// Gets the ErrorTextVisible property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public bool ErrorTextVisible
        {
            get
            {
                return _errorTextVisible;
            }

            set
            {
                if (_errorTextVisible == value)
                {
                    return;
                }

                var oldValue = _errorTextVisible;
                _errorTextVisible = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(ErrorTextVisiblePropertyName);
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

                RaisePropertyChanged(ErrorMessagePropertyName);

            }
        }

        //Bound to the BusyIndicator's IsBusy While Loading product 
        /// <summary>
        /// The <see cref="IsProductsLoading" /> property's name.
        /// </summary>
        public const string IsProductsLoadingPropertyName = "IsProductsLoading";

        private bool _isProductsLoading = false;

        /// <summary>
        /// Sets and gets the IsProductsLoading property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsProductsLoading
        {
            get
            {
                return _isProductsLoading;
            }

            set
            {
                if (_isProductsLoading == value)
                {
                    return;
                }

                _isProductsLoading = value;
                RaisePropertyChanged(IsProductsLoadingPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SerialCode" /> property's name.
        /// </summary>
        public const string SerialCodePropertyName = "SerialCode";

        private String _serialCode;

        /// <summary>
        /// Sets and gets the SerialCode property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public String SerialCode
        {
            get
            {
                return _serialCode;
            }

            set
            {
                if (_serialCode == value)
                {
                    return;
                }

                _serialCode = value;
                RaisePropertyChanged(SerialCodePropertyName);
            }
        }


        //selected Product from Product AutoCompleteBox
        /// <summary>
        /// The <see cref="SelectedProduct" /> property's name.
        /// </summary>
        public const string SelectedProductPropertyName = "SelectedProduct";

        private Product _selectedProduct;

        /// <summary>
        /// Sets and gets the SelectedProduct property.
        /// Changes to that property's value raise the PropertyChanged event. 
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


                _selectedProduct = value;
                RaisePropertyChanged(SelectedProductPropertyName);
                SelectedProductChanged(this, new EventArgs());
            }
        }

        //Bound with PaymentMethod ComboBox SelectedItem
        /// <summary>
        /// The <see cref="SelectedPaymentType" /> property's name.
        /// </summary>
        public const string SelectedPaymentTypePropertyName = "SelectedPaymentType";

        private String _selectedPaymentType;

        /// <summary>
        /// Sets and gets the SelectedPaymentType property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public String SelectedPaymentType
        {
            get
            {
                return _selectedPaymentType;
            }

            set
            {
                if (_selectedPaymentType == value)
                {
                    return;
                }

                _selectedPaymentType = value;
                RaisePropertyChanged(SelectedPaymentTypePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="IndentPriceTotal" /> property's name.
        /// </summary>
        public const string IndentPriceTotalPropertyName = "IndentPriceTotal";

        private Double _indentPriceTotal;

        /// <summary>
        /// Sets and gets the IndentPriceTotal property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Double IndentPriceTotal
        {
            get
            {
                return _indentPriceTotal;
            }

            set
            {
                if (_indentPriceTotal == value)
                {
                    return;
                }

                _indentPriceTotal = value;
                RaisePropertyChanged(IndentPriceTotalPropertyName);
            }
        }

        //Bound to the ListBox's SelectedItem
        /// <summary>
        /// The <see cref="SelectedIndentInfo" /> property's name.
        /// </summary>
        public const string SelectedIndentInfoPropertyName = "SelectedIndentInfo";

        private IndentInfo _selectedIndentInfo;

        /// <summary>
        /// Sets and gets the SelectedIndentInfo property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public IndentInfo SelectedIndentInfo
        {
            get
            {
                return _selectedIndentInfo;
            }

            set
            {
                if (_selectedIndentInfo == value)
                {
                    return;
                }

                _selectedIndentInfo = value;
                if (value != null) DeleteEnabled = true;
                RaisePropertyChanged(SelectedIndentInfoPropertyName);

            }
        }

        /// <summary>
        /// The <see cref="SelectedRegion" /> property's name.
        /// </summary>
        public const string SelectedRegionPropertyName = "SelectedRegion";

        private Region _selectedRegion;

        /// <summary>
        /// Sets and gets the SelectedRegion property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Region SelectedRegion
        {
            get
            {
                return _selectedRegion;
            }

            set
            {
                if (_selectedRegion == value)
                {
                    return;
                }

                _selectedRegion = value;
                RaisePropertyChanged(SelectedRegionPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="DeleteEnabled" /> property's name.
        /// </summary>
        public const string DeleteEnabledPropertyName = "DeleteEnabled";

        private bool _deleteEnabled;

        /// <summary>
        /// Sets and gets the DeleteEnabled property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool DeleteEnabled
        {
            get
            {
                return _deleteEnabled;
            }

            set
            {
                if (_deleteEnabled == value)
                {
                    return;
                }

                _deleteEnabled = value;
                RaisePropertyChanged(DeleteEnabledPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="RequestingDealer" /> property's name.
        /// </summary>
        public const string RequestingDealerPropertyName = "RequestingDealer";

        private Dealer _requestingDealer;

        /// <summary>
        /// Sets and gets the RequestingDealer property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Dealer RequestingDealer
        {
            get
            {
                return _requestingDealer;
            }

            set
            {
                if (_requestingDealer == value)
                {
                    return;
                }

                _requestingDealer = value;
                RaisePropertyChanged(RequestingDealerPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Date" /> property's name.
        /// </summary>
        public const string DatePropertyName = "Date";

        private DateTime _date;

        /// <summary>
        /// Sets and gets the Date property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public DateTime Date
        {
            get
            {
                return _date;
            }

            set
            {
                if (_date == value)
                {
                    return;
                }

                _date = value;
                RaisePropertyChanged(DatePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="IndentNo" /> property's name.
        /// </summary>
        public const string IndentNoPropertyName = "IndentNo";

        private String _indentNo;

        /// <summary>
        /// Sets and gets the IndentNo property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public String IndentNo
        {
            get
            {
                return _indentNo;
            }

            set
            {
                if (_indentNo == value)
                {
                    return;
                }

                _indentNo = value;
                RaisePropertyChanged(IndentNoPropertyName);
            }
        }
    }
}
