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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TrialBusinessManager.Views;
using TrialBusinessManager.Web;
using TrialBusinessManager.Models;
using System.ServiceModel.DomainServices.Client;
using System.Xml.Linq;
using System.ServiceModel.DomainServices.Client.ApplicationServices;
using System.Windows.Browser;
using System.Threading;
using System.Net.NetworkInformation;

namespace TrialBusinessManager
{
	public partial class MainViewControl : UserControl
	{
        AgroDomainContext _context = new AgroDomainContext();
        Employee myEmployee = new Employee();
        Region CurrentRegion = new Region();
        List<TreeViewItem> treemenus = new List<TreeViewItem>();
        Timer ConnectivityTimer;
        Thread TimerThread;
        
		public MainViewControl()
		{
			// Required to initialize variables
			InitializeComponent();
            this.ContentFrame.Navigated+=new NavigatedEventHandler(ContentFrame_Navigated);
            this.ContentFrame.NavigationFailed+=new NavigationFailedEventHandler(ContentFrame_NavigationFailed);
            this.ContentFrame.Navigating+=new NavigatingCancelEventHandler(ContentFrame_Navigating);
            this.MouseRightButtonDown += new MouseButtonEventHandler(Mouse_RightButtonDown);
            TimerThread = new Thread(new ThreadStart(StartTimer));
           // TimerThread.Start();
		}

        void StartTimer()
        {
            ConnectivityTimer = new Timer(TimerElapsed, null, 5000, System.Threading.Timeout.Infinite);
            while (true)
            {

            }
        }

        void TimerElapsed(object state)
        {
            this.Dispatcher.BeginInvoke((Action)delegate()
            {
                if (!NetworkInterface.GetIsNetworkAvailable() && StaticMessaging.IsLoggedIn && !StaticMessaging.IsErrorWindowShowing)
                {
                    StaticMessaging.IsErrorWindowShowing = true;
                    ConnectivityErrorWindow errorWindow = new ConnectivityErrorWindow();
                    errorWindow.Closed += (s, e) =>
                    {
                        StaticMessaging.IsErrorWindowShowing = false;
                        StaticMessaging.IsLoggedIn = false;
                    };
                    errorWindow.Show();
                }
                ConnectivityTimer = new Timer(TimerElapsed, null, 5000, System.Threading.Timeout.Infinite);
            });
        }

        //logout Completed event
        void MainViewControl_Completed(object sender, EventArgs e)
        {
            StaticMessaging.IsLoggedIn = false;
        }
        
