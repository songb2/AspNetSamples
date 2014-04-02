using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TraceDemo
{
    public partial class TestTrace : System.Web.UI.Page
    {
        // Need to run visual studio as administrator
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDataGrid();
            }
        }

        protected void btnWriteLog_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Trace.TraceError("btnWriteLog_Click method called");
        }

        protected void btnViewLog_Click(object sender, EventArgs e)
        {

            System.Diagnostics.Trace.TraceError("Page_Load method called");
        }

        protected void LogGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            LogGrid.PageIndex = e.NewPageIndex;
            BindDataGrid();
        }

        void BindDataGrid()
        {
            EventLog log = new EventLog();
            log.Log = "Application";
            log.MachineName = ".";
            LogGrid.DataSource = log.Entries;
            LogGrid.DataBind();
        }

        // sort not work, need to do again  
        protected void LogGrid_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable m_DataTable = LogGrid.DataSource as DataTable;

            if (m_DataTable != null)
            {
                DataView m_DataView = new DataView(m_DataTable);
                m_DataView.Sort = e.SortExpression + " " + e.SortDirection;

                LogGrid.DataSource = m_DataView;
                LogGrid.DataBind();
            }
        }
    }
}