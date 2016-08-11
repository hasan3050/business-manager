using System;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.ServiceModel.DomainServices.Client;
using TrialBusinessManager.Web;
using TrialBusinessManager.Models;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TrialBusinessManager.Views;

namespace TrialBusinessManager.ViewModels.Admin
{
    public class LedgerUpdateViewModel:ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        BusyWindow Busy = new BusyWindow();
        int flag = 0;

        public EntitySet<Region> RegionCollection { get { return _context.Regions; } }
        public EntitySet<Dealer> DealerCollection { get { return _context.Dealers; } }

        public ICommand SubmitCommand { get; set; }
        public ICommand ResetCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        #region Method
        public LedgerUpdateViewModel()
        {
            SubmitCommand = new RelayCommand(Submit);
            ResetCommand = new RelayCommand(Reset);
            AddCommand = new RelayCommand(Add);
            DeleteCommand = new RelayCommand(Delete);
            Busy.Show();
            LoadAll();
        }
        
        private void LoadAll()
        {
            LoadRegions();
            LoadDealers();
            LoadProducts();
        }

        private void LoadRegions()
        {
            _context.Load(_context.GetRegionsQuery()).Completed += (s, e) =>
            {
                flag++;
                if (flag == 3)
                    Busy.Close();
            };
        }

        private void LoadDealers()
        {
            _context.Load(_context.GetDealersQuery().Where(e => e.ActivityStatus != "Rejected")).Completed += (s, e) =>
                {
                    flag++;
                    if (flag == 3)
                        Busy.Close();
                };
        }

        private void LoadProducts()
        {
            _context.Load(_context.GetProductsQuery().Where(e => e.GroupName== "Default")).Completed += (s, e) =>
            {
                flag++;
                if (flag == 3)
                    Busy.Close();
            };
        }

        private void Add()
        {
            if (SelectedDealer == null)
            {
                MessageBox.Show("Please Select a Dealer!");
                return;
            }

            if (SelectedRegion == null)
            {
                MessageBox.Show("Please Select a Region!");
                return;
            }

            LedgerModule temp = new LedgerModule();
            temp.ProductCollection = new ObservableCollection<Product>(_context.Products);
            temp.Ledger.PartyId =SelectedDealer.DealerId ;
            temp.Ledger.IsDealerOrEmployee = true;
            temp.DealerName = SelectedDealer.Name;
            temp.Ledger.MemoNo="Unknown";
            temp.Ledger.Date = new DateTime(2011, 1, 1); ;
            BindLedger.Add(temp);

            SubmitButtonEnableBool = true;
        }

        private void Delete()
        {
            if (SelectedLedgerIndex < 0 || BindLedger.Count<=0)
            {
                MessageBox.Show("Please Select an entry to Delete!");
                return;
            }

            BindLedger.RemoveAt(SelectedLedgerIndex);
            if (BindLedger.Count <= 0) SubmitButtonEnableBool = false;
        }

        private void Reset()
        {
            BindLedger.Clear();
            SelectedDealer = new Dealer();
            SelectedRegion = new Region();
            SelectedLedgerIndex = -1;
            SubmitButtonEnableBool = false;
        }


        private bool validate()
        {
            foreach (var temp in BindLedger)
            {
                if (temp.SelectedMethod == null)
                {
                    MessageBox.Show("You must select Method for each entry!!");
                    return false;
                }

                if (String.IsNullOrEmpty(temp.SelectedProduct.ProductCode))
                {
                    MessageBox.Show("You must select Product for each entry!!");
                    return false;
                }

                if (temp.Ledger.CreditAmount <= 0 || temp.Ledger.CreditAmount <= 0)
                {
                    MessageBox.Show("You Have invalid inputs!!");
                    return false;
                }

               
            }

            return true;
        }
        
        private void Submit()
        {
            if (!validate()) return;

            foreach (LedgerModule temp in BindLedger)
            {
               
                temp.Ledger.Method = temp.SelectedMethod;
                temp.Ledger.IsDealerOrEmployee = true;
                
                if (temp.SelectedMethod == "Bill")
                    temp.Ledger.IsDebitOrCredit = true;

                else 
                    temp.Ledger.IsDebitOrCredit = false;

				temp.Ledger.Product= temp.SelectedProduct;

                
                
                temp.Ledger.MemoNo = "Unknown";

                _context.Ledgers.Add(temp.Ledger);
            }

            Busy.Show();
            _context.SubmitChanges(OnSubmitCompleted, null);
        }

        private void OnSubmitCompleted(SubmitOperation op)
        {
            Busy.Close();
            SubmitButtonEnableBool = true;
            
            if (op.HasError == true)
            {
                MessageBox.Show(string.Format("Update failed,please try again."));
                MessageBox.Show(op.Error.Message);
                op.MarkErrorAsHandled();
                _context.RejectChanges();
                return;
            }

            MessageBox.Show("Entry Successful!");
            Reset();
        }

        #endregion

        #region Properties

        public const string SelectedRegionPropertyName = "SelectedRegion";
        private Region _selectedRegion = new Region();
        public Region SelectedRegion
        {
            get { return _selectedRegion; }
            set
            {
                if (_selectedRegion == value) { return; }

                _selectedRegion = value;
                BindDealer =new ObservableCollection<Dealer>( DealerCollection.Where(e => e.RegionId == SelectedRegion.RegionId));
                RaisePropertyChanged(SelectedRegionPropertyName);
            }
        }

        public const string BindDealerPropertyName = "BindDealer";
        private ObservableCollection<Dealer> _bindDealer = new ObservableCollection<Dealer>();
        public ObservableCollection<Dealer> BindDealer
        {
            get { return _bindDealer; }
            set
            {
                if (_bindDealer == value) { return; }

                _bindDealer = value;
                RaisePropertyChanged(BindDealerPropertyName);
            }
        }

        public const string SelectedDealerPropertyName = "SelectedDealer";
        private Dealer _selectedDealer = new Dealer();
        public Dealer SelectedDealer
        {
            get { return _selectedDealer; }
            set
            {
                if (_selectedDealer == value) { return; }

                _selectedDealer = value;
                RaisePropertyChanged(SelectedDealerPropertyName);
            }
        }

        public const string SelectedLedgerIndexPropertyName = "SelectedLedgerIndex";
        private int _selectedLedgerIndex;
        public int SelectedLedgerIndex
        {
            get { return _selectedLedgerIndex; }
            set
            {
                if (_selectedLedgerIndex == value) { return; }

                _selectedLedgerIndex = value;
                RaisePropertyChanged(SelectedLedgerIndexPropertyName);
            }
        }

        public const string BindLedgerPropertyName = "BindLedger";
        private ObservableCollection<LedgerModule> _bindLedger = new ObservableCollection<LedgerModule>();
        public ObservableCollection<LedgerModule> BindLedger
        {
            get { return _bindLedger; }
            set
            {
                if (_bindLedger == value) { return; }

                _bindLedger = value;
                RaisePropertyChanged(BindLedgerPropertyName);
            }
        }

        public const string SubmitButtonEnableBoolPropertyName = "SubmitButtonEnableBool";
        private bool _submitButtonEnableBool = false;
        public bool SubmitButtonEnableBool
        {
            get { return _submitButtonEnableBool; }
            set
            {
                if (_submitButtonEnableBool == value) { return; }
                _submitButtonEnableBool = value;
                RaisePropertyChanged(SubmitButtonEnableBoolPropertyName);
            }
        }

        #endregion
    }
}
