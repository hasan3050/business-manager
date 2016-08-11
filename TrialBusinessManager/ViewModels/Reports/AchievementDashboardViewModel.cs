using System;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using TrialBusinessManager.Web;
using TrialBusinessManager.Web.DataTransferModels;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using GalaSoft.MvvmLight.Command;
using TrialBusinessManager.Web.Report.Model;
using TrialBusinessManager.Views;
using System.Collections.ObjectModel;

namespace TrialBusinessManager.ViewModels.Reports
{
    public class AchievementDashboardViewModel:ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        int count = 0;
        bool initialization = false;
        BusyWindow Busy = new BusyWindow();
        public EntitySet<Season> SeasonList { get { return _context.Seasons; } } 
        public List<AchievementDashboardModel> RawData = new List<AchievementDashboardModel>();

        public ICommand ViewDataCommand { get; set; }
        public ICommand MonthlyViewCommand { get; set; }
        public ICommand WeeklyViewCommand { get; set; }

        public AchievementDashboardViewModel()
        {
            ViewDataCommand = new RelayCommand(ViewData);
            MonthlyViewCommand = new RelayCommand(MonthlyView);
            WeeklyViewCommand = new RelayCommand(WeeklyView);
            LoadAll();
            
        }
        
        #region Load
        private void LoadAll()
        {

            Busy.Show();
            LoadRegions();
            LoadProducts();
            LoadSalesOfficers();
            LoadSeasons();
        }

        private void LoadRegions()
        {
            _context.GetRegionsForAchievementDashboard(OnInvokeRegionLoadCompleted,null);
        }

        private void LoadSalesOfficers()
        {
            _context.GetSalesOfficersForAchievementDashboard(OnInvokeSalesOfficerLoadCompleted, null);
        }

        private void LoadProducts()
        {
            _context.GetProductsForAchievementDashboard(OnInvokeProductLoadCompleted, null);
        }

        private void LoadSeasons()
        {
            _context.Load(_context.GetCurrentSeasonForReportQuery()).Completed += (s, e) =>
            {
                SelectedStartDate = SeasonList.FirstOrDefault().SeasonStartDate;
                SelectedEndDate = GetCurrentFortNightDate(DateTime.Now);
                CloseWindow();
            };
        }

        private void LoadAcievementData()
        {
            if (SelectedRegion.RegionName == "All")
            {
                _context.GetAchievementDashboardData(null, null, SelectedStartDate, SelectedEndDate, OnInvokeCompletedForReport, null);
            }
            else {
                _context.GetAchievementDashboardData(SelectedRegion.RegionId, null, SelectedStartDate, SelectedEndDate, OnInvokeCompletedForReport, null);
            }
        }

        #endregion Load

        #region Method

            #region ProcessingRelated
        
        void CloseWindow()
        {
            count++;
            if (count >= 4)
            {
                Busy.Close();
            }
        }

        private DateTime GetCurrentFortNightDate(DateTime now)
        {
            DateTime LastDateOfMonth = new DateTime(now.Date.Year, now.Date.Month, 1).AddMonths(1).AddDays(-1);
            DateTime MidDateOfMonth = LastDateOfMonth.AddDays((LastDateOfMonth.Day + 1) / (-2));

            return (now.Date.Day > MidDateOfMonth.Day) ? LastDateOfMonth : MidDateOfMonth;
        }

        private DateTime GetNextFortNightDate(DateTime now)
        {
            return GetCurrentFortNightDate(GetCurrentFortNightDate(now).AddDays(1));
        }

