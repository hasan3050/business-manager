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
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TrialBusinessManager.Models;
using GalaSoft.MvvmLight.Command;
using TrialBusinessManager.Views;
using TrialBusinessManager.Views.DO;


namespace TrialBusinessManager.ViewModels.DO
{
    public class ViewTransportationLossesViewModel:ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        BusyWindow Busy = new BusyWindow();
      
        ObservableCollection<Product> _Products = new ObservableCollection<Product>();
        public ViewTransportationLossesViewModel()
        {
            Busy.Show();
            Initialize();

        }

        void Initialize()
        {
            _selectedLoss = new TransportationLoss();
            BalanceLossCommand = new RelayCommand(BalanceLoss);
            _context.Load(_context.GetTransportationLossesByDOIDQuery(UserInfo.EmployeeID)).Completed += (s, e) =>
                {

                    PopulateProductComboBox();
                    Busy.Close();
                };
        }

        void PopulateProductComboBox()
        {
          
            for (int i = 0; i < _context.TransportationLosses.Count; i++)
            {
                if (!_Products.Any(e => e.ProductId == _context.TransportationLosses.ElementAt(i).Product.ProductId))
                    _Products.Add((Product)_context.TransportationLosses.ElementAt(i).Product);
            }

        }

        void BalanceLoss()
        {
            if (SelectedLoss == null)
            {
                MessageBox.Show("please select a transportation Loss!!");
                return;
            }

            if (SelectedLoss.HasBalanced.Equals(true))
            {
                MessageBox.Show("Already Balanced");
                return;
            }
            else {
                InventoryBalanceChildWindow obj = new InventoryBalanceChildWindow(SelectedLoss,_context);
                obj.Show();
            }
        }

        public EntitySet<TransportationLoss> Losses {get{return _context.TransportationLosses;}}
        public ObservableCollection<Product> Products { get { return _Products; } }

        public ICommand BalanceLossCommand { get; set; }

        /// <summary>
        /// The <see cref="SelectedProduct" /> property's name.
        /// </summary>
        public const string SelectedProductPropertyName = "SelectedProduct";

        private Product _selectedProduct  ;

        /// <summary>
        /// Gets the SelectedProduct property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public Product SelectedProduct
        {
            get
            {
                return _selectedProduct;
            }

            set
            {
                if (_selectedProduct == value)
                {
                    return;
                }

                var oldValue = _selectedProduct;
                _selectedProduct = value;

                // Remove one of the two calls below
                throw new NotImplementedException();

                // Update bindings, no broadcast
                RaisePropertyChanged(SelectedProductPropertyName);

                // Update bindings and broadcast change using GalaSoft.MvvmLight.Messenging
                RaisePropertyChanged(SelectedProductPropertyName, oldValue, value, true);
            }
        }

        /// <summary>
        /// The <see cref="SelectedLoss" /> property's name.
        /// </summary>
        public const string SelectedLossPropertyName = "SelectedLoss";

        private TransportationLoss _selectedLoss  ;

        /// <summary>
        /// Gets the SelectedLoss property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public TransportationLoss SelectedLoss
        {
            get
            {
                return _selectedLoss;
            }

            set
            {
                if (_selectedLoss == value)
                {
                    return;
                }

                var oldValue = _selectedLoss;
                _selectedLoss = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(SelectedLossPropertyName);
            }
        }
    }
}
