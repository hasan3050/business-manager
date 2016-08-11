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
using GalaSoft.MvvmLight.Messaging;
using TrialBusinessManager.ViewModels.NSM;


namespace TrialBusinessManager.Views
{
    public partial class EditInventoryInfoChildWindow : ChildWindow
    {
       
        AgroDomainContext _context = new AgroDomainContext();
        Inventory MyInventory = new Inventory();
        Employee SIC = new Employee();
        Employee DO = new Employee();
        BusyWindow Busy = new BusyWindow();
        
        public EditInventoryInfoChildWindow(AgroDomainContext SentContext,Inventory SentInventory)
        {

            InitializeComponent();

            _context = SentContext;
            MyInventory = SentInventory;

            InventoryNameTxtBox.Text = SentInventory.InventoryName;
            InventoryAddressTxtBox.Text = SentInventory.InventoryAddress;

            ComboBox1.ItemsSource = _context.Employees.Where(e => e.Designation == "Store In Charge").ToList();
            ComboBox2.ItemsSource = _context.Employees.Where(e => e.Designation == "Dispatch Officer").ToList();

            ComboBox1.SelectedItem= MyInventory.Employee;
            ComboBox2.SelectedItem = MyInventory.Employee1;

        }

      

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MyInventory = _context.Inventories.Where(r=>r.InventoryId==MyInventory.InventoryId).First();

            if (String.IsNullOrEmpty(InventoryNameTxtBox.Text) || String.IsNullOrEmpty(InventoryAddressTxtBox.Text))
            {
                MessageBox.Show("Please provide Inventory information!");
                return;
            }
          /*  if (_context.Inventories.Any(r => r.InventoryName==InventoryNameTxtBox.Text))
            {
                MessageBox.Show("There is already an inventory with this name, Inventory name has to be unique!");
                return;
            }*/
            if (MyInventory.RegionId != MyInventory.Employee.RegionId && MyInventory.Employee.Designation != "Invalid")
            {
                MessageBox.Show("Store In Charge belongs to another region!");
                return;
            }
            if (_context.Inventories.Any(r => r.Employee.EmployeeId == MyInventory.Employee.EmployeeId && r.InventoryId != MyInventory.InventoryId&&MyInventory.Employee.Designation!="Invalid"))
            {
                MessageBox.Show("Store In Charge already appointed to another inventory!");
                return;
            }

            if (MyInventory.RegionId != MyInventory.Employee1.RegionId && MyInventory.Employee1.Designation != "Invalid")
            {
                MessageBox.Show("Dispatch Officer belongs to another region!");
                return;
            }

            if (_context.Inventories.Any(r => r.Employee1.EmployeeId == MyInventory.Employee1.EmployeeId && r.InventoryId != MyInventory.InventoryId && MyInventory.Employee.Designation != "Invalid"))
            {
                MessageBox.Show("Dispatch Officer already appointed to another inventory!");
                return;
            }

            MyInventory.InventoryName = InventoryNameTxtBox.Text;
            MyInventory.InventoryAddress = InventoryAddressTxtBox.Text;
            Busy.Show();
            _context.SubmitChanges().Completed += (s, args) => { Messenger.Default.Send<string, ViewRegionsViewModel>("Messsage"); Busy.Close(); this.DialogResult = true; };

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            MyInventory.Employee = _context.Employees.Where(r => r.Designation == "Invalid" || r.UserName == "Invalid").First();
            MessageBox.Show("SIC has been reset!");
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            MyInventory.Employee1 = _context.Employees.Where(r => r.Designation == "Invalid" || r.UserName == "Invalid").First();
            MessageBox.Show("DO has been reset!");
        }

        private void ComboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                MyInventory.Employee = (Employee)ComboBox1.SelectedItem;
            }
            catch (Exception ex)
            { }
        }

        private void ComboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                MyInventory.Employee1 = (Employee)ComboBox2.SelectedItem;
            }
            catch (Exception ex)
            { }
        }
    }
}