        private DateTime GetPreviousFortNightDate(DateTime now)
        {
            return GetCurrentFortNightDate(GetCurrentFortNightDate(now).AddMonths(-1).AddDays(1));
        }

/*        private void PaddingFortNight(DateTime StartDate, DateTime EndDate)
        {
            DateTime StartFortNight=GetCurrentFortNightDate(StartDate);
            DateTime EndFortNight=GetCurrentFortNightDate(EndDate);

            int week = 1;

            for (DateTime now = StartFortNight; now <= EndFortNight; now = GetNextFortNightDate(now),week++)
            {
                if (DataList.Any(e => e.Time == now.ToShortDateString()) == false)
                {
                    ChartData temp = new ChartData()
                    {
                        Time = now.ToShortDateString(),
                        Week = week,
                        Collection = 0,
                        Sales = 0
                    };

                    DataList.Add(temp);
                }

                else
                {
                    DataList.Where(e => e.Time == now.ToShortDateString()).SingleOrDefault().Week = week;
                }
            }
    
            DataList = new ObservableCollection<ChartData>(DataList.OrderBy(e=>e.Week));
        }

        private void ProcessDataForChart()
        {
            DataList.Clear();

            List<AchievementDashboardModel>tempRawData=RawData;
      //      List<ProductModel> tempProductList = ProductList;

            if (SelectedRegion.RegionName != "All")
            {
                tempRawData = tempRawData.Where(e => e.RegionName == SelectedRegion.RegionName).ToList();
            }

            if (SelectedProduct.ProductGroupName != "All")
            {
                tempRawData = tempRawData.Where(e => e.ProductGroupName == SelectedProduct.ProductGroupName).ToList();
       //         tempProductList = ProductList.Where(e=>e.ProductGroupName==SelectedProduct.ProductGroupName).ToList();
            }

            if (SelectedStartDate > GetCurrentFortNightDate(SeasonList.FirstOrDefault().SeasonStartDate) ||
                SelectedEndDate < GetCurrentFortNightDate(SeasonList.FirstOrDefault().SeasonEndDate))
            {
                tempRawData = tempRawData.Where(e => e.Date > GetPreviousFortNightDate(SelectedStartDate) && e.Date <= GetCurrentFortNightDate(SelectedEndDate)).ToList();
            }
            
            var RawDataGroup = tempRawData.GroupBy(e => e.Date).OrderBy(e=>e.Key);

            foreach (var val in RawDataGroup)
            {
                ChartData temp = new ChartData();
                temp.Time = val.Key.ToShortDateString();
                temp.Sales = val.Sum(e => e.TotalSaleValue);
                temp.Collection = val.Sum(e => e.TotalCollectionValue);

                DataList.Add(temp);
            }

            PaddingFortNight(SelectedStartDate, SelectedEndDate);
            StatisticCalculation();
     //       MessageBox.Show(DataList.Count.ToString());
        }
 
        public void StatisticCalculation()
        {
            TotalCollection = DataList.Sum(e => e.Collection);
            TotalSale = DataList.Sum(e => e.Sales);
            Percentage = (TotalSale != 0) ? (TotalCollection * 100 / TotalSale) : 0;
        }
 */
        
        private void GenerateFilteredSalesOfficerList()
        {
            FilteredSalesOfficerList.Clear();
            if (SelectedRegion.RegionName != "All")
            {
                FilteredSalesOfficerList = SalesOfficerList.Where(e => e.RegionId == SelectedRegion.RegionId || e.EmployeeName=="All").OrderBy(e => e.EmployeeName).ToList();
              
            }
            else
                FilteredSalesOfficerList = SalesOfficerList.Where(e => e.EmployeeName == "All").ToList();

            SelectedSalesOfficer = FilteredSalesOfficerList.Where(e => e.EmployeeName == "All").SingleOrDefault();
        }

