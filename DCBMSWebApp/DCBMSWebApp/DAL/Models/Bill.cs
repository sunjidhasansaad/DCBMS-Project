using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCBMSWebApp.Models
{
    [Serializable]
    public class Bill
    {
        public int Id { get; set; }
        public string BillNo { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal DueAmount { get; set; }

    }
}