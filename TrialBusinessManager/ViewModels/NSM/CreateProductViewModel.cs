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

namespace TrialBusinessManager.ViewModels.NSM
{

    public class CreateProductViewModel:ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        BusyWindow MyWindow = new BusyWindow();
        String EditProductGroupName;
        bool ErrorFlag = false;
        ObservableCollection<Commission> RemoveCommissionCollection = new ObservableCollection<Commission>();
        public CreateProductViewModel()
        {
           Initialize();
        }

        void LoadData()
        {
            MyWindow.Show();
            _context.Load(_context.GetProductsQuery()).Completed += (s, e) =>
                {
                    MyWindow.Close();
                    PopulateWings();
                };
            _context.Load(_context.GetCommissionsQuery());
        }

        void RegisterCommands()
        {
            CreateBrandCommand = new RelayCommand(CreateBrand);
            WingChangedCommand = new RelayCommand(Wingchanged);
            TypeChangedCommand = new RelayCommand(TypeChanged);
            CreateBrandCommand = new RelayCommand(EditBrand);
            CreateWingCommand = new RelayCommand(EditWing);
            CreateTypeCommand = new RelayCommand(EditType);
            AddSKUCommand = new RelayCommand(AddSKU);
            AddCommissionCommand  = new RelayCommand(AddCommission);
            RemoveCommissionCommand = new RelayCommand(RemoveCommission);
            SaveCommand = new RelayCommand(Save);
            ResetCommand = new RelayCommand(Reset);
            RemoveSKUCommand = new RelayCommand(RemoveSKU);
            ActivateSKUCommand = new RelayCommand(ActivateSKU);
            UpdateCommand = new RelayCommand(UpdateEdit);
        }


        void Initialize()
        {
            newProduct = new Product(){IsImported=true,IsOpOrHibrid=true};
            RegisterCommands();
            LoadData();
        }

        void Reset()
        {
            NewProduct = new Product() { IsImported = true, IsOpOrHibrid = true };
            SKUcollection.Clear();
            CommissionCollection.Clear();
            SelectedType = null;
            SelectedTypeText = "";
            SelectedWing=null;
            SelectedWingText = "";
        }

        void Save()
        {
            ErrorFlag = false;
            if(_context.Products.Any(e=>e.GroupName==newProduct.GroupName))
            {
                MessageBox.Show("Product Name has to be unique!!");
                return;
            }
            if (_context.Products.Any(e => e.ProductCode == NewProduct.ProductCode))
            {
                MessageBox.Show("Product Code has to be unique!!");
                return;
            }

            if (_context.Products.Any(e => e.BrandName == NewProduct.BrandName))
            {
                MessageBox.Show("Brand Name has to be unique!!");
                return;
            }

            foreach (SKUPrice x in SKUcollection)
            {
                if (x.Price <= 0)
                {
                    MessageBox.Show("Price can not be zero or negative!");
                    return;
                }
                if (ErrorFlag == true) return;
           
            }

            if (ErrorFlag == true) return;
                 _context.RejectChanges();

            /////Creating separate products for each SKU///////
            foreach (SKUPrice x in SKUcollection)
            {
               
                SetProductProperties(x, NewProduct.GroupName + " " + x.SKU.ToString());
                
            }

            if (ErrorFlag == true)
            {
                _context.RejectChanges();
                return;
            }

            /////Creating the DUMMY product///////
            SKUPrice DummySKUPrice = new SKUPrice();
            DummySKUPrice.SKU = 0;
            DummySKUPrice.Price = 0;
            SetProductProperties(DummySKUPrice,"DUMMY");
            if (ErrorFlag == true)
            {
                _context.RejectChanges();
                return;
            }
            
            MyWindow.Show();
            _context.SubmitChanges().Completed += (s, args) => { MyWindow.Close(); Reset(); MessageBox.Show("Product Created"); };

        }

