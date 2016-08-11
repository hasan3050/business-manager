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
    public static class Messages
    {
        public static Indent indent;

        private static String _selectiontype="N";
        public static String SelectionType
        {
            get { return _selectiontype; }
            set
            {
                if (_selectiontype == value) { return; }
                _selectiontype = value;
            }
        }
    }
}
