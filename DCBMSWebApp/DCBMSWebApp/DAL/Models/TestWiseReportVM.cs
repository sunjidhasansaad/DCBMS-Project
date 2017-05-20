using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCBMSWebApp.DAL.Models
{
    public class TestWiseReportVM
    {
        public string TestName { get; set; }
        public int TotalTest { get; set; }
        public decimal TotalAmount { get; set; }
    }
}