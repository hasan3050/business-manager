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

namespace TrialBusinessManager.ViewModels.NSM
{
    public class ForwardIndentNSMViewModel:ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        int flag = 0;
        public ICommand RejectCommand { get; set; }
        public ICommand ForwardCommand { get; set; }
        BusyWindow Busy = new BusyWindow();
        private List<IndentProductInfo> IndentProductInfoCollection;
        private List<IndentProductInfo> IndentCollection ;
        public EntitySet<Inventory> InventoryCollection { get { return _context.Inventories; } }
        private List<Employee> EmployeeCollection { get { return _context.Employees.ToList(); } }

        public ForwardIndentNSMViewModel()
        {
            TotalPrice = 0;
            ButtonEnableBool = false;
            ErrorMessage = string.Empty;
            LoadAll();
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
            _context.Load(_context.GetInventoriesQuery().Where(e => e.StoreInChargeId == SelectedIndent.ForwardedToId)).Completed +=
                (s, e) => 
                { 
                    SelectedInventory = _context.Inventories.SingleOrDefault();
                    flag++;
                    if (flag == 3)
                    {
                        ButtonEnableBool = true;
                    }
                };
        }

        private void LoadIndent()
        {
            _context.Load(_context.GetIndentsQuery().Where(e => e.IndentId == SelectedIndent.IndentId)).Completed += (s, e) =>
                {
                    flag++;
                    if (flag == 3)
                    {
                        ButtonEnableBool = true;
                    }
                };
        }
        
        private void LoadIndentProduct()
        {
            LoadOperation<IndentProductInfo> LoadIndentProducts = _context.Load(_context.GetIndentProductInfoesQuery().Where(e => e.Indent.IssuedById == SelectedIndent.IssuedById));
            LoadIndentProducts.Completed += (s, e) =>
            {
                IndentCollection = _context.IndentProductInfos.Where(a => a.Indent.IssuedById == SelectedIndent.IssuedById &&
                                                                    (a.Indent.IndentStatus == "Forwarded to NSM" || a.Indent.IndentStatus == "Forwarded to SIC" || a.Indent.IndentStatus == "Forwarded to DO")).ToList(); // for finding indent info which are waiting to be generated for bill

                IndentProductInfoCollection = _context.IndentProductInfos.Where(o => o.IndentId == SelectedIndent.IndentId).ToList();
                GenerateBindIndentProduct();
                flag++;
                if (flag == 3)
                {
                    ButtonEnableBool = true;
                }
            };
        }

        #endregion

        #region Submit

        private void Reject()
        {
            ButtonEnableBool = false;
            Indent RejectedIndent = _context.Indents.Where(e => e.IndentId == SelectedIndent.IndentId).SingleOrDefault();
            RejectedIndent.IsConsideredByNSM = true;
            RejectedIndent.IndentStatus = "Rejected by NSM";
            Busy.Show();
            _context.SubmitChanges(OnSubmitCompleted, null);
        }

        private void Forward()
        {
            ButtonEnableBool = false;

            Indent ForwardedIndent = _context.Indents.Where(e => e.IndentId == SelectedIndent.IndentId).SingleOrDefault();
            ForwardedIndent.IsConsideredByNSM = true;
            ForwardedIndent.IndentStatus = "Forwarded to SIC";
            Busy.Show();
            _context.SubmitChanges(OnSubmitCompleted,null);
        }

        private void OnSubmitCompleted(SubmitOperation op)
        {
            Busy.Close();
            ButtonEnableBool = true;
            if (op.HasError == true)
            {
               
                return;
            }
            MessageBox.Show("Submitted!");
            Messenger.Default.Send<string, ViewIndentForForwardingByNSMViewModel>("Close");
        }

        private void Reset()
        {
            ButtonEnableBool = true;
            ErrorMessage = string.Empty;            
        }

        #endregion

        #region Edit & Verify

        private void GenerateBindIndentProduct()
        { 
            BindIndentProduct.Clear();
            for (int i = 0; i < IndentProductInfoCollection.Count; i++)
            {
                IndentInfoDetail temp = new IndentInfoDetail();
                temp.Product=   IndentProductInfoCollection[i].Product;
                temp.Commission=IndentProductInfoCollection[i].CommissionPercentage;
                temp.Quantity= (double) IndentProductInfoCollection[i].ProductQuantityByRM;
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
            ErrorMessage = string.Empty;

            if (SelectedIndent.Dealer.ActivityStatus != "Active")
            {
                ErrorMessage = string.Format("This Dealer is " + SelectedIndent.Dealer.ActivityStatus + "\n");
            }
            else if ( (x=(FloatingSum() + (SelectedIndent.Dealer.TotalDue-SelectedIndent.Dealer.TotalCollection))) > SelectedIndent.Dealer.CreditLimit)
            {
                ErrorMessage += string.Format("Credit Limit Exceeded by BDT "+(x-SelectedIndent.Dealer.CreditLimit).ToString());
            }
        }

        private void UpdateTotalPrice()
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
        private Inventory _selectedInventory = new Inventory();
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
