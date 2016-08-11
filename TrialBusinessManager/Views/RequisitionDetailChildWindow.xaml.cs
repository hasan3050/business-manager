using System.Windows;
using System.Windows.Controls;
using System;


namespace TrialBusinessManager.Views
{
    public partial class RequisitionDetailChildWindow: ChildWindow
    {

        public RequisitionDetailChildWindow()
        {
            InitializeComponent();           
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Application.Current.RootVisual.SetValue(IsEnabledProperty, true);

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

