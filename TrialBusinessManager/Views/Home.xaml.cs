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
using System.Windows.Threading;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;

namespace TrialBusinessManager.Views
{
    public partial class Home : Page
    {
        DispatcherTimer timer;
        private ObservableCollection<string> _images;
        private int _currentIndex = 0;
        public Home()
        {
            InitializeComponent();
            InitializeImages();
            StartTimer();
            
        }

        private void InitializeImages()
        {
            _images = new ObservableCollection<string>()
             {
                 "slide_1.png",
                  "slide_2.png",
                  "slide_3.png"
             };

            SetImageSource(_images[_currentIndex]);
        }

        private void SetImageSource(string imageName)
        {
            string name = "/TrialBusinessManager;component/Assets/Resources/" + imageName;
            BitmapImage bmi = new BitmapImage(new Uri(name, UriKind.Relative));
            theImage.Source = bmi;
        }

        public void StartTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 3);
            timer.Tick += new EventHandler(timer_Tick);
            ScrollOut.Completed += new EventHandler(ScrollOut_Completed);
            timer.Start();
        }

        void ScrollOut_Completed(object sender, EventArgs e)
        {
            SetImageSource(_images[_currentIndex]);
            FadeInAnimation.Begin();
            ScrollIn.Begin();
        }

        

        void timer_Tick(object sender, EventArgs e)
        {
            _currentIndex++;

            if (_currentIndex == _images.Count)
            {
                _currentIndex = 0;
            }

            ScrollOut.Begin();
            
        }
        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //the timer is not running,we must start it.
            if (!this.timer.IsEnabled)
            {
                this.timer.Start();
            }
        }
        //when we are navigating out of this page.
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            //the timer is running,we must stop it.
            if (this.timer.IsEnabled)
            {
                this.timer.Stop();
            }
        }

    }
}
