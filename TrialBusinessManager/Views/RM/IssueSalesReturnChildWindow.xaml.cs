using System;
using System.Net;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight;
using TrialBusinessManager.Web;
using GalaSoft.MvvmLight.Command;
using System.ServiceModel.DomainServices.Client;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using TrialBusinessManager.Models;
using TrialBusinessManager.Views;

namespace TrialBusinessManager.Views.RM
{
    public partial class IssueSalesReturnChildWindow : ChildWindow
    {
        AgroDomainContext _context = new AgroDomainContext();
        Dealer SelectedDealer = new Dealer();
        int BillandLotSelectionFlag = 0; //To check if both bill and lot is seleted before adding to datagrid//
        BusyWindow Busy = new BusyWindow();

        ObservableCollection<SalesReturnInfo> ReturnInfoCollection = new ObservableCollection<SalesReturnInfo>();
        ObservableCollection<String> LotCollection = new ObservableCollection<string>();
        ObservableCollection<Bill> BillCollection = new ObservableCollection<Bill>();
        ObservableCollection<BillProductInfo> BillProductInfoCollection = new ObservableCollection<BillProductInfo>();
        ObservableCollection<SalesReturnTracker> TrackerList = new ObservableCollection<SalesReturnTracker>();

        int Flag = 0;
        int QueryCount=6;
        bool InventorySelected = false;

        public IssueSalesReturnChildWindow(AgroDomainContext sentContext,Dealer sentDealer)
        {
            InitializeComponent();
            this._context = sentContext;
            SelectedDealer = sentDealer;
            OKButton.IsEnabled = false;
            


            _context.BillProductInfos.Clear();
            _context.Bills.Clear();
           
            _context.SalesReturnInfos.Clear();
            _context.SalesReturns.Clear();
       
            _context.TransportationLosses.Clear();
            _context.DueInfos.Clear();
            
            ReturnInfoCollection.Clear();
            BillCollection.Clear();

            //LoadDealer();
            LoadBillProducts();
            LoadTransprationLosses();
            LoadSalesReturns();
            LoadInventories();
            LoadDueInfoes();
          
           
            DealerNameTxtBox.Text = SelectedDealer.Name;

            DealerShopTextBox.Text = SelectedDealer.CompanyName;
            autoCompleteBox1.ItemsSource = BillProductInfoCollection;
           
            dataGrid1.ItemsSource = ReturnInfoCollection;
         
            BillcomboBox.ItemsSource = BillCollection;

        }


        void LoadDealer()
        {

            _context.Load(_context.GetDealersQuery().Where(e=>e.DealerId==SelectedDealer.DealerId)).Completed += (s, e) =>
            {
                Flag++;
                if (Flag == QueryCount)
                    OKButton.IsEnabled = true;
                
            };
        }
        void LoadInventories()
        {
           
            _context.Load(_context.GetInventoriesQuery()).Completed += (s, e) =>
            {
                Flag++;
                if (Flag == QueryCount)
                    OKButton.IsEnabled = true;
                InventorycomboBox.ItemsSource = _context.Inventories; 
            };
        }

        void LoadTransprationLosses()
        {
            _context.Load(_context.GetTransportationLossesQuery().Where(e=>e.Bill.DealerId==SelectedDealer.DealerId)).Completed+=new EventHandler(IssueSalesReturnChildWindow_Completed);

        }

        void LoadSalesReturns()
        {            
            _context.Load(_context.GetSalesReturnInfoesQuery().Where(e=>e.Bill.DealerId==SelectedDealer.DealerId)).Completed+=new EventHandler(IssueSalesReturnChildWindow_Completed);
        
        }

        void LoadDueInfoes()
        {
            _context.Load(_context.GetDueInfoesQuery().Where(e => e.DealerId == SelectedDealer.DealerId)).Completed += new EventHandler(IssueSalesReturnChildWindow_Completed);

        }

