using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TrialBusinessManager.Web.Report.ReportViewers
{
    public partial class InventoryLogReportViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        

        protected void ToDate_TextChanged(object sender, EventArgs e)
        {

        }

        protected void FromDate_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ViewReportBtn_Click1(object sender, EventArgs e)
        {
            InventoryLogReport.LocalReport.Refresh();
        }
    }
}