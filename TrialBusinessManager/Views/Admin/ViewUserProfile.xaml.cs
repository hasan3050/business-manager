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


namespace TrialBusinessManager.Views.Admin
{
    public partial class ViewUserProfile : Page
    {
        AgroDomainContext _context = new AgroDomainContext();
        public ViewUserProfile()
        {
            InitializeComponent();
            _context.Load(_context.GetEmployeesQuery().Where(e=>e.EmployeeId==UserInfo.EmployeeID));
        }

        
    }
}