        /// <summary>
        /// Frame on navigation. Security check goes on here.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContentFrame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            string uri = e.Uri.OriginalString;
            //checking for navigation validation.
            //checking for the static links.
            if (!uri.Equals("/Home") && !uri.Equals("/NoticeBoard") && !uri.Equals("/ReportsView") && !uri.Equals("/WorkList"))
            {
                bool valid = false;
                for (int i = 0; i < treemenus.Count; i++)
                {
                    if (this.isURIPresent(treemenus[i], uri))
                    {
                        valid = true;
                        break;
                    }
                }
                if (!valid)
                {
                    e.Cancel = true;
                    ContentFrame.StopLoading();
                    MessageBox.Show("The URL you requested is not valid for your user level!");
                    //ContentFrame.Navigate(new Uri("/Home", UriKind.Relative));
                    //ContentFrame.Refresh();
                }
            }
           
                
           
        }
        /// <summary>
        /// Frame navigated events.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // After the Frame navigates, ensure the HyperlinkButton representing the current page is selected
        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {

            /*foreach (UIElement child in MenuContainer.Children)
            {
                HyperlinkButton hb = child as HyperlinkButton;
                if (hb != null && hb.NavigateUri != null)
                {
                    if (ContentFrame.UriMapper.MapUri(e.Uri).ToString().Equals(ContentFrame.UriMapper.MapUri(hb.NavigateUri).ToString()))
                    {
                        VisualStateManager.GoToState(hb, "ActiveLink", true);
                    }
                    else
                    {
                        VisualStateManager.GoToState(hb, "InactiveLink", true);
                    }
                }
            }*/

        }
        /// <summary>
        /// Handling error during navigation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // If an error occurs during navigation, show an error window
        private void ContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            e.Handled = true;

        }
      
        /// <summary>
        /// Checks if the specified URI is present in the item or its subitems.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        private bool isURIPresent(TreeViewItem item,string uri)
        {
            bool flag = false;
            if (item.URI.Equals(uri))
            {
                flag = true;
            }
            else
            {
                for (int i = 0; i < item.SubItems.Count; i++)
                {
                    TreeViewItem subItem = item.SubItems[i];
                    
                    if (subItem.URI.Equals(uri))
                    {
                        flag = true;
                        break;
                    }
                }
            }
            return flag;
        }
        /// <summary>
        /// called once it's needed to be shown
        /// </summary>
        public void show()
        {
            this.Visibility=Visibility.Visible;
            UserInfo.Username = WebContext.Current.User.Name;
            _context.Load(_context.GetEmployeesByUsernameQuery(UserInfo.Username)).Completed += new EventHandler(dataLoad_OnProcess);
        }
        /// <summary>
		/// Intermediate step in between login and data load for main window.
		/// Fetches important user infos.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void dataLoad_OnProcess(object sender, EventArgs e)
        {
            myEmployee = (sender as LoadOperation<Employee>).Entities.FirstOrDefault();
            UserInfo.Designation = myEmployee.Designation;
            UserInfo.EmployeeID = myEmployee.EmployeeId;
            _context.Load(_context.GetRegionsByIdQuery(myEmployee.RegionId)).Completed += new EventHandler(dataLoad_Completed);
        }

        
		/// <summary>
		/// Fired once data load completes.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void dataLoad_Completed(object sender, EventArgs e)
        {
            CurrentRegion = (sender as LoadOperation<Region>).Entities.FirstOrDefault();
			string designation=UserInfo.Designation;
			this.UserText.Text=UserInfo.Username;
            UserInfo.Region = CurrentRegion;
            UserInfo.RegionName = CurrentRegion.RegionName;
			if(designation.Contains("Regional Manager"))
			{
				this.LoadData("RM");
			}
			if(designation.Contains("National"))
			{
				this.LoadData("NSM");
			}
            if (designation.Contains("Store"))
            {
                this.LoadData("SIC");
            }
            if (designation.Contains("Dispatch"))
            {
                this.LoadData("DO");

            }
            if (designation.Equals("Admin"))
            {
                this.LoadData("Admin");
            }
            if (designation.Equals("Accountant"))
            {
                this.LoadData("Accountant");
            }
            if (designation.Equals("Co-ordinator"))
            {
                this.LoadData("CO");
            }
            if (designation.Equals("Viewer"))
            {
                this.LoadData("Viewer");
            }
			 //close the loading screen that showed the user that data is being loaded.
            this.LoadingScreen.Visibility = Visibility.Collapsed;
            _context.Load(_context.GetInventoryByRegionIdQuery(CurrentRegion.RegionId)).Completed += new EventHandler(InventoryLoad_Completed);
    
			
        }
        /// <summary>
        /// Loads the inventories into the entity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void InventoryLoad_Completed(object sender, EventArgs e)
        {
            if (UserInfo.Designation == "Store In Charge" || UserInfo.Designation == "Dispatch Officer")
            {
                UserInfo.Inventory = new Inventory();
                UserInfo.Inventory = (sender as LoadOperation<Inventory>).Entities.SingleOrDefault();
            }
            // MessageBox.Show(UserInfo.Inventory.InventoryId.ToString());
        }
       
		/// <summary>
		/// Loads data for tree view based on designation.
		/// </summary>
		/// <param name="designation"></param>
		private void LoadData(string designation)
		{
            string fileName = "TreeViewDatas/treeview" + designation + ".xml";
			XDocument treeviewXML = XDocument.Load(fileName);
			//
			treemenus = this.GetSubMenus( treeviewXML.Element( "treeitems" ) );
			//
			this.sideTreeView.ItemsSource=treemenus;
		}
		/// <summary>
		/// Helper function for LoadData.
		/// </summary>
		/// <param name="element"></param>
		/// <returns></returns>
		private List<TreeViewItem> GetSubMenus( XElement element )
		{
			return ( from treeitem in element.Elements( "treeitem" )
					select new TreeViewItem()
					{
						Title = treeitem.Attribute( "name" ).Value,
						URI = treeitem.Attribute( "uri" ).Value,
						FontSize=Double.Parse(treeitem.Attribute( "fontsize" ).Value),
                        Icon=treeitem.Attribute("icon").Value,
						SubItems = this.GetSubMenus( treeitem )
					} ).ToList();
		
		}

        #region Events


		private void quickLinkClicked(object sender, System.Windows.RoutedEventArgs e)
		{
            this.floatingMenuStack.Children.Clear();
            HyperlinkButton button = sender as HyperlinkButton;
            if (button.Content.Equals("Create"))
            {
                //A temporary button.
                HyperlinkButton temp = new HyperlinkButton();
                temp.FontSize = 14.0;
                temp.Foreground = new SolidColorBrush(Colors.White);

                if (UserInfo.Designation.Contains("National"))
                {
                    
                    temp.Content = "Create Season";
                    temp.Click += new RoutedEventHandler(optionClicked);
                }
                else//default
                {
                    temp.Content = "Items will be added soon.";
                }
                //
                this.floatingMenuStack.Children.Add(temp);
            }
            else if (button.Content.Equals("Settings"))
            {
                //change password button.
                HyperlinkButton changePassword = new HyperlinkButton();
                changePassword.FontSize = 14.0;
                changePassword.Foreground = new SolidColorBrush(Colors.White);
                changePassword.Content = "Change Password";
                changePassword.Click+=new RoutedEventHandler(optionClicked);
                //log out button.
                HyperlinkButton logout = new HyperlinkButton();
                logout.FontSize = 14.0;
                logout.Foreground = new SolidColorBrush(Colors.White);
                logout.Content = "Log Out";
                logout.Click += new RoutedEventHandler(optionClicked);
                //reload button
                HyperlinkButton reload = new HyperlinkButton();
                reload.FontSize = 14.0;
                reload.Foreground = new SolidColorBrush(Colors.White);
                reload.Content = "Reload Page";
                reload.Click += new RoutedEventHandler(optionClicked);
                //add these to menu
                this.floatingMenuStack.Children.Add(changePassword);
				this.floatingMenuStack.Children.Add(reload);
                this.floatingMenuStack.Children.Add(logout);
            }
			//show the floating menu
            this.floatingMenu.Visibility = Visibility.Visible;
		}
        /// <summary>
        /// Once the change password option is selected from settings options.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optionClicked(object sender, System.Windows.RoutedEventArgs e)
        {
            //floating menu should be closed.
            this.floatingMenu.Visibility = Visibility.Collapsed;
            HyperlinkButton button= sender as HyperlinkButton;
            if (button.Content.Equals("Change Password"))
            {
                ChangePasswordView view = new ChangePasswordView();
                view.Show();
            }
            else if (button.Content.Equals("Log Out"))
            {
                WebContext.Current.Authentication.Logout(false).Completed+=new EventHandler(MainViewControl_Completed);
            }
            else if (button.Content.Equals("Reload Page"))
            {
                this.ContentFrame.Refresh();
            }
            else if (button.Content.Equals("Create Season"))
            {
              /*  CreateSeason seasonCreator = new CreateSeason();
                seasonCreator.Show();*/
            }
        }
		/// <summary>
		/// When the close button on floating menu for options(create,settings,calender) 
        /// get pressed.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void floatingMenuCloseButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			//floating menu is visible,close it.
			this.floatingMenu.Visibility=Visibility.Collapsed;
		}

        void Mouse_RightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void ContextMenuClicked(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            if (item.Header.Equals("Reload Page"))
            {
                this.ContentFrame.Refresh();
            }
            else
            {
                if (this.ContentFrame.CanGoBack)
                {
                    this.ContentFrame.GoBack();
                }
            }
        }
			
		#endregion

        private void home_click(object sender, RoutedEventArgs e)
        {
            this.ContentFrame.Navigate(new Uri("/Home",UriKind.Relative));
        }

        private void report_click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new Uri("/ReportsView", UriKind.Relative));
        }

        private void logout_click(object sender, RoutedEventArgs e)
        {
            WebContext.Current.Authentication.Logout(false).Completed += new EventHandler(MainViewControl_Completed);
        }

        private void settings_click(object sender, RoutedEventArgs e)
        {
            ChangePasswordView view = new ChangePasswordView();
            view.Show();
        }
    }
	/// <summary>
	/// A private class.
	/// Represents the tree view items in side menu view
	/// </summary>
	public class TreeViewItem
	{
		public string Title{get;set;}
		public string URI{get;set;}
		public double FontSize{get;set;}
        public string Icon { get;set;}
		public List<TreeViewItem> SubItems{get;set;}
	}
}