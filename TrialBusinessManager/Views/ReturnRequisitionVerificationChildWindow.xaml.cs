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

namespace TrialBusinessManager.Views
{
    public partial class ReturnRequisitionVerificationChildWindow : ChildWindow
    {
        long RequisitionId;
        AgroDomainContext _context = new AgroDomainContext();
        Requisition updateRequisition = new Requisition();
        ObservableCollection<RequisitionProductInfo> ProductList = new ObservableCollection<RequisitionProductInfo>();
        ObservableCollection<RequisitionPackageInfo> PackageList = new ObservableCollection<RequisitionPackageInfo>();
        int Flag = 0;
        int QueryCount = 5;
        BusyWindow Busy = new BusyWindow();
        public ReturnRequisitionVerificationChildWindow(long id)
        {
            InitializeComponent();
            OKButton.IsEnabled = false;
            CancelButton.IsEnabled = false;
            RequisitionId =id;

           // busyIndicator1.IsBusy = true;

            _context.Load(_context.GetRequisitionsQuery().Where(e=>e.RequisitionId==id)).Completed+=new EventHandler(ReturnRequisitionVerificationChildWindowCompleted);
            _context.Load(_context.GetRequisitionProductInfoesQuery().Where(e=>e.RequisitionId==id)).Completed+=new EventHandler(ReturnRequisitionVerificationProductCompleted);
            _context.Load(_context.GetRequisitionPackageInfoesQuery().Where(e=>e.RequisitionId==id)).Completed+=new EventHandler(ReturnRequisitionVerificationPackageCompleted);
            _context.Load(_context.GetInventoryProductInfoesQuery().Where(e => e.InventoryId == UserInfo.Inventory.InventoryId)).Completed += (s,e) => 
            {
                Flag++;
                if (Flag == 5)
                {
                    OKButton.IsEnabled = true;
                    CancelButton.IsEnabled = true;
                }
            };
            _context.Load(_context.GetInventoryPackageInfoesQuery().Where(e => e.InventoryId == UserInfo.Inventory.InventoryId)).Completed += (s, e) => {
                Flag++;
                if (Flag == 5)
                {
                    OKButton.IsEnabled = true;
                    CancelButton.IsEnabled = true;
                }
            };

        }

        void ReturnRequisitionVerificationProductCompleted(object sender, EventArgs e)
        {
            foreach (RequisitionProductInfo x in _context.RequisitionProductInfos)
            {
                ProductList.Add(x);

            }
            dataGrid1.ItemsSource = ProductList;
          
            Flag++;
            if (Flag == 5)
            {
                OKButton.IsEnabled = true;
                CancelButton.IsEnabled = true;
            }
        }

        void ReturnRequisitionVerificationPackageCompleted(object sender, EventArgs e)
        {
            foreach (RequisitionPackageInfo x in _context.RequisitionPackageInfos)
            {
                PackageList.Add(x);
            }
           dataGrid2.ItemsSource = PackageList;
           
            Flag++;
           if (Flag == 5)
           {
               OKButton.IsEnabled = true;
               CancelButton.IsEnabled = true;
           }
        }

        void ReturnRequisitionVerificationChildWindowCompleted(object sender, EventArgs e)
        {
            updateRequisition = _context.Requisitions.Where(r=>r.RequisitionId==RequisitionId).First();
            typeTextBox.Text = updateRequisition.RequisitionType;
           
            Flag++;
            if (Flag == 5)
            {
                OKButton.IsEnabled = true;
                CancelButton.IsEnabled = true;
            }
        }

