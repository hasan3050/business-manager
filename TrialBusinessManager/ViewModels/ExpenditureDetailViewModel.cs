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
    public class ExpenditureDetailViewModel : ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        private EntitySet<ExpenditureInfo> ExpenditureCollection { get { return _context.ExpenditureInfos; } }

        public ExpenditureDetailViewModel()
        {
            SelectedExpenditure = StaticMessaging.ExpenditureMessage;
            LoadExpenditure();
            LoadSalesOfficers();
            Print = new RelayCommand(PrintPage);
        }

        #region Method
        void PrintPage()
        {
            PrintFactory.PrintExpenditure(_context,SelectedExpenditure);
        }
        void LoadSalesOfficers()
        {
           _context.Load(_context.GetEmployeesQuery().Where(e => e.Designation == "Sales Officer"));
        }
        private void LoadExpenditure()
        {
            _context.Load(_context.GetExpenditureInfoesQuery().Where(e => e.ExpenditureId == SelectedExpenditure.ExpenditureId)).Completed += (s, e) =>
            {
                BindExpenditure = ExpenditureCollection.ToList();
            };
        }

        #endregion

        #region Properties

        public const string SelectedExpenditurePropertyName = "SelectedExpenditure";
        private Expenditure _selectedExpenditure = StaticMessaging.ExpenditureMessage;
        public Expenditure SelectedExpenditure
        {
            get { return _selectedExpenditure; }
            set
            {
                if (_selectedExpenditure == value) { return; }

                _selectedExpenditure = value;
                RaisePropertyChanged(SelectedExpenditurePropertyName);
            }
        }

        public const string BindExpenditurePropertyName = "BindExpenditure";
        private List<ExpenditureInfo> _bindExpenditure = new List<ExpenditureInfo>();
        public List<ExpenditureInfo> BindExpenditure
        {
            get { return _bindExpenditure; }
            set
            {
                if (_bindExpenditure == value) { return; }

                _bindExpenditure = value;
                RaisePropertyChanged(BindExpenditurePropertyName);
            }
        }
        public ICommand Print { get; set; }
        #endregion

    }
}
