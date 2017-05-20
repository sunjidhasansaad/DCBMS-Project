using System;
using System.Collections.Generic;
using DCBMSWebApp.BLL;
using DCBMSWebApp.DAL.Models;

namespace DCBMSWebApp.UI
{
    public partial class TestWiseReportUI : System.Web.UI.Page
    {
        ReportManager _reportManager = new ReportManager();
        private decimal total;
        protected void Page_Load(object sender, EventArgs e)
        {
            validationLabel.Text = "";
        }

        protected void ShowData(List<TestWiseReportVM> testWiseReport)
        {
            total = 0;

            testWiseReportGridView.DataSource = testWiseReport;
            testWiseReportGridView.DataBind();
            foreach (var test in testWiseReport)
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

                List<TestWiseReportVM> testWiseReports = _reportManager.GetTestWiseReport(dateFrom, dateTo);

                ShowData(testWiseReports);
            }
            else
            {
                validationLabel.Text = "Enter date range!";
            }
        }
    }
}