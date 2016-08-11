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
    public class MRRDetailViewModel : ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        private EntitySet<MRRProductInfo> MRRProductCollection { get { return _context.MRRProductInfos; } }
        private EntitySet<MRRPackageInfo> MRRPackageCollection { get { return _context.MRRPackageInfos; } }

        public MRRDetailViewModel()
        {
            SelectedMRR = StaticMessaging.MRRMessage;

            if (SelectedMRR.MRRType == "Product") 
                LoadMRRProduct();
            else
                LoadMRRPackage();


            Print = new RelayCommand(PrintPage);
        }

        #region Method
        private void PrintPage()
        {
            if (SelectedMRR.MRRType == "Product")
            {
                PrintFactory.PrintProductMRR(_context,SelectedMRR);
            }
            else
            {
                PrintFactory.PrintPackageMRR(_context, SelectedMRR);
            }
        }
        private void LoadMRRProduct()
        {
            _context.Load(_context.GetMRRProductInfoesQuery().Where(e => e.MRRId == SelectedMRR.MRRId)).Completed += (s, e) =>
            {
                BindMRRProduct=MRRProductCollection.ToList();    
            };
        }

        private void LoadMRRPackage()
        {
            _context.Load(_context.GetMRRPackageInfoesQuery().Where(e => e.MRRId == SelectedMRR.MRRId)).Completed += (s, e) =>
            {
                BindMRRPackage=MRRPackageCollection.ToList();
            };
        }

        #endregion

        #region Properties

        public const string SelectedMRRPropertyName = "SelectedMRR";
        private MRR _selectedMRR = StaticMessaging.MRRMessage;
        public MRR SelectedMRR
        {
            get { return _selectedMRR; }
            set
            {
                if (_selectedMRR == value) { return; }

                _selectedMRR = value;
                RaisePropertyChanged(SelectedMRRPropertyName);
            }
        }

        public const string BindMRRPackagePropertyName = "BindMRRPackage";
        private List<MRRPackageInfo> _bindMRRPackage = new List<MRRPackageInfo>();
        public List<MRRPackageInfo> BindMRRPackage
        {
            get { return _bindMRRPackage; }
            set
            {
                if (_bindMRRPackage == value) { return; }

                _bindMRRPackage = value;
                RaisePropertyChanged(BindMRRPackagePropertyName);
            }
        }

        public const string BindMRRProductPropertyName = "BindMRRProduct";
        private List<MRRProductInfo> _bindMRRProduct = new List<MRRProductInfo>();
        public List<MRRProductInfo> BindMRRProduct
        {
            get { return _bindMRRProduct; }
            set
            {
                if (_bindMRRProduct == value) { return; }

                _bindMRRProduct = value;
                RaisePropertyChanged(BindMRRProductPropertyName);
            }
        }
        public ICommand Print { get; set; }

        #endregion

    }
}
