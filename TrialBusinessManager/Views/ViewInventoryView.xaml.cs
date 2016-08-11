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
using TrialBusinessManager.ViewModels;
using System.Collections.ObjectModel;
using TrialBusinessManager.Models;
using TrialBusinessManager.Web;

namespace TrialBusinessManager.Views
{
    public partial class ViewInventoryView : Page
    {
        public ObservableCollection<Chartview> MyChart;
        public ViewInventoryView()
        {
            InitializeComponent();
            PieChart.Visibility = Visibility.Collapsed;
            BarChart.Visibility = Visibility.Collapsed;
            PackageBarChart.Visibility = Visibility.Collapsed;
            PackagePieChart.Visibility = Visibility.Collapsed;
            //this.DataContext = new ViewInventoryViewModel();
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //MessageBox.Show("Changed");
                var SelectedProduct = (InventoryProductModel)dataGrid1.SelectedItem;
                MyChart = new ObservableCollection<Chartview>();

                MyChart.Add(new Chartview("finished", (Double)SelectedProduct.FinishedQuantity));
                MyChart.Add(new Chartview("Unfinished", (Double)SelectedProduct.UnfinishedQuantity));
                MyChart.Add(new Chartview("On processing", (Double)SelectedProduct.OnProcessingQuantity));

                PieChart.DataContext = MyChart;
                BarChart.DataContext = MyChart;

                PieChart.Visibility = Visibility.Visible;
                BarChart.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            { }
        }

        private void dataGrid2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var SelectedPackage = (InventoryPackageModel)dataGrid2.SelectedItem;
                MyChart = new ObservableCollection<Chartview>();

                MyChart.Add(new Chartview("finished", (Double)SelectedPackage.FinishedQuantity));
                MyChart.Add(new Chartview("Unfinished", (Double)SelectedPackage.UnfinishedQuantity));
                MyChart.Add(new Chartview("On processing", (Double)SelectedPackage.OnProcessingQuantity));

                PackagePieChart.DataContext = MyChart;
                PackageBarChart.DataContext = MyChart;

                PackagePieChart.Visibility = Visibility.Visible;
                PackageBarChart.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            { }
        }

        private void autoCompleteBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                InventoryProductModel selectedProduct = new InventoryProductModel();
                selectedProduct = (InventoryProductModel)autoCompleteBox1.SelectedItem;
                dataGrid1.UpdateLayout();
                this.dataGrid1.Dispatcher.BeginInvoke(delegate
                {

                    this.dataGrid1.Focus();

                    dataGrid1.SelectedItem = selectedProduct;

                    this.dataGrid1.ScrollIntoView(selectedProduct, this.dataGrid1.Columns[0]);

                });
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void autoCompleteBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                InventoryPackageModel selectedPackage = new InventoryPackageModel();
                selectedPackage = (InventoryPackageModel)autoCompleteBox2.SelectedItem;
                dataGrid2.UpdateLayout();
                this.dataGrid2.Dispatcher.BeginInvoke(delegate
                {

                    this.dataGrid2.Focus();

                    dataGrid2.SelectedItem = selectedPackage;

                    this.dataGrid2.ScrollIntoView(selectedPackage, this.dataGrid2.Columns[0]);

                });
            }
            catch (Exception ex)
            {
                return;
            }
        }

       
    }
}
