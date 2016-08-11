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
using System.Collections.ObjectModel;
using System.Collections;
using TrialBusinessManager.Models;
using System.ServiceModel.DomainServices.Client;
using System.Xml.Linq;
using System.ServiceModel.DomainServices.Client.ApplicationServices;

namespace TrialBusinessManager.Views
{
    public partial class SetSalesTargetForOfficers : ChildWindow
    {
        //constants
        const string SET_MODE = "set";
        const string EDIT_MODE = "edit";
        const string SET_MODE_NOTIFIER = "Operation is in SET mode.Any change will be new entry in database.";
        const string EDIT_MODE_NOTIFIER = "Operation is in EDIT mode.Any change will update corresponding entry in database.";
        const string SUCCESSFUL_NOTIFIER = "Target successfully distributed for ";
        string[] Months = { "Jan", "Feb", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        //
        AgroDomainContext _context=new AgroDomainContext();
        DateTime NullDate = DateTime.MinValue;
        DateTime today = DateTime.Today;
        DateTime start;
        DateTime end;
        Employee selectedEmployee=null;
        ObservableCollection<CustomDataGridRow> itemsCollection = new ObservableCollection<CustomDataGridRow>();
        string selectedProductType = "";
        Season selectedSeason = null;
		string mode=SET_MODE;//default mode
        //region id of this user
        long RegionID;
        //counts async operations
        int op_counter = 0;

        public SetSalesTargetForOfficers()
        {
            InitializeComponent();
            start = NullDate;
            end = NullDate;
            this.BusyBar.Visibility = System.Windows.Visibility.Visible;
            //set the region id
            RegionID = UserInfo.Region.RegionId;
            this.LoadData();
            this.radioGroup.IsEnabled = true;
            
            
        }
        /// <summary>
        /// Loads data for region combo and products combo.
        /// </summary>
        private void LoadData()
        {
            this.op_counter = 0;
            //get all the sales officers under this regional manager
            _context.Load(_context.GetEmployeesQuery().Where(entity=>entity.RegionId==this.RegionID && entity.Designation.Contains("Sales"))).Completed += (sender1, event1) =>
                {
                    this.officersCombo.ItemsSource = _context.Employees;
                    this.op_counter++;
                    if (this.op_counter == 5)
                    {
                        this.decorateBusyBarAndMessagePane();
                    }
                   
                };
            //ensures the seasons that are loaded are only which have end dates later than today.
            _context.Load(_context.GetSeasonsQuery()).Completed += (sender11, event11) =>
            {
                this.seasonCombo.ItemsSource = _context.Seasons.Where(season => this.isEarlier(this.today, season.SeasonEndDate));
                this.op_counter++;
                if (this.op_counter == 5)
                {
                    this.decorateBusyBarAndMessagePane();
                }
            };
            _context.Load(_context.GetProductsQuery().Where(e => e.ProductName == "DUMMY")).Completed += (sender2, event2) =>
            {
                var product_types = (from product in _context.Products select product.ProductType).Distinct();
                this.productCombo.ItemsSource = product_types.ToList();
                this.op_counter++;
                if (this.op_counter == 5)
                {
                    this.decorateBusyBarAndMessagePane();
                }
                
            };
            _context.Load(_context.GetRegionTargetsQuery().Where(entity => entity.RegionId == this.RegionID)).Completed += (sender3, event3) =>
            {
                this.op_counter++;
                if (this.op_counter == 5)
                {
                    this.decorateBusyBarAndMessagePane();
                }
            };
            _context.Load(_context.GetSalesOfficerTargetsQuery()).Completed += (sender4, event4) =>
            {
                this.op_counter++;
                if (this.op_counter == 5)
                {
                    this.decorateBusyBarAndMessagePane();
                }
            };
                    
        }
       
        /// <summary>
        /// init data grid with proper datas
        /// </summary>
        /// <param name="product_type"></param>
        private void initDataGrid(string product_type)
        {
            //if items collection is not empty, make it empty.
            if (this.itemsCollection.Count != 0)
            {
                this.itemsCollection.Clear();
            }
            //
            var products_list = _context.Products.Where(entity => entity.ProductType.Equals(product_type));
            //check if it's empty
            if (products_list.ToList().Count != 0)
            {
                if (this.mode.Equals(SET_MODE))
                {
                    showDataForSetMode(products_list);
                }
                else if (this.mode.Equals(EDIT_MODE))
                {
                    showDataForEditMode(products_list);
                }
            }//failed to fetch product list,its empty
            else
            {
                MessageBox.Show("Failed to fetch products list.Please try again.");
            }
            try
            {
                this.productListGrid.ItemsSource = this.itemsCollection;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Shows data for set mode
        /// </summary>
        /// <param name="products_list"></param>
        private void showDataForSetMode(IEnumerable<Product> products_list)
        {
            try
            {
                foreach (Product product in products_list)
                {
                    //the region target for this product
                    var regionTarget = _context.RegionTargets.Where(entity => entity.Product_ProductId == product.ProductId).First();
                    this.itemsCollection.Add(new CustomDataGridRow()
                    {
                        GroupName = product.GroupName,
                        ProductID = product.ProductId,
                        TargetQuantity = Convert.ToString(regionTarget.TargetQuantity),
                        DistributedQuantity = "0.0"
                    });
                }
            }
            catch (Exception ex)
            { }
        }

        /// <summary>
        /// Shows data for edit mode.
        /// </summary>
        /// <param name="products_list"></param>
        private void showDataForEditMode(IEnumerable<Product> products_list)
        {
                var officerTargets = _context.SalesOfficerTargets.Where(entity => entity.SalesOfficerId.Equals(selectedEmployee.EmployeeId) && entity.SeasonId.Equals(((Season)this.seasonCombo.SelectedItem).SeasonId));
                foreach (Product product in products_list)
                {

                    var officerTargets_for_product = officerTargets.Where(entity => entity.ProductId.Equals(product.ProductId));
                    var officerTarget_quantity = from officer_target in officerTargets_for_product select officer_target.TargetQuantity;
                    if (_context.RegionTargets.Any(entity => entity.Product_ProductId == product.ProductId))
                    {
                        var regionTarget = _context.RegionTargets.Where(entity => entity.Product_ProductId == product.ProductId).First();
                        //check if the list is empty
                        if (officerTarget_quantity.ToList().Count != 0)
                        {
                            this.itemsCollection.Add(new CustomDataGridRow()
                            {
                                GroupName = product.GroupName,
                                ProductID = product.ProductId,
                                TargetQuantity = Convert.ToString(regionTarget.TargetQuantity),
                                DistributedQuantity = Convert.ToString(officerTarget_quantity.ToList()[0])
                            });
                        }
                        else
                        {
                            MessageBox.Show("Previous sales targets could not be loaded.Please try again!");
                            break;
                        }
                    }
                }
        }
        /// <summary>
        /// Checks if the time span is within season
        /// </summary>
        /// <param name="first"></param>
        /// <param name="last"></param>
        /// <param name="season"></param>
        /// <returns></returns>
        private bool isWithinSeason(DateTime first, DateTime last, Season season)
        {
            bool retVal = false;
            if ((this.isLater(first, season.SeasonStartDate) && this.isEarlier(first, season.SeasonEndDate)) || (this.isLater(last, season.SeasonStartDate) && this.isEarlier(last, season.SeasonEndDate)))
            {
                retVal = true;
            }
            if (this.isEqual(first, season.SeasonStartDate))
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
       
        /// <summary>
        /// Busy bar gets collapsed and messagepane gets visible.
        /// </summary>
        private void decorateBusyBarAndMessagePane()
        {
            if (this.BusyBar.Visibility == Visibility.Visible)
            {
                this.BusyBar.Visibility = Visibility.Collapsed;
                //make the messagepane visible
                this.messagePane.Visibility = Visibility.Visible;
                
            }
        }
        /// <summary>
        /// Updates existiing  data
        /// </summary>
        private void UpdateData()
        {
            if (officersCombo.SelectedItem != null)
            {
               
                foreach (CustomDataGridRow row in itemsCollection)
                {
                    SalesOfficerTarget target = _context.SalesOfficerTargets.Where(e => e.ProductId == row.ProductID && e.SalesOfficerId == this.selectedEmployee.EmployeeId).FirstOrDefault();
                    target.TargetQuantity = Convert.ToDouble(row.DistributedQuantity);
                }
                try
                {
                    //show the saving data overlay
                    this.overlay.Visibility = Visibility.Visible;
                    _context.SubmitChanges().Completed += (s, args) =>
                    {
                        this.messagePane.Text = SUCCESSFUL_NOTIFIER + selectedEmployee.UserName;
                        //hide the saving data overlay
                        this.overlay.Visibility = Visibility.Collapsed;

                    };


                }
                catch (Exception ex)
                {
                    this.messagePane.Text = "Something went wrong.Please retry.";
                    //hide the saving data overlay
                    this.overlay.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                MessageBox.Show("Before saving you need to select an officer.");
            }
        }
        /// <summary>
        /// 
        /// Adds new data
        /// </summary>
        private void AddNewData()
        {
                foreach (CustomDataGridRow row in itemsCollection)
                {
                    SalesOfficerTarget target = new SalesOfficerTarget();
                    if (selectedEmployee != null)
                    {
                        target.SalesOfficerId = selectedEmployee.EmployeeId;
                        target.ProductId = row.ProductID;
                        target.TargetQuantity = Convert.ToDouble(row.DistributedQuantity);
                        target.AchievedAmount = 0.0;
                        target.DistributedQuantity = 0.0;
                        if (target.TargetQuantity < 0)
                        {
                            MessageBox.Show("Quantity can not be negative!");
                            return;
                        }

                    }
                    if (_context.SalesOfficerTargets.Any(e=> e.SalesOfficerId == target.SalesOfficerId && e.ProductId == target.ProductId&&e.TargetQuantity>0))
                    {
                        MessageBox.Show("Entry for this Season already exists.", "Already added...", MessageBoxButton.OK);
                        return;
     
                    }
                    else
                    {
                        //adding to season due to table relations
                        if(target.TargetQuantity!=0)
                        this.selectedSeason.SalesOfficerTargets.Add(target);
                    }

                }
                try
                {
                    //show the saving data overlay
                    this.overlay.Visibility = Visibility.Visible;
                    _context.SubmitChanges().Completed += (s, args) => 
                    { 
                        this.messagePane.Text = SUCCESSFUL_NOTIFIER + selectedEmployee.UserName;
                        //hide the saving data overlay
                        this.overlay.Visibility = Visibility.Collapsed;

                    };
     
                    
                }
                catch (Exception ex)
                {
                    this.messagePane.Text = "Something went wrong.Please retry.";
                    //hide the saving data overlay
                    this.overlay.Visibility = Visibility.Collapsed;
                }
        }

        /// <summary>
        /// Saves data when the mode is Set mode.
        /// </summary>
        private void SaveDataInSetMode()
        {
            var seasons = _context.Seasons.ToList();
            bool prevExists = false;
            foreach (Season season in seasons)
            {
                if (this.isWithinSeason(start, end, season))
                {
                    prevExists = true;
                    break;
                }
            }

            if (!this.isEarlier(start, end))
            {
                MessageBox.Show("Start date should be earlier than end date!");
            }
            else
            {
                //check if the season is not existing.
                if (!prevExists)
                {
                    AddNewData();
                }
                else
                {

                    MessageBox.Show("Conflicts with existing season span.Select different start and end date.");
                }

            }
        }

        

        #region Class for row
        public class CustomDataGridRow
        {
            public string GroupName { get; set; }
            public Int64 ProductID { get; set; }
            public string TargetQuantity { get; set; }
            public string DistributedQuantity { get; set; }

        }
        #endregion

        #region Events

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void quickSearchBox_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
        	if(this.quickSearchBox.Text.Contains("find"))
			{
				this.quickSearchBox.Text="";
			}
			// TODO: Add event handler implementation here.
        }

        private void quickSearchBox_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
			if(this.quickSearchBox.Text.Contains(""))
			{
				this.quickSearchBox.Text="find a product from below...";
			}
        	// TODO: Add event handler implementation here.
        }
        
        private void productCombo_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            //when we set index to -1 this event is fired that time too, so we set it to check for that case.
            if (this.productCombo.SelectedIndex != -1)
            {
                selectedProductType = (string)e.AddedItems[0];
                if (this.selectedEmployee != null && this.selectedSeason != null)
                {
                    this.initDataGrid(selectedProductType);
                }
            }
        	// TODO: Add event handler implementation here.
        }

        private void endDatePicker_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            DatePicker picker = (DatePicker)sender;
            end = (DateTime)e.AddedItems[0];
        	
        }

        private void startDatePicker_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            DatePicker picker=(DatePicker)sender;
            start =(DateTime)e.AddedItems[0];
            
        }