        void LoadBillProducts()
        {

            _context.Load(_context.GetBillsQuery().Where(e => e.Dealer.DealerId == SelectedDealer.DealerId)).Completed += new EventHandler(IssueSalesReturnChildWindow_Completed);

            _context.Load(_context.GetBillProductInfoesQuery().Where(r => r.Bill.DealerId == SelectedDealer.DealerId)).Completed += new EventHandler(BillLoad_Completed);     //This will not be required when Product is loaded in cache//
          
        
        }

        

        void IssueSalesReturnChildWindow_Completed(object sender, EventArgs e)
        {
            Flag++;
            if (Flag == QueryCount)
            {
                OKButton.IsEnabled = true;
            
            }

        }

        void BillLoad_Completed(object sender, EventArgs e)
        {
            foreach (BillProductInfo x in _context.BillProductInfos)
            {
                if (!BillProductInfoCollection.Any(r => r.Product == x.Product))
                {
                    BillProductInfoCollection.Add(x);
                }
            }
            
            Flag++;
            if (Flag == QueryCount)
                OKButton.IsEnabled = true;

        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
          
            try
            {
                
                if (InventorySelected == false)
                {
                    MessageBox.Show("Please select an inventory to forward!");
                    return;
                }
                if (ReturnInfoCollection.Count == 0)
                {
                    MessageBox.Show("Please select a product to issue Sales Return!!");
                    return;

                }
                foreach (SalesReturnInfo x in ReturnInfoCollection)
                {
                   
                    if (x.PlacedQuantity <= 0)
                    {
                        MessageBox.Show("Quantity can not be zero or negative!!");
                        return;
                    }
                    SalesReturnTracker MyTracker = new SalesReturnTracker();
                    MyTracker = TrackerList.Where(r => r.BillId == x.BillId && r.ProductId == x.ProductId).Single();
                  //  MessageBox.Show("Bill:"+MyTracker.BillAmount.ToString()+"T.L:"+MyTracker.TLossAmount.ToString()+"S.R:"+MyTracker.SalesReturnAmount.ToString());
                    if (x.PlacedQuantity > MyTracker.BillAmount - (MyTracker.SalesReturnAmount + MyTracker.TLossAmount))
                    {
                        MessageBox.Show("The return amount of "+x.Product.ProductName + " is currently invalid for sales return for selected Bill!!");
                        return;
                    }

                    if (isDueValid(x) == false)
                    {
                        return; // TO check if already paid portion is returned//
                    }

                }
                
                _context.SalesReturnInfos.Clear();
                _context.SalesReturns.Clear();
               
                Double TotalPlacedAmount = 0;
                SalesReturn mySalesReturn = new SalesReturn();
                Inventory SelectedInventory = new Inventory();
                SelectedInventory = (Inventory)InventorycomboBox.SelectedItem;
                mySalesReturn.DateOfIssue = DateTime.Now;
                mySalesReturn.DealerId = SelectedDealer.DealerId;
                mySalesReturn.RMId = UserInfo.EmployeeID;
                mySalesReturn.SalesReturnCode = DateTime.Now.ToString();
                mySalesReturn.Status = "Placed";
                mySalesReturn.TotalAcceptedAmount = 0;
                mySalesReturn.SendToInventoryId = SelectedInventory.InventoryId;
                mySalesReturn.TotalPlacedAmount = TotalPlacedAmount;
             
                foreach (SalesReturnInfo x in ReturnInfoCollection)
                {
                  //  MessageBox.Show(x.Bill.DealerId.ToString() + "," + SelectedDealer.DealerId.ToString());
                    if (x.Bill.DealerId != SelectedDealer.DealerId)
                    {
                        MessageBox.Show("Some error occured,this window will close now. please refresh the page and try again");
                        this.DialogResult = true;
                    }

                    

                    mySalesReturn.SalesReturnInfoes.Add(x);
                    TotalPlacedAmount += x.PlacedQuantity * x.ProductPrice;
                }
                //_context.SalesReturns.Clear();
                _context.SalesReturns.Add(mySalesReturn);
                //MessageBox.Show(_context.SalesReturns.Count.ToString());
                Busy.Show();
                _context.SubmitChanges().Completed += new EventHandler(IssueSalesReturnChildWindowCompleted);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
               // MessageBox.Show("Some error occured due to connectivity problem, this window will now close. Please issue the sales return again!");
                this.DialogResult = true;
            }
           
        }

