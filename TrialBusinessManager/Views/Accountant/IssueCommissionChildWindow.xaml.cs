using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.ServiceModel.DomainServices.Client;
using System.Windows.Input;
using TrialBusinessManager.Models;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using TrialBusinessManager.Web;

namespace TrialBusinessManager.Views.Accountant
{
    public partial class IssueCommissionChildWindow : ChildWindow
    {
        AgroDomainContext _context = new AgroDomainContext();
        Dealer MyDealer = new Dealer();
        public List<DueInfo> DueCollection { get { return _context.DueInfos.Where(e => e.DealerId == MyDealer.DealerId).ToList(); } }
        public List<Bill> BillCollection { get { return _context.Bills.Where(e => e.DealerId == MyDealer.DealerId).ToList(); } }
        public EntitySet<Product> ProductCollection { get { return _context.Products; } }
        private List<BillProductDetail> BillProductCollection;
        long SelectedBillId = 0;
        BusyWindow Busy = new BusyWindow();
        public IssueCommissionChildWindow(Dealer SelectedDealer)
        {
            InitializeComponent();
            MyDealer = SelectedDealer;
            LoadDealer();
            LoadDueCommission();
          
        }

        private void LoadDealer()
        {
            _context.Load(_context.GetDealerByIDQuery(MyDealer.DealerId));
        }

        private void LoadDueCommission()
        {
            _context.Load(_context.GetDueInfoForBillPaymentsQuery().Where(r => r.DealerId == MyDealer.DealerId)).Completed += new EventHandler(IssueCommissionChildWindow_Completed);     
        }

        private void GenerateBillProductCollection()
        {
            BillProductCollection = new List<BillProductDetail>();
            BillProductCollection.Clear();
            foreach (DueInfo nowDue in DueCollection.Where(e=>e.BillId==SelectedBillId))
            {
                BillProductDetail temp = new BillProductDetail();

                temp.GenerateBillDetail(nowDue);

                if (nowDue.PaymentDeadLine.Date < DateTime.Now.Date && temp.Payable >= 0)
                {
                    temp.Commission = 0;
                    temp.NetPrice = temp.TotalPrice - temp.Loss - temp.SalesReturn;
                    temp.Payable = temp.NetPrice - temp.Paid;
                }

                BillProductCollection.Add(temp);
            }

            dataGrid1.ItemsSource = BillProductCollection;
       
        }

        void IssueCommissionChildWindow_Completed(object sender, EventArgs e)
        {
            autoCompleteBox1.ItemsSource = _context.Bills;
        }

        private void Submit()
        {
            foreach (BillProductDetail x in BillProductCollection)
            {
                if (x.Amount > 0)
                {
                    _context.DueInfos.Where(e => e.BillId == x.Bill.BillId && e.ProductId == x.Product.ProductId).Single().CommissionOnBenefit += x.Amount;
                    _context.DueInfos.Where(e => e.BillId == x.Bill.BillId && e.ProductId == x.Product.ProductId).Single().ProductCost-=x.Amount;
                    _context.Dealers.Where(e => e.DealerId == MyDealer.DealerId).Single().TotalDue -= x.Amount;
                    _context.Ledgers.Add(GenerateLedger(x,MyDealer));
                }
            
            }
            Busy.Show();
            _context.SubmitChanges().Completed += new EventHandler(Submit_completed);
        
        }

        Ledger GenerateLedger(BillProductDetail x,Dealer dealer)
        {
            Ledger ledger = new Ledger();
            ledger.CreditAmount = x.Amount;
            ledger.Date = DateTime.Now;
            ledger.MemoNo = "BI" + x.Bill.BillId.ToString();
            ledger.Method = "Commission On Benefit";
            ledger.ProductId = x.Product.ProductId;
            ledger.ProductQuantity = 0;
            ledger.IsDealerOrEmployee = true;
            ledger.IsDebitOrCredit = false;
            ledger.PartyId = dealer.DealerId;
            //ledger.

            return ledger;
        }

        void Submit_completed(object sender, EventArgs e)
        {
            Busy.Close();
            MessageBox.Show("Commission has been successfully added!!");
            this.DialogResult = true;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {

            Submit();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
           try
            {
                Bill SelectedDetail = new Bill();
                SelectedDetail = (Bill)autoCompleteBox1.SelectedItem;
                if (SelectedDetail.BillId > 0)
                {
                    SelectedBillId = SelectedDetail.BillId;
                    GenerateBillProductCollection();
                }

            }
            catch (Exception ex)
            {
                
            }

        }

       
    }
}

