using System;
using System.Net;
using System.Linq;
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
using GalaSoft.MvvmLight.Command;
using System.ServiceModel.DomainServices.Client;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace TrialBusinessManager.Models
{
    public class ExpenditureInfoModel:ViewModelBase
    {
        /// <summary>
        /// The <see cref="ExpenditureInfo" /> property's name.
        /// </summary>
        public const string ExpenditureInfoPropertyName = "ExpenditureInfo";

        private ExpenditureInfo expenditureInfo ;

        /// <summary>
        /// Sets and gets the ExpenditureInfo property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ExpenditureInfo ExpenditureInfo
        {
            get
            {
                return expenditureInfo;
            }

            set
            {
                if (expenditureInfo == value)
                {
                    return;
                }

                expenditureInfo = value;
                RaisePropertyChanged(ExpenditureInfoPropertyName);
            }
        }

        ObservableCollection<String> remarks = new ObservableCollection<string>
        {
           "Travelling","Food","Dealer Expense" ,"Others" 
        };
        public ObservableCollection<string> Remarks { get { return remarks; } set { } }




        /// <summary>
        /// The <see cref="SelectedRemark" /> property's name.
        /// </summary>
        public const string SelectedRemarkPropertyName = "SelectedRemark";

        private String selectedRemark ;

        /// <summary>
        /// Sets and gets the SelectedRemark property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public String SelectedRemark
        {
            get
            {
                return selectedRemark;
            }

            set
            {
                if (selectedRemark == value)
                {
                    return;
                }

                selectedRemark = value;
                RaisePropertyChanged(SelectedRemarkPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Name" /> property's name.
        /// </summary>
        public const string NamePropertyName = "Name";

        private String name ;

        /// <summary>
        /// Sets and gets the Name property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public String Name
        {
            get
            {
                return name;
            }

            set
            {
                if (name == value)
                {
                    return;
                }

                name = value;
                RaisePropertyChanged(NamePropertyName);
            }
        }

    }
}
