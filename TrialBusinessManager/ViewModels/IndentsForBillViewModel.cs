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
using System.ServiceModel.DomainServices.Client;
using TrialBusinessManager.Views;
using TrialBusinessManager.Models;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Linq;

namespace TrialBusinessManager.ViewModels
{
    public class IndentsForBillViewModel:ViewModelBase
    {
        AgroDomainContext _context = new AgroDomainContext();
        BusyWindow window = new BusyWindow();
        GenerateBillChildWindow BillPlacementWindow;

        public IndentsForBillViewModel()
        {
            RegisterCommands();
            window.Show();
            _context.Load(_context.GetIndentsQuery().Where(c => (c.ForwardedToId.Equals(UserInfo.Inventory.StoreInChargeId)) && c.IndentStatus.Equals("Forwarded to DO"))).Completed += new EventHandler(IndentsForBillViewModel_Completed);        
        }

        void OnIndentReceived(Indent BillGeneratedIndent)
        {
            Indent MyIndent = new Indent();
            MyIndent = _context.Indents.Where(c => c.IndentId.Equals(BillGeneratedIndent.IndentId)).First();
            MyIndent.IndentStatus = "Dispatched";
            BillPlacementWindow.Close();
        }

        void RegisterCommands()
        {
            GenerateBillCommand = new RelayCommand(generateBill);
            DetailsCommand = new RelayCommand(ViewDetails);
        }

        void IndentsForBillViewModel_Completed(object sender, EventArgs e)
        {
            window.Close();
        }

        void ViewDetails()
        {
            if (SelectedIndent == null)
                MessageBox.Show("Please select an indent");
        }

        void generateBill()
        {
            if (SelectedIndent == null)
            {
                MessageBox.Show("Please select an indent");
                return;
            }

            if (SelectedIndent.IndentStatus != "Forwarded to DO")
            {
                MessageBox.Show("this indent is already dispatched!!");
                return;
            }

            
            Messages.indent = new Indent();
            Messages.indent = SelectedIndent;
            
            try
            {
                GenerateBillViewModel x = new GenerateBillViewModel();
                x = (GenerateBillViewModel)BillPlacementWindow.DataContext;
                Messenger.Default.Unregister(x);
            }
            catch (Exception ex)
            {

            }


            StaticMessaging.BillIndentMessage = SelectedIndent;
            BillPlacementWindow=new GenerateBillChildWindow();
            BillPlacementWindow.Closed += new EventHandler(BillPlacementWindow_Closed);
            BillPlacementWindow.Show();
        }

        void BillPlacementWindow_Closed(object sender, EventArgs e)
        {
            if (StaticMessaging.BillGenerated == true)
            {
                _context.Indents.Remove(_context.Indents.Where(c => c.IndentId.Equals(SelectedIndent.IndentId)).Single());
                StaticMessaging.BillGenerated = false;
            }
        }

        public EntitySet<Indent> indents { get { return _context.Indents; } }
        
        public ICommand DetailsCommand { get; set; }
        public ICommand GenerateBillCommand { get; set; }

        /// <summary>
        /// The <see cref="SelectedIndent" /> property's name.
        /// </summary>
        public const string SelectedIndentPropertyName = "SelectedIndent";

        private Indent _selectedIndent  ;

        /// <summary>
        /// Sets and gets the SelectedIndent property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Indent SelectedIndent
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

                _selectedIndent = value;
                RaisePropertyChanged(SelectedIndentPropertyName);
            }
        }
    }
}
