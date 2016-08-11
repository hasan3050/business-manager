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
    public class LotAllocationModel:ViewModelBase
    {
        /// <summary>
        /// The <see cref="Info" /> property's name.
        /// </summary>
        public const string InfoPropertyName = "Info";

        private InventoryProductInfo _info ;

        /// <summary>
        /// Sets and gets the Info property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public InventoryProductInfo Info
        {
            get
            {
                return _info;
            }

            set
            {
                if (_info == value)
                {
                    return;
                }

                _info = value;
                RaisePropertyChanged(InfoPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="AvailablePackets" /> property's name.
        /// </summary>
        public const string AvailablePacktesPropertyName = "AvailablePackets";

        private Double _availablePackets  ;

        /// <summary>
        /// Sets and gets the AvailablePacktes property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Double AvailablePackets
        {
            get
            {
                return _availablePackets;
            }

            set
            {
                if (_availablePackets == value)
                {
                    return;
                }

                _availablePackets = value;
                RaisePropertyChanged(AvailablePacktesPropertyName);
            }
        }
        

        /// <summary>
        /// The <see cref="AllotedPackets" /> property's name.
        /// </summary>
        public const string AllotedQuanityPropertyName = "AllotedPackets";

        private Double _allocatedPackets  ;

        /// <summary>
        /// Sets and gets the AllotedQuanity property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Double AllotedPackets
        {
            get
            {
                return _allocatedPackets;
            }

            set
            {
                if (_allocatedPackets == value)
                {
                    return;
                }

                _allocatedPackets = value;
                RaisePropertyChanged(AllotedQuanityPropertyName);
            }
        }
    }
}
