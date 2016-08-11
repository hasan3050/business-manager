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
using TrialBusinessManager.Web;
using System.Windows.Printing;
using TrialBusinessManager.UserControls;

namespace TrialBusinessManager.Models
{
    public static class PrintFactory
    {
        static int pageSize = 12;
        public static void PrintIndent(AgroDomainContext _context,Indent MyIndent)
        {
            PrintIndentUserControl Printable = new PrintIndentUserControl(_context,MyIndent);
            PrintDocument DocToPrint = new PrintDocument();

            DocToPrint.PrintPage += (s, e) =>
            {
                Printable.Width = e.PrintableArea.Width;
                e.PageVisual = Printable;
            };

            DocToPrint.Print("Indent");
        }

        public static void PrintBill(AgroDomainContext _context,Bill MyBill)
        {
            int pageNum = 1;
           
            int totalRow = MyBill.BillProductInfoes.Count;
            int totalPageNum = (int)Math.Ceiling(totalRow*1.0 / pageSize);
            
            if (totalPageNum == 1)
                pageNum = 3;
            
            PrintDocument DocToPrint = new PrintDocument();
            

            DocToPrint.PrintPage += (s, e) =>
            {
                PrintBillUserControl Printable = new PrintBillUserControl(_context, MyBill);
              //  Printable.addData(pageNum, pageSize);
                
               // if(pageNum>2)
                Printable.Width = e.PrintableArea.Width;

               
                
                e.PageVisual = Printable;
                        

                
            };

            DocToPrint.Print("MyBill");
        }

        public static void PrintRequisition(AgroDomainContext _context,Requisition MyRequisition)
        {
            PrintRequisitionUserControl Printable = new PrintRequisitionUserControl(_context, MyRequisition);
            PrintDocument DocToPrint = new PrintDocument();

            DocToPrint.PrintPage += (s, e) =>
            {
                Printable.Width = e.PrintableArea.Width;
                e.PageVisual = Printable;
            };

            DocToPrint.Print("Requisition Form");
        }

        public static void PrintProductMRR(AgroDomainContext _context,MRR MyMRR)
        {
            PrintMRRProductUserControl Printable = new PrintMRRProductUserControl(_context,MyMRR);
            PrintDocument DocToPrint = new PrintDocument();

            DocToPrint.PrintPage += (s, e) =>
            {
                Printable.Width = e.PrintableArea.Width;
                e.PageVisual = Printable;
            };

            DocToPrint.Print("MRR Form");
        }

        public static void PrintPackageMRR(AgroDomainContext _context, MRR MyMRR)
        {
            PrintMRRPackageUserControl Printable = new PrintMRRPackageUserControl(_context, MyMRR);
            PrintDocument DocToPrint = new PrintDocument();

            DocToPrint.PrintPage += (s, e) =>
            {
                Printable.Width = e.PrintableArea.Width;
                e.PageVisual = Printable;
            };

            DocToPrint.Print("MRR Form");
        }

        public static void PrintPLR(AgroDomainContext _context, PLR MyPLR)
        {
            PrintPLRUserControl Printable = new PrintPLRUserControl(_context, MyPLR);
            PrintDocument DocToPrint = new PrintDocument();

            DocToPrint.PrintPage += (s, e) =>
            {
                Printable.Width = e.PrintableArea.Width;
                e.PageVisual = Printable;
            };

            DocToPrint.Print("PLR Form");
        
        }

        public static void PrintSalesReturn(AgroDomainContext _context, SalesReturn MySalesReturn)
        {
            PrintSalesReturnUserControl Printable = new PrintSalesReturnUserControl(_context, MySalesReturn);
            PrintDocument DocToPrint = new PrintDocument();

            DocToPrint.PrintPage += (s, e) =>
            {
                Printable.Width = e.PrintableArea.Width;
                e.PageVisual = Printable;
            };

            DocToPrint.Print("PLR Form");
        
        }
        public static void PrintExpenditure(AgroDomainContext _context, Expenditure MyExpenditure)
        {
            PrintExpenditureUserControl Printable = new PrintExpenditureUserControl(_context, MyExpenditure);
            PrintDocument DocToPrint = new PrintDocument();

            DocToPrint.PrintPage += (s, e) =>
            {
                Printable.Width = e.PrintableArea.Width;
                e.PageVisual = Printable;
            };

            DocToPrint.Print("PLR Form");

        }
    }
}
