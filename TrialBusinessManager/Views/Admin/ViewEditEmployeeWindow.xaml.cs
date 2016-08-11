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
using TrialBusinessManager.Models;
using System.ServiceModel.DomainServices.Client;

namespace TrialBusinessManager.Views.Admin
{
    public partial class ViewEditEmployeeWindow : ChildWindow
    {
        AgroDomainContext _context = new AgroDomainContext();
        Employee MyEmployee=new Employee();
        BusyWindow MyWindow = new BusyWindow();

        public ViewEditEmployeeWindow(AgroDomainContext Context,Employee Employee)
        {
            InitializeComponent();
            _context = Context;
            MyEmployee = Employee;
            EmployeeGrid.DataContext = MyEmployee;
            PersonGrid.DataContext = MyEmployee.Person;
            RegionCombo.ItemsSource = _context.Regions;
            ActivityStatusCombo.ItemsSource = ConstantCollections.ActivityStatus;
            DesignationCombo.ItemsSource = ConstantCollections.Designations;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            
            MyWindow.Show();
            _context.SubmitChanges(OnSubmitCompleted,null);
        }

        void OnSubmitCompleted(SubmitOperation so)
        {
            MyWindow.Close();
            if (so.HasError)
            {
                MessageBox.Show("Submit Failed!! please try again");
                return;
            }
            this.DialogResult = true;
        }

        private void ResetPassBtn_Click(object sender, RoutedEventArgs e)
        {
            MyEmployee.Password = MD5Core.GetString(MD5Core.GetHash("1000"));
            ResetPassBtn.IsEnabled = false;
        }

        private void ActivityStatusCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MyEmployee.ActivityStatus = (String)ActivityStatusCombo.SelectedItem;
        }

        private void RegionCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MyEmployee.Region = (Region)RegionCombo.SelectedItem;
        }

        private void DesignationCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MyEmployee.Designation = (String)DesignationCombo.SelectedItem;
        }
    }
}

