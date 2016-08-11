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
using System.Windows.Printing;
using System.Windows.Controls;
using TrialBusinessManager.UserControls;

namespace TrialBusinessManager.ViewModels
{
    public class BillDetailViewModel : ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        double totalAmountWithoutCom=0;
        public List<DueInfo> DueCollection = new List<DueInfo>();
        ObservableCollection<UnpaidBillModel> UnpaidBills = new ObservableCollection<UnpaidBillModel>();
        int flag = 0;
        Double DueTotal = 0;
        private EntitySet<BillProductInfo> BillProductCollection { get { return _context.BillProductInfos; } }
      
        public BillDetailViewModel()
        {
            Enabled = false;
            SelectedBill = StaticMessaging.BillMessage;
            Message = string.Empty;
            _context = StaticMessaging.Context;
            LoadBillProduct();
            LoadBillDueInfoes();
            if (UserInfo.Designation == "Accountant")
                _context.GetInventoriesQuery();
           

            Print = new RelayCommand(PrintPage);
        }

        #region Method
        void PrintPage()
        {
            
            PrintDocument pd = new PrintDocument();
            int itemsCount = SelectedBill.BillProductInfoes.Count;
            int dueitemsCount=UnpaidBills.Count;
            int count = 0;
            int duecount = 0;
            bool dueFlag = false;
           // UnpaidBills.OrderByDescending(y => y.Bill.BillCode);
            if (UserInfo.Designation == "Accountant")
            {
                PrintFactory.PrintBill(_context, SelectedBill);
            }

            else
            {

                pd.PrintPage += (s, e) =>
                {

                    e.HasMorePages = false;
                    int index = 0;
                    int dueindex = 0;
                    StackPanel itemHost = new StackPanel();
                    BillPrintHeader Header = new BillPrintHeader(SelectedBill);
                    BillFooterControl footer = new BillFooterControl();
                    itemHost.Children.Add(Header);
                    if (!dueFlag)
                        itemHost.Children.Add(new BillItemHeader());

                    if (!dueFlag)
                    {
                        for (index = count; index < itemsCount; index++)
                        {
                            PrintBillModel Info = new PrintBillModel();
                            Info.Product = _context.Products.Where(c => c.ProductId.Equals(SelectedBill.BillProductInfoes.ElementAt(index).ProductId)).Single();
                            Info.Info = SelectedBill.BillProductInfoes.ElementAt(index);

                            BillItemControl row = new BillItemControl();
                            row.DataContext = Info;
                            itemHost.Children.Add(row);

                            itemHost.Measure(new Size(e.PrintableArea.Width, Double.PositiveInfinity));

                            e.HasMorePages = (itemHost.DesiredSize.Height > e.PrintableArea.Height);

                            if (itemHost.DesiredSize.Height > e.PrintableArea.Height)
                            {
                                for (int i = 0; i < 3; i++)
                                {
                                    itemHost.Children.RemoveAt(itemHost.Children.Count - 1);
                                }

                                count = index - 2;
                                break;
                            }
                        }
                    }
                    if (!dueFlag)
                        if (index == itemsCount)
                        {
                            //add middlecontrol
                            BillPrintMiddleControl middlecontrol = new BillPrintMiddleControl();

                            middlecontrol.WithOutCommTxtBlock.Text = "Total Price(Without commission): " + totalAmountWithoutCom.ToString() + " Tk only";
                            middlecontrol.WithCommTxtBlock.Text = "Total Price(With commission): " + TotalAmount.ToString() + " Tk only";
                            middlecontrol.WarningTxtBlock.Text = "Last valid date of paying this bill is " + SelectedBill.PaymentDeadline.ToShortDateString() + "\n" + "*No commission if last payment date expires.";
                            itemHost.Children.Add(middlecontrol);

                            if (itemHost.DesiredSize.Height > e.PrintableArea.Height + 70)
                            {
                                itemHost.Children.Remove(middlecontrol);
                                e.HasMorePages = true;
                            }

                            else
                                dueFlag = true;

                        }

                    if (dueFlag)
                    {
                        itemHost.Children.Add(new BillDueHeaderControl());



                        for (dueindex = duecount; dueindex < dueitemsCount; dueindex++)
                        {

                            itemHost.Measure(new Size(e.PrintableArea.Width, Double.PositiveInfinity));
                            BillDueItemControl dueitem = new BillDueItemControl(UnpaidBills.ElementAt(dueindex));
                            itemHost.Children.Add(dueitem);

                            if (itemHost.DesiredSize.Height > e.PrintableArea.Height - 80 && UnpaidBills.Count >= 1)
                            {
                                for (int i = 0; i < 2; i++)
                                {
                                    itemHost.Children.RemoveAt(itemHost.Children.Count - 1);
                                }

                                e.HasMorePages = true;
                                duecount = dueindex - 1;
                                break;
                            }

                            else
                            {
                                e.HasMorePages = false;


                            }

                        }

                        if (dueindex == dueitemsCount)
                        {
                            BillDueItemControl dueForTotal = new BillDueItemControl(DueTotal);
                            itemHost.Children.Add(dueForTotal);

                        }

                    }



                    itemHost.Measure(new Size(e.PrintableArea.Width, Double.PositiveInfinity));
                    footer.Height = e.PrintableArea.Height - itemHost.DesiredSize.Height;
                    //footer.Height = 100;


                    itemHost.Children.Add(footer);
                    e.PageVisual = itemHost;

                };

                pd.Print("New Printing");
            }
        }

