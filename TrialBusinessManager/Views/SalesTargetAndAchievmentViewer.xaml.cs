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
using System.Collections.ObjectModel;
using System.Collections;
using TrialBusinessManager.Models;
using System.ServiceModel.DomainServices.Client;
using System.Xml.Linq;
using System.ServiceModel.DomainServices.Client.ApplicationServices;
using System.Windows.Data;
using System.Windows.Controls.DataVisualization.Charting;
using TrialBusinessManager.Views;
namespace TrialBusinessManager
{
    public partial class SalesTargetAndAchievmentViewer : Page
    {
        AgroDomainContext _context = new AgroDomainContext();
        ObservableCollection<DataGridData> dataCollection = new ObservableCollection<DataGridData>();
        int pageSize;
        Employee selectedOfficer = null;
        DateTime today = DateTime.Today;
        Season currentSeason = null;
        /// <summary>
        /// totalTarget is the total of all targets of all regions in curent season.
        /// totalAchievement is the total of all achievements of all regions in this season.
        /// </summary>
        double totalTarget = 0;
        double totalAchievedQuantity = 0;
        double totalAchievedAmount = 0;
        /// <summary>
        /// totalTargetForSelectedRegion is the total of all targets of this.selectedRegion in current season.
        /// At this moment totalTargetForSelectedRegion is none of use.But it might be of use in near future.
        /// totalAchievementForSelectedRegion is the total of all achievements of all regions in current season.
        /// </summary>
        double totalTargetForSelectedRegion = 0;
        double totalAchievedQuantityForSelectedOfficer = 0;
        double totalAchievedAmountForSelectedOfficer = 0;
        BusyWindow busyIndicator = new BusyWindow();
        public SalesTargetAndAchievmentViewer()
        {
            InitializeComponent();
            this.pageSize = this.datagridPager.PageSize;
        
            this.LoadEmployeesFor(UserInfo.Region.RegionId);
        }

        #region SeasonChecking
        /// <summary>
        /// Checks if the date is within the season.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="season"></param>
        /// <returns></returns>
        private bool isWithinSeason(DateTime date, Season season)
        {
            bool retVal = false;
            if ((this.isLater(date, season.SeasonStartDate) && this.isEarlier(date, season.SeasonEndDate)))
            {
                retVal = true;
            }
            if (this.isEqual(date, season.SeasonStartDate))
            {
                retVal = true;
            }
            return retVal;
        }
        /// <summary>
        /// Returns whether first date is greater than or equal or less than last.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="last"></param>
        /// <returns></returns>
        private int CompareDates(DateTime first, DateTime last)
        {
            return first.Date.CompareTo(last.Date);
        }
        /// <summary>
        /// Checks if first date is earlier than last.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="last"></param>
        /// <returns></returns>
        private bool isEarlier(DateTime first, DateTime last)
        {
            return this.CompareDates(first, last) < 0;
        }
        /// <summary>
        /// Checks if first date is equal to last.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="last"></param>
        /// <returns></returns>
        private bool isEqual(DateTime first, DateTime last)
        {
            return this.CompareDates(first, last) == 0;
        }
        /// <summary>
        /// Checks if first date is later than last.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="last"></param>
        /// <returns></returns>
        private bool isLater(DateTime first, DateTime last)
        {
            return this.CompareDates(first, last) > 0;
        }