        private bool Validate()
        {
            foreach (RequisitionProductInfo x in ProductList)
            {
                InventoryProductInfo up = new InventoryProductInfo();
                InventoryProductInfo dummy = new InventoryProductInfo();

                up = _context.InventoryProductInfos.Where(f => f.ProductId == x.Product.ProductId && f.LotId == x.LotId).First();
                dummy = _context.InventoryProductInfos.Where(f => f.Product.GroupName == x.Product.GroupName && f.Product.ProductName == "DUMMY" && f.LotId == x.LotId).First();

                if (updateRequisition.RequisitionType == ConstantCollections.RequisitionTypeName.ElementAt(1))
                {
                    if (up.OnProcessingQuantity < x.LotQuantity)
                    {
                        MessageBox.Show("Not enough " + x.Product.ProductName + " availabe to return!");
                        return false;
                    }
                    if (x.LotQuantity < 0)
                    {
                        MessageBox.Show("Quantity can not be negative!");
                        return false;

                    }
                  

                }

                else
                {
                    if (up.FinishedQuantity < x.LotQuantity)
                    {
                        MessageBox.Show("Not enough " + x.Product.ProductName + " availabe to return!");
                        return false;
                    }
                    if (x.LotQuantity < 0)
                    {
                        MessageBox.Show("Quantity can not be negative!");
                        return false;

                    }
                 
                }

            }

            foreach (RequisitionPackageInfo x in PackageList)
            {
                InventoryPackageInfo up = new InventoryPackageInfo();
                up = _context.InventoryPackageInfos.Where(f => f.PackageId == x.Package.PackageId && f.InventoryId == UserInfo.Inventory.InventoryId).First();

                if (updateRequisition.RequisitionType == ConstantCollections.RequisitionTypeName.ElementAt(1))
                {
                    if (up.OnProcessingQuantity < x.AcceptedQuantity)
                    {
                        MessageBox.Show("Not enough " + x.Package.PackageName + " availabe to return!");
                        return false;
                    }
                    if (x.AcceptedQuantity < 0)
                    {
                        MessageBox.Show("Quantity can not be negative!");
                        return false;

                    }
                 
                }

                else
                {
                    if (up.FinishedQuantity < x.AcceptedQuantity)
                    {
                        MessageBox.Show("Not enough " + x.Package.PackageName + " availabe to return!");
                        return false;
                    }
                    if (x.AcceptedQuantity < 0)
                    {
                        MessageBox.Show("Quantity can not be negative!");
                        return false;

                    }
                   }

            }

            return true;
       }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (Validate() == false)
                return;
            foreach (RequisitionProductInfo x in ProductList)
            {
                InventoryProductInfo up = new InventoryProductInfo();
                InventoryProductInfo dummy = new InventoryProductInfo();
                 
                up = _context.InventoryProductInfos.Where(f=>f.ProductId==x.Product.ProductId&&f.LotId==x.LotId).First();
                dummy = _context.InventoryProductInfos.Where(f => f.Product.GroupName == x.Product.GroupName && f.Product.ProductName == "DUMMY" && f.LotId == x.LotId).First();
                
                if (updateRequisition.RequisitionType == ConstantCollections.RequisitionTypeName.ElementAt(1))
                {
                   
                    up.OnProcessingQuantity -= x.LotQuantity;
                    up.UnfinishedQuantity += x.LotQuantity;
                    dummy.UnfinishedQuantity += x.LotQuantity;
                    dummy.OnProcessingQuantity -= x.LotQuantity;

                }

                else
                {
                   
                    up.FinishedQuantity -= x.LotQuantity;
                    up.UnfinishedQuantity += x.LotQuantity;
                    dummy.UnfinishedQuantity += x.LotQuantity;
                    dummy.FinishedQuantity -= x.LotQuantity;
                 }

            }

            foreach (RequisitionPackageInfo x in PackageList)
            {
                InventoryPackageInfo up = new InventoryPackageInfo();
                up = _context.InventoryPackageInfos.Where(f => f.PackageId== x.Package.PackageId&&f.InventoryId==UserInfo.Inventory.InventoryId).First();
               
                if (updateRequisition.RequisitionType == ConstantCollections.RequisitionTypeName.ElementAt(1))
                {
                   
                    up.OnProcessingQuantity -= x.AcceptedQuantity;
                    up.UnfinishedQuantity += x.AcceptedQuantity;

                }

                else
                {
                   
                    up.FinishedQuantity -= x.AcceptedQuantity;
                    up.UnfinishedQuantity += x.AcceptedQuantity;
                }

            }
            updateRequisition.Status = "Approved";
            InventoryMesseging.RequisitionStatus = "Approved";
            Busy.Show();
            _context.SubmitChanges().Completed += new EventHandler(ReturnRequisitionVerificationChildWindow_Completed);
           
        }

        void ReturnRequisitionVerificationChildWindow_Completed(object sender, EventArgs e)
        {
            InventoryMesseging.RequisitionStatus = "Approved";
            Busy.Close();
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            updateRequisition.Status = "Rejected";
            Busy.Close();
            _context.SubmitChanges().Completed += new EventHandler(ReturnRequisitionVerificationChildWindowReject_Completed);
           
           
        }


        void ReturnRequisitionVerificationChildWindowReject_Completed(object sender, EventArgs e)
        {
            InventoryMesseging.RequisitionStatus = "Rejected";
            this.DialogResult = true;
        }
    }
}

