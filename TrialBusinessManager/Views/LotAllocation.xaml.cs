using System;
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
using GalaSoft.MvvmLight.Messaging;
using TrialBusinessManager.Models;
using TrialBusinessManager.Web;
using System.Collections.ObjectModel;
using System.ServiceModel.DomainServices.Client;
using TrialBusinessManager.ViewModels;
using GalaSoft.MvvmLight;
using System.ComponentModel;

namespace TrialBusinessManager.Views
{
    public partial class LotAllocation : ChildWindow,INotifyPropertyChanged
    {
        AgroDomainContext _context = new AgroDomainContext();
        Double PreviousQuantity;
        List<InventoryProductInfo> InfoList = new List<InventoryProductInfo>();
        bool flag = false;
        public bool success = false;
        
        public LotAllocation(ProductMessage Message)
        {
            InitializeComponent();
            MessageTxt.Text = "Loading....Please wait!";
            Initialize(Message);
            this.DataContext = this;
        }

        void StorePreviousQuanity()
        {
            PreviousQuantity = SelectedLot.AllotedPackets;
        }

        void Initialize(ProductMessage Message)
        {
           
            Collections = new ObservableCollection<LotAllocationModel>();
            TotalAllotedQuantity = 0;
            ButtonEnabled = false;
            LoadInventoryInfoes(Message);
        }

        void SubmitAllocation()
        {
            success = true;
            StaticMessaging.LotAllocations = new ObservableCollection<LotAllocationModel>();
            StaticMessaging.LotAllocations = Collections;
            this.DialogResult = true;            
        }
        
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        void LoadInventoryInfoes(ProductMessage message)
        {
            Messenger.Default.Send<Product>(message.Product);    //why???
            RequestedQuantity = message.Quanity;
            if (flag == false)
            {
                //gets all thr infos of this inventory
                
                _context.Load(_context.GetInventoryProductInfoesQuery().Where(c => c.InventoryId == UserInfo.Inventory.InventoryId)).Completed += (s, e) =>
                {
                    InfoList = _context.InventoryProductInfos.ToList();
                    flag = true;
                    AddItem(message);

                };
            }
            else { AddItem(message); }
        }

        void AddItem(ProductMessage message)
        {
            for (int i = 0; i < InfoList.Count; i++)
            {
               
                if (InfoList[i].Product.ProductName == message.Product.ProductName)
                {
                    LotAllocationModel item = new LotAllocationModel();
                    item.AllotedPackets = 0;
                    item.Info = new InventoryProductInfo();
                    item.Info = InfoList[i];
                    item.Info.FinishedQuantity = Math.Round(item.Info.FinishedQuantity, 5);
                    item.AvailablePackets = Math.Round((item.Info.FinishedQuantity / message.Product.StockKeepingUnit), 0);
                    Collections.Add(item);
                }
            }

            if (Collections.Count <= 0) MessageTxt.Text = "This product is not available in the inventory for dispatch.";
            else MessageTxt.Text = "";
        }

        void QuantityEdited()
        {
            TotalAllotedQuantity = 0;
            for (int i = 0; i < Collections.Count; i++)
            {
                if (Collections[i].AllotedPackets > Collections[i].AvailablePackets)
                {
                    MessageBox.Show("Available packets are less than alloted packets.");
                    Collections[i].AllotedPackets = 0;
                }

                if (TotalAllotedQuantity + Collections[i].AllotedPackets > RequestedQuantity)
                {
                    Collections[i].AllotedPackets = RequestedQuantity - TotalAllotedQuantity;
                }

                TotalAllotedQuantity += Collections[i].AllotedPackets;
            }

            ButtonEnabled = (TotalAllotedQuantity == RequestedQuantity);
        }

        /// <summary>
        /// The <see cref="TotalAllotedQuantity" /> property's name.
        /// </summary>
        public const string TotalAllotedQuantityPropertyName = "TotalAllotedQuantity";

        private Double _totalAllotedQuanity  ;

        /// <summary>
        /// Sets and gets the TotalAllotedQuantity property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Double TotalAllotedQuantity
        {
            get
            {
                return _totalAllotedQuanity;
            }

            set
            {
                if (_totalAllotedQuanity == value)
                {
                    return;
                }

                _totalAllotedQuanity = value;
                RaisePropertyChanged(TotalAllotedQuantityPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="RequestedQuantity" /> property's name.
        /// </summary>
        public const string RequestedQuantityPropertyName = "RequestedQuantity";

        private Double _requestedQuantity  ;

        /// <summary>
        /// Sets and gets the RequestedQuantity property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Double RequestedQuantity
        {
            get
            {
                return _requestedQuantity;
            }

            set
            {
                if (_requestedQuantity == value)
                {
                    return;
                }

                _requestedQuantity = value;
                RaisePropertyChanged(RequestedQuantityPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="ButtonEnabled" /> property's name.
        /// </summary>
        public const string ButtonEnabledPropertyName = "ButtonEnabled";

        private bool _buttonEnabled  ;

        /// <summary>
        /// Sets and gets the ButtonEnabled property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool ButtonEnabled
        {
            get
            {
                return _buttonEnabled;
            }

            set
            {
                if (_buttonEnabled == value)
                {
                    return;
                }

                _buttonEnabled = value;
                RaisePropertyChanged(ButtonEnabledPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SelectedLot" /> property's name.
        /// </summary>
        public const string SelectedLotPropertyName = "SelectedLot";

        private LotAllocationModel _selectedLot  ;

        /// <summary>
        /// Sets and gets the SelectedLot property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public LotAllocationModel SelectedLot
        {
            get
            {
                return _selectedLot;
            }

            set
            {
                if (_selectedLot == value)
                {
                    return;
                }

                _selectedLot = value;
                RaisePropertyChanged(SelectedLotPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Collections" /> property's name.
        /// </summary>
        public const string CollectionsPropertyName = "Collections";

        private ObservableCollection<LotAllocationModel> _collections  ;

        /// <summary>
        /// Sets and gets the Collections property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<LotAllocationModel> Collections
        {
            get
            {
                return _collections;
            }

            set
            {
                if (_collections == value)
                {
                    return;
                }

                _collections = value;
                RaisePropertyChanged(CollectionsPropertyName);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void LotDataGrid_CellEditEnded(object sender, DataGridCellEditEndedEventArgs e)
        {
            QuantityEdited();
        }

        private void LotDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StorePreviousQuanity();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            SubmitAllocation();
        }
    }
}

