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

namespace TrialBusinessManager
{
    public partial class DetailedNoticeView : ChildWindow
    {
        NoticeBoardViewDelegate del;
        NoticeBoard notice;
        VIEW_MODE mode;
        ACCESS_ID accessLevel;
        public DetailedNoticeView(NoticeBoardViewDelegate del,ACCESS_ID level)
        {
            InitializeComponent();
            this.del = del;
            mode=VIEW_MODE.EDIT_MODE;//default
            accessLevel = level;
        }
        /// <summary>
        /// Can be set to view/edit only or creation mode.
        /// View/Edit mode shows a notice and enables to edit an existing one.
        /// Create mode is for creating new Notice.
        /// </summary>
        /// <param name="mode"></param>
        public void setViewMode(VIEW_MODE mode)
        {
            this.mode = mode;
        }
        /// <summary>
        /// Call when its time to create some notice.
        /// </summary>
        public void prepareForCreateMode()
        {
            this.editButton.Content = "Save";
            this.fromBlock.IsReadOnly = false;
            this.titleBlock.IsReadOnly = false;
            this.dateBlock.IsReadOnly = false;
            this.detailsBlock.IsReadOnly = false;
            this.DateBorder.Visibility = Visibility.Collapsed;
            this.editDatePane.Visibility = Visibility.Visible;
            this.fromBlock.Text = "";
            this.titleBlock.Text = "";
            this.dateBlock.Text = "";
            this.detailsBlock.Blocks.Clear();
        }
        /// <summary>
        /// Before calling this make sure that notice is set via setNoticeForView
        /// </summary>
        public void prepareForEditMode()
        {
            this.editButton.Content = "Edit";
            this.fromBlock.IsReadOnly = true;
            this.titleBlock.IsReadOnly = true;
            this.dateBlock.IsReadOnly = true;
            this.detailsBlock.IsReadOnly = true;
            this.DateBorder.Visibility = Visibility.Visible;
            this.editDatePane.Visibility = Visibility.Collapsed;
            if (accessLevel == ACCESS_ID.LIMITED_ACCESS)
            {
                //should not have the editing ability
                this.editButton.IsEnabled = false;
            }
            this.setUpDataForView();
           
        }
        /// <summary>
        /// Used to update the info label.
        /// </summary>
        /// <param name="info"></param>
        public void updateInfo(string info)
        {
            this.infoBlock.Text = info;
        }
        /// <summary>
        /// In case you need to refer notice explicitly in edit mode.
        /// </summary>
        /// <param name="notice"></param>
        public void setNoticeForView(NoticeBoard notice)
        {
            this.notice = notice;
            
        }
        //shows a notice in detail view.
        private void setUpDataForView()
        {
            this.fromBlock.Text = "National Sales Manager";
            this.dateBlock.Text = notice.StartDate.ToLongDateString() + "---" + notice.EndDate.ToLongDateString();
            this.titleBlock.Text = notice.NoticeSubject;
            Paragraph paragraph = new Paragraph();
            Run text = new Run();
            text.Text = notice.NoticeBody;
            paragraph.Inlines.Add(text);
            //at first clear the rich box
            this.detailsBlock.Blocks.Clear();
            this.detailsBlock.Blocks.Add(paragraph);
        }
        /*
        private void boldButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	// TODO: Add event handler implementation here.
            // change text
            if (this.detailsBlock != null && this.detailsBlock.Selection.Text.Length > 0)
            {
                if (this.detailsBlock.Selection.GetPropertyValue(Run.FontWeightProperty) is FontWeight && ((FontWeight)this.detailsBlock.Selection.GetPropertyValue(Run.FontWeightProperty))== FontWeights.Normal)
                {
                    this.detailsBlock.Selection.ApplyPropertyValue(Run.FontWeightProperty, FontWeights.Bold);
                }
                else
                {
                    this.detailsBlock.Selection.ApplyPropertyValue(Run.FontWeightProperty, FontWeights.Normal);
                }
            }

            // return focus
            if (this.detailsBlock != null)
            {
                this.detailsBlock.Focus();
            }
        }
        */
        private void buttonClicked(object sender, System.Windows.RoutedEventArgs e)
        {
            Button button=(Button)sender;
            if (button.Content.Equals("Back"))
            {
                this.DialogResult = true;
                this.del.Update();
                this.Close();
            }
            if (button.Content.Equals("Save"))
            {
                if (this.mode == VIEW_MODE.EDIT_MODE)
                {
                    saveButtonActionOnEditMode(button);
                }
                else
                {
                    saveButtonActionOnCreateMode(button);
                }
            }
            //this case is valid only when this is in edit mode.
            if (button.Content.Equals("Edit"))
            {
                this.detailsBlock.IsReadOnly = false;
                //disable the tool control
                //this.ToolControl.IsEnabled = true;
                button.Content = "Save";
            }
            
        }
        private void saveButtonActionOnCreateMode(Button button)
        {
            //empty it if it was from previous time
            this.notice = new NoticeBoard();
            DateTime startTime =(DateTime)this.startDatePicker.SelectedDate;
            DateTime endTime = (DateTime)this.endDatePicker.SelectedDate;
            //set notice body
            this.detailsBlock.SelectAll();
            this.notice.NoticeBody = this.detailsBlock.Selection.Text;
            //set notice title
            this.notice.NoticeSubject = this.titleBlock.Text;
            //set notice validity time
            this.notice.StartDate = startTime;
            this.notice.EndDate = endTime;
            this.notice.IsActive = true;
            this.notice.IssuedById = UserInfo.EmployeeID;
            this.del.AddNotice(this.notice);
            //empty it for further use
            this.notice = null;
        }
        private void saveButtonActionOnEditMode(Button button)
        {
            this.detailsBlock.IsReadOnly = true;
            //enable the tool control for bold,italic and other control,next feature!
            //this.ToolControl.IsEnabled = false;
            this.detailsBlock.SelectAll();
            notice.NoticeBody = this.detailsBlock.Selection.Text;
            //send this to delegate
            this.del.UpdatedNotice(this.notice);
            button.Content = "Edit";
        }
    }
}