        private void radioButtonClicked(object sender, System.Windows.RoutedEventArgs e)
        {
        	RadioButton radio = sender as RadioButton;
            if (radio.IsChecked == true)
            {
                if (radio.Content.ToString().Equals("Set"))
                {
                    this.messagePane.Text = SET_MODE_NOTIFIER;
                   
                    mode = SET_MODE;
                }
                else
                {
                    this.messagePane.Text = EDIT_MODE_NOTIFIER;
                   
                    mode = EDIT_MODE;
                }
                this.initDataGrid(selectedProductType);
            }
        }

        private void quickSearchBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
        	if(e.Key==Key.Enter)
			{
				string product = this.quickSearchBox.Text.ToLower();
                for (int i = 0; i < this.itemsCollection.Count; i++)
                {
                    if (this.itemsCollection[i].GroupName.ToLower().Contains(product))
                    {
                        //scroll to that product
                        this.productListGrid.Dispatcher.BeginInvoke(delegate
                        {

                            this.productListGrid.Focus();

                            this.productListGrid.SelectedIndex=i;

                            this.productListGrid.ScrollIntoView(this.productListGrid.SelectedItem, this.productListGrid.Columns[0]);

                        });
                        break;
                    }
                }
				
			}
			
        }

        private void saveButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.selectedSeason != null)
            {
                if (this.mode == SET_MODE)
                {
                    this.AddNewData();
                    //SaveDataInSetMode();
                }
                else
                {
                    this.UpdateData();
                }
            }
            else
            {
                MessageBox.Show("Please select a season first.");
            }

        }

        
        
		
        private void officersComboSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedEmployee = (Employee)e.AddedItems[0];
            if (this.selectedSeason != null && !this.selectedProductType.Equals(""))
            {
                this.initDataGrid(this.selectedProductType);
            }
        }

        private void seasonCombo_selectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            this.selectedSeason = (Season)e.AddedItems[0];
            if (this.selectedEmployee != null && !this.selectedProductType.Equals(""))
            {
                this.initDataGrid(this.selectedProductType);
            }
        }

        #endregion





    }
}

