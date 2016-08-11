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

namespace TrialBusinessManager.Views
{
    public partial class ConnectivityErrorWindow : ChildWindow
    {
        public ConnectivityErrorWindow()
        {
            InitializeComponent();
        }

        public ConnectivityErrorWindow(String ErrorMessage)
        {
            InitializeComponent();
            ErrorText.Text = ErrorMessage;
            this.Title = "Error!!";
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

