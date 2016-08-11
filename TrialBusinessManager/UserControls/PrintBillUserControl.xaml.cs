using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using TrialBusinessManager.Web;
using System.Collections.ObjectModel;
using TrialBusinessManager.Models;
using TrialBusinessManager.Views;
using System.ServiceModel.DomainServices.Client;

namespace TrialBusinessManager.UserControls
{
    public class UnpaidBillModel
    {
        public Bill Bill { get; set; }
        public Double TotalAmount { get; set; }
        public Double DueAmount { get; set; }
    }
    
    public partial class PrintBillUserControl : UserControl
    {
        AgroDomainContext _context = new AgroDomainContext();
        BusyWindow Busy = new BusyWindow();
        Bill MyBill = new Bill();
        ObservableCollection<PrintBillModel> Infos = new ObservableCollection<PrintBillModel>();
        ObservableCollection<UnpaidBillModel> UnpaidBills = new ObservableCollection<UnpaidBillModel>();
        public List<DueInfo> DueCollection=new List<DueInfo>(); 
        double total = 0;
        
        double DueTotal = 0;
        private void LoadDueInfo()
        {           
            DueCollection = _context.DueInfos.Where(o => o.DealerId == MyBill.DealerId).ToList();            
        }
        public PrintBillUserControl(AgroDomainContext Context,Bill Bill)
        {
            InitializeComponent();
            MyBill = Bill;
            _context = Context;
            SetBindings();
            PopulateModelCollection();
            CalculateTotal();
            
        }

        public void addData(int pageIndex,int pageSize)
        {
           

            Infos.Clear();
            int lastIndex = pageIndex * pageSize - 1;
            int firstIndex = (lastIndex - pageSize) + 1;

            if (lastIndex >= MyBill.BillProductInfoes.Count) lastIndex = MyBill.BillProductInfoes.Count - 1;
              
            for (int i = firstIndex; i <= lastIndex; i++)
            {
                PrintBillModel Info = new PrintBillModel();
                Info.Product = _context.Products.Where(c => c.ProductId.Equals(MyBill.BillProductInfoes.ElementAt(i).ProductId)).Single();
                Info.Info = MyBill.BillProductInfoes.ElementAt(i);
                Infos.Add(Info);
            }
        }

        void PopulateModelCollection()
        {
            if (!Flag)
                for (int i = 0; i < _context.BillProductInfos.Count; i++)
                {
                    if (_context.BillProductInfos.ElementAt(i).BillId.Equals(MyBill.BillId))
                    {
                        PrintBillModel Info = new PrintBillModel();
                        Info.Product = _context.Products.Where(c => c.ProductId.Equals(_context.BillProductInfos.ElementAt(i).ProductId)).Single();
                        Info.Info = _context.BillProductInfos.ElementAt(i);
                        Infos.Add(Info);
                    }
                }
            else
            {
                for (int i = 0; i < MyBill.BillProductInfoes.Count; i++)
                {
                    PrintBillModel Info = new PrintBillModel();
                    Info.Product = _context.Products.Where(c => c.ProductId.Equals(MyBill.BillProductInfoes.ElementAt(i).ProductId)).Single();
                    Info.Info = MyBill.BillProductInfoes.ElementAt(i);
                    Infos.Add(Info);
                }
            }
          //  MessageBox.Show(Infos.Count.ToString());
        }

        

        void SetBindings()
        {
            DealerControl.DataContext = MyBill.Dealer;
            BillInfoGrid.DataContext = MyBill;
            BillInfoDataGrid.ItemsSource = Infos;
            InventoryTxtBox.Text = _context.Inventories.Where(r => r.DispatchOfficerId == MyBill.DispatchedById).First().InventoryName;
        }

        void CalculateTotal()
        {
            double sum = 0,price_withComm=0,price_withoutCommission=0;

            for (int i = 0; i < MyBill.BillProductInfoes.Count; i++)
            {
                sum += MyBill.BillProductInfoes.ElementAt(i).LotQuantity * MyBill.BillProductInfoes.ElementAt(i).Product.StockKeepingUnit;
                price_withComm += (MyBill.BillProductInfoes.ElementAt(i).LotQuantity * MyBill.BillProductInfoes.ElementAt(i).Product.PricePerUnit) - (MyBill.BillProductInfoes.ElementAt(i).LotQuantity * MyBill.BillProductInfoes.ElementAt(i).Product.PricePerUnit) * (MyBill.BillProductInfoes.ElementAt(i).ComissionPercentage) / 100;
                price_withoutCommission += (MyBill.BillProductInfoes.ElementAt(i).LotQuantity * MyBill.BillProductInfoes.ElementAt(i).Product.PricePerUnit);
            }
            QuantityKgTxtBox.Text = sum.ToString()+" Kg";
            PriceWOCTextBox.Text = price_withoutCommission.ToString() + " BDT";
            PriceWCtextBox.Text = price_withComm.ToString() + " BDT";
        }

        private bool _flag;
        public bool Flag
        {
            get
            {
                return _flag;
            }

            set
            {
                if (_flag == value)
                {
                    return;
                }

                var oldValue = _flag;
                _flag = value;
            }
        }
       
    }
}
