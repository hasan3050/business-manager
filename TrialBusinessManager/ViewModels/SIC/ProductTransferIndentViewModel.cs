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
    public class ProductTransferIndentViewModel:ViewModelBase
    {
        AgroDomainContext _context=new AgroDomainContext();
        TransferIndent Indent = new TransferIndent();
        BusyWindow MyWindow = new BusyWindow();
        int flag=0;

        public ProductTransferIndentViewModel()
        {
            Initialize();
        }

        void Initialize()
        {
            RegisterForCommands();
            LoadData();
        }

        void LoadData()
        {
            MyWindow.Show();
            TransferDetails = new ObservableCollection<TransferIndentDetail>();
            _context.Load(_context.GetInventoriesQuery()).Completed+=(s,e)=>
                {
                    flag++;
                    if(flag==2)
                        MyWindow.Close();
                };
            _context.Load(_context.GetProductsQuery()).Completed+=(s,e)=>
                {
                     flag++;
                     if(flag==2)
                        MyWindow.Close();
                };
        }

        void RegisterForCommands()
        {
            AddCommand = new RelayCommand(AddItem);
            DeleteCommand = new RelayCommand(DeleteItem);
            ResetCommand = new RelayCommand(Reset);
            SubmitCommand = new RelayCommand(Submit);
        }

        TransferIndentDetail ConvertProductToTransferDetails(Product product)
        {
            TransferIndentDetail Detail = new TransferIndentDetail();
            Detail.Product = new Product();
            Detail.Product = product;
            return Detail;
        }

        void AddItem()
        {
            if (SelectedProduct == null)
            {
                MessageBox.Show("Please select a product!");
                return;
            }

            TransferDetails.Add(ConvertProductToTransferDetails(SelectedProduct));
        }

        void DeleteItem()
        {
            if (SelectedDetail == null)
            {
                MessageBox.Show("Please select an item to delete!");
                return;
            }

            TransferDetails.Remove(SelectedDetail);
        }

        void ContructTransferIndentInitials()
        {
            Indent.Status = "Placed By SIC";
            Indent.DateOfIssue = DateTime.Now;
            Indent.IssuedById = UserInfo.EmployeeID;
            Indent.IssuedFromInventoryId = UserInfo.Inventory.InventoryId;
            Indent.IssuedToInventoryId = SelectedInvenotry.InventoryId;
            Indent.IssuedToSICId = SelectedInvenotry.StoreInChargeId;
        }

        void Submit()
        {
            if (!Validate())
            {
                return;
            }

            MyWindow.Show();

            ContructTransferIndentInitials();

            foreach (var item in TransferDetails)
                Indent.TransferIndentDetails.Add(item);

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
                MessageBox.Show("Transfer Indent succesfully issued!!");
            }

            Reset();
        }

        void Reset()
        {
            TransferDetails.Clear();
            Indent = new TransferIndent();
            SelectedInvenotry = null;
            SelectedProduct = null;
        }

        bool Validate()
        {
            ErrorMessage = "";
            bool flag=true;
            
            if (SelectedInvenotry == null)
            {
                ErrorMessage+="You have to select an inventory to forward\n";
                flag=false;
            }
            else if(SelectedInvenotry.InventoryId.Equals(UserInfo.Inventory.InventoryId))
            {
                ErrorMessage+="Invalid Inventory selection\n";
                flag = false;
            }

            if (TransferDetails.Count == 0)
            {
                ErrorMessage += "You didnt add any product for transfer\n";
                flag = false;
            }

            foreach (var item in TransferDetails)
            {
                if (item.PlacedProductQuantity <= 0)
                {
                    ErrorMessage += "You have items with zero or negative quantity\n";
                    flag = false;
                }
            }

            return flag;
        }

        

        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SubmitCommand { get; set; }
        public ICommand ResetCommand { get; set; }

        public EntitySet<Product> Products { get{return _context.Products;} }
        public EntitySet<Inventory> Inventories { get{return _context.Inventories;} }

        /// <summary>
        /// The <see cref="SelectedInvenotry" /> property's name.
        /// </summary>
        public const string SelectedInvenotryPropertyName = "SelectedInvenotry";

        private Inventory _selectedInventory  ;

        /// <summary>
        /// Gets the SelectedInvenotry property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public Inventory SelectedInvenotry
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
                RaisePropertyChanged(SelectedInvenotryPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SelectedProduct" /> property's name.
        /// </summary>
        public const string SelectedProductPropertyName = "SelectedProduct";

        private Product _selectedProduct ;

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
        /// The <see cref="TransferDetails" /> property's name.
        /// </summary>
        public const string TransferDetailsPropertyName = "TransferDetails";

        private ObservableCollection<TransferIndentDetail> _transferDetails  ;

        /// <summary>
        /// Gets the TransferDetails property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public ObservableCollection<TransferIndentDetail> TransferDetails
        {
            get
            {
                return _transferDetails;
            }

            set
            {
                if (_transferDetails == value)
                {
                    return;
                }

                var oldValue = _transferDetails;
                _transferDetails = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(TransferDetailsPropertyName);

            }
        }

        /// <summary>
        /// The <see cref="SelectedDetail" /> property's name.
        /// </summary>
        public const string SelectedDetailPropertyName = "SelectedDetail";

        private TransferIndentDetail _selectedDetail ;

        /// <summary>
        /// Gets the SelectedDetail property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public TransferIndentDetail SelectedDetail
        {
            get
            {
                return _selectedDetail;
            }

            set
            {
                if (_selectedDetail == value)
                {
                    return;
                }

                var oldValue = _selectedDetail;
                _selectedDetail = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(SelectedDetailPropertyName);

            }
        }

        /// <summary>
        /// The <see cref="ErrorMessage" /> property's name.
        /// </summary>
        public const string ErrorMessagePropertyName = "ErrorMessage";

        private String _errorMessage ="" ;

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
