using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCBMSWebApp.Models
{
    public class Test
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Fee { get; set; }
        public TestType TestType { get; set; }

    }
}