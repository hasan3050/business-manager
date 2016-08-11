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
    public partial class SalesReturnDetailChildWindow : ChildWindow
    {
        public SalesReturnDetailChildWindow()
        {
            InitializeComponent();
        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Application.Current.RootVisual.SetValue(IsEnabledProperty, true);

        }

        private void CancelButton_Click_1(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}

