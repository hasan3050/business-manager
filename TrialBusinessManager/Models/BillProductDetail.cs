using TrialBusinessManager.Web;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace TrialBusinessManager.Models
{
    public class BillProductDetail:ViewModelBase
    {
        public BillProductDetail()
        { 
                
        }
        
        private Bill _bill;
        public Bill Bill
        {
            get { return _bill; }
            set { if (value != _bill) _bill = value; RaisePropertyChanged("Bill"); }
        }

        private Product _product;
        public Product Product
        {
            get { return _product; }
            set { if (value != _product) _product = value; RaisePropertyChanged("Product"); }
        }

        private double _commission;
        public double Commission
        {
            get { return _commission; }
            set { if (value != _commission) _commission = value; RaisePropertyChanged("Commission"); }
        }

        private double _totalPrice;
        public double TotalPrice
        {
            set { if(value!=_totalPrice) _totalPrice=value; RaisePropertyChanged("TotalPrice"); }
            get { return _totalPrice; }
        }

        private double _netPrice;
        public double NetPrice
        {
            set { if (value != _netPrice) _netPrice = value; RaisePropertyChanged("NetPrice"); }
            get { return _netPrice; }
        }

        private double _paid;
        public double Paid
        {
            get { return _paid; }
            set { if (value != _paid) _paid = value; RaisePropertyChanged("Paid"); }
        }

        private double _loss;
        public double Loss
        {
            get { return _loss; }
            set { if (value != _loss) _loss = value; RaisePropertyChanged("Loss"); }
        }

        private double _amount;
        public double Amount
        {
            get { return _amount; }
            set
            {
                if (value != _amount)
                {
                    if (value > this.Payable)
                    {
                        value = this.Payable;
                        MessageBox.Show("Placed Amount Exceedes Payable Amount");
                    }

                    if (value < 0)
                    {
                        value = 0;
                        MessageBox.Show("Placed Amount can not be negative!");
                    }
                    _amount = value; 
                    RaisePropertyChanged("Amount");
                }
            }
        }

        private double _payable;
        public double Payable
        {
            get { return _payable; }
            set { if (value != _payable) _payable = value; RaisePropertyChanged("Payable"); }
        }

        private double _salesReturn;
        public double SalesReturn
        {
            get { return _salesReturn; }
            set { if (value != _salesReturn) _salesReturn = value; RaisePropertyChanged("SalesReturn"); }
        }

/*        public void GenerateBillDetail(List<BillProductInfo> BillProductList,List<TransportationLoss>TLossList, List<BillPaymentInfo> PaymentList,List<SalesReturnInfo>ReturnList)
        {
            this.Bill = BillProductList.First().Bill;
            this.Product = BillProductList[0].Product;
      //      Commission = BillProductList[0].ComissionPercentage;//need to set from outside for commission expired bill
      //      this.Loss = TLossList.Where(e=>e.Product==this.Product).Sum(e => e.LossQuantity*e.UnitPrice);
            this.Paid = PaymentList.Where(e => e.Product == this.Product).Sum(e => e.Amount);            
            
            this.Loss = 0;
            foreach (TransportationLoss temp in TLossList)
            {
                if(temp.Product==this.Product)
                this.Loss += (temp.LossQuantity * temp.UnitPrice);
            }

            List<SalesReturnInfo> tempList = ReturnList.Where(e => e.Product == this.Product).ToList();
            this.SalesReturn = 0;
            foreach (SalesReturnInfo temp in tempList)
            {
                this.SalesReturn += (temp.ProductPrice * temp.AcceptedQuantity);
            }

            this.TotalPrice = BillProductList.Sum(e => e.LotQuantity)*BillProductList[0].ProductPrice;
            this.NetPrice = (this.TotalPrice - this.Loss - this.SalesReturn) * (1 - (this.Commission / 100.0));
            this.Payable = (this.NetPrice - this.Paid) < 0 ? 0 : (this.NetPrice - this.Paid);
        }
        */
        public void GenerateBillDetail(DueInfo BillProduct)
        {
            this.Bill = BillProduct.Bill;
            this.Product = BillProduct.Product;
            //      Commission = BillProductList[0].ComissionPercentage;//need to set from outside for commission expired bill
            //      this.Loss = TLossList.Where(e=>e.Product==this.Product).Sum(e => e.LossQuantity*e.UnitPrice);
            this.Paid = BillProduct.Paid;
            this.Loss = BillProduct.TransportationLoss;
            this.SalesReturn = BillProduct.SalesReturn;
            this.TotalPrice = BillProduct.ProductCost;
            this.Commission = BillProduct.CommissionOnSell;
            this.NetPrice = (this.TotalPrice - this.Loss - this.SalesReturn) * (1 - (this.Commission / 100.0));
            this.Payable = (this.NetPrice - this.Paid) < 0 ? 0 : (this.NetPrice - this.Paid);
        }
    }
}
