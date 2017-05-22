using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using DCBMSWebApp.BLL;
using DCBMSWebApp.DAL.Models;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

namespace DCBMSWebApp.UI
{
    public partial class TypeWiseReportUI : System.Web.UI.Page
    {
        ReportManager _reportManager = new ReportManager();
        private decimal total;
        protected void Page_Load(object sender, EventArgs e)
        {
            validationLabel.Text = "";
        }

        protected void ShowData(List<TypeWiseReportVM> typeWiseReport)
        {
            total = 0;

            typeWiseReportGridView.DataSource = typeWiseReport;
            typeWiseReportGridView.DataBind();
            foreach (var test in typeWiseReport)
            {
                total += test.TotalAmount;
            }
            totalTextBox.Text = total.ToString();
        }


        protected void showButton_OnClick(object sender, EventArgs e)
        {
            total = 0;
            if (!string.IsNullOrEmpty(fromDateTextBox.Text) && !string.IsNullOrEmpty(toDateTextBox.Text))
            {
                string dateFrom = fromDateTextBox.Text;
                string dateTo = toDateTextBox.Text;

                List<TypeWiseReportVM> typeWiseReports = _reportManager.GetTypeWiseReport(dateFrom, dateTo);

                ShowData(typeWiseReports);
            }
            else
            {
                validationLabel.Text = "Enter date range!";
            }
        }

        protected void pdfButton_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(fromDateTextBox.Text) && !string.IsNullOrEmpty(toDateTextBox.Text))
            {
                string dateFrom = fromDateTextBox.Text;
                string dateTo = toDateTextBox.Text;

                GeneratePdf(dateFrom, dateTo);
            }
            else
            {
                validationLabel.Text = "Enter date range!";
            }
        }

        public void GeneratePdf(string dateFrom, string dateTo)
        {
            decimal totalAmount = 0;

            List<TypeWiseReportVM> typeWiseReports = _reportManager.GetTypeWiseReport(dateFrom, dateTo);

            string from = dateFrom;
            string to = dateTo;

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[4]
            {
                new DataColumn("Sl No",typeof(int)),
                new DataColumn("Test Type Name",typeof(string)),
                new DataColumn("Total No. of Test",typeof(int)),
                new DataColumn("Total Amount",typeof(decimal))
            });

            int sl = 0;
            foreach (var test in typeWiseReports)
            {

                sl = sl + 1;
                totalAmount += test.TotalAmount;
                dt.Rows.Add(sl, test.TypeName, test.TotalNoOfTest, test.TotalAmount);

            }
            StringBuilder sbr = new StringBuilder();
            StringWriter sw = new StringWriter(sbr);
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            StringBuilder sb = new StringBuilder();
            sb.Append("<table width = '100%' cellspacing ='0' cellpading = '2'>");
            sb.Append("<tr><td align = 'center'><b>Test Wise Report</b></td></tr>");

            sb.Append("<tr><td><b>From : </b>");
            sb.Append(from);
            sb.Append("<tr><td><b>Date : </b>");
            sb.Append(to);
            sb.Append("</td></tr>");
            sb.Append("</table>");
            sb.Append("<br/>");


            sb.Append("<table border = '1'>");
            sb.Append("<tr>");

            foreach (DataColumn column in dt.Columns)
            {
                sb.Append("<th>");
                sb.Append(column.ColumnName);
                sb.Append("</th>");

            }
            sb.Append("</tr>");
            foreach (DataRow row in dt.Rows)
            {
                sb.Append("<tr>");
                foreach (DataColumn column in dt.Columns)
                {
                    sb.Append("<td>");
                    sb.Append(row[column]);
                    sb.Append("</td>");
                }
                sb.Append("</tr>");
            }
            sb.Append("<tr><td align = 'right' colspan = '");
            sb.Append(dt.Columns.Count - 1);
            sb.Append("'><b>Total : </b></td>");
            sb.Append("<td>");
            sb.Append(totalAmount);
            sb.Append("</td>");
            sb.Append("</tr></table>");

            StringReader sr = new StringReader(sb.ToString());
            Document pdfDoc = new Document(PageSize.A4, 40f, 40f, 40f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=TypeWiseReport.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Write(pdfDoc);
            Response.End();


        }
    }
}