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
    public static class UserInfo
    {
        public static string Username { get; set; }
        public static long EmployeeID { get; set; }
        public static string Designation { get; set; }
        public static string RegionName { get; set; }
        public static Region Region{ get; set; }
        public static Inventory Inventory { get; set; }
    }
}
