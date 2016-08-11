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

namespace TrialBusinessManager.Views
{
    public partial class ViewNoticeDetailsChildWindow : ChildWindow
    {
        public ViewNoticeDetailsChildWindow(NoticeBoard Notice)
        {
            InitializeComponent();
            DatetextBox.Text = Notice.StartDate.ToShortDateString();
            SubjecttextBox.Text = Notice.NoticeSubject;
            BodytextBox.Text = Notice.NoticeBody;
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

