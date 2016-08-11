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
using GalaSoft.MvvmLight.Messaging;
using TrialBusinessManager.Web;
using System.ServiceModel.DomainServices.Client;
using System.Linq;
using TrialBusinessManager.Models;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using TrialBusinessManager.Views.RM;
using TrialBusinessManager.Views;


namespace TrialBusinessManager.ViewModels.RM
{
    public class VerifyBillViewModel:ViewModelBase
    {
        
        AgroDomainContext _context = new AgroDomainContext();
        bool ContextReceived = false;
        TransportationLossWindow TLWindow = new TransportationLossWindow();
        BusyWindow MyWindow = new BusyWindow();

        public VerifyBillViewModel()
        {
            Initialize();
        }

        void Initialize()
        {
            RegisterForMessages();
            RegisterCommands();
        }

        void RegisterCommands()
        {
            VerifyBillCommand = new RelayCommand(VerifyBill);
        }

        void BroadcastMessages()
        {
            Messenger.Default.Send<AgroDomainContext, TransportationLossViewModel>(_context);
            Messenger.Default.Send<Bill, TransportationLossViewModel>(Bill);
            Messenger.Default.Send<Dealer,TransportationLossViewModel>(Dealer);
        }

        void VerifyBill()
        {
            if (!Bill.HasProductLoss)
            {
                MyWindow.Show();
                Bill.BillStatus = "Verified by RM";
                
                _context.SubmitChanges(OnSubmitCompleted,null);
                
            }
            else
            {
                BroadcastMessages();
                TLWindow.Show();
            }
        }

        void OnSubmitCompleted(SubmitOperation so)
        {
            MyWindow.Close();
            if (so.HasError)
            {
                return;
            }
            else
            {
                MessageBox.Show("Bill Status Updated!!!!");
                Messenger.Default.Send<String,BillsForVerificationViewModel>("Verified");
                return;
            }
        }
        
        void RegisterForMessages()
        {
            Messenger.Default.Register<AgroDomainContext>(this, OnContextReceived);
            Messenger.Default.Register<Dealer>(this, OnDealerReceived);
            Messenger.Default.Register<Bill>(this, OnBillReceived);
            Messenger.Default.Register<String>(this, OnVerificationReceived);
        }

        void OnVerificationReceived(String Message)
        {
            if (Message == "Verified")
            {
                TLWindow.Close();
                Messenger.Default.Send<String,BillsForVerificationViewModel>("Verified");
            }
        }

        void OnContextReceived(AgroDomainContext _ReceivedContext)
        {
            _context = _ReceivedContext;
            ContextReceived = true;
        }

        void OnBillReceived(Bill SelectedBill)
        {
            Bill = new Bill();
            Bill = SelectedBill;
            ShowBillDetail();
        }

        void ShowBillDetail()
        {
            DataGridInfos = new ObservableCollection<BillRowModel>();
            
            for (int i = 0; i < Bill.BillProductInfoes.Count; i++)
            {
                BillRowModel row = new BillRowModel();
                row.Product=new Product();
                row.Info = new BillProductInfo();
                row.Info = Bill.BillProductInfoes.ElementAt(i);
                row.Product = _context.Products.Where(c => c.ProductId.Equals(row.Info.ProductId)).Single();
                DataGridInfos.Add(row);
            }
        }

        void OnDealerReceived(Dealer MyDealer)
        {
            Dealer = new Dealer();
            Dealer = MyDealer;
        }

        public ICommand VerifyBillCommand { get; set; }

        /// <summary>
        /// The <see cref="DataGridInfos" /> property's name.
        /// </summary>
        public const string DataGridInfosPropertyName = "DataGridInfos";

        private ObservableCollection<BillRowModel> _dataGridInfos  ;

        /// <summary>
        /// Gets the DataGridInfos property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public ObservableCollection<BillRowModel> DataGridInfos
        {
            get
            {
                return _dataGridInfos;
            }

            set
            {
                if (_dataGridInfos == value)
                {
                    return;
                }

                var oldValue = _dataGridInfos;
                _dataGridInfos = value;

                

                // Update bindings, no broadcast
                RaisePropertyChanged(DataGridInfosPropertyName);

               
            }
        }



        /// <summary>
        /// The <see cref="Dealer" /> property's name.
        /// </summary>
        public const string DealerPropertyName = "Dealer";

        private Dealer _dealer  ;

        /// <summary>
        /// Gets the Dealer property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public Dealer Dealer
        {
            get
            {
                return _dealer;
            }

            set
            {
                if (_dealer == value)
                {
                    return;
                }

                var oldValue = _dealer;
                _dealer = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(DealerPropertyName);

               
            }
        }

        /// <summary>
        /// The <see cref="Bill" /> property's name.
        /// </summary>
        public const string BillPropertyName = "Bill";

        private Bill _bill  ;

        /// <summary>
        /// Gets the Bill property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public Bill Bill
        {
            get
            {
                return _bill;
            }

            set
            {
                if (_bill == value)
                {
                    return;
                }

                var oldValue = _bill;
                _bill = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(BillPropertyName);

       
            }
        }

       
    }
}
