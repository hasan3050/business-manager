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
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using System.ServiceModel.DomainServices.Client;
using System.Linq;
using System.Collections.ObjectModel;
using TrialBusinessManager.Models;
using GalaSoft.MvvmLight.Command;
using TrialBusinessManager.Views;
using System.ComponentModel.DataAnnotations;

namespace TrialBusinessManager.ViewModels.RM
{

    public class ProductInfo
    {
        public Double PurchasedPackets { get; set; }
        public Double UnitPrice { get; set; }
    }
    
    public class TransportationLossViewModel:ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        BusyWindow MyWindow = new BusyWindow();
        Dictionary<Product, ProductInfo> MyDic;
        bool ContextReceived = false;

        public TransportationLossViewModel()
        {
            Initialize(); 
        }

        void Initialize()
        {
            LossInfos = new ObservableCollection<LossInfo>();
            Products = new ObservableCollection<Product>();
            RegisterCommands();
            RegisterForMessages();
        }

        void RegisterCommands()
        {
            AddCommand = new RelayCommand(AddTransportationLoss);
            DeleteCommand = new RelayCommand(DeleteInfo);
            SubmitCommand = new RelayCommand(VerifyBill);
        }


        //all money updates here

        void AddTransportationLoss()
        {
            if (SelectedProduct == null)
            {
                MessageBox.Show("Please select a product");
                return;
            }

            for (int i = 0; i < LossInfos.Count; i++)
            {
                if (LossInfos[i].Product.Equals(SelectedProduct))
                {
                    MessageBox.Show("product Already added!!!!");
                    return;
                }
            }

            ConstructAndAddLossInfo(SelectedProduct);
        }

        void DeleteInfo()
        {
            if (SelectedInfo != null)
            {
                LossInfos.Remove(SelectedInfo);
            }
            else
            {
                MessageBox.Show("please select a row!!!");
                return;
            }
        }

        void ConstructAndAddLossInfo(Product Product)
        {
            LossInfo Info = new LossInfo();
            Info.Product = new Product();
            Info.Product = Product;
            Info.PurchasedQuantity = MyDic[Info.Product].PurchasedPackets;
            Info.LostAmount = 0;
            Info.LostQuantity = 0;
            Info.UnitPrice = MyDic[Info.Product].UnitPrice;

            LossInfos.Add(Info);
        }

        void VerifyBill()
        {
            if (LossInfos.Count == 0)
            {
                MessageBox.Show("No products added!!!");
                return;
            }
            
            for (int i = 0; i < LossInfos.Count; i++)
            {
                if (LossInfos[i].LostQuantity == 0 || LossInfos[i].LostQuantity > LossInfos[i].PurchasedQuantity)
                {
                    MessageBox.Show("You have transportation losses with invalid quantity!!");
                    return;
                }

            }

            MyWindow.Show();
            ConstructTranpsorationLosses();
            Bill.BillStatus = "Verified by RM";
            
            if ((Bill.ValidationErrors != null) &&(Bill.ValidationErrors.Count > 0))
            {
                foreach (ValidationResult result in Bill.ValidationErrors)
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
            
            if (so.HasError)
            {
                return;
            }
            else
            {
                MessageBox.Show("Bill Status Updated!!!!");
                Messenger.Default.Send<String,VerifyBillViewModel>("Verified");
            }
        }

        void ConstructTranpsorationLosses()
        {
            
            for (int i = 0; i < LossInfos.Count; i++)
            {
                TransportationLoss MyLoss = new TransportationLoss();
                MyLoss.ProductId = LossInfos[i].Product.ProductId;
                MyLoss.LossQuantity = LossInfos[i].LostQuantity;
                MyLoss.HasBalanced = false;
                //this will be converted to unit price
                MyLoss.UnitPrice = LossInfos[i].UnitPrice;
                MyLoss.Remarks = "RM Verified";
                //this field may be removed
                //a new value will be added MyLoss.BalancedQuantity
                MyLoss.BalancedQuantity = 0;
                
                if ((MyLoss.ValidationErrors != null) && (MyLoss.ValidationErrors.Count > 0))
                {
                    foreach (ValidationResult result in MyLoss.ValidationErrors)
                    {
                        string error = string.Format("Property [{0}] has problem [{1}]",
                          result.MemberNames.First(), // ?  
                          result.ErrorMessage);

                        MessageBox.Show(error, "Error", MessageBoxButton.OK);
                    }
                }

                Bill.TransportationLosses.Add(MyLoss);
            }
        }

        void RegisterForMessages()
        {
            Messenger.Default.Register<AgroDomainContext>(this, OnContextReceived);
            Messenger.Default.Register<Bill>(this, OnBillReceived);
            Messenger.Default.Register<Dealer>(this, OnDealerReceived);
        }

        void OnDealerReceived(Dealer MyDealer)
        {
            Dealer = new Dealer();
            Dealer = MyDealer;
        }

        void OnContextReceived(AgroDomainContext _ReceivedContext)
        {
            _context = _ReceivedContext;
            ContextReceived = true;
        }

        void OnBillReceived(Bill MyBill)
        {
            LossInfos.Clear();
            Bill = new Bill();
            Bill = MyBill;
            PopulateDictionary();
            PopulateProductComboBox();
        }

        void PopulateDictionary()
        {
            MyDic = new Dictionary<Product, ProductInfo>();
            for (int i = 0; i < Bill.BillProductInfoes.Count; i++)
            {
                Product MyProduct = new Product();
                MyProduct = _context.Products.Where(c => c.ProductId.Equals(Bill.BillProductInfoes.ElementAt(i).ProductId)).Single();

                if (MyDic.ContainsKey(MyProduct))
                {
                    MyDic[MyProduct].PurchasedPackets += Bill.BillProductInfoes.ElementAt(i).LotQuantity;
                }
                else
                {
                    ProductInfo Info = new ProductInfo();
                    Info.UnitPrice = Bill.BillProductInfoes.ElementAt(i).ProductPrice;
                    Info.PurchasedPackets = Bill.BillProductInfoes.ElementAt(i).LotQuantity;
                    MyDic.Add(MyProduct, Info);
                }
            }
        }
        
        void PopulateProductComboBox()
        {
            Products.Clear();

            foreach (KeyValuePair<Product, ProductInfo> entry in MyDic)
            {
                Products.Add(entry.Key);
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SubmitCommand { get; set; }
        
        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<LossInfo> LossInfos { get; set; }

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
        /// The <see cref="Bill" /> property's name.
        /// </summary>
        public const string BillPropertyName = "Bill";

        private Bill _bill  ;

        /// <summary>
        /// Gets the Bill property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
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

                var oldValue = _bill;
                _bill = value;

              

                // Update bindings, no broadcast
                RaisePropertyChanged(BillPropertyName);

                
            }
        }

        /// <summary>
        /// The <see cref="SelectedInfo" /> property's name.
        /// </summary>
        public const string SelectedInfoPropertyName = "SelectedInfo";

        private LossInfo _selectedInfo  ;

        /// <summary>
        /// Gets the SelectedInfo property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public LossInfo SelectedInfo
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
        /// The <see cref="Dealer" /> property's name.
        /// </summary>
        public const string DealerPropertyName = "Dealer";

        private Dealer _dealer  ;

        /// <summary>
        /// Gets the Dealer property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public Dealer Dealer
        {
            get
            {
                return _dealer;
            }

            set
            {
                if (_dealer == value)
                {
                    return;
                }

                var oldValue = _dealer;
                _dealer = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(DealerPropertyName);
            }
        }

        
    }
}
