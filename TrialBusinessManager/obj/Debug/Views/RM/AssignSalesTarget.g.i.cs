﻿#pragma checksum "D:\Latest Version\Version5\businessmanager_original\TrialBusinessManager\Views\RM\AssignSalesTarget.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "53E53B623BCB43AFCFA0E14BFC55D320"
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


namespace TrialBusinessManager.Views.RM {
    
    
    public partial class AssignSalesTarget : System.Windows.Controls.ChildWindow {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Button CancelButton;
        
        internal System.Windows.Controls.Button OKButton;
        
        internal System.Windows.Controls.TextBox ProductTxtBox;
        
        internal System.Windows.Controls.DataGrid dataGrid;
        
        internal System.Windows.Controls.TextBox TotalTextBox;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/TrialBusinessManager;component/Views/RM/AssignSalesTarget.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.CancelButton = ((System.Windows.Controls.Button)(this.FindName("CancelButton")));
            this.OKButton = ((System.Windows.Controls.Button)(this.FindName("OKButton")));
            this.ProductTxtBox = ((System.Windows.Controls.TextBox)(this.FindName("ProductTxtBox")));
            this.dataGrid = ((System.Windows.Controls.DataGrid)(this.FindName("dataGrid")));
            this.TotalTextBox = ((System.Windows.Controls.TextBox)(this.FindName("TotalTextBox")));
        }
    }
}

