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

namespace TrialBusinessManager.Views.NSM
{
    public partial class TransferIndentVerificationChildWindow : ChildWindow
    {
        AgroDomainContext _context = new AgroDomainContext();
        TransferIndent indent = new TransferIndent();
        public ObservableCollection<TransferIndentDetail> Details = new ObservableCollection<TransferIndentDetail>();
        BusyWindow MyWindow = new BusyWindow();
        public TransferIndentVerificationChildWindow(AgroDomainContext Context, TransferIndent SelectedIndent)
        {
            InitializeComponent();
            _context = Context;
            indent = SelectedIndent;
            InitializeUI();
            DetailDataGrid.ItemsSource = Details;
            _context.Load(_context.GetInventoriesQuery().Where(e => e.InventoryId == indent.IssuedToInventoryId)).Completed += (a, b) =>
            {

                ToInventorytextBox.Text = _context.Inventories.Where(e => e.InventoryId == indent.IssuedToInventoryId).Single().InventoryName;
            };
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
            indent.Status = "Verified By NSM";
            MyWindow.Show();
            _context.SubmitChanges().Completed += (s, args) =>
             {
                 //MessageBox.Show("Submitted..");
                 MyWindow.Close();
                 this.DialogResult = true;
             };
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            indent.Status = "Rejected";
            MyWindow.Show();
            _context.SubmitChanges().Completed += (s, args) =>
            {
                MessageBox.Show("Submitted..");
                MyWindow.Close();
                this.DialogResult = true;
            };
        }
    }
}

