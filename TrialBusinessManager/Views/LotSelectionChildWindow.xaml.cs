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
using TrialBusinessManager.Models;
using GalaSoft.MvvmLight.Messaging;

namespace TrialBusinessManager.Views
{
    public partial class LotSelectionChildWindow : ChildWindow 
    {
        AgroDomainContext _context = new AgroDomainContext();
        RequisitionProductInfo reqProduct = new RequisitionProductInfo();
        ObservableCollection<LotInfo> LotInfoCollection;
        ObservableCollection<RequisitionProductInfo> RequisitionProductBalaceCollection = new ObservableCollection<RequisitionProductInfo>();
        public LotSelectionChildWindow(AgroDomainContext myContext, RequisitionProductInfo myProduct, ObservableCollection<LotInfo> LotCollection, ObservableCollection<RequisitionProductInfo> RequisitionProductCollection)
        {

            InitializeComponent();
            
            LotInfoCollection=new ObservableCollection<LotInfo>();
            LotInfoCollection = LotCollection;
            _context = myContext;
            reqProduct = myProduct;
            foreach (RequisitionProductInfo x in RequisitionProductCollection.Where(e => e.Product.ProductName != myProduct.Product.ProductName))
            {
               // MessageBox.Show(x.Product.ProductName+" "+x.LotQuantity.ToString());
                RequisitionProductBalaceCollection.Add(x);
            }
            LoadCollection();
            dataGrid1.ItemsSource = LotInfoCollection;
            reqAmountTxtBox.Text = reqProduct.PlacedQuantity.ToString();
            this.DataContext = this;
           // MessageBox.Show(myProduct.Product.ProductName);
          
        
        }

        private void LoadCollection()
        {
            foreach (InventoryProductInfo x in _context.InventoryProductInfos.Where(e=>e.Product.GroupName==reqProduct.Product.GroupName&&e.Product.ProductName=="DUMMY"))
            {
                LotInfo lotInfo = new LotInfo();
                Double SelectedQuantity = 0;
                lotInfo.LotId = x.LotId;
                lotInfo.Quantity = 0;
                if (RequisitionProductBalaceCollection.Any(r => r.Product.GroupName == x.Product.GroupName && r.LotId == x.LotId))
                {
                   foreach (RequisitionProductInfo ReqProduct in RequisitionProductBalaceCollection.Where(r => r.Product.GroupName == x.Product.GroupName && r.LotId == x.LotId))
                   {
                      // if(ReqProduct.ProductId!=reqProduct.ProductId)
                            SelectedQuantity += ReqProduct.LotQuantity;
                   }
                }

                lotInfo.AvailableQuantity = x.UnfinishedQuantity-SelectedQuantity;
                lotInfo.LotProduct = x.Product;
                LotInfoCollection.Add(lotInfo);
             
            }
        
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            LotInfoCollection.Clear();
            this.DialogResult = false;
        }

      

        private void ChildWindow_Closed(object sender, EventArgs e)
        {

        }

        private void dataGrid1_CellEditEnded(object sender, DataGridCellEditEndedEventArgs e)
        {
            if (LotInfoCollection.Count > 0)
            {
                double total=0;
                for (int i = 0; i < LotInfoCollection.Count(); i++)
                {
                    
                    if (LotInfoCollection.ElementAt(i).Quantity < 0)
                    {
                        MessageBox.Show("Quantity can not be negative!");
                        LotInfoCollection.ElementAt(i).Quantity = 0;
                        return;
                    }

                    if (LotInfoCollection.ElementAt(i).AvailableQuantity < LotInfoCollection.ElementAt(i).Quantity)
                    {
                        MessageBox.Show("Quantity can not exceed available quantity!");
                        LotInfoCollection.ElementAt(i).Quantity = 0;
                        return;
                    }

                    total += LotInfoCollection.ElementAt(i).Quantity;
                
                }
                if (reqProduct.PlacedQuantity < total)
                {
                    var selected = (LotInfo)dataGrid1.SelectedItem;
                    LotInfoCollection.Where(r => r.LotId == selected.LotId).First().Quantity = 0;
                    MessageBox.Show("Quantity can not exceed requested quantity!");
                    return;
                       
                }
                totaltextBox.Text = total.ToString();
            
            }

        }

        

        
        
    }
}