        private void ProcessDataForDataGrid()
        {
            ProcessedData.Clear();

            List<AchievementDashboardModel> tempRawData = RawData.ToList();
            List<ProductModel> tempProductList = ProductList.OrderBy(e=>e.ProductType).ThenBy(r=>r.ProductSubType).ThenBy(o=>o.ProductGroupName).ToList();
          //  List<ProductModel> tempProductList = ProductList;
            if (SelectedRegion.RegionName != "All")
            {
                tempRawData = tempRawData.Where(e => e.RegionName == SelectedRegion.RegionName).ToList();

                if (SelectedSalesOfficer.EmployeeName != "All")
                    tempRawData = tempRawData.Where(e => e.SalesOfficerId == SelectedSalesOfficer.EmployeeId).ToList();
            }

            if (SelectedProduct.ProductGroupName != "All")
            {
                tempRawData = tempRawData.Where(e => e.ProductGroupName == SelectedProduct.ProductGroupName).ToList();
                tempProductList = ProductList.Where(e => e.ProductGroupName == SelectedProduct.ProductGroupName).ToList();
            }
            
            if (SelectedStartDate > GetCurrentFortNightDate(SeasonList.FirstOrDefault().SeasonStartDate) ||
                SelectedEndDate < GetPreviousFortNightDate(SeasonList.FirstOrDefault().SeasonEndDate))
            {
                tempRawData = tempRawData.Where(e => e.Date > GetPreviousFortNightDate(SelectedStartDate) && e.Date <= GetCurrentFortNightDate(SelectedEndDate)).ToList();
            }
            
            foreach (ProductModel p in tempProductList)
            {    
                if (p.ProductGroupName != "All")
                {
                    AchievementDashboardModel temp = new AchievementDashboardModel();
                    temp.ProductGroupName = p.ProductGroupName;
                    temp.ProductSubType = p.ProductSubType;
                    
                     if (SelectedRegion.RegionName != "All")
                    {
                        temp.RegionSalesTargetVollume = tempRawData.Any(e => e.ProductGroupName == p.ProductGroupName) ? 
                                                        tempRawData.Where(e => e.ProductGroupName == p.ProductGroupName).FirstOrDefault().RegionSalesTargetVollume : 0;

                        if (SelectedSalesOfficer.EmployeeName != "All")
                        {
                            temp.SalesOfficerId = SelectedSalesOfficer.EmployeeId;
                            temp.SalesOfficerName = SelectedSalesOfficer.EmployeeName;
                            temp.SalesOfficerSalesTargetVollume = tempRawData.Any(e => e.ProductGroupName == p.ProductGroupName) ? 
                                                                  tempRawData.Where(e => e.ProductGroupName == p.ProductGroupName).FirstOrDefault().SalesOfficerSalesTargetVollume : 0;
                        }
                    }

                    else
                    {
                        temp.RegionSalesTargetVollume = 0;
                        foreach (RegionModel r in RegionList)
                        {
                            if (r.RegionName != "All")
                            {
                                temp.RegionSalesTargetVollume += 
                                    tempRawData.Any(e => e.ProductGroupName == p.ProductGroupName && e.RegionId == r.RegionId) ? 
                                    tempRawData.Where(e => e.ProductGroupName == p.ProductGroupName && e.RegionId == r.RegionId).FirstOrDefault().RegionSalesTargetVollume : 0;
                            }
                        }
                    }

                    temp.TotalSalesVollume = tempRawData.Any(e => e.ProductGroupName == p.ProductGroupName) ? tempRawData.Where(e => e.ProductGroupName == p.ProductGroupName).Sum(e => e.TotalSalesVollume) : 0;
                    temp.TotalReturnVollume = tempRawData.Any(e => e.ProductGroupName == p.ProductGroupName) ? tempRawData.Where(e => e.ProductGroupName == p.ProductGroupName).Sum(e => e.TotalReturnVollume) : 0;
                    temp.TotalSalesVollume -= temp.TotalReturnVollume;
                    //for the ease of binding against two possible choise "RegionTargetVollume" & "SalesOfficerTargetVollume" if instead of a whole region only the SO is selected 
                    if (SelectedRegion.RegionName != "All" && SelectedSalesOfficer.EmployeeName != "All")
                        temp.RegionSalesTargetVollume = temp.SalesOfficerSalesTargetVollume;

                    temp.RemainingSalesVollume = temp.RegionSalesTargetVollume - temp.TotalSalesVollume;
                    
                    ProcessedData.Add(temp);
                }

                //ProcessedData.OrderBy(o=>o.ProductType).ThenBy(r=>r.ProductSubType).ThenBy(e=>e.ProductGroupName);
               // ProcessedData=new ObservableCollection<AchievementDashboardModel>(ProcessedData.OrderBy(y => y.ProductSubType).ThenBy(y => y.ProductType).ThenBy(y => y.ProductGroupName));
                
            }
           // MessageBox.Show(ProcessedData.Count.ToString()+"   "+tempRawData.Count.ToString());
        }

            #endregion ProcessingRelated

            #region InvokeCompleted

        private void OnInvokeProductLoadCompleted(InvokeOperation<IEnumerable<ProductModel>> invOp)
        {
            CloseWindow();
            if (invOp.HasError)
            {
                MessageBox.Show(string.Format("Method Failed: {0}", invOp.Error.Message));
                invOp.MarkErrorAsHandled();
            }
            else
            {
                ProductList = invOp.Value.ToList();

                SelectedProduct = ProductList.Where(e=>e.ProductGroupName=="All").SingleOrDefault();
            }
            //MessageBox.Show(ProductList.Count.ToString());
        }

