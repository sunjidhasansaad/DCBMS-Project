using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCBMSWebApp.DAL.Models
{
    [Serializable]
    public class TestWithTypeVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Fee { get; set; }
        public string Type { get; set; }
    }
}