using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using DCBMSWebApp.DAL.Models;

namespace DCBMSWebApp.DAL.Gateway
{
    public class TestBillGateway
    {
        private string connectionString =
        WebConfigurationManager.ConnectionStrings["DCBMSConnectionString"].ConnectionString;

        public int Save(TestBillVM testBillVm)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"INSERT INTO TestBill(BillNo,TestId) VALUES('"
                + testBillVm.BillNo + "','" + testBillVm.TestId + "')";
            SqlCommand command = new SqlCommand(query, connection);


            connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            connection.Close();
            return rowAffected;
        }
    }
}