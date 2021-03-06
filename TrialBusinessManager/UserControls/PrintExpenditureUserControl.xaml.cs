﻿using System;
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


namespace TrialBusinessManager.UserControls
{
    public partial class PrintExpenditureUserControl : UserControl
    {
        public PrintExpenditureUserControl(AgroDomainContext SentContext,Expenditure SentExpenditure)
        {
            InitializeComponent();
            dataGrid1.ItemsSource = SentContext.ExpenditureInfos.Where(e=>e.ExpenditureId==SentExpenditure.ExpenditureId);
            RegionNameTxtBox.Text = SentContext.Regions.Where(e=>e.RegionId==SentExpenditure.RegionId).First().RegionName;
            issueDatetextbox.Text = SentExpenditure.DateOfIssue.ToString();
            ExCodetextBox1.Text = SentExpenditure.ExpenditureCode.ToString();
           }
    }
}
