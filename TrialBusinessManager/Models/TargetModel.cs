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
    public class TargetModel: ViewModelBase
    {
        

            private SalesOfficerTarget salesOfficerTarget = new SalesOfficerTarget();

            public SalesOfficerTarget SalesOfficerTarget
            {
                get
                {
                    return salesOfficerTarget;
                }

                set
                {
                    if (salesOfficerTarget == value)
                    {
                        return;
                    }

                    salesOfficerTarget = value;
                    RaisePropertyChanged("SalesOfficerTarget");
                }
            }

            private String name;

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
                    RaisePropertyChanged("Name");
                }
            }
        }

    
}
