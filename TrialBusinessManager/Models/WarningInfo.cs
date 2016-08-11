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

namespace TrialBusinessManager.Models
{
    public class WarningInfo : ViewModelBase
    {
        public const string LotIdPropertyName = "Billcode";

        private String billcode;

        /// <summary>
        /// Sets and gets the LotId property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public String Billcode
        {
            get
            {
                return billcode;
            }

            set
            {
                if (billcode == value)
                {
                    return;
                }

                billcode = value;
                RaisePropertyChanged(LotIdPropertyName);
            }
        }

        public const string LotProductPropertyName = "Due";

        private DueInfo due;

        /// <summary>
        /// Sets and gets the LotProduct property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public DueInfo Due
        {
            get
            {
                return due;
            }

            set
            {
                if (due == value)
                {
                    return;
                }

                due = value;
                RaisePropertyChanged("Due");
            }
        }


    }
}
