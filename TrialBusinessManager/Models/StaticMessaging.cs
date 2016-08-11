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
using System.Collections.ObjectModel;

namespace TrialBusinessManager.Models
{
    public static class StaticMessaging
    {
        
        private static bool _isErrorWindowShowing = false;
        public static bool IsErrorWindowShowing
        {
            get { return _isErrorWindowShowing; }
            set
            {
                if (_isErrorWindowShowing == value) { return; }
                _isErrorWindowShowing = value;
            }
        }
        private static bool _isLoggedIn= false;
        public static bool IsLoggedIn
        {
            get { return _isLoggedIn; }
            set
            {
                if (_isLoggedIn == value) { return; }
                _isLoggedIn = value;
            }
        }
        
        private static bool _billGenerated = false;
        public static bool BillGenerated
        {
            get { return _billGenerated; }
            set
            {
                if (_billGenerated == value) { return; }
                _billGenerated = value;
            }
        }
        
        private static bool _SICIndentVerificationSubmitted = false;
        public static bool SICIndentVerificationSubmitted
        {
            get { return _SICIndentVerificationSubmitted; }
            set
            {
                if (_SICIndentVerificationSubmitted == value) { return; }
                _SICIndentVerificationSubmitted = value;
            }
        }
        
        private static bool _accountantBillPaymentSubmitted=false;
        public static bool AccountantBillPaymentSubmitted
        {
            get { return _accountantBillPaymentSubmitted; }
            set
            {
                if (_accountantBillPaymentSubmitted == value) { return; }
                _accountantBillPaymentSubmitted = value;
            }
        }
        
        private static BillPayment _billPaymentMessage = new BillPayment();
        public static BillPayment BillPaymentMessage
        {
            get { return _billPaymentMessage; }
            set
            {
                if (_billPaymentMessage == value) { return; }
                _billPaymentMessage = value;
            }
        }

        private static Bill _billMessage = new Bill();
        public static Bill BillMessage
        {
            get { return _billMessage; }
            set
            {
                if (_billMessage == value) { return; }
                _billMessage = value;
            }
        }

        private static RegionTarget _targetMessage = new RegionTarget();
        public static RegionTarget TargetMessage
        {
            get { return _targetMessage; }
            set
            {
                if (_targetMessage == value) { return; }
                _targetMessage = value;
            }
        }

        private static Indent _billIndentMessage = new Indent();
        public static Indent BillIndentMessage
        {
            get { return _billIndentMessage; }
            set
            {
                if (_billIndentMessage == value) { return; }
                _billIndentMessage = value;
            }
        }

        private static Indent _indentMessage = new Indent();
        public static Indent IndentMessage
        {
            get { return _indentMessage; }
            set
            {
                if (_indentMessage == value) { return; }
                _indentMessage = value;
            }
        }

        private static MRR _MRRMessage = new MRR();
        public static MRR MRRMessage
        {
            get { return _MRRMessage; }
            set
            {
                if (_MRRMessage == value) { return; }
                _MRRMessage = value;
            }
        }

        private static PLR _PLRMessage = new PLR();
        public static PLR PLRMessage
        {
            get { return _PLRMessage; }
            set
            {
                if (_PLRMessage == value) { return; }
                _PLRMessage = value;
            }
        }

        private static Expenditure _expenditureMessage = new Expenditure();
        public static Expenditure ExpenditureMessage
        {
            get { return _expenditureMessage; }
            set
            {
                if (_expenditureMessage == value) { return; }
                _expenditureMessage = value;
            }
        }

        private static SalesReturn _salesReturnMessage = new SalesReturn();
        public static SalesReturn SalesReturnMessage
        {
            get { return _salesReturnMessage; }
            set
            {
                if (_salesReturnMessage == value) { return; }
                _salesReturnMessage = value;
            }
        }

        private static Dealer _dealerMessage = new Dealer();
        public static Dealer DealerMessage
        {
            get { return _dealerMessage; }
            set
            {
                if (_dealerMessage == value) { return; }
                _dealerMessage = value;
            }
        }

        private static Requisition _requisitionMessage = new Requisition();
        public static Requisition RequisitionMessage
        {
            get { return _requisitionMessage; }
            set
            {
                if (_requisitionMessage == value) { return; }
                _requisitionMessage = value;
            }
        }

        public static ObservableCollection<LotAllocationModel> LotAllocations{ get; set; }

        public class ProductTransferForBind
        {
            public ProductTransfer Transfer { get; set; }
            public string InventoryToName { get; set; }
            public string SICName { get; set; }
        }

        private static ProductTransferForBind _productTransfer = new ProductTransferForBind();
        public static ProductTransferForBind ProductTransferMessage
        {
            get { return _productTransfer; }
            set
            {
                if (_productTransfer == value) { return; }
                _productTransfer = value;
            }
        }

        private static AgroDomainContext _context = new AgroDomainContext();
        public static AgroDomainContext Context
        {
            get { return _context; }
            set
            {
                if (_context == value) { return; }
                _context = value;
            }
        }
      
    }
}
