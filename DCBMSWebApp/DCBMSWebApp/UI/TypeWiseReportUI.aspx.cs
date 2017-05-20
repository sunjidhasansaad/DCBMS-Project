using System;
using System.Collections.Generic;
using DCBMSWebApp.BLL;
using DCBMSWebApp.DAL.Models;

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
    }
}