﻿using System;
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
using TrialBusinessManager.Models;

namespace TrialBusinessManager.Views.DO
{
    public partial class ProductTransfer : Page
    {
        public ProductTransfer()
        {
            
            InitializeComponent();
            InventoryTxt.Text = UserInfo.Inventory.InventoryName;
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

    }
}
