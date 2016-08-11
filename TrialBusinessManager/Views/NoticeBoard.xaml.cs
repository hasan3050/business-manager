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
using TrialBusinessManager.Views;
namespace TrialBusinessManager
{
    //defines the user privilege for this user
    public enum ACCESS_ID{FULL_ACCESS = 523,//for NSM only.
        LIMITED_ACCESS};
    //defines how the details view will pop up,with edit or create capability
    public enum VIEW_MODE { EDIT_MODE,CREATE_MODE};
    /// <summary>
    /// Via this delegate the DetailedNoticeView child window contact to this class.
    /// </summary>
    public interface NoticeBoardViewDelegate
    {
        void UpdatedNotice(NoticeBoard notice);
        void AddNotice(NoticeBoard notice);
        void Update();
    }
    
    public partial class NoticeBoardView : Page, NoticeBoardViewDelegate
    {
        //red is for error, blue is for confirmation
        SolidColorBrush RED = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
        SolidColorBrush BLUE = new SolidColorBrush(Color.FromArgb(255, 0, 0, 255));
        //
        AgroDomainContext _context = new AgroDomainContext();
        /// <summary>
        /// This view is responsible for showing up the details of the notice and edit it.
        /// </summary>
        DetailedNoticeView detailView; 
        ///
        /// The following 2 collections have one to one mapping. Once a noticeInfo object is selected in
        /// datagrid it invokes the details view for corresponding notice objects using simple array indexing.
        ///
        //these objects are model for details view
        ObservableCollection<NoticeBoard> notices = new ObservableCollection<NoticeBoard>();
        //these objects are model for datagrid view
        ObservableCollection<NoticeInfo> noticeInfos = new ObservableCollection<NoticeInfo>();
        //this is the collection of objects to be deleted from the datagrid
        ObservableCollection<NoticeInfo> infosToDelete = new ObservableCollection<NoticeInfo>();
        //how the view can be manipulated are fixed by these identities

        ACCESS_ID accessLevel;
        
        public NoticeBoardView()
        {
            InitializeComponent();
            this.LoadData();
            //check which type of user is this.
            if (UserInfo.Designation.Contains("National"))
            {
                accessLevel =ACCESS_ID.FULL_ACCESS;
            }
            else
            {
                accessLevel =ACCESS_ID.LIMITED_ACCESS;//default
                this.createButton.Visibility = Visibility.Collapsed;//create functionality will be absent
            }
            //initialize the detail view object with this as delegate.
            detailView = new DetailedNoticeView(this,this.accessLevel);
            
        }
        #region HouseKeeping
        private void LoadData()
        {
            //show the busyindicator
          //  this.busyIndicator.Visibility = Visibility.Visible;
            _context.Load(_context.GetNoticeBoardsQuery()).Completed += new EventHandler(NoticeBoardView_Completed);
           
        }

        void NoticeBoardView_Completed(object sender, EventArgs e)
        {
            try
            {
                this.initDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not load notices." + ex.Message + ".Reload.");
            }
        }
        /// <summary>
        /// Shows specified information at the info block.
        /// </summary>
        /// <param name="info"></param>
        private void updateInfo(string info,Brush color)
        {

        }
        /// <summary>
        /// inits the datagrid
        /// </summary>
        private void initDataGrid()
        {
          
            this.notices.Clear();
            this.noticeInfos.Clear();
             
              /*  
                foreach (NoticeBoard notice in _context.NoticeBoards)
                {
                    //add it to notice datamodel first
                  /*  this.notices.Add(notice);
                    
                    NoticeInfo info = new NoticeInfo();
                    info.Title = notice.NoticeSubject;
                    info.IsActive = notice.IsActive;
                    info.ID = notice.NoticeId.ToString();
                    info.Details = notice.NoticeBody;
                    this.noticeInfos.Add(info);
                    
                }
                if (this.noticeInfos.Count == 0)
                {
                    MessageBox.Show("Failed to represent noticeboard visual.Please reload.");
                    
                }
              //  noticeList.ItemsSource = this.noticeInfos;*/

                dataGrid1.ItemsSource = _context.NoticeBoards;
           
        }
        #endregion

