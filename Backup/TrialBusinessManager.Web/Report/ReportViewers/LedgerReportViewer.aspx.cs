﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TrialBusinessManager.Web.Report.ReportViewers
{
    public partial class LedgerReportViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ViewReportBtn_Click(object sender, EventArgs e)
        {
            LedgerReport.LocalReport.Refresh();
        }

        protected void ToDate_TextChanged(object sender, EventArgs e)
        {

        }
    }
}