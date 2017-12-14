using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Microsoft.Reporting.WebForms;
using System.Data.SqlClient;
using System.Data;

namespace Web2
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string x = Request.QueryString["BillNo"];

            int id = Convert.ToInt32(x);

            if (!IsPostBack)
            {
                string conString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
                SqlConnection cn = new SqlConnection();
                cn = new SqlConnection(conString);
                string str1;
                str1 = " Select * From BillClass1 where bid="+id+"";                
                DataSet ds1 = new DataSet();
                SqlCommand cmd1 = new SqlCommand(str1,cn);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds1);                
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\Report1.rdlc";
                ReportDataSource reportDataSource = new ReportDataSource();
                reportDataSource.Name = "DataSet1";
                reportDataSource.Value = ds1.Tables[0];
                ReportViewer1.LocalReport.DataSources.Add(reportDataSource);
            }


        }
    }
}