        void IssueSalesReturnChildWindowCompleted(object sender, EventArgs e)
        {
            MessageBox.Show("The Sales return is successfully issued.");
            Busy.Close();
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            
        }
        //When a product will be selected from the auto complete box..Bills having that product will load in the bill combo box//
        private void autoCompleteBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
               
                BillProductInfo selectedProduct = (BillProductInfo)autoCompleteBox1.SelectedItem;

                BillCollection.Clear();
                LotCollection.Clear();

                foreach (BillProductInfo x in _context.BillProductInfos)
                {
                    if (x.Bill.DealerId == SelectedDealer.DealerId)
                    {
                        if (x.Product == selectedProduct.Product)
                            if (!BillCollection.Any(r => r.BillId == x.BillId))
                            {
                                if (x.Bill.DealerId == SelectedDealer.DealerId)
                                    BillCollection.Add(x.Bill);
                            }
                    }
                    else 
                    {
                        MessageBox.Show("Some error occured,this window will close now. please refresh and try again");
                        this.DialogResult = true;
                    }
                }
            }
             catch (Exception ex)
            {
                    return;

            }
          
        }
        //When a product is selected in the autocomeplete box and bill of that product is selected in bill combo box then lot collection will be pupulated with the lots of selected bills and products//
        private void BillcomboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                BillandLotSelectionFlag = 0;
                Bill selectedBill = (Bill)BillcomboBox.SelectedItem;
                  
                BillProductInfo selectedProduct = (BillProductInfo)autoCompleteBox1.SelectedItem;

                LotCollection.Clear();
               // BillCollection.Clear();
                LotCollection.Add("Return");
                foreach (BillProductInfo x in _context.BillProductInfos)
                {
                    if(x.BillId==selectedBill.BillId&&x.ProductId==selectedProduct.ProductId)
                           LotCollection.Add(x.LotId);
                }

                LotcomboBox.ItemsSource = LotCollection;
                BillandLotSelectionFlag = 1;
            }
            catch (Exception ex)
            {
              
                return;
            }
            
        }

        private void button1_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (BillandLotSelectionFlag == 2)
                {
                   
                    SalesReturnInfo selectedReturn = new SalesReturnInfo();
                    BillProductInfo selected = new BillProductInfo();

                    selected = (BillProductInfo)autoCompleteBox1.SelectedItem;
                   // MessageBox.Show(selected.ProductPrice.ToString()+" "+selected.BillId.ToString());
                    selectedReturn.Product = selected.Product;
                    selectedReturn.LotId = (string)LotcomboBox.SelectedItem;
                    selectedReturn.PlacedQuantity = 0;
                    selectedReturn.AcceptedQuantity = 0;
                    selectedReturn.Bill = (Bill)BillcomboBox.SelectedItem;
                    //MessageBox.Show(((Bill)BillcomboBox.SelectedItem).BillCode);
                    selectedReturn.ProductPrice = (Double)_context.BillProductInfos.Where(t => t.BillId == selectedReturn.Bill.BillId && t.ProductId==selectedReturn.ProductId).First().ProductPrice; //Not Subtracting the price with the bill commission percentage//
                   
                    if (ReturnInfoCollection.Any(r => r.BillId == selectedReturn.BillId && r.Product.ProductId == selectedReturn.Product.ProductId &&r.LotId==selectedReturn.LotId))
                    {
                        return;
                    }

                    ReturnInfoCollection.Add(selectedReturn);
                    ///////////////////////For Tracking validation///////////////////
                    SalesReturnTracker Tracker = new SalesReturnTracker();
                    Tracker.ProductId = selected.ProductId;
                    Tracker.BillId = selectedReturn.BillId;
                    Tracker.BillAmount=0;

                    foreach (BillProductInfo x in _context.BillProductInfos.Where(r => r.BillId == Tracker.BillId))
                    {
                        if (x.ProductId == Tracker.ProductId)
                        {
                            Tracker.BillAmount += x.LotQuantity;
                        }
                    }
                    Tracker.TLossAmount=0;
                    foreach (TransportationLoss x in _context.TransportationLosses.Where(r => r.BillId == Tracker.BillId))
                    {
                        if (x.ProductId == Tracker.ProductId)
                        {
                            Tracker.TLossAmount += x.LossQuantity;
                        
                        }

                    }
                    Tracker.SalesReturnAmount = 0;
                    foreach (SalesReturnInfo x in _context.SalesReturnInfos.Where(r => r.BillId == Tracker.BillId&&r.ProductId==Tracker.ProductId&&r.SalesReturnId>0&&r.ProductId>0&&r.BillId>0))
                    {
                        MessageBox.Show(x.SalesReturn.SalesReturnId.ToString()+".."+x.SalesReturn.DateOfIssue.ToString()+".."+x.SalesReturn.Status);
                        if (x.ProductId == Tracker.ProductId)
                        {
                            
                            if (x.SalesReturn.Status == "Placed" || x.SalesReturn.Status == "Verified by NSM")
                            {
                                MessageBox.Show("Placed: "+x.PlacedQuantity.ToString());
                                Tracker.SalesReturnAmount+=x.PlacedQuantity;
                            }
                            if (x.SalesReturn.Status == "Approved")
                            {
                                MessageBox.Show("Approved "+x.AcceptedQuantity.ToString());
                                Tracker.SalesReturnAmount += x.AcceptedQuantity;
                            }
                           
                        
                        }
                           

                    }
                    TrackerList.Add(Tracker);
                    MessageBox.Show("Product amount on Bill: "+Tracker.BillAmount.ToString()+" Pc"+"\n"+"Transportation Loss Amount: "+Tracker.TLossAmount.ToString()+" Pc"+"\n"+"Issued sales return amount: "+Tracker.SalesReturnAmount.ToString()+" Pc");
                }


            }
            catch (Exception x)
            {
                return;
            }

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SalesReturnInfo delete = new SalesReturnInfo();
                delete = (SalesReturnInfo)dataGrid1.SelectedItem;
                ReturnInfoCollection.Remove((SalesReturnInfo)dataGrid1.SelectedItem);
                
                TrackerList.Remove(TrackerList.Where(r=>r.BillId==delete.BillId&&r.ProductId==delete.ProductId).Single());
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void LotcomboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BillandLotSelectionFlag == 1)
                BillandLotSelectionFlag = 2;
        }

        bool isDueValid(SalesReturnInfo p)
        {
            DueInfo due = new DueInfo();
            due = _context.DueInfos.Where(e => e.BillId == p.Bill.BillId & e.ProductId == p.Product.ProductId).First();
         //   MessageBox.Show(due.ProductCost.ToString()+" "+due.Paid.ToString());
            if ((due.ProductCost - (due.Paid+due.SalesReturn)) >= (p.PlacedQuantity * p.ProductPrice))
                return true;

            MessageBox.Show("Selected amount for bill: " +p.Bill.BillCode+" is invalid!");
            return false;
        }

        class SalesReturnTracker
        {
            public long BillId;
            public long ProductId;
            public double BillAmount;
            public double TLossAmount;
            public double SalesReturnAmount;
        
        }

        private void InventorycomboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InventorySelected = true;
        }

        
    }
}