        #region Delegate Method
        /// <summary>
        /// Fired once a notice gets edited in the childview.
        /// </summary>
        /// <param name="notice"></param>
        public void UpdatedNotice(NoticeBoard notice)
        {
            var allNotice = _context.NoticeBoards;
            foreach (NoticeBoard not in allNotice)
            {
                //update the notice in main database
                if (not.NoticeId == notice.NoticeId)
                {
                    not.NoticeBody = notice.NoticeBody;
                    break;
                }
            }
            try
            {
                _context.SubmitChanges();
                this.detailView.updateInfo("Your changes were saved successfully!");
            }
            catch (Exception ex)
            {
                this.detailView.updateInfo("Could not save your change.Retry!");
            }
        }
        /// <summary>
        /// Adds a notice to the notice list.
        /// </summary>
        /// <param name="notice"></param>
        public void AddNotice(NoticeBoard notice)
        {
          //  var allNotice = _context.NoticeBoards;
            _context.NoticeBoards.Add(notice);
            try
            {
                _context.SubmitChanges();
                this.detailView.updateInfo("Notice added successfully!");
            }
            catch (Exception ex)
            {
                this.detailView.updateInfo("Could not add the notice.Retry!");
            }

        }
        /// <summary>
        /// Fired each time the childview gets closed and so an auto refresh needs to take place.
        /// </summary>
        public void Update()
        {
            this.refresh();
        }
        #endregion

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        #region Notice info Class
        public class NoticeInfo
        {

            public string ID { get; set; }
            public string Title { get; set; }
            public string Details { get; set; }
            public bool IsActive { get; set; }
        }
        #endregion
        #region events
        private void reloadButtonClicked(object sender, RoutedEventArgs e)
        {
            refresh();
        }
        /// <summary>
        /// refreshes the datagrid
        /// </summary>
        private void refresh()
        {
           
        }
        
       
        private void deleteButtonPressed(object sender, RoutedEventArgs e)
        {
           
                try
                {
                    _context.NoticeBoards.Remove((NoticeBoard)dataGrid1.SelectedItem);
                    _context.SubmitChanges();

                    this.updateInfo("Deleted successfully!", BLUE);
                    
                }
                catch (Exception ex)
                {
                    this.updateInfo("Could not delete.Retry!",RED );
                }
                //now reload everything.
                this.refresh();
          
        }

        private void detailsButtonPressed(object sender, System.Windows.RoutedEventArgs e)
        {
            NoticeInfo info = this.dataGrid1.SelectedItem as NoticeInfo;
            NoticeBoard selectedNotice = null;
            foreach (NoticeBoard notice in this.notices)
            {
                if (notice.NoticeId==long.Parse(info.ID))
                {
                    //the notice is found,show it.
                    selectedNotice = notice;
                    break;
                }
            }
            //set the selected notice for showing in detail view
            this.detailView.setNoticeForView(selectedNotice);
            this.detailView.setViewMode(VIEW_MODE.EDIT_MODE);
            this.detailView.prepareForEditMode();
            //show it with the specified notice.
            this.detailView.Show();
            
        }
        //when any checkbox for deletion is selected
        private void checkBoxOnCellClicked(object sender, System.Windows.RoutedEventArgs e)
        {
            CheckBox chbkDelete = sender as CheckBox;
            bool? res = chbkDelete.IsChecked;
            if ((bool)res)
            {
                this.infosToDelete.Add(this.dataGrid1.SelectedItem as NoticeInfo);
            }
            else
            {
                this.infosToDelete.Remove(this.dataGrid1.SelectedItem as NoticeInfo);
            }
        }

        private void createButtonPressed(object sender, System.Windows.RoutedEventArgs e)
        {
            this.detailView.setViewMode(VIEW_MODE.CREATE_MODE);
            this.detailView.prepareForCreateMode();
            this.detailView.Show();
        }
        #endregion

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewNoticeDetailsChildWindow obj = new ViewNoticeDetailsChildWindow((NoticeBoard)dataGrid1.SelectedItem);
                obj.Show();
            }
            catch (Exception ex)
            { 
            
            }
        }

    }
}
