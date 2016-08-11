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

namespace TrialBusinessManager
{
    public partial class ChangePasswordView : ChildWindow
    {
        string newPassword = "";
        AgroDomainContext _context = new AgroDomainContext();
        Employee employee;
        public ChangePasswordView()
        {
            InitializeComponent();
            //load the user info for this user.
            _context.Load(_context.GetEmployeesByUsernameQuery(UserInfo.Username)).Completed+=(sender,e)=>
                {
                    LoadEmployee();
                };
        }
        /// <summary>
        /// Load the current employee.
        /// </summary>
        private void LoadEmployee()
        {
            employee = _context.Employees.FirstOrDefault();
            if (employee != null)
            {
                this.OKButton.IsEnabled = true;
            }
            else
            {
                errorField.Text = "Severe error!Close this by clicking 'Cancel',then reopen.";
                errorBlock.Visibility = Visibility.Visible;
            }
        }
        /// <summary>
        /// The main method which is responsible for password changing
        /// </summary>
        private void changePassword()
        {
            
            //if current password is correctly given
            if (MD5Core.GetString(MD5Core.GetHash(this.currentPasswordField.Password.Trim())).Equals(employee.Password))
            {
                string newFieldData = newPasswordField.Password.Trim();
                string confirmFieldData = confirmPasswordField.Password.Trim();
                //if new and confirm fields are not empty
                if (newFieldData.Length != 0 && confirmFieldData.Length != 0)
                {
                    //if new == confirmed
                    if (newFieldData.Equals(confirmFieldData))
                    {
                        //save this password after hashing
                        employee.Password = MD5Core.GetString(MD5Core.GetHash(newFieldData));
                        try
                        {
                            _context.SubmitChanges();
                            //successful
                            this.DialogResult = true;
                        }
                        catch (Exception ex)
                        {
                            errorField.Text = "Password saving failed.Retry!";
                            errorBlock.Visibility = Visibility.Visible;
                        }

                    }
                    else
                    {
                        errorField.Text = "The password in new and confirm field must match.";
                        errorBlock.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    errorField.Text = "New password/confirm password field can't be empty.";
                    errorBlock.Visibility = Visibility.Visible;
                }
            }
            else
            {
                errorField.Text = "Current password does not match.";
                errorBlock.Visibility = Visibility.Visible;
            }
        }

        #region Events

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            changePassword();
        }
        
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
        /// <summary>
        /// Once any input field get focus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void inputFieldFocused(object sender, System.Windows.RoutedEventArgs e)
        {
            //if the error pane was opened, should be closed.
            errorField.Text = "";
            errorBlock.Visibility = Visibility.Collapsed;
        }
        /// <summary>
        /// Once the enter key is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void keyPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                changePassword();
            }
        }
        #endregion
    }
}

