using System;
using System.Collections.Generic;
using DCBMSWebApp.BLL;
using DCBMSWebApp.DAL.Models;

namespace DCBMSWebApp.UI
{
    public partial class UnpaidBillReportUI : System.Web.UI.Page
    {
        ReportManager _reportManager = new ReportManager();
        private decimal total;
        protected void Page_Load(object sender, EventArgs e)
        {
            validationLabel.Text = "";
        }

        protected void ShowData(List<UnpaidBillReportVM> unpaidBillReport)
        {
            total = 0;

            unpaidBillReportGridView.DataSource = unpaidBillReport;
            unpaidBillReportGridView.DataBind();
            foreach (var unpaidBill in unpaidBillReport)
            {
                total += unpaidBill.BillAmount;
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

                List<UnpaidBillReportVM> unpaidBillReports = _reportManager.UnpaidBillReport(dateFrom, dateTo);

                ShowData(unpaidBillReports);
            }
            else
            {
                validationLabel.Text = "Enter date range!";
            }
        }
    }
}