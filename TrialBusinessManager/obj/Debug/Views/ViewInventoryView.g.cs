﻿#pragma checksum "D:\Somee Version\Version5\businessmanager_original\TrialBusinessManager\Views\ViewInventoryView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "F18D4C9823410972D881DC0D7601BBB6"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace TrialBusinessManager.Views {
    
    
    public partial class ViewInventoryView : System.Windows.Controls.Page {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.TabControl myTabControl;
        
        internal System.Windows.Controls.TabItem ProductTabItem;
        
        internal System.Windows.Controls.AutoCompleteBox autoCompleteBox1;
        
        internal System.Windows.Controls.TextBlock textBlock1;
        
        internal System.Windows.Controls.DataGrid dataGrid1;
        
        internal System.Windows.Controls.DataVisualization.Charting.Chart PieChart;
        
        internal System.Windows.Controls.DataVisualization.Charting.Chart BarChart;
        
        internal System.Windows.Controls.TabItem PackageTabItem;
        
        internal System.Windows.Controls.Grid Grid2;
        
        internal System.Windows.Controls.AutoCompleteBox autoCompleteBox2;
        
        internal System.Windows.Controls.TextBlock textBlock2;
        
        internal System.Windows.Controls.DataGrid dataGrid2;
        
        internal System.Windows.Controls.DataVisualization.Charting.Chart PackagePieChart;
        
        internal System.Windows.Controls.DataVisualization.Charting.Chart PackageBarChart;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/TrialBusinessManager;component/Views/ViewInventoryView.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.myTabControl = ((System.Windows.Controls.TabControl)(this.FindName("myTabControl")));
            this.ProductTabItem = ((System.Windows.Controls.TabItem)(this.FindName("ProductTabItem")));
            this.autoCompleteBox1 = ((System.Windows.Controls.AutoCompleteBox)(this.FindName("autoCompleteBox1")));
            this.textBlock1 = ((System.Windows.Controls.TextBlock)(this.FindName("textBlock1")));
            this.dataGrid1 = ((System.Windows.Controls.DataGrid)(this.FindName("dataGrid1")));
            this.PieChart = ((System.Windows.Controls.DataVisualization.Charting.Chart)(this.FindName("PieChart")));
            this.BarChart = ((System.Windows.Controls.DataVisualization.Charting.Chart)(this.FindName("BarChart")));
            this.PackageTabItem = ((System.Windows.Controls.TabItem)(this.FindName("PackageTabItem")));
            this.Grid2 = ((System.Windows.Controls.Grid)(this.FindName("Grid2")));
            this.autoCompleteBox2 = ((System.Windows.Controls.AutoCompleteBox)(this.FindName("autoCompleteBox2")));
            this.textBlock2 = ((System.Windows.Controls.TextBlock)(this.FindName("textBlock2")));
            this.dataGrid2 = ((System.Windows.Controls.DataGrid)(this.FindName("dataGrid2")));
            this.PackagePieChart = ((System.Windows.Controls.DataVisualization.Charting.Chart)(this.FindName("PackagePieChart")));
            this.PackageBarChart = ((System.Windows.Controls.DataVisualization.Charting.Chart)(this.FindName("PackageBarChart")));
        }
    }
}

