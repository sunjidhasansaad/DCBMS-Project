using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DCBMSWebApp.DAL.Gateway;
using DCBMSWebApp.DAL.Models;

namespace DCBMSWebApp.BLL
{
    public class ReportManager
    {
        ReportGateway _reportGateway = new ReportGateway();

        public List<TestWiseReportVM> GetTestWiseReport(string dateFrom, string dateTo)
        {
            return _reportGateway.GetTestWiseReport(dateFrom, dateTo);
        }

        public List<UnpaidBillReportVM> UnpaidBillReport(string dateFrom, string dateTo)
        {
            return _reportGateway.UnpaidBillReport(dateFrom, dateTo);
        }
    }
}