using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using DCBMSWebApp.DAL.Models;

namespace DCBMSWebApp.DAL.Gateway
{
    public class ReportGateway
    {
        private string connectionString =
            WebConfigurationManager.ConnectionStrings["DCBMSConnectionString"].ConnectionString;

        public List<TestWiseReportVM> GetTestWiseReport(string dateFrom, string dateTo)
        {
            List<TestWiseReportVM> testWiseReport = new List<TestWiseReportVM>();
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"SELECT Tests.Name TestName,count(R.TestId) Totaltest,sum(R.TotalAmount) TotalAmount
FROM Tests
LEFT OUTER JOIN V_Report AS R on R.TestId = Tests.Id
AND R.Date BETWEEN  '" + dateFrom + "' AND '" + dateTo + "'GROUP BY Tests.Name;";

            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                TestWiseReportVM test = new TestWiseReportVM();


                test.TestName = reader["TestName"].ToString();
                test.TotalTest = (int)reader["Totaltest"];
                if (reader["TotalAmount"] == DBNull.Value)
                {
                    test.TotalAmount = 0;
                }
                else
                {
                    test.TotalAmount = (decimal)reader["TotalAmount"];
                }


                testWiseReport.Add(test);
            }
            reader.Close();
            connection.Close();
            return testWiseReport;
        }

    }
}