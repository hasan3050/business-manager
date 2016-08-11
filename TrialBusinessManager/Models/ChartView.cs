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

namespace TrialBusinessManager.Models
{
    public class Chartview
    {
        public String Status { get; set; }
        public Double Amount { get; set; }

        public Chartview(String status, Double amount)
        {
            Status = status;
            Amount = amount;

        }

    }
}
