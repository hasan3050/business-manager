﻿#pragma checksum "D:\Somee Version\Version5\businessmanager_original\TrialBusinessManager\Views\DetailedNoticeView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "1A587473D6E794A979406FF9CFEA4847"
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
    
    
    public partial class DetailedNoticeView : System.Windows.Controls.ChildWindow {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid messagePane;
        
        internal System.Windows.Controls.Border FromBorder;
        
        internal System.Windows.Controls.TextBox fromBlock;
        
        internal System.Windows.Controls.Border DateBorder;
        
        internal System.Windows.Controls.TextBox dateBlock;
        
        internal System.Windows.Controls.Border SubjectBorder;
        
        internal System.Windows.Controls.TextBox titleBlock;
        
        internal System.Windows.Controls.RichTextBox detailsBlock;
        
        internal System.Windows.Controls.Border ToolBox;
        
        internal System.Windows.Controls.ContentControl ToolControl;
        
        internal System.Windows.Controls.Button boldButton;
        
        internal System.Windows.Controls.Button backButton;
        
        internal System.Windows.Controls.Button editButton;
        
        internal System.Windows.Controls.TextBlock infoBlock;
        
        internal System.Windows.Controls.Grid editDatePane;
        
        internal System.Windows.Controls.DatePicker startDatePicker;
        
        internal System.Windows.Controls.DatePicker endDatePicker;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/TrialBusinessManager;component/Views/DetailedNoticeView.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.messagePane = ((System.Windows.Controls.Grid)(this.FindName("messagePane")));
            this.FromBorder = ((System.Windows.Controls.Border)(this.FindName("FromBorder")));
            this.fromBlock = ((System.Windows.Controls.TextBox)(this.FindName("fromBlock")));
            this.DateBorder = ((System.Windows.Controls.Border)(this.FindName("DateBorder")));
            this.dateBlock = ((System.Windows.Controls.TextBox)(this.FindName("dateBlock")));
            this.SubjectBorder = ((System.Windows.Controls.Border)(this.FindName("SubjectBorder")));
            this.titleBlock = ((System.Windows.Controls.TextBox)(this.FindName("titleBlock")));
            this.detailsBlock = ((System.Windows.Controls.RichTextBox)(this.FindName("detailsBlock")));
            this.ToolBox = ((System.Windows.Controls.Border)(this.FindName("ToolBox")));
            this.ToolControl = ((System.Windows.Controls.ContentControl)(this.FindName("ToolControl")));
            this.boldButton = ((System.Windows.Controls.Button)(this.FindName("boldButton")));
            this.backButton = ((System.Windows.Controls.Button)(this.FindName("backButton")));
            this.editButton = ((System.Windows.Controls.Button)(this.FindName("editButton")));
            this.infoBlock = ((System.Windows.Controls.TextBlock)(this.FindName("infoBlock")));
            this.editDatePane = ((System.Windows.Controls.Grid)(this.FindName("editDatePane")));
            this.startDatePicker = ((System.Windows.Controls.DatePicker)(this.FindName("startDatePicker")));
            this.endDatePicker = ((System.Windows.Controls.DatePicker)(this.FindName("endDatePicker")));
        }
    }
}
