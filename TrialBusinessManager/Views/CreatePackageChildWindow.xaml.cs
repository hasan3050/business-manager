using System;
using System.Net;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight;
using TrialBusinessManager.Web;
using GalaSoft.MvvmLight.Command;
using System.ServiceModel.DomainServices.Client;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using TrialBusinessManager.Models;
using TrialBusinessManager.Views;

namespace TrialBusinessManager.Views
{
    public partial class CreatePackageChildWindow : ChildWindow
    {
        AgroDomainContext _context = new AgroDomainContext();
        bool LoadingFlag = false;

        public CreatePackageChildWindow()
        {
            InitializeComponent();
            TurnToDefaultUI();
            LoadPackages();
        }

        void TurnToDefaultUI()
        {
            OKButton.IsEnabled = false;
            Namelabel.Visibility = Visibility.Collapsed;
            Codelabel.Visibility = Visibility.Collapsed;
        
        }

        void LoadPackages()
        {
            _context.Load(_context.GetPackagesQuery()).Completed += (s, args) => { LoadingFlag = true; };
        
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            Package NewPackage = new Package();
            NewPackage.PackageName = NametextBox.Text;
            NewPackage.PackageCode = CodetextBox.Text;
            NewPackage.PackageStatus = "Active";
            NewPackage.AdminId = UserInfo.EmployeeID;
            NewPackage.NSMId = UserInfo.EmployeeID;
            NewPackage.IntroducedDate = DateTime.Now;
            _context.Packages.Add(NewPackage);
            _context.SubmitChanges().Completed += (s, args) => 
            { 
                //NametextBox.Text = ""; CodetextBox.Text = ""; TurnToDefaultUI(); 
                this.DialogResult = true;
            };
           
        }


        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }


        private void NametextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_context.Packages.Any(r => r.PackageName == NametextBox.Text))
            {
                if( NametextBox.Text=="")
                     return;
                Namelabel.Visibility = Visibility.Visible;
                OKButton.IsEnabled = false;
                return;

            }
            else 
            {
                Namelabel.Visibility = Visibility.Collapsed;
                if (CheckValidation() == true)
                {
                    OKButton.IsEnabled = true;
                }
            }

        }

        private void CodetextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (_context.Packages.Any(r => r.PackageCode == CodetextBox.Text))
            {
                if(CodetextBox.Text=="")
                    return;
                Codelabel.Visibility = Visibility.Visible;
                OKButton.IsEnabled = false;
                return;

            }
            else
            {
                Codelabel.Visibility = Visibility.Collapsed;
                if (CheckValidation() == true)
                {
                    OKButton.IsEnabled = true;
                }
            }
        }

        bool CheckValidation()
        {
            if (!_context.Packages.Any(r => r.PackageName == NametextBox.Text || r.PackageCode == CodetextBox.Text || CodetextBox.Text == "" || NametextBox.Text==""))
            {
                Namelabel.Visibility = Visibility.Collapsed;
                Codelabel.Visibility = Visibility.Collapsed;
                return true;
            }
            else
                return false;
        }
    }
}

