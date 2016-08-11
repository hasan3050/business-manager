using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TrialBusinessManager.Web;
using TrialBusinessManager.Models;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.ServiceModel.DomainServices.Client;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Messaging;
using TrialBusinessManager.Views;
using TrialBusinessManager.Views.RM;

namespace TrialBusinessManager.ViewModels.RM
{
    public class ForwardIndentViewModel:ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        int flag = 0;
        BusyWindow Busy = new BusyWindow();
        public ICommand CellEditCommand { get; set; }
        public ICommand RejectCommand { get; set; }
        public ICommand ForwardCommand { get; set; }

        private List<IndentProductInfo> IndentProductInfoCollection;
        private List<IndentProductInfo> IndentCollection ;
        public EntitySet<Inventory> InventoryCollection { get { return _context.Inventories; } }

        public ForwardIndentViewModel()
        {
            ButtonEnableBool = false;
            LoadAll();
            ErrorMessage = string.Empty;
            TotalPrice = 0;
            CellEditCommand = new RelayCommand(CellEdit);
            RejectCommand = new RelayCommand(Reject);
            ForwardCommand = new RelayCommand(Forward);
        }

        #region Method

        #region Load
        private void LoadAll()
        {
            LoadIndent();
            LoadIndentProduct();
            LoadInventory();
        }

        private void LoadInventory()
        {
            _context.Load(_context.GetInventoriesQuery()).Completed += (s, e) =>
                {
                    flag++;
                    if (flag == 3) ButtonEnableBool = true;
                };
        }

        private void LoadEmployee()
        {
            _context.Load(_context.GetEmployeesQuery().Where(e => e.Designation == "Store In Charge")).Completed += (s, e) =>
                {
                   
                };
        }

        private void LoadIndent()
        {
            _context.Load(_context.GetIndentsQuery().Where(e => e.IndentId == SelectedIndent.IndentId)).Completed += (s, e) =>
                {
                    flag++;
                    if (flag == 3) ButtonEnableBool = true;
                };
        }
        
        private void LoadIndentProduct()
        {
            LoadOperation<IndentProductInfo> LoadIndentProducts = _context.Load(_context.GetIndentProductInfoesQuery().Where(e => e.Indent.IssuedById == SelectedIndent.IssuedById));
            LoadIndentProducts.Completed += (s, e) =>
            {
                flag++;
                if (flag == 3) ButtonEnableBool = true;
                IndentCollection = _context.IndentProductInfos.Where(a => a.Indent.IssuedById == SelectedIndent.IssuedById &&
                                                                   (a.Indent.IndentStatus == "Forwarded to NSM" || a.Indent.IndentStatus == "Forwarded to SIC" || a.Indent.IndentStatus == "Forwarded to DO")).ToList(); // for finding indent info which are waiting to be generated for bill
                
                IndentProductInfoCollection = _context.IndentProductInfos.Where(o => o.IndentId == SelectedIndent.IndentId).ToList();
                GenerateBindIndentProduct();
            };
        }

        #endregion

        #region Submit

        private void Reject()
        {
            ButtonEnableBool = false;
            Indent RejectedIndent = _context.Indents.Where(e => e.IndentId == SelectedIndent.IndentId).SingleOrDefault();
            RejectedIndent.IndentStatus = "Rejected by RM";
            
            _context.SubmitChanges(OnSubmitCompleted, null);
        }

        private void Forward()
        {
            
            if (SelectedInventory != null)
            {
                ButtonEnableBool = false;
                for (int i = 0; i < BindIndentProduct.Count; i++)
                {
                    IndentProductInfoCollection[i].ProductQuantityByRM = BindIndentProduct[i].Quantity;
                    IndentProductInfoCollection[i].EditTimeRM = DateTime.Now;
                }
                /*             if (SelectedInventory == null)
                             {
                                 MessageBox.Show("No active Store In Charge is available for selected region!");
                                 ButtonEnableBool = true;
                                 return;
                             }*/
                Indent ForwardedIndent = _context.Indents.Where(e => e.IndentId == SelectedIndent.IndentId).SingleOrDefault();
                ForwardedIndent.ForwardedToInventoryId = SelectedInventory.InventoryId;
                ForwardedIndent.ForwardedToId = SelectedInventory.StoreInChargeId;

                if (NSMCheckedBool)
                {
                    ForwardedIndent.IndentStatus = "Forwarded to NSM";
                }

                else
                {
                    ForwardedIndent.IndentStatus = "Forwarded to SIC";
                }

                Busy.Show();
                _context.SubmitChanges(OnSubmitCompleted, null);
            }
            else 
            {
                MessageBox.Show("PLEASE SELECT AN INVENTORY TO FORWARD");   
            }
        }

