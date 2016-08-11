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
using System.Collections.ObjectModel;

namespace TrialBusinessManager.Models
{
    public static class ConstantCollections
    {

        public static ObservableCollection<string> DealerStatus = new ObservableCollection<string>
        {
            "Pending",
            "Active",
            "Defaulter",
            "Inactive"

        };

        public static ObservableCollection<string> SalesReturnStatus = new ObservableCollection<string>
        {
            "Placed",
            "Verified by NSM",
            "Rejected",
            "Approved"
        };

        public static ObservableCollection<String> BuisnessTypes = new ObservableCollection<string>
        {
            "Agro","Seed","Fertilizer","Pesticides"
        };

        public static ObservableCollection<String> Transports = new ObservableCollection<string>
        {
            "Truck","Ship","CNG","Nasimon","Currier","Boat","Van","Multiple Vehicle","Others"
        };

       
        public static ObservableCollection<string> IndentStatusType = new ObservableCollection<string>
        {
            "Placed by Dealer",
            "Rejected by RM",
            "Forwarded to NSM",
            "Rejected by NSM",
            "Forwarded to SIC",
            "Forwarded to DO",
            "Dispatched"
        };

        public static ObservableCollection<string> BillPaymentStatus = new ObservableCollection<string>
        {
            "Issued by RM",
            "Approved by Accountant",
            "Rejected by Accountant"
        };

        public static ObservableCollection<string> BillStatus = new ObservableCollection<string>
        {
            "Dispatched",
            "Verified by RM",
            "Verified by RM Partially Paid",
            "Verified by RM Fully Paid"
        };

        public static ObservableCollection<string> ProductStatusType = new ObservableCollection<string>
        {
            "Pending",
            "Active",
            "Inactive"
        };

        public static ObservableCollection<string> PackageStatusType = new ObservableCollection<string>
        {
            "Pending",
            "Active",
            "Inactive"
        };

        public static ObservableCollection<string> MRRStatusType = new ObservableCollection<string>
        {
            "Issued",
            "Rejected",
            "Approved"
        };

        public static ObservableCollection<string> RequisitionTypeName = new ObservableCollection<string>
        {
            "Requisition For Packing",
            "Return Onprocessing Goods",
            "Return Finished Goods"
        };

        public static ObservableCollection<string> PLRStatusType = new ObservableCollection<string>
        {
            "Pending",
            "Rejected",
            "Verified"
        };

        public static ObservableCollection<string> ExpenditureStatusType = new ObservableCollection<string>
        {
            "Pending",
            "Approved by NSM",
            "Rejected by NSM",
            "Approved by Accountant",
            "Rejected by Accountant"
           
        };

        public static ObservableCollection<string> RequisitionStatusType = new ObservableCollection<string>
        {
            
             "Pending",
            "Rejected",
            "Approved"
            
           
        };

        public static ObservableCollection<string> ProductTypeName = new ObservableCollection<string>
        {
            "Rice",
            "Tea",
            "Fruit",
            "Paddy",
            "Fertilizer",
            "Jute"
        };

        public static ObservableCollection<string> ProductWingName = new ObservableCollection<string>
        {
            "Agro",
            "Seed",
            "Bio-Tech",
            "Vegetable"
        };

        public static ObservableCollection<string> CommissionName = new ObservableCollection<string>
        {
            "Advance",
            "On Cash",
            "One Month Installment",
            "Two Month Installment",
            "Three Month Installment"
        };

        public static ObservableCollection<string> TimePeriodName = new ObservableCollection<string> 
        { 
            "January", "Early January", "Mid January", "Late January", 
            "February", "Early February", "Mid February", "Late February", 
            "March", "Early March", "Mid March", "Late March", 
            "April", "Early April", "Mid April", "Late April", 
            "May", "Early May", "Mid May", "Late May", 
            "June", "Early June", "Mid June", "Late June", 
            "July", "Early July", "Mid July", "Late July", 
            "August", "Early August", "Mid August", "Late August", 
            "September", "Early September", "Mid September", "Late September",
            "October", "Early October", "Mid October", "Late October", 
            "November", "Early November", "Mid November", "Late November", 
            "December", "Early December", "Mid December", "Late December" 
        };

        public static ObservableCollection<String> Designations = new ObservableCollection<string>
        {
            "Regional Manager","National Sales Manager","Accountant","Dispatch Officer","Store In Charge","Sales Officer","Co-ordinator","Viewer"
        };

        public static ObservableCollection<String> Wings = new ObservableCollection<string>
        {
            "Seed","Agro","Bio-tech","Others"
        };

        public static ObservableCollection<String> ActivityStatus = new ObservableCollection<string>
        {
            "Active","Inactive"
        };

        public static int SICID =7 ;
        
    }
    
}