        #endregion

       
        /// <summary>
        /// Loads data sales officer combo for specific regionID and other datas
        /// </summary>
        private void LoadEmployeesFor(long ID)
        {
            //show up the busy indicator
            this.busyIndicator.Show();
            //
            _context.Load(_context.GetSeasonsQuery()).Completed += (s, e) =>
            {
                
                this.currentSeason = _context.Seasons.Where(season => this.isWithinSeason(this.today, season)).FirstOrDefault();
                //get all the sales officers under this regional manager,so now we have only the employees under this RM.
                _context.Load(_context.GetEmployeesQuery().Where(entity => entity.RegionId == ID && entity.Designation.Contains("Sales"))).Completed += (sender1, event1) =>
                {
                    if (_context.Employees.Where(r => r.Designation.Contains("Sales")).Count() > 0)
                    {
                        this.officersCombo.ItemsSource = _context.Employees.Where(r => r.Designation.Contains("Sales"));

                        _context.Load(_context.GetSalesOfficerTargetsQuery().Where(salestarget => salestarget.SeasonId == this.currentSeason.SeasonId)).Completed += (sender2, event2) =>
                        {

                            if (_context.SalesOfficerTargets.Where(salestarget => salestarget.SeasonId == this.currentSeason.SeasonId).Count() > 0)
                            {
                                _context.Load(_context.GetProductsQuery()).Completed += (sender, events) =>
                                {
                                    this.CalculateTotalsForAllOfficers();
                                    this.busyIndicator.Close();
                                };
                            }
                            else
                            {
                                busyIndicator.Close();
                            }
                        };
                    }
                    else
                    {
                        busyIndicator.Close();
                    }
                    

                    
                };
            
                
            };

            busyIndicator.Close();
            
        }
        /// <summary>
        /// Calculates the total targets and achievements.
        /// </summary>
        private void CalculateTotalsForAllOfficers()
        {
            if (this.currentSeason != null)
            {
                this.totalTarget = 0;
                this.totalAchievedQuantity = 0;
                this.totalAchievedAmount = 0;
                //add up in total
                foreach (SalesOfficerTarget target in _context.SalesOfficerTargets)
                {
                    if (this.isOfThisRegion(target.SalesOfficerId))
                    {
                        this.totalTarget += target.TargetQuantity;
                        this.totalAchievedQuantity += target.DistributedQuantity - target.SalesReturnQuantity - target.TransportationLossQuantity;
                        this.totalAchievedAmount += target.AchievedAmount;
                    }
                    
                }
            }
            else
            {
                MessageBox.Show("Please reload this page by clicking \n on same page link in treeview.");
            }
        }
        /// <summary>
        /// Checks if the employee with the ID is of this region.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        private bool isOfThisRegion(long ID)
        {
            bool retVal = false;
            var employee=_context.Employees.Where(e=> e.EmployeeId==ID);
            if (employee.Count()!=0)
            {
                retVal = true;
            }
            return retVal;
        }
        /// <summary>
        /// Calculates the total targets and achievements for the selected officer.
        /// </summary>
        /// <param name="targets"></param>
        private void CalculateTotalsForSelectedOfficer(List<SalesOfficerTarget> targets)
        {
            this.totalTargetForSelectedRegion = 0;
            this.totalAchievedQuantityForSelectedOfficer = 0;
            this.totalAchievedAmountForSelectedOfficer = 0;

            foreach (SalesOfficerTarget target in targets)
            {
                this.totalTargetForSelectedRegion += target.TargetQuantity;
                this.totalAchievedQuantityForSelectedOfficer += target.DistributedQuantity - target.SalesReturnQuantity - target.TransportationLossQuantity;
                this.totalAchievedAmountForSelectedOfficer += target.AchievedAmount;
            }
        }
        /// <summary>
        /// Shows data for specific officer
        /// </summary>
        /// <param name="ID"></param>
        private void LoadDataForOfficer(long ID)
        {
            List<SalesOfficerTarget> targets = _context.SalesOfficerTargets.Where(e => e.SalesOfficerId == ID).ToList();
            this.CalculateTotalsForSelectedOfficer(targets);
            this.InitDataGridAndPager(targets);
            this.UpdateDataVisual();
        }

