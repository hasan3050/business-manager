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
using TrialBusinessManager.Views;
using TrialBusinessManager.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel.DataAnnotations;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using TrialBusinessManager.Views.SIC;
using TrialBusinessManager.Views.NSM;
using TrialBusinessManager.Views.DO;


namespace TrialBusinessManager.ViewModels.SIC
{
    public class ProductTransferNotificationsViewModel:ViewModelBase
    {
        AgroDomainContext _context=new AgroDomainContext();
        TransferIndent Indent = new TransferIndent();
        BusyWindow MyWindow = new BusyWindow();
        int flag = 0;

        public ProductTransferNotificationsViewModel()
        {
            Initialize();
        }

        void Initialize()
        {
            RegisterForCommands();
            LoadData();
        }

        void LoadData()
        {
            MyWindow.Show();
            //
            //
            if (UserInfo.Designation == "Store In Charge")
            {
                var query = _context.GetTransferIndentsQuery().Where(c => c.IssuedToInventoryId.Equals(UserInfo.Inventory.InventoryId) && c.Status.Equals("Verified By NSM"));
                _context.Load(query).Completed += (s, e) =>
                    {
                        flag++;
                        if (flag == 2)
                        {
                            MyWindow.Close();
                        }
                    };

                _context.Load(_context.GetTransferIndentDetailsQuery().Where(c => c.TransferIndent.IssuedToInventoryId.Equals(UserInfo.Inventory.InventoryId) && c.TransferIndent.Status.Equals("Verified By NSM"))).Completed += (s, e) =>
                    {
                        flag++;
                        if (flag == 2)
                        {
                            MyWindow.Close();
                        }
                    };
            }
            else if (UserInfo.Designation == "National Sales Manager")
            {
                var query = _context.GetTransferIndentsQuery().Where(c => c.Status.Equals("Placed By SIC"));
                _context.Load(query).Completed += (s, e) =>
                {
                    flag++;
                    if (flag == 2)
                    {
                        MyWindow.Close();
                    }
                };

                _context.Load(_context.GetTransferIndentDetailsQuery().Where(c =>c.TransferIndent.Status.Equals("Placed By SIC"))).Completed += (s, e) =>
                {
                    flag++;
                    if (flag == 2)
                    {
                        MyWindow.Close();
                    }
                };
            
            }
            else if (UserInfo.Designation == "Dispatch Officer")
            {
                var query = _context.GetTransferIndentsQuery().Where(c => c.IssuedToInventoryId.Equals(UserInfo.Inventory.InventoryId) && c.Status.Equals("Forwarded To Dispatch"));
                _context.Load(query).Completed += (s, e) =>
                {
                    flag++;
                    if (flag == 2)
                    {
                        MyWindow.Close();
                    }
                };

                _context.Load(_context.GetTransferIndentDetailsQuery().Where(c => c.TransferIndent.IssuedToInventoryId.Equals(UserInfo.Inventory.InventoryId) && c.TransferIndent.Status.Equals("Forwarded To Dispatch"))).Completed += (s, e) =>
                {
                    flag++;
                    if (flag == 2)
                    {
                        MyWindow.Close();
                    }
                };
            }
        }

        void RegisterForCommands()
        {
            ApproveTransferCommand = new RelayCommand(ApproveTransfer);
        }

        void ApproveTransfer()
        {
            if(SelectedIndent==null)
            {
                MessageBox.Show("Please select a transfer indent to approve!!");
                return;
            }
            if (UserInfo.Designation == "Store In Charge")
            {
                if (SelectedIndent.Status != "Verified By NSM")
                {
                    MessageBox.Show("This Indent is already approved!!");
                    return;
                }
                else
                {
                    ApproveTransferIndentDetailWindow Detail = new ApproveTransferIndentDetailWindow(_context, SelectedIndent);
                    Detail.Show();
                }
            }

            else if (UserInfo.Designation == "National Sales Manager")
            {
                if (SelectedIndent.Status != "Placed By SIC")
                {
                    MessageBox.Show("This Indent is already approved!!");
                    return;
                }
                else
                {
                    TransferIndentVerificationChildWindow Detail = new TransferIndentVerificationChildWindow(_context, SelectedIndent);
                    Detail.Show();
                }
            
            }

            else if (UserInfo.Designation == "Dispatch Officer")
            {
               // MessageBox.Show(SelectedIndent.Status);
               // if (SelectedIndent.Status == "Forwarded To Dispatch")
                if (SelectedIndent.Status != "Dispatched")
                {
                    GenerateProductTransferChildWindow obj = new GenerateProductTransferChildWindow(SelectedIndent,_context);
                    obj.Show();
                    
                }
               /* else
                {
                    MessageBox.Show("This Indent is already approved!!!");
                    return;
                }*/
            }

        }

        public ICommand ApproveTransferCommand { get; set; }

        public EntitySet<TransferIndent> Indents { get { return _context.TransferIndents;} }

                /// <summary>
        /// The <see cref="SelectedIndent" /> property's name.
        /// </summary>
        public const string SelectedIndentPropertyName = "SelectedIndent";

        private TransferIndent _selectedIndent  ;

        /// <summary>
        /// Gets the SelectedIndent property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public TransferIndent SelectedIndent
        {
            get
            {
                return _selectedIndent;
            }

            set
            {
                if (_selectedIndent == value)
                {
                    return;
                }

                var oldValue = _selectedIndent;
                _selectedIndent = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(SelectedIndentPropertyName);

            }
        }
    }
}
