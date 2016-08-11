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
using System.Windows.Navigation;
using TrialBusinessManager.Web;
using TrialBusinessManager.Views;
using System.ServiceModel.DomainServices.Client;

namespace TrialBusinessManager.Test
{
    public partial class TestBill : Page
    {
        int DealerId = 141;                                                       //dealer for test
        int inventoryId = 13;                                                     //test inventory
        int[] products = new int[5] { 51, 55, 99, 103, 398 };                     //set of products
        int IndentId = 100;
        int ProductId;
        int DOId = 105;
        Random rand = new Random();
        List<int> result = new List<int>();
        Bill TBill;
        Indent TestIndent;
        BusyWindow busy = new BusyWindow();
        List<InventoryProductInfo> SelectedInfoes = new List<InventoryProductInfo>();    //stores the infoes for the current product
        HashSet<int> check = new HashSet<int>();
        AgroDomainContext _context = new AgroDomainContext();

        public TestBill()
        {
            InitializeComponent();
            loadInventory();
        }

        void loadInventory()
        {
            busy.Show();
            _context.Load(_context.GetInventoryProductInfoesByInventoryIdQuery(inventoryId)).Completed += (s, e) =>
            {
                busy.Close();
            };
        }

        void storeInfoes(int ProductId)
        {
            SelectedInfoes = _context.InventoryProductInfos.Where(e => e.ProductId.Equals(ProductId)).ToList();
        }

        void initialzeBill()
        {
            TBill = new Bill();
            TBill.DateOfIssue = DateTime.Now;
            TBill.DealerId = DealerId;
            TBill.DispatchedById = DOId;
            TBill.BillStatus = "Test Bill";
            TBill.EmergencyContactNo = "0000";
            TBill.IndentId = IndentId;
            TBill.TransportType = "test";
            TBill.TransportCost = 12;
            TBill.TotalProductCost = 0;
            TBill.TotalPaid = 0;
            TBill.VehicleNo = "12";
            TBill.PaymentDeadline = DateTime.Now.AddDays(7);
        }



        void addBillInfoes()
        {
            Random myRand = new Random();
            for (int i = 0; i < SelectedInfoes.Count; i++)
            {
                BillProductInfo info = new BillProductInfo();
                info.ProductPrice = SelectedInfoes.ElementAt(i).Product.PricePerUnit;
                info.LotId = SelectedInfoes.ElementAt(i).LotId;
                info.LotQuantity = myRand.Next(1, (int)(SelectedInfoes.ElementAt(i).FinishedQuantity / 2));
                info.Product = SelectedInfoes.ElementAt(i).Product;
                TBill.BillProductInfoes.Add(info);
            }

            SelectedInfoes.Clear();
        }

        void generateBill()
        {
            initialzeBill();
            pickProducts();
            for (int i = 0; i < result.Count; i++)
            {
                ProductId = products[result.ElementAt(i)];
                storeInfoes(ProductId);
                addBillInfoes();
            }
            _context.Bills.Add(TBill);
            busy.Show();
            _context.SubmitChanges(OnSubmitCompleted, null);
        }

        void OnSubmitCompleted(SubmitOperation so)
        {
            busy.Close();
            if (so.HasError)
            {
                MessageBox.Show(so.Error.ToString());
               // this.DialogResult = true;
            }

            else
            {
                MessageBox.Show("Done!!");
                //this.DialogResult = true;
            }

        }


        void pickProducts()
        {
            for (Int32 i = 0; i < 3; i++)
            {
                int curValue = rand.Next(0, 4);
                while (check.Contains(curValue))
                {
                    curValue = rand.Next(0, 4);
                }
                result.Add(curValue);
                check.Add(curValue);
            }
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Test_btn_Click(object sender, RoutedEventArgs e)
        {
            generateBill();
        }

    }
}
