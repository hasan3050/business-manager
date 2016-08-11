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

namespace TrialBusinessManager
{
    public partial class MainPage : UserControl
    {
        //this is used to check if the log out is happening,so all home cleaning should be
        //done on it's log out happening call.
        private readonly AuthenticationService _AuthService = WebContext.Current.Authentication;
        
        public MainPage()
        {
            InitializeComponent();
            this.Loaded += MainPage_Loaded;
            _AuthService.LoggedOut += new EventHandler<AuthenticationEventArgs>(AuthService_LoginChanged);
        }
        private void AuthService_LoginChanged(object sender, AuthenticationEventArgs e)
        {
            HtmlPage.Document.Submit();
        }


        /* zoom level checking...
        App.Current.Host.Content.Zoomed += (s, e) =>
        {
            double factor=App.Current.Host.Content.ZoomFactor;
            if(factor>1)
            {
                //restricting the app zooming when browser zoom is greater than 1.
            }
			
        };
         * */

        /// <summary>
        /// Fired once the main screen gets loaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!WebContext.Current.Authentication.User.Identity.IsAuthenticated)
            {
                LoginForm login = new LoginForm();
                login.Closed += (s, e2) =>
                {
                    MainView.show();
                };
                login.Show();
            }
        }
    }
		
		
}