        private void LoadBillDueInfoes()
        {
            _context.DueInfos.Clear();
            _context.Load(_context.GetDueInfoForBillPaymentsQuery().Where(e => e.DealerId == SelectedBill.DealerId && e.DueStatus == "Unpaid"), LoadBehavior.RefreshCurrent, true).Completed += (s, e) =>
            {
                DueCollection = _context.DueInfos.Where(o => o.DealerId == SelectedBill.DealerId).ToList(); 
                flag++;
                if (flag == 2)
                {
                    ShowUnpaidBills();
                    Enabled = true;
                }
            };
        }
        
        private void LoadEmployee()
        {
            _context.Load(_context.GetEmployeesQuery().Where(e => e.EmployeeId == SelectedBill.DispatchedById)).Completed += (s, e) =>
                {
                    flag++;
                    if (flag == 2)
                    {
                        ShowUnpaidBills();
                        Enabled = true;
                    }
                };
        }

     

        void ShowUnpaidBills()
        {
            
            for (int i = 0; i < _context.Bills.Count; i++)
            {
                Bill CurrentBill = new Bill();
                CurrentBill = _context.Bills.ElementAt(i);

                if (CurrentBill.Dealer.Equals(SelectedBill.Dealer) && !CurrentBill.BillStatus.Equals("Verified By RM Fully Paid"))
                {
                    UnpaidBillModel Model = new UnpaidBillModel();
                    Model.Bill = CurrentBill;
                    Model.DueAmount = 0;
                    Model.TotalAmount = 0;

                    List<DueInfo> tempDueInfo = DueCollection.Where(e => e.BillId == CurrentBill.BillId).ToList();

                    foreach (DueInfo due in tempDueInfo)
                    {
                        BillProductDetail Detail = new BillProductDetail();
                        Detail.GenerateBillDetail(due);
                        Model.TotalAmount += Detail.NetPrice;
                        Model.DueAmount += Detail.Payable;
                    }
                      
                    DueTotal += Model.DueAmount;
                    if(Model.DueAmount>0)
                        UnpaidBills.Add(Model);
                }
                
            }
            UnpaidBills=new ObservableCollection<UnpaidBillModel>(UnpaidBills.OrderByDescending(y => y.Bill.DateOfIssue).ToList());
            
        }


        private void LoadBillProduct()
        {
            _context.BillProductInfos.Clear();
            _context.Load(_context.GetBillProductInfoesQuery().Where(e => e.BillId == SelectedBill.BillId)).Completed += (s, e) =>
            {
                
                BindBillInfo.Clear();

                foreach (BillProductInfo Info in BillProductCollection)
                {
                    BillRowModel temp = new BillRowModel();
                    temp.Product = Info.Product;
                    temp.Product.PricePerUnit = Info.ProductPrice;
                    temp.Info = Info;
                    try
                    {
                        TransportationLoss x = SelectedBill.TransportationLosses.Where(o => o.ProductId == temp.Product.ProductId).SingleOrDefault();
                        temp.Loss = (x.LossQuantity * x.UnitPrice) * (1 - (Info.ComissionPercentage / 100.0));
                    }
                    catch
                    {
                        temp.Loss = 0;
                    }
                    
                    temp.TotalQuantity = 0;
                    temp.TotalPrice = 0;
                    temp.NetPrice = 0;
                    
                    BindBillInfo.Add(temp);
                }
                
                TotalAmount = BindBillInfo.Sum(o => o.NetPrice);
                totalAmountWithoutCom = BindBillInfo.Sum(o => o.TotalPrice);
                
                flag++;
                if (flag == 2)
                {
                    ShowUnpaidBills();
                    Enabled = true;
                }
            };

            
        }

        #endregion

        #region Properties

        private bool _enabled;
        public bool Enabled
        {
            get { return _enabled; }
            set { if (value != _enabled)_enabled = value; RaisePropertyChanged("Enabled"); }
        }

        private string _Message;
        public string Message
        {
            get { return _Message; }
            set { if (value != _Message)_Message = value; RaisePropertyChanged("Message"); }
        }

        public const string SelectedBillPropertyName = "SelectedBill";
        private Bill _selectedBill = StaticMessaging.BillMessage;
        public Bill SelectedBill
        {
            get { return _selectedBill; }
            set
            {
                if (_selectedBill == value) { return; }

                _selectedBill = value;
                RaisePropertyChanged(SelectedBillPropertyName);
            }
        }

        public const string BindBillInfoPropertyName = "BindBillInfo";
        private ObservableCollection<BillRowModel> _bindBillInfo = new ObservableCollection<BillRowModel>();
        public ObservableCollection<BillRowModel> BindBillInfo
        {
            get { return _bindBillInfo; }
            set
            {
                if (_bindBillInfo == value) { return; }

                _bindBillInfo = value;
                RaisePropertyChanged(BindBillInfoPropertyName);
            }
        }

        public const string TotalAmountPropertyName = "TotalAmount";
        private double _totalAmount = 0;
        public double TotalAmount
        {
            get { return _totalAmount; }
            set
            {
                if (_totalAmount == value) { return; }

                _totalAmount = value;
                RaisePropertyChanged(TotalAmountPropertyName);
            }
        }
        public ICommand Print { get; set; }
        #endregion

    }
}
