using System;
using System.Windows;
using System.Windows.Controls;
using TrialBusinessManager.Web;
using TrialBusinessManager.Models;
using System.Linq;
using System.Collections.ObjectModel;
using System.ServiceModel.DomainServices.Client;

namespace TrialBusinessManager.Views.DO
{
    public partial class GenerateProductTransferChildWindow : ChildWindow
    {
        AgroDomainContext _context = new AgroDomainContext();
        TransferIndent TIndent = new TransferIndent();
        bool ComboBoxSelection = false;
        int flag = 0;

        ObservableCollection<ProductTransferDetail> ProductTransferDetailList = new ObservableCollection<ProductTransferDetail>();
        ObservableCollection<LotInfo> LotInfoCollection = new ObservableCollection<LotInfo>();
        BusyWindow Busy = new BusyWindow();
        public GenerateProductTransferChildWindow(TransferIndent MyIdent,AgroDomainContext SentContext)
        {

            InitializeComponent();
            EditBtn.IsEnabled = false;
            OKButton.IsEnabled = false;
            _context = SentContext;
            TIndent = MyIdent;
            comboBox1.ItemsSource = TIndent.TransferIndentDetails;
            dataGrid1.ItemsSource = ProductTransferDetailList;
            TransportTypeComboBox.ItemsSource = ConstantCollections.Transports;
            LoadInventoryProducts();
            LoadTransferIndentProducts();
            ClearContext();
        }

        private void LoadTransferIndentProducts()
        {
            _context.Load(_context.GetTransferIndentDetailsQuery().Where(r => r.TransferIndentId==TIndent.TransferIndentId)).Completed+=(s,e)=>
            {
                flag++;
                if (flag == 2)
                {
                    EditBtn.IsEnabled = true;
                    OKButton.IsEnabled = true;
                }
            };
     
        }
        private void LoadInventoryProducts()
        {
            _context.Load(_context.GetInventoryProductInfoesQuery().Where(r => r.InventoryId == UserInfo.Inventory.InventoryId)).Completed += (s, e) =>
                {
                    flag++;
                    if (flag == 2)
                    {
                        EditBtn.IsEnabled = true;
                        OKButton.IsEnabled = true;
                    }
                };
        }

        private void ClearContext()
        {
            while (_context.ProductTransferDetails.Any(e => e.ProductTransferId < 1))
            {
                _context.ProductTransferDetails.Remove(_context.ProductTransferDetails.Where(e => e.ProductTransferId < 1).First());
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
           
            if (String.IsNullOrEmpty(VehicleNoTextBox.Text) || String.IsNullOrEmpty(EmergencyContactTextBox.Text) || String.IsNullOrEmpty(TransportCostTextBox.Text) || String.IsNullOrEmpty((String)TransportTypeComboBox.SelectedItem))
            {
                MessageBox.Show("Please provide necessary input!!");
                return;

            }
            else
            {
                ProductTransfer NewTransfer = new ProductTransfer();
                NewTransfer.DateOfIssue = DateTime.Now;
                NewTransfer.DateOfRecieve = DateTime.Now;
                NewTransfer.EmergencyContactNo = EmergencyContactTextBox.Text;
                NewTransfer.TransportType = (String)TransportTypeComboBox.SelectedItem;
                NewTransfer.VehicleNo = VehicleNoTextBox.Text;
                NewTransfer.ProductLossCost = 0;
                double temp;
                Double.TryParse(TransportCostTextBox.Text, out temp);
                NewTransfer.TransportCost = temp;

                NewTransfer.ProductTransferCode = "n/a";
                NewTransfer.ProductTransferStatus = "Dispatched";

                NewTransfer.SendFromInventoryId = UserInfo.Inventory.InventoryId;
                NewTransfer.RecievedToInventoryId = TIndent.IssuedFromInventoryId;
                NewTransfer.IssuedByDOId = UserInfo.EmployeeID;
                NewTransfer.RecievedBySICId = -1;
                NewTransfer.TransferIndentId = TIndent.TransferIndentId;
              //  NewTransfer.ProductTransferDetails.

                foreach (ProductTransferDetail x in ProductTransferDetailList)
                {
                    NewTransfer.ProductTransferDetails.Add(x);
                }

                foreach (ProductTransferDetail x in ProductTransferDetailList)
                {
                    
                    InventoryProductInfo update = new InventoryProductInfo();
                    InventoryProductInfo dummy = new InventoryProductInfo();
                    update = _context.InventoryProductInfos.Where(w => w.InventoryId == UserInfo.Inventory.InventoryId && w.Product.ProductId == x.Product.ProductId&&w.LotId==x.LotId).Single();
                    dummy = _context.InventoryProductInfos.Where(w => w.InventoryId == UserInfo.Inventory.InventoryId && w.Product.GroupName == x.Product.GroupName && w.LotId == x.LotId&&w.Product.ProductName=="DUMMY").Single();
                    update.FinishedQuantity -= x.TransferQuantity*x.Product.StockKeepingUnit;
                    dummy.FinishedQuantity -= x.TransferQuantity*x.Product.StockKeepingUnit;

                }
                
                TIndent.Status ="Dispatched";
                _context.ProductTransfers.Add(NewTransfer);
                foreach (TransferIndent x in _context.TransferIndents)
                {
                    if (x.TransferIndentId < 1)
                    {
                        _context.TransferIndents.Remove(x);
                    }
                }


                     Busy.Show();
                     _context.SubmitChanges(OnSubmitCompleted, null);
                     
                    
            }
        }

        void OnSubmitCompleted(SubmitOperation so)
        {
            Busy.Close();
            if (so.HasError)
            {
                MessageBox.Show("Some error occured. This windows will now close. Please again generate the product transfer!");
                this.DialogResult = true;
            }

            else
            {
                MessageBox.Show("Product Transfer successfully submitted. This windows will now close");
                this.DialogResult = true;
            }

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxSelection = true;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBoxSelection == true)
            {
                TransferLotAllocationChildWindow obj = new TransferLotAllocationChildWindow(_context, (TransferIndentDetail)comboBox1.SelectedItem, LotInfoCollection, ProductTransferDetailList);
                obj.Show();
                obj.Closed += new EventHandler(obj_Closed);
            }
        }

