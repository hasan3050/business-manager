﻿#pragma checksum "D:\Final Version\Version5\businessmanager_original\TrialBusinessManager\Views\DO\GenerateProductTransferChildWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "716D9ACF1B13749A3898120D65CF097E"
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


namespace TrialBusinessManager.Views.DO {
    
    
    public partial class GenerateProductTransferChildWindow : System.Windows.Controls.ChildWindow {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Button CancelButton;
        
        internal System.Windows.Controls.Button OKButton;
        
        internal System.Windows.Controls.TextBox EmergencyContactTextBox;
        
        internal System.Windows.Controls.TextBox VehicleNoTextBox;
        
        internal System.Windows.Controls.TextBox TransportCostTextBox;
        
        internal System.Windows.Controls.ComboBox TransportTypeComboBox;
        
        internal System.Windows.Controls.ComboBox comboBox1;
        
        internal System.Windows.Controls.Button EditBtn;
        
        internal System.Windows.Controls.DataGrid dataGrid1;
        
        internal System.Windows.Controls.DataGridTextColumn productNameColumn;
        
        internal System.Windows.Controls.DataGridTextColumn productCodeColumn;
        
        internal System.Windows.Controls.DataGridTextColumn productBrandColumn;
        
        internal System.Windows.Controls.DataGridTextColumn packetSizeColumn;
        
        internal System.Windows.Controls.DataGridTextColumn LotNumberColumn;
        
        internal System.Windows.Controls.DataGridTextColumn TotalQuantityColumn;
        
        internal System.Windows.Controls.Button RejectBtn;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/TrialBusinessManager;component/Views/DO/GenerateProductTransferChildWindow.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.CancelButton = ((System.Windows.Controls.Button)(this.FindName("CancelButton")));
            this.OKButton = ((System.Windows.Controls.Button)(this.FindName("OKButton")));
            this.EmergencyContactTextBox = ((System.Windows.Controls.TextBox)(this.FindName("EmergencyContactTextBox")));
            this.VehicleNoTextBox = ((System.Windows.Controls.TextBox)(this.FindName("VehicleNoTextBox")));
            this.TransportCostTextBox = ((System.Windows.Controls.TextBox)(this.FindName("TransportCostTextBox")));
            this.TransportTypeComboBox = ((System.Windows.Controls.ComboBox)(this.FindName("TransportTypeComboBox")));
            this.comboBox1 = ((System.Windows.Controls.ComboBox)(this.FindName("comboBox1")));
            this.EditBtn = ((System.Windows.Controls.Button)(this.FindName("EditBtn")));
            this.dataGrid1 = ((System.Windows.Controls.DataGrid)(this.FindName("dataGrid1")));
            this.productNameColumn = ((System.Windows.Controls.DataGridTextColumn)(this.FindName("productNameColumn")));
            this.productCodeColumn = ((System.Windows.Controls.DataGridTextColumn)(this.FindName("productCodeColumn")));
            this.productBrandColumn = ((System.Windows.Controls.DataGridTextColumn)(this.FindName("productBrandColumn")));
            this.packetSizeColumn = ((System.Windows.Controls.DataGridTextColumn)(this.FindName("packetSizeColumn")));
            this.LotNumberColumn = ((System.Windows.Controls.DataGridTextColumn)(this.FindName("LotNumberColumn")));
            this.TotalQuantityColumn = ((System.Windows.Controls.DataGridTextColumn)(this.FindName("TotalQuantityColumn")));
            this.RejectBtn = ((System.Windows.Controls.Button)(this.FindName("RejectBtn")));
        }
    }
}

