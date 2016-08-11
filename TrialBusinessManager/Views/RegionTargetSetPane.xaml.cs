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
    public partial class RegionTargetSetPane : ChildWindow
    {
        //constants
        const string SET_MODE = "set";
        const string EDIT_MODE = "edit";
        const string SET_MODE_NOTIFIER = "Operation is in SET mode.Any change will be new entry in database.";
        const string EDIT_MODE_NOTIFIER = "Operation is in EDIT mode.Any change will update corresponding entry in database.";
        string[] Months = { "Jan", "Feb", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        //
        AgroDomainContext _context=new AgroDomainContext();
        DateTime NullDate = DateTime.MinValue;
        DateTime start;
        DateTime end;
        Season selectedSeason = null;
        Region selectedRegion=null;
        ObservableCollection<CustomDataGridRow> itemsCollection = new ObservableCollection<CustomDataGridRow>();
        string selectedProductType = "";
		string mode=SET_MODE;//default mode
        int opCounter = 0;
        int selectionCounter = 0;
        public RegionTargetSetPane()
        {
            InitializeComponent();
            start = NullDate;
            end = NullDate;
           // this.BusyBar.Visibility = System.Windows.Visibility.Visible;
            this.LoadData();
            
        }
        /// <summary>
        /// Loads data for region combo and products combo.
        /// </summary>
        private void LoadData()
        {
            this.opCounter = 0;
            _context.Load(_context.GetRegionsQuery()).Completed += (sender, events) =>
                {
                    this.regionCombo.ItemsSource = _context.Regions;
                    this.opCounter++;
                    
                    if (this.opCounter == 4)
                    {
                        this.decorateBusyBarAndMessagePane();
                    }
           
                };
            _context.Load(_context.GetSeasonsQuery()).Completed += (sender1, event1) =>
                  {
                      this.seasonCombo.ItemsSource = _context.Seasons;
                      this.opCounter++;

                      if (this.opCounter == 4)
                      {
                          this.decorateBusyBarAndMessagePane();
                      }
          
                  };
             _context.Load(_context.GetProductsQuery().Where(e=>e.ProductName=="DUMMY")).Completed += (sender2, event2) =>
                   {

                       var product_types = (from product in _context.Products select product.ProductType).Distinct();
         
                       this.productCombo.ItemsSource = product_types.ToList();
                       this.opCounter++;

                       if (this.opCounter == 4)
                       {
                           this.decorateBusyBarAndMessagePane();
                       }
             
                   };


             _context.Load(_context.GetRegionTargetsQuery()).Completed += (sender3, event3) =>
             {
                 this.opCounter++;

                 if (this.opCounter == 4)
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
                //edit mode
                if (this.mode.Equals(EDIT_MODE))
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
        /// Populates data in grid based on edit mode.
        /// </summary>
        /// <param name="products_list"></param>
        private void showDataForEditMode(IEnumerable<Product> products_list)
        {
                var region_targets = _context.RegionTargets.Where(entity => entity.RegionId.Equals(((Region)this.regionCombo.SelectedItem).RegionId) && entity.SeasonId.Equals(((Season)this.seasonCombo.SelectedItem).SeasonId));
               
                foreach (Product product in products_list)
                {
                    var region_targets_for_product= region_targets.Where(entity=> entity.Product_ProductId.Equals(product.ProductId));
                    var region_target_quantity = from region_target in region_targets_for_product select region_target.TargetQuantity;
                    
                    //check if the list is empty
                    if (region_target_quantity.ToList().Count != 0)
                    {
                        this.itemsCollection.Add(new CustomDataGridRow()
                        {
                            GroupName = product.GroupName,
                            ProductID = product.ProductId,
                            TargetQuantity = Convert.ToString(region_target_quantity.ToList()[0])
                        });
                    }

                }
           
        }

        /// <summary>
        /// Populates data in grid based on set mode.
        /// </summary>
        /// <param name="products_list"></param>
        private void showDataForSetMode(IEnumerable<Product> products_list)
        {

                //disabled,enable the radio group first
                if (this.radioGroup.IsEnabled == false)
                {
                    this.radioGroup.IsEnabled = true;
                }

                foreach (Product product in products_list)
                {
                    this.itemsCollection.Add(new CustomDataGridRow()
                    {
                        GroupName = product.GroupName,
                        ProductID = product.ProductId,
                        TargetQuantity = "0.0"
                    });
                }
        }
        /// <summary>
        /// In case edit mode is not valid, it takes back to set state.
        /// </summary>
        private void getBackToSetState()
        {
            this.setRadio.IsChecked = true;
         
            this.SeasonSelectionPane.Visibility = Visibility.Collapsed;
            this.mode = SET_MODE;
            this.initDataGrid(this.selectedProductType);
        }
        
        /// <summary>
        /// Busy bar gets collapsed and messagepane gets visible.
        /// </summary>
        private void decorateBusyBarAndMessagePane()
        {
            if (this.BusyBar.Visibility == System.Windows.Visibility.Visible)
            {
                this.BusyBar.Visibility = System.Windows.Visibility.Collapsed;
                //make the messagepane visible
                this.messagePane.Visibility = System.Windows.Visibility.Visible;
                
            }
        }
        /// <summary>
        /// Updates existing data in database
        /// </summary>
        private void UpdateData()
        {
           
                this.overlay.Visibility = System.Windows.Visibility.Visible;
                foreach (CustomDataGridRow row in itemsCollection)
                {
                    if (selectedRegion != null && selectedSeason != null)
                    {
                        RegionTarget target = _context.RegionTargets.Where(e => e.Product_ProductId == row.ProductID && e.RegionId == selectedRegion.RegionId && e.SeasonId == selectedSeason.SeasonId).FirstOrDefault();
                        if (selectedRegion != null)
                        {
                            target.TargetQuantity = Convert.ToDouble(row.TargetQuantity);
                        }
                    }
                    else{
                        return;
                    }
                }
                _context.SubmitChanges().Completed += (sender1, event1) =>
                {
                    //hide the saving data overlay
                    this.overlay.Visibility = System.Windows.Visibility.Collapsed;
                };
        }
        private bool isExisting(RegionTarget target)
        {
            return this.selectedSeason.RegionTargets.Any(e => e.Product_ProductId == target.Product_ProductId&&e.RegionId==target.RegionId);
        }
        /// <summary>
        /// Adds a new data to database
        /// </summary>
        private void AddNewData()
        {
            
                this.overlay.Visibility = System.Windows.Visibility.Visible;
                
                
                foreach (CustomDataGridRow row in itemsCollection)
                {
                    RegionTarget target = new RegionTarget();
                    
                    if (selectedRegion != null)
                    {
                        target.RegionId = selectedRegion.RegionId;                        
                        target.Product_ProductId = row.ProductID;
                        target.TargetQuantity = Convert.ToDouble(row.TargetQuantity);
                        target.AchievedAmount = 0.0;
                        target.DistributedQuantity = 0.0;
                        target.SalesReturnQuantity = 0.0;
                        if (!this.isExisting(target)&&target.TargetQuantity>0)
                        {
                            this.selectedSeason.RegionTargets.Add(target);
                        }
                    }

                }
                _context.SubmitChanges().Completed += (sender1, event1) =>
                {
                    this.overlay.Visibility = System.Windows.Visibility.Collapsed;
                };
        }

        /// <summary>
        /// Checks if the time span is within season
        /// </summary>
        /// <param name="first"></param>
        /// <param name="last"></param>
        /// <param name="season"></param>
        /// <returns></returns>
        private bool isWithinSeason(DateTime first,DateTime last,Season season)
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
            return this.CompareDates(first,last) == 0;
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
       
        public class CustomDataGridRow
        {
            public string GroupName { get; set; }
            public Int64 ProductID { get; set; }
            public string TargetQuantity { get; set; }

        }

        

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
        private void regionCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //when we set index to -1 this event is fired that time too, so we set it to check for that case.
            if (this.regionCombo.SelectedIndex != -1)
            {
                selectionCounter++;
                selectedRegion = (Region)e.AddedItems[0];
                if (this.productCombo.SelectedIndex != -1)
                {
                    var SelectedProduct = (String)productCombo.SelectedItem;
                    selectedProductType = SelectedProduct;
                    if (this.selectedRegion != null && !this.selectedProductType.Equals("") && this.selectedSeason != null)
                    {
                        this.initDataGrid(selectedProductType);
                    }
                    else
                    {
                       // MessageBox.Show("Select a region and season first.");
                    }
                }
            }

        }
        

        private void productCombo_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            //when we set index to -1 this event is fired that time too, so we set it to check for that case.
            if (this.productCombo.SelectedIndex != -1)
            {
                selectionCounter++;
                selectedProductType = (string)e.AddedItems[0];
                if (this.selectedRegion != null && !this.selectedProductType.Equals("") && this.selectedSeason != null)
                {
                    this.initDataGrid(selectedProductType);
                }
                else
                {
                  //  MessageBox.Show("Select a region and season first.");
                }
            }

        }

        private void endDatePicker_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            DatePicker picker = (DatePicker)sender;
            end = (DateTime)e.AddedItems[0];
            //update season info
         
        }

        private void startDatePicker_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            DatePicker picker=(DatePicker)sender;
            start =(DateTime)e.AddedItems[0];
            //update season info
          
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
            if (this.itemsCollection.Count > 0)
            {
                if (selectionCounter<3)
                    return;
                if (this.mode == SET_MODE)
                {

                    AddNewData();
                }
                else
                {
                    this.UpdateData();
                }
            }
            else
            {
                MessageBox.Show("Select a region,season and a product type.");
            }
            
        }

        private void seasonCombo_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (this.seasonCombo.SelectedIndex != -1)
            {
                selectionCounter++;
                this.selectedSeason = (Season)e.AddedItems[0];

                if (this.regionCombo.SelectedIndex != -1)
                {

                   
                    if (this.productCombo.SelectedIndex != -1)
                    {
                        var SelectedProduct=(String)productCombo.SelectedItem;
                        selectedProductType = SelectedProduct;
                        if (this.selectedRegion != null && !this.selectedProductType.Equals("") && this.selectedSeason != null)
                        {
                            this.initDataGrid(selectedProductType);
                        }
                        else
                        {
                          //  MessageBox.Show("Select a region and season first.");
                        }
                    }
                }
            }
        }


        #endregion
		
		

        

       


    }
}

