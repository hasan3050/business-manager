﻿#pragma checksum "D:\Somee Version\Version5\businessmanager_original\TrialBusinessManager\Views\NoticeBoard.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C25BBE148A0A9A1394070BF9A968A378"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17626
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


namespace TrialBusinessManager {
    
    
    public partial class NoticeBoardView : System.Windows.Controls.Page {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Button refreshButton;
        
        internal System.Windows.Controls.ProgressBar busyIndicator;
        
        internal System.Windows.Controls.Button deleteButton;
        
        internal System.Windows.Controls.Button createButton;
        
        internal System.Windows.Controls.DataGrid dataGrid1;
        
        internal System.Windows.Controls.Button button1;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/TrialBusinessManager;component/Views/NoticeBoard.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.refreshButton = ((System.Windows.Controls.Button)(this.FindName("refreshButton")));
            this.busyIndicator = ((System.Windows.Controls.ProgressBar)(this.FindName("busyIndicator")));
            this.deleteButton = ((System.Windows.Controls.Button)(this.FindName("deleteButton")));
            this.createButton = ((System.Windows.Controls.Button)(this.FindName("createButton")));
            this.dataGrid1 = ((System.Windows.Controls.DataGrid)(this.FindName("dataGrid1")));
            this.button1 = ((System.Windows.Controls.Button)(this.FindName("button1")));
        }
    }
}

