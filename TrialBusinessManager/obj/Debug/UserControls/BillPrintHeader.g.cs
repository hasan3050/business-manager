﻿#pragma checksum "D:\Latest Version\Version5\businessmanager_original\TrialBusinessManager\UserControls\BillPrintHeader.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "DE84BEE869C8A3EC1D7565CCBD0DCF45"
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
using TrialBusinessManager.UserControls;


namespace TrialBusinessManager.UserControls {
    
    
    public partial class BillPrintHeader : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.StackPanel LayoutRoot;
        
        internal TrialBusinessManager.UserControls.DealerInfoControl DealerControl;
        
        internal TrialBusinessManager.UserControls.PrintBillInfoControl BillControl;
        
        internal System.Windows.Controls.TextBlock textBlock1;
        
        internal System.Windows.Controls.TextBox PaymentMethodTxt;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/TrialBusinessManager;component/UserControls/BillPrintHeader.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.StackPanel)(this.FindName("LayoutRoot")));
            this.DealerControl = ((TrialBusinessManager.UserControls.DealerInfoControl)(this.FindName("DealerControl")));
            this.BillControl = ((TrialBusinessManager.UserControls.PrintBillInfoControl)(this.FindName("BillControl")));
            this.textBlock1 = ((System.Windows.Controls.TextBlock)(this.FindName("textBlock1")));
            this.PaymentMethodTxt = ((System.Windows.Controls.TextBox)(this.FindName("PaymentMethodTxt")));
        }
    }
}

