using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DCBMSWebApp.DAL.Gateway;
using DCBMSWebApp.DAL.Models;

namespace DCBMSWebApp.BLL
{
    public class TestBillManager
    {
      
        TestBillGateway _testBillGateway= new TestBillGateway();
        public void Save(TestBillVM testBillVm)
        {
            int rowAffected =  _testBillGateway.Save(testBillVm);

        }
    }
}