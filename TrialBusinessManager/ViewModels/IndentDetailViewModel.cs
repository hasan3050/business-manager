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

namespace TrialBusinessManager.ViewModels
{
    public class IndentDetailViewModel:ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();        
        private List<IndentProductInfo> IndentProductInfoCollection;
        private int flag = 0;

        public IndentDetailViewModel()
        {
            LoadProduct();
            LoadIndent();
            Print = new RelayCommand(PrintPage);
        }

        #region Method

        private void PrintPage()
        {
            PrintFactory.PrintIndent(_context,SelectedIndent);
        }

        private void LoadProduct()
        {
            _context.Load(_context.GetProductsQuery()).Completed += (s, e) => 
            {
                flag++;
                
                if (flag == 2) 
                    GenerateBindIndentProduct();
            };
        }
        private void LoadIndent()
        {
            _context.Load(_context.GetIndentDetailsQuery().Where(e => e.IndentId == SelectedIndent.IndentId)).Completed += (s, e) =>
                {
                    IndentProductInfoCollection = _context.IndentProductInfos.Where(o => o.IndentId == SelectedIndent.IndentId).ToList();
                    flag++;
                    
                    if (flag == 2) 
                        GenerateBindIndentProduct();
                };
        }

        private void GenerateBindIndentProduct()
        { 
            BindIndentProduct.Clear();
            
            if (SelectedIndent.IsConsideredByNSM == true) 
                ErrorMessage = "This Indent has been considered by National Sales Manager.";
            else 
                ErrorMessage = string.Empty;
            
            for (int i = 0; i < IndentProductInfoCollection.Count; i++)
            {
                IndentInfoDetail temp = new IndentInfoDetail();
                temp.Product=   IndentProductInfoCollection[i].Product;
                temp.Commission=IndentProductInfoCollection[i].CommissionPercentage;
      
                if (SelectedIndent.IndentStatus == "Placed by Dealer" || SelectedIndent.IndentStatus == "Rejected by RM")
                    temp.Quantity=  IndentProductInfoCollection[i].ProductQuantity;

                else if (SelectedIndent.IndentStatus == "Forwarded to NSM" || SelectedIndent.IndentStatus == "Rejected by NSM" || SelectedIndent.IndentStatus == "Forwarded to SIC")
                {
                    if (IndentProductInfoCollection[i].ProductQuantityByRM == null)
                        temp.Quantity = 0;
                    else
                        temp.Quantity = (double)IndentProductInfoCollection[i].ProductQuantityByRM;
                }

                else if (SelectedIndent.IndentStatus == "Forwarded to DO" || SelectedIndent.IndentStatus == "Dispatched")
                {
                    if (IndentProductInfoCollection[i].ProductQuantityBySIC == null)
                        temp.Quantity = 0;
                    else
                        temp.Quantity = (double)IndentProductInfoCollection[i].ProductQuantityBySIC;
                }
                temp.Price =    IndentProductInfoCollection[i].ProductPrice;
                temp.TotalQuantity = 0;
                temp.TotalPrice = 0;
                temp.NetPrice = 0;
                temp.PlacedQuantity = IndentProductInfoCollection[i].ProductQuantity;
                temp.ProductInfo = IndentProductInfoCollection[i];
                BindIndentProduct.Add(temp);
            }

            TotalPrice = BindIndentProduct.Sum(e=>e.NetPrice);
        }

        #endregion

        #region Properties
        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { if (value != _errorMessage)_errorMessage = value; RaisePropertyChanged("ErrorMessage"); }
        }
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
        public ICommand Print { get; set; }
        #endregion

        
    }
}
