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

namespace TrialBusinessManager.ViewModels.SIC
{
    public class ForwardIndentSICViewModel:ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        BusyWindow Busy = new BusyWindow();
        public ICommand CellEditCommand { get; set; }
        public ICommand ForwardCommand { get; set; }

        private List<IndentProductInfo> IndentProductInfoCollection;
        private List<InventoryProductInfo> InventoryProductCollection ;
        private int flag,loadFlag;
        
        public ForwardIndentSICViewModel()
        {
            StaticMessaging.SICIndentVerificationSubmitted = false;
            flag = 0;
            loadFlag = 0;
            ButtonEnableBool = false;
            LoadAll();
            TotalPrice = 0;
            IndentVerify();
            ErrorMessage = string.Empty;
            CellEditCommand = new RelayCommand(CellEdit);
            ForwardCommand = new RelayCommand(Forward);
        }

        #region Method

        #region Load
        private void LoadAll()
        {
            LoadInventory();
            LoadIndent();
            LoadIndentProduct();
        }

        private void LoadInventory()
        {
            _context.Load(_context.GetInventorieWithInventoryProductInfoesQuery().Where(e => e.StoreInChargeId == UserInfo.EmployeeID)).Completed += (s, e) => 
            {
                loadFlag++;
                if (loadFlag == 3) ButtonEnableBool = true;
                flag++;
                InventoryProductCollection = _context.InventoryProductInfos.ToList();
                if (flag == 2) IndentVerify();
            };
        }

        private void LoadIndent()
        {
            _context.Load(_context.GetIndentsQuery().Where(e => e.IndentId == SelectedIndent.IndentId)).Completed += (s, e) =>
                {
                    loadFlag++;
                    if (loadFlag == 3) ButtonEnableBool = true;
                };
        }
        
        private void LoadIndentProduct()
        {
            LoadOperation<IndentProductInfo> LoadIndentProducts = _context.Load(_context.GetIndentProductInfoesQuery().Where(e => e.IndentId==SelectedIndent.IndentId));
            LoadIndentProducts.Completed += (s, e) =>
            {
                loadFlag++;
                if (loadFlag == 3) ButtonEnableBool = true;
                flag++;
                IndentProductInfoCollection = _context.IndentProductInfos.Where(o => o.IndentId == SelectedIndent.IndentId).ToList();
                GenerateBindIndentProduct();
                if (flag == 2) IndentVerify();
            };
        }

        #endregion

        #region Submit

        private void Forward()
        {
            ButtonEnableBool = false;
            for (int i = 0; i < BindIndentProduct.Count; i++)
            {
                IndentProductInfoCollection[i].ProductQuantityBySIC = BindIndentProduct[i].Quantity;
                IndentProductInfoCollection[i].EditTimeSIC = DateTime.Now;
            }

            Indent ForwardedIndent = _context.Indents.Where(e => e.IndentId == SelectedIndent.IndentId).SingleOrDefault();
            ForwardedIndent.ForwardedToId = UserInfo.EmployeeID;
            ForwardedIndent.IndentStatus = "Forwarded to DO";
            Busy.Show();
            _context.SubmitChanges(OnSubmitCompleted,null);
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
            StaticMessaging.SICIndentVerificationSubmitted = true;
            ButtonEnableBool = false;
            ErrorMessage = "The Indent has been successfully forwarded,Please close this window to continue";
            return;
        }

        private void Reset()
        {
            ButtonEnableBool = true;
            ErrorMessage = string.Empty;
        }

        #endregion

        #region Edit & Verify

        private void CellEdit()
        {
            UpdateTotalPrice();
            IndentVerify();
        }

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

        private void IndentVerify()
        {
            double quantity;
            List<InventoryProductInfo> temp;
  //          string str=string.Empty;
            if (ErrorMessage.Contains("Edited Quantity") == false) ErrorMessage = string.Empty;

            for(int i=0;i<BindIndentProduct.Count;i++)
            {
                temp = InventoryProductCollection.Where(e=>e.ProductId==BindIndentProduct[i].Product.ProductId).ToList();
                quantity=temp.Sum(e=>e.UnfinishedQuantity)+temp.Sum(e=>e.OnProcessingQuantity)+temp.Sum(e=>e.FinishedQuantity);
  //              str += string.Format(BindIndentProduct[i].Product.ProductName + " -> Quantity : " + quantity + "\n");
                if(BindIndentProduct[i].TotalQuantity > quantity)
                    ErrorMessage+=string.Format(BindIndentProduct[i].Product.ProductName+" is inadequate in Inventory by "+(BindIndentProduct[i].TotalQuantity-quantity)+"\n");
            }
  //          MessageBox.Show(str);
        }

        private void UpdateTotalPrice()
        {
            if (SelectedIndex > -1 && BindIndentProduct[SelectedIndex].Quantity < 0)
            {
                MessageBox.Show("Edited Quantity can't be negative!");
                BindIndentProduct[SelectedIndex].Quantity = 0;
                return;
            }

            if (SelectedIndex > -1 && Math.Ceiling(BindIndentProduct[SelectedIndex].Quantity)<=Math.Ceiling(BindIndentProduct[SelectedIndex].PlacedQuantity))
            {
                CellEditBool = true;
                if (ErrorMessage.Contains("Edited Quantity")) ErrorMessage = string.Empty;
            }

            else if (SelectedIndex > -1)
            {
                CellEditBool = false;
          //      ErrorMessage = string.Format("Edited Quantity of "+BindIndentProduct[SelectedIndex].Product.ProductName+" Exceeded Placed Quantity by "+(BindIndentProduct[SelectedIndex].Quantity-BindIndentProduct[SelectedIndex].PlacedQuantity));//change in UI by label
                MessageBox.Show(string.Format("Edited Quantity of " + BindIndentProduct[SelectedIndex].Product.ProductName + " Exceeded Placed Quantity by " + (BindIndentProduct[SelectedIndex].Quantity - BindIndentProduct[SelectedIndex].PlacedQuantity)));
                BindIndentProduct[SelectedIndex].Quantity=BindIndentProduct[SelectedIndex].PlacedQuantity;
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
