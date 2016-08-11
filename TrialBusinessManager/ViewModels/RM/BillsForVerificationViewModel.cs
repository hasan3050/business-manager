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
using System.ServiceModel.DomainServices.Client;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using TrialBusinessManager.Views;
using System.Linq;
using TrialBusinessManager.Views.RM;
using TrialBusinessManager.Models;

namespace TrialBusinessManager.ViewModels.RM
{
    public class BillsForVerificationViewModel:ViewModelBase
    {

        AgroDomainContext _context = new AgroDomainContext();
        int flag = 0;

        //busy indicator
        BusyWindow MyWindow = new BusyWindow();

        VerifyBillWindow BillVerificationWindow = new VerifyBillWindow();

        public BillsForVerificationViewModel()
        {
            Initialize();
        }

        void Initialize()
        {
            MyWindow.Show();
            RegisterCommands();
            RegisterForMessages();
            LoadData();
        }

        void LoadData()
        {
            //Have to add filter by RM 
            //dont forget to correct the query!!!!!!!!!!!!!!!!!!!!!!!!!!
            _context.Load(_context.GetBillsForVerificationQuery(UserInfo.Region.RegionId)).Completed+=(s,e)=>
                {
                    flag++;
                    if(flag==2) 
                     MyWindow.Close();
                };

            _context.Load(_context.GetProductsQuery()).Completed += (p, q) =>
                {
                    flag++;
                    if (flag == 2)
                        MyWindow.Close();
                };
        }

        void RegisterCommands()
        {
            VerifyBillCommand = new RelayCommand(VerifyBill);
        }

        void BrodacastMessages()
        {
            Messenger.Default.Send<AgroDomainContext,VerifyBillViewModel>(_context);
            Messenger.Default.Send<Bill,VerifyBillViewModel>(SelectedBill);
            Messenger.Default.Send<Dealer,VerifyBillViewModel>(SelectedBill.Dealer);
        }

        void RegisterForMessages()
        {
            Messenger.Default.Register<String>(this, OnVerificationReceived);
        }

        void OnVerificationReceived(String Message)
        {
            if (Message == "Verified")
            {
                BillVerificationWindow.Close();
            }
        }

        void VerifyBill()
        {
            if (SelectedBill == null)
            {
                MessageBox.Show("please select a bill!!");
                return;
            }

            if (SelectedBill.BillStatus.Equals("Verified by RM"))
            {
                MessageBox.Show("this bill is approved!!!");
                return;
            }

            RemoveBill = new Bill();
            RemoveBill = SelectedBill;
            BrodacastMessages();
            BillVerificationWindow.Show();
        }

        public ICommand VerifyBillCommand { get; set; }

        public EntitySet<Bill> Bills { get { return _context.Bills; } }

        /// <summary>
        /// The <see cref="RemoveBill" /> property's name.
        /// </summary>
        public const string RemoveBillPropertyName = "RemoveBill";

        private Bill _removeBill;

        /// <summary>
        /// Gets the RemoveBill property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public Bill RemoveBill
        {
            get
            {
                return _removeBill;
            }

            set
            {
                if (_removeBill == value)
                {
                    return;
                }

                var oldValue = _removeBill;
                _removeBill = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(RemoveBillPropertyName);

            }
        }

        /// <summary>
        /// The <see cref="SelectedBill" /> property's name.
        /// </summary>
        public const string SelectedBillPropertyName = "SelectedBill";

        private Bill _selectedBill  ;

        /// <summary>
        /// Gets the SelectedBill property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public Bill SelectedBill
        {
            get
            {
                return _selectedBill;
            }

            set
            {
                if (_selectedBill == value)
                {
                    return;
                }

                var oldValue = _selectedBill;
                _selectedBill = value;


                // Update bindings, no broadcast
                RaisePropertyChanged(SelectedBillPropertyName);

            }
        }

    }
}
