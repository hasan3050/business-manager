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
using TrialBusinessManager.Models;

namespace TrialBusinessManager.UserControls
{
    public partial class PrintIndentUserControl : UserControl
    {
        ObservableCollection<PrintIndentModel> Infos = new ObservableCollection<PrintIndentModel>();
        AgroDomainContext _context = new AgroDomainContext();
        Indent Indent = new Indent();
        double total = 0;


        public PrintIndentUserControl(AgroDomainContext Context,Indent MyIndent)
        {
            InitializeComponent();
            Indent = MyIndent;
            _context = Context;
            PopulateModelCollection();
            SetBindings();
            CalculateTotal();
        }

        void PopulateModelCollection()
        {
            for (int i = 0; i < Indent.IndentProductInfoes.Count; i++)
            {
                PrintIndentModel Info = new PrintIndentModel();
                Info.Product = _context.Products.Where(c => c.ProductId.Equals(Indent.IndentProductInfoes.ElementAt(i).ProductId)).Single();
                Info.Info = Indent.IndentProductInfoes.ElementAt(i);
                Infos.Add(Info);
            }
        }

        void SetBindings()
        {
            DealerControl.DataContext = Indent.Dealer;
            InfoPanel.DataContext = Indent;
            indentInfoDataGrid.ItemsSource = Infos;
        }

        void CalculateTotal()
        {
            for (int i = 0; i < Infos.Count; i++)
            {
                total += Infos[i].NetPrice;
            }

            TotalTxt.Text = total.ToString();
        }
    }
}