        private void OnInvokeSalesOfficerLoadCompleted(InvokeOperation<IEnumerable<EmployeeModel>> invOp)
        {
            CloseWindow();
            if (invOp.HasError)
            {
                MessageBox.Show(string.Format("Method Failed: {0}", invOp.Error.Message));
                invOp.MarkErrorAsHandled();
            }
            else
            {
                SalesOfficerList = invOp.Value.ToList();
                
                SelectedSalesOfficer = SalesOfficerList.Where(e => e.EmployeeName == "All").SingleOrDefault();
                FilteredSalesOfficerList = SalesOfficerList.Where(e => e.EmployeeName == "All").ToList();
            }
        }

        private void OnInvokeRegionLoadCompleted(InvokeOperation<IEnumerable<RegionModel>> invOp)
        {
            CloseWindow();
            if (invOp.HasError)
            {
                MessageBox.Show(string.Format("Method Failed: {0}", invOp.Error.Message));
                invOp.MarkErrorAsHandled();
            }
            else
            {
                RegionList = invOp.Value.OrderBy(e=>e.RegionName).ToList(); 
                SelectedRegion = RegionList.Where(e=>e.RegionName=="All").SingleOrDefault();
            }
            //MessageBox.Show(RegionList.Count.ToString());
        }

        private void OnInvokeCompletedForReport(InvokeOperation<IEnumerable<AchievementDashboardModel>> invOp)
        {
            Busy.Close();
            if (invOp.HasError)
            {
                MessageBox.Show(string.Format("Method Failed: {0}", invOp.Error.Message));
                invOp.MarkErrorAsHandled();
            }
            else
            {
                RawData = invOp.Value.ToList();
                initialization = true;
                ProcessDataForDataGrid();
                //ProcessDataForChart();
            }
   //         MessageBox.Show(RawData.Count.ToString());
        }

            #endregion InvokeCompleted

        #endregion Method

        #region Command
        
        public void ViewData()
        {
            //if (initialization == false)
            {
                Busy.Show();
                LoadAcievementData();
            }

           /* else
            {
                ProcessDataForDataGrid();
             //   ProcessDataForChart();
            }*/
        }

        public void MonthlyView()
        {
        }

        public void WeeklyView()
        {
        
        }

        #endregion Command
        
        #region Properties
        
        public const string DataListPropertyName = "DataList";
        private ObservableCollection<ChartData> _dataList=new ObservableCollection<ChartData>();
        public ObservableCollection<ChartData> DataList
        {
            get { return _dataList; }
            set
            {
                if (_dataList == value) { return; }

                _dataList = value;
                RaisePropertyChanged(DataListPropertyName);
            }
        }

        public const string ProcessedDataPropertyName = "ProcessedData";
        private ObservableCollection<AchievementDashboardModel> _processedData=new ObservableCollection<AchievementDashboardModel>();
        public ObservableCollection<AchievementDashboardModel> ProcessedData
        {
            get { return _processedData; }
            set
            {
                if (_processedData == value) { return; }

                _processedData = value;
                RaisePropertyChanged(ProcessedDataPropertyName);
            }
        }

        public const string RegionListPropertyName = "RegionList";
        private List<RegionModel> _regionList = new List<RegionModel>();
        public List<RegionModel> RegionList
        {
            get { return _regionList; }
            set
            {
                if (_regionList == value) { return; }

                _regionList = value;
                RaisePropertyChanged(RegionListPropertyName);
            }
        }

        public const string ProductListPropertyName = "ProductList";
        private List<ProductModel> _productList = new List<ProductModel>();
        public List<ProductModel> ProductList
        {
            get { return _productList; }
            set
            {
                if (_productList == value) { return; }

                _productList = value;
                RaisePropertyChanged(ProductListPropertyName);
            }
        }

        public const string SalesOfficerListPropertyName = "SalesOfficerList";
        private List<EmployeeModel> _salesOfficerList = new List<EmployeeModel>();
        public List<EmployeeModel> SalesOfficerList
        {
            get { return _salesOfficerList; }
            set
            {
                if (_salesOfficerList == value) { return; }

                _salesOfficerList = value;
                RaisePropertyChanged(SalesOfficerListPropertyName);
            }
        }

        public const string FilteredSalesOfficerListPropertyName = "FilteredSalesOfficerList";
        private List<EmployeeModel> _filteredSalesOfficerList = new List<EmployeeModel>();
        public List<EmployeeModel> FilteredSalesOfficerList
        {
            get { return _filteredSalesOfficerList; }
            set
            {
                if (_filteredSalesOfficerList == value) { return; }

                _filteredSalesOfficerList = value;
                RaisePropertyChanged(FilteredSalesOfficerListPropertyName);
            }
        }

