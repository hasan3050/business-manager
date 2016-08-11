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
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TrialBusinessManager.Models;
using GalaSoft.MvvmLight.Command;

namespace TrialBusinessManager.ViewModels
{
    public class SalesReturnViewModel:ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        public event EventHandler SelectedProductChanged;
        public event EventHandler BillNoChanged;
        public Employee RM;
        IndentInfo InfoToBeAdded=new IndentInfo();
        SalesReturn _SalesReturn;
        Int64 DealerID;
        Int64 RMId;

        public void registerCommands()
        {
            DeleteCommand = new RelayCommand(DeleteSalesReturnInfo);
            ResetCommand = new RelayCommand(Reset);
            SaveCommand = new RelayCommand(SaveSalesReturn);
            //EditQuantityCommand = new RelayCommand(QuantityEdited);
         
        }

        //Method to be called in Constructor for Initialization
        public void initialize()
        {
            //Open child window for loading
            RM = new Employee();
            RequestingDealer = new Dealer();
            DealerID = new Int64();
            _context.Load(_context.GetEmployeesByUsernameQuery(UserInfo.Username)).Completed+=new EventHandler(RM_Loaded);
            _context.Load(_context.GetBillsQuery()).Completed += new EventHandler(BillLoading_Completed);
            SelectedProductChanged += new EventHandler(SalesReturnViewModel_SelectedProductChanged);
            BillNoChanged += new EventHandler(SalesReturnViewModel_BillNoChanged);
        }
        

        void BillLoading_Completed(object sender, EventArgs e)
        {
            _context.Load(_context.GetProductsQuery()).Completed+=new EventHandler(ProductLoading_Completed);
        }

        void RM_Loaded(object sender, EventArgs e)
        {
            Employee myEmployee = (sender as LoadOperation<Employee>).Entities.FirstOrDefault();
            RMId = myEmployee.EmployeeId;
            RM = myEmployee;
        }

       
        void Dealer_Loaded(object sender, EventArgs e)
        {
            Dealer myDealer = (sender as LoadOperation<Dealer>).Entities.FirstOrDefault();
            RequestingDealer = myDealer;
        }

        void DeleteSalesReturnInfo()
        {
            AcceptedTotal -= SelectedSalesReturnInfo.TotalAcceptedAmount;
            PlacedTotal -= SelectedSalesReturnInfo.TotalPlacedAmount;
            SalesReturnInfos.Remove(SelectedSalesReturnInfo);
            DeleteEnabled = false;
            SelectedSalesReturnInfo = null;
        }

        void Reset()
        {
            SalesReturnInfos.Clear();
            AcceptedTotal = 0;
            PlacedTotal = 0;
        }

        void SaveSalesReturn()
        {

        }

        void ProductLoading_Completed(object sender, EventArgs e)
        {
            //close child window
        }

        IndentInfo ConstructSalesReturnInfo(Product product, BillProductInfo bill)
        {
            IndentInfo AddInfo = new IndentInfo();
            AddInfo.ProductName = product.ProductName;
            AddInfo.BrandName = product.ProductCode;
            AddInfo.PacketSize = product.StockKeepingUnit;
            AddInfo.UnitPrice = product.PricePerUnit;
            AddInfo.TotalAcceptedAmount = 0;
            AddInfo.TotalPlacedAmount = 0;
            AddInfo.PlacedQuantity = 0;
            AddInfo.AcceptedQuantity = 0;
            //AddInfo.LotID = bill.LotId;
            return AddInfo;
        }

        void ConstructSalesReturnInitials()
        {
            _SalesReturn = new SalesReturn();
            _SalesReturn.DateOfIssue = DateTime.Now;
            _SalesReturn.DealerId = DealerID;
            _SalesReturn.Dealer = RequestingDealer;
            _SalesReturn.TotalAcceptedAmount = AcceptedTotal;
            _SalesReturn.TotalPlacedAmount = PlacedTotal;
            _SalesReturn.RMId = RMId;
            _SalesReturn.Employee = RM;
        }

        SalesReturnInfo ConvertIndentInfoToSalesReturnInfo(IndentInfo InfoToConvert)
        {
            SalesReturnInfo ConvertedInfo = new SalesReturnInfo();
            ConvertedInfo.AcceptedQuantity = InfoToConvert.AcceptedQuantity;
         //   ConvertedInfo.LotId = InfoToConvert.LotID;
            ConvertedInfo.PlacedQuantity = InfoToConvert.PlacedQuantity;
            ConvertedInfo.Product = InfoToConvert.Product;
            ConvertedInfo.ProductId = InfoToConvert.ProductID;
            ConvertedInfo.ProductPrice = InfoToConvert.UnitPrice;
            return ConvertedInfo;
        }

        

        //when a bill is selected bring all billproductinfos for that bill
        //load the dealer for whom the bill is
        //populate products combobox with products in the selected bill
        void SalesReturnViewModel_BillNoChanged(object sender, EventArgs e)
        {
            BillProductInfos = _context.Bills.Where(c => c.BillId.Equals(SelectedBill.BillId)).SingleOrDefault().BillProductInfoes.ToList();
            DealerID = _context.Bills.Where(c => c.BillId.Equals(SelectedBill.BillId)).SingleOrDefault().Indent.IssuedById;
            _context.Load(_context.GetDealerByIDQuery(DealerID)).Completed+=new EventHandler(Dealer_Loaded);
            
            for (int i = 0; i < BillProductInfos.Count; i++)
            {
                
                Product AddProduct = new Product();
                AddProduct = _context.Products.Where(c => c.ProductId.Equals(BillProductInfos[i].ProductId)).SingleOrDefault();
                Products.Add(AddProduct);
            }
        }