        /// <summary>
        /// Updates the visual datas in proper places.
        /// </summary>
        private void UpdateDataVisual()
        {
            this.totalTargetField.Text = Convert.ToString(this.totalTarget);
            this.totalAchievementField.Text = Convert.ToString(this.totalAchievedQuantity);
            this.totalAchievedAmountField.Text = Convert.ToString(this.totalAchievedAmount);
            this.officerNameBlock.Text = this.selectedOfficer.Person.Name;
            this.officerQuantityAchievmentBlock.Text = Convert.ToString(this.totalAchievedQuantityForSelectedOfficer);
            this.officerQuantityPercentageBlock.Text = Convert.ToString(this.totalAchievedQuantityForSelectedOfficer * 100 / this.totalAchievedQuantity);
            this.officerAmountAchievementBlock.Text = Convert.ToString(this.totalAchievedAmountForSelectedOfficer);
            this.officerAmountPercentageBlock.Text = Convert.ToString(this.totalAchievedAmountForSelectedOfficer * 100 / this.totalAchievedAmount);
        }
        /// <summary>
        /// Inits datagrid and pager
        /// </summary>
        private void InitDataGridAndPager(List<SalesOfficerTarget> targets)
        {
            try
            {
                dataCollection.Clear();
                foreach (SalesOfficerTarget target in targets)
                {
                    Product product = new Product();
                    product = _context.Products.Where(e => e.ProductId == target.ProductId).FirstOrDefault();
                    //
                    DataGridData data = new DataGridData();
                    data.ProductName = product.GroupName;
                    data.TargetQuantity = Convert.ToString(target.TargetQuantity);
                    data.AchievedQuantity = Convert.ToString(target.DistributedQuantity - target.SalesReturnQuantity - target.TransportationLossQuantity);
                    dataCollection.Add(data);
                }
                PagedCollectionView view = new PagedCollectionView(dataCollection);
                this.datagridPager.Source = view;
                this.dataList.ItemsSource = view;
                this.ProcessBarChartDataForPage(this.datagridPager.PageIndex);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Data loading..");
                return;
            }
        }


        /// <summary>
        /// Returns data for a page of specified size and index.
        /// The formulae for fetching page data is:
        /// startIndex=pageIndex*pageSize;
        /// lastIndex=pageIndex*pageSize + pageSize;
        /// [The last index is not actually the index of last object,
        /// the last object will be found at lastIndex-1]
        /// data= collection data from startIndex to lastIndex
        /// </summary>
        /// <param name="pageIndex">Index of the page in pager</param>
        /// <param name="pageSize">Size of each page</param>
        /// <returns></returns>
        private ObservableCollection<DataGridData> fetchDataForPageIndex(int pageIndex, int pageSize)
        {
            ObservableCollection<DataGridData> temp = new ObservableCollection<DataGridData>();
            int startIndex = pageIndex * pageSize;
            int lastIndex = pageIndex * pageSize + pageSize;
            for (int i = startIndex; i < lastIndex; i++)
            {
                if (i > (this.dataCollection.Count - 1))
                {
                    //i has reached the end of datacollection already,we should not proceed.
                    break;
                }
                temp.Add(this.dataCollection[i]);
            }
            return temp;
        }

        /// <summary>
        /// Method which processes and shows up actual barchart.
        /// </summary>
        private void ProcessBarChartDataForPage(int index)
        {
            ObservableCollection<DataGridData> temp = this.fetchDataForPageIndex(index, this.pageSize);
            List<KeyValuePair<string, double>> data = new List<KeyValuePair<string, double>>();
            foreach (DataGridData row in temp)
            {
                //FrameworkElement cellContent = this.dataList.Columns[0].GetCellContent(row);
                KeyValuePair<string, double> pair = new KeyValuePair<string, double>(row.ProductName, Double.Parse(row.AchievedQuantity));
                data.Add(pair);
            }
            //for safety we make the itemsource null before each time a new itemsource is assigned
            if (((BarSeries)barChart.Series[0]).ItemsSource != null)
            {
                ((BarSeries)barChart.Series[0]).ItemsSource = null;
            }
            ((BarSeries)barChart.Series[0]).ItemsSource = data;

        }

        /// <summary>
        /// Class that represents data in each datagrid row.
        /// </summary>
        public class DataGridData
        {
            public string ProductName { get; set; }
            public string TargetQuantity { get; set; }
            public string AchievedQuantity { get; set; }
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void officersComboSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.selectedOfficer  = (Employee)e.AddedItems[0];
            if (this.selectedOfficer != null)
            {
                this.LoadDataForOfficer(this.selectedOfficer.EmployeeId);
            }
        }

        private void pagerIndexChanged(object sender, System.EventArgs e)
        {
            this.ProcessBarChartDataForPage(this.datagridPager.PageIndex);
        	// TODO: Add event handler implementation here.
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            SetSalesTargetForOfficers obj = new SetSalesTargetForOfficers();
            obj.Show();
        }
    }
}