        void obj_Closed(object sender, EventArgs e)
        {
            //To remove products where Lotquantity is zero//
            while (LotInfoCollection.Any(f => f.Quantity <= 0))
            {

                LotInfoCollection.Remove(LotInfoCollection.Where(f => f.Quantity <= 0).First());


            }
            //To delete previous occurances of the product,to overrite the lot allocation//
            TransferIndentDetail SelectedDetails = (TransferIndentDetail)comboBox1.SelectedItem;
            while (ProductTransferDetailList.Any(r => r.Product.ProductId == SelectedDetails.Product.ProductId))
            {

                ProductTransferDetail del = new ProductTransferDetail();
                del = (ProductTransferDetail)ProductTransferDetailList.Where(t => t.Product.ProductId == SelectedDetails.Product.ProductId).FirstOrDefault();
                ProductTransferDetailList.Remove(del);



            }
         
            if (LotInfoCollection.Count() > 0)
            {

                for (int i = 0; i < LotInfoCollection.Count(); i++)
                {

                    ProductTransferDetail PTDetail = new ProductTransferDetail();
                    PTDetail.Product = SelectedDetails.Product;

                    PTDetail.TransferQuantity = LotInfoCollection.ElementAt(i).Quantity;
                    PTDetail.LotId = LotInfoCollection.ElementAt(i).LotId;
                    PTDetail.RecievedQuantity = 0;
                 ////   PTDetail.PurchasePricePerUnit = 0;
                    PTDetail.PurchasePricePerUnit =(Double)_context.InventoryProductInfos.Where(r => r.ProductId == PTDetail.Product.ProductId && r.LotId == PTDetail.LotId&&r.InventoryId==UserInfo.Inventory.InventoryId).Single().UnitLotCost;

                    if (PTDetail.TransferQuantity > 0)
                    {
                        ProductTransferDetailList.Add(PTDetail);
                    }

                }

            }

            LotInfoCollection.Clear();
        }

        private void RejectBtn_Click(object sender, RoutedEventArgs e)
        {
            TIndent.Status = "Rejected";
            foreach (TransferIndent x in _context.TransferIndents)
            {
                if (x.TransferIndentId < 1)
                {
                    _context.TransferIndents.Remove(x);
                }
            }
            Busy.Show();

            _context.SubmitChanges().Completed += (s, args) =>
            {
                Busy.Close();
                MessageBox.Show("Product Transfer successfully rejected. This windows will now close.");
                this.DialogResult = true;

            };

        }
    }
}

