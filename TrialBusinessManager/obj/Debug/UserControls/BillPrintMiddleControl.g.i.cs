﻿#pragma checksum "D:\Latest Version\Version5\businessmanager_original\TrialBusinessManager\UserControls\BillPrintMiddleControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "809AE305266DE2DEE2E3584EA531C02A"
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


namespace TrialBusinessManager.UserControls {
    
    
    public partial class BillPrintMiddleControl : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.StackPanel LayoutRoot;
        
        internal System.Windows.Controls.TextBlock WithOutCommTxtBlock;
        
        internal System.Windows.Controls.TextBlock WithCommTxtBlock;
        
        internal System.Windows.Controls.TextBlock WarningTxtBlock;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/TrialBusinessManager;component/UserControls/BillPrintMiddleControl.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.StackPanel)(this.FindName("LayoutRoot")));
            this.WithOutCommTxtBlock = ((System.Windows.Controls.TextBlock)(this.FindName("WithOutCommTxtBlock")));
            this.WithCommTxtBlock = ((System.Windows.Controls.TextBlock)(this.FindName("WithCommTxtBlock")));
            this.WarningTxtBlock = ((System.Windows.Controls.TextBlock)(this.FindName("WarningTxtBlock")));
        }
    }
}

