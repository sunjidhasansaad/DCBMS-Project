using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCBMSWebApp.DAL.Models
{
    [Serializable]
    public class TestBillVM
    {
        public int TestId { get; set; }
        public string BillNo { get; set; }
    }
}