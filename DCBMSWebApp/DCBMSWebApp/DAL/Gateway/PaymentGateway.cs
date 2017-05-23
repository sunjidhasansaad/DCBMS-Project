using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using DCBMSWebApp.DAL.Models;
using DCBMSWebApp.Models;

namespace DCBMSWebApp.DAL.Gateway
{
    public class PaymentGateway
    {
        private string connectionString =
           WebConfigurationManager.ConnectionStrings["DCBMSConnectionString"].ConnectionString;
        public List<Test> GetTestsByBillNo(string billNo)
        {
            List<Test> tests = new List<Test>();
            // ReSharper disable once SuggestVarOrType_BuiltInTypes
            string query = @"SELECT Tests.Name,Tests.Fee from Tests Join TestBill on Tests.Id = TestBill.TestId Where TestBill.BillNo = ('" + billNo + "')";
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();


            while (dataReader.Read())
            {
                Test aTest = new Test();
                aTest.Name = (string)dataReader["Name"];
                aTest.Fee = (decimal)dataReader["Fee"];

                tests.Add(aTest);
            }
            dataReader.Close();
            connection.Close();
            return tests;
        }

        public int PayAmount(Bill abill)
        {
            string query = @"Update Bills SET PaidAmount ='" + abill.PaidAmount + "',DueAmount='" + abill.DueAmount + "' Where BillNo = '" + abill.BillNo + "'";
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            connection.Close();
            return rowAffected;
        }
    }
}