        //when a product is selected
        //add it converting the corresponding billproductinfo to indentinfo
        //to the datagrid
        void SalesReturnViewModel_SelectedProductChanged(object sender, EventArgs e)
        {
            BillProductInfo AddInfo = new BillProductInfo();
            AddInfo = BillProductInfos.Where(c => c.ProductId.Equals(SelectedProduct.ProductId)).SingleOrDefault();   //chosen product to be added to the datagrid
            InfoToBeAdded = ConstructSalesReturnInfo(SelectedProduct,AddInfo);
            SalesReturnInfos.Add(InfoToBeAdded);
        }

        //row edited
        void QuantityEdited()
        {
            PlacedQuantityEdited();
            AcceptedQuantityEdited();
        }

        //placed quantity edited
        void PlacedQuantityEdited()
        {
            double subtract = SelectedSalesReturnInfo.TotalPlacedAmount;
            SelectedSalesReturnInfo.TotalPlacedAmount = SelectedSalesReturnInfo.PlacedQuantity * SelectedSalesReturnInfo.UnitPrice * SelectedSalesReturnInfo.PacketSize;
            PlacedTotal -= subtract;
            PlacedTotal += SelectedSalesReturnInfo.TotalPlacedAmount;
        }


        //accepted quantity edited
        void AcceptedQuantityEdited()
        {
            double subtract = SelectedSalesReturnInfo.TotalAcceptedAmount;
            SelectedSalesReturnInfo.TotalAcceptedAmount = SelectedSalesReturnInfo.AcceptedQuantity * SelectedSalesReturnInfo.UnitPrice * SelectedSalesReturnInfo.PacketSize;
            AcceptedTotal -= subtract;
            AcceptedTotal += SelectedSalesReturnInfo.TotalAcceptedAmount;
        }

        //commands
        public ICommand DeleteCommand { get; set; }
        public ICommand ResetCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand EditQuantityCommand { get; set; }

        public List<BillProductInfo> BillProductInfos { get; set; }                //all product's infos for a particualr bill
        public ObservableCollection<Product> Products { get; set; }                //Bound to select product autoComplete
        public ObservableCollection<IndentInfo> SalesReturnInfos { get; set; }     //Bound to datagrid
        //public EntitySet<Bill> Bills { get{return _context.Bills;} set; }          //bount to bill select autocomplete
        
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

        

        /// <summary>
        /// The <see cref="IsLoading" /> property's name.
        /// </summary>
        public const string IsLoadingPropertyName = "IsLoading";

        private bool _isLoading ;

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
        /// The <see cref="SelectedSalesReturnInfo" /> property's name.
        /// </summary>
        public const string SelectedSalesReturnInfoPropertyName = "SelectedSalesReturnInfo";

        private IndentInfo _selectedSalesReturnInfo  ;

        /// <summary>
        /// Sets and gets the SelectedSalesReturnInfo property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public IndentInfo SelectedSalesReturnInfo
        {
            get
            {
                return _selectedSalesReturnInfo;
            }

            set
            {
                if (_selectedSalesReturnInfo == value)
                {
                    return;
                }

                _selectedSalesReturnInfo = value;
                if (value != null) DeleteEnabled = true;
                RaisePropertyChanged(SelectedSalesReturnInfoPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="PlacedTotal" /> property's name.
        /// </summary>
        public const string PlacedTotalPropertyName = "PlacedTotal";

        private Double _PlacedTotal  ;

        /// <summary>
        /// Sets and gets the PlacedTotal property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Double PlacedTotal
        {
            get
            {
                return _PlacedTotal;
            }

            set
            {
                if (_PlacedTotal == value)
                {
                    return;
                }

                _PlacedTotal = value;
                RaisePropertyChanged(PlacedTotalPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="AcceptedTotal" /> property's name.
        /// </summary>
        public const string AcceptedTotalPropertyName = "AcceptedTotal";

        private Double _acceptedTotal  ;

        /// <summary>
        /// Sets and gets the AcceptedTotal property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Double AcceptedTotal
        {
            get
            {
                return _acceptedTotal;
            }

            set
            {
                if (_acceptedTotal == value)
                {
                    return;
                }

                _acceptedTotal = value;
                RaisePropertyChanged(AcceptedTotalPropertyName);
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

        private DateTime _date  ;

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
        /// The <see cref="SalesReturnNo" /> property's name.
        /// </summary>
        public const string SalesReturnNoPropertyName = "SalesReturnNo";

        private String _salesReturnNo  ;

        /// <summary>
        /// Sets and gets the SalesReturnNo property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public String SalesReturnNo
        {
            get
            {
                return _salesReturnNo;
            }

            set
            {
                if (_salesReturnNo == value)
                {
                    return;
                }

                _salesReturnNo = value;
                RaisePropertyChanged(SalesReturnNoPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SelectedBill" /> property's name.
        /// </summary>
        public const string SelectedBillPropertyName = "SelectedBill";

        private Bill _selectedBill  ;

        /// <summary>
        /// Sets and gets the SelctedBill property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Bill SelectedBill
        {
            get
            {
                return _selectedBill;
            }

            set
            {
                if (_selectedBill == value)
                {
                    return;
                }

                _selectedBill = value;
                RaisePropertyChanged(SelectedBillPropertyName);
                BillNoChanged(this, new EventArgs());
            }
        }
    }
}
