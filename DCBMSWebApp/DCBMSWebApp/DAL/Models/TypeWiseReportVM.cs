using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCBMSWebApp.DAL.Models
{
    public class TypeWiseReportVM
    {
        public string TypeName { get; set; }
        public int TotalNoOfTest { get; set; }
        public decimal TotalAmount { get; set; }
    }
}