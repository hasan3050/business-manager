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
using System.ServiceModel.DomainServices.Client.ApplicationServices;
using TrialBusinessManager.Views;
using TrialBusinessManager.Models;

namespace TrialBusinessManager
{
    public partial class App : Application
    {
        bool ExceptionFlag = false;
        public App()
        {

            this.Startup += this.Application_Startup;
            this.UnhandledException += this.Application_UnhandledException;



            InitializeComponent();
            WebContext context = new WebContext();
            context.Authentication = new FormsAuthentication();
            ApplicationLifetimeObjects.Add(context);

        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            this.RootVisual = new MainPage();
            StaticMessaging.IsErrorWindowShowing = false;
            StaticMessaging.IsLoggedIn = false;
        }
        
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            
        /*    e.Handled = true;
            if (!ExceptionFlag)
            {
                ExceptionFlag = true;
                ConnectivityErrorWindow ErrorWindow = new ConnectivityErrorWindow();
                ErrorWindow.Closed += new EventHandler(ErrorWindow_Closed);
                ErrorWindow.Show();
            }
         */
             MessageBox.Show(e.ExceptionObject.Message);
            // If the app is running outside of the debugger then report the exception using
            // a ChildWindow control.
            if (!System.Diagnostics.Debugger.IsAttached)
             {
                 // NOTE: This will allow the application to continue running after an exception has been thrown
                 // but not handled. 
                 // For production applications this error handling should be replaced with something that will 
                 // report the error to the website and stop the application.
                 e.Handled = true;
                 ChildWindow errorWin = new ErrorWindow(e.ExceptionObject);
                 errorWin.Show();
             }
        }

        void ErrorWindow_Closed(object sender, EventArgs e)
        {
            ExceptionFlag = false;
        }
         
    }
}