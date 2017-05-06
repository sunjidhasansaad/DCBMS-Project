using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCBMSWebApp.Models
{
    public class Bill
    {
        public int Id { get; set; }
        public string BillNo { get; set; }
        public DateTime Date { get; set; }
        public double TotalAmount { get; set; }
        public double PaidAmount { get; set; }
        public double DueAmount { get; set; }

    }
}