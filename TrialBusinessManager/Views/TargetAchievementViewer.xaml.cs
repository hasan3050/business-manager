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
using System.Windows.Controls.DataVisualization.Charting;
using TrialBusinessManager.Web;
using System.Collections.ObjectModel;
using System.Collections;
using TrialBusinessManager.Models;
using System.ServiceModel.DomainServices.Client;
using System.Xml.Linq;
using System.ServiceModel.DomainServices.Client.ApplicationServices;
using System.Windows.Data;
using TrialBusinessManager.Views;

namespace TrialBusinessManager
{
    public partial class TargetAchievementViewer : Page
    {
        AgroDomainContext _context = new AgroDomainContext();
        ObservableCollection<DataGridData> dataCollection = new ObservableCollection<DataGridData>();
        ObservableCollection<string> regions = new ObservableCollection<string>();
        int SelectionFlag = 0;
        //the size of data each time the datagrid shows up
        int pageSize;
        DateTime today=DateTime.Today;
        Season currentSeason = null;
        Region selectedRegion = null;
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
        double totalAchievedQuantityForSelectedRegion = 0;
        double totalAchievedAmountForSelectedRegion = 0;
        BusyWindow busyIndicator = new BusyWindow();
        
        public TargetAchievementViewer()
        {
            InitializeComponent();
            //by default the region of this user is selected region
            this.selectedRegion = UserInfo.Region;
            //setting the datagrid width and height to auto
            this.dataList.Width = double.NaN;
            this.dataList.Height = double.NaN;
            this.pageSize = this.datagridPager.PageSize;
            this.LoadData();
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
        /// Loads the data needed.
        /// </summary>
        private void LoadData()
        {
            //show up the busy indicator
            this.busyIndicator.Show();
            //
            try
            {
                

                _context.Load(_context.GetRegionsQuery()).Completed += (sender, evnt) =>
                {
                    this.regionCombo.ItemsSource = _context.Regions;
                };
                _context.Load(_context.GetSeasonsQuery()).Completed += (s, e) =>
                {
                   
                    //calculate the region targets total.

                    //fetch all region targets for this season
                    if (_context.Seasons.Count > 0)
                    {
                        if (_context.Seasons.Where(season => this.isWithinSeason(this.today, season)).FirstOrDefault() != null)
                        {
                            this.currentSeason = _context.Seasons.Where(season => this.isWithinSeason(this.today, season)).FirstOrDefault();
                            _context.Load(_context.GetRegionTargetsQuery().Where(regiontarget => regiontarget.SeasonId == this.currentSeason.SeasonId)).Completed += (sender1, event1) =>
                            {
                                if (_context.RegionTargets.Where(regiontarget => regiontarget.SeasonId == this.currentSeason.SeasonId).Count() > 0)
                                {
                                    _context.Load(_context.GetProductsQuery()).Completed += (sender, events) =>
                                    {
                                        this.CalculateTotalsForAllRegions();
                                        this.LoadDataForRegion(this.selectedRegion.RegionId);
                                        //close the busyindicator
                                        this.busyIndicator.Close();
                                    };
                                }
                                else
                                {
                                    this.busyIndicator.Close();
                                }


                            };
                        }
                        else 
                        {
                            MessageBox.Show("Current date is not within any season!");
                        }
                       
                    }
                    else
                    {
                        this.busyIndicator.Close();
                    }

                };

            }
            catch (Exception ex)
            {
                MessageBox.Show("Please reload.");
                this.busyIndicator.Close();
            }
            finally
            {
                this.busyIndicator.Close();
            }
        }
        /// <summary>
        /// Calculates the total targets and achievements.
        /// </summary>
        private void CalculateTotalsForAllRegions()
        {
            if(this.currentSeason!=null)
            {
                this.totalTarget = 0;
                this.totalAchievedQuantity = 0;
                this.totalAchievedAmount = 0;
                //add up in total
                foreach (RegionTarget target in _context.RegionTargets)
                {
                    this.totalTarget += target.TargetQuantity;
                    this.totalAchievedQuantity += target.DistributedQuantity - target.SalesReturnQuantity - target.TransportationLossQuantity;
                    this.totalAchievedAmount += target.AchievedAmount;
                }
                
            }
            else
            {
                MessageBox.Show("Please reload this page by clicking \n on same page link in treeview.");
            }
        }
        /// <summary>
        /// Calculates the total targets and achievements for the selected region.
        /// </summary>
        /// <param name="targets"></param>
        private void CalculateTotalsForSelectedRegion(List<RegionTarget> targets)
        {
            this.totalTargetForSelectedRegion = 0;
            this.totalAchievedQuantityForSelectedRegion = 0;
            this.totalAchievedAmountForSelectedRegion = 0;

            foreach (RegionTarget target in targets)
            {
                this.totalTargetForSelectedRegion += target.TargetQuantity;
                this.totalAchievedQuantityForSelectedRegion += target.DistributedQuantity - target.SalesReturnQuantity - target.TransportationLossQuantity;
                this.totalAchievedAmountForSelectedRegion += target.AchievedAmount;
            }
        }
        /// <summary>
        /// Updates the visual datas in proper places.
        /// </summary>
        private void UpdateDataVisual()
        {
            this.totalTargetField.Text = Convert.ToString(this.totalTarget);
            this.totalAchievementField.Text = Convert.ToString(this.totalAchievedQuantity);
            this.totalAchievedAmountField.Text = Convert.ToString(this.totalAchievedAmount);
            this.regionNameBlock.Text = this.selectedRegion.RegionName;
            this.regionQuantityAchievmentBlock.Text = Convert.ToString(this.totalAchievedQuantityForSelectedRegion);
            this.regionQuantityPercentageBlock.Text = Convert.ToString(this.totalAchievedQuantityForSelectedRegion * 100 / this.totalAchievedQuantity);
            this.regionAmountAchievementBlock.Text = Convert.ToString(this.totalAchievedAmountForSelectedRegion);
            this.regionAmountPercentageBlock.Text = Convert.ToString(this.totalAchievedAmountForSelectedRegion*100/this.totalAchievedAmount);
        }
        /// <summary>
        /// Loads data from database for this season of that region. 
        /// </summary>
        /// <param name="ID"></param>
        private void LoadDataForRegion(long ID)
        {
            List<RegionTarget> targets = _context.RegionTargets.Where(e => e.RegionId == ID).ToList();
            this.CalculateTotalsForSelectedRegion(targets);
            this.InitDataGridAndPager(targets);
            this.UpdateDataVisual();
        }
        /// <summary>
        /// Inits datagrid and pager
        /// </summary>
        private void InitDataGridAndPager(List<RegionTarget> targets)
        {
            dataCollection.Clear();
            foreach (RegionTarget target in targets)
            {
                Product product = _context.Products.Where(e=>e.ProductId==target.Product_ProductId).FirstOrDefault();
                //
                DataGridData data = new DataGridData();
                data.ProductName = product.GroupName;
                data.TargetQuantity = Convert.ToString(target.TargetQuantity);
                data.AchievedQuantity = Convert.ToString(target.DistributedQuantity-target.SalesReturnQuantity-target.TransportationLossQuantity);
                data.AchievedAmount = Convert.ToString(target.AchievedAmount);
                dataCollection.Add(data);
            }
            PagedCollectionView view = new PagedCollectionView(dataCollection);
            this.datagridPager.Source = view;
            this.dataList.ItemsSource = view;
            this.ProcessBarChartDataForPage(this.datagridPager.PageIndex);

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
            ObservableCollection<DataGridData> temp = this.fetchDataForPageIndex(index,this.pageSize);
            List<KeyValuePair<string, double>> data = new List<KeyValuePair<string, double>>();
            foreach (DataGridData row in temp)
            {
                //FrameworkElement cellContent = this.dataList.Columns[0].GetCellContent(row);
                KeyValuePair<string, double> pair = new KeyValuePair<string, double>(row.ProductName,Double.Parse(row.AchievedQuantity));
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
            public string AchievedAmount { get; set; }
        }
        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
        /// <summary>
        /// Once the paging index is changed,this is fired
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pagerIndexChanged(object sender, EventArgs e)
        {
            this.ProcessBarChartDataForPage(this.datagridPager.PageIndex);
        }
        /// <summary>
        /// When a new region gets selected in the combo.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newRegionSelected(object sender, SelectionChangedEventArgs e)
        {
            this.selectedRegion=(Region)e.AddedItems[0];
            if (this.selectedRegion != null)
            {
                this.LoadDataForRegion(this.selectedRegion.RegionId);
            }
            /*
            this.regionNameBlock.Text = SelectedRegion.RegionName;
            this.LoadDataForRegion(SelectedRegion.RegionId);
             */
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            RegionTargetSetPane SetPane = new RegionTargetSetPane();
            SetPane.Show();
        }
    }
}
