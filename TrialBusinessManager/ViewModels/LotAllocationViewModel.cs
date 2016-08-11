using System;
using System.Net;
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
using TrialBusinessManager.Models;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Messaging;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using GalaSoft.MvvmLight.Command;
using TrialBusinessManager.Views;
using System.Collections.Generic;

namespace TrialBusinessManager.ViewModels
{
    //all quanitities are packets
    public class LotAllocationViewModel:ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        Double PreviousQuantity;
        List<InventoryProductInfo> InfoList = new List<InventoryProductInfo>();
        bool flag = false;

        public LotAllocationViewModel()
        {
            Initialize();
        }

        void Initialize()
        {
            Collections = new ObservableCollection<LotAllocationModel>();
            RegisterCommands(); 
            TotalAllotedQuantity = 0;
            ButtonEnabled = false;
            Messenger.Default.Register<ProductMessage>(this, MessageReceived);
        }

        void RegisterCommands()
        {
            EditQuantityCommand = new RelayCommand(QuantityEdited);
            OKButtonCommand = new RelayCommand(SubmitAllocation);
            SelectionChangedCommand = new RelayCommand(StorePreviousQuanity);
        }

        void StorePreviousQuanity()
        {
            PreviousQuantity = SelectedLot.AllotedPackets;
        }

        void QuantityEdited()
        {
            TotalAllotedQuantity = 0 ;
            for (int i = 0 ; i < Collections.Count; i++)
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

        void SubmitAllocation()
        {
            Messenger.Default.Send<ObservableCollection<LotAllocationModel>>(Collections);
            Messenger.Default.Send<String, LotAllocation>("Close");
        }

        void MessageReceived(ProductMessage message)
        {
            
            Messenger.Default.Send<Product>(message.Product);
            RequestedQuantity = message.Quanity;
         
            if (flag == false)
            {
                Message = "Loading....Please wait!";
                MessageBox.Show(Message);
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
                //if info belongs to the involved product
            //    MessageBox.Show(InfoList.ElementAt(i).Product.ProductName);
              //  MessageBox.Show(message.Product.ProductName);
               // break;
                if(InfoList[i].Product.ProductName==message.Product.ProductName)
                {
                    LotAllocationModel item = new LotAllocationModel();
                    item.AllotedPackets = 0;
                    item.Info = new InventoryProductInfo();
                    item.Info = InfoList[i];
                    item.AvailablePackets = item.Info.FinishedQuantity / message.Product.StockKeepingUnit;
                    Collections.Add(item);
                  //  MessageBox.Show(i.ToString());
                }
            }

            if (Collections.Count <= 0) Message = "This product is not available in the inventory for dispatch.";
            else Message = "";
            MessageBox.Show(Message);
        }

        public ICommand EditQuantityCommand { get; set; }
        public ICommand OKButtonCommand { get; set; }
        public ICommand SelectionChangedCommand { get; set; }

        
        public const string MessagePropertyName = "Message";
        private String _message="";

      
        public String Message
        {
            get
            {
                return _message;
            }

            set
            {
                if (_message == value)
                {
                    return;
                }

                _message = value;
                RaisePropertyChanged(MessagePropertyName);
            }
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
    }
}
