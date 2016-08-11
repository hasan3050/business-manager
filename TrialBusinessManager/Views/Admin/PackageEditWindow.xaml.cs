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
using System.ServiceModel.DomainServices.Client;

namespace TrialBusinessManager.Views.Admin
{
    public partial class PackageEditWindow : ChildWindow
    {
        AgroDomainContext _context = new AgroDomainContext();
        Package Package = new Package();
        BusyWindow Busy = new BusyWindow();
        
        public PackageEditWindow(AgroDomainContext Context,Package MyPackage)
        {
            InitializeComponent();
            _context = Context;
            Package = MyPackage;
            NameTxt.Text = MyPackage.PackageName;
            CodeTxt.Text = MyPackage.PackageCode;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (_context.Packages.Any(c => c.PackageName.Equals(NameTxt.Text)))
            {
                MessageBox.Show("Package Name must be unique.");
                return;
            }

            if (_context.Packages.Any(c => c.PackageCode.Equals(CodeTxt.Text)))
            {
                MessageBox.Show("Package Name must be unique.");
                return;
            }

            Package.PackageName = NameTxt.Text;
            Package.PackageCode = CodeTxt.Text;
            
            Busy.Show();
            _context.SubmitChanges(OnSubmitCompleted,null);
        }

        void OnSubmitCompleted(SubmitOperation so)
        {
            Busy.Close();

            if (so.HasError)
            {
                MessageBox.Show("package edit failed, please try again");
                this.DialogResult = false;
            }
            else
            {
                MessageBox.Show("package edited!!");
                this.DialogResult = true;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