        void SetProductProperties(SKUPrice x,String ProductName)
        {
            Product MyProduct = new Product(); 
            try
            {
               

                MyProduct.BrandName = NewProduct.BrandName;
                MyProduct.GroupName = NewProduct.GroupName;
                MyProduct.ProductCode = NewProduct.ProductCode;
                MyProduct.PurchasePeriodStart = NewProduct.PurchasePeriodStart;
                MyProduct.PurchasePeriodEnd = NewProduct.PurchasePeriodEnd;
                MyProduct.SalesPeriodStart = NewProduct.SalesPeriodStart;
                MyProduct.SalesPeriodEnd = NewProduct.SalesPeriodEnd;
                MyProduct.IsImported = NewProduct.IsImported;
                MyProduct.IsOpOrHibrid = NewProduct.IsOpOrHibrid;
                MyProduct.SubType = NewProduct.SubType;
                MyProduct.ProductName = ProductName;
                MyProduct.StockKeepingUnit = x.SKU;
                MyProduct.PricePerUnit = x.Price;
                MyProduct.ProductStatus = "Active";
                MyProduct.IntroducedDate = DateTime.Now;
                //If false then input s from comboBox
                if (string.IsNullOrEmpty(SelectedTypeText) == true)
                {
                    MyProduct.ProductType = SelectedType.Value;
                }
                //If else then input is from TextBox
                else
                {
                    MyProduct.ProductType = SelectedTypeText;
                }
                //If false then input s from comboBox
                if (string.IsNullOrEmpty(SelectedWingText) == true)
                {
                    MyProduct.ProductWing = SelectedWing.Value;
                }
                //If else then input is from TextBox
                else
                {
                    MyProduct.ProductWing = SelectedWingText;
                }
                if (ProductName == "DUMMY")
                {

                    foreach (Commission commission in CommissionCollection)
                    {
                        if (commission.Percentage <= 0)
                        {
                            MessageBox.Show("Invalid commission input!");
                            ErrorFlag = true;
                            return;
                        }

                        if (commission.Duration < 0)
                        {
                            MessageBox.Show("Invalid commission input!");
                            ErrorFlag = true;
                            return;
                        }
                        Commission newCommission = new Commission();
                        newCommission.AdminId = commission.AdminId;
                        newCommission.CommissionName = commission.CommissionName;
                        newCommission.CommissionStatus = commission.CommissionStatus;
                        newCommission.Duration = commission.Duration;
                        newCommission.IntroducedDate = commission.IntroducedDate;
                        newCommission.NSMId = commission.NSMId;
                        newCommission.Percentage = commission.Percentage;

                        MyProduct.Commissions.Add(newCommission);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Please provide necessary information to create a product!");
                ErrorFlag = true;
                return;
            }
           if (CanSave(MyProduct) == true)
           {
               if(MyProduct.ProductName=="DUMMY")
                   _context.Products.Add(MyProduct);

               else if(!_context.Products.Any(e=>e.ProductName==MyProduct.ProductName))
                _context.Products.Add(MyProduct);
           }
           else
           {
               MessageBox.Show("Please provide necessary information to create a product!");
               ErrorFlag = true;
               return;
           }
        }

        void UpdateEdit()
        {
            String GroupName="";
            try
            {
                
                if (_context.Products.Any(e => e.GroupName == NewProduct.GroupName && e.ProductId != NewProduct.ProductId && e.ProductName == "DUMMY"))
                {
                    MessageBox.Show("Product Name has to be unique!!");
                    return;
                }
                if (_context.Products.Any(e => e.ProductCode == NewProduct.ProductCode&&e.ProductId!=NewProduct.ProductId&&e.ProductName=="DUMMY"))
                {
                    MessageBox.Show("Product Code has to be unique!!");
                    return;
                }

                if (_context.Products.Any(e => e.BrandName == NewProduct.BrandName && e.ProductId != NewProduct.ProductId && e.ProductName == "DUMMY"))
                {
                    MessageBox.Show("Brand Name has to be unique!!");
                    return;
                }

                if (CanSave(NewProduct) == false)
                {
                    MessageBox.Show("Please provide necessary information to update the product !!");
                    return;
                }
                foreach (SKUPrice x in TrackSKUcollection)
                {
                    

                        Product MyProduct = new Product();
                        MyProduct.BrandName = NewProduct.BrandName;
                        MyProduct.GroupName = NewProduct.GroupName;
                        MyProduct.ProductCode = NewProduct.ProductCode;
                        MyProduct.PurchasePeriodStart = NewProduct.PurchasePeriodStart;
                        MyProduct.PurchasePeriodEnd = NewProduct.PurchasePeriodEnd;
                        MyProduct.SalesPeriodStart = NewProduct.SalesPeriodStart;
                        MyProduct.SalesPeriodEnd = NewProduct.SalesPeriodEnd;
                        MyProduct.IsImported = NewProduct.IsImported;
                        MyProduct.IsOpOrHibrid = NewProduct.IsOpOrHibrid;
                        MyProduct.ProductWing = NewProduct.ProductWing;
                        MyProduct.ProductType = NewProduct.ProductType;
                        MyProduct.SubType = NewProduct.SubType;
                        MyProduct.StockKeepingUnit = x.SKU;
                        MyProduct.ProductName = NewProduct.GroupName + " " + x.SKU.ToString();


                        MyProduct.ProductStatus = "Active";
                        MyProduct.PricePerUnit = x.Price;


                        MyProduct.IntroducedDate = DateTime.Now;
                      /*  if (MyProduct.PricePerUnit <= 0)
                        {
                            MessageBox.Show("Price can not be zero or negative!");
                            return;
                        }*/


                        _context.Products.Add(MyProduct);
                        GroupName = MyProduct.GroupName;
                    

                }
                foreach (Product MyProduct in _context.Products.Where(e => e.GroupName == EditProductGroupName))
                {
                    MyProduct.BrandName = NewProduct.BrandName;
                    MyProduct.GroupName = NewProduct.GroupName;
                    MyProduct.ProductCode = NewProduct.ProductCode;
                    MyProduct.PurchasePeriodStart = NewProduct.PurchasePeriodStart;
                    MyProduct.PurchasePeriodEnd = NewProduct.PurchasePeriodEnd;
                    MyProduct.SalesPeriodStart = NewProduct.SalesPeriodStart;
                    MyProduct.SalesPeriodEnd = NewProduct.SalesPeriodEnd;
                    MyProduct.IsImported = NewProduct.IsImported;
                    MyProduct.IsOpOrHibrid = NewProduct.IsOpOrHibrid;
                    MyProduct.ProductWing = NewProduct.ProductWing;
                    MyProduct.ProductType = NewProduct.ProductType;
                    MyProduct.SubType = NewProduct.SubType;

                   /* if (MyProduct.PricePerUnit <= 0)
                    {
                        MessageBox.Show("Price can not be zero or negative!");
                        return;
                    }*/
                    if (MyProduct.ProductName != "DUMMY")
                    {
                        MyProduct.ProductName = NewProduct.GroupName + " " + MyProduct.StockKeepingUnit.ToString();
                    }
                    if (SKUcollection.Any(r => r.SKU == MyProduct.StockKeepingUnit))
                    {
                        MyProduct.ProductStatus = SKUcollection.Where(r => r.SKU == MyProduct.StockKeepingUnit).First().Status;
                        MyProduct.PricePerUnit = SKUcollection.Where(r => r.SKU == MyProduct.StockKeepingUnit).First().Price;
                    }

                    MyProduct.IntroducedDate = DateTime.Now;
                    GroupName = MyProduct.GroupName;

                }
              
                foreach (Commission x in RemoveCommissionCollection)
                {
                    _context.Commissions.Remove(x); 

                }
               
                foreach (Commission commission in CommissionCollection)
                {
                    if (!_context.Commissions.Any(e => e.CommissionId == commission.CommissionId))
                    {
                        Commission newCommission = new Commission();
                        newCommission.AdminId = commission.AdminId;
                        newCommission.CommissionName = commission.CommissionName;
                        newCommission.CommissionStatus = commission.CommissionStatus;
                        newCommission.Duration = commission.Duration;
                        newCommission.IntroducedDate = commission.IntroducedDate;
                        newCommission.NSMId = commission.NSMId;
                        newCommission.Percentage = commission.Percentage;

                        _context.Products.Where(e => e.GroupName == EditProductGroupName && e.ProductName == "DUMMY").First().Commissions.Add(newCommission);
                    }
                    
                    else 
                    {

                        _context.Commissions.Where(e => e.CommissionId == commission.CommissionId).First().Duration = commission.Duration;
                        _context.Commissions.Where(e => e.CommissionId == commission.CommissionId).First().Percentage = commission.Percentage;
                        _context.Commissions.Where(e => e.CommissionId == commission.CommissionId).First().CommissionStatus = commission.CommissionStatus;


                    }
                }
               
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
                return;
            }
            MyWindow.Show();
            while (_context.Products.Any(r => r.GroupName == GroupName && r.StockKeepingUnit == 0 && r.ProductName != "DUMMY"))
            {
                _context.Products.Remove(_context.Products.Where(r => r.GroupName == GroupName && r.StockKeepingUnit == 0 && r.ProductName != "DUMMY").First());
            }
            _context.SubmitChanges().Completed += (s, args) => { MyWindow.Close(); MessageBox.Show("Product successfully updated !!"); };
        
        }

        private bool CanSave(Product NewProduct)
        {
            if (string.IsNullOrEmpty(NewProduct.ProductName) == false
            && string.IsNullOrEmpty(NewProduct.ProductCode) == false
            && string.IsNullOrEmpty(NewProduct.BrandName) == false
            && string.IsNullOrEmpty(NewProduct.PurchasePeriodStart) == false
            && string.IsNullOrEmpty(NewProduct.PurchasePeriodEnd) == false
            && string.IsNullOrEmpty(NewProduct.SalesPeriodStart) == false
            && string.IsNullOrEmpty(NewProduct.SalesPeriodEnd) == false
            && string.IsNullOrEmpty(NewProduct.ProductWing) == false
            && string.IsNullOrEmpty(NewProduct.SubType) == false
            && string.IsNullOrEmpty(NewProduct.ProductType) == false
            && string.IsNullOrEmpty(NewProduct.GroupName) == false)

                return true;

            return false;
        }

        void CreateBrand()
        {
            NewProduct = new Product();
            SKUcollection.Clear();
            CommissionCollection.Clear();
            BrandEditable = false;
        }

        void AddSKU()
        {
            if (selectedSKU == 0)
            {
                MessageBox.Show("Stock Keeping Unit can not be zero !!");
                return;
            }
            if (!SKUcollection.Any(e => e.SKU == selectedSKU))
            {
                SKUPrice newSKU = new SKUPrice();
                newSKU.SKU = selectedSKU;
                newSKU.Price = 0;
                newSKU.Status = "Active";
                SKUcollection.Add(newSKU);
                TrackSKUcollection.Add(newSKU);
            }
            
        }

        void AddCommission()
        {
            try
            {
                if (!String.IsNullOrEmpty(SelectedCommissionName))
                {
                    if (!CommissionCollection.Any(e => e.CommissionName == SelectedCommissionName))
                    {
                        Commission newCommission = new Commission();
                        newCommission.CommissionName = SelectedCommissionName;

                        if (newCommission.CommissionName == "Advanced")
                        {
                            newCommission.Duration = 0;
                        }
                        else if (newCommission.CommissionName == "On Cash")
                        {

                            newCommission.Duration = 7;

                        }
                        else if (newCommission.CommissionName == "One Month Installment")
                        {

                            newCommission.Duration = 30;

                        }
                        else if (newCommission.CommissionName == "Two Month Installment")
                        {

                            newCommission.Duration = 60;

                        }
                        else if (newCommission.CommissionName == "Three Month Installment")
                        {

                            newCommission.Duration = 90;

                        }
                        else 
                        {
                            newCommission.Duration = 0;
                        }

                        newCommission.Percentage = 0;
                        newCommission.CommissionStatus = "Active";
                        newCommission.IntroducedDate = DateTime.Now;
                        newCommission.AdminId = UserInfo.EmployeeID;
                        newCommission.NSMId = UserInfo.EmployeeID;
                        CommissionCollection.Add(newCommission);
                    }
                }
            }
            catch (Exception ex)
            {
                return; 
            }

        }

        void RemoveCommission()
        {
            try
            {
                RemoveCommissionCollection.Add(CommissionCollection.ElementAt(SelectedCommission));
                CommissionCollection.RemoveAt(SelectedCommission);
                
            }
            catch (Exception ex)
            {
                return;
            }
        
        }

        void Wingchanged()
        {
            try
            {
                
                ProductTypes = new ObservableCollection<StringValue>();
                var ProductWithGrouping = _context.Products.Where(e => e.ProductWing == SelectedWing.Value).GroupBy(c => c.ProductType);

                ProductTypes.Clear();

                foreach (var temp in ProductWithGrouping)
                {
                    ProductTypes.Add(new StringValue(temp.Key));
                }
            }
            catch (Exception ex)
            { return; }
        }

        void TypeChanged()
        {
            ProductGroups = new ObservableCollection<StringValue>();
            try
            {
                var ProductWithGrouping = _context.Products.Where(e => e.ProductWing == SelectedWing.Value && e.ProductType == SelectedType.Value);

                foreach (var x in ProductWithGrouping)
                {
                    StringValue s = new StringValue(x.GroupName);
                    if (!ProductGroups.Any(c=>c.Value.Equals(s.Value)))
                        ProductGroups.Add(s);
                }
            }
            catch (Exception ex)
            {
            }
        }

        void PopulateWings()
        {
            ProductWings = new ObservableCollection<StringValue>();
            var ProductWithGrouping=_context.Products.GroupBy(c =>c.ProductWing);
            

            foreach (var x in ProductWithGrouping)
            {
                StringValue s = new StringValue(x.Key);
                if (!ProductWings.Contains(s))
                {
                    ProductWings.Add(s);
                }
            }
        }

        void EditBrand()
        {
            BrandEditable = false;
        }

        void EditWing()
        {
            WingEditable = false;
        }

        void EditType()
        {
            TypeEditable = false;
        }

        ///////////////Public methods to toggle selections////////////////
        public void ToggleWings()
        {
            if (WingEditable == false)
                WingEditable = true;
            else
                WingEditable = false;
        }
        public void ToggleTypes()
        {
            if (TypeEditable == false)
                TypeEditable = true;
            else
                TypeEditable = false;
        
        }
        void GroupSelected()
        {
           
            EditProductGroupName = NewProduct.GroupName;
            SKUcollection.Clear();
            CommissionCollection.Clear();
            RemoveCommissionCollection.Clear();
            foreach (Product x in _context.Products.Where(e => e.GroupName == NewProduct.GroupName))
            {
                if (x.StockKeepingUnit != 0)
                {
                    SKUPrice y = new SKUPrice();
                    y.SKU = x.StockKeepingUnit;
                    y.Price = x.PricePerUnit;
                    y.Status = x.ProductStatus;
                    SKUcollection.Add(y);
                }

            }

            foreach (Commission x in _context.Commissions.Where(e=>e.Product.GroupName==NewProduct.GroupName&&e.Product.ProductName=="DUMMY"))
            {
               
                CommissionCollection.Add(x);
            }
        
        }

        void RemoveSKU()
        {
            try
            {
                if (_context.Products.Any(e => e.GroupName == EditProductGroupName && e.StockKeepingUnit == SKUcollection.ElementAt(SKUindex).SKU))
                {
                    SKUcollection.ElementAt(SKUindex).Status = "Inactive";
                }
                else
                {
                    SKUPrice removeSKU = SKUcollection.ElementAt(SKUindex);
                    SKUcollection.Remove(removeSKU);
                    TrackSKUcollection.Remove(removeSKU);
                }
            }
            catch (Exception ex)
            { }

        }

        void ActivateSKU()
        {
            try
            {
                if (_context.Products.Any(e => e.GroupName == EditProductGroupName && e.StockKeepingUnit == SKUcollection.ElementAt(SKUindex).SKU))
                {
                    SKUcollection.ElementAt(SKUindex).Status = "Active";
                }
            }
            catch (Exception ex)
            { }
        }

        public ICommand WingChangedCommand { get; set; }
        public ICommand TypeChangedCommand { get; set; }
        public ICommand CreateBrandCommand { get; set; }
        public ICommand CreateTypeCommand { get; set; }
        public ICommand CreateWingCommand { get; set; }
        public ICommand AddSKUCommand { get; set; }
        public ICommand RemoveSKUCommand { get; set; }
        public ICommand ActivateSKUCommand { get; set; }
        public ICommand AddCommissionCommand { get; set; }
        public ICommand RemoveCommissionCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand ResetCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        /// <summary>
        /// The <see cref="SelectedBrand" /> property's name.
        /// </summary>
        public const string SelectedBrandPropertyName = "SelectedGroup";

        private StringValue _selectedGroup;

        /// <summary>
        /// Gets the SelectedBrand property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public StringValue SelectedGroup
        {
            get
            {
                return _selectedGroup;
            }

            set
            {
                if (_selectedGroup == value)
                {
                    return;
                }

                var oldValue = _selectedGroup;
                _selectedGroup = value;
                BrandEditable = true;
                try
                {
                    NewProduct = _context.Products.Where(e => e.GroupName == value.Value&&e.ProductName=="DUMMY").First();
                    GroupSelected();
                }
                catch (Exception ex)
                { }
          
                RaisePropertyChanged(SelectedBrandPropertyName);

            }
        }

        /// <summary>
        /// The <see cref="ProductBrands" /> property's name.
        /// </summary>
        public const string ProductBrandsPropertyName = "ProductGroups";

        private ObservableCollection<StringValue> _ProductGroups;

        /// <summary>
        /// Gets the ProductBrands property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public ObservableCollection<StringValue> ProductGroups
        {
            get
            {
                return _ProductGroups;
            }

            set
            {
                if (_ProductGroups == value)
                {
                    return;
                }

                var oldValue = _ProductGroups;
                _ProductGroups = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(ProductBrandsPropertyName);

            }
        }

        /// <summary>
        /// The <see cref="SelectedType" /> property's name.
        /// </summary>
        public const string SelectedTypePropertyName = "SelectedType";

        private StringValue _selectedType  ;

        /// <summary>
        /// Gets the SelectedType property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public StringValue SelectedType
        {
            get
            {
                return _selectedType;
            }

            set
            {
                if (_selectedType == value)
                {
                    return;
                }

                var oldValue = _selectedType;
                _selectedType = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(SelectedTypePropertyName);

            }
        }

        /// <summary>
        /// The <see cref="SelectedWing" /> property's name.
        /// </summary>
        public const string SelectedWingPropertyName = "SelectedWing";

        private StringValue _selectedWing  ;

        /// <summary>
        /// Gets the SelectedWing property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public StringValue SelectedWing
        {
            get
            {
                return _selectedWing;
            }

            set
            {
                if (_selectedWing == value)
                {
                    return;
                }

                var oldValue = _selectedWing;
                _selectedWing = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(SelectedWingPropertyName);

            }
        }


        private String _selectedWingText=null;

        /// <summary>
        /// Gets the SelectedWing property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public String SelectedWingText
        {
            get
            {
                return _selectedWingText;
            }

            set
            {
                if (_selectedWingText == value)
                {
                    return;
                }

                var oldValue = _selectedWingText;
                _selectedWingText = value;

                // Update bindings, no broadcast
                RaisePropertyChanged("SelectedWingText");

            }
        }

        private String _selectedTypeText=null;

        /// <summary>
        /// Gets the SelectedWing property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public String SelectedTypeText
        {
            get
            {
                return _selectedTypeText;
            }

            set
            {
                if (_selectedTypeText == value)
                {
                    return;
                }

                var oldValue = _selectedTypeText;
                _selectedTypeText = value;

                // Update bindings, no broadcast
                RaisePropertyChanged("SelectedTypeText");

            }
        }




        /// <summary>
        /// The <see cref="TypeEditable" /> property's name.
        /// </summary>
        public const string TypeEditablePropertyName = "TypeEditable";

        private bool _typeEditable =false ;

        /// <summary>
        /// Gets the TypeEditable property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public bool TypeEditable
        {
            get
            {
                return _typeEditable;
            }

            set
            {
                if (_typeEditable == value)
                {
                    return;
                }

                var oldValue = _typeEditable;
                _typeEditable = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(TypeEditablePropertyName);

            }
        }

        /// <summary>
        /// The <see cref="WingEditable" /> property's name.
        /// </summary>
        public const string WingEditablePropertyName = "WingEditable";

        private bool _wingEditable = false;

        /// <summary>
        /// Gets the WingEditable property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public bool WingEditable
        {
            get
            {
                return _wingEditable;
            }

            set
            {
                if (_wingEditable == value)
                {
                    return;
                }

                var oldValue = _wingEditable;
                _wingEditable = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(WingEditablePropertyName);

            }
        }

        private Product newProduct;

        /// <summary>
        /// Gets the WingEditable property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public Product NewProduct
        {
            get
            {
                return newProduct;
            }

            set
            {
                if (newProduct == value)
                {
                    return;
                }

                newProduct= value;

                // Update bindings, no broadcast
                RaisePropertyChanged("NewProduct");

            }
        }

        private Double selectedSKU;

        /// <summary>
        /// Gets the WingEditable property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public Double SelectedSKU
        {
            get
            {
                return selectedSKU;
            }

            set
            {
                if (selectedSKU == value)
                {
                    return;
                }

                selectedSKU = value;

                // Update bindings, no broadcast
                RaisePropertyChanged("SelectedSKU");

            }
        }

        private int _SKUindex;

        /// <summary>
        /// Gets the WingEditable property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public int SKUindex
        {
            get
            {
                return _SKUindex;
            }

            set
            {
                if (_SKUindex == value)
                {
                    return;
                }

                _SKUindex = value;

                // Update bindings, no broadcast
                RaisePropertyChanged("SKUindex");

            }
        }


        /// <summary>
        /// The <see cref="BrandEditable" /> property's name.
        /// </summary>
        public const string BrandEditablePropertyName = "BrandEditable";

        private bool _brandEditable = true;

        /// <summary>
        /// Gets the BrandEditable property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public bool BrandEditable
        {
            get
            {
                return _brandEditable;
            }

            set
            {
                if (_brandEditable == value)
                {
                    return;
                }

                var oldValue = _brandEditable;
                _brandEditable = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(BrandEditablePropertyName);

            }
        }

        /// <summary>
        /// The <see cref="ProductTypes" /> property's name.
        /// </summary>
        public const string ProductTypesPropertyName = "ProductTypes";

        private ObservableCollection<StringValue> _productTypes  ;

        /// <summary>
        /// Gets the ProductTypes property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public ObservableCollection<StringValue> ProductTypes
        {
            get
            {
                return _productTypes;
            }

            set
            {
                if (_productTypes == value)
                {
                    return;
                }

                var oldValue = _productTypes;
                _productTypes = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(ProductTypesPropertyName);

            }
        }

        /// <summary>
        /// The <see cref="SelectedCommissionName" /> property's name.
        /// </summary>
        public const string SelectedCommissionNamePropertyName = "SelectedCommissionName";
        private string _selectedCommissionName = string.Empty;

        public string SelectedCommissionName
        {
            get { return _selectedCommissionName; }

            set
            {
                if (_selectedCommissionName == value) { return; }

                _selectedCommissionName = value;
                RaisePropertyChanged(SelectedCommissionNamePropertyName);
            }
        }

        private int _selectedCommission;

        public int SelectedCommission
        {
            get { return _selectedCommission; }

            set
            {
                if (_selectedCommission == value) { return; }

                _selectedCommission = value;
                RaisePropertyChanged("SelectedCommission");
            }
        }


        private ObservableCollection<SKUPrice> TrackSKUcollection = new ObservableCollection<SKUPrice>();


        /// <summary>
        /// The <see cref="CommissionCollection" /> Commission Collaection bound to the View and will be inserted to database.
        /// </summary>
        /// //
        public const string SKUcollectionPropertyName = "SKUcollection";
        private ObservableCollection<SKUPrice> _SKUcollection = new ObservableCollection<SKUPrice>();


        public ObservableCollection<SKUPrice> SKUcollection
        {
            get { return _SKUcollection; }
            set
            {
                if (_SKUcollection == value)
                {
                    return;
                }

                _SKUcollection = value;
                RaisePropertyChanged(SKUcollectionPropertyName);
            }
        }



        // Constant period for binding with the combo box
        public const string PeriodPropertyName = "Period";
        private ObservableCollection<string> _period = ConstantCollections.TimePeriodName;

        public ObservableCollection<string> Period
        {
            get { return _period; }
            set { RaisePropertyChanged(PeriodPropertyName); }
        }

        //The static collection for static commision list//
        /// <summary>
        /// The <see cref="CommissionType" /> property's name.
        /// </summary>
        public const string CommissionTypePropertyName = "CommissionType";
        private ObservableCollection<string> _commissionType = ConstantCollections.CommissionName;

        public ObservableCollection<string> CommissionType
        {
            get { return _commissionType; }
            set { RaisePropertyChanged(CommissionTypePropertyName); }
        }


        //for Added Commission to datagrid//
        /// <summary>
        /// The <see cref="CommissionCollection" /> Commission Collaection bound to the View and will be inserted to database.
        /// </summary>
        public const string CommissionCollectionPropertyName = "CommissionCollection";
        private ObservableCollection<Commission> _commissionCollection = new ObservableCollection<Commission>();

        public ObservableCollection<Commission> CommissionCollection
        {
            get { return _commissionCollection; }
            set
            {
                if (_commissionCollection == value)
                {
                    return;
                }

                _commissionCollection = value;
                RaisePropertyChanged(CommissionCollectionPropertyName);
            }
        }


        /// <summary>
        /// The <see cref="ProductWings" /> property's name.
        /// </summary>
        public const string ProductWingsPropertyName = "ProductWings";

        private ObservableCollection<StringValue> _productWings;

        /// <summary>
        /// Gets the ProductWings property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public ObservableCollection<StringValue> ProductWings
        {
            get
            {
                return _productWings;
            }

            set
            {
                if (_productWings == value)
                {
                    return;
                }

                var oldValue = _productWings;
                _productWings = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(ProductWingsPropertyName);

            }
        }
    }

    public class StringValue:ViewModelBase
    {
        public StringValue()
        {
        }
        public StringValue(string s)
        {
            Value = s;
        }

        /// <summary>
        /// The <see cref="Value" /> property's name.
        /// </summary>
        public const string ValuePropertyName = "Value";

        private String _value=""  ;

        /// <summary>
        /// Gets the Value property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public String Value
        {
            get
            {
                return _value;
            }

            set
            {
                if (_value == value)
                {
                    return;
                }

                var oldValue = _value;
                _value = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(ValuePropertyName);

            }
        }
        


    }

    public class SKUPrice : ViewModelBase
    {
        private Double sku;


        public Double SKU
        {
            get
            {
                return sku;
            }

            set
            {
                if (sku == value)
                {
                    return;
                }

                sku = value;

                // Update bindings, no broadcast
                RaisePropertyChanged("SKU");

            }
        }

        private Double price;

        public Double Price
        {
            get
            {
                return price;
            }

            set
            {
                if (price == value)
                {
                    return;
                }

                price = value;

                // Update bindings, no broadcast
                RaisePropertyChanged("Price");

            }
        }

        private String status;

        public String Status
        {
            get
            {
                return status;
            }

            set
            {
                if (status == value)
                {
                    return;
                }

                status = value;

                // Update bindings, no broadcast
                RaisePropertyChanged("Status");

            }
        }

    }
}
