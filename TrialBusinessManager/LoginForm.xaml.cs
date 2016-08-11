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
using System.Windows.Browser;
using System.ServiceModel.DomainServices.Client.ApplicationServices;
using TrialBusinessManager.Models;

namespace TrialBusinessManager
{
    public partial class LoginForm : ChildWindow
    {

        public string CookieKey = "cred";
        public LoginForm()
        {
            InitializeComponent();
            this.InitLoginData();
        }

        

        #region Cookie handlers
        /// <summary>
        /// Prepare login window if the user had saved previous login.
        /// </summary>
        private void InitLoginData()
        {
            string data = this.GetCookie(this.CookieKey);
            if (data != null)
            {
                MessageBox.Show("Your login info is currently saved as you selected 'Remember Me' before.", "Welcome to Anvil Lab Business Manager", MessageBoxButton.OK);
                string[] infos = data.Split('@');
                if (infos.Length == 2)
                {
                    this.userNameField.Text = infos[0];
                    this.passWordField.Password = infos[1];
                }
            }
        }
        /// <summary>
        /// Sets the cookie.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        private void SetCookie(string key, string username, string password)
        {
            // Expire in 7 days
            DateTime expireDate = DateTime.Now + TimeSpan.FromDays(7);

            string newCookie = key + "=" + username + "@" + password + ";expires=" + expireDate.ToString("R");
            HtmlPage.Document.SetProperty("cookie", newCookie);
        }
        /// <summary>
        /// Gets the preset cookie, the username and password
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private string GetCookie(string key)
        {
            string[] cookies = HtmlPage.Document.Cookies.Split(';');

            foreach (string cookie in cookies)
            {
                string[] keyValue = cookie.Split('=');
                if (keyValue.Length == 2)
                {
                    if (keyValue[0].ToString() == key)
                    {
                        return keyValue[1];
                    }

                }
            }
            return null;
        }
        #endregion

        #region Helper Methods

        /// <summary>
        /// Checks if the control is visible.
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        private bool isVisible(System.Windows.UIElement control)
        {
            bool retVal = false;
            if (control.Visibility == System.Windows.Visibility.Visible)
            {
                retVal = true;
            }
            return retVal;

        }
        /// <summary>
        /// Controls the active state of login button in a swapping manner.
        /// </summary>
        private void setLoginButtonActivity()
        {
            this.loginButton.IsEnabled = !this.loginButton.IsEnabled;
        }
        /// <summary>
        /// Hides the specified control
        /// </summary>
        /// <param name="control"></param>
        private void Hide(System.Windows.UIElement control)
        {
            control.Visibility = System.Windows.Visibility.Collapsed;
        }
        /// <summary>
        /// Shows the specified control
        /// </summary>
        /// <param name="control"></param>
        private void Show(System.Windows.UIElement control)
        {
            control.Visibility = System.Windows.Visibility.Visible;
        }
        /// <summary>
        /// Shows error in validation block of login window.
        /// </summary>
        /// <param name="msg"></param>
        private void postValidationError(string msg)
        {
            this.validationField.Text = msg;
            this.validationBox.Visibility = System.Windows.Visibility.Visible;
        }
        /// <summary>
        /// Login function
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        private void login(string username, string password)
        {
            //set the cookie if the login is remembered.
            if (this.rememberMeBox.IsChecked == true)
            {
                this.SetCookie(this.CookieKey, username, password);
            }
            //if another login is not ongoing
            if (!WebContext.Current.Authentication.IsLoggingIn)
            {
                string hashPassword = MD5Core.GetString(MD5Core.GetHash(password));
                LoginOperation loginOp = WebContext.Current.Authentication.Login(
                new LoginParameters(username, hashPassword));
                //show the busy indicator.
                this.Show(this.CustomBusyIndicator);
                //deactivate the login button.
                this.setLoginButtonActivity();
                //
                loginOp.Completed += (s2, e2) =>
                {
                    //hide the busy indicator.
                    this.Hide(this.CustomBusyIndicator);
                    //activate the login button.
                    this.setLoginButtonActivity();
                    //
                    if (loginOp.HasError)
                    {
                        if (loginOp.Error.Message.Contains("HTTP"))
                        {
                            this.postValidationError("Host unreachable.Please check your internet connection.");
                        }
                        else
                        {
                            this.postValidationError("Unknown internal error.Please retry.");
                        }
                        loginOp.MarkErrorAsHandled();
                        return;
                    }
                    else if (!loginOp.LoginSuccess)
                    {
                        this.postValidationError("Login failed.Try again.");
                        return;
                    }
                    else
                    {
                        //login completed successfully,login window must get collapsed.
                        this.login_Closed(s2, e2);
                        StaticMessaging.IsLoggedIn = true;
                    }

                };
            }

        }
        /// <summary>
        /// Called once the login is successful.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void login_Closed(object sender, EventArgs e)
        {
            this.DialogResult = true;
            

        }
        #endregion

        #region Events

        /// <summary>
        /// Fired when user clicks login in main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loginButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //if the validation box was visible previously
            if (this.isVisible(this.validationBox))
            {
                //hide the box.
                this.Hide(this.validationBox);
            }
            this.login(this.userNameField.Text, this.passWordField.Password);
        }

        private void userNameField_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            //user name field got focus. erase the default text.
            if (this.userNameField.Text.Equals("username..."))
            {
                this.userNameField.Text = "";
            }

        }


        private void userNameField_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            //focus is lost.set the default text again
            if (this.userNameField.Text.Equals(""))
            {
                this.userNameField.Text = "username...";
            }
        }

        private void passWordField_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            //focus received.remove prev data.
            if (this.passWordField.Password.Equals("12345"))
            {
                this.passWordField.Password = "";
            }
        }

        private void passWordField_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            //focus lost.set prev data again.
            if (this.passWordField.Password.Equals(""))
            {
                this.passWordField.Password = "12345";
            }
        }

        private void LoginView_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //if the validation box was visible previously
                if (this.isVisible(this.validationBox))
                {
                    //hide the box.
                    this.Hide(this.validationBox);
                }
                if (!this.userNameField.Text.Trim().Equals("") && !this.passWordField.Password.Trim().Equals("") && !this.userNameField.Text.Trim().Contains("username") && !this.passWordField.Password.Trim().Equals("12345"))
                {
                    this.login(this.userNameField.Text, this.passWordField.Password);
                }
            }
        }
        #endregion
    }
}

