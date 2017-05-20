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

        public List<TypeWiseReportVM> GetTypeWiseReport(string dateFrom, string dateTo)
        {
            List<TypeWiseReportVM> typeWiseReport = new List<TypeWiseReportVM>();
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"SELECT A1.Type TypeName,count(A2.TypeId) TotalTest,sum(A2.TotalAmount) TotalAmount
FROM TestTypes AS A1
LEFT OUTER JOIN V_Report AS A2 on A2.TypeId = A1.Id AND A2.Date BETWEEN  '" + dateFrom + "' AND '" + dateTo + "'GROUP BY A1.Type;";

            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                TypeWiseReportVM test = new TypeWiseReportVM();


                test.TypeName = reader["TypeName"].ToString();
                test.TotalNoOfTest = (int)reader["TotalTest"];
                if (reader["TotalAmount"] == DBNull.Value)
                {
                    test.TotalAmount = 0;
                }
                else
                {
                    test.TotalAmount = (decimal)reader["TotalAmount"];
                }


                typeWiseReport.Add(test);
            }
            reader.Close();
            connection.Close();
            return typeWiseReport;
        }
        public List<UnpaidBillReportVM> UnpaidBillReport(string dateFrom, string dateTo)
        {
            List<UnpaidBillReportVM> unpaidBillReport = new List<UnpaidBillReportVM>();
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"SELECT Patients.BillNo,Patients.MobileNo,Patients.Name,Bills.TotalAmount From Patients,Bills  WHERE Patients.BillNo = Bills.BillNo AND Bills.PaidAmount<>Bills.TotalAmount AND Bills.Date  BETWEEN '" + dateFrom + "' AND '" + dateTo + "'ORDER BY Patients.Name;";

            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                UnpaidBillReportVM report = new UnpaidBillReportVM();


                report.BillNo = reader["BillNo"].ToString();
                report.ContactNo = reader["MobileNo"].ToString();
                report.PatientName = reader["Name"].ToString();
                report.BillAmount = (decimal)reader["TotalAmount"];

                unpaidBillReport.Add(report);
            }
            reader.Close();
            connection.Close();
            return unpaidBillReport;
        }
    
    }
}