        private void OnSubmitCompleted(SubmitOperation op)
        {
            Busy.Close();
            ButtonEnableBool = true;
            if (op.HasError == true)
            {
                _context.RejectChanges();
                return;
            }
            
            MessageBox.Show("Submitted!");
            Messenger.Default.Send<string, ViewIndentForForwardingByRMViewModel>("Close");
            
        }

        private void Reset()
        {
            ButtonEnableBool = true;
            ErrorMessage = string.Empty;
            NSMCheckedBool = false;
        }

        #endregion

        #region Edit & Verify

        private void CellEdit()
        {
            UpdateTotalPrice();
        }

        private void GenerateBindIndentProduct()
        { 
            BindIndentProduct.Clear();
            for (int i = 0; i < IndentProductInfoCollection.Count; i++)
            {
                IndentInfoDetail temp = new IndentInfoDetail();
                temp.Product=   IndentProductInfoCollection[i].Product;
                temp.Commission=IndentProductInfoCollection[i].CommissionPercentage;
                temp.Quantity=  IndentProductInfoCollection[i].ProductQuantity;
                temp.Price =    IndentProductInfoCollection[i].ProductPrice;
                temp.TotalQuantity = 0;
                temp.TotalPrice = 0;
                temp.NetPrice = 0;
                temp.PlacedQuantity = temp.Quantity;
                BindIndentProduct.Add(temp);
            }
    
            UpdateTotalPrice();
        }

        private double FloatingSum()
        {
            double sum = 0;
            for (int i = 0; i < IndentCollection.Count; i++)
            {
                if (IndentCollection[i].IndentId != SelectedIndent.IndentId)
                {
                    sum += ((IndentCollection[i].ProductPrice * IndentCollection[i].ProductQuantity) * (1 - IndentCollection[i].CommissionPercentage/100));
                }
            }

            sum += BindIndentProduct.Sum(e => e.NetPrice);

            return sum;
        }

        private void IndentVerify()
        {
            double x;
            if (SelectedIndent.Dealer.ActivityStatus != "Active")
            {
                ErrorMessage = string.Format("This Dealer is " + SelectedIndent.Dealer.ActivityStatus + "\n");
                NSMCheckedBool = true;
            }
            else if ( (x=(FloatingSum() + (SelectedIndent.Dealer.TotalDue-SelectedIndent.Dealer.TotalCollection))) > SelectedIndent.Dealer.CreditLimit)
            {
                ErrorMessage = string.Format("Credit Limit Exceeded by BDT "+(x-SelectedIndent.Dealer.CreditLimit).ToString());
                NSMCheckedBool = true;
            }
            else
            {
                if (ErrorMessage.Contains("Edited Quantity") == false) ErrorMessage = string.Empty;
                NSMCheckedBool = false;
            }
        }

        private void UpdateTotalPrice()
        {
            if (SelectedIndex > -1 && Math.Ceiling(BindIndentProduct[SelectedIndex].Quantity)<=Math.Ceiling(BindIndentProduct[SelectedIndex].PlacedQuantity))
            {
                if (BindIndentProduct[SelectedIndex].Quantity < 0)
                    BindIndentProduct[SelectedIndex].Quantity = BindIndentProduct[SelectedIndex].PlacedQuantity;
                CellEditBool = true;
                if (ErrorMessage.Contains("Edited Quantity Exceeded Placed Quantity")) ErrorMessage = string.Empty;
            }

            else if (SelectedIndex > -1)
            {
                CellEditBool = false;
          //      ErrorMessage = string.Format("Edited Quantity Exceeded Placed Quantity by "+(BindIndentProduct[SelectedIndex].Quantity-BindIndentProduct[SelectedIndex].PlacedQuantity));//change in UI by label
                MessageBox.Show(string.Format("Edited Quantity Exceeded Placed Quantity by " + (BindIndentProduct[SelectedIndex].Quantity - BindIndentProduct[SelectedIndex].PlacedQuantity)));
                BindIndentProduct[SelectedIndex].Quantity = BindIndentProduct[SelectedIndex].PlacedQuantity;

            }

            if (CellEditBool)
            {
                TotalPrice = 0;

                for (int i = 0; i < BindIndentProduct.Count; i++)
                {
                    BindIndentProduct[i].TotalQuantity = 0;
                    BindIndentProduct[i].TotalPrice = 0;
                    BindIndentProduct[i].NetPrice = 0;
                    TotalPrice += BindIndentProduct[i].NetPrice;
                }
                IndentVerify();
            }
        }
        #endregion
        #endregion

