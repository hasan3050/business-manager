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

namespace TrialBusinessManager.Views
{
    public partial class CreateSeason : ChildWindow
    {
        AgroDomainContext _context = new AgroDomainContext();
        DateTime NullDate = DateTime.MinValue;
        DateTime start;
        DateTime end;
        Season selectedSeason = null;
        string[] Months = { "Jan", "Feb", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        public CreateSeason()
        {
            InitializeComponent();
            this.start = this.NullDate;
            this.end = this.NullDate;
            this.LoadData();
        }
        private void LoadData()
        {
            _context.Load(_context.GetSeasonsQuery()).Completed += (s, e) =>
                {
                    this.seasonCombo.ItemsSource = _context.Seasons.OrderBy(season => season.SeasonEndDate);
                };
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
        /// <summary>
        /// Checks if the date is overlapping with existing season,alternative of isWithinSeason
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        private bool isOverlapping(DateTime start)
        {
            bool retVal = false;
            Season s = _context.Seasons.OrderBy(season => season.SeasonEndDate).First();
            if (this.isEarlier(start, s.SeasonEndDate))
            {
                retVal = true;
            }
            return retVal;
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
        /// Shows season info in text block
        /// </summary>
        private void showSeasonInfo()
        {
            this.seasonNameBlock.Text = start.Day + "," + Months[start.Month - 1] + "," + start.Year + "->" + end.Day + "," + Months[end.Month - 1] + "," + end.Year;
        }

        private void Create()
        {
            //ensures input is given.
            if (this.start.Equals(this.NullDate) || this.end.Equals(this.NullDate))
            {
                MessageBox.Show("You must select a Start and End date.");
            }
            else
            {
                //ensures right inout is given.
                if (this.CheckDateValidity())
                {
                    bool isConflicting=false;
                    foreach(Season season in _context.Seasons)
                    {
                        if(this.isWithinSeason(this.start,this.end,season))
                        {
                            isConflicting=true;
                            break;
                        }
                    }
                    //ensures if the input can be taken.
                    if (isConflicting)
                    {
                        MessageBox.Show("Conflicts with existing season.Select a different Start & End date.");
                    }
                    else
                    {
                        Season newSeason = new Season();
                        newSeason.SeasonStartDate = this.start;
                        newSeason.SeasonEndDate = this.end;
                        newSeason.SeasonName = this.start.Date + "----" + this.end.Date;
                        newSeason.IsCurrentSeason = false;
                        try
                        {
                            this.createButton.IsEnabled = false;
                            //
                            _context.Seasons.Add(newSeason);
                            _context.SubmitChanges().Completed += (s, es) =>
                            {
                                MessageBox.Show("Season created successfully.");
                                this.createButton.IsEnabled = true;
                            };
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error creating Season.Details:\n" + ex.Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Start date must be earlier than End date.");
                }
            }
        }

        private void Edit()
        {
            //ensures input is given.
            if (this.selectedSeason != null)
            {
                if (this.start.Equals(this.NullDate) || this.end.Equals(this.NullDate))
                {
                    MessageBox.Show("You must select a Start and End date.");
                }
                else
                {
                    //ensures right inout is given.
                    if (this.CheckDateValidity())
                    {
                        //ensures if the input can be taken.
                        if (this.isOverlapping(this.start))
                        {
                            MessageBox.Show("Conflicts with existing season.Select a different Start & End date.");
                        }
                        else
                        {
                            this.selectedSeason.SeasonStartDate = this.start;
                            this.selectedSeason.SeasonEndDate = this.end;
                            this.selectedSeason.SeasonName = this.start.Date + "----" + this.end.Date;
                            try
                            {
                                this.editButton.IsEnabled = false;
                                _context.SubmitChanges().Completed += (s, es) =>
                                {
                                    MessageBox.Show("Season edited successfully.");
                                    this.editButton.IsEnabled = true;
                                };
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error editing Season.Details:\n" + ex.Message);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Start date must be earlier than End date.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a season at first.");
            }
        }





        private void startDatePicker_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            this.start = this.NullDate;
            this.start = (DateTime)e.AddedItems[0];
            this.showSeasonInfo();
        }

        private void endDatePicker_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            this.end = this.NullDate;
            this.end = (DateTime)e.AddedItems[0];
            this.showSeasonInfo();
            
        }
        /// <summary>
        /// Checks if the start and end date is valid for creating season.
        /// </summary>
        /// <returns></returns>
        private bool CheckDateValidity()
        {
            bool retval = true;
            if (!this.isEarlier(this.start, this.end))
            {
                retval = false;
            }
            return retval;
        }

        private void seasonCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.selectedSeason = null;
            this.selectedSeason = (Season)e.AddedItems[0];
            if (this.selectedSeason != null)
            {
                this.startDatePicker_edit.SelectedDate = this.selectedSeason.SeasonStartDate;
                this.endDatePicker_Edit.SelectedDate = this.selectedSeason.SeasonEndDate;
            }
        }

        private void createButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Create();

        }


        private void editButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	this.Edit();
        }


    }
}

