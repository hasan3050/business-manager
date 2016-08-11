using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TrialBusinessManager.Web;
using TrialBusinessManager.Models;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.ServiceModel.DomainServices.Client;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;

namespace TrialBusinessManager.ViewModels
{
    public class PLRDetailViewModel : ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        private EntitySet<PLRProductInfo> PLRProductCollection { get { return _context.PLRProductInfos; } }
        private EntitySet<PLRPackageInfo> PLRPackageCollection { get { return _context.PLRPackageInfos; } }

        public PLRDetailViewModel()
        {
            SelectedPLR = StaticMessaging.PLRMessage;
            LoadPLRProduct();
            LoadPLRPackage();

            Print = new RelayCommand(PrintPage);
        }

        #region Method

        void PrintPage()
        {
            PrintFactory.PrintPLR(_context,SelectedPLR);
        }
        private void LoadPLRProduct()
        {
            _context.Load(_context.GetPLRProductInfoesQuery().Where(e => e.PLRId == SelectedPLR.PLRId)).Completed += (s, e) =>
            {
                BindPLRProduct = PLRProductCollection.ToList();
            };
        }

        private void LoadPLRPackage()
        {
            _context.Load(_context.GetPLRPackageInfoesQuery().Where(e => e.PLRId == SelectedPLR.PLRId)).Completed += (s, e) =>
            {
                BindPLRPackage = PLRPackageCollection.ToList();
            };
        }

        #endregion

        #region Properties

        public const string SelectedPLRPropertyName = "SelectedPLR";
        private PLR _selectedPLR = StaticMessaging.PLRMessage;
        public PLR SelectedPLR
        {
            get { return _selectedPLR; }
            set
            {
                if (_selectedPLR == value) { return; }

                _selectedPLR = value;
                RaisePropertyChanged(SelectedPLRPropertyName);
            }
        }

        public const string BindPLRPackagePropertyName = "BindPLRPackage";
        private List<PLRPackageInfo> _bindPLRPackage = new List<PLRPackageInfo>();
        public List<PLRPackageInfo> BindPLRPackage
        {
            get { return _bindPLRPackage; }
            set
            {
                if (_bindPLRPackage == value) { return; }

                _bindPLRPackage = value;
                RaisePropertyChanged(BindPLRPackagePropertyName);
            }
        }

        public const string BindPLRProductPropertyName = "BindPLRProduct";
        private List<PLRProductInfo> _bindPLRProduct = new List<PLRProductInfo>();
        public List<PLRProductInfo> BindPLRProduct
        {
            get { return _bindPLRProduct; }
            set
            {
                if (_bindPLRProduct == value) { return; }

                _bindPLRProduct = value;
                RaisePropertyChanged(BindPLRProductPropertyName);
            }
        }
        public ICommand Print { get; set; }
        #endregion

    }
}
