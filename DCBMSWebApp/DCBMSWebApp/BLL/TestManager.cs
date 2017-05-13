using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DCBMSWebApp.DAL.Gateway;
using DCBMSWebApp.DAL.Models;
using DCBMSWebApp.Models;

namespace DCBMSWebApp.BLL
{
    public class TestManager
    {
        TestGateway _testGateway = new TestGateway();
        public string Save(Test test)
        {
            if (_testGateway.IsTestExist(test))
            {
                return "Test already exists.";
            }
            int rowAffected = _testGateway.Save(test);
            if (rowAffected > 0)
            {
                return "Test saved successfully.";
            }
            return "Failed to save test.";
        }

        public List<Test> GetAll()
        {
            return _testGateway.GetAll();
        }

        public List<TestWithTypeVM> GetAllWithType()
        {
            return _testGateway.GetAllWithType();
        }
    }
}