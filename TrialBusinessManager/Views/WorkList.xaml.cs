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
using System.Windows.Navigation;

namespace TrialBusinessManager
{
    public partial class WorkList : Page
    {
        public WorkList()
        {
            InitializeComponent();
            HyperlinkButton button = new HyperlinkButton();
            
            button.Content = 
            button.Style = this.GetStyle("FavoriteMenuStyle");
           // this.row1.Children.Add(button);
            button.Width = 114;
            button.Height = 85;
            button.Margin = new Thickness(10, 0, 0, 0);
            
        }
        private Style GetStyle(string styleName)
        {
            Style style = Application.Current.Resources[styleName] as Style;
            return style;
            
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
    }
}
