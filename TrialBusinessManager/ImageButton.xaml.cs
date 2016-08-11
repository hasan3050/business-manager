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

namespace TrialBusinessManager
{
    public partial class ImageButton : UserControl
    {
        //1. Declare the dependency property as static, readonly field in your class.
        public static readonly DependencyProperty ButtonImageSourceProperty = DependencyProperty.Register(
            "ButtonImageSource",                               //Property name
            typeof( ImageSource ),                       //Property type
            typeof( ImageButton ),                       //type of prvider
            new PropertyMetadata( ButtonImageSourceChanged ) );//Callback invoked on property value has changes

        public static readonly DependencyProperty ButtonContentProperty = DependencyProperty.Register(
            "ButtonContent",                               //Property name
            typeof(String),                       //Property type
            typeof(ImageButton),                       //type of prvider
            new PropertyMetadata(ButtonContentChanged));//Callback invoked on property value has changes


        public event RoutedEventHandler Click;
 
        public ImageButton()
        {
            InitializeComponent();
        }
 
        // Create the property and use the SetValue/GetValue methods of the DependencyObject class
        public ImageSource ButtonImageSource
        {
            set
            {
                this.SetValue( ButtonImageSourceProperty, value );
            }
            get
            {
                return ( ImageSource )this.GetValue( ButtonImageSourceProperty );
            }
        }

        public String ButtonContent
        {
            set
            {
                this.SetValue(ButtonContentProperty, value);
            }
            get
            {
                return (String)this.GetValue(ButtonContentProperty);
            }
        }
 
        private static void ButtonImageSourceChanged( object sender, DependencyPropertyChangedEventArgs args )
        {
        
        }

        private static void ButtonContentChanged(object sender, DependencyPropertyChangedEventArgs args)
        {

        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Click != null)
            {
                Click(this, e);
            }
        }
    }
}
