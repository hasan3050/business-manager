using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using TrialBusinessManager.Web;
using System.Collections.ObjectModel;
using System.ServiceModel.DomainServices.Client;

namespace TrialBusinessManager.Views.SIC
{
    public partial class ApproveTransferIndentDetailWindow : ChildWindow
    {
        AgroDomainContext _context = new AgroDomainContext();
        TransferIndent indent = new TransferIndent();
        public ObservableCollection<TransferIndentDetail> Details = new ObservableCollection<TransferIndentDetail>();
        BusyWindow MyWindow = new BusyWindow();
        
        public ApproveTransferIndentDetailWindow(AgroDomainContext Context,TransferIndent SelectedIndent)
        {
            InitializeComponent();
            _context = Context;
            indent = SelectedIndent;
            InitializeUI();
            DetailDataGrid.ItemsSource = Details;
            
            foreach (var item in _context.TransferIndentDetails)
            {
                if (item.TransferIndentId.Equals(indent.TransferIndentId))
                {
                    Details.Add(item);
                }
            }
        }

        void InitializeUI()
        {
            try
            {
                CodeText.Text = indent.TransferIndentCode;

            }
            catch (Exception ex)
            {
            }
            DateText.Text = indent.DateOfIssue.Date.ToShortDateString();
            InventoryText.Text = indent.Inventory.InventoryName;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

       

        private void Forward_BtnCLick(object sender, RoutedEventArgs e)
        {
            foreach (var item in Details)
            {
                if (item.ApprovedProductQuantity < 0 || item.ApprovedProductQuantity>item.PlacedProductQuantity)
                {
                    MessageBox.Show("you have items with invalid approved quantity!!");
                    return;
                }
            }

            indent.Status = "Forwarded to Dispatch";
            MyWindow.Show();
            _context.SubmitChanges(OnSubmitCompleted,null);
        }

        void OnSubmitCompleted(SubmitOperation so)
        {
            MyWindow.Close();

            if (so.HasError)
            {
             //   MessageBox.Show("Failed!!please try again.");
               // so.MarkErrorAsHandled();
            }
            else
            {
                MessageBox.Show("Successfully forwarded to Dispatch!!");
            }
            DialogResult = true;

            
        }
    }
}

