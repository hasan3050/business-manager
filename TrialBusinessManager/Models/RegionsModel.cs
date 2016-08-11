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
    public class RegionsModel:ViewModelBase
    {
        /// <summary>
        /// The <see cref="InventoryAddress" /> property's name.
        /// </summary>
        public const string InventoryAddressPropertyName = "InventoryAddress";

        private String _inventoryAddress  ;

        /// <summary>
        /// Gets the InventoryAddress property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public String InventoryAddress
        {
            get
            {
                return _inventoryAddress;
            }

            set
            {
                if (_inventoryAddress == value)
                {
                    return;
                }

                var oldValue = _inventoryAddress;
                _inventoryAddress = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(InventoryAddressPropertyName);

            }
        }

        /// <summary>
        /// The <see cref="DO" /> property's name.
        /// </summary>
        public const string DOPropertyName = "DO";

        private String _dO  ;

        /// <summary>
        /// Gets the DO property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public String DO
        {
            get
            {
                return _dO;
            }

            set
            {
                if (_dO == value)
                {
                    return;
                }

                var oldValue = _dO;
                _dO = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(DOPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SIC" /> property's name.
        /// </summary>
        public const string SICPropertyName = "SIC";

        private String _sic  ;

        /// <summary>
        /// Gets the SIC property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public String SIC
        {
            get
            {
                return _sic;
            }

            set
            {
                if (_sic == value)
                {
                    return;
                }

                var oldValue = _sic;
                _sic = value;

                // Update bindings and broadcast change using GalaSoft.MvvmLight.Messenging
                RaisePropertyChanged(SICPropertyName, oldValue, value, true);
            }
        }

        /// <summary>
        /// The <see cref="Region" /> property's name.
        /// </summary>
        public const string RegionPropertyName = "Region";

        private Region _region ;

        /// <summary>
        /// Gets the Region property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public Region Region
        {
            get
            {
                return _region;
            }

            set
            {
                if (_region == value)
                {
                    return;
                }

                var oldValue = _region;
                _region = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(RegionPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Inventory" /> property's name.
        /// </summary>
        public const string InventoryPropertyName = "Inventory";

        private Inventory _inventory;

        /// <summary>
        /// Gets the Inventory property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public Inventory Inventory
        {
            get
            {
                return _inventory;
            }

            set
            {
                if (_inventory == value)
                {
                    return;
                }

                var oldValue = _inventory;
                _inventory = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(InventoryPropertyName);

            }
        }
    }
}
