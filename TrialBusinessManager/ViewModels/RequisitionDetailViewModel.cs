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
    public class RequisitionDetailViewModel : ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        private EntitySet<RequisitionProductInfo> RequisitionProductCollection { get { return _context.RequisitionProductInfos; } }
        private EntitySet<RequisitionPackageInfo> RequisitionPackageCollection { get { return _context.RequisitionPackageInfos; } }

        public RequisitionDetailViewModel()
        {
            SelectedRequisition = StaticMessaging.RequisitionMessage;
            
            LoadRequisitionProduct();
            LoadRequisitionPackage();

            Print = new RelayCommand(PrintPage);
        }

        #region Method
        void PrintPage()
        {
            PrintFactory.PrintRequisition(_context,SelectedRequisition);
        }
        private void LoadRequisitionProduct()
        {
            _context.Load(_context.GetRequisitionProductInfoesQuery().Where(e => e.RequisitionId == SelectedRequisition.RequisitionId)).Completed += (s, e) =>
            {
                BindRequisitionProduct = RequisitionProductCollection.ToList();
            };
        }

        private void LoadRequisitionPackage()
        {
            _context.Load(_context.GetRequisitionPackageInfoesQuery().Where(e => e.RequisitionId == SelectedRequisition.RequisitionId)).Completed += (s, e) =>
            {
                BindRequisitionPackage = RequisitionPackageCollection.ToList();
            };
        }

        #endregion

        #region Properties

        public const string SelectedRequisitionPropertyName = "SelectedRequisition";
        private Requisition _selectedRequisition = StaticMessaging.RequisitionMessage;
        public Requisition SelectedRequisition
        {
            get { return _selectedRequisition; }
            set
            {
                if (_selectedRequisition == value) { return; }

                _selectedRequisition = value;
                RaisePropertyChanged(SelectedRequisitionPropertyName);
            }
        }

        public const string BindRequisitionPackagePropertyName = "BindRequisitionPackage";
        private List<RequisitionPackageInfo> _bindRequisitionPackage = new List<RequisitionPackageInfo>();
        public List<RequisitionPackageInfo> BindRequisitionPackage
        {
            get { return _bindRequisitionPackage; }
            set
            {
                if (_bindRequisitionPackage == value) { return; }

                _bindRequisitionPackage = value;
                RaisePropertyChanged(BindRequisitionPackagePropertyName);
            }
        }

        public const string BindRequisitionProductPropertyName = "BindRequisitionProduct";
        private List<RequisitionProductInfo> _bindRequisitionProduct = new List<RequisitionProductInfo>();
        public List<RequisitionProductInfo> BindRequisitionProduct
        {
            get { return _bindRequisitionProduct; }
            set
            {
                if (_bindRequisitionProduct == value) { return; }

                _bindRequisitionProduct = value;
                RaisePropertyChanged(BindRequisitionProductPropertyName);
            }
        }
        public ICommand Print { get; set; }
        #endregion

    }
}
