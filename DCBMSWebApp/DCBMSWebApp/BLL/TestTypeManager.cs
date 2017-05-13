using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DCBMSWebApp.DAL.Gateway;
using DCBMSWebApp.DAL.Models;
using DCBMSWebApp.Models;

namespace DCBMSWebApp.BLL
{
    public class TestTypeManager
    {
        TestTypeGateway _testTypeGateway = new TestTypeGateway();
        public string Save(TestType testType)
        {
            if (_testTypeGateway.IsTypeExist(testType))
            {
                return "Type already exists.";
            }
            int rowAffected = _testTypeGateway.Save(testType);
            if (rowAffected > 0)
            {
                return "Type saved successfully.";
            }
            return "Failed to save type.";
        }

        public List<TestType> GetAll()
        {
            return _testTypeGateway.GetAll();
        }

    }
}