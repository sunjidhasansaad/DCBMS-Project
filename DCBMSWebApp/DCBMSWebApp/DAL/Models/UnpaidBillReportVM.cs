using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCBMSWebApp.DAL.Models
{
    public class UnpaidBillReportVM
    {
        public string BillNo { get; set; }
        public string ContactNo { get; set; }
        public string PatientName { get; set; }
        public decimal BillAmount { get; set; }

    }
}