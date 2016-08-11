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
using TrialBusinessManager.Web;

namespace TrialBusinessManager.Models
{
    public static class InventoryMesseging
    {
        public static long RequisitionID{get; set;}
        public static String RequisitionStatus { get; set; }
        public static Region SentRegion { get; set; }
    }
}