        public const string SelectedRegionPropertyName = "SelectedRegion";
        private RegionModel _selectedRegion = new RegionModel();
        public RegionModel SelectedRegion
        {
            get { return _selectedRegion; }
            set
            {
                if (_selectedRegion == value) { return; }

                _selectedRegion = value;
                if(count>=4)
                    GenerateFilteredSalesOfficerList();
                RaisePropertyChanged(SelectedRegionPropertyName);
            }
        }

        public const string SelectedSalesOfficerPropertyName = "SelectedSalesOfficer";
        private EmployeeModel _selectedSalesOfficer;
        public EmployeeModel SelectedSalesOfficer
        {
            get { return _selectedSalesOfficer; }
            set
            {
                if (_selectedSalesOfficer == value) { return; }

                _selectedSalesOfficer = value;
                RaisePropertyChanged(SelectedSalesOfficerPropertyName);
            }
        }

        public const string SelectedProductPropertyName = "SelectedProduct";
        private ProductModel _selectedProduct = new ProductModel();
        public ProductModel SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                if (_selectedProduct == value) { return; }

                _selectedProduct = value;
                RaisePropertyChanged(SelectedProductPropertyName);
            }
        }

        public const string SelectedSeasonPropertyName = "SelectedSeason";
        private Season _selectedSeason;
        public Season SelectedSeason
        {
            get { return _selectedSeason; }
            set
            {
                if (_selectedSeason == value) { return; }

                _selectedSeason = value;
                RaisePropertyChanged(SelectedSeasonPropertyName);
            }
        }

        public const string SelectedStartDatePropertyName = "SelectedStartDate";
        private DateTime _selectedStartDate=DateTime.Now;
        public DateTime SelectedStartDate
        {
            get { return _selectedStartDate; }
            set
            {
                if (_selectedStartDate == value) { return; }

                _selectedStartDate = value;
            //comented this checking to show achievement data for previous seasosns. In previous version we only displayed report of current season 
            //    if (_selectedStartDate < SeasonList.FirstOrDefault().SeasonStartDate)
            //        _selectedStartDate = SeasonList.FirstOrDefault().SeasonStartDate;
            //    else _selectedStartDate = GetPreviousFortNightDate(_selectedStartDate);
                RaisePropertyChanged(SelectedStartDatePropertyName);
            }
        }

        public const string SelectedEndDatePropertyName = "SelectedEndDate";
        private DateTime _selectedEndDate=DateTime.Now;
        public DateTime SelectedEndDate
        {
            get { return _selectedEndDate; }
            set
            {
                if (_selectedEndDate == value) { return; }

                _selectedEndDate = value;
                if (_selectedEndDate > SeasonList.FirstOrDefault().SeasonEndDate)
                    _selectedEndDate = SeasonList.FirstOrDefault().SeasonEndDate;
                else
                    _selectedEndDate = GetCurrentFortNightDate(_selectedEndDate);
                RaisePropertyChanged(SelectedEndDatePropertyName);
            }
        }

  /*      public const string TotalSalePropertyName = "TotalSale";
        private double _totalSale;
        public double TotalSale
        {
            get { return _totalSale; }
            set
            {
                if (_totalSale == value) { return; }

                _totalSale = value;
                RaisePropertyChanged(TotalSalePropertyName);
            }
        }

        public const string TotalCollectionPropertyName = "TotalCollection";
        private double _totalCollection;
        public double TotalCollection
        {
            get { return _totalCollection; }
            set
            {
                if (_totalCollection == value) { return; }

                _totalCollection = value;
                RaisePropertyChanged(TotalCollectionPropertyName);
            }
        }

        public const string PercentagePropertyName = "Percentage";
        private double _percentage;
        public double Percentage
        {
            get { return Math.Round(_percentage,3); }
            set
            {
                if (_percentage == value) { return; }

                _percentage = value;
                RaisePropertyChanged(PercentagePropertyName);
            }
        }*/
       
        #endregion Properties

    }

    public class ChartData
    {
        public ChartData()
        { }

        public string Time { get; set; }
        public double Collection { get; set; }
        public double Sales { get; set; }
        public int Week { get; set; }
    }
}