        #region Properties

        public const string SelectedIndentPropertyName = "SelectedIndent";
        private Indent _selectedIndent=StaticMessaging.IndentMessage;
        public Indent SelectedIndent
        {
            get { return _selectedIndent; }
            set
            {
                if (_selectedIndent == value) { return; }

                _selectedIndent = value;
                RaisePropertyChanged(SelectedIndentPropertyName);
            }
        }

        public const string SelectedInventoryPropertyName = "SelectedInventory";
        private Inventory _selectedInventory = null;
       
        public Inventory SelectedInventory
        {
            get { return _selectedInventory; }
            set
            {
                if (_selectedInventory == value) { return; }

                _selectedInventory = value;
                RaisePropertyChanged(SelectedInventoryPropertyName);
            }
        }

        public const string BindIndentProductPropertyName = "BindIndentProduct";
        private ObservableCollection<IndentInfoDetail> _bindIndentProduct = new ObservableCollection<IndentInfoDetail>();
        public ObservableCollection<IndentInfoDetail> BindIndentProduct
        {
            get { return _bindIndentProduct; }
            set
            {
                if (_bindIndentProduct == value) { return; }

                _bindIndentProduct = value;
                RaisePropertyChanged(BindIndentProductPropertyName);
            }
        }

        public const string TotalPricePropertyName = "TotalPrice";
        private double _totalPrice =0;
        public double TotalPrice
        {
            get { return _totalPrice; }
            set
            {
                if (_totalPrice == value) { return; }

                _totalPrice = value;
                RaisePropertyChanged(TotalPricePropertyName);
            }
        }

        public const string ErrorMessagePropertyName = "ErrorMessage";
        private string _errorMessage = string.Empty;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                if (_errorMessage == value) { return; }

                _errorMessage = value;
                RaisePropertyChanged(ErrorMessagePropertyName);
            }
        }

        public const string NSMMessagePropertyName = "NSMMessage";
        private string _nsmMessage = string.Empty;
        public string NSMMessage
        {
            get { return _nsmMessage; }
            set
            {
                if (_nsmMessage == value) { return; }

                _nsmMessage = value;
                RaisePropertyChanged(NSMMessagePropertyName);
            }
        }

        public const string SelectedIndexPropertyName = "SelectedIndex";
        private int _selectedIndex ;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                if (_selectedIndex == value) { return; }

                _selectedIndex = value;
                RaisePropertyChanged(SelectedIndexPropertyName);
            }
        }

        public const string CellEditBoolPropertyName = "CellEditBool";
        private bool _cellEditBool=true;
        public bool CellEditBool
        {
            get { return _cellEditBool; }
            set
            {
                if (_cellEditBool == value) { return; }

                _cellEditBool = value;
                RaisePropertyChanged(CellEditBoolPropertyName);
            }
        }

        public const string NSMCheckedBoolPropertyName = "NSMCheckedBool";
        private bool _NSMCheckedBool = false;
        public bool NSMCheckedBool
        {
            get { return _NSMCheckedBool; }
            set
            {
                if (_NSMCheckedBool == value) { return; }

                _NSMCheckedBool = value;
                if (_NSMCheckedBool == false)
                    NSMMessage = string.Empty;
                else
                    NSMMessage = "This Indent will be forwarded to NSM for consideration!";
                RaisePropertyChanged(NSMCheckedBoolPropertyName);
            }
        }

        public const string ButtonEnableBoolPropertyName = "ButtonEnableBool";
        private bool _ButtonEnableBool = true;
        public bool ButtonEnableBool
        {
            get { return _ButtonEnableBool; }
            set
            {
                if (_ButtonEnableBool == value) { return; }

                _ButtonEnableBool = value;
                RaisePropertyChanged(ButtonEnableBoolPropertyName);
            }
        }

        #endregion
    }
}
