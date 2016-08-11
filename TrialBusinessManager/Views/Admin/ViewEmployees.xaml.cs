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
using System.Windows.Data;
using System.Collections.ObjectModel;
using TrialBusinessManager.Models;

namespace TrialBusinessManager.Views.Admin
{
    public partial class ViewEmployees : Page
    {
        AgroDomainContext _context = new AgroDomainContext();
        BusyWindow MyWindow = new BusyWindow();
        PagedCollectionView Employees;
        ObservableCollection<Region> Regions = new ObservableCollection<Region>();
        
        public ViewEmployees()
        {
            InitializeComponent();
            LoadData();
        }

        void LoadData()
        {
            MyWindow.Show();
            
            Employees = new PagedCollectionView(_context.Employees);
            DesignationCombo.ItemsSource = ConstantCollections.Designations;
            
            _context.Load(_context.GetEmployeesQuery()).Completed += (s, e) =>
                {
                    
                    dataGrid1.ItemsSource = Employees;
                    MyPager.Source = dataGrid1.ItemsSource;
                    
                    for (int i = 0; i < _context.Employees.Count; i++)
                    {
                        if (!Regions.Contains(_context.Employees.ElementAt(i).Region))
                        {
                            Regions.Add(_context.Employees.ElementAt(i).Region);
                        }
                    }

                    RegionCombo.ItemsSource = Regions;
                    MyWindow.Close();
                };
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void RegionCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Employees.Filter = null;
            Employees.Filter = new Predicate<object>((obj) => 
            {
                var Employee = obj as Employee;
                if(DesignationCombo.SelectedIndex==-1)
                 return (Employee != null && Employee.Region.Equals((Region)RegionCombo.SelectedItem));
                else
                 return (Employee != null && Employee.Region.Equals((Region)RegionCombo.SelectedItem) && Employee.Designation.Equals((String)DesignationCombo.SelectedItem));
            });
        }

        private void DesignationCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Employees.Filter = null;
            Employees.Filter = new Predicate<object>((obj) =>
            {
                var Employee = obj as Employee;
                if (RegionCombo.SelectedIndex == -1)
                    return (Employee != null && Employee.Designation.Equals((String)DesignationCombo.SelectedItem));
                else
                    return (Employee != null && Employee.Region.Equals((Region)RegionCombo.SelectedItem) && Employee.Designation.Equals((String)DesignationCombo.SelectedItem));
            });
        }

        private void NoFilterBtn_Click(object sender, RoutedEventArgs e)
        {
            Employees.Filter = null;
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            if ((Employee)dataGrid1.SelectedItem == null)
            {
                MessageBox.Show("please choose an employee!!");
                return;
            }

            ViewEditEmployeeWindow MyWindow = new ViewEditEmployeeWindow(_context,(Employee)dataGrid1.SelectedItem);
            MyWindow.Show();

        }

        

    }
}
