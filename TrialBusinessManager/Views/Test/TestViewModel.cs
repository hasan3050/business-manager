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
using System.Collections.Generic;
using TrialBusinessManager.Web;
using System.Linq;

namespace TrialBusinessManager.Test
{
    public class TestViewModel:ViewModelBase
    {
        int DealerId=141;                                                  //dealer for test
        int inventoryId=13;                                                //test inventory
        int[] products = new int[5]{51,55,99,103,398};                     //set of products
        int IndentId=100;
        int ProductId;
        int DOId=105;
        Random rand = new Random();
        List<int> result = new List<int>();
        Bill TestBill;
        Indent TestIndent;
        List<InventoryProductInfo> SelectedInfoes = new List<InventoryProductInfo>();    //stores the infoes for the current product
        HashSet<int> check = new HashSet<int>();
        AgroDomainContext _context = new AgroDomainContext();

        void loadInventory()
        {
            _context.Load(_context.GetInventoryProductInfoesByInventoryIdQuery(inventoryId)).Completed += (s, e) =>
                {
                };
        }

        void storeInfoes(int ProductId)
        {
            SelectedInfoes = _context.InventoryProductInfos.Where(e => e.ProductId.Equals(ProductId)).ToList();
        }

        void initialzeBill()
        {
            TestBill = new Bill();
            TestBill.DateOfIssue = DateTime.Now;
            TestBill.DealerId = DealerId;
            TestBill.DispatchedById = DOId;
            TestBill.BillStatus = "Test Bill";
            TestBill.EmergencyContactNo = "0000";
            TestBill.IndentId = IndentId;
            TestBill.PaymentDeadline = DateTime.Now;
        }

        

        void addBillInfoes()
        {
            Random myRand = new Random();
            for (int i = 0; i < SelectedInfoes.Count; i++)
            {
                BillProductInfo info = new BillProductInfo();
                info.ProductPrice = SelectedInfoes.ElementAt(i).Product.PricePerUnit;
                info.LotId = SelectedInfoes.ElementAt(i).LotId;
                info.LotQuantity = myRand.Next(1, (int)(SelectedInfoes.ElementAt(i).FinishedQuantity/2));
                info.Product = SelectedInfoes.ElementAt(i).Product;
                TestBill.BillProductInfoes.Add(info);    
            }

            SelectedInfoes.Clear();
        }

        void generateBill()
        {
            initialzeBill();
            pickProducts();
            for (int i = 0; i < result.Count; i++)
            {
                ProductId=products[result.ElementAt(i)];
                storeInfoes(ProductId);
                addBillInfoes();
            }
            _context.Bills.Add(TestBill);
        }

        void pickProducts()
        {
            for (Int32 i = 0; i < 3; i++)
            {
                int curValue = rand.Next(0,4);
                while (check.Contains(curValue))
                {
                    curValue = rand.Next(0,4);
                }
                result.Add(curValue);
                check.Add(curValue);
            }
        }
        
    }
}
