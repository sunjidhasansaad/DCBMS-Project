﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCBMSWebApp.Models
{
    [Serializable]
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string MobileNo { get; set; }
        public string BillNo { get; set; }

    }
}