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
    public partial class PLRConfirmationChildWindow : ChildWindow
    {
        AgroDomainContext _context = new AgroDomainContext();
        long plrID;
        PLR selectedPLR = new PLR();
        BusyWindow Busy = new BusyWindow();
        int Flag = 0;
        public PLRConfirmationChildWindow(PLR SentPLR,AgroDomainContext SentContext)
        {

            InitializeComponent();
            OKButton.IsEnabled = false;
            button1.IsEnabled = false;
            this._context = SentContext;
            selectedPLR = SentPLR;
            plrID = SentPLR.PLRId;
            ShowPLRData();
            LoadData();
           
      
        }
        void LoadData()
        { //Will Change when all the products will be loaded in cache//
            _context.Load(_context.GetInventoryProductInfoesQuery().Where(e => e.Inventory.InventoryId == selectedPLR.Inventory.InventoryId)).Completed += (s, arg) =>
            { 
                Flag++;
                if(Flag==5)
                {
                    OKButton.IsEnabled=true;
                    button1.IsEnabled=true;
                }
                
            };

            _context.Load(_context.GetInventoryPackageInfoesQuery().Where(e => e.Inventory.InventoryId == selectedPLR.Inventory.InventoryId)).Completed += (s, arg) =>
            { 
                Flag++;
                if(Flag==5)
                {
                    OKButton.IsEnabled=true;
                    button1.IsEnabled=true;
                }
              
            };
            
            _context.Load(_context.GetPLRProductInfoesQuery().Where(e => e.PLRId == selectedPLR.PLRId)).Completed += (s, arg) =>
            { 
                Flag++;
                if(Flag==5)
                {
                    OKButton.IsEnabled=true;
                    button1.IsEnabled=true;
                }
                dataGrid1.ItemsSource = _context.PLRProductInfos.Where(e => e.PLRId == plrID); 
            };
           
            _context.Load(_context.GetPLRPackageInfoesQuery().Where(e => e.PLRId == plrID)).Completed += (s, arg) =>
            {
                Flag++;
                if(Flag==5)
                {
                    OKButton.IsEnabled=true;
                    button1.IsEnabled=true;
                }
                dataGrid2.ItemsSource = _context.PLRPackageInfos.Where(e => e.PLRId == plrID);
            };
           
            _context.Load(_context.GetPLRsQuery().Where(e => e.PLRId == plrID)).Completed += (s, arg) =>
                {
                      Flag++;
                        if(Flag==5)
                        {
                            OKButton.IsEnabled=true;
                            button1.IsEnabled=true;
                        }
                    selectedPLR = _context.PLRs.Where(e => e.PLRId == plrID).First();
                  
                };
           
        }

        void ShowPLRData()
        {
            textBox1.Text = selectedPLR.DateOfIssue.ToString();
            textBox2.Text=selectedPLR.Employee.Person.Name;
            textBox3.Text = selectedPLR.Employee.Region.RegionName;
            textBox4.Text = selectedPLR.Employee.Designation;
        }

        void UpdateInventory()
        { 
            foreach (PLRProductInfo x in _context.PLRProductInfos.Where(e=>e.PLRId==plrID))
            {
                if (_context.InventoryProductInfos.Where(e => e.Inventory.InventoryId == selectedPLR.Inventory.InventoryId).Any(e => e.Product == x.Product && e.LotId == x.LotId))
                {
                    InventoryProductInfo update = new InventoryProductInfo();
                    InventoryProductInfo dummy = new InventoryProductInfo();

                    update = _context.InventoryProductInfos.Where(e => e.Inventory.InventoryId == selectedPLR.Inventory.InventoryId).Where(e => e.Product == x.Product && e.LotId == x.LotId).First();
                    dummy = _context.InventoryProductInfos.Where(e => e.Inventory.InventoryId == selectedPLR.Inventory.InventoryId).Where(e => e.Product.GroupName == x.Product.GroupName && e.LotId == x.LotId&&e.Product.ProductName=="DUMMY").First();
                    
                    if (x.State == "Finished")
                    {
                        if (dummy.FinishedQuantity > x.Quantity)
                        {
                            update.FinishedQuantity -= x.Quantity;
                            dummy.FinishedQuantity -= x.Quantity;
                        }
                        else 
                        {

                            MessageBox.Show("Now enough product for PLR deduction, PLR can not be accepted!");
                            return;
                        }
                    }
                    else if (x.State == "Unfinished")
                    {
                        if (dummy.UnfinishedQuantity > x.Quantity)
                        {
                            dummy.UnfinishedQuantity -= x.Quantity;
                        }
                        
                        else
                        {

                            MessageBox.Show("Now enough product for PLR deduction, PLR can not be accepted!");
                            return;
                        }
                    }

                    else
                    {
                        if (dummy.OnProcessingQuantity < x.Quantity)
                        {
                            update.OnProcessingQuantity -= x.Quantity;
                            dummy.OnProcessingQuantity -= x.Quantity;
                        }
                        
                        else
                        {

                            MessageBox.Show("Now enough product for PLR deduction, PLR can not be accepted!");
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show(x.Product.ProductName+" currently does not exist in inventory!!!!");
                    return;
                }
                
            
            }
            foreach (PLRPackageInfo x in _context.PLRPackageInfos.Where(e => e.PLRId == plrID))
            {
                if (_context.InventoryPackageInfos.Where(e => e.Inventory.InventoryId == selectedPLR.Inventory.InventoryId).Any(e => e.Package == x.Package ))
                {
                    InventoryPackageInfo update = new InventoryPackageInfo();
                    update = _context.InventoryPackageInfos.Where(e => e.Inventory.InventoryId == selectedPLR.Inventory.InventoryId).Where(e => e.Package == x.Package).First();
                    if (x.State == "Finished")
                    {
                        update.FinishedQuantity -= x.Quantity;
                    }
                    if (x.State == "Unfinished")
                    {
                        update.UnfinishedQuantity -= x.Quantity;
                    }
                    else
                    {
                        update.OnProcessingQuantity -= x.Quantity;
                    }
                }
                else
                {
                    MessageBox.Show(x.Package.PackageName + " currently does not exist in inventory!!!!");
                    return;
                }


            }
        
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            Busy.Show();
            UpdateInventory();
            selectedPLR.Status = "Verified";
            _context.SubmitChanges().Completed += new EventHandler(PLRConfirmationChildWindow_Completed);
           
        }

        void PLRConfirmationChildWindow_Completed(object sender, EventArgs e)
        {
            Busy.Close();
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Busy.Show();
            selectedPLR.Status = "Rejected";
            _context.SubmitChanges().Completed += new EventHandler(PLRConfirmationChildWindow_Completed);
           
        }
    }
}

